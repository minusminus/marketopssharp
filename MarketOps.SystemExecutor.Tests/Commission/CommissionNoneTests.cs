using MarketOps.SystemExecutor.Commission;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;

namespace MarketOps.SystemExecutor.Tests.Commission
{
    [TestFixture]
    public class CommissionNoneTests
    {
        private readonly CommissionNone _testObj = new CommissionNone();

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(10, 10)]
        [TestCase(100, 100)]
        public void Calculate__Returns0(int volume, float price)
        {
            _testObj.Calculate(StockType.Stock, volume, price).ShouldBe(0);
        }
    }
}
