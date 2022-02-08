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
    /// Enter: on next tick open after trned start
    /// Exit options:
    /// - stop on sma
    /// - stop on min of 3 lows
    /// </summary>
    internal class SignalsLongBBTrendStocks : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private readonly StockDataRange _dataRange;
        private readonly int _bbPeriod;
        private readonly float _bbSigmaWidth;
        private readonly ISystemDataLoader _dataLoader;
        private readonly IStockDataProvider _dataProvider;
        private readonly IMMSignalVolume _signalVolumeCalculator;

        private readonly StockDefinition _stock;
        private readonly StockStat _statBB;

        private BBTrendType _currentTrend = BBTrendType.Unknown;
        public SignalsLongBBTrendStocks(string stockName, StockDataRange dataRange, int bbPeriod, float bbSigmaWidth, ISystemDataLoader dataLoader, IStockDataProvider dataProvider, IMMSignalVolume signalVolumeCalculator)
        {
            _dataRange = dataRange;
            _bbPeriod = bbPeriod;
            _bbSigmaWidth = bbSigmaWidth;
            _dataLoader = dataLoader;
            _dataProvider = dataProvider;
            _signalVolumeCalculator = signalVolumeCalculator;

            _stock = _dataProvider.GetStockDefinition(stockName);
            _statBB = new StatBB("")
                .SetParam(StatBBParams.Period, new MOParamInt() { Value = _bbPeriod })
                .SetParam(StatBBParams.SigmaWidth, new MOParamFloat() { Value = _bbSigmaWidth });
        }

        public SystemDataDefinition GetDataDefinition() => new SystemDataDefinition()
        {
            stocks = new List<SystemStockDataDefinition>() {
                    new SystemStockDataDefinition()
                    {
                        stock = _stock,
                        dataRange = _dataRange,
                        stats = new List<StockStat>() { _statBB }
                    }
                }
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            List<Signal> res = new List<Signal>();
            if (leadingIndex <= _statBB.BackBufferLength) return res;

            StockPricesData data = _dataLoader.Get(_stock.FullName, _dataRange, 0, ts, ts);

            _currentTrend = BBTrendRecognizer.BBTrendRecognizer.RecognizeTrend(data, (StatBB)_statBB, leadingIndex, _currentTrend, out _);
            BBTrendExpectation expectation = BBTrendRecognizer.BBTrendRecognizer.GetExpectation(data, (StatBB)_statBB, leadingIndex, _currentTrend);

            if (systemState.PositionsActive.Count > 0)
            {
                if (systemState.PositionsActive.Count > 1)
                    throw new Exception("More than 1 active position");
                if ((expectation == BBTrendExpectation.DownAndFalling) || (expectation == BBTrendExpectation.DownButPossibleChange))
                    systemState.PositionsActive[0].CloseMode = PositionCloseMode.OnOpen;
            }
            else
            {
                if ((expectation == BBTrendExpectation.UpAndRaising) || (expectation == BBTrendExpectation.UpButPossibleChange))
                    res.Add(CreateSignal(PositionDir.Long, systemState, data.C[leadingIndex]));
            }

            return res;
        }

        private Signal CreateSignal(PositionDir dir, SystemState systemState, float currentClosePrice) =>
            new Signal()
            {
                Stock = _stock,
                DataRange = _dataRange,
                IntradayInterval = 0,
                Type = SignalType.EnterOnOpen,
                Direction = dir,
                ReversePosition = false,
                Volume = _signalVolumeCalculator.Calculate(systemState, _stock.Type, currentClosePrice)
            };
    }
}
