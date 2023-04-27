using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;
using MarketOps.StockData;

namespace MarketOps.Tests.StockData
{
    [TestFixture]
    public class DateTimeOperationsTests
    {
        [TestCase(2021, 05, 11, 16, 10, StockDataRange.Monthly, 0, 2021, 04, 11, 16, 10)]
        [TestCase(2021, 05, 11, 16, 10, StockDataRange.Weekly, 0, 2021, 05, 04, 16, 10)]
        [TestCase(2021, 05, 11, 16, 10, StockDataRange.Daily, 0, 2021, 05, 10, 16, 10)]
        [TestCase(2021, 05, 11, 16, 10, StockDataRange.Intraday, 0, 2021, 05, 11, 16, 10)]
        [TestCase(2021, 05, 11, 16, 10, StockDataRange.Intraday, 10, 2021, 05, 11, 16, 00)]
        [TestCase(2021, 05, 11, 16, 10, StockDataRange.Tick, 0, 2021, 05, 11, 16, 09)]
        public void OneTickBefore__ReturnsCorrectly(int tsY, int tsM, int tsD, int tsH, int tsMM, StockDataRange stockDataRange, int interadayInterval
            , int etsY, int etsM, int etsD, int etsH, int etsMM)
        {
            DateTimeOperations.OneTickBefore(new DateTime(tsY, tsM, tsD, tsH, tsMM, 0), new StockPricesData(0) { Range = stockDataRange, IntradayInterval = interadayInterval })
                .ShouldBe(new DateTime(etsY, etsM, etsD, etsH, etsMM, 0));
        }

        [Test]
        public void OneTickBefore_UndefinedRange__Throws()
        {
            Should.Throw<ArgumentException>(() => DateTimeOperations.OneTickBefore(DateTime.Now, new StockPricesData(0) { Range = StockDataRange.Undefined, IntradayInterval = 0 }));
        }
    }
}
