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
            currentStock.prices = data.GetPricesData(currentStock.stock, StockDataRange.Day, 0, new DateTime(2018, 01, 01), DateTime.Now.Date);
            stockVolumeChart1.LoadStockData(currentStock.prices);
        }
    }
}
