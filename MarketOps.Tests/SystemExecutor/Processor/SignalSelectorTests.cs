using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.Processor;
using MarketOps.Tests.SystemExecutor.Mocks;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.Tests.SystemExecutor.Processor
{
    [TestFixture]
    public class SignalSelectorTests
    {
        [Test]
        public void OnOpen_OpenSignal__ReturnsTrue()
        {
            SignalSelector.OnOpen(new Signal() { Type = SignalType.EnterOnOpen }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnOpen_NotOpenSignal__ReturnsFalse()
        {
            SignalSelector.OnOpen(new Signal() { Type = SignalType.EnterOnClose }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnClose_CloseSignal__ReturnsTrue()
        {
            SignalSelector.OnClose(new Signal() { Type = SignalType.EnterOnClose }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnClose_NotCloseSignal__ReturnsFalse()
        {
            SignalSelector.OnClose(new Signal() { Type = SignalType.EnterOnOpen }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnPrice_NotPriceSignal__ReturnsFalse()
        {
            SignalSelector.OnPrice(new Signal() { Type = SignalType.EnterOnOpen }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [TestCase(PositionDir.Long, 75, true)]
        [TestCase(PositionDir.Long, 120, false)]
        [TestCase(PositionDir.Long, 20, true)]
        [TestCase(PositionDir.Short, 75, true)]
        [TestCase(PositionDir.Short, 120, true)]
        [TestCase(PositionDir.Short, 20, false)]
        public void OnPrice(PositionDir positionDir, float price, bool expected)
        {
            SignalSelector.OnPrice(
                new Signal() { Type = SignalType.EnterOnPrice, Direction = positionDir, Price = price },
                StockPricesDataUtils.CreatePricesData(0, 100, 50, 0),
                0).ShouldBe(expected);
        }
    }
}
