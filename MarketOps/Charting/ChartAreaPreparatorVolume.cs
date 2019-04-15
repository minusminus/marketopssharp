using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Charting
{
    internal class ChartAreaPreparatorVolume : ChartAreaPreparatorBase
    {
        public void Prepare(ChartArea area)
        {
            area.AxisX.LabelStyle.Enabled = false;
            area.AxisX.MajorTickMark.Enabled = false;
            area.CursorX.IsUserEnabled = true;
            area.CursorY.IsUserEnabled = true;
            PrepareAxis(area.AxisX);
            PrepareAxis(area.AxisY);
        }
    }
}
