using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;
using System;
using System.Linq;

namespace MarketOps.System.Extensions
{
    /// <summary>
    /// Calculates system current equity value.
    /// </summary>
    internal class SystemValueCalculator
    {
        public float Calc(System system, DateTime ts, IDataLoader dataLoader)
        {
            return CalcActive(system, ts, dataLoader) + system.Cash;
        }

        private float CalcActive(System system, DateTime ts, IDataLoader dataLoader)
        {
            return system.PositionsActive.Sum(p =>
            {
                StockPricesData prices = dataLoader.Get(p.Stock.Name, p.DataRange, p.IntradayInterval, ts, ts);
                int ix = prices.FindByTS(ts);
                return prices.C[ix] * p.Volume;
            });
        }
    }
}
