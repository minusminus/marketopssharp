using MarketOps.SystemData.Types;
using MarketOps.SystemData.Extensions;

namespace MarketOps.SystemAnalysis.SystemSummary
{
    /// <summary>
    /// Calculates wins and losses of system state.
    /// </summary>
    internal static class WinsLossesCalculator
    {
        public static void Calculate(SystemStateSummary summary, SystemState systemState)
        {
            summary.ProcessedTicks = systemState.Equity.Count;
            summary.ClosedPositionsCount = systemState.PositionsClosed.Count;
            SumWinsLossesAndCalcAvgs(summary, systemState);
            CalcProbabilities(summary);
            CalcExpectedPositionValue(summary);
        }

        private static void SumWinsLossesAndCalcAvgs(SystemStateSummary summary, SystemState systemState)
        {
            systemState.PositionsClosed.ForEach(p =>
            {
                float value = p.Value();
                if (value > 0)
                {
                    summary.Wins++;
                    summary.SumWins += value;
                } else
                {
                    summary.Losses++;
                    summary.SumLosses -= value;
                }
            });
            if (summary.Wins > 0) summary.AvgWin = summary.SumWins / summary.Wins;
            if (summary.Losses > 0) summary.AvgLoss = summary.SumLosses / summary.Losses;
            if (summary.AvgLoss != 0) summary.AvgWinLossRatio = summary.AvgWin / summary.AvgLoss;
        }

        private static void CalcProbabilities(SystemStateSummary summary)
        {
            if (summary.ClosedPositionsCount == 0) return;
            summary.WinProbability = summary.Wins / (float)summary.ClosedPositionsCount;
            summary.LossProbability = summary.Losses / (float)summary.ClosedPositionsCount;
        }

        private static void CalcExpectedPositionValue(SystemStateSummary summary)
        {
            summary.ExpectedPositionValue = (summary.WinProbability * summary.AvgWin) - (summary.LossProbability * summary.AvgLoss);
        }
    }
}
