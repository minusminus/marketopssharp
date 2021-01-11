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
            this.tcSimulationData = new System.Windows.Forms.TabControl();
            this.tabSimDataResults = new System.Windows.Forms.TabPage();
            this.tabSimDataEquityLog = new System.Windows.Forms.TabPage();
            this.pnlSimulationCharts = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.paramsSim = new MarketOps.Controls.StockData.MOParamsEditor();
            this.pnlSimulationStart = new System.Windows.Forms.Panel();
            this.btnSimLoadDefinition = new System.Windows.Forms.Button();
            this.btnSim = new System.Windows.Forms.Button();
            this.dtpSimFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpSimTo = new System.Windows.Forms.DateTimePicker();
            this.edtInitialCash = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSDRInitialValue = new System.Windows.Forms.Label();
            this.lblSDRFinalValueOnClosedPositions = new System.Windows.Forms.Label();
            this.lblSDRFinalValueOnLastTick = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSDRStartTS = new System.Windows.Forms.Label();
            this.lblSDRStopTS = new System.Windows.Forms.Label();
            this.pnlSimDataResults = new System.Windows.Forms.Panel();
            this.lblSDRClosedPositionsCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSDRLosses = new System.Windows.Forms.Label();
            this.lblSDRWins = new System.Windows.Forms.Label();
            this.lblSDRAvgLoss = new System.Windows.Forms.Label();
            this.lblSDRAvgWin = new System.Windows.Forms.Label();
            this.lblSDRSumLosses = new System.Windows.Forms.Label();
            this.lblSDRSumWins = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblSDRLossProbability = new System.Windows.Forms.Label();
            this.lblSDRWinProbability = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblSDRAvgWinLossRatio = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSDRExpectedPositionValue = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblSDRProcessedTicks = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.menuMain.SuspendLayout();
            this.tcCharts.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabCharts.SuspendLayout();
            this.tabSimulation.SuspendLayout();
            this.pnlSimResult.SuspendLayout();
            this.tcSimulationData.SuspendLayout();
            this.tabSimDataResults.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlSimulationStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtInitialCash)).BeginInit();
            this.pnlSimDataResults.SuspendLayout();
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
            this.pnlSimResult.Controls.Add(this.tcSimulationData);
            this.pnlSimResult.Controls.Add(this.pnlSimulationCharts);
            this.pnlSimResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSimResult.Location = new System.Drawing.Point(253, 0);
            this.pnlSimResult.Name = "pnlSimResult";
            this.pnlSimResult.Size = new System.Drawing.Size(947, 667);
            this.pnlSimResult.TabIndex = 1;
            // 
            // tcSimulationData
            // 
            this.tcSimulationData.Controls.Add(this.tabSimDataResults);
            this.tcSimulationData.Controls.Add(this.tabSimDataEquityLog);
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
            // tabSimDataEquityLog
            // 
            this.tabSimDataEquityLog.Location = new System.Drawing.Point(4, 22);
            this.tabSimDataEquityLog.Name = "tabSimDataEquityLog";
            this.tabSimDataEquityLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimDataEquityLog.Size = new System.Drawing.Size(939, 277);
            this.tabSimDataEquityLog.TabIndex = 1;
            this.tabSimDataEquityLog.Text = "Equity log";
            this.tabSimDataEquityLog.UseVisualStyleBackColor = true;
            // 
            // pnlSimulationCharts
            // 
            this.pnlSimulationCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSimulationCharts.Location = new System.Drawing.Point(0, 0);
            this.pnlSimulationCharts.Name = "pnlSimulationCharts";
            this.pnlSimulationCharts.Size = new System.Drawing.Size(947, 667);
            this.pnlSimulationCharts.TabIndex = 0;
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
            this.paramsSim.Location = new System.Drawing.Point(0, 181);
            this.paramsSim.Name = "paramsSim";
            this.paramsSim.Size = new System.Drawing.Size(251, 484);
            this.paramsSim.TabIndex = 1;
            // 
            // pnlSimulationStart
            // 
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
            this.pnlSimulationStart.Size = new System.Drawing.Size(251, 181);
            this.pnlSimulationStart.TabIndex = 0;
            // 
            // btnSimLoadDefinition
            // 
            this.btnSimLoadDefinition.Location = new System.Drawing.Point(21, 30);
            this.btnSimLoadDefinition.Name = "btnSimLoadDefinition";
            this.btnSimLoadDefinition.Size = new System.Drawing.Size(200, 23);
            this.btnSimLoadDefinition.TabIndex = 3;
            this.btnSimLoadDefinition.Text = "Load definition";
            this.btnSimLoadDefinition.UseVisualStyleBackColor = true;
            this.btnSimLoadDefinition.Click += new System.EventHandler(this.btnSimLoadDefinition_Click);
            // 
            // btnSim
            // 
            this.btnSim.Location = new System.Drawing.Point(21, 147);
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
            this.dtpSimFrom.Location = new System.Drawing.Point(124, 69);
            this.dtpSimFrom.Name = "dtpSimFrom";
            this.dtpSimFrom.Size = new System.Drawing.Size(97, 20);
            this.dtpSimFrom.TabIndex = 1;
            // 
            // dtpSimTo
            // 
            this.dtpSimTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSimTo.Location = new System.Drawing.Point(124, 95);
            this.dtpSimTo.Name = "dtpSimTo";
            this.dtpSimTo.Size = new System.Drawing.Size(97, 20);
            this.dtpSimTo.TabIndex = 2;
            // 
            // edtInitialCash
            // 
            this.edtInitialCash.Location = new System.Drawing.Point(124, 121);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Initial cash";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Initial value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Final value on positions";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Final value on last tick";
            // 
            // lblSDRInitialValue
            // 
            this.lblSDRInitialValue.AutoSize = true;
            this.lblSDRInitialValue.Location = new System.Drawing.Point(146, 60);
            this.lblSDRInitialValue.Name = "lblSDRInitialValue";
            this.lblSDRInitialValue.Size = new System.Drawing.Size(41, 13);
            this.lblSDRInitialValue.TabIndex = 6;
            this.lblSDRInitialValue.Text = "label10";
            // 
            // lblSDRFinalValueOnClosedPositions
            // 
            this.lblSDRFinalValueOnClosedPositions.AutoSize = true;
            this.lblSDRFinalValueOnClosedPositions.Location = new System.Drawing.Point(146, 76);
            this.lblSDRFinalValueOnClosedPositions.Name = "lblSDRFinalValueOnClosedPositions";
            this.lblSDRFinalValueOnClosedPositions.Size = new System.Drawing.Size(41, 13);
            this.lblSDRFinalValueOnClosedPositions.TabIndex = 7;
            this.lblSDRFinalValueOnClosedPositions.Text = "label11";
            // 
            // lblSDRFinalValueOnLastTick
            // 
            this.lblSDRFinalValueOnLastTick.AutoSize = true;
            this.lblSDRFinalValueOnLastTick.Location = new System.Drawing.Point(146, 92);
            this.lblSDRFinalValueOnLastTick.Name = "lblSDRFinalValueOnLastTick";
            this.lblSDRFinalValueOnLastTick.Size = new System.Drawing.Size(41, 13);
            this.lblSDRFinalValueOnLastTick.TabIndex = 8;
            this.lblSDRFinalValueOnLastTick.Text = "label12";
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Stop TS";
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
            // lblSDRStopTS
            // 
            this.lblSDRStopTS.AutoSize = true;
            this.lblSDRStopTS.Location = new System.Drawing.Point(146, 26);
            this.lblSDRStopTS.Name = "lblSDRStopTS";
            this.lblSDRStopTS.Size = new System.Drawing.Size(41, 13);
            this.lblSDRStopTS.TabIndex = 27;
            this.lblSDRStopTS.Text = "label11";
            // 
            // pnlSimDataResults
            // 
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
            // lblSDRClosedPositionsCount
            // 
            this.lblSDRClosedPositionsCount.AutoSize = true;
            this.lblSDRClosedPositionsCount.Location = new System.Drawing.Point(146, 112);
            this.lblSDRClosedPositionsCount.Name = "lblSDRClosedPositionsCount";
            this.lblSDRClosedPositionsCount.Size = new System.Drawing.Size(41, 13);
            this.lblSDRClosedPositionsCount.TabIndex = 29;
            this.lblSDRClosedPositionsCount.Text = "label12";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Closed positions";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Wins";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Losses";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(114, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Avg.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(114, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Avg.";
            // 
            // lblSDRLosses
            // 
            this.lblSDRLosses.AutoSize = true;
            this.lblSDRLosses.Location = new System.Drawing.Point(70, 144);
            this.lblSDRLosses.Name = "lblSDRLosses";
            this.lblSDRLosses.Size = new System.Drawing.Size(40, 13);
            this.lblSDRLosses.TabIndex = 36;
            this.lblSDRLosses.Text = "Losses";
            // 
            // lblSDRWins
            // 
            this.lblSDRWins.AutoSize = true;
            this.lblSDRWins.Location = new System.Drawing.Point(70, 128);
            this.lblSDRWins.Name = "lblSDRWins";
            this.lblSDRWins.Size = new System.Drawing.Size(31, 13);
            this.lblSDRWins.TabIndex = 35;
            this.lblSDRWins.Text = "Wins";
            // 
            // lblSDRAvgLoss
            // 
            this.lblSDRAvgLoss.AutoSize = true;
            this.lblSDRAvgLoss.Location = new System.Drawing.Point(146, 176);
            this.lblSDRAvgLoss.Name = "lblSDRAvgLoss";
            this.lblSDRAvgLoss.Size = new System.Drawing.Size(26, 13);
            this.lblSDRAvgLoss.TabIndex = 38;
            this.lblSDRAvgLoss.Text = "Avg";
            // 
            // lblSDRAvgWin
            // 
            this.lblSDRAvgWin.AutoSize = true;
            this.lblSDRAvgWin.Location = new System.Drawing.Point(146, 160);
            this.lblSDRAvgWin.Name = "lblSDRAvgWin";
            this.lblSDRAvgWin.Size = new System.Drawing.Size(26, 13);
            this.lblSDRAvgWin.TabIndex = 37;
            this.lblSDRAvgWin.Text = "Avg";
            // 
            // lblSDRSumLosses
            // 
            this.lblSDRSumLosses.AutoSize = true;
            this.lblSDRSumLosses.Location = new System.Drawing.Point(70, 176);
            this.lblSDRSumLosses.Name = "lblSDRSumLosses";
            this.lblSDRSumLosses.Size = new System.Drawing.Size(26, 13);
            this.lblSDRSumLosses.TabIndex = 42;
            this.lblSDRSumLosses.Text = "Avg";
            // 
            // lblSDRSumWins
            // 
            this.lblSDRSumWins.AutoSize = true;
            this.lblSDRSumWins.Location = new System.Drawing.Point(70, 160);
            this.lblSDRSumWins.Name = "lblSDRSumWins";
            this.lblSDRSumWins.Size = new System.Drawing.Size(26, 13);
            this.lblSDRSumWins.TabIndex = 41;
            this.lblSDRSumWins.Text = "Avg";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 176);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "Sum losses";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 160);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 13);
            this.label16.TabIndex = 39;
            this.label16.Text = "Sum wins";
            // 
            // lblSDRLossProbability
            // 
            this.lblSDRLossProbability.AutoSize = true;
            this.lblSDRLossProbability.Location = new System.Drawing.Point(146, 144);
            this.lblSDRLossProbability.Name = "lblSDRLossProbability";
            this.lblSDRLossProbability.Size = new System.Drawing.Size(26, 13);
            this.lblSDRLossProbability.TabIndex = 46;
            this.lblSDRLossProbability.Text = "Avg";
            // 
            // lblSDRWinProbability
            // 
            this.lblSDRWinProbability.AutoSize = true;
            this.lblSDRWinProbability.Location = new System.Drawing.Point(146, 128);
            this.lblSDRWinProbability.Name = "lblSDRWinProbability";
            this.lblSDRWinProbability.Size = new System.Drawing.Size(26, 13);
            this.lblSDRWinProbability.TabIndex = 45;
            this.lblSDRWinProbability.Text = "Avg";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(114, 144);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 13);
            this.label17.TabIndex = 44;
            this.label17.Text = "Prob.";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(114, 128);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(32, 13);
            this.label18.TabIndex = 43;
            this.label18.Text = "Prob.";
            // 
            // lblSDRAvgWinLossRatio
            // 
            this.lblSDRAvgWinLossRatio.AutoSize = true;
            this.lblSDRAvgWinLossRatio.Location = new System.Drawing.Point(146, 192);
            this.lblSDRAvgWinLossRatio.Name = "lblSDRAvgWinLossRatio";
            this.lblSDRAvgWinLossRatio.Size = new System.Drawing.Size(26, 13);
            this.lblSDRAvgWinLossRatio.TabIndex = 48;
            this.lblSDRAvgWinLossRatio.Text = "Avg";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 192);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 47;
            this.label14.Text = "W/L Ratio";
            // 
            // lblSDRExpectedPositionValue
            // 
            this.lblSDRExpectedPositionValue.AutoSize = true;
            this.lblSDRExpectedPositionValue.Location = new System.Drawing.Point(146, 208);
            this.lblSDRExpectedPositionValue.Name = "lblSDRExpectedPositionValue";
            this.lblSDRExpectedPositionValue.Size = new System.Drawing.Size(26, 13);
            this.lblSDRExpectedPositionValue.TabIndex = 50;
            this.lblSDRExpectedPositionValue.Text = "Avg";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 208);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(120, 13);
            this.label19.TabIndex = 49;
            this.label19.Text = "Expected position value";
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
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tcCharts.ResumeLayout(false);
            this.pnlCharts.ResumeLayout(false);
            this.pnlCharts.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tabCharts.ResumeLayout(false);
            this.tabSimulation.ResumeLayout(false);
            this.pnlSimResult.ResumeLayout(false);
            this.tcSimulationData.ResumeLayout(false);
            this.tabSimDataResults.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlSimulationStart.ResumeLayout(false);
            this.pnlSimulationStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtInitialCash)).EndInit();
            this.pnlSimDataResults.ResumeLayout(false);
            this.pnlSimDataResults.PerformLayout();
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
        private System.Windows.Forms.TabPage tabSimDataEquityLog;
        private System.Windows.Forms.Panel pnlSimulationCharts;
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
    }
}

