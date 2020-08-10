using MarketOps.StockData.Types;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// Open price selector for system processor.
    /// </summary>
    internal static class OpenPriceSelector
    {
        public static float OnOpen(StockPricesData pricesData, int priceIndex, Signal signal) => pricesData.O[priceIndex];

        public static float OnClose(StockPricesData pricesData, int priceIndex, Signal signal) => pricesData.C[priceIndex];

        public static float OnPrice(StockPricesData pricesData, int priceIndex, Signal signal)
        {
            if (((signal.Direction == PositionDir.Long) && (pricesData.O[priceIndex] >= signal.Price))
             || ((signal.Direction == PositionDir.Short) && (pricesData.O[priceIndex] <= signal.Price)))
                return pricesData.O[priceIndex];
            else
                return signal.Price;
        }
    }
}
