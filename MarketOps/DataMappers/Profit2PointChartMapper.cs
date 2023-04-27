using MarketOps.Controls.PointChart;
using MarketOps.SystemData.Extensions;
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
        public static PointChartData Map(List<Position> input) =>
            new PointChartData(input.GetX(), input.GetY());

        private static double[] GetX(this List<Position> input) =>
            input.Select(x => (double)x.Value()).ToArray();

        private static double[] GetY(this List<Position> input) =>
            input.Select(x => (double)x.TicksActive).ToArray();
    }
}
