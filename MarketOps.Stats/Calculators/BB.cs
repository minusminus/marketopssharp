using MarketOps.Maths;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Calculates Bollinger Bands on provided data
    /// </summary>
    public static class BB
    {
        public static BBData Calculate(float[] data, int period, float sigmaWidth) => 
            CanCalculate(data, period, sigmaWidth)
                ? CalculateBBData(data, period, sigmaWidth)
                : new BBData() { SMA = new float[0], BBL = new float[0], BBH = new float[0] };

        private static BBData CalculateBBData(float[] data, int period, float sigmaWidth)
        {
            BBData res = new BBData();
            res.SMA = (new SMA()).Calculate(data, period);
            res.BBL = new float[res.SMA.Length];
            res.BBH = new float[res.SMA.Length];

            for (int i = 0; i < res.SMA.Length; i++)
            {
                float stddev = StdDev.Calculate(data, res.SMA[i], i, period);
                res.BBL[i] = res.SMA[i] - sigmaWidth * stddev;
                res.BBH[i] = res.SMA[i] + sigmaWidth * stddev;
            }

            return res;
        }

        private static bool CanCalculate(float[] data, int period, float sigmaWidth) => 
            (data.Length >= period) 
            && (period > 0) 
            && (sigmaWidth > 0);
    }
}
