using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
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
using MarketOps.StockData;
using System.Linq;

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
        private SystemState _currentSimSystemState;
        private SystemStateSummary _currentSimSystemSummary;

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

            dbgPositions.OnPositionClick += dbgPositions_OnPositionClick;

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
        private StockPricesData OnGetChartData(StockDisplayData displayData, DateTime tsFrom, DateTime tsTo)
        {
            displayData.TsFrom = tsFrom;
            displayData.TsTo = tsTo;
            return _dataProvider.GetPricesData(displayData.Stock, displayData.Prices.Range, 0, tsFrom.AddDays(-1), tsTo);
        }

        private StockPricesData OnPrependChartData(StockDisplayData displayData)
        {
            DateTime ts = DateTimeOperations.OneTickBefore(displayData.TsFrom, displayData.Prices);
            displayData.TsFrom = ts.AddDays(-1).AddYears(-1);
            return _dataProvider.GetPricesData(displayData.Stock, displayData.Prices.Range, 0, displayData.TsFrom, ts);
        }

        private void OnRecalculateStockStats(StockDisplayData displayData)
        {
            foreach (var stat in displayData.Stats)
                stat.Calculate(displayData.Prices);
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

        private bool GetStockDefinition(string stockName, out StockDefinition stockDef)
        {
            stockDef = null;
            try
            {
                stockDef = _dataProvider.GetStockDefinition(stockName);
                return true;
            } catch(Exception e)
            {
                _msgDisplay.Error(e.Message);
                return false;
            }
        }

        private bool GetStockDisplayData(string stockName, StockDataRange dataRange, DateTime tsFrom, DateTime tsTo, out StockDisplayData displayData)
        {
            displayData = null;
            if (!GetStockDefinition(stockName, out StockDefinition stockDef)) return false;
            displayData = new StockDisplayData()
            {
                TsFrom = tsFrom,
                TsTo = tsTo,
                Stock = stockDef,
                Prices = _dataProvider.GetPricesData(stockDef, dataRange, 0, tsFrom, tsTo)
            };
            return true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!GetStockDisplayData(edtStockName.Text.Trim(), (StockDataRange)cbStockDataRange.SelectedItem, DateTime.Now.Date.AddYears(-1), DateTime.Now.Date, out StockDisplayData displayData)) return;
            AddTabWithChart(tcCharts, displayData);
        }

        private PriceVolumePanel AddTabWithChart(TabControl tabControl, StockDisplayData displayData)
        {
            TabPage tab = new TabPage($"[{displayData.Stock.Name}] {displayData.Stock.FullName} ({displayData.Prices.Range})") { BorderStyle = BorderStyle.FixedSingle };
            tabControl.TabPages.Add(tab);
            PriceVolumePanel pvp = new PriceVolumePanel { Name = "pvp", Dock = DockStyle.Fill };
            pvp.OnPrependData += OnPrependChartData;
            pvp.OnGetData += OnGetChartData;
            pvp.OnRecalculateStockStats += OnRecalculateStockStats;
            tab.Controls.Add(pvp);
            tabControl.SelectTab(tab);
            tabControl.Refresh();
            pvp.LoadData(displayData, _stockInfoGenerator, _stockStatsInfoGenerator);
            return pvp;
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
            while (tcSimulationCharts.TabCount > 2)
                tcSimulationCharts.TabPages.RemoveAt(tcSimulationCharts.TabCount - 1);
            paramsSim.SaveParams(_currentSimSystemDef.SystemParams);

            _currentSimSystemState = new SystemState() { InitialCash = (float)edtInitialCash.Value, Cash = (float)edtInitialCash.Value };
            SystemRunner runner = new SystemRunner(_dataProvider, _systemDataLoader);
            runner.Run(_currentSimSystemDef, _currentSimSystemState, dtpSimFrom.Value.Date, dtpSimTo.Value.Date);
            _currentSimSystemSummary = SystemStateSummaryCalculator.Calculate(_currentSimSystemState);
            ShowSimulationResult(_currentSimSystemState, _currentSimSystemSummary);
        }

        private void dbgPositions_OnPositionClick(Position position)
        {
            if ((_currentSimSystemState == null) || (_currentSimSystemSummary == null)) return;

            if (!GetStockDisplayData(position.Stock.Name, position.DataRange, _currentSimSystemSummary.StartTS, _currentSimSystemSummary.StopTS, out StockDisplayData displayData)) return;
            var pvp = AddTabWithChart(tcSimulationCharts, displayData);
            pvp.AddPositions(_currentSimSystemState.PositionsClosed.Where(p => p.Stock == position.Stock).ToList());
        }

        private void ShowSimulationResult(SystemState systemState, SystemStateSummary summary)
        {
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
            lblSDRProfitPcntOnTicks.Text = summary.CummYProfitPcntOnTicks.ToDisplayPcnt();

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

            lblSDREqDistrAvg.Text = summary.EquityDistribution.Average.ToDisplay(4);
            lblSDREqDistrStdDev.Text = summary.EquityDistribution.StdDev.ToDisplay(4);
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
