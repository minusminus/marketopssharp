namespace MarketOps
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
            this.button1 = new System.Windows.Forms.Button();
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
            this.tabSimChartsEqOnPositions = new System.Windows.Forms.TabPage();
            this.tcSimulationData = new System.Windows.Forms.TabControl();
            this.tabSimDataResults = new System.Windows.Forms.TabPage();
            this.pnlSimDataResults = new System.Windows.Forms.Panel();
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
            this.tabSimDataDrawdowns = new System.Windows.Forms.TabPage();
            this.splitDrawDowns = new System.Windows.Forms.SplitContainer();
            this.gbDDTicks = new System.Windows.Forms.GroupBox();
            this.gbDDPositions = new System.Windows.Forms.GroupBox();
            this.tabSimDataProfits = new System.Windows.Forms.TabPage();
            this.splitProfits = new System.Windows.Forms.SplitContainer();
            this.gbProfitsValue = new System.Windows.Forms.GroupBox();
            this.gbProfitPcnt = new System.Windows.Forms.GroupBox();
            this.tabSimDataLog = new System.Windows.Forms.TabPage();
            this.edtSimDataLog = new System.Windows.Forms.TextBox();
            this.pnlTop = new System.Windows.Forms.Panel();
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
            this.lblSDRProfitPcntOnTicks = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblSDREqDistrStdDev = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblSDREqDistrAvg = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.chartEquity = new MarketOps.Controls.SystemEquity.SystemEquityChart();
            this.chartEquityOnPositions = new MarketOps.Controls.SystemEquity.SystemEquityChart();
            this.dbgPositions = new MarketOps.Controls.SystemPositionsGrid.SystemPositionsGrid();
            this.chartDDTicks = new MarketOps.Controls.PointChart.PointChart();
            this.chartDDPositions = new MarketOps.Controls.PointChart.PointChart();
            this.chartProfitValue = new MarketOps.Controls.PointChart.PointChart();
            this.chartProfitPcnt = new MarketOps.Controls.PointChart.PointChart();
            this.paramsSim = new MarketOps.Controls.StockData.MOParamsEditor();
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
            this.tabSimDataProfits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitProfits)).BeginInit();
            this.splitProfits.Panel1.SuspendLayout();
            this.splitProfits.Panel2.SuspendLayout();
            this.splitProfits.SuspendLayout();
            this.gbProfitsValue.SuspendLayout();
            this.gbProfitPcnt.SuspendLayout();
            this.tabSimDataLog.SuspendLayout();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 430);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.tcCharts.Deselected += new System.Windows.Forms.TabControlEventHandler(this.tcCharts_Deselected);
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
            this.pnlCharts.Controls.Add(this.button1);
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
            // tcSimulationData
            // 
            this.tcSimulationData.Controls.Add(this.tabSimDataResults);
            this.tcSimulationData.Controls.Add(this.tabSimDataPositions);
            this.tcSimulationData.Controls.Add(this.tabSimDataDrawdowns);
            this.tcSimulationData.Controls.Add(this.tabSimDataProfits);
            this.tcSimulationData.Controls.Add(this.tabSimDataLog);
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
            // lblSDRExpectedUnitReturn
            // 
            this.lblSDRExpectedUnitReturn.AutoSize = true;
            this.lblSDRExpectedUnitReturn.Location = new System.Drawing.Point(382, 106);
            this.lblSDRExpectedUnitReturn.Name = "lblSDRExpectedUnitReturn";
            this.lblSDRExpectedUnitReturn.Size = new System.Drawing.Size(26, 13);
            this.lblSDRExpectedUnitReturn.TabIndex = 58;
            this.lblSDRExpectedUnitReturn.Text = "Avg";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(248, 106);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(102, 13);
            this.label22.TabIndex = 57;
            this.label22.Text = "Expected unit return";
            // 
            // lblSDRMaxDDOnPositions
            // 
            this.lblSDRMaxDDOnPositions.AutoSize = true;
            this.lblSDRMaxDDOnPositions.Location = new System.Drawing.Point(382, 170);
            this.lblSDRMaxDDOnPositions.Name = "lblSDRMaxDDOnPositions";
            this.lblSDRMaxDDOnPositions.Size = new System.Drawing.Size(26, 13);
            this.lblSDRMaxDDOnPositions.TabIndex = 56;
            this.lblSDRMaxDDOnPositions.Text = "Avg";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(248, 170);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(105, 13);
            this.label23.TabIndex = 55;
            this.label23.Text = "Max DD on positions";
            // 
            // lblSDRMaxDDOnTicks
            // 
            this.lblSDRMaxDDOnTicks.AutoSize = true;
            this.lblSDRMaxDDOnTicks.Location = new System.Drawing.Point(382, 154);
            this.lblSDRMaxDDOnTicks.Name = "lblSDRMaxDDOnTicks";
            this.lblSDRMaxDDOnTicks.Size = new System.Drawing.Size(26, 13);
            this.lblSDRMaxDDOnTicks.TabIndex = 54;
            this.lblSDRMaxDDOnTicks.Text = "Avg";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(248, 154);
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
            this.lblSDRExpectedPositionValue.Location = new System.Drawing.Point(382, 122);
            this.lblSDRExpectedPositionValue.Name = "lblSDRExpectedPositionValue";
            this.lblSDRExpectedPositionValue.Size = new System.Drawing.Size(26, 13);
            this.lblSDRExpectedPositionValue.TabIndex = 50;
            this.lblSDRExpectedPositionValue.Text = "Avg";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(248, 122);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(120, 13);
            this.label19.TabIndex = 49;
            this.label19.Text = "Expected position value";
            // 
            // lblSDRAvgWinLossRatio
            // 
            this.lblSDRAvgWinLossRatio.AutoSize = true;
            this.lblSDRAvgWinLossRatio.Location = new System.Drawing.Point(382, 90);
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
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 47;
            this.label14.Text = "W/L ratio";
            // 
            // lblSDRLossProbability
            // 
            this.lblSDRLossProbability.AutoSize = true;
            this.lblSDRLossProbability.Location = new System.Drawing.Point(382, 42);
            this.lblSDRLossProbability.Name = "lblSDRLossProbability";
            this.lblSDRLossProbability.Size = new System.Drawing.Size(26, 13);
            this.lblSDRLossProbability.TabIndex = 46;
            this.lblSDRLossProbability.Text = "Avg";
            // 
            // lblSDRWinProbability
            // 
            this.lblSDRWinProbability.AutoSize = true;
            this.lblSDRWinProbability.Location = new System.Drawing.Point(382, 26);
            this.lblSDRWinProbability.Name = "lblSDRWinProbability";
            this.lblSDRWinProbability.Size = new System.Drawing.Size(26, 13);
            this.lblSDRWinProbability.TabIndex = 45;
            this.lblSDRWinProbability.Text = "Avg";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(350, 42);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 13);
            this.label17.TabIndex = 44;
            this.label17.Text = "Prob.";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(350, 26);
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
            this.lblSDRAvgLoss.Location = new System.Drawing.Point(382, 74);
            this.lblSDRAvgLoss.Name = "lblSDRAvgLoss";
            this.lblSDRAvgLoss.Size = new System.Drawing.Size(26, 13);
            this.lblSDRAvgLoss.TabIndex = 38;
            this.lblSDRAvgLoss.Text = "Avg";
            // 
            // lblSDRAvgWin
            // 
            this.lblSDRAvgWin.AutoSize = true;
            this.lblSDRAvgWin.Location = new System.Drawing.Point(382, 58);
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
            this.label7.Location = new System.Drawing.Point(350, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Avg.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(350, 58);
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
            this.lblSDRClosedPositionsCount.Location = new System.Drawing.Point(382, 10);
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
            this.gbDDTicks.Text = "Ticks";
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
            this.gbDDPositions.Text = "Positions";
            // 
            // tabSimDataProfits
            // 
            this.tabSimDataProfits.Controls.Add(this.splitProfits);
            this.tabSimDataProfits.Location = new System.Drawing.Point(4, 22);
            this.tabSimDataProfits.Name = "tabSimDataProfits";
            this.tabSimDataProfits.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimDataProfits.Size = new System.Drawing.Size(939, 277);
            this.tabSimDataProfits.TabIndex = 3;
            this.tabSimDataProfits.Text = "Profits";
            this.tabSimDataProfits.UseVisualStyleBackColor = true;
            // 
            // splitProfits
            // 
            this.splitProfits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitProfits.IsSplitterFixed = true;
            this.splitProfits.Location = new System.Drawing.Point(3, 3);
            this.splitProfits.Name = "splitProfits";
            // 
            // splitProfits.Panel1
            // 
            this.splitProfits.Panel1.Controls.Add(this.gbProfitsValue);
            // 
            // splitProfits.Panel2
            // 
            this.splitProfits.Panel2.Controls.Add(this.gbProfitPcnt);
            this.splitProfits.Size = new System.Drawing.Size(933, 271);
            this.splitProfits.SplitterDistance = 470;
            this.splitProfits.TabIndex = 0;
            // 
            // gbProfitsValue
            // 
            this.gbProfitsValue.Controls.Add(this.chartProfitValue);
            this.gbProfitsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbProfitsValue.Location = new System.Drawing.Point(0, 0);
            this.gbProfitsValue.Name = "gbProfitsValue";
            this.gbProfitsValue.Size = new System.Drawing.Size(470, 271);
            this.gbProfitsValue.TabIndex = 3;
            this.gbProfitsValue.TabStop = false;
            this.gbProfitsValue.Text = "Value";
            // 
            // gbProfitPcnt
            // 
            this.gbProfitPcnt.Controls.Add(this.chartProfitPcnt);
            this.gbProfitPcnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbProfitPcnt.Location = new System.Drawing.Point(0, 0);
            this.gbProfitPcnt.Name = "gbProfitPcnt";
            this.gbProfitPcnt.Size = new System.Drawing.Size(459, 271);
            this.gbProfitPcnt.TabIndex = 3;
            this.gbProfitPcnt.TabStop = false;
            this.gbProfitPcnt.Text = "Percentage";
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
            // lblSDREqDistrStdDev
            // 
            this.lblSDREqDistrStdDev.AutoSize = true;
            this.lblSDREqDistrStdDev.Location = new System.Drawing.Point(146, 170);
            this.lblSDREqDistrStdDev.Name = "lblSDREqDistrStdDev";
            this.lblSDREqDistrStdDev.Size = new System.Drawing.Size(26, 13);
            this.lblSDREqDistrStdDev.TabIndex = 64;
            this.lblSDREqDistrStdDev.Text = "Avg";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(12, 170);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(134, 13);
            this.label25.TabIndex = 63;
            this.label25.Text = "Equity pcnt diff distr stddev";
            // 
            // lblSDREqDistrAvg
            // 
            this.lblSDREqDistrAvg.AutoSize = true;
            this.lblSDREqDistrAvg.Location = new System.Drawing.Point(146, 154);
            this.lblSDREqDistrAvg.Name = "lblSDREqDistrAvg";
            this.lblSDREqDistrAvg.Size = new System.Drawing.Size(26, 13);
            this.lblSDREqDistrAvg.TabIndex = 62;
            this.lblSDREqDistrAvg.Text = "Avg";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 154);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(120, 13);
            this.label27.TabIndex = 61;
            this.label27.Text = "Equity pcnt diff distr avg";
            // 
            // chartEquity
            // 
            this.chartEquity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEquity.Location = new System.Drawing.Point(3, 3);
            this.chartEquity.Name = "chartEquity";
            this.chartEquity.Size = new System.Drawing.Size(933, 332);
            this.chartEquity.TabIndex = 0;
            // 
            // chartEquityOnPositions
            // 
            this.chartEquityOnPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEquityOnPositions.Location = new System.Drawing.Point(3, 3);
            this.chartEquityOnPositions.Name = "chartEquityOnPositions";
            this.chartEquityOnPositions.Size = new System.Drawing.Size(933, 332);
            this.chartEquityOnPositions.TabIndex = 0;
            // 
            // dbgPositions
            // 
            this.dbgPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbgPositions.Location = new System.Drawing.Point(3, 3);
            this.dbgPositions.Name = "dbgPositions";
            this.dbgPositions.Size = new System.Drawing.Size(933, 271);
            this.dbgPositions.TabIndex = 0;
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
            // chartDDPositions
            // 
            this.chartDDPositions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartDDPositions.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartDDPositions.Location = new System.Drawing.Point(3, 16);
            this.chartDDPositions.Name = "chartDDPositions";
            this.chartDDPositions.Size = new System.Drawing.Size(457, 181);
            this.chartDDPositions.TabIndex = 1;
            // 
            // chartProfitValue
            // 
            this.chartProfitValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartProfitValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartProfitValue.Location = new System.Drawing.Point(3, 16);
            this.chartProfitValue.Name = "chartProfitValue";
            this.chartProfitValue.Size = new System.Drawing.Size(464, 181);
            this.chartProfitValue.TabIndex = 0;
            // 
            // chartProfitPcnt
            // 
            this.chartProfitPcnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartProfitPcnt.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartProfitPcnt.Location = new System.Drawing.Point(3, 16);
            this.chartProfitPcnt.Name = "chartProfitPcnt";
            this.chartProfitPcnt.Size = new System.Drawing.Size(453, 181);
            this.chartProfitPcnt.TabIndex = 0;
            // 
            // paramsSim
            // 
            this.paramsSim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paramsSim.Location = new System.Drawing.Point(0, 251);
            this.paramsSim.Name = "paramsSim";
            this.paramsSim.Size = new System.Drawing.Size(251, 414);
            this.paramsSim.TabIndex = 1;
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
            this.tabSimDataProfits.ResumeLayout(false);
            this.splitProfits.Panel1.ResumeLayout(false);
            this.splitProfits.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitProfits)).EndInit();
            this.splitProfits.ResumeLayout(false);
            this.gbProfitsValue.ResumeLayout(false);
            this.gbProfitPcnt.ResumeLayout(false);
            this.tabSimDataLog.ResumeLayout(false);
            this.tabSimDataLog.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlSimulationStart.ResumeLayout(false);
            this.pnlSimulationStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtInitialCash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button button1;
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
    }
}

