﻿using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;

namespace MarketOps.StockData
{
    /// <summary>
    /// Data formatting methods
    /// </summary>
    public class DataFormatting
    {
        private static Dictionary<StockType, string> _priceValueFormats = new Dictionary<StockType, string>()
            {
                { StockType.Stock, "F4" },
                { StockType.Index, "F4" },
                { StockType.Future, "F4" },
                { StockType.InvestmentFund, "F2" },
                { StockType.NBPCurrency, "F2" },
                { StockType.Forex, "F4" },
            };

        /// <summary>
        /// returns formatted string of price value
        /// </summary>
        /// <param name="stockType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatPrice(StockType stockType, double value)
        {
            return value.ToString(_priceValueFormats[stockType]);
        }

        private static Dictionary<StockDataRange, string> _dataRangeFormatStrings = new Dictionary<StockDataRange, string>()
            {
                { StockDataRange.Undefined, "yyyy-MM-dd" },
                { StockDataRange.Day, "yyyy-MM-dd" },
                { StockDataRange.Week, "yyyy-MM-dd" },
                { StockDataRange.Month, "yyyy-MM-dd" },
                { StockDataRange.Intraday, "yyyy-MM-dd hh:mm" },
                { StockDataRange.Tick, "yyyy-MM-dd hh:mm:ss" },
            };

        /// <summary>
        /// returns format string to convert datetime acording to range
        /// </summary>
        /// <param name="dataRange"></param>
        /// <returns></returns>
        public static string DataRangeFormatString(StockDataRange dataRange)
        {
            return _dataRangeFormatStrings[dataRange];
        }

        /// <summary>
        /// returns datetime format for data range select controls
        /// </summary>
        /// <param name="dataRange"></param>
        /// <returns></returns>
        public static string DataRangeDateTimeInputFormat(StockDataRange dataRange)
        {
            if ((dataRange == StockDataRange.Intraday) || (dataRange == StockDataRange.Tick)) return "yyyy-MM-dd hh:mm";
            return "yyyy-MM-dd";
        }
    }
}