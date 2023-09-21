namespace MarketOps.Controls.PointChart
{
    /// <summary>
    /// PointChart data object
    /// </summary>
    public class PointChartData
    {
        public readonly double[] X;
        public readonly double[] Y;

        public PointChartData(double[] x, double[] y)
        {
            X = x;
            Y = y;
        }
    }
}
