using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketOps.Controls;
using MarketOps.StockData.Types;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Extensions;
using MarketOps.DataProvider.Pg;
using MarketOps.DataProvider.Pg.Bossa;
using MarketOps.DataPump;
using MarketOps.DataPump.Forms;
using MarketOps.DataPump.Types;
using MarketOps.Stats.Stats;

namespace MarketOps
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            pnlPV.OnPrependData += OnPrependChartData;
            pnlPV.OnGetData += OnGetChartData;
            pnlPV.OnRecalculateStockStats += OnRecalculateStockStats;
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

        private void OnRecalculateStockStats(StockDisplayData currentData)
        {
            foreach (var stat in currentData.Stats)
                stat.Calculate(currentData.Prices);
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
            pnlPV.LoadData(currentStock, new StockDisplayDataInfoGenerator(), new StockStatsInfoGenerator());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StockStat stat = new StatBB();

            FormEditStockStatParams frm = new FormEditStockStatParams();
            if (!frm.Execute(stat.StatParams)) return;

            stat.Calculate(pnlPV.CurrentData.Prices);
            pnlPV.AddStat(stat);
            pnlPV.RefreshData();
            pnlPV.Refresh();
        }

        private void dataPumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dlPath = Path.Combine(
                Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                "DataPumpDownload");
            DirectoryUtils.ClearDir(dlPath, true);

            DataTableSelector selector = new DataTableSelector();
            IDataPumpProvider dataPumpProvider = new PgDataPumpProvider(selector);
            IDataPump dataPump = DataPumpFactory.Get(DataPumpType.Bossa, dataPumpProvider, dlPath);
            DataPumper dataPumper = new DataPumper(dataPumpProvider, dataPump);

            FormDataPump frm = new FormDataPump(dataPumper);
            frm.Execute();
        }
    }
}
