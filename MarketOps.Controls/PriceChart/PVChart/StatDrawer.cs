using MarketOps.StockData.Types;
using ScottPlot.Plottable;
using ScottPlot;
using System.Drawing;
using System.Collections.Generic;

namespace MarketOps.Controls.PriceChart.PVChart
{
    /// <summary>
    /// Draws StockStat on plot.
    /// </summary>
    internal static class StatDrawer
    {
        public static void DrawStat(this Plot plot, StockStat stat, in double[] xs, List<ScatterPlot> createdPlots = null)
        {
            for (int i = 0; i < stat.DataCount; i++)
            {
                var series = plot.DrawSeriesData(stat, i, xs);
                createdPlots?.Add(series);
            }
        }

        private static ScatterPlot DrawSeriesData(this Plot plot, StockStat stat, int seriesIndex, in double[] xs)
        {
            double[] seriesData = new double[xs.Length];
            seriesData.SetInitialNans(stat.BackBufferLength);
            seriesData.FillData(stat.Data(seriesIndex), stat.BackBufferLength);
            return plot.AddSeriesLine(seriesData, stat.DataColor[seriesIndex], xs);
        }

        private static void SetInitialNans(this double[] seriesData, int backBufferLength)
        {
            for (int i = 0; i < backBufferLength; i++)
                seriesData[i] = double.NaN;
        }

        private static void FillData(this double[] seriesData, in float[] statData, int backBufferLength)
        {
            for (int i = 0; i < statData.Length; i++)
                seriesData[i + backBufferLength - 1] = statData[i];
        }

        private static ScatterPlot AddSeriesLine(this Plot plot, in double[] seriesData, Color seriesColor, in double[] xs)
        {
            var result = plot.AddScatterLines(xs, seriesData, seriesColor);
            result.OnNaN = ScatterPlot.NanBehavior.Gap;
            return result;
        }
    }
}
