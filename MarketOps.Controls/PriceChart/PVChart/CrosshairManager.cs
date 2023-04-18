using ScottPlot.Plottable;
using ScottPlot;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace MarketOps.Controls.PriceChart.PVChart
{
    internal delegate void CrosshairVisibilityChanged(FormsPlot chart);
    internal delegate string CrosshairPositionTooltip(double value);

    /// <summary>
    /// Manages Crosshair plots on FormsPlot objects.
    /// </summary>
    internal class CrosshairManager
    {
        private readonly List<FormsPlot> _charts = new List<FormsPlot>();
        private readonly List<Crosshair> _crosshairs = new List<Crosshair>();

        public event CrosshairVisibilityChanged OnCrosshairVisibilityChanged;
        public event CrosshairPositionTooltip OnVerticalPositionTooltip;

        public void Add(FormsPlot chart, bool verticalLabel)
        {
            _charts.Add(chart);
            LinkEvents(chart);

            var crosshair = chart.Plot.AddCrosshair(0, 0);
            _crosshairs.Add(crosshair);
            crosshair.SetUpCrosshair();
            crosshair.VerticalLine.PositionLabel = verticalLabel;
            crosshair.VerticalLine.PositionFormatter = VerticalPositionFormatter;
            crosshair.IsVisible = false;
        }

        public void Remove(FormsPlot chart) =>
            Remove(FindChartIndex(chart));

        public void Remove(int index)
        {
            UnlinkEvents(_charts[index]);
            _charts[index].Plot.Remove(_crosshairs[index]);
            _charts.RemoveAt(index);

            _crosshairs.RemoveAt(index);
        }

        public void Clear()
        {
            while (_charts.Count > 0)
                Remove(0);
        }

        private void LinkEvents(FormsPlot chart)
        {
            chart.MouseMove += OnCrosshairMouseMove;
            chart.MouseEnter += OnCrosshairMouseEnter;
            chart.MouseLeave += OnCrosshairMouseLeave;
        }

        private void UnlinkEvents(FormsPlot chart)
        {
            chart.MouseMove -= OnCrosshairMouseMove;
            chart.MouseEnter -= OnCrosshairMouseEnter;
            chart.MouseLeave -= OnCrosshairMouseLeave;
        }

        private void OnCrosshairMouseEnter(object sender, EventArgs e) =>
            SetCrosshairVisibility(true);

        private void OnCrosshairMouseLeave(object sender, EventArgs e) =>
            SetCrosshairVisibility(false);

        private string VerticalPositionFormatter(double value) => 
            OnVerticalPositionTooltip != null
                ? OnVerticalPositionTooltip(value)
                : value.ToString();

        private int FindChartIndex(FormsPlot chart) =>
            _charts.IndexOf(chart);

        private void OnCrosshairMouseMove(object sender, MouseEventArgs e)
        {
            FormsPlot chart = (FormsPlot)sender;
            var mouseCoords = chart.GetMouseCoordinates();
            int senderIndex = FindChartIndex(chart);

            for (int i = 0; i < _charts.Count; i++)
                MoveCrosshair(i, mouseCoords.x, mouseCoords.y, senderIndex);
        }

        private void MoveCrosshair(int index, double x, double y, int senderIndex)
        {
            var crossPlot = _crosshairs[index];
            crossPlot.X = x;
            crossPlot.Y = y;

            crossPlot.HorizontalLine.IsVisible = index == senderIndex;

            _charts[index].Refresh();
        }

        private void SetCrosshairVisibility(bool visible)
        {
            for (int i = 0; i < _crosshairs.Count; i++)
            {
                _crosshairs[i].IsVisible = visible;
                OnCrosshairVisibilityChanged?.Invoke(_charts[i]);
            }
        }
    }
}
