using System;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Calculates Average True Range of provided prices data
    /// 
    /// Current value is max of: H[i]-L[i], H[i]-C[i-1], C[i-1]-L[i], hence GetValue works on i+1 and i
    /// </summary>
    public class ATR : CumulativeDataCalculator
    {
        public float[] Calculate(float[] dataH, float[] dataL, float[] dataC, int period) => 
            CanCalculate(dataH, dataL, dataC, period)
                ? CalculateCumulative(dataH.Length - 1, period, i => GetValue(dataH, dataL, dataC, i))
                : (new float[0]);

        private bool CanCalculate(float[] dataH, float[] dataL, float[] dataC, int period) =>
            (dataH.Length == dataL.Length)
            && (dataH.Length == dataC.Length)
            && ((dataH.Length - 1) >= period)
            && (period > 0);

        private float GetValue(float[] dataH, float[] dataL, float[] dataC, int valueIndex)
        {
            float r1 = dataH[valueIndex + 1] - dataL[valueIndex + 1];
            float r2 = dataH[valueIndex + 1] - dataC[valueIndex];
            float r3 = dataC[valueIndex] - dataL[valueIndex + 1];

            return Math.Max(Math.Max(r1, r2), r3);
        }
    }
}
