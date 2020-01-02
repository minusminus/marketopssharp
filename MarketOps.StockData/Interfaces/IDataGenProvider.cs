using System;
using MarketOps.StockData.Types;

namespace MarketOps.StockData.Interfaces
{
    /// <summary>
    /// Interface for data generation database provider
    /// </summary>
    public interface IDataGenProvider
    {
        string GetTableName(StockType stockType, StockDataRange dataRange, int intradayInterval);
        DateTime GetMaxTS(StockDefinition stockDefinition, StockDataRange dataRange, int intradayInterval);
        void ExecuteSQL(string qry);
    }
}
