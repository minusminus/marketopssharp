using ScottPlot.Plottable;
using ScottPlot;
using System.Windows.Forms;
using System;

namespace MarketOps.Controls.ChartsUtils
{
    /// <summary>
    /// Moves tooltip between points on plot.
    /// 
    /// Mover should be called UnlinkPlot before recreation.
    /// </summary>
    internal class PlotTooltipMover
    {
        private readonly FormsPlot _formsPlot;
        private readonly Tooltip _tooltip;
        private readonly Func<(double x, double y), double, (double x, double y, int index)> _getNearestPoint;
        private readonly Func<(double x, double y, int index), string> _getTooltipLabel;

        private bool _mouseOverPlot;
        private int _lastHitIndex;

        public PlotTooltipMover(FormsPlot formsPlot, Tooltip tooltip,
            Func<(double x, double y), double, (double x, double y, int index)> getNearestPoint,
            Func<(double x, double y, int index), string> getTooltipLabel)
        {
            _formsPlot = formsPlot;
            _tooltip = tooltip;
            _getNearestPoint = getNearestPoint;
            _getTooltipLabel = getTooltipLabel;

            _mouseOverPlot = false;
            _lastHitIndex = -1;
            LinkPlot();
        }

        public void UnlinkPlot()
        {
            _formsPlot.MouseEnter -= OnMouseEnter;
            _formsPlot.MouseLeave -= OnMouseLeave;
            _formsPlot.MouseMove -= OnMouseMove;
        }

        private void LinkPlot()
        {
            _formsPlot.MouseEnter += OnMouseEnter;
            _formsPlot.MouseLeave += OnMouseLeave;
            _formsPlot.MouseMove += OnMouseMove;
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            _mouseOverPlot = true;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            _mouseOverPlot = false;
            _tooltip.IsVisible = false;
            _lastHitIndex = -1;
            _formsPlot.Refresh();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseOverPlot) return;

            var mouseCoords = _formsPlot.GetMouseCoordinates();
            double xyRatio = _formsPlot.Plot.XAxis.Dims.PxPerUnit / _formsPlot.Plot.YAxis.Dims.PxPerUnit;
            var pos = _getNearestPoint(mouseCoords, xyRatio);
            if (_lastHitIndex == pos.index) return;

            _lastHitIndex = pos.index;
            _tooltip.IsVisible = true;
            _tooltip.X = pos.x;
            _tooltip.Y = pos.y;
            _tooltip.Label = _getTooltipLabel(pos);

            _formsPlot.Refresh();
        }
    }
}
