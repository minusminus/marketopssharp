using System;

namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Generates random equities of specified count and length for win probability and average win and loss percentage.
    /// </summary>
    internal static class MonteCarloDataGenerator
    {
        public static void Generate(double[][] data, float winProbability, float avgPcntWin, float avgPcntLoss)
        {
            Random randomGenerator = new Random();
            for (int i = 0; i < data.Length; i++)
                CalculateSingleRow(data[i], winProbability, avgPcntWin, avgPcntLoss, randomGenerator);
        }

        private static void CalculateSingleRow(double[] data, float winProbability, float avgPcntWin, float avgPcntLoss, Random randomGenerator)
        {
            data[0] = MonteCarloConsts.InitialValue;
            for (int i = 1; i < data.Length; i++)
                data[i] = data[i - 1] * ((randomGenerator.NextDouble() < winProbability) ? (1f + avgPcntWin) : (1f - avgPcntLoss));
        }
    }
}
