namespace MarketOps.Controls.PriceChart
{
    partial class PriceVolumePanel
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

        #region Kod wygenerowany przez Projektanta składników

        /// <summary> 
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować 
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceVolumePanel));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnMirrorChart = new System.Windows.Forms.CheckBox();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnDataRange = new System.Windows.Forms.Button();
            this.btnPrependData = new System.Windows.Forms.Button();
            this.btnPriceChartCandle = new System.Windows.Forms.CheckBox();
            this.btnPriceChartLine = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblStatSelectedInfo = new System.Windows.Forms.Label();
            this.lblSelectedInfo = new System.Windows.Forms.Label();
            this.lblStockInfo = new System.Windows.Forms.Label();
            this.cmnChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miStatsOnPriceChart = new System.Windows.Forms.ToolStripMenuItem();
            this.miAdditionalStats = new System.Windows.Forms.ToolStripMenuItem();
            this.chartPV = new MarketOps.Controls.PriceChart.PriceVolumeChart();
            this.pnlButtons.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.cmnChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnMirrorChart);
            this.pnlButtons.Controls.Add(this.btnDataRange);
            this.pnlButtons.Controls.Add(this.btnPrependData);
            this.pnlButtons.Controls.Add(this.btnPriceChartCandle);
            this.pnlButtons.Controls.Add(this.btnPriceChartLine);
            this.pnlButtons.Controls.Add(this.btnRefresh);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(606, 32);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnMirrorChart
            // 
            this.btnMirrorChart.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnMirrorChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMirrorChart.ImageIndex = 4;
            this.btnMirrorChart.ImageList = this.ilIcons;
            this.btnMirrorChart.Location = new System.Drawing.Point(268, 4);
            this.btnMirrorChart.Name = "btnMirrorChart";
            this.btnMirrorChart.Size = new System.Drawing.Size(32, 24);
            this.btnMirrorChart.TabIndex = 7;
            this.btnMirrorChart.TabStop = false;
            this.btnMirrorChart.UseVisualStyleBackColor = true;
            this.btnMirrorChart.CheckedChanged += new System.EventHandler(this.btnMirrorChart_CheckedChanged);
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "candlestick-chart.png");
            this.ilIcons.Images.SetKeyName(1, "line-chart.png");
            this.ilIcons.Images.SetKeyName(2, "previous.png");
            this.ilIcons.Images.SetKeyName(3, "refresh.png");
            this.ilIcons.Images.SetKeyName(4, "revert-hand-drawn-arrows2.png");
            this.ilIcons.Images.SetKeyName(5, "table-selection.png");
            // 
            // btnDataRange
            // 
            this.btnDataRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataRange.ImageIndex = 5;
            this.btnDataRange.ImageList = this.ilIcons;
            this.btnDataRange.Location = new System.Drawing.Point(96, 4);
            this.btnDataRange.Name = "btnDataRange";
            this.btnDataRange.Size = new System.Drawing.Size(32, 24);
            this.btnDataRange.TabIndex = 6;
            this.btnDataRange.TabStop = false;
            this.btnDataRange.UseVisualStyleBackColor = true;
            this.btnDataRange.Click += new System.EventHandler(this.btnDataRange_Click);
            // 
            // btnPrependData
            // 
            this.btnPrependData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrependData.ImageIndex = 2;
            this.btnPrependData.ImageList = this.ilIcons;
            this.btnPrependData.Location = new System.Drawing.Point(58, 4);
            this.btnPrependData.Name = "btnPrependData";
            this.btnPrependData.Size = new System.Drawing.Size(32, 24);
            this.btnPrependData.TabIndex = 5;
            this.btnPrependData.TabStop = false;
            this.btnPrependData.UseVisualStyleBackColor = true;
            this.btnPrependData.Click += new System.EventHandler(this.btnPrependData_Click);
            // 
            // btnPriceChartCandle
            // 
            this.btnPriceChartCandle.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPriceChartCandle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPriceChartCandle.ImageIndex = 0;
            this.btnPriceChartCandle.ImageList = this.ilIcons;
            this.btnPriceChartCandle.Location = new System.Drawing.Point(354, 4);
            this.btnPriceChartCandle.Name = "btnPriceChartCandle";
            this.btnPriceChartCandle.Size = new System.Drawing.Size(32, 24);
            this.btnPriceChartCandle.TabIndex = 4;
            this.btnPriceChartCandle.TabStop = false;
            this.btnPriceChartCandle.UseVisualStyleBackColor = true;
            this.btnPriceChartCandle.CheckedChanged += new System.EventHandler(this.btnPriceChartCandle_CheckedChanged);
            // 
            // btnPriceChartLine
            // 
            this.btnPriceChartLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPriceChartLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPriceChartLine.ImageIndex = 1;
            this.btnPriceChartLine.ImageList = this.ilIcons;
            this.btnPriceChartLine.Location = new System.Drawing.Point(316, 4);
            this.btnPriceChartLine.Name = "btnPriceChartLine";
            this.btnPriceChartLine.Size = new System.Drawing.Size(32, 24);
            this.btnPriceChartLine.TabIndex = 3;
            this.btnPriceChartLine.TabStop = false;
            this.btnPriceChartLine.UseVisualStyleBackColor = true;
            this.btnPriceChartLine.CheckedChanged += new System.EventHandler(this.btnPriceChartLine_CheckedChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ImageIndex = 3;
            this.btnRefresh.ImageList = this.ilIcons;
            this.btnRefresh.Location = new System.Drawing.Point(10, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 24);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblStatSelectedInfo);
            this.pnlInfo.Controls.Add(this.lblSelectedInfo);
            this.pnlInfo.Controls.Add(this.lblStockInfo);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.Location = new System.Drawing.Point(0, 32);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(606, 45);
            this.pnlInfo.TabIndex = 2;
            // 
            // lblStatSelectedInfo
            // 
            this.lblStatSelectedInfo.AutoSize = true;
            this.lblStatSelectedInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStatSelectedInfo.Location = new System.Drawing.Point(2, 30);
            this.lblStatSelectedInfo.Name = "lblStatSelectedInfo";
            this.lblStatSelectedInfo.Size = new System.Drawing.Size(83, 12);
            this.lblStatSelectedInfo.TabIndex = 2;
            this.lblStatSelectedInfo.Text = "lblStatSelectedInfo";
            // 
            // lblSelectedInfo
            // 
            this.lblSelectedInfo.AutoSize = true;
            this.lblSelectedInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSelectedInfo.Location = new System.Drawing.Point(2, 16);
            this.lblSelectedInfo.Name = "lblSelectedInfo";
            this.lblSelectedInfo.Size = new System.Drawing.Size(66, 12);
            this.lblSelectedInfo.TabIndex = 1;
            this.lblSelectedInfo.Text = "lblSelectedInfo";
            // 
            // lblStockInfo
            // 
            this.lblStockInfo.AutoSize = true;
            this.lblStockInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStockInfo.Location = new System.Drawing.Point(2, 2);
            this.lblStockInfo.Name = "lblStockInfo";
            this.lblStockInfo.Size = new System.Drawing.Size(54, 12);
            this.lblStockInfo.TabIndex = 0;
            this.lblStockInfo.Text = "lblStockInfo";
            // 
            // cmnChart
            // 
            this.cmnChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStatsOnPriceChart,
            this.miAdditionalStats});
            this.cmnChart.Name = "cmnChart";
            this.cmnChart.ShowImageMargin = false;
            this.cmnChart.Size = new System.Drawing.Size(144, 48);
            // 
            // miStatsOnPriceChart
            // 
            this.miStatsOnPriceChart.Name = "miStatsOnPriceChart";
            this.miStatsOnPriceChart.Size = new System.Drawing.Size(143, 22);
            this.miStatsOnPriceChart.Text = "Stats on price chart";
            // 
            // miAdditionalStats
            // 
            this.miAdditionalStats.Name = "miAdditionalStats";
            this.miAdditionalStats.Size = new System.Drawing.Size(143, 22);
            this.miAdditionalStats.Text = "Additional stats";
            // 
            // chartPV
            // 
            this.chartPV.ContextMenuStrip = this.cmnChart;
            this.chartPV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPV.Location = new System.Drawing.Point(0, 77);
            this.chartPV.Name = "chartPV";
            this.chartPV.Size = new System.Drawing.Size(606, 298);
            this.chartPV.TabIndex = 1;
            // 
            // PriceVolumePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartPV);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlButtons);
            this.Name = "PriceVolumePanel";
            this.Size = new System.Drawing.Size(606, 375);
            this.Resize += new System.EventHandler(this.PriceVolumePanel_Resize);
            this.pnlButtons.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.cmnChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnRefresh;
        private PriceVolumeChart chartPV;
        private System.Windows.Forms.CheckBox btnPriceChartLine;
        private System.Windows.Forms.CheckBox btnPriceChartCandle;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblStockInfo;
        private System.Windows.Forms.Label lblSelectedInfo;
        private System.Windows.Forms.Button btnPrependData;
        private System.Windows.Forms.Button btnDataRange;
        private System.Windows.Forms.CheckBox btnMirrorChart;
        private System.Windows.Forms.Label lblStatSelectedInfo;
        private System.Windows.Forms.ContextMenuStrip cmnChart;
        private System.Windows.Forms.ToolStripMenuItem miStatsOnPriceChart;
        private System.Windows.Forms.ToolStripMenuItem miAdditionalStats;
        private System.Windows.Forms.ImageList ilIcons;
    }
}
