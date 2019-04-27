using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.Extensions
{
    /// <summary>
    /// extensions for StockVolumeChart
    /// </summary>
    public static class StockVolumeChartExtensions
    {
        public static void ClearStockData(this StockVolumeChart chart)
        {
            chart.PricesCandles.Points.Clear();
            chart.Volume.Points.Clear();
        }

        public static void LoadStockData(this StockVolumeChart chart, StockPricesData data)
        {
            ClearStockData(chart);
            AppendStockData(chart, data);
        }

        public static void AppendStockData(this StockVolumeChart chart, StockPricesData data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                int ix = chart.PricesCandles.Points.AddXY(data.TS[i], data.H[i]);
                chart.PricesCandles.Points[ix].YValues[1] = data.L[i];
                chart.PricesCandles.Points[ix].YValues[2] = data.O[i];
                chart.PricesCandles.Points[ix].YValues[3] = data.C[i];

                chart.PricesLine.Points.AddXY(data.TS[i], data.C[i]);
                chart.Volume.Points.AddXY(data.TS[i], data.V[i]);
            }
        }

        public static void PrependStockData(this StockVolumeChart chart, StockPricesData data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                chart.PricesCandles.Points.InsertXY(i, data.TS[i], data.H[i]);
                chart.PricesCandles.Points[i].YValues[1] = data.L[i];
                chart.PricesCandles.Points[i].YValues[2] = data.O[i];
                chart.PricesCandles.Points[i].YValues[3] = data.C[i];

                chart.PricesLine.Points.InsertXY(i, data.TS[i], data.C[i]);
                chart.Volume.Points.InsertXY(i, data.TS[i], data.V[i]);
            }
        }
    }
}
