using MarketOps.SystemExecutor;
using MarketOps.SystemAnalysis.SystemSummary;
using System.Linq;

namespace MarketOps.SystemAnalysis
{
    /// <summary>
    /// Summary calculator of system state.
    /// </summary>
    public class SystemStateSummaryCalculator
    {
        public SystemStateSummary Calculate(SystemState systemState)
        {
            SystemStateSummary summary = new SystemStateSummary();

            GetSystemValues(summary, systemState);
            ProcessClosedPositions(summary, systemState);

            return summary;
        }

        private void GetSystemValues(SystemStateSummary summary, SystemState systemState)
        {
            summary.StartTS = systemState.Equity.FirstOrDefault().TS;
            summary.StopTS = systemState.Equity.LastOrDefault().TS;

            summary.InitialValue = systemState.InitialCash;
            summary.FinalValueOnLastTick = systemState.Equity.LastOrDefault().Value;
            summary.FinalValueOnClosedPositions = systemState.ClosedPositionsEquity.LastOrDefault().Value;
        }

        private void ProcessClosedPositions(SystemStateSummary summary, SystemState systemState) => WinsLossesCalculator.Calculate(summary, systemState);
    }
}
