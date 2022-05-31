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

        public Signal Generate(StockDefinition stock, DateTime ts, int indexLong, int indexShort, SystemState systemState, LongBBTrendInfo trendInfo,
            StockPricesData dataLong, StockPricesData dataShort, StatBB statBBLong, StatATR statATRShort)
        {
            trendInfo.CurrentTrend = BBTrendRecognizer.BBTrendRecognizer.RecognizeTrendOnC(dataLong, statBBLong, indexLong, trendInfo.CurrentTrend, out _, ref trendInfo.CurrentTrendStartIndex);
            BBTrendExpectation expectation = BBTrendRecognizer.BBTrendRecognizer.GetExpectation(dataLong, statBBLong, indexLong, trendInfo.CurrentTrend);

            if (expectation != BBTrendExpectation.UpAndRaising) return null;

            if (TrendStartedNotLaterThanNTicksAgo(trendInfo, indexLong, 1)
                || PriceAboveMaxOfPreviousH(dataLong, indexLong, 10, dataLong.H[indexLong]))
                return CreateSignal(stock, ts, PositionDir.Long, systemState, dataShort.C[indexShort], statATRShort.Data(StatATRData.ATR)[indexShort - statATRShort.BackBufferLength]);

            return null;
        }

        private bool PriceAboveMaxOfPreviousH(StockPricesData data, int index, int length, float price)
        {
            for (int i = 1; i <= length; i++)
                if (data.H[index - i] > price)
                    return false;
            return true;
        }

        private bool TrendStartedNotLaterThanNTicksAgo(LongBBTrendInfo trendInfo, int index, int n) =>
            (index - trendInfo.CurrentTrendStartIndex) <= n;

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
