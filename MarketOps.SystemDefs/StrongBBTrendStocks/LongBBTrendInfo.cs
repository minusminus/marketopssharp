using MarketOps.SystemDefs.BBTrendRecognizer;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
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
