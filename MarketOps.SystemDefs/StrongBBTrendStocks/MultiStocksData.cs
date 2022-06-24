using MarketOps.Stats.Stats;
using MarketOps.StockData.Types;
using System.Linq;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Data for multi stocks long trends.
    /// </summary>
    internal class MultiStocksData
    {
        public readonly StockDefinition[] Stocks;
        public readonly StatBB[] StatsBB;
        public readonly StatBBTrendPositionLong[] StatsBBTrend;
        public readonly StatATR[] StatsATR;
        public readonly LongBBTrendInfo[] TrendInfo;

        public MultiStocksData(int length)
        {
            Stocks = new StockDefinition[length];
            StatsBB = new StatBB[length];
            StatsBBTrend = new StatBBTrendPositionLong[length];
            StatsATR = new StatATR[length];
            TrendInfo = Enumerable.Range(1, length).Select(i => new LongBBTrendInfo()).ToArray();
        }
    }
}
