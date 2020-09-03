﻿using MarketOps.System.Processor;
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
using MarketOps.System.Extensions;

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
        private SystemState _systemState;

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
            _systemState = new SystemState() { Cash = InitialCash };
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

        private void CheckEmptySystemState()
        {
            _systemState.Cash.ShouldBe(InitialCash);
            _systemState.PositionsActive.Count.ShouldBe(0);
            _systemState.PositionsClosed.Count.ShouldBe(0);
            _systemState.ClosedPositionsEquity.Count.ShouldBe(0);
            _systemState.Equity.Count.ShouldBe(PricesCount);
            _systemState.Equity.All(x => x.Value == InitialCash).ShouldBeTrue();
            _mmPositionCloseCalculator.DidNotReceiveWithAnyArgs().CalculateCloseMode(default, default);
            _stockStatMock.CalculateCallCount.ShouldBe(1);
        }

        private void CheckPositionOpenedSystemState(PositionDir expectedDir, float expectedPrice, int expectedPositionTicks)
        {
            _systemState.Cash.ShouldBe(InitialCash - expectedPrice);
            _systemState.PositionsActive.Count.ShouldBe(1);
            _systemState.PositionsActive[0].Direction.ShouldBe(expectedDir);
            _systemState.PositionsActive[0].Open.ShouldBe(expectedPrice);
            _systemState.PositionsActive[0].TicksActive.ShouldBe(expectedPositionTicks);
            _systemState.PositionsClosed.Count.ShouldBe(0);
            _systemState.ClosedPositionsEquity.Count.ShouldBe(0);
            _systemState.Equity.Count.ShouldBe(PricesCount);
            _systemState.Equity.All(x => x.Value == InitialCash).ShouldBeTrue();
            _mmPositionCloseCalculator.ReceivedWithAnyArgs(expectedPositionTicks).CalculateCloseMode(default, default);
            _stockStatMock.CalculateCallCount.ShouldBe(1);
        }

        private void CheckPositionReversedSystemState(PositionDir expectedActiveDir, PositionDir expectedClosedDir, float expectedPrice, int expectedPositionTicks, int expectedClosedPositionTicks)
        {
            _systemState.Cash.ShouldBe(InitialCash - expectedPrice);
            _systemState.PositionsActive.Count.ShouldBe(1);
            _systemState.PositionsActive[0].Direction.ShouldBe(expectedActiveDir);
            _systemState.PositionsActive[0].Open.ShouldBe(expectedPrice);
            _systemState.PositionsActive[0].TicksActive.ShouldBe(expectedPositionTicks);
            _systemState.PositionsClosed.Count.ShouldBe(1);
            _systemState.PositionsClosed[0].Direction.ShouldBe(expectedClosedDir);
            _systemState.PositionsClosed[0].Open.ShouldBe(expectedPrice);
            _systemState.PositionsClosed[0].TicksActive.ShouldBe(expectedClosedPositionTicks);
            _systemState.ClosedPositionsEquity.Count.ShouldBe(1);
            _systemState.Equity.Count.ShouldBe(PricesCount);
            _systemState.Equity.All(x => x.Value == InitialCash).ShouldBeTrue();
            _mmPositionCloseCalculator.ReceivedWithAnyArgs(expectedPositionTicks + expectedClosedPositionTicks - 2).CalculateCloseMode(default, default);
            _stockStatMock.CalculateCallCount.ShouldBe(1);
        }

        [Test, Combinatorial]
        public void Process_NoSignals__EmptyEquity([Values(false, true)] bool withGeneratorOnOpen, [Values(false, true)] bool withGeneratorOnClose)
        {
            _signalGeneratorOnOpen.GenerateOnOpen(default, default).ReturnsForAnyArgs(new List<Signal>());
            _signalGeneratorOnClose.GenerateOnClose(default, default).ReturnsForAnyArgs(new List<Signal>());

            TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, (withGeneratorOnOpen ? _signalGeneratorOnOpen : null), (withGeneratorOnClose ? _signalGeneratorOnClose : null), _commission, _slippage, _mmPositionCloseCalculator);
            TestObj.Process(_systemState, LastDate.AddDays(-PricesCount), LastDate);
            CheckEmptySystemState();
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
            TestObj.Process(_systemState, LastDate.AddDays(-PricesCount), LastDate);
            if (expectedHit)
                CheckPositionOpenedSystemState(positionDir, expectedPrice, expectedPositionTicks);
            else
                CheckEmptySystemState();
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
            TestObj.Process(_systemState, LastDate.AddDays(-PricesCount), LastDate);
            if (expectedHit)
                CheckPositionOpenedSystemState(positionDir, expectedPrice, expectedPositionTicks);
            else
                CheckEmptySystemState();
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
            TestObj.Process(_systemState, LastDate.AddDays(-PricesCount), LastDate);
            if (expectedHit)
                CheckPositionOpenedSystemState(positionDir, expectedPrice, expectedPositionTicks);
            else
                CheckEmptySystemState();
        }

        [TestCase(SignalType.EnterOnPrice, PositionDir.Long, StartingPrice, true, PositionDir.Short, StartingPrice, PricesCount - 1)]
        [TestCase(SignalType.EnterOnPrice, PositionDir.Short, StartingPrice, true, PositionDir.Long, StartingPrice, PricesCount - 1)]
        public void Process_SignalReverse_WithOpenedPosition(SignalType signalType, PositionDir positionDir, float price,
            bool expectedHit, PositionDir expectedPositionDir, float expectedPrice, int expectedPositionTicks)
        {
            Signal sig = new Signal()
            {
                Stock = _stock,
                DataRange = StockDataRange.Daily,
                IntradayInterval = 0,
                Type = signalType,
                Direction = positionDir,
                ReversePosition = true,
                Price = price,
                Volume = 1
            };

            _signalGeneratorOnClose.GenerateOnClose(default, default).ReturnsForAnyArgs(args =>
            {
                if (args.ArgAt<int>(1) == 0)
                    return new List<Signal>() { sig };
                return new List<Signal>();
            });

            _systemState.Open(LastDate.AddDays(-PricesCount - 1), positionDir, StartingPrice, sig, _slippage, _commission);

            TestObj = new SystemProcessor(_dataProvider, _dataLoader, _dataDefinitionProvider, null, _signalGeneratorOnClose, _commission, _slippage, _mmPositionCloseCalculator);
            TestObj.Process(_systemState, LastDate.AddDays(-PricesCount), LastDate);
            if (expectedHit)
                CheckPositionReversedSystemState(expectedPositionDir, positionDir, expectedPrice, expectedPositionTicks, 3);
            else
                CheckEmptySystemState();
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
