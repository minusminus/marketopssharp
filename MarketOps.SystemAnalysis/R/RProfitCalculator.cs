using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MarketOps.SystemAnalysis.R
{
    /// <summary>
    /// Calculates R profit data of system state.
    /// </summary>
    internal static class RProfitCalculator
    {
        public static void Calculate(SystemStateSummary summary, SystemState systemState)
        {
            summary.AvgRProfit = CalculateRProfiAvg(systemState);
            summary.RProfitDistribution = CalculateRProfitDistribution(systemState);
        }

        private static float CalculateRProfiAvg(SystemState systemState) =>
            (systemState.PositionsClosed.Count > 0)
                ? systemState.PositionsClosed.Average(p => p.RProfit)
                : 0;

        private static List<RProfitDistribution> CalculateRProfitDistribution(SystemState systemState) => 
            systemState.PositionsClosed
                    .Select(p => (float)Math.Floor(p.RProfit))
                    .GroupBy(x => x)
                    .Select(g => new RProfitDistribution() { R = g.Key, Count = g.Count() })
                    .ToList();
    }
}
