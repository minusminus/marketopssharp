using MarketOps.Controls.ColumnChart;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.DataMappers
{
    /// <summary>
    /// Maps R distribution to ColumnChartData
    /// </summary>
    internal static class RProfitDistribution2ColumnChartMapper
    {
        public static ColumnChartData Map(List<RProfitDistribution> input) => 
            new ColumnChartData(input.GetPositions(), input.GetValues());

        private static double[] GetPositions(this List<RProfitDistribution> input) =>
            input.Select(x => (double)x.R).ToArray();

        private static double[] GetValues(this List<RProfitDistribution> input) =>
            input.Select(x => (double)x.Count).ToArray();
    }
}
