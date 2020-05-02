using System;
using System.Collections.Generic;

namespace MarketOps.System.Interfaces
{
    /// <summary>
    /// Interface for signal generation on tick close.
    /// </summary>
    public interface ISignalGeneratorOnClose
    {
        void Initialize(DateTime tsStart);
        List<Signal> GenerateOnClose(DateTime ts);
    }
}
