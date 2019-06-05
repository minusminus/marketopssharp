using System;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Calculates Simple Moving Average on provided array
    /// </summary>
    public class SMA : CumulativeDataCalculator
    {
        private bool CanCalculate(float[] data, int period)
        {
            return (data.Length >= period) && (period > 0);
        }

        public float[] Calculate(float[] data, int period)
        {
            if (!CanCalculate(data, period)) return new float[0];
            return CalculateCumulative(data.Length, period, i => data[i]);
        }
    }
}
