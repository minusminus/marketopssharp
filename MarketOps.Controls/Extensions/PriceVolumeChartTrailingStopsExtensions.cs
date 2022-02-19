using System.Windows.Forms.DataVisualization.Charting;
using MarketOps.Controls.PriceChart;
using System.Collections.Generic;
using MarketOps.SystemData.Types;
using System;

namespace MarketOps.Controls.Extensions
{
    /// <summary>
    /// extensions for traling stops on PriceVolumeChart
    /// </summary>
    public static class PriceVolumeChartTrailingStopsExtensions
    {
        public static void AddPositionsTrailingStops(this PriceVolumeChart chart, List<Position> positions)
        {
            InitializeTrailingStopsSeriesWithEmptyPoints(chart);
            foreach (var position in positions)
                AddTrailingStopData(chart, position.TrailingStop);
            chart.TrailingStopL.Enabled = true;
        }

        private static void InitializeTrailingStopsSeriesWithEmptyPoints(PriceVolumeChart chart)
        {
            chart.TrailingStopL.Points.Clear();
            for (int i = 0; i < chart.PricesCandles.Points.Count; i++)
                AddDisabledTrailingStopValue(chart.TrailingStopL, chart.PricesCandles.Points[i].XValue);
        }

        private static void AddTrailingStopData(PriceVolumeChart chart, List<PositionTrailingStopData> data)
        {
            int startIndex = FindTSIndex(chart, data[0].TS);
            for (int i = 0; i < data.Count; i++)
                EnableTrailingStopValue(chart.TrailingStopL, startIndex + i, data[i].Value);
            if ((startIndex + data.Count) < chart.PricesCandles.Points.Count)
                EnableTrailingStopValue(chart.TrailingStopL, startIndex + data.Count, data[data.Count - 1].Value);
        }

        private static void AddDisabledTrailingStopValue(Series series, double ts)
        {
            int i = series.Points.AddXY(ts, 0);
            series.Points[i].IsEmpty = true;
        }

        private static int FindTSIndex(PriceVolumeChart chart, DateTime ts) => 
            chart.PricesCandles.Points.IndexOf(chart.PricesCandles.Points.FindByValue(ts.ToOADate(), "X"));

        private static void EnableTrailingStopValue(Series series, int index, float value)
        {
            series.Points[index].YValues[0] = value;
            series.Points[index].IsEmpty = false;
        }
    }
}
