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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.dbgEquity = new System.Windows.Forms.DataGridView();
            this.TS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartEquity = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.dbgEquity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEquity)).BeginInit();
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
            // chartEquity
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.Name = "areaEquity";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 80F;
            chartArea1.Position.Width = 100F;
            chartArea2.AlignWithChartArea = "areaEquity";
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.IsStartedFromZero = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.Name = "areaCapitalUsage";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 20F;
            chartArea2.Position.Width = 100F;
            chartArea2.Position.Y = 80F;
            this.chartEquity.ChartAreas.Add(chartArea1);
            this.chartEquity.ChartAreas.Add(chartArea2);
            this.chartEquity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEquity.Location = new System.Drawing.Point(201, 0);
            this.chartEquity.Name = "chartEquity";
            series1.ChartArea = "areaEquity";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "seriesEquity";
            series1.XValueMember = "TS";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "Value";
            series2.ChartArea = "areaCapitalUsage";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "seriesCapitalUsage";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chartEquity.Series.Add(series1);
            this.chartEquity.Series.Add(series2);
            this.chartEquity.Size = new System.Drawing.Size(877, 472);
            this.chartEquity.TabIndex = 1;
            this.chartEquity.Text = "chart1";
            title1.DockedToChartArea = "areaCapitalUsage";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            title1.ForeColor = System.Drawing.Color.DarkGray;
            title1.Name = "titleCapitalUsage";
            title1.Text = "Capital usage [%]";
            this.chartEquity.Titles.Add(title1);
            // 
            // SystemEquityChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartEquity);
            this.Controls.Add(this.dbgEquity);
            this.Name = "SystemEquityChart";
            this.Size = new System.Drawing.Size(1078, 472);
            ((System.ComponentModel.ISupportInitialize)(this.dbgEquity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEquity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dbgEquity;
        private System.Windows.Forms.DataGridViewTextBoxColumn TS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEquity;
    }
}
