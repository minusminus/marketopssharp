using MarketOps.StockData.Types;
using System;

namespace MarketOps.SystemExecutor.Extensions
{
    /// <summary>
    /// Extensions for volume calculations.
    /// </summary>
    public static class VolumeExtensions
    {
        public static float TruncateToAllowedVolume(this float volume, StockType stockType)
        {
            if (stockType == StockType.InvestmentFund) return volume;
            return (float)Math.Floor(volume);
        }
    }
}
