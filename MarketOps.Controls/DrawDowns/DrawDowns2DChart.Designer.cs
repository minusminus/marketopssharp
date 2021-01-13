namespace MarketOps.Controls.DrawDowns
{
    partial class DrawDowns2DChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartDD2D = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.srcDD2D = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chartDD2D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.srcDD2D)).BeginInit();
            this.SuspendLayout();
            // 
            // chartDD2D
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.LabelStyle.Format = "F2";
            chartArea1.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.Name = "areaDD2D";
            this.chartDD2D.ChartAreas.Add(chartArea1);
            this.chartDD2D.DataSource = this.srcDD2D;
            this.chartDD2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDD2D.Location = new System.Drawing.Point(0, 0);
            this.chartDD2D.Name = "chartDD2D";
            series1.ChartArea = "areaDD2D";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Name = "seriesDD2D";
            series1.XValueMember = "DDValue";
            series1.YValueMembers = "Length";
            this.chartDD2D.Series.Add(series1);
            this.chartDD2D.Size = new System.Drawing.Size(408, 275);
            this.chartDD2D.TabIndex = 0;
            this.chartDD2D.Text = "chart1";
            // 
            // DrawDowns2DChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartDD2D);
            this.Name = "DrawDowns2DChart";
            this.Size = new System.Drawing.Size(408, 275);
            ((System.ComponentModel.ISupportInitialize)(this.chartDD2D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.srcDD2D)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartDD2D;
        private System.Windows.Forms.BindingSource srcDD2D;
    }
}
