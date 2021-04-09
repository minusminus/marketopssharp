using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
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
using MarketOps.SystemExecutor;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemExecutor.Slippage;
using MarketOps.SystemExecutor.Commission;
using MarketOps.SystemAnalysis.SystemSummary;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Types;
using MarketOps.DataMappers;
using MarketOps.Config.SystemExecutor;
using MarketOps.Config.Stats;
using MarketOps.Config.App;

namespace MarketOps
{
    public partial class FormMain : Form
    {
        private readonly IStockInfoGenerator _stockInfoGenerator = new StockDisplayDataInfoGenerator();
        private readonly IStockStatsInfoGenerator _stockStatsInfoGenerator = new StockStatsInfoGenerator();
        private readonly MsgDisplay _msgDisplay;

        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _systemDataLoader;

        private readonly SystemExecutionLoggerToTextBox _systemExecutionLogger;
        private readonly List<ConfigSystemDefinition> _configSystemDefinitions;
        private readonly SystemDefinitionFactory _systemDefinitionFactory;
        private SystemDefinition _currentSimSystemDef;

        public FormMain()
        {
            InitializeComponent();
            _msgDisplay = new MsgDisplay(this, "MarketOps");
            _dataProvider = DataProvidersFactory.GetStockDataProvider();
            _systemDataLoader = SystemDataLoaderFactory.Get(_dataProvider);
            _systemExecutionLogger = new SystemExecutionLoggerToTextBox(edtSimDataLog);
            _configSystemDefinitions = ConfigSystemDefsLoader.Load();
            _systemDefinitionFactory = new SystemDefinitionFactory(_dataProvider, _systemDataLoader, new SlippageNone(), new CommissionNone(), _systemExecutionLogger);
            StatsFactories.Initialize();

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            tcCharts.TabPages.Clear();
            PrepareStockDataRangeSource();
            InitializeSim();
            LoadConfig();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfig();
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
            InitializeSimSystemChoice();
        }

        private void InitializeSimSystemChoice()
        {
            cbSystemChoice.DataSource = _configSystemDefinitions;
            cbSystemChoice.DisplayMember = "Description";
            cbSystemChoice.ValueMember = "Description";
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
            ConfigSystemDefinition config = (ConfigSystemDefinition)cbSystemChoice.SelectedItem;
            _currentSimSystemDef = _systemDefinitionFactory.Get(config);

            lblSimSystemName.Text = config.Description;
            paramsSim.LoadParams(_currentSimSystemDef.SystemParams);
        }

        private void btnSim_Click(object sender, EventArgs e)
        {
            if (_currentSimSystemDef == null)
            {
                _msgDisplay.Error("System definition not loaded.");
                return;
            }

            edtSimDataLog.Clear();
            paramsSim.SaveParams(_currentSimSystemDef.SystemParams);

            SystemState systemState = new SystemState() { InitialCash = (float)edtInitialCash.Value, Cash = (float)edtInitialCash.Value };

            SystemRunner runner = new SystemRunner(_dataProvider, _systemDataLoader);
            runner.Run(_currentSimSystemDef, systemState, dtpSimFrom.Value.Date, dtpSimTo.Value.Date);
            ShowSimulationResult(systemState);

            //_msgDisplay.Info("zrobione");
        }

        private void ShowSimulationResult(SystemState systemState)
        {
            SystemStateSummary summary = SystemStateSummaryCalculator.Calculate(systemState);

            DisplaySummary(summary);
            ShowPositions(systemState);
            ShowEquityCharts(systemState);
            ShowDDCharts(summary);
            ShowProfitCharts(systemState);
        }

        private void DisplaySummary(SystemStateSummary summary)
        {
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
            lblSDRExpectedUnitReturn.Text = summary.AvgLoss != 0 ? summary.ExpectedUnitReturn.ToDisplay() : "---";
            lblSDRExpectedPositionValue.Text = summary.AvgLoss != 0 ? summary.ExpectedPositionValue.ToDisplay() : "---";

            lblSDRMaxDDOnTicks.Text = summary.DDTicks.MaxDD().ToDisplayPcnt();
            lblSDRMaxDDOnPositions.Text = summary.DDClosedPositions.MaxDD().ToDisplayPcnt();
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

        private void ShowDDCharts(SystemStateSummary summary)
        {
            chartDDTicks.LoadData(SystemDrawDown2PointChartMapper.Map(summary.DDTicks));
            chartDDPositions.LoadData(SystemDrawDown2PointChartMapper.Map(summary.DDClosedPositions));
        }

        private void ShowProfitCharts(SystemState systemState)
        {
            chartProfitValue.LoadData(Profit2PointChartMapper.Map(systemState.PositionsClosed));
            chartProfitPcnt.LoadData(ProfitPcnt2PointChartMapper.Map(systemState.PositionsClosed));
        }

        private void LoadConfig()
        {
            var appConfig = AppConfigOps.Load();
            if (appConfig.Charts != null)
            {
                if (!string.IsNullOrEmpty(appConfig.Charts.StockName))
                    edtStockName.Text = appConfig.Charts.StockName;
                if (!string.IsNullOrEmpty(appConfig.Charts.StockDataRange))
                    cbStockDataRange.Text = appConfig.Charts.StockDataRange;
            }
            if (appConfig.Simulation != null)
            {
                if (!string.IsNullOrEmpty(appConfig.Simulation.SystemChoice))
                    cbSystemChoice.Text = appConfig.Simulation.SystemChoice;
                if (appConfig.Simulation.SimFrom != DateTime.MinValue)
                    dtpSimFrom.Value = appConfig.Simulation.SimFrom;
                if (appConfig.Simulation.SimTo != DateTime.MinValue)
                    dtpSimTo.Value = appConfig.Simulation.SimTo;
                if (appConfig.Simulation.InitialCash > 0)
                    edtInitialCash.Value = appConfig.Simulation.InitialCash;
            }
        }

        private void SaveConfig()
        {
            var appConfig = new AppConfig
            {
                Charts = new AppConfigCharts(),
                Simulation = new AppConfigSimulation()
            };

            appConfig.Charts.StockName = edtStockName.Text;
            appConfig.Charts.StockDataRange = cbStockDataRange.Text;

            appConfig.Simulation.SystemChoice = cbSystemChoice.Text;
            appConfig.Simulation.SimFrom = dtpSimFrom.Value;
            appConfig.Simulation.SimTo = dtpSimTo.Value;
            appConfig.Simulation.InitialCash = edtInitialCash.Value;

            AppConfigOps.Save(appConfig);
        }
    }
}
