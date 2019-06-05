using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Calculates Bollinger Bands on provided data
    /// </summary>
    public class BB
    {
        private bool CanCalculate(float[] data, int period, float sigmaWidht)
        {
            return (data.Length >= period) && (period > 0) && (sigmaWidht > 0);
        }

        private float CalcStdDev(float[] data, float avg, int ixStart, int width)
        {
            float val = 0;
            for (int i = ixStart; i < ixStart + width; i++)
                val += (data[i] - avg)*(data[i] - avg);
            return (float) Math.Sqrt(val/(float) width);
        }

        public BBData Calculate(float[] data, int period, float sigmaWidht)
        {
            if (!CanCalculate(data, period, sigmaWidht))
                return new BBData() {SMA = new float[0], BBL = new float[0], BBH = new float[0]};

            BBData res = new BBData();
            res.SMA = (new SMA()).Calculate(data, period);
            res.BBL = new float[res.SMA.Length];
            res.BBH = new float[res.SMA.Length];

            for (int i = 0; i < res.SMA.Length; i++)
            {
                float stddev = CalcStdDev(data, res.SMA[i], i, period);
                res.BBL[i] = res.SMA[i] - sigmaWidht*stddev;
                res.BBH[i] = res.SMA[i] + sigmaWidht*stddev;
            }

            return res;
        }
    }
}
