using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.MM;
using MarketOps.Tests.SystemExecutor.Mocks;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;

namespace MarketOps.Tests.SystemExecutor.MM
{
    [TestFixture]
    public class MMTrailingStopMinMaxOfNTests
    {
        private const int MinOfL = 5;
        private const int MaxOfH = 3;
        private const int StopMargin = 1;
        private const int PricesCount = 10;
        private readonly DateTime LastDate = DateTime.Now.Date;
        private ISystemDataLoader _dataLoader;
        private ITickAdder _tickAdder;

        private MMTrailingStopMinMaxOfN _testObj;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = SystemDataLoaderUtils.CreateSubstitute(PricesCount, LastDate);
            _tickAdder = Substitute.For<ITickAdder>();
            _tickAdder.AddTicks(default, default, default, default).ReturnsForAnyArgs(args => (float)args[2] + (int)args[3]);
            _testObj = new MMTrailingStopMinMaxOfN(MinOfL, MaxOfH, StopMargin, _dataLoader, _tickAdder);
        }

        [TestCase(1, 4)]
        [TestCase(8, 8)]
        public void CalculateCloseMode_LongPosition__CalculatesCorrectly(float currentCloseModePrice, float expected)
        {
            Position position = CreatePosition(PositionDir.Long, currentCloseModePrice);

            _testObj.CalculateCloseMode(position, LastDate);

            position.CloseMode.ShouldBe(PositionCloseMode.OnStopHit);
            position.CloseModePrice.ShouldBe(expected);
        }

        [TestCase(12, 10)]
        [TestCase(8, 8)]
        public void CalculateCloseMode_ShortPosition__CalculatesCorrectly(float currentCloseModePrice, float expected)
        {
            Position position = CreatePosition(PositionDir.Short, currentCloseModePrice);

            _testObj.CalculateCloseMode(position, LastDate);

            position.CloseMode.ShouldBe(PositionCloseMode.OnStopHit);
            position.CloseModePrice.ShouldBe(expected);
        }

        private Position CreatePosition(PositionDir dir, float closeModePrice) =>
            new Position()
            {
                Stock = new StockDefinition(),
                Direction = dir,
                DataRange = StockDataRange.Daily,
                IntradayInterval = 0,
                CloseModePrice = closeModePrice
            };
    }
}
