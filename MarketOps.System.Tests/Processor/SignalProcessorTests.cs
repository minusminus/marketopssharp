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
using System.Linq;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class SignalProcessorTests
    {
        private readonly DateTime LastDate = DateTime.Now.Date;
        private readonly int PricesCount = 10;
        private readonly float InitialCash = 10000;

        private static readonly StockDefinition _stock = new StockDefinition();

        private SignalProcessor TestObj;
        private IDataLoader _dataLoader;
        private StockPricesData _pricesData;

        private bool _signalSelectorCalled;
        private bool _openPriceLevelCalled;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = Substitute.For<IDataLoader>();
            TestObj = new SignalProcessor(_dataLoader);
            _signalSelectorCalled = false;
            _openPriceLevelCalled = false;

            _pricesData = new StockPricesData(PricesCount);
            for (int i = 0; i < _pricesData.Length; i++)
            {
                _pricesData.O[i] = i;
                _pricesData.H[i] = i;
                _pricesData.L[i] = i;
                _pricesData.C[i] = i;
                _pricesData.TS[i] = LastDate.AddDays(-_pricesData.Length + i + 1);
            }
            _dataLoader.Get(default, default, default, default, default).ReturnsForAnyArgs(_pricesData);
        }

        private List<Signal> CreateSignals() => new List<Signal>() {
            new Signal() { Stock = _stock, Direction = PositionDir.Long, Price = 10, Volume = 10, ReversePosition = false },
            new Signal() { Stock = _stock, Direction = PositionDir.Short, Price = 10, Volume = 10, ReversePosition = false },
            new Signal() { Stock = _stock, Direction = PositionDir.Long, Price = 10, Volume = 10, ReversePosition = true },
            new Signal() { Stock = _stock, Direction = PositionDir.Short, Price = 10, Volume = 10, ReversePosition = true },
        };

        private SystemEquity CreateEquity() => new SystemEquity() { Cash = InitialCash };

        private void CheckProcessResult(SystemEquity equity, List<Signal> signals, bool expectedSelectorCalled, bool expectedPriceLevelCalled,
            int expectedSignalsCount, int expectedPositionsActiveCount, int expectedValueCount)
        {
            _signalSelectorCalled.ShouldBe(expectedSelectorCalled);
            _openPriceLevelCalled.ShouldBe(expectedPriceLevelCalled);
            signals.Count.ShouldBe(expectedSignalsCount);
            equity.PositionsActive.Count.ShouldBe(expectedPositionsActiveCount);
            equity.PositionsClosed.Count.ShouldBe(0);
            equity.ClosedPositionsValue.Count.ShouldBe(0);
            equity.Value.Count.ShouldBe(expectedValueCount);
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

        [Test]
        public void Process_EmptySignalsList__DoesNothing()
        {
            SystemEquity equity = CreateEquity();
            List<Signal> signals = new List<Signal>();
            TestObj.Process(signals, LastDate, equity,
                (_) => { _signalSelectorCalled = true; return new List<Signal>() { new Signal() }; },
                (_, __, ___) => { _openPriceLevelCalled = true; return -1; });
            CheckProcessResult(equity, signals, false, false, 0, 0, 0);
        }

        [Test]
        public void Process_SignalSelectorReturnsEmptyList__DoesNothing()
        {
            SystemEquity equity = CreateEquity();
            List<Signal> signals = CreateSignals();
            TestObj.Process(signals, LastDate, equity,
                (_) => { _signalSelectorCalled = true; return new List<Signal>(); },
                (_, __, ___) => { _openPriceLevelCalled = true; return -1; });
            CheckProcessResult(equity, signals, true, false, 4, 0, 0);
        }

        [Test]
        public void Process_OpenLongOnPrice__OpensPosition()
        {
            SystemEquity equity = CreateEquity();
            List<Signal> signals = CreateSignals();
            Signal openSignal = signals.Where(s => (s.Direction == PositionDir.Long) && (!s.ReversePosition)).First();
            TestObj.Process(signals, LastDate, equity,
                (sigs) => { _signalSelectorCalled = true; return sigs.Where(s => (s.Direction == PositionDir.Long) && (!s.ReversePosition)); },
                (_, __, sig) => { _openPriceLevelCalled = true; return sig.Price; });
            CheckProcessResult(equity, signals, true, true, 3, 1, 0);
            signals.Contains(openSignal).ShouldBeFalse();
            CheckPositionsActive(equity, 0, openSignal, PositionDir.Long, openSignal.Price, openSignal.Volume, LastDate);
            equity.Cash.ShouldBe(InitialCash - openSignal.Price * openSignal.Volume);
        }

        [Test]
        public void Process_OpenShortOnPrice__OpensPosition()
        {
            SystemEquity equity = CreateEquity();
            List<Signal> signals = CreateSignals();
            Signal openSignal = signals.Where(s => (s.Direction == PositionDir.Short) && (!s.ReversePosition)).First();
            TestObj.Process(signals, LastDate, equity,
                (sigs) => { _signalSelectorCalled = true; return sigs.Where(s => (s.Direction == PositionDir.Short) && (!s.ReversePosition)); },
                (_, __, sig) => { _openPriceLevelCalled = true; return sig.Price; });
            CheckProcessResult(equity, signals, true, true, 3, 1, 0);
            signals.Contains(openSignal).ShouldBeFalse();
            CheckPositionsActive(equity, 0, openSignal, PositionDir.Short, openSignal.Price, openSignal.Volume, LastDate);
            equity.Cash.ShouldBe(InitialCash - openSignal.Price * openSignal.Volume);
        }
    }
}
