using MarketOps.SystemData.Types;
using System;
using System.Linq;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// Verifies system signal.
    /// Throws exceptions on errors.
    /// </summary>
    internal static class SignalVerifier
    {
        public static void Verify(Signal signal)
        {
            if (signal.Rebalance)
                VerifyRebalanceSignal(signal);
            else
                VerifyStandardSignal(signal);
        }

        private static void VerifyRebalanceSignal(Signal signal)
        {
            if (signal.NewBalance == null)
                ThrowException(signal, "New balance list undefined");
            if ((signal.Type != SignalType.EnterOnOpen) && (signal.Type != SignalType.EnterOnClose))
                ThrowException(signal, $"Rebalance is suported only for OnOpen and OnClose signals (unsupported type: {signal.Type})");
            if (signal.NewBalance.Sum(x => x.balance) > 1)
                ThrowException(signal, $"Total new balance is greater than 1");
        }

        private static void VerifyStandardSignal(Signal signal)
        {
            if (signal.Stock == null)
                ThrowException(signal, "Signal stock undefined");
            if (signal.Volume == 0)
                ThrowException(signal, "Signal volume 0");
        }

        private static void ThrowException(Signal signal, string header) =>
            throw new Exception($"{header} for: {signal.Stock.FullName} (ID = {signal.Stock.ID})");
    }
}
