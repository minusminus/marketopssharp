using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            data.ID = Convert.ToInt32(reader["id"]);
            data.Type = (StockType)Convert.ToInt32(reader["typ"]);
            data.Name = Convert.ToString(reader["nazwaspolki"]);
        }
    }
}
