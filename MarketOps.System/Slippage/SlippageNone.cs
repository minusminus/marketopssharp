using System;
using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;

namespace MarketOps.System.Slippage
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
