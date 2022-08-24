using MarketOps.Maths;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Avg to StdDev on percent changes calculator
    /// </summary>
    public static class AvgToStdDevPercent
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
                averages[i] = averages[i].DivideByZeroToZero(StdDev.Calculate(percentChanges, averages[i], i, range));
            return averages;
        }
    }
}
