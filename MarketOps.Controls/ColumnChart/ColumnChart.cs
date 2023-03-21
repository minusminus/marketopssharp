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

        public void LoadData(ColumnChartData data, double barTickWidth)
        {
            plotColumns.Plot.Clear();
            _barPlot = plotColumns.Plot.AddBar(data.Values, data.Positions);
            _barPlot.BarWidth = barTickWidth * 0.8;
            _barPlot.ShowValuesAboveBars = true;
            _barPlot.Font.Size = PlotConsts.TooltipTextSize;
            _barPlot.Font.Color = PlotConsts.AxisTextColor;
            plotColumns.Plot.SetAxisLimits(yMin: 0);

            plotColumns.Refresh();
        }
    }
}
