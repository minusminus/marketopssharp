using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Controls
{
    /// <summary>
    /// Calculates Y axis range for current X axis selection.
    /// Rounds result values up and down.
    /// </summary>
    internal class ChartYViewRangeCalculator
    {
        public Tuple<double, double> CalculateRange(Axis xAxis, DataPointCollection dataPoints)
        {
            int istart = (int)xAxis.ScaleView.ViewMinimum;
            int istop = Math.Min((int)xAxis.ScaleView.ViewMaximum, dataPoints.Count);
            double maxy = double.MinValue;
            double miny = double.MaxValue;
            for (int i = istart; i < istop; i++)
            {
                maxy = Math.Max(maxy, dataPoints[i].YValues[0]);    //H
                miny = Math.Min(miny, dataPoints[i].YValues[1]);    //L
            }
            double addition = (maxy - miny) * 0.05;
            return new Tuple<double, double>(Math.Floor(miny - addition), Math.Ceiling(maxy + addition));
        }
    }
}
