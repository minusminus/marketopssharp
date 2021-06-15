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
            dataLoader.GetWithIndex(data.Stocks[0].FullName, dataRange, ts, 0, out StockPricesData spData0, out _);
            DateTime simLastTs = spData0.TS[spData0.TS.Length - 1];

            for (int i = 0; i < data.Stocks.Length; i++)
            {
                data.Active[i] = dataLoader.GetWithIndex(data.Stocks[i].FullName, dataRange, ts, Math.Max(profitRange, changeRange) + 1, out StockPricesData spData, out int dataIndex);
                if (!data.Active[i]) continue;
                data.Active[i] = NotLastButOneDataIndex(spData, dataIndex, simLastTs);
                if (!data.Active[i]) continue;

                data.Prices[i] = spData.C[dataIndex];
                data.AvgProfit[i] = AvgChangeInPercent(spData.C, dataIndex, profitRange, null);
                data.AvgChange[i] = AvgChangeInPercent(spData.C, dataIndex, changeRange, Math.Abs);
                data.AvgChangeSigma[i] = StdDev(spData.C, dataIndex, changeRange, data.AvgChange[i]);
            }
        }

        private static bool NotLastButOneDataIndex(StockPricesData spData, int dataIndex, DateTime simLastTs) =>
            (spData.TS[spData.TS.Length - 1] == simLastTs) || (dataIndex < spData.Length - 2);

        private static double AvgChangeInPercent(float[] tbl, int startIndex, int range, Func<double, double> operation)
        {
            double sum = 0;
            for (int i = 0; i < range; i++)
            {
                double change = ChangeInPercent(tbl[startIndex - i], tbl[startIndex - i - 1]);
                if (operation != null)
                    change = operation(change);
                //sum += ChangeInPercent(tbl[startIndex - i], tbl[startIndex - i - 1]);
                sum += change;
            }
            return sum / (double)range;
        }

        private static double StdDev(float[] tbl, int startIndex, int range, double avgValue)
        {
            double sum = 0;
            for (int i = 0; i < range; i++)
            {
                double value = Math.Abs(ChangeInPercent(tbl[startIndex - i], tbl[startIndex - i - 1]));
                sum += (avgValue - value) * (avgValue - value);
            }
            return (double)Math.Sqrt(sum / (double)range);
        }

        private static double ChangeInPercent(double current, double prev) => (prev != 0) ? (current - prev) / prev : 0;
    }
}
