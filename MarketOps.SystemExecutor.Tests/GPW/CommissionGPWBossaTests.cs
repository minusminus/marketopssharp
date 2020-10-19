using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;
using MarketOps.SystemExecutor.GPW;

namespace MarketOps.SystemExecutor.Tests.GPW
{
    [TestFixture]
    public class CommissionGPWBossaTests
    {
        private readonly CommissionGPWBossa _testObj = new CommissionGPWBossa();

        [TestCase(1, 100, 5)]
        [TestCase(100, 1, 5)]
        [TestCase(1, 1315, 5)]
        [TestCase(1, 1316, 5)]
        [TestCase(1, 1318.43f, 5.01f)]
        [TestCase(100, 138.43f, 52.6f)]
        public void Calculate_Stock__CorrectCommission(int volume, float price, float expected)
        {
            _testObj.Calculate(StockType.Stock, volume, price).ShouldBe(expected);
        }

        [TestCase(1, 1, 9.9f)]
        [TestCase(10, 100, 99)]
        [TestCase(25, 2154.34f, 247.5f)]
        public void Calculate_IndexFuture__CorrectCommission(int volume, float price, float expected)
        {
            _testObj.Calculate(StockType.IndexFuture, volume, price).ShouldBe(expected);
        }

        [Test]
        public void Calculate_OtherTypes__Retunrs0()
        {
            _testObj.Calculate(StockType.Undefined, 10, 1234).ShouldBe(0, StockType.Undefined.ToString());
            _testObj.Calculate(StockType.Index, 10, 1234).ShouldBe(0, StockType.Index.ToString());
            _testObj.Calculate(StockType.InvestmentFund, 10, 1234).ShouldBe(0, StockType.InvestmentFund.ToString());
            _testObj.Calculate(StockType.NBPCurrency, 10, 1234).ShouldBe(0, StockType.NBPCurrency.ToString());
            _testObj.Calculate(StockType.Forex, 10, 1234).ShouldBe(0, StockType.Forex.ToString());
        }
    }
}
