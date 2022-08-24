namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Calculates Simple Moving Average on provided array
    /// </summary>
    public class SMA : CumulativeDataCalculator
    {
        public float[] Calculate(float[] data, int period) =>
            CanCalculate(data, period)
                ? CalculateCumulative(data.Length, period, i => data[i])
                : new float[0];

        private bool CanCalculate(float[] data, int period) => 
            (data.Length >= period) && (period > 0);
    }
}
