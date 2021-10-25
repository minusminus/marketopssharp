using MarketOps.Controls.PointChart;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.DataMappers
{
    /// <summary>
    /// Maps position profit to PointChartData
    /// </summary>
    internal static class Profit2PointChartMapper
    {
        public static List<PointChartData> Map(List<Position> input) =>
            input.Select(CreateNewDataObject).ToList();

        private static PointChartData CreateNewDataObject(Position pos) =>
            new ProfitPointChartData(pos);
    }
}
