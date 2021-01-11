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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pnlSimulationCharts = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.paramsSim = new MarketOps.Controls.StockData.MOParamsEditor();
            this.pnlSimulationStart = new System.Windows.Forms.Panel();
            this.btnSim = new System.Windows.Forms.Button();
            this.dtpSimFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpSimTo = new System.Windows.Forms.DateTimePicker();
            this.btnSimLoadDefinition = new System.Windows.Forms.Button();
            this.menuMain.SuspendLayout();
            this.tcCharts.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabCharts.SuspendLayout();
            this.tabSimulation.SuspendLayout();
            this.pnlSimResult.SuspendLayout();
            this.tcSimulationData.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlSimulationStart.SuspendLayout();
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
            this.tcSimulationData.Controls.Add(this.tabPage2);
            this.tcSimulationData.Controls.Add(this.tabPage3);
            this.tcSimulationData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tcSimulationData.Location = new System.Drawing.Point(0, 393);
            this.tcSimulationData.Name = "tcSimulationData";
            this.tcSimulationData.SelectedIndex = 0;
            this.tcSimulationData.Size = new System.Drawing.Size(947, 274);
            this.tcSimulationData.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(939, 248);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(939, 248);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            this.paramsSim.Location = new System.Drawing.Point(0, 154);
            this.paramsSim.Name = "paramsSim";
            this.paramsSim.Size = new System.Drawing.Size(251, 511);
            this.paramsSim.TabIndex = 1;
            // 
            // pnlSimulationStart
            // 
            this.pnlSimulationStart.Controls.Add(this.btnSimLoadDefinition);
            this.pnlSimulationStart.Controls.Add(this.btnSim);
            this.pnlSimulationStart.Controls.Add(this.dtpSimFrom);
            this.pnlSimulationStart.Controls.Add(this.dtpSimTo);
            this.pnlSimulationStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSimulationStart.Location = new System.Drawing.Point(0, 0);
            this.pnlSimulationStart.Name = "pnlSimulationStart";
            this.pnlSimulationStart.Size = new System.Drawing.Size(251, 154);
            this.pnlSimulationStart.TabIndex = 0;
            // 
            // btnSim
            // 
            this.btnSim.Location = new System.Drawing.Point(21, 106);
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
            this.dtpSimFrom.Location = new System.Drawing.Point(21, 80);
            this.dtpSimFrom.Name = "dtpSimFrom";
            this.dtpSimFrom.Size = new System.Drawing.Size(97, 20);
            this.dtpSimFrom.TabIndex = 1;
            // 
            // dtpSimTo
            // 
            this.dtpSimTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSimTo.Location = new System.Drawing.Point(124, 80);
            this.dtpSimTo.Name = "dtpSimTo";
            this.dtpSimTo.Size = new System.Drawing.Size(97, 20);
            this.dtpSimTo.TabIndex = 2;
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
            this.pnlTop.ResumeLayout(false);
            this.pnlSimulationStart.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel pnlSimulationCharts;
        private System.Windows.Forms.Panel pnlSimulationStart;
        private System.Windows.Forms.Button btnSimLoadDefinition;
    }
}

