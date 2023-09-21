using MarketOps.Controls.ChartsUtils;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.Extensions
{
    /// <summary>
    /// Extensions for StoskStats class.
    /// </summary>
    internal static class StockStatExtensions
    {
        public static bool IsPricesStat(this StockStat stat) =>
            stat.ChartArea == PlotConsts.PricesAreaName;
    }
}
