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

        public DateTime TSFrom => dtFrom.Value;
        public DateTime TSTo => dtTo.Value;

        public bool Execute(DateTime tsFrom, DateTime tsTo)
        {
            dtFrom.Value = tsFrom;
            dtTo.Value = tsTo;
            return (ShowDialog() == DialogResult.OK);
        }
    }
}
