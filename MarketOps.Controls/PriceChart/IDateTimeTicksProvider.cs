using ScottPlot;
using System;

namespace MarketOps.Controls.PriceChart
{
    /// <summary>
    /// Interface for mechanism providing datetime ticks string values for price chart.
    /// </summary>
    internal interface IDateTimeTicksProvider
    {
        (string[] values, double[] positions) Get(in DateTime[] tsArray, in AxisLimits axisLimits);
    }
}
