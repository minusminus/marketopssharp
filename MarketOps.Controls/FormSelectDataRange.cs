using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketOps.Controls
{
    public partial class FormSelectDataRange : Form
    {
        public FormSelectDataRange()
        {
            InitializeComponent();
        }

        public DateTime TsFrom => dtFrom.Value;
        public DateTime TsTo => dtTo.Value;

        public bool Execute(DateTime tsFrom, DateTime tsTo, string tsFormat)
        {
            dtFrom.CustomFormat = tsFormat;
            dtTo.CustomFormat = tsFormat;
            dtFrom.Value = tsFrom;
            dtTo.Value = tsTo;
            return (ShowDialog() == DialogResult.OK);
        }
    }
}
