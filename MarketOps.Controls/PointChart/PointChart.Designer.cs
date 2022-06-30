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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartPoints = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPoints
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
            chartArea1.Name = "areaPoints";
            this.chartPoints.ChartAreas.Add(chartArea1);
            this.chartPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPoints.Location = new System.Drawing.Point(0, 0);
            this.chartPoints.Name = "chartPoints";
            series1.ChartArea = "areaPoints";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Name = "seriesPoints";
            series1.XValueMember = "X";
            series1.YValueMembers = "Y";
            this.chartPoints.Series.Add(series1);
            this.chartPoints.Size = new System.Drawing.Size(408, 275);
            this.chartPoints.TabIndex = 0;
            this.chartPoints.Text = "chart1";
            // 
            // PointChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartPoints);
            this.Name = "PointChart";
            this.Size = new System.Drawing.Size(408, 275);
            ((System.ComponentModel.ISupportInitialize)(this.chartPoints)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPoints;
    }
}
