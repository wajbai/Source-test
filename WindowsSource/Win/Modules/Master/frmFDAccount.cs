//This is to perform following Activities
//1.FDOpening
//2.FDInvestment
//3.FDRenewals
//4.FDPostInterests
//5.FDWithdrawals
//Based on the Interest Mode (simple /Compound) Interest will be calculated
//When there is withdrawal, Interest will be calculated in "Simple" interest mode.
//***********************************************************************************************
//Simple Interest=Principal amount*(No.of.days/365)*(Int.rate/100)---example=Prinipla amount=1000,date from=01/03/2015,date to---31/03/2015,Int.rate=10
//Sim.Int.Calculation=1000*(30/365)*(10/100)=8.22

//Compound Interest=principal amount(1+r/n)^nt--example =prinicpal amount=1000,datefrom=01/03/2016,date to-31/03/2016
//Int.rate=10/100=0.1,No of.days=30,n=(30(no.of.days)/30)/3=0.333,t=1/12=0.083,  
//Compound.Interest=1000(1+0.1/1)^(0.333*0.083)=1009.51,    Interst amount=1009.51-1000=9.51
//***********************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using Bosco.Utility;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors;
using Bosco.Model.Business;
using DevExpress.XtraLayout;
using System.Resources;
using AcMEDSync.Model;
using Bosco.Model.UIModel.Master;
using ACPP.Modules.Transaction;
using ACPP.Modules.UIControls;
using System.Text.RegularExpressions;


namespace ACPP.Modules.Master
{
    public partial class frmFDAccount : frmFinanceBaseAdd
    {
        #region Event Handlers
        public event EventHandler UpdateHeld;
        #endregion

        #region Decelaration
        ResultArgs resultArgs = null;
        private int fdAccountId = 0;
        private DataSet dsCostCentre = new DataSet();
        double CurrentLedgerBalance = 0;
        FDTypes fdTypes;
        double amount = 0;
        double interestRate = 0;
        double IntrestAmount = 0;
        private DataSet dsRenewal = new DataSet();
        public int FDVoucherId = 0;
        string TransMode = string.Empty;
        string FDType = string.Empty;
        double NoOfDays = 0;

        //On 24/07/2023, Voucher Authorization details
        public int AuthorizedStatus { get; set; }

        //06/11/2024, To validate FD value chagnes with its history -----------------
        DateTime PrevFDDate;
        double PrevAmount = -1;
        double PrevInterestAmount = -1;
        double PrevPenaltyAmount = -1;
        Int32 PrevInterestMode = -1;
        //---------------------------------------------------------------------------

        /// <summary>
        /// On 24/07/2023, based on the setting, alert and get the confirmation
        /// </summary>
        private int ConfirmVoucherAuthorization
        {
            get
            {
                int rnt = AuthorizedStatus;
                if (VoucherId == 0 && AppSetting.ConfirmAuthorizationVoucherEntry == 1 && fdTypes != FDTypes.OP)
                {
                    string msg = "Is this Voucher Authorized ?";
                    if (this.ShowConfirmationMessage(msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        rnt = (int)Status.Active;
                    }
                }

                return rnt;
            }
        }

        private Int32 BaseFDInvestmentType { get; set; }

        Int32 entryfdInvestmentType = (Int32)FDInvestmentType.FD;
        private Int32 EntryFDInvestmentType
        {
            get
            {
                Int32 fdinvestmenttype = (Int32)FDInvestmentType.FD;

                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    if (fdTypes == FDTypes.IN)
                    {
                        if (glkpFDLedgerDetails.GetSelectedDataRow() != null)
                        {
                            DataRowView drv = glkpFDLedgerDetails.GetSelectedDataRow() as DataRowView;
                            if (drv != null)
                            {
                                fdinvestmenttype = UtilityMember.NumberSet.ToInteger(drv[ledgersystem.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());
                            }
                        }
                    }
                    else if (fdTypes == FDTypes.OP)
                    {
                        if (glkpLedgers.GetSelectedDataRow() != null)
                        {
                            DataRowView drv = glkpLedgers.GetSelectedDataRow() as DataRowView;
                            if (drv != null)
                            {
                                fdinvestmenttype = UtilityMember.NumberSet.ToInteger(drv[ledgersystem.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());
                            }
                        }
                    }
                }
                return fdinvestmenttype;
            }
            set
            {
                entryfdInvestmentType = value;
            }
        }

        /// <summary>
        /// Onm 04/09/2024, To check currency based voucher details
        /// </summary>
        private bool IsCurrencyEnabledVoucher
        {
            get
            {
                Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                double currencyamt = UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text);
                double exchagnerate = UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                double liveexchagnerate = UtilityMember.NumberSet.ToDouble(lblLiveExchangeRate.Text);
                double actalamount = UtilityMember.NumberSet.ToDouble(txtActualAmount.Text);

                return (currencycountry > 0 && currencyamt > 0 && exchagnerate > 0 && actalamount > 0 && liveexchagnerate > 0);
            }
        }

        #endregion

        #region Constructor
        public frmFDAccount()
        {
            InitializeComponent();
        }
        /// <summary>
        /// No use of this Constructor
        /// </summary>
        /// <param name="FDVoucherId"></param>
        public frmFDAccount(int FDVoucherId, string type)
            : this()
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                this.FDVoucherId = FDVoucherId;
                fdAccountSystem.VoucherId = FDVoucherId;
                resultArgs = fdAccountSystem.FetchRenewalByVoucherId();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        fdAccountId = dr[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                        ProjectId = dr[fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                        FDType = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                        if (!this.LoginUser.IsFullRightsReservedUser)
                        {
                            if (CommonMethod.ApplyUserRights((int)Renewal.ModifyFixedDepostRenewal) != 0 && (FDType == FDRenewalTypes.IRI.ToString() || FDRenewalTypes.ACI.ToString() == FDType))
                            {
                                this.fdTypes = FDType == FDRenewalTypes.IRI.ToString() ? FDTypes.RN : (FDType == FDRenewalTypes.ACI.ToString()) ? FDTypes.RN : FDTypes.IN;
                            }
                            else if (CommonMethod.ApplyUserRights((int)Withdrewal.WithdrawFixedDeposit) != 0 && FDType == FDRenewalTypes.WDI.ToString())
                            {
                                this.fdTypes = FDTypes.WD;
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            //this.fdTypes = FDType = (FDTypes)UtilityMember.EnumSet.GetEnumItemType(typeof(FDTypes), type);
                            this.fdTypes = FDType == FDRenewalTypes.IRI.ToString() ? FDTypes.RN : (FDType == FDRenewalTypes.ACI.ToString()) ? FDTypes.RN : (FDType == FDRenewalTypes.WDI.ToString()) ? FDTypes.WD : (FDType == FDRenewalTypes.PWD.ToString()) ? FDTypes.PWD : (FDType == FDRenewalTypes.RIN.ToString()) ? FDTypes.RIN : FDTypes.IN;
                        }
                    }
                }
                else
                {
                    fdAccountSystem.FDType = type;
                    resultArgs = fdAccountSystem.FetchFDAccountByVoucherId();
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        fdAccountId = dr[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                        ProjectId = dr[fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                        FDType = dr[fdAccountSystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString() : string.Empty;
                        if (!this.LoginUser.IsFullRightsReservedUser)
                        {
                            if (CommonMethod.ApplyUserRights((int)FDInvestment.EditFixedInvestment) != 0)
                            {
                                this.fdTypes = FDType == FDTypes.IN.ToString() ? FDTypes.IN : FDTypes.OP;
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            this.fdTypes = FDType == FDTypes.IN.ToString() ? FDTypes.IN : FDTypes.OP;
                        }
                    }
                    fdAccountSystem.FDAccountId = fdAccountId;
                    FDRenewalCount = fdAccountSystem.CountRenewalDetails();
                }
            }
        }
        /// <summary>
        /// To route the FDAccount based on the Type such as (OP/IN/RN/POI/WDI)
        /// </summary>
        /// <param name="FDAccountId">If FDacountId>0 then it will route the Form based on the type.</param>
        /// <param name="VoucherId">if VoucherId>0 then it will route the screen either Renewal/Withdrawal/Post Interest Else Opening Screen will be routed.</param>
        /// <param name="fdTypes">FD Types will be RN,WDI,POI,In,OP</param>
        public frmFDAccount(int FDAccountId, int VoucherId, FDTypes fdTypes)
            : this()
        {
            fdAccountId = FDAccountId;
            this.FDVoucherId = VoucherId;
            this.fdTypes = fdTypes;

            //as On 03/11/2021, For Post Interest, the Logic was fixed to send dtmaster and dtRenewal
            //It means another Constractor, but few places, they send in this constractor
            //If it is Post interest we assign its renewal, its master and renewals
            if (this.fdTypes == FDTypes.POI && VoucherId > 0)
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    fdAccountSystem.VoucherId = VoucherId;
                    resultArgs = fdAccountSystem.FetchFDAccountId();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        FDRenewalId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                        ProjectId = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                        DataSet dsFDRenewal = fdAccountSystem.GetFDMasterRenewalDetails(FDAccountId);
                        if (dsFDRenewal.Tables.Count > (int)YesNo.Yes)
                        {
                            dtMaster = dsFDRenewal.Tables[0];
                            dtRenewal = dsFDRenewal.Tables[1];
                        }
                    }
                    this.FDVoucherId = 0;
                }
            }
        }

        /// <summary>
        /// If there is only one renewal exists in the FD Account,this constructor will be used to modify.
        /// </summary>
        /// <param name="fdRenewalId">To Identify each renewal uniquely</param>
        /// <param name="FDAccountId">FD account Id brlongs to FD Renewal Id</param>
        /// <param name="FdTypes">FD Types will be RN,WDI,POI,In,OP</param>
        /// <param name="dtMasters">Loads the master details </param>
        /// <param name="dtRenewval">Loads the renewal details</param>
        public frmFDAccount(int fdRenewalId, int FDAccountId, FDTypes FdTypes, DataTable dtMasters, DataTable dtRenewval, bool frmRprt = false)
            : this()
        {
            FDRenewalId = fdRenewalId;
            fdTypes = FdTypes;
            dtMaster = dtMasters;
            dtRenewal = dtRenewval;
            fdAccountId = FDAccountId;

            //On 27/10/2021, to set project id when drilling from reports
            if (fdRenewalId > 0 && frmRprt && dtMasters != null && dtMasters.Rows.Count > 0)
            {
                ProjectId = UtilityMember.NumberSet.ToInteger(dtMaster.Rows[0]["PROJECT_ID"].ToString());
            }
        }
        #endregion

        #region Properties
        private double FDACIInsAmount { get; set; }
        public int ProjectId { get; set; }
        public int LedgerId { get; set; }
        public int ChargeLedgerId { get; set; }
        public int BankLedgerId { get; set; }
        public double FDInterestRate { get; set; }
        private int VoucherId { get; set; }
        private int FDIntrestVoucherId { get; set; }
        private int transVoucherMethod = 0;
        private int TransVoucherMethod
        {
            set { transVoucherMethod = value; }
            get { return transVoucherMethod; }
        }

        private int WithdrwalReceiptTransVoucherMethod { get; set; }

        private string CashBankLedgerGroup { get; set; }
        public double IntrestCalculatedAmount { get; set; }
        public double ReInvestedAmount { get; set; }

        // chinna 06.09.2017
        public double ExpectedMaturityInterestAmount { get; set; }

        private int PrevProjectId { get; set; }
        private int PrevLedgerId { get; set; }
        public string FdProjectName { get; set; }
        public string LedgerName { get; set; }
        public string FDAccountNumber { get; set; }
        public double FDAmount { get; set; }
        private int FDRenewalId { get; set; }
        private int FDAccountId { get; set; }
        private DataTable dtMaster { get; set; }
        private DataTable dtRenewal { get; set; }
        public int FDBankID { get; set; }
        public string MaturityDate { get; set; }
        public string RenewalDate { get; set; }
        public string BankName { get; set; }
        private double FDInterestAmount { get; set; }
        private double FDAccumulatedPrincipalAmount { get; set; }
        public double FDReInvestedPrincipalAmount { get; set; }
        private double FDPrincipalAmount { get; set; }
        private double FDInterestReceivedAmount { get; set; }
        private double FDAccumulatedInterestAmount { get; set; }
        private double FDInsRate { get; set; }
        private string RenewalType { get; set; }
        private int InterestType { get; set; }
        private int PreviousInterestType { get; set; }
        private string InterestVoucherNumber { get; set; }
        private double InterestRate { get; set; }
        public int FDRenewalCount { get; set; }
        private double timesPerNumber { get; set; }
        private double timesPerYear { get; set; }
        private double AcutalAccumulatedInsAmount { get; set; }
        private int PrevFDLedgerId { get; set; }
        private double WithdrawAmount { get; set; }
        private double TempPrincipalAmount { get; set; }
        public double InvestedAmount { get; set; }
        public string FDActiveStatus { get; set; }

        public int VoucherDefinitionId { get; set; }
        public int WithdrwalReceiptVoucherDefinitionId { get; set; }

        public int BankBranchId
        {
            get
            {
                int BranchId = 0;
                if (glkpCashBankLedgers.EditValue != null)
                {
                    DataRowView dv = glkpCashBankLedgers.Properties.GetRowByKeyValue(this.UtilityMember.NumberSet.ToInteger(glkpCashBankLedgers.EditValue.ToString())) as DataRowView;
                    if (dv != null)
                    {
                        BranchId = this.UtilityMember.NumberSet.ToInteger(dv.Row["BANK_ID"].ToString());
                    }
                }
                return BranchId;
            }

        }//Added By sugan---(for FDInvestment only)to load bank and branch in add mode based on the bank Account Selection of glkpCashBankLedgers combo is changed 

        //***************Added by sugan to maintain validation if withdrawal amount exceeds to maximum invested amount*******************************
        private double InvestmentAmount { get; set; }
        private double Withdrawals { get; set; }
        //*******************************************************************************************************************************************
        #endregion

        #region PostInterest Properties
        public DateTime PostInterestCreatedDate { get; set; }
        public decimal PostInterestRate { get; set; }
        public DateTime PostInterestMaturityDate { get; set; }
        public int PostInterestType { get; set; }
        public string PostRenewalType { get; set; }

        #endregion

        #region Audit Lock
        private DateTime dtLockDateFrom { get; set; }
        private DateTime dtLockDateTo { get; set; }
        #endregion

        #region Events
        private void frmFDAccount_Load(object sender, EventArgs e)
        {
            System.Resources.ResourceManager resoureManager = new ResourceManager(typeof(frmFDAccount));
            string width = resoureManager.GetString("width");
            if (!string.IsNullOrEmpty(width))
            {
                this.Width = Convert.ToInt32(width);
            }
            SetTitle();
            SetDefault();
            LoadDefaults();
            EnableControls();
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyUseRights();
            }
            else
            {
                glkpBranch.Properties.Buttons[1].Visible = true;
            }
            this.CenterToParent();
            if (FDRenewalCount != 0)
            {
                if (fdTypes == FDTypes.IN)
                {
                    this.ShowSuccessMessageOnToolTip(this.GetMessage(MessageCatalog.Master.Mapping.CANNOT_CHANGE_FDLEDGE_BANKACCOUNT));
                }
                else
                {
                    this.ShowSuccessMessageOnToolTip(this.GetMessage(MessageCatalog.Master.Mapping.CANNOT_CHANGE_PROJECT_FDLEDGER));
                }
            }
            if (!this.AppSetting.LanguageId.Equals("en-US"))
            {
                SetAlignmentForLanguage();
            }

            EnableFDRenewalTransMode();

            popupecFD.Visible = (fdAccountId > 0);
            if (popupecFD.Visible) popupecFD.Width = 25;


            lblClosedOn.Width = 175;

            //On 07/05/2024, To hide mutual fund details
            lcMutualFolioNo.Visibility = lcBaseNAVPerUnit.Visibility = lcBaseNAVNoOfUnits.Visibility = LayoutVisibility.Never;
            lcMutualFundScheme.Visibility = lcMutualFundModeHolding.Visibility = LayoutVisibility.Never;

            lcRNNAVPerUnit.Visibility = lcRNNAVNoOfUnits.Visibility = LayoutVisibility.Never;

            //31/07/2024, Other than India, let us lock TDS Amount
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                lciTDSAmount.Visibility = LayoutVisibility.Never;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValidCurrency() && ValidateFDAccountDetails())
                {
                    using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                    {
                        //fdAccountSystem.FDLedgerId = 0;
                        fdAccountSystem.FDAccountId = fdAccountId;
                        fdAccountSystem.VoucherId = VoucherId;
                        fdAccountSystem.VoucherDefinitionId = VoucherDefinitionId;

                        //On 24/07/2023, To alert to authorize voucher based on setting in Finance ----------------------------------
                        fdAccountSystem.AuthorizedStatus = ConfirmVoucherAuthorization;
                        //-----------------------------------------------------------------------------------------------------------

                        //On 08/05/2024, Mutual Fund Properties
                        fdAccountSystem.InvestmentTypeId = (Int32)FDInvestmentType.FD;
                        fdAccountSystem.MFFolioNo = fdAccountSystem.MFSchemeName = string.Empty;
                        fdAccountSystem.MFNAVPerUnit = fdAccountSystem.MFNoOfUnit = 0;
                        fdAccountSystem.MFModeOfHolding = 0;

                        if (fdTypes == FDTypes.RN || fdTypes == FDTypes.RIN || fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD || fdTypes == FDTypes.POI)
                        {
                            fdAccountSystem.FDIntrestVoucherId = FDIntrestVoucherId;
                            fdAccountSystem.FDRenewalId = FDRenewalId;
                            fdAccountSystem.FDInterstCalAmount = this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                            fdAccountSystem.ProjectId = ProjectId;
                            if (cboInterestMode.SelectedIndex == 0)
                            {
                                if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)
                                {
                                    fdAccountSystem.FDLedgerId = FDRenewalId == 0 ? LedgerId : PrevFDLedgerId;
                                }
                                fdAccountSystem.LedgerId = glkpInterestLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString()) : 0;
                                fdAccountSystem.CashBankId = glkpBankInterestLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBankInterestLedger.EditValue.ToString()) : 0;
                            }
                            else
                            {
                                fdAccountSystem.LedgerId = glkpInterestLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString()) : 0;
                                fdAccountSystem.CashBankId = FDRenewalId == 0 ? LedgerId : PrevFDLedgerId;
                            }
                            fdAccountSystem.CashBankLedgerAmt = lblCashLedgerAmt.Text;
                            fdAccountSystem.FDLedgerAmt = lblIntrestLedgerAmt.Text;
                            fdAccountSystem.FDVoucherNo = txtRenewalVoucherNo.Text;
                            fdAccountSystem.FDPrinicipalAmount = this.UtilityMember.NumberSet.ToDouble(FDAmount.ToString());
                            fdAccountSystem.VoucherIntrestMode = cboInterestMode.SelectedIndex;
                            if (fdTypes == FDTypes.RN)
                            {
                                fdAccountSystem.FDRenewalType = cboInterestMode.SelectedIndex == 0 ? FDRenewalTypes.IRI.ToString() : FDRenewalTypes.ACI.ToString();
                                fdAccountSystem.IntrestAmount = this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                                fdAccountSystem.FDInterestRate = this.UtilityMember.NumberSet.ToDouble(txtFdInterestRate.Text);
                                fdAccountSystem.FDTDSAmount = this.UtilityMember.NumberSet.ToDouble(txtTDSAmount.Text);
                                fdAccountSystem.FDExpectedRenewMaturityValue = this.UtilityMember.NumberSet.ToDouble(txtExpectedMaturityRenewalValue.Text);
                                fdAccountSystem.ExpectedInterestAmount = this.UtilityMember.NumberSet.ToDouble(txtRenewalExpectedInteres.Text);
                            }
                            else if (fdTypes == FDTypes.RIN)
                            {
                                fdAccountSystem.FDRenewalType = FDRenewalTypes.RIN.ToString();
                                fdAccountSystem.FDLedgerId = LedgerId;
                                fdAccountSystem.ReInvestmentAmount = this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text);
                            }
                            else if (fdTypes == FDTypes.POI)
                            {
                                //fdAccountSystem.FDRenewalType = !string.IsNullOrEmpty(PostRenewalType) ? PostRenewalType : cboInterestMode.SelectedIndex == 0 ? FDRenewalTypes.IRI.ToString() : FDRenewalTypes.ACI.ToString();
                                fdAccountSystem.FDRenewalType = cboInterestMode.SelectedIndex == 0 ? FDRenewalTypes.IRI.ToString() : FDRenewalTypes.ACI.ToString();
                                fdAccountSystem.IntrestAmount = this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                                fdAccountSystem.FDInterestRate = this.UtilityMember.NumberSet.ToDouble(PostInterestRate.ToString());
                                fdAccountSystem.FDTDSAmount = this.UtilityMember.NumberSet.ToDouble(txtTDSAmount.Text);
                            }
                            else
                            {
                                fdAccountSystem.FDRenewalType = FDRenewalTypes.WDI.ToString();
                                fdAccountSystem.IntrestAmount = this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                                fdAccountSystem.WithdrawAmount = this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text);
                                fdAccountSystem.FDTDSAmount = this.UtilityMember.NumberSet.ToDouble(txtTDSAmount.Text);
                                fdAccountSystem.FDExpectedRenewMaturityValue = this.UtilityMember.NumberSet.ToDouble(txtExpectedMaturityRenewalValue.Text);
                                fdAccountSystem.FDInterestRate = interestRate;

                                //On 20/05/2022, for penalty amount
                                fdAccountSystem.ChargeMode = 0;
                                fdAccountSystem.ChargeAmount = 0;
                                fdAccountSystem.ChargeLedgerId = 0;
                                if (lcPenaltyAmount.Visibility == LayoutVisibility.Always && cbPenaltyMode.SelectedIndex > 0 &&
                                    this.UtilityMember.NumberSet.ToDouble(txtPenaltyAmount.Text) > 0 && glkpPenaltyLedger.EditValue != null)
                                {
                                    fdAccountSystem.ChargeAmount = this.UtilityMember.NumberSet.ToDouble(txtPenaltyAmount.Text);
                                    fdAccountSystem.ChargeLedgerId = glkpPenaltyLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpPenaltyLedger.EditValue.ToString()) : 0;
                                    fdAccountSystem.ChargeMode = cbPenaltyMode.SelectedIndex;
                                }

                                if (FDAmount == UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text))
                                    fdAccountSystem.ClosedDate = dteClosedOn.DateTime;
                                if ((FDAmount) > UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text))
                                {
                                    fdAccountSystem.FDRenewalType = FDRenewalTypes.PWD.ToString();
                                }
                                else
                                {
                                    fdAccountSystem.FDRenewalType = FDRenewalTypes.WDI.ToString();
                                }
                            }

                            fdAccountSystem.InterestType = fdTypes == FDTypes.POI ? PostInterestType : cboInsType.SelectedIndex;
                            fdAccountSystem.FDTransType = fdTypes == FDTypes.RN ? FDTypes.RN.ToString() : fdTypes == FDTypes.RIN ? FDTypes.RIN.ToString() : fdTypes == FDTypes.POI ? FDTypes.POI.ToString() : fdTypes == FDTypes.WD ? FDTypes.WD.ToString() : FDTypes.PWD.ToString();
                            fdAccountSystem.FDTransMode = lblTransMode.Text;

                            //31/05/2017, for POI, assign maturity date as renewal date
                            //fdAccountSystem.CreatedOn = FDTypes.RN == fdTypes ? dteRenewalOn.DateTime : FDTypes.POI == fdTypes ? PostInterestCreatedDate : dteClosedOn.DateTime;
                            fdAccountSystem.CreatedOn = FDTypes.RN == fdTypes ? dteRenewalOn.DateTime : FDTypes.RIN == fdTypes ? dePostDate.DateTime : FDTypes.POI == fdTypes ? dePostDate.DateTime : dteClosedOn.DateTime;

                            //On 12/01/2024, For POI, to set proper maturity date
                            // by aldrin to save the maturity date as it is in the OP/RN/IN while widthdrawal
                            //fdAccountSystem.FDMaturityDate = FDTypes.RN == fdTypes ? dteFDMaturityDate.DateTime : FDTypes.POI == fdTypes ? dePostDate.DateTime : FDTypes.RIN == fdTypes || FDTypes.WD == fdTypes || FDTypes.PWD == fdTypes ? UtilityMember.DateSet.ToDate(lblAssignMaturedon.Text, false) : UtilityMember.DateSet.ToDate(MaturityDate, false);
                            //On 10/05/2024, For Mutual Fund set Matiruty date as null
                            if ((fdTypes == FDTypes.POI || fdTypes == FDTypes.WD || fdTypes == FDTypes.POI || fdTypes == FDTypes.RIN) && BaseFDInvestmentType == (Int32)FDInvestmentType.MutualFund)
                            {
                                fdAccountSystem.FDMaturityDate = DateTime.MinValue;
                            }
                            else
                            {
                                fdAccountSystem.FDMaturityDate = FDTypes.RN == fdTypes ? dteFDMaturityDate.DateTime : FDTypes.POI == fdTypes ? UtilityMember.DateSet.ToDate(lblAssignMaturedon.Text, false) : FDTypes.RIN == fdTypes || FDTypes.WD == fdTypes || FDTypes.PWD == fdTypes ? UtilityMember.DateSet.ToDate(lblAssignMaturedon.Text, false) : UtilityMember.DateSet.ToDate(MaturityDate, false);
                            }

                            //fdAccountSystem.FDMaturityDate = FDTypes.RN == fdTypes ? dteFDMaturityDate.DateTime : FDTypes.POI == fdTypes ? UtilityMember.DateSet.ToDate(MaturityDate, false) : FDTypes.RIN == fdTypes ? UtilityMember.DateSet.ToDate(lblAssignMaturedon.Text, false) : UtilityMember.DateSet.ToDate(MaturityDate, false);//dteClosedOn.DateTime;  // by aldrin to save the maturity date as it is in the OP/RN/IN while widthdrawal
                            fdAccountSystem.FDInterestAmount = IntrestCalculatedAmount;
                            fdAccountSystem.ReceiptNo = txtFDReceiptNo.Text;
                            //  fdAccountSystem.FDInterestRate = this.UtilityMember.NumberSet.ToDouble(txtFdInterestRate.Text);
                            fdAccountSystem.CashBankLedgerGroup = TransactionMode.CR.ToString();
                            fdAccountSystem.FDType = fdTypes.ToString();
                            if (VoucherId.Equals((int)YesNo.No))
                                fdAccountSystem.TransVoucherMethod = TransVoucherMethod;
                            fdAccountSystem.BankId = FDBankID;
                        }
                        else
                        {
                            fdAccountSystem.PrevLedgerId = PrevLedgerId;
                            fdAccountSystem.PrevProjectId = PrevProjectId;
                            fdAccountSystem.BankId = glkpBranch.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBranch.EditValue.ToString()) : 0;
                            if (fdTypes == FDTypes.OP)
                            {
                                fdAccountSystem.LedgerId = glkpLedgers.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedgers.EditValue.ToString()) : 0;
                                fdAccountSystem.FDSubTypes = "FD-O";
                            }
                            else
                            {
                                fdAccountSystem.LedgerId = glkpFDLedgerDetails.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpFDLedgerDetails.EditValue.ToString()) : 0;
                                fdAccountSystem.BankLedgerId = glkpCashBankLedgers.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCashBankLedgers.EditValue.ToString()) : 0;
                                fdAccountSystem.FDSubTypes = "FD-I";
                            }

                            //On 08/05/2022, To set mutual fund properites 
                            if (EntryFDInvestmentType == (int)FDInvestmentType.MutualFund)
                            {
                                fdAccountSystem.InvestmentTypeId = EntryFDInvestmentType;
                                fdAccountSystem.MFFolioNo = txtMutualFolioNo.Text.Trim();
                                fdAccountSystem.MFSchemeName = txtMutualFundScheme.Text.Trim();
                                fdAccountSystem.MFNAVPerUnit = this.UtilityMember.NumberSet.ToDouble(txtBaseNAVPerUnit.Text);
                                fdAccountSystem.MFNoOfUnit = this.UtilityMember.NumberSet.ToDouble(txtBaseNAVNoOfUnits.Text); ;
                                fdAccountSystem.MFModeOfHolding = cblutualFundModeHolding.SelectedIndex;
                            }

                            fdAccountSystem.ProjectId = glkpProDetails.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProDetails.EditValue.ToString()) : 0;
                            fdAccountSystem.CashBankId = fdTypes == FDTypes.IN ? glkpCashBankLedgers.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCashBankLedgers.EditValue.ToString()) : 0 : 0;
                            fdAccountSystem.FDAccountNumber = txtAccountNumber.Text;
                            fdAccountSystem.FDAccountHolderName = txtAccountHolder.Text.Trim();
                            fdAccountSystem.FDScheme = cboFDScheme.SelectedIndex == 0 ? (int)FDScheme.Normal : (int)FDScheme.Flexi;
                            fdAccountSystem.InterestType = cboInterestType.SelectedIndex;
                            if (fdTypes == FDTypes.OP)
                            {
                                fdAccountSystem.FDOPInvestmentDate = lblCurrentDate.Text;
                            }
                            fdAccountSystem.FdAmount = this.UtilityMember.NumberSet.ToDouble(txtAmount.Text);
                            fdAccountSystem.FDTransType = fdTypes.ToString();
                            fdAccountSystem.FDInterestAmount = IntrestCalculatedAmount;
                            fdAccountSystem.FDExpectedMaturityValue = this.UtilityMember.NumberSet.ToDouble(txtExpectedMaturityValue.Text);
                            fdAccountSystem.ExpectedInterestAmount = this.UtilityMember.NumberSet.ToDouble(txtCreationExceptedInterest.Text);

                            fdAccountSystem.FDVoucherNo = txtVoucherNo.Text;
                            fdAccountSystem.CashBankLedgerGroup = TransactionMode.CR.ToString();
                            fdAccountSystem.CashBankLedgerAmt = fdTypes == FDTypes.IN ? lblCashBankLedgerAmt.Text : string.Empty;
                            fdAccountSystem.FDLedgerAmt = lblLedgerCurAmt.Text;
                            if (fdAccountId == 0)
                            {
                                if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual) { fdAccountSystem.FDVoucherNo = txtVoucherNo.Text; }
                                else { fdAccountSystem.TransVoucherMethod = TransVoucherMethod; }
                            }
                            fdAccountSystem.CreatedOn = deCreatedDate.DateTime;
                            if (EntryFDInvestmentType == (Int32)FDInvestmentType.MutualFund && (fdTypes == FDTypes.IN || fdTypes == FDTypes.OP))
                                fdAccountSystem.FDMaturityDate = DateTime.MinValue;
                            else
                                fdAccountSystem.FDMaturityDate = deDateOfMaturity.DateTime;

                            fdAccountSystem.ReceiptNo = txtReceiptNo.Text;
                            fdAccountSystem.FDInterestRate = this.UtilityMember.NumberSet.ToDouble(txtIntrestRate.Text);
                        }
                        if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)
                        {
                            fdAccountSystem.FdAmount = this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text);
                            fdAccountSystem.FDInterstCalAmount = this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                            fdAccountSystem.FDCashBankWithdrawAmount = this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text);
                            fdAccountSystem.FDActiveStatus = FDActiveStatus;
                            if (FDAmount == UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text))
                                fdAccountSystem.ClosedDate = dteClosedOn.DateTime;
                            if ((FDAmount) > UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text))
                            {
                                fdAccountSystem.FDRenewalType = FDRenewalTypes.PWD.ToString();
                            }
                            else
                            {
                                fdAccountSystem.FDRenewalType = FDRenewalTypes.WDI.ToString();
                            }

                            //On 06/10/2022, for fd withdrwal receipt interest nubmer
                            fdAccountSystem.FDWithdrwalReceiptVoucherNo = txtWithdrwalReceiptVNo.Text;
                        }

                        if (fdTypes == FDTypes.RIN)
                        {
                            fdAccountSystem.FdAmount = this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text);
                            fdAccountSystem.FDCashBankWithdrawAmount = this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text);
                            fdAccountSystem.FDActiveStatus = FDActiveStatus;
                            fdAccountSystem.FDRenewalType = FDRenewalTypes.RIN.ToString();
                        }

                        //On 27/10/2023, To assign FD Trans Mode --------------------------------------------------------
                        fdAccountSystem.FDRenewalTransMode = TransSource.Dr;
                        if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD || fdTypes == FDTypes.POI || fdTypes == FDTypes.RN)
                        {
                            //On 27/10/2023, For Post Interest accumulated, for accumulated Post Interest
                            if ((fdTypes == FDTypes.POI || fdTypes == FDTypes.RN) && cboInterestMode.SelectedIndex == ((int)FDRenewalTypes.ACI) &&
                                cboTransMode.Text.ToUpper() == TransSource.Cr.ToString().ToUpper())
                            {
                                fdAccountSystem.FDRenewalTransMode = (cboTransMode.Text.ToUpper() == TransSource.Cr.ToString().ToUpper()) ? TransSource.Cr : TransSource.Dr;
                            }
                            else if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)
                            {
                                fdAccountSystem.FDRenewalTransMode = TransSource.Cr;
                            }
                        }

                        //On 15/10/2024, To Set currency details ---------------------------------------------------------------------------------------------------
                        fdAccountSystem.CurrencyCountryId = 0;
                        fdAccountSystem.ExchangeRate = 1;
                        fdAccountSystem.LiveExchangeRate = 1;
                        fdAccountSystem.CalculatedAmount = 0;
                        fdAccountSystem.ActualAmount = 0;
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            fdAccountSystem.CurrencyCountryId = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                            fdAccountSystem.ExchangeRate = this.UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text);
                            fdAccountSystem.LiveExchangeRate = this.UtilityMember.NumberSet.ToDecimal(lblLiveExchangeRate.Text);
                            fdAccountSystem.CalculatedAmount = this.UtilityMember.NumberSet.ToDecimal(lblCalculatedAmt.Text);
                            fdAccountSystem.ContributionAmount = this.UtilityMember.NumberSet.ToDecimal(txtCurrencyAmount.Text);
                            fdAccountSystem.ActualAmount = this.UtilityMember.NumberSet.ToDecimal(txtActualAmount.Text);
                        }
                        //-------------------------------------------------------------------------------------------------------------------------------------------


                        fdAccountSystem.LedgerNotes = mtxtNotes.Text;
                        fdAccountSystem.FDTransMode = lblTransMode.Text;
                        fdAccountSystem.dtCashBankLedger = ConstructFDTable();
                        fdAccountSystem.dtFDLedger = ConstructFDTable();
                        if ((fdAccountSystem.WithdrawAmount > 0 && fdAccountSystem.IntrestAmount > 0) || (fdAccountSystem.WithdrawAmount > 0) && fdTypes.Equals(FDTypes.WD))
                        {
                            fdAccountSystem.dteWithdrawReceiptDate = dteClosedOn.DateTime;
                            resultArgs = fdAccountSystem.SaveFDAccount();
                        }
                        else if (fdTypes.Equals(FDTypes.OP) || fdTypes.Equals(FDTypes.RN) || fdTypes.Equals(FDTypes.IN) || fdTypes.Equals(FDTypes.POI) || fdTypes.Equals(FDTypes.RIN))
                        {
                            resultArgs = fdAccountSystem.SaveFDAccount();
                        }
                        else if (fdAccountSystem.WithdrawAmount == 0 && fdAccountSystem.IntrestAmount == 0 && fdTypes.Equals(FDTypes.WD))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.CANNOT_WITHDRAW));
                            resultArgs.Success = false;
                        }
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            FetchFDLedgerBalanceAmt();
                            FetchCashBankLedgerAmt();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            if (fdAccountId == 0)
                            {
                                ClearControls();
                                LoadCurrentDate();
                                //chinna
                                // 06.10.2017
                                // lblMaturityDate.Text = deDateOfMaturity.DateTime != DateTime.MinValue.Date ? deDateOfMaturity.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(0) : " : " + this.UtilityMember.NumberSet.ToCurrency(0);
                            }

                            if (fdTypes == FDTypes.RN || fdTypes.Equals(FDTypes.POI) || fdTypes == FDTypes.RIN)
                            {
                                if (this.FDVoucherId == 0)
                                {
                                    ClearControls();
                                    GetLastRenewalDate();
                                }
                                this.Close();
                            }
                            else if (fdTypes == FDTypes.WD)
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            //On 18/12/2020, for Expired Licese
                            if (resultArgs != null)
                            {
                                this.ShowMessageBox(resultArgs.Message);
                                if (mtxtNotes.Visible)
                                {
                                    mtxtNotes.Select();
                                    mtxtNotes.Focus();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpBranch_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmBankAdd frmAdd = new frmBankAdd();
                frmAdd.ShowDialog();
                if (frmAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadBank();
                    if (frmAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpBranch.EditValue = this.UtilityMember.NumberSet.ToInteger(frmAdd.ReturnValue.ToString());
                    }
                }
            }
        }

        private void ApplyUseRights()
        {
            if (CommonMethod.ApplyUserRights((int)Forms.CreateBank) != 0)
            {
                glkpBranch.Properties.Buttons[1].Visible = true;
            }
            else
            {
                glkpBranch.Properties.Buttons[1].Visible = false;
            }
        }

        private void txtIntrestRate_Leave(object sender, EventArgs e)
        {
            if (cboInterestType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
        }

        private void deDateOfMaturity_EditValueChanged(object sender, EventArgs e)
        {
            if (cboInterestType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAmount);
            if (!string.IsNullOrEmpty(txtIntrestRate.Text) && this.UtilityMember.NumberSet.ToDouble(txtIntrestRate.Text) != 0)
            {
                CalculateInterestAmount();
            }
        }

        private void txtAmount_EditValueChanged(object sender, EventArgs e)
        {
            //On 24/10/2024, To get proper amount and assigned to currency amount
            SetCurrencyAmount();
        }

        private void glkpProDetails_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ProjectId = glkpProDetails.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProDetails.EditValue.ToString()) : 0;
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    fdAccountSystem.ProjectId = ProjectId;

                    if (fdTypes == FDTypes.OP)
                    {
                        FetchLeder(glkpLedgers);
                    }
                    else
                    {
                        FetchLeder(glkpFDLedgerDetails);
                    }
                }
                this.SetBorderColorForGridLookUpEdit(glkpLedgers);
                this.SetBorderColorForGridLookUpEdit(glkpFDLedgerDetails);
                this.SetBorderColorForGridLookUpEdit(glkpBranch);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void glkpFDLedgerDetails_EditValueChanged(object sender, EventArgs e)
        {
            FetchFDLedgerBalanceAmt();

            //Show Mutual Fund Properties
            ShowMutualFundProperties(glkpFDLedgerDetails);
        }

        private void glkpCashBankLedgers_EditValueChanged(object sender, EventArgs e)
        {
            FetchCashBankLedgerAmt();
            FetchLedgerGroup();
            LoadBank();
            //On 05/11/2024
            //if (BankBranchId > 0
            if (BankBranchId > 0 && fdAccountId == 0)
                glkpBranch.EditValue = BankBranchId;
            // by aldrin. When invent the amount from cash the ledger is not showing
            //else 
            //glkpBranch.EditValue = string.Empty;
            SetBorderColorForGridLookUpEdit(glkpBranch);
        }

        private void glkpLedgers_EditValueChanged(object sender, EventArgs e)
        {
            FetchFDLedgerBalanceAmt();
            //Show Mutual Fund Properties
            ShowMutualFundProperties(glkpLedgers);

            //On 18/10/2024, To fix fd ledger's opening exchange rate
            if (this.AppSetting.AllowMultiCurrency == 1 && fdTypes == FDTypes.OP)
            {
                if (glkpLedgers.GetSelectedDataRow() != null)
                {
                    DataRowView drv = glkpLedgers.GetSelectedDataRow() as DataRowView;
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        if (drv != null)
                        {
                            txtExchangeRate.Text = drv[ledgersystem.AppSchema.Ledger.OP_EXCHANGE_RATEColumn.ColumnName].ToString();
                            txtExchangeRate.Tag = txtExchangeRate.Text;
                            txtExchangeRate.Enabled = false;
                        }
                    }
                }
            }
        }

        private void cboInterestMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (fdTypes == FDTypes.RN || fdTypes == FDTypes.POI)
            {
                if (cboInterestMode.SelectedIndex != 0)
                {
                    lblCapBankInterestLedger.Visibility = LayoutVisibility.Never;
                    lblCashBankLedgerCap.Visibility = lblCashLedgerAmt.Visibility = emptySpaceItem16.Visibility = LayoutVisibility.Never;
                    lblFDIntrestAmount.Visibility = lblAssignPriniciapalintrestAmount.Visibility = LayoutVisibility.Always;
                    double dinterestamt = this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                    dinterestamt *= (this.AppSetting.EnableFDAdjustmentEntry == 1 && cboTransMode.Text.ToUpper() == TransSource.Cr.ToString().ToUpper() ? -1 : 1);

                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(FDAmount.ToString()) + dinterestamt) + " " + TransactionMode.DR.ToString();
                    if (fdTypes.Equals(FDTypes.RN))
                        CalculateInterestAmount();
                }
                else
                {
                    lblCapBankInterestLedger.Visibility = LayoutVisibility.Always;
                    lblCashBankLedgerCap.Visibility = lblCashLedgerAmt.Visibility = emptySpaceItem16.Visibility = LayoutVisibility.Always;
                    lblFDIntrestAmount.Visibility = lblAssignPriniciapalintrestAmount.Visibility = LayoutVisibility.Always;
                    lblAssignTotalAmt.Text = lblAssignPrinicipalAmount.Text;
                    if (fdTypes.Equals(FDTypes.RN))
                        CalculateInterestAmount();
                }


                if (this.AppSetting.IsCountryOtherThanIndia)
                    lciTDSAmount.Visibility = LayoutVisibility.Never;
                else
                    lciTDSAmount.Visibility = LayoutVisibility.Always;

                //On 04/10/202, chagne VO mode based on voucher type
                FetchVoucherMethod();
                //LoadVoucherNo(fdTypes);
            }
            EnableFDRenewalTransMode();
        }

        private void glkpInterestLedger_EditValueChanged(object sender, EventArgs e)
        {
            int LedgerId = glkpInterestLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpInterestLedger.EditValue.ToString()) : 0;
            BalanceProperty balanceProperty = FetchCurrentBalance(LedgerId);
            lblIntrestLedgerAmt.Text = this.UtilityMember.NumberSet.ToCurrency(balanceProperty.Amount) + " " + balanceProperty.TransMode;

            //On 17/10/2024, To show cash/bank/fd currenct balance
            if (this.AppSetting.AllowMultiCurrency == 1 && (balanceProperty.GroupId == (int)FixedLedgerGroup.Cash || balanceProperty.GroupId == (int)FixedLedgerGroup.BankAccounts ||
                                balanceProperty.GroupId == (int)FixedLedgerGroup.FixedDeposit))
            {
                lblIntrestLedgerAmt.Text = balanceProperty.CurrencySymbol + " " + (this.UtilityMember.NumberSet.ToNumber(balanceProperty.Amount) + " " + balanceProperty.TransFCMode);
            }
        }

        private void glkpBankInterestLedger_EditValueChanged(object sender, EventArgs e)
        {
            int LedgerId = glkpBankInterestLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBankInterestLedger.EditValue.ToString()) : 0;
            BalanceProperty balanceProperty = FetchCurrentBalance(LedgerId);
            lblCashLedgerAmt.Text = this.UtilityMember.NumberSet.ToCurrency(balanceProperty.Amount) + " " + balanceProperty.TransMode;

            //On 17/10/2024, To show cash/bank/fd currenct balance
            if (this.AppSetting.AllowMultiCurrency == 1 && (balanceProperty.GroupId == (int)FixedLedgerGroup.Cash || balanceProperty.GroupId == (int)FixedLedgerGroup.BankAccounts ||
                                balanceProperty.GroupId == (int)FixedLedgerGroup.FixedDeposit))
            {
                lblCashLedgerAmt.Text = balanceProperty.CurrencySymbol + " " + (this.UtilityMember.NumberSet.ToNumber(balanceProperty.Amount) + " " + balanceProperty.TransFCMode);
            }
        }

        private void txtRenewalInterestRate_Leave(object sender, EventArgs e)
        {
            if (fdTypes == FDTypes.RN || fdTypes == FDTypes.POI)
            {
                if (cboInterestMode.SelectedIndex != 0)
                {
                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount - FDInterestAmount + this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text)) + " " + TransactionMode.DR.ToString();
                }
            }
            if (cboInsType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
        }

        private void txtRenewalVoucherNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRenewalVoucherNo);
        }

        private void glkpInterestLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpInterestLedger);
        }

        private void glkpBankInterestLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpBankInterestLedger);
        }

        private void txtVoucherNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtVoucherNo);
        }

        private void glkpProDetails_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpProDetails);
        }

        private void glkpLedgers_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpLedgers);
        }

        private void glkpCashBankLedgers_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpCashBankLedgers);
        }

        private void glkpFDLedgerDetails_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpFDLedgerDetails);
        }

        private void glkpBranch_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpBranch);
        }

        private void txtAccountNumber_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAccountNumber);
        }

        private void deCreatedDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(deCreatedDate);
        }

        private void deDateOfMaturity_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(deDateOfMaturity);
            if (cboInterestType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
        }

        private void txtFdInterestRate_Leave(object sender, EventArgs e)
        {
            if (cboInsType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
        }

        private void dteFDMaturityDate_Leave(object sender, EventArgs e)
        {
            if (cboInsType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
        }

        private void dteFDMaturityDate_EditValueChanged(object sender, EventArgs e)
        {
            if (cboInsType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
        }

        private void cboInsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInsType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
        }

        private void cboInterestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInterestType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
        }

        private void deCreatedDate_EditValueChanged(object sender, EventArgs e)
        {
            if (cboInterestType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }

            if (FDTypes.IN == fdTypes)
            {
                LoadProject();
            }
            LoadCashBankLedger(glkpCashBankLedgers);

            //On 20/10/2021, To skip ledger closed date -------------
            if (fdTypes == FDTypes.OP)
            {
                FetchLeder(glkpLedgers);
            }
            else
            {
                FetchLeder(glkpFDLedgerDetails);
            }
            //------------------------------------------------------
        }

        private void dteRenewalOn_EditValueChanged(object sender, EventArgs e)
        {
            if (cboInsType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
            LoadCashBankLedger(glkpBankInterestLedger);

            //On 20/10/2021, To skip ledger closed date -------------
            LoadLedger(glkpInterestLedger);
            //------------------------------------------------
        }

        private void txtRenewalInterestRate_EditValueChanged(object sender, EventArgs e)
        {
            if (fdTypes == FDTypes.RN)
            {
                if (cboInterestMode.SelectedIndex != 0)
                {
                    double Value = FDAmount;
                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(Value + this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text)) + " " + TransactionMode.DR.ToString();
                }
            }
            //On 24/10/2024, To get proper amount and assigned to currency amount
            SetCurrencyAmount();

        }

        private void dteClosedOn_EditValueChanged(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(dteClosedOn);
            if (!dteClosedOn.DateTime.Equals(DateTime.MinValue))
            {
                //CalculateWithdrawInsAmount();
            }
            LoadCashBankLedger(glkpBankInterestLedger);

            //On 20/10/2021, To skip ledger closed date -------------
            LoadLedger(glkpInterestLedger);
            //------------------------------------------------------

            //On 23/05/2022, To Load General LEdges ------------------
            LoadAllGeneralLedger(glkpPenaltyLedger);
            //---------------------------------------------------------
        }

        private void txtWithdrawAmount_Leave(object sender, EventArgs e)
        {
            ////Validate amount exceeds to principal amount.
            //if (IsExceedsthePrincipalAmount())
            //{
            //    txtWithdrawAmount.Focus();
            //}
        }
        //******Changed by sugan-To validate the withdrawal date if it is less than the account created date ************************************************************************
        private void dteClosedOn_Leave(object sender, EventArgs e)
        {
            if (!dteClosedOn.DateTime.Equals(DateTime.MinValue))
            {
                CalculateWithdrawInsAmount();
            }
        }

        private void frmFDAccount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                frmDatePicker datePicker;
                if (e.KeyCode == Keys.F3)
                {
                    if (fdTypes == FDTypes.IN || fdTypes == FDTypes.OP)
                    {
                        datePicker = new frmDatePicker(deCreatedDate.DateTime, DatePickerType.VoucherDate, fdTypes);
                        datePicker.ShowDialog();
                        deCreatedDate.DateTime = AppSetting.VoucherDate;
                    }
                    else if (fdTypes == FDTypes.RN)
                    {
                        datePicker = new frmDatePicker(dteRenewalOn.DateTime, DatePickerType.VoucherDate);
                        datePicker.ShowDialog();
                        //dteRenewalOn.DateTime = AppSetting.VoucherDate;
                    }
                    else if (fdTypes == FDTypes.WD)
                    {
                        datePicker = new frmDatePicker(dteClosedOn.DateTime, DatePickerType.VoucherDate);
                        datePicker.ShowDialog();
                        //dteClosedOn.DateTime = AppSetting.VoucherDate;
                    }
                    else if (fdTypes == FDTypes.RIN)
                    {
                        datePicker = new frmDatePicker(dePostDate.DateTime, DatePickerType.VoucherDate);
                        datePicker.ShowDialog();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This is to load the FD Legders mapped to the project selected in the "grlkpProDetails" 	while creating FD opening and FD Investment.
        /// </summary>
        /// Parameter: FD Ledgers are Fetched Based on the type Ledger_SUB_Type="FD" adn Project_id
        /// <param name="glkpEdit"></param>
        /// <returns></returns>
        private DataTable FetchLeder(GridLookUpEdit glkpEdit)
        {
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    fdAccountSystem.ProjectId = ProjectId;

                    string bankdate = FDTypes.RN == fdTypes ? this.UtilityMember.DateSet.ToDate(dteRenewalOn.DateTime.ToShortDateString()) :
                                               FDTypes.POI == fdTypes ? this.UtilityMember.DateSet.ToDate(dePostDate.DateTime.ToShortDateString()) :
                                               FDTypes.OP == fdTypes || FDTypes.IN == fdTypes ? this.UtilityMember.DateSet.ToDate(deCreatedDate.DateTime.ToShortDateString()) :
                                               this.UtilityMember.DateSet.ToDate(dteClosedOn.DateTime.ToShortDateString());

                    if (bankdate != this.UtilityMember.DateSet.ToDate(DateTime.MinValue.ToShortDateString()))
                    {
                        fdAccountSystem.LedgerClosedDateForFilter = bankdate;
                    }

                    resultArgs = fdAccountSystem.FetchLedgerByProject();

                    Int32 PrevLedgerId = glkpEdit.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpEdit.EditValue.ToString()) : 0;

                    if (resultArgs.Success)
                    {
                        DataTable dtLedger = resultArgs.DataSource.Table;

                        //21/08/2024, To set Bank Ledger currency mode
                        //On 16/10/2024, If multi currency enabled, let us load cash and bank ledgers only for selected currency
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                            dtLedger.DefaultView.RowFilter = fdAccountSystem.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + " = " + currencycountry;
                            dtLedger = dtLedger.DefaultView.ToTable();
                        }

                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpEdit, dtLedger, fdAccountSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, fdAccountSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        glkpEdit.EditValue = glkpEdit.Properties.GetKeyValue(0);
                        FetchVoucherMethod();
                        if (fdTypes == FDTypes.IN)
                        {
                            LoadCashBankLedger(glkpCashBankLedgers);
                            FetchCashBankLedgerAmt();
                        }
                        FetchFDLedgerBalanceAmt();
                    }

                    // On 02/11/2021, to retain already selected ledger
                    if (glkpEdit.Properties.GetDisplayValueByKeyValue(PrevLedgerId) != null)
                    {
                        glkpEdit.EditValue = PrevLedgerId;
                    }
                    else
                    {
                        glkpEdit.EditValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// This is to load the branch Details based on the project selected in the "glkpProDetails" while creating "FD opening/FD Investment"
        /// This Bank details is needed because while renewal/withdrawal, Interest will be 	accumulated to the Prinicipal 
        /// amount(Journal entry) or Interest will be received(Receipt 	Entry)	
        /// While "Interest received" amount will be transacted  from "bank Interets Ledger" 	to "Bank(our bank Selected in FDopening/FD Investment").
        /// </summary>
        /// <param name="InitialValue"></param>
        private void LoadBank()
        {
            try
            {
                using (BankSystem bankSystem = new BankSystem())
                {
                    resultArgs = bankSystem.FetchBankDetailsforLookup();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBranch, resultArgs.DataSource.Table, bankSystem.AppSchema.Bank.BANKColumn.ColumnName, bankSystem.AppSchema.Bank.BANK_IDColumn.ColumnName);
                        //glkpBranch.EditValue = glkpBranch.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// On 15/10/2024, To load currency details
        /// </summary>
        private void LoadCountryCurrency()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    //On 15/10/2024, If multi currency enabled, let us load only currencies with have exchange rate for voucher date
                    string date = AppSetting.YearFrom;
                    if (this.AppSetting.AllowMultiCurrency == 1)
                        resultArgs = countrySystem.FetchCountryCurrencyDetails(this.UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                    else
                        resultArgs = countrySystem.FetchCountryDetails();

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtCurencyCountry = resultArgs.DataSource.Table;
                        dtCurencyCountry.DefaultView.RowFilter = "";

                        //15/10/2024, Load Currecny which have exchange rate
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            dtCurencyCountry.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " >0"; ;
                            dtCurencyCountry = dtCurencyCountry.DefaultView.ToTable();
                        }
                        //this.UtilityMember.ComboSet.BindLookUpEditCombo(glkpCurrencyCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCurrencyCountry, resultArgs.DataSource.Table,
                            countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        //2615/10/2024, For new voucher, set default currecny (global setting)
                        if (FDAccountId == 0 && this.AppSetting.AllowMultiCurrency == 1)
                        {
                            object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(this.AppSetting.Country);
                            if (findcountry != null)
                            {
                                glkpCurrencyCountry.EditValue = string.IsNullOrEmpty(this.AppSetting.Country) ? 0 : UtilityMember.NumberSet.ToInteger(this.AppSetting.Country);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void CalculateExchangeRate()
        {
            try
            {
                Double ActualAmt = this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) * this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                lblCalculatedAmt.Text = ActualAmt.ToString();
                txtActualAmount.Text = ActualAmt.ToString();

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void ShowCurrencyDetails()
        {
            int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            lblCurrencySymbol.Text = string.Empty;
            if (txtExchangeRate.Tag == null) txtExchangeRate.Text = "1";
            lblLiveExchangeRate.Text = "1";
            try
            {
                if (CountryId != 0)
                {
                    using (CountrySystem countrySystem = new CountrySystem())
                    {
                        ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                        if (result.Success)
                        {
                            lblCurrencySymbol.Text = countrySystem.CurrencySymbol;
                            lblLiveExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                            if (txtExchangeRate.Tag == null)
                                txtExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);

                        }
                    }
                }

                glkpCurrencyCountry.Enabled = false;
                if (fdTypes == FDTypes.IN || fdTypes == FDTypes.OP)
                {
                    using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                    {
                        glkpCurrencyCountry.Enabled = true;
                        if (fdAccountSystem.CountRenewalDetails() > 0)
                        {
                            glkpCurrencyCountry.Enabled = false;
                            txtExchangeRate.Enabled = false;
                        }
                        else if (fdTypes == FDTypes.OP)
                        {
                            txtExchangeRate.Enabled = false;
                        }
                    }
                }

                txtActualAmount.Enabled = false;

                AssignLiveExchangeRate();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
        /// </summary>
        private void AssignLiveExchangeRate()
        {
            lblLiveExchangeRate.ForeColor = Color.Black;
            lcLiveExchangeRate.AppearanceItemCaption.ForeColor = Color.Black;
            lblLiveExchangeRate.Font = new System.Drawing.Font(lblLiveExchangeRate.Font.FontFamily, lblLiveExchangeRate.Font.Size, FontStyle.Regular);
            lcLiveExchangeRate.AppearanceItemCaption.Font = new System.Drawing.Font(lcLiveExchangeRate.AppearanceItemCaption.Font.FontFamily,
                    lcLiveExchangeRate.AppearanceItemCaption.Font.Size, FontStyle.Regular);

            int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            using (CountrySystem countrySystem = new CountrySystem())
            {
                ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                if (result.Success)
                {
                    lblLiveExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                    this.ShowWaitDialog("Fetching Live Exchange Rate");
                    double liveExchangeAmount = this.AppSetting.CurrencyLiveExchangeRate(DateTime.Today.Date, countrySystem.CurrencyCode, AppSetting.CurrencyCode);
                    this.CloseWaitDialog();
                    if (liveExchangeAmount > 0)
                    {
                        lblLiveExchangeRate.Text = UtilityMember.NumberSet.ToNumber(liveExchangeAmount);

                        lblLiveExchangeRate.ForeColor = Color.Green;
                        lcLiveExchangeRate.AppearanceItemCaption.ForeColor = Color.Green;
                        lblLiveExchangeRate.Font = new System.Drawing.Font(lblLiveExchangeRate.Font.FontFamily, lblLiveExchangeRate.Font.Size, (FontStyle.Bold | FontStyle.Underline));
                        lcLiveExchangeRate.AppearanceItemCaption.Font = new System.Drawing.Font(lcLiveExchangeRate.AppearanceItemCaption.Font.FontFamily,
                                lcLiveExchangeRate.AppearanceItemCaption.Font.Size, (FontStyle.Bold | FontStyle.Underline));
                    }
                }
            }
        }

        /// <summary>
        /// On 17/10/2024, To show fd amount in currency
        /// </summary>
        private void ShowFDAmountDetailInCurrency()
        {
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                double amt = getAmountAlone(lblAssignPrinicipalAmount.Text);
                lblAssignPrinicipalAmount.Text = lblCurrencySymbol.Text + " " + this.UtilityMember.NumberSet.ToNumber(amt);

                amt = getAmountAlone(lblAssignTotalAmt.Text);
                lblAssignTotalAmt.Text = lblCurrencySymbol.Text + " " + this.UtilityMember.NumberSet.ToNumber(amt) + " " + TransactionMode.DR.ToString();

                amt = getAmountAlone(lblAssignPriniciapalintrestAmount.Text);
                lblAssignPriniciapalintrestAmount.Text = lblCurrencySymbol.Text + " " + this.UtilityMember.NumberSet.ToNumber(amt);

                amt = getAmountAlone(lblRINAmount.Text);
                lblRINAmount.Text = lblCurrencySymbol.Text + " " + this.UtilityMember.NumberSet.ToNumber(amt);

                amt = getAmountAlone(lblAssignIntrestamount.Text);
                lblAssignIntrestamount.Text = lblCurrencySymbol.Text + " " + this.UtilityMember.NumberSet.ToNumber(amt);

                amt = getAmountAlone(lblFDRenewalIntrestAmount.Text);
                lblFDRenewalIntrestAmount.Text = lblCurrencySymbol.Text + " " + this.UtilityMember.NumberSet.ToNumber(amt);
            }
        }

        private double getAmountAlone(string amt)
        {
            double rtn = 0;

            try
            {
                amt = amt.ToUpper().Replace(TransSource.Cr.ToString().ToUpper(), string.Empty).Replace(TransSource.Dr.ToString().ToUpper(), string.Empty).Trim();
                rtn = UtilityMember.NumberSet.ToDouble(amt.Split().Last());
            }
            catch (Exception err)
            {
                this.ShowMessageBox(err.Message);
            }
            return rtn;
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingProject = new MappingSystem())
                {
                    if (FDTypes.IN == fdTypes)
                    {
                        mappingProject.ProjectClosedDate = deCreatedDate.DateTime.ToShortDateString();
                    }
                    resultArgs = mappingProject.FetchProjectsLookup();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        Int32 Prevprojectid = glkpProDetails.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProDetails.EditValue.ToString()) : this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProDetails, resultArgs.DataSource.Table, mappingProject.AppSchema.Project.PROJECTColumn.ColumnName, mappingProject.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        //glkpProDetails.EditValue = glkpProDetails.Properties.GetKeyValue(0);
                        glkpProDetails.EditValue = (glkpProDetails.Properties.GetDisplayValueByKeyValue(Prevprojectid) != null ? Prevprojectid : 0);
                        //ProjectId = Prevprojectid;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadCurrentDate()
        {
            this.deDateOfMaturity.EditValueChanged -= new System.EventHandler(this.deDateOfMaturity_EditValueChanged);
            //deDateOfMaturity.DateTime = deCreatedDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            this.deDateOfMaturity.EditValueChanged += new System.EventHandler(this.deDateOfMaturity_EditValueChanged);

            if (FDTypes.OP == fdTypes)
            {
                DateTime beginingDate = this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                deCreatedDate.DateTime = beginingDate.AddDays(-1);
                //deCreatedDate.Properties.MinValue = beginingDate.AddDays(-1);
                deCreatedDate.Properties.MaxValue = beginingDate.AddDays(-1);
            }
        }
        /// <summary>
        /// This method to load the subsequent interest date after each post interest.
        /// Date Calculation Steps: 1. Get the Maximum RnewalDate from the "Date entered" and FD-Account_id
        /// Step 2.Add 1 day to the maximum renewal date and Assign that date to PodtInterest Date
        /// </summary>
        private void LoadPostDate()
        {
            this.dePostDate.EditValueChanged -= new System.EventHandler(this.dePostDate_EditValueChanged);
            //dePostDate.DateTime = PostInterestCreatedDate;
            using (FDAccountSystem fdaccountsystem = new FDAccountSystem())
            {
                fdaccountsystem.FDAccountId = fdAccountId;
                if (fdaccountsystem.HasFDRenewal() > 0)
                {
                    DataTable dtMaxrenewladate = fdaccountsystem.GetmaxRenewals(dePostDate.DateTime).DataSource.Table;
                    if (dtMaxrenewladate != null && dtMaxrenewladate.Rows.Count > 0 && dtMaxrenewladate.Rows[0]["RENEWAL_DATE"] != DBNull.Value)
                    {
                        // dePostDate.DateTime = UtilityMember.DateSet.ToDate(dtMaxrenewladate.Rows[0]["RENEWAL_DATE"].ToString(), false).AddDays(1); // by sugan
                        //dePostDate.DateTime = UtilityMember.DateSet.ToDate(dtMaxrenewladate.Rows[0]["RENEWAL_DATE"].ToString(), false);
                        PostInterestCreatedDate = UtilityMember.DateSet.ToDate(dtMaxrenewladate.Rows[0]["RENEWAL_DATE"].ToString(), false);
                    }
                    else
                    {
                        //dePostDate.DateTime = PostInterestCreatedDate;
                    }
                }

            }
            this.dePostDate.EditValueChanged += new System.EventHandler(this.dePostDate_EditValueChanged);
        }
        /// <summary>
        /// This is to load the bank Interest Ledgers mapped to the selected Project to maintain FD Interests
        /// This method is used to Load the Cash/Bank Ledegrs for FD Invsetment ,FD Renewals and Withdrawals so Glkp is used as a parameter.
        /// </summary>
        /// <param name="glkpLedger">ProjectId</param>
        private void LoadCashBankLedger(GridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {

                    ledgerSystem.ProjectId = ProjectId;
                    string bankdate = FDTypes.RN == fdTypes ? this.UtilityMember.DateSet.ToDate(dteRenewalOn.DateTime.ToShortDateString()) :
                                                FDTypes.POI == fdTypes || FDTypes.RIN == fdTypes ? this.UtilityMember.DateSet.ToDate(dePostDate.DateTime.ToShortDateString()) :
                                                FDTypes.IN == fdTypes ? this.UtilityMember.DateSet.ToDate(deCreatedDate.DateTime.ToShortDateString()) :
                                                this.UtilityMember.DateSet.ToDate(dteClosedOn.DateTime.ToShortDateString());

                    if (bankdate != this.UtilityMember.DateSet.ToDate(DateTime.MinValue.ToShortDateString()))
                    {
                        ledgerSystem.LedgerClosedDateForFilter = bankdate;
                    }
                    ResultArgs resultArgsCashBank = ledgerSystem.FetchCashBankLedger();
                    if (resultArgsCashBank.Success && resultArgsCashBank.DataSource.Table != null)
                    {
                        Int32 PrevCashBankLedgerId = glkpLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) : 0;

                        DataTable dtCashBank = resultArgsCashBank.DataSource.Table;

                        //21/08/2024, To set Bank Ledger currency mode
                        //On 16/10/2024, If multi currency enabled, let us load cash and bank ledgers only for selected currency
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                            dtCashBank.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.CUR_COUNTRY_IDColumn.ColumnName + " = " + currencycountry;
                            dtCashBank = dtCashBank.DefaultView.ToTable();
                        }

                        //if (bankdate != this.UtilityMember.DateSet.ToDate(DateTime.MinValue.ToShortDateString()))
                        //{
                        //    //dtCashBank.DefaultView.RowFilter = "(DATE_CLOSED >='" + bankdate + "' OR DATE_CLOSED IS NULL) AND " +
                        //    //                                    "(DATE_OPENED <='" + bankdate + "' OR DATE_OPENED IS NULL)";
                        //    //dtCashBank = dtCashBank.DefaultView.ToTable();

                        //    dtCashBank = FilterLedgerByDateClosed(dtCashBank, true, this.UtilityMember.DateSet.ToDate(bankdate,false));
                        //}
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedger, dtCashBank, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        //glkpLedger.EditValue = glkpLedger.Properties.GetDisplayValueByKeyValue(CashBankLedgerId)!=null ? CashBankLedgerId:  glkpLedger.Properties.GetKeyValue(0);
                        if (glkpLedger.Properties.GetDisplayValueByKeyValue(PrevCashBankLedgerId) != null)
                        {
                            glkpLedger.EditValue = PrevCashBankLedgerId;
                        }
                        else
                        {
                            glkpLedger.EditValue = 0;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// On 04/09/2024, If multi currency enabled, all the currency details must be filled
        /// </summary>
        /// <returns></returns>
        private bool isValidCurrency()
        {
            bool isValid = true;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                if (!IsCurrencyEnabledVoucher)
                { //On 04/09/2024, If multi currency enabled, all the currency details must be filled
                    isValid = false;
                    MessageRender.ShowMessage("As Multi Currency option is enabled, All the Currecny details should be filled.");
                    glkpCurrencyCountry.Select();
                    glkpCurrencyCountry.Focus();
                }

                //To Validate Amount
                if (isValid)
                {
                    double amt = 0;
                    if (fdTypes == FDTypes.IN || fdTypes == FDTypes.OP) amt = UtilityMember.NumberSet.ToDouble(txtAmount.Text);
                    else if (fdTypes == FDTypes.RN || fdTypes == FDTypes.POI) amt = UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                    else if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD || fdTypes == FDTypes.RIN) amt = UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text);
                    else amt = UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);

                    if (amt != UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text))
                    {
                        isValid = false;
                        MessageRender.ShowMessage("Mismatching Currency Amount with Fixed Deposit Amount.");
                        txtCurrencyAmount.Select();
                        txtCurrencyAmount.Focus();
                    }
                }
            }

            return isValid;
        }

        private void SetCurrencyAmount()
        {
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                /*string amt = txtCurrencyAmount.Text;
                if (fdTypes == FDTypes.IN || fdTypes == FDTypes.OP) 
                    txtAmount.Text = amt;
                else if (fdTypes == FDTypes.RN || fdTypes == FDTypes.POI)
                {
                    txtRenewalInterestRate.Text = amt;
                    txtRenewalInterestRate_Leave(txtRenewalInterestRate, null);
                }
                else if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD) txtWithdrawAmount.Text = amt;
                else
                {
                    txtRenewalInterestRate.Text = amt;
                }*/

                if (fdTypes == FDTypes.IN || fdTypes == FDTypes.OP)
                    txtCurrencyAmount.Text = txtAmount.Text;
                else if (fdTypes == FDTypes.RN || fdTypes == FDTypes.POI)
                {
                    txtCurrencyAmount.Text = txtRenewalInterestRate.Text;
                }
                else if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD | fdTypes == FDTypes.RIN) txtCurrencyAmount.Text = txtWithdrawAmount.Text;
                else
                {
                    txtCurrencyAmount.Text = txtRenewalInterestRate.Text;
                }

            }
        }

        private bool ValidateFDAccountDetails()
        {

            bool isFDAccount = true;
            if (fdTypes == FDTypes.IN || fdTypes == FDTypes.OP)
            {
                if (HasFDAccount() > 0 && fdAccountId.Equals((int)YesNo.No))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.ACCOUNT_NUMBER_AVAILABLE));
                    txtAccountNumber.Focus();
                    isFDAccount = false;
                    return isFDAccount;
                }

                if (glkpProDetails.EditValue == null || glkpProDetails.EditValue.ToString().Equals("0") || string.IsNullOrEmpty(glkpProDetails.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_NAME_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpProDetails);
                    isFDAccount = false;
                    glkpProDetails.Focus();
                }

                if (HasFDReInvestment() > 0)
                {
                    if (cboFDScheme.SelectedIndex == 0)
                    {
                        this.ShowMessageBox("Re-Investment is made, can not change the fd scheme");
                        isFDAccount = false;
                        cboFDScheme.SelectedIndex = 1;
                        cboFDScheme.Focus();
                    }
                }
            }

            if (glkpBranch.EditValue == null || glkpBranch.EditValue.ToString().Equals("0") || string.IsNullOrEmpty(glkpBranch.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_BANK_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpBranch);
                isFDAccount = false;
                glkpBranch.Focus();
            }

            if (isFDAccount && fdTypes == FDTypes.IN)
            {
                if (TransVoucherMethod != (int)TransactionVoucherMethod.Automatic && (string.IsNullOrEmpty(txtVoucherNo.Text) || txtVoucherNo.Text.Trim() == "0"))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                    this.SetBorderColor(txtVoucherNo);
                    isFDAccount = false;
                    txtVoucherNo.Focus();
                }
                else if (string.IsNullOrEmpty(glkpCashBankLedgers.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CASH_BANK_LEDGERS));
                    this.SetBorderColorForGridLookUpEdit(glkpCashBankLedgers);
                    isFDAccount = false;
                    glkpCashBankLedgers.Focus();
                }
                else if (string.IsNullOrEmpty(deCreatedDate.Text) || deCreatedDate.DateTime == DateTime.MinValue)
                {
                    this.ShowMessageBox("Create On is empty");
                    this.SetBorderColorForDateTimeEdit(deCreatedDate);
                    isFDAccount = false;
                    deCreatedDate.Focus();
                }
                else if (deCreatedDate.DateTime > this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                {
                    this.ShowMessageBox("Create On should be with in the transaction period");
                    this.SetBorderColorForDateTimeEdit(deCreatedDate);
                    isFDAccount = false;
                    deCreatedDate.Focus();
                }
                else if (deCreatedDate.DateTime < this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))
                {
                    this.ShowMessageBox("Create On should be with in the transaction period");
                    this.SetBorderColorForDateTimeEdit(deCreatedDate);
                    isFDAccount = false;
                    deCreatedDate.Focus();
                }
                else if (string.IsNullOrEmpty(glkpFDLedgerDetails.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FIXED_DEPOSIT_LEDGER_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpFDLedgerDetails);
                    isFDAccount = false;
                    glkpFDLedgerDetails.Focus();
                }
                else if (TransVoucherMethod != (int)TransactionVoucherMethod.Automatic)
                {
                    if (string.IsNullOrEmpty(txtVoucherNo.Text))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                        this.SetBorderColor(txtVoucherNo);
                        isFDAccount = false;
                        txtVoucherNo.Focus();
                    }
                }
                else if (string.IsNullOrEmpty(glkpCashBankLedgers.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CASH_BANK_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpCashBankLedgers);
                    isFDAccount = false;
                    glkpCashBankLedgers.Focus();
                }
                else if (!isValidProject())
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.STARTDATE_LESSTHAN_INVESTMENTDATE));
                    this.SetBorderColorForDateTimeEdit(dteRenewalOn);
                    isFDAccount = false;
                    deCreatedDate.Focus();
                }
                else if (IsVoucherDateLocked())
                {
                    this.SetBorderColorForDateTimeEdit(dteRenewalOn);
                    isFDAccount = false;
                    dteRenewalOn.Focus();
                }
                else if (IsFDAccountClosed())
                {
                    isFDAccount = false;
                    this.SetBorderColorForGridLookUpEdit(glkpCashBankLedgers);
                    glkpCashBankLedgers.Focus();
                }
            }

            if (fdTypes == FDTypes.RIN)
            {
                if (TransVoucherMethod != (int)TransactionVoucherMethod.Automatic)
                {
                    if (string.IsNullOrEmpty(txtRenewalVoucherNo.Text) || txtRenewalVoucherNo.Text.Trim() == "0")
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                        this.SetBorderColor(txtRenewalVoucherNo);
                        isFDAccount = false;
                        txtRenewalVoucherNo.Focus();
                    }
                }

                if (string.IsNullOrEmpty(dePostDate.Text.Trim()) || dePostDate.DateTime == DateTime.MinValue)
                {
                    this.ShowMessageBox("Re-Investment Date is empty");
                    this.SetBorderColorForDateTimeEdit(dteClosedOn);
                    isFDAccount = false;
                    dePostDate.Focus();
                }
                else if (dePostDate.DateTime > this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                {
                    this.ShowMessageBox("Re-Investment Date should be with in the transaction period");
                    this.SetBorderColorForDateTimeEdit(dePostDate);
                    isFDAccount = false;
                    dePostDate.Focus();
                }
                else if (dePostDate.DateTime < this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))
                {
                    this.ShowMessageBox("Re-Investment Date should be with in the transaction period");
                    this.SetBorderColorForDateTimeEdit(dePostDate);
                    isFDAccount = false;
                    dePostDate.Focus();
                }
                else if (string.IsNullOrEmpty(txtWithdrawAmount.Text.Trim()))
                {
                    this.ShowMessageBox("Re-Investment Amount is empty");
                    this.SetBorderColor(txtWithdrawAmount);
                    isFDAccount = false;
                    txtWithdrawAmount.Focus();
                }

                else if (string.IsNullOrEmpty(glkpBankInterestLedger.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CASH_BANK_LEDGERS));
                    this.SetBorderColorForGridLookUpEdit(glkpBankInterestLedger);
                    isFDAccount = false;
                    glkpBankInterestLedger.Focus();
                }

                else if (string.IsNullOrEmpty(glkpInterestLedger.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_BANK_INTEREST_LED_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpInterestLedger);
                    isFDAccount = false;
                    glkpBankInterestLedger.Focus();
                }
                //else if (dePostDate.DateTime <= this.UtilityMember.DateSet.ToDate(lblRINDate.Text, false) && (!(lblRINDate.Text.Equals("0"))))
                else if ((this.UtilityMember.DateSet.ToDate(lblRINDate.Text, false) > dePostDate.DateTime) && (!(lblRINDate.Text.Equals("0"))))
                {
                    this.ShowMessageBox("Re-Investment Date should greater than the Last Re-Investment Date");
                    isFDAccount = false;
                    dePostDate.Focus();
                    return isFDAccount;
                }
                else if (lblDateofMaturity.Visibility != LayoutVisibility.Never && this.UtilityMember.DateSet.ValidateDate(this.UtilityMember.DateSet.ToDate(this.MaturityDate, false), dePostDate.DateTime))
                {
                    this.ShowMessageBox("Re-Investment Date should be less than the Maturity Date");
                    isFDAccount = false;
                    dePostDate.Focus();
                    return isFDAccount;
                }
                else if (!(string.IsNullOrEmpty(lblAssignLastRenewedOn.Text.Trim())))
                {
                    if (lblAssignLastRenewedOn.Text != "0")
                    {
                        if (!(dePostDate.DateTime >= this.UtilityMember.DateSet.ToDate(lblAssignLastRenewedOn.Text, false)))
                        {
                            this.ShowMessageBox("Re-Investment Date should be greater than or equal to Last Renewal Date");
                            isFDAccount = false;
                            dePostDate.Focus();
                            return isFDAccount;
                        }
                    }
                }

                if ((!(dePostDate.DateTime == PostInterestCreatedDate)) && dePostDate.DateTime <= PostInterestCreatedDate)
                {
                    using (FDAccountSystem fdsystem = new FDAccountSystem())
                    {
                        fdsystem.FDAccountId = fdAccountId;
                        if (fdsystem.GetNoOfPostInterestsCount() > 0)
                        {
                            if ((!(dePostDate.DateTime == PostInterestCreatedDate)) && dePostDate.DateTime <= PostInterestCreatedDate)
                            {
                                this.ShowMessageBox("Re-Investment Date should be greater than or equal to Post Interest Date");
                                isFDAccount = false;
                                dePostDate.Focus();
                                return isFDAccount;
                            }
                        }
                    }
                }


                if (BankLedgerId != 0)
                {
                    if (BankLedgerId != this.UtilityMember.NumberSet.ToInteger(glkpBankInterestLedger.EditValue.ToString()))
                    {
                        DialogResult dialogResult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FDLedger.FD_NOT_APPROPRIATE_BANK) + System.Environment.NewLine + this.GetMessage(MessageCatalog.Master.Mapping.WANT_TO_PROCEED), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult != DialogResult.Yes)
                        {
                            isFDAccount = false;
                        }
                    }
                }
            }

            if (fdTypes == FDTypes.RN)
            {
                if (TransVoucherMethod != (int)TransactionVoucherMethod.Automatic)
                {
                    if (string.IsNullOrEmpty(txtRenewalVoucherNo.Text) || txtRenewalVoucherNo.Text.Trim() == "0")
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                        this.SetBorderColor(txtRenewalVoucherNo);
                        isFDAccount = false;
                        txtRenewalVoucherNo.Focus();
                    }
                }

                if (isFDAccount)
                {
                    if (cboInterestMode.SelectedIndex != 0)
                    {
                        /*if (txtRenewalInterestRate.Text == "0.00")
                        {
                            this.ShowMessageBox("Actual Interest amount should be greater than Zero in the Accumulated interest");
                            this.SetBorderColor(txtRenewalInterestRate);
                            isFDAccount = false;
                            txtRenewalInterestRate.Focus();
                        }*/
                    }
                    if (cboInterestMode.SelectedIndex == 0 &&
                          (glkpBankInterestLedger.EditValue == null || glkpBankInterestLedger.EditValue.ToString().Equals("0") || string.IsNullOrEmpty(glkpBankInterestLedger.Text.Trim())))
                    {
                        if (cboInterestMode.SelectedIndex == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CASH_BANK_LEDGERS));
                            this.SetBorderColorForGridLookUpEdit(glkpBankInterestLedger);
                            isFDAccount = false;
                            glkpBankInterestLedger.Focus();
                        }
                    }
                    else if (glkpInterestLedger.EditValue == null || glkpInterestLedger.EditValue.ToString().Equals("0") || string.IsNullOrEmpty(glkpInterestLedger.Text))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_BANK_INTEREST_LED_EMPTY));
                        this.SetBorderColorForGridLookUpEdit(glkpInterestLedger);
                        isFDAccount = false;
                        glkpInterestLedger.Focus();
                    }
                    else if (cboInterestMode.SelectedIndex == ((int)FDRenewalTypes.ACI) && cboTransMode.Text.ToUpper() == TransSource.Cr.ToString().ToUpper())
                    {   //On 13/12/2023, For Post Interest accmulated, to alert if FD Amount is reduced
                        if (this.ShowConfirmationMessage("Since Fixed Deposit Trans Mode is Credit, Are you sure to reduce Fixed Deposit Amount ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == System.Windows.Forms.DialogResult.No)
                        {
                            isFDAccount = false;
                            this.SetBorderColorForComboBoxEdit(cboTransMode);
                            cboTransMode.Focus();
                        }
                        txtTDSAmount.Text = "0";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dteRenewalOn.Text.Trim()))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_CREATED_DATE_EMPTY));
                            this.SetBorderColorForDateTimeEdit(dteRenewalOn);
                            isFDAccount = false;
                            dteRenewalOn.Focus();
                        }
                        else if (string.IsNullOrEmpty(dteFDMaturityDate.Text.Trim()))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_EMPTY));
                            this.SetBorderColorForDateTimeEdit(deDateOfMaturity);
                            isFDAccount = false;
                            deDateOfMaturity.Focus();
                        }
                        else if (dteRenewalOn.DateTime.Equals(DateTime.MinValue))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_CREATED_DATE_EMPTY));
                            this.SetBorderColorForDateTimeEdit(dteRenewalOn);
                            isFDAccount = false;
                            dteRenewalOn.Focus();
                        }
                        else if (dteFDMaturityDate.DateTime.Equals(DateTime.MinValue))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_EMPTY));
                            this.SetBorderColorForDateTimeEdit(dteFDMaturityDate);
                            isFDAccount = false;
                            dteFDMaturityDate.Focus();
                        }
                        else if (this.UtilityMember.DateSet.ValidateDate(dteFDMaturityDate.DateTime, dteRenewalOn.DateTime))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_LESS_THAN_CREATED_DATE));
                            this.SetBorderColorForDateTimeEdit(deDateOfMaturity);
                            isFDAccount = false;
                            deDateOfMaturity.Focus();
                        }
                        else if (dteRenewalOn.DateTime > this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_RENEWAL_DATE_WITHIN_TRANSACTION));
                            this.SetBorderColorForDateTimeEdit(dteRenewalOn);
                            isFDAccount = false;
                            dteRenewalOn.Focus();
                        }
                        else if (dteRenewalOn.DateTime < this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_RENEWAL_DATE_WITHIN_TRANSACTION));
                            this.SetBorderColorForDateTimeEdit(dteRenewalOn);
                            isFDAccount = false;
                            dteRenewalOn.Focus();
                        }

                        else if (IsVoucherDateLocked())
                        {
                            this.SetBorderColorForDateTimeEdit(deDateOfMaturity);
                            isFDAccount = false;
                            dteRenewalOn.Focus();
                        }
                        else if (IsFDAccountClosed())
                        {
                            isFDAccount = false;
                            this.SetBorderColorForDateTimeEdit(deCreatedDate);
                            deCreatedDate.Focus();
                        }
                    }
                    if (BankLedgerId != 0)
                    {
                        if (BankLedgerId != this.UtilityMember.NumberSet.ToInteger(glkpBankInterestLedger.EditValue.ToString()))
                        {
                            DialogResult dialogResult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FDLedger.FD_BANK_IS_NOT_CORRECT) + System.Environment.NewLine + "Do you want to proceed?", "AcME ERP", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dialogResult != DialogResult.Yes)
                            {
                                isFDAccount = false;
                            }
                        }
                    }
                    if (FDRenewalId == 0)
                    {
                        if (!this.UtilityMember.DateSet.ValidateDate(this.UtilityMember.DateSet.ToDate(this.MaturityDate, false), dteRenewalOn.DateTime))
                        {
                            //On 08/10/2021, To show affected date in message
                            string msg = this.GetMessage(MessageCatalog.Master.FDLedger.FD_RENEWAL_DATE_NOT_GREATER_THAN_MATURITY_DATE);
                            msg = "Renewal Date should be greater than or equal to Last Maturity Date";
                            msg += " (" + this.UtilityMember.DateSet.ToDate(this.MaturityDate.ToString()) + ")";
                            this.ShowMessageBox(msg);
                            isFDAccount = false;
                            deCreatedDate.Focus();
                            return isFDAccount;
                        }
                    }

                    //On 31/08/2022, To avoild duplication while editing the renewals too
                    //if (FDRenewalId == 0 && CheckRenewalDuplicate() > 0)
                    if (CheckRenewalDuplicate() > 0)
                    {
                        //this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.RENEWAL_MADE_ALREADY) + "  " + dteRenewalOn.DateTime.ToShortDateString());
                        string msg = "FD Renewal/Partial Withdrwal has been made for this FD Account on this Date " + dteRenewalOn.DateTime.ToShortDateString();
                        this.ShowMessageBox(msg);
                        isFDAccount = false;
                        dteRenewalOn.Focus();
                    }
                }
            }

            if (fdTypes == FDTypes.POI)
            {
                if (TransVoucherMethod != (int)TransactionVoucherMethod.Automatic && (string.IsNullOrEmpty(txtRenewalVoucherNo.Text) || txtRenewalVoucherNo.Text.Trim() == "0"))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                    this.SetBorderColor(txtRenewalVoucherNo);
                    isFDAccount = false;
                    txtRenewalVoucherNo.Focus();
                }
                else if (string.IsNullOrEmpty(dePostDate.Text) || dePostDate.DateTime == DateTime.MinValue)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.INTEREST_DATE_EMPTY));
                    this.SetBorderColorForDateTimeEdit(dePostDate);
                    isFDAccount = false;
                    dePostDate.Focus();
                }
                else if (glkpBankInterestLedger.Visible && string.IsNullOrEmpty(glkpBankInterestLedger.Text)) //On 25/10/2021, To Check Cash & bank Ledger
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CASH_BANK_LEDGERS));
                    this.SetBorderColorForGridLookUpEdit(glkpBankInterestLedger);
                    isFDAccount = false;
                    glkpCashBankLedgers.Focus();
                }
                else if (glkpInterestLedger.EditValue == null || glkpInterestLedger.EditValue.ToString().Equals("0") || string.IsNullOrEmpty(glkpInterestLedger.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_BANK_INTEREST_LED_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpInterestLedger);
                    isFDAccount = false;
                    glkpInterestLedger.Focus();
                }
                else if ((!(dePostDate.DateTime == PostInterestCreatedDate)) && dePostDate.DateTime <= PostInterestCreatedDate)
                {
                    //On 08/10/2021, To show proper message about last renewal details
                    string msg = this.GetMessage(MessageCatalog.Master.Mapping.INTEREST_DATE_IS_GREATER_THAN);
                    msg = "Post Interest Date should not be greater than Last Maturity Date.";
                    msg += System.Environment.NewLine + "Creation or Last Renewal/Post Interest on : " + UtilityMember.DateSet.ToDate(PostInterestCreatedDate.ToShortDateString());
                    msg += " and its Maturity date is " + UtilityMember.DateSet.ToDate(PostInterestMaturityDate.ToShortDateString());
                    this.SetBorderColorForDateTimeEdit(dePostDate);
                    this.ShowMessageBox(msg);
                    isFDAccount = false;
                    dePostDate.Focus();
                }

                else if ((BaseFDInvestmentType != (int)FDInvestmentType.MutualFund) && dePostDate.DateTime > PostInterestMaturityDate) //(dePostDate.DateTime >= PostInterestMaturityDate) On 31/08/2022, to pass Post Interest on maturity date
                {
                    //On 08/10/2021, To show proper message about last renewal details
                    string msg = this.GetMessage(MessageCatalog.Master.Mapping.INTEREST_DATE_IS_LESS_THAN);
                    msg += System.Environment.NewLine + "Creation or Last Renewal/Post Interest on : " + UtilityMember.DateSet.ToDate(PostInterestCreatedDate.ToShortDateString());
                    msg += " and its Maturity date is " + UtilityMember.DateSet.ToDate(PostInterestMaturityDate.ToShortDateString());
                    this.SetBorderColorForDateTimeEdit(dePostDate);
                    this.ShowMessageBox(msg);
                    isFDAccount = false;
                    dePostDate.Focus();
                }
                else if (dePostDate.DateTime > this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                {
                    this.ShowMessageBox("Post Date should be with in the transaction period");
                    this.SetBorderColorForDateTimeEdit(dePostDate);
                    isFDAccount = false;
                    dePostDate.Focus();
                }
                else if (dePostDate.DateTime < this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))
                {
                    this.ShowMessageBox("Post Date should be with in the transaction period");
                    this.SetBorderColorForDateTimeEdit(dePostDate);
                    isFDAccount = false;
                    dePostDate.Focus();
                }
                else if (txtRenewalInterestRate.Text == "0.00")
                {
                    this.ShowMessageBox("Actual Interest amount should be greater than Zero");
                    this.SetBorderColor(txtRenewalInterestRate);
                    isFDAccount = false;
                    txtRenewalInterestRate.Focus();
                }
                else if (IsVoucherDateLocked())
                {
                    this.SetBorderColor(dePostDate);
                    isFDAccount = false;
                    dePostDate.Focus();
                }
                else if (IsFDAccountClosed())
                {
                    isFDAccount = false;
                    this.SetBorderColorForDateTimeEdit(dePostDate);
                    dePostDate.Focus();
                }
                else if (cboInterestMode.SelectedIndex == ((int)FDRenewalTypes.ACI) && cboTransMode.Text.ToUpper() == TransSource.Cr.ToString().ToUpper())
                {   //On 27/10/2023, For Post Interest accmulated, to alert if FD Amount is reduced
                    if (this.ShowConfirmationMessage("Since Fixed Deposit Trans Mode is Credit, Are you sure to reduce Fixed Deposit Amount ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == System.Windows.Forms.DialogResult.No)
                    {
                        isFDAccount = false;
                        this.SetBorderColorForComboBoxEdit(cboTransMode);
                        cboTransMode.Focus();
                    }
                    txtTDSAmount.Text = "0";
                }
            }
            if ((fdTypes == FDTypes.OP || fdTypes == FDTypes.IN) && isFDAccount)
            {
                if (glkpProDetails.EditValue == null || string.IsNullOrEmpty(glkpProDetails.EditValue.ToString()) || glkpProDetails.EditValue.ToString().Equals("0"))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_NAME_EMPTY));
                    this.SetBorderColorForGridLookUpEdit(glkpProDetails);
                    isFDAccount = false;
                    glkpProDetails.Focus();
                    return isFDAccount;
                }
                else if (string.IsNullOrEmpty(txtAccountNumber.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_ACCOUNT_NUMBER_EMPTY));
                    this.SetBorderColor(txtAccountNumber);
                    isFDAccount = false;
                    txtAccountNumber.Focus();
                    return isFDAccount;
                }
                else if (string.IsNullOrEmpty(txtAmount.Text.Trim()) || txtAmount.Text == "0.00" || txtAmount.Text == "0"
                    || this.UtilityMember.NumberSet.ToDouble(txtAmount.Text) < 0)
                {
                    if (txtAmount.Text == string.Empty)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_AMOUNT_EMPTY));
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_AMOUNT_GREATER_THAN_ZERO));
                    }
                    this.SetBorderColor(txtAmount);
                    isFDAccount = false;
                    txtAmount.Focus();
                    return isFDAccount;
                }


                if (fdTypes == FDTypes.OP)
                {
                    if (deCreatedDate.DateTime >= this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CREATED_DATE_LESS_THAN_FINANCIAL_YEAR));
                        isFDAccount = false;
                        deCreatedDate.Focus();
                        return isFDAccount;
                    }
                    else if (glkpLedgers.EditValue == null || glkpLedgers.EditValue.ToString().Equals("0") || string.IsNullOrEmpty(glkpLedgers.Text))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FIXED_DEPOSIT_LEDGER_EMPTY));
                        this.SetBorderColorForGridLookUpEdit(glkpLedgers);
                        isFDAccount = false;
                        glkpLedgers.Focus();
                    }

                    else if (EntryFDInvestmentType != (int)FDInvestmentType.MutualFund && deDateOfMaturity.DateTime < this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false))
                    //if (this.UtilityMember.DateSet.ValidateDate(deDateOfMaturity.DateTime, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))) 
                    {
                        //this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_MATURITY_DATE_FINANCIAL_YEAR));
                        this.ShowMessageBox("The Maturity Date should be greater than Books Begin date for the Opening Fixed Deposit");
                        isFDAccount = false;
                        deDateOfMaturity.Focus();
                    }
                    else if (EntryFDInvestmentType != (int)FDInvestmentType.MutualFund && deDateOfMaturity.DateTime < this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false))
                    //if (this.UtilityMember.DateSet.ValidateDate(deDateOfMaturity.DateTime, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))) 
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_MATURITY_DATE_FINANCIAL_YEAR));
                        isFDAccount = false;
                        deDateOfMaturity.Focus();
                    }
                    else if ((EntryFDInvestmentType == (int)FDInvestmentType.MutualFund) &&
                        (string.IsNullOrEmpty(txtMutualFolioNo.Text) || UtilityMember.NumberSet.ToDouble(txtBaseNAVNoOfUnits.Text) == 0 || UtilityMember.NumberSet.ToDouble(txtBaseNAVPerUnit.Text) == 0))
                    {
                        this.ShowMessageBox("Fill Mutual Fund details properly.");
                        isFDAccount = false;
                        txtMutualFolioNo.Focus();
                    }
                }
            }
            if ((fdTypes == FDTypes.OP || fdTypes == FDTypes.IN) && isFDAccount)
            {
                if (string.IsNullOrEmpty(deCreatedDate.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_CREATED_DATE_EMPTY));
                    this.SetBorderColorForDateTimeEdit(deCreatedDate);
                    isFDAccount = false;
                    deCreatedDate.Focus();
                }
                else if (string.IsNullOrEmpty(deDateOfMaturity.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_EMPTY));
                    this.SetBorderColorForDateTimeEdit(deDateOfMaturity);
                    isFDAccount = false;
                    deDateOfMaturity.Focus();
                }
                else if (fdTypes == FDTypes.OP && EntryFDInvestmentType != (int)FDInvestmentType.MutualFund && deDateOfMaturity.DateTime < this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false))
                //(this.UtilityMember.DateSet.ValidateDate(deDateOfMaturity.DateTime, deCreatedDate.DateTime))
                {
                    //this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_LESS_THAN_CREATED_DATE));
                    this.ShowMessageBox("The Maturity Date should be greater than Books Begin date for the Opening Fixed Deposit");
                    this.SetBorderColorForDateTimeEdit(deDateOfMaturity);
                    isFDAccount = false;
                    deDateOfMaturity.Focus();
                    return isFDAccount;
                }
                else if (fdTypes == FDTypes.IN && EntryFDInvestmentType != (int)FDInvestmentType.MutualFund && deDateOfMaturity.DateTime < this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))
                //(this.UtilityMember.DateSet.ValidateDate(deDateOfMaturity.DateTime, deCreatedDate.DateTime))
                {
                    //this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_LESS_THAN_CREATED_DATE));
                    //this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_MATURITY_DATE_FINANCIAL_YEAR));
                    this.ShowMessageBox("The Maturity Date should be greater than Financial Year's Year From date");
                    this.SetBorderColorForDateTimeEdit(deDateOfMaturity);
                    isFDAccount = false;
                    deDateOfMaturity.Focus();
                    return isFDAccount;
                }
                else if ((EntryFDInvestmentType == (int)FDInvestmentType.MutualFund) &&
                        (string.IsNullOrEmpty(txtMutualFolioNo.Text) || UtilityMember.NumberSet.ToDouble(txtBaseNAVNoOfUnits.Text) == 0 || UtilityMember.NumberSet.ToDouble(txtBaseNAVPerUnit.Text) == 0))
                {
                    this.ShowMessageBox("Fill Mutual Fund details properly.");
                    isFDAccount = false;
                    txtMutualFolioNo.Focus();
                }
            }

            if (fdTypes == FDTypes.WD)
            {
                if (TransVoucherMethod != (int)TransactionVoucherMethod.Automatic)
                {
                    if (string.IsNullOrEmpty(txtRenewalVoucherNo.Text) || txtRenewalVoucherNo.Text == "0")
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                        this.SetBorderColor(txtVoucherNo);
                        isFDAccount = false;
                        txtRenewalVoucherNo.Focus();
                    }
                }

                if (isFDAccount && WithdrwalReceiptTransVoucherMethod != (int)TransactionVoucherMethod.Automatic)
                {
                    if (string.IsNullOrEmpty(txtWithdrwalReceiptVNo.Text) && UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text) > 0)
                    {
                        this.ShowMessageBox("Fixed Deposit withdrawal Receipt Voucher Number is empty");
                        this.SetBorderColor(txtWithdrwalReceiptVNo);
                        isFDAccount = false;
                        txtWithdrwalReceiptVNo.Focus();
                    }
                }

                if (isFDAccount)
                {
                    if (string.IsNullOrEmpty(dteClosedOn.Text.Trim()))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_WITHDRAWAL_ON_EMPTY));
                        this.SetBorderColorForDateTimeEdit(dteClosedOn);
                        isFDAccount = false;
                        dteClosedOn.Focus();
                    }
                    else if (dteClosedOn.DateTime == DateTime.MinValue)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_WITHDRAWAL_ON_EMPTY));
                        this.SetBorderColorForDateTimeEdit(dteClosedOn);
                        isFDAccount = false;
                        dteClosedOn.Focus();
                    }
                    else if (dteClosedOn.DateTime > this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                    {
                        this.ShowMessageBox("Withdrawal on should be with in the transaction period");
                        this.SetBorderColorForDateTimeEdit(dteClosedOn);
                        isFDAccount = false;
                        dteClosedOn.Focus();
                    }
                    else if (dteClosedOn.DateTime < this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false))
                    {
                        this.ShowMessageBox("Withdrawal on should be with in the transaction period");
                        this.SetBorderColorForDateTimeEdit(dteClosedOn);
                        isFDAccount = false;
                        dteClosedOn.Focus();
                    }
                    else if (string.IsNullOrEmpty(glkpBankInterestLedger.Text.Trim()))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CASH_BANK_LEDGERS));
                        this.SetBorderColorForGridLookUpEdit(glkpBankInterestLedger);
                        isFDAccount = false;
                        glkpBankInterestLedger.Focus();
                    }
                    else if (string.IsNullOrEmpty(glkpInterestLedger.Text.Trim()))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_BANK_INTEREST_LED_EMPTY));
                        this.SetBorderColorForGridLookUpEdit(glkpInterestLedger);
                        isFDAccount = false;
                        glkpBankInterestLedger.Focus();
                    }
                    else if (!this.AppSetting.DateSet.ValidateDate(this.AppSetting.DateSet.ToDate(this.RenewalDate, false), dteClosedOn.DateTime))
                    {
                        if (HasRenewal())
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_WITHDRAWAL_ON_GREATER_THAN_RENEWAL_DATE));
                            isFDAccount = false;
                            dteClosedOn.Focus();
                        }
                        //else
                        //{
                        //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FixedDeposit.FD_MATURITY_DATE_LESS_THAN_CREATED_DATE));
                        //    this.SetBorderColorForDateTimeEdit(deDateOfMaturity);
                        //    isFDAccount = false;
                        //    deDateOfMaturity.Focus();
                        //}
                    }
                    else if (IsVoucherDateLocked())
                    {
                        isFDAccount = false;
                        dteClosedOn.Focus();
                    }
                    else if (IsFDAccountClosed())
                    {
                        isFDAccount = false;
                        this.SetBorderColorForGridLookUpEdit(glkpBankInterestLedger);
                        glkpBankInterestLedger.Focus();
                    }

                    //if (FDRenewalId == 0 && CheckRenewalDuplicate() > 0)
                    //{
                    //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.RENEWAL_MADE_ALREADY) + "  " + dteClosedOn.DateTime.ToShortDateString());
                    //    isFDAccount = false;
                    //    dteRenewalOn.Focus();
                    //    return isFDAccount;
                    //}

                    if (BankLedgerId != 0)
                    {
                        if (BankLedgerId != this.UtilityMember.NumberSet.ToInteger(glkpBankInterestLedger.EditValue.ToString()))
                        {
                            DialogResult dialogResult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FDLedger.FD_NOT_APPROPRIATE_BANK) + System.Environment.NewLine + this.GetMessage(MessageCatalog.Master.Mapping.WANT_TO_PROCEED), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dialogResult != DialogResult.Yes)
                            {
                                isFDAccount = false;
                            }
                        }
                    }
                    //Validate amount exceeds to principal amount.
                    if (IsExceedsthePrincipalAmount())
                    {
                        isFDAccount = false;
                        txtWithdrawAmount.Focus();
                    }
                }
            }


            if (fdTypes == FDTypes.IN && isFDAccount)
            {
                if (TransMode == TransactionMode.CR.ToString())
                {
                    DialogResult dialogResult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FDLedger.FD_BANK_CASH_GOES_CREDIT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult != DialogResult.Yes)
                    {
                        isFDAccount = false;
                    }
                }
            }

            /// by aldrin this delete action is moved to FDAccountSystem. To bring this delete action under one begin and commit.
            /// 
            //if (FDAccountId > 0 && txtRenewalInterestRate.Text == "0.00")
            //{
            //    if (!DeleteFDVouchers())
            //    {
            //        isFDAccount = false;
            //    }
            //}

            // On 14/08/2019, Validate TDSamount without interest amount
            if (isFDAccount && UtilityMember.NumberSet.ToDouble(txtTDSAmount.Text) > 0 && UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text) == 0)
            {
                this.ShowMessageBox("Can't process as TDS amount without Interest Amount");
                isFDAccount = false;
                txtTDSAmount.Focus();
            }
            else if (isFDAccount && cbPenaltyMode.SelectedIndex > 0 && lcPenaltyAmount.Visibility == LayoutVisibility.Always && UtilityMember.NumberSet.ToDouble(txtPenaltyAmount.Text) > 0)
            {
                if (glkpPenaltyLedger.EditValue == null || this.UtilityMember.NumberSet.ToInteger(glkpPenaltyLedger.EditValue.ToString()) == 0)
                {
                    this.ShowMessageBox("Penalty Ledger is empty");
                    isFDAccount = false;
                    this.SetBorderColorForGridLookUpEdit(glkpPenaltyLedger);
                    glkpPenaltyLedger.Focus();
                }
                else
                {
                    using (FDAccountSystem fdsystem = new FDAccountSystem())
                    {
                        Int32 penaltyledgerid = glkpPenaltyLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpPenaltyLedger.EditValue.ToString()) : 0;
                        ResultArgs result = fdsystem.ValidatePenaltyAmount(cbPenaltyMode.SelectedIndex, UtilityMember.NumberSet.ToDouble(txtPenaltyAmount.Text),
                                                 penaltyledgerid, UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text), UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text));
                        if (!result.Success)
                        {
                            this.ShowMessageBox(result.Message);
                            isFDAccount = false;
                            this.SetBorderColor(txtPenaltyAmount);
                            txtPenaltyAmount.Focus();
                        }
                    }
                }
            }

            return isFDAccount;
        }

        /// <summary>
        /// This is to check wheather that FD account has some renewals or not.
        /// To get the renewals,use the renewl_id and FD_account_id to the 'fdRnewal' table
        /// </summary>
        /// <returns></returns>
        /// <params>FD_account_id</params>
        /// 
        private bool HasRenewal()
        {
            bool HasRenewalSuccess = false;
            if (dtRenewal != null && dtRenewal.Rows.Count != 0)
            {
                foreach (DataRow dr in dtRenewal.Rows)
                {
                    if (this.UtilityMember.NumberSet.ToInteger(dr["FD_ACCOUNT_ID"].ToString()) == FDAccountId && dr["RENEWAL_TYPE"].ToString() != FDTypes.PWD.ToString())
                    {
                        HasRenewalSuccess = true;
                        break;
                    }
                }
            }
            return HasRenewalSuccess;
        }
        /// <summary>
        /// This is to check wheather FDaccountId has FDAccountNumber to check the Duplication 	of FDAccountNumber.
        /// </summary>
        /// <returns></returns>
        /// <params>FdAccountNumber</params>
        /// 
        private int HasFDAccount()
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountNumber = txtAccountNumber.Text;
                return fdAccountSystem.HasFDAccount();
            }
        }

        /// <summary>
        /// This is to check Has FD Account while change the FD Flexi to FD 
        /// </summary>
        /// <returns></returns>
        private int HasFDReInvestment()
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = fdAccountId;
                return fdAccountSystem.HasFDReInvestment();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        private void LoadDefaults()
        {
            PreviousInterestType = -1;

            //If it is not any of the such as (RN/WD/POI),project will be filled in the combo..
            //Otherwise project will be assigned to the Textbox based on the "projectId" Property
            if (fdTypes != FDTypes.RN && fdTypes != FDTypes.WD && fdTypes != FDTypes.PWD && fdTypes != FDTypes.POI && fdTypes != FDTypes.RIN)
            {
                LoadProject();
            }

            //On 15/10/2024, To load multi currency
            lcgCurrencyDetails.Visibility = LayoutVisibility.Never;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                LoadCountryCurrency();
                lcgCurrencyDetails.Visibility = LayoutVisibility.Always;
                lcActualAmount.Visibility = LayoutVisibility.Never;
                txtCurrencyAmount.Enabled = false;
                lcCurrencyAmount.Visibility = LayoutVisibility.Never;
            }
            LoadBank();
            if (fdTypes == FDTypes.IN)
            {
                LoadCashBankLedger(glkpCashBankLedgers);
                // FetchVoucherMethod();
            }
            FillFDAccountDetails();
            if (fdTypes == FDTypes.OP || fdTypes == FDTypes.IN)
            {
                if (fdAccountId == 0)
                {
                    //chinna
                    // 06.10.2017
                    //lblMaturityDate.Text = deDateOfMaturity.DateTime != DateTime.MinValue.Date ? deDateOfMaturity.Text + " : " + this.UtilityMember.NumberSet.ToCurrency(0) : " : " + this.UtilityMember.NumberSet.ToCurrency(0);
                }
            }
            //This is to assign values only for RN/WD/POI
            if (fdTypes == FDTypes.RN || fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD || fdTypes == FDTypes.POI || fdTypes == FDTypes.RIN)
            {
                if (fdTypes == FDTypes.RIN)
                {
                    FetchLeder(glkpInterestLedger);
                }
                else
                {
                    LoadLedger(glkpInterestLedger);
                }

                //On 23/05/2022, Load Widthdrwal Penalty Ledgers ---------
                lcPenaltyMode.Visibility = lcPenaltyAmount.Visibility = lcPenaltyLedger.Visibility = LayoutVisibility.Never;
                if ((fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)) //this.AppSetting.AllowMultiCurrency==0 && 
                {
                    //On 24/05/2022
                    LoadAllGeneralLedger(glkpPenaltyLedger);

                    //Hide Penalty details
                    lcPenaltyMode.Visibility = lcPenaltyAmount.Visibility = lcPenaltyLedger.Visibility = LayoutVisibility.Always;
                }
                //-----------------------------------------

                LoadCashBankLedger(glkpBankInterestLedger);
                if (fdAccountId != 0 && FDRenewalId == 0 && this.FDVoucherId == 0)
                {
                    lblAssignLastRenewedOn.Text = "   ";
                    lblAssignMaturedon.Text = " ";
                    //On 01/11/2023, To Show latest matured on for POI also
                    //lblLastMaturedon.Text = fdTypes == FDTypes.POI ? lblLastMaturedon.Text = " " : lblLastMaturedon.Text;

                    //09.10.2017 .. chinna
                    if (fdTypes != FDTypes.POI)
                    {
                        lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(ExpectedMaturityInterestAmount.ToString()));
                    }
                    if (fdTypes == FDTypes.POI)
                    {
                        lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(0);
                        lblIntrestAmount.Text = "Expected Interest Amount";
                    }
                    //lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(IntrestCalculatedAmount.ToString()));
                    lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(0);
                    //On 28/10/2022
                    txtRenewalInterestRate.Text = "0"; //txtRenewalInterestRate.Text = Math.Round(IntrestCalculatedAmount, 2).ToString();

                    lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount);
                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount) + " " + TransactionMode.DR.ToString();
                    lblAssignPriniciapalintrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(InvestedAmount);
                    lblRINAmount.Text = this.UtilityMember.NumberSet.ToCurrency(ReInvestedAmount);
                    glkpBranch.EditValue = FDBankID;
                    if (BankLedgerId != 0)
                        glkpBankInterestLedger.EditValue = BankLedgerId;
                    lblBankName.Text = string.IsNullOrEmpty(BankName) ? "0" : BankName;
                    lblAssignProject.Text = string.IsNullOrEmpty(FdProjectName) ? "0" : FdProjectName;
                    lblAssignFDLedger.Text = string.IsNullOrEmpty(LedgerName) ? "0" : LedgerName;
                    lblAssignAccountNumber.Text = string.IsNullOrEmpty(FDAccountNumber) ? "0" : FDAccountNumber;
                    if (fdTypes == FDTypes.RN || fdTypes == FDTypes.POI)
                    {
                        FetchAccumulatedAmount();
                    }
                    dteRenewalOn.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                    dteRenewalOn.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);

                    if (fdTypes == FDTypes.RIN)
                    {
                        dePostDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                        dePostDate.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                        glkpInterestLedger.EditValue = LedgerId;
                        glkpInterestLedger.Enabled = false;
                    }
                    GetLastRenewalDate();
                    GetLastFDReInvestmentDate();

                    //On 01/11/2023, To Show latest matured on for POI also
                    lblAssignMaturedon.Text = !string.IsNullOrEmpty(MaturityDate) ? this.UtilityMember.DateSet.ToDate(MaturityDate.ToString()) : string.Empty;
                    /*if (fdTypes != FDTypes.POI)
                    {
                        //lblAssignMaturedon.Text = MaturityDate != string.Empty ? this.UtilityMember.DateSet.ToDate(MaturityDate.ToString()) : string.Empty;
                        lblAssignMaturedon.Text = !string.IsNullOrEmpty(MaturityDate) ? this.UtilityMember.DateSet.ToDate(MaturityDate.ToString()) : string.Empty;
                    }*/
                }
                else if (FDVoucherId != 0)
                {
                    if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)
                        this.Text = this.GetMessage(MessageCatalog.Master.Mapping.WITHDRAW);
                    else if (fdTypes == FDTypes.POI)
                    {
                        this.Text = this.GetMessage(MessageCatalog.Master.FDRenewal.FDPOST_INTEREST_MODIFY);
                        // This is to assign the Post Interest Created Date -24.08.2018
                        dePostDate.DateTime = PostInterestCreatedDate;
                    }
                    else if (fdTypes == FDTypes.RIN)
                    {
                        this.Text = "FD Re-Investment";
                    }
                    else
                        this.Text = this.GetMessage(MessageCatalog.Master.FDRenewal.FD_RENEWAL_MODIFY);
                    AssignTransactionDetails();
                    GetLastFDRenewalDate();
                    GetLastFDReInvestmentDate();
                }
                else
                {
                    //To assign values for Renewal(Modify)
                    AssignValues();
                    GetLastFDRenewalDate();
                    GetLastFDReInvestmentDate();
                }
                // by aldrin to set min and max value to the renewal date
                dteRenewalOn.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                dteRenewalOn.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);

                if (fdTypes == FDTypes.RIN)
                {
                    dePostDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                    dePostDate.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                    if (lblAssignMaturedon.Text == "01/01/0001 00:00:00" || lblAssignMaturedon.Text == "0")
                    {
                        lblAssignMaturedon.Text = UtilityMember.DateSet.ToDate(PostInterestMaturityDate.ToShortDateString(), false).ToShortDateString();
                    }
                    glkpInterestLedger.Enabled = false;

                }
            }
            if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)
            {
                cboInterestMode.Properties.Items.Clear();
                cboInterestMode.Properties.Items.Add("Withdrawal");
                cboInterestMode.SelectedIndex = 0;
                cboInterestMode.Enabled = false;
                FetchAccumulatedAmount();

                //if (WithdrawAmount > 0)
                //{
                //    txtWithdrawAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(WithdrawAmount.ToString()));
                //}
                //else
                //{
                //    txtWithdrawAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(FDAmount.ToString()));
                //}
                txtWithdrawAmount.Text = WithdrawAmount > 0 ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(WithdrawAmount.ToString())) : this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(FDAmount.ToString()));

                dteClosedOn.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                this.dteClosedOn.EditValueChanged -= new System.EventHandler(this.dteClosedOn_EditValueChanged);
                if (FDVoucherId != 0)
                {
                    dteClosedOn.DateTime = fdAccountId != 0 && FDVoucherId == 0 ? this.UtilityMember.DateSet.ToDate(this.MaturityDate, false).AddDays(1) : this.UtilityMember.DateSet.ToDate(this.MaturityDate, false); //-- aldrin
                }

                CalculateWithdrawInsAmount();//added by sugan to calculate the interest amount for partial withdraw
                dteClosedOn.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                this.dteClosedOn.EditValueChanged += new System.EventHandler(this.dteClosedOn_EditValueChanged);
            }

            if (fdTypes == FDTypes.RIN)
            {
                cboInterestMode.Properties.Items.Clear();
                cboInterestMode.Properties.Items.Add("Re-Investment");
                cboInterestMode.SelectedIndex = 0;
                cboInterestMode.Enabled = false;
            }

            using (FDAccountSystem fdsystem = new FDAccountSystem(fdAccountId))
            {
                int fdscheme = fdsystem.FDScheme;
                lblNoofRINCaption.Visibility = lblNoofRIN.Visibility = lblRINDateCaption.Visibility =
                    lblRINDate.Visibility = lblRINAmount.Visibility = lblRINAmt.Visibility = (this.UIAppSetting.EnableFlexiFD == "1" && fdscheme == 1 ? LayoutVisibility.Always : LayoutVisibility.Never);

                BaseFDInvestmentType = fdsystem.InvestmentTypeId;

                //On  10/05/2024, To lock the properties only for Mutual Fund
                if ((fdTypes == FDTypes.POI || fdTypes == FDTypes.WD || fdTypes == FDTypes.POI || fdTypes == FDTypes.RIN) && BaseFDInvestmentType == (Int32)FDInvestmentType.MutualFund)
                {
                    lblIntrestAmount.Visibility = LayoutVisibility.Never;
                    lblFDRenewalIntrestAmount.Visibility = LayoutVisibility.Never;

                    lblLastMaturedon.Visibility = LayoutVisibility.Never;
                    lblAssignMaturedon.Visibility = LayoutVisibility.Never;
                }
            }

            //On 24/05/2022, Load Widthdrwal Penalty Ledgers ---------
            if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)
            {
                glkpPenaltyLedger.EditValue = ChargeLedgerId;

                lblAssignPrinicipalAmount.Text = UtilityMember.NumberSet.ToCurrency((FDAmount - WithdrawAmount));

            }
            //-----------------------------------------

            //On 16/10/2024, To set currency amount
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                if (FDIntrestVoucherId > 0 || FDVoucherId > 0 || VoucherId > 0)
                {
                    Int32 vid = (FDVoucherId > 0 ? FDVoucherId : (VoucherId > 0 ? VoucherId : FDIntrestVoucherId));

                    if (vid > 0)
                    {
                        using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem(vid))
                        {
                            txtExchangeRate.Text = vsystem.ExchangeRate.ToString();
                            txtExchangeRate.Tag = vsystem.ExchangeRate.ToString();
                            txtCurrencyAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(vsystem.ContributionAmount.ToString())).ToString();
                            txtActualAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(vsystem.ActualAmount.ToString())).ToString();
                            lblCalculatedAmt.Text = vsystem.ActualAmount.ToString();

                        }
                    }
                }
            }

            //On 17/10/2024, To show fd details balance based on currency
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ShowFDAmountDetailInCurrency();
            }

            FetchVoucherMethod();
        }
        /// <summary>
        /// This is to fetch the Accumlatedamount of FD account/Current Prinicipal Amount of FD 	Account while Renewal/Withdrawal/PostInterest.
        /// Steps: Fetch Prinicpal amount with FD_acount_id from "fdaccount" table
        /// 2. FetchAccumulatedInterestAmunt from 'fdrenewal' table 
        /// 3.add the Prinicplaamount with AccumulatedInterestAmount to get the Current Prinicipal Amount
        /// </summary>
        /// <params>FD_ACCOUNT_ID</params>
        /// 
        private void FetchAccumulatedAmount()
        {
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = fdAccountId;

                AcutalAccumulatedInsAmount = FDAccumulatedPrincipalAmount = fdAccountSystem.FetchAccumulatedAmount();
                lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAccumulatedPrincipalAmount);
                if ((fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD) && TempPrincipalAmount > 0 && WithdrawAmount > 0)
                {
                    lblAssignPrinicipalAmount.Text = WithdrawAmount.Equals(TempPrincipalAmount) && WithdrawAmount > 0 && TempPrincipalAmount > 0 ? this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount - WithdrawAmount) : WithdrawAmount > TempPrincipalAmount ? this.UtilityMember.NumberSet.ToCurrency(TempPrincipalAmount + AcutalAccumulatedInsAmount - WithdrawAmount) : this.UtilityMember.NumberSet.ToCurrency(TempPrincipalAmount - WithdrawAmount);
                }
                DataTable dtCurrentPrincipalAmount = fdAccountSystem.FetchCurrentPrincipalAmount(FDAccountId.ToString(), MaturityDate).DataSource.Table;
                if ((fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD || fdTypes == FDTypes.RIN) && FDAccountId != 0 && FDRenewalId != 0 && TempPrincipalAmount > 0 && WithdrawAmount > 0)
                {
                    if (dtCurrentPrincipalAmount != null && dtCurrentPrincipalAmount.Rows.Count > 0)
                    {
                        InvestmentAmount = this.UtilityMember.NumberSet.ToDouble(dtCurrentPrincipalAmount.Rows[0]["INVESTED AMOUNT"].ToString());
                        lblRINAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(dtCurrentPrincipalAmount.Rows[0]["REINVESTED_AMOUNT"].ToString()));
                        ReInvestedAmount = this.UtilityMember.NumberSet.ToDouble(dtCurrentPrincipalAmount.Rows[0]["REINVESTED_AMOUNT"].ToString());
                        lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(AcutalAccumulatedInsAmount);
                        Withdrawals = this.UtilityMember.NumberSet.ToDouble(dtCurrentPrincipalAmount.Rows[0]["WITHDRAWALS"].ToString());
                        if (dtCurrentPrincipalAmount.Rows[0]["PRINCIPAL"].ToString() != "0")
                        {
                            lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(dtCurrentPrincipalAmount.Rows[0]["PRINCIPAL"].ToString()));
                        }
                        else
                        {
                            lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(TempPrincipalAmount);
                        }
                    }
                }
                lblAssignTotalAmt.Text = lblAssignPrinicipalAmount.Text + " " + TransactionMode.DR.ToString();

                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    lblAssignIntrestamount.Text = lblCurrencySymbol.Text + " " + this.UtilityMember.NumberSet.ToNumber(FDAccumulatedPrincipalAmount);
                }

                //using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                //{
                //    fdAccountSystem.FDAccountId = fdAccountId;
                //    AcutalAccumulatedInsAmount = FDAccumulatedPrincipalAmount = fdAccountSystem.FetchAccumulatedAmount();
                //    lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAccumulatedPrincipalAmount);
                //    if ((fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD) && TempPrincipalAmount > 0 && WithdrawAmount > 0)
                //    {
                //        lblAssignPrinicipalAmount.Text = WithdrawAmount.Equals(TempPrincipalAmount) && WithdrawAmount > 0 && TempPrincipalAmount > 0 ? this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount - WithdrawAmount) : WithdrawAmount > TempPrincipalAmount ? this.UtilityMember.NumberSet.ToCurrency(TempPrincipalAmount + AcutalAccumulatedInsAmount - WithdrawAmount) : this.UtilityMember.NumberSet.ToCurrency(TempPrincipalAmount - WithdrawAmount);
                //    }
                //    //***********************
                //    //added by sugan--to load the current principal amount
                //    DataTable dtCurrentPrincipalAmount = fdAccountSystem.FetchCurrentPrincipalAmount(FDAccountId.ToString(), MaturityDate).DataSource.Table;
                //    if ((fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD || fdTypes == FDTypes.RIN) && FDAccountId != 0 && FDRenewalId != 0 && TempPrincipalAmount > 0 && WithdrawAmount > 0)
                //    {
                //        if (dtCurrentPrincipalAmount != null && dtCurrentPrincipalAmount.Rows.Count > 0)
                //        {
                //            InvestmentAmount = this.UtilityMember.NumberSet.ToDouble(dtCurrentPrincipalAmount.Rows[0]["INVESTED AMOUNT"].ToString());
                //            Withdrawals = this.UtilityMember.NumberSet.ToDouble(dtCurrentPrincipalAmount.Rows[0]["WITHDRAWALS"].ToString());
                //            if (dtCurrentPrincipalAmount.Rows[0]["AMOUNT"].ToString() != "0")
                //            {
                //                lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(dtCurrentPrincipalAmount.Rows[0]["AMOUNT"].ToString()));
                //            }
                //            else
                //            {
                //                lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(TempPrincipalAmount);
                //            }
                //        }
                //    }
                //    //***********************
                //    lblAssignTotalAmt.Text = lblAssignPrinicipalAmount.Text + " " + TransactionMode.DR.ToString();
                //}
            }
        }

        private void SetTitle()
        {
            //On 30/09/2022, hide
            //lcItemInfo.Visibility = LayoutVisibility.Never;
            if (fdTypes == FDTypes.OP)
            {
                this.Text = fdAccountId == 0 ? this.GetMessage(MessageCatalog.Master.FDLedger.FD_ACCOUNT_ADD) : (fdTypes == FDTypes.RN) ? this.GetMessage(MessageCatalog.Master.FDRenewal.FD_RENEWAL) : this.GetMessage(MessageCatalog.Master.FDLedger.FD_ACCOUNT_EDIT);
                lciPostDate.Visibility = LayoutVisibility.Never;
                lcgRenewalMasterDetails.Visibility = lcgRenewalDetails.Visibility = lblFDRenewalDetails.Visibility = LayoutVisibility.Never;
                lcVoucherNo.Visibility = LayoutVisibility.Never;
                emptySpaceItem1.Visibility = lblLedgerFrom.Visibility = lblFDLedgerTo.Visibility = LayoutVisibility.Never;
                lblCashBankLedgers.Visibility = lblCashBankLedgerAmt.Visibility = LayoutVisibility.Never;
                lblFDLedgerBal.Visibility = lblLedgerCurAmt.Visibility = LayoutVisibility.Never;
                lblClosingAmount.Visibility = LayoutVisibility.Never;
                lblFDRenewalDetails.Visibility = LayoutVisibility.Never;
                lblLedgerName.Visibility = lblProject.Visibility = lblLedgerAmt.Visibility = lblLedgerBal.Visibility = LayoutVisibility.Always;
                lcInvestmentType.Visibility = LayoutVisibility.Never;
                emptySpaceItem25.Visibility = LayoutVisibility.Never;

            }
            else if (fdTypes == FDTypes.IN)
            {
                this.Text = fdAccountId == 0 ? this.GetMessage(MessageCatalog.Master.FDLedger.FD_INVESTMENT_ADD) : this.GetMessage(MessageCatalog.Master.FDLedger.FD_INVESTMENT_EDIT);
                lciPostDate.Visibility = LayoutVisibility.Never;
                lcgRenewalMasterDetails.Visibility = lcgRenewalDetails.Visibility = lblFDRenewalDetails.Visibility = LayoutVisibility.Never;
                lblLedgerName.Visibility = LayoutVisibility.Never;
                lblClosingAmount.Visibility = LayoutVisibility.Never;
                lblOpDateAsOn.Visibility = lblCurrentDate.Visibility = emptyOpeningBalance.Visibility = LayoutVisibility.Never;
                lblLedgerAmt.Visibility = lblLedgerBal.Visibility = LayoutVisibility.Never;
                deCreatedDate.Properties.MinValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                deCreatedDate.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                lcInvestmentType1.Visibility = LayoutVisibility.Never;
                emptySpaceItem26.Visibility = LayoutVisibility.Never;
            }
            else if (fdTypes == FDTypes.RIN)
            {
                this.Text = "FD Re-Investment";
                lblClosingAmount.Text = "Re-Investment Amount <color=red>*</color>";
                lblClosingAmount.TextSize = new System.Drawing.Size(125, lblClosingAmount.TextSize.Height);
                glkpInterestLedger.Enabled = false;
                lblCapInterestLedger.Text = "By / Dr <color=red>*</color>";
                lblCapBankInterestLedger.Text = "To / Cr <color=red>*</color>";

                lciPostDate.Visibility = LayoutVisibility.Always;
                lblClosedOn.Visibility = LayoutVisibility.Never;

                lciTDSAmount.Visibility = LayoutVisibility.Never;
                lblRenewalIntrestAmount.Visibility = LayoutVisibility.Never;
                lblIntrestAmount.Visibility = LayoutVisibility.Never;
                lblFDRenewalIntrestAmount.Visibility = LayoutVisibility.Never;

                //lciPostDate.Visibility = LayoutVisibility.Never;
                emptyOpeningBalance.Visibility = LayoutVisibility.Never;
                lblOpDateAsOn.Visibility = LayoutVisibility.Never;
                lblCurrentDate.Visibility = LayoutVisibility.Never;
                lblFDNo.Visibility = LayoutVisibility.Never;
                emptySpaceItem6.Visibility = LayoutVisibility.Never;
                lblAmount.Visibility = LayoutVisibility.Never;
                //emptySpaceItem8.Visibility = emptySpaceItem29.Visibility = LayoutVisibility.Never;
                lblTransMode.Visibility = LayoutVisibility.Never;
                lblDateofMaturity.Visibility = LayoutVisibility.Never;
                emptySpaceItem7.Visibility = LayoutVisibility.Never;
                lblInterestRate.Visibility = LayoutVisibility.Never;
                lblExpectedMaturityAmountValue.Visibility = LayoutVisibility.Never;
                lblFDAccountNo.Visibility = lblAccountHolder.Visibility = lblFDScheme.Visibility = LayoutVisibility.Never;
                lblFDRenewalDetails.Visibility = lcgFDOpDetails.Visibility = LayoutVisibility.Never;
                lblBranch.Visibility = LayoutVisibility.Never;
                lcgFDAccountDetails.Visibility = LayoutVisibility.Never;
                //lblTotalAmount.Visibility = lblAssignTotalAmt.Visibility = LayoutVisibility.Never;
            }

            else if (FDTypes.WD == fdTypes || FDTypes.PWD == fdTypes)
            {
                this.Text = this.GetMessage(MessageCatalog.Master.FDLedger.FD_WITHDRAWAL);
                lciPostDate.Visibility = LayoutVisibility.Never;
                emptyOpeningBalance.Visibility = LayoutVisibility.Never;
                lblOpDateAsOn.Visibility = LayoutVisibility.Never;
                lblCurrentDate.Visibility = LayoutVisibility.Never;
                lblFDNo.Visibility = LayoutVisibility.Never;
                emptySpaceItem6.Visibility = LayoutVisibility.Never;
                lblAmount.Visibility = LayoutVisibility.Never;
                //emptySpaceItem8.Visibility = emptySpaceItem29.Visibility = LayoutVisibility.Never;
                lblTransMode.Visibility = LayoutVisibility.Never;
                lblDateofMaturity.Visibility = LayoutVisibility.Never;
                emptySpaceItem7.Visibility = LayoutVisibility.Never;
                lblInterestRate.Visibility = LayoutVisibility.Never;
                lblExpectedMaturityAmountValue.Visibility = LayoutVisibility.Never;
                // 06.10.2016
                //chinna
                //lblMaturityDate.Visibility = LayoutVisibility.Never;
                lblFDAccountNo.Visibility = lblAccountHolder.Visibility = lblFDScheme.Visibility = LayoutVisibility.Never;
                lblFDRenewalDetails.Visibility = lcgFDOpDetails.Visibility = LayoutVisibility.Never;
                lblBranch.Visibility = LayoutVisibility.Never;
                lcgFDAccountDetails.Visibility = LayoutVisibility.Never;
                lblTotalAmount.Visibility = lblAssignTotalAmt.Visibility = LayoutVisibility.Never;
                lblNoofRIN.Visibility = lblRINDate.Visibility = lblNoofRINCaption.Visibility = lblRINDateCaption.Visibility = LayoutVisibility.Never; // this is to unvisible the Labels
                //dteClosedOn.DateTime = DateTime.Now;
                lcItemInfo.Visibility = LayoutVisibility.Always;
            }
            else if (FDTypes.POI == fdTypes)
            {
                this.Text = fdAccountId != 0 && FDRenewalId == 0 ? this.GetMessage(MessageCatalog.Master.FDPostInterest.FD_POSTINTEREST) : this.GetMessage(MessageCatalog.Master.Mapping.FD_POST_INTEREST_MODIFY);
                lblRenewalType.Text = this.GetMessage(MessageCatalog.Master.FDPostInterest.FD_POSTINTEREST) + " <color=red>*</color>";
                this.Width = 900;
                lciPostDate.Visibility = LayoutVisibility.Always;
                simpleLabelItem3.Text = this.GetMessage(MessageCatalog.Master.Mapping.NO_OF_POSTS);
                lblLastRenewedOn.Text = this.GetMessage(MessageCatalog.Master.Mapping.LAST_POST_ON);

                lblFDRenewalDetails.Visibility = LayoutVisibility.Never;
                lblFDRenewalOn.Visibility = LayoutVisibility.Never;
                lblFDReceiptNo.Visibility = LayoutVisibility.Never;
                layoutControlItem2.Visibility = LayoutVisibility.Never;
                lblFDInterestRate.Visibility = LayoutVisibility.Never;
                lblFDDateOfMaturity.Visibility = LayoutVisibility.Never;
                //chinna
                //06.10.2017
                //simpleLabelItem2.Visibility = LayoutVisibility.Never;
                //lblAssignFDMaturedOn.Visibility = LayoutVisibility.Never;
                ExpectedRenewalMaturityValue.Visibility = lcRenewalExpectedInterest.Visibility = LayoutVisibility.Never;

                lcgFDAccountDetails.Visibility = LayoutVisibility.Never;

                lcgFDOpDetails.Visibility = LayoutVisibility.Never;
                //emptySpaceItem8.Visibility = emptySpaceItem29.Visibility = LayoutVisibility.Never;
                lblClosedOn.Visibility = LayoutVisibility.Never;
                lblAmount.Visibility = lblTransMode.Visibility = emptySpaceItem16.Visibility = LayoutVisibility.Never;
                lblFDAccountNo.Visibility = lblAccountHolder.Visibility = lblFDScheme.Visibility = emptySpaceItem19.Visibility = emptySpaceItem17.Visibility = LayoutVisibility.Never;
                emptyOpeningBalance.Visibility = lblOpDateAsOn.Visibility = lblCurrentDate.Visibility = LayoutVisibility.Never;
                lblClosingAmount.Visibility = LayoutVisibility.Never;
                lblBranch.Visibility = LayoutVisibility.Never;
                lcgGroupCaption.Visibility = LayoutVisibility.Never;
                emptySpaceItem16.Visibility = LayoutVisibility.Always;
                lcgGroupCaption.TextVisible = lblFDRenewalDetails.TextVisible = txtAmount.Enabled = false;
                lblFDRenewalDetails.GroupBordersVisible = false;
                lblNoofRIN.Visibility = lblRINDate.Visibility = lblNoofRINCaption.Visibility = lblRINDateCaption.Visibility = LayoutVisibility.Never; // this is to unvisible the Labels
                LoadPostDate();//This is to load the load the post date in "Post Interest" form, based on the FD Account Created/Renewal on date if it has renewals.
                lcItemInfo.Visibility = LayoutVisibility.Always;
            }
            else
            {
                this.Text = fdAccountId != 0 && FDRenewalId == 0 ? this.GetMessage(MessageCatalog.Master.FDRenewal.FD_RENEWAL) : this.GetMessage(MessageCatalog.Master.FDRenewal.FD_RENEWAL_MODIFY);

                lciPostDate.Visibility = LayoutVisibility.Never;

                lblFDRenewalDetails.Visibility = LayoutVisibility.Always;
                lblFDRenewalOn.Visibility = LayoutVisibility.Always;
                lblFDReceiptNo.Visibility = LayoutVisibility.Always;
                layoutControlItem2.Visibility = LayoutVisibility.Always;
                lblFDInterestRate.Visibility = LayoutVisibility.Always;
                lblFDDateOfMaturity.Visibility = LayoutVisibility.Always;

                //chinna
                //06.10.2017
                //simpleLabelItem2.Visibility = LayoutVisibility.Always;
                //lblAssignFDMaturedOn.Visibility = LayoutVisibility.Always;
                ExpectedRenewalMaturityValue.Visibility = lcRenewalExpectedInterest.Visibility = LayoutVisibility.Always;
                lcgFDAccountDetails.Visibility = LayoutVisibility.Always;

                lcgFDOpDetails.Visibility = LayoutVisibility.Never;
                //emptySpaceItem8.Visibility = emptySpaceItem29.Visibility = LayoutVisibility.Never;
                lblClosedOn.Visibility = LayoutVisibility.Never;
                lblAmount.Visibility = lblTransMode.Visibility = emptySpaceItem16.Visibility = LayoutVisibility.Never;
                lblFDAccountNo.Visibility = lblAccountHolder.Visibility = lblFDScheme.Visibility = emptySpaceItem19.Visibility = emptySpaceItem17.Visibility = LayoutVisibility.Never;
                emptyOpeningBalance.Visibility = lblOpDateAsOn.Visibility = lblCurrentDate.Visibility = LayoutVisibility.Never;
                lblClosingAmount.Visibility = LayoutVisibility.Never;
                lblBranch.Visibility = LayoutVisibility.Never;
                lcgGroupCaption.Visibility = LayoutVisibility.Never;
                emptySpaceItem16.Visibility = LayoutVisibility.Always;
                lcgGroupCaption.TextVisible = lblFDRenewalDetails.TextVisible = txtAmount.Enabled = false;
                lcgFDAccountDetails.Text = this.GetMessage(MessageCatalog.Master.FDRenewal.FD_RENEWAL_DETAILS);
                lblFDRenewalDetails.GroupBordersVisible = false;
                lcItemInfo.Visibility = LayoutVisibility.Always;
            }
            LoadCurrentDate();//To load the Next renewal date and Maturity date while renewal the FD Account.If no renewals
            //exists renewal on date will be updated based on the Account maturity date.

            //28/11/2018, to lock reinvestment feature based on setting
            if (lblFDScheme.Visibility == LayoutVisibility.Always && (this.UIAppSetting.EnableFlexiFD != "1"))
            {
                lblFDScheme.Visibility = LayoutVisibility.Never;
            }

            //On 06/11/2024
            emptyDateSpace.Visibility = LayoutVisibility.Never;
            if (lblClosedOn.Visibility == LayoutVisibility.Always || lciPostDate.Visibility == LayoutVisibility.Always)
            {
                emptyDateSpace.Visibility = LayoutVisibility.Always;
            }
        }
        /// <summary>
        /// To fill the FD Account values to concern controls in the form.
        /// </summary>
        private void FillFDAccountDetails()
        {
            try
            {
                //Assign Vlaues for FD Investment/FD Opening
                //Check for Type such as (RN/WD/POI) if it is not of any type, it will route screen such as "FDInvestment(Modify)/FDOpening(Modify))
                //otherwise it will route to renewal(modify)/POI(modify)
                if (fdTypes != FDTypes.RN && fdTypes != FDTypes.WD && fdTypes != FDTypes.PWD && fdTypes != FDTypes.POI && fdTypes != FDTypes.RIN)
                {
                    if (fdAccountId != 0)
                    {
                        using (FDAccountSystem fdAccountSystem = new FDAccountSystem(fdAccountId))
                        {
                            PrevProjectId = fdAccountSystem.PrevProjectId;
                            PrevLedgerId = fdAccountSystem.PrevLedgerId;
                            VoucherId = fdAccountSystem.VoucherId;
                            glkpBranch.EditValue = fdAccountSystem.BankId;
                            glkpProDetails.EditValue = fdAccountSystem.ProjectId;
                            lblLedgerCurAmt.Text = fdAccountSystem.FDLedgerCurBalance;
                            txtAccountHolder.Text = fdAccountSystem.FDAccountHolderName;
                            txtAccountNumber.Text = fdAccountSystem.FDAccountNumber;
                            cboFDScheme.SelectedIndex = fdAccountSystem.FDScheme;
                            txtAmount.Text = this.UtilityMember.NumberSet.ToNumber(fdAccountSystem.FdAmount);
                            txtIntrestRate.Text = fdAccountSystem.FDInterestRate.ToString();
                            deCreatedDate.DateTime = fdAccountSystem.CreatedOn;
                            deDateOfMaturity.DateTime = fdAccountSystem.FDMaturityDate;
                            //chinna 
                            // 06.10.2017
                            //lblMaturityDate.Text = deDateOfMaturity.DateTime != DateTime.MinValue.Date ? deDateOfMaturity.Text + " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestCalculatedAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestCalculatedAmount);
                            mtxtNotes.Text = fdAccountSystem.LedgerNotes;
                            txtExpectedMaturityValue.Text = this.UtilityMember.NumberSet.ToNumber(fdAccountSystem.FDExpectedMaturityValue);
                            txtCreationExceptedInterest.Text = this.UtilityMember.NumberSet.ToNumber(fdAccountSystem.ExpectedInterestAmount);
                            if (fdTypes == FDTypes.IN)
                            {
                                glkpFDLedgerDetails.EditValue = fdAccountSystem.LedgerId;
                                //this.glkpCashBankLedgers.EditValueChanged -= new System.EventHandler(this.glkpCashBankLedgers_EditValueChanged);
                                glkpCashBankLedgers.EditValue = fdAccountSystem.BankLedgerId;

                                this.glkpCashBankLedgers.EditValueChanged += new System.EventHandler(this.glkpCashBankLedgers_EditValueChanged);
                            }
                            else
                            {
                                glkpLedgers.EditValue = fdAccountSystem.LedgerId;
                            }
                            cboInterestType.SelectedIndex = fdAccountSystem.InterestType;
                            txtVoucherNo.Text = fdAccountSystem.FDVoucherNo;
                            txtReceiptNo.Text = fdAccountSystem.ReceiptNo;
                            if (fdAccountSystem.InterestType == 0)
                            {
                                CalculateInterestAmount();
                            }
                            else
                            {
                                CompoundInterest();
                            }

                            //16/10/2024, To set currency details
                            if (this.AppSetting.AllowMultiCurrency == 1)
                            {
                                glkpCurrencyCountry.EditValue = fdAccountSystem.CurrencyCountryId;
                                object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(fdAccountSystem.CurrencyCountryId);
                                if (findcountry == null) glkpCurrencyCountry.EditValue = null;
                                txtExchangeRate.Text = fdAccountSystem.ExchangeRate.ToString();
                                txtExchangeRate.Tag = txtExchangeRate.Text;
                                txtCurrencyAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(fdAccountSystem.ContributionAmount.ToString())).ToString();
                                lblCalculatedAmt.Text = fdAccountSystem.CalculatedAmount.ToString();
                                txtActualAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(fdAccountSystem.ActualAmount.ToString())).ToString();

                                ShowCurrencyDetails();
                            }

                            //09/05/2024, To set Mutaul FD Properties
                            EntryFDInvestmentType = fdAccountSystem.InvestmentTypeId;
                            BaseFDInvestmentType = EntryFDInvestmentType;
                            txtMutualFolioNo.Text = fdAccountSystem.MFFolioNo;
                            txtMutualFundScheme.Text = fdAccountSystem.MFSchemeName;
                            txtBaseNAVPerUnit.Text = fdAccountSystem.MFNAVPerUnit.ToString();
                            txtBaseNAVNoOfUnits.Text = fdAccountSystem.MFNoOfUnit.ToString();
                            cblutualFundModeHolding.SelectedIndex = fdAccountSystem.MFModeOfHolding;
                        }
                    }
                }
                else if (fdTypes != FDTypes.IN && fdTypes != FDTypes.OP)
                {
                    using (FDAccountSystem fdsystem = new FDAccountSystem(fdAccountId))
                    {
                        //On 16/10/2024, To show FD's currency for all renewals, post intererest and widthdrwals 
                        if (fdTypes != FDTypes.IN && fdTypes != FDTypes.OP)
                        {
                            if (this.AppSetting.AllowMultiCurrency == 1 && fdAccountId > 0)
                            {
                                glkpCurrencyCountry.EditValue = fdsystem.CurrencyCountryId;
                                object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(fdsystem.CurrencyCountryId);
                                if (findcountry == null) glkpCurrencyCountry.EditValue = null;
                                txtExchangeRate.Text = fdsystem.ExchangeRate.ToString();
                                if (fdTypes == FDTypes.RN || fdTypes == FDTypes.POI || fdTypes == FDTypes.WD)
                                {
                                    LoadCashBankLedger(glkpBankInterestLedger);
                                }
                                else
                                {
                                    LoadCashBankLedger(glkpCashBankLedgers);
                                }

                                ShowCurrencyDetails();
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally
            {
                CheckFDTransDateAndLockFDModification();

                if (fdTypes == FDTypes.IN)
                    ShowMutualFundProperties(glkpFDLedgerDetails);
                else
                    ShowMutualFundProperties(glkpLedgers);


            }
        }

        private DataTable ConstructFDTable()
        {
            DataTable dtCashBank = new DataTable();
            using (VoucherTransactionSystem voucherTrans = new VoucherTransactionSystem())
            {
                dtCashBank.Columns.Add(new DataColumn(voucherTrans.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName, typeof(int)));
                dtCashBank.Columns.Add(new DataColumn(voucherTrans.AppSchema.FDAccount.EXCHANGE_RATEColumn.ColumnName, typeof(decimal)));
                dtCashBank.Columns.Add(new DataColumn(voucherTrans.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName, typeof(decimal)));
                dtCashBank.Columns.Add(new DataColumn(voucherTrans.AppSchema.FDAccount.AMOUNTColumn.ColumnName, typeof(decimal)));
                dtCashBank.Columns.Add(new DataColumn(voucherTrans.AppSchema.Ledger.LEDGER_BALANCEColumn.ColumnName, typeof(string)));
                dtCashBank.Columns.Add(new DataColumn(voucherTrans.AppSchema.FDAccount.TRANS_MODEColumn.ColumnName, typeof(string)));
                dtCashBank.Columns.Add(new DataColumn(voucherTrans.AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName, typeof(string)));
                dtCashBank.Columns.Add(new DataColumn(voucherTrans.AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName, typeof(string)));
                dtCashBank.Columns.Add(new DataColumn(voucherTrans.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName, typeof(string)));
            }
            return dtCashBank;
        }
        /// <summary>
        /// This is to set the voucher method such as Receipt/Contra/Journal based on the selected 	Project for each Investment/Renewal/Withdrawal/PostInterest.
        /// </summary>
        /// <params>PROJECT_ID</params>
        private void FetchVoucherMethod()
        {
            if (!fdTypes.Equals(FDTypes.OP))
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    if (fdTypes.Equals(FDTypes.IN))
                    {
                        //resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "4", (int)DefaultVoucherTypes.Contra);
                        VoucherDefinitionId = (int)DefaultVoucherTypes.Contra;
                        resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "3", VoucherDefinitionId);
                    }
                    else if (fdTypes.Equals(FDTypes.RN))
                    {
                        if (cboInterestMode.SelectedIndex != 0)
                        {
                            VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;
                            resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "4", VoucherDefinitionId);
                        }
                        else
                        {
                            VoucherDefinitionId = (int)DefaultVoucherTypes.Receipt;
                            resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "1", VoucherDefinitionId);
                        }
                    }
                    else if (fdTypes.Equals(FDTypes.WD) || fdTypes.Equals(FDTypes.PWD) || fdTypes.Equals(FDTypes.RIN))
                    {
                        VoucherDefinitionId = (int)DefaultVoucherTypes.Contra;
                        resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "3", VoucherDefinitionId);
                    }

                    else if (fdTypes.Equals(FDTypes.POI))
                    {
                        if (cboInterestMode.SelectedIndex != 0)
                        {
                            VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;
                            resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "4", VoucherDefinitionId);
                        }
                        else
                        {
                            VoucherDefinitionId = (int)DefaultVoucherTypes.Receipt;
                            resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "1", VoucherDefinitionId);
                        }
                    }
                    //if (cboInterestMode.SelectedIndex != 0)
                    //{
                    //    resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "4");
                    //}
                    //else
                    //{
                    //    resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "3");
                    //}
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        TransVoucherMethod = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][projectSystem.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());

                        if (TransVoucherMethod == 1)
                        {
                            if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD || fdTypes == FDTypes.RIN || fdTypes == FDTypes.RN || fdTypes == FDTypes.POI)
                            {
                                txtRenewalVoucherNo.Enabled = false;
                                if (VoucherId == 0 && FDRenewalId == 0)
                                {
                                    LoadVoucherNo(fdTypes);
                                }
                                else if (VoucherId > 0 || FDRenewalId > 0)
                                {
                                    if (PreviousInterestType == cboInterestMode.SelectedIndex)
                                    {
                                        txtRenewalVoucherNo.Text = InterestVoucherNumber;
                                    }
                                    else
                                    {
                                        LoadVoucherNo(fdTypes);
                                    }
                                }
                            }
                            else
                            {
                                txtVoucherNo.Enabled = false;
                                if (VoucherId == 0)
                                {
                                    LoadVoucherNo(fdTypes);
                                }
                            }
                        }
                        else
                        {
                            if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD || fdTypes == FDTypes.RIN || fdTypes == FDTypes.RN)
                            {
                                txtRenewalVoucherNo.Enabled = true;
                            }
                            else
                            {
                                txtVoucherNo.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        TransVoucherMethod = 2;
                    }

                    //On 05/10/2022, For withdrwal fd voucher, should have two voucher numbers
                    //1. Default for Contra Voucher (withdrwal), 2. Receipt Voucher ------------------------------
                    if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)
                    {
                        WithdrwalReceiptTransVoucherMethod = (int)DefaultVoucherTypes.Receipt;
                        resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "1", WithdrwalReceiptTransVoucherMethod);
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            WithdrwalReceiptTransVoucherMethod = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][projectSystem.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                            txtWithdrwalReceiptVNo.Enabled = (WithdrwalReceiptTransVoucherMethod != 1);

                            if (VoucherId == 0)
                            {
                                if (WithdrwalReceiptTransVoucherMethod == 1)
                                {
                                    LoadVoucherNo(fdTypes);
                                }
                                else
                                {
                                    txtWithdrwalReceiptVNo.Text = string.Empty;
                                }
                            }
                        }
                    }
                    //--------------------------------------------------------------------------------------------

                }
            }
        }

        private void LoadVoucherNo(FDTypes fdTypes)
        {
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.ProjectId = ProjectId;
                if (fdTypes.Equals(FDTypes.IN))
                {
                    voucherTransaction.VoucherType = VoucherSubTypes.CN.ToString();
                    voucherTransaction.VoucherDefinitionId = (int)DefaultVoucherTypes.Contra;
                    voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(deCreatedDate.Text, false);
                    txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
                }
                else if (fdTypes.Equals(FDTypes.RN) || fdTypes.Equals(FDTypes.POI))
                {
                    if (cboInterestMode.SelectedIndex == 1)
                    {
                        voucherTransaction.VoucherType = VoucherSubTypes.JN.ToString();
                        voucherTransaction.VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;
                    }
                    else
                    {
                        voucherTransaction.VoucherType = VoucherSubTypes.RC.ToString();
                        voucherTransaction.VoucherDefinitionId = (int)DefaultVoucherTypes.Receipt;
                    }
                    //voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dteRenewalOn.Text, false);
                    voucherTransaction.VoucherDate = fdTypes != FDTypes.POI ? this.UtilityMember.DateSet.ToDate(dteRenewalOn.Text, false) : this.UtilityMember.DateSet.ToDate(dePostDate.Text, false);
                    txtRenewalVoucherNo.Text = voucherTransaction.TempVoucherNo();
                }
                else if (fdTypes.Equals(FDTypes.WD) || fdTypes.Equals(FDTypes.PWD) || fdTypes.Equals(FDTypes.RIN))
                {
                    //On 05/10/2022, to get receipt voucher no for withdrwal
                    if ((fdTypes.Equals(FDTypes.WD) || fdTypes.Equals(FDTypes.PWD)))
                    {
                        voucherTransaction.VoucherType = VoucherSubTypes.RC.ToString();
                        voucherTransaction.VoucherDefinitionId = (int)DefaultVoucherTypes.Receipt;
                        voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dteClosedOn.Text, false);
                        txtWithdrwalReceiptVNo.Text = voucherTransaction.TempVoucherNo();
                    }

                    voucherTransaction.VoucherType = VoucherSubTypes.CN.ToString();
                    voucherTransaction.VoucherDefinitionId = (int)DefaultVoucherTypes.Contra;
                    voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dteClosedOn.Text, false);
                    txtRenewalVoucherNo.Text = voucherTransaction.TempVoucherNo();
                }

                VoucherDefinitionId = voucherTransaction.VoucherDefinitionId;
            }

        }

        private void FetchLedgerGroup()
        {
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                ledgersystem.LedgerId = glkpCashBankLedgers.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCashBankLedgers.EditValue.ToString()) : 0;
                int LedgerGroupId = ledgersystem.FetchLedgerGroupById();
                CashBankLedgerGroup = LedgerGroupId == (int)FixedLedgerGroup.Cash ? ledgerSubType.CA.ToString() : ledgerSubType.BK.ToString();
            }
        }

        private void ClearControls()
        {
            glkpCashBankLedgers.EditValue = glkpCashBankLedgers.Properties.GetKeyValue(0);

            glkpCurrencyCountry.EditValue = null;
            txtActualAmount.Text = string.Empty;
            txtCurrencyAmount.Text = string.Empty;
            txtExchangeRate.Text = string.Empty;
            lblCalculatedAmt.Text = "0.0";

            //To retain to entre same fd ledger
            if (fdTypes != FDTypes.IN)
            {
                glkpFDLedgerDetails.EditValue = glkpFDLedgerDetails.Properties.GetKeyValue(0);
            }

            glkpBankInterestLedger.EditValue = glkpBankInterestLedger.Properties.GetKeyValue(0);
            glkpInterestLedger.EditValue = glkpInterestLedger.Properties.GetKeyValue(0);
            cboInterestMode.SelectedIndex = 0;
            txtAccountHolder.Text = txtAccountNumber.Text = txtReceiptNo.Text = txtVoucherNo.Text = txtWithdrwalReceiptVNo.Text = txtIntrestRate.Text = txtAmount.Text = mtxtNotes.Text = txtCreationExceptedInterest.Text = txtExpectedMaturityValue.Text = txtExpectedMaturityRenewalValue.Text = string.Empty;
            txtCreationExceptedInterest.Text = txtRenewalExpectedInteres.Text = string.Empty;
            txtRenewalInterestRate.Text = "0";
            IntrestCalculatedAmount = 0;
            txtWithdrawAmount.Text = string.Empty;
            lblAssignPriniciapalintrestAmount.Text = lblAssignIntrestamount.Text = "0";
            cboTransMode.SelectedIndex = 0;
            txtRNNAVNoOfUnits.Text = "0";
            txtRNNAVPerUnit.Text = "0";
            cblutualFundModeHolding.SelectedIndex = 0;

            if (fdTypes == FDTypes.OP)
            {
                glkpProDetails.Focus();
                txtMutualFolioNo.Text = txtMutualFundScheme.Text = string.Empty;
                txtBaseNAVNoOfUnits.Text = "0";
                txtBaseNAVPerUnit.Text = "0";
                cblutualFundModeHolding.SelectedIndex = 0;
            }
            else if (fdTypes == FDTypes.IN)
            {
                FetchVoucherMethod();

                txtMutualFolioNo.Text = txtMutualFundScheme.Text = string.Empty;
                txtBaseNAVNoOfUnits.Text = "0";
                txtBaseNAVPerUnit.Text = "0";
                cblutualFundModeHolding.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// This is to load the current balance of Ledger selected.
        /// </summary>
        /// <params>Ledger Id</params>
        private void FetchCashBankLedgerAmt()
        {
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                BalanceProperty balProperty = balancesystem.GetBalance(ProjectId, glkpCashBankLedgers.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCashBankLedgers.EditValue.ToString()) : 0, "", BalanceSystem.BalanceType.CurrentBalance);
                CurrentLedgerBalance = this.UtilityMember.NumberSet.ToDouble(balProperty.Amount.ToString());
                lblCashBankLedgerAmt.Text = this.UtilityMember.NumberSet.ToCurrency(balProperty.Amount) + " " + balProperty.TransMode;
                TransMode = balProperty.TransMode;

                //On 17/10/2024, To show cash/bank/fd currenct balance
                if (this.AppSetting.AllowMultiCurrency == 1 && (balProperty.GroupId == (int)FixedLedgerGroup.Cash || balProperty.GroupId == (int)FixedLedgerGroup.BankAccounts ||
                                balProperty.GroupId == (int)FixedLedgerGroup.FixedDeposit))
                {
                    CurrentLedgerBalance = this.UtilityMember.NumberSet.ToDouble(balProperty.AmountFC.ToString());
                    lblCashBankLedgerAmt.Text = balProperty.CurrencySymbol + " " + this.UtilityMember.NumberSet.ToNumber(balProperty.AmountFC) + " " + balProperty.TransFCMode;
                }
            }
        }


        private void FetchFDLedgerBalanceAmt()
        {
            int LedgerId = 0;
            if (fdTypes == FDTypes.IN)
            {
                LedgerId = glkpFDLedgerDetails.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpFDLedgerDetails.EditValue.ToString()) : 0;
            }
            else
            {
                LedgerId = glkpLedgers.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedgers.EditValue.ToString()) : 0;
            }
            using (FDAccountSystem fdAccount = new FDAccountSystem())
            {
                BalanceProperty balProperty;
                using (BalanceSystem balancesystem = new BalanceSystem())
                {
                    balProperty = balancesystem.GetBalance(ProjectId, LedgerId, "", BalanceSystem.BalanceType.CurrentBalance);
                }
                if (fdTypes == FDTypes.IN)
                {
                    lblLedgerCurAmt.Text = !string.IsNullOrEmpty(balProperty.Amount.ToString()) ? this.UtilityMember.NumberSet.ToCurrency(balProperty.Amount) + " " + balProperty.TransMode : "0";
                }
                else
                {
                    lblLedgerAmt.Text = !string.IsNullOrEmpty(balProperty.Amount.ToString()) ? this.UtilityMember.NumberSet.ToCurrency(balProperty.Amount) + " " + balProperty.TransMode : "0";
                }

                //On 17/10/2024, To show cash/bank/fd currenct balance
                if (this.AppSetting.AllowMultiCurrency == 1 && (balProperty.GroupId == (int)FixedLedgerGroup.Cash || balProperty.GroupId == (int)FixedLedgerGroup.BankAccounts ||
                                    balProperty.GroupId == (int)FixedLedgerGroup.FixedDeposit))
                {
                    if (this.AppSetting.AllowMultiCurrency == 1)
                    {
                        lblLedgerAmt.Text = balProperty.CurrencySymbol + " " + (!string.IsNullOrEmpty(balProperty.AmountFC.ToString()) ? this.UtilityMember.NumberSet.ToNumber(balProperty.AmountFC) + " " + balProperty.TransMode : "0");
                    }
                }
            }
        }

        private void ShowMutualFundProperties(GridLookUpEdit grdLookup)
        {
            Int32 fdinvestmenttype = (Int32)FDInvestmentType.FD;
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                lblInvestmentTypeValue.Text = FDInvestmentType.FD.ToString();
                if (grdLookup.GetSelectedDataRow() != null)
                {
                    DataRowView drv = grdLookup.GetSelectedDataRow() as DataRowView;
                    if (drv != null)
                    {
                        fdinvestmenttype = UtilityMember.NumberSet.ToInteger(drv[ledgersystem.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());

                        if (fdTypes == FDTypes.IN)
                            lblInvestmentTypeValue.Text = drv[ledgersystem.AppSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName].ToString();
                        else
                            lblInvestmentTypeValue1.Text = drv[ledgersystem.AppSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName].ToString();
                    }
                }
            }

            //On 07/05/2024, To hide mutual fund details
            lcMutualFolioNo.Visibility = lcBaseNAVPerUnit.Visibility = lcBaseNAVNoOfUnits.Visibility = LayoutVisibility.Never;
            lcMutualFundScheme.Visibility = lcMutualFundModeHolding.Visibility = LayoutVisibility.Never;

            lblInsRate.Visibility = lblInterestRate.Visibility = emptySpaceItem7.Visibility = LayoutVisibility.Never;
            lblDateofMaturity.Visibility = lblExpectedMaturityAmountValue.Visibility = lcCreationExceptedInterest.Visibility = LayoutVisibility.Never;
            emptySpaceItem10.Visibility = emptySpaceItem27.Visibility = LayoutVisibility.Never;
            lcRNNAVPerUnit.Visibility = lcRNNAVNoOfUnits.Visibility = LayoutVisibility.Never;

            if (deDateOfMaturity.DateTime == DateTime.MinValue) deDateOfMaturity.Text = string.Empty;
            if (lcgFDAccountDetails.Visibility == LayoutVisibility.Always && (fdTypes == FDTypes.OP || fdTypes == FDTypes.IN))
            {
                if (fdinvestmenttype == (Int32)FDInvestmentType.MutualFund)
                {
                    lblInsRate.Visibility = lblInterestRate.Visibility = emptySpaceItem7.Visibility = LayoutVisibility.Never;
                    lblDateofMaturity.Visibility = lblExpectedMaturityAmountValue.Visibility = lcCreationExceptedInterest.Visibility = LayoutVisibility.Never;
                    emptySpaceItem10.Visibility = emptySpaceItem27.Visibility = LayoutVisibility.Never;

                    lcMutualFolioNo.Visibility = lcBaseNAVPerUnit.Visibility = lcBaseNAVNoOfUnits.Visibility = LayoutVisibility.Always;
                    lcMutualFundScheme.Visibility = lcMutualFundModeHolding.Visibility = LayoutVisibility.Always;
                    emptySpaceItem27.Visibility = LayoutVisibility.Always;
                    //On 09/05/2024, For Maturity Date, to fix empty value for Muttaul Fund
                    deDateOfMaturity.Text = DateTime.MinValue.ToShortDateString();
                }
                else
                {
                    lcMutualFolioNo.Visibility = lcBaseNAVPerUnit.Visibility = lcBaseNAVNoOfUnits.Visibility = LayoutVisibility.Never;
                    lcMutualFundScheme.Visibility = lcMutualFundModeHolding.Visibility = LayoutVisibility.Never;

                    lblInsRate.Visibility = lblInterestRate.Visibility = emptySpaceItem7.Visibility = LayoutVisibility.Always;
                    lblDateofMaturity.Visibility = lblExpectedMaturityAmountValue.Visibility = lcCreationExceptedInterest.Visibility = LayoutVisibility.Always;
                    emptySpaceItem10.Visibility = LayoutVisibility.Always;
                }
            }
            else if (fdTypes == FDTypes.POI || fdTypes == FDTypes.RIN)
            {
                /*if (BaseFDInvestmentType == (Int32)FDInvestmentType.MutualFund)
                {
                    lcRNNAVPerUnit.Visibility = lcRNNAVNoOfUnits.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    lcRNNAVPerUnit.Visibility = lcRNNAVNoOfUnits.Visibility = LayoutVisibility.Never;
                }*/

            }
        }

        /// <summary>
        /// To set properties to controls based on the Type(RN/POI/WD)
        /// </summary>
        /// 
        private void SetDefault()
        {
            lcWithdrwalReceiptVNo.Visibility = LayoutVisibility.Never;
            if (fdTypes != FDTypes.RN)
            {
                if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)
                {
                    lblInvestedOn.Text = this.GetMessage(MessageCatalog.Master.Mapping.CLOSED_ON) + " <color=red>*</color>";
                    lblTotalAmount.Text = this.GetMessage(MessageCatalog.Master.Mapping.WITHDRAW_AMOUNT);
                    lcWithdrwalReceiptVNo.Visibility = LayoutVisibility.Always;
                }
                if (fdTypes == FDTypes.RIN)
                {
                    lblInvestedOn.Text = this.GetMessage(MessageCatalog.Master.Mapping.CLOSED_ON) + " <color=red>*</color>";
                    lblTotalAmount.Text = "Principal Amount after Re-Invested ";
                    lblTotalAmount.TextAlignMode = TextAlignModeItem.CustomSize;
                    lblTotalAmount.Width = 225;
                }
                else if (fdTypes == FDTypes.POI)
                {
                    lblTotalAmount.Text = "Principal Amount after Post Interest ";
                }
            }
            else
            {
                lblAmount.Text = this.GetMessage(MessageCatalog.Master.Mapping.PRINCIPAL_AMOUNT);
                lblInvestedOn.Text = this.GetMessage(MessageCatalog.Master.Mapping.RENEWAL_ON) + " <color=red>*</color>";
            }
            lcgGroupCaption.Text = this.fdTypes == FDTypes.OP ? this.GetMessage(MessageCatalog.Master.FDLedger.FD_OPENING_GROUP_CAPTION) : this.GetMessage(MessageCatalog.Master.FDLedger.FD_INVESTMENT_GROUP_CAPTION);
            lblCurrentDate.Text = this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false).ToShortDateString();
            this.Height = fdTypes == FDTypes.OP ? 360 : fdTypes == FDTypes.IN ? 410 : FDTypes.RN == fdTypes ? 470 : FDTypes.POI == fdTypes ? 400 : 370;

            //On 15/102024, to set form height
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                this.Height += lcgCurrencyDetails.Height;
            }

            //deDateOfMaturity.DateTime = deCreatedDate.DateTime;


            UcFDHistory ucFDhistory = new UcFDHistory();
            ucFDhistory.FDAccountId = this.fdAccountId;
            PopupContainerControl popctl = new PopupContainerControl();
            popctl.Controls.Add(ucFDhistory);
            popctl.Left = 0;
            popctl.Width = this.Width + 350;
            popctl.Height = 300;
            popctl.Location = new Point(0, popctl.Height);
            popupecFD.Properties.PopupControl = popctl;
        }
        /// <summary>
        /// This is to calculate the interest amount based on the maturity date, Interest 	
        /// Percentage,Interest Type(Int.Rec/Acc.Int) and Interest Mode (Simple/Compund).
        /// Steps To Caluclate the Interest Amount: No of Days:Prinicpalamount/100*Int.rate*No of days/365
        /// No of Days: MaturityDate-RenewalDate/PostInterestDate
        /// </summary>
        /// <params>Investment Date/Renewal Date/Opening Date,Date_of_maturity,Interest type,Interest Mode.</params>

        private void CalculateInterestAmount()
        {
            double NoofDays = 0;
            try
            {
                if (fdTypes == FDTypes.POI)
                {
                    if (cboInterestMode.SelectedIndex == 0)
                    {
                        amount = FDAmount;
                    }
                    else
                    {
                        amount = FDAmount + this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                    }

                    interestRate = this.UtilityMember.NumberSet.ToDouble(PostInterestRate.ToString());
                    if (dePostDate.DateTime != DateTime.MinValue.Date)
                    {
                        NoofDays = (dePostDate.DateTime - PostInterestCreatedDate).TotalDays;

                        if (NoofDays < 0) NoofDays = 0;
                    }
                    if (lblAssignPrinicipalAmount.Text.Contains(AppSetting.Currency))
                    {
                        double ppamt = this.UtilityMember.NumberSet.ToDouble(lblAssignPrinicipalAmount.Text.TrimStart(AppSetting.Currency.ToCharArray()));
                        IntrestCalculatedAmount = interestRate != 0 ? (ppamt / 100 * interestRate * NoofDays / 365) : amount;
                    }
                    //IntrestCalculatedAmount = interestRate != 0 ? (amount / 100 * interestRate * NoofDays / 365) : amount;
                    IntrestAmount = this.UtilityMember.NumberSet.ToDouble(IntrestCalculatedAmount.ToString());
                    lblIntrestAmount.Text = "Expected Interest Amount";
                    //lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(IntrestCalculatedAmount.ToString()));
                    //txtRenewalInterestRate.Text = Math.Round(IntrestCalculatedAmount, 2).ToString(); // depostdate_leave
                }

                else if (UtilityMember.NumberSet.ToDecimal(txtIntrestRate.Text) != 0 || UtilityMember.NumberSet.ToDecimal(txtFdInterestRate.Text) != 0)
                {
                    if (fdTypes != FDTypes.RN)
                    {
                        amount = this.UtilityMember.NumberSet.ToDouble(txtAmount.Text);
                    }
                    else
                    {
                        if (cboInterestMode.SelectedIndex == 0)
                        {
                            amount = FDAmount;
                        }
                        else
                        {
                            amount = FDAmount + this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                        }
                    }

                    if (fdTypes != FDTypes.RN)
                    {
                        interestRate = this.UtilityMember.NumberSet.ToDouble(txtIntrestRate.Text);
                    }
                    else
                    {
                        interestRate = this.UtilityMember.NumberSet.ToDouble(txtFdInterestRate.Text);
                    }

                    if (fdTypes == FDTypes.RN)
                    {
                        if (dteFDMaturityDate.DateTime.Date != DateTime.MinValue.Date)
                            NoofDays = (dteFDMaturityDate.DateTime - dteRenewalOn.DateTime).TotalDays;
                    }

                    else if (fdTypes == FDTypes.OP || fdTypes == FDTypes.IN || fdTypes == FDTypes.RIN)
                    {
                        if (deDateOfMaturity.DateTime != DateTime.MinValue.Date)
                            NoofDays = (deDateOfMaturity.DateTime - deCreatedDate.DateTime).TotalDays;
                    }

                    IntrestCalculatedAmount = interestRate != 0 ? (amount / 100 * interestRate * NoofDays / 365) : amount;
                    IntrestAmount = this.UtilityMember.NumberSet.ToDouble(IntrestCalculatedAmount.ToString());
                    if (fdTypes == FDTypes.RN)
                    {
                        //chinna
                        //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount);
                    }

                    else
                    {
                        //chinna
                        // 06.10.2017
                        // lblMaturityDate.Text = deDateOfMaturity.DateTime != DateTime.MinValue.Date ? deDateOfMaturity.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount);
                    }
                }
                else
                {
                    if (fdTypes == FDTypes.RN)
                    {
                        //chinna
                        //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(0) : " : " + this.UtilityMember.NumberSet.ToCurrency(0);
                    }
                    else
                    {
                        //chinna
                        // 06.10.2017
                        //lblMaturityDate.Text = deDateOfMaturity.DateTime != DateTime.MinValue.Date ? deDateOfMaturity.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(0) : " : " + this.UtilityMember.NumberSet.ToCurrency(0);
                    }
                }

                //On 17/10/2024, To show fd details balance based on currency
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    ShowFDAmountDetailInCurrency();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// This is to Fetch the Interest Legder which has as flag"Is_BANK_INTEREST_LEDGER=1/Set this as Bank Interest Ledger)
        /// </summary>
        /// <param name="glkpLedger">this Method is used for Renewals/Post Interetest/Witdrawals gridLookups to load the "CR" Ledger 
        /// So GridLookupEDit is taken as a Prameter</param>
        private void LoadLedger(GridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;

                    string transdate = FDTypes.RN == fdTypes ? this.UtilityMember.DateSet.ToDate(dteRenewalOn.DateTime.ToShortDateString()) :
                                               FDTypes.POI == fdTypes ? this.UtilityMember.DateSet.ToDate(dePostDate.DateTime.ToShortDateString()) :
                                               FDTypes.IN == fdTypes ? this.UtilityMember.DateSet.ToDate(deCreatedDate.DateTime.ToShortDateString()) :
                                               this.UtilityMember.DateSet.ToDate(dteClosedOn.DateTime.ToShortDateString());

                    if (transdate != this.UtilityMember.DateSet.ToDate(DateTime.MinValue.ToShortDateString()))
                    {
                        ledgerSystem.LedgerClosedDateForFilter = transdate;
                    }

                    resultArgs = ledgerSystem.FetchFDinterestLedger();

                    Int32 PrevLedgerId = glkpLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) : 0;

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        glkpLedger.EditValue = glkpLedger.Properties.GetKeyValue(0);
                        //PrevLedgerId = glkpLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) : 0;
                    }
                    else if (resultArgs.Success) //As on 02/11/2021, even though there is no records, bind it, to refresh already loaded ledgers
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    }

                    // On 02/11/2021, to retain already selected ledger
                    if (glkpLedger.Properties.GetDisplayValueByKeyValue(PrevLedgerId) != null)
                    {
                        glkpLedger.EditValue = PrevLedgerId;
                    }

                    FetchVoucherMethod();
                    //else
                    //{
                    //    glkpLedger.EditValue = null;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// On 20/05/2022, to load all general ledgers except Interest Ledgers
        /// </summary>
        /// <param name="glkpLedger">this Method is used for Renewals/Post Interetest/Witdrawals gridLookups to load the "CR" Ledger 
        /// So GridLookupEDit is taken as a Prameter</param>
        private void LoadAllGeneralLedger(GridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;

                    string transdate = FDTypes.RN == fdTypes ? this.UtilityMember.DateSet.ToDate(dteRenewalOn.DateTime.ToShortDateString()) :
                                               FDTypes.POI == fdTypes ? this.UtilityMember.DateSet.ToDate(dePostDate.DateTime.ToShortDateString()) :
                                               FDTypes.IN == fdTypes ? this.UtilityMember.DateSet.ToDate(deCreatedDate.DateTime.ToShortDateString()) :
                                               this.UtilityMember.DateSet.ToDate(dteClosedOn.DateTime.ToShortDateString());

                    if (transdate != this.UtilityMember.DateSet.ToDate(DateTime.MinValue.ToShortDateString()))
                    {
                        ledgerSystem.LedgerClosedDateForFilter = transdate;
                    }

                    resultArgs = ledgerSystem.FetchLedgerByGroup();
                    Int32 PrevLedgerId = glkpLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpLedger.EditValue.ToString()) : 0;

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtGeneralAllLedgers = resultArgs.DataSource.Table;
                        dtGeneralAllLedgers.DefaultView.RowFilter = ledgerSystem.AppSchema.Ledger.IS_BANK_FD_PENALTY_LEDGERColumn.ColumnName + "= 1";
                        dtGeneralAllLedgers = dtGeneralAllLedgers.DefaultView.ToTable();
                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpLedger, dtGeneralAllLedgers, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, true, string.Empty);
                        glkpLedger.EditValue = glkpLedger.Properties.GetKeyValue(0);
                    }
                    else if (resultArgs.Success)
                    {
                        DataTable dtGeneralAllLedgers = resultArgs.DataSource.Table;
                        dtGeneralAllLedgers.DefaultView.RowFilter = "";
                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, true, string.Empty);
                    }

                    // On 02/11/2021, to retain already selected ledger
                    if (glkpLedger.Properties.GetDisplayValueByKeyValue(PrevLedgerId) != null)
                    {
                        glkpLedger.EditValue = PrevLedgerId;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private BalanceProperty FetchCurrentBalance(int LedgerId)
        {
            BalanceProperty balProperty;
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                balProperty = balancesystem.GetBalance(ProjectId, LedgerId, "", BalanceSystem.BalanceType.CurrentBalance);
            }
            return balProperty;
        }

        /// <summary>
        /// This is to load the subsequent date while renewal and to load the interest amount when 	interest type changed.
        /// </summary>
        /// <params>Previous_renewal_date,FD_RENEWAL_ID</params>
        private void GetLastRenewalDate()
        {
            double PrincipalAmount = 0;
            try
            {
                DataTable dtFdRenewalDate = new DataTable();
                DataTable dtLastPostInterest = new DataTable(); /// changed by sugan
                DateTime dtRenewalOn = DateTime.Now;/// changed by sugan
                DateTime dtMaturityOn = DateTime.Now; /// changed by sugan
                DateTime dtMaturityOnForRenewals = DateTime.MinValue;
                double IntrestAmount = 0;
                double InterestAmount = 0;
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    fdAccountSystem.FDAccountId = fdAccountId;
                    //To load the last renewal date in "renew on" for next renewal ..,
                    //Condtions:If "FDAccount" has Post Interests Last "Post Interest+add one" day will be the next "renew On" Date
                    //Otherwise "Maturity Date+One day" will be next Renewal On date.
                    resultArgs = fdTypes != FDTypes.POI ? fdAccountSystem.GetLastRenewalDate() : fdAccountSystem.GetNoOfPostInterests();

                    //On 19/01/2024, To get recent renewal date
                    if (fdTypes == FDTypes.POI)
                    {
                        ResultArgs result = fdAccountSystem.GetLastRenewalDate();
                        if (result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dtRecentRenewalDate = result.DataSource.Table.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                            DataRow dr = dtRecentRenewalDate.Rows[0];
                            dtMaturityOnForRenewals = dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                        }
                    }

                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        lblAssignNoOfRenewal.Text = resultArgs.DataSource.Table.Rows.Count.ToString();
                        if (fdTypes != FDTypes.WD || fdTypes != FDTypes.PWD)
                        {
                            dtFdRenewalDate = resultArgs.DataSource.Table.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                            lblAssignLastRenewedOn.Text = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty;
                            if (fdTypes != FDTypes.POI)
                            {
                                double FDExpectedAmt = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToDouble(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName].ToString()) : 0;
                                lblFDRenewalIntrestAmount.Text = FDExpectedAmt != 0 ? this.UtilityMember.NumberSet.ToCurrency(FDExpectedAmt) : this.UtilityMember.NumberSet.ToCurrency(0);
                            }
                            if (FDRenewalId == 0)
                            {
                                foreach (DataRow dr in dtFdRenewalDate.Rows)
                                {
                                    //On 12/01/2024, For POI, to set proper last renewal on/posted on
                                    //lblAssignLastRenewedOn.Text = fdTypes != FDTypes.POI ? (dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty)
                                    //                                                    : (dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty);

                                    lblAssignLastRenewedOn.Text = fdTypes != FDTypes.POI ? (dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty)
                                                                                        : (dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty);

                                    DateTime MaturityDate = dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                                    if (fdTypes == FDTypes.POI && dtMaturityOnForRenewals != DateTime.MinValue)
                                    {
                                        MaturityDate = dtMaturityOnForRenewals;
                                    }
                                    this.MaturityDate = MaturityDate.ToString();

                                    // This is to Disable the POI Matured Date Caption is empty
                                    //if (fdTypes != FDTypes.POI)
                                    //{
                                    //    lblAssignMaturedon.Text = (dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty);
                                    //}
                                    //else
                                    //{
                                    //    lblLastMaturedon.Text = lblAssignMaturedon.Text = " ";
                                    //}

                                    //dteRenewalOn.DateTime = MaturityDate.AddDays(1);
                                    //dteFDMaturityDate.DateTime = dteRenewalOn.DateTime.AddDays(1);
                                    if (FDTypes.WD != fdTypes || FDTypes.WD != fdTypes)
                                    {
                                        RenewalType = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                                        interestRate = dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToDouble(dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName].ToString()) : 0;

                                        if (RenewalType == FDRenewalTypes.ACI.ToString())
                                        {
                                            InterestAmount = dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToDouble(dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString()) : 0;
                                            PrincipalAmount = amount = this.UtilityMember.NumberSet.ToDouble(FDAmount.ToString());
                                        }
                                        else
                                        {
                                            InterestAmount = dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToDouble(dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString()) : 0;
                                            PrincipalAmount = amount = FDAmount;
                                        }

                                        //******************* Changed By Sugan
                                        if (fdAccountSystem.HasFDPostInterests() > 0)
                                        {
                                            //On 31/08/2022, to pass post interest on maturity date
                                            //dtLastPostInterest = fdAccountSystem.GetNoOfPostInterestsBayDateRange(UtilityMember.DateSet.ToDate(RenewalDate, false)).DataSource.Table.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                                            ResultArgs result = fdAccountSystem.GetNoOfPostInterestsBayDateRange(UtilityMember.DateSet.ToDate(RenewalDate, false));
                                            if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                                            {
                                                DataTable dt = result.DataSource.Table;
                                                dtLastPostInterest = dt.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                                            }
                                            //--------------------------------------------------------------------------------

                                            if (dtLastPostInterest != null && dtLastPostInterest.Rows.Count > 0)
                                            {
                                                if (fdTypes != FDTypes.POI)
                                                {
                                                    if (dtLastPostInterest.Rows[0]["FD_TYPE"].ToString() != "RN")
                                                    {
                                                        dtRenewalOn = dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                                        dtMaturityOn = MaturityDate;
                                                    }
                                                    else
                                                    {
                                                        dtRenewalOn = dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                                        dtMaturityOn = dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                                    }
                                                }
                                                else
                                                {
                                                    dtRenewalOn = dtMaturityOn = dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                                }
                                            }
                                        }
                                        //*******************************
                                        else
                                        {
                                            //dtRenewalOn = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                            //dtMaturityOn = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                        }
                                        double NoOfDays = 0;
                                        if (dtMaturityOn.Date != DateTime.MinValue.Date)// by aldrin when the maturity date is calculated the amount is goin negative.
                                        {
                                            NoOfDays = (dtMaturityOn.Date - dtRenewalOn.Date).TotalDays;
                                        }
                                        int InterestType = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName].ToString()) : 0;
                                        if (InterestType == 0)
                                        {
                                            IntrestAmount = PrincipalAmount / 100 * interestRate * NoOfDays / 365;
                                        }
                                        else
                                        {
                                            interestRate = interestRate / 100;
                                            IntrestAmount = CalculateTimesPerYear(dtMaturityOn.Date, dtRenewalOn.Date);
                                            IntrestAmount = IntrestAmount - (PrincipalAmount - InterestAmount);
                                        }
                                        // lblFDRenewalIntrestAmount.Text = ExpectedMaturityInterestAmount != 0 ? this.UtilityMember.NumberSet.ToCurrency(ExpectedMaturityInterestAmount) : this.UtilityMember.NumberSet.ToCurrency(0);
                                        txtRenewalInterestRate.Text = IntrestAmount != 0 ? this.UtilityMember.NumberSet.ToNumber(IntrestAmount) : "0";
                                        if (FDRenewalId == 0)
                                        {
                                            //chinna
                                            //  lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(0) : " : " + this.UtilityMember.NumberSet.ToCurrency(0);
                                        }
                                        else
                                        {
                                            //chinna
                                            //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount);
                                        }
                                        if (RenewalType == FDRenewalTypes.ACI.ToString())
                                        {
                                            this.cboInterestMode.SelectedIndexChanged -= new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                            cboInterestMode.Text = this.GetMessage(MessageCatalog.Master.Mapping.ACCUMULATED_INTEREST);
                                            cboInterestMode.SelectedIndex = 1;
                                            if (FDTypes.RIN != fdTypes)
                                            {
                                                lblCapBankInterestLedger.Visibility = emptySpaceItem16.Visibility = lblCashBankLedgerCap.Visibility = lblCashLedgerAmt.Visibility = LayoutVisibility.Never;
                                            }
                                            this.cboInterestMode.SelectedIndexChanged += new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            dtFdRenewalDate = resultArgs.DataSource.Table.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                            interestRate = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToDouble(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName].ToString()) : 0;
                            PrincipalAmount = this.UtilityMember.NumberSet.ToDouble(FDAmount.ToString());
                            if (fdAccountSystem.HasFDPostInterests() <= 0)
                            {
                                dtRenewalOn = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                            }
                            else
                            {
                                //On 31/08/2022, to pass post interest on maturity date -----------------------------------
                                //dtLastPostInterest = fdAccountSystem.GetNoOfPostInterestsBayDateRange(this.UtilityMember.DateSet.ToDate(this.MaturityDate, false).AddDays(1)).DataSource.Table.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                                ResultArgs result = fdAccountSystem.GetNoOfPostInterestsBayDateRange(this.UtilityMember.DateSet.ToDate(this.MaturityDate, false).AddDays(1));
                                if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                                {
                                    DataTable dt = result.DataSource.Table;
                                    dtLastPostInterest = dt.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                                }
                                //------------------------------------------------------------------------------------------

                                if (dtLastPostInterest != null && dtLastPostInterest.Rows.Count > 0)
                                {
                                    if (dtLastPostInterest.Rows[0]["FD_TYPE"].ToString() != "RN")
                                    {
                                        dtRenewalOn = dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                    }
                                    else
                                    {
                                        dtRenewalOn = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                    }
                                }
                                else
                                {
                                    dtRenewalOn = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                }
                            }
                            dtMaturityOn = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                            this.RenewalDate = dtRenewalOn.Date.ToShortDateString();
                            this.dteClosedOn.EditValueChanged -= new System.EventHandler(this.dteClosedOn_EditValueChanged);
                            //dteClosedOn.DateTime = dtRenewalOn.Date.AddDays(1); //-- aldrin
                            this.dteClosedOn.EditValueChanged += new System.EventHandler(this.dteClosedOn_EditValueChanged);
                            double NoOfDays = 0;
                            if (dtMaturityOn.Date != DateTime.MinValue.Date)// by aldrin when the maturity date is calculated the amount is goin negative.
                            {
                                NoOfDays = (dtMaturityOn.Date - dtRenewalOn.Date).TotalDays;
                            }
                            IntrestAmount = PrincipalAmount / 100 * interestRate * NoOfDays / 365;
                            lblFDRenewalIntrestAmount.Text = IntrestAmount != 0 ? this.UtilityMember.NumberSet.ToCurrency(IntrestAmount) : this.UtilityMember.NumberSet.ToCurrency(0);
                            txtRenewalInterestRate.Text = IntrestAmount != 0 ? this.UtilityMember.NumberSet.ToNumber(IntrestAmount) : this.UtilityMember.NumberSet.ToNumber(0);
                            this.MaturityDate = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty;
                            lblAssignLastRenewedOn.Text = dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtFdRenewalDate.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty;
                        }
                    }
                    else
                    {
                        //******************* Changed By Sugan
                        //dteRenewalOn.DateTime = this.UtilityMember.DateSet.ToDate(MaturityDate, false).AddDays(1);
                        interestRate = FDInterestRate;
                        if (fdAccountSystem.HasFDPostInterests() > 0)
                        {
                            //On 31/08/2022, to pass post interest on maturity date
                            //dtLastPostInterest = fdAccountSystem.GetNoOfPostInterestsBayDateRange(UtilityMember.DateSet.ToDate(RenewalDate, false)).DataSource.Table.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                            ResultArgs result = fdAccountSystem.GetNoOfPostInterestsBayDateRange(UtilityMember.DateSet.ToDate(RenewalDate, false));
                            if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                            {
                                DataTable dt = result.DataSource.Table;
                                dtLastPostInterest = dt.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                                RenewalType = dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                            }
                            //--------------------------------------------------------


                            if (RenewalType == FDRenewalTypes.ACI.ToString())
                            {
                                PrincipalAmount = amount = this.UtilityMember.NumberSet.ToDouble(FDAmount.ToString());
                            }
                            else
                            {
                                PrincipalAmount = amount = FDAmount;
                            }
                            if (dtLastPostInterest != null && dtLastPostInterest.Rows.Count > 0)
                            {
                                if (dtLastPostInterest.Rows[0]["FD_TYPE"].ToString() != "RN")
                                {
                                    dtRenewalOn = dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                    dtMaturityOn = this.UtilityMember.DateSet.ToDate(MaturityDate, false);
                                }
                                else
                                {
                                    dtRenewalOn = dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                    dtMaturityOn = dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dtLastPostInterest.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                }
                                double NoOfDays = 0;
                                if (dtMaturityOn.Date != DateTime.MinValue.Date) { NoOfDays = (dtMaturityOn.Date - dtRenewalOn.Date).TotalDays; }
                                int InterestType = cboInsType.SelectedIndex;
                                if (InterestType == 0)
                                {
                                    IntrestAmount = PrincipalAmount / 100 * interestRate * NoOfDays / 365;
                                }
                                else
                                {
                                    interestRate = interestRate / 100;
                                    IntrestAmount = CalculateTimesPerYear(dtMaturityOn.Date, dtRenewalOn.Date);
                                    IntrestAmount = IntrestAmount - (PrincipalAmount - InterestAmount);
                                }

                                // 09.10.2017 .. chinna
                                // lblFDRenewalIntrestAmount.Text = IntrestAmount != 0 ? this.UtilityMember.NumberSet.ToCurrency(IntrestAmount) : this.UtilityMember.NumberSet.ToCurrency(0);
                                lblFDRenewalIntrestAmount.Text = ExpectedMaturityInterestAmount != 0 ? this.UtilityMember.NumberSet.ToCurrency(ExpectedMaturityInterestAmount) : this.UtilityMember.NumberSet.ToCurrency(0);
                                txtRenewalInterestRate.Text = IntrestAmount != 0 ? this.UtilityMember.NumberSet.ToNumber(IntrestAmount) : "0";
                            }
                        }
                        //*******************************

                        //*****************************
                        if (fdTypes == FDTypes.POI)
                        {
                            PostDateLeave();
                        }
                        //added by sugan****************
                        //To set the Post Interest date with in the renewal Period
                        //dteFDMaturityDate.DateTime = dteRenewalOn.DateTime.AddDays(1); //-- aldrin

                        //chinna
                        //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(0) : " : " + this.UtilityMember.NumberSet.ToCurrency(0);

                        this.dteClosedOn.EditValueChanged -= new System.EventHandler(this.dteClosedOn_EditValueChanged);
                        //dteClosedOn.DateTime = this.UtilityMember.DateSet.ToDate(RenewalDate, false); //-- aldrin
                        this.dteClosedOn.EditValueChanged += new System.EventHandler(this.dteClosedOn_EditValueChanged);
                    }

                    if (fdTypes == FDTypes.RIN)
                    {
                        resultArgs = fdAccountSystem.GetNoOfReInvestment();
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void AssignValues()
        {
            try
            {
                if (fdAccountId != 0 && FDRenewalId != 0)
                {
                    using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                    {
                        if (dtRenewal != null && dtMaster != null && dtMaster.Rows.Count != 0 && dtRenewal.Rows.Count != 0)
                        {
                            lblCapBankInterestLedger.Visibility = LayoutVisibility.Always;
                            DataView dvRenewal = dtRenewal.DefaultView;
                            dvRenewal.RowFilter = fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName + "=" + FDRenewalId + "";
                            dtRenewal = dvRenewal.ToTable();
                            DataView dvMaster = dtMaster.DefaultView;
                            dvMaster.RowFilter = fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName + "=" + this.fdAccountId + "";
                            dtMaster = dvMaster.ToTable();
                            if (dtRenewal != null && dtMaster != null && dtMaster.Rows.Count != 0 && dtRenewal.Rows.Count != 0)
                            {
                                FDIntrestVoucherId = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString()) : 0;
                                FDRenewalId = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                                VoucherId = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString()) : 0;
                                FDAccountId = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                                RenewalType = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != DBNull.Value ? dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : "0";
                                if (RenewalType == FDRenewalTypes.ACI.ToString())
                                {
                                    LedgerId = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                    PrevFDLedgerId = LedgerId;
                                }
                                else
                                {

                                    LedgerId = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                    PrevFDLedgerId = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                }
                                InterestType = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName].ToString()) : 0;

                                txtRenewalVoucherNo.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName] != DBNull.Value ? dtRenewal.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString() : "0";
                                InterestVoucherNumber = dtRenewal.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName] != DBNull.Value ? dtRenewal.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString() : "0";
                                dteRenewalOn.DateTime = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                dteFDMaturityDate.DateTime = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                txtReceiptNo.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RECEIPT_NOColumn.ColumnName] != DBNull.Value ? dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RECEIPT_NOColumn.ColumnName].ToString() : string.Empty;
                                txtIntrestRate.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName] != DBNull.Value ? dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName].ToString() : string.Empty;
                                string InsAmount = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString())) : "0";
                                FDAccumulatedPrincipalAmount = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dtMaster.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString()) : 0;
                                FDAmount = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDRenewal.PRINCIPAL_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dtMaster.Rows[0][fdAccountSystem.AppSchema.FDRenewal.PRINCIPAL_AMOUNTColumn.ColumnName].ToString()) : 0;
                                FDInsRate = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName].ToString()) : 0;

                                //09.10.2017 .. chinna
                                //lblFDRenewalIntrestAmount.Text = InsAmount;

                                //lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(IntrestCalculatedAmount.ToString()));//changed by sugan
                                cboInsType.SelectedIndex = InterestType;
                                if (RenewalType == FDRenewalTypes.ACI.ToString())
                                {
                                    fdAccountSystem.RenewedDate = dteRenewalOn.DateTime.AddDays(-1);
                                    fdAccountSystem.FDAccountId = FDAccountId;
                                    FDAccumulatedPrincipalAmount = fdAccountSystem.FetchAccumulatedInterestAmount();
                                    lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAccumulatedPrincipalAmount);
                                    FDACIInsAmount = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString()) : 0;

                                    //On 20/12/2023, If FD Transmode is credit, change interest amount in negative
                                    if (dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_TRANS_MODEColumn.ColumnName].ToString().ToUpper() == TransactionMode.CR.ToString().ToUpper())
                                    {
                                        FDACIInsAmount = FDACIInsAmount * -1;
                                    }

                                    lblAssignPriniciapalintrestAmount.Text = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDRenewal.PRINCIPAL_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(dtMaster.Rows[0][fdAccountSystem.AppSchema.FDRenewal.PRINCIPAL_AMOUNTColumn.ColumnName].ToString())) : this.UtilityMember.NumberSet.ToCurrency(0);
                                    if (InterestType == 0)
                                    {
                                        CalculateInterestRate();
                                        lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDACIInsAmount) + " " + TransactionMode.DR.ToString();
                                    }
                                    else
                                    {
                                        interestRate = FDInsRate / 100;
                                        double InsAmount1 = CalculateTimesPerYear(dteFDMaturityDate.DateTime, dteRenewalOn.DateTime);
                                        InsAmount1 = InsAmount1 != 0 ? InsAmount1 - FDAmount : 0;
                                        lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount);
                                        lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount) + " " + TransactionMode.DR.ToString();
                                        FDAmount = FDAmount + FDAccumulatedPrincipalAmount;

                                        //chinna
                                        //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(InsAmount1) : " : " + this.UtilityMember.NumberSet.ToCurrency(InsAmount1);
                                    }

                                }
                                else
                                {

                                    lblAssignPriniciapalintrestAmount.Text = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDRenewal.PRINCIPAL_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(dtMaster.Rows[0][fdAccountSystem.AppSchema.FDRenewal.PRINCIPAL_AMOUNTColumn.ColumnName].ToString())) : this.UtilityMember.NumberSet.ToCurrency(0);
                                    fdAccountSystem.RenewedDate = dteRenewalOn.DateTime.AddDays(-1);
                                    fdAccountSystem.FDAccountId = FDAccountId;
                                    FDAccumulatedPrincipalAmount = fdAccountSystem.FetchAccumulatedInterestAmount();
                                    lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAccumulatedPrincipalAmount);
                                    if (InterestType == 0)
                                    {
                                        CalculateInterestRate();
                                    }
                                    else
                                    {
                                        interestRate = FDInsRate / 100;
                                        double InsAmount1 = CalculateTimesPerYear(dteFDMaturityDate.DateTime, dteRenewalOn.DateTime);
                                        InsAmount1 = InsAmount1 - FDAmount;
                                        lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount);
                                        lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount) + " " + TransactionMode.DR.ToString();
                                        FDAmount = FDAmount + FDAccumulatedPrincipalAmount;

                                        //chinna
                                        //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(InsAmount1) : " : " + this.UtilityMember.NumberSet.ToCurrency(InsAmount1);
                                    }
                                }
                                txtRenewalInterestRate.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString())) : "0";
                                if (fdTypes == FDTypes.POI)
                                {
                                    lblFDRenewalIntrestAmount.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString())) : "0";
                                }
                                if (fdTypes != FDTypes.RIN)
                                {
                                    txtTDSAmount.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.TDS_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.TDS_AMOUNTColumn.ColumnName].ToString())) : "0";
                                    txtExpectedMaturityRenewalValue.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName].ToString())) : "0";
                                    txtRenewalExpectedInteres.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_INTEREST_VALUEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_INTEREST_VALUEColumn.ColumnName].ToString())) : "0";
                                    txtFdInterestRate.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName] != DBNull.Value ? dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName].ToString() : string.Empty;
                                    txtFDReceiptNo.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RECEIPT_NOColumn.ColumnName] != DBNull.Value ? dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RECEIPT_NOColumn.ColumnName].ToString() : string.Empty;

                                    if (fdTypes == FDTypes.WD)
                                    {
                                        dteClosedOn.DateTime = UtilityMember.DateSet.ToDate(dtRenewal.Rows[0]["RENEWAL_DATE"].ToString(), false);
                                        //On 06/10/2022, to get withdrwal receipt voucher no
                                        if (FDIntrestVoucherId > 0)
                                        {
                                            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem(FDIntrestVoucherId))
                                            {
                                                txtWithdrwalReceiptVNo.Text = vouchersystem.VoucherNo;
                                            }
                                            glkpInterestLedger.EditValue = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                        }

                                    }
                                }
                                if (RenewalType == FDRenewalTypes.IRI.ToString())
                                {
                                    this.cboInterestMode.SelectedIndexChanged -= new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                    cboInterestMode.SelectedIndex = 0;
                                    if (fdTypes != FDTypes.WD || fdTypes != FDTypes.PWD)
                                    {
                                        lblCapInterestLedger.Visibility = lblIntrestLedger.Visibility = lblIntrestLedgerAmt.Visibility = LayoutVisibility.Always;
                                    }
                                    this.cboInterestMode.SelectedIndexChanged += new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                    glkpInterestLedger.EditValue = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                    glkpBankInterestLedger.EditValue = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                }
                                else
                                {
                                    this.cboInterestMode.SelectedIndexChanged -= new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                    cboInterestMode.Text = this.GetMessage(MessageCatalog.Master.Mapping.ACCUMULATED_INTEREST);
                                    cboInterestMode.SelectedIndex = 1;
                                    //*********************To show the Expected interest as on amount while load the form****************************************
                                    if (InterestType == 0)
                                    {
                                        CalculateInterestAmount();
                                    }
                                    //*************************************************************
                                    this.cboInterestMode.SelectedIndexChanged += new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                    lblCapInterestLedger.Visibility = lblIntrestLedger.Visibility = lblIntrestLedgerAmt.Visibility = LayoutVisibility.Always;
                                    lblCapBankInterestLedger.Visibility = lblCashBankLedgerCap.Visibility = lblCashLedgerAmt.Visibility = emptySpaceItem16.Visibility = LayoutVisibility.Never;

                                    //On 18/12/2020, For Reinvestment (Needs FD Ledger Id for Voucher Trans not for FD renewals)
                                    //glkpInterestLedger.EditValue = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                    if (RenewalType == FDRenewalTypes.RIN.ToString())
                                    {
                                        if (dtMaster.Rows.Count > 0)
                                        {
                                            glkpInterestLedger.EditValue = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                        }
                                    }
                                    else
                                    {
                                        glkpInterestLedger.EditValue = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName].ToString()) : 0;

                                        //On 12/10/2022, to have paritally withdrwal from fd module
                                        if (RenewalType == FDRenewalTypes.WDI.ToString() || RenewalType == FDRenewalTypes.PWD.ToString())
                                        {
                                            lblCashLedgerAmt.Visibility = emptySpaceItem16.Visibility = lblCashLedgerAmt.Visibility = lblCapBankInterestLedger.Visibility = LayoutVisibility.Always;
                                            glkpBankInterestLedger.EditValue = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName].ToString()) : 0;

                                            //23/05/2022, to fix charge mode and charge amount
                                            ChargeLedgerId = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.CHARGE_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.CHARGE_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                            cbPenaltyMode.SelectedIndex = dtRenewal.Rows[0]["CHARGE_MODE"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0]["CHARGE_MODE"].ToString()) : 0;
                                            txtPenaltyAmount.Text = dtRenewal.Rows[0]["CHARGE_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0]["CHARGE_AMOUNT"].ToString())) : "0";
                                        }
                                    }
                                }
                                lblAssignProject.Text = dtMaster.Rows[0][fdAccountSystem.AppSchema.Project.PROJECTColumn.ColumnName] != DBNull.Value ? dtMaster.Rows[0][fdAccountSystem.AppSchema.Project.PROJECTColumn.ColumnName].ToString() : "0";
                                lblAssignFDLedger.Text = dtMaster.Rows[0][fdAccountSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName] != DBNull.Value ? dtMaster.Rows[0][fdAccountSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString() : "0";
                                lblAssignAccountNumber.Text = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName] != DBNull.Value ? dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString() : "0";
                                lblBankName.Text = dtMaster.Rows[0][fdAccountSystem.AppSchema.Bank.BANKColumn.ColumnName] != DBNull.Value ? dtMaster.Rows[0][fdAccountSystem.AppSchema.Bank.BANKColumn.ColumnName].ToString() : "0";
                                mtxtNotes.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName] != DBNull.Value ? dtRenewal.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString() : "0";
                                glkpBranch.EditValue = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.BANK_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.BANK_IDColumn.ColumnName].ToString()) : 0;
                                ProjectId = dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtMaster.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;
                                BaseFDInvestmentType = dtMaster.Rows[0][fdAccountSystem.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtMaster.Rows[0][fdAccountSystem.AppSchema.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString()) : (Int32)FDInvestmentType.FD;
                                if (fdTypes == FDTypes.POI)
                                {
                                    //DataView dvPostRenewal = dtRenewal.AsDataView();
                                    //dvPostRenewal.RowFilter = "FD_RENEWAL_ID=" + FDRenewalId;
                                    //DataTable dtNewRenewal = dvPostRenewal.ToTable();
                                    PostInterestCreatedDate = UtilityMember.DateSet.ToDate(dtRenewal.Rows[0]["RENEWAL_DATE"].ToString(), false);
                                    PostInterestRate = UtilityMember.NumberSet.ToDecimal(dtRenewal.Rows[0]["INTEREST_RATE"].ToString());

                                    //On 12/01/2024, For POI, to set proper for posted on
                                    //dePostDate.DateTime = UtilityMember.DateSet.ToDate(dtRenewal.Rows[0]["MATURITY_DATE"].ToString(), false);
                                    dePostDate.DateTime = UtilityMember.DateSet.ToDate(dtRenewal.Rows[0]["RENEWAL_DATE"].ToString(), false);
                                    PostDateLeave();

                                    //On 03/11/2021, To assign post interest maturity date, from report or voucher view screen, it is not assigninig
                                    if (PostInterestMaturityDate == DateTime.MinValue)
                                    {
                                        PostInterestMaturityDate = UtilityMember.DateSet.ToDate(dtMaster.Rows[0]["MATURITY_DATE"].ToString(), false);
                                    }

                                    if (cboInterestMode.SelectedIndex == 1)
                                    {
                                        cboTransMode.SelectedIndex = 0;
                                        if (dtRenewal.Rows[0]["FD_TRANS_MODE"].ToString().ToUpper() == TransSource.Cr.ToString().ToUpper())
                                        {
                                            cboTransMode.SelectedIndex = 1;
                                        }
                                    }

                                    lblAssignMaturedon.Text = UtilityMember.DateSet.ToDate(PostInterestMaturityDate.ToShortDateString(), false).ToShortDateString();

                                }
                                else if (fdTypes == FDTypes.RN)
                                {
                                    if (cboInterestMode.SelectedIndex == 1)
                                    {
                                        cboTransMode.SelectedIndex = 0;
                                        if (dtRenewal.Rows[0]["FD_TRANS_MODE"].ToString().ToUpper() == TransSource.Cr.ToString().ToUpper())
                                        {
                                            cboTransMode.SelectedIndex = 1;
                                        }
                                    }
                                }

                                if (fdTypes == FDTypes.RIN)
                                {
                                    dePostDate.DateTime = UtilityMember.DateSet.ToDate(dtRenewal.Rows[0]["RENEWAL_DATE"].ToString(), false);
                                    lblCapBankInterestLedger.Visibility = lblCashBankLedgerCap.Visibility = lblCashLedgerAmt.Visibility = emptySpaceItem16.Visibility = LayoutVisibility.Always;
                                    txtWithdrawAmount.Text = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString())) : "0";
                                    glkpBankInterestLedger.EditValue = dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                    FDReInvestedPrincipalAmount = fdAccountSystem.FetchReInvestedAmount();
                                    lblRINAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDReInvestedPrincipalAmount);
                                    lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount + FDReInvestedPrincipalAmount);

                                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount + FDReInvestedPrincipalAmount + this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text)) + " " + TransactionMode.DR.ToString();

                                    //  lblRINAmount.Text = InsAmount;
                                }

                                PreviousInterestType = (cboInsType.Visible ? cboInsType.SelectedIndex : cboInterestMode.Visible ? cboInterestMode.SelectedIndex : cboInterestType.SelectedIndex);
                            }
                        }

                        //On 17/10/2024, To show fd details balance based on currency
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            ShowFDAmountDetailInCurrency();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally
            {

                CheckFDTransDateAndLockFDModification();
            }
        }

        private void AssignTransactionDetails()
        {
            try
            {
                if (fdAccountId != 0 && FDVoucherId != 0)
                {
                    using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                    {
                        fdAccountSystem.FDAccountId = fdAccountId;
                        fdAccountSystem.FDVoucherId = this.FDVoucherId;
                        ResultArgs resultArgsTransDetails = fdAccountSystem.FetchRenewalDetails();
                        if (resultArgsTransDetails.Success && resultArgsTransDetails.DataSource.Table != null && resultArgsTransDetails.DataSource.Table.Rows.Count != 0)
                        {
                            FDIntrestVoucherId = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString()) : 0;
                            FDRenewalId = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_RENEWAL_IDColumn.ColumnName].ToString()) : 0;
                            VoucherId = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString()) : 0;
                            FDAccountId = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName].ToString()) : 0;
                            PrevFDLedgerId = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                            LedgerId = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                            txtRenewalVoucherNo.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString() : "0";
                            InterestVoucherNumber = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString() : "0";
                            deCreatedDate.DateTime = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                            deDateOfMaturity.DateTime = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                            txtFDReceiptNo.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RECEIPT_NOColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RECEIPT_NOColumn.ColumnName].ToString() : string.Empty;
                            txtFdInterestRate.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName].ToString() : string.Empty;
                            FDInsRate = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName].ToString()) : 0;
                            string InsAmount = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString())) : "0";

                            //chinna
                            //lblAssignFDMaturedOn.Text = deDateOfMaturity.DateTime.ToShortDateString() + " : " + InsAmount;

                            string InsTypeVal = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString();
                            if (InsTypeVal.Equals(FDRenewalTypes.WDI.ToString()))
                            {
                                //if (this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()).Equals(this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"].ToString())))
                                //{
                                //    FDAmount = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()) : 0;
                                //}
                                //else
                                //{
                                //    FDAmount = resultArgs.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"].ToString()) : 0;
                                //}

                                FDAmount = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()) : 0;
                                // double WithdrawInsAmount = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString()) : 0;
                                // FDAmount = FDAmount + WithdrawInsAmount;
                            }
                            else
                            {
                                FDAmount = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()) : 0;
                            }
                            InterestType = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName].ToString()) : 0;
                            PreviousInterestType = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_TYPEColumn.ColumnName].ToString()) : 0;
                            lblAssignPrinicipalAmount.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString())) : string.Empty;
                            lblAssignPriniciapalintrestAmount.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString())) : "0";
                            TempPrincipalAmount = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()) : 0;
                            string RenewalType = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : "0";
                            //lblFDRenewalIntrestAmount.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"].ToString())) : "0";
                            //txtRenewalInterestRate.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"].ToString())) : "0";

                            //On 14/08/2018, to show only Interest amount after making tds voucher entry -------------------------------------------------------
                            //lblFDRenewalIntrestAmount.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"].ToString())) : "0";
                            //txtRenewalInterestRate.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"].ToString())) : "0";
                            if (this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["TDS_AMOUNT"].ToString()) > 0)
                            {
                                lblFDRenewalIntrestAmount.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["RENEWAL_INS_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["RENEWAL_INS_AMOUNT"].ToString())) : "0";
                                txtRenewalInterestRate.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["RENEWAL_INS_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["RENEWAL_INS_AMOUNT"].ToString())) : "0";
                            }
                            else
                            {
                                lblFDRenewalIntrestAmount.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"].ToString())) : "0";
                                txtRenewalInterestRate.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"].ToString())) : "0";
                            }
                            //------------------------------------------------------------------------------------------------------------------------------------

                            txtTDSAmount.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["TDS_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["TDS_AMOUNT"].ToString())) : "0";
                            dteRenewalOn.DateTime = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                            dteFDMaturityDate.DateTime = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;

                            //23/05/2022, to fix charge mode and charge amount
                            ChargeLedgerId = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.CHARGE_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.CHARGE_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                            cbPenaltyMode.SelectedIndex = resultArgsTransDetails.DataSource.Table.Rows[0]["CHARGE_MODE"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0]["CHARGE_MODE"].ToString()) : 0;
                            txtPenaltyAmount.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["CHARGE_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["CHARGE_AMOUNT"].ToString())) : "0";

                            if (RenewalType == FDRenewalTypes.IRI.ToString())
                            {
                                this.cboInterestMode.SelectedIndexChanged -= new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                cboInterestMode.SelectedIndex = 0;
                                this.cboInterestMode.SelectedIndexChanged += new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                fdAccountSystem.RenewedDate = dteRenewalOn.DateTime.AddDays(-1);
                                fdAccountSystem.FDAccountId = FDAccountId;
                                FDAccumulatedPrincipalAmount = fdAccountSystem.FetchAccumulatedInterestAmount();
                                if (fdTypes != FDTypes.WD || fdTypes != FDTypes.PWD)
                                {
                                    lblCapInterestLedger.Visibility = lblIntrestLedger.Visibility = lblIntrestLedgerAmt.Visibility = LayoutVisibility.Always;
                                    lblCapBankInterestLedger.Visibility = lblCashBankLedgerCap.Visibility = lblCashLedgerAmt.Visibility = emptySpaceItem16.Visibility = LayoutVisibility.Always;
                                }
                                lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAccumulatedPrincipalAmount);
                                if (InterestType == 0)
                                {
                                    CalculateInterestRate();
                                }
                                else
                                {
                                    cboInsType.SelectedIndex = InterestType;
                                    interestRate = FDInsRate / 100;
                                    double insAmount = CalculateTimesPerYear(dteFDMaturityDate.DateTime, dteRenewalOn.DateTime);
                                    insAmount = insAmount - FDAmount;
                                    lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount);
                                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount) + " " + TransactionMode.DR.ToString();
                                    FDAmount = FDAmount + FDAccumulatedPrincipalAmount;
                                    //chinna
                                    //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(insAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(insAmount);
                                }
                                glkpInterestLedger.EditValue = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName].ToString()) : 0;

                            }
                            else if (RenewalType == FDRenewalTypes.WDI.ToString() || RenewalType == FDRenewalTypes.PWD.ToString())
                            {
                                txtRenewalInterestRate.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["RENEWAL_INS_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["RENEWAL_INS_AMOUNT"].ToString())) : string.Empty;
                                lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text));
                                this.MaturityDate = resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_DATE"] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_DATE"].ToString() : string.Empty;
                                lblAssignMaturedon.Text = deDateOfMaturity.DateTime.ToShortDateString();

                                fdAccountSystem.RenewedDate = deCreatedDate.DateTime;
                                double Accumlated = fdAccountSystem.FetchAccumulatedInterestAmount();
                                double reInvested = fdAccountSystem.FetchReInvestedAmount();
                                double withdrawals = fdAccountSystem.FetchFullWithdrawal();
                                FDAmount = (FDAmount + Accumlated + reInvested) - withdrawals;
                                lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount);
                                //On 06/10/2022, to get withdrwal receipt voucher no
                                if (FDIntrestVoucherId > 0)
                                {
                                    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem(FDIntrestVoucherId))
                                    {
                                        txtWithdrwalReceiptVNo.Text = vouchersystem.VoucherNo;
                                    }
                                    glkpInterestLedger.EditValue = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                                }
                            }
                            else if (RenewalType == FDRenewalTypes.RIN.ToString())
                            {
                                double FDAmt = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName] != DBNull.Value ? UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()) : 0;

                                lblCapBankInterestLedger.Visibility = lblCashBankLedgerCap.Visibility = lblCashLedgerAmt.Visibility = emptySpaceItem16.Visibility = LayoutVisibility.Always;
                                txtWithdrawAmount.Text = resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(resultArgsTransDetails.DataSource.Table.Rows[0]["VOUCHER_AMOUNT"].ToString())) : "0";
                                dePostDate.DateTime = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                                lblAssignMaturedon.Text = deDateOfMaturity.DateTime.ToShortDateString();

                                fdAccountSystem.RenewedDate = deCreatedDate.DateTime;
                                FDReInvestedPrincipalAmount = fdAccountSystem.FetchReInvestedAmount();
                                double amount = FDReInvestedPrincipalAmount - this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text);
                                lblRINAmount.Text = this.UtilityMember.NumberSet.ToCurrency(amount);

                                fdAccountSystem.RenewedDate = dteRenewalOn.DateTime.AddDays(-1);
                                fdAccountSystem.FDAccountId = FDAccountId;
                                FDAccumulatedPrincipalAmount = fdAccountSystem.FetchAccumulatedInterestAmount();
                                lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAccumulatedPrincipalAmount);
                                lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmt + amount + FDAccumulatedPrincipalAmount);
                                glkpInterestLedger.EditValue = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                            }
                            else
                            {
                                this.cboInterestMode.SelectedIndexChanged -= new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                cboInterestMode.Text = this.GetMessage(MessageCatalog.Master.Mapping.ACCUMULATED_INTEREST);
                                cboInterestMode.SelectedIndex = 1;
                                cboInsType.SelectedIndex = InterestType;
                                this.cboInterestMode.SelectedIndexChanged += new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                fdAccountSystem.RenewedDate = dteRenewalOn.DateTime.AddDays(-1);
                                fdAccountSystem.FDAccountId = FDAccountId;
                                FDAccumulatedPrincipalAmount = fdAccountSystem.FetchAccumulatedInterestAmount();
                                lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAccumulatedPrincipalAmount);
                                if (InterestType == 0)
                                {
                                    CalculateInterestRate();
                                }
                                else
                                {
                                    interestRate = FDInsRate / 100;
                                    double InsAmount1 = CalculateTimesPerYear(dteFDMaturityDate.DateTime, dteRenewalOn.DateTime);
                                    InsAmount1 = InsAmount1 - FDAmount;
                                    lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount);
                                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount) + " " + TransactionMode.DR.ToString();
                                    FDAmount = FDAmount + FDAccumulatedPrincipalAmount;
                                    // lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(InsAmount1);
                                }
                                lblCapInterestLedger.Visibility = lblIntrestLedger.Visibility = lblIntrestLedgerAmt.Visibility = LayoutVisibility.Always;
                                lblCapBankInterestLedger.Visibility = lblCashBankLedgerCap.Visibility = lblCashLedgerAmt.Visibility = emptySpaceItem16.Visibility = LayoutVisibility.Never;
                                glkpInterestLedger.EditValue = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.INTEREST_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                            }
                            glkpBankInterestLedger.EditValue = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName].ToString()) : 0;
                            //    glkpInterestLedger.EditValue = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                            lblBankName.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Bank.BANKColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Bank.BANKColumn.ColumnName].ToString() : string.Empty;
                            lblAssignProject.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Project.PROJECTColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Project.PROJECTColumn.ColumnName].ToString() : "0";
                            lblAssignFDLedger.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString() : "0";
                            lblAssignAccountNumber.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString() : "0";
                            mtxtNotes.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString() : string.Empty;
                            txtExpectedMaturityRenewalValue.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName].ToString() : "0";
                            txtRenewalExpectedInteres.Text = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_INTEREST_VALUEColumn.ColumnName] != DBNull.Value ? resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_INTEREST_VALUEColumn.ColumnName].ToString() : "0";

                            glkpBranch.EditValue = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.BANK_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.BANK_IDColumn.ColumnName].ToString()) : 0;
                            ProjectId = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDAccount.PROJECT_IDColumn.ColumnName].ToString()) : 0;


                            //On 14/12/2023, To set FD Trans Mode
                            cboTransMode.SelectedIndex = 0;
                            if (RenewalType == FDRenewalTypes.ACI.ToString())
                            {
                                if (cboInterestMode.SelectedIndex == 1)
                                {
                                    string fdtransmode = resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_TRANS_MODEColumn.ColumnName] != DBNull.Value ?
                                                resultArgsTransDetails.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.FD_TRANS_MODEColumn.ColumnName].ToString() : string.Empty;
                                    if (fdtransmode.ToUpper() == TransSource.Cr.ToString().ToUpper())
                                    {
                                        cboTransMode.SelectedIndex = 1;
                                    }
                                }
                            }
                        }
                    }
                }

                //On 17/10/2024, To show fd details balance based on currency
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    ShowFDAmountDetailInCurrency();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally
            {
                CheckFDTransDateAndLockFDModification();
            }
        }


        private void CheckFDTransDateAndLockFDModification()
        {
            //As on 26/10/2021, To Check FD Trans Date (All renewals, Investment) should be with in current FY ---------------
            if (FDTypes.OP != fdTypes)
            {
                DateTime FDTransDate = FDTypes.RN == fdTypes ? this.UtilityMember.DateSet.ToDate(dteRenewalOn.DateTime.ToShortDateString(), false) :
                                                  FDTypes.POI == fdTypes ? this.UtilityMember.DateSet.ToDate(dePostDate.DateTime.ToShortDateString(), false) :
                                                  FDTypes.IN == fdTypes ? this.UtilityMember.DateSet.ToDate(deCreatedDate.DateTime.ToShortDateString(), false) :
                                                  (dteClosedOn.DateTime == DateTime.MinValue ? this.UtilityMember.DateSet.ToDate(dteRenewalOn.DateTime.ToShortDateString(), false) : this.UtilityMember.DateSet.ToDate(dteClosedOn.DateTime.ToShortDateString(), false));

                if ((FDTransDate != DateTime.MinValue) && (!(FDTransDate >= this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false)
                               && FDTransDate <= this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))))
                {
                    this.ShowMessageBox("Selected Fixed Detail doesn't fall on Current Financial Year, Change Financial Year and modify Renewal detail");
                    this.Close();
                }
            }
            //-------------------------------------------------------------------------------------------------------------------
        }

        private void AssignFDRenewalDetails(DataTable dtRenewal)
        {
            try
            {
                if (dtRenewal != null)
                {
                    using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                    {
                        foreach (DataRow dr in dtRenewal.Rows)
                        {
                            lblAssignLastRenewedOn.Text = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty;
                            dteRenewalOn.DateTime = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;
                            dteFDMaturityDate.DateTime = dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != null ? this.UtilityMember.DateSet.ToDate(dr[fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false) : DateTime.Now;

                            if (FDTypes.WD != fdTypes || fdTypes != FDTypes.PWD)
                            {
                                double InterestRate = dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToDouble(dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_RATEColumn.ColumnName].ToString()) : 0;
                                double PrincipalAmount = this.UtilityMember.NumberSet.ToDouble(FDAmount.ToString());
                                double InsAmount = dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToDouble(dr[fdAccountSystem.AppSchema.FDRenewal.INTEREST_AMOUNTColumn.ColumnName].ToString()) : 0;
                                double NoOfDays = 0;
                                if (dteFDMaturityDate.DateTime.Date != null) { NoOfDays = (dteFDMaturityDate.DateTime.Date - dteRenewalOn.DateTime.Date).TotalDays; }
                                lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(InsAmount);
                                FDAmount = FDAmount - InsAmount;

                                double IntrestAmount = FDAmount / 100 * InterestRate * NoOfDays / 365;
                                lblAssignPriniciapalintrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount);
                                lblAssignIntrestamount.Text = this.UtilityMember.NumberSet.ToCurrency(0);
                                lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount);

                                lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + InsAmount);
                                txtRenewalInterestRate.Text = IntrestAmount != 0 ? this.UtilityMember.NumberSet.ToNumber(IntrestAmount) : "0";
                                if (FDRenewalId == 0)
                                {
                                    //chinna
                                    //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(0) : " : " + this.UtilityMember.NumberSet.ToCurrency(0);
                                }
                                else
                                {
                                    //chinna
                                    // lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount);
                                }
                                string LastRenewalType = dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName] != null ? dr[fdAccountSystem.AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName].ToString() : string.Empty;
                                if (LastRenewalType == FDRenewalTypes.ACI.ToString())
                                {
                                    this.cboInterestMode.SelectedIndexChanged -= new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                    cboInterestMode.Text = this.GetMessage(MessageCatalog.Master.Mapping.ACCUMULATED_INTEREST);
                                    cboInterestMode.SelectedIndex = 1;
                                    this.cboInterestMode.SelectedIndexChanged += new System.EventHandler(this.cboInterestMode_SelectedIndexChanged);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally
            {
                CheckFDTransDateAndLockFDModification();
            }
        }

        /// <summary>
        /// This is to calculate the interest amount based on the type(Int.rec/Acc.Int)
        /// Percentage,Interest Type(Int.Rec/Acc.Int) and Interest Mode (Simple/Compund).
        /// Steps To Caluclate the Interest Amount: No of Days:Prinicpalamount/100*Int.rate*No of days/365
        /// No of Days: MaturityDate-RenewalDate/PostInterestDate
        /// </summary>
        /// <params>Interest percentage,Date of Inv/opening/Renewal/Withdarwal date,Int.Mode</params>
        private void CalculateInterestRate()
        {
            double InsAmount = 0;
            try
            {
                double NoOfDays = 0;
                if (dteFDMaturityDate.DateTime.Date != DateTime.MinValue.Date)
                { NoOfDays = (dteFDMaturityDate.DateTime.Date - dteRenewalOn.DateTime.Date).TotalDays; }
                if (RenewalType == FDRenewalTypes.ACI.ToString())
                {
                    InsAmount = (FDAmount / 100 * FDInsRate * NoOfDays / 365);
                    lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount);
                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount) + " " + TransactionMode.DR.ToString();
                    FDAmount = FDAmount + FDAccumulatedPrincipalAmount;
                }
                else
                {
                    InsAmount = (FDAmount / 100 * FDInsRate * NoOfDays / 365);
                    lblAssignPrinicipalAmount.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount);
                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(FDAmount + FDAccumulatedPrincipalAmount) + " " + TransactionMode.DR.ToString();
                    FDAmount = FDAmount + FDAccumulatedPrincipalAmount;
                }
                //chinna
                //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(InsAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(InsAmount);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }

            //On 17/10/2024, To show fd details balance based on currency
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ShowFDAmountDetailInCurrency();
            }
        }

        private void GetLastFDRenewalDate()
        {
            DataTable dtFdRenewal = new DataTable();
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = fdAccountId;
                fdAccountSystem.FDVoucherId = FDVoucherId;
                fdAccountSystem.FDRenewalId = FDRenewalId;

                //On 12/01/2024, to show proper last post on date for POI
                //resultArgs = fdTypes != FDTypes.POI || fdTypes != FDTypes.RIN ? fdAccountSystem.GetLastRenewalDate() : fdAccountSystem.GetNoOfPostInterests();
                resultArgs = fdTypes != FDTypes.POI ? fdAccountSystem.GetLastRenewalDate() : fdAccountSystem.GetNoOfPostInterests();

                //temp
                // On 07/09/2022, since existing, they dont get withdrwal/partial amount when we edit from voucher view receipt screen
                // modify receipt interest voucher
                fdAccountSystem.FDContraVoucherId = 0;
                if (fdTypes == FDTypes.WD || fdTypes == FDTypes.PWD)
                {
                    fdAccountSystem.FDContraVoucherId = VoucherId;
                }
                //----------------------------------------------------------------------------------------------------------

                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    lblAssignNoOfRenewal.Text = resultArgs.DataSource.Table.Rows.Count.ToString();
                    dtFdRenewal = resultArgs.DataSource.Table.AsEnumerable().Reverse().Take(1).CopyToDataTable();
                    //this.RenewalDate = dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty;
                    lblAssignLastRenewedOn.Text = dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty;

                    if (fdTypes != FDTypes.POI)
                    {
                        if (resultArgs.Success)
                        {
                            resultArgs = fdAccountSystem.GetMaturityValue();

                            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                lblFDRenewalIntrestAmount.Text = resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][fdAccountSystem.AppSchema.FDRenewal.EXPECTED_MATURITY_VALUEColumn.ColumnName].ToString())) : string.Empty;
                            }
                        }
                    }

                    //On 01/11/2023, To Show latest matured on for POI also
                    lblAssignMaturedon.Text = dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty;
                    /*// This is Make Empty the FD Type Post Interest Value Details 
                    if (fdTypes != FDTypes.POI)
                    {
                        lblAssignMaturedon.Text = dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.MATURITY_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty;
                    }
                    else
                    {
                        lblLastMaturedon.Text = lblAssignMaturedon.Text = " ";
                    }*/

                    this.dteClosedOn.EditValueChanged -= new System.EventHandler(this.dteClosedOn_EditValueChanged);
                    //dteClosedOn.DateTime = ; //-- aldrin
                    this.dteClosedOn.EditValueChanged += new System.EventHandler(this.dteClosedOn_EditValueChanged);

                }
                WithdrawAmount = fdAccountSystem.FetchWithdrawalAmount();
            }
        }

        private void GetLastFDReInvestmentDate()
        {
            DataTable dtFdRenewal = new DataTable();
            using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
            {
                fdAccountSystem.FDAccountId = fdAccountId;
                fdAccountSystem.FDVoucherId = FDVoucherId;
                fdAccountSystem.FDRenewalId = FDRenewalId;
                resultArgs = fdAccountSystem.GetNoOfReInvestment();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    lblNoofRIN.Text = resultArgs.DataSource.Table.Rows.Count.ToString();

                    dtFdRenewal = resultArgs.DataSource.Table.AsEnumerable().Reverse().Take(1).CopyToDataTable();

                    lblRINDate.Text = dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(dtFdRenewal.Rows[0][fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName].ToString(), false).ToShortDateString() : string.Empty;

                    this.dteClosedOn.EditValueChanged -= new System.EventHandler(this.dteClosedOn_EditValueChanged);
                    this.dteClosedOn.EditValueChanged += new System.EventHandler(this.dteClosedOn_EditValueChanged);
                }
            }
        }

        private void EnableControls()
        {
            try
            {
                if (FDRenewalCount != 0)
                {
                    if (fdTypes == FDTypes.IN)
                    {
                        glkpProDetails.Enabled = glkpCashBankLedgers.Enabled = glkpFDLedgerDetails.Enabled = false;
                    }
                    else
                    {
                        glkpProDetails.Enabled = glkpLedgers.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void CompoundInterest()
        {
            try
            {
                if (fdTypes == FDTypes.POI)
                {
                    interestRate = this.UtilityMember.NumberSet.ToDouble(PostInterestRate.ToString());

                    if (cboInterestMode.SelectedIndex == 0)
                    {
                        amount = FDAmount;
                    }
                    else
                    {
                        amount = FDAmount + this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                    }

                    interestRate = interestRate / 100;
                    IntrestCalculatedAmount = CalculateTimesPerYear(PostInterestMaturityDate, PostInterestCreatedDate);
                    IntrestCalculatedAmount = IntrestCalculatedAmount != 0 ? IntrestCalculatedAmount - amount : 0;
                    IntrestAmount = this.UtilityMember.NumberSet.ToDouble(IntrestCalculatedAmount.ToString());

                    if (fdTypes == FDTypes.POI)
                    {
                        lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(IntrestCalculatedAmount.ToString()));
                        //txtRenewalInterestRate.Text = Math.Round(IntrestCalculatedAmount, 2).ToString();
                    }
                    else
                    {
                        //chinna
                        // 06.10.2017
                        //lblMaturityDate.Text = deDateOfMaturity.DateTime != DateTime.MinValue.Date ? deDateOfMaturity.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount);
                    }
                }

                else if (UtilityMember.NumberSet.ToDecimal(txtIntrestRate.Text) != 0 || UtilityMember.NumberSet.ToDecimal(txtFdInterestRate.Text) != 0)
                {
                    if (fdTypes != FDTypes.RN)
                    {
                        amount = this.UtilityMember.NumberSet.ToDouble(txtAmount.Text);
                        interestRate = this.UtilityMember.NumberSet.ToDouble(txtIntrestRate.Text);
                    }
                    else if (fdTypes == FDTypes.RN)
                    {
                        interestRate = this.UtilityMember.NumberSet.ToDouble(txtFdInterestRate.Text);

                        if (cboInterestMode.SelectedIndex == 0)
                        {
                            amount = FDAmount;
                        }
                        else
                        {
                            amount = FDAmount + this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                        }
                    }

                    interestRate = interestRate / 100;
                    if (fdTypes == FDTypes.RN)
                    {
                        IntrestCalculatedAmount = CalculateTimesPerYear(dteFDMaturityDate.DateTime, dteRenewalOn.DateTime);
                    }

                    else if (fdTypes == FDTypes.OP || fdTypes == FDTypes.IN)
                    {
                        IntrestCalculatedAmount = CalculateTimesPerYear(deDateOfMaturity.DateTime, deCreatedDate.DateTime);
                    }
                    IntrestCalculatedAmount = IntrestCalculatedAmount != 0 ? IntrestCalculatedAmount - amount : 0;
                    IntrestAmount = this.UtilityMember.NumberSet.ToDouble(IntrestCalculatedAmount.ToString());
                    if (fdTypes == FDTypes.RN)
                    {
                        //chinna
                        //lblAssignFDMaturedOn.Text = dteFDMaturityDate.DateTime != DateTime.MinValue.Date ? dteFDMaturityDate.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount);
                    }
                    else
                    {
                        //chinna
                        //06.10.2017
                        //lblMaturityDate.Text = deDateOfMaturity.DateTime != DateTime.MinValue.Date ? deDateOfMaturity.DateTime.ToShortDateString() + " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount) : " : " + this.UtilityMember.NumberSet.ToCurrency(IntrestAmount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }

        }

        private double CalculateTimesPerYear(DateTime dtStartDate, DateTime dtEndDate)
        {
            double timesPerNumber = 0;
            double timesPerYear = 0;
            double values = 0;
            double InsAmount = 0;
            NoOfDays = (dtStartDate.Date - dtEndDate.Date).TotalDays;
            if (NoOfDays != 0)
            {
                values = NoOfDays / 30;

                timesPerNumber = values / 3;

                timesPerYear = values / 12;
                // (1 + r/n)
                double body = 1 + (interestRate / timesPerNumber);
                // nt
                timesPerYear = timesPerNumber * timesPerYear;

                double exponent = body * timesPerYear;
                InsAmount = amount * Math.Pow(body, exponent);
            }
            else
            {
                InsAmount = 0;
            }
            // P(1 + r/n)^nt
            return InsAmount;
        }

        private void SetAlignmentForLanguage()
        {
            if (this.AppSetting.LanguageId.Equals("pt-PT"))
            {
                if (fdTypes.Equals(FDTypes.OP))
                {
                    SetAlignment(TextAlignModeItem.UseParentOptions);
                }
                else if (fdTypes.Equals(FDTypes.IN))
                {
                    SetAlignment(TextAlignModeItem.UseParentOptions);
                }
                else if (fdTypes.Equals(FDTypes.RN))
                {
                    SetAlignment(TextAlignModeItem.UseParentOptions);
                    emptySpaceItem18.Width = 330;
                    lblFDDateOfMaturity.Width = 200;
                    this.Width = 870;
                }
                else
                {
                    SetAlignment(TextAlignModeItem.UseParentOptions);
                    //emptySpaceItem13.Width = 0;
                    emptySpaceItem18.Width = 316;
                    lblClosedOn.Width = 230;
                    this.Width = 870;
                }
            }
            else if (this.AppSetting.LanguageId.Equals("id-ID"))
            {
                if (fdTypes.Equals(FDTypes.OP))
                {
                    SetAlignment(TextAlignModeItem.UseParentOptions);
                    //chinna
                    //06.10.2017
                    //lblMaturityDate.TextAlignMode = TextAlignModeItem.UseParentOptions; 
                    lblExpectedMaturityAmountValue.Width = 170;
                    this.Width = 860;
                }
                else if (fdTypes.Equals(FDTypes.IN))
                {
                    SetAlignment(TextAlignModeItem.UseParentOptions);
                    lblExpectedMaturityAmountValue.Width = 170;
                    this.Width = 860;
                }
                else if (fdTypes.Equals(FDTypes.RN))
                {
                    //chinna
                    //lblAssignFDMaturedOn.Width = 150;
                    lblFDRenewalOn.TextAlignMode = TextAlignModeItem.AutoSize;
                    lcRenewalVoucherNo.TextAlignMode = TextAlignModeItem.UseParentOptions;
                    lblRenewalType.TextAlignMode = TextAlignModeItem.UseParentOptions;
                    layoutControlItem2.TextAlignMode = TextAlignModeItem.CustomSize;
                    layoutControlItem2.Width = 190;
                    //chinna
                    ExpectedRenewalMaturityValue.Width = 200;
                    emptySpaceItem18.Width = 330;
                    this.Width = 870;
                }
                else
                {
                    SetAlignment(TextAlignModeItem.UseParentOptions);
                    emptySpaceItem17.Width = 10;
                    emptySpaceItem18.Width = 306;
                    lblClosedOn.Width = 250;
                    this.Width = 860;
                }
            }
        }

        /// <summary>
        ///On 26/10/2023, To enable FD Renewal Trans Mode only for Post Interest alone 
        /// </summary>
        private void EnableFDRenewalTransMode()
        {
            //On 26/10/2023, To enable FD Renewal Trans Mode only for Post Interest alone --------------------------------------------------------
            lcFDTransMode.Visibility = LayoutVisibility.Never;
            if (this.AppSetting.EnableFDAdjustmentEntry == 1 && cboInterestMode.SelectedIndex == 1 &&
                (this.fdTypes == FDTypes.POI || this.fdTypes == FDTypes.RN))
            {
                lcFDTransMode.Visibility = LayoutVisibility.Always;
            }
            //----------------------------------------------------------------------------------------------------------------------------
        }

        private void SetAlignment(TextAlignModeItem txtAlignModeItem)
        {
            if (fdTypes.Equals(FDTypes.OP))
            {
                lblLedgerName.TextAlignMode = lblProject.TextAlignMode = txtAlignModeItem;
                lblAmount.TextAlignMode = lblDateofMaturity.TextAlignMode = txtAlignModeItem;
            }
            else if (fdTypes.Equals(FDTypes.IN))
            {
                lblLedgerName.TextAlignMode = lblProject.TextAlignMode = txtAlignModeItem;
                lblAmount.TextAlignMode = lblDateofMaturity.TextAlignMode = txtAlignModeItem;
                lcVoucherNo.TextAlignMode = txtAlignModeItem;
                lblLedgerFrom.TextAlignMode = lblFDLedgerTo.TextAlignMode = txtAlignModeItem;
            }
            else if (fdTypes.Equals(FDTypes.RN))
            {
                lblRenewalType.TextAlignMode = txtAlignModeItem;
                lcRenewalVoucherNo.TextAlignMode = txtAlignModeItem;
                lblFDRenewalOn.TextAlignMode = lblFDReceiptNo.TextAlignMode = lblFDDateOfMaturity.TextAlignMode = txtAlignModeItem;

                // chinna, lblAssignFDMaturedOn,simpleLabelItem2
                //layoutControlItem2.TextAlignMode = lblFDInterestRate.TextAlignMode = simpleLabelItem2.TextAlignMode = lblAssignFDMaturedOn.TextAlignMode = txtAlignModeItem;

                layoutControlItem2.TextAlignMode = lblFDInterestRate.TextAlignMode = txtAlignModeItem;
                lblBankName.TextAlignMode = lblTotalAmount.TextAlignMode = lblAssignTotalAmt.TextAlignMode = TextAlignModeItem.CustomSize;
                lblRenewalProject.TextAlignMode = lblAssignProject.TextAlignMode = TextAlignModeItem.CustomSize;
                lblFDLedgerName.TextAlignMode = lblAssignFDLedger.TextAlignMode = TextAlignModeItem.CustomSize;
                lblLastRenewedOn.TextAlignMode = lblAssignLastRenewedOn.TextAlignMode = TextAlignModeItem.CustomSize;
                simpleLabelItem3.TextAlignMode = lblAssignNoOfRenewal.TextAlignMode = TextAlignModeItem.UseParentOptions;
            }
            else
            {
                lblRenewalType.TextAlignMode = txtAlignModeItem;
                lcRenewalVoucherNo.TextAlignMode = txtAlignModeItem;
                lblRenewalType.TextAlignMode = txtAlignModeItem;
                lblBankName.TextAlignMode = lblTotalAmount.TextAlignMode = lblAssignTotalAmt.TextAlignMode = TextAlignModeItem.CustomSize;
                lblRenewalProject.TextAlignMode = lblAssignProject.TextAlignMode = TextAlignModeItem.CustomSize;
                lblFDLedgerName.TextAlignMode = lblAssignFDLedger.TextAlignMode = TextAlignModeItem.CustomSize;
                lblLastRenewedOn.TextAlignMode = lblAssignLastRenewedOn.TextAlignMode = TextAlignModeItem.CustomSize;
                simpleLabelItem3.TextAlignMode = lblAssignNoOfRenewal.TextAlignMode = TextAlignModeItem.CustomSize;
                lblClosedOn.TextAlignMode = TextAlignModeItem.UseParentOptions;
            }
        }

        private int CheckRenewalDuplicate()
        {
            try
            {
                using (FDRenewalSystem fdRenewalSystem = new FDRenewalSystem())
                {
                    fdRenewalSystem.FDAccountId = fdAccountId;
                    if (fdTypes.Equals(FDTypes.RN))
                        fdRenewalSystem.RenewedDate = this.UtilityMember.DateSet.ToDate(dteRenewalOn.DateTime.ToShortDateString(), false);
                    else
                        fdRenewalSystem.RenewedDate = this.UtilityMember.DateSet.ToDate(dteClosedOn.DateTime.ToShortDateString(), false);
                    resultArgs = fdRenewalSystem.CheckDuplicateRenewal(FDRenewalId);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// This is to calculate the withDrawal Interest amount based on the WithDrawal Date.
        /// </summary>
        ///  Current Prinicipal Amount / 100 * interestRate * NoOfDays / 365
        ///  No of days=Current Withdrawasl Date Given-Maturity Date
        /// <params>Withdrawal Date,Interest Rate and Interest percentage.</params>
        private void CalculateWithdrawInsAmount()
        {
            double NoOfDays = 0;
            if ((fdTypes.Equals(FDTypes.WD) || fdTypes.Equals(FDTypes.PWD)) && FDAccountId == 0 && FDVoucherId == 0)
            {
                using (FDAccountSystem fdaccountsystem = new FDAccountSystem())
                {
                    fdaccountsystem.FDAccountId = fdAccountId;
                    if (fdaccountsystem.HasFDWithdrawal() > 0)
                    {
                        if (dteClosedOn.DateTime < this.UtilityMember.DateSet.ToDate(this.MaturityDate, false))
                        // if (this.UtilityMember.DateSet.ToDate(this.MaturityDate, false) < dteClosedOn.DateTime)
                        {
                            if (dteClosedOn.DateTime >= this.UtilityMember.DateSet.ToDate(this.RenewalDate, false))
                            {
                                if (dteClosedOn.DateTime.Date != DateTime.MinValue.Date)
                                {
                                    NoOfDays = (dteClosedOn.DateTime.Date - this.UtilityMember.DateSet.ToDate(this.RenewalDate, false).Date).TotalDays;
                                }
                                IntrestAmount = this.FDAmount / 100 * interestRate * NoOfDays / 365;
                                txtRenewalInterestRate.Text = this.UtilityMember.NumberSet.ToNumber(IntrestAmount);
                                lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToNumber(IntrestAmount);//added by sugan---to calculate the partial withdraw interest amount
                            }
                            else if (dteClosedOn.DateTime != DateTime.MinValue)
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.WITHDRAW_DATE_GREATER_THAN_RENEWAL_DATE));
                                txtRenewalInterestRate.Text = this.UtilityMember.NumberSet.ToNumber(0);
                            }
                        }
                        else if (dteClosedOn.DateTime >= this.UtilityMember.DateSet.ToDate(this.MaturityDate, false))
                        {
                            if (dteClosedOn.DateTime.Date != DateTime.MinValue.Date)
                            {
                                NoOfDays = (dteClosedOn.DateTime.Date - this.UtilityMember.DateSet.ToDate(this.RenewalDate, false).Date).TotalDays;
                            }
                            IntrestAmount = this.FDAmount / 100 * interestRate * NoOfDays / 365;
                            txtRenewalInterestRate.Text = this.UtilityMember.NumberSet.ToNumber(IntrestAmount);
                            lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToNumber(IntrestAmount);//added by sugan---to calculate the partial withdraw interest amount
                        }
                    }
                    else
                    {
                        if (dteClosedOn.DateTime != DateTime.MinValue && dteClosedOn.DateTime < PostInterestCreatedDate)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.WITHDRAW_DATE_GREATER_THAN_CREATED_DATE));
                            txtRenewalInterestRate.Text = this.UtilityMember.NumberSet.ToNumber(0);
                        }
                        else if (dteClosedOn.DateTime > PostInterestCreatedDate && dteClosedOn.DateTime < this.UtilityMember.DateSet.ToDate(this.MaturityDate, false))
                        {
                            if (dteClosedOn.DateTime.Date != DateTime.MinValue.Date)
                            {
                                NoOfDays = (dteClosedOn.DateTime.Date - PostInterestCreatedDate.Date).TotalDays;
                            }
                            IntrestAmount = this.FDAmount / 100 * interestRate * NoOfDays / 365;
                            txtRenewalInterestRate.Text = this.UtilityMember.NumberSet.ToNumber(IntrestAmount);

                            // if the withdrwal screen date is below maturity amount is changed -19.10.2017

                            // lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToNumber(IntrestAmount);
                        }
                    }
                }

            }
        }

        private ResultArgs FetchProjectDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs;
        }

        private bool isValidProject()
        {
            bool isValid = true;
            try
            {
                resultArgs = FetchProjectDetails();
                DataView dvProject = resultArgs.DataSource.Table.DefaultView;
                dvProject.RowFilter = "PROJECT_ID=" + this.UtilityMember.NumberSet.ToInteger(glkpProDetails.EditValue.ToString()) + "";
                if (dvProject != null && dvProject.Count > 0)
                {
                    DateTime dtDateFrom = this.UtilityMember.DateSet.ToDate(dvProject[0]["DATE_STARTED"].ToString(), false);
                    if (deCreatedDate.DateTime < dtDateFrom)
                    {
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return isValid;
        }

        /// <summary>
        /// On 21/09/2021, To FD date is locked or not
        /// </summary>
        /// <returns></returns>
        private bool IsVoucherDateLocked()
        {
            bool rtn = false;
            DateTime dFDDate = DateTime.MinValue;
            Int32 Pid = glkpProDetails.EditValue == null ? ProjectId : this.UtilityMember.NumberSet.ToInteger(glkpProDetails.EditValue.ToString());
            try
            {
                if (Pid > 0)
                {
                    string Pname = glkpProDetails.EditValue == null ? lblAssignProject.Text : glkpProDetails.Text.ToString();
                    using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                    {
                        if (fdTypes.Equals(FDTypes.WD) || fdTypes.Equals(FDTypes.PWD))
                        {
                            dFDDate = dteClosedOn.DateTime;
                        }
                        else if (fdTypes.Equals(FDTypes.IN))
                        {
                            dFDDate = deCreatedDate.DateTime;
                        }
                        else if (fdTypes.Equals(FDTypes.RN))
                        {
                            dFDDate = dteRenewalOn.DateTime;
                        }
                        else if (fdTypes.Equals(FDTypes.POI))
                        {
                            dFDDate = dePostDate.DateTime;
                        }

                        rtn = base.IsVoucherLockedForDate(Pid, dFDDate, true);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
                rtn = true;
            }
            finally { }

            return rtn;
        }


        /// <summary>
        /// On 08/11/2021, To check FD account is closed or not
        /// </summary>
        /// <returns></returns>
        private bool IsFDAccountClosed()
        {
            bool rtn = false;

            try
            {
                using (FDAccountSystem fdaccountsystem = new FDAccountSystem())
                {
                    fdaccountsystem.FDAccountId = FDAccountId;
                    rtn = (fdaccountsystem.CheckFDRenewalClosed() > 0);
                    if (rtn)
                    {
                        this.ShowMessageBox("As Fixed Deposit Account is already closed, You could not modify it. Please Re-Open this Account in Closed FD Details screen and modify.");
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
                rtn = true;
            }
            finally { }
            return rtn;
        }

        /// <summary>
        /// This is to check wheather withdrawal amount exceeds the prinicaipal amount in each 	withdrawal.
        /// Steps To find check Exceeds the PrincipalAmount: 1.Amount entered in "withdrawal Amount textBox"
        /// 2.Check WithdrawalAmount > CurrentPrincipla amount
        /// </summary>
        /// <returns></returns>
        /// <Params>PrinicpalAmount</Params>
        private bool IsExceedsthePrincipalAmount()
        {
            bool isExceed = false;
            try
            {
                if (fdAccountId != 0 && FDRenewalId != 0)
                {
                    /* if (InvestmentAmount != 0 && Withdrawals != 0)
                     {
                         if ((InvestmentAmount + AcutalAccumulatedInsAmount + ReInvestedAmount) < (Withdrawals + ((this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text)) - WithdrawAmount)))
                         {
                             this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.BreakUp.FD_WITHDRAWALAMOUNT_EXCEED));
                             isExceed = true;
                         }
                     }*/

                    if ((FDAmount - WithdrawAmount) < (Withdrawals + ((this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text)) - WithdrawAmount)))
                    {
                        this.ShowMessageBox("Withdrawal amount should be less than the balance amount.");
                        isExceed = true;
                    }
                }
                else if (fdAccountId != 0 && FDRenewalId == 0)
                {
                    if (FDAmount < this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.BreakUp.FD_WITHDRAWALAMOUNT_EXCEED));
                        isExceed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex);
            }
            return isExceed;
        }

        //***************************************************************************************************
        /// <summary>
        /// by aldrin
        /// While edit the FD renwals, when the intrest mount is 0 then delete vouchers in the voucher masters trans and voucher trans
        /// </summary>
        private bool DeleteFDVouchers()
        {
            bool Istrue = true;
            using (FDAccountSystem fdaccount = new FDAccountSystem())
            {
                resultArgs = fdaccount.DeleteIntrestVoucher(this.VoucherId);
                if (resultArgs.Success)
                {
                    Istrue = true;
                }
                else
                {
                    Istrue = false;
                    this.ShowMessageBox(resultArgs.Message + Environment.NewLine + resultArgs.Exception);
                }
            }
            return Istrue;
        }
        #endregion

        #region PostInterestEvents
        private void dePostDate_EditValueChanged(object sender, EventArgs e)
        {
            //if (cboInsType.SelectedIndex == 0)
            //{
            //    CalculateInterestAmount();
            //}
            //else
            //{
            //    CompoundInterest();
            //}
            PostDateLeave();
            LoadCashBankLedger(glkpBankInterestLedger);

            //On 20/10/2021, To skip ledger closed date -------------
            //LoadLedger(glkpInterestLedger);

            if (fdTypes == FDTypes.RIN)
            {
                FetchLeder(glkpInterestLedger);
            }
            else
            {
                LoadLedger(glkpInterestLedger);
            }
            //------------------------------------------------
        }

        private void dePostDate_Leave(object sender, EventArgs e)
        {
            // PostDateLeave();
        }

        private void PostDateLeave()
        {
            if (cboInsType.SelectedIndex == 0)
            {
                CalculateInterestAmount();
            }
            else
            {
                CompoundInterest();
            }
            if (FDRenewalId == 0)
            {
                //On 24/10/2024, after changing post interest date, values is cleared ------------------------------
                //On 28/10/2022
                //txtRenewalInterestRate.Text = "0"; //Math.Round(IntrestCalculatedAmount, 2).ToString(); // depostdate_leave
                //---------------------------------------------------------------------------------------------------

                lblFDRenewalIntrestAmount.Text = this.UtilityMember.NumberSet.ToCurrency(IntrestCalculatedAmount);

                //On 04/10/2022, to Load Voucher no only newly renewal
                LoadVoucherNo(fdTypes);
            }

            //On 04/10/2022, to Load Voucher no only newly renewal
            //LoadVoucherNo(fdTypes);
        }
        #endregion

        private void txtWithdrawAmount_EditValueChanged(object sender, EventArgs e)
        {
            if (fdTypes == FDTypes.RIN)
            {
                double Value = FDAmount + FDAccumulatedPrincipalAmount + FDReInvestedPrincipalAmount;
                lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(Value + this.UtilityMember.NumberSet.ToDouble(txtWithdrawAmount.Text)) + " " + TransactionMode.DR.ToString();
            }

            //On 24/10/2024, To get proper amount and assigned to currency amount
            SetCurrencyAmount();
        }

        private void cbPenaltyMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPenaltyMode.SelectedIndex == 0)
            {
                txtPenaltyAmount.Text = string.Empty;
                glkpPenaltyLedger.EditValue = 0;
            }
        }

        private void popupecFD_Click(object sender, EventArgs e)
        {

        }

        private void cboTransMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lcFDTransMode.Visibility == LayoutVisibility.Always && (this.fdTypes == FDTypes.POI || this.fdTypes == FDTypes.RN))
            {
                lciTDSAmount.Visibility = (cboTransMode.SelectedIndex == 1 || this.AppSetting.IsCountryOtherThanIndia ? LayoutVisibility.Never : LayoutVisibility.Always);
            }

            if (fdTypes == FDTypes.RN || fdTypes == FDTypes.POI)
            {
                if (cboInterestMode.SelectedIndex == 1)
                {
                    double dinterestamt = this.UtilityMember.NumberSet.ToDouble(txtRenewalInterestRate.Text);
                    dinterestamt *= (this.AppSetting.EnableFDAdjustmentEntry == 1 && cboTransMode.Text.ToUpper() == TransSource.Cr.ToString().ToUpper() ? -1 : 1);
                    lblAssignTotalAmt.Text = this.UtilityMember.NumberSet.ToCurrency(this.UtilityMember.NumberSet.ToDouble(FDAmount.ToString()) + dinterestamt) + " " + TransactionMode.DR.ToString();
                }
                else
                {
                    lblAssignTotalAmt.Text = lblAssignPrinicipalAmount.Text;
                }
            }
        }

        private void frmFDAccount_Activated(object sender, EventArgs e)
        {
            if (fdTypes == FDTypes.IN)
                ShowMutualFundProperties(glkpFDLedgerDetails);
            else
                ShowMutualFundProperties(glkpLedgers);
        }

        private void glkpCurrencyCountry_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ShowCurrencyDetails();

                if (fdTypes == FDTypes.OP || fdTypes == FDTypes.IN)
                {
                    if (fdTypes == FDTypes.OP)
                    {
                        FetchLeder(glkpLedgers);
                    }
                    else if (fdTypes == FDTypes.IN)
                    {
                        FetchLeder(glkpFDLedgerDetails);
                    }
                    glkpProDetails.Select();
                    glkpProDetails.Focus();
                }
            }
        }

        private void txtCurrencyAmount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) >= 0)
            {
                CalculateExchangeRate();
                //SetCurrencyAmount();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtCurrencyAmount.Text = "0";
                CalculateExchangeRate();
            }
        }

        private void txtExchangeRate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text) >= 0)
            {
                CalculateExchangeRate();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtExchangeRate.Text = "1";
                CalculateExchangeRate();
            }
        }


    }
}
