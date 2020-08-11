using MarketOps.StockData.Types;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// Close price selector for system processor.
    /// </summary>
    internal static class ClosePriceSelector
    {
        public static float OnOpen(Position position, StockPricesData pricesData, int priceIndex) => pricesData.O[priceIndex];

        public static float OnClose(Position position, StockPricesData pricesData, int priceIndex) => pricesData.C[priceIndex];

        public static float OnPrice(Position position, StockPricesData pricesData, int priceIndex)
        {
            if (((position.Direction == PositionDir.Long) && (pricesData.O[priceIndex] <= position.CloseModePrice))
             || ((position.Direction == PositionDir.Short) && (pricesData.O[priceIndex] >= position.CloseModePrice)))
                return pricesData.O[priceIndex];
            else
                return position.CloseModePrice;
        }
    }
}
