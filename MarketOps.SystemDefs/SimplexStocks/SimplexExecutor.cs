using System;
using System.Linq;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemDefs.SimplexTools;
using Microsoft.SolverFoundation.Services;

namespace MarketOps.SystemDefs.SimplexStocks
{
    /// <summary>
    /// Executes simplex solver for simplex stocks system.
    /// </summary>
    internal static class SimplexExecutor
    {
        /// <summary>
        /// Executor data with active funds only.
        /// </summary>
        private class SimplexExecutorData
        {
            public readonly int[] ActiveStocks;
            public readonly double[] Prices;
            public readonly double[] AvgChange;
            public readonly double[] AvgChangeSigma;
            public readonly double[] AvgProfit;

            public SimplexExecutorData(SimplexStocksData stocksData)
            {
                ActiveStocks = stocksData.Active.Select((b, i) => (b, i)).Where(x => x.b && (x.i > 0)).Select(x => x.i).ToArray();
                Prices = ActiveStocks.Select(i => stocksData.Prices[i]).ToArray();
                AvgChange = ActiveStocks.Select(i => stocksData.AvgChange[i]).ToArray();
                AvgChangeSigma = ActiveStocks.Select(i => stocksData.AvgChangeSigma[i]).ToArray();
                AvgProfit = ActiveStocks.Select(i => stocksData.AvgProfit[i]).ToArray();
            }
        }

        public static float[] Execute(SimplexStocksData stocksData,
            double portfolioValue, double acceptableSingleDD, double riskSigmaMultiplier, double maxSinglePositionSize, double maxPortfolioRisk,
            int truncateBalanceToNthPlace)
        {
            SimplexExecutorData data = new SimplexExecutorData(stocksData);
            if (data.ActiveStocks.Length == 0) return new float[stocksData.Stocks.Length];

            double maxSingleDDValue = portfolioValue * acceptableSingleDD;
            double maxPositionValue = portfolioValue * maxSinglePositionSize;
            double maxPortfolioAggressiveValue = portfolioValue * maxPortfolioRisk;

            SolverContext solverContext = new SolverContext();
            Model model = solverContext.CreateModel();

            model.AddDecisions(data.ActiveStocks.Select(i => new Decision(Domain.RealNonnegative, $"_{i}")).ToArray());

            model.AddConstraint("acceptable_single_DD",
                TermBuilder.SumProducts(model.Decisions, (i) => data.Prices[i] * (data.AvgChange[i] + data.AvgChangeSigma[i] * riskSigmaMultiplier)) <= maxSingleDDValue);

            model.AddConstraint("max_portfolio_aggressive_value",
                TermBuilder.SumProducts(model.Decisions, data.Prices) <= maxPortfolioAggressiveValue);

            model.AddConstraints("max_single_position_size",
                TermBuilder.BuildTerms(model.Decisions, (decision, i) => data.Prices[i] * decision <= maxPositionValue));

            model.AddConstraints("all_positions_positive",
                TermBuilder.BuildTerms(model.Decisions, (decision, i) => data.AvgProfit[i] * decision >= 0));

            model.AddConstraints("nonnegative",
                TermBuilder.NonNegative(model.Decisions));

            model.AddGoal("max_avg_profit", GoalKind.Maximize, TermBuilder.SumProducts(model.Decisions, data.AvgProfit));

            return CalculateBalance(solverContext.Solve(new SimplexDirective()), stocksData, portfolioValue, truncateBalanceToNthPlace);
        }

        private static float[] CalculateBalance(Solution solution, SimplexStocksData stocksData, double portfolioValue, int truncateBalanceToNthPlace)
        {
            float[] result = new float[stocksData.Stocks.Length];

            foreach (Decision decision in solution.Decisions)
            {
                int idx = Int32.Parse(decision.Name.Substring(1));
                result[idx] = ((float)(stocksData.Prices[idx] * decision.ToDouble() / portfolioValue)).TruncateToNthPlace(truncateBalanceToNthPlace);
            }
            result[0] = 1f - result.Skip(1).Sum();

            return result;
        }
    }
}
