using System;

namespace MarketOps.Extensions
{
    /// <summary>
    /// Extensions to display values as text.
    /// </summary>
    internal static class ValueDisplayExtensions
    {
        public static string ToDisplay(this float value) => value.ToDisplay(2);
        
        public static string ToDisplay(this float value, int precision) => value.ToString($"F{precision}");

        public static string ToDisplay(this int value) => value.ToString();

        public static string ToDisplay(this DateTime value) => value.ToString("yyyy-MM-dd");

        public static string ToDisplayPcnt(this float value) => $"{(100f * value).ToDisplay(2)} %";
    }
}
