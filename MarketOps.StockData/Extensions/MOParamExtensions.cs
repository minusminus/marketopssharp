using MarketOps.StockData.Types;

namespace MarketOps.StockData.Extensions
{
    /// <summary>
    /// MOParam extension methods
    /// </summary>
    public static class MOParamExtensions
    {
        public static T As<T>(this MOParam param) => (T)param.Value;
    }
}
