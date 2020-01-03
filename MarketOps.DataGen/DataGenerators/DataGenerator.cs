using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.DataGen.DataGenerators
{
    /// <summary>
    /// Aggregate stock data generator.
    /// </summary>
    internal class DataGenerator
    {
        private readonly IDataGenProvider _provider;

        public DataGenerator(IDataGenProvider provider)
        {
            _provider = provider;
        }

        /*
        wzor zapytania mieiseczne/tygodniowe:

        insert into at_mies_test(fk_id_spolki, ts, open, high, low, close, volume)
        select 289 as "fk_id_spolki", data.grouper as "ts", (min(array[data.id, data.open]))[2] as "open", max(data.high) as "high", min(data.low) as "low", (max(array[data.id, data.close]))[2] as "close", sum(data.volume) as "volume"
        from 
        (
        select *, date_trunc('month', ts) as "grouper" --, date_trunc('week', ts)
        from at_dzienne1
        where fk_id_spolki = 289
        and ts >= date_trunc('month', date '2019-01-01')
        ) data
        group by data.grouper
        order by data.grouper

        update at_mies_test
        set open = T.open, high = T.high, low = T.low, close = T.close, volume = T.volume
        from
        (
        select 289 as "fk_id_spolki", data.grouper as "ts", (min(array[data.id, data.open]))[2] as "open", max(data.high) as "high", min(data.low) as "low", (max(array[data.id, data.close]))[2] as "close", sum(data.volume) as "volume"
        from 
        (
        select *, date_trunc('month', ts) as "grouper" --, date_trunc('week', ts)
        from at_dzienne1
        where fk_id_spolki = 289
        and ts >= date_trunc('month', date '2019-01-01')
        ) data
        where data.grouper = date_trunc('month', date '2019-01-01')
        group by data.grouper
        ) T
        where at_mies_test.fk_id_spolki=T.fk_id_spolki and at_mies_test.ts=T.ts
  
         */

        public void GenerateMonthly(StockDefinition stockDefinition)
        {
            DateTime tsFrom = _provider.GetMaxTS(stockDefinition, StockDataRange.Monthly, 0);
            string dataTable = _provider.GetTableName(stockDefinition.Type, StockDataRange.Daily, 0);

            if (tsFrom != DateTime.MinValue)
            {
                UpdateLastTSRow(stockDefinition.ID, dataTable, "month", tsFrom);
                InsertNextRows(stockDefinition.ID, dataTable, "month", tsFrom);
            }
            else
                InsertAllRows(stockDefinition.ID, dataTable, "month", tsFrom);
        }

        private string GetDataQuery(int stockId, string dataTableName, string dataRange, DateTime tsFrom, string rowSelectorOp)
        {
            return
                $"select {stockId} as \"fk_id_spolki\", data.grouper as \"ts\", (min(array[data.id, data.open]))[2] as \"open\", max(data.high) as \"high\", min(data.low) as \"low\", (max(array[data.id, data.close]))[2] as \"close\", sum(data.volume) as \"volume\" " +
                $"from " +
                $"( " +
                $"select *, date_trunc('{dataRange}', ts) as \"grouper\" " +
                $"from {dataTableName} " +
                $"where fk_id_spolki = {stockId} " +
                $"and ts >= date_trunc('{dataRange}', date '{tsFrom.ToString("yyyy-MM-dd")}') " +
                $") data " +
                $"where data.grouper {rowSelectorOp} date_trunc('{dataRange}', date '{tsFrom.ToString("yyyy-MM-dd")}') " +
                $"group by data.grouper " +
                $"order by data.grouper";
        }

        private void UpdateLastTSRow(int stockId, string dataTableName, string dataRange, DateTime tsFrom)
        {
            string qry =
                "update at_mies_test " +
                "set open = T.open, high = T.high, low = T.low, close = T.close, volume = T.volume " +
                "from " +
                "( " +
                GetDataQuery(stockId, dataTableName, dataRange, tsFrom, "=") +
                ") T " +
                "where at_mies_test.fk_id_spolki = T.fk_id_spolki and at_mies_test.ts = T.ts";

            _provider.ExecuteSQL(qry);
        }

        private void InsertNextRows(int stockId, string dataTableName, string dataRange, DateTime tsFrom)
        {
            string qry =
                "insert into at_mies_test(fk_id_spolki, ts, open, high, low, close, volume)" +
                GetDataQuery(stockId, dataTableName, dataRange, tsFrom, ">");

            _provider.ExecuteSQL(qry);
        }

        private void InsertAllRows(int stockId, string dataTableName, string dataRange, DateTime tsFrom)
        {
            string qry =
                "insert into at_mies_test(fk_id_spolki, ts, open, high, low, close, volume)" +
                GetDataQuery(stockId, dataTableName, dataRange, tsFrom, ">=");

            _provider.ExecuteSQL(qry);
        }
    }
}
