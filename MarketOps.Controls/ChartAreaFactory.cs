using MarketOps.Controls.ChartsUtils;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Controls
{
    /// <summary>
    /// Chart area factory for price chart.
    /// </summary>
    internal class ChartAreaFactory
    {
        public ChartArea CreateArea(string areaName, float areaHeight)
        {
            Font fontLabel = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, (byte)(238));

            return new ChartArea(areaName)
            {
                AlignWithChartArea = PlotConsts.PricesAreaName,
                AxisX =
                {
                    IsLabelAutoFit = false,
                    IsStartedFromZero = false,
                    LabelStyle =
                    {
                        Enabled = false,
                        Font = fontLabel,
                        ForeColor = System.Drawing.Color.DarkGray
                    },
                    LineColor = System.Drawing.Color.DarkGray,
                    MajorGrid = {LineColor = System.Drawing.Color.LightGray},
                    MajorTickMark =
                    {
                        Enabled = false,
                        LineColor = System.Drawing.Color.DarkGray
                    }
                },
                AxisY =
                {
                    IsLabelAutoFit = false,
                    IsStartedFromZero = false,
                    LabelStyle =
                    {
                        Font = fontLabel,
                        ForeColor = System.Drawing.Color.DarkGray
                    },
                    LineColor = System.Drawing.Color.DarkGray,
                    MajorGrid = {LineColor = System.Drawing.Color.LightGray},
                    MajorTickMark = {LineColor = System.Drawing.Color.DarkGray}
                },
                CursorX =
                {
                    IsUserEnabled = true,
                    LineColor = System.Drawing.Color.Gray
                },
                CursorY =
                {
                    IsUserEnabled = true,
                    LineColor = System.Drawing.Color.Gray
                },
                Position =
                {
                    Auto = false,
                    Height = areaHeight,
                    Width = 100F,
                    Y = 100F - areaHeight
                }
            };
        }
    }
}
