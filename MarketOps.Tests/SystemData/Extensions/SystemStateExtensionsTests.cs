using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.SystemData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.Tests.SystemExecutor.Mocks;

namespace MarketOps.Tests.SystemData.Extensions
{
    [TestFixture]
    public class SystemStateExtensionsTests
    {
        private SystemState _testObj;
        private StockDefinition _stock;
        private StockDefinition _stock2;
        private StockPricesData _stockPrices;
        private ISystemDataLoader _dataLoader;
        private ICommission _commission;
        private ISlippage _slippage;

        private const float CashValue = 100;
        private const float Price1 = 10;
        private const int Vol1 = 2;
        private const float Price2 = 20;
        private const int Vol2 = 1;
        private const float Close1 = 25;
        private const float Commission = 1;
        private const string StockName1 = "Stock 1";
        private const string StockName2 = "Stock 2";
        private readonly DateTime CurrentTS = DateTime.Now.Date;
        private readonly DateTime CurrentTS2 = DateTime.Now.AddDays(-1).Date;
        private readonly Signal EntrySignal = new Signal()
        {
            DataRange = StockDataRange.Daily,
            IntradayInterval = 0
        };

        [SetUp]
        public void SetUp()
        {
            _stock = new StockDefinition()
            {
                Type = StockType.Stock,
                FullName = StockName1
            };
            _stock2 = new StockDefinition();
            _testObj = new SystemState() { Cash = CashValue };
            _testObj.Equity.Add(new SystemValue() { Value = CashValue });
            _stockPrices = new StockPricesData(1);
            _dataLoader = SystemDataLoaderUtils.CreateSubstitute(_stockPrices);
            _commission = CommissionUtils.CreateSubstitute(Commission);
            _slippage = SlippageUtils.CreateSusbstitute();
            EntrySignal.Stock = _stock;
            EntrySignal.InitialStopMode = SignalInitialStopMode.NoStop;
            EntrySignal.InitialStopValue = 0;
        }

        private void CheckOpenedPosition(Position pos, StockDefinition stock, PositionDir dir, float open, float vol, float commission, DateTime ts, 
            StockDataRange range, int interval, PositionCloseMode closeMode, float closeModePrice)
        {
            pos.Stock.ShouldBe(stock);
            pos.Direction.ShouldBe(dir);
            pos.TSOpen.ShouldBe(ts);
            pos.Open.ShouldBe(open);
            pos.OpenCommission.ShouldBe(commission);
            pos.EquityValueOnTickBeforeOpen.ShouldBe(CashValue);
            pos.Volume.ShouldBe(vol);
            pos.DataRange.ShouldBe(range);
            pos.IntradayInterval.ShouldBe(interval);
            pos.EntrySignal.ShouldBe(EntrySignal);
            pos.CloseMode.ShouldBe(closeMode);
            pos.CloseModePrice.ShouldBe(closeModePrice);
        }

        private void CheckClosedPosition(int index, PositionDir dir, float open, float close, float vol, float commission, DateTime ts, float prevValueOnPosition)
        {
            _testObj.PositionsClosed[index].Close.ShouldBe(close);
            _testObj.PositionsClosed[index].CloseCommission.ShouldBe(commission);
            _testObj.PositionsClosed[index].TSClose.ShouldBe(ts);
            _testObj.ClosedPositionsEquity[index].Value.ShouldBe(prevValueOnPosition + (close - open) * vol * (dir == PositionDir.Long ? 1 : -1) - commission);
            _testObj.ClosedPositionsEquity[index].TS.ShouldBe(ts);
        }

        private Position CreatePosition(PositionDir dir, float open, float vol) => new Position()
        {
            Stock = _stock,
            Direction = dir,
            Open = open,
            Volume = vol
        };

        [TestCase(PositionDir.Long, 100, 10, StockDataRange.Daily, 0)]
        [TestCase(PositionDir.Short, 100, 10, StockDataRange.Weekly, 0)]
        [TestCase(PositionDir.Long, 10, 20, StockDataRange.Intraday, 60)]
        public void Open__AddsToActive_SubsCash(PositionDir dir, float price, float vol, StockDataRange range, int intradayInterval)
        {
            _testObj.Open(_stock, dir, CurrentTS, price, vol, Commission, range, intradayInterval, EntrySignal);
            
            _testObj.Cash.ShouldBe(CashValue - _testObj.PositionsActive[0].DirectionMultiplier() * (price * vol) - Commission);
            _testObj.PositionsActive.Count.ShouldBe(1);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, dir, price, vol, Commission, CurrentTS, range, intradayInterval, PositionCloseMode.DontClose, 0);
        }

        [Test]
        public void Open_Twice__AddsToActive_SubsCash()
        {
            _testObj.Open(_stock, PositionDir.Long, CurrentTS, Price1, Vol1, Commission, StockDataRange.Daily, 0, EntrySignal);
            _testObj.PositionsActive.Count.ShouldBe(1);
            _testObj.Cash.ShouldBe(CashValue - _testObj.PositionsActive[0].DirectionMultiplier() * (Price1 * Vol1) - Commission);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, PositionDir.Long, Price1, Vol1, Commission, CurrentTS, StockDataRange.Daily, 0, PositionCloseMode.DontClose, 0);

            _testObj.Open(_stock2, PositionDir.Short, CurrentTS2, Price2, Vol2, Commission, StockDataRange.Daily, 10, EntrySignal);
            _testObj.PositionsActive.Count.ShouldBe(2);
            _testObj.Cash.ShouldBe(CashValue - _testObj.PositionsActive[0].DirectionMultiplier() * (Price1 * Vol1) - _testObj.PositionsActive[1].DirectionMultiplier() * (Price2 * Vol2) - Commission * 2);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, PositionDir.Long, Price1, Vol1, Commission, CurrentTS, StockDataRange.Daily, 0, PositionCloseMode.DontClose, 0);
            CheckOpenedPosition(_testObj.PositionsActive[1], _stock2, PositionDir.Short, Price2, Vol2, Commission, CurrentTS2, StockDataRange.Daily, 10, PositionCloseMode.DontClose, 0);
        }

        [Test]
        public void Open_WithInitialStop__InitializesPositionStop([Values] SignalInitialStopMode initialStopMode)
        {
            EntrySignal.InitialStopMode = initialStopMode;
            EntrySignal.InitialStopValue = (initialStopMode == SignalInitialStopMode.OnPrice) ? 10f : 0;
            
            _testObj.Open(_stock, PositionDir.Long, CurrentTS, 100f, 1, Commission, StockDataRange.Daily, 0, EntrySignal);
            
            _testObj.PositionsActive.Count.ShouldBe(1);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, PositionDir.Long, 100f, 1, Commission, CurrentTS, StockDataRange.Daily, 0, 
                (initialStopMode == SignalInitialStopMode.OnPrice) ? PositionCloseMode.OnStopHit : PositionCloseMode.DontClose,
                (initialStopMode == SignalInitialStopMode.OnPrice) ? 10f : 0);
        }

        [TestCase(PositionDir.Long, 100, 150, 10)]
        [TestCase(PositionDir.Long, 150, 100, 10)]
        [TestCase(PositionDir.Short, 100, 150, 10)]
        [TestCase(PositionDir.Short, 150, 100, 10)]
        public void Close__MovesToClosed_AddsCash_AddsValueOnPosition(PositionDir dir, float open, float close, float vol)
        {
            Position pos = CreatePosition(dir, open, vol);
            _testObj.PositionsActive.Add(pos);
            
            _testObj.Close(0, CurrentTS, close, Commission);
            
            _testObj.Cash.ShouldBe(CashValue + pos.DirectionMultiplier() * (close * vol) - Commission);
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

        [TestCase(PositionDir.Long, 100, 10, 150, 5)]
        [TestCase(PositionDir.Long, 150, 10, 100, 5)]
        [TestCase(PositionDir.Short, 150, 10, 100, 5)]
        [TestCase(PositionDir.Short, 100, 10, 150, 5)]
        [TestCase(PositionDir.Long, 100, 10, 150, -5)]
        public void AddToPosition_NewVolPositive__ClosesCurrent_CreatesNewWithNewVolume(PositionDir dir, float open, float vol, float addPrice, float addVol)
        {
            Position pos = CreatePosition(dir, open, vol);
            _testObj.PositionsActive.Add(pos);
            
            _testObj.AddToPosition(0, CurrentTS, addPrice, addVol, EntrySignal, _slippage, _commission);
            
            _testObj.Cash.ShouldBe(CashValue - pos.DirectionMultiplier() * (addPrice * addVol) - 2 * Commission);
            _testObj.PositionsClosed.Count.ShouldBe(1);
            _testObj.ClosedPositionsEquity.Count.ShouldBe(1);
            CheckClosedPosition(0, dir, open, addPrice, vol, Commission, CurrentTS, 0);
            _testObj.PositionsActive.Count.ShouldBe(1);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, dir, addPrice, vol + addVol, Commission, CurrentTS, StockDataRange.Daily, 0, PositionCloseMode.DontClose, 0);
        }

        [TestCase(PositionDir.Long, 100, 10, 150, -15)]
        [TestCase(PositionDir.Long, 150, 10, 100, -15)]
        [TestCase(PositionDir.Short, 150, 10, 100, -15)]
        [TestCase(PositionDir.Short, 100, 10, 150, -15)]
        public void AddToPosition_NewVolNegative__ClosesCurrent(PositionDir dir, float open, float vol, float addPrice, float addVol)
        {
            Position pos = CreatePosition(dir, open, vol);
            _testObj.PositionsActive.Add(pos);
            
            _testObj.AddToPosition(0, CurrentTS, addPrice, addVol, EntrySignal, _slippage, _commission);
            
            _testObj.Cash.ShouldBe(CashValue + pos.DirectionMultiplier() * (addPrice * vol) - Commission);
            _testObj.PositionsClosed.Count.ShouldBe(1);
            _testObj.ClosedPositionsEquity.Count.ShouldBe(1);
            CheckClosedPosition(0, dir, open, addPrice, vol, Commission, CurrentTS, 0);
            _testObj.PositionsActive.Count.ShouldBe(0);
        }

        [TestCase(PositionDir.Long, 100, 10, 150, 5)]
        [TestCase(PositionDir.Long, 150, 10, 100, 5)]
        [TestCase(PositionDir.Short, 150, 10, 100, 5)]
        [TestCase(PositionDir.Short, 100, 10, 150, 5)]
        public void ReducePosition__ClosesCurrent_CreatesNewWithReducedVolume(PositionDir dir, float open, float vol, float reducePrice, float reduceVol)
        {
            Position pos = CreatePosition(dir, open, vol);
            _testObj.PositionsActive.Add(pos);
            
            _testObj.ReducePosition(0, CurrentTS, reducePrice, reduceVol, EntrySignal, _slippage, _commission);
            
            _testObj.Cash.ShouldBe(CashValue + pos.DirectionMultiplier() * (reducePrice * reduceVol) - 2 * Commission);
            _testObj.PositionsClosed.Count.ShouldBe(1);
            _testObj.ClosedPositionsEquity.Count.ShouldBe(1);
            CheckClosedPosition(0, dir, open, reducePrice, vol, Commission, CurrentTS, 0);
            _testObj.PositionsActive.Count.ShouldBe(1);
            CheckOpenedPosition(_testObj.PositionsActive[0], _stock, dir, reducePrice, vol - reduceVol, Commission, CurrentTS, StockDataRange.Daily, 0, PositionCloseMode.DontClose, 0);
        }

        [Test]
        public void CalcCurrentValue_NoActivePositions__CalculatesOnlyCash()
        {
            _stockPrices.TS[0] = CurrentTS;
            _stockPrices.C[0] = Close1;
            
            _testObj.CalcCurrentValue(CurrentTS, _dataLoader);
            
            _testObj.Equity.Count.ShouldBe(2);
            _testObj.Equity[0].Value.ShouldBe(CashValue);
            _testObj.Equity[1].Value.ShouldBe(CashValue);
            _testObj.Equity[1].TS.ShouldBe(CurrentTS);
        }

        [Test]
        public void CalcCurrentValue_WithActivePositions__CalculatesCashAndPosition()
        {
            _testObj.PositionsActive.Add(CreatePosition(PositionDir.Long, Price1, Vol1));
            _stockPrices.TS[0] = CurrentTS;
            _stockPrices.C[0] = Close1;

            _testObj.CalcCurrentValue(CurrentTS, _dataLoader);

            _testObj.Equity.Count.ShouldBe(2);
            _testObj.Equity[0].Value.ShouldBe(CashValue);
            _testObj.Equity[1].Value.ShouldBe(CashValue + Close1 * Vol1);
            _testObj.Equity[1].TS.ShouldBe(CurrentTS);
        }

        [Test]
        public void CalcCurrentValue_Multiple_MixedCase__CalculatesCashAndPosition()
        {
            _testObj.PositionsActive.Add(CreatePosition(PositionDir.Long, Price1, Vol1));
            _stockPrices.TS[0] = CurrentTS;
            _stockPrices.C[0] = Close1;
            _testObj.CalcCurrentValue(CurrentTS, _dataLoader);
            _testObj.Equity.Count.ShouldBe(2);
            _testObj.Equity[0].Value.ShouldBe(CashValue);
            _testObj.Equity[1].Value.ShouldBe(CashValue + Close1 * Vol1);
            _testObj.Equity[1].TS.ShouldBe(CurrentTS);

            _testObj.CloseAll(CurrentTS2, Close1, _slippage, CommissionUtils.CreateSubstitute());
            _stockPrices.TS[0] = CurrentTS2;
            _stockPrices.C[0] = Close1;
            _testObj.CalcCurrentValue(CurrentTS2, _dataLoader);
            _testObj.Equity.Count.ShouldBe(3);
            _testObj.Equity[0].Value.ShouldBe(CashValue);
            _testObj.Equity[1].Value.ShouldBe(CashValue + Close1 * Vol1);
            _testObj.Equity[1].TS.ShouldBe(CurrentTS);
            _testObj.Equity[2].Value.ShouldBe(CashValue + Close1 * Vol1);
            _testObj.Equity[2].TS.ShouldBe(CurrentTS2);
        }

        [TestCase(StockName1, 0)]
        [TestCase(StockName2, -1)]
        public void FindActivePositionIndex__ReturnsCorrectly(string stockName, int expectedIndex)
        {
            _testObj.PositionsActive.Add(CreatePosition(PositionDir.Long, Price1, Vol1));

            _testObj.FindActivePositionIndex(stockName).ShouldBe(expectedIndex);
        }
    }
}
