namespace MarketOps.Controls.SystemPositionsGrid
{
    partial class SystemPositionsGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dbgPositions = new System.Windows.Forms.DataGridView();
            this.srcPositions = new System.Windows.Forms.BindingSource(this.components);
            this.LP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Profit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TSOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Open = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenCommission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TSClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Close = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CloseCommission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ticks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dbgPositions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.srcPositions)).BeginInit();
            this.SuspendLayout();
            // 
            // dbgPositions
            // 
            this.dbgPositions.AllowUserToAddRows = false;
            this.dbgPositions.AllowUserToDeleteRows = false;
            this.dbgPositions.AllowUserToResizeRows = false;
            this.dbgPositions.AutoGenerateColumns = false;
            this.dbgPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgPositions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LP,
            this.StockName,
            this.Dir,
            this.Profit,
            this.TSOpen,
            this.Open,
            this.OpenCommission,
            this.TSClose,
            this.Close,
            this.CloseCommission,
            this.Volume,
            this.Ticks});
            this.dbgPositions.DataSource = this.srcPositions;
            this.dbgPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbgPositions.Location = new System.Drawing.Point(0, 0);
            this.dbgPositions.Name = "dbgPositions";
            this.dbgPositions.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dbgPositions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dbgPositions.RowHeadersVisible = false;
            this.dbgPositions.Size = new System.Drawing.Size(1085, 274);
            this.dbgPositions.TabIndex = 0;
            // 
            // LP
            // 
            this.LP.DataPropertyName = "LP";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LP.DefaultCellStyle = dataGridViewCellStyle1;
            this.LP.HeaderText = "LP";
            this.LP.Name = "LP";
            this.LP.ReadOnly = true;
            this.LP.Width = 40;
            // 
            // StockName
            // 
            this.StockName.DataPropertyName = "StockName";
            this.StockName.HeaderText = "Stock";
            this.StockName.Name = "StockName";
            this.StockName.ReadOnly = true;
            // 
            // Dir
            // 
            this.Dir.DataPropertyName = "Dir";
            this.Dir.HeaderText = "Dir";
            this.Dir.Name = "Dir";
            this.Dir.ReadOnly = true;
            this.Dir.Width = 40;
            // 
            // Profit
            // 
            this.Profit.DataPropertyName = "Profit";
            dataGridViewCellStyle2.Format = "F4";
            this.Profit.DefaultCellStyle = dataGridViewCellStyle2;
            this.Profit.HeaderText = "Profit";
            this.Profit.Name = "Profit";
            this.Profit.ReadOnly = true;
            this.Profit.Width = 70;
            // 
            // TSOpen
            // 
            this.TSOpen.DataPropertyName = "TSOpen";
            this.TSOpen.HeaderText = "Open TS";
            this.TSOpen.Name = "TSOpen";
            this.TSOpen.ReadOnly = true;
            // 
            // Open
            // 
            this.Open.DataPropertyName = "Open";
            this.Open.HeaderText = "Open";
            this.Open.Name = "Open";
            this.Open.ReadOnly = true;
            this.Open.Width = 70;
            // 
            // OpenCommission
            // 
            this.OpenCommission.DataPropertyName = "OpenCommission";
            this.OpenCommission.HeaderText = "Comm.";
            this.OpenCommission.Name = "OpenCommission";
            this.OpenCommission.ReadOnly = true;
            this.OpenCommission.Width = 50;
            // 
            // TSClose
            // 
            this.TSClose.DataPropertyName = "TSClose";
            this.TSClose.HeaderText = "Close TS";
            this.TSClose.Name = "TSClose";
            this.TSClose.ReadOnly = true;
            // 
            // Close
            // 
            this.Close.DataPropertyName = "Close";
            this.Close.HeaderText = "Close";
            this.Close.Name = "Close";
            this.Close.ReadOnly = true;
            this.Close.Width = 70;
            // 
            // CloseCommission
            // 
            this.CloseCommission.DataPropertyName = "CloseCommission";
            this.CloseCommission.HeaderText = "Comm.";
            this.CloseCommission.Name = "CloseCommission";
            this.CloseCommission.ReadOnly = true;
            this.CloseCommission.Width = 50;
            // 
            // Volume
            // 
            this.Volume.DataPropertyName = "Volume";
            this.Volume.HeaderText = "Volume";
            this.Volume.Name = "Volume";
            this.Volume.ReadOnly = true;
            this.Volume.Width = 70;
            // 
            // Ticks
            // 
            this.Ticks.DataPropertyName = "Ticks";
            this.Ticks.HeaderText = "Ticks";
            this.Ticks.Name = "Ticks";
            this.Ticks.ReadOnly = true;
            this.Ticks.Width = 50;
            // 
            // SystemPositionsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dbgPositions);
            this.Name = "SystemPositionsGrid";
            this.Size = new System.Drawing.Size(1085, 274);
            ((System.ComponentModel.ISupportInitialize)(this.dbgPositions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.srcPositions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dbgPositions;
        private System.Windows.Forms.BindingSource srcPositions;
        private System.Windows.Forms.DataGridViewTextBoxColumn LP;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Profit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TSOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Open;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenCommission;
        private System.Windows.Forms.DataGridViewTextBoxColumn TSClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Close;
        private System.Windows.Forms.DataGridViewTextBoxColumn CloseCommission;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ticks;
    }
}
