using System;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Types;

namespace MarketOps.Tests.SystemData.Extensions
{
    [TestFixture]
    public class SystemCapitalUsageCalculatorTests
    {
        private SystemState _testSys;

        const float CashValue = 100;

        [SetUp]
        public void SetUp()
        {
            _testSys = new SystemState() { Cash = CashValue };
        }

        [Test]
        public void Calc_Empty__Returns0()
        {
            SystemCapitalUsageCalculator.Calc(new SystemState()).ShouldBe(0);
        }

        [Test]
        public void Calc_CashOnly__Returns0()
        {
            SystemCapitalUsageCalculator.Calc(_testSys).ShouldBe(0);
        }

        [TestCase(PositionDir.Long)]
        [TestCase(PositionDir.Short)]
        public void Calc_WithActivePositions__ReturnsCorrectly(PositionDir dir)
        {
            const float StockPrice = 1f;
            const int StockVolume = 5;

            _testSys.PositionsActive.Add(new Position()
            {
                Stock = new StockDefinition(),
                Direction = dir,
                Open = StockPrice,
                Volume = StockVolume
            });
            _testSys.Cash -= StockPrice * StockVolume;

            SystemCapitalUsageCalculator.Calc(_testSys).ShouldBe(StockPrice * StockVolume / CashValue);
        }
    }
}
