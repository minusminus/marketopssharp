using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using System;

namespace MarketOps.SystemDefs.SimplexFunds
{
    /// <summary>
    /// Simplex funds data calculator.
    /// </summary>
    internal static class SimplexFundsDataCalculator
    {
        public static void Initialize(SimplexFundsData data, string[] fundsNames, IStockDataProvider dataProvider)
        {
            for (int i = 0; i < fundsNames.Length; i++)
            {
                data.Stocks[i] = dataProvider.GetStockDefinition(fundsNames[i]);
            }
        }

        public static void CalculateAvgProfit(SimplexFundsData data, int profitRange, DateTime ts, StockDataRange dataRange, ISystemDataLoader dataLoader)
        {
            for (int i = 0; i < data.Stocks.Length; i++)
            {
                if (!dataLoader.GetWithIndex(data.Stocks[i].FullName, dataRange, ts, profitRange + 1, out StockPricesData spData, out int dataIndex)) continue;
                data.AvgProfit[i] = AvgChangeInPercent(spData.C, dataIndex, profitRange);
            }
        }

        public static void CalculateAvgChange(SimplexFundsData data, int changeRange, DateTime ts, StockDataRange dataRange, ISystemDataLoader dataLoader)
        {
            for (int i = 0; i < data.Stocks.Length; i++)
            {
                if (!dataLoader.GetWithIndex(data.Stocks[i].FullName, dataRange, ts, changeRange + 1, out StockPricesData spData, out int dataIndex)) continue;
                data.AvgChange[i] = AvgChangeInPercent(spData.C, dataIndex, changeRange);
                data.AvgChangeSigma[i] = StdDev(spData.C, dataIndex, changeRange, data.AvgChange[i]);
            }
        }

        private static float AvgChangeInPercent(float[] tbl, int startIndex, int range)
        {
            float sum = 0;
            for (int i = 0; i < range; i++)
                sum += ChangeInPercent(tbl[startIndex - i], tbl[startIndex - i - 1]);
            return sum / (float)range;
        }

        private static float StdDev(float[] tbl, int startIndex, int range, float avgValue)
        {
            float sum = 0;
            for (int i = 0; i < range; i++)
            {
                float value = ChangeInPercent(tbl[startIndex - i], tbl[startIndex - i - 1]);
                sum += (avgValue - value) * (avgValue - value);
            }
            return (float)Math.Sqrt(sum / (float)range);
        }

        private static float ChangeInPercent(float current, float prev) => 100f * ((prev != 0) ? (current - prev) / prev : 0);
    }
}
