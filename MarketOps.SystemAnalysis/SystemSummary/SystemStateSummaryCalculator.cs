using MarketOps.SystemAnalysis.DrawDowns;
using MarketOps.SystemAnalysis.Equity;
using MarketOps.SystemAnalysis.R;
using MarketOps.SystemData.Types;
using System;
using System.Linq;

namespace MarketOps.SystemAnalysis.SystemSummary
{
    /// <summary>
    /// Summary calculator of system state.
    /// </summary>
    public static class SystemStateSummaryCalculator
    {
        public static SystemStateSummary Calculate(SystemState systemState)
        {
            SystemStateSummary summary = CreateNewSummary();
            GetSystemValues(summary, systemState);
            ProcessWinsLosses(summary, systemState);
            CalculateDrawDowns(summary, systemState);
            CalculateEquityDistribution(summary, systemState);
            CalculateRProfit(summary, systemState);
            return summary;
        }

        private static SystemStateSummary CreateNewSummary() => new SystemStateSummary();

        private static void GetSystemValues(SystemStateSummary summary, SystemState systemState)
        {
            summary.StartTS = systemState.Equity.FirstOrDefault().TS;
            summary.StopTS = systemState.Equity.LastOrDefault().TS;

            summary.InitialValue = systemState.InitialCash;
            summary.FinalValueOnLastTick = systemState.Equity.LastOrDefault().Value;
            summary.FinalValueOnClosedPositions = systemState.ClosedPositionsEquity.LastOrDefault().Value;

            double yearsOfSim = (summary.StopTS - summary.StartTS).TotalDays / 365.0;
            summary.CummYProfitPcntOnTicks = (float)CummulativeProfit.Calculate(summary.InitialValue, summary.FinalValueOnLastTick, yearsOfSim);
            summary.TransactionsPerYear = (int)Math.Floor(systemState.PositionsClosed.Count / yearsOfSim);
        }

        private static void ProcessWinsLosses(SystemStateSummary summary, SystemState systemState) => 
            WinsLossesCalculator.Calculate(summary, systemState);

        private static void CalculateDrawDowns(SystemStateSummary summary, SystemState systemState)
        {
            summary.DDTicks = DrawDownsCalculator.Calculate(systemState.Equity);
            summary.DDClosedPositions = DrawDownsCalculator.Calculate(systemState.ClosedPositionsEquity);
        }

        private static void CalculateEquityDistribution(SystemStateSummary summary, SystemState systemState)
        {
            summary.EquityDistribution = EquityDistributionCalculator.Calculate(systemState.Equity);
        }

        private static void CalculateRProfit(SystemStateSummary summary, SystemState systemState) =>
            RProfitCalculator.Calculate(summary, systemState);
    }
}
