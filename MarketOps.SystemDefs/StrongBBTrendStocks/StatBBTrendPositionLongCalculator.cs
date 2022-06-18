using MarketOps.Stats.Calculators;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Calculates strong trend (on monthly data).
    /// Trend starts tick after BB breakout.
    /// Trend ends tick after trailing stop breakout.
    /// </summary>
    public class StatBBTrendPositionLongCalculator
    {
        public float[] Calculate(float[] dataC, float[] dataL, int bbPeriod, float bbSigmaWidth, int trailingStopMinOfN)
        {
            var bbData = BB.Calculate(dataC, bbPeriod, bbSigmaWidth);
            var hlData = HLChannel.Calculate(dataL, dataL, trailingStopMinOfN);


            return new float[0];
        }
    }
}
