using MarketOps.StockData.Types;

namespace MarketOps.StockData.Extensions
{
    /// <summary>
    /// StockStatParam extension methods
    /// </summary>
    public static class StockStatParamExtensions
    {
        public static T As<T>(this StockStatParam param)
        {
            return (T)param.Value;
        }
    }
}
