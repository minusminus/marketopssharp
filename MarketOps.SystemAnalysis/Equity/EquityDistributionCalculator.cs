using MarketOps.SystemData.Types;
using MarketOps.Maths;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemAnalysis.Equity
{
    /// <summary>
    /// Equity distribution calculator for system.
    /// </summary>
    public static class EquityDistributionCalculator
    {
        public static EquityDistribution Calculate(List<SystemValue> equity)
        {
            if (equity.Count <= 1)
                return new EquityDistribution();

            float[] differences = PercentChanges.Calculate<SystemValue>(equity, (v) => v.Value);
            float avg = differences.Average();
            return new EquityDistribution()
            {
                Average = 100f * avg,
                StdDev = 100f * StdDev.Calculate(differences, avg)
            };
        }
    }
}
