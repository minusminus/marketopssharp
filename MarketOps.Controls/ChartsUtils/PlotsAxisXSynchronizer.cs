using ScottPlot;

namespace MarketOps.Controls.ChartsUtils
{
    /// <summary>
    /// Synchronizes plots on X axis only.
    /// </summary>
    internal class PlotsAxisXSynchronizer : BasePlotsAxisSynchronizer
    {
        public PlotsAxisXSynchronizer(params FormsPlot[] formsPlots) : base(formsPlots) { }

        protected override void SetLimits(FormsPlot formsPlot, AxisLimits newAxisLimits) => 
            formsPlot.Plot.SetAxisLimitsX(newAxisLimits.XMin, newAxisLimits.XMax);
    }
}
