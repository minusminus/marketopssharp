using System;
using System.Drawing;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Statistic data for stock
    /// </summary>
    public abstract class StockStat
    {
        private readonly string _chartArea;
        private string _uid;
        protected string _name;
        protected float[][] _data;
        protected Color[] _dataColors;
        protected string[] _dataNames;
        protected readonly MOParams _statParams = new MOParams();

        public string UID => _uid;
        public string Name => _name;
        public int DataCount => _data.Length;
        public float[] Data(int index) => _data[index];
        public Color[] DataColor => _dataColors;
        public string DataName(int index) => _dataNames[index];
        public string ChartArea => _chartArea;
        public MOParams StatParams => _statParams;
        public int BackBufferLength => GetBackBufferLength();

        public StockStat(string chartArea)
        {
            _chartArea = chartArea;
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
            _dataNames = new string[seriesCount];
        }

        protected abstract void InitializeData();
        protected abstract void InitializeStatParams();
        protected abstract int GetBackBufferLength();

        public abstract void Calculate(StockPricesData data);
    }
}
