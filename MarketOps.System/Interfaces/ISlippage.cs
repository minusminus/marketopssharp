using System;
using MarketOps.StockData.Types;

namespace MarketOps.System.Interfaces
{
    /// <summary>
    /// Interface for system position slippage calculation.
    /// </summary>
    public interface ISlippage
    {
        float CalculateClose(StockType stockType, DateTime ts, PositionDir dir, float value);
        float CalculateOpen(StockType stockType, DateTime ts, PositionDir dir, float value);
    }
}
