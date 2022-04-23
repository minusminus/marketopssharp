namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Result of MonteCarlo simulation.
    /// </summary>
    public class MonteCarloResult
    {
        public readonly int Count;
        public readonly int Length;

        public readonly float[][] Data;
        public readonly float[] AverageData;

        public int Wins, Losses;
        public float WinsPcnt, LossesPcnt;

        public float BestCase, WorstCase, AverageCase;
        public float BestCaseYPcnt, WorstCaseYPcnt, AverageCaseYPcnt;

        public float MaxDrawDown;
        public int LongestDrawDown;

        public MonteCarloResult(int count, int length)
        {
            Count = count;
            Length = length;
            Data = new float[count][];
            for (int i = 0; i < count; i++)
                Data[i] = new float[length];
            AverageData = new float[length];
        }
    }
}
