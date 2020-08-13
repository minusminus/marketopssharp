using System;
using System.Collections.Generic;
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
        private readonly List<float> _o = new List<float>();
        private readonly List<float> _h = new List<float>();
        private readonly List<float> _l = new List<float>();
        private readonly List<float> _c = new List<float>();
        private readonly List<Int64> _v = new List<Int64>();
        private readonly List<DateTime> _ts = new List<DateTime>();

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
            _o.Add(reader.GetFieldValue<Single>(iopen));
            _h.Add(reader.GetFieldValue<Single>(ihigh));
            _l.Add(reader.GetFieldValue<Single>(ilow));
            _c.Add(reader.GetFieldValue<Single>(iclose));
            _v.Add(reader.GetFieldValue<Int64>(ivolume));
            _ts.Add(reader.GetFieldValue<DateTime>(its));
        }

        public StockPricesData ToStockPricesData()
        {
            StockPricesData data = new StockPricesData(_o.Count);
            Array.Copy(_o.ToArray(), data.O, data.Length);
            Array.Copy(_h.ToArray(), data.H, data.Length);
            Array.Copy(_l.ToArray(), data.L, data.Length);
            Array.Copy(_c.ToArray(), data.C, data.Length);
            Array.Copy(_v.ToArray(), data.V, data.Length);
            Array.Copy(_ts.ToArray(), data.TS, data.Length);
            return data;
        }
    }
}
