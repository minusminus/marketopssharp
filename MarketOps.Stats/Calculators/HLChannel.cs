using System.Linq;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Calculates high-low channel on provided data.
    /// </summary>
    public static class HLChannel
    {
        public static HLChannelData Calculate(float[] highs, float[] lows, int period)
        {
            if (!CanCalculate(highs, lows, period))
                return new HLChannelData() { H = new float[0], L = new float[0] };

            return new HLChannelData
            {
                H = RingBufferCalculator<float>.Calculate(highs, period, (data) => data.Max()),
                L = RingBufferCalculator<float>.Calculate(lows, period, (data) => data.Min()),
            };
        }

        private static bool CanCalculate(float[] highs, float[] lows, int period) =>
            (highs.Length == lows.Length) 
            && (highs.Length >= period) 
            && (lows.Length >= period) 
            && (period > 0);
    }
}
