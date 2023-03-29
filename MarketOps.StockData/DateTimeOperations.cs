using MarketOps.StockData.Types;
using System;

namespace MarketOps.StockData
{
    /// <summary>
    /// Operations on datetime.
    /// </summary>
    public static class DateTimeOperations
    {
        public static DateTime OneTickBefore(DateTime ts, StockPricesData data)
        {
            switch (data.Range)
            {
                case StockDataRange.Monthly:
                    return ts.AddMonths(-1);
                case StockDataRange.Weekly:
                    return ts.AddDays(-7);
                case StockDataRange.Daily:
                    return ts.AddDays(-1);
                case StockDataRange.Intraday:
                    return ts.AddMinutes(-data.IntradayInterval);
                case StockDataRange.Tick:
                    return ts.AddMinutes(-1);
            }
            throw new ArgumentException("Unknown range");
        }
    }
}
