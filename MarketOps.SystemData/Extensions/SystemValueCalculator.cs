using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Linq;

namespace MarketOps.SystemData.Extensions
{
    /// <summary>
    /// Calculates system equity value.
    /// </summary>
    public static class SystemValueCalculator
    {
        public static float Calc(SystemState system, DateTime ts, ISystemDataLoader dataLoader) => 
            CalcActive(system, ts, dataLoader) + system.Cash;

        private static float CalcActive(SystemState system, DateTime ts, ISystemDataLoader dataLoader) =>
            system.PositionsActive
                .Sum(p =>
                {
                    (StockPricesData prices, int ix) = dataLoader.GetPricesDataAndIndex(p.Stock.FullName, p.DataRange, p.IntradayInterval, ts);
                    return p.DirectionMultiplier() * prices.C[ix] * p.Volume;
                });
    }
}
