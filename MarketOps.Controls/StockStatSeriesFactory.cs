using System.Windows.Forms.DataVisualization.Charting;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.Controls
{
    /// <summary>
    /// Series factory for stock stats
    /// </summary>
    internal class StockStatSeriesFactory
    {
        public Series CreateSeries(StockStat stat, int seriesIndex) => new Series(stat.ChartSeriesName(seriesIndex))
        {
            ChartArea = stat.ChartArea,
            ChartType = SeriesChartType.Line,
            Color = stat.DataColor[seriesIndex],
            Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
            IsXValueIndexed = true,
            YValueType = ChartValueType.Single
        };
    }
}
