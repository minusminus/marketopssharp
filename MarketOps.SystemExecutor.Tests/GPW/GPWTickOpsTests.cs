using System;
using MarketOps.SystemExecutor.GPW;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;

namespace MarketOps.SystemExecutor.Tests.GPW
{
    [TestFixture]
    public class GPWTickOpsTests
    {
        private readonly GPWTickOps _testObj = new GPWTickOps();

        private readonly DateTime _dateBefore20190304 = new DateTime(2019, 03, 03);
        private readonly DateTime _dateAfter20190304 = new DateTime(2019, 12, 12);

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
        public void AlignUp_Stock_Before20190304(float value, float expected)
        {
            _testObj.AlignUp(StockType.Stock, _dateBefore20190304, value).ShouldBe(expected);
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
        public void AlignDown_Stock_Before20190304(float value, float expected)
        {
            _testObj.AlignDown(StockType.Stock, _dateBefore20190304, value).ShouldBe(expected);
        }

        [TestCase(1, 1)]
        [TestCase(0.99F, 0.99F)]
        [TestCase(1.001F, 1.01F)]
        [TestCase(0.991F, 1)]
        [TestCase(1.991F, 2)]
        public void AlignUp_Index(float value, float expected)
        {
            _testObj.AlignUp(StockType.Index, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(1, 1)]
        [TestCase(0.99F, 0.99F)]
        [TestCase(1.001F, 1)]
        [TestCase(0.991F, 0.99F)]
        [TestCase(1.991F, 1.99F)]
        public void AlignDown_Index(float value, float expected)
        {
            _testObj.AlignDown(StockType.Index, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(1, 1)]
        [TestCase(0.99F, 1)]
        [TestCase(1.001F, 2)]
        [TestCase(0.991F, 1)]
        [TestCase(1.991F, 2)]
        [TestCase(2.001F, 3)]
        public void AlignUp_Future(float value, float expected)
        {
            _testObj.AlignUp(StockType.IndexFuture, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(1, 1)]
        [TestCase(0.99F, 1)]
        [TestCase(1.001F, 1)]
        [TestCase(0.991F, 1)]
        [TestCase(1.991F, 1)]
        [TestCase(2.001F, 2)]
        public void AlignDown_Future(float value, float expected)
        {
            _testObj.AlignDown(StockType.IndexFuture, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(0.0001F, 0.0001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.99991F, 1)]
        [TestCase(1.00001F, 1.0001F)]
        public void AlignUp_InvestmentFund(float value, float expected)
        {
            _testObj.AlignUp(StockType.InvestmentFund, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(0.0001F, 0.0001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.99991F, 0.9999F)]
        [TestCase(1.00001F, 1)]
        public void AlignDown_InvestmentFund(float value, float expected)
        {
            _testObj.AlignDown(StockType.InvestmentFund, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(0.0001F, 0.0001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.99991F, 1)]
        [TestCase(1.00001F, 1.0001F)]
        public void AlignUp_NBPCurrency(float value, float expected)
        {
            _testObj.AlignUp(StockType.NBPCurrency, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(0.0001F, 0.0001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.99991F, 0.9999F)]
        [TestCase(1.00001F, 1)]
        public void AlignDown_NBPCurrency(float value, float expected)
        {
            _testObj.AlignDown(StockType.InvestmentFund, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(0.000001F, 0.000001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.9999991F, 1)]
        [TestCase(1.000001F, 1.000001F)]
        //[TestCase(1.0000001F, 1.000001F)] //out of float accuracy
        public void AlignUp_Forex(float value, float expected)
        {
            _testObj.AlignUp(StockType.Forex, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(0.000001F, 0.000001F)]
        [TestCase(0.001F, 0.001F)]
        [TestCase(1, 1)]
        [TestCase(0.9999991F, 0.999999F)]
        [TestCase(1.000001F, 1.000001F)]
        public void AlignDown_Forex(float value, float expected)
        {
            _testObj.AlignDown(StockType.Forex, _dateAfter20190304, value).ShouldBe(expected);
        }

        [TestCase(1, 0, 1f)]
        [TestCase(1, 1, 1.01f)]
        [TestCase(1, 10, 1.1f)]
        [TestCase(2, 1, 2.02F)]
        [TestCase(5, 1, 5.05F)]
        [TestCase(10, 1, 10.1F)]
        [TestCase(20, 1, 20.2F)]
        [TestCase(50, 1, 50.5F)]
        [TestCase(100, 1, 101)]
        [TestCase(200, 1, 202)]
        [TestCase(500, 1, 505)]
        [TestCase(1000, 1, 1010)]
        [TestCase(2000, 1, 2020)]
        [TestCase(5000, 1, 5050)]
        [TestCase(10000, 1, 10100)]
        [TestCase(20000, 1, 20200)]
        [TestCase(50000, 1, 50500)]
        [TestCase(1, -1, 0.99f)]
        [TestCase(1, -10, 0.9f)]
        [TestCase(2, -1, 1.98F)]
        [TestCase(5, -1, 4.95F)]
        [TestCase(10, -1, 9.9F)]
        [TestCase(20, -1, 19.8F)]
        [TestCase(50, -1, 49.5F)]
        [TestCase(100, -1, 99)]
        [TestCase(200, -1, 198)]
        [TestCase(500, -1, 495)]
        [TestCase(1000, -1, 990)]
        [TestCase(2000, -1, 1980)]
        [TestCase(5000, -1, 4950)]
        [TestCase(10000, -1, 9900)]
        [TestCase(20000, -1, 19800)]
        [TestCase(50000, -1, 49500)]
        public void AddTicks_Stock_Before20190304(float value, int ticks, float expected)
        {
            _testObj.AddTicks(StockType.Stock, _dateBefore20190304, value, ticks).ShouldBe(expected);
        }

        [TestCase(1, 0, 1f)]
        [TestCase(1, 1, 1.0002f)]
        [TestCase(1, 10, 1.002f)]
        [TestCase(2, 1, 2.0005f)]
        [TestCase(5, 1, 5.001f)]
        [TestCase(10, 1, 10.002f)]
        [TestCase(20, 1, 20.005f)]
        [TestCase(50, 1, 50.01f)]
        [TestCase(100, 1, 100.02f)]
        [TestCase(200, 1, 200.05f)]
        [TestCase(500, 1, 500.1f)]
        [TestCase(1000, 1, 1000.2f)]
        [TestCase(2000, 1, 2000.5f)]
        [TestCase(5000, 1, 5001)]
        [TestCase(10000, 1, 10002)]
        [TestCase(20000, 1, 20005)]
        [TestCase(50000, 1, 50010)]
        [TestCase(1, -1, 0.9998f)]
        [TestCase(1, -10, 0.998f)]
        [TestCase(2, -1, 1.9995f)]
        [TestCase(5, -1, 4.999f)]
        [TestCase(10, -1, 9.998f)]
        [TestCase(20, -1, 19.995f)]
        [TestCase(50, -1, 49.99f)]
        [TestCase(100, -1, 99.98f)]
        [TestCase(200, -1, 199.95f)]
        [TestCase(500, -1, 499.9f)]
        [TestCase(1000, -1, 999.8f)]
        [TestCase(2000, -1, 1999.5f)]
        [TestCase(5000, -1, 4999)]
        [TestCase(10000, -1, 9998)]
        [TestCase(20000, -1, 19995)]
        [TestCase(50000, -1, 49990)]
        public void AddTicks_Stock_After20190304(float value, int ticks, float expected)
        {
            _testObj.AddTicks(StockType.Stock, _dateAfter20190304, value, ticks).ShouldBe(expected);
        }

        [TestCase(100, 0, 100)]
        [TestCase(100, 1, 100.01f)]
        [TestCase(100, 10, 100.1f)]
        [TestCase(100, -1, 99.99f)]
        [TestCase(100, -10, 99.9f)]
        public void AddTicks_Index(float value, int ticks, float expected)
        {
            _testObj.AddTicks(StockType.Index, _dateAfter20190304, value, ticks).ShouldBe(expected);
        }

        [TestCase(100, 0, 100)]
        [TestCase(100, 1, 101)]
        [TestCase(100, 10, 110)]
        [TestCase(100, -1, 99)]
        [TestCase(100, -10, 90)]
        public void AddTicks_Future(float value, int ticks, float expected)
        {
            _testObj.AddTicks(StockType.IndexFuture, _dateAfter20190304, value, ticks).ShouldBe(expected);
        }

        [TestCase(100, 0, 100)]
        [TestCase(100, 1, 100.0001f)]
        [TestCase(100, 10, 100.001f)]
        [TestCase(100, -1, 99.9999f)]
        [TestCase(100, -10, 99.999f)]
        public void AddTicks_InvestmentFund(float value, int ticks, float expected)
        {
            _testObj.AddTicks(StockType.InvestmentFund, _dateAfter20190304, value, ticks).ShouldBe(expected);
        }

        [TestCase(100, 0, 100)]
        [TestCase(100, 1, 100.000001f)]
        [TestCase(100, 10, 100.00001f)]
        [TestCase(100, -1, 99.999999f)]
        [TestCase(100, -10, 99.99999f)]
        public void AddTicks_Forex(float value, int ticks, float expected)
        {
            _testObj.AddTicks(StockType.Forex, _dateAfter20190304, value, ticks).ShouldBe(expected);
        }

        [TestCase(100, 0, 100)]
        [TestCase(100, 1, 100.0001f)]
        [TestCase(100, 10, 100.001f)]
        [TestCase(100, -1, 99.9999f)]
        [TestCase(100, -10, 99.999f)]
        public void AddTicks_NBPCurrency(float value, int ticks, float expected)
        {
            _testObj.AddTicks(StockType.NBPCurrency, _dateAfter20190304, value, ticks).ShouldBe(expected);
        }
    }
}
