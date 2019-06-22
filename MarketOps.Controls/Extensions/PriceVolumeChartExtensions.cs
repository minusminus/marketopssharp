using System;
using MarketOps.StockData.Types;
using System.Windows.Forms.DataVisualization.Charting;
using MarketOps.StockData.Extensions;

namespace MarketOps.Controls.Extensions
{
    /// <summary>
    /// extensions for PriceVolumeChart
    /// </summary>
    public static class PriceVolumeChartExtensions
    {
        public static void ClearStockData(this PriceVolumeChart chart)
        {
            chart.ClearAllSeriesData();
        }

        public static void LoadStockData(this PriceVolumeChart chart, StockPricesData data)
        {
            ClearStockData(chart);
            AppendStockData(chart, data);
        }

        public static void AppendStockData(this PriceVolumeChart chart, StockPricesData data)
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

        public static void AppendStockStatData(this PriceVolumeChart chart, StockPricesData data, StockStat stat)
        {
            for (int i = 0; i < stat.DataCount; i++)
            {
                Series s = chart.GetSeries(stat.ChartSeriesName(i));
                float[] currdata = stat.Data(i);
                int tsstartindex = data.Length - currdata.Length;
                for (int j = 0; j < tsstartindex; j++)
                {
                    int ix = s.Points.AddXY(data.TS[j], 0);
                    s.Points[ix].IsEmpty = true;
                }
                for (int j = 0; j < currdata.Length; j++)
                    s.Points.AddXY(data.TS[tsstartindex + j], currdata[j]);
            }
        }

        public static void PrependStockData(this PriceVolumeChart chart, StockPricesData data)
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
