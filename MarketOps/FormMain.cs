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

namespace MarketOps
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            pnlPV.OnPrependData += OnPrependChartData;
            pnlPV.OnGetData += OnGetChartData;
        }

        private StockPricesData OnGetChartData(StockDisplayData currentData, DateTime tsFrom, DateTime tsTo)
        {
            IStockDataProvider dataProvider = new PgStockDataProvider();
            currentData.TsFrom = tsFrom;
            currentData.TsTo = tsTo;
            return dataProvider.GetPricesData(currentData.Stock, currentData.Prices.Range, 0, tsFrom.AddDays(-1), tsTo);
        }

        private StockPricesData OnPrependChartData(StockDisplayData currentData)
        {
            DateTime ts = currentData.TsFrom;
            currentData.TsFrom = currentData.TsFrom.AddDays(-1).AddYears(-1);
            IStockDataProvider dataProvider = new PgStockDataProvider();
            StockPricesData newdata = dataProvider.GetPricesData(currentData.Stock, currentData.Prices.Range, 0, currentData.TsFrom, ts);
            return newdata;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            IStockDataProvider dataProvider = new PgStockDataProvider();
            StockDisplayData currentStock = new StockDisplayData()
            {
                TsFrom = DateTime.Now.Date.AddYears(-1),
                TsTo = DateTime.Now.Date,
                Stock = dataProvider.GetStockDefinition("WIG")
            };
            currentStock.Prices = dataProvider.GetPricesData(currentStock.Stock, StockDataRange.Day, 0, currentStock.TsFrom, currentStock.TsTo);
            pnlPV.LoadData(currentStock, new StockDisplayDataInfoGenerator());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"{pnlPV.Chart.ChartAreas["areaPrices"].AxisX.ScaleView.ViewMinimum}, {pnlPV.Chart.ChartAreas["areaPrices"].AxisX.ScaleView.ViewMaximum}");
        }
    }
}
