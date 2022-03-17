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
        public static List<ColumnChartData> Map(List<RProfitDistribution> input) =>
            input.Select(CreateNewDataObject).ToList();

        private static ColumnChartData CreateNewDataObject(RProfitDistribution profitDistribution) =>
            new RProfitDistributionColumnChartData(profitDistribution);
    }
}
