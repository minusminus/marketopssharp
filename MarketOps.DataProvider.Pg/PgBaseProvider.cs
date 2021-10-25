using System;
using System.Collections.Generic;
using MarketOps.StockData.Types;
using Npgsql;

namespace MarketOps.DataProvider.Pg
{
    /// <summary>
    /// base class for data provider on postgres
    /// </summary>
    public class PgBaseProvider
    {
        public List<StockDefinition> GetAllStockDefinitions()
        {
            List<StockDefinition> res = new List<StockDefinition>();

            string qry = $"select * from at_spolki2";
            ProcessSelectQuery(qry, (reader) =>
            {
                if (!reader.HasRows) return;
                while (reader.Read())
                {
                    StockDefinition def = new StockDefinition();
                    PgDataToStockDefinitionConverter.ToStockDefinition(reader, def);
                    res.Add(def);
                }
            });
            return res;
        }

        protected void ProcessSelectQuery(string qry, Action<NpgsqlDataReader> rowsProcessor)
        {
            using (NpgsqlConnection conn = OpenConnection())
            using (NpgsqlCommand cmd = new NpgsqlCommand(qry, conn))
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                rowsProcessor(reader);
        }

        protected void ExecuteQuery(string qry)
        {
            using (NpgsqlConnection conn = OpenConnection())
            using (NpgsqlCommand cmd = new NpgsqlCommand(qry, conn))
                cmd.ExecuteNonQuery();
        }

        private NpgsqlConnection OpenConnection()
        {
            NpgsqlConnection res = new NpgsqlConnection(PgDBConnectionString.ConnectionString);
            res.Open();
            return res;
        }
    }
}
