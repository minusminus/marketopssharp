using System;
using MarketOps.SystemExecutor.MM;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using MarketOps.SystemData.Types;
using MarketOps.StockData.Types;

namespace MarketOps.SystemExecutor.Tests.MM
{
    [TestFixture]
    public class MMSignalVolumeOneItemTests
    {
        private readonly MMSignalVolumeOneItem _testObj = new MMSignalVolumeOneItem();

        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(10, 10)]
        [TestCase(12345678, 12345678)]
        public void Calculate__ReturnsOne(int cash, float price)
        {
            _testObj.Calculate(new SystemState() { Cash = cash }, StockType.Stock, price).ShouldBe(1);
        }

        [Test]
        public void Calculate_RandomValues__ReturnsOne()
        {
            Random r = new Random();
            Enumerable.Range(1, 10).ToList()
                .ForEach(_ =>
                {
                    int v = r.Next(1000);
                    float p = (float)r.Next(1000);
                    _testObj.Calculate(new SystemState() { Cash = v }, StockType.Stock, p).ShouldBe(1, $"{v}, {p}");
                });
        }
    }
}
