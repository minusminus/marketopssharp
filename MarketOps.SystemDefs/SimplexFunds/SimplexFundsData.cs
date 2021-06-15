using MarketOps.StockData.Types;

namespace MarketOps.SystemDefs.SimplexFunds
{
    /// <summary>
    /// Data for simplex funds.
    /// </summary>
    internal class SimplexFundsData
    {
        public readonly StockDefinition[] Stocks;
        public readonly double[] Prices;
        public readonly double[] AvgChange;
        public readonly double[] AvgChangeSigma;
        public readonly double[] AvgProfit;

        public SimplexFundsData(int length)
        {
            Stocks = new StockDefinition[length];
            Prices = new double[length];
            AvgChange = new double[length];
            AvgChangeSigma = new double[length];
            AvgProfit = new double[length];
        }
    }
}
