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
            RecognizeTrendOnLH(data.L, data.H, statBB.Data(StatBBData.BBL), statBB.Data(StatBBData.BBH), statBB.BackBufferLength, leadingIndex, currentTrend, out trendStartLevel, ref trendStartIndex);

        public static BBTrendType RecognizeTrendOnLH(float[] dataL, float[] dataH, float[] bbL, float[] bbH, int bbBackBufferLength, int leadingIndex, BBTrendType currentTrend,
            out float trendStartLevel, ref int trendStartIndex) =>
            RecognizeTrend(bbL, bbH, bbBackBufferLength, leadingIndex, currentTrend, dataL[leadingIndex], dataH[leadingIndex], out trendStartLevel, ref trendStartIndex);

        public static BBTrendType RecognizeTrendOnC(StockPricesData data, StatBB statBB, int leadingIndex, BBTrendType currentTrend, 
            out float trendStartLevel, ref int trendStartIndex) =>
            RecognizeTrendOnC(data.C, statBB.Data(StatBBData.BBL), statBB.Data(StatBBData.BBH), statBB.BackBufferLength, leadingIndex, currentTrend, out trendStartLevel, ref trendStartIndex);

        public static BBTrendType RecognizeTrendOnC(float[] dataC, float[] bbL, float[] bbH, int bbBackBufferLength, int leadingIndex, BBTrendType currentTrend,
            out float trendStartLevel, ref int trendStartIndex) =>
            RecognizeTrend(bbL, bbH, bbBackBufferLength, leadingIndex, currentTrend, dataC[leadingIndex], dataC[leadingIndex], out trendStartLevel, ref trendStartIndex);

        public static BBTrendExpectation GetExpectation(StockPricesData data, StatBB statBB, int leadingIndex, BBTrendType currentTrend) =>
            GetExpectation(data.C, statBB.Data(StatBBData.SMA), statBB.BackBufferLength, leadingIndex, currentTrend);

        public static BBTrendExpectation GetExpectation(float[] dataC, float[] bbSMA, int bbBackBufferLength, int leadingIndex, BBTrendType currentTrend)
        {
            if (currentTrend == BBTrendType.Up)
                return dataC[leadingIndex] > bbSMA[leadingIndex - bbBackBufferLength + 1]
                    ? BBTrendExpectation.UpAndRaising
                    : BBTrendExpectation.UpButPossibleChange;
            if (currentTrend == BBTrendType.Down)
                return dataC[leadingIndex] <= bbSMA[leadingIndex - bbBackBufferLength + 1]
                    ? BBTrendExpectation.DownAndFalling
                    : BBTrendExpectation.DownButPossibleChange;
            return BBTrendExpectation.Unknown;
        }

        private static BBTrendType RecognizeTrend(float[] bbL, float[] bbH, int bbBackBufferLength, int leadingIndex, BBTrendType currentTrend, float lowValue, 
            float highValue, out float trendStartLevel, ref int trendStartIndex)
        {
            trendStartLevel = 0;
            if (((currentTrend == BBTrendType.Unknown) || (currentTrend == BBTrendType.Up))
                && (lowValue < Math.Max(bbL[leadingIndex - bbBackBufferLength + 1], bbL[leadingIndex - bbBackBufferLength])))
            {
                trendStartLevel = Math.Max(bbL[leadingIndex - bbBackBufferLength + 1], bbL[leadingIndex - bbBackBufferLength]);
                trendStartIndex = leadingIndex;
                return BBTrendType.Down;
            }
            if (((currentTrend == BBTrendType.Unknown) || (currentTrend == BBTrendType.Down))
                && (highValue > Math.Min(bbH[leadingIndex - bbBackBufferLength + 1], bbH[leadingIndex - bbBackBufferLength])))
            {
                trendStartLevel = Math.Min(bbH[leadingIndex - bbBackBufferLength + 1], bbH[leadingIndex - bbBackBufferLength]);
                trendStartIndex = leadingIndex;
                return BBTrendType.Up;
            }
            return currentTrend;
        }
    }
}
