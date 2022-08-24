using MarketOps.Controls.PointChart;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.DataMappers
{
    /// <summary>
    /// Maps position R profit to PointChartData
    /// </summary>
    internal static class RProfit2PointChartMapper
    {
        public static List<PointChartData> Map(List<Position> input) =>
            input.Select(CreateNewDataObject).ToList();

        private static PointChartData CreateNewDataObject(Position pos) =>
            new RProfitPointChartData(pos);
    }
}
