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
using MarketOps.Controls.ChartsUtils;
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

        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _systemDataLoader;

        private SystemDefinition _currentSimSystemDef;

        public FormMain()
        {
            InitializeComponent();
            _msgDisplay = new MsgDisplay(this, "MarketOps");
            _dataProvider = DataProvidersFactory.GetStockDataProvider();
            _systemDataLoader = SystemDataLoaderFactory.Get(_dataProvider);

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            tcCharts.TabPages.Clear();
            PrepareStockDataRangeSource();
            InitializeSim();
        }

        #region price chart events
        private StockPricesData OnGetChartData(StockDisplayData currentData, DateTime tsFrom, DateTime tsTo)
        {
            currentData.TsFrom = tsFrom;
            currentData.TsTo = tsTo;
            return _dataProvider.GetPricesData(currentData.Stock, currentData.Prices.Range, 0, tsFrom.AddDays(-1), tsTo);
        }

        private StockPricesData OnPrependChartData(StockDisplayData currentData)
        {
            DateTime ts = currentData.TsFrom;
            currentData.TsFrom = currentData.TsFrom.AddDays(-1).AddYears(-1);
            return _dataProvider.GetPricesData(currentData.Stock, currentData.Prices.Range, 0, currentData.TsFrom, ts);
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
            if (!GetStockDefinition(_dataProvider, out StockDefinition stockDef)) return;
            StockDisplayData currentStock = new StockDisplayData()
            {
                TsFrom = DateTime.Now.Date.AddYears(-1),
                TsTo = DateTime.Now.Date,
                Stock = stockDef
            };
            currentStock.Prices = _dataProvider.GetPricesData(currentStock.Stock, (StockDataRange)cbStockDataRange.SelectedItem, 0, currentStock.TsFrom, currentStock.TsTo);
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

        private void btnSimLoadDefinition_Click(object sender, EventArgs e)
        {
            _currentSimSystemDef = new PriceCrossingSMA(_dataProvider, _systemDataLoader, new SlippageNone(), new CommissionNone());
            _currentSimSystemDef.SystemParams.Set(PriceCrossingSMAParams.StockName, "KGHM");
            _currentSimSystemDef.SystemParams.Set(PriceCrossingSMAParams.SMAPeriod, 20);

            paramsSim.LoadParams(_currentSimSystemDef.SystemParams);
        }

        private void btnSim_Click(object sender, EventArgs e)
        {
            if (_currentSimSystemDef == null)
            {
                _msgDisplay.Error("System definition not loaded.");
                return;
            }

            paramsSim.SaveParams(_currentSimSystemDef.SystemParams);

            SystemState systemState = new SystemState() { InitialCash = (float)edtInitialCash.Value, Cash = (float)edtInitialCash.Value };

            SystemRunner runner = new SystemRunner(_dataProvider, _systemDataLoader);
            runner.Run(_currentSimSystemDef, systemState, dtpSimFrom.Value.Date, dtpSimTo.Value.Date);
            ShowSimulationResult(systemState);

            //_msgDisplay.Info("zrobione");
        }

        private void ShowSimulationResult(SystemState systemState)
        {
            CalcAndShowSummary(systemState);
            ShowPositions(systemState);
            ShowEquityCharts(systemState);
        }

        private void CalcAndShowSummary(SystemState systemState)
        {
            SystemStateSummary summary = new SystemStateSummaryCalculator().Calculate(systemState);

            lblSDRStartTS.Text = summary.StartTS.ToDisplay();
            lblSDRStopTS.Text = summary.StopTS.ToDisplay();
            lblSDRProcessedTicks.Text = summary.ProcessedTicks.ToDisplay();

            lblSDRInitialValue.Text = summary.InitialValue.ToDisplay();
            lblSDRFinalValueOnClosedPositions.Text = summary.FinalValueOnClosedPositions.ToDisplay();
            lblSDRFinalValueOnLastTick.Text = summary.FinalValueOnLastTick.ToDisplay();

            lblSDRClosedPositionsCount.Text = summary.ClosedPositionsCount.ToDisplay();
            lblSDRWins.Text = summary.Wins.ToDisplay();
            lblSDRWinProbability.Text = summary.WinProbability.ToDisplay();
            lblSDRSumWins.Text = summary.SumWins.ToDisplay();
            lblSDRAvgWin.Text = summary.AvgWin.ToDisplay();
            lblSDRLosses.Text = summary.Losses.ToDisplay();
            lblSDRLossProbability.Text = summary.LossProbability.ToDisplay();
            lblSDRSumLosses.Text = summary.SumLosses.ToDisplay();
            lblSDRAvgLoss.Text = summary.AvgLoss.ToDisplay();
            lblSDRAvgWinLossRatio.Text = summary.AvgLoss != 0 ? summary.AvgWinLossRatio.ToDisplay() : "---";
            lblSDRExpectedPositionValue.Text = summary.ExpectedPositionValue.ToDisplay();
        }

        private void ShowPositions(SystemState systemState)
        {
            dbgPositions.LoadData(systemState);
        }

        private void ShowEquityCharts(SystemState systemState)
        {
            chartEquity.LoadData(systemState.Equity);
            chartEquityOnPositions.LoadData(systemState.ClosedPositionsEquity);
        }
    }
}
