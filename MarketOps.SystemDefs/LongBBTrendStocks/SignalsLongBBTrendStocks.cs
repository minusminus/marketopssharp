﻿using System;
using System.Collections.Generic;
using MarketOps.SystemData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Types;
using System.Linq;

namespace MarketOps.SystemDefs.LongBBTrendStocks
{
    /// <summary>
    /// Signals for long trends on stocks.
    /// 
    /// Trend starts when price breaks BBH. 
    /// Enter: on next tick open after trend start
    /// Exit options:
    /// - stop on min of N lows (3, 5)
    /// </summary>
    internal class SignalsLongBBTrendStocks : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private readonly StockDataRange _dataRange;
        private readonly int _bbPeriod;
        private readonly float _bbSigmaWidth;
        private readonly ISystemDataLoader _dataLoader;
        private readonly IStockDataProvider _dataProvider;
        private readonly SignalGenerator _signalGenerator;
        private readonly PositionManager _positionManager;

        private LongBBTrendInfo _trendInfo = new LongBBTrendInfo();
        private readonly StockDefinition _stock;
        private readonly StockStat _statBB, _statATR;

        //spolki o min obrocie 300k w ostatnim roku
        //private readonly string[] _stocksNames = { "KGHM","PKOBP","ALLEGRO","PKNORLEN","PZU","CDPROJEKT","PEKAO","DINOPL","JSW","PGNIG","CCC",
        //    "ORANGEPL","LOTOS","SANPL","PGE","LPP","ALIOR","CYFRPLSAT","MBANK","TAURONPE","MILLENNIUM","MERCATOR","ASSECOPOL","KERNEL","TSGAMES",
        //    "ENEA","HUUUGE","XTB","GPW","PEPCO","GRUPAAZOTY","KRUK","CIECH","PKPCARGO","EUROCASH","AMREST","11BIT","KETY","ASBIS","BUDIMEX",
        //    "LIVECHAT","BIOMEDLUB"};

        public SignalsLongBBTrendStocks(string stockName, StockDataRange dataRange, int bbPeriod, float bbSigmaWidth, int atrPeriod, 
            ISystemDataLoader dataLoader, IStockDataProvider dataProvider, IMMSignalVolume signalVolumeCalculator,
            ITickAligner tickAligner)
        {
            _dataRange = dataRange;
            _bbPeriod = bbPeriod;
            _bbSigmaWidth = bbSigmaWidth;
            _dataLoader = dataLoader;
            _dataProvider = dataProvider;
            _signalGenerator = new SignalGenerator(dataRange, signalVolumeCalculator, tickAligner);
            _positionManager = new PositionManager();

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
            if ((leadingIndex <= _statBB.BackBufferLength) || (leadingIndex <= _statATR.BackBufferLength)) return new List<Signal>();

            StockPricesData data = _dataLoader.Get(_stock.FullName, _dataRange, 0, ts, ts);

            ManageCurrentPositions(data, leadingIndex, systemState);
            return GenerateSignals(ts, data, leadingIndex, systemState);
        }

        private List<Signal> GenerateSignals(DateTime ts, StockPricesData data, int leadingIndex, SystemState systemState)
        {
            Signal signal = _signalGenerator.Generate(_stock, ts, leadingIndex, systemState, _trendInfo, data, (StatBB)_statBB, (StatATR)_statATR);
            return (signal != null)
                ? new List<Signal>() { signal }
                : new List<Signal>();
        }

        private void ManageCurrentPositions(StockPricesData data, int leadingIndex, SystemState systemState)
        {
            if (systemState.PositionsActive.Count <= 0) return;
            if (systemState.PositionsActive.Count > 1)
                throw new Exception("More than 1 active position");

            _positionManager.Manage(systemState.PositionsActive[0], data, leadingIndex);
        }
    }
}
