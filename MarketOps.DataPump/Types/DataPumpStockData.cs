using System;

namespace MarketOps.DataPump.Types
{
    /// <summary>
    /// Data to save to database.
    /// </summary>
    internal class DataPumpStockData
    {
        public string O;
        public string H;
        public string L;
        public string C;
        public string RefCourse;
        public string V;
        public DateTime TS;
    }
}
