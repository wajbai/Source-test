using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;

using ACPP.Modules.Master;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Utility.CommonMemberSet;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Setting;
using DevExpress.Utils;


namespace ACPP.Modules.Master
{
    public partial class frmAccountingPeriodView : frmFinanceBase
    {
        #region Variable Decelartion
        ResultArgs resultArgs = null;
        private string colAccYearId = "ACC_YEAR_ID";
        private string Status = "STATUS";
        private string Flag = "FLAG";
        private string Active = "Active";
        private string InActive = "InActive";
        private const string colVoucherDate = "VOUCHER_DATE";
        private int RowIndex = 0;
        private bool IsFirstTrans = true;
        #endregion

        #region Constructors

        public frmAccountingPeriodView()
        {
            InitializeComponent();
        }
        public frmAccountingPeriodView(bool IsTrans)
            : this()
        {
            IsFirstTrans = IsTrans;
        }
        #endregion

        #region Properties
        private int accPeriodId = 0;
        private int AccPeriodId
        {
            get
            {

                RowIndex = gvAccPeriod.FocusedRowHandle;
                accPeriodId = gvAccPeriod.GetFocusedRowCellValue(colaccPeriodId) != null ? this.UtilityMember.NumberSet.ToInteger(gvAccPeriod.GetFocusedRowCellValue(colaccPeriodId).ToString()) : 0;
                return accPeriodId;
            }
            set
            {
                accPeriodId = value;
            }
        }

        private string accPeriodStatus = string.Empty;
        private string AccPeriodStatus
        {
            get
            {

                RowIndex = gvAccPeriod.FocusedRowHandle;
                accPeriodStatus = gvAccPeriod.GetFocusedRowCellValue(colStatus).ToString();
                return accPeriodStatus;
            }
            set
            {
                accPeriodStatus = value;
            }
        }

        private string AccPeriodYearFrom
        {
            get
            {

                RowIndex = gvAccPeriod.FocusedRowHandle;
                return  gvAccPeriod.GetFocusedRowCellValue(colYearFrom).ToString();
            }
        }


        private string AccPeriodYearTo
        {
            get
            {

                RowIndex = gvAccPeriod.FocusedRowHandle;
                return gvAccPeriod.GetFocusedRowCellValue(colYearTo).ToString();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Load the Account Period details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>    

        private void frmAccountingPeriodView_Load(object sender, EventArgs e)
        {
            //SetVisibileShortCuts(false, false);
            //LoadAccountingPeriod();

            // FetchAccountPeriodDetails();

            // ucToolBarCostCentre.DisableDeleteButton = true;
            //ucToolBarCostCentre.DisableEditButton = true;

            ApplyUserRights();


            if (this.isEditable)
            {
                ucToolBarCostCentre.DisableMoveTransaction = true;
                ucToolBarCostCentre.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Always;
                ucToolBarCostCentre.ChangeMoveVoucherCaption = "Move Previous FY";

                SuperToolTip sTooltip1 = new SuperToolTip();
                // Create a tooltip item that represents a header.
                ToolTipTitleItem titleItem1 = new ToolTipTitleItem();
                titleItem1.Text = "Move to Previous Finance Year";
                // Add the tooltip items to the SuperTooltip.
                sTooltip1.Items.Add(titleItem1);

                ucToolBarCostCentre.ChangeMoveVoucherTooltip = sTooltip1;
            }
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(TransactionPeriods.CreateTransaction);
            this.enumUserRights.Add(TransactionPeriods.EditTransaction);
            this.enumUserRights.Add(TransactionPeriods.DeleteTransaction);
            this.enumUserRights.Add(TransactionPeriods.PrintTransaction);
            this.enumUserRights.Add(TransactionPeriods.ViewTransaction);
            
            this.ApplyUserRights(ucToolBarCostCentre, this.enumUserRights, (int)Menus.TransactionPeriod);
        }

        /// <summary>
        /// Fire When the add button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_AddClicked(object sender, EventArgs e)
        {
            ShowAccountingPeriodForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// Edit the Accounting Period form based on the Accounting Period id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_EditClicked(object sender, EventArgs e)
        {
            ShowAccPeriodForm();
        }

        /// <summary>
        /// Fires when grid is double clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gcCostCentre_DoubleClick(object sender, EventArgs e)
        {
            ShowAccPeriodForm();
        }

        /// <summary>
        /// Delete the Account Period based on the Accounting Period id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (AccPeriodId != 0)
                {
                    using (AccouingPeriodSystem accPeriodSystem = new AccouingPeriodSystem())
                    {
                        if (gvAccPeriod.RowCount != 0)
                        {
                            if (AccPeriodStatus != "Active")
                            {
                                if (IsTransactoinExists())
                                {
                                    if (ValidateDeletePeriod())
                                    {
                                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            accPeriodSystem.AccPeriodId = AccPeriodId;
                                            resultArgs = accPeriodSystem.DeleteAccountingSignDetails();
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = accPeriodSystem.DeleteAccountingAuditorNoteSign();
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = accPeriodSystem.DeleteReportFYDevelopmentalBudgetDetails();

                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = accPeriodSystem.DeleteACCountryCurrencyExchangeRate(AccPeriodYearFrom, AccPeriodYearTo);

                                                        if (resultArgs.Success)
                                                        {
                                                            resultArgs = accPeriodSystem.DeleteAccountingPeriodDetials();
                                                            if (resultArgs.Success)
                                                            {
                                                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                                //FetchAccountPeriodDetails();
                                                                LoadAccountingPeriod();
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                                else
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_CANNOT_DELETE));
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_ACTIVE));
                            }
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Print the Accounting Period Print details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcAccPeriod, this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_PRINT_CAPTION), PrintType.DT, gvAccPeriod);
        }

        /// <summary>
        /// Enable or Disable Show Filter 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAccPeriod.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAccPeriod, colYearFrom);
            }
        }

        /// <summary>
        /// Set record counts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gvCostCentre_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvAccPeriod.RowCount.ToString();
        }

        /// <summary>
        /// Refresh the grie after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadAccountingPeriod();
            gvAccPeriod.FocusedRowHandle = RowIndex;
            this.SetTransacationPeriod();
        }

        /// <summary>
        /// To Refresh Period
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarCostCentre_RefreshClicked(object sender, EventArgs e)
        {
            LoadAccountingPeriod();
        }
        /// <summary>
        /// Close the Accounting Period form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBarCostCentre_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAccountingPeriodView_Activated(object sender, EventArgs e)
        {
            SetVisibileShortCuts(false, true);
            LoadAccountingPeriod();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Load the Accounting Period details
        /// </summary>

        private void FetchAccountPeriodDetails()
        {
            try
            {
                using (AccouingPeriodSystem accPeriodSystem = new AccouingPeriodSystem())
                {
                    resultArgs = accPeriodSystem.FetchAccountingPeriodDetails();
                    gcAccPeriod.DataSource = resultArgs.DataSource.Table;
                    gcAccPeriod.RefreshDataSource();

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private bool ValidateDeletePeriod()
        {
            bool isValid = true;
            try
            {
                using (AccouingPeriodSystem accPeriodSystem = new AccouingPeriodSystem(AccPeriodId))
                {
                    if (this.UtilityMember.DateSet.ToDate(accPeriodSystem.YearFrom, false) != DateTime.MinValue)
                    {
                        resultArgs = accPeriodSystem.CheckAccountingPeriod();
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            this.ShowMessageBoxWarning("Cannot delete in between Accounting Period.");
                            isValid = false;
                        }
                    }
                    else
                    {
                        this.ShowMessageBoxError("Accounting Period from is invalid.");
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            return isValid;
        }

        /// <summary>
        /// Check wheater the transaction entered for this transaction period
        /// </summary>
        /// <returns></returns>
        private bool IsTransactoinExists()
        {
            bool isTransactoion = true;
            try
            {
                using (AccouingPeriodSystem accPeriodSystem = new AccouingPeriodSystem())
                {
                    accPeriodSystem.AccPeriodId = AccPeriodId;
                    resultArgs = accPeriodSystem.CheckIsTransaction();
                    if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                    {
                        isTransactoion = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return isTransactoion;
        }

        /// <summary>
        /// Show Accounting Period Form based on the id.
        /// </summary>
        /// <param name="AccPeriodId"></param>

        private void ShowAccountingPeriodForm(int accPeriodId)
        {
            try
            {
                frmAccountingPeriodAdd frmAccPeriod = new frmAccountingPeriodAdd(accPeriodId, gvAccPeriod.RowCount);
                frmAccPeriod.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAccPeriod.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void ShowAccPeriodForm()
        {
            if (this.isEditable)
            {
                if (AccPeriodId != 0)
                {
                    if (gvAccPeriod.RowCount != 0)
                    {
                        ShowAccountingPeriodForm(AccPeriodId);
                    }
                }
                else
                {
                    if (!chkShowFilter.Checked)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
            }
        }
        #endregion

        private void gvAccPeriod_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (view.GetRowCellDisplayText(e.RowHandle, view.Columns[Status]).ToString() == Active)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }
        }

        private DataTable SetAccountPeriodStatus(DataTable dtSource)
        {
            DataTable dtAccSource = dtSource;
            for (int i = 0; i < dtAccSource.Rows.Count; i++)
            {
                if (dtAccSource.Rows[i][Status].ToString() == Active)
                {
                    dtAccSource.Rows[i][Flag] = (int)YesNo.Yes;
                }
                else
                {
                    dtAccSource.Rows[i][Flag] = (int)YesNo.No;
                }
            }
            return dtAccSource;
        }

        private DataTable SetCurrentTransPeriod(string transPeriodId, YesNo status, CheckEdit chkStatus)
        {
            DataTable dtAccSource = (DataTable)gcAccPeriod.DataSource;
            if (chkStatus.CheckState == CheckState.Checked)
            {
                for (int i = 0; i < dtAccSource.Rows.Count; i++)
                {
                    if (dtAccSource.Rows[i][colAccYearId].ToString() == transPeriodId)
                    {
                        if (chkStatus.CheckState == CheckState.Checked)
                        {
                            dtAccSource.Rows[i][Flag] = (int)status;
                            dtAccSource.Rows[i][Status] = Active;
                            UpdateTransacationPeriod();
                            this.ShowCurrentPeriod();

                            //On 03/05/2018, reset report year from and to
                            Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this);
                            report.ResetReportPropertyTransYearChange();
                        }
                    }
                    else
                    {
                        dtAccSource.Rows[i][Flag] = (int)YesNo.No;
                        dtAccSource.Rows[i][Status] = InActive;
                    }
                }
            }
            return dtAccSource;
        }


        private void UpdateTransacationPeriod()
        {
            using (GlobalSetting globalSystem = new GlobalSetting())
            {
                resultArgs = globalSystem.UpdateAccountingPeriod(AccPeriodId.ToString());
                if (resultArgs.Success)
                {
                    using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                    {
                        resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                        if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                        {
                            this.AppSetting.AccPeriodInfo = resultArgs.DataSource.Table.DefaultView;
                            ApplyRecentPrjectDetails();
                            this.SetTransacationPeriod();
                        }
                    }
                }
            }
        }

        private void LoadAccountingPeriod()
        {
            try
            {
                using (AccouingPeriodSystem accPeriodSystem = new AccouingPeriodSystem())
                {
                    resultArgs = accPeriodSystem.FetchAccountingPeriodDetailsForSettings();
                    resultArgs.DataSource.Table.Columns.Add(Flag, typeof(int));
                    gcAccPeriod.DataSource = SetAccountPeriodStatus(resultArgs.DataSource.Table);

                    //#On 30/04/2021, Assign All FYs
                    resultArgs = accPeriodSystem.FetchAccountingPeriodDetails();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        this.AppSetting.AllAccountingPeriods = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void rchkCheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chkEdit = (CheckEdit)sender;
            if (IsAccountingPeriodSelected(chkEdit))
            {
                if (chkEdit.CheckState == CheckState.Checked)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_CHANGE_PERIOD), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gcAccPeriod.DataSource = SetCurrentTransPeriod(AccPeriodId.ToString(), YesNo.Yes, chkEdit);
                    }
                    else
                    {
                        chkEdit.CheckState = CheckState.Unchecked;
                    }
                }
            }
            else
            {
                gcAccPeriod.DataSource = SetCurrentTransPeriod(AccPeriodId.ToString(), YesNo.No, chkEdit);
            }
            this.SetTransacationPeriod();
        }

        private bool IsAccountingPeriodSelected(CheckEdit chkSelect)
        {
            bool isSelected = true;
            DataTable dtAccSource = (DataTable)gcAccPeriod.DataSource;
            foreach (DataRow dr in dtAccSource.Rows)
            {
                if (dr[Status].ToString() == Active)
                {
                    isSelected = (chkSelect.CheckState == CheckState.Checked) ? true : false;
                    if (dr[colAccYearId].ToString() == AccPeriodId.ToString())
                    {
                        if (chkSelect.CheckState == CheckState.Unchecked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_ONE_ACTIVE));
                            chkSelect.CheckState = CheckState.Checked;
                        }
                        isSelected = false;
                    }
                    break;
                }
                else
                {
                    isSelected = false;
                }
            }
            return isSelected;
        }

        private void ApplyRecentPrjectDetails()
        {
            try
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                {
                    accountingSystem.YearFrom = this.AppSetting.YearFrom;
                    accountingSystem.YearTo = this.AppSetting.YearTo;
                    resultArgs = accountingSystem.FetchRecentProjectDetails(this.LoginUser.LoginUserId);
                    if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtProject = resultArgs.DataSource.Table;
                        foreach (DataRow dr in dtProject.Rows)
                        {
                            if (string.IsNullOrEmpty(dr[colVoucherDate].ToString()))
                            {
                                dr[colVoucherDate] = dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                            }
                        }
                        this.AppSetting.UserProjectInfor = resultArgs.DataSource.Table.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        //{
        //    //if (KeyData == (Keys.Control | Keys.F))
        //    //{
        //    //    gvAccPeriod.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
        //    //    gvAccPeriod.OptionsView.

        //    //}
        //    return base.ProcessCmdKey(ref msg, KeyData);
        //}

        private void frmAccountingPeriodView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        public void LoadTransPeriodAdd()
        {
            if (IsFirstTrans)
            {
                frmAccountingPeriodAdd accountAdd = new frmAccountingPeriodAdd();
                accountAdd.ShowDialog();
                UpdateTransacationFirstTransPeriod(accountAdd.AccID);
                LoadAccountingPeriod();
                this.ShowCurrentPeriod();
            }
        }
        private void UpdateTransacationFirstTransPeriod(int accountid)
        {
            using (GlobalSetting globalSystem = new GlobalSetting())
            {
                resultArgs = globalSystem.UpdateAccountingPeriod(accountid.ToString());
                if (resultArgs.Success)
                {
                    using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                    {
                        resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                        if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                        {
                            this.AppSetting.AccPeriodInfo = resultArgs.DataSource.Table.DefaultView;
                            ApplyRecentPrjectDetails();
                            this.SetTransacationPeriod();
                        }
                    }
                }
            }
        }

        private void frmAccountingPeriodView_EnterClicked(object sender, EventArgs e)
        {
            ShowAccPeriodForm();
        }

        private void ucToolBarCostCentre_MoveTransaction(object sender, EventArgs e)
        {
            string msg = "Are you sure to create new Finance Year before first Finance Year '" + AppSetting.FirstFYDateFrom.ToShortDateString() + "' ?" +
                    System.Environment.NewLine + "Note : It will refresh all Opening Balances and Acme.erp will be restarted to apply new Finance Year.";

            if (this.ShowConfirmationMessage(msg,  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                using (AccouingPeriodSystem acc = new AccouingPeriodSystem())
                {
                    this.ShowWaitDialog("Refreshing Ledger Balances");
                    ResultArgs result = acc.MovePreviousFYPeriod();
                    this.CloseWaitDialog();
                    if (!result.Success)
                    {
                        MessageRender.ShowMessage(result.Message);
                    }
                    else
                    {
                        this.ShowMessageBox("Acme.erp will be restarted to apply new Finance Year.");
                        Bosco.Utility.ConfigSetting.SettingProperty.Is_Application_Logout = true;
                        Application.Restart();
                    }
                }
            }
        }


    }
}