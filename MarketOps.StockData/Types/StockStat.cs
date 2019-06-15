using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistic data for stock
    /// </summary>
    public abstract class StockStat
    {
        protected float[][] _data;

        public int Count => _data.Length;
        public float[] Data(int index) => _data[index];
        public abstract void Calculate(StockPricesData data);
    }
}
