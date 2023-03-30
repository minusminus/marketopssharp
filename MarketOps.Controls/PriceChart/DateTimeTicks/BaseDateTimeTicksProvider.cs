using ScottPlot.Renderable;
using System;

namespace MarketOps.Controls.PriceChart.DateTimeTicks
{
    /// <summary>
    /// Base class for calculating datetime ticks string values for price chart.
    /// </summary>
    internal abstract class BaseDateTimeTicksProvider : IDateTimeTicksProvider
    {
        public (string[] values, double[] positions) Get(DateTime[] ts, Axis xAxis)
        {

        }

        protected abstract string MapTsToString(DateTime ts);
    }
}
