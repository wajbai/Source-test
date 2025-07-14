using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ACPP.Modules.Dsync
{
    public partial class frmMismatchedLedgerList : frmFinanceBaseAdd
    {
        DataTable dtMismatchedDetails = new DataTable();
        public frmMismatchedLedgerList()
        {
            InitializeComponent();
        }

        public frmMismatchedLedgerList(DataTable dtMismatched)
            : this()
        {
            dtMismatchedDetails = dtMismatched;
        }

        private void frmMismatchedLedgerList_Load(object sender, EventArgs e)
        {
            if (dtMismatchedDetails != null && dtMismatchedDetails.Rows.Count > 0)
            {
                gcMismatchedLedgers.DataSource = dtMismatchedDetails;
                gcMismatchedLedgers.RefreshDataSource();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvMismatchedLedgers_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvMismatchedLedgers.RowCount.ToString();
        }
    }
}