using System;

namespace MarketOps.Extensions
{
    /// <summary>
    /// Extensions to display values as text.
    /// </summary>
    internal static class ValueDisplayExtensions
    {
        public static string ToDisplay(this float value) => value.ToString("F2");

        public static string ToDisplay(this int value) => value.ToString();

        public static string ToDisplay(this DateTime value) => value.ToString("yyyy-MM-dd");

        public static string ToDisplayPcnt(this float value) => $"{(100f * value).ToString("F2")} %";
    }
}
