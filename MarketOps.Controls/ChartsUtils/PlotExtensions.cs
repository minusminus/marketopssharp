using ScottPlot.Plottable;
using ScottPlot;

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
