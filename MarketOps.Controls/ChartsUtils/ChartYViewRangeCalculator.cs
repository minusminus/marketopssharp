using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Controls.ChartsUtils
{
    /// <summary>
    /// Calculates Y axis range for current X axis selection.
    /// Rounds result values up and down.
    /// </summary>
    internal static class ChartYViewRangeCalculator
    {
        public static Tuple<double, double> InitialRange()
        {
            return new Tuple<double, double>(double.MaxValue, double.MinValue);
        }

        public static Tuple<double, double> PostprocessRange(Tuple<double, double> currRange)
        {
            double addition = (currRange.Item2 - currRange.Item1) * 0.05;
            return new Tuple<double, double>(Math.Floor(currRange.Item1 - addition), Math.Ceiling(currRange.Item2 + addition));
        }

        public static Tuple<double, double> CalculateRangeCandles(Axis xAxis, DataPointCollection dataPoints, Tuple<double, double> currRange)
        {
            int istart = (int)xAxis.ScaleView.ViewMinimum;
            int istop = Math.Min((int)xAxis.ScaleView.ViewMaximum, dataPoints.Count);
            double maxy = currRange.Item2;
            double miny = currRange.Item1;
            for (int i = istart; i < istop; i++)
                if (!dataPoints[i].IsEmpty)
                {
                    maxy = Math.Max(maxy, dataPoints[i].YValues[0]);    //H
                    miny = Math.Min(miny, dataPoints[i].YValues[1]);    //L
                }
            return new Tuple<double, double>(miny, maxy);
        }

        public static Tuple<double, double> CalculateRangeLine(Axis xAxis, DataPointCollection dataPoints, Tuple<double, double> currRange)
        {
            int istart = (int)xAxis.ScaleView.ViewMinimum;
            int istop = Math.Min((int)xAxis.ScaleView.ViewMaximum, dataPoints.Count);
            double maxy = currRange.Item2;
            double miny = currRange.Item1;
            for (int i = istart; i < istop; i++)
                if (!dataPoints[i].IsEmpty)
                {
                    maxy = Math.Max(maxy, dataPoints[i].YValues[0]);
                    miny = Math.Min(miny, dataPoints[i].YValues[0]);
                }
            return new Tuple<double, double>(miny, maxy);
        }
    }
}
