using System;
using MarketOps.StockData.Types;

namespace MarketOps.SystemExecutor.Interfaces
{
    /// <summary>
    /// Interface for mechanism to add ticks to price.
    /// </summary>
    public interface ITickAdder
    {
        float AddTicks(StockType stockType, DateTime ts, float value, int ticks);
    }
}
