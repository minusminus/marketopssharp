using MarketOps.StockData.Types;
using System;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Heikin-Ashi calculator.
    /// </summary>
    public static class HeikinAshi
    {
        public static HeikinAshiData Calculate(in StockPricesData data) => 
            CanCalculate(data)
                ? CalcualteData(data)
                : new HeikinAshiData(0);

        private static bool CanCalculate(in StockPricesData data) => 
            (data.Length > 1);

        private static HeikinAshiData CalcualteData(in StockPricesData data)
        {
            var result = new HeikinAshiData(data.Length - 1);

            for (int i = 1; i < data.Length; i++)
            {
                result.C[i - 1] = (data.O[i] + data.H[i] + data.L[i] + data.C[i]) / 4f;
                result.O[i - 1] = (data.O[i - 1] + data.C[i - 1]) / 2f;
                result.H[i - 1] = Math.Max(data.H[i], Math.Max(result.O[i - 1], result.C[i - 1]));
                result.L[i - 1] = Math.Min(data.L[i], Math.Min(result.O[i - 1], result.C[i - 1]));
                result.TS[i - 1] = data.TS[i];
            }

            return result;
        }
    }
}
