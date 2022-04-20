namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Result of MonteCarlo simulation.
    /// </summary>
    public class MonteCarloResult
    {
        public readonly float[,] Data;
        public readonly float[] AverageData;

        public int Wins, Losses;
        public float WinsPcnt, LossesPcnt;

        public float BestCase, WorstCase, AverageCase;
        public float BestCaseYPcnt, WorstCaseYPcnt, AverageCaseYPcnt;

        public MonteCarloResult(int count, int length)
        {
            Data = new float[count, length];
            AverageData = new float[length];
        }
    }
}
