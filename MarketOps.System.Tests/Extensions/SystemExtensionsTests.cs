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
        const float Close1 = 25;
        private readonly DateTime CurrentTS = DateTime.Now.Date;
        private readonly DateTime CurrentTS2 = DateTime.Now.AddDays(-1).Date;

        [SetUp]
        public void SetUp()
        {
            _stock = new StockDefinition();
            _stock2 = new StockDefinition();
            _testObj = new System() { Cash = CashValue };
        }

        private void CheckOpenedPosition(Position pos, StockDefinition stock,  PositionDir dir, float open, int vol, DateTime ts, StockDataRange range, int interval)
        {
            pos.Stock.ShouldBe(stock);
            pos.Direction.ShouldBe(dir);
            pos.TSOpen.ShouldBe(ts);
            pos.Open.ShouldBe(open);
            pos.Volume.ShouldBe(vol);
            pos.DataRange.ShouldBe(range);
            pos.IntradayInterval.ShouldBe(interval);
        }

        private void CheckClosedPosition(int index, PositionDir dir, float open, float close, int vol, DateTime ts, float prevValueOnPosition)
        {
            _testObj.PositionsClosed[index].Close.ShouldBe(close);
            _testObj.PositionsClosed[index].TSClose.ShouldBe(ts);
            _testObj.ValueOnPositions[index].ShouldBe(prevValueOnPosition + (close - open) * vol * (dir == PositionDir.Long ? 1 : -1));
        }

        [TestCase(PositionDir.Long, 100, 10, StockDataRange.Daily, 0)]
        [TestCase(PositionDir.Short, 100, 10, StockDataRange.Weekly, 0)]
        [TestCase(PositionDir.Long, 10, 20, StockDataRange.Intraday, 60)]
        public void Open__AddsToActive_SubsCash(PositionDir dir, float price, int vol, StockDataRange range, int intradayInterval)
        {
            _testObj.Open(_stock, dir, CurrentTS, price, vol, range, intradayInterval);
            _testObj.Cash.ShouldBe(CashValue - price * vol);
            _testObj.PositionsActive.Count.ShouldBe(1);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, dir, price, vol, CurrentTS, range, intradayInterval);
        }

        [Test]
        public void Open_Twice__AddsToActive_SubsCash()
        {
            _testObj.Open(_stock, PositionDir.Long, CurrentTS, Price1, Vol1, StockDataRange.Daily, 0);
            _testObj.Cash.ShouldBe(CashValue - Price1 * Vol1);
            _testObj.PositionsActive.Count.ShouldBe(1);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, PositionDir.Long, Price1, Vol1, CurrentTS, StockDataRange.Daily, 0);

            _testObj.Open(_stock2, PositionDir.Short, CurrentTS2, Price2, Vol2, StockDataRange.Daily, 10);
            _testObj.Cash.ShouldBe(CashValue - Price1 * Vol1 - Price2 * Vol2);
            _testObj.PositionsActive.Count.ShouldBe(2);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, PositionDir.Long, Price1, Vol1, CurrentTS, StockDataRange.Daily, 0);
            CheckOpenedPosition(_testObj.PositionsActive[1], _stock2, PositionDir.Short, Price2, Vol2, CurrentTS2, StockDataRange.Daily, 10);
        }

        [TestCase(PositionDir.Long, 100, 150, 10)]
        [TestCase(PositionDir.Long, 150, 100, 10)]
        [TestCase(PositionDir.Short, 100, 150, 10)]
        [TestCase(PositionDir.Short, 150, 100, 10)]
        public void Close__MovesToClosed_AddsCash_AddsValueOnPosition(PositionDir dir, float open, float close, int vol)
        {
            Position pos = new Position()
            {
                Direction = dir,
                Open = open,
                Volume = vol
            };
            _testObj.PositionsActive.Add(pos);
            _testObj.Close(0, CurrentTS, close);
            _testObj.Cash.ShouldBe(CashValue + (close - open) * vol * (dir == PositionDir.Long ? 1 : -1));
            _testObj.PositionsActive.Count.ShouldBe(0);
            _testObj.PositionsClosed.Count.ShouldBe(1);
            _testObj.ValueOnPositions.Count.ShouldBe(1);
            CheckClosedPosition(0, dir, open, close, vol, CurrentTS, 0);
        }

        [Test]
        public void Close_Twice__MovesToClosed_AddsCash_AddsValueOnPosition()
        {
            Position pos = new Position()
            {
                Direction = PositionDir.Long,
                Open = Price1,
                Volume = Vol1
            };
            Position pos2 = new Position()
            {
                Direction = PositionDir.Long,
                Open = Price2,
                Volume = Vol2
            };
            _testObj.PositionsActive.Add(pos);
            _testObj.PositionsActive.Add(pos2);
            _testObj.Close(0, CurrentTS, Close1);
            _testObj.Cash.ShouldBe(CashValue + (Close1 - Price1) * Vol1);
            _testObj.PositionsActive.Count.ShouldBe(1);
            _testObj.PositionsClosed.Count.ShouldBe(1);
            _testObj.ValueOnPositions.Count.ShouldBe(1);
            CheckClosedPosition(0, PositionDir.Long, Price1, Close1, Vol1, CurrentTS, 0);

            _testObj.Close(0, CurrentTS2, Close1);
            _testObj.Cash.ShouldBe(CashValue + (Close1 - Price1) * Vol1 + (Close1 - Price2) * Vol2);
            _testObj.PositionsActive.Count.ShouldBe(0);
            _testObj.PositionsClosed.Count.ShouldBe(2);
            _testObj.ValueOnPositions.Count.ShouldBe(2);
            CheckClosedPosition(0, PositionDir.Long, Price1, Close1, Vol1, CurrentTS, 0);
            CheckClosedPosition(1, PositionDir.Long, Price2, Close1, Vol2, CurrentTS2, _testObj.ValueOnPositions[0]);
        }

        [TestCase(0, 0)]
        [TestCase(10, -1)]
        [TestCase(10, 10)]
        public void Close_IndexOutOfRange__Throws(int positionsCount, int index)
        {
            for (int i = 0; i < positionsCount; i++)
                _testObj.PositionsActive.Add(new Position());
            Should.Throw<ArgumentOutOfRangeException>(() => _testObj.Close(index, CurrentTS, Close1));
        }

        [Test]
        public void CloseAll__MovesToClosed_AddsCash_AddsValueOnPosition()
        {
            Position pos = new Position()
            {
                Direction = PositionDir.Long,
                Open = Price1,
                Volume = Vol1
            };
            Position pos2 = new Position()
            {
                Direction = PositionDir.Long,
                Open = Price2,
                Volume = Vol2
            };
            _testObj.PositionsActive.Add(pos);
            _testObj.PositionsActive.Add(pos2);
            _testObj.CloseAll(CurrentTS, Close1);
            _testObj.Cash.ShouldBe(CashValue + (Close1 - Price1) * Vol1 + (Close1 - Price2) * Vol2);
            _testObj.PositionsActive.Count.ShouldBe(0);
            _testObj.PositionsClosed.Count.ShouldBe(2);
            _testObj.ValueOnPositions.Count.ShouldBe(2);
            CheckClosedPosition(0, PositionDir.Long, Price1, Close1, Vol1, CurrentTS, 0);
            CheckClosedPosition(1, PositionDir.Long, Price2, Close1, Vol2, CurrentTS, _testObj.ValueOnPositions[0]);
        }
    }
}
