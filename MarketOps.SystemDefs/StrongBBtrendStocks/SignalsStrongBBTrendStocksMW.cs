using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Signals for strong trends stocks.
    /// Long positions only.
    /// 
    /// Opens position on weekly data after breakout on monthly BB. Position opened on next week open after breakout.
    /// Initial stop ATR taken for weekly data.
    /// </summary>
    internal class SignalsStrongBBTrendStocksMW : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private const StockDataRange DataRangeLong = StockDataRange.Monthly;
        private const StockDataRange DataRangeShort = StockDataRange.Weekly;

        private readonly ISystemDataLoader _dataLoader;
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly SignalGenerator _signalGenerator;
        private readonly PositionManager _positionManager;

        private readonly MultiStocksData _stocks;
        private readonly int _maxRequiredLongBackBufferLength;
        private readonly int _maxRequiredShortBackBufferLength;

        private readonly string[] _stocksNames;

        public SignalsStrongBBTrendStocksMW(string stockName, int bbPeriod, float bbSigmaWidth, int atrPeriod,
            ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger,
            IMMSignalVolume signalVolumeCalculator, IMMPositionCloseCalculator positionCloseCalculator, ITickAligner tickAligner)
        {
            _stocksNames = new string[] { stockName };
            _dataLoader = dataLoader;
            _dataProvider = dataProvider;
            _systemExecutionLogger = systemExecutionLogger;
            _signalGenerator = new SignalGenerator(DataRangeShort, signalVolumeCalculator, positionCloseCalculator, tickAligner);
            _positionManager = new PositionManager();

            _maxRequiredLongBackBufferLength = bbPeriod; //Math.Max(bbPeriod, atrPeriod);
            _maxRequiredShortBackBufferLength = atrPeriod;
            _stocks = new MultiStocksData(_stocksNames.Length);
            InitializeStocksData(bbPeriod, bbSigmaWidth, atrPeriod);
        }

        public SystemDataDefinition GetDataDefinition() =>
            new SystemDataDefinition()
            {
                stocks = _stocks.Stocks
                    .Select((def, i) =>
                    {
                        return new SystemStockDataDefinition()
                        {
                            stock = def,
                            dataRange = DataRangeShort,
                            stats = new List<StockStat>() { _stocks.StatsATR[i] }
                        };
                    })
                    .Concat(_stocks.Stocks
                        .Select((def, i) =>
                        {
                            return new SystemStockDataDefinition()
                            {
                                stock = def,
                                dataRange = DataRangeLong,
                                stats = new List<StockStat>() { _stocks.StatsBB[i] }
                            };
                        }))
                    .ToList()
            };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            ManageCurrentPositions(ts, systemState);

            if (!ts.MonthEndsInCurrentWeek()) return new List<Signal>();
            var signals = GenerateSignals(ts, systemState);
            //LogData(ts, systemState, signals);
            return signals;
        }

        private void InitializeStocksData(int bbPeriod, float bbSigmaWidth, int atrPeriod)
        {
            for (int i = 0; i < _stocksNames.Length; i++)
            {
                _stocks.Stocks[i] = _dataProvider.GetStockDefinition(_stocksNames[i]);
                StockStat statBB = new StatBB("")
                    .SetParam(StatBBParams.Period, new MOParamInt() { Value = bbPeriod })
                    .SetParam(StatBBParams.SigmaWidth, new MOParamFloat() { Value = bbSigmaWidth });
                _stocks.StatsBB[i] = (StatBB)statBB;
                StockStat statATR = new StatATR("")
                    .SetParam(StatATRParams.Period, new MOParamInt() { Value = atrPeriod });
                _stocks.StatsATR[i] = (StatATR)statATR;
            }
        }

        private void ManageCurrentPositions(DateTime ts, SystemState systemState)
        {
            if (systemState.PositionsActive.Count <= 0) return;

            for (int i = 0; i < systemState.PositionsActive.Count; i++)
            {
                if (!_dataLoader.GetWithIndex(systemState.PositionsActive[i].Stock.FullName, DataRangeShort, ts, out StockPricesData data, out int index))
                    throw new Exception("ManageCurrentPositions can't get stock prices data");
                _positionManager.Manage(systemState.PositionsActive[i], data, index);
            }
        }

        private List<Signal> GenerateSignals(DateTime ts, SystemState systemState) =>
            _stocks.Stocks
                .Select((def, i) => GenerateSignalForStock(i, ts, systemState))
                .Where(signal => signal != null)
                .ToList();

        private Signal GenerateSignalForStock(int stockIndex, DateTime ts, SystemState systemState)
        {
            if (PositionExists(stockIndex, systemState)) return null;
            if (!_dataLoader.GetWithIndex(_stocks.Stocks[stockIndex].FullName, DataRangeShort, ts, _maxRequiredShortBackBufferLength, out StockPricesData dataShort, out int indexShort)) return null;
            if (!_dataLoader.GetWithIndex(_stocks.Stocks[stockIndex].FullName, DataRangeLong, ts.FirstDayOfCurrentMonth(), _maxRequiredLongBackBufferLength, out StockPricesData dataLong, out int indexLong)) return null;
            return _signalGenerator.Generate(_stocks.Stocks[stockIndex], ts, indexLong, indexShort, systemState, _stocks.TrendInfo[stockIndex], dataLong, dataShort, _stocks.StatsBB[stockIndex], _stocks.StatsATR[stockIndex]);
        }

        private bool PositionExists(int stockIndex, SystemState systemState) =>
            systemState.FindActivePositionIndex(_stocks.Stocks[stockIndex].FullName) > -1;

        private void LogData(DateTime ts, SystemState systemState, List<Signal> signals)
        {
            _systemExecutionLogger.Add(
                $"{ts.Date:yyyy-MM-dd}:" + Environment.NewLine
                + $"active pos: {systemState.PositionsActive.Count}, signals: {signals.Count}"
                );
        }
    }
}
