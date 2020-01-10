using System;
using MarketOps.System.GPW;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;

namespace MarketOps.System.Tests.GPW
{
    [TestFixture]
    public class GPWTickAlignerTests
    {
        private readonly GPWTickAligner _testObj = new GPWTickAligner();

        [TestCase(1, 1)]
        [TestCase(1.001F, 1.01F)]
        [TestCase(0.991F, 1)]
        [TestCase(1.991F, 2)]
        [TestCase(2.01F, 2.02F)]
        [TestCase(5.01F, 5.05F)]
        [TestCase(10.01F, 10.1F)]
        [TestCase(20.01F, 20.2F)]
        [TestCase(50.01F, 50.5F)]
        [TestCase(100.01F, 101)]
        [TestCase(200.01F, 202)]
        [TestCase(500.01F, 505)]
        [TestCase(1000.01F, 1010)]
        [TestCase(2000.01F, 2020)]
        [TestCase(5000.01F, 5050)]
        [TestCase(10000.01F, 10100)]
        [TestCase(20000.01F, 20200)]
        [TestCase(50000.01F, 50500)]
        public void Up_Stock_Before20190304(float value, float expected)
        {
            _testObj.Up(StockType.Stock, new DateTime(2019, 03, 03), value).ShouldBe(expected);
        }

        [TestCase(1, 1)]
        [TestCase(1.001F, 1)]
        [TestCase(0.991F, 0.99F)]
        [TestCase(1.991F, 1.99F)]
        [TestCase(2.01F, 2)]
        [TestCase(5.01F, 5)]
        [TestCase(10.01F, 10)]
        [TestCase(20.01F, 20)]
        [TestCase(50.01F, 50)]
        [TestCase(100.01F, 100)]
        [TestCase(200.01F, 200)]
        [TestCase(500.01F, 500)]
        [TestCase(1000.01F, 1000)]
        [TestCase(2000.01F, 2000)]
        [TestCase(5000.01F, 5000)]
        [TestCase(10000.01F, 10000)]
        [TestCase(20000.01F, 20000)]
        [TestCase(50000.01F, 50000)]
        public void Down_Stock_Before20190304(float value, float expected)
        {
            _testObj.Down(StockType.Stock, new DateTime(2019, 03, 03), value).ShouldBe(expected);
        }
    }
}
