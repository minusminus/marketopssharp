using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.DataProvider.Pg;
using MarketOps.StockData.Types;

namespace MarketOps.DataProvider.Pq.Tests
{
    [TestFixture]
    public class PgStockDataProviderTests
    {
        private readonly PgStockDataProvider TestObj = new PgStockDataProvider();

        const int STOCKID_WIG = 288;
        const string STOCKNAME_WIG = "WIG";

        [Test]
        public void GetStockDefinition_NonExistingID__Throws()
        {
            Should.Throw<Exception>(() => TestObj.GetStockDefinition(-1));
        }

        [Test]
        public void GetStockDefinition_ExistingID__GetsData()
        {
            StockDefinition data = TestObj.GetStockDefinition(1);
            data.ID.ShouldBe(1);
            data.Name.ShouldBe("TRITON");
            data.Type.ShouldBe(StockType.Stock);

            data = TestObj.GetStockDefinition(STOCKID_WIG);
            data.ID.ShouldBe(STOCKID_WIG);
            data.Name.ShouldBe(STOCKNAME_WIG);
            data.Type.ShouldBe(StockType.Index);
        }

        [Test]
        public void GetStockDefinition_NonExistingName__Throws()
        {
            Should.Throw<Exception>(() => TestObj.GetStockDefinition("non existing stock name"));
        }

        [Test]
        public void GetStockDefinition_ExistingName__GetsData()
        {
            StockDefinition data = TestObj.GetStockDefinition("TRITON");
            data.ID.ShouldBe(1);
            data.Name.ShouldBe("TRITON");
            data.Type.ShouldBe(StockType.Stock);

            data = TestObj.GetStockDefinition(STOCKNAME_WIG);
            data.ID.ShouldBe(STOCKID_WIG);
            data.Name.ShouldBe(STOCKNAME_WIG);
            data.Type.ShouldBe(StockType.Index);
        }

        [Test]
        public void GetPricesData_TSInRange__GetsData()
        {
            StockPricesData data = TestObj.GetPricesData(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Daily, 0, new DateTime(2017, 1, 1), new DateTime(2018, 1, 1));
            data.Range.ShouldBe(StockDataRange.Daily);
            data.IntrradayInterval.ShouldBe(0);
            data.Length.ShouldBeGreaterThan(0);
            for (int i = 1; i < data.Length; i++)
                data.TS[i].ShouldBeGreaterThan(data.TS[i - 1]);
        }

        [Test]
        public void GetPricesData_TSOutOfRange__GetsEmptyObject()
        {
            StockPricesData data = TestObj.GetPricesData(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Daily, 0, new DateTime(1980, 1, 1), new DateTime(1981, 1, 1));
            data.Range.ShouldBe(StockDataRange.Daily);
            data.IntrradayInterval.ShouldBe(0);
            data.Length.ShouldBe(0);
        }

        [Test]
        public void GetNearestTickGE_TSInRange_ExactTS__GetsTS()
        {
            TestObj.GetNearestTickGE(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Daily, 0, new DateTime(2018, 5, 2)).ShouldBe(new DateTime(2018, 5, 2));
        }

        [Test]
        public void GetNearestTickGE_TSInRange_NotExactTS__GetsNextTS()
        {
            TestObj.GetNearestTickGE(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Daily, 0, new DateTime(2018, 5, 1)).ShouldBe(new DateTime(2018, 5, 2));
        }

        [Test]
        public void GetNearestTickGE_TSOutOfRange__Throws()
        {
            Should.Throw<Exception>(() => TestObj.GetNearestTickGE(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Daily, 0, new DateTime(2118, 5, 2)));
        }

        [Test]
        public void GetNearestTickGETicksBefore_TSInRange_ExactTS__GetsTS()
        {
            TestObj.GetNearestTickGETicksBefore(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Daily, 0, new DateTime(2018, 5, 2), 10).ShouldBe(new DateTime(2018, 4, 17));
        }

        [Test]
        public void GetNearestTickGETicksBefore_TSInRange_NotExactTS__GetsTS()
        {
            TestObj.GetNearestTickGETicksBefore(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Daily, 0, new DateTime(2018, 5, 1), 10).ShouldBe(new DateTime(2018, 4, 17));
        }

        [Test]
        public void GetNearestTickGETicksBefore_TSOutOfRange__Throws()
        {
            Should.Throw<Exception>(() => TestObj.GetNearestTickGETicksBefore(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Daily, 0, new DateTime(2118, 5, 2), 10));
        }

        [Test]
        public void GetNearestTickGETicksBefore_TSInRange_NotEnoughTicks__Throws()
        {
            Should.Throw<Exception>(() => TestObj.GetNearestTickGETicksBefore(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Daily, 0, new DateTime(2018, 5, 2), 1000000));
        }
    }
}
