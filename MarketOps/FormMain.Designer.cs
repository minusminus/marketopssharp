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
            this.tcCharts = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.cbStockDataRange = new System.Windows.Forms.ComboBox();
            this.edtStockName = new System.Windows.Forms.TextBox();
            this.dataGenerationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.tcCharts.SuspendLayout();
            this.pnlMain.SuspendLayout();
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
            this.menuMain.Size = new System.Drawing.Size(953, 24);
            this.menuMain.TabIndex = 9;
            this.menuMain.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataPumpToolStripMenuItem,
            this.dataGenerationToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(42, 20);
            this.toolStripMenuItem1.Text = "Data";
            // 
            // dataPumpToolStripMenuItem
            // 
            this.dataPumpToolStripMenuItem.Name = "dataPumpToolStripMenuItem";
            this.dataPumpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dataPumpToolStripMenuItem.Text = "Data pump";
            this.dataPumpToolStripMenuItem.Click += new System.EventHandler(this.dataPumpToolStripMenuItem_Click);
            // 
            // tcCharts
            // 
            this.tcCharts.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcCharts.Controls.Add(this.tabPage1);
            this.tcCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCharts.Location = new System.Drawing.Point(195, 24);
            this.tcCharts.Multiline = true;
            this.tcCharts.Name = "tcCharts";
            this.tcCharts.SelectedIndex = 0;
            this.tcCharts.Size = new System.Drawing.Size(758, 509);
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
            this.tabPage1.Size = new System.Drawing.Size(750, 480);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.cbStockDataRange);
            this.pnlMain.Controls.Add(this.edtStockName);
            this.pnlMain.Controls.Add(this.btnLoad);
            this.pnlMain.Controls.Add(this.button1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(195, 509);
            this.pnlMain.TabIndex = 0;
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
            // dataGenerationToolStripMenuItem
            // 
            this.dataGenerationToolStripMenuItem.Name = "dataGenerationToolStripMenuItem";
            this.dataGenerationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dataGenerationToolStripMenuItem.Text = "Data generation";
            this.dataGenerationToolStripMenuItem.Click += new System.EventHandler(this.dataGenerationToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 533);
            this.Controls.Add(this.tcCharts);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MarketOps";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tcCharts.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
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
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox edtStockName;
        private System.Windows.Forms.ComboBox cbStockDataRange;
        private System.Windows.Forms.ToolStripMenuItem dataGenerationToolStripMenuItem;
    }
}

