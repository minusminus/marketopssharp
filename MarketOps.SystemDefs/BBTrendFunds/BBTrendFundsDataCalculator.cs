using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemDefs.BBTrendRecognizer;
using System;

namespace MarketOps.SystemDefs.BBTrendFunds
{
    /// <summary>
    /// Trend funds data calculations.
    /// </summary>
    internal static class BBTrendFundsDataCalculator
    {
        public static void Initialize(BBTrendFundsData data, string[] fundsNames, int bbPeriod, float bbSigmaWidth, int hlPeriod, IStockDataProvider dataProvider)
        {
            for (int i = 0; i < fundsNames.Length; i++)
            {
                data.Stocks[i] = dataProvider.GetStockDefinition(fundsNames[i]);
                StockStat statBB = new StatBB("")
                    .SetParam(StatBBParams.Period, new MOParamInt() { Value = bbPeriod })
                    .SetParam(StatBBParams.SigmaWidth, new MOParamFloat() { Value = bbSigmaWidth });
                data.StatsBB[i] = (StatBB)statBB;
                data.CurrentTrends[i] = BBTrendType.Unknown;
                data.CurrentExpectations[i] = BBTrendExpectation.Unknown;
                StockStat statHL = new StatHLChannel("")
                    .SetParam(StatHLChannelParams.Period, new MOParamInt() { Value = hlPeriod });
                data.StatsHLChannel[i] = (StatHLChannel)statHL;
                data.ExpectationChanged[i] = false;
            }
        }

        public static void CalculateTrendsAndExpectations(BBTrendFundsData data, DateTime ts, StockDataRange dataRange, ISystemDataLoader dataLoader)
        {
            for (int i = 0; i < data.Stocks.Length; i++)
            {
                if (!dataLoader.GetWithIndex(data.Stocks[i].FullName, dataRange, ts, data.StatsBB[i].BackBufferLength, out StockPricesData spData, out int dataIndex)) continue;
                int trendStartIndex = 0;
                BBTrendType lastTrend = data.CurrentTrends[i];
                data.CurrentTrends[i] = BBTrendRecognizer.BBTrendRecognizer.RecognizeTrendOnLH(spData, data.StatsBB[i], dataIndex, data.CurrentTrends[i], out float trendStartLevel, ref trendStartIndex);
                if (lastTrend != data.CurrentTrends[i])
                {
                    data.UpTrendStartValues[i] = trendStartLevel;// spData.H[dataIndex];
                    data.TrendLength[i] = 0;
                }
                data.TrendLength[i]++;
                BBTrendExpectation lastExpectation = data.CurrentExpectations[i];
                data.CurrentExpectations[i] = BBTrendRecognizer.BBTrendRecognizer.GetExpectation(spData, data.StatsBB[i], dataIndex, data.CurrentTrends[i]);
                data.ExpectationChanged[i] = (lastExpectation != data.CurrentExpectations[i]);
            }
        }

        public static void CalculateMaxValuesAndStops(BBTrendFundsData data, float stopWidth, DateTime ts, StockDataRange dataRange, ISystemDataLoader dataLoader)
        {
            for (int i = 0; i < data.Stocks.Length; i++)
            {
                if (data.CurrentTrends[i] != BBTrendType.Up)
                {
                    data.UpTrendMaxValues[i] = float.MinValue;
                    data.UpTrendStopValues[i] = float.MinValue;
                    continue;
                }

                if (!dataLoader.GetWithIndex(data.Stocks[i].FullName, dataRange, ts, out StockPricesData spData, out int dataIndex)) continue;
                //data.UpTrendMaxValues[i] = Math.Max(spData.H[dataIndex], data.UpTrendMaxValues[i]);
                data.UpTrendMaxValues[i] = Math.Max(spData.C[dataIndex], data.UpTrendMaxValues[i]);
                data.UpTrendStopValues[i] = data.UpTrendMaxValues[i] * (1f - stopWidth);
            }
        }

        public static void CheckStops(BBTrendFundsData data, DateTime ts, StockDataRange dataRange, ISystemDataLoader dataLoader)
        {
            for (int i = 0; i < data.Stocks.Length; i++)
            {
                if (data.CurrentTrends[i] != BBTrendType.Up)
                {
                    data.StoppedOut[i] = false;
                    continue;
                }

                if (!dataLoader.GetWithIndex(data.Stocks[i].FullName, dataRange, ts, out StockPricesData spData, out int dataIndex)) continue;
                if (data.StoppedOut[i])
                    data.StoppedOut[i] = (spData.C[dataIndex] <= data.StoppedOutValues[i]) || !PriceAbovePrevMaxH(data, i, dataIndex, spData.C[dataIndex]);
                else
                    data.StoppedOut[i] = (spData.C[dataIndex] <= data.UpTrendStopValues[i]);
            }
        }

        private static bool PriceAbovePrevMaxH(BBTrendFundsData data, int stockIndex, int dataIndex, float price)
        {
            StatHLChannel statHL = data.StatsHLChannel[stockIndex];
            if (dataIndex < statHL.BackBufferLength + 1) return false;
            return (statHL.Data(StatHLChannelData.H)[dataIndex - statHL.BackBufferLength] < price);
        }
    }
}
