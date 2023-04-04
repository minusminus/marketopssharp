using MarketOps.Controls.ChartsUtils;
using ScottPlot.Plottable;
using ScottPlot;
using System.Windows.Forms;

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
            formsPlot.SetUpFormsPlot();
            formsPlot.Plot.SetUpBottomPlotXAxis();
            formsPlot.Configuration.LockVerticalAxis = true;
        }

        public static void DisplayOnControlsBottom(this FormsPlot formsPlot, Control parent, int height)
        {
            formsPlot.Height = height;
            formsPlot.Parent = parent;
            formsPlot.Dock = DockStyle.Bottom;
            formsPlot.Visible = true;
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
