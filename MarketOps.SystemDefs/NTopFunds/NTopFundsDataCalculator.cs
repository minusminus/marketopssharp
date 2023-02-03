using MarketOps.Maths;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using System;

namespace MarketOps.SystemDefs.NTopFunds
{
    /// <summary>
    /// N top funds data calculations.
    /// </summary>
    internal static class NTopFundsDataCalculator
    {
        public static void Initialize(NTopFundsData data, string[] fundsNames, IStockDataProvider dataProvider)
        {
            for (int i = 0; i < fundsNames.Length; i++)
                data.Stocks[i] = dataProvider.GetStockDefinition(fundsNames[i]);
        }

        public static void Calculate(NTopFundsData data, DateTime ts, int profitRange, int changeRange, StockDataRange dataRange, ISystemDataLoader dataLoader)
        {
            dataLoader.GetWithIndex(data.Stocks[0].FullName, dataRange, ts, 0, out StockPricesData spData0, out _);
            DateTime simLastTs = spData0.TS[spData0.TS.Length - 1];

            for (int i = 0; i < data.Stocks.Length; i++)
            {
                data.Active[i] = dataLoader.GetWithIndex(data.Stocks[i].FullName, dataRange, ts, Math.Max(profitRange, changeRange) + 1, out StockPricesData spData, out int dataIndex)
                                && NotLastButOneDataIndex(spData, dataIndex, simLastTs);
                if (!data.Active[i]) continue;

                data.Prices[i] = spData.C[dataIndex];
                data.Profit[i] = AvgChangeInPercent(spData.C, dataIndex, profitRange, null);
                data.Risk[i] = AvgChangeInPercent(spData.C, dataIndex, changeRange, Math.Abs);
                //data.AvgChangeSigma[i] = StdDev(spData.C, dataIndex, changeRange, data.AvgChange[i]);
            }
        }

        private static bool NotLastButOneDataIndex(StockPricesData spData, int dataIndex, DateTime simLastTs) =>
            (spData.TS[spData.TS.Length - 1] == simLastTs) || (dataIndex < spData.Length - 2);

        private static double AvgChangeInPercent(float[] tbl, int startIndex, int range, Func<double, double> operation)
        {
            double sum = 0;
            for (int i = 0; i < range; i++)
            {
                double change = ChangeInPercent.Calculate(tbl[startIndex - i], tbl[startIndex - i - 1]);
                if (operation != null)
                    change = operation(change);
                sum += change;
            }
            return sum / (double)range;
        }
    }
}
