using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.StockData.Tests
{
    [TestFixture]
    public class StockPricesDataFindByTSTests
    {
        private readonly DateTime TestStartTS = DateTime.Now;
        const int TESTDATALEN = 3;

        private StockPricesData CreateTestObj()
        {
            StockPricesData res = new StockPricesData(3);
            Array.Copy(new float[TESTDATALEN] { 10, 20, 30 }, res.O, TESTDATALEN);
            Array.Copy(new float[TESTDATALEN] { 100, 200, 300 }, res.H, TESTDATALEN);
            Array.Copy(new float[TESTDATALEN] { 1, 2, 3 }, res.L, TESTDATALEN);
            Array.Copy(new float[TESTDATALEN] { 10, 20, 30 }, res.C, TESTDATALEN);
            Array.Copy(new Int64[TESTDATALEN] { 1, 2, 3 }, res.V, TESTDATALEN);
            Array.Copy(new DateTime[TESTDATALEN] { TestStartTS.AddDays(-2), TestStartTS.AddDays(-1), TestStartTS }, res.TS, TESTDATALEN);
            return res;
        }

        [Test]
        public void ValuesOnList()
        {
            StockPricesData d = CreateTestObj();
            d.FindByTS(TestStartTS).ShouldBe(TESTDATALEN - 1, "last");
            d.FindByTS(TestStartTS.AddDays(-1)).ShouldBe(TESTDATALEN - 2, "mid");
            d.FindByTS(TestStartTS.AddDays(-2)).ShouldBe(TESTDATALEN - 3, "first");
        }

        [Test]
        public void ValueInRangeButNotOnList()
        {
            CreateTestObj().FindByTS(TestStartTS.AddDays(-1.5)).ShouldBeLessThan(0);
        }

        [Test]
        public void ValueOutOfRangeLeft()
        {
            CreateTestObj().FindByTS(TestStartTS.AddDays(-100)).ShouldBeLessThan(0);
        }

        [Test]
        public void ValueOutOfRangeRight()
        {
            CreateTestObj().FindByTS(TestStartTS.AddDays(100)).ShouldBeLessThan(0);
        }
    }
}
