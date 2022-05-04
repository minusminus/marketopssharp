namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Heikin-Ashi Open/Close calculator.
    /// Calculates open and close data based on heikin-ashi technique.
    /// </summary>
    public static class HeikinAshiOC
    {
        public static HeikinAshiOCData Calculate(float[] opens, float[] highs, float[] lows, float[] closes) => 
            CanCalculate(opens, highs, lows, closes)
                ? CalculateOCData(opens, highs, lows, closes)
                : new HeikinAshiOCData() { O = new float[0], C = new float[0] };

        private static bool CanCalculate(float[] opens, float[] highs, float[] lows, float[] closes) =>
            (opens.Length > 1)
            && (opens.Length == highs.Length)
            && (opens.Length == lows.Length)
            && (opens.Length == closes.Length);

        public static HeikinAshiOCData CalculateOCData(float[] opens, float[] highs, float[] lows, float[] closes)
        {
            HeikinAshiOCData res = new HeikinAshiOCData()
            {
                O = new float[opens.Length - 1],
                C = new float[opens.Length - 1]
            };

            for (int i = 1; i < opens.Length; i++)
            {
                res.O[i - 1] = (opens[i - 1] + closes[i - 1]) / 2f;
                res.C[i - 1] = (opens[i] + highs[i] + lows[i] + closes[i]) / 4f;
            }

            return res;
        }
    }
}
