using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.SystemExecutor.Interfaces;
using System;
using System.Linq;

namespace MarketOps.SystemExecutor.Extensions
{
    /// <summary>
    /// Calculates system equity value.
    /// </summary>
    internal class SystemValueCalculator
    {
        public float Calc(SystemState system, DateTime ts, ISystemDataLoader dataLoader)
        {
            return CalcActive(system, ts, dataLoader) + system.Cash;
        }

        private float CalcActive(SystemState system, DateTime ts, ISystemDataLoader dataLoader)
        {
            return system.PositionsActive.Sum(p =>
            {
                StockPricesData prices = dataLoader.Get(p.Stock.Name, p.DataRange, p.IntradayInterval, ts, ts);
                int ix = prices.FindByTS(ts);
                return p.DirectionMultiplier() * prices.C[ix] * p.Volume;
            });
        }
    }
}
