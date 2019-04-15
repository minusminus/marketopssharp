using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Charting
{
    /// <summary>
    /// base class for chart data area preparation
    /// </summary>
    internal class ChartAreaPreparatorBase
    {
        protected void PrepareAxis(Axis axis)
        {
            axis.MajorGrid.LineColor = Color.LightGray;
            axis.LineColor = Color.DarkGray;
            axis.MajorTickMark.LineColor = Color.DarkGray;
            axis.LabelStyle.ForeColor = Color.DarkGray;
        }
    }
}
