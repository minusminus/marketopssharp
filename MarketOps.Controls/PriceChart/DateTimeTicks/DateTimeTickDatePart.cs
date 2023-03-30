using System;

namespace MarketOps.Controls.PriceChart.DateTimeTicks
{
    /// <summary>
    /// DateTimeTicksProvider for date part only.
    /// </summary>
    internal class DateTimeTickDatePart : BaseDateTimeTicksProvider
    {
        protected override string MapTsToString(DateTime ts) => 
            ts.ToString("yyyy-MM-dd");
    }
}
