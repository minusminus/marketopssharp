﻿using MarketOps.SystemData.Types;
using System;

namespace MarketOps.SystemData.Interfaces
{
    /// <summary>
    /// Interface for postion close mode and level calculator.
    /// Implementation should update CloseMode and CloseModePrice.
    /// </summary>
    public interface IMMPositionCloseCalculator
    {
        void CalculateCloseMode(Position position, DateTime ts);
    }
}
