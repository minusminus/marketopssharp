using MarketOps.SystemData.Types;
using System;
using System.Collections.Generic;

namespace MarketOps.SystemData.Interfaces
{
    /// <summary>
    /// Interface for signal generation on tick open.
    /// </summary>
    public interface ISignalGeneratorOnOpen
    {
        List<Signal> GenerateOnOpen(DateTime ts, int leadingIndex, SystemState systemState);
    }
}
