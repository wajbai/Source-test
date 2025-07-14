///This form is to show active FD openings and Investment Accounts which is for renewals/PostInterests
//and also shows the renewal History and Post Interest History
//Active-FD account which is not withdrawn ..,Closed--Fd account withdrawan full amount
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Bosco.Model.Transaction;
using Bosco.Utility;
using DevExpress.XtraLayout.Utils;
using ACPP.Modules.Transaction;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.Master
{
    public partial class frmRenewalView : frmFinanceBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        FDTypes fdTypes;
        
        DataSet dsFDRenewal = new DataSet();
        int RenewalId = 0;
        bool lockParentDblClick = false;
        #endregion

        #region Properties

        private int AccountId = 0;
        private int FDAccountId
        {
            get
            {
                AccountId = gvRenewal.GetFocusedRowCellValue(colFDAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colFDAccountId).ToString()) : 0;
                return AccountId;
            }
            set { AccountId = value; }
        }

        private int projectId = 0;
        private int ProjectId
        {
            get
            {
                projectId = gvRenewal.GetFocusedRowCellValue(colProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colProjectId).ToString()) : 0;
                return projectId;
            }
            set { projectId = value; }
        }

        private int LedgerId = 0;
        private int FDLedgerId
        {
            get
            {
                LedgerId = gvRenewal.GetFocusedRowCellValue(colLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colLedgerId).ToString()) : 0;
                return LedgerId;
            }
            set { LedgerId = value; }
        }

        private string ProjectName = string.Empty;
        private string projectName
        {
            get
            {
                ProjectName = gvRenewal.GetFocusedRowCellValue(colProject) != null ? gvRenewal.GetFocusedRowCellValue(colProject).ToString() : string.Empty;
                return ProjectName;
            }
            set
            {
                ProjectName = value;
            }
        }

        private Int32 ProjectIdFDAccount
        {
            get
            {
                Int32 pid = 0;
                if (gcRenewalView.MainView != null)
                {
                    pid = (gcRenewalView.MainView as GridView).GetFocusedRowCellValue(colProjectId) != null ?
                        UtilityMember.NumberSet.ToInteger((gcRenewalView.MainView as GridView).GetFocusedRowCellValue(colProjectId).ToString()) : 0;
                }
                return pid;
            }
        }

        private string ProjectNameFDAccount
        {
            get
            {
                string pname = string.Empty;
                if (gcRenewalView.MainView != null)
                {
                    pname = (gcRenewalView.MainView as GridView).GetFocusedRowCellValue(colProject) != null ? (gcRenewalView.MainView as GridView).GetFocusedRowCellValue(colProject).ToString() : string.Empty;
                }
                return pname;
            }
        }

        private string fdLedgerName = string.Empty;
        private string FDLedgerName
        {
            get
            {
                fdLedgerName = gvRenewal.GetFocusedRowCellValue(colLedgerName) != null ? gvRenewal.GetFocusedRowCellValue(colLedgerName).ToString() : string.Empty;
                return fdLedgerName;
            }
            set
            {
                fdLedgerName = value;
            }
        }

        private string fdBankAccountNumber = string.Empty;
        private string FDBankAccountNumber
        {
            get
            {
                fdBankAccountNumber = gvRenewal.GetFocusedRowCellValue(colFDAccountNo) != null ? gvRenewal.GetFocusedRowCellValue(colFDAccountNo).ToString() : string.Empty;
                return fdBankAccountNumber;
            }
            set
            {
                fdBankAccountNumber = value;
            }
        }

        private double fdAmount = 0;
        private double FDPrinicipalAmount
        {
            get
            {
                fdAmount = gvRenewal.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvRenewal.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return fdAmount;
            }
            set
            {
                fdAmount = value;
            }
        }

        private double fdIntrestAmount = 0;
        private double FDIntrestAmount
        {
            get
            {
                fdIntrestAmount = gvRenewal.GetFocusedRowCellValue(colInsAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvRenewal.GetFocusedRowCellValue(colInsAmount).ToString()) : 0;
                return fdIntrestAmount;
            }
            set
            {
                fdIntrestAmount = value;
            }
        }

        private double fdreinvestedamount = 0;
        private double FDReInvestedAmount
        {
            get
            {
                fdreinvestedamount = gvRenewal.GetFocusedRowCellValue(gvcolReInvestedAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvRenewal.GetFocusedRowCellValue(gvcolReInvestedAmount).ToString()) : 0;
                return fdreinvestedamount;
            }
            set
            {
                fdreinvestedamount = value;
            }
        }

        private double fdExpectedIntrestAmount = 0;
        private double FDExpectedIntrestAmount
        {
            get
            {
                fdExpectedIntrestAmount = gvRenewal.GetFocusedRowCellValue(colExpectedMaturityValue) != null ? this.UtilityMember.NumberSet.ToDouble(gvRenewal.GetFocusedRowCellValue(colExpectedMaturityValue).ToString()) : 0;
                return fdExpectedIntrestAmount;
            }
            set
            {
                fdExpectedIntrestAmount = value;
            }
        }

        private DateTime dtematurityDate;
        private DateTime dteMaturityDate
        {
            get
            {
                dtematurityDate = gvRenewal.GetFocusedRowCellValue(colMaturityDate) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colMaturityDate).ToString(), false) : DateTime.Now;
                return dtematurityDate;
            }
            set
            {
                dtematurityDate = value;
            }
        }

        private int Status = 0;
        private int FDStatus
        {
            get
            {
                Status = gvRenewal.GetFocusedRowCellValue(colFDRenewalStatus) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colFDRenewalStatus).ToString()) : 0;
                return Status;
            }
            set
            {
                Status = value;
            }
        }

        private string fdStatus = string.Empty;
        private string FDWithdrawStatus
        {
            get
            {
                fdStatus = gvRenewal.GetFocusedRowCellValue(colFDRenewalStatus) != null ? gvRenewal.GetFocusedRowCellValue(colFDRenewalStatus).ToString() : string.Empty;
                return fdStatus;
            }
            set
            {
                fdStatus = value;
            }
        }

        private int fdRenewalId = 0;
        private int FDRenewalId
        {
            get
            {
                fdRenewalId = gvRenewal.GetFocusedRowCellValue(colFDRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colFDRenewalId).ToString()) : 0;
                return fdRenewalId;
            }
            set
            {
                fdRenewalId = value;
            }
        }

        private int fdVoucherId = 0;
        private int FDVoucherId
        {
            get
            {
                fdVoucherId = gvRenewal.GetFocusedRowCellValue(colRenewalVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colRenewalVoucherId).ToString()) : 0;
                return fdVoucherId;
            }
            set
            {
                fdVoucherId = value;
            }
        }

        private string fdTransType = string.Empty;
        private string FDTransType
        {
            get
            {
                fdTransType = gvRenewal.GetFocusedRowCellValue(colTransType) != null ? gvRenewal.GetFocusedRowCellValue(colTransType).ToString() : string.Empty;
                return fdTransType;
            }
            set
            {
                fdTransType = value;
            }
        }
        private string fdRenewalTypes = string.Empty;
        private string FDRenewalTypes
        {
            get
            {
                fdRenewalTypes = gvFDRenewal.GetFocusedRowCellValue(colRenewalType) != null ? gvFDRenewal.GetFocusedRowCellValue(colRenewalType).ToString() : string.Empty;
                return fdRenewalTypes;
            }
            set
            {
                fdRenewalTypes = value;
            }
        }

        private int BankAccountId = 0;
        private int FDBankId
        {
            get
            {
                BankAccountId = gvRenewal.GetFocusedRowCellValue(colBankId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colBankId).ToString()) : 0;
                return BankAccountId;
            }
            set
            {
                BankAccountId = value;
            }
        }

        private string fdBankName = string.Empty;
        private string FDBankName
        {
            get
            {
                fdBankName = gvRenewal.GetFocusedRowCellValue(colBranch) != null ? gvRenewal.GetFocusedRowCellValue(colBranch).ToString() : string.Empty;
                return fdBankName;
            }
            set
            {
                fdBankName = value;
            }
        }

        private double Amount = 0;
        private double PrincipalAmount
        {
            get
            {
                Amount = gvRenewal.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvRenewal.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return Amount;
            }
            set
            {
                Amount = value;
            }
        }

        private double insAmount = 0;
        private double InvestedAmount
        {
            get
            {
                insAmount = gvRenewal.GetFocusedRowCellValue(colPrincipalAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvRenewal.GetFocusedRowCellValue(colPrincipalAmount).ToString()) : 0;
                return insAmount;
            }
            set
            {
                insAmount = value;
            }
        }

        private string CreatedOn = string.Empty;
        private string FDRenewalDate
        {
            get
            {
                CreatedOn = gvRenewal.GetFocusedRowCellValue(colMaturityDate) != null ? gvRenewal.GetFocusedRowCellValue(colMaturityDate).ToString() : string.Empty;
                return CreatedOn;
            }
            set
            {
                CreatedOn = value;
            }
        }
        private int bankLedgerId = 0;
        private int BankLedgerId
        {
            get
            {
                bankLedgerId = gvRenewal.GetFocusedRowCellValue(colBankAccountLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colBankAccountLedgerId).ToString()) : 0;
                return bankLedgerId;
            }
            set { bankLedgerId = value; }
        }

        private string _fdStatus = string.Empty;
        private string FDActiveStatus
        {
            get
            {
                _fdStatus = gvRenewal.GetFocusedRowCellValue(colStatus) != null ? gvRenewal.GetFocusedRowCellValue(colStatus).ToString() : string.Empty;
                return _fdStatus;
            }
            set
            {
                _fdStatus = value;
            }
        }

        private double FDInterestRate = 0;
        private double InterestRate
        {
            get { return gvRenewal.GetFocusedRowCellValue(colInsRate) != null ? this.UtilityMember.NumberSet.ToDouble(gvRenewal.GetFocusedRowCellValue(colInsRate).ToString()) : 0; }
            set { FDInterestRate = value; }
        }

        private int RenewalID { get; set; }
        private int fdAccountId { get; set; }
        private DataTable dtFDRenewal { get; set; }
        private DataTable dtFDMaster { get; set; }
        private DataSet dsRenewal { get; set; }
        private DateTime fdRenewalDate { get; set; }

        private int closedVoucherId = 0;
        private int ClosedVoucherId
        {
            get { return gvRenewal.GetFocusedRowCellValue(colVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colVoucherId).ToString()) : 0; }
        }

        #endregion

        #region Post Interest properties
        public DateTime FDAccountCreatedDate { get; set; }
        public decimal FDAccountInsRate { get; set; }
        public DateTime FDPostMaturityDate { get; set; }
        public int FDPostInterestType { get; set; }
        public string FDPostRenewalType { get; set; }
        #endregion

        #region Constructor
        public frmRenewalView()
        {
            InitializeComponent();
        }

        public frmRenewalView(FDTypes fdTypes)
            : this()
        {
            this.fdTypes = fdTypes;

            if (this.fdTypes == FDTypes.WD || this.fdTypes == FDTypes.POC)
            {
                gvRenewal.OptionsSelection.EnableAppearanceFocusedRow = false;
                gvWithDrawalHistory.OptionsSelection.EnableAppearanceFocusedRow = false;
            }
        }

        #endregion

        #region Events

        private void frmRenewalView_Load(object sender, EventArgs e)
        {
            ApplyUserRights();
            SetAlignment();

            //31/07/2024, Other than India, let us lock TDS Amount
            colTDSAmount.Visible = colPOITDSAmount.Visible = colWithdrwalTDSAmt.Visible = !(this.AppSetting.IsCountryOtherThanIndia);
            
            this.AttachGridContextMenu(gcRenewalView);
            this.AttachGridContextMenu(ucFDHistory1.GridFDHistory);
        }

        private void frmRenewalView_Activated(object sender, EventArgs e)
        {
            //Added by Carmel Raj M
            if (chkSelectAll.Checked)
                SetVisibileShortCuts(false, true);
            else
                SetVisibileShortCuts(true, true);

            LoadDefaults();
            FetchFDAccountDetails();
            DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dteMaturityAsOn.DateTime = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtyearfrom;
            HasFDRenewal(FDAccountId);

            //On 16/10/2024, To show currency
            ColCurrencyName.Visible = false;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ColCurrencyName.Visible = true;
                ColCurrencyName.VisibleIndex = (colAmount.VisibleIndex - 1);
            }

        }

        private void ucToolBarRenewal_AddClicked(object sender, EventArgs e)
        {
            if (fdTypes != FDTypes.POI)
            {
                ShowFDAccountForm();
            }
            else if (fdTypes == FDTypes.POI)
            {
                ShowFDPostInterestForm();
            }
        }

        private void ucToolBarRenewal_EditClicked(object sender, EventArgs e)
        {
            //11/11/2021, to have common method
            InvokeModifiedDetails();

            /*if (fdTypes != FDTypes.POI && fdTypes != FDTypes.RIN)
            {
                ShowRenewalForm();
            }
            else if (fdTypes == FDTypes.POI)
            {
                ShowFDPostInterestEditForm();
            }
            else if (fdTypes == FDTypes.RIN)
            {
                showFDReInvestmentEditForm();
            }*/
        }

        /// <summary>
        /// While deleting the Fd Account ,It will check for the renewal existance,if renewal/PostInterest exists,alert message will be shown
        /// otherwise deletes the selected FD account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBarRenewal_DeleteClicked(object sender, EventArgs e)
        {
            Int32 Rowindex = 0;
            Rowindex = gvRenewal.FocusedRowHandle;
            if (fdTypes.Equals(FDTypes.RN) || fdTypes.Equals(FDTypes.POI) || fdTypes.Equals(FDTypes.RIN) || fdTypes.Equals(FDTypes.WD))
            {
                if (HasRenewals() > 1 || HasPostInterests() > 1 || HasReInvestment() > 1)
                {
                    frmRenewals frmRenewals = new frmRenewals(FDAccountId, FDLedgerName, projectName, FDBankAccountNumber, dsFDRenewal, fdTypes, "Delete", FDTransType);

                    frmRenewals.FDModifyAccountCreatedDate = FDAccountCreatedDate;
                    frmRenewals.FDModifyAccountInsRate = FDAccountInsRate;
                    frmRenewals.FDModifyPostMaturityDate = FDPostMaturityDate;
                    frmRenewals.FDModifyPostRenewalType = FDPostRenewalType;

                    frmRenewals.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmRenewals.ShowDialog();
                    FetchFDAccountDetails();
                }
                else
                {
                    using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                    {
                        fdAccountSystem.RenewedDate = fdRenewalDate;
                        if (fdAccountSystem.CheckFDWithdrawal().Equals(0))
                        {
                            if (!base.IsVoucherLockedForDate(ProjectIdFDAccount, fdRenewalDate, true))
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    fdAccountSystem.FDRenewalId = RenewalId;
                                    fdAccountSystem.FDWithdrwalInterestVoucherId = 0;

                                    if (fdTypes == FDTypes.WD)
                                    {
                                        if (gcRenewalView.FocusedView != null)
                                        {
                                            GridView gvWithdrwHistory =  gcRenewalView.FocusedView as GridView;
                                            fdAccountSystem.FDWithdrwalInterestVoucherId = gvWithdrwHistory.GetFocusedRowCellValue(colWDFDInterestVoucherID) != null ? this.UtilityMember.NumberSet.ToInteger(gvWithdrwHistory.GetFocusedRowCellValue(colWDFDInterestVoucherID).ToString()) : 0; 
                                        }
                                    }
                                    resultArgs = fdAccountSystem.DeleteFDRenewals();
                                    if (resultArgs.Success)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                        HasFDRenewal(FDAccountId);
                                    }
                                    else
                                    {
                                        this.ShowMessageBoxWarning(resultArgs.Message);
                                    }
                                }
                            }
                            FetchFDAccountDetails();
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CANNOT_DELETE_ASSCOCIATE_RENEWAL));
                        }
                    }
                }
            }
            else if (fdTypes.Equals(FDTypes.WD) || fdTypes.Equals(FDTypes.POC))
            {
                DeleteFDWithdrawal();
                FetchFDAccountDetails();
                FilterFDRenewal();
            }
            gvRenewal.FocusedRowHandle = Rowindex;
        }

        private void ucToolBarRenewal_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucToolBarRenewal_PrintClicked(object sender, EventArgs e)
        {
            if (fdTypes != FDTypes.RIN)
                this.PrintGridViewDetails(gcRenewalView, this.GetMessage(MessageCatalog.Master.FDRenewal.FD_RENEWAL_DETAILS), PrintType.DS, gvRenewal, true);
            if (fdTypes == FDTypes.RIN)
            {
                this.PrintGridViewDetails(gcRenewalView, "FD Re-Investment Details", PrintType.DS, gvRenewal, true);
            }
        }

        private void ucToolBarRenewal_RefreshClicked(object sender, EventArgs e)
        {
            cboSetActive.SelectedIndex = 0;
            chkSelectAll.Checked = true;
            FetchFDAccountDetails();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvRenewal.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                //this.SetFocusRowFilter(gvFDRenewal, colFDAccountNo);
                this.SetFocusRowFilter(gvRenewal, colFDAccountNo);
            }
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchFDAccountDetails();
            cboSetActive.SelectedIndex = 0;
            HasFDRenewal(FDAccountId);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            FilterFDRenewal();
        }

        private void gvRenewal_DoubleClick(object sender, EventArgs e)
        {
            //On 09/10/2017, When double clik on Child gridivew, its getting fired in parent gridview too
            //to lock this issue, check gvRenewal.FocusedRowHandle 
            try
            {
                if (!lockParentDblClick)
                {

                    if (fdTypes != FDTypes.WD && fdTypes != FDTypes.POC)
                    {
                        if (this.isEditable)
                        {
                            ShowFDAccountForm();
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                        }
                    }
                    else
                    {
                        //  if (this.isEditable)
                        //   {
                        string Status = gvRenewal.GetFocusedRowCellValue(colStatus) != null ? gvRenewal.GetFocusedRowCellValue(colStatus).ToString() : string.Empty;
                        if (!Status.Equals("Closed"))
                        {
                            ShowFDAccountForm();
                        }
                        // }
                        // else
                        //    {
                        //  this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                        //  }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }

        private void gvFDRenewal_DoubleClick(object sender, EventArgs e)
        {
            return; //Temp on 11/11/2021
            try
            {
                lockParentDblClick = true;
                if (fdTypes != FDTypes.WD && fdTypes != FDTypes.POC)
                {
                    if (this.isEditable)
                    {
                        //As on 11/11/2021, to have renewals list order
                        InvokeModifiedDetails();

                        //DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                        //RenewalID = gridView.GetFocusedRowCellValue(colFDRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colFDRenewalId).ToString()) : 0;
                        //fdAccountId = gridView.GetFocusedRowCellValue(colFDLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colFDLedgerId).ToString()) : 0;
                        //DateTime RenewalDate = gridView.GetFocusedRowCellValue(colRenewalDate) != null ? this.UtilityMember.DateSet.ToDate(gridView.GetFocusedRowCellValue(colRenewalDate).ToString(), false) : DateTime.MinValue;
                        //if (!(RenewalDate >= this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false)
                        //    && RenewalDate <= this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false)))
                        //{
                        //    this.ShowMessageBox("Selected Renewal detail doesn't fall on Current Financial Year, Change Financial Year and modify Renewal detail");
                        //}
                        //else
                        //{
                        //    if (!base.IsVoucherLockedForDate(ProjectId, RenewalDate, true))
                        //    {
                        //        frmFDAccount frmFDAccount = new frmFDAccount(RenewalID, fdAccountId, fdTypes, dsFDRenewal.Tables[0], dsFDRenewal.Tables[1]);
                        //        frmFDAccount.UpdateHeld += new EventHandler(OnUpdateHeld);
                        //        frmFDAccount.ProjectId = ProjectId;
                        //        frmFDAccount.ShowDialog();
                        //    }
                        //}
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
                        //ShowFDAccountForm();
                        //As on 11/11/2021, to have renewals list order
                        InvokeModifiedDetails();
                    }
                }

                //On 09/10/2017, When double clik on Child gridivew, its getting fired in parent gridview too
                //to lock this issue, check gvRenewal.FocusedRowHandle 
                try
                {
                    BeginInvoke(new MethodInvoker(delegate { lockParentDblClick = false; }));
                }
                catch (Exception err)
                {
                    string errmsg = err.Message;
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }       
        }

        private void gvRenewal_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    int FDAccountID = gvRenewal.GetFocusedRowCellValue(colFDAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colFDAccountId).ToString()) : 0;
                    HasFDRenewal(FDAccountID);

                    ucFDHistory1.FDAccountId = FDAccountId;
                    ucFDHistory1.ShowPanelCaptionHeader = false;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void gvRenewal_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = "# " + gvRenewal.RowCount.ToString();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                SetVisibileShortCuts(false, false);
                FetchFDAccountDetails();
                dteMaturityAsOn.Enabled = btnApply.Enabled = cboSetActive.Enabled = false;
                ucToolBarRenewal.DisableDeleteButton = false;
                cboSetActive.SelectedIndex = 0;
            }
            else
            {
                SetVisibileShortCuts(true, false);
                dteMaturityAsOn.Enabled = btnApply.Enabled = cboSetActive.Enabled = true;
                FilterFDRenewal();
            }
        }

        private void frmRenewalView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void gvFDRenewal_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int FDAccountID = gvRenewal.GetFocusedRowCellValue(colRenewalHistoryFDAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colRenewalHistoryFDAccountId).ToString()) : 0;
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    HasFDRenewal(FDAccountID);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void frmRenewalView_EnterClicked(object sender, EventArgs e)
        {
            ShowRenewalForm();
        }

        private void cboSetActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked && cboSetActive.SelectedIndex == 0)
                {
                    FetchFDAccountDetails();
                    ucToolBarRenewal.DisableAddButton = true;
                    ucToolBarRenewal.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (!chkSelectAll.Checked && cboSetActive.SelectedIndex == 0)
                {
                    FetchFDAccountDetails();
                    ucToolBarRenewal.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
                    ucToolBarRenewal.DisableAddButton = true;

                }
                else
                {
                    FilterFDRenewal();
                    ucToolBarRenewal.DisableDeleteButton = true;
                    ucToolBarRenewal.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
                    ucToolBarRenewal.DisableAddButton = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void gvRenewal_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string Maturedon = string.Empty;
                Maturedon = gvRenewal.GetRowCellDisplayText(e.RowHandle, gvRenewal.Columns["MATURITY_DATE"]);
                DateTime dtMaturitydate = this.UtilityMember.DateSet.ToDate(Maturedon, false);
                if (dtMaturitydate <= DateTime.Today)
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void gvReInvestmentView_DoubleClick(object sender, EventArgs e)
        {
            return; //Temp on 11/11/2021

            lockParentDblClick = true;
            fdTypes = FDTypes.RIN;
            if (fdTypes != FDTypes.WD && fdTypes != FDTypes.POC)
            {
                if (this.isEditable)
                {
                    //As on 11/11/2021, to have renewals list order
                    InvokeModifiedDetails();

                    //DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    //RenewalID = gridView.GetFocusedRowCellValue(colFDRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colFDRenewalId).ToString()) : 0;
                    //fdAccountId = gridView.GetFocusedRowCellValue(colFDLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colFDLedgerId).ToString()) : 0;
                    //DateTime RenewalDate = gridView.GetFocusedRowCellValue(colRenewalDate) != null ? this.UtilityMember.DateSet.ToDate(gridView.GetFocusedRowCellValue(colRenewalDate).ToString(), false) : DateTime.MinValue;
                    //if (!base.IsVoucherLockedForDate(ProjectIdFDAccount, RenewalDate, true))
                    //{
                    //    frmFDAccount frmFDAccount = new frmFDAccount(RenewalID, fdAccountId, fdTypes, dsFDRenewal.Tables[0], dsFDRenewal.Tables[1]);
                    //    frmFDAccount.PostInterestCreatedDate = FDAccountCreatedDate;
                    //    frmFDAccount.PostInterestRate = FDAccountInsRate;
                    //    frmFDAccount.PostInterestMaturityDate = FDPostMaturityDate;
                    //    frmFDAccount.PostRenewalType = FDPostRenewalType;

                    //    frmFDAccount.ProjectId = ProjectId;
                    //    frmFDAccount.ShowDialog();
                    //}
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
                    //As on 11/11/2021, to have renewals list order
                    InvokeModifiedDetails();
                    //ShowFDAccountForm();
                }
            }

            //On 09/10/2017, When double clik on Child gridivew, its getting fired in parent gridview too
            //to lock this issue, check gvRenewal.FocusedRowHandle 
            try
            {
                BeginInvoke(new MethodInvoker(delegate { lockParentDblClick = false; }));
            }
            catch (Exception err)
            {
                string errmsg = err.Message;
            }
        }
        #endregion

        #region Post Interest Events
        private void ucToolBarRenewal_PostInterestClicked(object sender, EventArgs e)
        {
            this.fdTypes = FDTypes.POI;
            ShowFDPostInterestForm();

        }

        private void gvFDPostInterest_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int FDAccountID = gvRenewal.GetFocusedRowCellValue(colFDAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colFDAccountId).ToString()) : 0;
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    HasFDRenewal(FDAccountID);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void gvFDPostInterest_DoubleClick(object sender, EventArgs e)
        {
            return; //Temp on 11/11/2021

            lockParentDblClick = true;
            fdTypes = FDTypes.POI;
            if (fdTypes != FDTypes.WD && fdTypes != FDTypes.POC)
            {
                if (this.isEditable)
                {
                    //As on 11/11/2021, to have renewals list order
                    InvokeModifiedDetails();

                    //DevExpress.XtraGrid.Views.Grid.GridView gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    //RenewalID = gridView.GetFocusedRowCellValue(colFDRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colFDRenewalId).ToString()) : 0;
                    //fdAccountId = gridView.GetFocusedRowCellValue(colFDLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gridView.GetFocusedRowCellValue(colFDLedgerId).ToString()) : 0;
                    //DateTime RenewalDate = gridView.GetFocusedRowCellValue(colRenewalDate) != null ? this.UtilityMember.DateSet.ToDate(gridView.GetFocusedRowCellValue(colRenewalDate).ToString(), false) : DateTime.MinValue;
                    //if (!base.IsVoucherLockedForDate(ProjectIdFDAccount, RenewalDate, true))
                    //{
                    //    frmFDAccount frmFDAccount = new frmFDAccount(RenewalID, fdAccountId, fdTypes, dsFDRenewal.Tables[0], dsFDRenewal.Tables[1]);

                    //    frmFDAccount.PostInterestCreatedDate = FDAccountCreatedDate;
                    //    frmFDAccount.PostInterestRate = FDAccountInsRate;
                    //    frmFDAccount.PostInterestMaturityDate = FDPostMaturityDate;
                    //    frmFDAccount.PostRenewalType = FDPostRenewalType;

                    //    frmFDAccount.ProjectId = ProjectId;
                    //    frmFDAccount.ShowDialog();
                    //}
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
                    //As on 11/11/2021, to have renewals list order
                    InvokeModifiedDetails();

                    //ShowFDAccountForm();
                }
            }

            //On 09/10/2017, When double clik on Child gridivew, its getting fired in parent gridview too
            //to lock this issue, check gvRenewal.FocusedRowHandle 
            try
            {
                BeginInvoke(new MethodInvoker(delegate { lockParentDblClick = false; }));
            }
            catch (Exception err)
            {
                string errmsg = err.Message;
            }
        }
        #endregion

        #region Post Interest Methods
        private void ShowFDPostInterestForm()
        {
            try
            {
                if (gvRenewal.RowCount > 0)
                {
                    if (FDAccountId > 0)
                    {
                        ShowForm(FDAccountId);
                        //this.fdTypes = FDTypes.RN;
                        FetchFDAccountDetails();
                    }
                    else
                    {
                        if (!chkShowFilter.Checked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                        }
                    }
                }
                else
                {
                    if (fdTypes == FDTypes.POI)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDRenewal.RENEWAL_VIEW_NO_RECORD_TO_RENEW));
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDRenewal.WITHDRAW_VIEW_NO_RECORD));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// This is to check wheather FD Account has Multiple Post interest based on the FD 	Account Id
        /// </summary>
        /// <param name="FDAccountId"></param>
        /// <params name="FD_TYPE">"POI"</params>
        private void HasFDPostInterest(int FDAccountId)
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = FDAccountId;
                if (fdAccountSystem.HasFDRenewal() > 0)
                {
                    DataTable dtLastRenewal = fdAccountSystem.GetLastRenewalIdByFDAccount().DataSource.Table;
                    FDAccountCreatedDate = UtilityMember.DateSet.ToDate(dtLastRenewal.Rows[0]["RENEWAL_DATE"].ToString(), false);
                    FDAccountInsRate = UtilityMember.NumberSet.ToDecimal(dtLastRenewal.Rows[0]["INTEREST_RATE"].ToString());
                    FDPostMaturityDate = gvRenewal.GetFocusedRowCellValue(colMaturityDate) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colMaturityDate).ToString(), false) : DateTime.Now;
                    FDPostInterestType = UtilityMember.NumberSet.ToInteger(dtLastRenewal.Rows[0]["INTEREST_TYPE"].ToString());

                    ucToolBarRenewal.DisableEditButton = true;
                    ucToolBarRenewal.DisableDeleteButton = true;
                }
                else
                {
                    FDAccountCreatedDate = gvRenewal.GetFocusedRowCellValue(colCreatedOn) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colCreatedOn).ToString(), false) : DateTime.Now;
                    FDAccountInsRate = gvRenewal.GetFocusedRowCellValue(colInsRate) != null ? this.UtilityMember.NumberSet.ToDecimal(gvRenewal.GetFocusedRowCellValue(colInsRate).ToString()) : 0;
                    FDPostMaturityDate = gvRenewal.GetFocusedRowCellValue(colMaturityDate) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colMaturityDate).ToString(), false) : DateTime.Now;
                    FDPostInterestType = gvRenewal.GetFocusedRowCellValue(colInsType) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colInsType).ToString()) : 0;

                    ucToolBarRenewal.DisableEditButton = false;
                    ucToolBarRenewal.DisableDeleteButton = false;

                }
                ucToolBarRenewal.DisablePostInterest = true;
            }
        }

        /// <summary>
        /// Show renewal form such as "frmRenewals" if it has more than one renewal.
        ///	It will check for "HasRenewals()" based on the FD_ACCOUNT_ID
        /// </summary>
        /// Steps: In this, First it checks for renewals/PostInterest exists..
        /// If FD account has more than one 
        /// renewals/PostInterest ,
        /// it will route to the "frmRenewals" screen to select the recent 	renewal/PostInterest for edit.
        /// If only one renewal/PostInterest exists it edit the current 	renewal/PostInterest.
        ///Methods Used:HasRenewals(),HasPostInterest(),fetchFDAccountDetails()
        private void ShowFDPostInterestEditForm()
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                if (this.isEditable)
                {
                    if (HasRenewals() > (int)YesNo.Yes)
                    {
                        frmRenewals frmRenewals = new frmRenewals(FDAccountId, FDLedgerName, projectName, FDBankAccountNumber, dsFDRenewal, fdTypes, "Edit", FDTransType);

                        frmRenewals.FDModifyAccountCreatedDate = FDAccountCreatedDate;
                        frmRenewals.FDModifyAccountInsRate = FDAccountInsRate;
                        frmRenewals.FDModifyPostMaturityDate = FDPostMaturityDate;
                        frmRenewals.FDModifyPostRenewalType = FDPostRenewalType;
                        frmRenewals.FDTransType = FDTransType;

                        frmRenewals.UpdateHeld += new EventHandler(OnUpdateHeld);
                        frmRenewals.ShowDialog();
                        FetchFDAccountDetails();
                    }
                    else if (HasPostInterests() == (int)YesNo.Yes)
                    {
                        DataView dvMaster = dsFDRenewal.Tables[0].DefaultView;
                        dvMaster.RowFilter = fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + "=" + FDAccountId;
                        DataTable dtMaster = dvMaster.ToTable();
                        DataView dvRenewal = dsFDRenewal.Tables[1].DefaultView;
                        dvRenewal.RowFilter = fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + "=" + FDAccountId;
                        DataTable dtrenewal = dvRenewal.ToTable();

                        if (!base.IsVoucherLockedForDate(ProjectIdFDAccount, FDAccountCreatedDate, true))
                        {
                            frmFDAccount frmAccount = new frmFDAccount(this.UtilityMember.NumberSet.ToInteger(dtrenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()), FDAccountId, fdTypes, dtMaster, dtrenewal);
                            frmAccount.PostInterestCreatedDate = FDAccountCreatedDate;
                            frmAccount.PostInterestRate = FDAccountInsRate;
                            frmAccount.PostInterestMaturityDate = FDPostMaturityDate;
                            frmAccount.PostRenewalType = FDPostRenewalType;

                            frmAccount.FDAmount = FDPrinicipalAmount;
                            frmAccount.ProjectId = ProjectId;
                            frmAccount.FDInterestRate = InterestRate;
                            frmAccount.UpdateHeld += new EventHandler(OnUpdateHeld);
                            frmAccount.ShowDialog();
                        }
                    }
                    else
                    {
                        //if No post Interest exists,FDopening(modify)/fdInvestment (Modify) form will be shown
                        ShowFDAccountForm();
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                }
            }
        }

        /// <summary>
        /// This is to get the count  of Post Interest Exists for selected FD account
        /// </summary>
        /// <params>Renewal Id is "Unique post Interets Id"</params>
        /// <returns>return the Post Interests count that the particular FD has</returns>
        private int HasPostInterests()
        {
            int haspostInterest = 0;
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    if (dsFDRenewal.Tables.Count > (int)YesNo.Yes)
                    {
                        foreach (DataRow dr in dsFDRenewal.Tables[1].Rows)
                        {
                            if (this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) == FDAccountId)
                            {

                                RenewalId = dr[fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                                fdRenewalDate = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                                haspostInterest++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            return haspostInterest;
        }
        #endregion

        #region Methods
        /// <summary>
        /// This is to set the user rights for Renewal ,Post Interest and Witjdrawal
        /// </summary>

        private void ApplyUserRights()
        {
            if (fdTypes == FDTypes.RN)
            {
                this.enumUserRights.Add(Renewal.RenewFixedDeposit);
                this.enumUserRights.Add(Renewal.ModifyFixedDepostRenewal);
                this.enumUserRights.Add(Renewal.DeleteFixedDepositRenewal);
                this.enumUserRights.Add(Renewal.PrintFixedDepositRenewal);
                this.enumUserRights.Add(Renewal.ViewFixedDepositRenewal);
                // ucToolBarRenewal.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Always;
                this.ApplyUserRights(ucToolBarRenewal, this.enumUserRights, (int)Menus.FixedDepositRenewal);
            }
            else if (fdTypes == FDTypes.POI)
            {
                this.enumUserRights.Add(FDPostInterest.PostInterestFixedDeposit);
                this.enumUserRights.Add(FDPostInterest.PrintFixedDepositPostInterest);

                // ucToolBarRenewal.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Always;
                this.ApplyUserRights(ucToolBarRenewal, this.enumUserRights, (int)Menus.FixedDepositPostInterest);

                ucToolBarRenewal.VisibleMoveTrans= ucToolBarRenewal.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else if (fdTypes == FDTypes.RIN)
            {
                this.enumUserRights.Add(ReInvestment.ReInvestmentFixedDeposit);
                this.enumUserRights.Add(ReInvestment.ModifyFixedDepostReInvestment);
                this.enumUserRights.Add(ReInvestment.DeleteFixedDepositReInvestment);
                this.enumUserRights.Add(ReInvestment.PrintFixedDepositReInvestment);
                //ucToolBarRenewal.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Always;
                this.ApplyUserRights(ucToolBarRenewal, this.enumUserRights, (int)Menus.FixedDepositReInvestment);
                ucToolBarRenewal.VisibleMoveTrans = ucToolBarRenewal.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;

                //this.enumUserRights.Add(Renewal.RenewFixedDeposit);
                //this.enumUserRights.Add(Renewal.ModifyFixedDepostRenewal);
                //this.enumUserRights.Add(Renewal.DeleteFixedDepositRenewal);
                //this.enumUserRights.Add(Renewal.PrintFixedDepositRenewal);
                //this.enumUserRights.Add(Renewal.ViewFixedDepositRenewal);
                //// ucToolBarRenewal.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Always;
                //this.ApplyUserRights(ucToolBarRenewal, this.enumUserRights, (int)Menus.FixedDepositRenewal);
            }
            else
            {
                this.enumUserRights.Add(Withdrewal.WithdrawFixedDeposit);
                this.enumUserRights.Add(Withdrewal.PrintFixedDepositWithdraw);
                this.enumUserRights.Add(Withdrewal.ViewFixedDepositWithdraw);

                this.ApplyUserRights(ucToolBarRenewal, this.enumUserRights, (int)Menus.FDWithdrawal);
                ucToolBarRenewal.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
                // ucToolBarRenewal.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
                ucToolBarRenewal.DisableDeleteButton = false;
                ucToolBarRenewal.VisibleEditButton = ucToolBarRenewal.VisibleMoveTrans = ucToolBarRenewal.VisibleDownloadExcel = ucToolBarRenewal.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fdAccId"></param>
        private void ShowForm(int fdAccId)
        {
            try
            {
                frmFDAccount frmAccont = new frmFDAccount(fdAccId, 0, this.fdTypes);
                //To set the Post Interest date after/Before each renewal.
                frmAccont.PostInterestCreatedDate = FDAccountCreatedDate;
                frmAccont.PostInterestRate = FDAccountInsRate;
                frmAccont.PostInterestMaturityDate = FDPostMaturityDate;
                frmAccont.PostRenewalType = FDPostRenewalType;

                frmAccont.FdProjectName = projectName;
                frmAccont.LedgerName = FDLedgerName;
                frmAccont.FDAccountNumber = FDBankAccountNumber;
                frmAccont.ProjectId = ProjectId;
                frmAccont.BankLedgerId = BankLedgerId;

                //On 16/08/2018, For Opening FDs, when we renewal opening FD bank ledger is not showing even it is renewed once
                //so fix last renewal's Bankledger
                if (BankLedgerId == 0)
                {
                    using (FDAccountSystem fdsystem = new FDAccountSystem())
                    {
                        fdsystem.FDAccountId = fdAccId;
                        ResultArgs resultarg = fdsystem.GetLastRenewalIdByFDAccount();
                        if (resultarg != null && resultarg.Success)
                        {
                            DataTable dtLastRenewalDetails = resultarg.DataSource.Table;
                            if (dtLastRenewalDetails != null && dtLastRenewalDetails.Rows.Count > 0)
                            {
                                frmAccont.BankLedgerId = this.UtilityMember.NumberSet.ToInteger(dtLastRenewalDetails.Rows[0][fdsystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName].ToString());
                            }
                        }
                    }
                }
                //-------------------------------------------------------------------------------------------------------------------

                frmAccont.LedgerId = FDLedgerId;
                frmAccont.FDBankID = FDBankId;
                frmAccont.BankName = FDBankName;
                frmAccont.FDAmount = PrincipalAmount;
                frmAccont.InvestedAmount = InvestedAmount;
                frmAccont.RenewalDate = FDRenewalDate;
                frmAccont.FDActiveStatus = FDActiveStatus;
                frmAccont.FDInterestRate = InterestRate;
                //Route the Closed FD while editing COntra or Receipt entry
                if (FDActiveStatus.Equals(FDWithdrawalStatus.Closed.ToString()))
                {
                    frmAccont.FDVoucherId = GetVoucherId();
                }
                frmAccont.IntrestCalculatedAmount = (fdTypes != FDTypes.WD && fdTypes != FDTypes.POC) ? FDIntrestAmount : FDIntrestAmount;
                frmAccont.ReInvestedAmount = FDReInvestedAmount;
                GetMaturity();
                frmAccont.ExpectedMaturityInterestAmount = FDExpectedIntrestAmount;
                double values = FDPrinicipalAmount;
                frmAccont.MaturityDate = dteMaturityDate.Date.ToShortDateString();
                frmAccont.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmAccont.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// To get the Maturity Amount for the Expected Interest Amount
        /// </summary>
        private void GetMaturity()
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = fdAccountId;
                fdAccountSystem.FDRenewalId = FDRenewalId;
                resultArgs = fdAccountSystem.GetMaturityValue();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    FDExpectedIntrestAmount = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName].ToString()) : 0;
                }
            }
        }

        private int GetVoucherId()
        {
            int VoucherId = 0;
            using (FDRenewalSystem fdRenewalSystem = new FDRenewalSystem())
            {
                fdRenewalSystem.FDAccountId = FDAccountId;
                VoucherId = fdRenewalSystem.GetVoucherId();
            }
            return VoucherId;
        }

        private void ShowFDAccountForm()
        {
            try
            {
                if (gvRenewal.RowCount > 0)
                {
                    if (FDAccountId > 0)
                    {
                        ShowForm(FDAccountId);
                        FetchFDAccountDetails();
                    }
                    else
                    {
                        if (!chkShowFilter.Checked)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_EDIT));
                        }
                    }
                }
                else
                {
                    if (fdTypes == FDTypes.RN)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDRenewal.RENEWAL_VIEW_NO_RECORD_TO_RENEW));
                    }
                    else if (fdTypes == FDTypes.RIN)
                    {
                        this.ShowMessageBox("No record is available to Re-Investment");
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDRenewal.WITHDRAW_VIEW_NO_RECORD));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Show renewal form such as "frmRenewals" if it has more than one renewal.
        ///	It will check for "HasRenewals()" based on the FD_ACCOUNT_ID
        /// </summary>
        /// Steps: In this, First it checks for renewals/PostInterest exists..
        /// If FD account has more than one 
        /// renewals/PostInterest ,
        /// it will route to the "frmRenewals" screen to select the recent 	renewal/PostInterest for edit.
        /// If only one renewal/PostInterest exists it edit the current 	renewal/PostInterest.
        ///Methods Used:HasRenewals(),HasPostInterest(),fetchFDAccountDetails()
        private void ShowRenewalForm()
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                if (this.isEditable)
                {
                    if (HasRenewals() > (int)YesNo.Yes)
                    {
                        frmRenewals frmRenewals = new frmRenewals(FDAccountId, FDLedgerName, projectName, FDBankAccountNumber, dsFDRenewal, fdTypes, "Edit", FDTransType);

                        frmRenewals.FDModifyAccountCreatedDate = FDAccountCreatedDate;
                        frmRenewals.FDModifyAccountInsRate = FDAccountInsRate;
                        frmRenewals.FDModifyPostMaturityDate = FDPostMaturityDate;
                        frmRenewals.FDModifyPostRenewalType = FDPostRenewalType;

                        frmRenewals.UpdateHeld += new EventHandler(OnUpdateHeld);
                        frmRenewals.ShowDialog();
                        FetchFDAccountDetails();
                    }
                    else if (HasRenewals() == (int)YesNo.Yes)
                    {
                        DataView dvMaster = dsFDRenewal.Tables[0].DefaultView;
                        dvMaster.RowFilter = fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + "=" + FDAccountId;
                        DataTable dtMaster = dvMaster.ToTable();
                        DataView dvRenewal = dsFDRenewal.Tables[1].DefaultView;
                        dvRenewal.RowFilter = fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + "=" + FDAccountId;
                        DataTable dtrenewal = dvRenewal.ToTable();

                        //On 08/08/2019, In Edit mode, If selected renewal records is not blong to current finance year, 
                        //Prompt proper message and lock to edit
                        if (dtrenewal != null && dtrenewal.Rows.Count > 0)
                        {
                            DateTime RenewalDate = this.UtilityMember.DateSet.ToDate(dtrenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                            if (!(RenewalDate >= this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false)
                                && RenewalDate <= this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false)))
                            {
                                this.ShowMessageBox("Selected Renewal detail doesn't fall on the Current Financial Year, Change Financial Year and modify Renewal detail");
                            }
                            else
                            {
                                if (!base.IsVoucherLockedForDate(ProjectId, RenewalDate, true))
                                {
                                    frmFDAccount frmAccount = new frmFDAccount(this.UtilityMember.NumberSet.ToInteger(dtrenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()), FDAccountId, fdTypes, dtMaster, dtrenewal);
                                    frmAccount.FDAmount = FDPrinicipalAmount;
                                    frmAccount.ProjectId = ProjectId;
                                    frmAccount.FDInterestRate = InterestRate;
                                    frmAccount.UpdateHeld += new EventHandler(OnUpdateHeld);
                                    frmAccount.ShowDialog();
                                }
                            }
                        }


                    }
                    else
                    {
                        //if No renewals exsits FD Opening(edit)/FD Invsetment Edit will be shown
                        ShowFDAccountForm();
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                }
            }
        }

        /// <summary>
        /// This is to Edit the Renewal Form
        /// </summary>
        private void showFDReInvestmentEditForm()
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                if (this.isEditable)
                {
                    if (HasRenewals() > (int)YesNo.Yes)
                    {
                        frmRenewals frmRenewals = new frmRenewals(FDAccountId, FDLedgerName, projectName, FDBankAccountNumber, dsFDRenewal, fdTypes, "Edit", FDTransType);
                        frmRenewals.FDModifyAccountCreatedDate = FDAccountCreatedDate;
                        frmRenewals.FDModifyAccountInsRate = FDAccountInsRate;
                        frmRenewals.FDModifyPostMaturityDate = FDPostMaturityDate;
                        frmRenewals.FDModifyPostRenewalType = FDPostRenewalType;

                        frmRenewals.UpdateHeld += new EventHandler(OnUpdateHeld);
                        frmRenewals.ShowDialog();
                        FetchFDAccountDetails();
                    }
                    else if (HasRenewals() == (int)YesNo.Yes)
                    {
                        DataView dvMaster = dsFDRenewal.Tables[0].DefaultView;
                        dvMaster.RowFilter = fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + "=" + FDAccountId;
                        DataTable dtMaster = dvMaster.ToTable();
                        DataView dvRenewal = dsFDRenewal.Tables[1].DefaultView;
                        dvRenewal.RowFilter = fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + "=" + FDAccountId;
                        DataTable dtrenewal = dvRenewal.ToTable();
                        if (dtrenewal.Rows.Count > 0)
                        {
                            DateTime RenewalDate = this.UtilityMember.DateSet.ToDate(dtrenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                            if (!base.IsVoucherLockedForDate(ProjectIdFDAccount, RenewalDate, true))
                            {
                                frmFDAccount frmAccount = new frmFDAccount(this.UtilityMember.NumberSet.ToInteger(dtrenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()), FDAccountId, fdTypes, dtMaster, dtrenewal);
                                frmAccount.FDAmount = FDPrinicipalAmount;
                                frmAccount.ProjectId = ProjectId;
                                frmAccount.FDInterestRate = InterestRate;
                                if (fdTypes == FDTypes.RIN)
                                    frmAccount.PostInterestMaturityDate = FDPostMaturityDate;
                                frmAccount.UpdateHeld += new EventHandler(OnUpdateHeld);
                                frmAccount.ShowDialog();
                            }
                        }
                        else
                        {
                            this.ShowMessageBox("Fixed Deposit Re-Investment is not found");
                        }
                    }
                    else
                    {
                        //if No renewals exsits FD Opening(edit)/FD Invsetment Edit will be shown
                        ShowFDAccountForm();
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                }
            }
        }

        /// <summary>
        /// This is to check whaeather particular FD Account has FD Renewals based on the FD 	Account ID.
        /// </summary>
        /// <returns></returns>
        /// <params>FD_ACCOUNT_ID</params>
        private int HasRenewals()
        {
            int hasRenewal = 0;
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    if (dsFDRenewal.Tables.Count > (int)YesNo.Yes)
                    {
                        foreach (DataRow dr in dsFDRenewal.Tables[1].Rows)
                        {
                            if (this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) == FDAccountId)
                            {
                                RenewalId = dr[fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                                fdRenewalDate = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                                hasRenewal++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            return hasRenewal;
        }

        /// <summary>
        /// This is to check whaeather particular FD Account has FD Renewals based on the FD 	Account ID.
        /// </summary>
        /// <returns></returns>
        /// <params>FD_ACCOUNT_ID</params>
        private int HasReInvestment()
        {
            int hasReinvestment = 0;
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    if (dsFDRenewal.Tables.Count > (int)YesNo.Yes)
                    {
                        foreach (DataRow dr in dsFDRenewal.Tables[1].Rows)
                        {
                            if (this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) == FDAccountId)
                            {
                                RenewalId = dr[fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                                fdRenewalDate = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                                hasReinvestment++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            return hasReinvestment;
        }

        /// <summary>
        /// This is to Load the FD Account Details as Master-Detail Record which are "Active".
        /// Steps: Fetch the Renewals As "Renewasl History" based on FD_ACCOUNT_ID
        /// Fetch "Post Interest History" based on the FD_account_id
        /// </summary>
        /// <params>CLOSING_STATUS,FD_ACCOUNT_ID</params>
        private void FetchFDAccountDetails()
        {
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    fdAccountSystem.FDTransType = "OP','IN";
                    fdAccountSystem.FDType = (fdTypes==FDTypes.POC? FDTypes.WD.ToString() :  fdTypes.ToString());
                    dsFDRenewal = fdAccountSystem.LoadFDRenewalDetails();

                    if (dsFDRenewal.Tables.Count > (int)YesNo.Yes)
                    {
                        dtFDMaster = dsFDRenewal.Tables[0];
                        dtFDRenewal = dsFDRenewal.Tables[1];
                    }
                    if (dsFDRenewal.Tables.Count != (int)YesNo.No)
                    {
                        if (fdTypes.Equals(FDTypes.WD) || fdTypes.Equals(FDTypes.POC))
                        {
                            //dsRenewal = dsFDRenewal;
                            //DataView dvWithdraw = dsFDRenewal.Tables[FDRenewalCaption.Master.ToString()].DefaultView;
                            //dvWithdraw.RowFilter = fdAccountSystem.AppSchema.FDRenewal.CLOSING_STATUSColumn.ColumnName + "<>'" + FDWithdrawalStatus.Closed.ToString() + "'";
                            //gcRenewalView.DataSource = dvWithdraw.ToTable();
                            //gcRenewalView.RefreshDataSource();
                            //dvWithdraw.RowFilter = "";

                            //changed by sugan to maintian withdrawal history
                            gcRenewalView.DataSource = dsRenewal = dsFDRenewal;
                            gcRenewalView.DataMember = FDRenewalCaption.Master.ToString();
                            gcRenewalView.RefreshDataSource();
                        }
                        else
                        {
                            gcRenewalView.DataSource = dsRenewal = dsFDRenewal;
                            gcRenewalView.DataMember = FDRenewalCaption.Master.ToString();
                            gcRenewalView.RefreshDataSource();
                        }
                    }
                    else
                    {
                        ucToolBarRenewal.DisableEditButton = ucToolBarRenewal.DisableDeleteButton = false;
                        gcRenewalView.DataSource = null;
                        gcRenewalView.RefreshDataSource();
                    }
                }
                ucFDHistory1.FDAccountId = FDAccountId;
                ucFDHistory1.ShowPanelCaptionHeader = false;

                colFDScheme.Visible = false;
                if (this.UIAppSetting.EnableFlexiFD == "1")
                {
                    colFDScheme.Visible = true;
                    colFDScheme.VisibleIndex = colFDInvestmentType.VisibleIndex + 1;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void DeleteFDAccount()
        {
            if (gvRenewal.RowCount != (int)YesNo.No)
            {
                if (FDAccountId != (int)YesNo.No)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteFDInvestmentDetails();
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

        /// <summary>
        /// Rremove all the Related FD Vouchers,Renewals and Post Interests
        /// </summary>
        private void DeleteFDInvestmentDetails()
        {
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    fdAccountSystem.ProjectId = ProjectId;
                    fdAccountSystem.LedgerId = FDLedgerId;
                    fdAccountSystem.FDAccountId = FDAccountId;
                    fdAccountSystem.FDTransMode = TransactionMode.DR.ToString();
                    fdAccountSystem.FDVoucherId = FDVoucherId;
                    fdAccountSystem.FDTransType = FDTransType;
                    fdAccountSystem.FDRenewalType = fdTypes.ToString();
                    fdAccountSystem.FdAmount = FDPrinicipalAmount;
                    if (FDTransType == FDTypes.OP.ToString())
                    {
                        fdAccountSystem.FDOPInvestmentDate = this.AppSetting.YearFrom;
                    }
                    resultArgs = fdAccountSystem.RemoveFDAccountDetails();
                    if (resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                        FetchFDAccountDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadDefaults()
        {
            if (fdTypes == FDTypes.RN)
            {
                ucToolBarRenewal.ChangeAddCaption = FDRenewalCaption.Renew.ToString();
                ucToolBarRenewal.ChangeCaption = FDRenewalCaption.Modify.ToString();
                lblStatus.Visibility = LayoutVisibility.Never;
            }
            else if (fdTypes == FDTypes.POI)
            {
                ucToolBarRenewal.ChangeAddCaption = UtilityMember.EnumSet.GetDescriptionFromEnumValue(FDPostInterestCaption.PostInterest);
                ucToolBarRenewal.ChangeCaption = FDPostInterestCaption.Modify.ToString();
                lblStatus.Visibility = LayoutVisibility.Never;
            }
            else if (fdTypes == FDTypes.RIN)
            {
                ucToolBarRenewal.ChangeAddCaption = "Re-Investment";
                ucToolBarRenewal.ChangeCaption = "Modify";
                // ucToolBarRenewal.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
                lblStatus.Visibility = LayoutVisibility.Never;
            }
            else if (fdTypes == FDTypes.POC)
            {
                ucToolBarRenewal.ChangeAddCaption = FDRenewalCaption.Charge.ToString();
                ucToolBarRenewal.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
                ucToolBarRenewal.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
                lblStatus.Visibility = LayoutVisibility.Always;
            }
            else
            {
                ucToolBarRenewal.ChangeAddCaption = FDRenewalCaption.Withdraw.ToString();
                ucToolBarRenewal.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
                ucToolBarRenewal.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
                lblStatus.Visibility = LayoutVisibility.Always;

                if (fdTypes == FDTypes.WD)
                {
                    ucToolBarRenewal.ChangeCaption = FDRenewalCaption.Modify.ToString();

                    ucToolBarRenewal.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
                    ucToolBarRenewal.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }
            this.Text = fdTypes == FDTypes.RN ? this.GetMessage(MessageCatalog.Master.FDRenewal.FD_RENEWAL) : 
                                   fdTypes == FDTypes.POI ? this.GetMessage(MessageCatalog.Master.FDPostInterest.FD_POSTINTEREST) :
                                   fdTypes == FDTypes.RIN ? "Re-Investment" : fdTypes == FDTypes.POC ? "Investment Post Charge" : this.GetMessage(MessageCatalog.Master.FDLedger.FD_WITHDRAWAL);
        }

        /// <summary>
        /// This is to filter the FD Accounts based on the Status and maturity date.
        /// </summary>
        /// <params>:  Status,Maturity date as on</params>
        private void FilterFDRenewal()
        {
            string FDAccountId = string.Empty;
            DataSet dsFDRenewal = new DataSet();
            try
            {
                string FDStatus = cboSetActive.SelectedIndex.Equals((int)YesNo.Yes) ? FDWithdrawalStatus.Closed.ToString() : FDWithdrawalStatus.Active.ToString();
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    if (dsRenewal != null)
                    {
                        DataTable dtMaturedFd = dsRenewal.Tables[0];
                        if (dtMaturedFd != null && dtMaturedFd.Rows.Count != (int)YesNo.No)
                        {
                            DataView dvMaturedFd = dtMaturedFd.DefaultView;
                            if (fdTypes.Equals(FDTypes.WD) || fdTypes.Equals(FDTypes.POC))
                            {
                                dvMaturedFd.RowFilter = fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName + " <='" + dteMaturityAsOn.DateTime.ToString("yyyy-MM-dd") + "' AND " + fdAccountSystem.AppSchema.FDRenewal.CLOSING_STATUSColumn.ColumnName + "='" + FDStatus + "'";
                            }
                            else
                            {
                                dvMaturedFd.RowFilter = fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName + "<='" + dteMaturityAsOn.DateTime.ToString("yyyy-MM-dd") + "'";
                            }

                            if (dvMaturedFd.Count != (int)YesNo.No)
                            {
                                dvMaturedFd.ToTable().TableName = FDRenewalCaption.Master.ToString();
                                dsFDRenewal.Tables.Add(dvMaturedFd.ToTable());
                                foreach (DataRow dr in dvMaturedFd.ToTable().Rows)
                                {
                                    FDAccountId += dr[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != DBNull.Value ? dr[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] + "," : string.Empty;
                                }
                                FDAccountId = FDAccountId.TrimEnd(',');
                                if (dsRenewal.Tables.Count > (int)YesNo.Yes)
                                {
                                    DataView dvRenewal = dsRenewal.Tables[1].DefaultView;
                                    dvRenewal.RowFilter = fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName + " IN( " + FDAccountId + ")";
                                    dvRenewal.ToTable().TableName = "Renewal History";
                                    if (dvRenewal.Count != 0 && fdTypes.Equals(FDTypes.RN))
                                    {
                                        dsFDRenewal.Tables.Add(dvRenewal.ToTable());
                                        dsFDRenewal.Relations.Add(dsFDRenewal.Tables[1].TableName, dsFDRenewal.Tables[0].Columns[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName], dsFDRenewal.Tables[1].Columns[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName]);
                                        gcRenewalView.DataSource = dsFDRenewal;
                                        gcRenewalView.DataMember = FDRenewalCaption.Master.ToString();
                                        gcRenewalView.RefreshDataSource();
                                    }
                                    else
                                    {
                                        gcRenewalView.DataSource = dvMaturedFd.ToTable();
                                        gcRenewalView.RefreshDataSource();
                                    }
                                    dvRenewal.RowFilter = "";
                                }
                                else
                                {
                                    gcRenewalView.DataSource = dvMaturedFd.ToTable();
                                    gcRenewalView.RefreshDataSource();
                                }
                            }
                            else
                            {
                                gcRenewalView.DataSource = dvMaturedFd.ToTable();
                                gcRenewalView.RefreshDataSource();
                            }
                            dvMaturedFd.RowFilter = "";
                        }
                    }

                    ucFDHistory1.FDAccountId = this.FDAccountId;

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void SetAlignment()
        {
            if (!this.AppSetting.LanguageId.Equals("en-US"))
            {
                if (this.AppSetting.LanguageId.Equals("pt-PT"))
                {
                    emptySpaceItem2.Width = 550;
                    layoutControlItem1.Width = 50;
                    lblDateofMaturity.Width = 290;
                }
                else
                {
                    emptySpaceItem2.Width = 550;
                    layoutControlItem1.Width = 50;
                    lblDateofMaturity.Width = 290;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                // dteMaturityAsOn.Focus();
                if (!chkSelectAll.Checked)
                {
                    frmDatePicker datePicker = new frmDatePicker(dteMaturityAsOn.DateTime, DatePickerType.VoucherDate);
                    datePicker.ShowDialog();
                    dteMaturityAsOn.DateTime = AppSetting.VoucherDate;
                }
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        /// <summary>
        /// To check wheather FD Account has FD Renewals or Not  and also set Last Post Interest date+ one day as next "renewal On" date.
        /// </summary>
        /// <param name="FDAccountId">if fdAccountId>0,FD Renewal will be shown.There is no chance for having FDAccount is zero ,
        /// because in renewal view all the existing FDs only be shown</param>
        private void HasFDRenewal(int FDAccountId)
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = FDAccountId;
                if (fdTypes==FDTypes.RN && fdAccountSystem.HasFDRenewal() > 0)
                {
                    //fetch the recent renewal Id to set the Next renewal on date
                    DataTable dtLastRenewal = fdAccountSystem.GetLastRenewalIdByFDAccount().DataSource.Table;

                    if (dtLastRenewal != null && dtLastRenewal.Rows.Count > 0)
                    {
                        //fetch the recent post interest Id to set the next renewal on date
                        if (fdAccountSystem.HasFDPostInterests() > 0)
                        {
                            DataTable dtLastPostInterest = fdAccountSystem.GetPostInterestIdByFDAccount().DataSource.Table;
                            if (dtLastPostInterest != null && dtLastPostInterest.Rows.Count > 0)
                            {
                                FDAccountCreatedDate = UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0]["MATURITY_DATE"].ToString(), false);
                                // FDAccountInsRate = UtilityMember.NumberSet.ToDecimal(dtLastPostInterest.Rows[0]["INTEREST_RATE"].ToString());
                            }
                        }
                        else
                        {
                            FDAccountCreatedDate = UtilityMember.DateSet.ToDate(dtLastRenewal.Rows[0]["RENEWAL_DATE"].ToString(), false);
                            // FDAccountInsRate = UtilityMember.NumberSet.ToDecimal(dtLastRenewal.Rows[0]["INTEREST_RATE"].ToString());
                        }
                    }
                    FDAccountInsRate = UtilityMember.NumberSet.ToDecimal(dtLastRenewal.Rows[0]["INTEREST_RATE"].ToString());
                    FDPostMaturityDate = gvRenewal.GetFocusedRowCellValue(colMaturityDate) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colMaturityDate).ToString(), false) : DateTime.Now;
                    FDPostInterestType = UtilityMember.NumberSet.ToInteger(dtLastRenewal.Rows[0]["INTEREST_TYPE"].ToString()); // cbpinsType---Simple/Compound
                    FDPostRenewalType = dtLastRenewal.Rows[0]["RENEWAL_TYPE"].ToString();//cbointerestmode-----IR/ACI

                    if (fdTypes != FDTypes.RIN)
                    {
                        ucToolBarRenewal.DisableEditButton = true;
                        ucToolBarRenewal.DisableDeleteButton = true;
                    }
                }
                else
                {
                    //If FD account has only Post Interests
                    if (fdTypes == FDTypes.POI && fdAccountSystem.HasFDPostInterests() > 0)
                    {
                        /*DataTable dtFDAccount = fdAccountSystem.FetchFDAccountDetailsByFDAccountId().DataSource.Table;
                        if (dtFDAccount != null && dtFDAccount.Rows.Count > 0)
                        {
                            FDPostMaturityDate = UtilityMember.DateSet.ToDate(dtFDAccount.Rows[0]["MATURED_ON"].ToString(), false);
                        }
                        else
                        {
                            FDPostMaturityDate = gvRenewal.GetFocusedRowCellValue(colMaturityDate) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colMaturityDate).ToString(), false) : DateTime.Now;
                        }*/
                        FDPostMaturityDate = gvRenewal.GetFocusedRowCellValue(colMaturityDate) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colMaturityDate).ToString(), false) : DateTime.Now;

                        DataTable dtLastPostInterest = fdAccountSystem.GetPostInterestIdByFDAccount().DataSource.Table;
                        if (dtLastPostInterest != null && dtLastPostInterest.Rows.Count > 0)
                        {
                            //On 12/01/2024, to have proper renewal date for POST Interest
                            //FDAccountCreatedDate = UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0]["MATURITY_DATE"].ToString(), false);
                            FDAccountCreatedDate = UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0]["RENEWAL_DATE"].ToString(), false);
                        }

                        ucToolBarRenewal.DisableEditButton = true;
                        ucToolBarRenewal.DisableDeleteButton = true;
                    }
                    else if (fdTypes == FDTypes.WD && fdAccountSystem.HasFDPartialwithdrawal() > 0)
                    {
                        ucToolBarRenewal.DisableEditButton = true;
                        ucToolBarRenewal.DisableDeleteButton = true;
                    }
                    else
                    {
                        if (gvRenewal.GetFocusedRowCellValue(colTransType) != null)
                        {
                            //if (gvRenewal.GetFocusedRowCellValue(colTransType).ToString() == FDTypes.OP.ToString())
                            //{
                            //    FDAccountCreatedDate = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom,false);
                            //}
                            //else
                            //{
                            FDAccountCreatedDate = gvRenewal.GetFocusedRowCellValue(colCreatedOn) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colCreatedOn).ToString(), false) : DateTime.Now;
                            // }
                        }
                        FDPostMaturityDate = gvRenewal.GetFocusedRowCellValue(colMaturityDate) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colMaturityDate).ToString(), false) : DateTime.Now;
                        ucToolBarRenewal.DisableEditButton = false;
                        ucToolBarRenewal.DisableDeleteButton = false;
                    }

                    FDAccountInsRate = gvRenewal.GetFocusedRowCellValue(colInsRate) != null ? this.UtilityMember.NumberSet.ToDecimal(gvRenewal.GetFocusedRowCellValue(colInsRate).ToString()) : 0;
                    //FDPostMaturityDate = gvRenewal.GetFocusedRowCellValue(colMaturityDate) != null ? this.UtilityMember.DateSet.ToDate(gvRenewal.GetFocusedRowCellValue(colMaturityDate).ToString(), false) : DateTime.Now;
                    FDPostInterestType = gvRenewal.GetFocusedRowCellValue(colInsType) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colInsType).ToString()) : 0;
                    FDPostRenewalType = gvRenewal.GetFocusedRowCellValue(colRenewalType) != null ? gvRenewal.GetFocusedRowCellValue(colRenewalType).ToString() : string.Empty;
                }


                // To visible the modify and Delete option 22.11.2018
                if (fdTypes == FDTypes.RIN)
                {
                    if (fdAccountSystem.HasFDReInvestment() > 0)
                    {
                        ucToolBarRenewal.DisableEditButton = true;
                        ucToolBarRenewal.DisableDeleteButton = true;
                    }
                    else
                    {
                        ucToolBarRenewal.DisableEditButton = false;
                        ucToolBarRenewal.DisableDeleteButton = false;
                    }
                }
                ucToolBarRenewal.DisablePostInterest = true;
            }
        }

        /// <summary>
        /// This is to delete the FD Withdrawal based on the FD_ACCOUNT_ID and 	Voucher_id if it does not contain any renewals.
        /// </summary>
        /// steps:	In this, First it checks for renewals/PostInterest exists..
        /// If FD account has more than one renewals/PostInterest ,it will route to the "frmRenewals" screen to select the recent 
        /// renewal/PostInterest for delete. If only one renewal/PostInterest exists it deletes the current renewal/PostInterest.
        /// If no renewals Exist, it Checks for the "Withdrawal Exists" and shows the Alert message as "Cannot be Deleted".
        /// <params>FD_VOUCHER_ID and FD_ACCOUNT_ID</params>
        private void DeleteFDWithdrawal()
        {
            string FDVoucherId = string.Empty;
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    if (FDAccountId > 0)
                    {
                        fdAccountSystem.FDAccountId = FDAccountId;
                        resultArgs = fdAccountSystem.FetchVoucherByFDAccount();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_CONFIRMATION_DELETE), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                                    {
                                        FDVoucherId += dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString() + "," : string.Empty;
                                        FDVoucherId += dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString() : string.Empty;
                                        // FDVoucherId = FDVoucherId.TrimEnd(',');
                                        string[] VoucherId = FDVoucherId.Split(',');
                                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                                        {
                                            foreach (string VId in VoucherId)
                                            {
                                                voucherTransaction.VoucherId = !string.IsNullOrEmpty(VId) ? this.UtilityMember.NumberSet.ToInteger(VId) : 0;
                                                if (voucherTransaction.VoucherId > 0)
                                                {
                                                    resultArgs = voucherTransaction.DeleteVoucherTrans();
                                                }
                                            }
                                            if (resultArgs.Success)
                                            {
                                                fdAccountSystem.FDVoucherId = voucherTransaction.VoucherId;
                                                resultArgs = fdAccountSystem.DeleteFDRenewalsByVoucherId();
                                                if (resultArgs.Success)
                                                {
                                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NOSELECTION_FOR_DELETE));
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }


        /// <summary>
        /// On 11/1//2021, to call modified renewlas 
        /// </summary>
        private void InvokeModifiedDetails()
        {
            try
            {
                if (this.isEditable)
                {
                    Int32 RowIndex = gvRenewal.FocusedRowHandle; 
                    if (fdTypes != FDTypes.POI && fdTypes != FDTypes.RIN)
                    {
                        ShowRenewalForm();
                    }
                    else if (fdTypes == FDTypes.POI)
                    {
                        ShowFDPostInterestEditForm();
                    }
                    else if (fdTypes == FDTypes.RIN)
                    {
                        showFDReInvestmentEditForm();
                    }
                    gvRenewal.FocusedRowHandle = RowIndex;
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_USER_RIGHTS));
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }
        #endregion

        private void gvFDRenewal_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //on 04/10/2022, Change Renewl Type text
            if (e.Column.FieldName == colRenewalType.FieldName)
            {
                if (e.Value!=null )
                {
                    GridView gv = sender as GridView;
                    DataRow dr = gv.GetDataRow(gv.GetRowHandle(e.ListSourceRowIndex)) as DataRow;
                    string displaytxt = e.Value.ToString().ToUpper();
                    string transmode = TransactionMode.DR.ToString().ToUpper();
                    if (dr != null)
                    {
                        transmode = dr[colPOIFDTransMode.FieldName].ToString().Trim().ToUpper();
                    }

                    if (displaytxt == Bosco.Utility.FDRenewalTypes.IRI.ToString().ToUpper())
                    {
                        displaytxt = "Interest Received";
                    }
                    else if (displaytxt == Bosco.Utility.FDRenewalTypes.ACI.ToString().ToUpper())
                    {
                        displaytxt = (transmode == TransactionMode.CR.ToString().ToUpper() ? "Fixed Deposit Adjustment" : "Interest Accumulated");
                    }
                    
                    //e.DisplayText = (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.IRI.ToString().ToUpper() ?
                    //    "Interest Received" : (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.ACI.ToString().ToUpper() ? "Interest Accumulated" : string.Empty));
                    e.DisplayText = displaytxt;
                }
            }
            else if (e.Column.FieldName == colIntrestAmount.FieldName)
            {
                if (sender != null && e.Value != null)
                {
                    GridView gv = sender as GridView;
                    DataRow dr = gv.GetDataRow(gv.GetRowHandle(e.ListSourceRowIndex)) as DataRow;

                    if (dr != null)
                    {
                        string transmode = dr[colPOIFDTransMode.FieldName].ToString().Trim().ToUpper();
                        if (transmode == TransactionMode.CR.ToString().ToUpper())
                        {
                            double amt = UtilityMember.NumberSet.ToDouble(e.Value.ToString()) * (transmode == TransactionMode.CR.ToString().ToUpper() ? -1 : 1);
                            e.DisplayText = UtilityMember.NumberSet.ToNumber(amt);
                        }
                    }
                }
            }
        }

        private void gvFDPostInterest_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //on 04/10/2022, Change Renewl Type text
            if (e.Column.FieldName == colPOIRenewalType.FieldName)
            {
                if (e.Value != null)
                {
                    GridView gv = sender as GridView;
                    DataRow dr = gv.GetDataRow(gv.GetRowHandle(e.ListSourceRowIndex)) as DataRow;
                    string displaytxt = e.Value.ToString().ToUpper();
                    string transmode = TransactionMode.DR.ToString().ToUpper();
                    if (dr != null)
                    {
                        transmode = dr[colPOIFDTransMode.FieldName].ToString().Trim().ToUpper();
                    }

                    if (displaytxt == Bosco.Utility.FDRenewalTypes.IRI.ToString().ToUpper())
                    {
                        displaytxt = "Interest Received";
                    }
                    else if (displaytxt == Bosco.Utility.FDRenewalTypes.ACI.ToString().ToUpper())
                    {
                        displaytxt = (transmode == TransactionMode.CR.ToString().ToUpper() ? "Fixed Deposit Adjustment" : "Interest Accumulated");
                    }

                    //e.DisplayText = (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.IRI.ToString().ToUpper() ?
                    //    "Interest Received" : (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.ACI.ToString().ToUpper() ? "Interest Accumulated" : string.Empty));
                    e.DisplayText = displaytxt;

                    //e.DisplayText = (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.IRI.ToString().ToUpper() ?
                    //    "Interest Received" : (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.ACI.ToString().ToUpper() ? "Interest Accumulated" : string.Empty));
                }
            }
            else if (e.Column.FieldName == colPOIIntrestAmount.FieldName)
            {
                if (sender !=null && e.Value != null)
                {
                    GridView gv = sender as GridView;
                    DataRow dr = gv.GetDataRow(gv.GetRowHandle(e.ListSourceRowIndex)) as DataRow;
                    
                    if (dr != null)
                    {
                        string transmode = dr[colPOIFDTransMode.FieldName].ToString().Trim().ToUpper();
                        if (transmode == TransactionMode.CR.ToString().ToUpper())
                        {
                            double amt = UtilityMember.NumberSet.ToDouble(e.Value.ToString()) * (transmode == TransactionMode.CR.ToString().ToUpper() ? -1 : 1);
                            e.DisplayText = UtilityMember.NumberSet.ToNumber(amt);
                        }
                    }
                }
            }
        }

        private void gvWithDrawalHistory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int FDAccountID = gvRenewal.GetFocusedRowCellValue(colFDAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewal.GetFocusedRowCellValue(colFDAccountId).ToString()) : 0;
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    HasFDRenewal(FDAccountID);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void dockManager1_ActivePanelChanged(object sender, DevExpress.XtraBars.Docking.ActivePanelChangedEventArgs e)
        {
            if (dockManager1.ActivePanel != null)
            {
                dockFDHPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
        }

        private void gvWithDrawalHistory_DoubleClick(object sender, EventArgs e)
        {
            return; //Temp on 11/11/2021
            try
            {
                lockParentDblClick = true;
                if (fdTypes != FDTypes.WD && fdTypes != FDTypes.POC)
                {
                    if (this.isEditable)
                    {
                        //As on 11/11/2021, to have renewals list order
                        InvokeModifiedDetails();
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
                        
                        InvokeModifiedDetails();
                    }
                }

                //On 09/10/2017, When double clik on Child gridivew, its getting fired in parent gridview too
                //to lock this issue, check gvRenewal.FocusedRowHandle 
                try
                {
                    BeginInvoke(new MethodInvoker(delegate { lockParentDblClick = false; }));
                }
                catch (Exception err)
                {
                    string errmsg = err.Message;
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }

        private void ucFDHistory1_Load(object sender, EventArgs e)
        {

        }

    }
}