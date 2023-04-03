using MarketOps.Controls.ChartsUtils;
using ScottPlot.Plottable;
using ScottPlot;

namespace MarketOps.Controls.PriceChart.PVChart
{
    /// <summary>
    /// Formatting plots.
    /// </summary>
    internal static class PlotsFormatter
    {
        const float YAxisSizeLimit = 50;

        public static void SetUpFormsPlot(this FormsPlot formsPlot)
        {
            formsPlot.Plot.SetUpPlotArea();
            formsPlot.RemoveDefaultRightClickEvent();
            formsPlot.Plot.YAxis.SetSizeLimit(YAxisSizeLimit, YAxisSizeLimit);
        }

        public static void SetUpAdditionalFormsPlot(this FormsPlot formsPlot)
        {
            SetUpFormsPlot(formsPlot);
            formsPlot.Plot.SetUpBottomPlotXAxis();
            formsPlot.Configuration.LockVerticalAxis = true;
        }

        public static void SetUpPricesPlot(this FinancePlot plot)
        {
            plot.ColorUp = PlotConsts.CandleColorUp;
            plot.ColorDown = PlotConsts.CandleColorDown;
            plot.WickColor = PlotConsts.CandleColorDown;
            plot.Sequential = true;
        }

        public static void SetUpVolumePlot(this BarPlot plot)
        {
            plot.Color = PlotConsts.PrimaryPointColor;
            plot.BarWidth = 0.6;
        }
    }
}
