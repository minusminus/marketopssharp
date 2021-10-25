using MarketOps.StockData.Types;

namespace MarketOps.StockData.Extensions
{
    /// <summary>
    /// StockStat class extensions
    /// </summary>
    public static class StockStatExtensions
    {
        public static string ChartSeriesName(this StockStat stat, int seriesIndex) => $"{stat.UID}_{seriesIndex}";

        public static StockStat SetParam(this StockStat stat, string paramName, MOParam paramValue)
        {
            stat.StatParams.Set(paramName, paramValue);
            return stat;
        }
    }
}
