using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.PriceChart
{
    public partial class PriceVolumeChart : UserControl
    {
        public PriceVolumeChart()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetChartMode(PriceVolumeChartMode.Candles);
            PVChart.MouseWheel += PVChart_MouseWheel;
        }

        #region public properties and events
        public PriceVolumeChartMode ChartMode { get; private set; }

        public Series PricesCandles => PVChart.Series["dataPricesCandles"];
        public Series PricesLine => PVChart.Series["dataPricesLine"];
        public Series Volume => PVChart.Series["dataVolume"];

        public delegate void ChartValueSelected(int selectedIndex);
        public event ChartValueSelected OnChartValueSelected;

        public delegate string GetAxisXToolTip(int selectedIndex);
        public event GetAxisXToolTip OnGetAxisXToolTip;
        public delegate string GetAxisYToolTip(double selectedValue);
        public event GetAxisYToolTip OnGetAxisYToolTip;
        #endregion

        #region internal events
        private void PVChart_MouseMove(object sender, MouseEventArgs e)
        {
            if (PricesCandles.Points.Count == 0) return;

            foreach (var area in PVChart.ChartAreas)
            {
                if (area.CursorX.IsUserEnabled)
                    area.CursorX.SetCursorPixelPosition(e.Location, true);
                if (area.CursorY.IsUserEnabled)
                    area.CursorY.SetCursorPixelPosition(e.Location, true);
            }

            int xSelectedIndex = (int)PVChart.ChartAreas["areaPrices"].CursorX.Position - 1;
            OnChartValueSelected?.Invoke(xSelectedIndex);
            SetPriceAreaToolTips(e.Location);
        }


        private void PVChart_MouseWheel(object sender, MouseEventArgs e)
        {
            Axis ax = PVChart.ChartAreas["areaPrices"].AxisX;
            Tuple<double, double> zoom = (new ChartZoomCalculator()).CalculateZoom(e.Delta < 0,
                ModifierKeys.HasFlag(Keys.Control), ax, e.Location);
            using (new SuspendDrawingUpdate(PVChart))
            {
                ax.ScaleView.Zoom(zoom.Item1, zoom.Item2);
                SetYViewRange();
            }
            PVChart_MouseMove(sender, e);
            SetPriceAreaToolTips(e.Location);
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

        private void PVChart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (e.Axis != PVChart.ChartAreas["areaPrices"].AxisX) return;
            using (new SuspendDrawingUpdate(PVChart))
                SetYViewRange();
        }
        #endregion

        public void ClearAllSeriesData()
        {
            foreach (var series in PVChart.Series)
                series.Points.Clear();
        }

        public Series GetSeries(string seriesName)
        {
            return PVChart.Series[seriesName];
        }

        public void AddPriceAreaStatSeries(StockStat stat)
        {
            StockStatSeriesCreator factory = new StockStatSeriesCreator();
            for (int i = 0; i < stat.DataCount; i++)
                PVChart.Series.Add(factory.CreatePriceAreaSeries(stat, i));
        }

        public void SetChartMode(PriceVolumeChartMode newMode)
        {
            ChartMode = newMode;
            PricesCandles.Enabled = (newMode == PriceVolumeChartMode.Candles);
            PricesLine.Enabled = (newMode == PriceVolumeChartMode.Lines);
            PricesLine.XValueType = ChartValueType.Date;
        }

        public void ResetZoom()
        {
            PVChart.ChartAreas["areaPrices"].AxisX.ScaleView.ZoomReset();
            SetYViewRange();
        }

        public void SetYViewRange()
        {
            Axis ax = PVChart.ChartAreas["areaPrices"].AxisX;
            Axis ay = PVChart.ChartAreas["areaPrices"].AxisY;

            ChartYViewRangeCalculator calc = new ChartYViewRangeCalculator();
            Tuple<double, double> range = calc.CalculateRangeCandles(ax, PricesCandles.Points, calc.InitialRange());
            var list = PVChart.Series.Where(x => (x.ChartArea == "areaPrices") && (x.Name != "dataPricesCandles") && (x.Name != "dataPricesLines")).ToList();
            foreach (var s in list)
                range = calc.CalculateRangeLine(ax, s.Points, range);
            range = calc.PostprocessRange(range);

            ay.Minimum = range.Item1;
            ay.Maximum = range.Item2;
        }

        public void ReversePricesYAxis(bool reversed)
        {
            PVChart.ChartAreas["areaPrices"].AxisY.IsReversed = reversed;
        }

        private void SetPriceAreaToolTips(Point cursorLocation)
        {
            int xSelectedIndex = (int)PVChart.ChartAreas["areaPrices"].CursorX.Position - 1;

            Axis ay = PVChart.ChartAreas["areaPrices"].AxisY;
            if (cursorLocation.Y >= 0)
            {
                double yval = ay.PixelPositionToValue(cursorLocation.Y);
                tooltipAxisY.ShowIfPosChanged(OnGetAxisYToolTip?.Invoke(yval), PVChart, 0, cursorLocation.Y - 10);
            }
            if (xSelectedIndex >= 0)
            {
                int xAxisRoundedPosition = (int)PVChart.ChartAreas["areaPrices"].AxisX.ValueToPixelPosition(PVChart.ChartAreas["areaPrices"].CursorX.Position);
                tooltipAxisX.ShowIfPosChanged(OnGetAxisXToolTip?.Invoke(xSelectedIndex), PVChart, xAxisRoundedPosition, (int)ay.ValueToPixelPosition(ay.ScaleView.ViewMinimum) + 2);
            }
        }
    }
}
