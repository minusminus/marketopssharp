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

        public StockPricesData(int length)
        {
            Range = StockDataRange.Undefined;
            IntrradayInterval = 0;
            O = new float[length];
            H = new float[length];
            L = new float[length];
            C = new float[length];
            V = new Int64[length];
            TS = new DateTime[length];
        }
    }
}
