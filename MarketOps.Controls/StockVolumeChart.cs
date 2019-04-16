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
    public partial class StockVolumeChart : UserControl
    {
        public StockVolumeChart()
        {
            InitializeComponent();
        }

        public Series Prices => SVChart.Series["dataPrices"];
        public Series Volume => SVChart.Series["dataVolume"];

        public delegate void ChartValueSelected(int selectedIndex);
        public event ChartValueSelected OnChartValueSelected;

        private void SVChart_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            foreach (var area in SVChart.ChartAreas)
            {
                if (area.CursorX.IsUserEnabled)
                    area.CursorX.SetCursorPixelPosition(p, true);
                if (area.CursorY.IsUserEnabled)
                    area.CursorY.SetCursorPixelPosition(p, true);
            }

            HitTestResult hit = SVChart.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (hit.ChartElementType == ChartElementType.DataPoint)
            {
                //DataPoint dp = hit.Object as DataPoint;
                OnChartValueSelected?.Invoke(hit.PointIndex);
            }
        }
    }
}
