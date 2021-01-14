using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.SystemExecutor.Slippage;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemExecutor.Tests.Slippage
{
    [TestFixture]
    public class SlippageNoneTests
    {
        private readonly SlippageNone _testObj = new SlippageNone();

        [TestCase(PositionDir.Long, 100)]
        [TestCase(PositionDir.Short, 200)]
        [TestCase(PositionDir.Long, 0)]
        [TestCase(PositionDir.Short, 0)]
        public void CalculateClose__ValueNotChanged(PositionDir dir, float value)
        {
            _testObj.CalculateClose(StockType.Stock, DateTime.Now, dir, value).ShouldBe(value);
        }

        [TestCase(PositionDir.Long, 100)]
        [TestCase(PositionDir.Short, 200)]
        [TestCase(PositionDir.Long, 0)]
        [TestCase(PositionDir.Short, 0)]
        public void CalculateOpen__ValueNotChanged(PositionDir dir, float value)
        {
            _testObj.CalculateOpen(StockType.Stock, DateTime.Now, dir, value).ShouldBe(value);
        }
    }
}
