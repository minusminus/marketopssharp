using ScottPlot;
using ScottPlot.Plottable;
using System.Collections.Generic;

namespace MarketOps.Controls.PriceChart.PVChart
{
    /// <summary>
    /// Removes stats from plot.
    /// </summary>
    internal static class StatsRemover
    {
        public static void RemoveStats(this Plot plot, List<ScatterPlot> series)
        {
            for (int i = 0; i < series.Count; i++)
                plot.RemoveSerieFromPlot(series[i]);
        }

        private static void RemoveSerieFromPlot(this Plot plot, ScatterPlot serie) => 
            plot.Remove(serie);
    }
}
