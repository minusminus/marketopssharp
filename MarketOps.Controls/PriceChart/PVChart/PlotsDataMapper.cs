using MarketOps.Stats.Calculators;
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

        public static OHLC[] MapToOHLCData(this HeikinAshiData data)
        {
            if (data.C.Length == 0)
                return new OHLC[0];

            //ScottPlot can't skip rendering this OHLC element
            //so it is set like this until ScottPlot will be able to skip OHLC elements like double.NaN
            var firstEmptyElement = new OHLC[] { new OHLC(data.O[0], data.O[0], data.O[0], data.O[0], 0, 1) };

            return firstEmptyElement
                .Concat(
                    Enumerable.Range(0, data.O.Length)
                        .Select(i => MapToOHLC(i))
                )
                .ToArray();

            OHLC MapToOHLC(int index) =>
                new OHLC(data.O[index], data.H[index], data.L[index], data.C[index], data.TS[index].ToOADate(), 1);
        }

        public static double[] MapToCloseData(this StockPricesData data) =>
            data.C
                .Select(x => (double)x)
                .ToArray();

        public static double[] MapToVolumeData(this StockPricesData data) =>
            data.V
                .Select(x => (double)x)
                .ToArray();
    }
}
