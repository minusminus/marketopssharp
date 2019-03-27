using System;
using MarketOps.StockData.Types;

namespace MarketOps.StockData.Extensions
{
    /// <summary>
    /// StockPricesData extension methods
    /// </summary>
    public static class StockPricesDataExtensions
    {
        /// <summary>
        /// merges two prices data objects (adds one to another)
        /// </summary>
        /// <param name="original"></param>
        /// <param name="toMerge"></param>
        /// <returns></returns>
        public static StockPricesData Merge(this StockPricesData original, StockPricesData toMerge)
        {
            StockPricesDataMerger merger = new StockPricesDataMerger();
            return merger.Merge(original, toMerge);
        }

        /// <summary>
        /// finds index of data by specified timestamp
        /// assumes data is sorted by TS
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static int FindByTS(this StockPricesData data, DateTime ts)
        {
            return Array.BinarySearch<DateTime>(data.TS, ts);
        }
    }
}
