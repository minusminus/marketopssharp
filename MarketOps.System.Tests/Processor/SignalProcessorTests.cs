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
    public class SignalProcessorTests
    {
        private readonly DateTime LastDate = DateTime.Now.Date;
        private readonly int PricesCount = 10;

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

        [Test]
        public void Process_EmptySignalsList__DoesNothing()
        {
            List<Signal> signals = new List<Signal>();
            SystemEquity equity = new SystemEquity();
            TestObj.Process(signals, LastDate, equity,
                (_) => { _signalSelectorCalled = true; return new List<Signal>() { new Signal() }; },
                (_, __, ___) => { _openPriceLevelCalled = true; return -1; });
            _signalSelectorCalled.ShouldBeFalse();
            _openPriceLevelCalled.ShouldBeFalse();
            signals.Count.ShouldBe(0);
            equity.PositionsActive.Count.ShouldBe(0);
            equity.PositionsClosed.Count.ShouldBe(0);
            equity.ClosedPositionsValue.Count.ShouldBe(0);
            equity.Value.Count.ShouldBe(0);
        }
    }
}
