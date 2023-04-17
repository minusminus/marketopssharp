using System;
using System.Linq;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Base class for cumulative data calculations (e.g. SMA, ATR).
    /// </summary>
    public class CumulativeDataCalculator
    {
        protected float[] CalculateCumulative(int dataLength, int period, Func<int, float>getValue)
        {
            float[] res = new float[dataLength - period + 1];

            float currentSum = GetInitialSum(period - 1, getValue);
            for (int i = 0; i < res.Length; i++)
            {
                currentSum += getValue(i + period - 1);
                res[i] = currentSum / (float)period;
                currentSum -= getValue(i);
            }

            return res;
        }

        private float GetInitialSum(int period, Func<int, float> getValue) =>
            Enumerable.Range(0, period)
            .Select(i => getValue(i))
            .Sum();
    }
}
