using MarketOps.StockData.Types;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// Signal selectors for system processor.
    /// Accepts specified signals.
    /// </summary>
    internal static class SignalSelector
    {
        public static bool OnOpen(Signal signal, StockPricesData pricesData, int priceIndex) =>
            signal.Type == SignalType.EnterOnOpen;

        public static bool OnClose(Signal signal, StockPricesData pricesData, int priceIndex) =>
            signal.Type == SignalType.EnterOnClose;

        public static bool OnPrice(Signal signal, StockPricesData pricesData, int priceIndex) =>
            (signal.Type == SignalType.EnterOnPrice)
            && (
                ((signal.Direction == PositionDir.Long) && (signal.Price <= pricesData.H[priceIndex]))
                || ((signal.Direction == PositionDir.Short) && (signal.Price >= pricesData.L[priceIndex]))
            );
    }
}
