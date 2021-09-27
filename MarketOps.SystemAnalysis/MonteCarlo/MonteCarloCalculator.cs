using System;

namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Calculates random equities of specified count and length for win probability and average win and loss.
    /// </summary>
    public static class MonteCarloCalculator
    {
        public static MonteCarloResult Calculate(int count, int length, float winProbability, float avgWin, float avgLoss)
        {
            MonteCarloResult result = new MonteCarloResult(count, length);
            CalculateData(result.data, winProbability, avgWin, avgLoss);
            CalculateSummary(result);
            return result;
        }

        private static void CalculateData(float[,] data, float winProbability, float avgWin, float avgLoss)
        {
            Random randomGenerator = new Random();
            for (int i = 0; i < data.GetLength(0); i++)
                CalculateSingleRow(data, i, winProbability, avgWin, avgLoss, randomGenerator);
        }

        private static void CalculateSummary(MonteCarloResult data)
        {
            int rowsCount = data.data.GetLength(0);
            int rowLength = data.data.GetLength(1);

            data.Wins = 0;
            for (int i = 0; i < rowsCount; i++)
                data.Wins += (data.data[i, rowLength - 1] > 0) ? 1 : 0;
            data.Losses = rowsCount - data.Wins;
            data.WinsPcnt = (float)data.Wins / (float)rowsCount;
            data.LosesPcnt = 1.0f - data.WinsPcnt;
        }

        private static void CalculateSingleRow(float[,] data, int rowIndex, float winProbability, float avgWin, float avgLoss, Random randomGenerator)
        {
            data[rowIndex, 0] = 0;
            for (int i = 1; i < data.GetLength(1); i++) 
                data[rowIndex, i] = data[rowIndex, i - 1] + ((randomGenerator.NextDouble() < winProbability) ? avgWin : -avgLoss);
        }
    }
}
