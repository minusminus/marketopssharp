using System;
using MarketOps.StockData.Types;
using Npgsql;

namespace MarketOps.DataProvider.Pg
{
    /// <summary>
    /// converts data from postgres db reader to StockDefinition object
    /// </summary>
    internal class PgDataToStockDefinitionConverter
    {
        public static void ToStockDefinition(NpgsqlDataReader reader, StockDefinition data)
        {
            data.ID = reader.GetFieldValue<int>(reader.GetOrdinal("id"));
            data.Type = (StockType)reader.GetFieldValue<int>(reader.GetOrdinal("typ"));
            data.Name = reader.GetFieldValue<string>(reader.GetOrdinal("nazwaspolki"));
            data.Enabled = reader.GetFieldValue<bool>(reader.GetOrdinal("aktywna"));
            data.StockName = reader.GetFieldValue<string>(reader.GetOrdinal("nazwaakcji"));
        }
    }
}
