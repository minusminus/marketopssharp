using System;
using System.Collections.Generic;
using MarketOps.SystemData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemDefs.BBTrendRecognizer;

namespace MarketOps.SystemDefs.LongBBTrendStocks
{
    /// <summary>
    /// Signals for long trends on stocks.
    /// 
    /// Trend starts when price breaks BBH. 
    /// Enter: on next tick open after trend start
    /// Exit options:
    /// - stop on sma
    /// - stop on min of N lows (3, 5)
    /// </summary>
    internal class SignalsLongBBTrendStocks : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private const int TrailingStopTicksBelow = -2;
        private const int TrailingStopMinOfL = 5;

        private readonly StockDataRange _dataRange;
        private readonly int _bbPeriod;
        private readonly float _bbSigmaWidth;
        private readonly ISystemDataLoader _dataLoader;
        private readonly IStockDataProvider _dataProvider;
        private readonly IMMSignalVolume _signalVolumeCalculator;
        private readonly ITickAdder _tickAdder;
        private readonly ITickAligner _tickAligner;

        private readonly StockDefinition _stock;
        private readonly StockStat _statBB, _statATR;

        private BBTrendType _currentTrend = BBTrendType.Unknown;
        private int _currentTrendStartIndex = -1;

        //spolki o min obrocie 300k w ostatnim roku
        //private readonly string[] _stocksNames = { "KGHM","PKOBP","ALLEGRO","PKNORLEN","PZU","CDPROJEKT","PEKAO","DINOPL","JSW","PGNIG","CCC",
        //    "ORANGEPL","LOTOS","SANPL","PGE","LPP","ALIOR","CYFRPLSAT","MBANK","TAURONPE","MILLENNIUM","MERCATOR","ASSECOPOL","KERNEL","TSGAMES",
        //    "ENEA","HUUUGE","XTB","GPW","PEPCO","GRUPAAZOTY","KRUK","CIECH","PKPCARGO","EUROCASH","AMREST","11BIT","KETY","ASBIS","BUDIMEX",
        //    "LIVECHAT","BIOMEDLUB"};

        public SignalsLongBBTrendStocks(string stockName, StockDataRange dataRange, int bbPeriod, float bbSigmaWidth, int atrPeriod, 
            ISystemDataLoader dataLoader, IStockDataProvider dataProvider, IMMSignalVolume signalVolumeCalculator,
            ITickAdder tickAdder, ITickAligner tickAligner)
        {
            _dataRange = dataRange;
            _bbPeriod = bbPeriod;
            _bbSigmaWidth = bbSigmaWidth;
            _dataLoader = dataLoader;
            _dataProvider = dataProvider;
            _signalVolumeCalculator = signalVolumeCalculator;
            _tickAdder = tickAdder;
            _tickAligner = tickAligner;

            _stock = _dataProvider.GetStockDefinition(stockName);
            _statBB = new StatBB("")
                .SetParam(StatBBParams.Period, new MOParamInt() { Value = _bbPeriod })
                .SetParam(StatBBParams.SigmaWidth, new MOParamFloat() { Value = _bbSigmaWidth });
            _statATR = new StatATR("")
                .SetParam(StatATRParams.Period, new MOParamInt() { Value = atrPeriod });
        }

        public SystemDataDefinition GetDataDefinition() => new SystemDataDefinition()
        {
            stocks = new List<SystemStockDataDefinition>() {
                    new SystemStockDataDefinition()
                    {
                        stock = _stock,
                        dataRange = _dataRange,
                        stats = new List<StockStat>() { _statBB, _statATR }
                    }
                }
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            List<Signal> res = new List<Signal>();
            if ((leadingIndex <= _statBB.BackBufferLength) || (leadingIndex <= _statATR.BackBufferLength)) return res;

            StockPricesData data = _dataLoader.Get(_stock.FullName, _dataRange, 0, ts, ts);

            _currentTrend = BBTrendRecognizer.BBTrendRecognizer.RecognizeTrendOnC(data, (StatBB)_statBB, leadingIndex, _currentTrend, out _, ref _currentTrendStartIndex);
            BBTrendExpectation expectation = BBTrendRecognizer.BBTrendRecognizer.GetExpectation(data, (StatBB)_statBB, leadingIndex, _currentTrend);

            if (systemState.PositionsActive.Count > 0)
            {
                if (systemState.PositionsActive.Count > 1)
                    throw new Exception("More than 1 active position");
                //if ((expectation == BBTrendExpectation.DownAndFalling) || (expectation == BBTrendExpectation.DownButPossibleChange))
                //    systemState.PositionsActive[0].CloseMode = PositionCloseMode.OnOpen;

                CalculateTrailingStop(ts, systemState.PositionsActive[0], data, leadingIndex);
            }
            else
            {
                if ((expectation == BBTrendExpectation.UpAndRaising)
                    //&& PriceAboveMaxOfPreviousH(data, leadingIndex, 4, data.C[leadingIndex]))
                    && TrendStartedNotLaterThanNTicksAgo(leadingIndex, 1))
                    res.Add(CreateSignal(ts, PositionDir.Long, systemState, data.C[leadingIndex], _statATR.Data(StatATRData.ATR)[leadingIndex - _statATR.BackBufferLength]));
            }

            return res;
        }

        private Signal CreateSignal(DateTime ts, PositionDir dir, SystemState systemState, float currentClosePrice, float currentAtr) =>
            new Signal()
            {
                Stock = _stock,
                DataRange = _dataRange,
                IntradayInterval = 0,
                Type = SignalType.EnterOnOpen,
                Direction = dir,
                InitialStopMode = SignalInitialStopMode.OnPrice,
                InitialStopValue = AlignDown(_stock.Type, ts, currentClosePrice - currentAtr),
                ReversePosition = false,
                Volume = _signalVolumeCalculator.Calculate(systemState, _stock.Type, currentClosePrice)
            };

        private void CalculateTrailingStop(DateTime ts, Position position, StockPricesData data, int leadingIndex)
        {
            position.CloseMode = PositionCloseMode.OnStopHit;
            position.CloseModePrice = Math.Max(
                position.CloseModePrice,
                AddTicks(position.Stock.Type, ts, MinOfL(data, leadingIndex, TrailingStopMinOfL), TrailingStopTicksBelow)
                );
        }

        private float AlignDown(StockType stockType, DateTime ts, float value) => 
            (_tickAligner != null) ? _tickAligner.AlignDown(stockType, ts, value) : value;

        private float AddTicks(StockType stockType, DateTime ts, float value, int ticks) =>
            (_tickAdder != null) ? _tickAdder.AddTicks(stockType, ts, value, ticks) : value;

        private float MinOfL(StockPricesData data, int leadingIndex, int length)
        {
            float currentMin = data.L[leadingIndex];
            for (int i = 1; i < length; i++)
                if (data.L[leadingIndex - i] < currentMin)
                    currentMin = data.L[leadingIndex - i];
            return currentMin;
        }

        private bool PriceAboveMaxOfPreviousH(StockPricesData data, int leadingIndex, int length, float price)
        {
            for (int i = 1; i <= length; i++)
                if (data.H[leadingIndex - i] > price)
                    return false;
            return true;
        }

        private bool TrendStartedNotLaterThanNTicksAgo(int leadingIndex, int n) =>
            (leadingIndex - _currentTrendStartIndex) <= n;
    }
}
