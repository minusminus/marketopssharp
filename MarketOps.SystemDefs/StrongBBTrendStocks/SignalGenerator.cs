using MarketOps.Stats.Stats;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemDefs.BBTrendRecognizer;
using System;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Signal generator for StrongBBTrend.
    /// Checks trend on long data range, generates signal for short data range.
    /// </summary>
    internal class SignalGenerator
    {
        private readonly StockDataRange _signalDataRange;
        private readonly IMMSignalVolume _signalVolumeCalculator;
        private readonly ITickAligner _tickAligner;

        public SignalGenerator(StockDataRange signalDataRange, IMMSignalVolume signalVolumeCalculator, ITickAligner tickAligner)
        {
            _signalDataRange = signalDataRange;
            _signalVolumeCalculator = signalVolumeCalculator;
            _tickAligner = tickAligner;
        }

        public Signal Generate(StockDefinition stock, DateTime ts, int longIndex, int shortIndex, SystemState systemState, LongBBTrendInfo trendInfo,
            StockPricesData data, StatBB statBBLong, StatATR statATRShort)
        {
            trendInfo.CurrentTrend = BBTrendRecognizer.BBTrendRecognizer.RecognizeTrendOnC(data, statBBLong, longIndex, trendInfo.CurrentTrend, out _, ref trendInfo.CurrentTrendStartIndex);
            BBTrendExpectation expectation = BBTrendRecognizer.BBTrendRecognizer.GetExpectation(data, statBBLong, longIndex, trendInfo.CurrentTrend);

            if (expectation != BBTrendExpectation.UpAndRaising) return null;

            if (TrendStartedNotLaterThanNTicksAgo(trendInfo, longIndex, 1))
                return CreateSignal(stock, ts, PositionDir.Long, systemState, data.C[shortIndex], statATRShort.Data(StatATRData.ATR)[shortIndex - statATRShort.BackBufferLength]);
            else if (PriceAboveMaxOfPreviousH(data, longIndex, 10, data.H[longIndex]))
                return CreateSignal(stock, ts, PositionDir.Long, systemState, data.C[shortIndex], statATRShort.Data(StatATRData.ATR)[shortIndex - statATRShort.BackBufferLength]);

            return null;
        }

        private bool PriceAboveMaxOfPreviousH(StockPricesData data, int longIndex, int length, float price)
        {
            for (int i = 1; i <= length; i++)
                if (data.H[longIndex - i] > price)
                    return false;
            return true;
        }

        private bool TrendStartedNotLaterThanNTicksAgo(LongBBTrendInfo trendInfo, int longIndex, int n) =>
            (longIndex - trendInfo.CurrentTrendStartIndex) <= n;

        private Signal CreateSignal(StockDefinition stock, DateTime ts, PositionDir dir, SystemState systemState, float currentClosePrice, float currentAtr)
        {
            float volume = _signalVolumeCalculator.Calculate(systemState, stock.Type, currentClosePrice, currentClosePrice - currentAtr);
            return (volume > 0)
                ? new Signal()
                {
                    Stock = stock,
                    DataRange = _signalDataRange,
                    IntradayInterval = 0,
                    Type = SignalType.EnterOnOpen,
                    Direction = dir,
                    InitialStopMode = SignalInitialStopMode.OnPrice,
                    InitialStopValue = AlignDown(stock.Type, ts, currentClosePrice - currentAtr),
                    ReversePosition = false,
                    Volume = volume
                }
                : null;
        }

        private float AlignDown(StockType stockType, DateTime ts, float value) =>
            (_tickAligner != null) ? _tickAligner.AlignDown(stockType, ts, value) : value;
    }
}
