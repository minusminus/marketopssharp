using System;
using MarketOps.StockData.Types;
using Npgsql;

namespace MarketOps.DataProvider.Pg
{
    /// <summary>
    /// converts data from postgres db reader to DataPumpDownloadDefinition object
    /// </summary>
    internal class PgDataToDownloadDefinitionConverter
    {
        private static string GetStringOrEmpty(NpgsqlDataReader reader, string fieldName)
        {
            int fieldIndex;
            fieldIndex = reader.GetOrdinal(fieldName);
            return reader.IsDBNull(fieldIndex) ? "" : reader.GetFieldValue<string>(fieldIndex);
        }

        public static void ToDownloadDefinition(NpgsqlDataReader reader, DataPumpDownloadDefinition data)
        {
            data.Type = (StockType)reader.GetFieldValue<int>(reader.GetOrdinal("typ"));
            data.PathDaily = GetStringOrEmpty(reader, "path_dzienne");
            data.PathIntra = GetStringOrEmpty(reader, "path_intra");
        }
    }
}
