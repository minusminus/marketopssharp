using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Base class for cumulative data calculations (e.g. SMA, ATR).
    /// </summary>
    public class CumulativeDataCalculator
    {
        private float GetInitialSum(int period, Func<int, float> getValue)
        {
            float res = 0;
            for (int i = 0; i < period; i++)
                res += getValue(i);
            return res;
        }

        protected float[] CalculateCumulative(int dataLength, int period, Func<int, float>getValue )
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
    }
}
