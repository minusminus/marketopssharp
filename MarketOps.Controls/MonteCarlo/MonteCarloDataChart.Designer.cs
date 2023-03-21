
namespace MarketOps.Controls.MonteCarlo
{
    partial class MonteCarloDataChart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.plotData = new ScottPlot.FormsPlot();
            this.SuspendLayout();
            // 
            // plotData
            // 
            this.plotData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotData.Location = new System.Drawing.Point(0, 0);
            this.plotData.Name = "plotData";
            this.plotData.Size = new System.Drawing.Size(420, 236);
            this.plotData.TabIndex = 1;
            // 
            // MonteCarloDataChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plotData);
            this.Name = "MonteCarloDataChart";
            this.Size = new System.Drawing.Size(420, 236);
            this.ResumeLayout(false);

        }

        #endregion
        private ScottPlot.FormsPlot plotData;
    }
}
