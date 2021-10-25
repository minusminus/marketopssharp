using System;

namespace MarketOps.DataProvider.Pg
{
    internal static class PgDataExtensions
    {
        public static string ToTimestampQueryValue(this DateTime dt) => $"timestamp '{dt.ToString("yyyy-MM-dd HH:mm")}'";
    }
}
