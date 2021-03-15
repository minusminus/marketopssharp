using NUnit.Framework;
using Shouldly;
using MarketOps.SystemExecutor.Processor;
using MarketOps.StockData.Types;
using System;
using System.Collections.Generic;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemExecutor.Tests.Processor
{
    [TestFixture]
    public class SignalVerifierTests
    {
        private readonly StockDefinition _stock = new StockDefinition() { ID = 1 };

        [Test]
        public void Verify_Signal_NullStock__Throws()
        {
            Signal signal = new Signal() { Stock = null };
            Should.Throw<Exception>(() => SignalVerifier.Verify(signal));
        }

        [Test]
        public void Verify_Signal_ZeroVolume__Throws()
        {
            Signal signal = new Signal() { Stock = _stock, Volume = 0 };
            Should.Throw<Exception>(() => SignalVerifier.Verify(signal));
        }

        [Test]
        public void Verify_Signal_NotNullStock_NonZeroVolume__DoesNotThrow()
        {
            Signal signal = new Signal() { Stock = _stock, Volume = 1 };
            SignalVerifier.Verify(signal);
        }

        [Test]
        public void Verify_Rebalance_NullNewBalanceList__Throws()
        {
            Signal signal = new Signal() { Stock = null, Rebalance = true, NewBalance = null };
            Should.Throw<Exception>(() => SignalVerifier.Verify(signal));
        }

        [TestCase(SignalType.EnterOnPrice)]
        public void Verify_Rebalance_UnsupportedSignalType__Throws(SignalType signalType)
        {
            Signal signal = new Signal() { Stock = null, Type = signalType, Rebalance = true, NewBalance = new List<(StockDefinition stockDef, float balance)>() };
            Should.Throw<Exception>(() => SignalVerifier.Verify(signal));
        }

        [TestCase(SignalType.EnterOnOpen)]
        [TestCase(SignalType.EnterOnClose)]
        public void Verify_Rebalance_SupportedSignalType__DoesNotThrow(SignalType signalType)
        {
            Signal signal = new Signal() { Stock = null, Direction = PositionDir.Long, Type = signalType, Rebalance = true, NewBalance = new List<(StockDefinition stockDef, float balance)>() };
            SignalVerifier.Verify(signal);
        }
    }
}
