using System;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistic data for stock
    /// </summary>
    public abstract class StockStat
    {
        protected float[][] _data;
        protected StockStatParams _statParams = new StockStatParams();

        public int DataCount => _data.Length;
        public float[] Data(int index) => _data[index];
        public StockStatParams StatParams => _statParams;

        public StockStat()
        {
            InitializeData();
            InitializeStatParams();
        }

        protected abstract void InitializeData();
        protected abstract void InitializeStatParams();

        public abstract void Calculate(StockPricesData data);
    }
}
