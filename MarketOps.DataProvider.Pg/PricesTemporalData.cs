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
    /// temporal data for prices data reading
    /// converts data from datareader to stockpricesdata
    /// </summary>
    internal class PricesTemporalData
    {
        private List<float> o = new List<float>();
        private List<float> h = new List<float>();
        private List<float> l = new List<float>();
        private List<float> c = new List<float>();
        private List<Int64> v = new List<Int64>();
        private List<DateTime> ts = new List<DateTime>();

        public void AddRecord(NpgsqlDataReader reader)
        {
            o.Add(Convert.ToSingle(reader["open"]));
            h.Add(Convert.ToSingle(reader["high"]));
            l.Add(Convert.ToSingle(reader["low"]));
            c.Add(Convert.ToSingle(reader["close"]));
            v.Add(Convert.ToInt64(reader["volume"]));
            ts.Add(Convert.ToDateTime(reader["ts"]));
        }

        public StockPricesData ToStockPricesData()
        {
            StockPricesData data = new StockPricesData(o.Count);
            Array.Copy(o.ToArray(), data.O, data.Length);
            Array.Copy(h.ToArray(), data.H, data.Length);
            Array.Copy(l.ToArray(), data.L, data.Length);
            Array.Copy(c.ToArray(), data.C, data.Length);
            Array.Copy(v.ToArray(), data.V, data.Length);
            Array.Copy(ts.ToArray(), data.TS, data.Length);
            return data;
        }
    }
}
