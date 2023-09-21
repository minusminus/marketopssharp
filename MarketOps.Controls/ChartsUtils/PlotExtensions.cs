using ScottPlot.Plottable;
using ScottPlot;
using System.Drawing;

namespace MarketOps.Controls.ChartsUtils
{
    /// <summary>
    /// Extensions to ScottPlot.Plot.
    /// </summary>
    internal static class PlotExtensions
    {
        public static void SetUpPlotArea(this Plot plot)
        {
            plot.XAxis.Color(PlotConsts.AxisColor);
            plot.XAxis.TickLabelStyle(fontSize: PlotConsts.AxisTextSize, color: PlotConsts.AxisTextColor);
            plot.YAxis.Color(PlotConsts.AxisColor);
            plot.YAxis.TickLabelStyle(fontSize: PlotConsts.AxisTextSize, color: PlotConsts.AxisTextColor);
            plot.XAxis2.Hide();
            plot.YAxis2.Hide();
            plot.Layout(padding: 0);
        }

        public static void SetUpBottomPlotXAxis(this Plot plot)
        {
            plot.XAxis.SetSizeLimit(PlotConsts.BottomPlotXTicksSize, PlotConsts.BottomPlotXTicksSize);
            plot.XAxis.Ticks(false);
        }

        public static void AddVerticalLines(this Plot plot, in double[] verticalLines, Color color)
        {
            for (int i = 0; i < verticalLines.Length; i++)
                plot.AddVerticalLine(x: verticalLines[i], color: color);
        }

        public static Tooltip CreateTooltip(this Plot plot)
        {
            var result = plot.AddTooltip(" ", 0, 0);
            result.LabelPadding = 2;
            result.Font.Size = PlotConsts.TooltipTextSize;
            result.Font.Color = PlotConsts.AxisTextColor;
            result.BorderWidth = 1;
            result.BorderColor = PlotConsts.AxisColor;
            result.HitTestEnabled = false;
            result.IsVisible = false;
            return result;
        }
    }
}
