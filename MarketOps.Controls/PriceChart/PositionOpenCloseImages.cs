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
        public const string LongOpen = "longopen";
        public const string LongClose = "longclose";
        public const string ShortOpen = "shortopen";
        public const string ShortClose = "shortclose";

        public static readonly Dictionary<string, Bitmap> Images = new Dictionary<string, Bitmap>()
        {
            { LongOpen, MarketOps_Controls.uparrow_green },
            { LongClose, MarketOps_Controls.downarrow_red },
            { ShortOpen, MarketOps_Controls.downarrow_green },
            { ShortClose, MarketOps_Controls.uparrow_red }
        };


        public const int IndexOpen = 0;
        public const int IndexClose = 1;

        public static readonly Dictionary<PositionDir, string[]> ImageName = new Dictionary<PositionDir, string[]>()
        {
            {
                PositionDir.Long,
                new []{ LongOpen, LongClose }
            },
            {
                PositionDir.Short,
                new []{  ShortOpen, ShortClose }
            },
        };

        public static readonly Dictionary<PositionDir, int[]> AnnotationAnchorYValueIndex = new Dictionary<PositionDir, int[]>()
        {
            {
                PositionDir.Long,
                new [] {1, 0}
            },
            {
                PositionDir.Short,
                new [] {0, 1}
            },
        };

        public static readonly Dictionary<PositionDir, ContentAlignment[]> AnnotationContentAlignment = new Dictionary<PositionDir, ContentAlignment[]>()
        {
            {
                PositionDir.Long,
                new [] { ContentAlignment.TopCenter, ContentAlignment.BottomCenter }
            },
            {
                PositionDir.Short,
                new [] { ContentAlignment.BottomCenter, ContentAlignment.TopCenter }
            },
        };
    }
}
