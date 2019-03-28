using System;
using MarketOps.StockData.Types;

namespace MarketOps.StockData.Interfaces
{
    /// <summary>
    /// Interface for external source data provider
    /// </summary>
    public interface IStockDataProvider
    {
        StockPricesData GetPricesData(int stockID, DateTime tsFrom, DateTime tsTo);
        StockDefinition GetStockDefinition(int stockID);
    }
}
