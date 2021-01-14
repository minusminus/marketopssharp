namespace MarketOps.Controls.PointChart
{
    /// <summary>
    /// PointChart data object
    /// </summary>
    public abstract class PointChartData
    {
        protected float _x;
        protected int _y;

        public float X => _x;
        public int Y => _y;
    }
}
