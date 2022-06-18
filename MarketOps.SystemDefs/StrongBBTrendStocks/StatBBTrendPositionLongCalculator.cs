using MarketOps.Stats.Calculators;
using MarketOps.SystemDefs.BBTrendRecognizer;
using System;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Calculates strong trend (on monthly data).
    /// Trend starts tick after BB breakout.
    /// Trend ends tick after trailing stop breakout.
    /// </summary>
    internal static class StatBBTrendPositionLongCalculator
    {
        private const float NoTrend = 0;
        private const float InTrend = 1;

        public static float[] Calculate(float[] dataC, float[] dataL, int bbPeriod, float bbSigmaWidth, int trailingStopMinOfN)
        {
            var bbData = BB.Calculate(dataC, bbPeriod, bbSigmaWidth);
            var hlData = HLChannel.Calculate(dataL, dataL, trailingStopMinOfN);

            return CalculateData(dataC, dataL, bbData, hlData, bbPeriod, trailingStopMinOfN);
        }

        private static float[] CalculateData(float[] dataC, float[] dataL, BBData bbData, HLChannelData hlData, int bbPeriod, int trailingStopMinOfN)
        {
            LongBBTrendInfo trendInfo = new LongBBTrendInfo();
            int startIndex = Math.Max(bbPeriod, trailingStopMinOfN);
            float[] result = new float[dataC.Length - startIndex + 1];
            result[0] = NoTrend;
            for (int i = startIndex + 1; i < dataC.Length; i++)
            {
                trendInfo.CurrentTrend = BBTrendRecognizer.BBTrendRecognizer.RecognizeTrendOnC(dataC, bbData.BBL, bbData.BBH, bbPeriod, i - 1, trendInfo.CurrentTrend, out _, ref trendInfo.CurrentTrendStartIndex);

                if (result[i - 1] == NoTrend)
                {
                    BBTrendExpectation expectation = BBTrendRecognizer.BBTrendRecognizer.GetExpectation(dataC, bbData.SMA, bbPeriod, i - 1, trendInfo.CurrentTrend);
                    result[i] = (expectation == BBTrendExpectation.UpAndRaising) ? InTrend : NoTrend;
                }
                else
                    result[i] = (hlData.L[i - 1 - trailingStopMinOfN] <= dataL[i - 1]) ? NoTrend : InTrend;
            }
            return result;
        }
    }
}
