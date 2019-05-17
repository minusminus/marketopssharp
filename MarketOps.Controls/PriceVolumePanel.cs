using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketOps.Controls.Extensions;
using MarketOps.Controls.Types;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.Controls
{
    public partial class PriceVolumePanel : UserControl
    {
        public PriceVolumePanel()
        {
            InitializeComponent();
            btnPriceChartCandle.Checked = true;
            lblStockInfo.Text = "";
            lblSelectedInfo.Text = "";
            chartPV.OnChartValueSelected += OnChartValueSelected;
        }

        #region internal data
        private StockDisplayData _currentData;
        private IStockInfoGenerator _currentInfoGenerator;
        #endregion

        #region public properties and events
        public PriceVolumeChart Chart => chartPV;

        public delegate StockPricesData GetAdditionalData(StockDisplayData currentData);
        public event GetAdditionalData OnPrependData;
        //public event GetAdditionalData OnAppendData;
        public delegate StockPricesData GetData(StockDisplayData currentData, DateTime tsFrom, DateTime tsTo);
        public event GetData OnGetData;
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
            using (new SuspendDrawingUpdate(chartPV))
                chartPV.PrependStockData(newData);
            chartPV.ResetZoom();
            lblStockInfo.Text = _currentInfoGenerator.GetStockInfo(_currentData);
        }

        private void btnDataRange_Click(object sender, EventArgs e)
        {
            if (OnGetData == null) return;
            FormSelectDataRange frm = new FormSelectDataRange();
            if (!frm.Execute(_currentData.TsFrom, _currentData.TsTo, _currentData.Prices.DataRangeDateTimeInputFormat())) return;
            _currentData.Prices = OnGetData.Invoke(_currentData, frm.TsFrom, frm.TsTo);
            using (new SuspendDrawingUpdate(chartPV))
                chartPV.LoadStockData(_currentData.Prices);
            chartPV.ResetZoom();
            lblStockInfo.Text = _currentInfoGenerator.GetStockInfo(_currentData);
        }

        private void btnMirrorChart_CheckedChanged(object sender, EventArgs e)
        {
            chartPV.ReversePricesYAxis(btnMirrorChart.Checked);
        }
        #endregion

        private void OnChartValueSelected(int selectedIndex)
        {
            if (_currentData == null) return;
            if ((selectedIndex >= 0) && (selectedIndex < _currentData.Prices.Length))
                lblSelectedInfo.Text = _currentInfoGenerator.GetStockSelectedInfo(_currentData, selectedIndex);
        }

        public void LoadData(StockDisplayData data, IStockInfoGenerator infoGenerator)
        {
            _currentData = data;
            _currentInfoGenerator = infoGenerator;
            using (new SuspendDrawingUpdate(chartPV))
                chartPV.LoadStockData(_currentData.Prices);
            chartPV.ResetZoom();
            lblStockInfo.Text = _currentInfoGenerator.GetStockInfo(_currentData);
        }
    }
}
