using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Charting
{
    /// <summary>
    /// prepares chart data series for volume data
    /// </summary>
    internal class DataSeriesPreparatorVolume
    {
        public void Prepare(Series series)
        {
            series.XValueType = ChartValueType.Date;
            series.YValueType = ChartValueType.Int64;
            series.IsXValueIndexed = true;
            series.Color = Color.RoyalBlue;
        }
    }
}
