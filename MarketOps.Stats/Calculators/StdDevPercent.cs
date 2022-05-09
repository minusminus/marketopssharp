using MarketOps.Maths;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// StdDev on percent changes calculator
    /// </summary>
    public static class StdDevPercent
    {
        public static float[] Calculate(float[] data, int range)
        {
            if (!CanCalculate(data, range)) return new float[0];
            return CalculateStdDevs(data, range);
        }

        private static bool CanCalculate(float[] data, int range) =>
            (data.Length >= range + 1) && (range > 1);

        private static float[] CalculateStdDevs(float[] data, int range)
        {
            var percentChanges = PercentChanges.Calculate(data);
            var averages = new SMA().Calculate(percentChanges, range);
            for (int i = 0; i < averages.Length; i++)
                averages[i] = 100f * StdDev.Calculate(percentChanges, averages[i], i, range);
            return averages;
        }
    }
}
