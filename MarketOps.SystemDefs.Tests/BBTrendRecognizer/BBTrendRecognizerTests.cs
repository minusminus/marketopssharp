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

        [TestCase(1, 2, 3, 1, 2, 3, 1.5f, 1.5f, 2.5f, 2.5f, BBTrendType.Unknown, BBTrendType.Unknown)]  //no break
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 1.5f, 2.5f, 2.5f, BBTrendType.Up, BBTrendType.Up)]
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 1.5f, 2.5f, 2.5f, BBTrendType.Down, BBTrendType.Down)]
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 1.5f, 2.5f, 3.25f, BBTrendType.Unknown, BBTrendType.Up)]    //bbh break
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 1.5f, 2.5f, 3.75f, BBTrendType.Unknown, BBTrendType.Up)]
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 1.5f, 2.5f, 3.25f, BBTrendType.Down, BBTrendType.Up)]
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 1.5f, 2.5f, 3.75f, BBTrendType.Down, BBTrendType.Up)]
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 0.75f, 2.5f, 2.5f, BBTrendType.Unknown, BBTrendType.Down)]  //bbl break
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 0.25f, 2.5f, 2.5f, BBTrendType.Unknown, BBTrendType.Down)]
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 0.75f, 2.5f, 2.5f, BBTrendType.Up, BBTrendType.Down)]
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 0.25f, 2.5f, 2.5f, BBTrendType.Up, BBTrendType.Down)]
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 0.75f, 2.5f, 3.25f, BBTrendType.Unknown, BBTrendType.Down)] //both bands break
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 0.75f, 2.5f, 3.25f, BBTrendType.Up, BBTrendType.Down)]
        [TestCase(1, 2, 3, 0.5f, 2, 3.5f, 1.5f, 0.75f, 2.5f, 3.25f, BBTrendType.Down, BBTrendType.Up)]
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

            MarketOps.SystemDefs.BBTrendRecognizer.BBTrendRecognizer.RecognizeTrend(_pricesData, _statBBMock, bbPeriod + analyzedPrices - 1, currTrend).ShouldBe(expectedTrend);
        }

        [TestCase(2, 1, BBTrendType.Unknown, BBTrendExpectation.Unknown)]
        [TestCase(2, 2, BBTrendType.Unknown, BBTrendExpectation.Unknown)]
        [TestCase(2, 3, BBTrendType.Unknown, BBTrendExpectation.Unknown)]
        [TestCase(2, 1, BBTrendType.Up, BBTrendExpectation.UpButPossibleChange)]
        [TestCase(2, 2, BBTrendType.Up, BBTrendExpectation.UpButPossibleChange)]
        [TestCase(2, 3, BBTrendType.Up, BBTrendExpectation.UpAndRaising)]
        [TestCase(2, 1, BBTrendType.Down, BBTrendExpectation.DownAndFalling)]
        [TestCase(2, 2, BBTrendType.Down, BBTrendExpectation.DownAndFalling)]
        [TestCase(2, 3, BBTrendType.Down, BBTrendExpectation.DownButPossibleChange)]
        public void GetExpectation__RecognizesCorrectly(float currSMA, float currC,
            BBTrendType currTrend, BBTrendExpectation expectedTrend)
        {
            _statBBMock.SMA[1] = currSMA;
            _pricesData.C[bbPeriod + 1] = currC;

            MarketOps.SystemDefs.BBTrendRecognizer.BBTrendRecognizer.GetExpectation(_pricesData, _statBBMock, bbPeriod + analyzedPrices - 1, currTrend).ShouldBe(expectedTrend);
        }
    }
}
