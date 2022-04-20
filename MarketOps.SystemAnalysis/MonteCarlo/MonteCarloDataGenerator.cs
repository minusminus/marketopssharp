using System;

namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Generates random equities of specified count and length for win probability and average win and loss percentage.
    /// </summary>
    internal static class MonteCarloDataGenerator
    {
        public static void Generate(float[,] data, float winProbability, float avgPcntWin, float avgPcntLoss)
        {
            Random randomGenerator = new Random();
            for (int i = 0; i < data.GetLength(0); i++)
                CalculateSingleRow(data, i, winProbability, avgPcntWin, avgPcntLoss, randomGenerator);
        }

        private static void CalculateSingleRow(float[,] data, int rowIndex, float winProbability, float avgPcntWin, float avgPcntLoss, Random randomGenerator)
        {
            data[rowIndex, 0] = MonteCarloConsts.InitialValue;
            for (int i = 1; i < data.GetLength(1); i++)
                data[rowIndex, i] = data[rowIndex, i - 1] * ((randomGenerator.NextDouble() < winProbability) ? (1f + avgPcntWin) : (1f - avgPcntLoss));
        }
    }
}
