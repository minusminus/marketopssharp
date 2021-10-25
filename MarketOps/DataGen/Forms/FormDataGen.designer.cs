namespace MarketOps.DataPump.Forms
{
    partial class FormDataGen
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcDataGenParams = new System.Windows.Forms.TabControl();
            this.tabDaily = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbGenModeDailyMonthly = new System.Windows.Forms.CheckBox();
            this.cbGenModeDailyWeekly = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbGenDailyTypeForex = new System.Windows.Forms.CheckBox();
            this.cbGenDailyTypeNBPCurrency = new System.Windows.Forms.CheckBox();
            this.cbGenDailyTypeInvestmentFund = new System.Windows.Forms.CheckBox();
            this.cbGenDailyTypeFuture = new System.Windows.Forms.CheckBox();
            this.cbGenDailyTypeIndex = new System.Windows.Forms.CheckBox();
            this.cbGenDailyTypeStock = new System.Windows.Forms.CheckBox();
            this.tabIntra = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.edtLog = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGen = new System.Windows.Forms.Button();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.lblProgressType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProgressProg = new System.Windows.Forms.Label();
            this.lblProgressStock = new System.Windows.Forms.Label();
            this.lblImportProgressCaption2 = new System.Windows.Forms.Label();
            this.lblImportProgressCaption1 = new System.Windows.Forms.Label();
            this.tcDataGenParams.SuspendLayout();
            this.tabDaily.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabIntra.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcDataGenParams
            // 
            this.tcDataGenParams.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcDataGenParams.Controls.Add(this.tabDaily);
            this.tcDataGenParams.Controls.Add(this.tabIntra);
            this.tcDataGenParams.Controls.Add(this.tabLog);
            this.tcDataGenParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDataGenParams.Location = new System.Drawing.Point(5, 18);
            this.tcDataGenParams.Name = "tcDataGenParams";
            this.tcDataGenParams.SelectedIndex = 0;
            this.tcDataGenParams.Size = new System.Drawing.Size(257, 193);
            this.tcDataGenParams.TabIndex = 0;
            this.tcDataGenParams.TabStop = false;
            // 
            // tabDaily
            // 
            this.tabDaily.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabDaily.Controls.Add(this.groupBox3);
            this.tabDaily.Controls.Add(this.groupBox2);
            this.tabDaily.Location = new System.Drawing.Point(4, 25);
            this.tabDaily.Name = "tabDaily";
            this.tabDaily.Padding = new System.Windows.Forms.Padding(3);
            this.tabDaily.Size = new System.Drawing.Size(249, 164);
            this.tabDaily.TabIndex = 0;
            this.tabDaily.Text = "Daily";
            this.tabDaily.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbGenModeDailyMonthly);
            this.groupBox3.Controls.Add(this.cbGenModeDailyWeekly);
            this.groupBox3.Location = new System.Drawing.Point(142, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(99, 148);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mode";
            // 
            // cbGenModeDailyMonthly
            // 
            this.cbGenModeDailyMonthly.AutoSize = true;
            this.cbGenModeDailyMonthly.Location = new System.Drawing.Point(6, 40);
            this.cbGenModeDailyMonthly.Name = "cbGenModeDailyMonthly";
            this.cbGenModeDailyMonthly.Size = new System.Drawing.Size(63, 17);
            this.cbGenModeDailyMonthly.TabIndex = 3;
            this.cbGenModeDailyMonthly.Text = "Monthly";
            this.cbGenModeDailyMonthly.UseVisualStyleBackColor = true;
            // 
            // cbGenModeDailyWeekly
            // 
            this.cbGenModeDailyWeekly.AutoSize = true;
            this.cbGenModeDailyWeekly.Location = new System.Drawing.Point(6, 20);
            this.cbGenModeDailyWeekly.Name = "cbGenModeDailyWeekly";
            this.cbGenModeDailyWeekly.Size = new System.Drawing.Size(62, 17);
            this.cbGenModeDailyWeekly.TabIndex = 2;
            this.cbGenModeDailyWeekly.Text = "Weekly";
            this.cbGenModeDailyWeekly.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbGenDailyTypeForex);
            this.groupBox2.Controls.Add(this.cbGenDailyTypeNBPCurrency);
            this.groupBox2.Controls.Add(this.cbGenDailyTypeInvestmentFund);
            this.groupBox2.Controls.Add(this.cbGenDailyTypeFuture);
            this.groupBox2.Controls.Add(this.cbGenDailyTypeIndex);
            this.groupBox2.Controls.Add(this.cbGenDailyTypeStock);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(130, 148);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data type";
            // 
            // cbGenDailyTypeForex
            // 
            this.cbGenDailyTypeForex.AutoSize = true;
            this.cbGenDailyTypeForex.Location = new System.Drawing.Point(7, 120);
            this.cbGenDailyTypeForex.Name = "cbGenDailyTypeForex";
            this.cbGenDailyTypeForex.Size = new System.Drawing.Size(52, 17);
            this.cbGenDailyTypeForex.TabIndex = 5;
            this.cbGenDailyTypeForex.Text = "Forex";
            this.cbGenDailyTypeForex.UseVisualStyleBackColor = true;
            // 
            // cbGenDailyTypeNBPCurrency
            // 
            this.cbGenDailyTypeNBPCurrency.AutoSize = true;
            this.cbGenDailyTypeNBPCurrency.Location = new System.Drawing.Point(7, 100);
            this.cbGenDailyTypeNBPCurrency.Name = "cbGenDailyTypeNBPCurrency";
            this.cbGenDailyTypeNBPCurrency.Size = new System.Drawing.Size(100, 17);
            this.cbGenDailyTypeNBPCurrency.TabIndex = 4;
            this.cbGenDailyTypeNBPCurrency.Text = "NBP currencies";
            this.cbGenDailyTypeNBPCurrency.UseVisualStyleBackColor = true;
            // 
            // cbGenDailyTypeInvestmentFund
            // 
            this.cbGenDailyTypeInvestmentFund.AutoSize = true;
            this.cbGenDailyTypeInvestmentFund.Location = new System.Drawing.Point(7, 80);
            this.cbGenDailyTypeInvestmentFund.Name = "cbGenDailyTypeInvestmentFund";
            this.cbGenDailyTypeInvestmentFund.Size = new System.Drawing.Size(107, 17);
            this.cbGenDailyTypeInvestmentFund.TabIndex = 3;
            this.cbGenDailyTypeInvestmentFund.Text = "Investment funds";
            this.cbGenDailyTypeInvestmentFund.UseVisualStyleBackColor = true;
            // 
            // cbGenDailyTypeFuture
            // 
            this.cbGenDailyTypeFuture.AutoSize = true;
            this.cbGenDailyTypeFuture.Location = new System.Drawing.Point(7, 60);
            this.cbGenDailyTypeFuture.Name = "cbGenDailyTypeFuture";
            this.cbGenDailyTypeFuture.Size = new System.Drawing.Size(61, 17);
            this.cbGenDailyTypeFuture.TabIndex = 2;
            this.cbGenDailyTypeFuture.Text = "Futures";
            this.cbGenDailyTypeFuture.UseVisualStyleBackColor = true;
            // 
            // cbGenDailyTypeIndex
            // 
            this.cbGenDailyTypeIndex.AutoSize = true;
            this.cbGenDailyTypeIndex.Location = new System.Drawing.Point(7, 40);
            this.cbGenDailyTypeIndex.Name = "cbGenDailyTypeIndex";
            this.cbGenDailyTypeIndex.Size = new System.Drawing.Size(63, 17);
            this.cbGenDailyTypeIndex.TabIndex = 1;
            this.cbGenDailyTypeIndex.Text = "Indexes";
            this.cbGenDailyTypeIndex.UseVisualStyleBackColor = true;
            // 
            // cbGenDailyTypeStock
            // 
            this.cbGenDailyTypeStock.AutoSize = true;
            this.cbGenDailyTypeStock.Location = new System.Drawing.Point(7, 20);
            this.cbGenDailyTypeStock.Name = "cbGenDailyTypeStock";
            this.cbGenDailyTypeStock.Size = new System.Drawing.Size(59, 17);
            this.cbGenDailyTypeStock.TabIndex = 0;
            this.cbGenDailyTypeStock.Text = "Stocks";
            this.cbGenDailyTypeStock.UseVisualStyleBackColor = true;
            // 
            // tabIntra
            // 
            this.tabIntra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabIntra.Controls.Add(this.label1);
            this.tabIntra.Location = new System.Drawing.Point(4, 25);
            this.tabIntra.Name = "tabIntra";
            this.tabIntra.Padding = new System.Windows.Forms.Padding(3);
            this.tabIntra.Size = new System.Drawing.Size(249, 164);
            this.tabIntra.TabIndex = 1;
            this.tabIntra.Text = "Intra";
            this.tabIntra.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Intraday generation is not supported";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.edtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 25);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(249, 164);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // edtLog
            // 
            this.edtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtLog.Location = new System.Drawing.Point(3, 3);
            this.edtLog.Multiline = true;
            this.edtLog.Name = "edtLog";
            this.edtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.edtLog.Size = new System.Drawing.Size(243, 158);
            this.edtLog.TabIndex = 0;
            this.edtLog.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tcDataGenParams);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(267, 216);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generate from";
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(21, 243);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(252, 23);
            this.btnGen.TabIndex = 2;
            this.btnGen.Text = "Generate";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // pnlProgress
            // 
            this.pnlProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProgress.Controls.Add(this.lblProgressType);
            this.pnlProgress.Controls.Add(this.label3);
            this.pnlProgress.Controls.Add(this.lblProgressProg);
            this.pnlProgress.Controls.Add(this.lblProgressStock);
            this.pnlProgress.Controls.Add(this.lblImportProgressCaption2);
            this.pnlProgress.Controls.Add(this.lblImportProgressCaption1);
            this.pnlProgress.Location = new System.Drawing.Point(12, 272);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(267, 63);
            this.pnlProgress.TabIndex = 3;
            // 
            // lblProgressType
            // 
            this.lblProgressType.AutoSize = true;
            this.lblProgressType.Location = new System.Drawing.Point(78, 10);
            this.lblProgressType.Name = "lblProgressType";
            this.lblProgressType.Size = new System.Drawing.Size(82, 13);
            this.lblProgressType.TabIndex = 5;
            this.lblProgressType.Text = "lblProgressType";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Type:";
            // 
            // lblProgressProg
            // 
            this.lblProgressProg.AutoSize = true;
            this.lblProgressProg.Location = new System.Drawing.Point(78, 40);
            this.lblProgressProg.Name = "lblProgressProg";
            this.lblProgressProg.Size = new System.Drawing.Size(80, 13);
            this.lblProgressProg.TabIndex = 3;
            this.lblProgressProg.Text = "lblProgressProg";
            // 
            // lblProgressStock
            // 
            this.lblProgressStock.AutoSize = true;
            this.lblProgressStock.Location = new System.Drawing.Point(78, 25);
            this.lblProgressStock.Name = "lblProgressStock";
            this.lblProgressStock.Size = new System.Drawing.Size(86, 13);
            this.lblProgressStock.TabIndex = 2;
            this.lblProgressStock.Text = "lblProgressStock";
            // 
            // lblImportProgressCaption2
            // 
            this.lblImportProgressCaption2.AutoSize = true;
            this.lblImportProgressCaption2.Location = new System.Drawing.Point(10, 40);
            this.lblImportProgressCaption2.Name = "lblImportProgressCaption2";
            this.lblImportProgressCaption2.Size = new System.Drawing.Size(51, 13);
            this.lblImportProgressCaption2.TabIndex = 1;
            this.lblImportProgressCaption2.Text = "Progress:";
            // 
            // lblImportProgressCaption1
            // 
            this.lblImportProgressCaption1.AutoSize = true;
            this.lblImportProgressCaption1.Location = new System.Drawing.Point(10, 25);
            this.lblImportProgressCaption1.Name = "lblImportProgressCaption1";
            this.lblImportProgressCaption1.Size = new System.Drawing.Size(38, 13);
            this.lblImportProgressCaption1.TabIndex = 0;
            this.lblImportProgressCaption1.Text = "Stock:";
            // 
            // FormDataGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 347);
            this.Controls.Add(this.pnlProgress);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataGen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Generation";
            this.tcDataGenParams.ResumeLayout(false);
            this.tabDaily.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabIntra.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcDataGenParams;
        private System.Windows.Forms.TabPage tabDaily;
        private System.Windows.Forms.TabPage tabIntra;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbGenDailyTypeFuture;
        private System.Windows.Forms.CheckBox cbGenDailyTypeIndex;
        private System.Windows.Forms.CheckBox cbGenDailyTypeStock;
        private System.Windows.Forms.CheckBox cbGenDailyTypeForex;
        private System.Windows.Forms.CheckBox cbGenDailyTypeNBPCurrency;
        private System.Windows.Forms.CheckBox cbGenDailyTypeInvestmentFund;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.Label lblProgressProg;
        private System.Windows.Forms.Label lblProgressStock;
        private System.Windows.Forms.Label lblImportProgressCaption2;
        private System.Windows.Forms.Label lblImportProgressCaption1;
        private System.Windows.Forms.Label lblProgressType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TextBox edtLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbGenModeDailyMonthly;
        private System.Windows.Forms.CheckBox cbGenModeDailyWeekly;
    }
}