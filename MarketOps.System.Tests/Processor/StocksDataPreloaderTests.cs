using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.System.Processor;
using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using MarketOps.StockData.Interfaces;
using MarketOps.System.Interfaces;
using MarketOps.System.Tests.Mocks;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class StocksDataPreloaderTests
    {
        const int BackBufRange = 10;

        private StocksDataPreloader TestObj;

        private IStockDataProvider _dataProvider;
        private IDataLoader _dataLoader;
        private StockStatMock _stat;
        private StockPricesData _pricesData;

        private SystemStockDataDefinition Stock1() => new SystemStockDataDefinition()
        {
            stock = new StockDefinition() { Name = "KGHM" },
            dataRange = StockDataRange.Daily,
            stats = new List<StockStat>()
        };
        private SystemStockDataDefinition Stock2() => new SystemStockDataDefinition()
        {
            stock = new StockDefinition() { Name = "PKOBP" },
            dataRange = StockDataRange.Daily,
            stats = new List<StockStat>()
        };

        [SetUp]
        public void SetUp()
        {
            _dataProvider = Substitute.For<IStockDataProvider>();
            _dataLoader = Substitute.For<IDataLoader>();
            TestObj = new StocksDataPreloader(_dataProvider, _dataLoader);

            _pricesData = new StockPricesData(2 * BackBufRange);
            for(int i=0; i<_pricesData.Length; i++)
            {
                _pricesData.O[i] = BackBufRange;
                _pricesData.H[i] = BackBufRange;
                _pricesData.L[i] = BackBufRange;
                _pricesData.C[i] = BackBufRange;
                _pricesData.TS[i] = DateTime.Now.Date;
            }
            _stat = new StockStatMock("", BackBufRange);

            _dataProvider.GetNearestTickGETicksBefore(default, default, default, default, default).ReturnsForAnyArgs(DateTime.MinValue);
            _dataLoader.Get(default, default, default, default, default).ReturnsForAnyArgs(_pricesData);
        }

        private void TestPreloadDataAndPrecalcStats(List<(SystemStockDataDefinition stock, int max)> testBackBuffer,
            int ExpectedGetNearestTickGETicksBefore, int ExpectedGet, int ExpectedCalculateCallCount)
        {
            TestObj.PreloadDataAndPrecalcStats(DateTime.MinValue, DateTime.MaxValue, testBackBuffer);
            _dataProvider.ReceivedWithAnyArgs(ExpectedGetNearestTickGETicksBefore).GetNearestTickGETicksBefore(default, default, default, default, default);
            _dataLoader.ReceivedWithAnyArgs(ExpectedGet).Get(default, default, default, default, default);
            _stat.CalculateCallCount.ShouldBe(ExpectedCalculateCallCount);
        }

        [Test]
        public void PreloadDataAndPrecalcStats_EmptyData__DoesNothing()
        {
            TestPreloadDataAndPrecalcStats(
                new List<(SystemStockDataDefinition stock, int max)>(),
                0, 0, 0);
        }

        [Test]
        public void PreloadDataAndPrecalcStats_OneStock_NoStats__Preloads()
        {
            SystemStockDataDefinition stock1 = Stock1();

            TestPreloadDataAndPrecalcStats(
                new List<(SystemStockDataDefinition stock, int max)>()
                {
                    (stock1, 0)
                },
                1, 1, 0);
        }

        [Test]
        public void PreloadDataAndPrecalcStats_OneStock_OneStat__PreloadsAndCalcs()
        {
            SystemStockDataDefinition stock1 = Stock1();
            stock1.stats.Add(_stat);

            TestPreloadDataAndPrecalcStats(
                new List<(SystemStockDataDefinition stock, int max)>()
                {
                    (stock1, BackBufRange)
                },
                1, 1, 1);
        }

        [Test]
        public void PreloadDataAndPrecalcStats_OneStock_MultiStat__PreloadsAndCalcs()
        {
            SystemStockDataDefinition stock1 = Stock1();
            stock1.stats.Add(_stat);
            stock1.stats.Add(_stat);
            stock1.stats.Add(_stat);

            TestPreloadDataAndPrecalcStats(
                new List<(SystemStockDataDefinition stock, int max)>()
                {
                    (stock1, BackBufRange)
                },
                1, 1, 3);
        }

        [Test]
        public void PreloadDataAndPrecalcStats_MultiStock_NoStats__Preloads()
        {
            SystemStockDataDefinition stock1 = Stock1();
            SystemStockDataDefinition stock2 = Stock2();

            TestPreloadDataAndPrecalcStats(
                new List<(SystemStockDataDefinition stock, int max)>()
                {
                    (stock1, 0),
                    (stock2, 0)
                },
                2, 2, 0);
        }

        [Test]
        public void PreloadDataAndPrecalcStats_MultiStock_OneStat__PreloadsAndCalcs()
        {
            SystemStockDataDefinition stock1 = Stock1();
            stock1.stats.Add(_stat);
            SystemStockDataDefinition stock2 = Stock2();
            stock2.stats.Add(_stat);

            TestPreloadDataAndPrecalcStats(
                new List<(SystemStockDataDefinition stock, int max)>()
                {
                    (stock1, BackBufRange),
                    (stock2, BackBufRange)
                },
                2, 2, 2);
        }

        [Test]
        public void PreloadDataAndPrecalcStats_MultiStock_MultiStat__PreloadsAndCalcs()
        {
            SystemStockDataDefinition stock1 = Stock1();
            stock1.stats.Add(_stat);
            stock1.stats.Add(_stat);
            stock1.stats.Add(_stat);
            SystemStockDataDefinition stock2 = Stock2();
            stock2.stats.Add(_stat);
            stock2.stats.Add(_stat);

            TestPreloadDataAndPrecalcStats(
                new List<(SystemStockDataDefinition stock, int max)>()
                {
                    (stock1, BackBufRange),
                    (stock2, BackBufRange)
                },
                2, 2, 5);
        }
    }
}
