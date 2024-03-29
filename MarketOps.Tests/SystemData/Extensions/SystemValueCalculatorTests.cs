﻿using System;
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
    public class SystemValueCalculatorTests
    {
        private ISystemDataLoader _dataLoader;
        private SystemState _testSys;
        private StockPricesData _stockPrices;

        const float CashValue = 100;
        const float PriceL = 100;
        const float PriceH = 150;
        const int Vol = 5;
        private readonly DateTime CurrentTS = DateTime.Now.Date;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = Substitute.For<ISystemDataLoader>();
            _testSys = new SystemState() { Cash = CashValue };
            _stockPrices = new StockPricesData(1);

            _stockPrices.TS[0] = CurrentTS;
            _stockPrices.C[0] = PriceL;
            _dataLoader
                .Get(Arg.Compat.Any<string>(), Arg.Compat.Any<StockDataRange>(), Arg.Compat.Any<int>(), Arg.Compat.Any<DateTime>(), Arg.Compat.Any<DateTime>())
                .Returns(_stockPrices);
        }

        [Test]
        public void Calc_Empty__Returns0()
        {
            SystemValueCalculator.Calc(new SystemState(), DateTime.Now, _dataLoader).ShouldBe(0);
        }

        [Test]
        public void Calc_CashOnly__ReturnsCash()
        {
            SystemValueCalculator.Calc(_testSys, CurrentTS, _dataLoader).ShouldBe(CashValue);
        }

        [Test]
        public void Calc_WithClosedPosition__ClosedDoesNotCount()
        {
            _testSys.PositionsClosed.Add(new Position()
            {
                Direction = PositionDir.Long,
                TSOpen = CurrentTS.AddDays(-10),
                TSClose = CurrentTS,
                Open = PriceL,
                Close = PriceH,
                Volume = Vol
            });

            SystemValueCalculator.Calc(_testSys, CurrentTS, _dataLoader).ShouldBe(CashValue);
        }

        [TestCase(PositionDir.Long)]
        [TestCase(PositionDir.Short)]
        public void Calc_WithActivePosition__ReturnsCashAndPositionValue(PositionDir dir)
        {
            _testSys.PositionsActive.Add(new Position()
            {
                Stock = new StockDefinition(),
                Direction = dir,
                Volume = Vol
            });

            SystemValueCalculator.Calc(_testSys, CurrentTS, _dataLoader).ShouldBe(CashValue + dir.DirectionMultiplier() * PriceL * Vol);
        }

        [TestCase(PositionDir.Long)]
        [TestCase(PositionDir.Short)]
        public void Calc_WithTwoActivePositions__ReturnsCashAndPositionsValue(PositionDir dir)
        {
            const int posCount = 2;
            for (int i = 0; i < posCount; i++)
                _testSys.PositionsActive.Add(new Position()
                {
                    Stock = new StockDefinition(),
                    Direction = dir,
                    Volume = Vol
                });

            SystemValueCalculator.Calc(_testSys, CurrentTS, _dataLoader).ShouldBe(CashValue + dir.DirectionMultiplier() * PriceL * Vol * posCount);
        }
    }
}
