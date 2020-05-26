using System;
using MarketOps.StockData.Types;

namespace MarketOps.StockData.Extensions
{
    /// <summary>
    /// StockStat class extensions
    /// </summary>
    public static class StockStatExtensions
    {
        public static string ChartSeriesName(this StockStat stat, int seriesIndex)
        {
            return $"{stat.UID}_{seriesIndex}";
        }

        public static StockStat SetParam(this StockStat stat, string paramName, StockStatParam paramValue)
        {
            stat.StatParams.Set(paramName, paramValue);
            return stat;
        }
    }
}
