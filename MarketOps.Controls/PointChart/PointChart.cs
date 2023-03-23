using MarketOps.Controls.ChartsUtils;
using ScottPlot.Plottable;
using System.Windows.Forms;

namespace MarketOps.Controls.PointChart
{
    public partial class PointChart : UserControl
    {
        private ScatterPlot _scatter;
        private Tooltip _tooltip;
        private PlotTooltipMover _tooltipMover;
        private string _tooltipFormat;

        public PointChart()
        {
            InitializeComponent();
            plotPoints.Plot.SetUpPlotArea();
        }

        public void LoadData(PointChartData data, string tooltipFormat)
        {
            _tooltipFormat = tooltipFormat;

            _tooltipMover?.UnlinkPlot();
            _tooltipMover = null;
            plotPoints.Plot.Clear();

            _scatter = plotPoints.Plot.AddScatter(data.X, data.Y, lineWidth: 0);
            _tooltip = plotPoints.Plot.CreateTooltip();
            _tooltipMover = new PlotTooltipMover(plotPoints, _tooltip, GetNearestPoint, GetTooltipLabel);

            plotPoints.Refresh();
        }

        private (double x, double y, int index) GetNearestPoint((double x, double y) mouseCoords, double xyRatio) => 
            _scatter.GetPointNearest(mouseCoords.x, mouseCoords.y, xyRatio);

        private string GetTooltipLabel((double x, double y, int index) nearestPoint) =>
            string.Format(_tooltipFormat, _scatter.Xs[nearestPoint.index], _scatter.Ys[nearestPoint.index]);
    }
}
