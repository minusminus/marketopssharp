using MarketOps.StockData.Types;
using System.Collections.Generic;

namespace MarketOps.StockData
{
    /// <summary>
    /// Data formatting methods
    /// </summary>
    public static class DataFormatting
    {
        private static readonly Dictionary<StockType, string> PriceValueFormats = new Dictionary<StockType, string>()
            {
                { StockType.Stock, "F4" },
                { StockType.Index, "F4" },
                { StockType.IndexFuture, "F4" },
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
        public static string FormatPrice(StockType stockType, double value) => value.ToString(PriceValueFormats[stockType]);

        public static string FormatStatValue(float value) => value.ToString("F2");

        private static readonly Dictionary<StockDataRange, string> DataRangeFormatStrings = new Dictionary<StockDataRange, string>()
            {
                { StockDataRange.Undefined, "yyyy-MM-dd" },
                { StockDataRange.Daily, "yyyy-MM-dd" },
                { StockDataRange.Weekly, "yyyy-MM-dd" },
                { StockDataRange.Monthly, "yyyy-MM-dd" },
                { StockDataRange.Intraday, "yyyy-MM-dd hh:mm" },
                { StockDataRange.Tick, "yyyy-MM-dd hh:mm:ss" },
            };

        /// <summary>
        /// returns format string to convert datetime acording to range
        /// </summary>
        /// <param name="dataRange"></param>
        /// <returns></returns>
        public static string DataRangeFormatString(StockDataRange dataRange) => DataRangeFormatStrings[dataRange];

        /// <summary>
        /// returns datetime format for data range select controls
        /// </summary>
        /// <param name="dataRange"></param>
        /// <returns></returns>
        public static string DataRangeDateTimeInputFormat(StockDataRange dataRange) =>
            (dataRange == StockDataRange.Intraday) || (dataRange == StockDataRange.Tick) ? "yyyy-MM-dd hh:mm" : "yyyy-MM-dd";
    }
}
