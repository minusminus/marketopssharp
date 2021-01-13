using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemAnalysis.Extensions
{
    /// <summary>
    /// Extensions for drawdawns.
    /// </summary>
    public static class DrawDownsExtensions
    {
        public static float DD(this SystemDrawDown dd) =>
            (dd.TopValue.Value - dd.BottomValue.Value) / Math.Abs(dd.TopValue.Value);

        public static float MaxDD(this List<SystemDrawDown> dds) =>
            dds.Max(v => v.DD());
    }
}
