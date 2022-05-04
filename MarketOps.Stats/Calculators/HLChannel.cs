using System.Linq;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Calculates high-low channel on provided data.
    /// </summary>
    public static class HLChannel
    {
        public static HLChannelData Calculate(float[] highs, float[] lows, int period) => 
            CanCalculate(highs, lows, period)
                ? CalculateHLChannelData(highs, lows, period)
                : new HLChannelData() { H = new float[0], L = new float[0] };

        private static HLChannelData CalculateHLChannelData(float[] highs, float[] lows, int period) =>
            new HLChannelData()
            {
                H = RingBufferCalculator<float>.Calculate(highs, period, (data) => data.Max()),
                L = RingBufferCalculator<float>.Calculate(lows, period, (data) => data.Min()),
            };

        private static bool CanCalculate(float[] highs, float[] lows, int period) =>
            (highs.Length == lows.Length) 
            && (highs.Length >= period) 
            && (lows.Length >= period) 
            && (period > 0);
    }
}
