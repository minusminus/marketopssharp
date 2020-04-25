using System;
using System.Collections.Generic;

namespace MarketOps.System.Interfaces
{
    /// <summary>
    /// Interface for signal generation on tick open.
    /// </summary>
    public interface ISignalGeneratorOnOpen
    {
        List<Signal> GenerateOnOpen(DateTime ts);
    }
}
