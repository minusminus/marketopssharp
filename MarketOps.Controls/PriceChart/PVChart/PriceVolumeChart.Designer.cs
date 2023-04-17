namespace MarketOps.Controls.PriceChart.PVChart
{
    partial class PriceVolumeChart
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
            this.pnlCursorDataValues = new System.Windows.Forms.Panel();
            this.lblValueValue = new System.Windows.Forms.Label();
            this.lblValueInfo = new System.Windows.Forms.Label();
            this.lblTSValue = new System.Windows.Forms.Label();
            this.lblTSInfo = new System.Windows.Forms.Label();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.chartPrices = new ScottPlot.FormsPlot();
            this.chartVolume = new ScottPlot.FormsPlot();
            this.pnlCursorDataValues.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCursorDataValues
            // 
            this.pnlCursorDataValues.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCursorDataValues.Controls.Add(this.lblValueValue);
            this.pnlCursorDataValues.Controls.Add(this.lblValueInfo);
            this.pnlCursorDataValues.Controls.Add(this.lblTSValue);
            this.pnlCursorDataValues.Controls.Add(this.lblTSInfo);
            this.pnlCursorDataValues.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCursorDataValues.Location = new System.Drawing.Point(0, 0);
            this.pnlCursorDataValues.Name = "pnlCursorDataValues";
            this.pnlCursorDataValues.Size = new System.Drawing.Size(430, 30);
            this.pnlCursorDataValues.TabIndex = 1;
            // 
            // lblValueValue
            // 
            this.lblValueValue.AutoSize = true;
            this.lblValueValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblValueValue.Location = new System.Drawing.Point(41, 14);
            this.lblValueValue.Name = "lblValueValue";
            this.lblValueValue.Size = new System.Drawing.Size(35, 12);
            this.lblValueValue.TabIndex = 3;
            this.lblValueValue.Text = "label4";
            // 
            // lblValueInfo
            // 
            this.lblValueInfo.AutoSize = true;
            this.lblValueInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblValueInfo.Location = new System.Drawing.Point(3, 14);
            this.lblValueInfo.Name = "lblValueInfo";
            this.lblValueInfo.Size = new System.Drawing.Size(32, 12);
            this.lblValueInfo.TabIndex = 2;
            this.lblValueInfo.Text = "Value:";
            // 
            // lblTSValue
            // 
            this.lblTSValue.AutoSize = true;
            this.lblTSValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTSValue.Location = new System.Drawing.Point(41, 2);
            this.lblTSValue.Name = "lblTSValue";
            this.lblTSValue.Size = new System.Drawing.Size(35, 12);
            this.lblTSValue.TabIndex = 1;
            this.lblTSValue.Text = "label2";
            // 
            // lblTSInfo
            // 
            this.lblTSInfo.AutoSize = true;
            this.lblTSInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTSInfo.Location = new System.Drawing.Point(3, 2);
            this.lblTSInfo.Name = "lblTSInfo";
            this.lblTSInfo.Size = new System.Drawing.Size(19, 12);
            this.lblTSInfo.TabIndex = 0;
            this.lblTSInfo.Text = "TS:";
            // 
            // pnlCharts
            // 
            this.pnlCharts.Controls.Add(this.chartPrices);
            this.pnlCharts.Controls.Add(this.chartVolume);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(0, 30);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Size = new System.Drawing.Size(430, 272);
            this.pnlCharts.TabIndex = 2;
            // 
            // chartPrices
            // 
            this.chartPrices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPrices.Location = new System.Drawing.Point(0, 0);
            this.chartPrices.Name = "chartPrices";
            this.chartPrices.Size = new System.Drawing.Size(430, 213);
            this.chartPrices.TabIndex = 0;
            // 
            // chartVolume
            // 
            this.chartVolume.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chartVolume.Location = new System.Drawing.Point(0, 213);
            this.chartVolume.Name = "chartVolume";
            this.chartVolume.Size = new System.Drawing.Size(430, 59);
            this.chartVolume.TabIndex = 1;
            // 
            // PriceVolumeChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCharts);
            this.Controls.Add(this.pnlCursorDataValues);
            this.Name = "PriceVolumeChart";
            this.Size = new System.Drawing.Size(430, 302);
            this.pnlCursorDataValues.ResumeLayout(false);
            this.pnlCursorDataValues.PerformLayout();
            this.pnlCharts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlCursorDataValues;
        private System.Windows.Forms.Label lblValueValue;
        private System.Windows.Forms.Label lblValueInfo;
        private System.Windows.Forms.Label lblTSValue;
        private System.Windows.Forms.Label lblTSInfo;
        private System.Windows.Forms.Panel pnlCharts;
        private ScottPlot.FormsPlot chartPrices;
        private ScottPlot.FormsPlot chartVolume;
    }
}
