﻿namespace MarketOps
{
    partial class FormMain
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGenerationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcCharts = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.cbStockDataRange = new System.Windows.Forms.ComboBox();
            this.edtStockName = new System.Windows.Forms.TextBox();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabCharts = new System.Windows.Forms.TabPage();
            this.tabSimulation = new System.Windows.Forms.TabPage();
            this.pnlSimResult = new System.Windows.Forms.Panel();
            this.tcSimulationCharts = new System.Windows.Forms.TabControl();
            this.tabSimChartsEqTicks = new System.Windows.Forms.TabPage();
            this.chartEquity = new MarketOps.Controls.SystemEquity.SystemEquityChart();
            this.tabSimChartsEqOnPositions = new System.Windows.Forms.TabPage();
            this.chartEquityOnPositions = new MarketOps.Controls.SystemEquity.SystemEquityChart();
            this.tcSimulationData = new System.Windows.Forms.TabControl();
            this.tabSimDataResults = new System.Windows.Forms.TabPage();
            this.pnlSimDataResults = new System.Windows.Forms.Panel();
            this.lblSDRRProfitAvgToStdDev = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.lblSDRRProfitStdDev = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.lblSDRTransactionsPerYear = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblSDRRProfitAvg = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblSDRAvgPcntLoss = new System.Windows.Forms.Label();
            this.lblSDRAvgPcntWin = new System.Windows.Forms.Label();
            this.lblSDREqDistrStdDev = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblSDREqDistrAvg = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblSDRProfitPcntOnTicks = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblSDRExpectedUnitReturn = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblSDRMaxDDOnPositions = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblSDRMaxDDOnTicks = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblSDRProcessedTicks = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblSDRExpectedPositionValue = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblSDRAvgWinLossRatio = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSDRLossProbability = new System.Windows.Forms.Label();
            this.lblSDRWinProbability = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblSDRSumLosses = new System.Windows.Forms.Label();
            this.lblSDRSumWins = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblSDRAvgLoss = new System.Windows.Forms.Label();
            this.lblSDRAvgWin = new System.Windows.Forms.Label();
            this.lblSDRLosses = new System.Windows.Forms.Label();
            this.lblSDRWins = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSDRClosedPositionsCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSDRStopTS = new System.Windows.Forms.Label();
            this.lblSDRStartTS = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblSDRFinalValueOnLastTick = new System.Windows.Forms.Label();
            this.lblSDRFinalValueOnClosedPositions = new System.Windows.Forms.Label();
            this.lblSDRInitialValue = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabSimDataPositions = new System.Windows.Forms.TabPage();
            this.dbgPositions = new MarketOps.Controls.SystemPositionsGrid.SystemPositionsGrid();
            this.tabSimDataDrawdowns = new System.Windows.Forms.TabPage();
            this.splitDrawDowns = new System.Windows.Forms.SplitContainer();
            this.gbDDTicks = new System.Windows.Forms.GroupBox();
            this.chartDDTicks = new MarketOps.Controls.PointChart.PointChart();
            this.gbDDPositions = new System.Windows.Forms.GroupBox();
            this.chartDDPositions = new MarketOps.Controls.PointChart.PointChart();
            this.tabSimR = new System.Windows.Forms.TabPage();
            this.splitR = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chartRValue = new MarketOps.Controls.PointChart.PointChart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chartRDistribution = new MarketOps.Controls.ColumnChart.ColumnChart();
            this.tabSimDataProfits = new System.Windows.Forms.TabPage();
            this.splitProfits = new System.Windows.Forms.SplitContainer();
            this.gbProfitsValue = new System.Windows.Forms.GroupBox();
            this.chartProfitValue = new MarketOps.Controls.PointChart.PointChart();
            this.gbProfitPcnt = new System.Windows.Forms.GroupBox();
            this.chartProfitPcnt = new MarketOps.Controls.PointChart.PointChart();
            this.tabSimDataLog = new System.Windows.Forms.TabPage();
            this.edtSimDataLog = new System.Windows.Forms.TextBox();
            this.tabSimMonteCarlo = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chartMonteCarloData = new MarketOps.Controls.MonteCarlo.MonteCarloDataChart();
            this.gbMonteCarloStreaksLosing = new System.Windows.Forms.GroupBox();
            this.chartMonteCarloStreaksLosing = new MarketOps.Controls.MonteCarlo.MonteCarloStreaksGrid();
            this.gbMonteCarloStreaksWinning = new System.Windows.Forms.GroupBox();
            this.chartMonteCarloStreaksWinning = new MarketOps.Controls.MonteCarlo.MonteCarloStreaksGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblMonteCarloSimLongestWinningStreak = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.lblMonteCarloSimLongestLosingStreak = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lblMonteCarloSimLongestDD = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.lblMonteCarloSimMaxDD = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lblMonteCarloSimAvg = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lblMonteCarloSimWorst = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lblMonteCarloSimBest = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lblMonteCarloSimLosses = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lblMonteCarloSimWins = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.edtMonteCarloTransactionsPerYear = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.btnMonteCarloSim = new System.Windows.Forms.Button();
            this.edtMonteCarloAvgPcntLoss = new System.Windows.Forms.NumericUpDown();
            this.edtMonteCarloAvgPcntWin = new System.Windows.Forms.NumericUpDown();
            this.edtMonteCarloWinProb = new System.Windows.Forms.NumericUpDown();
            this.edtMonteCarloLength = new System.Windows.Forms.NumericUpDown();
            this.edtMonteCarloCount = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.paramsSim = new MarketOps.Controls.StockData.MOParamsEditor();
            this.pnlSimulationStart = new System.Windows.Forms.Panel();
            this.lblSimSystemName = new System.Windows.Forms.Label();
            this.cbSystemChoice = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.edtInitialCash = new System.Windows.Forms.NumericUpDown();
            this.btnSimLoadDefinition = new System.Windows.Forms.Button();
            this.btnSim = new System.Windows.Forms.Button();
            this.dtpSimFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpSimTo = new System.Windows.Forms.DateTimePicker();
            this.menuMain.SuspendLayout();
            this.tcCharts.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabCharts.SuspendLayout();
            this.tabSimulation.SuspendLayout();
            this.pnlSimResult.SuspendLayout();
            this.tcSimulationCharts.SuspendLayout();
            this.tabSimChartsEqTicks.SuspendLayout();
            this.tabSimChartsEqOnPositions.SuspendLayout();
            this.tcSimulationData.SuspendLayout();
            this.tabSimDataResults.SuspendLayout();
            this.pnlSimDataResults.SuspendLayout();
            this.tabSimDataPositions.SuspendLayout();
            this.tabSimDataDrawdowns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDrawDowns)).BeginInit();
            this.splitDrawDowns.Panel1.SuspendLayout();
            this.splitDrawDowns.Panel2.SuspendLayout();
            this.splitDrawDowns.SuspendLayout();
            this.gbDDTicks.SuspendLayout();
            this.gbDDPositions.SuspendLayout();
            this.tabSimR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitR)).BeginInit();
            this.splitR.Panel1.SuspendLayout();
            this.splitR.Panel2.SuspendLayout();
            this.splitR.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabSimDataProfits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitProfits)).BeginInit();
            this.splitProfits.Panel1.SuspendLayout();
            this.splitProfits.Panel2.SuspendLayout();
            this.splitProfits.SuspendLayout();
            this.gbProfitsValue.SuspendLayout();
            this.gbProfitPcnt.SuspendLayout();
            this.tabSimDataLog.SuspendLayout();
            this.tabSimMonteCarlo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbMonteCarloStreaksLosing.SuspendLayout();
            this.gbMonteCarloStreaksWinning.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloTransactionsPerYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloAvgPcntLoss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloAvgPcntWin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloWinProb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloCount)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlSimulationStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtInitialCash)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 51);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuMain.Size = new System.Drawing.Size(1208, 24);
            this.menuMain.TabIndex = 9;
            this.menuMain.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataPumpToolStripMenuItem,
            this.dataGenerationToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem1.Text = "Data";
            // 
            // dataPumpToolStripMenuItem
            // 
            this.dataPumpToolStripMenuItem.Name = "dataPumpToolStripMenuItem";
            this.dataPumpToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.dataPumpToolStripMenuItem.Text = "Data pump";
            this.dataPumpToolStripMenuItem.Click += new System.EventHandler(this.dataPumpToolStripMenuItem_Click);
            // 
            // dataGenerationToolStripMenuItem
            // 
            this.dataGenerationToolStripMenuItem.Name = "dataGenerationToolStripMenuItem";
            this.dataGenerationToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.dataGenerationToolStripMenuItem.Text = "Data generation";
            this.dataGenerationToolStripMenuItem.Click += new System.EventHandler(this.dataGenerationToolStripMenuItem_Click);
            // 
            // tcCharts
            // 
            this.tcCharts.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcCharts.Controls.Add(this.tabPage1);
            this.tcCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCharts.Location = new System.Drawing.Point(195, 0);
            this.tcCharts.Multiline = true;
            this.tcCharts.Name = "tcCharts";
            this.tcCharts.SelectedIndex = 0;
            this.tcCharts.Size = new System.Drawing.Size(1005, 667);
            this.tcCharts.TabIndex = 10;
            this.tcCharts.TabStop = false;
            this.tcCharts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tcCharts_MouseClick);
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(997, 638);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlCharts
            // 
            this.pnlCharts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCharts.Controls.Add(this.cbStockDataRange);
            this.pnlCharts.Controls.Add(this.edtStockName);
            this.pnlCharts.Controls.Add(this.btnLoad);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCharts.Location = new System.Drawing.Point(0, 0);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Size = new System.Drawing.Size(195, 667);
            this.pnlCharts.TabIndex = 0;
            // 
            // cbStockDataRange
            // 
            this.cbStockDataRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStockDataRange.FormattingEnabled = true;
            this.cbStockDataRange.Location = new System.Drawing.Point(100, 25);
            this.cbStockDataRange.Name = "cbStockDataRange";
            this.cbStockDataRange.Size = new System.Drawing.Size(75, 21);
            this.cbStockDataRange.TabIndex = 2;
            // 
            // edtStockName
            // 
            this.edtStockName.Location = new System.Drawing.Point(12, 25);
            this.edtStockName.Name = "edtStockName";
            this.edtStockName.Size = new System.Drawing.Size(82, 20);
            this.edtStockName.TabIndex = 1;
            this.edtStockName.Text = "WIG";
            // 
            // tcMain
            // 
            this.tcMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcMain.Controls.Add(this.tabCharts);
            this.tcMain.Controls.Add(this.tabSimulation);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 24);
            this.tcMain.Name = "tcMain";
            this.tcMain.Padding = new System.Drawing.Point(20, 6);
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1208, 702);
            this.tcMain.TabIndex = 0;
            this.tcMain.TabStop = false;
            // 
            // tabCharts
            // 
            this.tabCharts.Controls.Add(this.tcCharts);
            this.tabCharts.Controls.Add(this.pnlCharts);
            this.tabCharts.Location = new System.Drawing.Point(4, 31);
            this.tabCharts.Name = "tabCharts";
            this.tabCharts.Size = new System.Drawing.Size(1200, 667);
            this.tabCharts.TabIndex = 0;
            this.tabCharts.Text = "Charts";
            this.tabCharts.UseVisualStyleBackColor = true;
            // 
            // tabSimulation
            // 
            this.tabSimulation.Controls.Add(this.pnlSimResult);
            this.tabSimulation.Controls.Add(this.pnlTop);
            this.tabSimulation.Location = new System.Drawing.Point(4, 31);
            this.tabSimulation.Name = "tabSimulation";
            this.tabSimulation.Size = new System.Drawing.Size(1200, 667);
            this.tabSimulation.TabIndex = 1;
            this.tabSimulation.Text = "Simulation";
            this.tabSimulation.UseVisualStyleBackColor = true;
            // 
            // pnlSimResult
            // 
            this.pnlSimResult.Controls.Add(this.tcSimulationCharts);
            this.pnlSimResult.Controls.Add(this.tcSimulationData);
            this.pnlSimResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSimResult.Location = new System.Drawing.Point(253, 0);
            this.pnlSimResult.Name = "pnlSimResult";
            this.pnlSimResult.Size = new System.Drawing.Size(947, 667);
            this.pnlSimResult.TabIndex = 1;
            // 
            // tcSimulationCharts
            // 
            this.tcSimulationCharts.Controls.Add(this.tabSimChartsEqTicks);
            this.tcSimulationCharts.Controls.Add(this.tabSimChartsEqOnPositions);
            this.tcSimulationCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSimulationCharts.Location = new System.Drawing.Point(0, 0);
            this.tcSimulationCharts.Name = "tcSimulationCharts";
            this.tcSimulationCharts.SelectedIndex = 0;
            this.tcSimulationCharts.Size = new System.Drawing.Size(947, 364);
            this.tcSimulationCharts.TabIndex = 0;
            // 
            // tabSimChartsEqTicks
            // 
            this.tabSimChartsEqTicks.Controls.Add(this.chartEquity);
            this.tabSimChartsEqTicks.Location = new System.Drawing.Point(4, 22);
            this.tabSimChartsEqTicks.Name = "tabSimChartsEqTicks";
            this.tabSimChartsEqTicks.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimChartsEqTicks.Size = new System.Drawing.Size(939, 338);
            this.tabSimChartsEqTicks.TabIndex = 0;
            this.tabSimChartsEqTicks.Text = "Equity";
            this.tabSimChartsEqTicks.UseVisualStyleBackColor = true;
            // 
            // chartEquity
            // 
            this.chartEquity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEquity.Location = new System.Drawing.Point(3, 3);
            this.chartEquity.Name = "chartEquity";
            this.chartEquity.Size = new System.Drawing.Size(933, 332);
            this.chartEquity.TabIndex = 0;
            // 
            // tabSimChartsEqOnPositions
            // 
            this.tabSimChartsEqOnPositions.Controls.Add(this.chartEquityOnPositions);
            this.tabSimChartsEqOnPositions.Location = new System.Drawing.Point(4, 22);
            this.tabSimChartsEqOnPositions.Name = "tabSimChartsEqOnPositions";
            this.tabSimChartsEqOnPositions.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimChartsEqOnPositions.Size = new System.Drawing.Size(939, 338);
            this.tabSimChartsEqOnPositions.TabIndex = 1;
            this.tabSimChartsEqOnPositions.Text = "Equity on positions";
            this.tabSimChartsEqOnPositions.UseVisualStyleBackColor = true;
            // 
            // chartEquityOnPositions
            // 
            this.chartEquityOnPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEquityOnPositions.Location = new System.Drawing.Point(3, 3);
            this.chartEquityOnPositions.Name = "chartEquityOnPositions";
            this.chartEquityOnPositions.Size = new System.Drawing.Size(933, 332);
            this.chartEquityOnPositions.TabIndex = 0;
            // 
            // tcSimulationData
            // 
            this.tcSimulationData.Controls.Add(this.tabSimDataResults);
            this.tcSimulationData.Controls.Add(this.tabSimDataPositions);
            this.tcSimulationData.Controls.Add(this.tabSimDataDrawdowns);
            this.tcSimulationData.Controls.Add(this.tabSimR);
            this.tcSimulationData.Controls.Add(this.tabSimDataProfits);
            this.tcSimulationData.Controls.Add(this.tabSimDataLog);
            this.tcSimulationData.Controls.Add(this.tabSimMonteCarlo);
            this.tcSimulationData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tcSimulationData.Location = new System.Drawing.Point(0, 364);
            this.tcSimulationData.Name = "tcSimulationData";
            this.tcSimulationData.SelectedIndex = 0;
            this.tcSimulationData.Size = new System.Drawing.Size(947, 303);
            this.tcSimulationData.TabIndex = 1;
            // 
            // tabSimDataResults
            // 
            this.tabSimDataResults.Controls.Add(this.pnlSimDataResults);
            this.tabSimDataResults.Location = new System.Drawing.Point(4, 22);
            this.tabSimDataResults.Name = "tabSimDataResults";
            this.tabSimDataResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimDataResults.Size = new System.Drawing.Size(939, 277);
            this.tabSimDataResults.TabIndex = 0;
            this.tabSimDataResults.Text = "Results";
            this.tabSimDataResults.UseVisualStyleBackColor = true;
            // 
            // pnlSimDataResults
            // 
            this.pnlSimDataResults.Controls.Add(this.lblSDRRProfitAvgToStdDev);
            this.pnlSimDataResults.Controls.Add(this.label49);
            this.pnlSimDataResults.Controls.Add(this.lblSDRRProfitStdDev);
            this.pnlSimDataResults.Controls.Add(this.label46);
            this.pnlSimDataResults.Controls.Add(this.label39);
            this.pnlSimDataResults.Controls.Add(this.label42);
            this.pnlSimDataResults.Controls.Add(this.lblSDRTransactionsPerYear);
            this.pnlSimDataResults.Controls.Add(this.label37);
            this.pnlSimDataResults.Controls.Add(this.lblSDRRProfitAvg);
            this.pnlSimDataResults.Controls.Add(this.label35);
            this.pnlSimDataResults.Controls.Add(this.lblSDRAvgPcntLoss);
            this.pnlSimDataResults.Controls.Add(this.lblSDRAvgPcntWin);
            this.pnlSimDataResults.Controls.Add(this.lblSDREqDistrStdDev);
            this.pnlSimDataResults.Controls.Add(this.label25);
            this.pnlSimDataResults.Controls.Add(this.lblSDREqDistrAvg);
            this.pnlSimDataResults.Controls.Add(this.label27);
            this.pnlSimDataResults.Controls.Add(this.lblSDRProfitPcntOnTicks);
            this.pnlSimDataResults.Controls.Add(this.label24);
            this.pnlSimDataResults.Controls.Add(this.lblSDRExpectedUnitReturn);
            this.pnlSimDataResults.Controls.Add(this.label22);
            this.pnlSimDataResults.Controls.Add(this.lblSDRMaxDDOnPositions);
            this.pnlSimDataResults.Controls.Add(this.label23);
            this.pnlSimDataResults.Controls.Add(this.lblSDRMaxDDOnTicks);
            this.pnlSimDataResults.Controls.Add(this.label21);
            this.pnlSimDataResults.Controls.Add(this.lblSDRProcessedTicks);
            this.pnlSimDataResults.Controls.Add(this.label20);
            this.pnlSimDataResults.Controls.Add(this.lblSDRExpectedPositionValue);
            this.pnlSimDataResults.Controls.Add(this.label19);
            this.pnlSimDataResults.Controls.Add(this.lblSDRAvgWinLossRatio);
            this.pnlSimDataResults.Controls.Add(this.label14);
            this.pnlSimDataResults.Controls.Add(this.lblSDRLossProbability);
            this.pnlSimDataResults.Controls.Add(this.lblSDRWinProbability);
            this.pnlSimDataResults.Controls.Add(this.label17);
            this.pnlSimDataResults.Controls.Add(this.label18);
            this.pnlSimDataResults.Controls.Add(this.lblSDRSumLosses);
            this.pnlSimDataResults.Controls.Add(this.lblSDRSumWins);
            this.pnlSimDataResults.Controls.Add(this.label15);
            this.pnlSimDataResults.Controls.Add(this.label16);
            this.pnlSimDataResults.Controls.Add(this.lblSDRAvgLoss);
            this.pnlSimDataResults.Controls.Add(this.lblSDRAvgWin);
            this.pnlSimDataResults.Controls.Add(this.lblSDRLosses);
            this.pnlSimDataResults.Controls.Add(this.lblSDRWins);
            this.pnlSimDataResults.Controls.Add(this.label7);
            this.pnlSimDataResults.Controls.Add(this.label11);
            this.pnlSimDataResults.Controls.Add(this.label10);
            this.pnlSimDataResults.Controls.Add(this.label9);
            this.pnlSimDataResults.Controls.Add(this.lblSDRClosedPositionsCount);
            this.pnlSimDataResults.Controls.Add(this.label8);
            this.pnlSimDataResults.Controls.Add(this.lblSDRStopTS);
            this.pnlSimDataResults.Controls.Add(this.lblSDRStartTS);
            this.pnlSimDataResults.Controls.Add(this.label12);
            this.pnlSimDataResults.Controls.Add(this.label28);
            this.pnlSimDataResults.Controls.Add(this.lblSDRFinalValueOnLastTick);
            this.pnlSimDataResults.Controls.Add(this.lblSDRFinalValueOnClosedPositions);
            this.pnlSimDataResults.Controls.Add(this.lblSDRInitialValue);
            this.pnlSimDataResults.Controls.Add(this.label6);
            this.pnlSimDataResults.Controls.Add(this.label5);
            this.pnlSimDataResults.Controls.Add(this.label4);
            this.pnlSimDataResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSimDataResults.Location = new System.Drawing.Point(3, 3);
            this.pnlSimDataResults.Name = "pnlSimDataResults";
            this.pnlSimDataResults.Size = new System.Drawing.Size(933, 271);
            this.pnlSimDataResults.TabIndex = 0;
            // 
            // lblSDRRProfitAvgToStdDev
            // 
            this.lblSDRRProfitAvgToStdDev.AutoSize = true;
            this.lblSDRRProfitAvgToStdDev.Location = new System.Drawing.Point(401, 202);
            this.lblSDRRProfitAvgToStdDev.Name = "lblSDRRProfitAvgToStdDev";
            this.lblSDRRProfitAvgToStdDev.Size = new System.Drawing.Size(26, 13);
            this.lblSDRRProfitAvgToStdDev.TabIndex = 77;
            this.lblSDRRProfitAvgToStdDev.Text = "Avg";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(248, 202);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(105, 13);
            this.label49.TabIndex = 76;
            this.label49.Text = "R profit avg / stddev";
            // 
            // lblSDRRProfitStdDev
            // 
            this.lblSDRRProfitStdDev.AutoSize = true;
            this.lblSDRRProfitStdDev.Location = new System.Drawing.Point(401, 186);
            this.lblSDRRProfitStdDev.Name = "lblSDRRProfitStdDev";
            this.lblSDRRProfitStdDev.Size = new System.Drawing.Size(26, 13);
            this.lblSDRRProfitStdDev.TabIndex = 75;
            this.lblSDRRProfitStdDev.Text = "Avg";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(248, 186);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(76, 13);
            this.label46.TabIndex = 74;
            this.label46.Text = "R profit stddev";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(248, 106);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(69, 13);
            this.label39.TabIndex = 73;
            this.label39.Text = "Avg win pcnt";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(248, 122);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(71, 13);
            this.label42.TabIndex = 71;
            this.label42.Text = "Avg loss pcnt";
            // 
            // lblSDRTransactionsPerYear
            // 
            this.lblSDRTransactionsPerYear.AutoSize = true;
            this.lblSDRTransactionsPerYear.Location = new System.Drawing.Point(146, 138);
            this.lblSDRTransactionsPerYear.Name = "lblSDRTransactionsPerYear";
            this.lblSDRTransactionsPerYear.Size = new System.Drawing.Size(41, 13);
            this.lblSDRTransactionsPerYear.TabIndex = 70;
            this.lblSDRTransactionsPerYear.Text = "label12";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(12, 138);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(109, 13);
            this.label37.TabIndex = 69;
            this.label37.Text = "Transactions per year";
            // 
            // lblSDRRProfitAvg
            // 
            this.lblSDRRProfitAvg.AutoSize = true;
            this.lblSDRRProfitAvg.Location = new System.Drawing.Point(401, 170);
            this.lblSDRRProfitAvg.Name = "lblSDRRProfitAvg";
            this.lblSDRRProfitAvg.Size = new System.Drawing.Size(26, 13);
            this.lblSDRRProfitAvg.TabIndex = 68;
            this.lblSDRRProfitAvg.Text = "Avg";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(248, 170);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(65, 13);
            this.label35.TabIndex = 67;
            this.label35.Text = "R profit avg.";
            // 
            // lblSDRAvgPcntLoss
            // 
            this.lblSDRAvgPcntLoss.AutoSize = true;
            this.lblSDRAvgPcntLoss.Location = new System.Drawing.Point(401, 122);
            this.lblSDRAvgPcntLoss.Name = "lblSDRAvgPcntLoss";
            this.lblSDRAvgPcntLoss.Size = new System.Drawing.Size(26, 13);
            this.lblSDRAvgPcntLoss.TabIndex = 66;
            this.lblSDRAvgPcntLoss.Text = "Avg";
            // 
            // lblSDRAvgPcntWin
            // 
            this.lblSDRAvgPcntWin.AutoSize = true;
            this.lblSDRAvgPcntWin.Location = new System.Drawing.Point(401, 106);
            this.lblSDRAvgPcntWin.Name = "lblSDRAvgPcntWin";
            this.lblSDRAvgPcntWin.Size = new System.Drawing.Size(26, 13);
            this.lblSDRAvgPcntWin.TabIndex = 65;
            this.lblSDRAvgPcntWin.Text = "Avg";
            // 
            // lblSDREqDistrStdDev
            // 
            this.lblSDREqDistrStdDev.AutoSize = true;
            this.lblSDREqDistrStdDev.Location = new System.Drawing.Point(146, 180);
            this.lblSDREqDistrStdDev.Name = "lblSDREqDistrStdDev";
            this.lblSDREqDistrStdDev.Size = new System.Drawing.Size(26, 13);
            this.lblSDREqDistrStdDev.TabIndex = 64;
            this.lblSDREqDistrStdDev.Text = "Avg";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(12, 180);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(134, 13);
            this.label25.TabIndex = 63;
            this.label25.Text = "Equity pcnt diff distr stddev";
            // 
            // lblSDREqDistrAvg
            // 
            this.lblSDREqDistrAvg.AutoSize = true;
            this.lblSDREqDistrAvg.Location = new System.Drawing.Point(146, 164);
            this.lblSDREqDistrAvg.Name = "lblSDREqDistrAvg";
            this.lblSDREqDistrAvg.Size = new System.Drawing.Size(26, 13);
            this.lblSDREqDistrAvg.TabIndex = 62;
            this.lblSDREqDistrAvg.Text = "Avg";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 164);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(120, 13);
            this.label27.TabIndex = 61;
            this.label27.Text = "Equity pcnt diff distr avg";
            // 
            // lblSDRProfitPcntOnTicks
            // 
            this.lblSDRProfitPcntOnTicks.AutoSize = true;
            this.lblSDRProfitPcntOnTicks.Location = new System.Drawing.Point(146, 122);
            this.lblSDRProfitPcntOnTicks.Name = "lblSDRProfitPcntOnTicks";
            this.lblSDRProfitPcntOnTicks.Size = new System.Drawing.Size(41, 13);
            this.lblSDRProfitPcntOnTicks.TabIndex = 60;
            this.lblSDRProfitPcntOnTicks.Text = "label12";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(12, 122);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(95, 13);
            this.label24.TabIndex = 59;
            this.label24.Text = "Cumm. yearly profit";
            // 
            // lblSDRExpectedUnitReturn
            // 
            this.lblSDRExpectedUnitReturn.AutoSize = true;
            this.lblSDRExpectedUnitReturn.Location = new System.Drawing.Point(401, 138);
            this.lblSDRExpectedUnitReturn.Name = "lblSDRExpectedUnitReturn";
            this.lblSDRExpectedUnitReturn.Size = new System.Drawing.Size(26, 13);
            this.lblSDRExpectedUnitReturn.TabIndex = 58;
            this.lblSDRExpectedUnitReturn.Text = "Avg";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(248, 138);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(102, 13);
            this.label22.TabIndex = 57;
            this.label22.Text = "Expected unit return";
            // 
            // lblSDRMaxDDOnPositions
            // 
            this.lblSDRMaxDDOnPositions.AutoSize = true;
            this.lblSDRMaxDDOnPositions.Location = new System.Drawing.Point(644, 26);
            this.lblSDRMaxDDOnPositions.Name = "lblSDRMaxDDOnPositions";
            this.lblSDRMaxDDOnPositions.Size = new System.Drawing.Size(26, 13);
            this.lblSDRMaxDDOnPositions.TabIndex = 56;
            this.lblSDRMaxDDOnPositions.Text = "Avg";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(513, 26);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(105, 13);
            this.label23.TabIndex = 55;
            this.label23.Text = "Max DD on positions";
            // 
            // lblSDRMaxDDOnTicks
            // 
            this.lblSDRMaxDDOnTicks.AutoSize = true;
            this.lblSDRMaxDDOnTicks.Location = new System.Drawing.Point(644, 10);
            this.lblSDRMaxDDOnTicks.Name = "lblSDRMaxDDOnTicks";
            this.lblSDRMaxDDOnTicks.Size = new System.Drawing.Size(26, 13);
            this.lblSDRMaxDDOnTicks.TabIndex = 54;
            this.lblSDRMaxDDOnTicks.Text = "Avg";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(513, 10);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(86, 13);
            this.label21.TabIndex = 53;
            this.label21.Text = "Max DD on ticks";
            // 
            // lblSDRProcessedTicks
            // 
            this.lblSDRProcessedTicks.AutoSize = true;
            this.lblSDRProcessedTicks.Location = new System.Drawing.Point(146, 42);
            this.lblSDRProcessedTicks.Name = "lblSDRProcessedTicks";
            this.lblSDRProcessedTicks.Size = new System.Drawing.Size(41, 13);
            this.lblSDRProcessedTicks.TabIndex = 52;
            this.lblSDRProcessedTicks.Text = "label11";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 42);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 13);
            this.label20.TabIndex = 51;
            this.label20.Text = "Processed ticks";
            // 
            // lblSDRExpectedPositionValue
            // 
            this.lblSDRExpectedPositionValue.AutoSize = true;
            this.lblSDRExpectedPositionValue.Location = new System.Drawing.Point(401, 154);
            this.lblSDRExpectedPositionValue.Name = "lblSDRExpectedPositionValue";
            this.lblSDRExpectedPositionValue.Size = new System.Drawing.Size(26, 13);
            this.lblSDRExpectedPositionValue.TabIndex = 50;
            this.lblSDRExpectedPositionValue.Text = "Avg";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(248, 154);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(120, 13);
            this.label19.TabIndex = 49;
            this.label19.Text = "Expected position value";
            // 
            // lblSDRAvgWinLossRatio
            // 
            this.lblSDRAvgWinLossRatio.AutoSize = true;
            this.lblSDRAvgWinLossRatio.Location = new System.Drawing.Point(401, 90);
            this.lblSDRAvgWinLossRatio.Name = "lblSDRAvgWinLossRatio";
            this.lblSDRAvgWinLossRatio.Size = new System.Drawing.Size(26, 13);
            this.lblSDRAvgWinLossRatio.TabIndex = 48;
            this.lblSDRAvgWinLossRatio.Text = "Avg";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(248, 90);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 47;
            this.label14.Text = "Avg W/L ratio";
            // 
            // lblSDRLossProbability
            // 
            this.lblSDRLossProbability.AutoSize = true;
            this.lblSDRLossProbability.Location = new System.Drawing.Point(401, 42);
            this.lblSDRLossProbability.Name = "lblSDRLossProbability";
            this.lblSDRLossProbability.Size = new System.Drawing.Size(26, 13);
            this.lblSDRLossProbability.TabIndex = 46;
            this.lblSDRLossProbability.Text = "Avg";
            // 
            // lblSDRWinProbability
            // 
            this.lblSDRWinProbability.AutoSize = true;
            this.lblSDRWinProbability.Location = new System.Drawing.Point(401, 26);
            this.lblSDRWinProbability.Name = "lblSDRWinProbability";
            this.lblSDRWinProbability.Size = new System.Drawing.Size(26, 13);
            this.lblSDRWinProbability.TabIndex = 45;
            this.lblSDRWinProbability.Text = "Avg";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(369, 42);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 13);
            this.label17.TabIndex = 44;
            this.label17.Text = "Prob.";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(369, 26);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(32, 13);
            this.label18.TabIndex = 43;
            this.label18.Text = "Prob.";
            // 
            // lblSDRSumLosses
            // 
            this.lblSDRSumLosses.AutoSize = true;
            this.lblSDRSumLosses.Location = new System.Drawing.Point(306, 74);
            this.lblSDRSumLosses.Name = "lblSDRSumLosses";
            this.lblSDRSumLosses.Size = new System.Drawing.Size(26, 13);
            this.lblSDRSumLosses.TabIndex = 42;
            this.lblSDRSumLosses.Text = "Avg";
            // 
            // lblSDRSumWins
            // 
            this.lblSDRSumWins.AutoSize = true;
            this.lblSDRSumWins.Location = new System.Drawing.Point(306, 58);
            this.lblSDRSumWins.Name = "lblSDRSumWins";
            this.lblSDRSumWins.Size = new System.Drawing.Size(26, 13);
            this.lblSDRSumWins.TabIndex = 41;
            this.lblSDRSumWins.Text = "Avg";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(248, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "Sum losses";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(248, 58);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 13);
            this.label16.TabIndex = 39;
            this.label16.Text = "Sum wins";
            // 
            // lblSDRAvgLoss
            // 
            this.lblSDRAvgLoss.AutoSize = true;
            this.lblSDRAvgLoss.Location = new System.Drawing.Point(401, 74);
            this.lblSDRAvgLoss.Name = "lblSDRAvgLoss";
            this.lblSDRAvgLoss.Size = new System.Drawing.Size(26, 13);
            this.lblSDRAvgLoss.TabIndex = 38;
            this.lblSDRAvgLoss.Text = "Avg";
            // 
            // lblSDRAvgWin
            // 
            this.lblSDRAvgWin.AutoSize = true;
            this.lblSDRAvgWin.Location = new System.Drawing.Point(401, 58);
            this.lblSDRAvgWin.Name = "lblSDRAvgWin";
            this.lblSDRAvgWin.Size = new System.Drawing.Size(26, 13);
            this.lblSDRAvgWin.TabIndex = 37;
            this.lblSDRAvgWin.Text = "Avg";
            // 
            // lblSDRLosses
            // 
            this.lblSDRLosses.AutoSize = true;
            this.lblSDRLosses.Location = new System.Drawing.Point(306, 42);
            this.lblSDRLosses.Name = "lblSDRLosses";
            this.lblSDRLosses.Size = new System.Drawing.Size(40, 13);
            this.lblSDRLosses.TabIndex = 36;
            this.lblSDRLosses.Text = "Losses";
            // 
            // lblSDRWins
            // 
            this.lblSDRWins.AutoSize = true;
            this.lblSDRWins.Location = new System.Drawing.Point(306, 26);
            this.lblSDRWins.Name = "lblSDRWins";
            this.lblSDRWins.Size = new System.Drawing.Size(31, 13);
            this.lblSDRWins.TabIndex = 35;
            this.lblSDRWins.Text = "Wins";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(369, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Avg.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(369, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Avg.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(248, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Losses";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(248, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Wins";
            // 
            // lblSDRClosedPositionsCount
            // 
            this.lblSDRClosedPositionsCount.AutoSize = true;
            this.lblSDRClosedPositionsCount.Location = new System.Drawing.Point(401, 10);
            this.lblSDRClosedPositionsCount.Name = "lblSDRClosedPositionsCount";
            this.lblSDRClosedPositionsCount.Size = new System.Drawing.Size(41, 13);
            this.lblSDRClosedPositionsCount.TabIndex = 29;
            this.lblSDRClosedPositionsCount.Text = "label12";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(248, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Closed positions";
            // 
            // lblSDRStopTS
            // 
            this.lblSDRStopTS.AutoSize = true;
            this.lblSDRStopTS.Location = new System.Drawing.Point(146, 26);
            this.lblSDRStopTS.Name = "lblSDRStopTS";
            this.lblSDRStopTS.Size = new System.Drawing.Size(41, 13);
            this.lblSDRStopTS.TabIndex = 27;
            this.lblSDRStopTS.Text = "label11";
            // 
            // lblSDRStartTS
            // 
            this.lblSDRStartTS.AutoSize = true;
            this.lblSDRStartTS.Location = new System.Drawing.Point(146, 10);
            this.lblSDRStartTS.Name = "lblSDRStartTS";
            this.lblSDRStartTS.Size = new System.Drawing.Size(41, 13);
            this.lblSDRStartTS.TabIndex = 26;
            this.lblSDRStartTS.Text = "label10";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Stop TS";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(12, 10);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(46, 13);
            this.label28.TabIndex = 24;
            this.label28.Text = "Start TS";
            // 
            // lblSDRFinalValueOnLastTick
            // 
            this.lblSDRFinalValueOnLastTick.AutoSize = true;
            this.lblSDRFinalValueOnLastTick.Location = new System.Drawing.Point(146, 106);
            this.lblSDRFinalValueOnLastTick.Name = "lblSDRFinalValueOnLastTick";
            this.lblSDRFinalValueOnLastTick.Size = new System.Drawing.Size(41, 13);
            this.lblSDRFinalValueOnLastTick.TabIndex = 8;
            this.lblSDRFinalValueOnLastTick.Text = "label12";
            // 
            // lblSDRFinalValueOnClosedPositions
            // 
            this.lblSDRFinalValueOnClosedPositions.AutoSize = true;
            this.lblSDRFinalValueOnClosedPositions.Location = new System.Drawing.Point(146, 90);
            this.lblSDRFinalValueOnClosedPositions.Name = "lblSDRFinalValueOnClosedPositions";
            this.lblSDRFinalValueOnClosedPositions.Size = new System.Drawing.Size(41, 13);
            this.lblSDRFinalValueOnClosedPositions.TabIndex = 7;
            this.lblSDRFinalValueOnClosedPositions.Text = "label11";
            // 
            // lblSDRInitialValue
            // 
            this.lblSDRInitialValue.AutoSize = true;
            this.lblSDRInitialValue.Location = new System.Drawing.Point(146, 74);
            this.lblSDRInitialValue.Name = "lblSDRInitialValue";
            this.lblSDRInitialValue.Size = new System.Drawing.Size(41, 13);
            this.lblSDRInitialValue.TabIndex = 6;
            this.lblSDRInitialValue.Text = "label10";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Final value on last tick";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Final value on positions";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Initial value";
            // 
            // tabSimDataPositions
            // 
            this.tabSimDataPositions.Controls.Add(this.dbgPositions);
            this.tabSimDataPositions.Location = new System.Drawing.Point(4, 22);
            this.tabSimDataPositions.Name = "tabSimDataPositions";
            this.tabSimDataPositions.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimDataPositions.Size = new System.Drawing.Size(939, 277);
            this.tabSimDataPositions.TabIndex = 1;
            this.tabSimDataPositions.Text = "Positions";
            this.tabSimDataPositions.UseVisualStyleBackColor = true;
            // 
            // dbgPositions
            // 
            this.dbgPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbgPositions.Location = new System.Drawing.Point(3, 3);
            this.dbgPositions.Name = "dbgPositions";
            this.dbgPositions.Size = new System.Drawing.Size(933, 271);
            this.dbgPositions.TabIndex = 0;
            // 
            // tabSimDataDrawdowns
            // 
            this.tabSimDataDrawdowns.Controls.Add(this.splitDrawDowns);
            this.tabSimDataDrawdowns.Location = new System.Drawing.Point(4, 22);
            this.tabSimDataDrawdowns.Name = "tabSimDataDrawdowns";
            this.tabSimDataDrawdowns.Size = new System.Drawing.Size(939, 277);
            this.tabSimDataDrawdowns.TabIndex = 2;
            this.tabSimDataDrawdowns.Text = "Drawdowns";
            this.tabSimDataDrawdowns.UseVisualStyleBackColor = true;
            // 
            // splitDrawDowns
            // 
            this.splitDrawDowns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDrawDowns.IsSplitterFixed = true;
            this.splitDrawDowns.Location = new System.Drawing.Point(0, 0);
            this.splitDrawDowns.Name = "splitDrawDowns";
            // 
            // splitDrawDowns.Panel1
            // 
            this.splitDrawDowns.Panel1.Controls.Add(this.gbDDTicks);
            this.splitDrawDowns.Panel1.Padding = new System.Windows.Forms.Padding(1);
            // 
            // splitDrawDowns.Panel2
            // 
            this.splitDrawDowns.Panel2.Controls.Add(this.gbDDPositions);
            this.splitDrawDowns.Panel2.Padding = new System.Windows.Forms.Padding(1);
            this.splitDrawDowns.Size = new System.Drawing.Size(939, 277);
            this.splitDrawDowns.SplitterDistance = 470;
            this.splitDrawDowns.TabIndex = 4;
            // 
            // gbDDTicks
            // 
            this.gbDDTicks.Controls.Add(this.chartDDTicks);
            this.gbDDTicks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDDTicks.Location = new System.Drawing.Point(1, 1);
            this.gbDDTicks.Name = "gbDDTicks";
            this.gbDDTicks.Size = new System.Drawing.Size(468, 275);
            this.gbDDTicks.TabIndex = 2;
            this.gbDDTicks.TabStop = false;
            this.gbDDTicks.Text = "On ticks (length / percentage)";
            // 
            // chartDDTicks
            // 
            this.chartDDTicks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartDDTicks.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartDDTicks.Location = new System.Drawing.Point(3, 16);
            this.chartDDTicks.Name = "chartDDTicks";
            this.chartDDTicks.Size = new System.Drawing.Size(462, 181);
            this.chartDDTicks.TabIndex = 0;
            // 
            // gbDDPositions
            // 
            this.gbDDPositions.Controls.Add(this.chartDDPositions);
            this.gbDDPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDDPositions.Location = new System.Drawing.Point(1, 1);
            this.gbDDPositions.Name = "gbDDPositions";
            this.gbDDPositions.Size = new System.Drawing.Size(463, 275);
            this.gbDDPositions.TabIndex = 3;
            this.gbDDPositions.TabStop = false;
            this.gbDDPositions.Text = "On positions (length / percentage)";
            // 
            // chartDDPositions
            // 
            this.chartDDPositions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartDDPositions.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartDDPositions.Location = new System.Drawing.Point(3, 16);
            this.chartDDPositions.Name = "chartDDPositions";
            this.chartDDPositions.Size = new System.Drawing.Size(457, 181);
            this.chartDDPositions.TabIndex = 1;
            // 
            // tabSimR
            // 
            this.tabSimR.Controls.Add(this.splitR);
            this.tabSimR.Location = new System.Drawing.Point(4, 22);
            this.tabSimR.Name = "tabSimR";
            this.tabSimR.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimR.Size = new System.Drawing.Size(939, 277);
            this.tabSimR.TabIndex = 6;
            this.tabSimR.Text = "R";
            this.tabSimR.UseVisualStyleBackColor = true;
            // 
            // splitR
            // 
            this.splitR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitR.IsSplitterFixed = true;
            this.splitR.Location = new System.Drawing.Point(3, 3);
            this.splitR.Name = "splitR";
            // 
            // splitR.Panel1
            // 
            this.splitR.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitR.Panel2
            // 
            this.splitR.Panel2.Controls.Add(this.groupBox2);
            this.splitR.Size = new System.Drawing.Size(933, 271);
            this.splitR.SplitterDistance = 469;
            this.splitR.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chartRValue);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 271);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ticks length / R profit";
            // 
            // chartRValue
            // 
            this.chartRValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartRValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartRValue.Location = new System.Drawing.Point(3, 16);
            this.chartRValue.Name = "chartRValue";
            this.chartRValue.Size = new System.Drawing.Size(463, 181);
            this.chartRValue.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chartRDistribution);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 271);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "R profit distribution";
            // 
            // chartRDistribution
            // 
            this.chartRDistribution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartRDistribution.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartRDistribution.Location = new System.Drawing.Point(3, 16);
            this.chartRDistribution.Name = "chartRDistribution";
            this.chartRDistribution.Size = new System.Drawing.Size(454, 181);
            this.chartRDistribution.TabIndex = 0;
            // 
            // tabSimDataProfits
            // 
            this.tabSimDataProfits.Controls.Add(this.splitProfits);
            this.tabSimDataProfits.Location = new System.Drawing.Point(4, 22);
            this.tabSimDataProfits.Name = "tabSimDataProfits";
            this.tabSimDataProfits.Size = new System.Drawing.Size(939, 277);
            this.tabSimDataProfits.TabIndex = 3;
            this.tabSimDataProfits.Text = "Profits";
            this.tabSimDataProfits.UseVisualStyleBackColor = true;
            // 
            // splitProfits
            // 
            this.splitProfits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitProfits.IsSplitterFixed = true;
            this.splitProfits.Location = new System.Drawing.Point(0, 0);
            this.splitProfits.Name = "splitProfits";
            // 
            // splitProfits.Panel1
            // 
            this.splitProfits.Panel1.Controls.Add(this.gbProfitsValue);
            // 
            // splitProfits.Panel2
            // 
            this.splitProfits.Panel2.Controls.Add(this.gbProfitPcnt);
            this.splitProfits.Size = new System.Drawing.Size(939, 277);
            this.splitProfits.SplitterDistance = 473;
            this.splitProfits.TabIndex = 0;
            // 
            // gbProfitsValue
            // 
            this.gbProfitsValue.Controls.Add(this.chartProfitValue);
            this.gbProfitsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbProfitsValue.Location = new System.Drawing.Point(0, 0);
            this.gbProfitsValue.Name = "gbProfitsValue";
            this.gbProfitsValue.Size = new System.Drawing.Size(473, 277);
            this.gbProfitsValue.TabIndex = 3;
            this.gbProfitsValue.TabStop = false;
            this.gbProfitsValue.Text = "Ticks length / Value";
            // 
            // chartProfitValue
            // 
            this.chartProfitValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartProfitValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartProfitValue.Location = new System.Drawing.Point(3, 16);
            this.chartProfitValue.Name = "chartProfitValue";
            this.chartProfitValue.Size = new System.Drawing.Size(467, 181);
            this.chartProfitValue.TabIndex = 0;
            // 
            // gbProfitPcnt
            // 
            this.gbProfitPcnt.Controls.Add(this.chartProfitPcnt);
            this.gbProfitPcnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbProfitPcnt.Location = new System.Drawing.Point(0, 0);
            this.gbProfitPcnt.Name = "gbProfitPcnt";
            this.gbProfitPcnt.Size = new System.Drawing.Size(462, 277);
            this.gbProfitPcnt.TabIndex = 3;
            this.gbProfitPcnt.TabStop = false;
            this.gbProfitPcnt.Text = "Ticks length / Percentage";
            // 
            // chartProfitPcnt
            // 
            this.chartProfitPcnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartProfitPcnt.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartProfitPcnt.Location = new System.Drawing.Point(3, 16);
            this.chartProfitPcnt.Name = "chartProfitPcnt";
            this.chartProfitPcnt.Size = new System.Drawing.Size(456, 181);
            this.chartProfitPcnt.TabIndex = 0;
            // 
            // tabSimDataLog
            // 
            this.tabSimDataLog.Controls.Add(this.edtSimDataLog);
            this.tabSimDataLog.Location = new System.Drawing.Point(4, 22);
            this.tabSimDataLog.Name = "tabSimDataLog";
            this.tabSimDataLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimDataLog.Size = new System.Drawing.Size(939, 277);
            this.tabSimDataLog.TabIndex = 4;
            this.tabSimDataLog.Text = "Log";
            this.tabSimDataLog.UseVisualStyleBackColor = true;
            // 
            // edtSimDataLog
            // 
            this.edtSimDataLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtSimDataLog.Location = new System.Drawing.Point(3, 3);
            this.edtSimDataLog.Multiline = true;
            this.edtSimDataLog.Name = "edtSimDataLog";
            this.edtSimDataLog.ReadOnly = true;
            this.edtSimDataLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.edtSimDataLog.Size = new System.Drawing.Size(933, 271);
            this.edtSimDataLog.TabIndex = 0;
            this.edtSimDataLog.WordWrap = false;
            // 
            // tabSimMonteCarlo
            // 
            this.tabSimMonteCarlo.Controls.Add(this.panel2);
            this.tabSimMonteCarlo.Controls.Add(this.panel1);
            this.tabSimMonteCarlo.Location = new System.Drawing.Point(4, 22);
            this.tabSimMonteCarlo.Name = "tabSimMonteCarlo";
            this.tabSimMonteCarlo.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimMonteCarlo.Size = new System.Drawing.Size(939, 277);
            this.tabSimMonteCarlo.TabIndex = 5;
            this.tabSimMonteCarlo.Text = "Monte Carlo";
            this.tabSimMonteCarlo.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(207, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(729, 271);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chartMonteCarloData);
            this.panel4.Controls.Add(this.gbMonteCarloStreaksLosing);
            this.panel4.Controls.Add(this.gbMonteCarloStreaksWinning);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(729, 178);
            this.panel4.TabIndex = 2;
            // 
            // chartMonteCarloData
            // 
            this.chartMonteCarloData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMonteCarloData.Location = new System.Drawing.Point(0, 0);
            this.chartMonteCarloData.Name = "chartMonteCarloData";
            this.chartMonteCarloData.Size = new System.Drawing.Size(473, 178);
            this.chartMonteCarloData.TabIndex = 1;
            // 
            // gbMonteCarloStreaksLosing
            // 
            this.gbMonteCarloStreaksLosing.Controls.Add(this.chartMonteCarloStreaksLosing);
            this.gbMonteCarloStreaksLosing.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbMonteCarloStreaksLosing.Location = new System.Drawing.Point(473, 0);
            this.gbMonteCarloStreaksLosing.Name = "gbMonteCarloStreaksLosing";
            this.gbMonteCarloStreaksLosing.Size = new System.Drawing.Size(128, 178);
            this.gbMonteCarloStreaksLosing.TabIndex = 2;
            this.gbMonteCarloStreaksLosing.TabStop = false;
            this.gbMonteCarloStreaksLosing.Text = "Losing streaks";
            // 
            // chartMonteCarloStreaksLosing
            // 
            this.chartMonteCarloStreaksLosing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMonteCarloStreaksLosing.Location = new System.Drawing.Point(3, 16);
            this.chartMonteCarloStreaksLosing.Name = "chartMonteCarloStreaksLosing";
            this.chartMonteCarloStreaksLosing.Size = new System.Drawing.Size(122, 159);
            this.chartMonteCarloStreaksLosing.TabIndex = 0;
            // 
            // gbMonteCarloStreaksWinning
            // 
            this.gbMonteCarloStreaksWinning.Controls.Add(this.chartMonteCarloStreaksWinning);
            this.gbMonteCarloStreaksWinning.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbMonteCarloStreaksWinning.Location = new System.Drawing.Point(601, 0);
            this.gbMonteCarloStreaksWinning.Name = "gbMonteCarloStreaksWinning";
            this.gbMonteCarloStreaksWinning.Size = new System.Drawing.Size(128, 178);
            this.gbMonteCarloStreaksWinning.TabIndex = 3;
            this.gbMonteCarloStreaksWinning.TabStop = false;
            this.gbMonteCarloStreaksWinning.Text = "Winning streaks";
            // 
            // chartMonteCarloStreaksWinning
            // 
            this.chartMonteCarloStreaksWinning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMonteCarloStreaksWinning.Location = new System.Drawing.Point(3, 16);
            this.chartMonteCarloStreaksWinning.Name = "chartMonteCarloStreaksWinning";
            this.chartMonteCarloStreaksWinning.Size = new System.Drawing.Size(122, 159);
            this.chartMonteCarloStreaksWinning.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblMonteCarloSimLongestWinningStreak);
            this.panel3.Controls.Add(this.label44);
            this.panel3.Controls.Add(this.lblMonteCarloSimLongestLosingStreak);
            this.panel3.Controls.Add(this.label47);
            this.panel3.Controls.Add(this.lblMonteCarloSimLongestDD);
            this.panel3.Controls.Add(this.label43);
            this.panel3.Controls.Add(this.lblMonteCarloSimMaxDD);
            this.panel3.Controls.Add(this.label45);
            this.panel3.Controls.Add(this.lblMonteCarloSimAvg);
            this.panel3.Controls.Add(this.label40);
            this.panel3.Controls.Add(this.lblMonteCarloSimWorst);
            this.panel3.Controls.Add(this.label38);
            this.panel3.Controls.Add(this.lblMonteCarloSimBest);
            this.panel3.Controls.Add(this.label36);
            this.panel3.Controls.Add(this.lblMonteCarloSimLosses);
            this.panel3.Controls.Add(this.label34);
            this.panel3.Controls.Add(this.lblMonteCarloSimWins);
            this.panel3.Controls.Add(this.label32);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 178);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(729, 93);
            this.panel3.TabIndex = 0;
            // 
            // lblMonteCarloSimLongestWinningStreak
            // 
            this.lblMonteCarloSimLongestWinningStreak.AutoSize = true;
            this.lblMonteCarloSimLongestWinningStreak.Location = new System.Drawing.Point(326, 56);
            this.lblMonteCarloSimLongestWinningStreak.Name = "lblMonteCarloSimLongestWinningStreak";
            this.lblMonteCarloSimLongestWinningStreak.Size = new System.Drawing.Size(34, 13);
            this.lblMonteCarloSimLongestWinningStreak.TabIndex = 43;
            this.lblMonteCarloSimLongestWinningStreak.Text = "Wins:";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(210, 56);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(119, 13);
            this.label44.TabIndex = 42;
            this.label44.Text = "Longest winning streak:";
            // 
            // lblMonteCarloSimLongestLosingStreak
            // 
            this.lblMonteCarloSimLongestLosingStreak.AutoSize = true;
            this.lblMonteCarloSimLongestLosingStreak.Location = new System.Drawing.Point(326, 43);
            this.lblMonteCarloSimLongestLosingStreak.Name = "lblMonteCarloSimLongestLosingStreak";
            this.lblMonteCarloSimLongestLosingStreak.Size = new System.Drawing.Size(34, 13);
            this.lblMonteCarloSimLongestLosingStreak.TabIndex = 41;
            this.lblMonteCarloSimLongestLosingStreak.Text = "Wins:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(210, 43);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(110, 13);
            this.label47.TabIndex = 40;
            this.label47.Text = "Longest losing streak:";
            // 
            // lblMonteCarloSimLongestDD
            // 
            this.lblMonteCarloSimLongestDD.AutoSize = true;
            this.lblMonteCarloSimLongestDD.Location = new System.Drawing.Point(274, 21);
            this.lblMonteCarloSimLongestDD.Name = "lblMonteCarloSimLongestDD";
            this.lblMonteCarloSimLongestDD.Size = new System.Drawing.Size(34, 13);
            this.lblMonteCarloSimLongestDD.TabIndex = 39;
            this.lblMonteCarloSimLongestDD.Text = "Wins:";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(210, 21);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(67, 13);
            this.label43.TabIndex = 38;
            this.label43.Text = "Longest DD:";
            // 
            // lblMonteCarloSimMaxDD
            // 
            this.lblMonteCarloSimMaxDD.AutoSize = true;
            this.lblMonteCarloSimMaxDD.Location = new System.Drawing.Point(274, 8);
            this.lblMonteCarloSimMaxDD.Name = "lblMonteCarloSimMaxDD";
            this.lblMonteCarloSimMaxDD.Size = new System.Drawing.Size(34, 13);
            this.lblMonteCarloSimMaxDD.TabIndex = 37;
            this.lblMonteCarloSimMaxDD.Text = "Wins:";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(210, 8);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(49, 13);
            this.label45.TabIndex = 36;
            this.label45.Text = "Max DD:";
            // 
            // lblMonteCarloSimAvg
            // 
            this.lblMonteCarloSimAvg.AutoSize = true;
            this.lblMonteCarloSimAvg.Location = new System.Drawing.Point(55, 69);
            this.lblMonteCarloSimAvg.Name = "lblMonteCarloSimAvg";
            this.lblMonteCarloSimAvg.Size = new System.Drawing.Size(34, 13);
            this.lblMonteCarloSimAvg.TabIndex = 35;
            this.lblMonteCarloSimAvg.Text = "Wins:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 69);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(29, 13);
            this.label40.TabIndex = 34;
            this.label40.Text = "Avg:";
            // 
            // lblMonteCarloSimWorst
            // 
            this.lblMonteCarloSimWorst.AutoSize = true;
            this.lblMonteCarloSimWorst.Location = new System.Drawing.Point(55, 56);
            this.lblMonteCarloSimWorst.Name = "lblMonteCarloSimWorst";
            this.lblMonteCarloSimWorst.Size = new System.Drawing.Size(34, 13);
            this.lblMonteCarloSimWorst.TabIndex = 33;
            this.lblMonteCarloSimWorst.Text = "Wins:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 56);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(38, 13);
            this.label38.TabIndex = 32;
            this.label38.Text = "Worst:";
            // 
            // lblMonteCarloSimBest
            // 
            this.lblMonteCarloSimBest.AutoSize = true;
            this.lblMonteCarloSimBest.Location = new System.Drawing.Point(55, 43);
            this.lblMonteCarloSimBest.Name = "lblMonteCarloSimBest";
            this.lblMonteCarloSimBest.Size = new System.Drawing.Size(34, 13);
            this.lblMonteCarloSimBest.TabIndex = 31;
            this.lblMonteCarloSimBest.Text = "Wins:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 43);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(31, 13);
            this.label36.TabIndex = 30;
            this.label36.Text = "Best:";
            // 
            // lblMonteCarloSimLosses
            // 
            this.lblMonteCarloSimLosses.AutoSize = true;
            this.lblMonteCarloSimLosses.Location = new System.Drawing.Point(55, 21);
            this.lblMonteCarloSimLosses.Name = "lblMonteCarloSimLosses";
            this.lblMonteCarloSimLosses.Size = new System.Drawing.Size(34, 13);
            this.lblMonteCarloSimLosses.TabIndex = 29;
            this.lblMonteCarloSimLosses.Text = "Wins:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 21);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(43, 13);
            this.label34.TabIndex = 28;
            this.label34.Text = "Losses:";
            // 
            // lblMonteCarloSimWins
            // 
            this.lblMonteCarloSimWins.AutoSize = true;
            this.lblMonteCarloSimWins.Location = new System.Drawing.Point(55, 8);
            this.lblMonteCarloSimWins.Name = "lblMonteCarloSimWins";
            this.lblMonteCarloSimWins.Size = new System.Drawing.Size(34, 13);
            this.lblMonteCarloSimWins.TabIndex = 27;
            this.lblMonteCarloSimWins.Text = "Wins:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 8);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(34, 13);
            this.label32.TabIndex = 26;
            this.label32.Text = "Wins:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.edtMonteCarloTransactionsPerYear);
            this.panel1.Controls.Add(this.label33);
            this.panel1.Controls.Add(this.btnMonteCarloSim);
            this.panel1.Controls.Add(this.edtMonteCarloAvgPcntLoss);
            this.panel1.Controls.Add(this.edtMonteCarloAvgPcntWin);
            this.panel1.Controls.Add(this.edtMonteCarloWinProb);
            this.panel1.Controls.Add(this.edtMonteCarloLength);
            this.panel1.Controls.Add(this.edtMonteCarloCount);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 271);
            this.panel1.TabIndex = 0;
            // 
            // edtMonteCarloTransactionsPerYear
            // 
            this.edtMonteCarloTransactionsPerYear.Location = new System.Drawing.Point(108, 126);
            this.edtMonteCarloTransactionsPerYear.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.edtMonteCarloTransactionsPerYear.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtMonteCarloTransactionsPerYear.Name = "edtMonteCarloTransactionsPerYear";
            this.edtMonteCarloTransactionsPerYear.Size = new System.Drawing.Size(79, 20);
            this.edtMonteCarloTransactionsPerYear.TabIndex = 35;
            this.edtMonteCarloTransactionsPerYear.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(12, 128);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(78, 13);
            this.label33.TabIndex = 36;
            this.label33.Text = "Trans. per year";
            // 
            // btnMonteCarloSim
            // 
            this.btnMonteCarloSim.Location = new System.Drawing.Point(15, 155);
            this.btnMonteCarloSim.Name = "btnMonteCarloSim";
            this.btnMonteCarloSim.Size = new System.Drawing.Size(172, 23);
            this.btnMonteCarloSim.TabIndex = 36;
            this.btnMonteCarloSim.Text = "Simulate";
            this.btnMonteCarloSim.UseVisualStyleBackColor = true;
            this.btnMonteCarloSim.Click += new System.EventHandler(this.btnMonteCarloSim_Click);
            // 
            // edtMonteCarloAvgPcntLoss
            // 
            this.edtMonteCarloAvgPcntLoss.DecimalPlaces = 2;
            this.edtMonteCarloAvgPcntLoss.Location = new System.Drawing.Point(108, 102);
            this.edtMonteCarloAvgPcntLoss.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edtMonteCarloAvgPcntLoss.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.edtMonteCarloAvgPcntLoss.Name = "edtMonteCarloAvgPcntLoss";
            this.edtMonteCarloAvgPcntLoss.Size = new System.Drawing.Size(79, 20);
            this.edtMonteCarloAvgPcntLoss.TabIndex = 34;
            // 
            // edtMonteCarloAvgPcntWin
            // 
            this.edtMonteCarloAvgPcntWin.DecimalPlaces = 2;
            this.edtMonteCarloAvgPcntWin.Location = new System.Drawing.Point(108, 80);
            this.edtMonteCarloAvgPcntWin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.edtMonteCarloAvgPcntWin.Name = "edtMonteCarloAvgPcntWin";
            this.edtMonteCarloAvgPcntWin.Size = new System.Drawing.Size(79, 20);
            this.edtMonteCarloAvgPcntWin.TabIndex = 33;
            // 
            // edtMonteCarloWinProb
            // 
            this.edtMonteCarloWinProb.DecimalPlaces = 2;
            this.edtMonteCarloWinProb.Location = new System.Drawing.Point(108, 58);
            this.edtMonteCarloWinProb.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtMonteCarloWinProb.Name = "edtMonteCarloWinProb";
            this.edtMonteCarloWinProb.Size = new System.Drawing.Size(79, 20);
            this.edtMonteCarloWinProb.TabIndex = 32;
            // 
            // edtMonteCarloLength
            // 
            this.edtMonteCarloLength.Location = new System.Drawing.Point(108, 30);
            this.edtMonteCarloLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.edtMonteCarloLength.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.edtMonteCarloLength.Name = "edtMonteCarloLength";
            this.edtMonteCarloLength.Size = new System.Drawing.Size(79, 20);
            this.edtMonteCarloLength.TabIndex = 31;
            this.edtMonteCarloLength.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // edtMonteCarloCount
            // 
            this.edtMonteCarloCount.Location = new System.Drawing.Point(108, 8);
            this.edtMonteCarloCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.edtMonteCarloCount.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.edtMonteCarloCount.Name = "edtMonteCarloCount";
            this.edtMonteCarloCount.Size = new System.Drawing.Size(79, 20);
            this.edtMonteCarloCount.TabIndex = 30;
            this.edtMonteCarloCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(12, 105);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(71, 13);
            this.label31.TabIndex = 29;
            this.label31.Text = "Avg pcnt loss";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(12, 83);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(69, 13);
            this.label30.TabIndex = 28;
            this.label30.Text = "Avg pcnt win";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(12, 61);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 13);
            this.label29.TabIndex = 27;
            this.label29.Text = "Win prob.";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(12, 32);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(40, 13);
            this.label26.TabIndex = 26;
            this.label26.Text = "Length";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Count";
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.paramsSim);
            this.pnlTop.Controls.Add(this.pnlSimulationStart);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(253, 667);
            this.pnlTop.TabIndex = 0;
            // 
            // paramsSim
            // 
            this.paramsSim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paramsSim.Location = new System.Drawing.Point(0, 251);
            this.paramsSim.Name = "paramsSim";
            this.paramsSim.Size = new System.Drawing.Size(251, 414);
            this.paramsSim.TabIndex = 1;
            // 
            // pnlSimulationStart
            // 
            this.pnlSimulationStart.Controls.Add(this.lblSimSystemName);
            this.pnlSimulationStart.Controls.Add(this.cbSystemChoice);
            this.pnlSimulationStart.Controls.Add(this.label3);
            this.pnlSimulationStart.Controls.Add(this.label2);
            this.pnlSimulationStart.Controls.Add(this.label1);
            this.pnlSimulationStart.Controls.Add(this.edtInitialCash);
            this.pnlSimulationStart.Controls.Add(this.btnSimLoadDefinition);
            this.pnlSimulationStart.Controls.Add(this.btnSim);
            this.pnlSimulationStart.Controls.Add(this.dtpSimFrom);
            this.pnlSimulationStart.Controls.Add(this.dtpSimTo);
            this.pnlSimulationStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSimulationStart.Location = new System.Drawing.Point(0, 0);
            this.pnlSimulationStart.Name = "pnlSimulationStart";
            this.pnlSimulationStart.Size = new System.Drawing.Size(251, 251);
            this.pnlSimulationStart.TabIndex = 0;
            // 
            // lblSimSystemName
            // 
            this.lblSimSystemName.AutoSize = true;
            this.lblSimSystemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSimSystemName.Location = new System.Drawing.Point(28, 97);
            this.lblSimSystemName.Name = "lblSimSystemName";
            this.lblSimSystemName.Size = new System.Drawing.Size(34, 13);
            this.lblSimSystemName.TabIndex = 9;
            this.lblSimSystemName.Text = "From";
            // 
            // cbSystemChoice
            // 
            this.cbSystemChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSystemChoice.FormattingEnabled = true;
            this.cbSystemChoice.Location = new System.Drawing.Point(21, 21);
            this.cbSystemChoice.Name = "cbSystemChoice";
            this.cbSystemChoice.Size = new System.Drawing.Size(200, 21);
            this.cbSystemChoice.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Initial cash";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "From";
            // 
            // edtInitialCash
            // 
            this.edtInitialCash.Location = new System.Drawing.Point(124, 175);
            this.edtInitialCash.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.edtInitialCash.Name = "edtInitialCash";
            this.edtInitialCash.Size = new System.Drawing.Size(97, 20);
            this.edtInitialCash.TabIndex = 4;
            this.edtInitialCash.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // btnSimLoadDefinition
            // 
            this.btnSimLoadDefinition.Location = new System.Drawing.Point(21, 48);
            this.btnSimLoadDefinition.Name = "btnSimLoadDefinition";
            this.btnSimLoadDefinition.Size = new System.Drawing.Size(200, 23);
            this.btnSimLoadDefinition.TabIndex = 3;
            this.btnSimLoadDefinition.Text = "Load definition";
            this.btnSimLoadDefinition.UseVisualStyleBackColor = true;
            this.btnSimLoadDefinition.Click += new System.EventHandler(this.btnSimLoadDefinition_Click);
            // 
            // btnSim
            // 
            this.btnSim.Location = new System.Drawing.Point(21, 201);
            this.btnSim.Name = "btnSim";
            this.btnSim.Size = new System.Drawing.Size(200, 23);
            this.btnSim.TabIndex = 0;
            this.btnSim.Text = "Simulate";
            this.btnSim.UseVisualStyleBackColor = true;
            this.btnSim.Click += new System.EventHandler(this.btnSim_Click);
            // 
            // dtpSimFrom
            // 
            this.dtpSimFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSimFrom.Location = new System.Drawing.Point(124, 123);
            this.dtpSimFrom.Name = "dtpSimFrom";
            this.dtpSimFrom.Size = new System.Drawing.Size(97, 20);
            this.dtpSimFrom.TabIndex = 1;
            // 
            // dtpSimTo
            // 
            this.dtpSimTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSimTo.Location = new System.Drawing.Point(124, 149);
            this.dtpSimTo.Name = "dtpSimTo";
            this.dtpSimTo.Size = new System.Drawing.Size(97, 20);
            this.dtpSimTo.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 726);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MarketOps";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tcCharts.ResumeLayout(false);
            this.pnlCharts.ResumeLayout(false);
            this.pnlCharts.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tabCharts.ResumeLayout(false);
            this.tabSimulation.ResumeLayout(false);
            this.pnlSimResult.ResumeLayout(false);
            this.tcSimulationCharts.ResumeLayout(false);
            this.tabSimChartsEqTicks.ResumeLayout(false);
            this.tabSimChartsEqOnPositions.ResumeLayout(false);
            this.tcSimulationData.ResumeLayout(false);
            this.tabSimDataResults.ResumeLayout(false);
            this.pnlSimDataResults.ResumeLayout(false);
            this.pnlSimDataResults.PerformLayout();
            this.tabSimDataPositions.ResumeLayout(false);
            this.tabSimDataDrawdowns.ResumeLayout(false);
            this.splitDrawDowns.Panel1.ResumeLayout(false);
            this.splitDrawDowns.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDrawDowns)).EndInit();
            this.splitDrawDowns.ResumeLayout(false);
            this.gbDDTicks.ResumeLayout(false);
            this.gbDDPositions.ResumeLayout(false);
            this.tabSimR.ResumeLayout(false);
            this.splitR.Panel1.ResumeLayout(false);
            this.splitR.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitR)).EndInit();
            this.splitR.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabSimDataProfits.ResumeLayout(false);
            this.splitProfits.Panel1.ResumeLayout(false);
            this.splitProfits.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitProfits)).EndInit();
            this.splitProfits.ResumeLayout(false);
            this.gbProfitsValue.ResumeLayout(false);
            this.gbProfitPcnt.ResumeLayout(false);
            this.tabSimDataLog.ResumeLayout(false);
            this.tabSimDataLog.PerformLayout();
            this.tabSimMonteCarlo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.gbMonteCarloStreaksLosing.ResumeLayout(false);
            this.gbMonteCarloStreaksWinning.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloTransactionsPerYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloAvgPcntLoss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloAvgPcntWin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloWinProb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMonteCarloCount)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlSimulationStart.ResumeLayout(false);
            this.pnlSimulationStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtInitialCash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dataPumpToolStripMenuItem;
        private System.Windows.Forms.TabControl tcCharts;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel pnlCharts;
        private System.Windows.Forms.TextBox edtStockName;
        private System.Windows.Forms.ComboBox cbStockDataRange;
        private System.Windows.Forms.ToolStripMenuItem dataGenerationToolStripMenuItem;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabCharts;
        private System.Windows.Forms.TabPage tabSimulation;
        private System.Windows.Forms.Button btnSim;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.DateTimePicker dtpSimFrom;
        private System.Windows.Forms.DateTimePicker dtpSimTo;
        private System.Windows.Forms.Panel pnlSimResult;
        private Controls.StockData.MOParamsEditor paramsSim;
        private System.Windows.Forms.TabControl tcSimulationData;
        private System.Windows.Forms.TabPage tabSimDataResults;
        private System.Windows.Forms.TabPage tabSimDataPositions;
        private System.Windows.Forms.Panel pnlSimulationStart;
        private System.Windows.Forms.Button btnSimLoadDefinition;
        private System.Windows.Forms.NumericUpDown edtInitialCash;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSimDataResults;
        private System.Windows.Forms.Label lblSDRStopTS;
        private System.Windows.Forms.Label lblSDRStartTS;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblSDRFinalValueOnLastTick;
        private System.Windows.Forms.Label lblSDRFinalValueOnClosedPositions;
        private System.Windows.Forms.Label lblSDRInitialValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSDRClosedPositionsCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSDRLosses;
        private System.Windows.Forms.Label lblSDRWins;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSDRAvgLoss;
        private System.Windows.Forms.Label lblSDRAvgWin;
        private System.Windows.Forms.Label lblSDRSumLosses;
        private System.Windows.Forms.Label lblSDRSumWins;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblSDRLossProbability;
        private System.Windows.Forms.Label lblSDRWinProbability;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblSDRAvgWinLossRatio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblSDRExpectedPositionValue;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblSDRProcessedTicks;
        private System.Windows.Forms.Label label20;
        private Controls.SystemPositionsGrid.SystemPositionsGrid dbgPositions;
        private System.Windows.Forms.TabControl tcSimulationCharts;
        private System.Windows.Forms.TabPage tabSimChartsEqTicks;
        private System.Windows.Forms.TabPage tabSimChartsEqOnPositions;
        private Controls.SystemEquity.SystemEquityChart chartEquity;
        private Controls.SystemEquity.SystemEquityChart chartEquityOnPositions;
        private System.Windows.Forms.TabPage tabSimDataDrawdowns;
        private System.Windows.Forms.TabPage tabSimDataProfits;
        private System.Windows.Forms.Label lblSDRMaxDDOnPositions;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblSDRMaxDDOnTicks;
        private System.Windows.Forms.Label label21;
        private Controls.PointChart.PointChart chartDDTicks;
        private Controls.PointChart.PointChart chartDDPositions;
        private System.Windows.Forms.GroupBox gbDDPositions;
        private System.Windows.Forms.GroupBox gbDDTicks;
        private System.Windows.Forms.SplitContainer splitDrawDowns;
        private System.Windows.Forms.SplitContainer splitProfits;
        private System.Windows.Forms.GroupBox gbProfitsValue;
        private Controls.PointChart.PointChart chartProfitValue;
        private System.Windows.Forms.GroupBox gbProfitPcnt;
        private Controls.PointChart.PointChart chartProfitPcnt;
        private System.Windows.Forms.ComboBox cbSystemChoice;
        private System.Windows.Forms.Label lblSimSystemName;
        private System.Windows.Forms.Label lblSDRExpectedUnitReturn;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TabPage tabSimDataLog;
        private System.Windows.Forms.TextBox edtSimDataLog;
        private System.Windows.Forms.Label lblSDRProfitPcntOnTicks;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblSDREqDistrStdDev;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblSDREqDistrAvg;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblSDRAvgPcntLoss;
        private System.Windows.Forms.Label lblSDRAvgPcntWin;
        private System.Windows.Forms.TabPage tabSimMonteCarlo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown edtMonteCarloLength;
        private System.Windows.Forms.NumericUpDown edtMonteCarloCount;
        private System.Windows.Forms.NumericUpDown edtMonteCarloWinProb;
        private System.Windows.Forms.NumericUpDown edtMonteCarloAvgPcntLoss;
        private System.Windows.Forms.NumericUpDown edtMonteCarloAvgPcntWin;
        private System.Windows.Forms.Button btnMonteCarloSim;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblMonteCarloSimLosses;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label lblMonteCarloSimWins;
        private System.Windows.Forms.Label label32;
        private Controls.MonteCarlo.MonteCarloDataChart chartMonteCarloData;
        private System.Windows.Forms.TabPage tabSimR;
        private System.Windows.Forms.Label lblSDRRProfitAvg;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.SplitContainer splitR;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.PointChart.PointChart chartRValue;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.ColumnChart.ColumnChart chartRDistribution;
        private System.Windows.Forms.Label lblMonteCarloSimAvg;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lblMonteCarloSimWorst;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lblMonteCarloSimBest;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lblSDRTransactionsPerYear;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.NumericUpDown edtMonteCarloTransactionsPerYear;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lblMonteCarloSimLongestDD;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label lblMonteCarloSimMaxDD;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label lblMonteCarloSimLongestWinningStreak;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblMonteCarloSimLongestLosingStreak;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox gbMonteCarloStreaksLosing;
        private Controls.MonteCarlo.MonteCarloStreaksGrid chartMonteCarloStreaksLosing;
        private System.Windows.Forms.GroupBox gbMonteCarloStreaksWinning;
        private Controls.MonteCarlo.MonteCarloStreaksGrid chartMonteCarloStreaksWinning;
        private System.Windows.Forms.Label lblSDRRProfitAvgToStdDev;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label lblSDRRProfitStdDev;
        private System.Windows.Forms.Label label46;
    }
}

