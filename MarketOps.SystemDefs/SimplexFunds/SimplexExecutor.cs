using System.Linq;
using Microsoft.SolverFoundation.Services;

namespace MarketOps.SystemDefs.SimplexFunds
{
    /// <summary>
    /// Executes simplex solver for simplex funds system.
    /// </summary>
    internal static class SimplexExecutor
    {
        public static Solution Execute(string[] fundsName, SimplexFundsData fundsData, 
            double portfolioValue, double acceptableRisk, double riskSigmaMultiplier, double maxSinglePositionSize)
        {
            double riskValue = portfolioValue * acceptableRisk;

            SolverContext solverContext = new SolverContext();
            Model model = solverContext.CreateModel();

            model.AddDecisions(fundsName.Select(fundName => new Decision(Domain.RealNonnegative, fundName)).ToArray());

            model.AddConstraint("acceptable_risk",
                TermBuilder.SumProducts(model.Decisions, (i) => fundsData.AvgChange[i] * fundsData.AvgChangeSigma[i] * riskSigmaMultiplier) <= riskValue);

            model.AddConstraint("current_value", 
                TermBuilder.SumProducts(model.Decisions, fundsData.Prices) <= portfolioValue);

            model.AddConstraints("max_single_position_size",
                TermBuilder.BuildTerms(model.Decisions, (decision, i) => fundsData.Prices[i] * decision <= portfolioValue * maxSinglePositionSize));

            model.AddConstraints("nonnegative", 
                TermBuilder.NonNegative(model.Decisions));

            model.AddGoal("max_avg_profit", GoalKind.Maximize, TermBuilder.SumProducts(model.Decisions, fundsData.AvgProfit));

            return solverContext.Solve(new SimplexDirective());
        }
    }
}
