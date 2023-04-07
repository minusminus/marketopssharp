using MarketOps.StockData.Types;
using ScottPlot.Plottable;
using System.Collections.Generic;

namespace MarketOps.Controls.PriceChart.PVChart
{
    internal class PriceChartStat
    {
        public readonly StockStat Stat;
        public readonly List<ScatterPlot> ChartSeries = new List<ScatterPlot>();

        public PriceChartStat(StockStat stat)
        {
            Stat = stat;
        }
    }

    /// <summary>
    /// Manages additional FormsPlot objects on price chart.
    /// </summary>
    internal class PriceChartStatsManager
    {
        public readonly List<PriceChartStat> Charts = new List<PriceChartStat>();

        public PriceChartStat Add(StockStat stat)
        {
            var result = new PriceChartStat(stat);
            Charts.Add(result);
            return result;
        }

        public void Remove(int index) => 
            Charts.RemoveAt(index);

        public void Clear()
        {
            while (Charts.Count > 0)
                Remove(0);
        }
    }
}
