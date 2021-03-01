using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Types;
using MarketOps.SystemDefs.BBTrendRecognizer;
using MarketOps.StockData.Extensions;

namespace MarketOps.SystemDefs.Tests.BBTrendRecognizer
{
    [TestFixture]
    public class BBTrendRecognizerTests
    {
        const int bbPeriod = 10;
        const int analyzedPrices = 2;
        private StatBBMock _statBBMock;
        private StockPricesData _pricesData;

        [SetUp]
        public void SetUp()
        {
            _statBBMock = new StatBBMock("");
            _statBBMock.SetParam(StatBBParams.Period, new MOParamInt() { Value = bbPeriod });
            _pricesData = new StockPricesData(bbPeriod + analyzedPrices);
            _statBBMock.Calculate(_pricesData);
        }

        public void RecognizeTrend__RecognizesCorrectly(float prevBBL, float prevSMA, float prevBBH, 
            float currBBL, float currSMA, float currBBH, 
            float prevL, float currL,
            float prevH, float currH,
            BBTrendType currTrend, BBTrendType expectedTrend)
        {
            _statBBMock.BBL[0] = prevBBL;
            _statBBMock.BBL[1] = currBBL;
            _statBBMock.SMA[0] = prevSMA;
            _statBBMock.SMA[1] = currSMA;
            _statBBMock.BBH[0] = prevBBH;
            _statBBMock.BBH[1] = currBBH;
            _pricesData.L[bbPeriod] = prevL;
            _pricesData.L[bbPeriod + 1] = currL;
            _pricesData.H[bbPeriod] = prevH;
            _pricesData.H[bbPeriod + 1] = currH;

            MarketOps.SystemDefs.BBTrendRecognizer.BBTrendRecognizer.RecognizeTrend(_pricesData, _statBBMock, bbPeriod + analyzedPrices, currTrend).ShouldBe(expectedTrend);
        }
    }
}
