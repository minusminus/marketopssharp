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
            CalculateProbabilities(summary);
            summary.ExpectedPositionValue = ExpectedValue(summary.WinProbability, summary.AvgWin, summary.AvgLoss);
            summary.ExpectedUnitReturn = ExpectedValue(summary.WinProbability, summary.AvgWinLossRatio, 1);
        }

        private static void SumWinsLossesAndCalcAvgs(SystemStateSummary summary, SystemState systemState)
        {
            float sumPcntWins = 0;
            float sumPcntLosses = 0;

            systemState.PositionsClosed.ForEach(p =>
            {
                float value = p.Value();
                if (value > 0)
                {
                    summary.Wins++;
                    summary.SumWins += value;
                    sumPcntWins += value / p.OpenValue();
                } else
                {
                    summary.Losses++;
                    summary.SumLosses -= value;
                    sumPcntLosses += value / p.OpenValue();
                }
            });
            if (summary.Wins > 0)
            {
                summary.AvgWin = summary.SumWins / summary.Wins;
                summary.AvgPcntWin = sumPcntWins / summary.Wins;
            }
            if (summary.Losses > 0)
            {
                summary.AvgLoss = summary.SumLosses / summary.Losses;
                summary.AvgPcntLoss = sumPcntLosses / summary.Losses;
            }
            if (summary.AvgLoss != 0) summary.AvgWinLossRatio = summary.AvgWin / summary.AvgLoss;
        }

        private static void CalculateProbabilities(SystemStateSummary summary)
        {
            if (summary.ClosedPositionsCount == 0) return;
            summary.WinProbability = summary.Wins / (float)summary.ClosedPositionsCount;
            summary.LossProbability = summary.Losses / (float)summary.ClosedPositionsCount;
        }

        private static float ExpectedValue(float winProbability, float avgWin, float avgLoss) =>
            (winProbability * avgWin) - ((1f - winProbability) * avgLoss);
    }
}
