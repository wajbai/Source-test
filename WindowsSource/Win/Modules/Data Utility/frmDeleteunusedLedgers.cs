/*  Class Name      : frmDeleterunusedLedgers.cs
 *  Purpose         : Delete the unused Ledgers in the Transaction
 *  Author          : PRAVEEN
 *  Created on      : 18-Jul-2016
 */

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
using System.Text.RegularExpressions;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmDeleteunusedLedgers : frmFinanceBaseAdd
    {
        #region Variables
        GridCheckMarksSelection gcCheckBoxSelection;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmDeleteunusedLedgers()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        private void frmDeleteunusedLedgers_Load(object sender, EventArgs e)
        {
            LoadunusedLedgers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (gcDeleteLedgers.DataSource as DataTable != null)
                {
                    ArrayList arr = gcCheckBoxSelection.SelectedRowViews();

                    if (arr.Count > 0)
                    {
                        //DialogResult dr = XtraMessageBox.Show("Selected Ledgers will be deleted Permanently, Do you really want to Proceed.?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        DialogResult dr = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DELETE_UNUSED_lEDGER_CONFIRMATION), "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            using (LedgerSystem ledsystem = new LedgerSystem())
                            {
                                ShowWaitDialog();
                                string TyepId = string.Empty;
                                gvDeleteLedgers.UpdateCurrentRow();
                                if (arr != null)
                                {
                                    foreach (DataRowView drRow in arr)
                                    {
                                        TyepId += drRow["LEDGER_ID"].ToString() + ",";
                                    }
                                    TyepId = TyepId.TrimEnd(',');

                                    resultArgs = ledsystem.DeleteAllUnusedLedgers(TyepId);

                                    if (resultArgs.Success)
                                    {
                                        CloseWaitDialog();
                                        //this.ShowMessageBox("Ledgers deleted Successfully.");
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.UNUSED_LEDGER_DELETE_SUCCESS));
                                        this.Close();
                                    }
                                    else
                                    {
                                        CloseWaitDialog();
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.CANNOT_DELETE)); //Cannot Delete. This Record has association
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        CloseWaitDialog();
                        //this.ShowMessageBox("No ledgers are selected,try selecting Ledgers.");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.NO_LEDGER_SELECT));
                    }
                }
                else
                {
                    CloseWaitDialog();
                    //this.ShowMessageBox("Ledger details are not available to proceed.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.LEDGER_DETAIL_NOT_AVAILABLE_TO_PROCEED));
                }
            }

            catch (Exception ef)
            {
                CloseWaitDialog();
                this.ShowMessageBoxError(ef.Message);
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDeleteLedgers.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDeleteLedgers, colLedgerName);
            }
        }

        private void frmDeleteunusedLedgers_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvDeleteLedgers_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvDeleteLedgers.RowCount.ToString();
        }

        #endregion

        #region Methods

        public void LoadunusedLedgers()
        {
            using (LedgerSystem ledsystem = new LedgerSystem())
            {
                resultArgs = ledsystem.FetchAllUnusedLedgers();

                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcDeleteLedgers.DataSource = resultArgs.DataSource.Table;
                    gcCheckBoxSelection = new GridCheckMarksSelection(gvDeleteLedgers);
                }
                else
                {
                    gcDeleteLedgers.DataSource = null;
                }
            }

        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}