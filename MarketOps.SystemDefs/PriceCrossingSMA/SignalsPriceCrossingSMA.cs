using System;
using System.Collections.Generic;
using MarketOps.SystemData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Types;

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
        private readonly IMMSignalVolume _signalVolumeCalculator;

        private readonly StockDefinition _stock;
        private readonly StockStat _statSMA;

        public SignalsPriceCrossingSMA(string stockName, StockDataRange dataRange, int smaPeriod, ISystemDataLoader dataLoader, IStockDataProvider dataProvider, IMMSignalVolume signalVolumeCalculator)
        {
            _dataRange = dataRange;
            _smaPeriod = smaPeriod;
            _dataLoader = dataLoader;
            _dataProvider = dataProvider;
            _signalVolumeCalculator = signalVolumeCalculator;

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
                        dataRange = _dataRange,
                        stats = new List<StockStat>() { _statSMA }
                    }
                }
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            List<Signal> res = new List<Signal>();

            if (leadingIndex <= _statSMA.BackBufferLength) return res;

            StockPricesData data = _dataLoader.Get(_stock.Name, _dataRange, 0, ts, ts);

            if ((data.C[leadingIndex - 1] <= _statSMA.Data(StatSMAData.SMA)[leadingIndex - 1 - _statSMA.BackBufferLength])
                && (data.C[leadingIndex] > _statSMA.Data(StatSMAData.SMA)[leadingIndex - _statSMA.BackBufferLength]))
                res.Add(CreateSignal(PositionDir.Long, systemState, data.C[leadingIndex]));

            if ((data.C[leadingIndex - 1] >= _statSMA.Data(StatSMAData.SMA)[leadingIndex - 1 - _statSMA.BackBufferLength])
                && (data.C[leadingIndex] < _statSMA.Data(StatSMAData.SMA)[leadingIndex - _statSMA.BackBufferLength]))
                res.Add(CreateSignal(PositionDir.Short, systemState, data.C[leadingIndex]));

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
                ReversePosition = true,
                Volume = (systemState.PositionsActive.Count > 0)
                    ? GetLastVolume(systemState)
                    : _signalVolumeCalculator.Calculate(systemState, _stock.Type, currentClosePrice)
            };

        private float GetLastVolume(SystemState systemState)
        {
            if (systemState.PositionsActive.Count > 1)
                throw new Exception("More than 1 active position");
            return systemState.PositionsActive[0].Volume;
        }
    }
}
