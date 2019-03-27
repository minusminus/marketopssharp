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

        private void CheckMergedObject(StockPricesData merged, StockPricesData expected)
        {
            merged.Length.ShouldBe(expected.Length);
            merged.O.Length.ShouldBe(expected.O.Length);
            merged.H.Length.ShouldBe(expected.H.Length);
            merged.L.Length.ShouldBe(expected.L.Length);
            merged.C.Length.ShouldBe(expected.C.Length);
            merged.V.Length.ShouldBe(expected.V.Length);
            merged.TS.Length.ShouldBe(expected.TS.Length);
        }

        private StockPricesData CreateTestPricesObj()
        {
            const int DATALEN = 3;
            StockPricesData res = new StockPricesData(DATALEN);
            Array.Copy(new float[DATALEN] { 10, 20, 30 }, res.O, DATALEN);
            Array.Copy(new float[DATALEN] { 100, 200, 300 }, res.H, DATALEN);
            Array.Copy(new float[DATALEN] { 1, 2, 3 }, res.L, DATALEN);
            Array.Copy(new float[DATALEN] { 10, 20, 30 }, res.C, DATALEN);
            Array.Copy(new Int64[DATALEN] { 1, 2, 3 }, res.V, DATALEN);
            Array.Copy(new DateTime[DATALEN] { TestStartTS.AddDays(-2), TestStartTS.AddDays(-1), TestStartTS }, res.TS, DATALEN);
            return res;
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
    }
}
