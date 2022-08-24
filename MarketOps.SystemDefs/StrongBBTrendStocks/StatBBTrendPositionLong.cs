using MarketOps.StockData.Types;
using System;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Calculates strong trend (on monthly data).
    /// Trend starts tick after BB breakout.
    /// Trend ends tick after trailing stop breakout.
    /// </summary>
    internal class StatBBTrendPositionLong : StockStat
    {
        private readonly int _bbPeriod;
        private readonly float _bbSigmaWidth;
        private readonly int _trailingStopMinOfN;

        public StatBBTrendPositionLong(string chartArea, int bbPeriod, float bbSigmaWidth, int trailingStopMinOfN) : base(chartArea)
        {
            _bbPeriod = bbPeriod;
            _bbSigmaWidth = bbSigmaWidth;
            _trailingStopMinOfN = trailingStopMinOfN;
        }

        public override void Calculate(StockPricesData data)
        {
            _data[0] = StatBBTrendPositionLongCalculator.Calculate(data.C, data.L, _bbPeriod, _bbSigmaWidth, _trailingStopMinOfN);
        }

        protected override int GetBackBufferLength() =>
            Math.Max(_bbPeriod, _trailingStopMinOfN);

        protected override void InitializeData()
        {
            _name = "BBTrend Long";
            CreateDataStructures(1);
            //_dataColors[0] = Color.IndianRed;
            _dataNames[0] = "Indicator";
        }

        protected override void InitializeStatParams()
        {
        }
    }
}
