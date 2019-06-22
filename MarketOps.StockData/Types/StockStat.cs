using System;
using System.Drawing;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistic data for stock
    /// </summary>
    public abstract class StockStat
    {
        private string _uid;
        protected float[][] _data;
        protected Color[] _dataColors;
        protected StockStatParams _statParams = new StockStatParams();

        public string UID => _uid;
        public int DataCount => _data.Length;
        public float[] Data(int index) => _data[index];
        public Color DataColor(int index) => _dataColors[index];
        public StockStatParams StatParams => _statParams;

        public StockStat()
        {
            GenerateUID();
            InitializeData();
            InitializeStatParams();
        }

        private void GenerateUID()
        {
            _uid = Guid.NewGuid().ToString("N");
        }

        protected void CreateDataStructures(int seriesCount)
        {
            _data = new float[seriesCount][];
            _dataColors = new Color[seriesCount];
        }

        protected abstract void InitializeData();
        protected abstract void InitializeStatParams();

        public abstract void Calculate(StockPricesData data);
    }
}
