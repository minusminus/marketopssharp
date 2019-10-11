using System;
using MarketOps.StockData.Interfaces;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.DBWriters
{
    /// <summary>
    /// DB writer executing single query at a time.
    /// </summary>
    internal class DataPumpStockDataToDBWriter : IDataPumpStockDataToDBWriter
    {
        private readonly IDataPumpProvider _dataPumpProvider;
        private readonly InsertCommandGenerator _commandGenerator;

        public DataPumpStockDataToDBWriter(IDataPumpProvider dataPumpProvider, InsertCommandGenerator commandGenerator)
        {
            _dataPumpProvider = dataPumpProvider;
            _commandGenerator = commandGenerator;
        }

        public void StartSession()
        {
        }

        public void EndSession()
        {
        }

        public void WriteDaily(DataPumpStockData data, StockDefinition stockDefinition)
        {
            _dataPumpProvider.ExecuteSQL(_commandGenerator.InsertDaily(data, stockDefinition));
        }
    }
}
