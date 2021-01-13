using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemAnalysis.Extensions
{
    /// <summary>
    /// Extensions for drawdawns.
    /// </summary>
    public static class DrawDownsExtensions
    {
        public static float MaxDD(this List<SystemDrawDown> dds) => 
            dds.Max(v => (v.TopValue.Value - v.BottomValue.Value) / v.TopValue.Value);
    }
}
