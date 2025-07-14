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

namespace ACPP.Modules.Transaction
{
    public partial class frmBRS : frmFinanceBaseAdd
    {
        #region Variables
        private const string SELECT_COL = "SELECT";
        private const int GRID_MATIRILIZED_ON_COL = 2;

        public bool IsDateLoaded = false;
        #endregion

        #region Properties
        ResultArgs resultArgs = null;
        private DataTable bRSDetails = null;
        private DataTable BRSDetails
        {
            get
            {
                return bRSDetails;
            }
            set
            {
                bRSDetails = value;
            }
        }
        #endregion

        #region Constructor
        public frmBRS()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        private void frmBRS_Load(object sender, EventArgs e)
        {
            deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);

            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            LoadProject();
            //ucBankReconciliationToolBar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            //ucBankReconciliationToolBar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            //ucBankReconciliationToolBar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyUserRights();
            }
            else
            {
                ucBankReconciliationToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
                ucBankReconciliationToolBar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always; //For Fitler                
            }

            //Enable Filter properties ---------------------------------------------------------
            //gvBRS.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            //gvBRS.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.VisualAndText;
            //gvBRS.FilterEditorCreated += new DevExpress.XtraGrid.Views.Base.FilterControlEventHandler(gvBRS_FilterEditorCreated);
            //----------------------------------------------------------------------------------
            chkSelectAll.Visible = false;
        }



        //private void FilterControl_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Tab)
        //        e = new KeyEventArgs(Keys.Right | Keys.Left);
        //    base.OnKeyDown(e);
        //}

        //private void FilterControl_PopupMenuShowing(object sender, DevExpress.XtraEditors.Filtering.PopupMenuShowingEventArgs e)
        //{
        //    //Hide NOT, OR operator
        //    if (e.MenuType == FilterControlMenuType.Group)
        //    {
        //        for (int i = e.Menu.Items.Count - 1; i >= 0; i--)
        //        {
        //            if (e.Menu.Items[i].Caption == Localizer.Active.GetLocalizedString(StringId.FilterGroupNotAnd) ||
        //                e.Menu.Items[i].Caption == Localizer.Active.GetLocalizedString(StringId.FilterGroupNotOr))
        //            {
        //                e.Menu.Items.RemoveAt(i);
        //            }
        //        }


        //    }
        //}

        private void ApplyUserRights()
        {
            if (CommonMethod.ApplyUserRights((int)Reconciliation.PrintBankReconciliation) != 0)
            {
                ucBankReconciliationToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (CommonMethod.ApplyUserRights((int)Reconciliation.BankUnCleared) == 0)
            {
                chkUnCleared.Enabled = chkUnCleared.Checked = false;
            }
            if (CommonMethod.ApplyUserRights((int)Reconciliation.BankUnReconcilied) == 0)
            {
                chkUnReconciled.Enabled = chkUnReconciled.Checked = false;
            }
            if (CommonMethod.ApplyUserRights((int)Reconciliation.BankCleared) == 0)
            {
                chkCleared.Enabled = chkCleared.Checked = false;
            }
            if (CommonMethod.ApplyUserRights((int)Reconciliation.BankReconciled) == 0)
            {
                chkReconciled.Enabled = chkReconciled.Checked = false;
            }
        }

        private void grdlProjectName_EditValueChanged(object sender, EventArgs e)
        {
            FetchBankAccounts();
            FetchBRSDetails();
        }

        private void gvBRS_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (view.GetRowCellDisplayText(e.RowHandle, view.Columns["STATUS"]) == BankReconciliation.Cleared.ToString())
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                else if (view.GetRowCellDisplayText(e.RowHandle, view.Columns["STATUS"]) == BankReconciliation.Uncleared.ToString())
                {
                    e.Appearance.BackColor = Color.Wheat;
                }
                else if (view.GetRowCellDisplayText(e.RowHandle, view.Columns["STATUS"]) == BankReconciliation.Realized.ToString())
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                if (view.GetRowCellDisplayText(e.RowHandle, view.Columns["STATUS"]) == BankReconciliation.Unrealized.ToString())
                {
                    e.Appearance.BackColor = Color.Wheat;
                }

                if (this.AppSetting.IS_SDB_INM)
                {
                    if (!(view.GetRowCellDisplayText(e.RowHandle, view.Columns["CLIENT_CODE"]) == string.Empty))
                    {
                        // e.Appearance.BackColor = Color.Red;
                    }
                }
            }
        }

        private void rchkFlag_Click(object sender, EventArgs e)
        {
            string ThridPartyCode = gvBRS.GetFocusedRowCellValue(gccolThirdPartyCode) != null ? gvBRS.GetFocusedRowCellValue(gccolThirdPartyCode).ToString() : string.Empty;
            string ThridPartyMode = gvBRS.GetFocusedRowCellValue(colClientMode) != null ? gvBRS.GetFocusedRowCellValue(colClientMode).ToString() : string.Empty;

            DateTime voucherValidateDate = UtilityMember.DateSet.ToDate(gvBRS.GetFocusedRowCellValue(gvColDate).ToString(),false);
            Int32 projectId = glkpProject.EditValue != null ? this.AppSetting.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            bool islockeddate = this.IsVoucherLockedForDate(projectId, voucherValidateDate, true);
            if (!islockeddate)
            {
                if (this.AppSetting.IS_SDB_INM)
                {
                    //if (!string.IsNullOrEmpty(ThridPartyCode))
                    // if (ThridPartyMode == "Online")
                    // {
                    //     this.ShowMessageBox("This Voucher is posted by Third Party application, can not be deleted or modified");
                    //  }
                    // else
                    //{
                    if (gvBRS.GetFocusedRowCellValue(gvColStatus) != null)
                    {
                        DataTable dtBRSSort = gcBRS.DataSource as DataTable;
                        CheckEdit chkStatus = (CheckEdit)sender;
                        string status = gvBRS.GetFocusedRowCellValue(gvColStatus).ToString();
                        string voucherDate = gvBRS.GetFocusedRowCellValue(gvColDate).ToString();
                        if (chkStatus.Checked && status == BankReconciliation.Cleared.ToString())
                        {
                            gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Uncleared.ToString());
                            gvBRS.SetFocusedRowCellValue(gvColReconOn, null);
                        }
                        else if (chkStatus.Checked && status == BankReconciliation.Realized.ToString())
                        {
                            gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Unrealized.ToString());
                            gvBRS.SetFocusedRowCellValue(gvColReconOn, null);
                        }
                        else if (!chkStatus.Checked && status == BankReconciliation.Uncleared.ToString())
                        {
                            gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Cleared.ToString());
                            AddStausColumn(dtBRSSort);
                            gvBRS.SetFocusedRowCellValue(gvColReconOn, voucherDate);
                            gvBRS.FocusedColumn = gvColReconOn;
                            gvBRS.ShowEditor();
                        }
                        else if (!chkStatus.Checked && status == BankReconciliation.Unrealized.ToString())
                        {
                            gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Realized.ToString());
                            AddStausColumn(dtBRSSort);
                            gvBRS.SetFocusedRowCellValue(gvColReconOn, voucherDate);
                            gvBRS.FocusedColumn = gvColReconOn;
                            gvBRS.ShowEditor();
                        }
                        AddStausColumn(dtBRSSort);
                    }
                    // }
                } // hence you stop it
                else
                {
                    if (gvBRS.GetFocusedRowCellValue(gvColStatus) != null)
                    {
                        DataTable dtBRSSort = gcBRS.DataSource as DataTable;
                        CheckEdit chkStatus = (CheckEdit)sender;
                        string status = gvBRS.GetFocusedRowCellValue(gvColStatus).ToString();
                        string voucherDate = gvBRS.GetFocusedRowCellValue(gvColDate).ToString();
                        if (chkStatus.Checked && status == BankReconciliation.Cleared.ToString())
                        {
                            gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Uncleared.ToString());
                            gvBRS.SetFocusedRowCellValue(gvColReconOn, null);
                        }
                        else if (chkStatus.Checked && status == BankReconciliation.Realized.ToString())
                        {
                            gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Unrealized.ToString());
                            gvBRS.SetFocusedRowCellValue(gvColReconOn, null);
                        }
                        else if (!chkStatus.Checked && status == BankReconciliation.Uncleared.ToString())
                        {
                            gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Cleared.ToString());
                            AddStausColumn(dtBRSSort);
                            gvBRS.SetFocusedRowCellValue(gvColReconOn, voucherDate);
                            gvBRS.FocusedColumn = gvColReconOn;
                            gvBRS.ShowEditor();
                        }
                        else if (!chkStatus.Checked && status == BankReconciliation.Unrealized.ToString())
                        {
                            gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Realized.ToString());
                            AddStausColumn(dtBRSSort);
                            gvBRS.SetFocusedRowCellValue(gvColReconOn, voucherDate);
                            gvBRS.FocusedColumn = gvColReconOn;
                            gvBRS.ShowEditor();
                        }
                        AddStausColumn(dtBRSSort);
                    }
                }
            }
            chkSelectAll.Checked = false;
        }

        private void chkUnReconciled_CheckedChanged(object sender, EventArgs e)
        {
            SortStatus();
        }

        private void chkReconciled_CheckedChanged(object sender, EventArgs e)
        {
            SortStatus();
        }

        private void chkUnCleared_CheckedChanged(object sender, EventArgs e)
        {
            SortStatus();
        }

        private void chkCleared_CheckedChanged(object sender, EventArgs e)
        {
            SortStatus();
        }

        private void btnCloseBRS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            FetchBankAccounts();
            FetchBRSDetails();
            chkSelectAll.Checked = false;
        }

        private void btnSaveBRS_Click(object sender, EventArgs e)
        {
            DataTable dtBRSSave = gcBRS.DataSource as DataTable;

            if (dtBRSSave != null)
            {
                using (VoucherTransactionSystem voucherBRS = new VoucherTransactionSystem())
                {
                    this.ShowWaitDialog("Updating BRS");
                    resultArgs = voucherBRS.UpdateBRSDetails(dtBRSSave);
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        FetchBRSDetails();
                        this.CloseWaitDialog();
                    }
                    else
                    {
                        this.CloseWaitDialog();
                        MessageRender.ShowMessage(resultArgs.Message);
                    }
                }
            }
        }

        private void gvBRS_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int brsStatus = this.UtilityMember.NumberSet.ToInteger(gvBRS.GetFocusedRowCellValue(colFlag).ToString());
            string materializedOn = gvBRS.GetFocusedRowCellValue(gvColReconOn).ToString();
            DateTime voucherDate = this.UtilityMember.DateSet.ToDate(gvBRS.GetFocusedRowCellValue(gvColDate).ToString(), false);
            if (!IsValidBRSRow())
            {
                e.Valid = false;

                if (brsStatus == 1 && string.IsNullOrEmpty(materializedOn))
                {
                    gvBRS.FocusedColumn = gvColReconOn;
                    gvBRS.ShowEditor();
                }
                else if (brsStatus == 1 && !this.UtilityMember.DateSet.ValidateDate(voucherDate, this.UtilityMember.DateSet.ToDate(materializedOn, false)))
                {
                    // this.ShowMessageBox("Materialized on should be greater than Voucher Date");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.BRS_MATERIALIZED_DATE));
                    gvBRS.FocusedColumn = gvColReconOn;
                    gvBRS.ShowEditor();
                }
            }
            else if (!string.IsNullOrEmpty(materializedOn))
            {
                gvBRS.SetFocusedRowCellValue(colFlag, 1);
            }
        }

        #endregion

        #region Methods

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = deDateFrom.Text;
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;

                    Int32 projectId = (glkpProject.EditValue != null && !string.IsNullOrEmpty(glkpProject.EditValue.ToString())) ?
                         this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        // grdlProjectName.EditValue = grdlProjectName.Properties.GetKeyValue(0);
                        glkpProject.EditValue = (glkpProject.Properties.GetDisplayValueByKeyValue(projectId) != null ? projectId : glkpProject.Properties.GetKeyValue(0));
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void FetchBankAccounts()
        {
            try
            {
                this.ShowWaitDialog("Loading BRS");
                glkpBankLedger.Properties.DataSource = null;
                Int32 projectId = glkpProject.EditValue != null ? this.AppSetting.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                using (BankSystem banksystem = new BankSystem())
                {
                    banksystem.BankClosedDate = this.UtilityMember.DateSet.ToDate(deDateFrom.Text);
                    DataTable dtBankAccount = banksystem.FetchBankByProjectId(projectId.ToString());
                    if (dtBankAccount != null && dtBankAccount.Rows.Count > 0)
                    {
                        Int32 CashBankLedgerId = glkpBankLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBankLedger.EditValue.ToString()) : 0;

                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpBankLedger, dtBankAccount, "BANK", "LEDGER_ID", true, "- For all Bank Accounts -");
                        //glkpBankLedger.EditValue = glkpBankLedger.Properties.GetKeyValue(0);
                        glkpBankLedger.EditValue = glkpBankLedger.Properties.GetDisplayValueByKeyValue(CashBankLedgerId) != null ? CashBankLedgerId : glkpBankLedger.Properties.GetKeyValue(0);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally
            {
                this.CloseWaitDialog();
            }
        }

        private void FetchBRSDetails()
        {
            try
            {
                this.ShowWaitDialog("Loading BRS Balance");
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    Int32 BankLedgerId = glkpBankLedger.EditValue != null ? this.AppSetting.NumberSet.ToInteger(glkpBankLedger.EditValue.ToString()) : 0;
                    Int32 ProjectId = glkpProject.EditValue != null ? this.AppSetting.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;

                    if (BankLedgerId > 0)
                    {
                        resultArgs = voucherSystem.FetchBRSDetails(ProjectId, BankLedgerId, deDateFrom.DateTime, deDateTo.DateTime);
                        gvColBankAccount.Visible = false;
                    }
                    else
                    {
                        resultArgs = voucherSystem.FetchBRSDetails(ProjectId, deDateFrom.DateTime, deDateTo.DateTime);
                        gvColBankAccount.Visible = true;
                    }
                    //if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    if (resultArgs.Success)
                    {
                        DataTable dtBRS = AddStausColumn(resultArgs.DataSource.Table);
                        gcBRS.DataSource = BRSDetails = dtBRS;
                        GetBRSBankBalance();
                    }
                }
                SortStatus();
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally
            {
                this.CloseWaitDialog();
            }
        }

        private void SortStatus()
        {
            DataTable dtBRSSort = new DataTable();
            string SortColumn = string.Empty;
            if (BRSDetails != null && BRSDetails.Rows.Count > 0)
            {
                if (!chkUnReconciled.Checked && !chkReconciled.Checked && !chkUnCleared.Checked && !chkCleared.Checked)
                {
                    //chkUnReconciled.Checked = chkReconciled.Checked = chkUnCleared.Checked = chkCleared.Checked = true;
                    chkUnReconciled.Checked = chkUnCleared.Checked = true;
                }

                if (chkUnReconciled.Checked)
                    SortColumn += " STATUS='Unrealized'";
                if (chkReconciled.Checked)
                    SortColumn += " OR " + " STATUS='Realized'";
                if (chkUnCleared.Checked)
                    SortColumn += " OR " + " STATUS='Uncleared'";
                if (chkCleared.Checked)
                    SortColumn += " OR " + "STATUS='Cleared'";

                if (SortColumn.StartsWith(" OR"))
                    SortColumn = SortColumn.Substring(3, SortColumn.Length - 3);
                DataView dv = new DataView(BRSDetails);
                dv.RowFilter = SortColumn;
                dtBRSSort = dv.ToTable();
                gcBRS.DataSource = dtBRSSort;
                dv.RowFilter = "";
            }
        }

        private DataTable AddStausColumn(DataTable dtBRSDeatails)
        {
            if (!dtBRSDeatails.Columns.Contains(SELECT_COL))
                dtBRSDeatails.Columns.Add(SELECT_COL, typeof(Int32));
            foreach (DataRow drBRS in dtBRSDeatails.Rows)
            {
                if (drBRS["STATUS"].ToString() == "Realized" || drBRS["STATUS"].ToString() == "Cleared")
                {
                    drBRS[SELECT_COL] = (int)YesNo.Yes;
                }
                else
                {
                    drBRS[SELECT_COL] = (int)YesNo.No;
                }
            }
            return dtBRSDeatails;
        }

        private bool IsValidBRSRow()
        {
            bool IsBRSValid = true;
            try
            {
                int brsStatus = this.UtilityMember.NumberSet.ToInteger(gvBRS.GetFocusedRowCellValue(colFlag).ToString());
                string materializedOn = gvBRS.GetFocusedRowCellValue(gvColReconOn).ToString();
                DateTime voucherDate = this.UtilityMember.DateSet.ToDate(gvBRS.GetFocusedRowCellValue(gvColDate).ToString(), false);
                if (brsStatus > 0 && string.IsNullOrEmpty(materializedOn))
                    IsBRSValid = false;
                else if (brsStatus == 1 && !this.UtilityMember.DateSet.ValidateDate(voucherDate, this.UtilityMember.DateSet.ToDate(materializedOn, false)))
                {
                    IsBRSValid = false;
                }

                //else if (brsStatus == 0)
                //{
                //    IsBRSValid = true;
                //    gvBRS.SetFocusedRowCellValue(gvColReconOn, null);
                //}

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return IsBRSValid;
        }

        #endregion

        private void ucBankReconciliationToolBar_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcBRS, this.GetMessage(MessageCatalog.Transaction.VocherTransaction.BANK_RECONCILIATION), PrintType.DT, gvBRS);
        }

        private void ucBankReconciliationToolBar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucBankReconciliationToolBar_RefreshClicked(object sender, EventArgs e)
        {
            gvBRS.ActiveFilter.Clear();
            FetchBRSDetails();
        }

        private void gcBRS_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Enter || e.KeyData == System.Windows.Forms.Keys.Tab)
            {
                if (gvBRS.IsLastRow)
                {
                    btnSaveBRS.Select();
                    btnSaveBRS.Focus();
                }
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                //   deBRSDateFrom.Focus();

                frmDatePicker datePicker = new frmDatePicker(deDateTo.DateTime, DatePickerType.ChangePeriod);
                datePicker.ShowDialog();
                deDateTo.DateTime = AppSetting.VoucherDateTo;
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void rdetMaterializedOn_Leave(object sender, EventArgs e)
        {
            string materializedOn = gvBRS.GetFocusedRowCellValue(gvColReconOn).ToString();
            int brsStatus = this.UtilityMember.NumberSet.ToInteger(gvBRS.GetFocusedRowCellValue(colFlag).ToString());
            if (!string.IsNullOrEmpty(materializedOn) && brsStatus == 0)
            {
                gvBRS.SetFocusedRowCellValue(colFlag, 1);
                DataTable dtBRSSort = gcBRS.DataSource as DataTable;
                string status = gvBRS.GetFocusedRowCellValue(gvColStatus).ToString();
                if (status == BankReconciliation.Cleared.ToString())
                {
                    gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Uncleared.ToString());
                }
                else if (status == BankReconciliation.Realized.ToString())
                {
                    gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Unrealized.ToString());
                }
                else if (status == BankReconciliation.Uncleared.ToString())
                {
                    gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Cleared.ToString());
                    AddStausColumn(dtBRSSort);
                }
                else if (status == BankReconciliation.Unrealized.ToString())
                {
                    gvBRS.SetFocusedRowCellValue(gvColStatus, BankReconciliation.Realized.ToString());
                    AddStausColumn(dtBRSSort);
                }
                AddStausColumn(dtBRSSort);
                gvBRS.FocusedColumn = gvColReconOn;
                gvBRS.ShowEditor();
            }

        }


        /// <summary>
        /// This method is used to get BRS Balnace (Bank Balance as per cash book and Bank Balance as per Bank statement)
        /// </summary>
        private void GetBRSBankBalance()
        {
            try
            {
                ResultArgs resultarg = new ResultArgs();
                string transmode = string.Empty;
                int projectid = (glkpProject.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()));
                int bankAccountLedgerId = (glkpBankLedger.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpBankLedger.EditValue.ToString()));
                double Statementbankbalance = 0;
                double Cashbookbankbalance = 0;
                double NotMatrilzed = 0;
                double UnRealizedAmt = 0;
                double UnClearedAmt = 0;


                lblStatementBankBalanceValue.Text = "0.0";
                lblCashbookBankBalanceValue.Text = "0.0";
                lblNotMatrilizedValue.Text = "0.0";
                lclblBRSBalanceTitle.Text = "BRS Bank balance as on " + deDateTo.DateTime.ToShortDateString();

                //Get BRS for given DateTo
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    if (bankAccountLedgerId > 0)
                        resultarg = voucherSystem.FetchBRSDetailsByMaterialized(projectid, bankAccountLedgerId, deDateTo.DateTime);
                    else
                        resultarg = voucherSystem.FetchBRSDetailsByMaterialized(projectid, deDateTo.DateTime);
                }


                if (resultarg.Success && resultarg.DataSource.Table != null)
                {
                    DataTable dtBRSList = new DataTable();
                    dtBRSList = resultarg.DataSource.Table;

                    //GEt bank and fd closing balance and make as over all bank closing
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        BalanceProperty bankBalanceProperty;
                        if (bankAccountLedgerId == 0)
                        {
                            bankBalanceProperty = balanceSystem.GetBankBalance(projectid, deDateTo.DateTime.ToShortDateString(), BalanceSystem.BalanceType.ClosingBalance);
                        }
                        else
                        {
                            bankBalanceProperty = balanceSystem.GetBankBalance(0, projectid, bankAccountLedgerId, deDateTo.DateTime.ToShortDateString(), BalanceSystem.BalanceType.ClosingBalance);
                        }
                        //BalanceProperty fdBalanceProperty = balanceSystem.GetFDBalance(projectid, deBRSDateTo.DateTime.ToShortDateString(), BalanceSystem.BalanceType.ClosingBalance);
                        if (bankAccountLedgerId > 0 && AppSetting.AllowMultiCurrency ==1)
                        {
                            Cashbookbankbalance = (bankBalanceProperty.TransFCMode == TransactionMode.CR.ToString() ? -bankBalanceProperty.AmountFC : bankBalanceProperty.AmountFC);
                        }
                        else
                        {
                            Cashbookbankbalance = (bankBalanceProperty.TransMode == TransactionMode.CR.ToString() ? -bankBalanceProperty.Amount : bankBalanceProperty.Amount);
                        }
                        //Cashbookbankbalance += (fdBalanceProperty.TransMode == TransactionMode.CR.ToString() ? -fdBalanceProperty.Amount : fdBalanceProperty.Amount); ;
                    }

                    //Get Unrealized and UnCleared Amount
                    //UnRealizedAmt = this.UtilityMember.NumberSet.ToDouble(dtBRSList.Compute("SUM(RECEIPT)", "STATUS='Unrealized'").ToString());
                    //UnClearedAmt = this.UtilityMember.NumberSet.ToDouble(dtBRSList.Compute("SUM(PAYMENT)", "STATUS='Uncleared'").ToString());
                    UnRealizedAmt = this.UtilityMember.NumberSet.ToDouble(dtBRSList.Compute("SUM(UnRealised)", "").ToString());
                    UnClearedAmt = this.UtilityMember.NumberSet.ToDouble(dtBRSList.Compute("SUM(UnCleared)", "").ToString());

                    Statementbankbalance = Cashbookbankbalance - UnRealizedAmt;
                    Statementbankbalance += UnClearedAmt;
                    NotMatrilzed = UnRealizedAmt + UnClearedAmt;

                    lblStatementBankBalanceValue.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(Statementbankbalance)) + " " + (Statementbankbalance >= 0 ? TransactionMode.DR.ToString() : TransactionMode.CR.ToString()).ToString();
                    lblCashbookBankBalanceValue.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(Cashbookbankbalance)) + " " + (Cashbookbankbalance >= 0 ? TransactionMode.DR.ToString() : TransactionMode.CR.ToString()).ToString();
                    lblNotMatrilizedValue.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(NotMatrilzed)) + " " + (NotMatrilzed >= 0 ? TransactionMode.DR.ToString() : TransactionMode.CR.ToString()).ToString();
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Could not generate BRS Bank Balance " + err.Message);
            }
        }

        private void deBRSDateTo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void glkpBankLedger_EditValueChanged(object sender, EventArgs e)
        {
            FetchBRSDetails();
        }

        private void ucBankReconciliationToolBar_EditClicked(object sender, EventArgs e)
        {
            this.ShowCustomFilter(gcBRS);
        }

        private void deDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects----
            LoadProject();

            FetchBankAccounts();
            //--------------------------------------
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvBRS.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvBRS, gvcolChequeNo);
            }
        }

        private void glkpProject_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                SelectAllUnMaterializedVouchers();
            }
        }

        private void SelectAllUnMaterializedVouchers()
        {
            bool isValid = false;
            try
            {
                if (gcBRS.DataSource != null)
                {
                   
                    DataTable dtBRS = (gcBRS.DataSource as DataTable).DefaultView.ToTable();
                    Int32 projectId = glkpProject.EditValue != null ? this.AppSetting.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                    if (dtBRS.Rows.Count > 0)
                    {
                        dtBRS.DefaultView.RowFilter = "STATUS IN ('" + BankReconciliation.Uncleared.ToString() + "', '" + BankReconciliation.Unrealized.ToString() + "')";
                        isValid = (dtBRS.DefaultView.Count > 0);
                        if (!isValid)
                        {
                            this.ShowMessageBox("Uncleared/Unrealized Vouchers are not available");
                        }
                        dtBRS.DefaultView.RowFilter = string.Empty;

                        if (isValid && this.ShowConfirmationMessage("Are you sure to set Voucher Date as Materialized Date for all the Uncleared/Unrealized Vouchers ? ",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.ShowWaitDialog("Processing");
                            //CheckEdit chkStatus = (CheckEdit)sender;
                            int Row = 0;
                            foreach (DataRow drBRS in dtBRS.Rows)
                            {
                                string ThridPartyCode = gvBRS.GetRowCellValue(Row, gccolThirdPartyCode) != null ? gvBRS.GetRowCellValue(Row, gccolThirdPartyCode).ToString() : string.Empty;
                                string ThridPartyMode = gvBRS.GetRowCellValue(Row, colClientMode) != null ? gvBRS.GetRowCellValue(Row, colClientMode).ToString() : string.Empty;
                                string status = gvBRS.GetRowCellValue(Row, gvColStatus).ToString();
                                string voucherDate = gvBRS.GetRowCellValue(Row, gvColDate).ToString();
                                bool islockeddate = this.IsVoucherLockedForDate(projectId, UtilityMember.DateSet.ToDate(voucherDate, false), false, glkpProject.SelectedText);
                                if (!islockeddate)
                                {
                                    //  if (ThridPartyMode != "Online")
                                    // {
                                    if (status == BankReconciliation.Uncleared.ToString())
                                    {
                                        gvBRS.SetRowCellValue(Row, gvColStatus, BankReconciliation.Cleared.ToString());
                                        gvBRS.SetRowCellValue(Row, colFlag, (int)YesNo.Yes);
                                        gvBRS.SetRowCellValue(Row, gvColReconOn, voucherDate);
                                        gvBRS.FocusedColumn = gvColReconOn;
                                        gvBRS.ShowEditor();
                                    }
                                    else if (status == BankReconciliation.Unrealized.ToString())
                                    {
                                        gvBRS.SetRowCellValue(Row, gvColStatus, BankReconciliation.Realized.ToString());
                                        gvBRS.SetRowCellValue(Row, colFlag, (int)YesNo.Yes);
                                        gvBRS.SetRowCellValue(Row, gvColReconOn, voucherDate);
                                        gvBRS.FocusedColumn = gvColReconOn;
                                        gvBRS.ShowEditor();
                                    }
                                }

                                // }
                                Row++;
                            }
                        }
                        chkSelectAll.Checked = false;
                    }
                }
                this.CloseWaitDialog();
            }
            catch (Exception err)
            {
                this.CloseWaitDialog();
                this.ShowMessageBox(err.Message);
            }
            finally
            {
                this.CloseWaitDialog();
            }
        }

        private void btnSelectAllUnMaterializedVouchers_Click(object sender, EventArgs e)
        {
            SelectAllUnMaterializedVouchers();
        }

        private void gvBRS_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvBRS.RowCount.ToString();
        }

        //private void rdetMaterializedOn_EditValueChanged(object sender, EventArgs e)
        //{
        //    //if (this.AppSetting.IS_SDB_INM)
        //    //{
        //    //    string materializedOn = gvBRS.GetFocusedRowCellValue(gvColReconOn).ToString();
        //    //    string ThridPartyCode = gvBRS.GetFocusedRowCellValue(gccolThirdPartyCode) != null ? gvBRS.GetFocusedRowCellValue(gccolThirdPartyCode).ToString() : string.Empty;
        //    //    if (string.IsNullOrEmpty(ThridPartyCode))
        //    //    {
        //    //        MessageRender.ShowMessage("This Voucher is posted by Third Party application, can not be deleted or modified");
        //    //        gvBRS.SetFocusedRowCellValue(gvColReconOn, materializedOn);
        //    //        gvBRS.FocusedColumn = gvColReconOn;
        //    //        gvBRS.ShowEditor();
        //    //    }

        //    //}
        //}
    }
}

///// <summary>
//      /// Attach Filter Control properties
//      /// </summary>
//      /// <param name="sender"></param>
//      /// <param name="e"></param>
//      private void gvBRS_FilterEditorCreated(object sender, DevExpress.XtraGrid.Views.Base.FilterControlEventArgs e)
//      {
//          e.FilterControl.TabStop = true;
//          e.FilterControl.PopupMenuShowing += new DevExpress.XtraEditors.Filtering.PopupMenuShowingEventHandler(FilterControl_PopupMenuShowing);
//          e.FilterControl.ShowGroupCommandsIcon = true;
//          e.FilterControl.ShowOperandTypeIcon = true;
//          e.FilterControl.ShowToolTips = true;

//          //Load all visible columns into Fitler control, based on its properties
//          FilterColumnCollection filterColumns = new FilterColumnCollection();
//          foreach (GridColumn dc in gvBRS.Columns)
//          {
//              if (dc.Visible && dc.FieldName.ToUpper() != "SELECT")
//              {
//                  Type columndatatype = dc.ColumnType;
//                  RepositoryItem repitem = new RepositoryItemTextEdit();
//                  if (columndatatype == typeof(DateTime))
//                  {
//                      repitem = new RepositoryItemDateEdit();
//                  }

//                  CustomUnboundFilterColumn column = new CustomUnboundFilterColumn(dc.Caption, dc.FieldName, columndatatype, repitem, FilterColumnClauseClass.String);
//                  filterColumns.Add(column);
//              }
//          }

//          //e.FilterControl.KeyDown += new KeyEventHandler(FilterControl_KeyDown); 
//          //e.FilterControl

//          e.FilterControl.SetFilterColumnsCollection(filterColumns);                        
//      }
