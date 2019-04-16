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

namespace MarketOps
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IStockDataProvider data = new PgStockDataProvider();
            StockDefinition stock = data.GetStockDefinition("WIG");
            //StockPricesData prices = data.GetPricesData(stock, StockDataRange.Day, 0, new DateTime(2018, 01, 01), new DateTime(2019, 01, 01));
            StockPricesData prices = data.GetPricesData(stock, StockDataRange.Day, 0, new DateTime(2018, 01, 01), DateTime.Now.Date);

            for (int i = 0; i < prices.Length; i++)
            {
                int ix = stockVolumeChart1.Prices.Points.AddXY(prices.TS[i], prices.H[i]);
                stockVolumeChart1.Prices.Points[ix].YValues[1] = prices.L[i];
                stockVolumeChart1.Prices.Points[ix].YValues[2] = prices.O[i];
                stockVolumeChart1.Prices.Points[ix].YValues[3] = prices.C[i];

                stockVolumeChart1.Volume.Points.AddXY(prices.TS[i], prices.V[i]);
            }
        }

    }
}
