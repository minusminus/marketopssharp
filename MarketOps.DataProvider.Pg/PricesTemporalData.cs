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

        public void AddAllRecords(NpgsqlDataReader reader)
        {
            int iopen = reader.GetOrdinal("open");
            int ihigh = reader.GetOrdinal("high");
            int ilow = reader.GetOrdinal("low");
            int iclose = reader.GetOrdinal("close");
            int ivolume = reader.GetOrdinal("volume");
            int its = reader.GetOrdinal("ts");

            while (reader.Read())
                AddRecord(reader, iopen, ihigh, ilow, iclose, ivolume, its);
        }

        public void AddRecord(NpgsqlDataReader reader, int iopen, int ihigh, int ilow, int iclose, int ivolume, int its)
        {
            o.Add(reader.GetFieldValue<Single>(iopen));
            h.Add(reader.GetFieldValue<Single>(ihigh));
            l.Add(reader.GetFieldValue<Single>(ilow));
            c.Add(reader.GetFieldValue<Single>(iclose));
            v.Add(reader.GetFieldValue<Int64>(ivolume));
            ts.Add(reader.GetFieldValue<DateTime>(its));
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
