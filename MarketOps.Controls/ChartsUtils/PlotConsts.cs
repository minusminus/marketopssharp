﻿using System.Drawing;

namespace MarketOps.Controls.ChartsUtils
{
    /// <summary>
    /// Const for ScottPlot plots.
    /// </summary>
    internal static class PlotConsts
    {
        public static readonly Color PrimaryLineColor = Color.DodgerBlue;
        public static readonly Color SecondaryLineColor = Color.Orange;

        public static readonly Color PrimaryPointColor = Color.FromArgb(0x1F, 0x77, 0xB4);
        public static readonly Color SecondaryPointColor = Color.FromArgb(0xFF, 0x98, 0x96);

        public static readonly Color CandleColorUp = Color.White;
        public static readonly Color CandleColorDown = Color.Black;

        public static readonly Color AxisColor = Color.LightGray;
        public static readonly Color AxisTextColor = Color.DarkGray;
        public const float AxisTextSize = 8;
        public const float TooltipTextSize = 10;
        public const float BottomPlotXTicksSize = 5;

        public static readonly Color PositionAnnotationOpenColor = Color.Green;
        public static readonly Color PositionAnnotationCloseColor = Color.Red;

        public const string PricesAreaName = "areaPrices";  //for backward compatibility, to remove in future version
    }
}
