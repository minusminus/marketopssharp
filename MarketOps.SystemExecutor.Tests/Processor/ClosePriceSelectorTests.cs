using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.Processor;
using MarketOps.SystemExecutor.Tests.Mocks;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.SystemExecutor.Tests.Processor
{
    [TestFixture]
    public class ClosePriceSelectorTests
    {
        [Test]
        public void OnOpen__ReturnsOpenPrice()
        {
            ClosePriceSelector.OnOpen(new Position(), StockPricesDataUtils.CreatePricesData(10, 0, 0, 0), 0).ShouldBe(10);
        }

        [Test]
        public void OnClose__ReturnsClosePrice()
        {
            ClosePriceSelector.OnClose(new Position(), StockPricesDataUtils.CreatePricesData(0, 0, 0, 10), 0).ShouldBe(10);
        }

        [TestCase(PositionDir.Long, 50, 10)]
        [TestCase(PositionDir.Long, 7, 7)]
        [TestCase(PositionDir.Short, 50, 50)]
        [TestCase(PositionDir.Short, 7, 10)]
        public void OnStopHit(PositionDir positionDir, float price, float expected)
        {
            ClosePriceSelector.OnStopHit(
                new Position() { Direction = positionDir, CloseModePrice = price },
                StockPricesDataUtils.CreatePricesData(10, 100, 5, 20),
                0).ShouldBe(expected);
        }
    }
}
