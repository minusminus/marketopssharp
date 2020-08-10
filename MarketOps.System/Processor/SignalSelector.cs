using MarketOps.StockData.Types;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// Signals selectors for system processor.
    /// </summary>
    internal static class SignalSelector
    {
        public static IEnumerable<Signal> SignalsOnOpen(IEnumerable<Signal> signals) =>
            signals.Where(s => s.Type == SignalType.EnterOnOpen);

        public static IEnumerable<Signal> SignalsOnClose(IEnumerable<Signal> signals) =>
            signals.Where(s => s.Type == SignalType.EnterOnClose);

        public static IEnumerable<Signal> SignalsOnPrice(IEnumerable<Signal> signals, StockPricesData pricesData, int priceIndex) =>
            signals.Where(s => (s.Type == SignalType.EnterOnPrice)
            && (((s.Direction == PositionDir.Long) && (s.Price <= pricesData.H[priceIndex]))
            || ((s.Direction == PositionDir.Short) && (s.Price >= pricesData.L[priceIndex])))
            );
    }
}
