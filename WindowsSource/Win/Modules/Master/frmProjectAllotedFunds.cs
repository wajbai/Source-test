using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ACPP.Modules.Master
{
    public partial class frmProjectAllotedFunds : frmFinanceBaseAdd
    {
        #region Properties

        DataTable ProjectLedgerAmt = new DataTable();

        #endregion

        #region Constructor
        public frmProjectAllotedFunds()
        {
            InitializeComponent();
        }

        public frmProjectAllotedFunds(DataTable dt)
        {
            ProjectLedgerAmt = dt;
        }
        #endregion

        #region Events
        private void btnOk_Click(object sender, EventArgs e)
        {
            bool isValid = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods

        #endregion

    }
}