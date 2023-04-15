﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MarketOps.Controls.Extensions;
using MarketOps.Controls.Types;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using MarketOps.Config.Stats;
using MarketOps.SystemData.Types;
using MarketOps.StockData;
using MarketOps.Controls.ChartsUtils;
using MarketOps.Controls.PriceChart.PVChart;

namespace MarketOps.Controls.PriceChart
{
    public delegate StockPricesData GetAdditionalData(StockDisplayData currentData);
    public delegate StockPricesData GetData(StockDisplayData currentData, DateTime tsFrom, DateTime tsTo);
    public delegate void RecalculateStockStats(StockDisplayData currentData);

    public partial class PriceVolumePanel : UserControl
    {
        #region internal data
        private StockDisplayData _currentData;
        private IStockInfoGenerator _currentInfoGenerator;
        private IStockStatsInfoGenerator _currentStatsInfoGenerator;

        private readonly StockStatStickersPositioner _stickerPositioner;
        #endregion

        #region public properties and events
        public PriceVolumeChart Chart => chartPV;
        public StockDisplayData CurrentData => _currentData;

        public event GetAdditionalData OnPrependData;
        public event GetData OnGetData;
        public event RecalculateStockStats OnRecalculateStockStats;
        #endregion

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
            ReloadCurrentData();
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
            DisplayCurrentStockInfo();
        }

        private void btnMirrorChart_CheckedChanged(object sender, EventArgs e)
        {
            chartPV.ReversePricesYAxis(btnMirrorChart.Checked);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadCurrentData();
        }
        #endregion

        #region form events
        private void PriceVolumePanel_Resize(object sender, EventArgs e)
        {
            RepositionStatStickers();
        }
        #endregion

        private void OnChartValueSelected(int selectedIndex)
        {
            if ((_currentData == null) || (selectedIndex < 0) || (selectedIndex >= _currentData.Prices.Length)) return;
            lblSelectedInfo.Text = _currentInfoGenerator.GetStockSelectedInfo(_currentData, selectedIndex);
            lblStatSelectedInfo.Text = string.Join(", ", DataFormatting.SkipEmptyStrings(
                _currentStatsInfoGenerator.GetStatsSelectedInfo(_currentData, selectedIndex),
                GetTrailingStopInfo(selectedIndex)));
        }

        private string GetTrailingStopInfo(int selectedIndex)
        {
            //if ((!chartPV.TrailingStopL.Enabled) || chartPV.TrailingStopL.Points[selectedIndex].IsEmpty) return string.Empty;
            //return $"Trailing Stop: {DataFormatting.FormatPrice(_currentData.Stock.Type, chartPV.TrailingStopL.Points[selectedIndex].YValues[0])}";
            return "Trailing Stop: info to do";
        }

        private string OnGetAxisXToolTip(int selectedIndex)
        {
            if ((_currentData == null) || (selectedIndex < 0) || (selectedIndex >= _currentData.Prices.Length)) return "";
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
            UnlinkStatStickers();
            chartPV.LoadData(_currentData.Prices, _currentData.Stats);
            RepositionStatStickers();
            RecreatePositionsAnnotations();
            //using (new SuspendDrawingUpdate(chartPV))
            //{
                //chartPV.LoadStockData(_currentData.Prices);
                //foreach (var stat in _currentData.Stats)
                //    chartPV.AppendStockStatData(_currentData.Prices, stat);
                //chartPV.SetYViewRange();
                //RecreatePositionsAnnotations();
                //RecreatePositionsTrailingStops();
            //}
        }

        private void DisplayCurrentStockInfo()
        {
            lblStockInfo.Text = _currentInfoGenerator.GetStockInfo(_currentData);
        }

        private void RepositionStatStickers() => 
            _stickerPositioner.RepositionStickers();

        private void UnlinkStatStickers() =>
            _stickerPositioner.UnlinkStickers();

        public void LoadData(StockDisplayData data, IStockInfoGenerator infoGenerator, IStockStatsInfoGenerator statsInfoGenerator)
        {
            _currentData = data;
            _currentInfoGenerator = infoGenerator;
            _currentStatsInfoGenerator = statsInfoGenerator;
            ReloadCurrentData();
            DisplayCurrentStockInfo();
        }

        #region positions
        public void AddPositions(List<Position> positions)
        {
            _currentData.Positions.AddRange(positions);
            RecreatePositionsAnnotations();
            RecreatePositionsTrailingStops();
        }

        private void RecreatePositionsAnnotations()
        {
            chartPV.AddPositionsAnnotations(_currentData.Positions);
        }

        private void RecreatePositionsTrailingStops()
        {
            chartPV.AddPositionsTrailingStops(_currentData.Positions);
        }
        #endregion

        #region stats
        private void AddStat(StockStat stat)
        {
            chartPV.AddStockStat(stat);
            _currentData.Stats.Add(stat);
            StockStatSticker sticker = new StockStatSticker(stat, _currentStatsInfoGenerator);
            sticker.OnStickerDoubleClick += OnStatStickerDoubleClick;
            sticker.OnStickerMouseClick += OnStatStickerClick;
            _stickerPositioner.Add(sticker);
        }

        private void UpdateStat(StockStat stat, StockStatSticker sticker)
        {
            sticker.UpdateStatInfo();
            chartPV.UpdateStockStat(stat);
        }

        private void RemoveStat(StockStat stat, StockStatSticker sticker)
        {
            chartPV.RemoveStockStat(stat);
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
            StockStat stat = StatsFactories.PriceChart.Get(sender.ToString(), PlotConsts.PricesAreaName);
            if (!EditStat(stat)) return;
            CalculateStat(stat);
            AddStat(stat);
        }

        private void OnClickAdditionalStat(object sender, EventArgs e)
        {
            string newAreaName = $"Area{Guid.NewGuid():N}";

            StockStat stat = StatsFactories.Additional.Get(sender.ToString(), newAreaName);
            if (!EditStat(stat)) return;
            CalculateStat(stat);
            AddStat(stat);
        }

        private void OnStatStickerDoubleClick(StockStatSticker sticker, StockStat stat)
        {
            if (!EditStat(stat)) return;
            CalculateStat(stat);
            UpdateStat(stat, sticker);
        }

        private void OnStatStickerClick(StockStatSticker sticker, StockStat stat, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                RemoveStat(stat, sticker);
                Refresh();
            }
        }

        private bool EditStat(StockStat stat)
        {
            using (FormEditStockStatParams frm = new FormEditStockStatParams())
                return frm.Execute(stat);
        }

        private void CalculateStat(StockStat stat) => 
            stat.Calculate(CurrentData.Prices);
        #endregion
    }
}
