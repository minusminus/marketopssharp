using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.System.Extensions;
using MarketOps.StockData.Types;

namespace MarketOps.System.Tests.Extensions
{
    [TestFixture]
    public class SystemExtensionsTests
    {
        private System _testObj;
        private StockDefinition _stock;
        private StockDefinition _stock2;

        const float CashValue = 100;
        const float Price1 = 10;
        const int Vol1 = 2;
        const float Price2 = 20;
        const int Vol2 = 1;
        private readonly DateTime CurrentTS = DateTime.Now.Date;
        private readonly DateTime CurrentTS2 = DateTime.Now.AddDays(-1).Date;

        [SetUp]
        public void SetUp()
        {
            _stock = new StockDefinition();
            _stock2 = new StockDefinition();
            _testObj = new System() { Cash = CashValue };
        }

        private void CheckPosition(Position pos, StockDefinition stock,  PositionDir dir, float open, int vol, DateTime ts, StockDataRange range, int interval)
        {
            pos.Stock.ShouldBe(stock);
            pos.Direction.ShouldBe(dir);
            pos.TSOpen.ShouldBe(ts);
            pos.Open.ShouldBe(open);
            pos.Volume.ShouldBe(vol);
            pos.DataRange.ShouldBe(range);
            pos.IntradayInterval.ShouldBe(interval);
        }

        [Test]
        public void Open__AddsToActive_SubsCash()
        {
            _testObj.Open(_stock, PositionDir.Long, CurrentTS, Price1, Vol1, StockDataRange.Daily, 0);
            _testObj.Cash.ShouldBe(CashValue - Price1 * Vol1);
            _testObj.PositionsActive.Count.ShouldBe(1);
            CheckPosition(_testObj.PositionsActive[0], _stock, PositionDir.Long, Price1, Vol1, CurrentTS, StockDataRange.Daily, 0);
        }

        [Test]
        public void OpenTwice__AddsToActive_SubsCash()
        {
            _testObj.Open(_stock, PositionDir.Long, CurrentTS, Price1, Vol1, StockDataRange.Daily, 0);
            _testObj.Open(_stock2, PositionDir.Short, CurrentTS2, Price2, Vol2, StockDataRange.Daily, 10);
            _testObj.Cash.ShouldBe(CashValue - Price1 * Vol1 - Price2 * Vol2);
            _testObj.PositionsActive.Count.ShouldBe(2);
            CheckPosition(_testObj.PositionsActive[0], _stock, PositionDir.Long, Price1, Vol1, CurrentTS, StockDataRange.Daily, 0);
            CheckPosition(_testObj.PositionsActive[1], _stock2, PositionDir.Short, Price2, Vol2, CurrentTS2, StockDataRange.Daily, 10);
        }

        [TestCase(PositionDir.Long, 100, 150, 10)]
        [TestCase(PositionDir.Long, 150, 100, 10)]
        [TestCase(PositionDir.Short, 100, 150, 10)]
        [TestCase(PositionDir.Short, 150, 100, 10)]
        public void Close__MovesToClosed_AddsCash_AddsValueOsPosition(PositionDir dir, float open, float close, int vol)
        {
            Position pos = new Position()
            {
                Direction = dir,
                Open = open,
                Volume = vol
            };
            _testObj.PositionsActive.Add(pos);
            _testObj.Close(0, CurrentTS, close);
            _testObj.PositionsActive.Count.ShouldBe(0);
            _testObj.PositionsClosed.Count.ShouldBe(1);
            _testObj.PositionsClosed[0].Close.ShouldBe(close);
            _testObj.PositionsClosed[0].TSClose.ShouldBe(CurrentTS);
            _testObj.Cash.ShouldBe(CashValue + (close - open) * vol * (dir == PositionDir.Long ? 1 : -1));
            _testObj.ValueOnPositions.Count.ShouldBe(1);
            _testObj.ValueOnPositions[0].ShouldBe((close - open) * vol * (dir == PositionDir.Long ? 1 : -1));
        }
    }
}
