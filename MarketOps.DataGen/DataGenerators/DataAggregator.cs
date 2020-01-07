using System;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.DataGen.DataGenerators
{
    /// <summary>
    /// Aggregate stock data generator.
    /// </summary>
    internal class DataAggregator : IDataGen
    {
        private readonly IDataGenProvider _provider;

        public DataAggregator(IDataGenProvider provider)
        {
            _provider = provider;
        }

        public void GenerateWeekly(StockDefinition stockDefinition)
        {
            GenerateData(stockDefinition, StockDataRange.Weekly, "week");
        }

        public void GenerateMonthly(StockDefinition stockDefinition)
        {
            GenerateData(stockDefinition, StockDataRange.Monthly, "month");
        }

        private void GenerateData(StockDefinition stockDefinition, StockDataRange generateRange, string dataRange)
        {
            DateTime tsFrom = _provider.GetMaxTS(stockDefinition, generateRange, 0);
            string destTable = _provider.GetTableName(stockDefinition.Type, generateRange, 0);
            string srcTable = _provider.GetTableName(stockDefinition.Type, StockDataRange.Daily, 0);

            if (tsFrom != DateTime.MinValue)
                DeleteLastTSRow(stockDefinition.ID, destTable, tsFrom);
            InsertAllRows(stockDefinition.ID, destTable, srcTable, dataRange, tsFrom);
        }

        private void DeleteLastTSRow(int stockId, string destTableName, DateTime ts)
        {
            string qry = $"delete {destTableName} where fk_id_spolki = {stockId} and ts = date '{ts.ToString("yyyy-MM-dd")}'";
            _provider.ExecuteSQL(qry);
        }

        private void InsertAllRows(int stockId, string destTableName, string srcTableName, string dataRange, DateTime tsFrom)
        {
            /*
            query pattern for monthly/weekly data generation:

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
             */
            string qry =
                $"insert into {destTableName}(fk_id_spolki, ts, open, high, low, close, volume)" +
                $"select {stockId} as \"fk_id_spolki\", data.grouper as \"ts\", (min(array[data.id, data.open]))[2] as \"open\", max(data.high) as \"high\", min(data.low) as \"low\", (max(array[data.id, data.close]))[2] as \"close\", sum(data.volume) as \"volume\" " +
                $"from " +
                $"( " +
                $"select *, date_trunc('{dataRange}', ts) as \"grouper\" " +
                $"from {srcTableName} " +
                $"where fk_id_spolki = {stockId} " +
                $"and ts >= date_trunc('{dataRange}', date '{tsFrom.ToString("yyyy-MM-dd")}') " +
                $") data " +
                $"group by data.grouper " +
                $"order by data.grouper";

            _provider.ExecuteSQL(qry);
        }
    }
}
