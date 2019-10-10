using System;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Interface for stock price data writing to db
    /// </summary>
    internal interface IDataPumpStockDataToDBWriter
    {
        void StartSession();
        void EndSession();
        void WriteDaily(DataPumpStockData data, StockDefinition stockDefinition);
    }
}
