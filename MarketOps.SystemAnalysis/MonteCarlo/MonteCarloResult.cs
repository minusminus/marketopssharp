namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Result of MonteCarlo simulation.
    /// </summary>
    public class MonteCarloResult
    {
        public readonly float[,] data;

        public int Wins, Losses;
        public float WinsPcnt, LosesPcnt;

        public MonteCarloResult(int count, int length)
        {
            data = new float[count, length];
        }
    }
}
