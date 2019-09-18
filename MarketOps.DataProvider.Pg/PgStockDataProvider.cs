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

            string qry = $"select * from at_spolki where id={stockID}";
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

            string qry = $"select * from at_spolki where nazwaspolki='{stockName.ToUpper()}'";
            ProcessSelectQuery(qry, (reader) =>
            {
                if (!reader.HasRows)
                    throw new Exception($"No data for stock name={stockName}");
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
            res.IntrradayInterval = intradayInterval;
            return res;
        }
    }
}
