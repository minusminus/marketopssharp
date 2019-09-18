using System;
using Npgsql;

namespace MarketOps.DataProvider.Pg
{
    /// <summary>
    /// base class for data provider on postgres
    /// </summary>
    public class PgBaseProvider
    {
        private NpgsqlConnection OpenConnection()
        {
            NpgsqlConnection res = new NpgsqlConnection(PgDBConnectionString.ConnectionString);
            res.Open();
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
    }
}
