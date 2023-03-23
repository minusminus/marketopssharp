namespace MarketOps.Controls.SystemEquity
{
    partial class SystemEquityChart
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
            this.plotEquity = new ScottPlot.FormsPlot();
            this.plotCapitalUsage = new ScottPlot.FormsPlot();
            this.pnlCharts.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCharts
            // 
            this.pnlCharts.Controls.Add(this.plotEquity);
            this.pnlCharts.Controls.Add(this.plotCapitalUsage);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(0, 0);
            this.pnlCharts.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Size = new System.Drawing.Size(1078, 472);
            this.pnlCharts.TabIndex = 2;
            // 
            // plotEquity
            // 
            this.plotEquity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotEquity.Location = new System.Drawing.Point(0, 0);
            this.plotEquity.Name = "plotEquity";
            this.plotEquity.Size = new System.Drawing.Size(1078, 397);
            this.plotEquity.TabIndex = 2;
            // 
            // plotCapitalUsage
            // 
            this.plotCapitalUsage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plotCapitalUsage.Location = new System.Drawing.Point(0, 397);
            this.plotCapitalUsage.Name = "plotCapitalUsage";
            this.plotCapitalUsage.Size = new System.Drawing.Size(1078, 75);
            this.plotCapitalUsage.TabIndex = 3;
            // 
            // SystemEquityChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCharts);
            this.Name = "SystemEquityChart";
            this.Size = new System.Drawing.Size(1078, 472);
            this.pnlCharts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlCharts;
        private ScottPlot.FormsPlot plotCapitalUsage;
        private ScottPlot.FormsPlot plotEquity;
    }
}
