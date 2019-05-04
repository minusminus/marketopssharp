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

        private StockDisplayData currentData;
        private IStockInfoGenerator currentInfoGenerator;

        public PriceVolumeChart Chart => chartPV;

        public delegate StockPricesData GetAdditionalData(StockDisplayData currentData);
        public event GetAdditionalData OnPrependData;
        //public event GetAdditionalData OnAppendData;


        public void LoadData(StockDisplayData data, IStockInfoGenerator infoGenerator)
        {
            currentData = data;
            currentInfoGenerator = infoGenerator;
            chartPV.LoadStockData(currentData.prices);
            lblStockInfo.Text = currentInfoGenerator.GetStockInfo(currentData);
        }

        private void OnChartValueSelected(int selectedIndex)
        {
            if (currentData == null) return;
            if ((selectedIndex >= 0) && (selectedIndex < currentData.prices.Length))
                lblSelectedInfo.Text = currentInfoGenerator.GetStockSelectedInfo(currentData, selectedIndex);
        }

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
            StockPricesData newData = OnPrependData.Invoke(currentData);
            currentData.prices = currentData.prices.Merge(newData);
            chartPV.PrependStockData(newData);
            lblStockInfo.Text = currentInfoGenerator.GetStockInfo(currentData);
        }
    }
}
