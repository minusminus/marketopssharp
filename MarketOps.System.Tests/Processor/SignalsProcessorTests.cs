using NUnit.Framework;
using Shouldly;
using MarketOps.System.Processor;
using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using MarketOps.System.Interfaces;
using System.Linq;
using MarketOps.System.Tests.Mocks;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class SignalsProcessorTests
    {
        private readonly DateTime LastDate = DateTime.Now.Date;
        private readonly int PricesCount = 10;
        private readonly float InitialCash = 10000;

        private static readonly StockDefinition _stock = new StockDefinition() { ID = 1 };

        private SignalsProcessor TestObj;
        private IDataLoader _dataLoader;
        private ICommission _commission;
        private ISlippage _slippage;

        private bool _signalSelectorCalled;
        private bool _openPriceLevelCalled;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = DataLoaderUtils.CreateSubstitute(PricesCount, LastDate);
            _commission = CommissionUtils.CreateSubstitute();
            _slippage = SlippageUtils.CreateSusbstitute();
            TestObj = new SignalsProcessor(_dataLoader, _commission, _slippage);
            _signalSelectorCalled = false;
            _openPriceLevelCalled = false;
        }

        private List<Signal> CreateSignals() => new List<Signal>() {
            new Signal() { Stock = _stock, Direction = PositionDir.Long, Price = 10, Volume = 10, ReversePosition = false },
            new Signal() { Stock = _stock, Direction = PositionDir.Short, Price = 10, Volume = 10, ReversePosition = false },
            new Signal() { Stock = _stock, Direction = PositionDir.Long, Price = 10, Volume = 10, ReversePosition = true },
            new Signal() { Stock = _stock, Direction = PositionDir.Short, Price = 10, Volume = 10, ReversePosition = true },
        };

        private SystemEquity CreateEquity() => new SystemEquity() { Cash = InitialCash };

        private void CheckProcessResult(SystemEquity equity, List<Signal> signals, bool expectedSelectorCalled, bool expectedPriceLevelCalled,
            int expectedSignalsCount, int expectedPositionsActiveCount, int expectedPositionsClosedCount)
        {
            _signalSelectorCalled.ShouldBe(expectedSelectorCalled);
            _openPriceLevelCalled.ShouldBe(expectedPriceLevelCalled);
            signals.Count.ShouldBe(expectedSignalsCount);
            equity.PositionsActive.Count.ShouldBe(expectedPositionsActiveCount);
            equity.PositionsClosed.Count.ShouldBe(expectedPositionsClosedCount);
            equity.ClosedPositionsValue.Count.ShouldBe(expectedPositionsClosedCount);
            equity.Value.Count.ShouldBe(0);
        }

        private void CheckPositionsActive(SystemEquity equity, int posIndex, Signal expectedSignal, PositionDir expectedDir,
            float expectedOpen, int expectedVolume, DateTime expectedTS)
        {
            equity.PositionsActive[posIndex].EntrySignal.ShouldBe(expectedSignal);
            equity.PositionsActive[posIndex].Direction.ShouldBe(expectedDir);
            equity.PositionsActive[posIndex].Open.ShouldBe(expectedOpen);
            equity.PositionsActive[posIndex].Volume.ShouldBe(expectedVolume);
            equity.PositionsActive[posIndex].TSOpen.ShouldBe(expectedTS);
        }

        private void CheckPositionsClosed(SystemEquity equity, int posIndex, PositionDir expectedDir,
            float expectedClose, int expectedVolume, DateTime expectedTS)
        {
            equity.PositionsClosed[posIndex].Direction.ShouldBe(expectedDir);
            equity.PositionsClosed[posIndex].Close.ShouldBe(expectedClose);
            equity.PositionsClosed[posIndex].Volume.ShouldBe(expectedVolume);
            equity.PositionsClosed[posIndex].TSClose.ShouldBe(expectedTS);
        }

        private void TestOpenPosition(Func<Signal, bool> signalFilter, PositionDir expectedOpenedPosDir)
        {
            SystemEquity equity = CreateEquity();
            List<Signal> signals = CreateSignals();
            Signal openSignal = signals.First(signalFilter);
            TestObj.Process(signals, LastDate, equity,
                (sig, _, __) => { _signalSelectorCalled = true; return signalFilter(sig); },
                (sig, _, __) => { _openPriceLevelCalled = true; return sig.Price; });
            CheckProcessResult(equity, signals, true, true, 4, 1, 0);
            signals.Contains(openSignal).ShouldBeTrue();
            CheckPositionsActive(equity, 0, openSignal, expectedOpenedPosDir, openSignal.Price, openSignal.Volume, LastDate);
            equity.Cash.ShouldBe(InitialCash - (openSignal.Price * openSignal.Volume));
        }

        public void TestReversePosition(PositionDir activePosDir, PositionDir expectedOpenedPosDir)
        {
            bool signalFilter(Signal s) => (s.Direction == PositionDir.Long) && (s.ReversePosition);
            SystemEquity equity = CreateEquity();
            List<Signal> signals = CreateSignals();
            Signal openSignal = signals.First(signalFilter);
            Position activePosition = new Position() { Direction = activePosDir, Stock = _stock, Volume = 5 };
            equity.PositionsActive.Add(activePosition);
            TestObj.Process(signals, LastDate, equity,
                (sig, _, __) => { _signalSelectorCalled = true; return signalFilter(sig); },
                (sig, _, __) => { _openPriceLevelCalled = true; return sig.Price; });
            CheckProcessResult(equity, signals, true, true, 4, 1, 1);
            signals.Contains(openSignal).ShouldBeTrue();
            CheckPositionsClosed(equity, 0, activePosDir, openSignal.Price, activePosition.Volume, LastDate);
            CheckPositionsActive(equity, 0, openSignal, expectedOpenedPosDir, openSignal.Price, openSignal.Volume, LastDate);
            equity.Cash.ShouldBe(InitialCash + (openSignal.Price * activePosition.Volume) - (openSignal.Price * openSignal.Volume));
        }

        [Test]
        public void Process_EmptySignalsList__DoesNothing()
        {
            SystemEquity equity = CreateEquity();
            List<Signal> signals = new List<Signal>();
            TestObj.Process(signals, LastDate, equity,
                (_, __, ___) => { _signalSelectorCalled = true; return true; },
                (_, __, ___) => { _openPriceLevelCalled = true; return -1; });
            CheckProcessResult(equity, signals, false, false, 0, 0, 0);
        }

        [Test]
        public void Process_SignalSelectorReturnsFalse__DoesNothing()
        {
            SystemEquity equity = CreateEquity();
            List<Signal> signals = CreateSignals();
            TestObj.Process(signals, LastDate, equity,
                (_, __, ___) => { _signalSelectorCalled = true; return false; },
                (_, __, ___) => { _openPriceLevelCalled = true; return -1; });
            CheckProcessResult(equity, signals, true, false, 4, 0, 0);
        }

        [Test]
        public void Process_OpenLongOnPrice__OpensPosition()
        {
            TestOpenPosition((s) => (s.Direction == PositionDir.Long) && (!s.ReversePosition), PositionDir.Long);
        }

        [Test]
        public void Process_OpenShortOnPrice__OpensPosition()
        {
            TestOpenPosition((s) => (s.Direction == PositionDir.Short) && (!s.ReversePosition), PositionDir.Short);
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
            TestOpenPosition((s) => (s.Direction == PositionDir.Long) && (s.ReversePosition), PositionDir.Long);
        }

        [Test]
        public void Process_ReverseShortPos_NoActivePos__OpensPosition()
        {
            TestOpenPosition((s) => (s.Direction == PositionDir.Short) && (s.ReversePosition), PositionDir.Short);
        }

        [Test]
        public void Process_OpenLongOnPrice_TwoSignals__OpensPositions()
        {
            bool signalFilter(Signal s) => !s.ReversePosition;
            SystemEquity equity = CreateEquity();
            List<Signal> signals = CreateSignals();
            Signal[] openSignal = signals.Where(signalFilter).ToArray();
            openSignal.Length.ShouldBe(2);
            TestObj.Process(signals, LastDate, equity,
                (sig, _, __) => { _signalSelectorCalled = true; return signalFilter(sig); },
                (sig, _, __) => { _openPriceLevelCalled = true; return sig.Price; });
            CheckProcessResult(equity, signals, true, true, 4, 2, 0);
            CheckPositionsActive(equity, 0, openSignal[0], openSignal[0].Direction, openSignal[0].Price, openSignal[0].Volume, LastDate);
            CheckPositionsActive(equity, 1, openSignal[1], openSignal[1].Direction, openSignal[1].Price, openSignal[1].Volume, LastDate);
            equity.Cash.ShouldBe(InitialCash - (openSignal[0].Price * openSignal[0].Volume) - (openSignal[1].Price * openSignal[1].Volume));
        }
    }
}
