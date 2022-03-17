using MarketOps.Controls.ColumnChart;
using MarketOps.SystemData.Types;

namespace MarketOps.DataMappers
{
    internal class RProfitDistributionColumnChartData : ColumnChartData
    {
        public RProfitDistributionColumnChartData(RProfitDistribution profitDistribution)
        {
            _x = profitDistribution.R;
            _y = profitDistribution.Count;
            DistributionObject = profitDistribution;
        }

        public RProfitDistribution DistributionObject { get; }
    }
}
