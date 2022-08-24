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
        public static StockPricesData Merge(this StockPricesData original, StockPricesData toMerge) => 
            StockPricesDataMerger.Merge(original, toMerge);

        /// <summary>
        /// finds index of data by specified timestamp
        /// assumes data is sorted by TS
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static int FindByTS(this StockPricesData data, DateTime ts) => 
            Array.BinarySearch<DateTime>(data.TS, ts);

        /// <summary>
        /// finds index of first data greater or equal to specified timestamp
        /// assumes data is sorted by TS
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static int FindByTSGE(this StockPricesData data, DateTime ts)
        {
            int i = Array.BinarySearch<DateTime>(data.TS, ts);
            return (i >= 0) ? i : ((~i < data.Length) ? ~i : i);
        }

        /// <summary>
        /// finds index of first data lower or equal to specified timestamp
        /// assumes data is sorted by TS
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static int FindByTSLE(this StockPricesData data, DateTime ts)
        {
            int i = Array.BinarySearch<DateTime>(data.TS, ts);
            return (i >= 0) ? i : ~i - 1;
        }

        /// <summary>
        /// returns datetime format for data range select controls
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataRangeDateTimeInputFormat(this StockPricesData data) => DataFormatting.DataRangeDateTimeInputFormat(data.Range);

        /// <summary>
        /// returns format string to convert datetime acording to range
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataRangeFormatString(this StockPricesData data) => DataFormatting.DataRangeFormatString(data.Range);

        /// <summary>
        /// returns range description
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataRangeToString(this StockPricesData data)
        {
            switch (data.Range)
            {
                case StockDataRange.Monthly:
                    return "monthly";
                case StockDataRange.Weekly:
                    return "weekly";
                case StockDataRange.Daily:
                    return "daily";
                case StockDataRange.Intraday:
                    return data.IntrradayInterval%60 == 0
                        ? $"intra {data.IntrradayInterval/60}h"
                        : $"intra {data.IntrradayInterval}min";
                case StockDataRange.Tick:
                    return "ticks";
            }
            return "undefined";
        }

        /// <summary>
        /// returns minimum L value in specified range (length elements from startIndex)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static float MinOfL(this StockPricesData data, int startIndex, int length)
        {
            float min = data.L[startIndex];
            for (int i = 1; i < length; i++)
                if (data.L[startIndex - i] < min)
                    min = data.L[startIndex - i];
            return min;
        }

        /// <summary>
        /// returns maximum H value in specified range (length elements from startIndex)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static float MaxOfH(this StockPricesData data, int startIndex, int length)
        {
            float max = data.H[startIndex];
            for (int i = 1; i < length; i++)
                if (data.H[startIndex - i] > max)
                    max = data.H[startIndex - i];
            return max;
        }
    }
}
