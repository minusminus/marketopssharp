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

        [Test]
        public void GetStockDefinition_NonExistingID_Throws()
        {
            Should.Throw<Exception>(() => TestObj.GetStockDefinition(-1));
        }

        [Test]
        public void GetStockDefinition_ExistingID_GetsData()
        {
            StockDefinition data = TestObj.GetStockDefinition(1);
            data.ID.ShouldBe(1);
            data.Name.ShouldBe("TRITON");
            data.Type.ShouldBe(StockType.Stock);

            data = TestObj.GetStockDefinition(STOCKID_WIG);
            data.ID.ShouldBe(STOCKID_WIG);
            data.Name.ShouldBe("WIG");
            data.Type.ShouldBe(StockType.Index);
        }

        [Test]
        public void GetPricesData_TSInRange_GetsData()
        {
            StockPricesData data = TestObj.GetPricesData(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Day, 0, new DateTime(2017, 1, 1), new DateTime(2018, 1, 1));
            data.Range.ShouldBe(StockDataRange.Day);
            data.IntrradayInterval.ShouldBe(0);
            data.Length.ShouldBeGreaterThan(0);
            for (int i = 1; i < data.Length; i++)
                data.TS[i].ShouldBeGreaterThan(data.TS[i - 1]);
        }

        [Test]
        public void GetPricesData_TSOutOfRange_GetsEmptyObject()
        {
            StockPricesData data = TestObj.GetPricesData(TestObj.GetStockDefinition(STOCKID_WIG), StockDataRange.Day, 0, new DateTime(1980, 1, 1), new DateTime(1981, 1, 1));
            data.Range.ShouldBe(StockDataRange.Day);
            data.IntrradayInterval.ShouldBe(0);
            data.Length.ShouldBe(0);
        }
    }
}
