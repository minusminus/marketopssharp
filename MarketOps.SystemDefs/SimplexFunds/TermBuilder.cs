using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SolverFoundation.Services;

namespace MarketOps.SystemDefs.SimplexFunds
{
    /// <summary>
    /// Solver term builder.
    /// </summary>
    internal static class TermBuilder
    {
        public static Term SumProducts(IEnumerable<Decision> decisions, double[] coefs)
        {
            return SumProducts(decisions, (i) => coefs[i]);
        }

        public static Term SumProducts(IEnumerable<Decision> decisions, Func<int, Term> coefCalculator)
        {
            int i = 0;
            Term result = 0;
            foreach (Decision decision in decisions)
            {
                result += coefCalculator(i) * decision;
                i++;
            }
            return result;
        }

        public static Term[] NonNegative(IEnumerable<Decision> decisions)
        {
            return BuildTerms(decisions, (decision, i) => decision >= 0);
        }

        public static Term[] BuildTerms(IEnumerable<Decision> decisions, Func<Decision, int, Term> singleTermConstructor)
        {
            return decisions
                .Select(singleTermConstructor)
                .ToArray();
        }
    }
}
