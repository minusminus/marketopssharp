using MarketOps.Controls.PointChart;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;

namespace MarketOps.DataMappers
{
    /// <summary>
    /// PointChartData for position profit as a percentage.
    /// </summary>
    internal class ProfitPcntPointChartData : PointChartData
    {
        public ProfitPcntPointChartData(Position position)
        {
            _x = 100f * position.Value() / position.OpenValue();
            _y = position.TicksActive;
            PositionObject = position;
        }

        public Position PositionObject { get; }
    }
}
