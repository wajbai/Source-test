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
    public partial class frmMismatchedDetails : frmFinanceBaseAdd
    {
        DataTable dtMismatchedDetails = new DataTable();
        public frmMismatchedDetails()
        {
            InitializeComponent();
        }

        public frmMismatchedDetails(DataTable dtMismatched)
            : this()
        {
            dtMismatchedDetails = dtMismatched;
        }

        private void frmMismatchedDetails_Load(object sender, EventArgs e)
        {
            if (dtMismatchedDetails != null && dtMismatchedDetails.Rows.Count > 0)
            {
                gcMismatchedDetails.DataSource = dtMismatchedDetails;
                gcMismatchedDetails.RefreshDataSource();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvMismatchedDetails_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvMismatchedDetails.RowCount.ToString();
        }


    }
}