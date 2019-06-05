using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Calculates simple moving average on provided array
    /// </summary>
    public class SMA
    {
        private bool CanCalculate(float[] data, int period)
        {
            return (data.Length >= period) && (period > 0);
        }

        private float GetInitialSum(float[] data, int period)
        {
            return data.Take(period).Sum();
        }

        private float[] CalculateSMA(float[] data, int period)
        {
            float[] res = new float[data.Length - period + 1];

            float currentSum = GetInitialSum(data, period - 1);
            for (int i = 0; i < res.Length; i++)
            {
                currentSum += data[i + period - 1];
                res[i] = currentSum / (float)period;
                currentSum -= data[i];
            }

            return res;
        }

        public float[] Calculate(float[] data, int period)
        {
            if (!CanCalculate(data, period)) return new float[0];
            return CalculateSMA(data, period);
        }
    }
}
