using System;
using System.Collections.Generic;
using MarketOps.StockData.Types;

namespace MarketOps.StockData.Interfaces
{
    /// <summary>
    /// Interface for data pump database provider
    /// </summary>
    public interface IDataPumpProvider
    {
        List<StockDefinition> GetAllStockDefinitions();
        Dictionary<StockType, DataPumpDownloadDefinition> GetDownloadDefinitions();
        void ExecuteSQL(string qry);
    }
}
