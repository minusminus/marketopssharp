using MarketOps.Controls.Properties;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Drawing;

namespace MarketOps.Controls.PriceChart
{
    /// <summary>
    /// Images mapping for position open/close annotations.
    /// </summary>
    internal static class PositionOpenCloseImages
    {
        public const int IndexOpen = 0;
        public const int IndexClose = 1;

        public static readonly Dictionary<PositionDir, Bitmap[]> Images = new Dictionary<PositionDir, Bitmap[]>()
        {
            {
                PositionDir.Long,
                new []{
                    (Bitmap)Resources.ResourceManager.GetObject("uparrow_green"),
                    (Bitmap)Resources.ResourceManager.GetObject("downarrow_red"),
                }
            },
            {
                PositionDir.Short,
                new []{
                    (Bitmap)Resources.ResourceManager.GetObject("downarrow_green"),
                    (Bitmap)Resources.ResourceManager.GetObject("uparrow_red"),
                }
            },
        };
    }
}
