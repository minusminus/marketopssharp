using MarketOps.SystemExecutor.Processor;
using MarketOps.SystemExecutor.Tests.Mocks;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.SystemExecutor.Tests.Processor
{
    [TestFixture]
    public class ClosingPositionSelectorTests
    {
        [Test]
        public void OnOpen_CloseOnOpen__ReturnsTrue()
        {
            ClosingPositionSelector.OnOpen(new Position() { CloseMode = PositionCloseMode.OnOpen }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnOpen_NotCloseOnOpen__ReturnsFalse()
        {
            ClosingPositionSelector.OnOpen(new Position() { CloseMode = PositionCloseMode.OnClose }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnClose_CloseOnClose__ReturnsTrue()
        {
            ClosingPositionSelector.OnClose(new Position() { CloseMode = PositionCloseMode.OnClose }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnClose_NotCloseOnClose__ReturnsFalse()
        {
            ClosingPositionSelector.OnClose(new Position() { CloseMode = PositionCloseMode.OnOpen }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnStopHit_NotCloseOnPrice__ReturnsFalse()
        {
            ClosingPositionSelector.OnStopHit(new Position() { CloseMode = PositionCloseMode.OnClose }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [TestCase(PositionDir.Long, 75, true)]
        [TestCase(PositionDir.Long, 125, true)]
        [TestCase(PositionDir.Long, 25, false)]
        [TestCase(PositionDir.Short, 75, true)]
        [TestCase(PositionDir.Short, 125, false)]
        [TestCase(PositionDir.Short, 25, true)]
        public void OnStopHit(PositionDir positionDir, float closeModePrice, bool expected)
        {
            ClosingPositionSelector.OnStopHit(
                new Position() { Direction = positionDir, CloseMode = PositionCloseMode.OnStopHit, CloseModePrice = closeModePrice }, 
                StockPricesDataUtils.CreatePricesData(0, 100, 50, 0), 
                0).ShouldBe(expected);
        }
    }
}
