using System;
using System.Windows.Forms.DataVisualization.Charting;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.Controls
{
    /// <summary>
    /// Series creator for stock stats
    /// </summary>
    internal class StockStatSeriesCreator
    {
        public Series CreatePriceAreaSeries(StockStat stat, int seriesIndex)
        {
            Series res = new Series(stat.ChartSeriesName(seriesIndex));
            res.ChartArea = "areaPrices";
            res.ChartType = SeriesChartType.Line;
            res.Color = stat.DataColor(seriesIndex);
            res.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            res.IsXValueIndexed = true;
            res.YValueType = ChartValueType.Single;

            return res;
        }
    }
}
