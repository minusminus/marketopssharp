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
using MarketOps.Controls.Extensions;
using MarketOps.Controls.PriceChart;
using MarketOps.Controls.Types;
using MarketOps.DataGen;
using MarketOps.StockData.Types;
using MarketOps.StockData.Interfaces;
using MarketOps.DataPump;
using MarketOps.DataPump.Forms;
using MarketOps.DataPump.Types;
using MarketOps.Extensions;
using MarketOps.Stats.Stats;
using MarketOps.SystemExecutor;
using MarketOps.SystemDefs.PriceCrossingSMA;
using MarketOps.SystemExecutor.Interfaces;
using MarketOps.StockData.Extensions;
using MarketOps.SystemExecutor.Slippage;
using MarketOps.SystemExecutor.Commission;

namespace MarketOps
{
    public partial class FormMain : Form
    {
        private readonly IStockInfoGenerator _stockInfoGenerator = new StockDisplayDataInfoGenerator();
        private readonly IStockStatsInfoGenerator _stockStatsInfoGenerator = new StockStatsInfoGenerator();
        private readonly MsgDisplay _msgDisplay;

        public FormMain()
        {
            InitializeComponent();
            _msgDisplay = new MsgDisplay(this, "MarketOps");
            tcCharts.TabPages.Clear();
            PrepareStockDataRangeSource();
            InitializeSim();
        }

        #region price chart events
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
        #endregion

        private void PrepareStockDataRangeSource()
        {
            cbStockDataRange.DataSource = new List<StockDataRange>()
            {
                StockDataRange.Daily,
                StockDataRange.Weekly,
                StockDataRange.Monthly
            };
        }

        private void InitializeSim()
        {
            dtpSimTo.Value = DateTime.Now.Date;
            dtpSimFrom.Value = dtpSimTo.Value.AddYears(-5);
        }

        private bool GetStockDefinition(IStockDataProvider dataProvider, out StockDefinition stockDef)
        {
            stockDef = null;
            try
            {
                stockDef = dataProvider.GetStockDefinition(edtStockName.Text.Trim());
                return true;
            } catch(Exception e)
            {
                _msgDisplay.Error(e.Message);
                return false;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            IStockDataProvider dataProvider = DataProvidersFactory.GetStockDataProvider();
            StockDefinition stockDef;
            if (!GetStockDefinition(dataProvider, out stockDef)) return;
            StockDisplayData currentStock = new StockDisplayData()
            {
                TsFrom = DateTime.Now.Date.AddYears(-1),
                TsTo = DateTime.Now.Date,
                Stock = stockDef
            };
            currentStock.Prices = dataProvider.GetPricesData(currentStock.Stock, (StockDataRange)cbStockDataRange.SelectedItem, 0, currentStock.TsFrom, currentStock.TsTo);
            AddTabWithChart(currentStock);
        }

        private void AddTabWithChart(StockDisplayData currentStock)
        {
            TabPage tab = new TabPage($"{currentStock.Stock.Name} ({currentStock.Prices.Range})") {BorderStyle = BorderStyle.FixedSingle};
            tcCharts.TabPages.Add(tab);
            PriceVolumePanel pvp = new PriceVolumePanel {Name = "pvp", Dock = DockStyle.Fill};
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

        private void tcCharts_Deselected(object sender, TabControlEventArgs e)
        {
            e.TabPage?.HidePriceAreaToolTips();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //PriceVolumePanel pvp = (PriceVolumePanel)tcCharts.TabPages[0].Controls.Find("pvp", true)[0];
            //StockStat stat = new StatATR("");
            //stat.Calculate(pvp.CurrentData.Prices);
            //pvp.Chart.TestAddStat(stat);
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

        private void dataGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataGenProvider dataGenProvider = DataProvidersFactory.GetDataGenProvider();
            IDataGen dataGen = DataGenFactory.Get(dataGenProvider);
            DataGenerator dataGenerator = new DataGenerator(dataGenProvider, dataGen);

            FormDataGen frm = new FormDataGen(dataGenerator);
            frm.Execute();
        }

        private void btnSim_Click(object sender, EventArgs e)
        {
            IStockDataProvider dataProvider = DataProvidersFactory.GetStockDataProvider();
            ISystemDataLoader dataLoader = SystemDataLoaderFactory.Get(dataProvider);

            SystemDefinition systemDef = new PriceCrossingSMA(dataProvider, dataLoader, new SlippageNone(), new CommissionNone());
            systemDef.SystemParams.Set(PriceCrossingSMAParams.StockName, "KGHM");
            systemDef.SystemParams.Set(PriceCrossingSMAParams.SMAPeriod, 20);

            SystemState systemState = new SystemState() { Cash = 10000 };

            SystemRunner runner = new SystemRunner(dataProvider, dataLoader); 
            runner.Run(systemDef, systemState, dtpSimFrom.Value.Date, dtpSimTo.Value.Date);

            _msgDisplay.Info("zrobione");
        }
    }
}
