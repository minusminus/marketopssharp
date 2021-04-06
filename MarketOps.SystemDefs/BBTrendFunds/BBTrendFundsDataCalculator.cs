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
        public static void Initialize(BBTrendFundsData data, string[] fundsNames, int bbPeriod, float bbSigmaWidth, IStockDataProvider dataProvider)
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
                data.ExpectationChanged[i] = false;
            }
        }

        public static void CalculateTrendsAndExpectations(BBTrendFundsData data, DateTime ts, StockDataRange dataRange, ISystemDataLoader dataLoader)
        {
            for (int i = 0; i < data.Stocks.Length; i++)
            {
                StockPricesData spData = dataLoader.Get(data.Stocks[i].Name, dataRange, 0, ts, ts);
                int dataIndex = spData.FindByTS(ts);
                if (dataIndex < data.StatsBB[i].BackBufferLength) continue;
                data.CurrentTrends[i] = BBTrendRecognizer.BBTrendRecognizer.RecognizeTrend(spData, data.StatsBB[i], dataIndex, data.CurrentTrends[i]);
                BBTrendExpectation lastExpectation = data.CurrentExpectations[i];
                data.CurrentExpectations[i] = BBTrendRecognizer.BBTrendRecognizer.GetExpectation(spData, data.StatsBB[i], dataIndex, data.CurrentTrends[i]);
                data.ExpectationChanged[i] = (lastExpectation != data.CurrentExpectations[i]);
            }
        }
    }
}
