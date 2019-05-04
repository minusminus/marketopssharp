﻿namespace MarketOps.Controls
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
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chartPV = new MarketOps.Controls.PriceVolumeChart();
            this.btnPriceChartLine = new System.Windows.Forms.CheckBox();
            this.btnPriceChartCandle = new System.Windows.Forms.CheckBox();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblStockInfo = new System.Windows.Forms.Label();
            this.lblSelectedInfo = new System.Windows.Forms.Label();
            this.btnPrependData = new System.Windows.Forms.Button();
            this.pnlButtons.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
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
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(10, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 24);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Text = "s";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // chartPV
            // 
            this.chartPV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPV.Location = new System.Drawing.Point(0, 32);
            this.chartPV.Name = "chartPV";
            this.chartPV.Size = new System.Drawing.Size(606, 343);
            this.chartPV.TabIndex = 1;
            // 
            // btnPriceChartLine
            // 
            this.btnPriceChartLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPriceChartLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPriceChartLine.Location = new System.Drawing.Point(316, 4);
            this.btnPriceChartLine.Name = "btnPriceChartLine";
            this.btnPriceChartLine.Size = new System.Drawing.Size(32, 24);
            this.btnPriceChartLine.TabIndex = 3;
            this.btnPriceChartLine.TabStop = false;
            this.btnPriceChartLine.Text = "L";
            this.btnPriceChartLine.UseVisualStyleBackColor = true;
            this.btnPriceChartLine.CheckedChanged += new System.EventHandler(this.btnPriceChartLine_CheckedChanged);
            // 
            // btnPriceChartCandle
            // 
            this.btnPriceChartCandle.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPriceChartCandle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPriceChartCandle.Location = new System.Drawing.Point(354, 4);
            this.btnPriceChartCandle.Name = "btnPriceChartCandle";
            this.btnPriceChartCandle.Size = new System.Drawing.Size(32, 24);
            this.btnPriceChartCandle.TabIndex = 4;
            this.btnPriceChartCandle.TabStop = false;
            this.btnPriceChartCandle.Text = "C";
            this.btnPriceChartCandle.UseVisualStyleBackColor = true;
            this.btnPriceChartCandle.CheckedChanged += new System.EventHandler(this.btnPriceChartCandle_CheckedChanged);
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblSelectedInfo);
            this.pnlInfo.Controls.Add(this.lblStockInfo);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.Location = new System.Drawing.Point(0, 32);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(606, 32);
            this.pnlInfo.TabIndex = 2;
            // 
            // lblStockInfo
            // 
            this.lblStockInfo.AutoSize = true;
            this.lblStockInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStockInfo.Location = new System.Drawing.Point(2, 2);
            this.lblStockInfo.Name = "lblStockInfo";
            this.lblStockInfo.Size = new System.Drawing.Size(29, 12);
            this.lblStockInfo.TabIndex = 0;
            this.lblStockInfo.Text = "label1";
            // 
            // lblSelectedInfo
            // 
            this.lblSelectedInfo.AutoSize = true;
            this.lblSelectedInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSelectedInfo.Location = new System.Drawing.Point(2, 16);
            this.lblSelectedInfo.Name = "lblSelectedInfo";
            this.lblSelectedInfo.Size = new System.Drawing.Size(29, 12);
            this.lblSelectedInfo.TabIndex = 1;
            this.lblSelectedInfo.Text = "label1";
            // 
            // btnPrependData
            // 
            this.btnPrependData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrependData.Location = new System.Drawing.Point(58, 4);
            this.btnPrependData.Name = "btnPrependData";
            this.btnPrependData.Size = new System.Drawing.Size(32, 24);
            this.btnPrependData.TabIndex = 5;
            this.btnPrependData.TabStop = false;
            this.btnPrependData.Text = "prepend";
            this.btnPrependData.UseVisualStyleBackColor = true;
            this.btnPrependData.Click += new System.EventHandler(this.btnPrependData_Click);
            // 
            // PriceVolumePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.chartPV);
            this.Controls.Add(this.pnlButtons);
            this.Name = "PriceVolumePanel";
            this.Size = new System.Drawing.Size(606, 375);
            this.pnlButtons.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
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
    }
}
