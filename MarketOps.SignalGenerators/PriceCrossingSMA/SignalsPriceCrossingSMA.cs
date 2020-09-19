using System;
using System.Collections.Generic;
using MarketOps.System;
using MarketOps.System.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Interfaces;

namespace MarketOps.SystemDefs.PriceCrossingSMA
{
    /// <summary>
    /// Signal on price crossing sma up or down.
    /// </summary>
    internal class SignalsPriceCrossingSMA : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private readonly StockDataRange _dataRange;
        private readonly int _smaPeriod;
        private readonly ISystemDataLoader _dataLoader;
        private readonly IStockDataProvider _dataProvider;

        private readonly StockDefinition _stock;
        private readonly StockStat _statSMA;

        public SignalsPriceCrossingSMA(string stockName, StockDataRange dataRange, int smaPeriod, ISystemDataLoader dataLoader, IStockDataProvider dataProvider)
        {
            _dataRange = dataRange;
            _smaPeriod = smaPeriod;
            _dataLoader = dataLoader;
            _dataProvider = dataProvider;

            _stock = _dataProvider.GetStockDefinition(stockName);
            _statSMA = new StatSMA("")
                .SetParam(StatSMAParams.Period, new MOParamInt() { Value = _smaPeriod });
        }

        public SystemDataDefinition GetDataDefinition() => new SystemDataDefinition()
        {
            stocks = new List<SystemStockDataDefinition>() {
                    new SystemStockDataDefinition()
                    {
                        stock = _stock,
                        dataRange = _dataRange + 1,
                        stats = new List<StockStat>() { _statSMA }
                    }
                }
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex)
        {
            List<Signal> res = new List<Signal>();

            StockPricesData data = _dataLoader.Get(_stock.Name, _dataRange, 0, ts, ts);

            if ((data.C[leadingIndex - 1] <= _statSMA.Data(0)[leadingIndex - 1])
                && (data.C[leadingIndex] > _statSMA.Data(0)[leadingIndex]))
                res.Add(CreateSignal(PositionDir.Long));

            if ((data.C[leadingIndex - 1] >= _statSMA.Data(0)[leadingIndex - 1])
                && (data.C[leadingIndex] < _statSMA.Data(0)[leadingIndex]))
                res.Add(CreateSignal(PositionDir.Short));

            return res;
        }

        private Signal CreateSignal(PositionDir dir) =>
            new Signal()
            {
                Stock = _stock,
                DataRange = _dataRange,
                IntradayInterval = 0,
                Type = SignalType.EnterOnOpen,
                Direction = dir,
                ReversePosition = true
            };
    }
}
