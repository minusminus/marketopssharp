using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.SystemExecutor.Processor;
using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.Tests.SystemExecutor.Mocks;

namespace MarketOps.Tests.SystemExecutor.Processor
{
    [TestFixture]
    public class StocksDataPreloaderTests
    {
        const int BackBufRange = 10;

        private StocksDataPreloader TestObj;

        private IStockDataProvider _dataProvider;
        private ISystemDataLoader _dataLoader;
        private StockStatMock _stat;

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
            _dataProvider = StockDataProviderUtils.CreateSubstitute(DateTime.MinValue);
            _dataLoader = SystemDataLoaderUtils.CreateSubstitute(2 * BackBufRange, BackBufRange, DateTime.Now.Date);
            TestObj = new StocksDataPreloader(_dataProvider, _dataLoader);

            _stat = new StockStatMock("", BackBufRange);
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
