using MarketOps.Stats.Stats;
using MarketOps.StockData.Types;

namespace MarketOps.SystemDefs.Tests.BBTrendRecognizer
{
    public class StatBBMock : StatBB
    {
        public float[] BBH = new float[2];
        public float[] SMA = new float[2];
        public float[] BBL = new float[2];

        public StatBBMock(string chartArea) : base(chartArea)
        {
        }

        public override void Calculate(StockPricesData data)
        {
            _data[StatBBData.BBH] = BBH;
            _data[StatBBData.SMA] = SMA;
            _data[StatBBData.BBL] = BBL;
        }
    }
}
