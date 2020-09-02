using MarketOps.System.Processor;
using MarketOps.System.Tests.Mocks;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.StockData.Interfaces;
using MarketOps.System.Interfaces;
using System;
using MarketOps.StockData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class SystemProcessorTests
    {
        private readonly DateTime LastDate = DateTime.Now.Date;
        private const int PricesCount = 3;
        private const float InitialCash = 10000;
        private const float StartingPrice = 10;
        private const float PriceRange = 1;

        private static readonly StockDefinition _stock = new StockDefinition() { ID = 1 };
        private static readonly StockStatMock _stockStatMock = new StockStatMock("", 0);

        private IStockDataProvider _dataProvider;
        private IDataLoader _dataLoader;
        private ISystemDataDefinitionProvider _dataDefinitionProvider;
        private ISignalGeneratorOnOpen _signalGeneratorOnOpen;
        private ISignalGeneratorOnClose _signalGeneratorOnClose;
        private ICommission _commission;
        private ISlippage _slippage;
        private IMMPositionCloseCalculator _mmPositionCloseCalculator;

        private SystemProcessor TestObj;

        [SetUp]
        public void SetUp()
        {
            _dataProvider = StockDataProviderUtils.CreateSubstitute(DateTime.MinValue);
            _dataLoader = DataLoaderUtils.CreateSubstituteWithConstantPriceInRange(PricesCount, StartingPrice, PriceRange, LastDate);
            _dataDefinitionProvider = Substitute.For<ISystemDataDefinitionProvider>();
            _signalGeneratorOnOpen = Substitute.For<ISignalGeneratorOnOpen>();
            _signalGeneratorOnClose = Substitute.For<ISignalGeneratorOnClose>();
            _commission = CommissionUtils.CreateSubstitute();
            _slippage = SlippageUtils.CreateSusbstitute();
            _mmPositionCloseCalculator = Substitute.For<IMMPositionCloseCalculator>();

            //TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, _signalGeneratorOnOpen, _signalGeneratorOnClose, _commission, _slippage, _mmPositionCloseCalculator);

            _dataDefinitionProvider.GetDataDefinition().Returns(
                new SystemDataDefinition()
                {
                    stocks = new List<SystemStockDataDefinition>() {
                        new SystemStockDataDefinition()
                        {
                            stock = _stock,
                            dataRange = StockDataRange.Daily,
                            stats = new List<StockStat>() { _stockStatMock }
                        }
                    }
                }
                );
            _stockStatMock.CalculateCallCount = 0;
        }

        private void CheckEmptyEquity(SystemEquity equity)
        {
            equity.Cash.ShouldBe(InitialCash);
            equity.PositionsActive.Count.ShouldBe(0);
            equity.PositionsClosed.Count.ShouldBe(0);
            equity.ClosedPositionsValue.Count.ShouldBe(0);
            equity.Value.Count.ShouldBe(PricesCount);
            equity.Value.All(x => x.Value == InitialCash).ShouldBeTrue();
            _mmPositionCloseCalculator.DidNotReceiveWithAnyArgs().CalculateCloseMode(default, default);
            _stockStatMock.CalculateCallCount.ShouldBe(1);
        }

        private void CheckPositionOpenedEquity(SystemEquity equity, PositionDir expectedDir, float expectedPrice, int expectedPositionTicks)
        {
            equity.Cash.ShouldBe(InitialCash - expectedPrice);
            equity.PositionsActive.Count.ShouldBe(1);
            equity.PositionsActive[0].Direction.ShouldBe(expectedDir);
            equity.PositionsActive[0].Open.ShouldBe(expectedPrice);
            equity.PositionsActive[0].TicksActive.ShouldBe(expectedPositionTicks);
            equity.PositionsClosed.Count.ShouldBe(0);
            equity.ClosedPositionsValue.Count.ShouldBe(0);
            equity.Value.Count.ShouldBe(PricesCount);
            equity.Value.All(x => x.Value == InitialCash).ShouldBeTrue();
            _mmPositionCloseCalculator.ReceivedWithAnyArgs(expectedPositionTicks).CalculateCloseMode(default, default);
            _stockStatMock.CalculateCallCount.ShouldBe(1);
        }

        [Test, Combinatorial]
        public void Process_NoSignals__EmptyEquity([Values(false, true)] bool withGeneratorOnOpen, [Values(false, true)] bool withGeneratorOnClose)
        {
            _signalGeneratorOnOpen.GenerateOnOpen(default, default).ReturnsForAnyArgs(new List<Signal>());
            _signalGeneratorOnClose.GenerateOnClose(default, default).ReturnsForAnyArgs(new List<Signal>());

            TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, (withGeneratorOnOpen ? _signalGeneratorOnOpen : null), (withGeneratorOnClose ? _signalGeneratorOnClose : null), _commission, _slippage, _mmPositionCloseCalculator);
            CheckEmptyEquity(TestObj.Process(LastDate.AddDays(-PricesCount), LastDate, InitialCash));
        }

        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice, true, StartingPrice, PricesCount)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice - PriceRange - 1, true, StartingPrice, PricesCount)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice + PriceRange + 1, false, 0, 0)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice, true, StartingPrice, PricesCount)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice + PriceRange + 1, true, StartingPrice, PricesCount)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice - PriceRange - 1, false, 0, 0)]
        [TestCase(SignalType.EnterOnOpen, PositionDir.Long, 0, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnOpen, PositionDir.Short, 0, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnClose, PositionDir.Long, 0, true, StartingPrice, PricesCount)]
        [TestCase(SignalType.EnterOnClose, PositionDir.Short, 0, true, StartingPrice, PricesCount)]
        public void Process_SignalOnOpen(SignalType signalType, PositionDir positionDir, float price, 
            bool expectedHit, float expectedPrice, int expectedPositionTicks)
        {
            _signalGeneratorOnOpen.GenerateOnOpen(default, default).ReturnsForAnyArgs(args =>
            {
                if (args.ArgAt<int>(1) == 0)
                    return new List<Signal>() {
                    new Signal()
                    {
                        Stock = _stock,
                        DataRange = StockDataRange.Daily,
                        IntradayInterval = 0,
                        Type = signalType,
                        Direction = positionDir,
                        ReversePosition = false,
                        Price = price,
                        Volume = 1
                    }
                };
                return new List<Signal>();
            });

            TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, _signalGeneratorOnOpen, null, _commission, _slippage, _mmPositionCloseCalculator);
            SystemEquity testEquity = TestObj.Process(LastDate.AddDays(-PricesCount), LastDate, InitialCash);
            if (expectedHit)
                CheckPositionOpenedEquity(testEquity, positionDir, expectedPrice, expectedPositionTicks);
            else
                CheckEmptyEquity(testEquity);
        }

        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice - PriceRange - 1, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice + PriceRange + 1, false, 0, 0)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice + PriceRange + 1, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice - PriceRange - 1, false, 0, 0)]
        [TestCase(SignalType.EnterOnOpen, PositionDir.Long, 0, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnOpen, PositionDir.Short, 0, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnClose, PositionDir.Long, 0, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnClose, PositionDir.Short, 0, true, StartingPrice, PricesCount - 1)]
        public void Process_SignalOnClose(SignalType signalType, PositionDir positionDir, float price,
            bool expectedHit, float expectedPrice, int expectedPositionTicks)
        {
            _signalGeneratorOnClose.GenerateOnClose(default, default).ReturnsForAnyArgs(args =>
            {
                if (args.ArgAt<int>(1) == 0)
                    return new List<Signal>() {
                    new Signal()
                    {
                        Stock = _stock,
                        DataRange = StockDataRange.Daily,
                        IntradayInterval = 0,
                        Type = signalType,
                        Direction = positionDir,
                        ReversePosition = false,
                        Price = price,
                        Volume = 1
                    }
                };
                return new List<Signal>();
            });

            TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, null, _signalGeneratorOnClose, _commission, _slippage, _mmPositionCloseCalculator);
            SystemEquity testEquity = TestObj.Process(LastDate.AddDays(-PricesCount), LastDate, InitialCash);
            if (expectedHit)
                CheckPositionOpenedEquity(testEquity, positionDir, expectedPrice, expectedPositionTicks);
            else
                CheckEmptyEquity(testEquity);
        }

        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice - PriceRange - 1, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice + PriceRange + 1, false, 0, 0)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice + PriceRange + 1, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice - PriceRange - 1, false, 0, 0)]
        [TestCase(SignalType.EnterOnOpen, PositionDir.Long, 0, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnOpen, PositionDir.Short, 0, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnClose, PositionDir.Long, 0, true, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnClose, PositionDir.Short, 0, true, StartingPrice, PricesCount - 1)]
        public void Process_SignalReverse_NoOpenedPosition(SignalType signalType, PositionDir positionDir, float price,
            bool expectedHit, float expectedPrice, int expectedPositionTicks)
        {
            _signalGeneratorOnClose.GenerateOnClose(default, default).ReturnsForAnyArgs(args =>
            {
                if (args.ArgAt<int>(1) == 0)
                    return new List<Signal>() {
                    new Signal()
                    {
                        Stock = _stock,
                        DataRange = StockDataRange.Daily,
                        IntradayInterval = 0,
                        Type = signalType,
                        Direction = positionDir,
                        ReversePosition = true,
                        Price = price,
                        Volume = 1
                    }
                };
                return new List<Signal>();
            });

            TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, null, _signalGeneratorOnClose, _commission, _slippage, _mmPositionCloseCalculator);
            SystemEquity testEquity = TestObj.Process(LastDate.AddDays(-PricesCount), LastDate, InitialCash);
            if (expectedHit)
                CheckPositionOpenedEquity(testEquity, positionDir, expectedPrice, expectedPositionTicks);
            else
                CheckEmptyEquity(testEquity);
        }

        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice, true, StartingPrice, PricesCount - 1)]
        public void Process_SignalReverse_WithOpenedPosition(SignalType signalType, PositionDir positionDir, float price,
            bool expectedHit, float expectedPrice, int expectedPositionTicks)
        {
            _signalGeneratorOnClose.GenerateOnClose(default, default).ReturnsForAnyArgs(args =>
            {
                if ((args.ArgAt<int>(1) == 0) || (args.ArgAt<int>(1) == 1))
                    return new List<Signal>() {
                    new Signal()
                    {
                        Stock = _stock,
                        DataRange = StockDataRange.Daily,
                        IntradayInterval = 0,
                        Type = signalType,
                        Direction = positionDir,
                        ReversePosition = true,
                        Price = price,
                        Volume = 1
                    }
                };
                return new List<Signal>();
            });

            TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, null, _signalGeneratorOnClose, _commission, _slippage, _mmPositionCloseCalculator);
            SystemEquity testEquity = TestObj.Process(LastDate.AddDays(-PricesCount), LastDate, InitialCash);
            if (expectedHit)
                CheckPositionOpenedEquity(testEquity, positionDir, expectedPrice, expectedPositionTicks);
            else
                CheckEmptyEquity(testEquity);
        }

        //[Test]
        //public void Process_StopHit__ClosesPosition()
        //{

        //}

        //[Test]
        //public void Process_StopNotHit__PositionLeftOpen()
        //{

        //}
    }
}
