using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketOps.DataGen;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.Forms
{
    public partial class FormDataGen : Form
    {
        private readonly DataGenerator _dataGenerator;

        public FormDataGen(DataGenerator dataGenerator)
        {
            _dataGenerator = dataGenerator;
            InitializeComponent();
        }

        public void Execute()
        {
            UpdateControls(false);
            ClearProgress();
            tcDataGenParams.SelectedIndex = 0;

            ShowDialog();
        }

        private void UpdateControls(bool stateLocked)
        {
            tcDataGenParams.Enabled = !stateLocked;
            btnGen.Enabled = !stateLocked;
            pnlProgress.Enabled = stateLocked;
        }

        private void AddLog(string msg)
        {
            edtLog.AppendText(msg);
            edtLog.AppendText(Environment.NewLine);
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            UpdateControls(true);
            try
            {
                edtLog.Clear();
                ExecuteDataGeneration();
                if (edtLog.TextLength > 0)
                    tcDataGenParams.SelectedTab = tabLog;
            }
            finally
            {
                UpdateControls(false);
            }
            ClearProgress();
        }

        private void ClearProgress()
        {
            lblProgressType.Text = "";
            lblProgressStock.Text = "";
            lblProgressProg.Text = "";
        }

        private void OnStockStartProcessing(DataOpProcessingInfo info)
        {
            lblProgressStock.Text = $"[{info.CurrentPosition} / {info.TotalCount}] {info.Stock.Name}";
            Application.DoEvents();
        }

        private void OnStockProcessingException(DataOpProcessingInfo info, Exception e)
        {
            AddLog($"{info.Stock.Name} exception: {e.Message}");
        }

        private void OnProcessingException(string info, Exception e)
        {
            AddLog($"{info} exception: {e.Message}");
        }

        private void ExecuteDataGeneration()
        {
            List<Tuple<CheckBox, StockType>> defs = new List<Tuple<CheckBox, StockType>>()
            {
                new Tuple<CheckBox, StockType>(cbGenDailyTypeStock, StockType.Stock),
                new Tuple<CheckBox, StockType>(cbGenDailyTypeIndex, StockType.Index),
                new Tuple<CheckBox, StockType>(cbGenDailyTypeFuture, StockType.Future),
                new Tuple<CheckBox, StockType>(cbGenDailyTypeInvestmentFund, StockType.InvestmentFund),
                new Tuple<CheckBox, StockType>(cbGenDailyTypeNBPCurrency, StockType.NBPCurrency),
                new Tuple<CheckBox, StockType>(cbGenDailyTypeForex, StockType.Forex),
            };
            List<Tuple<CheckBox, DataGenDailyMode>> modes = new List<Tuple<CheckBox, DataGenDailyMode>>()
            {
                new Tuple<CheckBox, DataGenDailyMode>(cbGenModeDailyWeekly, DataGenDailyMode.Weekly),
                new Tuple<CheckBox, DataGenDailyMode>(cbGenModeDailyMonthly, DataGenDailyMode.Monthly)
            };

            AddLog($"Session start: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");
            _dataGenerator.StartSession(OnStockStartProcessing, OnStockProcessingException, OnProcessingException);
            try
            {
                foreach (var tuple in defs)
                    GenerateIfChecked(tuple.Item1, tuple.Item2, modes);
            }
            finally
            {
                _dataGenerator.EndSession();
                AddLog($"Session end: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");
            }
        }

        private void GenerateIfChecked(CheckBox cb, StockType stockType, List<Tuple<CheckBox, DataGenDailyMode>> modes)
        {
            if (!cb.Checked) return;
            lblProgressType.Text = stockType.ToString();
            foreach (var tuple in modes)
                if (tuple.Item1.Checked)
                    GenerateFromDaily(stockType, tuple.Item2);
        }

        private void GenerateFromDaily(StockType stockType, DataGenDailyMode mode)
        {
            switch (mode)
            {
                case DataGenDailyMode.Weekly:
                    _dataGenerator.GenerateWeekly(stockType);
                    break;
                case DataGenDailyMode.Monthly:
                    _dataGenerator.GenerateMonthly(stockType);
                    break;
            }
        }
    }
}
