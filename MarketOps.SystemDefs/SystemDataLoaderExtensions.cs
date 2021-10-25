using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using System;

namespace MarketOps.SystemDefs
{
    /// <summary>
    /// Extensions for ISystemDataLoader
    /// </summary>
    internal static class SystemDataLoaderExtensions
    {
        /// <summary>
        /// Gets stockpricesdata and index of specified ts. Returns true if index found.
        /// </summary>
        /// <param name="dataLoader"></param>
        /// <param name="stockName"></param>
        /// <param name="dataRange"></param>
        /// <param name="ts"></param>
        /// <param name="spData"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public static bool GetWithIndex(this ISystemDataLoader dataLoader, string stockName, StockDataRange dataRange, DateTime ts,
            out StockPricesData spData, out int dataIndex)
        {
            spData = dataLoader.Get(stockName, dataRange, 0, ts, ts);
            dataIndex = spData.FindByTS(ts);
            return dataIndex >= 0;
        }

        /// <summary>
        /// Gets stockpricesdata and index of specified ts. Returns true if index found, and has required length of back buffer.
        /// </summary>
        /// <param name="dataLoader"></param>
        /// <param name="stockName"></param>
        /// <param name="dataRange"></param>
        /// <param name="ts"></param>
        /// <param name="requiredBackBufferLength"></param>
        /// <param name="spData"></param>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public static bool GetWithIndex(this ISystemDataLoader dataLoader, string stockName, StockDataRange dataRange, DateTime ts, int requiredBackBufferLength,
            out StockPricesData spData, out int dataIndex)
        {
            if (GetWithIndex(dataLoader, stockName, dataRange, ts, out spData, out dataIndex))
                return dataIndex >= requiredBackBufferLength;
            return false;
        }
    }
}
