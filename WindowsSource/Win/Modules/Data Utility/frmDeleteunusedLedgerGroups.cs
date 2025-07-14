using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model.UIModel;
using System.Collections;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmDeleteunusedLedgerGroups : frmFinanceBaseAdd
    {

        #region Variables
        GridCheckMarksSelection gcCheckBoxSelection;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmDeleteunusedLedgerGroups()
        {
            InitializeComponent();
        }
        #endregion

        #region Events


        private void frmDeleteunusedLedgerGroups_Load(object sender, EventArgs e)
        {
            LoadunusedGroups();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDeleteGroups.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDeleteGroups, colLedgergroup);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList arr = gcCheckBoxSelection.SelectedRowViews();

                if (arr.Count > 0)
                {
                    //DialogResult dr = XtraMessageBox.Show("Selected Groups will be deleted Permanently, Do you really want to Proceed.?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    DialogResult dr = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DELETE_UNUSED_GROUPS_CONFIRMATION), "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        using (LedgerSystem ledsystem = new LedgerSystem())
                        {

                            string TyepId = string.Empty;
                            gvDeleteGroups.UpdateCurrentRow();
                            if (arr != null)
                            {
                                foreach (DataRowView drRow in arr)
                                {
                                    TyepId += drRow["GROUP_ID"].ToString() + ",";
                                }
                                TyepId = TyepId.TrimEnd(',');

                                resultArgs = ledsystem.DeleteAllUnusedLedgersGroups(TyepId);

                                if (resultArgs.Success)
                                {
                                    //this.ShowMessageBox("Ledger Groups deleted Successfully.");
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.LEDGER_GROUPS_DELETE_SUCCESS));
                                    this.Close();
                                }
                            }
                        }
                    }
                }
                else
                {
                    //this.ShowMessageBox("No Groups are selected,try selecting Ledgers.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.NO_LEDGER_GROUPS_SELECT));
                }
            }

            catch (Exception ef)
            {
                this.ShowMessageBoxError(ef.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDeleteGroups_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvDeleteGroups.RowCount.ToString();
        }

        private void frmDeleteunusedLedgerGroups_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        #endregion

        #region Methods
        public void LoadunusedGroups()
        {
            using (LedgerSystem ledsystem = new LedgerSystem())
            {
                resultArgs = ledsystem.FetchAllUnusedLedgersGroups();
                if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcDeleteGroups.DataSource = resultArgs.DataSource.Table;
                    gcCheckBoxSelection = new GridCheckMarksSelection(gvDeleteGroups);
                }
                else
                {
                    gcDeleteGroups.DataSource = null;
                }
            }

        }
        #endregion
    }
}