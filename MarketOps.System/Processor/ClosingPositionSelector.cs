using MarketOps.StockData.Types;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// Closing positions selector for system processor.
    /// Accepts specified positions.
    /// </summary>
    internal static class ClosingPositionSelector
    {
        public static bool OnOpen(Position position, StockPricesData pricesData, int priceIndex) =>
            position.CloseMode == PositionCloseMode.OnOpen;

        public static bool OnClose(Position position, StockPricesData pricesData, int priceIndex) =>
            position.CloseMode == PositionCloseMode.OnClose;

        public static bool OnPrice(Position position, StockPricesData pricesData, int priceIndex) =>
            (position.CloseMode == PositionCloseMode.OnPriceHit)
            && (
                ((position.Direction == PositionDir.Long) && (position.CloseModePrice >= pricesData.L[priceIndex]))
                || ((position.Direction == PositionDir.Short) && (position.CloseModePrice <= pricesData.H[priceIndex]))
            );
    }
}
