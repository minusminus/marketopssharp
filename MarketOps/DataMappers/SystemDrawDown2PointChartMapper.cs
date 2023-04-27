using MarketOps.Controls.PointChart;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.DataMappers
{
    /// <summary>
    /// Maps SystemDrawDown to PointChartData
    /// </summary>
    internal static class SystemDrawDown2PointChartMapper
    {
        public static PointChartData Map(List<SystemDrawDown> input) =>
            new PointChartData(input.GetX(), input.GetY());

        private static double[] GetX(this List<SystemDrawDown> input) =>
            input.Select(x => (double)(100.0 * x.DD())).ToArray();

        private static double[] GetY(this List<SystemDrawDown> input) =>
            input.Select(x => (double)x.Ticks).ToArray();
    }
}
