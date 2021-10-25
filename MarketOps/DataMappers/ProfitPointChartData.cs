using MarketOps.Controls.PointChart;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;

namespace MarketOps.DataMappers
{
    /// <summary>
    /// PointChartData for position profit.
    /// </summary>
    internal class ProfitPointChartData : PointChartData
    {
        public ProfitPointChartData(Position position)
        {
            _x = position.Value();
            _y = position.TicksActive;
            PositionObject = position;
        }

        public Position PositionObject { get; }
    }
}
