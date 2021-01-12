using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Controls.ChartsUtils
{
    /// <summary>
    /// Calculates zoom start and end for selected chart.
    /// Zoomin in cursor position, or on current view if ctrl pressed.
    /// Zoomout always on current view.
    /// </summary>
    internal class ChartZoomCalculator
    {
        public Tuple<double, double> CalculateZoom(bool wheelDown, bool ctrlDown, Axis xAxis, Point cursorLocation)
        {
            double xmin = xAxis.ScaleView.ViewMinimum;
            double xmax = xAxis.ScaleView.ViewMaximum;
            double xmid = (wheelDown || ctrlDown) ? xmin + (xmax - xmin) / 2 : xAxis.PixelPositionToValue(cursorLocation.X);

            const double zoomScale = 0.9;
            double halfzoomwidth = wheelDown ? ((xmax - xmin) / zoomScale / 2) : ((xmax - xmin) * zoomScale / 2);
            double zoomstart = Math.Max(xmid - halfzoomwidth + 1, 0);
            double zoomend = Math.Min(xmid + halfzoomwidth - 1, xAxis.Maximum);

            return new Tuple<double, double>(zoomstart, zoomend);
        }
    }
}
