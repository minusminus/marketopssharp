using NUnit.Framework;
using Shouldly;
using MarketOps.System.Processor;
using MarketOps.StockData.Types;
using System;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class PricesDataRangeFinderTests
    {
        private static int DataRange = 10;
        private static DateTime EndDate = DateTime.Now.Date;
        private static DateTime StartDate = EndDate.AddDays(-DataRange + 1);

        private StockPricesData _pricesData;

        [SetUp]
        public void SetUp()
        {
            _pricesData = new StockPricesData(DataRange);
            for (int i = 0; i < _pricesData.Length; i++)
            {
                _pricesData.O[i] = DataRange;
                _pricesData.H[i] = DataRange;
                _pricesData.L[i] = DataRange;
                _pricesData.C[i] = DataRange;
                _pricesData.TS[i] = StartDate.AddDays(i);
            }
        }

        private void TestFindInRange(DateTime findFrom, DateTime findTo, int expectedFrom, int expectedTo)
        {
            var (resFrom, resTo) = PricesDataRangeFinder.Find(_pricesData, findFrom, findTo);
            resFrom.ShouldBe(expectedFrom);
            resTo.ShouldBe(expectedTo);
        }

        [Test]
        public void Find_DatesOnRangeBorders__ReturnsBorders()
        {
            TestFindInRange(StartDate, EndDate, 0, _pricesData.Length - 1);
        }

        [Test]
        public void Find_DatesInRange__ReturnsIndexes()
        {
            TestFindInRange(StartDate.AddDays(2), EndDate.AddDays(-2), 2, _pricesData.Length - 1 - 2);
        }

        [Test]
        public void Find_DatesInRangeNotEqualValues__ReturnsIndexes()
        {
            TestFindInRange(StartDate.AddDays(2.5), EndDate.AddDays(-2.5), 3, _pricesData.Length - 1 - 3);
        }

        [Test]
        public void Find_StartBelowRange_EndInRange__StartsFromFirst()
        {
            TestFindInRange(StartDate.AddDays(-2), EndDate.AddDays(-2), 0, _pricesData.Length - 1 - 2);
        }

        [Test]
        public void Find_StartInRange_EndAboveRange__EndsWithLast()
        {
            TestFindInRange(StartDate.AddDays(2), EndDate.AddDays(2), 2, _pricesData.Length - 1);
        }

        [Test]
        public void Find_DatesOutOfRange__ReturnsBorders()
        {
            TestFindInRange(StartDate.AddDays(-2), EndDate.AddDays(2), 0, _pricesData.Length - 1);
        }

        [Test]
        public void Find_SameDatesInRange__ReturnsSameIndex()
        {
            TestFindInRange(StartDate.AddDays(2), StartDate.AddDays(2), 2, 2);
        }

        [Test]
        public void Find_SameDatesBelowRange__Throws()
        {
            Should.Throw<Exception>(() => PricesDataRangeFinder.Find(_pricesData, StartDate.AddDays(-2), StartDate.AddDays(-2)));
        }

        [Test]
        public void Find_SameDatesAboveRange__Throws()
        {
            Should.Throw<Exception>(() => PricesDataRangeFinder.Find(_pricesData, EndDate.AddDays(2), EndDate.AddDays(2)));
        }

        [Test]
        public void Find_EmptyPriceData__Throws()
        {
            Should.Throw<Exception>(() => PricesDataRangeFinder.Find(new StockPricesData(0), StartDate, EndDate));
        }
    }
}
