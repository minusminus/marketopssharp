using MarketOps.SystemAnalysis.DrawDowns;
using MarketOps.SystemExecutor;
using System.Linq;

namespace MarketOps.SystemAnalysis.SystemSummary
{
    /// <summary>
    /// Summary calculator of system state.
    /// </summary>
    public static class SystemStateSummaryCalculator
    {
        public static SystemStateSummary Calculate(SystemState systemState) => 
            CalculateDrawDowns(ProcessWinsLosses(GetSystemValues(CreateNewSummary(), systemState), systemState), systemState);

        private static SystemStateSummary CreateNewSummary() => new SystemStateSummary();

        private static SystemStateSummary GetSystemValues(SystemStateSummary summary, SystemState systemState)
        {
            summary.StartTS = systemState.Equity.FirstOrDefault().TS;
            summary.StopTS = systemState.Equity.LastOrDefault().TS;

            summary.InitialValue = systemState.InitialCash;
            summary.FinalValueOnLastTick = systemState.Equity.LastOrDefault().Value;
            summary.FinalValueOnClosedPositions = systemState.ClosedPositionsEquity.LastOrDefault().Value;
            return summary;
        }

        private static SystemStateSummary ProcessWinsLosses(SystemStateSummary summary, SystemState systemState)
        {
            WinsLossesCalculator.Calculate(summary, systemState);
            return summary;
        }

        private static SystemStateSummary CalculateDrawDowns(SystemStateSummary summary, SystemState systemState)
        {
            summary.DDTicks = DrawDownsCalculator.Calculate(systemState.Equity);
            summary.DDClosedPositions = DrawDownsCalculator.Calculate(systemState.ClosedPositionsEquity);
            return summary;
        }
    }
}
