using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Controls
{
    public partial class PriceVolumeChart : UserControl
    {
        public PriceVolumeChart()
        {
            InitializeComponent();
            SetChartMode(PriceVolumeChartMode.Candles);
            PVChart.MouseWheel += PVChart_MouseWheel;
        }

        public PriceVolumeChartMode ChartMode { get; private set; }
        public void SetChartMode(PriceVolumeChartMode newMode)
        {
            ChartMode = newMode;
            PricesCandles.Enabled = (newMode == PriceVolumeChartMode.Candles);
            PricesLine.Enabled = (newMode == PriceVolumeChartMode.Lines);
            PricesLine.XValueType = ChartValueType.Date;
        }

        public Series PricesCandles => PVChart.Series["dataPricesCandles"];
        public Series PricesLine => PVChart.Series["dataPricesLine"];
        public Series Volume => PVChart.Series["dataVolume"];

        public delegate void ChartValueSelected(int selectedIndex);
        public event ChartValueSelected OnChartValueSelected;

        private void PVChart_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var area in PVChart.ChartAreas)
            {
                if (area.CursorX.IsUserEnabled)
                    area.CursorX.SetCursorPixelPosition(e.Location, true);
                if (area.CursorY.IsUserEnabled)
                    area.CursorY.SetCursorPixelPosition(e.Location, true);
            }

            OnChartValueSelected?.Invoke((int)PVChart.ChartAreas["areaPrices"].CursorX.Position - 1);
        }


        public void ResetZoom()
        {
            PVChart.ChartAreas["areaPrices"].AxisX.ScaleView.ZoomReset();
            SetYViewRange();
        }

        private void PVChart_MouseWheel(object sender, MouseEventArgs e)
        {
            Axis ax = PVChart.ChartAreas["areaPrices"].AxisX;
            Tuple<double, double> zoom = (new ChartZoomCalculator()).CalculateZoom(e.Delta < 0,
                ModifierKeys.HasFlag(Keys.Control), ax, e.Location);
            ax.ScaleView.Zoom(zoom.Item1, zoom.Item2);
            SetYViewRange();
            PVChart_MouseMove(sender, e);
        }

        private void PVChart_MouseEnter(object sender, EventArgs e)
        {
            if (!PVChart.Focused)
                PVChart.Focus();
        }

        private void PVChart_MouseLeave(object sender, EventArgs e)
        {
            if (PVChart.Focused)
                PVChart.Parent.Focus();
        }

        public void SetYViewRange()
        {
            Axis ax = PVChart.ChartAreas["areaPrices"].AxisX;
            Axis ay = PVChart.ChartAreas["areaPrices"].AxisY;
            Tuple<double, double> range = (new ChartYViewRangeCalculator()).CalculateRange(ax, PricesCandles.Points);
            ay.Minimum = range.Item1;
            ay.Maximum = range.Item2;
        }

        private void PVChart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (e.Axis != PVChart.ChartAreas["areaPrices"].AxisX) return;
            SetYViewRange();
        }
    }
}
