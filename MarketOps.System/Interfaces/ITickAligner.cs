using System;
using MarketOps.StockData.Types;

namespace MarketOps.System.Interfaces
{
    /// <summary>
    /// Interface for mechanism to align value to possible price level.
    /// </summary>
    internal interface ITickAligner
    {
        float Up(StockType stockType, DateTime ts, float value);
        float Down(StockType stockType, DateTime ts, float value);
    }
}
