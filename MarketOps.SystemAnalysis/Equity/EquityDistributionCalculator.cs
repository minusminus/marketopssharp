using MarketOps.SystemData.Types;
using System;
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

            float[] differences = GetPercentDifferences(equity);
            float avg = differences.Average();
            return new EquityDistribution()
            {
                Average = 100f * avg,
                StdDev = 100f * CalculateStdDev(differences, avg)
            };
        }

        private static float[] GetPercentDifferences(List<SystemValue> equity)
        {
            float[] result = new float[equity.Count - 1];
            for (int i = 1; i < equity.Count; i++)
                result[i - 1] = equity[i - 1].Value != 0 ? (equity[i].Value - equity[i - 1].Value) / equity[i - 1].Value : 0;
            return result;
        }

        private static float CalculateStdDev(float[] values, float average)
        {
            float total = values.Aggregate(0f, (sum, val) => sum + (average - val) * (average - val));
            return (float)Math.Sqrt(total / (float)values.Length);
        }
    }
}
