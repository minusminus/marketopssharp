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
        //NpgsqlConnection conn = new NpgsqlConnection("");

        public StockPricesData GetPricesData(int stockID, DateTime tsFrom, DateTime tsTo)
        {
            throw new NotImplementedException();
        }

        public StockDefinition GetStockDefinition(int stockID)
        {
            throw new NotImplementedException();
        }
    }
}
