using System.Collections.Generic;

namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Result of MonteCarlo simulation.
    /// </summary>
    public class MonteCarloResult
    {
        public readonly int Count;
        public readonly int Length;

        public readonly double[][] Data;
        public readonly double[] AverageData;

        public int Wins, Losses;
        public float WinsPcnt, LossesPcnt;

        public float BestCase, WorstCase, AverageCase;
        public float BestCaseYPcnt, WorstCaseYPcnt, AverageCaseYPcnt;

        public float MaxDrawDown;
        public int LongestDrawDown;

        public List<MonteCarloStreakData> WinningStreaks;
        public List<MonteCarloStreakData> LosingStreaks;

        public MonteCarloResult(int count, int length)
        {
            Count = count;
            Length = length;
            Data = new double[count][];
            for (int i = 0; i < count; i++)
                Data[i] = new double[length];
            AverageData = new double[length];
        }
    }
}
