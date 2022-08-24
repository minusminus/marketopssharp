using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData;
using MarketOps.StockData.Types;

namespace MarketOps.Tests.StockData
{
    [TestFixture]
    public class StockPricesDataMergerTests
    {
        private readonly DateTime TestStartTS = DateTime.Now;
        private const int TestDataLength = 3;

        private void CheckMergedObject(StockPricesData merged, StockPricesData expected)
        {
            merged.Length.ShouldBe(expected.Length);
            merged.O.Length.ShouldBe(expected.O.Length);
            merged.H.Length.ShouldBe(expected.H.Length);
            merged.L.Length.ShouldBe(expected.L.Length);
            merged.C.Length.ShouldBe(expected.C.Length);
            merged.V.Length.ShouldBe(expected.V.Length);
            merged.TS.Length.ShouldBe(expected.TS.Length);
            for (int i = 0; i < merged.O.Length; i++) merged.O[i].ShouldBe(expected.O[i]);
            for (int i = 0; i < merged.H.Length; i++) merged.H[i].ShouldBe(expected.H[i]);
            for (int i = 0; i < merged.L.Length; i++) merged.L[i].ShouldBe(expected.L[i]);
            for (int i = 0; i < merged.C.Length; i++) merged.C[i].ShouldBe(expected.C[i]);
            for (int i = 0; i < merged.V.Length; i++) merged.V[i].ShouldBe(expected.V[i]);
            for (int i = 0; i < merged.TS.Length; i++) merged.TS[i].ShouldBe(expected.TS[i]);
        }

        private StockPricesData CreateTestPricesObj(double dateMove = 0)
        {
            StockPricesData res = new StockPricesData(TestDataLength);
            Array.Copy(new float[TestDataLength] { 10, 20, 30 }, res.O, TestDataLength);
            Array.Copy(new float[TestDataLength] { 100, 200, 300 }, res.H, TestDataLength);
            Array.Copy(new float[TestDataLength] { 1, 2, 3 }, res.L, TestDataLength);
            Array.Copy(new float[TestDataLength] { 10, 20, 30 }, res.C, TestDataLength);
            Array.Copy(new long[TestDataLength] { 1, 2, 3 }, res.V, TestDataLength);
            Array.Copy(new DateTime[TestDataLength] { TestStartTS.AddDays(dateMove - 2), TestStartTS.AddDays(dateMove - 1), TestStartTS.AddDays(dateMove) }, res.TS, TestDataLength);
            return res;
        }

        private void CopyDataToExpected(StockPricesData d1, StockPricesData d2, StockPricesData expected)
        {
            Array.Copy(d1.O, 0, expected.O, 0, TestDataLength);
            Array.Copy(d2.O, 0, expected.O, TestDataLength, TestDataLength);
            Array.Copy(d1.H, 0, expected.H, 0, TestDataLength);
            Array.Copy(d2.H, 0, expected.H, TestDataLength, TestDataLength);
            Array.Copy(d1.L, 0, expected.L, 0, TestDataLength);
            Array.Copy(d2.L, 0, expected.L, TestDataLength, TestDataLength);
            Array.Copy(d1.C, 0, expected.C, 0, TestDataLength);
            Array.Copy(d2.C, 0, expected.C, TestDataLength, TestDataLength);
            Array.Copy(d1.V, 0, expected.V, 0, TestDataLength);
            Array.Copy(d2.V, 0, expected.V, TestDataLength, TestDataLength);
            Array.Copy(d1.TS, 0, expected.TS, 0, TestDataLength);
            Array.Copy(d2.TS, 0, expected.TS, TestDataLength, TestDataLength);
        }

        [Test]
        public void MergeBothEmpty_ReturnsEmpty()
        {
            CheckMergedObject(StockPricesDataMerger.Merge(new StockPricesData(0), new StockPricesData(0)), new StockPricesData(0));
        }

        [Test]
        public void MergeLeftEmpty_ReturnsRight()
        {
            CheckMergedObject(StockPricesDataMerger.Merge(new StockPricesData(0), CreateTestPricesObj()), CreateTestPricesObj());
        }

        [Test]
        public void MergeRightEmpty_ReturnsLeft()
        {
            CheckMergedObject(StockPricesDataMerger.Merge(CreateTestPricesObj(), new StockPricesData(0)), CreateTestPricesObj());
        }

        [Test]
        public void MergeLeftBeforeRight()
        {
            StockPricesData d1 = CreateTestPricesObj(-100);
            StockPricesData d2 = CreateTestPricesObj();
            StockPricesData expected = new StockPricesData(2 * TestDataLength);
            CopyDataToExpected(d1, d2, expected);
            CheckMergedObject(StockPricesDataMerger.Merge(d1, d2), expected);
        }

        [Test]
        public void MergeRightBeforeLeft()
        {
            StockPricesData d1 = CreateTestPricesObj();
            StockPricesData d2 = CreateTestPricesObj(-100);
            StockPricesData expected = new StockPricesData(2 * TestDataLength);
            CopyDataToExpected(d2, d1, expected);
            CheckMergedObject(StockPricesDataMerger.Merge(d1, d2), expected);
        }

        [Test]
        public void OverlappingDataThrows()
        {
            Should.Throw<Exception>(() => { StockPricesDataMerger.Merge(CreateTestPricesObj(), CreateTestPricesObj()); }, "same objects");
            Should.Throw<Exception>(() => { StockPricesDataMerger.Merge(CreateTestPricesObj(), CreateTestPricesObj(-1)); }, "right -1");
            Should.Throw<Exception>(() => { StockPricesDataMerger.Merge(CreateTestPricesObj(), CreateTestPricesObj(-2)); }, "right -2");
        }
    }
}
