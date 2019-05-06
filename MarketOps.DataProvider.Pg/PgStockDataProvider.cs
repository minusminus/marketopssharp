using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using Npgsql;

namespace MarketOps.DataProvider.Pg
{
    /// <summary>
    /// stock prices data provider for AT database on postgres
    /// </summary>
    public class PgStockDataProvider : IStockDataProvider
    {
        private DataTableSelector tblSelector = new DataTableSelector();

        private NpgsqlConnection OpenConnection()
        {
            NpgsqlConnection res = new NpgsqlConnection(PgDBConnectionString.ConnectionString);
            res.Open();
            return res;
        }

        private void ProcessQuery(string qry, Action<NpgsqlDataReader> rowsProcessor)
        {
            using (NpgsqlConnection conn = OpenConnection())
            using (NpgsqlCommand cmd = new NpgsqlCommand(qry, conn))
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                rowsProcessor(reader);
        }

        public StockDefinition GetStockDefinition(int stockID)
        {
            StockDefinition res = new StockDefinition();

            string qry = $"select * from at_spolki where id={stockID}";
            ProcessQuery(qry, (reader) =>
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
            ProcessQuery(qry, (reader) =>
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

            string qry = $"select open, high, low, close, volume, ts from {tblSelector.GetTableName(stockDef.Type, dataRange, intradayInterval)} where fk_id_spolki={stockDef.ID} and ts >= {tsFrom.ToTimestampQueryValue()} and ts <= {tsTo.ToTimestampQueryValue()} order by ts";
            ProcessQuery(qry, (reader) =>
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
