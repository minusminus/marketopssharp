using MarketOps.System.Processor;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class OpenPriceSelectorTests
    {
        [Test]
        public void OnOpen__ReturnsOpenPrice()
        {
            OpenPriceSelector.OnOpen(new Signal(), StockPricesDataUtils.CreatePricesData(10, 0, 0, 0), 0).ShouldBe(10);
        }

        [Test]
        public void OnClose__ReturnsClosePrice()
        {
            OpenPriceSelector.OnClose(new Signal(), StockPricesDataUtils.CreatePricesData(0, 0, 0, 10), 0).ShouldBe(10);
        }

        [TestCase(PositionDir.Long, 50, 50)]
        [TestCase(PositionDir.Long, 5, 10)]
        [TestCase(PositionDir.Short, 50, 10)]
        [TestCase(PositionDir.Short, 7, 7)]
        public void OnPrice(PositionDir positionDir, float price, float expected)
        {
            OpenPriceSelector.OnPrice(
                new Signal() { Direction = positionDir, Price = price }, 
                StockPricesDataUtils.CreatePricesData(10, 100, 5, 20), 
                0).ShouldBe(expected);
        }
    }
}
