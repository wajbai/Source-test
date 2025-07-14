using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmProgressBar : frmFinanceBaseAdd
    {
        public event EventHandler OnProgressBarClose;
        public frmProgressBar()
        {
            InitializeComponent();
        }

        private void frmProgressBar_Load(object sender, EventArgs e)
        {
            InitializeProgressBarControl();
        }

        private void InitializeProgressBarControl()
        {
            progressBar.Properties.Minimum = 0;
            progressBar.Properties.Maximum = 0;
            progressBar.Properties.Step = 1;
            progressBar.PerformStep();
            progressBar.Visible = true;
        }

        public void OnUpdateProgressBar(object sender, EventArgs e)
        {
            progressBar.Invoke(new MethodInvoker(delegate { progressBar.PerformStep(); Application.DoEvents(); }));
        }

        public void OnUpdateProgressBarMaimum(object sender, EventArgs e)
        {
            progressBar.Invoke(new MethodInvoker(delegate { progressBar.Properties.Maximum = 2000; }));
        }

        //public void OnProgressBarClose(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
    }
}
