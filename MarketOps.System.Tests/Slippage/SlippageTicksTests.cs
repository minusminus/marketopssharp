using NUnit.Framework;
using Shouldly;
using MarketOps.System.Slippage;
using MarketOps.System.Interfaces;
using NSubstitute;
using MarketOps.StockData.Types;
using System;

namespace MarketOps.System.Tests.Slippage
{
    [TestFixture]
    public class SlippageTicksTests
    {
        private ITickAdder _tickAdder;
        private readonly DateTime _dt = DateTime.Now;

        [SetUp]
        public void SetUp()
        {
            _tickAdder = Substitute.For<ITickAdder>();
            _tickAdder
                .AddTicks(Arg.Compat.Any<StockType>(), Arg.Compat.Any<DateTime>(), Arg.Compat.Any<float>(), Arg.Compat.Any<int>())
                .Returns<float>(x => x.ArgAt<float>(2) + x.ArgAt<int>(3));
        }

        [TestCase(PositionDir.Long, 100, 0, 100)]
        [TestCase(PositionDir.Long, 100, 1, 99)]
        [TestCase(PositionDir.Long, 100, 2, 98)]
        [TestCase(PositionDir.Long, 100, 10, 90)]
        [TestCase(PositionDir.Short, 100, 0, 100)]
        [TestCase(PositionDir.Short, 100, 1, 101)]
        [TestCase(PositionDir.Short, 100, 2, 102)]
        [TestCase(PositionDir.Short, 100, 10, 110)]
        public void CalculateClose__ValueChanged(PositionDir dir, float value, int ticks, float expected)
        {
            SlippageTicks testObj = new SlippageTicks(_tickAdder, ticks);
            testObj.CalculateClose(StockType.Stock, _dt, dir, value).ShouldBe(expected);
        }

        [TestCase(PositionDir.Long, 100, 0, 100)]
        [TestCase(PositionDir.Long, 100, 1, 101)]
        [TestCase(PositionDir.Long, 100, 2, 102)]
        [TestCase(PositionDir.Long, 100, 10, 110)]
        [TestCase(PositionDir.Short, 100, 0, 100)]
        [TestCase(PositionDir.Short, 100, 1, 99)]
        [TestCase(PositionDir.Short, 100, 2, 98)]
        [TestCase(PositionDir.Short, 100, 10, 90)]
        public void CalculateOpen__ValueChanged(PositionDir dir, float value, int ticks, float expected)
        {
            SlippageTicks testObj = new SlippageTicks(_tickAdder, ticks);
            testObj.CalculateOpen(StockType.Stock, _dt, dir, value).ShouldBe(expected);
        }
    }
}
