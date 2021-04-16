namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Calculates percent of change in specified range.
    /// </summary>
    public class RangeChangePcnt
    {
        private bool CanCalculate(float[] data, int range)
        {
            return (data.Length >= range) && (range > 0);
        }

        public float[] Calculate(float[] data, int range)
        {
            if (!CanCalculate(data, range)) return new float[0];
            return CalculateChanges(data, range);
        }

        private float[] CalculateChanges(float[] data, int range)
        {
            float[] result = new float[data.Length - range + 1];
            for (int i = 0; i < result.Length; i++)
                result[i] = 100f * (data[i + range - 1] - data[i]) / data[i];
            return result;
        }
    }
}
