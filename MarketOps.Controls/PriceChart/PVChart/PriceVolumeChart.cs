using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MarketOps.Controls.ChartsUtils;
using MarketOps.Controls.ChartsUtils.AxisSynchronization;
using MarketOps.Controls.PriceChart.DateTimeTicks;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using ScottPlot;

namespace MarketOps.Controls.PriceChart.PVChart
{
    public partial class PriceVolumeChart : UserControl
    {
        private readonly PlotsAxisXSynchronizer _axisSynchronizer;
        private readonly StockStatsManager _stockStatsManager;
        private readonly AdditionalChartsManager _additionalChartsManager;
        private IDateTimeTicksProvider _datetimeTicksProvider;
        private StockPricesData _currentData;

        public PriceVolumeChart()
        {
            InitializeComponent();
            SetChartMode(PriceVolumeChartMode.Candles);
            //pnlCursorDataValues.BackColor = chartPrices.BackColor;
            _axisSynchronizer = new PlotsAxisXSynchronizer(chartPrices, chartVolume);
            _stockStatsManager = new StockStatsManager();
            _stockStatsManager.OnPriceStatAdded += OnPriceStatAdded;
            _stockStatsManager.OnAdditionalStatAdded += OnAdditionalStatAdded;
            _stockStatsManager.OnPriceStatRemoved += OnPriceStatRemoved;
            _stockStatsManager.OnAdditionalStatRemoved += OnAdditionalStatRemoved;
            _additionalChartsManager = new AdditionalChartsManager(_axisSynchronizer);

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

            foreach (var stat in stats)
                AddStockStat(stat);

            RefreshAllCharts();
        }

        public void AddStockStat(StockStat stat)
        {
            _stockStatsManager.Add(stat);
        }

        private void ClearAllCharts()
        {
            chartPrices.Plot.Clear();
            chartVolume.Plot.Clear();
            _additionalChartsManager.Clear();
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

        private void OnPriceStatAdded(StockStat stat)
        {
            //dodanie nowych plotow na wykresie ceny
        }

        private void OnAdditionalStatAdded(StockStat stat)
        {
            //dodanie nowego FormsPlot do _additionalCharts i narysowanie na nim wykresow StockStat
            FormsPlot chart = _additionalChartsManager.Add();
        }

        private void OnPriceStatRemoved(StockStat stat, int index)
        {
            //usuniecie plotow na wykresie ceny
        }

        private void OnAdditionalStatRemoved(StockStat stat, int index)
        {
            _additionalChartsManager.Remove(index);
        }

        #region public properties and events
        public PriceVolumeChartMode ChartMode { get; private set; }

        public Chart PVChartControl => PVChart;
        public ChartAreaCollection ChartAreas => PVChart.ChartAreas;
        public Series PricesCandles => PVChart.Series["dataPricesCandles"];
        public Series PricesLine => PVChart.Series["dataPricesLine"];
        public Series TrailingStopL => PVChart.Series["dataTrailingStopL"];
        public Series Volume => PVChart.Series["dataVolume"];

        public delegate void ChartValueSelected(int selectedIndex);
        public event ChartValueSelected OnChartValueSelected;

        public delegate string GetAxisXToolTip(int selectedIndex);
        public event GetAxisXToolTip OnGetAxisXToolTip;
        public delegate string GetAxisYToolTip(double selectedValue);
        public event GetAxisYToolTip OnGetAxisYToolTip;

        public delegate void AreaDoubleClick(string areaName);
        public event AreaDoubleClick OnAreaDoubleClick;
        #endregion

        #region internal events
        private void PVChart_MouseMove(object sender, MouseEventArgs e)
        {
            //if (PricesCandles.Points.Count == 0) return;

            //UpdateAreasCursors(e.Location);
            //int xSelectedIndex = (int)PVChart.ChartAreas[PlotConsts.PriceAreaName].CursorX.Position - 1;
            //OnChartValueSelected?.Invoke(xSelectedIndex);
            //SetPriceAreaToolTips(e.Location);
        }

        private void PVChart_MouseWheel(object sender, MouseEventArgs e)
        {
            //Axis ax = PVChart.ChartAreas[PlotConsts.PriceAreaName].AxisX;
            //Tuple<double, double> zoom = (new ChartZoomCalculator()).CalculateZoom(e.Delta < 0,
            //    ModifierKeys.HasFlag(Keys.Control), ax, e.Location);
            //using (new SuspendDrawingUpdate(PVChart))
            //{
            //    ax.ScaleView.Zoom(zoom.Item1, zoom.Item2);
            //    SetYViewRange();
            //}
            //PVChart_MouseMove(sender, e);
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

        private void PVChart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            //if (!Equals(e.Axis, PVChart.ChartAreas[PlotConsts.PriceAreaName].AxisX)) return;
            //using (new SuspendDrawingUpdate(PVChart))
            //    SetYViewRange();
        }

        private void PVChart_DoubleClick(object sender, EventArgs e)
        {
            //ChartArea currentArea = FindAreaUnderCursor(PVChart.PointToClient(Control.MousePosition));
            //if (currentArea == null) return;
            //OnAreaDoubleClick?.Invoke(currentArea.Name);
        }
        #endregion

        public Series GetSeries(string seriesName)
        {
            return PVChart.Series[seriesName];
        }

        public void AddStatSeries(StockStat stat)
        {
            StockStatSeriesFactory factory = new StockStatSeriesFactory();
            for (int i = 0; i < stat.DataCount; i++)
                PVChart.Series.Add(factory.CreateSeries(stat, i));
        }

        public void RemoveStatSeries(StockStat stat)
        {
            for (int i = 0; i < stat.DataCount; i++)
                PVChart.Series.Remove(PVChart.Series.FindByName(stat.ChartSeriesName(i)));
        }

        public void SetChartMode(PriceVolumeChartMode newMode)
        {
            ChartMode = newMode;
            PricesCandles.Enabled = (newMode == PriceVolumeChartMode.Candles);
            PricesLine.Enabled = (newMode == PriceVolumeChartMode.Lines);
            PricesLine.XValueType = ChartValueType.Date;
        }

        public void ReversePricesYAxis(bool reversed)
        {
            PVChart.ChartAreas[PlotConsts.PricesAreaName].AxisY.IsReversed = reversed;
        }

        public void HidePriceAreaToolTips()
        {
            lblTSValue.Text = "";
            lblValueValue.Text = "";
        }

        public void CreateNewArea(string areaName)
        {
            PVChart.ChartAreas.Add((new ChartAreaFactory()).CreateArea(areaName, 20F));
            ResizeAreas(20F);
        }

        public void RemoveArea(string areaName)
        {
            PVChart.ChartAreas.RemoveAt(PVChart.ChartAreas.IndexOf(areaName));
            ResizeAreas(20F);
        }

        private void ResizeAreas(float positionModifier)
        {
            ChartArea areaPrices = PVChart.ChartAreas[PlotConsts.PricesAreaName];
            areaPrices.Position.Height = 80F - ((PVChart.ChartAreas.Count - 2) * positionModifier);
            for (int i = 0; i < PVChart.ChartAreas.Count; i++)
            {
                ChartArea area = PVChart.ChartAreas[i];
                if (area.Name == PlotConsts.PricesAreaName) continue;
                area.Position.Y = areaPrices.Position.Height + ((i - 1) * positionModifier);
            }
        }
    }
}
