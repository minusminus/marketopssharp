namespace MarketOps.Controls.ColumnChart
{
    /// <summary>
    /// Column chart data object.
    /// </summary>
    public abstract class ColumnChartData
    {
        protected float _x;
        protected int _y;

        public float X => _x;
        public int Y => _y;
    }
}
