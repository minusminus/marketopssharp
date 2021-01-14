using System;
using MarketOps.StockData.Types;

namespace MarketOps.SystemData.Interfaces
{
    /// <summary>
    /// Interface for systme processing stock data loading.
    /// </summary>
    public interface ISystemDataLoader
    {
        StockPricesData Get(string stockName, StockDataRange dataRange, int intradayInterval, DateTime tsFrom, DateTime tsTo);
    }
}
