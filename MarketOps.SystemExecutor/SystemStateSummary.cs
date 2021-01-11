using System;

namespace MarketOps.SystemExecutor
{
    /// <summary>
    /// System execution summary.
    /// </summary>
    public class SystemStateSummary
    {
        public DateTime StartTS;
        public DateTime StopTS;
        public int ProcessedTicks;

        public float InitialValue;
        public float FinalValueOnLastTick;
        public float FinalValueOnClosedPositions;

        public int ClosedPositionsCount;
        public int Wins;
        public int Losses;
        public float WinProbability;
        public float LossProbability;
        public float SumWins;
        public float SumLosses;
        public float AvgWin;
        public float AvgLoss;
        public float AvgWinLossRatio;
        public float ExpectedPositionValue;
    }
}
