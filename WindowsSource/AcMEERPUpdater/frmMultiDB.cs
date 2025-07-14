using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AcMEERPUpdater
{
    public partial class frmMultiDB : Form
    {
        public DialogResult dialogResult = DialogResult.Cancel;
        public frmMultiDB()
        {
            InitializeComponent();
        }

        private void frmMultiDB_Load(object sender, EventArgs e)
        {
            DataTable dtBranches = General.ACMEERP_MULTI_DATABASES;
            if (dtBranches != null && dtBranches.Rows.Count > 0)
            {
                dgvBranches.DataSource = dtBranches;
                dgvBranches.Refresh();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgvBranches.Rows.Count > 0)
            {
                dialogResult = System.Windows.Forms.DialogResult.OK;
                General.ACMEERP_SELECTED_DATABASES = dgvBranches.DataSource as DataTable;
                this.Close();
            }
            else
            {
                MessageBox.Show("Database is not selected", "Acme.erp", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
    }
}
