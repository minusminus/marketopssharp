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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dbgEquity = new System.Windows.Forms.DataGridView();
            this.TS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.plotEquity = new ScottPlot.FormsPlot();
            this.plotCapitalUsage = new ScottPlot.FormsPlot();
            ((System.ComponentModel.ISupportInitialize)(this.dbgEquity)).BeginInit();
            this.pnlCharts.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbgEquity
            // 
            this.dbgEquity.AllowUserToAddRows = false;
            this.dbgEquity.AllowUserToDeleteRows = false;
            this.dbgEquity.AllowUserToResizeRows = false;
            this.dbgEquity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgEquity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TS,
            this.Value});
            this.dbgEquity.Dock = System.Windows.Forms.DockStyle.Left;
            this.dbgEquity.Location = new System.Drawing.Point(0, 0);
            this.dbgEquity.Name = "dbgEquity";
            this.dbgEquity.ReadOnly = true;
            this.dbgEquity.RowHeadersVisible = false;
            this.dbgEquity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dbgEquity.Size = new System.Drawing.Size(201, 472);
            this.dbgEquity.TabIndex = 0;
            // 
            // TS
            // 
            this.TS.DataPropertyName = "TS";
            this.TS.HeaderText = "TS";
            this.TS.Name = "TS";
            this.TS.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.DataPropertyName = "Value";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "F2";
            this.Value.DefaultCellStyle = dataGridViewCellStyle1;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // pnlCharts
            // 
            this.pnlCharts.Controls.Add(this.plotEquity);
            this.pnlCharts.Controls.Add(this.plotCapitalUsage);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(201, 0);
            this.pnlCharts.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Size = new System.Drawing.Size(877, 472);
            this.pnlCharts.TabIndex = 2;
            // 
            // plotEquity
            // 
            this.plotEquity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotEquity.Location = new System.Drawing.Point(0, 0);
            this.plotEquity.Name = "plotEquity";
            this.plotEquity.Size = new System.Drawing.Size(877, 397);
            this.plotEquity.TabIndex = 2;
            // 
            // plotCapitalUsage
            // 
            this.plotCapitalUsage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plotCapitalUsage.Location = new System.Drawing.Point(0, 397);
            this.plotCapitalUsage.Name = "plotCapitalUsage";
            this.plotCapitalUsage.Size = new System.Drawing.Size(877, 75);
            this.plotCapitalUsage.TabIndex = 3;
            // 
            // SystemEquityChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCharts);
            this.Controls.Add(this.dbgEquity);
            this.Name = "SystemEquityChart";
            this.Size = new System.Drawing.Size(1078, 472);
            ((System.ComponentModel.ISupportInitialize)(this.dbgEquity)).EndInit();
            this.pnlCharts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dbgEquity;
        private System.Windows.Forms.DataGridViewTextBoxColumn TS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Panel pnlCharts;
        private ScottPlot.FormsPlot plotCapitalUsage;
        private ScottPlot.FormsPlot plotEquity;
    }
}
