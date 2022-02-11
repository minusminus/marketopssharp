using MarketOps.Stats.Stats;
using MarketOps.StockData.Types;
using System;

namespace MarketOps.SystemDefs.BBTrendRecognizer
{
    /// <summary>
    /// Trend recognition based on BB.
    /// 
    /// Trend defined as:
    /// H over last or current BBH -> trend changed to up
    /// L below last or current BBL -> trend changed do down
    /// The same for C over and below band.
    /// 
    /// Trend types:
    /// trend = up and C > sma -> up and raising
    /// trend = up and C <= sma -> up but possible change
    /// trend = down and C > sma -> down but possible change
    /// trend = down and C <= sma -> down and falling
    /// </summary>
    internal static class BBTrendRecognizer
    {
        public static BBTrendType RecognizeTrendOnLH(StockPricesData data, StatBB statBB, int leadingIndex, BBTrendType currentTrend, 
            out float trendStartLevel, ref int trendStartIndex) => 
            RecognizeTrend(statBB, leadingIndex, currentTrend, data.L[leadingIndex], data.H[leadingIndex], out trendStartLevel, ref trendStartIndex);
        public static BBTrendType RecognizeTrendOnC(StockPricesData data, StatBB statBB, int leadingIndex, BBTrendType currentTrend, 
            out float trendStartLevel, ref int trendStartIndex) =>
            RecognizeTrend(statBB, leadingIndex, currentTrend, data.C[leadingIndex], data.C[leadingIndex], out trendStartLevel, ref trendStartIndex);

        public static BBTrendExpectation GetExpectation(StockPricesData data, StatBB statBB, int leadingIndex, BBTrendType currentTrend)
        {
            if (currentTrend == BBTrendType.Up)
                return data.C[leadingIndex] > statBB.Data(StatBBData.SMA)[leadingIndex - statBB.BackBufferLength + 1]
                    ? BBTrendExpectation.UpAndRaising
                    : BBTrendExpectation.UpButPossibleChange;
            if (currentTrend == BBTrendType.Down)
                return data.C[leadingIndex] <= statBB.Data(StatBBData.SMA)[leadingIndex - statBB.BackBufferLength + 1]
                    ? BBTrendExpectation.DownAndFalling
                    : BBTrendExpectation.DownButPossibleChange;
            return BBTrendExpectation.Unknown;
        }

        private static BBTrendType RecognizeTrend(StatBB statBB, int leadingIndex, BBTrendType currentTrend, float lowValue, 
            float highValue, out float trendStartLevel, ref int trendStartIndex)
        {
            trendStartLevel = 0;
            if (((currentTrend == BBTrendType.Unknown) || (currentTrend == BBTrendType.Up))
                && (lowValue < Math.Max(statBB.Data(StatBBData.BBL)[leadingIndex - statBB.BackBufferLength + 1], statBB.Data(StatBBData.BBL)[leadingIndex - statBB.BackBufferLength])))
            {
                trendStartLevel = Math.Max(statBB.Data(StatBBData.BBL)[leadingIndex - statBB.BackBufferLength + 1], statBB.Data(StatBBData.BBL)[leadingIndex - statBB.BackBufferLength]);
                trendStartIndex = leadingIndex;
                return BBTrendType.Down;
            }
            if (((currentTrend == BBTrendType.Unknown) || (currentTrend == BBTrendType.Down))
                && (highValue > Math.Min(statBB.Data(StatBBData.BBH)[leadingIndex - statBB.BackBufferLength + 1], statBB.Data(StatBBData.BBH)[leadingIndex - statBB.BackBufferLength])))
            {
                trendStartLevel = Math.Min(statBB.Data(StatBBData.BBH)[leadingIndex - statBB.BackBufferLength + 1], statBB.Data(StatBBData.BBH)[leadingIndex - statBB.BackBufferLength]);
                trendStartIndex = leadingIndex;
                return BBTrendType.Up;
            }
            return currentTrend;
        }
    }
}
