using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;
using System;
using System.Linq;

namespace MarketOps.System.Extensions
{
    /// <summary>
    /// Calculates system current equity value according to active and closed positions.
    /// </summary>
    internal class SystemValueCalculator
    {
        public float Calc(System system, DateTime ts, IDataLoader dataLoader)
        {
            return CalcActive(system, ts, dataLoader) + CalcClosed(system, ts) + system.Cash;
        }

        private float CalcClosed(System system, DateTime ts)
        {
            return system.PositionsClosed
                .Where(p => p.TSClose == ts)
                .Sum(p => p.ClosedValue());
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
