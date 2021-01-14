﻿using MarketOps.SystemData.Types;
using MarketOps.SystemData.Extensions;

namespace MarketOps.Controls.DrawDowns
{
    /// <summary>
    /// Maps SystemDrawDown.
    /// </summary>
    internal class SystemDrawDownMapper
    {
        public SystemDrawDownMapper(SystemDrawDown value)
        {
            DDValue = 100f * value.DD();
            Length = value.Ticks;
        }

        public float DDValue { get; }
        public int Length { get; }
    }
}
