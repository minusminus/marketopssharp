﻿using System;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemData.Interfaces
{
    /// <summary>
    /// Interface for system position slippage calculation.
    /// Calculates new value slipped by slippage.
    /// </summary>
    public interface ISlippage
    {
        float CalculateClose(StockType stockType, DateTime ts, PositionDir dir, float value);
        float CalculateOpen(StockType stockType, DateTime ts, PositionDir dir, float value);
    }
}
