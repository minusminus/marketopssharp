using MarketOps.Controls.ChartsUtils.AxisSynchronization;
using MarketOps.Controls.PriceChart.DateTimeTicks;
using MarketOps.StockData.Types;
using ScottPlot;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using ScottPlot.Plottable;

namespace MarketOps.Controls.PriceChart.PVChart
{
    public delegate void PriceVolumeChartValueSelected(int selectedIndex);
    public delegate string PriceVolumeChartGetAxisXToolTip(int selectedIndex);
    public delegate string PriceVolumeChartGetAxisYToolTip(double selectedValue);
    public delegate void PriceVolumeChartAreaDoubleClick(string areaName);

    public partial class PriceVolumeChart : UserControl
    {
        private readonly PlotsAxisXSynchronizer _axisSynchronizer;
        private readonly StockStatsManager _stockStatsManager;
        private readonly AdditionalChartsManager _additionalChartsManager;
        private readonly PriceChartStatsManager _priceStatsManager;
        private readonly CrosshairManager _crosshairManager;
        private IDateTimeTicksProvider _datetimeTicksProvider;
        private StockPricesData _currentData;

        private double[] _statsXs;
        private FinancePlot _ohlcPlot;
        private ScatterPlot _closePlot;

        #region public properties and events
        public PriceVolumeChartMode ChartMode { get; private set; }
        public int AdditionalChartsCount => _additionalChartsManager.Charts.Count;

        //public Chart PVChartControl => PVChart;
        //public Series PricesCandles => PVChart.Series["dataPricesCandles"];
        //public Series PricesLine => PVChart.Series["dataPricesLine"];
        //public Series TrailingStopL => PVChart.Series["dataTrailingStopL"];

        public event PriceVolumeChartValueSelected OnChartValueSelected;
        public event PriceVolumeChartGetAxisXToolTip OnGetAxisXToolTip;
        public event PriceVolumeChartGetAxisYToolTip OnGetAxisYToolTip;
        public event PriceVolumeChartAreaDoubleClick OnAreaDoubleClick;
        #endregion

        public PriceVolumeChart()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ChartMode = PriceVolumeChartMode.Candles;
            //pnlCursorDataValues.BackColor = chartPrices.BackColor;
            _axisSynchronizer = new PlotsAxisXSynchronizer(chartPrices, chartVolume);

            _stockStatsManager = new StockStatsManager();
            _stockStatsManager.OnPriceStatAdded += OnPriceStatAdded;
            _stockStatsManager.OnAdditionalStatAdded += OnAdditionalStatAdded;
            _stockStatsManager.OnPriceStatUpdated += OnPriceStatUpdated;
            _stockStatsManager.OnAdditionalStatUpdated += OnAdditionalStatUpdated;
            _stockStatsManager.OnPriceStatRemoved += OnPriceStatRemoved;
            _stockStatsManager.OnAdditionalStatRemoved += OnAdditionalStatRemoved;

            _additionalChartsManager = new AdditionalChartsManager(_axisSynchronizer);
            _priceStatsManager = new PriceChartStatsManager();
            _crosshairManager = new CrosshairManager();

            chartPrices.SetUpFormsPlot();
            chartPrices.Plot.XAxis.DateTimeFormat(true);
            chartPrices.AxesChanged += OnAxesChangedPricesTicksProvider;

            chartVolume.SetUpAdditionalFormsPlot();
        }

        public void LoadData(StockPricesData data, IReadOnlyList<StockStat> stats, bool refreshCharts = true)
        {
            _axisSynchronizer.Enabled = false;
            try
            {
                ClearAllCharts();
                _stockStatsManager.Clear();
                _datetimeTicksProvider = DateTimeTicksProviderFactory.Get(data.Range);
                _currentData = data;

                _statsXs = DataGen.Consecutive(_currentData.Length);
                _ohlcPlot = chartPrices.Plot.AddCandlesticks(data.MapToOHLCData());
                _ohlcPlot.SetUpPriceCandlesPlot();
                _closePlot = chartPrices.Plot.AddScatterLines(_statsXs, data.MapToCloseData());
                _closePlot.SetUpPriceClosePlot();
                SetChartMode(ChartMode, false);
                _crosshairManager.Add(chartPrices, true);

                chartVolume.Plot
                    .AddBar(data.MapToVolumeData())
                    .SetUpVolumePlot();
                _crosshairManager.Add(chartVolume, false);

                foreach (var stat in stats)
                    AddStockStat(stat);

                if (refreshCharts)
                    RefreshAllCharts();
                ResizePriceChartToWorkaroundContentDrawingProblem();
            }
            finally
            {
                _axisSynchronizer.Enabled = true;
            }
        }

        public void SetChartMode(PriceVolumeChartMode newMode, bool refreshChart = true)
        {
            ChartMode = newMode;
            if (_ohlcPlot != null)
                _ohlcPlot.IsVisible = (ChartMode == PriceVolumeChartMode.Candles);
            if (_closePlot != null)
                _closePlot.IsVisible = (ChartMode == PriceVolumeChartMode.Lines);
            if (refreshChart)
                chartPrices.Refresh();
        }

        private void ResizePriceChartToWorkaroundContentDrawingProblem()
        {
            using (var suspender = new SuspendDrawingUpdate(this))
            {
                chartPrices.Dock = DockStyle.None;
                chartPrices.Dock = DockStyle.Fill;
            }
            chartPrices.Invalidate();
        }

        private void ClearAllCharts()
        {
            chartPrices.Plot.Clear();
            chartVolume.Plot.Clear();
            _crosshairManager.Clear();
            _additionalChartsManager.Clear();
            _priceStatsManager.Clear();
            _stockStatsManager.Clear();
        }

        public void RefreshAllCharts()
        {
            chartPrices.Refresh();
            chartVolume.Refresh();
            _additionalChartsManager.RefreshAll();
        }

        private void OnAxesChangedPricesTicksProvider(object sender, EventArgs e)
        {
            if (_currentData == null) return;
            if ((FormsPlot)sender != chartPrices) return;

            var ticks = _datetimeTicksProvider.Get(_currentData.TS, chartPrices.Plot.GetAxisLimits());
            chartPrices.Configuration.AxesChangedEventEnabled = false;
            try
            {
                chartPrices.Plot.XTicks(ticks.positions, ticks.values);
                chartPrices.Refresh();
            } finally
            {
                chartPrices.Configuration.AxesChangedEventEnabled = true;
            }
        }

        /* ============================== cut here ============================================== */

        #region internal events
        private void PVChart_MouseMove(object sender, MouseEventArgs e)
        {
            //if (PricesCandles.Points.Count == 0) return;

            //UpdateAreasCursors(e.Location);
            //int xSelectedIndex = (int)PVChart.ChartAreas[PlotConsts.PriceAreaName].CursorX.Position - 1;
            //OnChartValueSelected?.Invoke(xSelectedIndex);
            //SetPriceAreaToolTips(e.Location);
        }

        private void PVChart_MouseEnter(object sender, EventArgs e)
        {
            //if (!PVChart.Focused)
            //    PVChart.Focus();
        }

        private void PVChart_MouseLeave(object sender, EventArgs e)
        {
            //if (PVChart.Focused)
            //    PVChart.Parent.Focus();
        }

        private void PVChart_DoubleClick(object sender, EventArgs e)
        {
            //ChartArea currentArea = FindAreaUnderCursor(PVChart.PointToClient(Control.MousePosition));
            //if (currentArea == null) return;
            //OnAreaDoubleClick?.Invoke(currentArea.Name);
        }
        #endregion

        public void ReversePricesYAxis(bool reversed)
        {
            //PVChart.ChartAreas[PlotConsts.PricesAreaName].AxisY.IsReversed = reversed;
        }

        public void HidePriceAreaToolTips()
        {
            lblTSValue.Text = "";
            lblValueValue.Text = "";
        }
    }
}
