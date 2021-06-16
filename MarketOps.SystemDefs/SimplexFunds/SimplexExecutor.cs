using System;
using System.Linq;
using MarketOps.SystemData.Extensions;
using Microsoft.SolverFoundation.Services;

namespace MarketOps.SystemDefs.SimplexFunds
{
    /// <summary>
    /// Executes simplex solver for simplex funds system.
    /// </summary>
    internal static class SimplexExecutor
    {
        /// <summary>
        /// Executor data with active funds only.
        /// </summary>
        private class SimplexExecutorData
        {
            public readonly int[] ActiveFunds;
            public readonly double[] Prices;
            public readonly double[] AvgChange;
            public readonly double[] AvgChangeSigma;
            public readonly double[] AvgProfit;

            public SimplexExecutorData(SimplexFundsData fundsData)
            {
                ActiveFunds = fundsData.Active.Select((b, i) => (b, i)).Where(x => x.b && (x.i > 0)).Select(x => x.i).ToArray();
                //ActiveFunds = fundsData.Active.Select((b, i) => (b, i)).Where(x => x.b).Select(x => x.i).ToArray();
                Prices = ActiveFunds.Select(i => fundsData.Prices[i]).ToArray();
                AvgChange = ActiveFunds.Select(i => fundsData.AvgChange[i]).ToArray();
                AvgChangeSigma = ActiveFunds.Select(i => fundsData.AvgChangeSigma[i]).ToArray();
                AvgProfit = ActiveFunds.Select(i => fundsData.AvgProfit[i]).ToArray();
            }
        }

        public static float[] Execute(SimplexFundsData fundsData, 
            double portfolioValue, double acceptableSingleDD, double riskSigmaMultiplier, double maxSinglePositionSize, double maxPortfolioRisk,
            int truncateBalanceToNthPlace)
        {
            SimplexExecutorData data = new SimplexExecutorData(fundsData);
            if (data.ActiveFunds.Length == 0) return new float[fundsData.Stocks.Length];

            double maxSingleDDValue = portfolioValue * acceptableSingleDD;
            double maxPositionValue = portfolioValue * maxSinglePositionSize;
            double maxPortfolioAggressiveValue = portfolioValue * maxPortfolioRisk;

            SolverContext solverContext = new SolverContext();
            Model model = solverContext.CreateModel();

            model.AddDecisions(data.ActiveFunds.Select(i => new Decision(Domain.RealNonnegative, $"_{i}")).ToArray());

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

            return CalculateBalance(solverContext.Solve(new SimplexDirective()), fundsData, portfolioValue, truncateBalanceToNthPlace);
        }

        private static float[] CalculateBalance(Solution solution, SimplexFundsData fundsData, double portfolioValue, int truncateBalanceToNthPlace)
        {
            float[] result = new float[fundsData.Stocks.Length];

            foreach (Decision decision in solution.Decisions)
            {
                int idx = Int32.Parse(decision.Name.Substring(1));
                result[idx] = ((float)(fundsData.Prices[idx] * decision.ToDouble() / portfolioValue)).TruncateToNthPlace(truncateBalanceToNthPlace);
            }
            result[0] = 1f - result.Skip(1).Sum();

            return result;
        }
    }
}
