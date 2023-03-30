using MarketOps.StockData.Types;
using System;

namespace MarketOps.Controls.PriceChart.DateTimeTicks
{
    /// <summary>
    /// Factory providing datetime ticks string values mechanisms.
    /// </summary>
    internal static class DateTimeTicksProviderFactory
    {
        public static IDateTimeTicksProvider Get(StockDataRange dataRange)
        {
            switch (dataRange)
            {
                case StockDataRange.Daily:
                case StockDataRange.Weekly:
                case StockDataRange.Monthly:  
                    return new DateTimeTickDatePart();
                default:
                    throw new ArgumentException($"{nameof(IDateTimeTicksProvider)} not implemented for data range: {dataRange}", nameof(dataRange));
            }
        }
    }
}
