using MarketOps.Controls.ChartsUtils.AxisSynchronization;
using MarketOps.Controls.PriceChart.DateTimeTicks;
using MarketOps.Controls.PriceChart.PVChart.Managers;
using MarketOps.StockData.Types;
using ScottPlot.Plottable;
using ScottPlot;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using MarketOps.Stats.Calculators;

namespace MarketOps.Controls.PriceChart.PVChart
{
    public delegate void PriceVolumeChartValueSelected(int selectedIndex);
    public delegate string PriceVolumeChartGetAxisXToolTip(int selectedIndex);
    public delegate string PriceVolumeChartGetAxisYToolTip(double selectedValue);
    //public delegate void PriceVolumeChartAreaDoubleClick(string areaName);

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
        private FinancePlot _haPlot;
        private ScatterPlot _closePlot;

        public PriceVolumeChartMode ChartMode { get; private set; }
        public int AdditionalChartsCount => _additionalChartsManager.Charts.Count;

        public event PriceVolumeChartValueSelected OnChartValueSelected;
        public event PriceVolumeChartGetAxisXToolTip OnGetAxisXToolTip;
        public event PriceVolumeChartGetAxisYToolTip OnGetAxisYToolTip;
        //public event PriceVolumeChartAreaDoubleClick OnAreaDoubleClick;

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
            _crosshairManager.OnCrosshairVisibilityChanged += OnCrosshairVisibilityChanged;
            _crosshairManager.OnVerticalPositionTooltip += OnCrosshairPositionTooltip;
            _crosshairManager.OnVerticalValueChanged += OnCrosshairVerticalValueChanged;

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
                var haData = HeikinAshi.Calculate(data);
                _haPlot = chartPrices.Plot.AddCandlesticks(haData.MapToOHLCData());
                _haPlot.SetUpPriceHeikinAshiPlot();
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

        public void RefreshAllCharts()
        {
            chartPrices.Refresh();
            chartVolume.Refresh();
            _additionalChartsManager.RefreshAll();
        }

        public void ReversePricesYAxis(bool reversed)
        {
            //PVChart.ChartAreas[PlotConsts.PricesAreaName].AxisY.IsReversed = reversed;
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
            ClearTrailingStopsData();
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

        private void OnCrosshairVisibilityChanged(FormsPlot chart) => 
            chart.Refresh();

        private string OnCrosshairPositionTooltip(double value) => 
            (value >= 0) && (OnGetAxisXToolTip != null)
                ? OnGetAxisXToolTip.Invoke((int)Math.Round(value))
                : string.Empty;

        private void OnCrosshairVerticalValueChanged(int selectedIndex) => 
            OnChartValueSelected?.Invoke(selectedIndex);
    }
}
