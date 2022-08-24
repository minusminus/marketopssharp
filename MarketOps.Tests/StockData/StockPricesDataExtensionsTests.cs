using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.Tests.StockData
{
    [TestFixture]
    public class StockPricesDataExtensionsTests
    {
        private readonly DateTime TestStartTS = DateTime.Now;
        private const int TestDataLength = 3;

        private StockPricesData CreateTestObj()
        {
            StockPricesData res = new StockPricesData(3);
            Array.Copy(new float[TestDataLength] { 10, 20, 30 }, res.O, TestDataLength);
            Array.Copy(new float[TestDataLength] { 100, 200, 300 }, res.H, TestDataLength);
            Array.Copy(new float[TestDataLength] { 1, 2, 3 }, res.L, TestDataLength);
            Array.Copy(new float[TestDataLength] { 10, 20, 30 }, res.C, TestDataLength);
            Array.Copy(new long[TestDataLength] { 1, 2, 3 }, res.V, TestDataLength);
            Array.Copy(new DateTime[TestDataLength] { TestStartTS.AddDays(-2), TestStartTS.AddDays(-1), TestStartTS }, res.TS, TestDataLength);
            return res;
        }

        [Test]
        public void FindByTS_ValuesOnList__ReturnsItemIndex()
        {
            StockPricesData d = CreateTestObj();
            d.FindByTS(TestStartTS).ShouldBe(TestDataLength - 1, "last");
            d.FindByTS(TestStartTS.AddDays(-1)).ShouldBe(TestDataLength - 2, "mid");
            d.FindByTS(TestStartTS.AddDays(-2)).ShouldBe(TestDataLength - 3, "first");
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
            d.FindByTSGE(TestStartTS).ShouldBe(TestDataLength - 1, "last");
            d.FindByTSGE(TestStartTS.AddDays(-1)).ShouldBe(TestDataLength - 2, "mid");
            d.FindByTSGE(TestStartTS.AddDays(-2)).ShouldBe(TestDataLength - 3, "first");
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
            d.FindByTSLE(TestStartTS).ShouldBe(TestDataLength - 1, "last");
            d.FindByTSLE(TestStartTS.AddDays(-1)).ShouldBe(TestDataLength - 2, "mid");
            d.FindByTSLE(TestStartTS.AddDays(-2)).ShouldBe(TestDataLength - 3, "first");
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

        [TestCase(2, 3, 1)]
        [TestCase(2, 2, 2)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 1, 2)]
        [TestCase(0, 1, 1)]
        public void MinOfL__ReturnsCorrectly(int startIndex, int length, float expected)
        {
            CreateTestObj().MinOfL(startIndex, length).ShouldBe(expected);
        }

        [TestCase(2, 3, 300)]
        [TestCase(2, 2, 300)]
        [TestCase(1, 2, 200)]
        [TestCase(1, 1, 200)]
        [TestCase(0, 1, 100)]
        public void MaxOfH__ReturnsCorrectly(int startIndex, int length, float expected)
        {
            CreateTestObj().MaxOfH(startIndex, length).ShouldBe(expected);
        }
    }
}
