using ScottPlot;

namespace MarketOps.Controls.ChartsUtils
{
    /// <summary>
    /// Extensions to ScottPlot.FormsPlot.
    /// </summary>
    internal static class FormsPlotExtensions
    {
        public static void RemoveDefaultRightClickEvent(this FormsPlot formsPlot) =>
            formsPlot.RightClicked -= formsPlot.DefaultRightClickEvent;
    }
}
