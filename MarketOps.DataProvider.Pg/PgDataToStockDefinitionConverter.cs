using MarketOps.StockData.Types;
using Npgsql;

namespace MarketOps.DataProvider.Pg
{
    /// <summary>
    /// converts data from postgres db reader to StockDefinition object
    /// </summary>
    internal static class PgDataToStockDefinitionConverter
    {
        public static void ToStockDefinition(NpgsqlDataReader reader, StockDefinition data)
        {
            data.ID = reader.GetFieldValue<int>(reader.GetOrdinal("id"));
            data.Type = (StockType)reader.GetFieldValue<int>(reader.GetOrdinal("stock_type"));
            data.Enabled = reader.GetFieldValue<bool>(reader.GetOrdinal("enabled"));
            data.FullName = reader.GetFieldValue<string>(reader.GetOrdinal("stock_fullname"));
            data.Name = reader.GetFieldValue<string>(reader.GetOrdinal("stock_name"));
            data.FileNameDaily = reader.GetFieldValue<string>(reader.GetOrdinal("filename_daily"));
        }
    }
}
