using MarketOps.StockData.Types;
using ScottPlot;
using System.Linq;

namespace MarketOps.Controls.PriceChart.PVChart
{
    /// <summary>
    /// Maps source data to plots data.
    /// </summary>
    internal static class PlotsDataMapper
    {
        public static OHLC[] MapToOHLCData(this StockPricesData data)
        {
            return Enumerable.Range(0, data.Length)
                .Select(i => MapToOHLC(i))
                .ToArray();

            OHLC MapToOHLC(int index) =>
                //new OHLC(data.O[index], data.H[index], data.L[index], data.C[index], data.TS[index].ToOADate(), 1, (double)data.V[index]);
                new OHLC(data.O[index], data.H[index], data.L[index], data.C[index], data.TS[index].ToOADate(), 1);
        }

        public static double[] MapToVolumeData(this StockPricesData data) =>
            data.V
                .Select(x => (double)x)
                .ToArray();
    }
}
