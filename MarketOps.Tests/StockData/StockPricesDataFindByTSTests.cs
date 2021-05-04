using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.Tests.StockData
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
            Array.Copy(new long[TESTDATALEN] { 1, 2, 3 }, res.V, TESTDATALEN);
            Array.Copy(new DateTime[TESTDATALEN] { TestStartTS.AddDays(-2), TestStartTS.AddDays(-1), TestStartTS }, res.TS, TESTDATALEN);
            return res;
        }

        [Test]
        public void FindByTS_ValuesOnList__ReturnsItemIndex()
        {
            StockPricesData d = CreateTestObj();
            d.FindByTS(TestStartTS).ShouldBe(TESTDATALEN - 1, "last");
            d.FindByTS(TestStartTS.AddDays(-1)).ShouldBe(TESTDATALEN - 2, "mid");
            d.FindByTS(TestStartTS.AddDays(-2)).ShouldBe(TESTDATALEN - 3, "first");
        }

        [Test]
        public void FindByTS_ValueInRangeButNotOnList_ReturnsNegativeValue()
        {
            CreateTestObj().FindByTS(TestStartTS.AddDays(-1.5)).ShouldBeLessThan(0);
        }

        [Test]
        public void FindByTS_ValueOutOfRangeLeft_ReturnsNegativeValue()
        {
            CreateTestObj().FindByTS(TestStartTS.AddDays(-100)).ShouldBeLessThan(0);
        }

        [Test]
        public void FindByTS_ValueOutOfRangeRight_ReturnsNegativeValue()
        {
            CreateTestObj().FindByTS(TestStartTS.AddDays(100)).ShouldBeLessThan(0);
        }

        [Test]
        public void FindByTSGE_ValueOnList__ReturnsItemIndex()
        {
            StockPricesData d = CreateTestObj();
            d.FindByTSGE(TestStartTS).ShouldBe(TESTDATALEN - 1, "last");
            d.FindByTSGE(TestStartTS.AddDays(-1)).ShouldBe(TESTDATALEN - 2, "mid");
            d.FindByTSGE(TestStartTS.AddDays(-2)).ShouldBe(TESTDATALEN - 3, "first");
        }

        [Test]
        public void FindByTSGE_ValueInRangeButNotOnList_ReturnsFirstGE()
        {
            CreateTestObj().FindByTSGE(TestStartTS.AddDays(-1.5)).ShouldBe(1);
        }

        [Test]
        public void FindByTSGE_ValueOutOfRangeLeft_ReturnsFirstIndex()
        {
            CreateTestObj().FindByTSGE(TestStartTS.AddDays(-100)).ShouldBe(0);
        }

        [Test]
        public void FindByTSGE_ValueOutOfRangeRight_ReturnsNegativeValue()
        {
            CreateTestObj().FindByTSGE(TestStartTS.AddDays(100)).ShouldBeLessThan(0);
        }

        [Test]
        public void FindByTSLE_ValueOnList__ReturnsItemIndex()
        {
            StockPricesData d = CreateTestObj();
            d.FindByTSLE(TestStartTS).ShouldBe(TESTDATALEN - 1, "last");
            d.FindByTSLE(TestStartTS.AddDays(-1)).ShouldBe(TESTDATALEN - 2, "mid");
            d.FindByTSLE(TestStartTS.AddDays(-2)).ShouldBe(TESTDATALEN - 3, "first");
        }

        [Test]
        public void FindByTSLE_ValueInRangeButNotOnList_ReturnsFirstLE()
        {
            CreateTestObj().FindByTSLE(TestStartTS.AddDays(-1.5)).ShouldBe(0);
        }

        [Test]
        public void FindByTSLE_ValueOutOfRangeLeft_ReturnsNegativeValue()
        {
            CreateTestObj().FindByTSLE(TestStartTS.AddDays(-100)).ShouldBeLessThan(0);
        }

        [Test]
        public void FindByTSLE_ValueOutOfRangeRight_ReturnsLastIndex()
        {
            CreateTestObj().FindByTSLE(TestStartTS.AddDays(100)).ShouldBe(2);
        }
    }
}
