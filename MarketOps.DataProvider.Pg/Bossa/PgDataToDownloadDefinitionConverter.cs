﻿using MarketOps.StockData.Types;
using Npgsql;

namespace MarketOps.DataProvider.Pg.Bossa
{
    /// <summary>
    /// converts data from postgres db reader to DataPumpDownloadDefinition object
    /// </summary>
    internal static class PgDataToDownloadDefinitionConverter
    {
        private static string GetStringOrEmpty(NpgsqlDataReader reader, string fieldName)
        {
            int fieldIndex = reader.GetOrdinal(fieldName);
            return reader.IsDBNull(fieldIndex) ? "" : reader.GetFieldValue<string>(fieldIndex);
        }

        public static void ToDownloadDefinition(NpgsqlDataReader reader, DataPumpDownloadDefinition data)
        {
            data.Type = (StockType)reader.GetFieldValue<int>(reader.GetOrdinal("typ"));
            data.PathDaily = GetStringOrEmpty(reader, "path_dzienne");
            data.FileNameDaily = GetStringOrEmpty(reader, "file_dzienne");
            data.PathIntra = GetStringOrEmpty(reader, "path_intra");
        }
    }
}
