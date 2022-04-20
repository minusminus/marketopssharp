namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Calculates random equities of specified count and length for win probability and average win and loss percentage.
    /// </summary>
    public static class MonteCarloCalculator
    {
        public static MonteCarloResult Calculate(int count, int length, float winProbability, float avgPcntWin, float avgPcntLoss, int transactionsPerYear)
        {
            MonteCarloResult result = new MonteCarloResult(count, length);
            MonteCarloDataGenerator.Generate(result.Data, winProbability, avgPcntWin, avgPcntLoss);
            MonteCarloAnalyzer.Analyze(result, transactionsPerYear);
            return result;
        }
    }
}
