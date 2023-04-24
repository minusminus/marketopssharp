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
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.chartPrices = new ScottPlot.FormsPlot();
            this.chartVolume = new ScottPlot.FormsPlot();
            this.pnlCharts.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCharts
            // 
            this.pnlCharts.Controls.Add(this.chartPrices);
            this.pnlCharts.Controls.Add(this.chartVolume);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(0, 0);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Size = new System.Drawing.Size(430, 302);
            this.pnlCharts.TabIndex = 2;
            // 
            // chartPrices
            // 
            this.chartPrices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPrices.Location = new System.Drawing.Point(0, 0);
            this.chartPrices.Name = "chartPrices";
            this.chartPrices.Size = new System.Drawing.Size(430, 243);
            this.chartPrices.TabIndex = 0;
            // 
            // chartVolume
            // 
            this.chartVolume.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chartVolume.Location = new System.Drawing.Point(0, 243);
            this.chartVolume.Name = "chartVolume";
            this.chartVolume.Size = new System.Drawing.Size(430, 59);
            this.chartVolume.TabIndex = 1;
            // 
            // PriceVolumeChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCharts);
            this.Name = "PriceVolumeChart";
            this.Size = new System.Drawing.Size(430, 302);
            this.pnlCharts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlCharts;
        private ScottPlot.FormsPlot chartPrices;
        private ScottPlot.FormsPlot chartVolume;
    }
}
