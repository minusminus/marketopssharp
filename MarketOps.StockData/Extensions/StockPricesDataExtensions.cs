using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
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

        /// <summary>
        /// returns datetime format for data range select controls
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataRangeDateTimeInputFormat(this StockPricesData data)
        {
            if ((data.Range == StockDataRange.Intraday) || (data.Range == StockDataRange.Tick)) return "yyyy-MM-dd hh:mm";
            return "yyyy-MM-dd";
        }

        /// <summary>
        /// returns format string to convert datetime acording to range
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataRangeFormatString(this StockPricesData data)
        {
            Dictionary<StockDataRange, string> formatStrings = new Dictionary<StockDataRange, string>()
            {
                { StockDataRange.Undefined, "yyyy-MM-dd" },
                { StockDataRange.Day, "yyyy-MM-dd" },
                { StockDataRange.Week, "yyyy-MM-dd" },
                { StockDataRange.Month, "yyyy-MM-dd" },
                { StockDataRange.Intraday, "yyyy-MM-dd hh:mm" },
                { StockDataRange.Tick, "yyyy-MM-dd hh:mm:ss" },
            };
            return formatStrings[data.Range];
        }

        /// <summary>
        /// returns range description
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataRangeToString(this StockPricesData data)
        {
            switch (data.Range)
            {
                case StockDataRange.Month:
                    return "monthly";
                case StockDataRange.Week:
                    return "weekly";
                case StockDataRange.Day:
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
    }
}
