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
        }

        private void PVChart_MouseWheel(object sender, MouseEventArgs e)
        {
            Axis ax = PVChart.ChartAreas["areaPrices"].AxisX;
            double xmin = ax.ScaleView.ViewMinimum;
            double xmax = ax.ScaleView.ViewMaximum;
            double xpos = ax.PixelPositionToValue(e.Location.X);

            const double zoomScale = 0.1;
            double addition = Math.Sign(e.Delta)*(xmax - xmin)*zoomScale;
            double halfzoomwidth = ((xmax - xmin)/2);
            //double zoomstart = Math.Max(xmin + addition, 0);
            //double zoomend = Math.Min(xmax - addition, ax.Maximum);
            double zoomstart = Math.Max(xpos - halfzoomwidth + addition, 0);
            double zoomend = Math.Min(xpos + halfzoomwidth - addition, ax.Maximum);
            ax.ScaleView.Zoom(zoomstart, zoomend);

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
    }
}
