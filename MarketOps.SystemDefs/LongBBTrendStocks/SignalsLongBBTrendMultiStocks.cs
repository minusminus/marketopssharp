using System;
using System.Collections.Generic;
using MarketOps.SystemData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Types;
using System.Linq;
using MarketOps.SystemData.Extensions;

namespace MarketOps.SystemDefs.LongBBTrendStocks
{
    /// <summary>
    /// Signals for multi stocks long trends on stocks.
    /// </summary>
    internal class SignalsLongBBTrendMultiStocks : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private readonly StockDataRange _dataRange;
        private readonly ISystemDataLoader _dataLoader;
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly SignalGenerator _signalGenerator;
        private readonly PositionManager _positionManager;

        private readonly MultiStocksData _stocks;
        private readonly int _maxRequiredBackBufferLength;

        //spolki o min obrocie 300k w ostatnim roku
        private readonly string[] _stocksNames = { "KGHM","PKOBP","ALLEGRO","PKNORLEN","PZU","CDPROJEKT","PEKAO","DINOPL","JSW","PGNIG","CCC",
            "ORANGEPL","LOTOS","SANPL","PGE","LPP","ALIOR","CYFRPLSAT","MBANK","TAURONPE","MILLENNIUM","MERCATOR","ASSECOPOL","KERNEL","TSGAMES",
            "ENEA","HUUUGE","XTB","GPW","PEPCO","GRUPAAZOTY","KRUK","CIECH","PKPCARGO","EUROCASH","AMREST","11BIT","KETY","ASBIS","BUDIMEX",
            "LIVECHAT","BIOMEDLUB"};

        public SignalsLongBBTrendMultiStocks(StockDataRange dataRange, int bbPeriod, float bbSigmaWidth, int atrPeriod,
            ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger,
            IMMSignalVolume signalVolumeCalculator, ITickAligner tickAligner)
        {
            _dataRange = dataRange;
            _dataLoader = dataLoader;
            _dataProvider = dataProvider;
            _systemExecutionLogger = systemExecutionLogger;
            _signalGenerator = new SignalGenerator(dataRange, signalVolumeCalculator, tickAligner);
            _positionManager = new PositionManager();

            _maxRequiredBackBufferLength = Math.Max(bbPeriod, atrPeriod);
            _stocks = new MultiStocksData(_stocksNames.Length);
            InitializeStocksData(bbPeriod, bbSigmaWidth, atrPeriod);
        }

        public SystemDataDefinition GetDataDefinition() => new SystemDataDefinition()
        {
            stocks = _stocks.Stocks
                .Select((def, i) =>
                {
                    return new SystemStockDataDefinition()
                    {
                        stock = def,
                        dataRange = _dataRange,
                        stats = new List<StockStat>() { _stocks.StatsBB[i], _stocks.StatsATR[i] }
                    };
                })
                .ToList()
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            ManageCurrentPositions(ts, systemState);
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
                if (!_dataLoader.GetWithIndex(systemState.PositionsActive[i].Stock.FullName, _dataRange, ts, out StockPricesData data, out int index))
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
            if (!_dataLoader.GetWithIndex(_stocks.Stocks[stockIndex].FullName, _dataRange, ts, _maxRequiredBackBufferLength, out StockPricesData data, out int index)) return null;
            if (PositionExists(stockIndex, systemState)) return null;
            return _signalGenerator.Generate(_stocks.Stocks[stockIndex], ts, index, systemState, _stocks.TrendInfo[stockIndex], data, _stocks.StatsBB[stockIndex], _stocks.StatsATR[stockIndex]);
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
