using MarketOps.StockData.Types;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// Open price selector for system processor.
    /// </summary>
    internal static class OpenPriceSelector
    {
        public static float OnOpen(Signal signal, StockPricesData pricesData, int priceIndex) => pricesData.O[priceIndex];

        public static float OnClose(Signal signal, StockPricesData pricesData, int priceIndex) => pricesData.C[priceIndex];

        public static float OnPrice(Signal signal, StockPricesData pricesData, int priceIndex)
        {
            if (((signal.Direction == PositionDir.Long) && (pricesData.O[priceIndex] >= signal.Price))
             || ((signal.Direction == PositionDir.Short) && (pricesData.O[priceIndex] <= signal.Price)))
                return pricesData.O[priceIndex];
            else
                return signal.Price;
        }
    }
}
