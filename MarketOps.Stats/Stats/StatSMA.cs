using System;
using MarketOps.StockData.Types;
using MarketOps.Stats.Calculators;

namespace MarketOps.Stats.Stats
{
    public class StatSMA : StockStat
    {
        public StatSMA()
        {
            _data = new float[1][];
        }

        public override void Calculate(StockPricesData data)
        {
            SMA calc = new SMA();
            _data[0] = calc.Calculate(data.C, 20);
        }
    }
}
