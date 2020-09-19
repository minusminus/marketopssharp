using MarketOps.StockData.Types;

namespace MarketOps.StockData.Extensions
{
    /// <summary>
    /// MOParams container extension methods.
    /// </summary>
    public static class MOParamsExtensions
    {
        public static void Set(this MOParams moParams, string paramName, int paramValue)
        {
            moParams.Set(paramName, new MOParamInt() { Name = paramName, Value = paramValue });
        }

        public static void Set(this MOParams moParams, string paramName, float paramValue)
        {
            moParams.Set(paramName, new MOParamFloat() { Name = paramName, Value = paramValue });
        }

        public static void Set(this MOParams moParams, string paramName, string paramValue)
        {
            moParams.Set(paramName, new MOParamString() { Name = paramName, Value = paramValue });
        }
    }
}
