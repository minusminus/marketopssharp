using MarketOps.StockData.Types;
using MarketOps.System.Processor;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class SignalSelectorTests
    {
        private List<Signal> CreateSignals() => new List<Signal>() {
            new Signal() { Type = SignalType.EnterOnOpen, Price = 1},
            new Signal() { Type = SignalType.EnterOnClose, Price = 2},
            new Signal() { Type = SignalType.EnterOnOpen, Price = 3},
            new Signal() { Type = SignalType.EnterOnClose, Price = 4},
            new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Long, Price = 100},
            new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Long, Price = 200},
            new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Long, Price = 300},
            new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Short, Price = 100},
            new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Short, Price = 200},
            new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Short, Price = 300},
        };

        private StockPricesData CreatePricesData(float o, float h, float l, float c)
        {
            StockPricesData res = new StockPricesData(1);
            res.O[0] = o;
            res.H[0] = h;
            res.L[0] = l;
            res.C[0] = c;
            return res;
        }

        private void CheckSelectedSignals(List<Signal> signals, float[] expectedPrices)
        {
            signals.Count.ShouldBe(expectedPrices.Length);
            foreach (float price in expectedPrices)
                signals.Where(x => x.Price == price).ShouldNotBeEmpty();
        }

        [Test]
        public void SignalsOnOpen_EmptyList__ReturnsEmptyList()
        {
            SignalSelector.SignalsOnOpen(new List<Signal>()).ShouldBeEmpty();
        }

        [Test]
        public void SignalsOnOpen_FilledList__ReturnsOnOpenOnly()
        {
            CheckSelectedSignals(SignalSelector.SignalsOnOpen(CreateSignals()).ToList(),
                new float[] { 1, 3 });
        }

        [Test]
        public void SignalsOnClose_EmptyList__ReturnsEmptyList()
        {
            SignalSelector.SignalsOnClose(new List<Signal>()).ShouldBeEmpty();
        }

        [Test]
        public void SignalsOnClose_FilledList__ReturnsOnCloseOnly()
        {
            CheckSelectedSignals(SignalSelector.SignalsOnClose(CreateSignals()).ToList(),
                new float[] { 2, 4 });
        }

        [Test]
        public void SignalsOnPrice_EmptyList__ReturnsEmptyList()
        {
            SignalSelector.SignalsOnPrice(new List<Signal>(), CreatePricesData(0, 0, 0, 0), 0).ShouldBeEmpty();
        }

        [Test]
        public void SignalsOnPrice_NoPriceInRange__ReturnsEmptyList()
        {
            SignalSelector.SignalsOnPrice(CreateSignals(), CreatePricesData(0, 50, 500, 0), 0).ShouldBeEmpty();
        }

        [Test]
        public void SignalsOnPrice_LongPriceInRange__ReturnsInRangeOnly()
        {
            CheckSelectedSignals(SignalSelector.SignalsOnPrice(CreateSignals(), CreatePricesData(0, 200, 500, 0), 0).ToList(),
                new float[] { 100, 200 });
        }

        [Test]
        public void SignalsOnPrice_ShortPriceInRange__ReturnsInRangeOnly()
        {
            CheckSelectedSignals(SignalSelector.SignalsOnPrice(CreateSignals(), CreatePricesData(0, 50, 200, 0), 0).ToList(),
                new float[] { 200, 300 });
        }
    }
}
