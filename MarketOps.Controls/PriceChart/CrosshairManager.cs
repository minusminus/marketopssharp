using ScottPlot;
using ScottPlot.Plottable;
using System.Collections.Generic;
using System.Windows.Forms;
using MarketOps.Controls.PriceChart.PVChart;

namespace MarketOps.Controls.PriceChart
{
    /// <summary>
    /// Manages Crosshair plots on FormsPlot objects.
    /// </summary>
    internal class CrosshairManager
    {
        private readonly List<FormsPlot> _charts = new List<FormsPlot>();
        private readonly List<Crosshair> _crosshairs = new List<Crosshair>();

        public void Add(FormsPlot chart, bool verticalLabel)
        {
            _charts.Add(chart);
            chart.MouseMove += OnCrosshairMouseMove;

            var crosshair = chart.Plot.AddCrosshair(0, 0);
            _crosshairs.Add(crosshair);
            crosshair.SetUpCrosshair();
            crosshair.VerticalLine.PositionLabel = verticalLabel;
        }

        public void Remove(FormsPlot chart) => 
            Remove(FindChartIndex(chart));

        public void Remove(int index)
        {
            _charts[index].MouseMove -= OnCrosshairMouseMove;
            _charts.RemoveAt(index);

            _crosshairs.RemoveAt(index);
        }

        public void Clear()
        {
            while (_charts.Count > 0)
                Remove(0);
        }

        private int FindChartIndex(FormsPlot chart) =>
            _charts.IndexOf(chart);

        private void OnCrosshairMouseMove(object sender, MouseEventArgs e)
        {
            FormsPlot chart = (FormsPlot)sender;
            var mouseCoords = chart.GetMouseCoordinates();
            int senderIndex = FindChartIndex(chart);

            for(int i=0; i<_charts.Count; i++)
                MoveCrosshair(i, mouseCoords.x, mouseCoords.y);

            void MoveCrosshair(int index, double x, double y)
            {
                var crossPlot = _crosshairs[index];
                crossPlot.X = x;
                crossPlot.Y = y;

                crossPlot.HorizontalLine.IsVisible = (index == senderIndex);

                _charts[index].Refresh();
            }
        }
    }
}
