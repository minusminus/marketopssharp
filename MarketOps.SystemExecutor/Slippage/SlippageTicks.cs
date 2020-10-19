using MarketOps.StockData.Types;
using MarketOps.SystemExecutor.Interfaces;
using System;

namespace MarketOps.SystemExecutor.Slippage
{
    /// <summary>
    /// Slippage in ticks in negative direction to price.
    /// </summary>
    public class SlippageTicks : ISlippage
    {
        private readonly int _ticks;
        private readonly ITickAdder _tickAdder;

        public SlippageTicks(ITickAdder tickAdder, int ticks)
        {
            _tickAdder = tickAdder;
            _ticks = ticks;
        }

        public float CalculateClose(StockType stockType, DateTime ts, PositionDir dir, float value)
        {
            return _tickAdder.AddTicks(stockType, ts, value, (dir == PositionDir.Long ? -1 : 1) * _ticks);
        }

        public float CalculateOpen(StockType stockType, DateTime ts, PositionDir dir, float value)
        {
            return _tickAdder.AddTicks(stockType, ts, value, (dir == PositionDir.Long ? 1 : -1) * _ticks);
        }
    }
}
