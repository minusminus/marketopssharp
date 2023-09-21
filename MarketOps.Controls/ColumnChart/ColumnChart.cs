using MarketOps.Controls.ChartsUtils;
using ScottPlot.Plottable;
using System.Windows.Forms;

namespace MarketOps.Controls.ColumnChart
{
    public partial class ColumnChart : UserControl
    {
        private BarPlot _barPlot;

        public ColumnChart()
        {
            InitializeComponent();
            plotColumns.Plot.SetUpPlotArea();
        }

        public void LoadData(ColumnChartData data, double barTickWidth, params double[] verticalLines)
        {
            plotColumns.Plot.Clear();

            plotColumns.Plot.AddVerticalLines(verticalLines, PlotConsts.SecondaryPointColor);
            _barPlot = AddBarPlot(data, barTickWidth);
            plotColumns.Plot.SetAxisLimits(yMin: 0);

            plotColumns.Refresh();
        }

        private BarPlot AddBarPlot(ColumnChartData data, double barTickWidth)
        {
            var result = plotColumns.Plot.AddBar(data.Values, data.Positions, PlotConsts.PrimaryPointColor);
            result.BarWidth = barTickWidth * 0.8;
            result.ShowValuesAboveBars = true;
            result.Font.Size = PlotConsts.TooltipTextSize;
            result.Font.Color = PlotConsts.AxisTextColor;
            return result;
        }
    }
}
