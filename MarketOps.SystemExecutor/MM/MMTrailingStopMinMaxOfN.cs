using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;

namespace MarketOps.SystemExecutor.MM
{
    /// <summary>
    /// Trailing stop calculator.
    /// Calculates min or max of N ticks (depending on position direction).
    /// </summary>
    public class MMTrailingStopMinMaxOfN : IMMPositionCloseCalculator
    {
        private readonly int _minOfL;
        private readonly int _maxOfH;
        private readonly int _stopTicksMargin;
        private readonly ISystemDataLoader _dataLoader;
        private readonly ITickAdder _tickAdder;

        public MMTrailingStopMinMaxOfN(int minOfL, int maxOfH, int stopTicksMargin, ISystemDataLoader dataLoader, ITickAdder tickAdder)
        {
            _minOfL = minOfL;
            _maxOfH = maxOfH;
            _stopTicksMargin = stopTicksMargin;
            _dataLoader = dataLoader;
            _tickAdder = tickAdder;
        }

        public void CalculateCloseMode(Position position, DateTime ts)
        {
            position.CloseMode = PositionCloseMode.OnStopHit;
            position.CloseModePrice = CalculateStopPrice(position, ts);
        }

        private float CalculateStopPrice(Position position, DateTime ts)
        {
            StockPricesData data = _dataLoader.Get(position.Stock.FullName, position.DataRange, position.IntradayInterval, ts, ts);
            int index = data.FindByTS(ts);
            return (index >= 0)
                ? CalculateTrailingStop(ts, position, data, index)
                : position.CloseModePrice;
        }

        private float CalculateTrailingStop(DateTime ts, Position position, StockPricesData data, int index) => 
            (position.Direction == PositionDir.Long)
                ? Math.Max(
                    position.CloseModePrice,
                    AddTicks(position.Stock.Type, ts, data.MinOfL(index, _minOfL), -_stopTicksMargin)
                    )
                : Math.Min(
                    position.CloseModePrice,
                    AddTicks(position.Stock.Type, ts, data.MaxOfH(index, _maxOfH), _stopTicksMargin)
                    );

        private float AddTicks(StockType stockType, DateTime ts, float value, int ticks) =>
            (_tickAdder != null) ? _tickAdder.AddTicks(stockType, ts, value, ticks) : value;
    }
}
