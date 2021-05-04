using NUnit.Framework;
using Shouldly;
using MarketOps.SystemExecutor.Processor;
using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using MarketOps.SystemData.Interfaces;
using System.Linq;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;
using MarketOps.Tests.SystemExecutor.Mocks;

namespace MarketOps.Tests.SystemExecutor.Processor
{
    [TestFixture]
    public class SignalsProcessorTests
    {
        private readonly DateTime LastDate = DateTime.Now.Date;
        private const int PricesCount = 10;
        private const float InitialCash = 10000;
        private const float SignalPrice = 10;
        private const int SignalVolume = 10;
        private const float SignalBalance = 0.4f;

        private readonly StockDefinition _stock = new StockDefinition() { ID = 1 };

        private SignalsProcessor TestObj;
        private ISystemDataLoader _dataLoader;
        private ICommission _commission;
        private ISlippage _slippage;

        private bool _signalSelectorCalled;
        private bool _openPriceLevelCalled;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = SystemDataLoaderUtils.CreateSubstitute(PricesCount, LastDate);
            _commission = CommissionUtils.CreateSubstitute();
            _slippage = SlippageUtils.CreateSusbstitute();
            TestObj = new SignalsProcessor(_dataLoader, _commission, _slippage);
            _signalSelectorCalled = false;
            _openPriceLevelCalled = false;
        }

        private SystemState CreateSystemState()
        {
            var res = new SystemState() { Cash = InitialCash };
            res.Signals.AddRange(new List<Signal>() {
                new Signal() { Stock = _stock, Direction = PositionDir.Long, Price = SignalPrice, Volume = SignalVolume, ReversePosition = false },
                new Signal() { Stock = _stock, Direction = PositionDir.Short, Price = SignalPrice, Volume = SignalVolume, ReversePosition = false },
                new Signal() { Stock = _stock, Direction = PositionDir.Long, Price = SignalPrice, Volume = SignalVolume, ReversePosition = true },
                new Signal() { Stock = _stock, Direction = PositionDir.Short, Price = SignalPrice, Volume = SignalVolume, ReversePosition = true },
                new Signal() { Stock = _stock, Direction = PositionDir.Long, Type = SignalType.EnterOnOpen, Rebalance = true, NewBalance = new List<(StockDefinition stockDef, float balance)>(){
                    (_stock, SignalBalance)
                } },
            });
            return res;
        }

        private void CheckProcessResult(SystemState equity, bool expectedSelectorCalled, bool expectedPriceLevelCalled,
            int expectedSignalsCount, int expectedPositionsActiveCount, int expectedPositionsClosedCount)
        {
            _signalSelectorCalled.ShouldBe(expectedSelectorCalled);
            _openPriceLevelCalled.ShouldBe(expectedPriceLevelCalled);
            equity.Signals.Count.ShouldBe(expectedSignalsCount);
            equity.PositionsActive.Count.ShouldBe(expectedPositionsActiveCount);
            equity.PositionsClosed.Count.ShouldBe(expectedPositionsClosedCount);
            equity.ClosedPositionsEquity.Count.ShouldBe(expectedPositionsClosedCount);
            equity.Equity.Count.ShouldBe(0);
        }

        private void CheckPositionsActive(SystemState equity, int posIndex, Signal expectedSignal, PositionDir expectedDir,
            float expectedOpen, float expectedVolume, DateTime expectedTS)
        {
            equity.PositionsActive[posIndex].EntrySignal.ShouldBe(expectedSignal);
            equity.PositionsActive[posIndex].Direction.ShouldBe(expectedDir);
            equity.PositionsActive[posIndex].Open.ShouldBe(expectedOpen);
            equity.PositionsActive[posIndex].Volume.ShouldBe(expectedVolume);
            equity.PositionsActive[posIndex].TSOpen.ShouldBe(expectedTS);
        }

        private void CheckPositionsClosed(SystemState equity, int posIndex, PositionDir expectedDir,
            float expectedClose, float expectedVolume, DateTime expectedTS)
        {
            equity.PositionsClosed[posIndex].Direction.ShouldBe(expectedDir);
            equity.PositionsClosed[posIndex].Close.ShouldBe(expectedClose);
            equity.PositionsClosed[posIndex].Volume.ShouldBe(expectedVolume);
            equity.PositionsClosed[posIndex].TSClose.ShouldBe(expectedTS);
        }

        private void TestOpenPosition(Func<Signal, bool> signalFilter, PositionDir expectedOpenedPosDir)
        {
            SystemState equity = CreateSystemState();
            Signal openSignal = equity.Signals.First(signalFilter);
            TestObj.Process(LastDate, equity,
                (sig, _, __) => { _signalSelectorCalled = true; return signalFilter(sig); },
                (sig, _, __) => { _openPriceLevelCalled = true; return sig.Price; });
            CheckProcessResult(equity, true, true, 5, 1, 0);
            equity.Signals.Contains(openSignal).ShouldBeTrue();
            CheckPositionsActive(equity, 0, openSignal, expectedOpenedPosDir, openSignal.Price, openSignal.Volume, LastDate);
            equity.Cash.ShouldBe(InitialCash - equity.PositionsActive[0].DirectionMultiplier() * (openSignal.Price * openSignal.Volume));
        }

        public void TestReversePosition(PositionDir activePosDir, PositionDir expectedOpenedPosDir)
        {
            bool signalFilter(Signal s) => !s.Rebalance && s.Direction == PositionDir.Long && s.ReversePosition;
            SystemState equity = CreateSystemState();
            Signal openSignal = equity.Signals.First(signalFilter);
            Position activePosition = new Position() { Direction = activePosDir, Stock = _stock, Volume = 5 };
            equity.PositionsActive.Add(activePosition);
            TestObj.Process(LastDate, equity,
                (sig, _, __) => { _signalSelectorCalled = true; return signalFilter(sig); },
                (sig, _, __) => { _openPriceLevelCalled = true; return sig.Price; });
            CheckProcessResult(equity, true, true, 5, 1, 1);
            equity.Signals.Contains(openSignal).ShouldBeTrue();
            CheckPositionsClosed(equity, 0, activePosDir, openSignal.Price, activePosition.Volume, LastDate);
            CheckPositionsActive(equity, 0, openSignal, expectedOpenedPosDir, openSignal.Price, openSignal.Volume, LastDate);
            equity.Cash.ShouldBe(InitialCash + activePosition.DirectionMultiplier() * (openSignal.Price * activePosition.Volume) - expectedOpenedPosDir.DirectionMultiplier() * (openSignal.Price * openSignal.Volume));
        }

        [Test]
        public void Process_EmptySignalsList__DoesNothing()
        {
            SystemState equity = CreateSystemState();
            equity.Signals.Clear();
            TestObj.Process(LastDate, equity,
                (_, __, ___) => { _signalSelectorCalled = true; return true; },
                (_, __, ___) => { _openPriceLevelCalled = true; return -1; });
            CheckProcessResult(equity, false, false, 0, 0, 0);
        }

        [Test]
        public void Process_SignalSelectorReturnsFalse__DoesNothing()
        {
            SystemState equity = CreateSystemState();
            TestObj.Process(LastDate, equity,
                (_, __, ___) => { _signalSelectorCalled = true; return false; },
                (_, __, ___) => { _openPriceLevelCalled = true; return -1; });
            CheckProcessResult(equity, true, false, 5, 0, 0);
        }

        [Test]
        public void Process_OpenLongOnPrice__OpensPosition()
        {
            TestOpenPosition((s) => !s.Rebalance && s.Direction == PositionDir.Long && !s.ReversePosition, PositionDir.Long);
        }

        [Test]
        public void Process_OpenShortOnPrice__OpensPosition()
        {
            TestOpenPosition((s) => !s.Rebalance && s.Direction == PositionDir.Short && !s.ReversePosition, PositionDir.Short);
        }

        [Test]
        public void Process_ReverseLongPos__ReversesPosition()
        {
            TestReversePosition(PositionDir.Long, PositionDir.Short);
        }

        [Test]
        public void Process_ReverseShortPos__ReversesPosition()
        {
            TestReversePosition(PositionDir.Short, PositionDir.Long);
        }

        [Test]
        public void Process_ReverseLongPos_NoActivePos__OpensPosition()
        {
            TestOpenPosition((s) => !s.Rebalance && s.Direction == PositionDir.Long && s.ReversePosition, PositionDir.Long);
        }

        [Test]
        public void Process_ReverseShortPos_NoActivePos__OpensPosition()
        {
            TestOpenPosition((s) => !s.Rebalance && s.Direction == PositionDir.Short && s.ReversePosition, PositionDir.Short);
        }

        [Test]
        public void Process_OpenLongOnPrice_TwoSignals__OpensPositions()
        {
            bool signalFilter(Signal s) => !s.Rebalance && !s.ReversePosition;
            SystemState equity = CreateSystemState();
            Signal[] openSignal = equity.Signals.Where(signalFilter).ToArray();
            openSignal.Length.ShouldBe(2);
            TestObj.Process(LastDate, equity,
                (sig, _, __) => { _signalSelectorCalled = true; return signalFilter(sig); },
                (sig, _, __) => { _openPriceLevelCalled = true; return sig.Price; });
            CheckProcessResult(equity, true, true, 5, 2, 0);
            CheckPositionsActive(equity, 0, openSignal[0], openSignal[0].Direction, openSignal[0].Price, openSignal[0].Volume, LastDate);
            CheckPositionsActive(equity, 1, openSignal[1], openSignal[1].Direction, openSignal[1].Price, openSignal[1].Volume, LastDate);
            equity.Cash.ShouldBe(InitialCash - openSignal[0].Price * openSignal[0].Volume + openSignal[1].Price * openSignal[1].Volume);
        }

        [Test]
        public void Process_Rebalance__ClosesAllPrevPositions_OpensNewRebalanced()
        {
            bool signalFilter(Signal s) => s.Rebalance;
            SystemState equity = CreateSystemState();
            Signal openSignal = equity.Signals.First(signalFilter);
            Position activePosition = new Position() { Direction = PositionDir.Long, Stock = _stock, Volume = 5 };
            equity.PositionsActive.Add(activePosition);
            TestObj.Process(LastDate, equity,
                (sig, _, __) => { _signalSelectorCalled = true; return signalFilter(sig); },
                (sig, _, __) => { _openPriceLevelCalled = true; return SignalPrice; });
            CheckProcessResult(equity, true, true, 5, 1, 1);
            equity.Signals.Contains(openSignal).ShouldBeTrue();
            CheckPositionsClosed(equity, 0, PositionDir.Long, SignalPrice, activePosition.Volume, LastDate);
            float totalValue = InitialCash + activePosition.Volume * SignalPrice;
            CheckPositionsActive(equity, 0, openSignal, PositionDir.Long, SignalPrice, totalValue * SignalBalance / SignalPrice, LastDate);
            equity.Cash.ShouldBe(totalValue * (1 - SignalBalance));
        }
    }
}
