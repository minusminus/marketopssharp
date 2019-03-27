using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData;
using MarketOps.StockData.Types;

namespace MarketOps.StockData.Tests
{
    [TestFixture]
    public class StockPricesDataMergerTests
    {
        private readonly StockPricesDataMerger TestObj = new StockPricesDataMerger();

        private readonly DateTime TestStartTS = DateTime.Now;
        const int TESTDATALEN = 3;

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
            StockPricesData res = new StockPricesData(TESTDATALEN);
            Array.Copy(new float[TESTDATALEN] { 10, 20, 30 }, res.O, TESTDATALEN);
            Array.Copy(new float[TESTDATALEN] { 100, 200, 300 }, res.H, TESTDATALEN);
            Array.Copy(new float[TESTDATALEN] { 1, 2, 3 }, res.L, TESTDATALEN);
            Array.Copy(new float[TESTDATALEN] { 10, 20, 30 }, res.C, TESTDATALEN);
            Array.Copy(new Int64[TESTDATALEN] { 1, 2, 3 }, res.V, TESTDATALEN);
            Array.Copy(new DateTime[TESTDATALEN] { TestStartTS.AddDays(dateMove - 2), TestStartTS.AddDays(dateMove - 1), TestStartTS.AddDays(dateMove) }, res.TS, TESTDATALEN);
            return res;
        }

        private void CopyDataToExpected(StockPricesData d1, StockPricesData d2, StockPricesData expected)
        {
            Array.Copy(d1.O, 0, expected.O, 0, TESTDATALEN);
            Array.Copy(d2.O, 0, expected.O, TESTDATALEN, TESTDATALEN);
            Array.Copy(d1.H, 0, expected.H, 0, TESTDATALEN);
            Array.Copy(d2.H, 0, expected.H, TESTDATALEN, TESTDATALEN);
            Array.Copy(d1.L, 0, expected.L, 0, TESTDATALEN);
            Array.Copy(d2.L, 0, expected.L, TESTDATALEN, TESTDATALEN);
            Array.Copy(d1.C, 0, expected.C, 0, TESTDATALEN);
            Array.Copy(d2.C, 0, expected.C, TESTDATALEN, TESTDATALEN);
            Array.Copy(d1.V, 0, expected.V, 0, TESTDATALEN);
            Array.Copy(d2.V, 0, expected.V, TESTDATALEN, TESTDATALEN);
            Array.Copy(d1.TS, 0, expected.TS, 0, TESTDATALEN);
            Array.Copy(d2.TS, 0, expected.TS, TESTDATALEN, TESTDATALEN);
        }

        [Test]
        public void MergeBothEmpty_ReturnsEmpty()
        {
            CheckMergedObject(TestObj.Merge(new StockPricesData(0), new StockPricesData(0)), new StockPricesData(0));
        }

        [Test]
        public void MergeLeftEmpty_ReturnsRight()
        {
            CheckMergedObject(TestObj.Merge(new StockPricesData(0), CreateTestPricesObj()), CreateTestPricesObj());
        }

        [Test]
        public void MergeRightEmpty_ReturnsLeft()
        {
            CheckMergedObject(TestObj.Merge(CreateTestPricesObj(), new StockPricesData(0)), CreateTestPricesObj());
        }

        [Test]
        public void MergeLeftBeforeRight()
        {
            StockPricesData d1 = CreateTestPricesObj(-100);
            StockPricesData d2 = CreateTestPricesObj();
            StockPricesData expected = new StockPricesData(2 * TESTDATALEN);
            CopyDataToExpected(d1, d2, expected);
            CheckMergedObject(TestObj.Merge(d1, d2), expected);
        }

        [Test]
        public void MergeRightBeforeLeft()
        {
            StockPricesData d1 = CreateTestPricesObj();
            StockPricesData d2 = CreateTestPricesObj(-100);
            StockPricesData expected = new StockPricesData(2 * TESTDATALEN);
            CopyDataToExpected(d2, d1, expected);
            CheckMergedObject(TestObj.Merge(d1, d2), expected);
        }

        [Test]
        public void OverlappingDataThrows()
        {
            Should.Throw<Exception>(() => { TestObj.Merge(CreateTestPricesObj(), CreateTestPricesObj()); }, "same objects");
            Should.Throw<Exception>(() => { TestObj.Merge(CreateTestPricesObj(), CreateTestPricesObj(-1)); }, "right -1");
            Should.Throw<Exception>(() => { TestObj.Merge(CreateTestPricesObj(), CreateTestPricesObj(-2)); }, "right -2");
        }
    }
}
