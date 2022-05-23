using MarketOps.Stats.Stats;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemDefs.BBTrendRecognizer;
using System;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Signal generator for StrongBBTrend
    /// </summary>
    internal class SignalGenerator
    {
        private readonly StockDataRange _dataRange;
        private readonly IMMSignalVolume _signalVolumeCalculator;
        private readonly ITickAligner _tickAligner;

        public SignalGenerator(StockDataRange dataRange, IMMSignalVolume signalVolumeCalculator, ITickAligner tickAligner)
        {
            _dataRange = dataRange;
            _signalVolumeCalculator = signalVolumeCalculator;
            _tickAligner = tickAligner;
        }

        public Signal Generate(StockDefinition stock, DateTime ts, int currentIndex, SystemState systemState, LongBBTrendInfo trendInfo,
            StockPricesData data, StatBB statBB, StatATR statATR)
        {
            trendInfo.CurrentTrend = BBTrendRecognizer.BBTrendRecognizer.RecognizeTrendOnC(data, statBB, currentIndex, trendInfo.CurrentTrend, out _, ref trendInfo.CurrentTrendStartIndex);
            BBTrendExpectation expectation = BBTrendRecognizer.BBTrendRecognizer.GetExpectation(data, statBB, currentIndex, trendInfo.CurrentTrend);

            if (expectation != BBTrendExpectation.UpAndRaising) return null;

            if (TrendStartedNotLaterThanNTicksAgo(trendInfo, currentIndex, 1))
                return CreateSignal(stock, ts, PositionDir.Long, systemState, data.C[currentIndex], statATR.Data(StatATRData.ATR)[currentIndex - statATR.BackBufferLength]);
            else if (PriceAboveMaxOfPreviousH(data, currentIndex, 10, data.H[currentIndex]))
                return CreateSignal(stock, ts, PositionDir.Long, systemState, data.C[currentIndex], statATR.Data(StatATRData.ATR)[currentIndex - statATR.BackBufferLength]);

            return null;
        }

        private bool PriceAboveMaxOfPreviousH(StockPricesData data, int currentIndex, int length, float price)
        {
            for (int i = 1; i <= length; i++)
                if (data.H[currentIndex - i] > price)
                    return false;
            return true;
        }

        private bool TrendStartedNotLaterThanNTicksAgo(LongBBTrendInfo trendInfo, int currentIndex, int n) =>
            (currentIndex - trendInfo.CurrentTrendStartIndex) <= n;

        private Signal CreateSignal(StockDefinition stock, DateTime ts, PositionDir dir, SystemState systemState, float currentClosePrice, float currentAtr)
        {
            float volume = _signalVolumeCalculator.Calculate(systemState, stock.Type, currentClosePrice, currentClosePrice - currentAtr);
            return (volume > 0)
                ? new Signal()
                {
                    Stock = stock,
                    DataRange = _dataRange,
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
