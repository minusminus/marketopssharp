namespace MarketOps.Maths
{
    /// <summary>
    /// Calculates standard deviation for population.
    /// </summary>
    public static class StdDev
    {
        public static float Calculate(float[] data, float averageValue) =>
            Calculate(data, averageValue, 0, data.Length);

        public static float Calculate(float[] data, float averageValue, int startIndex, int length)
        {
            if (data.Length == 0) return 0;

            float sum = 0;
            for (int i = startIndex; i < startIndex + length; i++)
                sum += (data[i] - averageValue) * (data[i] - averageValue);
            return (float)System.Math.Sqrt(sum / (float)length);
        }
    }
}
