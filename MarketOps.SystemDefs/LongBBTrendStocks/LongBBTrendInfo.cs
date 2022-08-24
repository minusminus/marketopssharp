using MarketOps.SystemDefs.BBTrendRecognizer;

namespace MarketOps.SystemDefs.LongBBTrendStocks
{
    /// <summary>
    /// Calculated trend info
    /// </summary>
    internal class LongBBTrendInfo
    {
        public BBTrendType CurrentTrend = BBTrendType.Unknown;
        public int CurrentTrendStartIndex = -1;
    }
}
