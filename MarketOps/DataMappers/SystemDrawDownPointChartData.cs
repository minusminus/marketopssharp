using MarketOps.Controls.PointChart;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;

namespace MarketOps.DataMappers
{
    /// <summary>
    /// PointChartData for SystemDrawDown
    /// </summary>
    internal class SystemDrawDownPointChartData : PointChartData
    {
        public SystemDrawDownPointChartData(SystemDrawDown dd)
        {
            _x = 100f * dd.DD();
            _y = dd.Ticks;
            DDObject = dd;
        }

        public SystemDrawDown DDObject { get; }
    }
}
