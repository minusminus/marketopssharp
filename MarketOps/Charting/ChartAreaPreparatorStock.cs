using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Charting
{
    /// <summary>
    /// prepares chart data area for stock data
    /// </summary>
    internal class ChartAreaPreparatorStock : ChartAreaPreparatorBase
    {
        public void Prepare(ChartArea area)
        {
            area.AxisX.IsStartedFromZero = false;
            area.CursorX.IsUserEnabled = true;
            area.CursorY.IsUserEnabled = true;
            PrepareAxis(area.AxisX);
            PrepareAxis(area.AxisY);
        }
    }
}
