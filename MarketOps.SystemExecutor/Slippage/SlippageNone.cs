using System;
using MarketOps.StockData.Types;
using MarketOps.SystemExecutor.Interfaces;

namespace MarketOps.SystemExecutor.Slippage
{
    /// <summary>
    /// No slippage is added to position.
    /// </summary>
    public class SlippageNone : ISlippage
    {
        public float CalculateClose(StockType stockType, DateTime ts, PositionDir dir, float value)
        {
            return value;
        }

        public float CalculateOpen(StockType stockType, DateTime ts, PositionDir dir, float value)
        {
            return value;
        }
    }
}
