using MarketOps.StockData.Types;
using MarketOps.System.Processor;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class SignalSelectorTests
    {
        private StockPricesData CreatePricesData(float o, float h, float l, float c)
        {
            StockPricesData res = new StockPricesData(1);
            res.O[0] = o;
            res.H[0] = h;
            res.L[0] = l;
            res.C[0] = c;
            return res;
        }

        [Test]
        public void OnOpen_OpenSignal__ReturnsTrue()
        {
            SignalSelector.OnOpen(new Signal() { Type = SignalType.EnterOnOpen }, CreatePricesData(0, 0, 0, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnOpen_NotOpenSignal__ReturnsFalse()
        {
            SignalSelector.OnOpen(new Signal() { Type = SignalType.EnterOnClose }, CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnClose_CloseSignal__ReturnsTrue()
        {
            SignalSelector.OnClose(new Signal() { Type = SignalType.EnterOnClose }, CreatePricesData(0, 0, 0, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnClose_NotCloseSignal__ReturnsFalse()
        {
            SignalSelector.OnClose(new Signal() { Type = SignalType.EnterOnOpen }, CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnPrice_NotPriceSignal__ReturnsFalse()
        {
            SignalSelector.OnPrice(new Signal() { Type = SignalType.EnterOnOpen }, CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnPrice_LongPriceSignal_OutOfRange__ReturnsFalse()
        {
            SignalSelector.OnPrice(new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Long, Price = 200 }, CreatePricesData(0, 100, 50, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnPrice_LongPriceSignal_InRange__ReturnsTrue()
        {
            SignalSelector.OnPrice(new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Long, Price = 75 }, CreatePricesData(0, 100, 50, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnPrice_LongPriceSignal_BelowRange__ReturnsTrue()
        {
            SignalSelector.OnPrice(new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Long, Price = 20 }, CreatePricesData(0, 100, 50, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnPrice_ShortPriceSignal_OutOfRange__ReturnsFalse()
        {
            SignalSelector.OnPrice(new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Short, Price = 20 }, CreatePricesData(0, 100, 50, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnPrice_ShortPriceSignal_InRange__ReturnsTrue()
        {
            SignalSelector.OnPrice(new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Short, Price = 75 }, CreatePricesData(0, 100, 50, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnPrice_ShortPriceSignal_AboveRange__ReturnsTrue()
        {
            SignalSelector.OnPrice(new Signal() { Type = SignalType.EnterOnPrice, Direction = PositionDir.Short, Price = 120 }, CreatePricesData(0, 100, 50, 0), 0).ShouldBeTrue();
        }
    }
}
