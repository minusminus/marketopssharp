using MarketOps.StockData.Interfaces;

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
    }
}
