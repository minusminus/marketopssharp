using System;

namespace MarketOps.SystemData.Extensions
{
    /// <summary>
    /// Extensions for float values.
    /// </summary>
    public static class FloatExtensions
    {
        public static float FlooredMultipleOfN(this float value, float n) => 
            (float)Math.Floor(value / n) * n;
    }
}
