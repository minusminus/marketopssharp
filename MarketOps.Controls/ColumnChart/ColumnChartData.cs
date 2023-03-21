namespace MarketOps.Controls.ColumnChart
{
    /// <summary>
    /// Column chart data object.
    /// </summary>
    public class ColumnChartData
    {
        public readonly double[] Positions;
        public readonly double[] Values;

        public ColumnChartData(double[] positions, double[] values)
        {
            Positions = positions;
            Values = values;
        }
    }
}
