using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using System;

namespace MarketOps.SystemData.Extensions
{
    /// <summary>
    /// Extensions to ISystemDataLoader interface
    /// </summary>
    public static class ISystemDataLoaderExtensions
    {
        public static (StockPricesData pricesData, int pricesDataIndex) GetPricesDataAndIndex(this ISystemDataLoader dataLoader, string stockName, StockDataRange dataRange, int intradayInterval, DateTime ts)
        {
            StockPricesData data = dataLoader.Get(stockName, dataRange, intradayInterval, ts, ts);
            return (data, data.FindByTS(ts));
        }
    }
}
