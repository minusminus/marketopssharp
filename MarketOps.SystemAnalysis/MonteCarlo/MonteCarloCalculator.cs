using System;

namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Calculates random equities of specified count and length for win probability and average win and loss percentage.
    /// </summary>
    public static class MonteCarloCalculator
    {
        private const float InitialValue = 1.0f;

        public static MonteCarloResult Calculate(int count, int length, float winProbability, float avgPcntWin, float avgPcntLoss)
        {
            MonteCarloResult result = new MonteCarloResult(count, length);
            CalculateData(result.data, winProbability, avgPcntWin, avgPcntLoss);
            CalculateSummary(result);
            return result;
        }

        private static void CalculateData(float[,] data, float winProbability, float avgPcntWin, float avgPcntLoss)
        {
            Random randomGenerator = new Random();
            for (int i = 0; i < data.GetLength(0); i++)
                CalculateSingleRow(data, i, winProbability, avgPcntWin, avgPcntLoss, randomGenerator);
        }

        private static void CalculateSummary(MonteCarloResult data)
        {
            int rowsCount = data.data.GetLength(0);
            int rowLength = data.data.GetLength(1);

            data.Wins = 0;
            for (int i = 0; i < rowsCount; i++)
                data.Wins += (data.data[i, rowLength - 1] > InitialValue) ? 1 : 0;
            data.Losses = rowsCount - data.Wins;
            data.WinsPcnt = (float)data.Wins / (float)rowsCount;
            data.LosesPcnt = 1.0f - data.WinsPcnt;
        }

        private static void CalculateSingleRow(float[,] data, int rowIndex, float winProbability, float avgPcntWin, float avgPcntLoss, Random randomGenerator)
        {
            data[rowIndex, 0] = InitialValue;
            for (int i = 1; i < data.GetLength(1); i++) 
                data[rowIndex, i] = data[rowIndex, i - 1] * ((randomGenerator.NextDouble() < winProbability) ? (1f + avgPcntWin) : (1f - avgPcntLoss));
        }
    }
}
