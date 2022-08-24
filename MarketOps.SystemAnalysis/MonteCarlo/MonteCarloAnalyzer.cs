namespace MarketOps.SystemAnalysis.MonteCarlo
{
    /// <summary>
    /// Calculates summary and analyzes MonteCarlo simulation data.
    /// </summary>
    internal static class MonteCarloAnalyzer
    {
        public static void Analyze(MonteCarloResult data, int transactionsPerYear)
        {
            CalculateAverageData(data);
            CalculateWinsAndLosses(data);
            SelectBestWorstAverageCase(data);
            CalculateCummulativeYProfitBestWorstAverageCase(data, transactionsPerYear);
            AnalyzeDrawDowns(data);
            CalculateStreaks(data);
        }

        private static void CalculateAverageData(MonteCarloResult data)
        {
            for (int column = 0; column < data.Length; column++)
                data.AverageData[column] = 0;
            for (int row = 0; row < data.Count; row++)
                for (int column = 0; column < data.Length; column++)
                    data.AverageData[column] += data.Data[row][column];
            for (int column = 0; column < data.Length; column++)
                data.AverageData[column] /= (float)data.Count;
        }

        private static void CalculateWinsAndLosses(MonteCarloResult data)
        {
            data.Wins = 0;
            for (int i = 0; i < data.Count; i++)
                data.Wins += (data.Data[i][data.Length - 1] > MonteCarloConsts.InitialValue) ? 1 : 0;
            data.Losses = data.Count - data.Wins;
            data.WinsPcnt = (float)data.Wins / (float)data.Count;
            data.LossesPcnt = 1.0f - data.WinsPcnt;
        }

        private static void SelectBestWorstAverageCase(MonteCarloResult data)
        {
            data.AverageCase = data.AverageData[data.Length - 1];
            data.BestCase = data.Data[0][data.Length - 1];
            data.WorstCase = data.Data[0][data.Length - 1];
            for (int i = 1; i < data.Count; i++)
            {
                if (data.Data[i][data.Length - 1] > data.BestCase)
                    data.BestCase = data.Data[i][data.Length - 1];
                if (data.Data[i][data.Length - 1] < data.WorstCase)
                    data.WorstCase = data.Data[i][data.Length - 1];
            }
        }
        private static void CalculateCummulativeYProfitBestWorstAverageCase(MonteCarloResult data, int transactionsPerYear)
        {
            float CummYProfit(float finalValue) =>
                (float)CummulativeProfit.Calculate(MonteCarloConsts.InitialValue, finalValue, (double)data.Length / (double)transactionsPerYear);

            data.AverageCaseYPcnt = CummYProfit(data.AverageCase);
            data.BestCaseYPcnt = CummYProfit(data.BestCase);
            data.WorstCaseYPcnt = CummYProfit(data.WorstCase);
        }

        private static void AnalyzeDrawDowns(MonteCarloResult data) =>
            new MonteCarloMaxDrawDownCalculator().Calculate(data, out data.MaxDrawDown, out data.LongestDrawDown);

        private static void CalculateStreaks(MonteCarloResult data) =>
            MonteCarloStreaksCalculator.Calculate(data.Data, out data.WinningStreaks, out data.LosingStreaks);
    }
}
