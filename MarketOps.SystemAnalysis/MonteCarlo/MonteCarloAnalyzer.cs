using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Calculates summary and analyzes MonteCarlo simulation data.
    /// </summary>
    internal static class MonteCarloAnalyzer
    {
        public static void Analyze(MonteCarloResult data, int transactionsPerYear)
        {
            int rowsCount = data.Data.GetLength(0);
            int rowLength = data.Data.GetLength(1);

            CalculateAverageData(data, rowsCount, rowLength);
            CalculateWinsAndLosses(data, rowsCount, rowLength);
            SelectBestWorstAverageCase(data, rowsCount, rowLength);
            CalculateCummulativeYProfitBestWorstAverageCase(data, rowLength, transactionsPerYear);
        }

        private static void CalculateAverageData(MonteCarloResult data, int rowsCount, int rowLength)
        {
            for (int column = 0; column < rowLength; column++)
                data.AverageData[column] = 0;
            for (int row = 0; row < rowsCount; row++)
                for (int column = 0; column < rowLength; column++)
                    data.AverageData[column] += data.Data[row, column];
            for (int column = 0; column < rowLength; column++)
                data.AverageData[column] /= (float)rowsCount;
        }

        private static void CalculateWinsAndLosses(MonteCarloResult data, int rowsCount, int rowLength)
        {
            data.Wins = 0;
            for (int i = 0; i < rowsCount; i++)
                data.Wins += (data.Data[i, rowLength - 1] > MonteCarloConsts.InitialValue) ? 1 : 0;
            data.Losses = rowsCount - data.Wins;
            data.WinsPcnt = (float)data.Wins / (float)rowsCount;
            data.LossesPcnt = 1.0f - data.WinsPcnt;
        }

        private static void SelectBestWorstAverageCase(MonteCarloResult data, int rowsCount, int rowLength)
        {
            data.AverageCase = data.AverageData[rowLength - 1];
            data.BestCase = data.Data[0, rowLength - 1];
            data.WorstCase = data.Data[0, rowLength - 1];
            for (int i = 1; i < rowsCount; i++)
            {
                if (data.Data[i, rowLength - 1] > data.BestCase)
                    data.BestCase = data.Data[i, rowLength - 1];
                if (data.Data[i, rowLength - 1] < data.WorstCase)
                    data.WorstCase = data.Data[i, rowLength - 1];
            }
        }
        private static void CalculateCummulativeYProfitBestWorstAverageCase(MonteCarloResult data, int rowLength, int transactionsPerYear)
        {
            float CummYProfit(float finalValue) =>
                (float)CummulativeProfit.Calculate(MonteCarloConsts.InitialValue, finalValue, (double)rowLength / (double)transactionsPerYear);

            data.AverageCaseYPcnt = CummYProfit(data.AverageCase);
            data.BestCaseYPcnt = CummYProfit(data.BestCase);
            data.WorstCaseYPcnt = CummYProfit(data.WorstCase);
        }
    }
}
