﻿using System;
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
            pnlPV.OnPrependData += OnPrependChartData;
        }

        private StockPricesData OnPrependChartData(StockDisplayData currentData)
        {
            DateTime ts = currentData.prices.TS[0].AddDays(-1);
            IStockDataProvider dataProvider = new PgStockDataProvider();
            StockPricesData newdata = dataProvider.GetPricesData(currentData.stock, currentData.prices.Range, 0, ts.AddYears(-1), ts);
            return newdata;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime ts = DateTime.Now.Date;
            StockDisplayData currentStock = new StockDisplayData();
            IStockDataProvider dataProvider = new PgStockDataProvider();
            currentStock.stock = dataProvider.GetStockDefinition("WIG");
            currentStock.prices = dataProvider.GetPricesData(currentStock.stock, StockDataRange.Day, 0, ts.AddYears(-1), ts);
            pnlPV.LoadData(currentStock, new StockDisplayDataInfoGenerator());
        }

    }
}
