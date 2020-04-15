using System;
using MarketOps.StockData.Types;

namespace MarketOps.System.Interfaces
{
    /// <summary>
    /// Interface for mechanism to align value to possible price level.
    /// </summary>
    public interface ITickAligner
    {
        float AlignUp(StockType stockType, DateTime ts, float value);
        float AlignDown(StockType stockType, DateTime ts, float value);
    }
}
