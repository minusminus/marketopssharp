using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// stock prices data object
    /// always sorted in ascending order on TS
    /// </summary>
    public class StockPricesData
    {
        public StockDataRange Range;
        public int IntrradayInterval;

        public float[] O;
        public float[] H;
        public float[] L;
        public float[] C;
        public Int64[] V;
        public DateTime[] TS;

        public int Length => O.Length;

        private void CreateTables(int length)
        {
            O = new float[length];
            H = new float[length];
            L = new float[length];
            C = new float[length];
            V = new Int64[length];
            TS = new DateTime[length];
        }

        public StockPricesData(int length)
        {
            Range = StockDataRange.Undefined;
            IntrradayInterval = 0;
            CreateTables(length);
        }

        public StockPricesData(StockPricesData data, int length)
        {
            Range = data.Range;
            IntrradayInterval = data.IntrradayInterval;
            CreateTables(length);
        }
    }
}
