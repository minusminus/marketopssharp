﻿using MarketOps.SystemData.Types;
using MarketOps.SystemExecutor.Processor;
using MarketOps.Tests.SystemExecutor.Mocks;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.Tests.SystemExecutor.Processor
{
    [TestFixture]
    public class ClosingPositionSelectorTests
    {
        [Test]
        public void OnOpen_CloseOnOpen__ReturnsTrue()
        {
            ClosingPositionSelector.OnOpen(new Position() { CloseMode = PositionCloseMode.OnOpen }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnOpen_NotCloseOnOpen__ReturnsFalse()
        {
            ClosingPositionSelector.OnOpen(new Position() { CloseMode = PositionCloseMode.OnClose }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnClose_CloseOnClose__ReturnsTrue()
        {
            ClosingPositionSelector.OnClose(new Position() { CloseMode = PositionCloseMode.OnClose }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeTrue();
        }

        [Test]
        public void OnClose_NotCloseOnClose__ReturnsFalse()
        {
            ClosingPositionSelector.OnClose(new Position() { CloseMode = PositionCloseMode.OnOpen }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [Test]
        public void OnStopHit_NotCloseOnPrice__ReturnsFalse()
        {
            ClosingPositionSelector.OnStopHit(new Position() { CloseMode = PositionCloseMode.OnClose }, StockPricesDataUtils.CreatePricesData(0, 0, 0, 0), 0).ShouldBeFalse();
        }

        [TestCase(PositionDir.Long, 75, true)]
        [TestCase(PositionDir.Long, 125, true)]
        [TestCase(PositionDir.Long, 25, false)]
        [TestCase(PositionDir.Short, 75, true)]
        [TestCase(PositionDir.Short, 125, false)]
        [TestCase(PositionDir.Short, 25, true)]
        public void OnStopHit(PositionDir positionDir, float closeModePrice, bool expected)
        {
            ClosingPositionSelector.OnStopHit(
                new Position() { Direction = positionDir, CloseMode = PositionCloseMode.OnStopHit, CloseModePrice = closeModePrice },
                StockPricesDataUtils.CreatePricesData(0, 100, 50, 0),
                0).ShouldBe(expected);
        }

        [TestCase(PositionDir.Long, 125, 1, true)]
        [TestCase(PositionDir.Long, 125, 2, false)]
        [TestCase(PositionDir.Long, 25, 1, false)]
        [TestCase(PositionDir.Long, 25, 2, false)]
        [TestCase(PositionDir.Short, 125, 1, false)]
        [TestCase(PositionDir.Short, 125, 2, false)]
        [TestCase(PositionDir.Short, 25, 1, true)]
        [TestCase(PositionDir.Short, 25, 2, false)]
        public void OnStopHitInFirstTick(PositionDir positionDir, float closeModePrice, int ticksActive, bool expected)
        {
            ClosingPositionSelector.OnStopHitInFirstTick(
                new Position() { Direction = positionDir, CloseMode = PositionCloseMode.OnStopHit, CloseModePrice = closeModePrice, TicksActive = ticksActive },
                StockPricesDataUtils.CreatePricesData(0, 100, 50, 0),
                0).ShouldBe(expected);
        }
    }
}
