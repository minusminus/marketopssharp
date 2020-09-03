using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.System.Extensions;
using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;
using MarketOps.System.Tests.Mocks;

namespace MarketOps.System.Tests.Extensions
{
    [TestFixture]
    public class SystemEquityExtensionsTests
    {
        private SystemState _testObj;
        private StockDefinition _stock;
        private StockDefinition _stock2;
        private StockPricesData _stockPrices;
        private IDataLoader _dataLoader;
        private ICommission _commission;
        private ISlippage _slippage;

        private const float CashValue = 100;
        private const float Price1 = 10;
        private const int Vol1 = 2;
        private const float Price2 = 20;
        private const int Vol2 = 1;
        private const float Close1 = 25;
        private const float Commission = 1;
        private readonly DateTime CurrentTS = DateTime.Now.Date;
        private readonly DateTime CurrentTS2 = DateTime.Now.AddDays(-1).Date;
        private readonly Signal EntrySignal = new Signal();

        [SetUp]
        public void SetUp()
        {
            _stock = new StockDefinition();
            _stock2 = new StockDefinition();
            _testObj = new SystemState() { Cash = CashValue };
            _stockPrices = new StockPricesData(1);
            _dataLoader = DataLoaderUtils.CreateSubstitute(_stockPrices);
            _commission = CommissionUtils.CreateSubstitute(Commission);
            _slippage = SlippageUtils.CreateSusbstitute();
        }

        private void CheckOpenedPosition(Position pos, StockDefinition stock, PositionDir dir, float open, int vol, float commission, DateTime ts, StockDataRange range, int interval)
        {
            pos.Stock.ShouldBe(stock);
            pos.Direction.ShouldBe(dir);
            pos.TSOpen.ShouldBe(ts);
            pos.Open.ShouldBe(open);
            pos.OpenCommission.ShouldBe(commission);
            pos.Volume.ShouldBe(vol);
            pos.DataRange.ShouldBe(range);
            pos.IntradayInterval.ShouldBe(interval);
            pos.EntrySignal.ShouldBe(EntrySignal);
        }

        private void CheckClosedPosition(int index, PositionDir dir, float open, float close, int vol, float commission, DateTime ts, float prevValueOnPosition)
        {
            _testObj.PositionsClosed[index].Close.ShouldBe(close);
            _testObj.PositionsClosed[index].CloseCommission.ShouldBe(commission);
            _testObj.PositionsClosed[index].TSClose.ShouldBe(ts);
            _testObj.ClosedPositionsEquity[index].Value.ShouldBe(prevValueOnPosition + (close - open) * vol * (dir == PositionDir.Long ? 1 : -1) - commission);
            _testObj.ClosedPositionsEquity[index].TS.ShouldBe(ts);
        }

        private Position CreatePosition(PositionDir dir, float open, int vol) => new Position()
        {
            Stock = _stock,
            Direction = dir,
            Open = open,
            Volume = vol
        };

        [TestCase(PositionDir.Long, 100, 10, StockDataRange.Daily, 0)]
        [TestCase(PositionDir.Short, 100, 10, StockDataRange.Weekly, 0)]
        [TestCase(PositionDir.Long, 10, 20, StockDataRange.Intraday, 60)]
        public void Open__AddsToActive_SubsCash(PositionDir dir, float price, int vol, StockDataRange range, int intradayInterval)
        {
            _testObj.Open(_stock, dir, CurrentTS, price, vol, Commission, range, intradayInterval, EntrySignal);
            _testObj.Cash.ShouldBe(CashValue - price * vol - Commission);
            _testObj.PositionsActive.Count.ShouldBe(1);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, dir, price, vol, Commission, CurrentTS, range, intradayInterval);
        }

        [Test]
        public void Open_Twice__AddsToActive_SubsCash()
        {
            _testObj.Open(_stock, PositionDir.Long, CurrentTS, Price1, Vol1, Commission, StockDataRange.Daily, 0, EntrySignal);
            _testObj.Cash.ShouldBe(CashValue - Price1 * Vol1 - Commission);
            _testObj.PositionsActive.Count.ShouldBe(1);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, PositionDir.Long, Price1, Vol1, Commission, CurrentTS, StockDataRange.Daily, 0);

            _testObj.Open(_stock2, PositionDir.Short, CurrentTS2, Price2, Vol2, Commission, StockDataRange.Daily, 10, EntrySignal);
            _testObj.Cash.ShouldBe(CashValue - Price1 * Vol1 - Price2 * Vol2 - Commission * 2);
            _testObj.PositionsActive.Count.ShouldBe(2);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, PositionDir.Long, Price1, Vol1, Commission, CurrentTS, StockDataRange.Daily, 0);
            CheckOpenedPosition(_testObj.PositionsActive[1], _stock2, PositionDir.Short, Price2, Vol2, Commission, CurrentTS2, StockDataRange.Daily, 10);
        }

        [TestCase(PositionDir.Long, 100, 150, 10)]
        [TestCase(PositionDir.Long, 150, 100, 10)]
        [TestCase(PositionDir.Short, 100, 150, 10)]
        [TestCase(PositionDir.Short, 150, 100, 10)]
        public void Close__MovesToClosed_AddsCash_AddsValueOnPosition(PositionDir dir, float open, float close, int vol)
        {
            Position pos = CreatePosition(dir, open, vol);
            _testObj.PositionsActive.Add(pos);
            _testObj.Close(0, CurrentTS, close, Commission);
            _testObj.Cash.ShouldBe(CashValue + close * vol - Commission);
            _testObj.PositionsActive.Count.ShouldBe(0);
            _testObj.PositionsClosed.Count.ShouldBe(1);
            _testObj.ClosedPositionsEquity.Count.ShouldBe(1);
            CheckClosedPosition(0, dir, open, close, vol, Commission, CurrentTS, 0);
        }

        [Test]
        public void Close_Twice__MovesToClosed_AddsCash_AddsValueOnPosition()
        {
            Position pos = CreatePosition(PositionDir.Long, Price1, Vol1);
            Position pos2 = CreatePosition(PositionDir.Long, Price2, Vol2);
            _testObj.PositionsActive.Add(pos);
            _testObj.PositionsActive.Add(pos2);
            _testObj.Close(0, CurrentTS, Close1, Commission);
            _testObj.Cash.ShouldBe(CashValue + Close1 * Vol1 - Commission);
            _testObj.PositionsActive.Count.ShouldBe(1);
            _testObj.PositionsClosed.Count.ShouldBe(1);
            _testObj.ClosedPositionsEquity.Count.ShouldBe(1);
            CheckClosedPosition(0, PositionDir.Long, Price1, Close1, Vol1, Commission, CurrentTS, 0);

            _testObj.Close(0, CurrentTS2, Close1, Commission);
            _testObj.Cash.ShouldBe(CashValue + Close1 * Vol1 + Close1 * Vol2 - Commission * 2);
            _testObj.PositionsActive.Count.ShouldBe(0);
            _testObj.PositionsClosed.Count.ShouldBe(2);
            _testObj.ClosedPositionsEquity.Count.ShouldBe(2);
            CheckClosedPosition(0, PositionDir.Long, Price1, Close1, Vol1, Commission, CurrentTS, 0);
            CheckClosedPosition(1, PositionDir.Long, Price2, Close1, Vol2, Commission, CurrentTS2, _testObj.ClosedPositionsEquity[0].Value);
        }

        [TestCase(0, 0)]
        [TestCase(10, -1)]
        [TestCase(10, 10)]
        public void Close_IndexOutOfRange__Throws(int positionsCount, int index)
        {
            for (int i = 0; i < positionsCount; i++)
                _testObj.PositionsActive.Add(new Position());
            Should.Throw<ArgumentOutOfRangeException>(() => _testObj.Close(index, CurrentTS, Close1, Commission));
        }

        [Test]
        public void CloseAll__MovesToClosed_AddsCash_AddsValueOnPosition()
        {
            Position pos = CreatePosition(PositionDir.Long, Price1, Vol1);
            Position pos2 = CreatePosition(PositionDir.Long, Price2, Vol2);
            _testObj.PositionsActive.Add(pos);
            _testObj.PositionsActive.Add(pos2);
            _testObj.CloseAll(CurrentTS, Close1, _slippage, _commission);
            _testObj.Cash.ShouldBe(CashValue + Close1 * Vol1 + Close1 * Vol2 - Commission * 2);
            _testObj.PositionsActive.Count.ShouldBe(0);
            _testObj.PositionsClosed.Count.ShouldBe(2);
            _testObj.ClosedPositionsEquity.Count.ShouldBe(2);
            CheckClosedPosition(0, PositionDir.Long, Price1, Close1, Vol1, Commission, CurrentTS, 0);
            CheckClosedPosition(1, PositionDir.Long, Price2, Close1, Vol2, Commission, CurrentTS, _testObj.ClosedPositionsEquity[0].Value);
        }

        [Test]
        public void CalcCurrentValue_NoActivePositions__CalculatesOnlyCash()
        {
            _stockPrices.TS[0] = CurrentTS;
            _stockPrices.C[0] = Close1;
            _testObj.CalcCurrentValue(CurrentTS, _dataLoader);
            _testObj.Equity.Count.ShouldBe(1);
            _testObj.Equity[0].Value.ShouldBe(CashValue);
            _testObj.Equity[0].TS.ShouldBe(CurrentTS);
        }

        [Test]
        public void CalcCurrentValue_WithActivePositions__CalculatesCashAndPosition()
        {
            _testObj.PositionsActive.Add(CreatePosition(PositionDir.Long, Price1, Vol1));
            _stockPrices.TS[0] = CurrentTS;
            _stockPrices.C[0] = Close1;
            _testObj.CalcCurrentValue(CurrentTS, _dataLoader);
            _testObj.Equity.Count.ShouldBe(1);
            _testObj.Equity[0].Value.ShouldBe(CashValue + Close1 * Vol1);
            _testObj.Equity[0].TS.ShouldBe(CurrentTS);
        }

        [Test]
        public void CalcCurrentValue_Multiple_MixedCase__CalculatesCashAndPosition()
        {
            _testObj.PositionsActive.Add(CreatePosition(PositionDir.Long, Price1, Vol1));
            _stockPrices.TS[0] = CurrentTS;
            _stockPrices.C[0] = Close1;
            _testObj.CalcCurrentValue(CurrentTS, _dataLoader);
            _testObj.Equity.Count.ShouldBe(1);
            _testObj.Equity[0].Value.ShouldBe(CashValue + Close1 * Vol1);
            _testObj.Equity[0].TS.ShouldBe(CurrentTS);

            _testObj.CloseAll(CurrentTS2, Close1, _slippage, CommissionUtils.CreateSubstitute());
            _stockPrices.TS[0] = CurrentTS2;
            _stockPrices.C[0] = Close1;
            _testObj.CalcCurrentValue(CurrentTS2, _dataLoader);
            _testObj.Equity.Count.ShouldBe(2);
            _testObj.Equity[0].Value.ShouldBe(CashValue + Close1 * Vol1);
            _testObj.Equity[0].TS.ShouldBe(CurrentTS);
            _testObj.Equity[1].Value.ShouldBe(CashValue + Close1 * Vol1);
            _testObj.Equity[1].TS.ShouldBe(CurrentTS2);
        }
    }
}
