using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Signal generator for StrongBBTrendStocksMD.
    /// Checks trend on long data range, generates signal for short data range.
    /// </summary>
    internal class SignalGeneratorMD
    {
        const int EntryPriceTicksAboveLevel = 1;
        const int InitialStopTicksBelowLevel = -1;

        private readonly StockDataRange _signalDataRange;
        private readonly IMMSignalVolume _signalVolumeCalculator;
        private readonly ITickAligner _tickAligner;
        private readonly ITickAdder _tickAdder;

        public SignalGeneratorMD(StockDataRange signalDataRange, IMMSignalVolume signalVolumeCalculator, ITickAligner tickAligner, ITickAdder tickAdder)
        {
            _signalDataRange = signalDataRange;
            _signalVolumeCalculator = signalVolumeCalculator;
            _tickAligner = tickAligner;
            _tickAdder = tickAdder;
        }

        public Signal Generate(StockDefinition stock, DateTime ts, SystemState systemState, StockPricesData data, int index)
        {
            if (HFallsInLastTicks(data, index))
                return CreateSignal(stock, ts, PositionDir.Long, systemState, GetEntryPrice(stock.Type, ts, data, index), GetInitialStop(stock.Type, ts, data, index));

            return null;
        }

        private bool HFallsInLastTicks(StockPricesData data, int index)
        {
            if ((data.H[index] <= data.H[index - 1]) && (data.H[index - 1] <= data.H[index - 2])) return true;
            if ((data.H[index] <= data.H[index - 1]) && (data.H[index - 1] <= data.H[index - 3]) && (data.H[index - 2] <= data.H[index - 3])) return true;
            if ((data.H[index] <= data.H[index - 2]) && (data.H[index - 2] <= data.H[index - 3]) && (data.H[index - 1] <= data.H[index - 3])) return true;
            return false;
        }

        private Signal CreateSignal(StockDefinition stock, DateTime ts, PositionDir dir, SystemState systemState, float entryPrice, float initialStopPrice)
        {
            float volume = _signalVolumeCalculator.Calculate(systemState, stock.Type, entryPrice, entryPrice - initialStopPrice);
            return (volume > 0)
                ? new Signal()
                {
                    Stock = stock,
                    DataRange = _signalDataRange,
                    IntradayInterval = 0,
                    Type = SignalType.EnterOnPrice,
                    Price = entryPrice,
                    Direction = dir,
                    InitialStopMode = SignalInitialStopMode.OnPrice,
                    InitialStopValue = initialStopPrice,
                    ReversePosition = false,
                    Volume = volume
                }
                : null;
        }

        private float GetEntryPrice(StockType stockType, DateTime ts, StockPricesData data, int index) => 
            AddTicks(stockType, ts, Math.Max(data.H[index], data.H[index - 1]), EntryPriceTicksAboveLevel);

        private float GetInitialStop(StockType stockType, DateTime ts, StockPricesData data, int index) =>
            AddTicks(stockType, ts, Math.Min(data.L[index], data.L[index - 1]), InitialStopTicksBelowLevel);

        //private float AlignDown(StockType stockType, DateTime ts, float value) =>
        //    (_tickAligner != null) ? _tickAligner.AlignDown(stockType, ts, value) : value;

        private float AddTicks(StockType stockType, DateTime ts, float value, int ticks) =>
            (_tickAdder != null) ? _tickAdder.AddTicks(stockType, ts, value, ticks) : value;
    }
}
