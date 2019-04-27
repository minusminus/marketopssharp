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

        public Series PricesCandles => SVChart.Series["dataPricesCandles"];
        public Series PricesLine => SVChart.Series["dataPricesLine"];
        public Series Volume => SVChart.Series["dataVolume"];

        public delegate void ChartValueSelected(int selectedIndex);
        public event ChartValueSelected OnChartValueSelected;

        private void SVChart_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var area in SVChart.ChartAreas)
            {
                if (area.CursorX.IsUserEnabled)
                    area.CursorX.SetCursorPixelPosition(e.Location, true);
                if (area.CursorY.IsUserEnabled)
                    area.CursorY.SetCursorPixelPosition(e.Location, true);
            }

            OnChartValueSelected?.Invoke((int)SVChart.ChartAreas["areaPrices"].CursorX.Position - 1);
        }
    }
}
