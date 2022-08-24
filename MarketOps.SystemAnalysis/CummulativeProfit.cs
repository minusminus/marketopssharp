using System;

namespace MarketOps.SystemAnalysis
{
    /// <summary>
    /// Calculates cummulative profit for specifed number of intervals
    /// </summary>
    internal static class CummulativeProfit
    {
        public static double Calculate(double initialValue, double finalValue, double numberOfIntervals) =>
            Math.Pow(finalValue / initialValue, 1 / numberOfIntervals) - 1f;
    }
}
