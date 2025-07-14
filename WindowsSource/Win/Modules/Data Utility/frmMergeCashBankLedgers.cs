using System;
using System.Drawing;

using Bosco.Model.UIModel;
using Bosco.Utility;
using System.Linq;
using DevExpress.XtraEditors;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.Windows.Forms;
using AcMEDSync.Model;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraGrid.Columns;
using System.Collections.Generic;
using Bosco.Utility.Base;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraEditors.Repository;
using DevExpress.Internal.DXWindow;
using DevExpress.ExpressApp;
using System.Reflection;
using ACPP.Modules.Transaction;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmMergeCashBankLedgers : frmFinanceBaseAdd
    {
        #region Variables
      
        #endregion

        #region Properties

        private Int32 FromProjectId
        {
            get
            {
                Int32 id = (glkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                return id;
            }

        }

        private Int32 MergeProjectId
        {
            get
            {
                Int32 id = (glkpMergeProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpMergeProject.EditValue.ToString()) : 0;
                return id;
            }
        }

        private Int32 FromProjectCashBankLedgerId
        {
            get
            {
                Int32 id = (glkpCashBankLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpCashBankLedger.EditValue.ToString()) : 0;
                return id;
            }

        }

        private Int32 MergeProjectCashBankLedgerId
        {
            get
            {
                Int32 id = (glkpMergeCashBankLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpMergeCashBankLedger.EditValue.ToString()) : 0;
                return id;
            }
        }
        #endregion

        #region Constructor
        public frmMergeCashBankLedgers()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmMergeCashBankLedgers_Load(object sender, EventArgs e)
        {
            this.Text = "Merge Cash/Bank Ledger";

            deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);

            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            LoadProject(glkpProject);
            LoadProject(glkpMergeProject);

            //For Temp purpose
            //chklcIncludeLedgerOpBalance.Checked = true;
            lcIncludeLedgerOpBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            chklcIncludeLedgerOpBalance.Visible = true;
        }

        private void btnMergeCashBank_Click(object sender, EventArgs e)
        {
            bool updated = false;
            string datefrom = string.Empty;
            string dateto = string.Empty;
            bool bincludeopeningbalance = chklcIncludeLedgerOpBalance.Checked;
            if (chkDuration.Checked)
            {
                datefrom = deDateFrom.DateTime.ToShortDateString();
                dateto = deDateTo.DateTime.ToShortDateString();
                bincludeopeningbalance = false;
            }

            if (ValidateAndConfirm())
            {
                using (MappingSystem mapping = new MappingSystem())
                {
                    this.ShowWaitDialog();
                    ResultArgs resultArg = mapping.MergeCashBankLedgersByProject(FromProjectId, FromProjectCashBankLedgerId, MergeProjectId,
                                                                                 MergeProjectCashBankLedgerId, datefrom, dateto, bincludeopeningbalance);
                    if (!resultArg.Success)
                    {
                        this.ShowMessageBox("Not able to Move/Merge Cash/Bank Ledgers (" +  resultArg.Message + ")");
                    }
                    else
                    {
                        updated = true;
                    }
                }
            }

            //Refresh Balance and other controls if updation is sucessfully.
            if (updated)
            {
                this.ShowWaitDialog();
               

                //If sucessfully refreshd, refresh both Projects
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    bool balancerefreshed = false;
                    //Refresh "From Project "

                    balanceSystem.ProjectId = FromProjectId;
                    //balanceSystem.LedgerId = FromProjectCashBankLedgerId; //18/03/2024, concern from ledger alone
                    balanceSystem.VoucherDate = chkDuration.Checked ? deDateFrom.DateTime.ToShortDateString() : AppSetting.BookBeginFrom;
                    this.ShowWaitDialog("Refreshing Ledger Balances");
                    ResultArgs result = balanceSystem.UpdateBulkTransBalance();
                    if (result.Success)
                    {
                        //Refresh Merged Project
                        balanceSystem.ProjectId = MergeProjectId;
                        //balanceSystem.LedgerId = MergeProjectCashBankLedgerId; //18/03/2024, concern from ledger alone
                        balanceSystem.VoucherDate = chkDuration.Checked ? deDateFrom.DateTime.ToShortDateString() : AppSetting.BookBeginFrom;
                        result = balanceSystem.UpdateBulkTransBalance();

                        if (result.Success)
                        {
                            balancerefreshed = true;
                            this.CloseWaitDialog();
                            ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.BALANCE_UPDATED));
                        }
                    }

                    this.CloseWaitDialog();
                    if (!balancerefreshed)
                    {
                        this.ShowMessageBox("Successfully merged Cash/Bank Ledger but could not Refresh Project balance. Refresh it manually.(" + result.Message + ")");
                    }
                }

                LoadProject(glkpProject);
                LoadProject(glkpMergeProject);

                FetchCashBankLedger(glkpCashBankLedger, glkpProject);
                FetchCashBankLedger(glkpMergeCashBankLedger, glkpMergeProject);

                chkDuration.Checked = false;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkDuration_CheckedChanged(object sender, EventArgs e)
        {
            lcDateFrom.Visibility = chkDuration.Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcDateTo.Visibility = lcDateFrom.Visibility;
            //chkDeleteCashBankLedger.Checked = false;
            //chkDeleteCashBankLedger.Enabled = chkDuration.Checked ? false : true;

            lcIncludeLedgerOpBalance.Visibility = chkDuration.Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            chklcIncludeLedgerOpBalance.Visible = true;
            chklcIncludeLedgerOpBalance.Checked = false;

            lclblInfo.Text = "* Move all Vouchers which includes Fixed Deposit Vouchers.  Balance will be Refreshed for Both Projects.";
            if (chkDuration.Checked)
            {
                lclblInfo.Text = "* As duration is selected, Move general Vouchers alone, It will not include Fixed Deposit Vouchers.";
            }
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            FetchCashBankLedger(glkpCashBankLedger, glkpProject);
        }

        private void glkpMergeProject_EditValueChanged(object sender, EventArgs e)
        {
            FetchCashBankLedger(glkpMergeCashBankLedger, glkpMergeProject);
        }

        private void deDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            //Reload Projects and Cash/Bank Ledgers based on Date From as Closed Date
            LoadProject(glkpProject);
            LoadProject(glkpMergeProject);

            FetchCashBankLedger(glkpCashBankLedger, glkpProject);
            FetchCashBankLedger(glkpMergeCashBankLedger, glkpMergeProject);
        }

        private void glkpCashBankLedger_EditValueChanged(object sender, EventArgs e)
        {
            

        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate Both Projects and its Cash/Bank Ledges and get confirmation message
        /// </summary>
        /// <returns></returns>
        private bool ValidateAndConfirm()
        {
            bool rtn = false;
            if (FromProjectId == 0 || MergeProjectId == 0 || FromProjectCashBankLedgerId == 0)
            {
                if (FromProjectId == 0)
                {
                    this.ShowMessageBox("Select source Project");
                    glkpProject.Select();
                    glkpProject.Focus();
                    rtn = false;
                }
                else if (MergeProjectId == 0)
                {
                    this.ShowMessageBox("Select Merge Project");
                    glkpMergeProject.Select();
                    glkpMergeProject.Focus();
                    rtn = false;
                }
                else if (FromProjectCashBankLedgerId == 0)
                {
                    this.ShowMessageBox("Select Source Cash/Bank Ledger");
                    glkpCashBankLedger.Select();
                    glkpCashBankLedger.Focus();
                    rtn = false;
                }
            }
            else if (FromProjectId == MergeProjectId && (FromProjectCashBankLedgerId == MergeProjectCashBankLedgerId && MergeProjectCashBankLedgerId==0))
            {
                //If Same Project and Same Cash bank Ledger are selected, prompt alert message
                this.ShowMessageBox("Same Project and Cash/Bank Ledger can't be merged");
                glkpProject.Select();
                glkpProject.Focus();
                rtn = false;
            }
            else if (chklcIncludeLedgerOpBalance.Checked)
            {
                if (this.ShowConfirmationMessage("Do you want to merge Ledger '" + glkpCashBankLedger.Text + "' Opening Balance ?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    rtn = true;
                }
            }
            else if (MergeProjectCashBankLedgerId == 0)
            {
                if (this.ShowConfirmationMessage("'" + glkpCashBankLedger.Text + "' Ledger will be mapped for '" + glkpMergeProject.Text + "'",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    rtn = true;
                }
            }
            else
            {
                rtn = true;
            }
            
            //Get Confirmation message, If Duriation is selected, prompat message that FD Vouchers will not be moved
            if (rtn)
            {
                string confirmationmsg = "Are you sure to Move/Merge Vouchers Project from '" + glkpProject.Text + "' to '" + glkpMergeProject.Text + "'" + System.Environment.NewLine + System.Environment.NewLine 
                    + " and Ledger from '" + glkpCashBankLedger.Text + (MergeProjectCashBankLedgerId > 0? "' to '" + glkpMergeCashBankLedger.Text + "'": "") + " ?. ";

                if (chkDuration.Checked)
                {
                    confirmationmsg +=  System.Environment.NewLine + System.Environment.NewLine + "Note : Since duration is selected, Fixed Deposit Vouchers will not be Merged/Moved";
                }

                if (this.ShowConfirmationMessage(confirmationmsg, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    rtn = true;
                }
                else
                { 
                    rtn = false; 
                }
            }

            return rtn; 
        }

        private void LoadProject(GridLookUpEdit lkpProject)
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate =  chkDuration.Checked? deDateFrom.Text : AppSetting.YearFrom;
                    ResultArgs resultArgs = mappingSystem.FetchProjectsLookup();
                    lkpProject.Properties.DataSource = null;

                    Int32 projectId = (lkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(lkpProject.EditValue.ToString()) : 0;
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (lkpProject.Name == glkpMergeProject.Name)
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(lkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                            lkpProject.EditValue = 0;
                        }
                        else
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(lkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                            lkpProject.EditValue = (lkpProject.Properties.GetDisplayValueByKeyValue(projectId) != null ? projectId : lkpProject.Properties.GetKeyValue(0));
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void FetchCashBankLedger(GridLookUpEdit lkpActiveCashBankLedger, GridLookUpEdit lkpActiveProject)
        {
            try
            {
                lkpActiveCashBankLedger.Properties.DataSource = null;
                Int32 projectId = lkpActiveProject.EditValue != null ? this.AppSetting.NumberSet.ToInteger(lkpActiveProject.EditValue.ToString()) : 0;

                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    if (chkDuration.Checked)
                        ledgersystem.LedgerClosedDateForFilter = this.UtilityMember.DateSet.ToDate(deDateFrom.Text);
                    else
                        ledgersystem.LedgerClosedDateForFilter = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom);

                    ledgersystem.ProjectId = projectId;
                    ResultArgs resultarg = ledgersystem.FetchCashBankLedgerByProject(projectId);
                    if (resultarg.Success && resultarg.DataSource.Table != null)
                    {
                        DataTable dtCashBankLedger = resultarg.DataSource.Table;
                        Int32 CashBankLedgerId = lkpActiveCashBankLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(lkpActiveCashBankLedger.EditValue.ToString()) : 0;

                        if (lkpActiveCashBankLedger.Name == glkpMergeCashBankLedger.Name)
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(lkpActiveCashBankLedger, dtCashBankLedger,
                                        ledgersystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, true, "-Select-");

                            //Select If Same bank ledger is available in Merge Project
                            CashBankLedgerId = glkpCashBankLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCashBankLedger.EditValue.ToString()) : 0;
                        }
                        else
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(lkpActiveCashBankLedger, dtCashBankLedger,
                                        ledgersystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, false, string.Empty);
                        }

                        lkpActiveCashBankLedger.EditValue = lkpActiveCashBankLedger.Properties.GetDisplayValueByKeyValue(CashBankLedgerId) != null ? CashBankLedgerId : lkpActiveCashBankLedger.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }
        #endregion

        private void glkpProject_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void glkpMergeProject_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void glkpMergeCashBankLedger_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        

    }
}