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
        public static PointChartData Map(List<Position> input) =>
            new PointChartData(input.GetX(), input.GetY());

        private static double[] GetX(this List<Position> input) =>
            input.Select(x => (double)x.RProfit).ToArray();

        private static double[] GetY(this List<Position> input) =>
            input.Select(x => (double)x.TicksActive).ToArray();
    }
}
