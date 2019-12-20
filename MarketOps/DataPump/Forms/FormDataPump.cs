using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.Forms
{
    public partial class FormDataPump : Form
    {
        private readonly DataPumper _dataPumper;

        public FormDataPump(DataPumper dataPumper)
        {
            _dataPumper = dataPumper;
            InitializeComponent();
        }

        public void Execute()
        {
            UpdateControls(false);
            ClearPumpProgress();
            tcDataPumpParams.SelectedIndex = 0;

            ShowDialog();
        }

        private void UpdateControls(bool stateLocked)
        {
            tcDataPumpParams.Enabled = !stateLocked;
            btnImport.Enabled = !stateLocked;
            pnlImportProgress.Enabled = stateLocked;
        }

        private void AddLog(string msg)
        {
            edtLog.AppendText(msg);
            edtLog.AppendText(Environment.NewLine);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            UpdateControls(true);
            try
            {
                edtLog.Clear();
                ExecuteDataPumping();
                if (edtLog.TextLength > 0)
                    tcDataPumpParams.SelectedTab = tabLog;
            }
            finally
            {
                UpdateControls(false);
            }
            ClearPumpProgress();
        }

        private void ClearPumpProgress()
        {
            lblImportProgressType.Text = "";
            lblImportProgressStock.Text = "";
            lblImportProgressProg.Text = "";
        }

        private void OnStockStartProcessing(DataPumperDailyProcessingInfo info)
        {
            lblImportProgressStock.Text = $"[{info.CurrentPosition} / {info.TotalCount}] {info.Stock.Name}";
            Application.DoEvents();
        }

        private void OnStockProcessingException(DataPumperDailyProcessingInfo info, Exception e)
        {
            AddLog($"{info.Stock.Name} exception: {e.Message}");
        }

        private void OnProcessingException(string info, Exception e)
        {
            AddLog($"{info} exception: {e.Message}");
        }

        private void ExecuteDataPumping()
        {
            List<Tuple<CheckBox, StockType>> defs = new List<Tuple<CheckBox, StockType>>()
            {
                new Tuple<CheckBox, StockType>(cbDPDailyTypeStock, StockType.Stock),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeIndex, StockType.Index),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeFuture, StockType.Future),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeInvestmentFund, StockType.InvestmentFund),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeNBPCurrency, StockType.NBPCurrency),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeForex, StockType.Forex),
            };

            AddLog($"Session start: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");
            _dataPumper.StartSession(OnStockStartProcessing, OnStockProcessingException, OnProcessingException);
            try
            {
                foreach (var tuple in defs)
                    PumpIfChecked(tuple.Item1, tuple.Item2);
            }
            finally
            {
                _dataPumper.EndSession();
                AddLog($"Session end: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");
            }
        }

        private void PumpIfChecked(CheckBox cb, StockType stockType)
        {
            if (!cb.Checked) return;
            lblImportProgressType.Text = stockType.ToString();
            _dataPumper.PumpDaily(stockType);
        }
    }
}
