namespace MarketOps.Controls.StockData
{
    partial class MOParamsEditor
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
            this.srcParams = new System.Windows.Forms.BindingSource(this.components);
            this.dbgParams = new System.Windows.Forms.DataGridView();
            this.ParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.srcParams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbgParams)).BeginInit();
            this.SuspendLayout();
            // 
            // dbgParams
            // 
            this.dbgParams.AllowUserToAddRows = false;
            this.dbgParams.AllowUserToDeleteRows = false;
            this.dbgParams.AllowUserToResizeRows = false;
            this.dbgParams.AutoGenerateColumns = false;
            this.dbgParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamName,
            this.ParamValue});
            this.dbgParams.DataSource = this.srcParams;
            this.dbgParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbgParams.Location = new System.Drawing.Point(0, 0);
            this.dbgParams.Name = "dbgParams";
            this.dbgParams.RowHeadersVisible = false;
            this.dbgParams.RowHeadersWidth = 20;
            this.dbgParams.Size = new System.Drawing.Size(426, 303);
            this.dbgParams.TabIndex = 1;
            // 
            // ParamName
            // 
            this.ParamName.DataPropertyName = "Name";
            this.ParamName.HeaderText = "Name";
            this.ParamName.Name = "ParamName";
            this.ParamName.ReadOnly = true;
            this.ParamName.Width = 150;
            // 
            // ParamValue
            // 
            this.ParamValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParamValue.DataPropertyName = "Value";
            this.ParamValue.HeaderText = "Value";
            this.ParamValue.Name = "ParamValue";
            // 
            // MOParamsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dbgParams);
            this.Name = "MOParamsEditor";
            this.Size = new System.Drawing.Size(426, 303);
            ((System.ComponentModel.ISupportInitialize)(this.srcParams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbgParams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource srcParams;
        private System.Windows.Forms.DataGridView dbgParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamValue;
    }
}
