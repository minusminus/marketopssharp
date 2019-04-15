using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Charting
{
    /// <summary>
    /// prepares chart data series for stock data
    /// </summary>
    internal class DataSeriesPreparatorStock
    {
        public void Prepare(Series series)
        {
            series.XValueType = ChartValueType.Date;
            series.YValueType = ChartValueType.Single;
            series.IsXValueIndexed = true;
            series.CustomProperties = "PriceDownColor=Black,PriceUpColor=White";
            series.BorderColor = Color.Black;
            series.Color = Color.Black;
        }
    }
}
