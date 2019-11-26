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
using MarketOps.Controls.PriceChart;
using MarketOps.Controls.Types;
using MarketOps.StockData.Types;
using MarketOps.StockData.Interfaces;
using MarketOps.DataPump;
using MarketOps.DataPump.Forms;
using MarketOps.DataPump.Types;
using MarketOps.Extensions;
using MarketOps.Stats.Stats;

namespace MarketOps
{
    public partial class FormMain : Form
    {
        private readonly IStockInfoGenerator _stockInfoGenerator = new StockDisplayDataInfoGenerator();
        private readonly IStockStatsInfoGenerator _stockStatsInfoGenerator = new StockStatsInfoGenerator();

        public FormMain()
        {
            InitializeComponent();
            tcCharts.TabPages.Clear();
        }

        private StockPricesData OnGetChartData(StockDisplayData currentData, DateTime tsFrom, DateTime tsTo)
        {
            IStockDataProvider dataProvider = DataProvidersFactory.GetStockDataProvider();
            currentData.TsFrom = tsFrom;
            currentData.TsTo = tsTo;
            return dataProvider.GetPricesData(currentData.Stock, currentData.Prices.Range, 0, tsFrom.AddDays(-1), tsTo);
        }

        private StockPricesData OnPrependChartData(StockDisplayData currentData)
        {
            DateTime ts = currentData.TsFrom;
            currentData.TsFrom = currentData.TsFrom.AddDays(-1).AddYears(-1);
            IStockDataProvider dataProvider = DataProvidersFactory.GetStockDataProvider();
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
            IStockDataProvider dataProvider = DataProvidersFactory.GetStockDataProvider();
            StockDisplayData currentStock = new StockDisplayData()
            {
                TsFrom = DateTime.Now.Date.AddYears(-1),
                TsTo = DateTime.Now.Date,
                Stock = dataProvider.GetStockDefinition(edtStockName.Text.Trim())
            };
            currentStock.Prices = dataProvider.GetPricesData(currentStock.Stock, StockDataRange.Day, 0, currentStock.TsFrom, currentStock.TsTo);
            AddTabWithChart(currentStock);
        }

        private void AddTabWithChart(StockDisplayData currentStock)
        {
            TabPage tab = new TabPage(currentStock.Stock.Name) {BorderStyle = BorderStyle.FixedSingle};
            tcCharts.TabPages.Add(tab);
            PriceVolumePanel pvp = new PriceVolumePanel {Dock = DockStyle.Fill};
            pvp.OnPrependData += OnPrependChartData;
            pvp.OnGetData += OnGetChartData;
            pvp.OnRecalculateStockStats += OnRecalculateStockStats;
            tab.Controls.Add(pvp);
            tcCharts.SelectTab(tab);
            tcCharts.Refresh();
            pvp.LoadData(currentStock, _stockInfoGenerator, _stockStatsInfoGenerator);
        }

        private void tcCharts_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                tcCharts.RemoveClickedTab(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StockStat stat = new StatBB();

            FormEditStockStatParams frm = new FormEditStockStatParams();
            if (!frm.Execute(stat.StatParams)) return;

            //stat.Calculate(pnlPV.CurrentData.Prices);
            //pnlPV.AddStat(stat);
            //pnlPV.RefreshData();
            //pnlPV.Refresh();
        }

        private void dataPumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dlPath = Path.Combine(
                Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                "DataPumpDownload");
            DirectoryUtils.ClearDir(dlPath, true);

            IDataPumpProvider dataPumpProvider = DataProvidersFactory.GetDataPumpProvider();
            IDataPump dataPump = DataPumpFactory.Get(DataPumpType.Bossa, dataPumpProvider, dlPath);
            DataPumper dataPumper = new DataPumper(dataPumpProvider, dataPump);

            FormDataPump frm = new FormDataPump(dataPumper);
            frm.Execute();
        }
    }
}
