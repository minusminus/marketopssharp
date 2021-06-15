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

        public static void Calculate(SimplexFundsData data, DateTime ts, int profitRange, int changeRange, StockDataRange dataRange, ISystemDataLoader dataLoader)
        {
            for (int i = 0; i < data.Stocks.Length; i++)
            {
                data.Active[i] = dataLoader.GetWithIndex(data.Stocks[i].FullName, dataRange, ts, Math.Max(profitRange, changeRange) + 1, out StockPricesData spData, out int dataIndex);
                if (!data.Active[i]) continue;

                data.Prices[i] = spData.C[dataIndex];
                data.AvgProfit[i] = AvgChangeInPercent(spData.C, dataIndex, profitRange);
                data.AvgChange[i] = AvgChangeInPercent(spData.C, dataIndex, changeRange);
                data.AvgChangeSigma[i] = StdDev(spData.C, dataIndex, changeRange, data.AvgChange[i]);
            }
        }

        private static double AvgChangeInPercent(float[] tbl, int startIndex, int range)
        {
            double sum = 0;
            for (int i = 0; i < range; i++)
                sum += ChangeInPercent(tbl[startIndex - i], tbl[startIndex - i - 1]);
            return sum / (double)range;
        }

        private static double StdDev(float[] tbl, int startIndex, int range, double avgValue)
        {
            double sum = 0;
            for (int i = 0; i < range; i++)
            {
                double value = ChangeInPercent(tbl[startIndex - i], tbl[startIndex - i - 1]);
                sum += (avgValue - value) * (avgValue - value);
            }
            return (double)Math.Sqrt(sum / (double)range);
        }

        private static double ChangeInPercent(double current, double prev) => (prev != 0) ? (current - prev) / prev : 0;
    }
}
