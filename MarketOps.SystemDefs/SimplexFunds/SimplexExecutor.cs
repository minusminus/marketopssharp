using System;
using System.Linq;
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

        public static float[] Execute(string[] fundsName, SimplexFundsData fundsData, 
            double portfolioValue, double acceptableRisk, double riskSigmaMultiplier, double maxSinglePositionSize)
        {
            SimplexExecutorData data = new SimplexExecutorData(fundsData);
            if (data.ActiveFunds.Length == 0) return new float[fundsData.Stocks.Length];

            double riskValue = portfolioValue * acceptableRisk;
            double maxAggressiveValue = portfolioValue * maxSinglePositionSize;

            SolverContext solverContext = new SolverContext();
            Model model = solverContext.CreateModel();

            model.AddDecisions(data.ActiveFunds.Select(i => new Decision(Domain.RealNonnegative, $"_{i}")).ToArray());

            model.AddConstraint("acceptable_risk",
                TermBuilder.SumProducts(model.Decisions, (i) => data.Prices[i] * (data.AvgChange[i] + data.AvgChangeSigma[i] * riskSigmaMultiplier)) <= riskValue);

            model.AddConstraint("max_aggressive_value", 
                TermBuilder.SumProducts(model.Decisions, data.Prices) <= maxAggressiveValue);

            model.AddConstraints("max_single_position_size",
                TermBuilder.BuildTerms(model.Decisions, (decision, i) => data.Prices[i] * decision <= maxAggressiveValue));

            model.AddConstraints("nonnegative", 
                TermBuilder.NonNegative(model.Decisions));

            model.AddGoal("max_avg_profit", GoalKind.Maximize, TermBuilder.SumProducts(model.Decisions, data.AvgProfit));

            return CalculateBalance(solverContext.Solve(new SimplexDirective()), fundsData, portfolioValue);
        }

        private static float[] CalculateBalance(Solution solution, SimplexFundsData fundsData, double portfolioValue)
        {
            float[] result = new float[fundsData.Stocks.Length];

            foreach (Decision decision in solution.Decisions)
            {
                int idx = Int32.Parse(decision.Name.Substring(1));
                result[idx] = (float)(fundsData.Prices[idx] * decision.ToDouble() / portfolioValue);
            }
            result[0] = 1f - result.Skip(1).Sum();

            return result;
        }
    }
}
