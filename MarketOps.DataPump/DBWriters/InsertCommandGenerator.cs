using System;
using MarketOps.StockData.Interfaces;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.DBWriters
{
    /// <summary>
    /// Generator of insert command for stock data to be written do db.
    /// </summary>
    internal class InsertCommandGenerator
    {
        private readonly IDataPumpProvider _dataPumpProvider;

        public InsertCommandGenerator(IDataPumpProvider dataPumpProvider)
        {
            _dataPumpProvider = dataPumpProvider;
        }

        private string PrepareFloat(string value)
        {
            return value.Replace(',', '.');
        }

        private string PrepareDate(DateTime ts)
        {
            return ts.ToString("yyyy-MM-dd");
        }

        public string InsertDaily(DataPumpStockData data, StockDefinition stockDefinition)
        {
            return $"insert into {_dataPumpProvider.GetTableName(stockDefinition.Type, StockDataRange.Day, 0)}(fk_id_spolki, ts, open, high, low, close, refcourse, volume) values " +
                $"({stockDefinition.ID}, to_date('{PrepareDate(data.TS)}', 'YYYY-MM-DD'), " +
                $"{PrepareFloat(data.O)}, {PrepareFloat(data.H)}, {PrepareFloat(data.L)}, {PrepareFloat(data.C)}, {PrepareFloat(data.RefCourse)}, {data.V})";
        }
    }
}
