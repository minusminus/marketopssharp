using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MarketOps.Controls.ChartsUtils.AxisSynchronization;
using MarketOps.Controls.PriceChart.DateTimeTicks;
using MarketOps.StockData.Types;
using ScottPlot;

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
        private IDateTimeTicksProvider _datetimeTicksProvider;
        private StockPricesData _currentData;

        private double[] _statsXs;

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
            SetChartMode(PriceVolumeChartMode.Candles);
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

            chartPrices.SetUpFormsPlot();
            chartPrices.Plot.XAxis.DateTimeFormat(true);
            chartPrices.AxesChanged += OnAxesChangedPricesTicksProvider;

            chartVolume.SetUpAdditionalFormsPlot();
        }

        public void LoadData(StockPricesData data, IReadOnlyList<StockStat> stats)
        {
            ClearAllCharts();
            _stockStatsManager.Clear();
            _datetimeTicksProvider = DateTimeTicksProviderFactory.Get(data.Range);
            _currentData = data;

            chartPrices.Plot
                .AddCandlesticks(data.MapToOHLCData())
                .SetUpPricesPlot();
            chartVolume.Plot
                .AddBar(data.MapToVolumeData())
                .SetUpVolumePlot();
            _statsXs = DataGen.Consecutive(_currentData.Length);

            foreach (var stat in stats)
                AddStockStat(stat);

            RefreshAllCharts();
        }

        private void ClearAllCharts()
        {
            chartPrices.Plot.Clear();
            chartVolume.Plot.Clear();
            _additionalChartsManager.Clear();
            _priceStatsManager.Clear();
            _stockStatsManager.Clear();
        }

        private void RefreshAllCharts()
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

        public void SetChartMode(PriceVolumeChartMode newMode)
        {
            ChartMode = newMode;
            //PricesCandles.Enabled = (newMode == PriceVolumeChartMode.Candles);
            //PricesLine.Enabled = (newMode == PriceVolumeChartMode.Lines);
            //PricesLine.XValueType = ChartValueType.Date;
        }

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
