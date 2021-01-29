using MarketOps.Stats.Stats;
using MarketOps.StockData.Types;

namespace MarketOps.SystemDefs.BBTrend
{
    /// <summary>
    /// Trend recognition based on BB .
    /// 
    /// Trend defined as:
    /// H over last or current BBH -> trend changed to up
    /// L below last or current BBL -> trend changed do down
    /// 
    /// Trend types:
    /// trend = up and C > sma -> up and raising
    /// trend = up and C <= sma -> up but possible change
    /// trend = down and C > sma -> down but possible change
    /// trend = down and C <= sma -> down and falling
    /// </summary>
    internal static class BBTrendRecognizer
    {
        public static BBTrendType RecognizeTrend(StockPricesData data, StockStat statBB, int leadingIndex, BBTrendType currentTrend)
        {
            if (((currentTrend == BBTrendType.Unknown) || (currentTrend == BBTrendType.Up))
                && ((data.L[leadingIndex] < statBB.Data(StatBBData.BBL)[leadingIndex - statBB.BackBufferLength]) || (data.L[leadingIndex] < statBB.Data(StatBBData.BBL)[leadingIndex - statBB.BackBufferLength - 1])))
                return BBTrendType.Down;
            if (((currentTrend == BBTrendType.Unknown) || (currentTrend == BBTrendType.Down))
                && ((data.H[leadingIndex] > statBB.Data(StatBBData.BBH)[leadingIndex - statBB.BackBufferLength]) || (data.H[leadingIndex] > statBB.Data(StatBBData.BBH)[leadingIndex - statBB.BackBufferLength - 1])))
                return BBTrendType.Up;
            return currentTrend;
        }

        public static BBTrendExpectation GetExpectation(StockPricesData data, StockStat statBB, int leadingIndex, BBTrendType currentTrend)
        {
            if (currentTrend == BBTrendType.Up)
                return data.C[leadingIndex] > statBB.Data(StatBBData.SMA)[leadingIndex - statBB.BackBufferLength]
                    ? BBTrendExpectation.UpAndRaising
                    : BBTrendExpectation.UpButPossibleChange;
            if (currentTrend == BBTrendType.Down)
                return data.C[leadingIndex] <= statBB.Data(StatBBData.SMA)[leadingIndex - statBB.BackBufferLength]
                    ? BBTrendExpectation.DownAndFalling
                    : BBTrendExpectation.DownButPossibleChange;
            return BBTrendExpectation.Unknown;
        }
    }
}
