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
        public static void ToDownloadDefinition(NpgsqlDataReader reader, DataPumpDownloadDefinition data)
        {
            data.Type = (StockType)reader.GetFieldValue<int>(reader.GetOrdinal("typ"));
            data.PathDaily = reader.GetFieldValue<string>(reader.GetOrdinal("path_dzienne"));
            data.PathIntra = reader.GetFieldValue<string>(reader.GetOrdinal("path_intra"));
        }
    }
}
