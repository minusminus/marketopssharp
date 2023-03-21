using MarketOps.Controls.ChartsUtils;
using ScottPlot.Plottable;
using System.Windows.Forms;

namespace MarketOps.Controls.PointChart
{
    public partial class PointChart : UserControl
    {
        private ScatterPlot _scatter;
        private Tooltip _tooltip;
        private bool _mouseOverPlot;
        private int _lastHitIndex;
        private string _tooltipFormat;

        public PointChart()
        {
            InitializeComponent();
            _mouseOverPlot = false;
            _lastHitIndex = -1;
            plotPoints.Plot.SetUpPlotArea();
        }

        public void LoadData(PointChartData data, string tooltipFormat)
        {
            _tooltipFormat = tooltipFormat;

            plotPoints.Plot.Clear();
            _scatter = plotPoints.Plot.AddScatter(data.X, data.Y, lineWidth: 0);
            _tooltip = plotPoints.Plot.CreateTooltip();

            plotPoints.Refresh();
        }

        private void plotPoints_MouseEnter(object sender, System.EventArgs e)
        {
            _mouseOverPlot = true;
        }

        private void plotPoints_MouseLeave(object sender, System.EventArgs e)
        {
            _mouseOverPlot = false;
            _tooltip.IsVisible = false;
            _lastHitIndex = -1;
            plotPoints.Refresh();
        }

        private void plotPoints_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseOverPlot) return;

            var mouseCoords = plotPoints.GetMouseCoordinates();
            double xyRatio = plotPoints.Plot.XAxis.Dims.PxPerUnit / plotPoints.Plot.YAxis.Dims.PxPerUnit;
            var pos = _scatter.GetPointNearest(mouseCoords.x, mouseCoords.y, xyRatio);
            if (_lastHitIndex == pos.index) return;

            _lastHitIndex = pos.index;
            _tooltip.IsVisible = true;
            _tooltip.X = _scatter.Xs[pos.index];
            _tooltip.Y = _scatter.Ys[pos.index];
            _tooltip.Label = string.Format(_tooltipFormat, _scatter.Xs[pos.index], _scatter.Ys[pos.index]);

            plotPoints.Refresh();
        }
    }
}
