using System;

namespace MarketOps.SystemExecutor.Extensions
{
    /// <summary>
    /// Extensions for mone value calculations.
    /// </summary>
    public static class MoneyValueExtensions
    {
        public static float TruncateTo2ndPlace(this float value) => 
            (float)(Math.Truncate(100m * (decimal)value) / 100m);
    }
}
