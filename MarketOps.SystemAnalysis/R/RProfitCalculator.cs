using MarketOps.Maths;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemAnalysis.R
{
    /// <summary>
    /// Calculates R profit data of system state.
    /// </summary>
    internal static class RProfitCalculator
    {
        public static void Calculate(SystemStateSummary summary, SystemState systemState)
        {
            summary.AvgRProfit = CalculateRProfiAvg(systemState.PositionsClosed);
            summary.StdDevRProfit = CalculateRProfiStdDev(systemState.PositionsClosed, summary.AvgRProfit);
            summary.RProfitAvgToStdDev = Division.DivideByZeroToZero(summary.AvgRProfit, summary.StdDevRProfit);
            summary.RProfitDistribution = CalculateRProfitDistribution(systemState.PositionsClosed);
        }

        private static float CalculateRProfiAvg(List<Position> positionsClosed) =>
            (positionsClosed.Count > 0)
                ? positionsClosed.Average(p => p.RProfit)
                : 0;

        private static float CalculateRProfiStdDev(List<Position> positionsClosed, float avgRProfit) =>
            (positionsClosed.Count > 0)
                ? StdDev.Calculate(positionsClosed.Select(p => p.RProfit).ToArray(), avgRProfit)
                : 0;

        private static List<RProfitDistribution> CalculateRProfitDistribution(List<Position> positionsClosed) =>
            positionsClosed
                    .Select(p => p.RProfit.FlooredMultipleOfN(0.5f))
                    .GroupBy(x => x)
                    .Select(g => new RProfitDistribution() { R = g.Key, Count = g.Count() })
                    .ToList();
    }
}
