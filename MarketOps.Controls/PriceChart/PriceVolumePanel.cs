using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MarketOps.Controls.Extensions;
using MarketOps.Controls.Types;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.Config.Stats;

namespace MarketOps.Controls.ChartsUtils
{
    public partial class PriceVolumePanel : UserControl
    {
        public PriceVolumePanel()
        {
            InitializeComponent();
            btnPriceChartCandle.Checked = true;
            lblStockInfo.Text = "";
            lblSelectedInfo.Text = "";
            lblStatSelectedInfo.Text = "";
            chartPV.OnChartValueSelected += OnChartValueSelected;
            chartPV.OnGetAxisXToolTip += OnGetAxisXToolTip;
            chartPV.OnGetAxisYToolTip += OnGetAxisYToolTip;
            PrepareStatsContextMenuItems();

            _stickerPositioner = new StockStatStickersPositioner(chartPV);
        }

        #region internal data
        private StockDisplayData _currentData;
        private IStockInfoGenerator _currentInfoGenerator;
        private IStockStatsInfoGenerator _currentStatsInfoGenerator;

        private readonly StockStatStickersPositioner _stickerPositioner;
        #endregion

        #region public properties and events
        public PriceVolumeChart Chart => chartPV;
        public StockDisplayData CurrentData => _currentData;

        public delegate StockPricesData GetAdditionalData(StockDisplayData currentData);
        public event GetAdditionalData OnPrependData;

        public delegate StockPricesData GetData(StockDisplayData currentData, DateTime tsFrom, DateTime tsTo);
        public event GetData OnGetData;

        public delegate void RecalculateStockStats(StockDisplayData currentData);
        public event RecalculateStockStats OnRecalculateStockStats;
        #endregion

        #region button actions
        private void btnPriceChartLine_CheckedChanged(object sender, EventArgs e)
        {
            if (!btnPriceChartLine.Checked) return;
            btnPriceChartCandle.Checked = false;
            chartPV.SetChartMode(PriceVolumeChartMode.Lines);
        }

        private void btnPriceChartCandle_CheckedChanged(object sender, EventArgs e)
        {
            if (!btnPriceChartCandle.Checked) return;
            btnPriceChartLine.Checked = false;
            chartPV.SetChartMode(PriceVolumeChartMode.Candles);
        }

        private void btnPrependData_Click(object sender, EventArgs e)
        {
            if (OnPrependData == null) return;
            StockPricesData newData = OnPrependData.Invoke(_currentData);
            _currentData.Prices = _currentData.Prices.Merge(newData);
            RecalculateStats();
            PrependData(newData);
            chartPV.ResetZoom();
            DisplayCurrentStockInfo();
        }

        private void btnDataRange_Click(object sender, EventArgs e)
        {
            if (OnGetData == null) return;
            FormSelectDataRange frm = new FormSelectDataRange();
            if (!frm.Execute(_currentData.TsFrom, _currentData.TsTo, _currentData.Prices.DataRangeDateTimeInputFormat())) return;
            _currentData.Prices = OnGetData.Invoke(_currentData, frm.TsFrom, frm.TsTo);
            RecalculateStats();
            ReloadCurrentData();
            chartPV.ResetZoom();
            DisplayCurrentStockInfo();
        }

        private void btnMirrorChart_CheckedChanged(object sender, EventArgs e)
        {
            chartPV.ReversePricesYAxis(btnMirrorChart.Checked);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        #endregion

        #region form events
        private void PriceVolumePanel_Resize(object sender, EventArgs e)
        {
            _stickerPositioner.RepositionStickers();
        }
        #endregion

        private void OnChartValueSelected(int selectedIndex)
        {
            if (_currentData == null) return;
            if ((selectedIndex >= 0) && (selectedIndex < _currentData.Prices.Length))
            {
                lblSelectedInfo.Text = _currentInfoGenerator.GetStockSelectedInfo(_currentData, selectedIndex);
                lblStatSelectedInfo.Text = _currentStatsInfoGenerator.GetStatsSelectedInfo(_currentData, selectedIndex);
            }
        }

        private string OnGetAxisXToolTip(int selectedIndex)
        {
            if (_currentData == null) return "";
            if ((selectedIndex < 0) || (selectedIndex >= _currentData.Prices.Length)) return "";
            return _currentInfoGenerator.GetAxisXToolTip(_currentData, selectedIndex);
        }

        private string OnGetAxisYToolTip(double selectedValue)
        {
            if (_currentData == null) return "";
            return _currentInfoGenerator.GetAxisYToolTip(_currentData, selectedValue);
        }

        private void RecalculateStats()
        {
            OnRecalculateStockStats?.Invoke(_currentData);
        }

        private void ReloadCurrentData()
        {
            using (new SuspendDrawingUpdate(chartPV))
            {
                chartPV.LoadStockData(_currentData.Prices);
                foreach (var stat in _currentData.Stats)
                    chartPV.AppendStockStatData(_currentData.Prices, stat);
                chartPV.SetYViewRange();
            }
        }

        private void PrependData(StockPricesData newData)
        {
            using (new SuspendDrawingUpdate(chartPV))
            {
                chartPV.PrependStockData(newData);
                foreach (var stat in _currentData.Stats)
                    chartPV.PrependStockStatData(_currentData.Prices, stat);
            }
        }

        private void DisplayCurrentStockInfo()
        {
            lblStockInfo.Text = _currentInfoGenerator.GetStockInfo(_currentData);
        }

        public void LoadData(StockDisplayData data, IStockInfoGenerator infoGenerator, IStockStatsInfoGenerator statsInfoGenerator)
        {
            _currentData = data;
            _currentInfoGenerator = infoGenerator;
            _currentStatsInfoGenerator = statsInfoGenerator;
            ReloadCurrentData();
            chartPV.ResetZoom();
            DisplayCurrentStockInfo();
        }

        public void RefreshData()
        {
            ReloadCurrentData();
        }

        public void AddStat(StockStat stat)
        {
            chartPV.AddStatSeries(stat);
            _currentData.Stats.Add(stat);
            StockStatSticker sticker = new StockStatSticker(stat, _currentStatsInfoGenerator);
            sticker.OnStickerDoubleClick += OnStatStickerDoubleClick;
            sticker.OnStickerMouseClick += OnStatStickerClick;
            _stickerPositioner.Add(sticker);
        }

        public void RemoveStat(StockStatSticker sticker, StockStat stat)
        {
            chartPV.RemoveStatSeries(stat);
            _currentData.Stats.Remove(stat);
            _stickerPositioner.Remove(sticker);
        }

        private void PrepareStatsContextMenuItems()
        {
            PrepareSingleStatContextMenuItems(miStatsOnPriceChart, StatsFactories.PriceChart.GetList(), OnClickPriceChartStat);
            PrepareSingleStatContextMenuItems(miAdditionalStats, StatsFactories.Additional.GetList(), OnClickAdditionalStat);
        }

        private void PrepareSingleStatContextMenuItems(ToolStripMenuItem menu, List<string> statsNames, EventHandler onClick)
        {
            ((ToolStripDropDownMenu) menu.DropDown).ShowImageMargin = false;
            foreach (string statName in statsNames)
            {
                ToolStripItem item = menu.DropDownItems.Add(statName);
                item.Click += onClick;
            }
        }

        private void OnClickPriceChartStat(object sender, EventArgs e)
        {
            StockStat stat = StatsFactories.PriceChart.Get(sender.ToString(), "areaPrices");
            if (!EditStat(stat)) return;
            AddStat(stat);
            CalculateStatAndRefreshChartData(stat);
        }

        private void OnClickAdditionalStat(object sender, EventArgs e)
        {
            string newAreaName = $"Area{Guid.NewGuid().ToString("N")}";

            StockStat stat = StatsFactories.Additional.Get(sender.ToString(), newAreaName);
            if (!EditStat(stat)) return;
            chartPV.CreateNewArea(newAreaName);
            AddStat(stat);
            CalculateStatAndRefreshChartData(stat);
        }

        private void OnStatStickerDoubleClick(StockStatSticker sticker, StockStat stat)
        {
            if (!EditStat(stat)) return;
            sticker.UpdateStatInfo();
            chartPV.UpdateStatSeriesDefinition(stat);
            CalculateStatAndRefreshChartData(stat);
        }

        private void OnStatStickerClick(StockStatSticker sticker, StockStat stat, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (stat.ChartArea != "areaPrices")
                    chartPV.RemoveArea(stat.ChartArea);
                RemoveStat(sticker, stat);
                Refresh();
            }
        }

        private bool EditStat(StockStat stat)
        {
            using (FormEditStockStatParams frm = new FormEditStockStatParams())
                if (!frm.Execute(stat)) return false;
            return true;
        }

        private void CalculateStatAndRefreshChartData(StockStat stat)
        {
            stat.Calculate(CurrentData.Prices);
            RefreshData();
            Refresh();
        }
    }
}
