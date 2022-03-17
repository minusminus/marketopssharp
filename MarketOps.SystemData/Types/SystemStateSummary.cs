using System;
using System.Collections.Generic;

namespace MarketOps.SystemData.Types
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
        public float CummYProfitPcntOnTicks;

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
        public float AvgPcntWin;
        public float AvgPcntLoss;
        public float ExpectedPositionValue;
        public float ExpectedUnitReturn;
        public float AvgRProfit;

        public List<RProfitDistribution> RProfitDistribution;
        public List<SystemDrawDown> DDTicks;
        public List<SystemDrawDown> DDClosedPositions;

        public EquityDistribution EquityDistribution;
    }
}
