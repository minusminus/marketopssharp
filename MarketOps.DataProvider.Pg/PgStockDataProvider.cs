using System;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.DataProvider.Pg
{
    /// <summary>
    /// stock prices database provider for AT database on postgres
    /// </summary>
    public class PgStockDataProvider : PgBaseProvider, IStockDataProvider
    {
        private readonly DataTableSelector _tblSelector = new DataTableSelector();

        public StockDefinition GetStockDefinition(int stockID)
        {
            StockDefinition res = new StockDefinition();

            string qry = $"select * from at_spolki2 where id={stockID}";
            ProcessSelectQuery(qry, (reader) =>
            {
                if (!reader.HasRows)
                    throw new Exception($"No data for stock id={stockID}");
                reader.Read();
                PgDataToStockDefinitionConverter.ToStockDefinition(reader, res);
            });
            return res;
        }

        public StockDefinition GetStockDefinition(string stockName)
        {
            StockDefinition res = new StockDefinition();

            string qry = $"select * from at_spolki2 where stock_fullname='{stockName}' or stock_name='{stockName.ToUpper()}'";
            ProcessSelectQuery(qry, (reader) =>
            {
                if (!reader.HasRows)
                    throw new Exception($"No data for stock name = {stockName}");
                reader.Read();
                PgDataToStockDefinitionConverter.ToStockDefinition(reader, res);
            });
            return res;
        }

        public StockPricesData GetPricesData(StockDefinition stockDef, StockDataRange dataRange, int intradayInterval, DateTime tsFrom, DateTime tsTo)
        {
            PricesTemporalData tmp = new PricesTemporalData();

            string qry = $"select open, high, low, close, volume, ts from {_tblSelector.GetTableName(stockDef.Type, dataRange, intradayInterval)} where fk_id_spolki={stockDef.ID} and ts >= {tsFrom.ToTimestampQueryValue()} and ts <= {tsTo.ToTimestampQueryValue()} order by ts";
            ProcessSelectQuery(qry, (reader) =>
            {
                if (!reader.HasRows) return;
                tmp.AddAllRecords(reader);
            });

            StockPricesData res = tmp.ToStockPricesData();
            res.Range = dataRange;
            res.IntradayInterval = intradayInterval;
            return res;
        }

        public DateTime GetNearestTickGE(StockDefinition stockDef, StockDataRange dataRange, int intradayInterval, DateTime ts)
        {
            DateTime res = DateTime.MinValue;

            string qry = $"select min(ts) from {_tblSelector.GetTableName(stockDef.Type, dataRange, intradayInterval)} where fk_id_spolki={stockDef.ID} and ts >= {ts.ToTimestampQueryValue()}";
            ProcessSelectQuery(qry, (reader) =>
            {
                reader.Read();
                if (reader.IsDBNull(0))
                    throw new Exception($"No nearest tick data for stock name = {stockDef.FullName}");
                res = reader.GetFieldValue<DateTime>(0);
            });
            return res;
        }

        public DateTime GetNearestTickGETicksBefore(StockDefinition stockDef, StockDataRange dataRange, int intradayInterval, DateTime ts, int ticksBefore)
        {
            DateTime res = DateTime.MinValue;

            string qry =
                "select min(ts), count(*) " +
                "from " +
                "( " +
                "select tsWindow.ts " +
                "from " +
                "( " +
                "select min(ts) as \"mints\" " +
                $"from {_tblSelector.GetTableName(stockDef.Type, dataRange, intradayInterval)} " +
                $"where fk_id_spolki = {stockDef.ID} " +
                $"and ts >= {ts.ToTimestampQueryValue()} " +
                $") tsNearest, {_tblSelector.GetTableName(stockDef.Type, dataRange, intradayInterval)} tsWindow " +
                $"where tsWindow.fk_id_spolki = {stockDef.ID} and tsWindow.ts < tsNearest.mints " +
                "order by tsWindow.ts desc " +
                $"limit {ticksBefore} " +
                ") t";
            ProcessSelectQuery(qry, (reader) =>
            {
                reader.Read();
                if ((!reader.IsDBNull(0)) && (reader.GetFieldValue<int>(1) == ticksBefore))
                    res = reader.GetFieldValue<DateTime>(0);
            });
            return res;
        }
    }
}
