//This form is to show active FD openings and Investment Accounts 
//Active-FD account which is not withdrawn ..,Closed--FD account which withdrawan full amount
using System;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Master
{
    public partial class frmFDAccountView : frmFinanceBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        FDTypes fdTypes;
        #endregion

        #region Construtor
        public frmFDAccountView()
        {
            InitializeComponent();
        }

        public frmFDAccountView(FDTypes fdTypes)
            : this()
        {
            this.fdTypes = fdTypes;
        }

        #endregion

        #region Properties

        private int AccountId = 0;
        private int FDAccountId
        {
            get
            {
                AccountId = gvFDAccount.GetFocusedRowCellValue(colAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvFDAccount.GetFocusedRowCellValue(colAccountId).ToString()) : 0;
                return AccountId;
            }
            set { AccountId = value; }
        }

        private int projectId = 0;
        private int ProjectId
        {
            get
            {
                projectId = gvFDAccount.GetFocusedRowCellValue(colProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvFDAccount.GetFocusedRowCellValue(colProjectId).ToString()) : 0;
                return projectId;
            }
            set { projectId = value; }
        }

        private string projectname= string.Empty;
        private string ProjectName
        {
            get
            {
                projectname = gvFDAccount.GetFocusedRowCellValue(colProjectName) != null ? gvFDAccount.GetFocusedRowCellValue(colProjectName).ToString() : string.Empty;
                return projectname;
            }
        }


        private int LedgerId = 0;
        private int FDLedgerId
        {
            get
            {
                LedgerId = gvFDAccount.GetFocusedRowCellValue(colLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvFDAccount.GetFocusedRowCellValue(colLedgerId).ToString()) : 0;
                return LedgerId;
            }
            set { LedgerId = value; }
        }
        private string FDTransMode = string.Empty;
        private string fdTransMode
        {
            get
            {
                FDTransMode = gvFDAccount.GetFocusedRowCellValue(colTransMode) != null ? gvFDAccount.GetFocusedRowCellValue(colTransMode).ToString() : string.Empty;
                return FDTransMode;
            }
            set
            {
                FDTransMode = value;
            }
        }

        private int FDVoucherId = 0;
        private int fdVoucherId
        {
            get
            {
                FDVoucherId = gvFDAccount.GetFocusedRowCellValue(colFDVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvFDAccount.GetFocusedRowCellValue(colFDVoucherId).ToString()) : 0;
                return FDVoucherId;
            }
            set
            {
                FDVoucherId = value;
            }
        }

        private string fdTransType = string.Empty;
        private string FDTransType
        {
            get
            {
                fdTransType = gvFDAccount.GetFocusedRowCellValue(colTransType) != null ? gvFDAccount.GetFocusedRowCellValue(colTransType).ToString() : string.Empty;
                return fdTransType;
            }
            set
            {
                fdTransType = value;
            }
        }

        private double fdAmount = 0;
        private double FDAmount
        {
            get
            {
                fdAmount = gvFDAccount.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvFDAccount.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return fdAmount;
            }
            set
            {
                fdAmount = value;
            }
        }

        private int Status = 0;
        private int FDStatus
        {
            get
            {
                Status = gvFDAccount.GetFocusedRowCellValue(colFDStatus) != null ? this.UtilityMember.NumberSet.ToInteger(gvFDAccount.GetFocusedRowCellValue(colFDStatus).ToString()) : 0;
                return Status;
            }
            set
            {
                Status = value;
            }
        }

        private DateTime createdon = DateTime.MinValue;
        private DateTime CreatedOn
        {
            get
            {
                createdon = gvFDAccount.GetFocusedRowCellValue(colCreatedOn) != null ? this.UtilityMember.DateSet.ToDate(gvFDAccount.GetFocusedRowCellValue(colCreatedOn).ToString(), false) : DateTime.MinValue;
                return createdon;
            }
        }

        private string fdAccountStatus = string.Empty;
        private string FDAccountStatus
        {
            get
            {
                fdAccountStatus = gvFDAccount.GetFocusedRowCellValue(colStatus) != null ? gvFDAccount.GetFocusedRowCellValue(colStatus).ToString() : string.Empty;
                return fdAccountStatus;
            }
            set
            {
                fdAccountStatus = value;
            }
        }
        
        private Int32 FDAccountSchemeId
        {
            get
            {
                Int32 fdAccountscheme = gvFDAccount.GetFocusedRowCellValue(colfdschemeid) != null ? 
                       UtilityMember.NumberSet.ToInteger(gvFDAccount.GetFocusedRowCellValue(colfdschemeid).ToString()) : 0;
                return fdAccountscheme;
            }
        }
        private int FDCount { get; set; }
        #endregion

        #region Events
        private void frmFDAccountView_Load(object sender, EventArgs e)
        {
            SetDefault();
            ApplyUserRights();
                        
            this.AttachGridContextMenu(gcFDAccount);
            this.AttachGridContextMenu(ucFDHistory1.GridFDHistory);
        }

        private void frmFDAccountView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj
            SetVisibileShortCuts(false, true );
            FetchFDAccountDetails();

            //On 16/10/2024, To show currency
            ColCurrencyName.Visible = false;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                colPrincipalAmount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.None;

                ColCurrencyName.Visible = true;
                ColCurrencyName.VisibleIndex = (colCreatedOn.VisibleIndex + 1);
            }
        }

        private void ApplyUserRights()
        {
            if (fdTypes == FDTypes.OP)
            {
                this.enumUserRights.Add(FDOpening.CreateFixedDeposit);
                this.enumUserRights.Add(FDOpening.EditFixedDeposit);
                this.enumUserRights.Add(FDOpening.DeleteFixedDeposit);
                this.enumUserRights.Add(FDOpening.PrintFixedDeposit);
                this.enumUserRights.Add(FDOpening.ViewFixedDeposit);
                this.ApplyUserRights(ucToolBarFDAccountView, this.enumUserRights, (int)Menus.FixedDeposit);
            }
            else
            {
                this.enumUserRights.Add(FDInvestment.CreateFixedInvestment);
                this.enumUserRights.Add(FDInvestment.EditFixedInvestment);
                this.enumUserRights.Add(FDInvestment.DeleteFixedInvestment);
                this.enumUserRights.Add(FDInvestment.PrintFixedInvestment);
                this.enumUserRights.Add(FDInvestment.ViewFixedInvestment);
                this.ApplyUserRights(ucToolBarFDAccountView, this.enumUserRights, (int)Menus.FixedInvestment);
            }
        }

        private void ucToolBarFDAccountView_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow);
        }

        private void ucToolBarFDAccountView_EditClicked(object sender, EventArgs e)
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = FDAccountId;
                FDCount = fdAccountSystem.CountRenewalDetails();
                if (FDCount == 0)
                {
                    ShowFDAccountForm();
                }
                else
                {
                    ShowFDAccountForm();
                }
            }
        }

        private void gvFDAccount_DoubleClick(object sender, EventArgs e)
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = FDAccountId;
                FDCount = fdAccountSystem.CountRenewalDetails();
                if (FDCount == 0)
                {
                    ShowFDAccountForm();
                }
                else
                {
                    ShowFDAccountForm();
                }
            }
        }

        private void ucToolBarFDAccountView_DeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (gvFDAccount.RowCount != 0)
                {
                    if (FDAccountId != 0)
                    {
                        if (!base.IsVoucherLockedForDate(ProjectId, CreatedOn, true))
                        {
                            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                            {
                                fdAccountSystem.FDAccountId = FDAccountId;
                                if (fdAccountSystem.CountRenewalDetails() == 0)
                                {
                                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        fdAccountSystem.ProjectId = ProjectId;
                                        fdAccountSystem.LedgerId = FDLedgerId;

                                        fdAccountSystem.FDTransMode = fdTransMode;
                                        fdAccountSystem.FDVoucherId = fdVoucherId;
                                        fdAccountSystem.FDTransType = FDTransType;
                                        fdAccountSystem.FdAmount = FDAmount;
                                        fdAccountSystem.FDOPInvestmentDate = this.AppSetting.YearFrom;

                                        resultArgs = fdAccountSystem.RemoveFDAccountDetails();

                                        if (resultArgs.Success)
                                        {
                                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                            FetchFDAccountDetails();
                                        }
                                        else
                                        {
                                            this.ShowMessageBox(resultArgs.Message);
                                        }

                                        //chinna
                                        if (resultArgs.Success && fdAccountSystem.CheckPhysicalFDAccountExists() != 0)
                                        {
                                            resultArgs = fdAccountSystem.DeleteFDPhysicalRenewalDetails();
                                            if (resultArgs.Success)
                                            {
                                                //resultArgs = fdAccountSystem.DeleteFDPhysicalAccountDetails();
                                                resultArgs = fdAccountSystem.DeleteFDPhysicalOpeningDetails();
                                            }
                                        }
                                        else
                                        {
                                            resultArgs = fdAccountSystem.DeleteFDPhysicalOpeningDetails();
                                        }
                                        
                                    }
                                }
                                else
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CANNOT_DELETE_ASSCOCIATE_RENEWAL));
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucToolBarFDAccountView_PrintClicked(object sender, EventArgs e)
        {
            if (fdTypes == FDTypes.OP)
            {
                this.PrintGridViewDetails(gcFDAccount, this.GetMessage(MessageCatalog.Master.FDLedger.FD_ACCOUNT_OP_PRINT_CAPTION), PrintType.DT, gvFDAccount, true);
            }
            else
            {
                this.PrintGridViewDetails(gcFDAccount, this.GetMessage(MessageCatalog.Master.FDLedger.FD_ACCOUNT_INV_PRINT_CAPTION), PrintType.DT, gvFDAccount, true);
            }
        }

        private void ucToolBarFDAccountView_RefreshClicked(object sender, EventArgs e)
        {
            FetchFDAccountDetails();
        }

        private void ucToolBarFDAccountView_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkFDAccount_CheckedChanged(object sender, EventArgs e)
        {
            gvFDAccount.OptionsView.ShowAutoFilterRow = chkFDAccount.Checked;
            if (chkFDAccount.Checked)
            {
                this.SetFocusRowFilter(gvFDAccount, colFDAccountNumber);
            }
        }

        /// <summary>
        /// Refresh the grid after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchFDAccountDetails();
        }
        /// <summary>
        /// Refresh the grid after adding and editing the values. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void gvFDAccount_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvFDAccount.RowCount.ToString();

            if (gcFDAccount.DataSource != null)
            {
                if (colPrincipalAmount.SummaryItem.SummaryType != DevExpress.Data.SummaryItemType.None)
                    colPrincipalAmount.SummaryItem.DisplayFormat = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(colPrincipalAmount.SummaryItem.SummaryValue.ToString()));
            }
        }

        private void frmFDAccountView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkFDAccount.Checked = (chkFDAccount.Checked) ? false : true;
        }
        #endregion

        #region Methods
        /// <summary>
        ///Route the FD Investment or FD Opening Form.
        /// </summary>
        /// <param name="FDAccountId"></param>
        private void ShowForm(int FDAccountId)
        {
            int RowIndex = 0;
            //try
            //{
            if (FDAccountId == 0 || !base.IsVoucherLockedForDate(ProjectId, CreatedOn, true))
            {
                RowIndex = gvFDAccount.FocusedRowHandle;
                if (!IsFDRenewalsAvailable(FDAccountId))
                {
                    frmFDAccount frmAccount = new frmFDAccount(FDAccountId, 0, fdTypes);
                    frmAccount.FDRenewalCount = FDCount;
                    frmAccount.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmAccount.ShowDialog();
                    
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_RENEWALS_AVAILABLE));
                }
                FDCount = 0;
                gvFDAccount.FocusedRowHandle = RowIndex;
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            //}
            //finally { }
        }


        private bool IsFDRenewalsAvailable(Int32 fdid)
        {
            bool isfdrenewals = false;

            if (fdid > 0)
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    fdAccountSystem.FDAccountId = FDAccountId;
                    Int32 FDCount = fdAccountSystem.CountRenewalDetails();

                    isfdrenewals = (FDCount > 0);
                }
            }
            return isfdrenewals;
        }

        /// <summary>
        /// To show only active FDs
        /// </summary>
        private void FetchFDAccountDetails()
        {
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    fdAccountSystem.FDTransType = fdTypes.ToString();
                    resultArgs = fdAccountSystem.FetchFDAccounts();
                    if (resultArgs.Success)
                    {
                        //On 14/11/2023, To show allt the opening and invested FDs and bring all the active FDs on Top
                        //resultArgs.DataSource.Table;
                        
                        //resultArgs.DataSource.Table.DefaultView.RowFilter = "CLOSING_STATUS<>'Closed'";
                        if (fdTypes == FDTypes.OP || fdTypes == FDTypes.IN)
                        {
                            resultArgs.DataSource.Table.DefaultView.Sort = fdAccountSystem.AppSchema.FDRenewal.CLOSING_STATUSColumn.ColumnName + " ASC, " +
                                                                            fdAccountSystem.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName + " DESC";
                            resultArgs.DataSource.Data = resultArgs.DataSource.Table.DefaultView.ToTable();
                            
                        }
                        else
                        {
                            resultArgs.DataSource.Table.DefaultView.RowFilter = fdAccountSystem.AppSchema.FDRenewal.CLOSING_STATUSColumn.ColumnName +  "<>'Closed'";
                        }
                        gcFDAccount.DataSource = resultArgs.DataSource.Table;
                        gcFDAccount.RefreshDataSource();
                    }
                    ucFDHistory1.FDAccountId = FDAccountId;
                    ucFDHistory1.ShowPanelCaptionHeader = false;
                }

                colFDScheme.Visible = false;
                if (this.UIAppSetting.EnableFlexiFD == "1")
                {
                    colFDScheme.Visible = true;
                    colFDScheme.VisibleIndex = colFDInvestmentType.VisibleIndex + 1;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ShowFDAccountForm()
        {
            try
            {
                if (fdTypes == FDTypes.OP)
                {
                    if (this.isEditable)
                    {
                        ShowFDEditFrom();
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                    }
                }
                else
                {
                    if (this.isEditable)
                    {
                        ShowFDEditFrom();
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }

        private void ShowFDEditFrom()
        {
            if (gvFDAccount.RowCount != 0)
            {
                if (FDAccountId != 0)
                {
                    ShowForm(FDAccountId);
                }
                else
                {
                    if (!chkFDAccount.Checked)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        private void SetDefault()
        {
            this.Text = fdTypes == FDTypes.OP ? this.GetMessage(MessageCatalog.Master.FDLedger.FD_OPENING_CAPTION) : this.GetMessage(MessageCatalog.Master.FDLedger.FD_INVESTMENT_CAPTION);
            //colAmount.Caption = fdTypes == FDTypes.OP ? this.GetMessage(MessageCatalog.Master.FixedDeposit.OP_BALANCE) : this.GetMessage(MessageCatalog.Master.FixedDeposit.AMOUNT);
            colAmount.Caption = this.GetMessage(MessageCatalog.Master.FixedDeposit.AMOUNT);
            colPrincipalAmount.Caption = fdTypes == FDTypes.OP ? this.GetMessage(MessageCatalog.Master.FixedDeposit.OP_BALANCE) : this.GetMessage(MessageCatalog.Master.FixedDeposit.INVESTED_AMOUNT);
            gvFDAccount.OptionsView.ShowFooter = true;
            
            if (fdTypes == FDTypes.OP)
            {
                colVNo.Visible = false;
            }

            //On 05/12/2024 to chnage fd scheme for temp purpose ---------------------------------------------------------------------
            lblFDScheme.Visible = cboFDScheme.Visible = btnUpdate.Visible = false;
        }
        #endregion

        private void frmFDAccountView_EnterClicked(object sender, EventArgs e)
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = FDAccountId;
                FDCount = fdAccountSystem.CountRenewalDetails();
                if (FDCount == 0)
                {
                    ShowFDAccountForm();
                }
                else
                {
                    ShowFDAccountForm();
                }
            }
        }

        private void dockManager1_ActivePanelChanged(object sender, DevExpress.XtraBars.Docking.ActivePanelChangedEventArgs e)
        {
            if (dockManager1.ActivePanel != null)
            {
                dockFDHPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
        }

        private void gvFDAccount_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucFDHistory1.FDAccountId = FDAccountId;

            //On 05/12/2024 to chnage fd scheme for temp purpose ---------------------------------------------------------------------
            ShowFDScheme();
            //-------------------------------------------------------------------------------------------------------------------------
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to Change Investment Scheme ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                == System.Windows.Forms.DialogResult.Yes)
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    fdAccountSystem.FDAccountId = FDAccountId;
                    ResultArgs result = fdAccountSystem.UpdateFDInvestmentScheme(cboFDScheme.SelectedIndex);
                    if (!result.Success) this.ShowMessageBox(result.Message);
                    FetchFDAccountDetails();

                    //On 05/12/2024 to chnage fd scheme for temp purpose ---------------------------------------------------------------------
                    ShowFDScheme();
                    //-------------------------------------------------------------------------------------------------------------------------
                }
            }
        }

        private void ShowFDScheme()
        {
            //On 05/12/2024 to chnage fd scheme for temp purpose ---------------------------------------------------------------------
            lblFDScheme.Visible = cboFDScheme.Visible = btnUpdate.Visible = false;
            if (AppSetting.EnableFlexiFD == "1")
            {
                if (fdTypes == FDTypes.OP || fdTypes == FDTypes.IN)
                {   
                    if (FDAccountSchemeId >= 0)
                    {
                        cboFDScheme.SelectedIndex = FDAccountSchemeId;
                        lblFDScheme.Visible = cboFDScheme.Visible = btnUpdate.Visible = true;
                    }
                }
            }
            //-------------------------------------------------------------------------------------------------------------------------
        }
    }
}
