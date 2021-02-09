using MarketOps.SystemData.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemData.Extensions
{
    /// <summary>
    /// Extensions for drawdawns.
    /// </summary>
    public static class DrawDownsExtensions
    {
        public static float DD(this SystemDrawDown dd) =>
            (dd.TopValue.Value - dd.BottomValue.Value) / Math.Abs(dd.TopValue.Value);

        public static float MaxDD(this List<SystemDrawDown> dds) =>
            dds.Count == 0 ? 0 : dds.Max(v => v.DD());
    }
}
