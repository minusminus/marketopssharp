﻿using MarketOps.SystemData.Types;
using System;
using System.Collections.Generic;

namespace MarketOps.SystemData.Interfaces
{
    /// <summary>
    /// Interface for signal generation on tick close.
    /// </summary>
    public interface ISignalGeneratorOnClose
    {
        List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState);
    }
}
