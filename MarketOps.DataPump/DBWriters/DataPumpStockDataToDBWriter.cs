using System;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.DBWriters
{
    /// <summary>
    /// DB writer executing single query at a time.
    /// </summary>
    internal class DataPumpStockDataToDBWriter : IDataPumpStockDataToDBWriter
    {
        private readonly IDataPumpProvider _dataPumpProvider;

        public DataPumpStockDataToDBWriter(IDataPumpProvider dataPumpProvider)
        {
            _dataPumpProvider = dataPumpProvider;
        }

        public void StartSession()
        {
        }

        public void EndSession()
        {
        }

        public void WriteDaily(DataPumpStockData data, StockDefinition stockDefinition)
        {
            throw new NotImplementedException();
        }
    }
}
