namespace MarketOps.Controls.PointChart
{
    partial class PointChart
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
            this.plotPoints = new ScottPlot.FormsPlot();
            this.SuspendLayout();
            // 
            // plotPoints
            // 
            this.plotPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotPoints.Location = new System.Drawing.Point(0, 0);
            this.plotPoints.Name = "plotPoints";
            this.plotPoints.Size = new System.Drawing.Size(408, 275);
            this.plotPoints.TabIndex = 1;
            this.plotPoints.MouseEnter += new System.EventHandler(this.plotPoints_MouseEnter);
            this.plotPoints.MouseLeave += new System.EventHandler(this.plotPoints_MouseLeave);
            this.plotPoints.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plotPoints_MouseMove);
            // 
            // PointChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plotPoints);
            this.Name = "PointChart";
            this.Size = new System.Drawing.Size(408, 275);
            this.ResumeLayout(false);

        }

        #endregion
        private ScottPlot.FormsPlot plotPoints;
    }
}
