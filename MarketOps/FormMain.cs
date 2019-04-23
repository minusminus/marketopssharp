using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketOps.StockData.Types;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Extensions;
using MarketOps.DataProvider.Pg;
using MarketOps.Controls.Extensions;
using MarketOps.Extensions;

namespace MarketOps
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            stockVolumeChart1.OnChartValueSelected += OnChartValueSelected;
        }

        private StockDisplayData currentStock = new StockDisplayData();

        private void OnChartValueSelected(int selectedIndex)
        {
            if ((selectedIndex >= 0) && (selectedIndex < currentStock.prices?.Length))
                lblStockSelectedPointInfo.Text = currentStock.GetInfo(selectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IStockDataProvider data = new PgStockDataProvider();
            currentStock.stock = data.GetStockDefinition("WIG");
            DateTime ts = DateTime.Now.Date;
            currentStock.prices = data.GetPricesData(currentStock.stock, StockDataRange.Day, 0, ts.AddYears(-1), ts);
            stockVolumeChart1.LoadStockData(currentStock.prices);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IStockDataProvider data = new PgStockDataProvider();
            DateTime ts = currentStock.prices.TS[0].AddDays(-1);
            StockPricesData newdata = data.GetPricesData(currentStock.stock, StockDataRange.Day, 0, ts.AddYears(-1), ts);
            stockVolumeChart1.PrependStockData(newdata);
            currentStock.prices = currentStock.prices.Merge(newdata);   //merger adds to earlier - needs change
        }
    }
}
