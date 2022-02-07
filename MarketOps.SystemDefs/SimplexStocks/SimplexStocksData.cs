using MarketOps.StockData.Types;

namespace MarketOps.SystemDefs.SimplexStocks
{
    /// <summary>
    /// Data for simplex stocks.
    /// </summary>
    internal class SimplexStocksData
    {
        public readonly StockDefinition[] Stocks;
        public readonly bool[] Active;
        public readonly double[] Prices;
        public readonly double[] AvgChange;
        public readonly double[] AvgChangeSigma;
        public readonly double[] AvgProfit;

        public SimplexStocksData(int length)
        {
            Stocks = new StockDefinition[length];
            Active = new bool[length];
            Prices = new double[length];
            AvgChange = new double[length];
            AvgChangeSigma = new double[length];
            AvgProfit = new double[length];
        }
    }
}
