using MarketOps.Controls.PointChart;
using MarketOps.SystemData.Types;

namespace MarketOps.DataMappers
{
    /// <summary>
    /// PointChartData for position R profit.
    /// </summary>
    internal class RProfitPointChartData : PointChartData
    {
        public RProfitPointChartData(Position position)
        {
            _x = position.RProfit;
            _y = position.TicksActive;
            PositionObject = position;
        }

        public Position PositionObject { get; }
    }
}
