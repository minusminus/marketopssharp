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

        [TestCase(1, 1)]
        [TestCase(0.99F, 0.99F)]
        [TestCase(1.001F, 1.01F)]
        [TestCase(0.991F, 1)]
        [TestCase(1.991F, 2)]
        public void Up_Index(float value, float expected)
        {
            _testObj.Up(StockType.Index, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }

        [TestCase(1, 1)]
        [TestCase(0.99F, 0.99F)]
        [TestCase(1.001F, 1)]
        [TestCase(0.991F, 0.99F)]
        [TestCase(1.991F, 1.99F)]
        public void Down_Index(float value, float expected)
        {
            _testObj.Down(StockType.Index, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }

        [TestCase(1, 1)]
        [TestCase(0.99F, 1)]
        [TestCase(1.001F, 2)]
        [TestCase(0.991F, 1)]
        [TestCase(1.991F, 2)]
        [TestCase(2.001F, 3)]
        public void Up_Future(float value, float expected)
        {
            _testObj.Up(StockType.Future, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }

        [TestCase(1, 1)]
        [TestCase(0.99F, 1)]
        [TestCase(1.001F, 1)]
        [TestCase(0.991F, 1)]
        [TestCase(1.991F, 1)]
        [TestCase(2.001F, 2)]
        public void Down_Future(float value, float expected)
        {
            _testObj.Down(StockType.Future, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }

        [TestCase(0.0001F, 0.0001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.99991F, 1)]
        [TestCase(1.00001F, 1.0001F)]
        public void Up_InvestmentFund(float value, float expected)
        {
            _testObj.Up(StockType.InvestmentFund, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }

        [TestCase(0.0001F, 0.0001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.99991F, 0.9999F)]
        [TestCase(1.00001F, 1)]
        public void Down_InvestmentFund(float value, float expected)
        {
            _testObj.Down(StockType.InvestmentFund, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }

        [TestCase(0.0001F, 0.0001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.99991F, 1)]
        [TestCase(1.00001F, 1.0001F)]
        public void Up_NBPCurrency(float value, float expected)
        {
            _testObj.Up(StockType.NBPCurrency, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }

        [TestCase(0.0001F, 0.0001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.99991F, 0.9999F)]
        [TestCase(1.00001F, 1)]
        public void Down_NBPCurrency(float value, float expected)
        {
            _testObj.Down(StockType.InvestmentFund, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }

        [TestCase(0.000001F, 0.000001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.9999991F, 1)]
        [TestCase(1.000001F, 1.000001F)]
        //[TestCase(1.0000001F, 1.000001F)] //out of float accuracy
        public void Up_Forex(float value, float expected)
        {
            _testObj.Up(StockType.Forex, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }

        [TestCase(0.000001F, 0.000001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.9999991F, 0.999999F)]
        [TestCase(1.000001F, 1.000001F)]
        public void Down_Forex(float value, float expected)
        {
            _testObj.Down(StockType.Forex, new DateTime(2019, 12, 12), value).ShouldBe(expected);
        }
    }
}
