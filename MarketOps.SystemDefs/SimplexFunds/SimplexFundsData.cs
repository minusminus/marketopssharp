using MarketOps.StockData.Types;

namespace MarketOps.SystemDefs.SimplexFunds
{
    /// <summary>
    /// Data for simplex funds.
    /// </summary>
    internal class SimplexFundsData
    {
        public readonly StockDefinition[] Stocks;
        public readonly float[] AvgChange;
        public readonly float[] AvgChangeSigma;
        public readonly float[] AvgProfit;

        public SimplexFundsData(int length)
        {
            Stocks = new StockDefinition[length];
            AvgChange = new float[length];
            AvgChangeSigma = new float[length];
            AvgProfit = new float[length];
        }
    }
}
