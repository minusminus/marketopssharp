using System;

namespace MarketOps.SystemData.Extensions
{
    /// <summary>
    /// Extensions for mone value calculations.
    /// </summary>
    public static class MoneyValueExtensions
    {
        public static float TruncateTo1stPlace(this float value) =>
            (float)(Math.Truncate(10m * (decimal)value) / 10m);

        public static float TruncateTo2ndPlace(this float value) => 
            (float)(Math.Truncate(100m * (decimal)value) / 100m);

        public static float TruncateToNthPlace(this float value, int n)
        {
            decimal m = (decimal)Math.Pow(10, n);
            return (float)(Math.Truncate(m * (decimal)value) / m);
        }
    }
}
