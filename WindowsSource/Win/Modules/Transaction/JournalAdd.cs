///On 09/02/2018 DuplicateVoucher Ref_01 : To make duplication voucher (repeat selected voucher one more time), Logic is show selected voucher details edit mode, 
/// it will display existing voucher detail and make voucherid to 0 or change add mode...(Remove VoucherId column in Transaction grid and Cash/Bank Grid), it was desinged voucher id 
/// should not be there in Add/Mode

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Repository;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Transaction;
using DevExpress.Utils.Frames;
using DevExpress.XtraGrid;
using ACPP.Modules.Master;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Text.RegularExpressions;
using AcMEDSync.Model;
using Bosco.Model.TDS;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Report.Base;

namespace ACPP.Modules.Transaction
{
    public partial class JournalAdd : frmFinanceBaseAdd
    {

        #region Decelaration
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        private DataSet dsCostCentre = new DataSet();
        private int TDSBookingFromPartyPayment = 0;
        bool isMouseClicked = false;
        bool isMultiCashBankEntry = false;
        private DateTime deMaxDate { get; set; }

        //On 24/07/2023, Voucher Authorization details
        public int AuthorizedStatus { get; set; }
        #endregion

        #region GstProperty
        public double gstCalcAmount = 0.0;
        public double cgstCalcAmount = 0.0;
        public double sgstCalcAmount = 0.0;
        public double igstCalcAmount = 0.0;

        private enum AdditionButttons
        {
            VendorGSTInvoiceDetails,
            SubLedgerDetails
        }

        public double UpdateGST = 0.0;

        private Int32 GSTInvoiceId { get; set; }
        private string GSTVendorInvoiceNo { get; set; }
        private string GSTVendorInvoiceDate { get; set; }
        private Int32 GSTVendorInvoiceType { get; set; }
        private Int32 GSTVendorId { get; set; }
        private DataTable DtGSTInvoiceMasterDetails = null;
        private DataTable DtGSTInvoiceMasterLedgerDetails = null;

        private bool IsGeneralInvolice
        {
            get
            {
                return (this.AppSetting.AllowMultiCurrency == 1 || this.AppSetting.IsCountryOtherThanIndia);
            }
        }

        private bool CanShowVendorGST
        {
            get
            {
                bool rtn = false;
                using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem())
                {
                    bool canshow = (colGStAmt.Visible || IsGeneralInvolice);
                    if (canshow && this.AppSetting.IncludeGSTVendorInvoiceDetails == "2")
                    {
                        if (colGStAmt.Visible)
                        {
                            DataTable dtVoucherTrans = gcTransaction.DataSource as DataTable;
                            if (dtVoucherTrans != null)
                            {
                                DataTable dtGSTLedgers = dtVoucherTrans.DefaultView.ToTable();
                                /*DataTable dtGSTLedgers = dtVoucherTrans.DefaultView.ToTable();
                                string gstledgers = vsystem.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName + ">0 AND " +
                                                    vsystem.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName + " = 1";
                                dtGSTLedgers.DefaultView.RowFilter = gstledgers;
                                rtn = (dtGSTLedgers.DefaultView.Count > 0);*/

                                string gstledgers = vsystem.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName + " = 1";
                                dtGSTLedgers.DefaultView.RowFilter = gstledgers;
                                rtn = (dtGSTLedgers.DefaultView.Count > 0);

                                /* //On 20/10/2023 Allow GST Invoice always without GST Amount
                                 * double cgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(CGST)", "").ToString());
                                double sgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(SGST)", "").ToString());
                                double igst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(IGST)", "").ToString());
                                rtn = (cgst > 0 || sgst > 0 || igst > 0);*/
                            }
                        }
                        else
                        {
                            rtn = true;
                        }
                    }
                }
                return rtn;
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
                double liveexchangerate = UtilityMember.NumberSet.ToDouble(lblLiveExchangeRate.Text);
                double actalamount = UtilityMember.NumberSet.ToDouble(txtActualAmount.Text);

                return (currencycountry > 0 && currencyamt > 0 && exchagnerate > 0 && actalamount > 0 && liveexchangerate > 0);
            }
        }

        private bool IsLocalCurrencyVoucher
        {
            get
            {
                Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                bool rtn = (this.AppSetting.IsCountryOtherThanIndia || this.AppSetting.AllowMultiCurrency == 1);

                if (rtn)
                {
                    rtn = (currencycountry == UtilityMember.NumberSet.ToInteger(this.AppSetting.Country));
                }
                return rtn;
            }
        }

        #endregion

        #region TDS Property
        private int NatureofPaymentId { get; set; }
        private DataSet dsTDSBooking = new DataSet();
        private DataSet dsTDSDeductionLater = new DataSet();
        public int BookingId { get; set; }
        public int ExpenseLedgerId { get; set; }
        public int DeducteeTypeLedgerId { get; set; }
        public int TDSPartyLedgerId { get; set; }
        private int LastFocusedRowHandle { get; set; }
        private int TDSLedgerId { get; set; }
        Dictionary<int, double> ExpAmount = new Dictionary<int, double>();
        private bool isTaxDeductable = false;
        private bool isDeductTDS = true;
        private int IsAlreadyDeducted { get; set; }
        private double ExemptionLimitAmount { get; set; }
        private int TaxLedgerId { get; set; }
        //  private bool isTaxDeducted = true;
        private DataTable dtPartyTrans { get; set; }
        private string TDSPartyNarration { get; set; }
        private DataView dvTransUcSummary { get; set; }
        private string TDSPartyVoucherNo { get; set; }
        private double ExemptionLimit { get; set; }
        public Dictionary<int, double> TDSAmountValidation = new Dictionary<int, double>();

        //On 09/02/2017, To make duplication entry or not(Ref_01)
        public bool DuplicateVoucher { get; set; }

        private int LedgerId
        {
            get
            {
                int ledgerId = 0;
                ledgerId = gvTransaction.GetFocusedRowCellValue(colLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colLedger).ToString()) : 0;

                //02/12/2019, To set GST properties for selected Ledger for Receipts and Payments----
                IsgstEnabledLedgers = false;
                ledgerMappedDefaultGSTID = 0;
                if (ledgerId > 0)
                {
                    DataRowView rowSelectedLedger = rglkpLedger.GetRowByKeyValue(ledgerId) as DataRowView;
                    if (rowSelectedLedger != null)
                    {
                        if (rowSelectedLedger.Row.Table.Columns.Contains("IS_GST_LEDGERS") && rowSelectedLedger.Row.Table.Columns.Contains("GST_ID"))
                        {
                            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes)) //28/12/2019, to check applicable from 
                            {
                                if (deDate.DateTime >= this.AppSetting.GSTStartDate)
                                {
                                    Int32 gstclassid = UtilityMember.NumberSet.ToInteger(rowSelectedLedger["GST_ID"].ToString());
                                    DateTime gstapplicablefrom = GetGSTApplicableFrom(gstclassid);
                                    IsgstEnabledLedgers = (UtilityMember.NumberSet.ToInteger(rowSelectedLedger["IS_GST_LEDGERS"].ToString()) == 1);
                                    if (deDate.DateTime >= gstapplicablefrom)
                                    {
                                        ledgerMappedDefaultGSTID = gstclassid;
                                    }
                                    else
                                    {
                                        ledgerMappedDefaultGSTID = this.AppSetting.GSTZeroClassId;
                                    }
                                }
                            }
                        }
                    }

                }
                //-----------------------------------------------------------------------------------
                return ledgerId;
            }
        }

        //02/12/2019, 
        bool IsgstEnabledLedgers;
        private bool IsGSTEnabledLedgers
        {
            get
            {
                if (LedgerId == 0)
                {
                    IsgstEnabledLedgers = false;
                }
                return IsgstEnabledLedgers;
            }
        }

        //29/11/2019, previous ledger id
        int previousLedgerId = 0;
        private int PreviousLedgerId
        {
            get
            {
                return previousLedgerId;
            }
            set
            {
                previousLedgerId = value;
            }
        }

        //02/12/2019, 
        int ledgerMappedDefaultGSTID;
        private int LedgerMappedDefaultGSTID
        {
            get
            {
                if (LedgerId == 0)
                {
                    ledgerMappedDefaultGSTID = 0;
                }
                return ledgerMappedDefaultGSTID;
            }
        }

        //29/11/2019, To set Ledger GST Ledger Class
        private int LedgerGSTClassId
        {
            get
            {
                int ledgerGSTclassId = 0;
                ledgerGSTclassId = gvTransaction.GetFocusedRowCellValue(colGSTLedgerClass) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colGSTLedgerClass).ToString()) : 0;
                return ledgerGSTclassId;
            }
            set
            {
                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGSTLedgerClass, value);
            }
        }

        //28/12/2019, To set Ledger GST Ledger Class
        private DateTime LedgerGSTClassApplicable
        {
            get
            {
                DateTime ledgerGSTclassapplicable = this.AppSetting.GSTStartDate;
                DataRowView drv = rglkpLedgerGST.GetRowByKeyValue(LedgerGSTClassId) as DataRowView;
                if (drv != null)
                {
                    ledgerGSTclassapplicable = UtilityMember.DateSet.ToDate(drv["APPLICABLE_FROM"].ToString(), false);
                }
                return ledgerGSTclassapplicable;
            }
        }
        #endregion

        #region Voucher Lock Properties
        private DateTime dtLockDateFrom { get; set; }
        private DateTime dtLockDateTo { get; set; }

        #endregion

        #region Constructor
        public JournalAdd()
        {
            InitializeComponent();
        }

        public JournalAdd(int voucherId)
            : this()
        {

            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem(voucherId))
            {
                ProjectId = voucherSystem.ProjectId;
                VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;
                ProjectName = voucherSystem.ProjectName;
                VoucherDefinitionId = voucherSystem.VoucherDefinitionId;
                VoucherId = voucherId;
            }

        }

        public JournalAdd(string recVoucherDate, int ProjId, string PrjName, int voucherId, int voucherIndex, Int32 voucherdefinitionid = 0, bool duplicatevoucher = false)
            : this()
        {
            ProjectId = ProjId;
            VoucherDefinitionId = voucherdefinitionid;
            ProjectName = PrjName;
            VoucherId = voucherId;
            RecentVoucherDate = recVoucherDate;

            //To make duplication voucher entry(Ref_01)
            if (duplicatevoucher)
            {
                DuplicateVoucher = duplicatevoucher;
            }
        }

        public JournalAdd(string recVoucherDate, int ProjId, string PrjName, int voucherIndex, int LedgerId, double LedgerAmount, int TDSBookingFromPayment)
            : this()
        {
            ProjectId = ProjId;
            VoucherDefinitionId = (int)DefaultVoucherTypes.Journal;
            ProjectName = PrjName;
            VoucherId = voucherId;
            RecentVoucherDate = recVoucherDate;
            this.TDSPartyLedgerId = LedgerId;
            PartyLedgerAmount = LedgerAmount;
            TDSBookingFromPartyPayment = TDSBookingFromPayment;
        }
        #endregion

        #region Property
        public int voucherId = 0;
        public int VoucherId
        {
            get { return voucherId; }
            set { voucherId = value; }
        }

        private DataView dvCostCentreInfo = null;
        private DataView CostCentreInfo
        {
            set { dvCostCentreInfo = value; }
            get { return dvCostCentreInfo; }
        }

        private int projectId = 0;
        private int ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }

        private string recentVoucherDate = string.Empty;
        private string RecentVoucherDate
        {
            set
            {
                recentVoucherDate = value;
            }
            get
            {
                return recentVoucherDate;
            }
        }

        private string projectName = string.Empty;
        private string ProjectName
        {
            set
            {
                projectName = value;
            }
            get
            {
                return projectName;
            }
        }

        //private int ledgerId;
        //private int LedgerId
        //{
        //    set
        //    {
        //        ledgerId = value;
        //    }
        //    get
        //    {
        //        ledgerId = gvTransaction.GetFocusedRowCellValue(colLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colLedger).ToString()) : 0;
        //        return ledgerId;
        //    }
        //}

        private double ledgerDebitAmount;
        private double LedgerDebitAmount
        {
            set
            {
                ledgerDebitAmount = value;
            }
            get
            {
                ledgerDebitAmount = gvTransaction.GetFocusedRowCellValue(colDebit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colDebit).ToString()) : 0;
                return ledgerDebitAmount;
            }
        }

        private double ledgerCreditAmount;
        private double LedgerCreditAmount
        {
            set
            {
                ledgerCreditAmount = value;
            }
            get
            {
                ledgerCreditAmount = gvTransaction.GetFocusedRowCellValue(colCredit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colCredit).ToString()) : 0;
                return ledgerCreditAmount;
            }
        }

        private int identification = 0;
        private int IdentificationValue
        {
            set
            {
                identification = value;
            }
            get
            {
                identification = gvTransaction.GetFocusedRowCellValue(colIdentification) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colIdentification).ToString()) : 0;
                return identification;
            }
        }

        private double summarycredit;
        private double SummaryCredit
        {
            get
            {
                summarycredit = this.UtilityMember.NumberSet.ToDouble(colCredit.SummaryItem.SummaryValue.ToString());
                return summarycredit;
            }
            set { summarycredit = value; }
        }

        private double summarydebit;
        private double SummaryDebit
        {
            get
            {
                summarydebit = this.UtilityMember.NumberSet.ToDouble(colDebit.SummaryItem.SummaryValue.ToString());
                return summarydebit;
            }
            set
            {
                summarydebit = value;
            }
        }

        private double summarygst;
        private double SummaryGST
        {
            get
            {
                if (AppSetting.EnableGST == "1")
                    summarygst = this.UtilityMember.NumberSet.ToDouble(colGStAmt.SummaryItem.SummaryValue.ToString());
                else
                    summarygst = 0;
                return summarygst;  // this.UtilityMember.NumberSet.ToDouble(colCredit.SummaryItem.SummaryValue.ToString()) +
            }

            set { summarygst = value; }
        }

        private double debitgst;
        private double DebitGST
        {
            get { return debitgst; }
            set { debitgst = value; }
        }

        private double creditgst;
        private double CreditGST
        {
            get { return creditgst; }
            set { creditgst = value; }
        }

        private double creditAmount = 0;
        private double CreditAmount
        {
            get { return creditAmount; }
            set { creditAmount = value; }
        }

        private double debitAmount = 0;
        private double DebitAmount
        {
            get { return debitAmount; }
            set { debitAmount = value; }
        }

        private int transVoucherMethod = 0;
        private int TransVoucherMethod
        {
            set
            {
                transVoucherMethod = value;
            }
            get
            {
                return transVoucherMethod;
            }
        }

        private string voucherMethod;
        private string VoucherMethod
        {
            get
            {
                return voucherMethod;
            }
            set
            {
                voucherMethod = value;
            }
        }

        private Int32 voucherdefinitionid = 0;
        private Int32 VoucherDefinitionId
        {
            set
            {
                voucherdefinitionid = value;

                /*if (voucherdefinitionid == (int)DefaultVoucherTypes.Journal)
                {
                    lcLblVoucerType.Text = DefaultVoucherTypes.Journal.ToString();
                    lcLblVoucerType.Visibility = LayoutVisibility.Never;
                }
                else
                {
                    using (VoucherSystem vouchersystem = new VoucherSystem())
                    {
                        vouchersystem.VoucherId = voucherdefinitionid;
                        ResultArgs result = vouchersystem.VoucherDetailsById();
                        if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dtVoucherTypeDetail = result.DataSource.Table;
                            lcLblVoucerType.Text = "Voucher Type : " + dtVoucherTypeDetail.Rows[0][vouchersystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName].ToString();
                            lcLblVoucerType.Visibility = LayoutVisibility.Always;
                            lcLblVoucerType.AppearanceItemCaption.Font = new Font(lcLblVoucerType.AppearanceItemCaption.Font.FontFamily, 10, FontStyle.Bold);
                        }
                    }
                }*/

                ResultArgs result = IsMultiVoucherTypes(ProjectId, ((int)DefaultVoucherTypes.Journal).ToString());
                if (result.Success)
                {
                    lcItemVoucerType.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    lcItemVoucerType.Visibility = LayoutVisibility.Never;
                }
            }
            get
            {
                return voucherdefinitionid;
            }
        }

        /// <summary>
        /// On 25/07/2023, based on the setting, alert and get the confirmation
        /// </summary>
        private int ConfirmVoucherAuthorization
        {
            get
            {
                int rnt = AuthorizedStatus;
                if (VoucherId == 0 && AppSetting.ConfirmAuthorizationVoucherEntry == 1)
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

        //01/03/2018, keep date value (To show alert message if selected date is locked for the selected project)
        private DateTime dePreviousVoucherDate { get; set; }

        private int IsTDSLedger { get; set; }
        private int ExpenseIsTDSLedger { get; set; }
        private int PartyIsTDSLedger { get; set; }
        private double ExpenseLedgerAmount { get; set; }
        private DataTable dtTDSBooking { get; set; }
        private DataTable dtTDSTransSummary { get; set; }
        private int PartyLedgerId { get; set; }
        public double PartyLedgerAmount { get; set; }
        private int DeducteeTypeId { get; set; }
        private decimal TDSNetPayableAmount { get; set; }
        private decimal TDSDeductedAmount { get; set; }
        private DataTable dtTransInfo { get; set; }
        private DataTable dtTDSMasterDetails { get; set; }
        private DataTable dtRemoveTDSTransSummary { get; set; }
        private double TDSLedgerCRAmount { get; set; }
        private double TDSLedgerDRAmount { get; set; }
        private bool isTDSEnableFlag = false;

        #endregion

        #region Events
        private void frmTransactionMultiAdd_Load(object sender, EventArgs e)
        {
            Setdefaults();
            SetLoadDefault();
            SetDisableShorcuts();
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyUserRights();
            }
            ucJournalShortcut.DisableDonor = DevExpress.XtraBars.BarItemVisibility.Never;
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else
            {
                if (VoucherId == 0)
                {
                    txtVoucher.Text = string.Empty;
                }
            }

            LoadGSTLedgerClass();

            FocusTransactionGrid();
            // ucJournalShortcut.DisableLedgerAdd = BarItemVisibility.Never;

            //On 31/05/2024, To enabled Delete and Print Voucher
            if (VoucherId != 0)
            {
                ucAdditionalInfo.DisableDeleteVocuher = BarItemVisibility.Always;
                ucAdditionalInfo.DisablePrintVoucher = BarItemVisibility.Always;
            }
            else
            {
                ucAdditionalInfo.DisableDeleteVocuher = BarItemVisibility.Never;
                ucAdditionalInfo.DisablePrintVoucher = BarItemVisibility.Never;
            }

            //On 04/09/2024, To enable currency details 
            lcCurrency.Visibility = lcCurrencyAmount.Visibility = lblDonorCurrency.Visibility = (this.AppSetting.AllowMultiCurrency == 1 ? LayoutVisibility.Always : LayoutVisibility.Never);
            lcExchangeRate.Visibility = lblCalculatedAmtCaption.Visibility = lblCalculatedAmt.Visibility = (this.AppSetting.AllowMultiCurrency == 1 ? LayoutVisibility.Always : LayoutVisibility.Never);
            lcActualAmount.Visibility = lcCurrencyEmptySpace.Visibility = (this.AppSetting.AllowMultiCurrency == 1 ? LayoutVisibility.Always : LayoutVisibility.Never); ;
            if (IsGeneralInvolice)
            {
                btnVendor.Text = "Attach Invoice";
                btnRemoveVendorGSTInvoce.Text = "Remove Invoice";
            }
            colLedgerBal.Visible = false;
        }

        private void ShowReferenceNumber()
        {
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableRefWiseReceiptANDPayment).Equals((int)YesNo.Yes))
            {
                colReferenceNumber.Visible = true;
            }
            else
            {
                colReferenceNumber.Visible = false;
            }

            if (this.AppSetting.EnableCashBankJournal == 1)
            {
                colChequeNo.Visible = true;
                colMaterilizedOn.Visible = true;
            }
            else
            {
                colChequeNo.Visible = false;
                colMaterilizedOn.Visible = false;
            }
        }

        private void ShowTransGST()
        {
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                colGSTLedgerClass.Visible = true;
                colGSTLedgerClass.VisibleIndex = 4;
                colGStAmt.Visible = true;
                colGStAmt.VisibleIndex = 5;
                ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, false);
            }
            else
            {
                colGSTLedgerClass.Visible = false;
                colGStAmt.Visible = false;
            }
        }

        /// <summary>
        /// GST Option for 10.01.2019
        /// Assign GST values from calculation and assign concern columns
        /// </summary>
        private void AssignGSTAmount(int selectedGSTClass)
        {
            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            {
                if (deDate.DateTime >= this.AppSetting.GSTStartDate)
                {
                    //int IsGSTExistLedger = IsGSTLedgers(LedgerId); //02/12/2019
                    LedgerGSTClassId = selectedGSTClass; //FetchGSTLedger(LedgerId); //29/11/2019, To set Ledger GST Ledger Class
                    if (IsValidaTransactionRow())
                    {
                        //02/12/2019
                        //28/12/2019, to check ledger GST applicable date
                        if (LedgerId > 0 && IsgstEnabledLedgers && deDate.DateTime >= LedgerGSTClassApplicable)
                        {
                            string GSTBalance = CalculateGST();

                            if (GSTBalance != string.Empty)
                            {
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGStAmt, GSTBalance);

                                string SpiltValues = GSTBalance;
                                decimal GSt = 0;
                                decimal SGSt = 0;
                                decimal CGSt = 0;
                                decimal IGSt = 0;
                                if (!string.IsNullOrEmpty(SpiltValues))
                                {
                                    string[] values = SpiltValues.Split('(');  // get values[0]
                                    GSt = this.UtilityMember.NumberSet.ToDecimal(values[0].ToString());

                                    string[] gstPercentages = values[1].Split(')'); // get values1[1]
                                    string[] gstPercentagesValues = gstPercentages[0].Split('+');
                                    SGSt = this.UtilityMember.NumberSet.ToDecimal(gstPercentagesValues[0].ToString());
                                    string[] gstPercentagesCGST = gstPercentagesValues[1].Split('/');
                                    CGSt = this.UtilityMember.NumberSet.ToDecimal(gstPercentagesValues[0].ToString());
                                }
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGST, GSt);
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCGST, CGSt);
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSGST, SGSt);
                                if (SGSt == 0 && CGSt == 0)
                                    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colIGST, GSt);

                                // Chinna 09/08/2023
                                UpdateGST = this.UtilityMember.NumberSet.ToDouble(GSt.ToString());

                                // Commanded by Chinna 08/08/2023
                                //// Cash Transaction
                                //gvBank.PostEditor();
                                //gvBank.UpdateCurrentRow();
                                //DataTable dtTemp = gcBank.DataSource as DataTable;
                                //if (CashLedgerId > 0)
                                //{
                                //    DataTable dtValue = gcTransaction.DataSource as DataTable;
                                //    string Balance = GSTTotalAmount(dtValue);
                                //    if (Balance != string.Empty) { gvBank.SetRowCellValue(gvBank.FocusedRowHandle, colCBGSTAmount, Balance); }
                                //}
                            }
                        }
                        else
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGStAmt, "  ");
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGST, 0);
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCGST, 0);
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colSGST, 0);
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colIGST, 0);

                            //# 28/12/2019, Reset Ledger GST Class Id
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGSTLedgerClass, this.AppSetting.GSTZeroClassId);


                            //25/04/2019, to reset gst amount for non gst ledgers
                            DataTable dtValue = gcTransaction.DataSource as DataTable;
                            string Balance = GSTTotalAmount(dtValue);
                        }
                    }
                    else
                    {
                        // Chinna - 11/08/2023 // Clear the Current GSt Values if make it Zero
                        ClearCurrentGSTValues();
                        gcTransaction.RefreshDataSource();
                    }
                }
                else
                {
                    DataTable dtVouchers = gcTransaction.DataSource as DataTable;
                    ClearGSTValues(dtVouchers);
                }
            }
        }

        /// <summary>
        /// Calculate GST values for given GST class Id
        /// </summary>
        /// <returns></returns>
        private string CalculateGST()
        {
            string value = "";
            // Replace the Both Amounts
            double LedgerAmount = LedgerCreditAmount > 0 ? LedgerCreditAmount : LedgerDebitAmount;
            //int GSTId = FetchGSTLedger(LedgerId);
            if (LedgerGSTClassId > 0)
            {
                int IsIGSTExistLedger = IsIGSTLedgers(LedgerId);
                if (IsIGSTExistLedger == 0)
                {
                    resultArgs = FetchGSTPercentages(LedgerGSTClassId); //FetchGSTPercentages(GSTId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        double gst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["GST"].ToString());
                        double cgst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["CGST"].ToString());
                        double sgst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["SGST"].ToString());

                        //On 10/05/2021, to have real GST values without rounding value---------------------------------
                        /*double gstCalcAmt = LedgerAmount * gst / 100;
                        double cgstCalcAmt = LedgerAmount * cgst / 100;
                        double sgstCalcAmt = LedgerAmount * sgst / 100;*/
                        //---------------------------------------------------------------------------------------------
                        // double gstCalcAmt = LedgerAmount * gst / 100;
                        double gstCalcAmt = LedgerAmount * gst / 100;
                        //   double cgstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * cgst / 100), 2);
                        double cgstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * cgst / 100), 2);

                        //  double sgstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * sgst / 100), 2);
                        double sgstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * sgst / 100), 2);
                        gstCalcAmt = cgstCalcAmt + sgstCalcAmt;

                        //value = UtilityMember.NumberSet.ToNumber(gstCalcAmt) + "  (" + UtilityMember.NumberSet.ToNumber(cgstCalcAmt) + " + " + UtilityMember.NumberSet.ToNumber(sgstCalcAmt) + ") ";
                        value = UtilityMember.NumberSet.ToNumber(gstCalcAmt) + "  (" + UtilityMember.NumberSet.ToNumber(cgstCalcAmt) + " + " + UtilityMember.NumberSet.ToNumber(sgstCalcAmt) + " / " + UtilityMember.NumberSet.ToNumber(0.00) + ") ";
                    }
                }
                else
                {
                    resultArgs = FetchGSTPercentages(LedgerGSTClassId); //FetchGSTPercentages(GSTId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        double gst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["GST"].ToString());
                        double igst = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["GST"].ToString());

                        //On 10/05/2021, to have real GST values without rounding value---------------------------------
                        /*double gstCalcAmt = LedgerAmount * gst / 100;
                        double igstCalcAmt = LedgerAmount * igst / 100;*/
                        //----------------------------------------------------------------------------------------------
                        // double gstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * gst / 100), 2);
                        double gstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * gst / 100), 2);
                        // double igstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * igst / 100), 2);
                        double igstCalcAmt = UtilityMember.NumberSet.TruncateDoubleByPrecision((LedgerAmount * igst / 100), 2);

                        value = UtilityMember.NumberSet.ToNumber(igstCalcAmt) + "  (" + UtilityMember.NumberSet.ToNumber(0.00) + " + " + UtilityMember.NumberSet.ToNumber(0.00) + " / " + UtilityMember.NumberSet.ToNumber(igstCalcAmt) + ") ";
                    }
                }
            }
            return value;
        }

        private int IsIGSTLedgers(int LedgerId)
        {
            int isGST = 0;
            using (VoucherTransactionSystem VoucherTrans = new VoucherTransactionSystem())
            {
                VoucherTrans.LedgerId = LedgerId;
                VoucherTrans.State = this.AppSetting.State;
                resultArgs = VoucherTrans.FetchIsIGSTApplicable();
                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    isGST = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][VoucherTrans.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName].ToString());
                }
            }
            return isGST;
        }

        private ResultArgs FetchGSTPercentages(int GStId)
        {
            using (GSTClassSystem gstclass = new GSTClassSystem())
            {
                resultArgs = gstclass.FetchGSt(GStId);
            }
            return resultArgs;
        }

        private string GSTTotalAmount(DataTable dtVoucherTrans)
        {
            string GStValues = string.Empty;
            if (deDate.DateTime >= this.AppSetting.GSTStartDate)
            {
                if (deDate.DateTime >= this.AppSetting.GSTStartDate)
                {
                    UpdateGST = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(GST)", "").ToString());
                    GStValues = this.UtilityMember.NumberSet.ToNumber(UpdateGST);
                }
            }
            return GStValues;
        }

        private void ClearGSTValues(DataTable dtVouchers)
        {
            if (VoucherId > 0)
            {
                foreach (DataRow dr in dtVouchers.Rows)
                {
                    dr["GST_AMOUNT"] = string.Empty;
                    dr["GST"] = 0;
                    dr["CGST"] = 0;
                    dr["SGST"] = 0;
                    dr["IGST"] = 0;
                    dtVouchers.AcceptChanges();
                }
            }

            ////26/04/2019, clear vendor gst invoice
            //GSTVendorInvoiceNo = string.Empty;
            //GSTVendorInvoiceDate = string.Empty;
            //GSTVendorInvoiceType = 0;
            //GSTVendorId = 0;
            //DtGSTInvoiceMasterDetails = null;
            //DtGSTInvoiceMasterLedgerDetails = null;
        }

        private void ClearCurrentGSTValues()
        {
            DataRowView drView = gvTransaction.GetRow(gvTransaction.FocusedRowHandle) as DataRowView;

            if (drView != null)
            {
                drView["LEDGER_GST_CLASS_ID"] = 0;
                drView["GST_AMOUNT"] = 0;
                drView["GST_AMOUNT"] = string.Empty;
                drView["GST"] = 0;
                drView["CGST"] = 0;
                drView["SGST"] = 0;
                drView["IGST"] = 0;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void JournalAdd_Shown(object sender, EventArgs e)
        {
            //FocusTransactionGrid();
            deDate.Focus();
        }

        private void gvTransaction_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colLedger) != null)
            {
                int GroupId = FetchLedgerDetails(LedgerId);
                if (e.Column == colDebit && LedgerCreditAmount > 0)
                {
                    e.Appearance.BackColor = Color.LightGray;
                }
                //   else if ((e.Column == colCredit || e.Column == colGSTLedgerClass) && (LedgerDebitAmount > 0 && (!GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors)))) // Chinna 08/08/2023
                // else if ((e.Column == colCredit || e.Column == colGSTLedgerClass) && (LedgerDebitAmount > 0 && !GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors))) // 21/08/2023
                else if (e.Column == colCredit && LedgerDebitAmount > 0) // 21/08/2023
                {
                    e.Appearance.BackColor = Color.LightGray;
                }
                //else if (e.Column == colGSTLedgerClass && LedgerDebitAmount > 0 && (!GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)))
                //{
                //    e.Appearance.BackColor = Color.LightGray;
                //}
                else if (e.Column == colReferenceNumber && (!GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)))
                {
                    e.Column.AppearanceCell.BackColor = Color.LightGray;
                }
                else if (e.Column == colReferenceNumber && (!GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors)))
                {
                    e.Column.AppearanceCell.BackColor = Color.LightGray;
                }
                else if ((e.Column == colChequeNo || e.Column == colMaterilizedOn) && !GroupId.Equals((int)FixedLedgerGroup.BankAccounts))
                {
                    e.Appearance.BackColor = Color.LightGray;
                }
            }
        }

        private void gvTransaction_ShowingEditor(object sender, CancelEventArgs e)
        {
            int GroupId = 0;
            if (gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colLedger) != null)
            {
                GroupId = FetchLedgerDetails(LedgerId);
                if (gvTransaction.FocusedColumn == colDebit && LedgerCreditAmount > 0)
                {
                    e.Cancel = true;
                }
                //else if ((gvTransaction.FocusedColumn == colCredit || gvTransaction.FocusedColumn == colGSTLedgerClass) && LedgerDebitAmount > 0) // Chinna 08/08/2023
                // else if ((gvTransaction.FocusedColumn == colCredit || gvTransaction.FocusedColumn == colGSTLedgerClass) && (LedgerDebitAmount > 0 && (!GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)))) // 21/08/2023 Chinna
                else if ((gvTransaction.FocusedColumn == colCredit && LedgerDebitAmount > 0))
                {
                    e.Cancel = true;
                }
                //else if (gvTransaction.FocusedColumn == colGSTLedgerClass && LedgerDebitAmount > 0 && (!GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)))
                //{
                //    e.Cancel = true;
                //}
                else if (gvTransaction.FocusedColumn == colLedger && LedgerId.Equals(0))
                {
                    e.Cancel = false;
                }
                else if (gvTransaction.FocusedColumn == colReferenceNumber && (GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)))
                {
                    e.Cancel = false;
                }
                else if (gvTransaction.FocusedColumn == colReferenceNumber && (GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors)))
                {
                    e.Cancel = false;
                }
                else if (gvTransaction.FocusedColumn == colReferenceNumber && (!GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)))
                {
                    e.Cancel = true;
                }
                else if (gvTransaction.FocusedColumn == colReferenceNumber && (!GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors)))
                {
                    e.Cancel = true;
                }
                else if ((gvTransaction.FocusedColumn == colChequeNo || gvTransaction.FocusedColumn == colMaterilizedOn) && (!GroupId.Equals((int)FixedLedgerGroup.BankAccounts)))
                {
                    e.Cancel = true;
                }



                if (gvTransaction.FocusedColumn == colCostcentre) // Added By praveen to restrict the costcentre form to be shown
                {
                    if (LedgerId > 0)
                    {
                        if (!CheckCostcentreEnabled(LedgerId))
                            e.Cancel = true;
                    }
                }

                //var grid = sender as GridView;
                //if (grid.FocusedColumn.FieldName == "Value")
                //{
                //    var row = grid.GetRow(grid.FocusedRowHandle) as // your model;
                //        // note that previous line should be different in case of for example a DataTable datasource
                //    grid.ActiveEditor.Properties.ReadOnly = true; // your condition based on the current row object
                //}
            }

            //if (LedgerDebitAmount > 0 && (!GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)))
            //{
            //    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGSTLedgerClass, 0);
            //    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGStAmt, string.Empty);
            //}
        }

        private void gcTransaction_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && (gvTransaction.FocusedColumn == colCredit || gvTransaction.FocusedColumn == colDebit)) // || gvTransaction.FocusedColumn == colGSTLedgerClass))
            {
                double dbtAmt = gvTransaction.GetFocusedRowCellValue(colDebit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colDebit).ToString()) : 0;
                double crtAmt = gvTransaction.GetFocusedRowCellValue(colCredit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colCredit).ToString()) : 0;

                if (gvTransaction.FocusedColumn == colCredit && crtAmt > 0 && LedgerId > 0)
                {
                    ShowCostCentre(crtAmt, LedgerId);
                    PartyIsTDSLedger = 0;
                    int FocusedRowIdentification = gvTransaction.GetFocusedRowCellValue(colIdentification) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colIdentification).ToString()) : 0;
                    if (FocusedRowIdentification == 0)
                    {
                        gvTransaction.SetFocusedRowCellValue(colIdentification, (int)YesNo.No);
                    }
                }
                else if (gvTransaction.FocusedColumn == colDebit && dbtAmt > 0 && LedgerId > 0)
                {
                    ShowCostCentre(dbtAmt);
                    gvTransaction.SetFocusedRowCellValue(colIdentification, (int)YesNo.No);
                    if (CheckIsTDSLedger() > 0)
                    {
                        if (ExpAmount.ContainsKey(gvTransaction.FocusedRowHandle))
                        {
                            ExpAmount.Remove(gvTransaction.FocusedRowHandle);
                        }
                        if (!ExpAmount.ContainsKey(gvTransaction.FocusedRowHandle))
                        {
                            ExpAmount.Add(gvTransaction.FocusedRowHandle, dbtAmt);
                        }
                        ExpenseLedgerAmount = ExpAmount.Sum(x => x.Value);

                        IsTaxDeductable(TransactionMode.DR, dbtAmt);
                    }
                }
                else if (gvTransaction.FocusedColumn == colLedger && LedgerId > 0)
                {

                }
                if (!gvTransaction.IsFirstRow)
                {
                    if (LedgerId == 0 && (SummaryCredit + CreditGST) == (SummaryDebit + DebitGST))
                    {
                        gvTransaction.CloseEditor();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        txtJNarration.Focus();
                        txtJNarration.Select();
                    }
                }

                //foreach (DataRow dr in dtSource.Rows)
                //{
                //    dtSource.Rows.IndexOf(dr);
                //    double DebitAmount = this.UtilityMember.NumberSet.ToDouble(dr["DEBIT"].ToString());
                //    int LedId = this.UtilityMember.NumberSet.ToInteger(dr["LedgerId"].ToString());
                //    if (LedId > 0 && DebitAmount > 0)
                //    {
                //        double sum = DebitAmount + (SummaryCredit + UpdateGST);

                //        if (sum != 0)
                //        {
                //            dr["DEBIT"] = sum;
                //        }

                //        //decimal SumDebit = debit + (SummaryCredit + UpdateGST);
                //        //if (SumDebit != string.Empty)
                //        //{
                //        //    dr["DEBIT"] = SumDebit;
                //        //}
                //    }
                //}
            }
            else
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && gvTransaction.FocusedColumn == colLedger || gvTransaction.FocusedColumn == colGSTLedgerClass)
                {
                    int LedgerID = gvTransaction.GetFocusedRowCellValue(colLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colLedger).ToString()) : 0;
                    if (LedgerID > 0)
                    {
                        if (CheckIsTDSLedger() > 0 && FetchGroupIdByLedgerId().Equals((int)TDSDefaultLedgers.SunderyCreditors))
                        {
                            IsTaxDeductable(TransactionMode.CR, ExpenseLedgerAmount, true);
                        }
                    }
                }
            }


        }

        /// <summary>
        /// This is to Replace the Credit amount to debit amount if equal while using GST
        /// Identify the First Entries are debit while Making Journal Transactions
        /// When you propose to update the Credit Entires check Two Previous rows Count Values always
        /// Ex : (Cr is current Row, Above Dr and Above Cr) OR Above is Empty if it is only one Row
        /// 
        /// </summary>
        private void UpdateGSTReplaced()
        {
            DataTable dtSource = gcTransaction.DataSource as DataTable;
            decimal FirstDebitGSTAmt = 0;
            decimal FirstCreditGSTAmt = 0;

            DataRow dtFirstdatarow = gvTransaction.GetDataRow(0);
            int FirstIndexId = 0;
            int GroupId = 0;
            if (dtFirstdatarow != null)
            {
                int LedId = this.UtilityMember.NumberSet.ToInteger(dtFirstdatarow["LEDGER_ID"].ToString());
                GroupId = FetchLedgerDetails(LedId);

                decimal DebitAmt = this.UtilityMember.NumberSet.ToDecimal(dtFirstdatarow["DEBIT"].ToString());
                if (DebitAmt > 0)
                {
                    FirstDebitGSTAmt = this.UtilityMember.NumberSet.ToDecimal(dtFirstdatarow["GST"].ToString());
                }

                decimal CreditAmt = this.UtilityMember.NumberSet.ToDecimal(dtFirstdatarow["CREDIT"].ToString());
                if (CreditAmt > 0)
                {
                    FirstCreditGSTAmt = this.UtilityMember.NumberSet.ToDecimal(dtFirstdatarow["GST"].ToString());
                }

                FirstIndexId = DebitAmt > 0 ? 1 : 0;

                int CurrentRowIndex = gvTransaction.FocusedRowHandle;

                // if (CurrentRowIndex > 0 && !GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)) // 21/08/2023 // Introduce Group Id also
                if (CurrentRowIndex > 0 && DebitAmt > 0) // FirstDebitGSTAmt == 0
                {
                    DataRowView drCurentRowValues = gvTransaction.GetRow(CurrentRowIndex) as DataRowView;
                    DataRowView drPreviousRow1Values = gvTransaction.GetRow(CurrentRowIndex - 1) == null ? null : gvTransaction.GetRow(CurrentRowIndex - 1) as DataRowView;
                    DataRowView drPreviousRow2Values = gvTransaction.GetRow(CurrentRowIndex - 2) == null ? null : gvTransaction.GetRow(CurrentRowIndex - 2) as DataRowView;

                    double CurrentCreditAmount = 0;
                    double PreviousR1DebitAmount = 0;
                    double PreviousR2CreditAmount = 0;

                    double PreviousR1GSTAmt = 0;

                    if (drCurentRowValues != null && drPreviousRow1Values != null)
                    {
                        CurrentCreditAmount = this.UtilityMember.NumberSet.ToDouble(drCurentRowValues["Credit"].ToString());
                        PreviousR1DebitAmount = this.UtilityMember.NumberSet.ToDouble(drPreviousRow1Values["Debit"].ToString());
                        PreviousR1GSTAmt = this.UtilityMember.NumberSet.ToDouble(drPreviousRow1Values["GST"].ToString());

                        if (drPreviousRow2Values != null)
                            PreviousR2CreditAmount = drPreviousRow2Values["Credit"] != null ? this.UtilityMember.NumberSet.ToDouble(drPreviousRow2Values["Credit"].ToString()) : 0;

                        if (PreviousR1DebitAmount > 0 && (PreviousR2CreditAmount > 0 || PreviousR2CreditAmount == 0))
                        {
                            if (CurrentCreditAmount > 0 && PreviousR1GSTAmt == 0)
                                drPreviousRow1Values["Debit"] = (CurrentCreditAmount + UpdateGST);
                        }
                    }
                }
                else if (CurrentRowIndex > 0 && CreditAmt > 0)
                {
                    DataRowView drCurentRowValues = gvTransaction.GetRow(CurrentRowIndex) as DataRowView;
                    DataRowView drPreviousRow1Values = gvTransaction.GetRow(CurrentRowIndex - 1) == null ? null : gvTransaction.GetRow(CurrentRowIndex - 1) as DataRowView;
                    DataRowView drPreviousRow2Values = gvTransaction.GetRow(CurrentRowIndex - 2) == null ? null : gvTransaction.GetRow(CurrentRowIndex - 2) as DataRowView;

                    //  double CurrentCreditAmount = 0;
                    // double PreviousR1DebitAmount = 0;
                    //  double PreviousR2CreditAmount = 0;

                    double CurrentDebitAmount = 0;
                    double PreviousR1CreditAmount = 0;
                    double PreviousR2DebitAmount = 0;

                    double PreviousR1GSTAmt = 0;

                    if (drCurentRowValues != null && drPreviousRow1Values != null)
                    {
                        CurrentDebitAmount = this.UtilityMember.NumberSet.ToDouble(drCurentRowValues["Debit"].ToString());
                        PreviousR1CreditAmount = this.UtilityMember.NumberSet.ToDouble(drPreviousRow1Values["Credit"].ToString());
                        PreviousR1GSTAmt = this.UtilityMember.NumberSet.ToDouble(drPreviousRow1Values["GST"].ToString());

                        if (drPreviousRow2Values != null)
                            PreviousR2DebitAmount = drPreviousRow2Values["Debit"] != null ? this.UtilityMember.NumberSet.ToDouble(drPreviousRow2Values["Debit"].ToString()) : 0;

                        if (PreviousR1CreditAmount > 0 && (PreviousR2DebitAmount > 0 || PreviousR2DebitAmount == 0))
                        {
                            if (CurrentDebitAmount > 0 && PreviousR1GSTAmt == 0)
                                drPreviousRow1Values["Credit"] = (CurrentDebitAmount + UpdateGST);
                        }
                    }
                }
            }

            //// gvTransaction.RowCount 
            //if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
            //{
            //    for (int i = 0; i <= gvTransaction.RowCount; i++)
            //    {
            //        //  DataRow dr = gvTransaction.GetRow(i) as DataRow;
            //        DataRowView drv = gvTransaction.GetRow(i) as DataRowView;
            //        if (drv != null)
            //        {
            //            if (drv.Row.Table.Columns.Contains("Debit"))
            //            {
            //                double DebitAmount = UtilityMember.NumberSet.ToDouble(drv["Debit"].ToString());
            //                if (DebitAmount > 0)
            //                { 
            //                    drv["Debit"] = (s + UpdateGST);
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
            ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, true);
        }

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvTransaction.PostEditor();
            gvTransaction.UpdateCurrentRow();
            if (gvTransaction.ActiveEditor == null)
            {
                gvTransaction.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);

            if (LedgerId > 0)
            {
                DataTable dtTrans = gcTransaction.DataSource as DataTable;
                string Balance = GetLedgerBalanceValues(dtTrans, LedgerId); //ShowLedgerBalance(LedgerId, dtTrans, true);
                if (Balance != string.Empty) { gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, Balance); }

                if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
                {
                    int GroupId = FetchLedgerDetails(LedgerId);
                    if (!GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors))
                    {

                    }
                    //if (LedgerDebitAmount > 0 && (!GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)))
                    //{
                    //    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGSTLedgerClass, 0);
                    //    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colGStAmt, string.Empty);
                    //}
                    //   else
                    //  {
                    //if (LedgerCreditAmount > 0 || (GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors) || (GroupId.Equals((int)TDSDefaultLedgers.CurrentAssets))))
                    //{

                    int CurrentRowIndex = gvTransaction.FocusedRowHandle;
                    DataRowView CurrentDataRow = gvTransaction.GetRow(CurrentRowIndex) == null ? null : gvTransaction.GetRow(CurrentRowIndex) as DataRowView;
                    DataRowView PreviousDataRow = gvTransaction.GetRow(CurrentRowIndex - 1) == null ? null : gvTransaction.GetRow(CurrentRowIndex - 1) as DataRowView;
                    double Prev1DebitAmount = 0;
                    double Prev1GstAmount = 0;
                    double CurrentGSTAmount = 0;
                    if (PreviousDataRow != null)
                    {
                        Prev1DebitAmount = this.UtilityMember.NumberSet.ToDouble(PreviousDataRow["Debit"].ToString());
                        Prev1GstAmount = this.UtilityMember.NumberSet.ToDouble(PreviousDataRow["GST"].ToString());
                        CurrentGSTAmount = this.UtilityMember.NumberSet.ToDouble(CurrentDataRow["GST"].ToString());
                    }
                    if (Prev1GstAmount == 0)  // 04/09/2023
                    {
                        if ((PreviousLedgerId != LedgerId && PreviousLedgerId != 0) || LedgerGSTClassId == 0)
                        {

                            AssignGSTAmount(LedgerMappedDefaultGSTID);
                        }
                        else
                        {
                            AssignGSTAmount(LedgerGSTClassId);
                        }
                    }
                    //}
                    //  }

                    UpdateGSTReplaced();
                }
            }

            PreviousLedgerId = LedgerId;

            // AssignGSTAmount(LedgerGSTClassId);

        }

        private void gvTransaction_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (LedgerId == 0)
            {
                e.Valid = false;
                gvTransaction.FocusedColumn = colLedger;
                gvTransaction.ShowEditor();
            }
        }

        private void JournalAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.A)
            {
                MessageBox.Show(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.YES_KEYDOWN));
            }
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
                if (e.KeyCode == Keys.F3)
                {
                    //  deDate.Focus(); // amal
                    frmDatePicker DatePicker = new frmDatePicker(deDate.DateTime, DatePickerType.VoucherDate);
                    //Added by Carmel Raj on 20-October-2015
                    //Purpose : Set max date property of frmDatePicker Object variable
                    DatePicker.deProjectMaxDate = deMaxDate;
                    DatePicker.ShowDialog();
                    deDate.DateTime = AppSetting.VoucherDate;
                    gcTransaction.Focus();
                }
                if (e.KeyCode == Keys.F4)// (Keys.Control | Keys.N)
                {
                    deDate.DateTime = deDate.DateTime.AddDays(1);
                }
                if (e.KeyCode == Keys.F5)
                {
                    ShowProjectSelectionWindow();
                    if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                    {
                        LoadVoucherNo();
                    }
                    else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual)
                    {
                        txtVoucher.Text = string.Empty;
                    }
                }
                if (e.KeyData == (Keys.Alt | Keys.M))
                {
                    LoadMapping();
                }
                if (e.KeyCode == Keys.F10)
                {
                    ShowLedgerForm();
                }

                if (e.KeyData == (Keys.Control | Keys.D))
                {
                    DeleteTransaction();
                }
                if (e.KeyData == (Keys.Alt | Keys.N))
                {
                    VoucherId = 0;
                    ClearControls();
                }
                if (e.KeyData == (Keys.Control | Keys.I))
                {
                    FocusTransactionGrid();
                }
                if (e.KeyData == (Keys.Alt | Keys.V))
                {
                    frmTransactionVoucherView voucview = new frmTransactionVoucherView(projectName, ProjectId, deDate.DateTime, true);
                    voucview.ShowDialog();
                }
                if (e.KeyData == (Keys.Alt | Keys.T))
                {
                    ShowCostCentreForm();
                }
                if (e.KeyData == (Keys.Alt | Keys.P))
                {
                    ShowLedgerOptionsForm();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void gvTransaction_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ErrorText = this.GetMessage(MessageCatalog.Common.COMMON_INVALID_EXCEPTION);
            e.WindowCaption = this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE);
        }

        private void rglkpLedger_Leave(object sender, EventArgs e)
        {
            if (LedgerId > 0)
            {
                gvTransaction.PostEditor();
                gvTransaction.UpdateCurrentRow();
                DataTable dtTrans = gcTransaction.DataSource as DataTable;
                string Balance = GetLedgerBalanceValues(dtTrans, LedgerId);
                DebitGST = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(GST)", "DEBIT>0 AND GST>0").ToString());
                CreditGST = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(GST)", "CREDIT>0 AND GST>0").ToString());
                int GroupId = 0;
                if (Balance != string.Empty)
                {
                    GroupId = FetchLedgerDetails(LedgerId);
                    if (PartyLedgerAmount > 0 && (!GroupId.Equals((int)TDSDefaultLedgers.DutiesandTaxes)) && (!GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors)))
                    {
                        if (VoucherId == 0)
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colDebit, PartyLedgerAmount);
                        }
                    }

                    //On 05/09/2024, To show currency amount by default
                    if (this.AppSetting.AllowMultiCurrency == 1 && VoucherId == 0)
                    {
                        double dcurrencyamount = this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text);
                        if (dcurrencyamount > 0)
                        {
                            double debit = gvTransaction.GetFocusedRowCellValue(colDebit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colDebit).ToString()) : 0;
                            double credit = gvTransaction.GetFocusedRowCellValue(colCredit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colCredit).ToString()) : 0;
                            
                            if (credit>0)
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCredit, dcurrencyamount);
                            else
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colDebit, dcurrencyamount);
                        }
                    }

                    // This is to Clear the values while changing SC,SD To Ordinary Ledgers
                    if ((!GroupId.Equals((int)TDSDefaultLedgers.DutiesandTaxes)) && (!GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors)))
                    {
                        gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colReferenceNumber, string.Empty);
                    }

                    gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colLedgerBal, Balance);
                }

                // This is to automatically update debit amount to credit amount starts here
                if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
                {
                    int CurrentRowIndex = gvTransaction.FocusedRowHandle;
                    DataRowView CurrentDataRow = gvTransaction.GetRow(CurrentRowIndex) == null ? null : gvTransaction.GetRow(CurrentRowIndex) as DataRowView;
                    DataRowView PreviousDataRow = gvTransaction.GetRow(CurrentRowIndex - 1) == null ? null : gvTransaction.GetRow(CurrentRowIndex - 1) as DataRowView;
                    double Prev1DebitAmount = 0;
                    double Prev1GstAmount = 0;
                    double CurrentGSTAmount = 0;
                    if (PreviousDataRow != null)
                    {
                        Prev1DebitAmount = this.UtilityMember.NumberSet.ToDouble(PreviousDataRow["Debit"].ToString());
                        Prev1GstAmount = this.UtilityMember.NumberSet.ToDouble(PreviousDataRow["GST"].ToString());
                        CurrentGSTAmount = this.UtilityMember.NumberSet.ToDouble(CurrentDataRow["GST"].ToString());
                    }

                    double SumCredit = 0;
                    double SumDebit = 0;

                    SumDebit = SummaryDebit + DebitGST;
                    SumCredit = SummaryCredit + CreditGST;

                    //if (Prev1DebitAmount > 0 && Prev1GstAmount > 0)
                    //    SumDebit = SummaryDebit + Prev1GstAmount;

                    //if (Prev1DebitAmount > 0 && Prev1GstAmount == 0)
                    //    SumCredit = SummaryCredit + CurrentGSTAmount;


                    // (SummaryDebit + SummaryGST);
                    //else
                    //    SumCredit = (SummaryCredit + CurrentGSTAmount);  // (SummaryCredit + SummaryGST);   // UpdateGST // 21/08/2023 

                    //if (DebitGST > 0 && CreditGST > 0)
                    // {

                    if (LedgerId > 0 && (SumDebit - SumCredit > 0))
                    {
                        //if (LedgerDebitAmount == 0 && !isTDSEnableFlag)  
                        if (LedgerDebitAmount == 0 && !isTDSEnableFlag)
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCredit, SumDebit - SumCredit);
                        }
                    }
                    else if (LedgerId > 0 && (SumCredit - SumDebit) > 0)
                    {
                        if (LedgerCreditAmount == 0 && !isTDSEnableFlag)
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colDebit, SumCredit - SumDebit);
                        }
                    }
                    // }
                    //else
                    //{
                    //    if (LedgerId > 0 && (SummaryDebit - SummaryCredit > 0))
                    //    {
                    //        if (LedgerDebitAmount == 0 && !isTDSEnableFlag)
                    //        {
                    //            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCredit, SummaryDebit - SummaryCredit);
                    //        }
                    //    }
                    //    else if (LedgerId > 0 && (SummaryCredit - SummaryDebit) > 0)
                    //    {
                    //        if (LedgerCreditAmount == 0 && !isTDSEnableFlag)
                    //        {
                    //            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colDebit, SummaryCredit - SummaryDebit);
                    //        }
                    //    }
                    //}

                    // 04/09/2023

                    // if (Prev1GstAmount == 0) // 
                    //  {

                    //  if (LedgerCreditAmount > 0 && Prev1GstAmount == 0)

                    if (Prev1GstAmount == 0)
                    {
                        if ((PreviousLedgerId != LedgerId && PreviousLedgerId != 0) || LedgerGSTClassId == 0)
                        {
                            //As on 30/10/2023, To retain zero based GST Ledger when voucer is edited
                            //AssignGSTAmount(LedgerMappedDefaultGSTID);
                            Int32 tmplegerid = gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colOldLedgerId) == null ? 0 : UtilityMember.NumberSet.ToInteger(gvTransaction.GetRowCellValue(gvTransaction.FocusedRowHandle, colOldLedgerId).ToString());
                            if (VoucherId > 0 && LedgerGSTClassId == 0 && tmplegerid == LedgerId)
                            {
                                AssignGSTAmount(this.AppSetting.GSTZeroClassId);
                            }
                            else
                            {
                                AssignGSTAmount(LedgerMappedDefaultGSTID);
                            }
                        }
                        else
                        {
                            AssignGSTAmount(LedgerGSTClassId);
                        }
                    }

                    //if (Prev1GstAmount == 0)
                    //AssignGSTAmount(LedgerGSTClassId);

                    if (DebitGST > 0 && CreditGST > 0) // Introduce to not replacing the values if gst is 0 both side
                    {
                        UpdateGSTReplaced();
                    }
                }
                else
                {
                    if (LedgerId > 0 && (SummaryDebit - SummaryCredit > 0))
                    {
                        if (LedgerDebitAmount == 0 && !isTDSEnableFlag)
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colCredit, SummaryDebit - SummaryCredit);
                        }
                    }
                    else if (LedgerId > 0 && (SummaryCredit - SummaryDebit) > 0)
                    {
                        if (LedgerCreditAmount == 0 && !isTDSEnableFlag)
                        {
                            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colDebit, SummaryCredit - SummaryDebit);
                        }
                    }
                }

                //// Ends Here
                //if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
                //{
                //    if (LedgerCreditAmount > 0)
                //    {
                //        if ((PreviousLedgerId != LedgerId && PreviousLedgerId != 0) || LedgerGSTClassId == 0)
                //        {
                //            AssignGSTAmount(LedgerMappedDefaultGSTID);
                //        }
                //        else
                //        {
                //            AssignGSTAmount(LedgerGSTClassId);
                //        }
                //    }

                //    //  UpdateGSTReplaced();
                //}
            }
            else
            {
                //gvTransaction.UpdateCurrentRow();
            }

            gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colIsGSTLedger, IsgstEnabledLedgers ? 1 : 0);
            PreviousLedgerId = LedgerId;
            //  AssignGSTAmount(LedgerGSTClassId);
        }

        private int FetchLedgerDetails(int LedgerID)
        {
            int GroupId = 0;
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.LedgerId = LedgerID;
                    GroupId = ledgerSystem.FetchLedgerGroupById();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return GroupId;
        }

        private string GetLedgerBalanceValues(DataTable dtTrans, int LedgerId)
        {
            string LedgerBalance = string.Empty;
            double OldValue = 0;
            double NewValue = 0;
            string NewValueMode = string.Empty;

            if (dtTrans != null)
            {
                NewValue = GetCalculatedAmount(LedgerId, dtTrans);
                OldValue = GetCalculatedTempAmount(LedgerId, dtTrans);

                LedgerBalance = GetCurBalance(LedgerId, OldValue, NewValue);
            }

            return LedgerBalance;
        }

        private string GetCurBalance(int LedId, double OldValue, double NewValue)
        {
            string Mode = string.Empty;
            double CurBal = 0.00;
            double dCalculateCurBal = 0.00;

            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                NewValue = NewValue * UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                OldValue = OldValue * UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
            }

            BalanceProperty Balance = FetchCurrentBalance(LedId);

            if (Balance.TransMode == TransactionMode.CR.ToString())
            {
                CurBal = -(Balance.Amount);
            }
            else
            {
                CurBal = Balance.Amount;
            }

            dCalculateCurBal = CurBal - (OldValue) + NewValue;
            if (dCalculateCurBal < 0)
            {
                Mode = TransactionMode.CR.ToString();
            }
            else
            {
                Mode = TransactionMode.DR.ToString();
            }

            return this.UtilityMember.NumberSet.ToCurrency(Math.Abs(dCalculateCurBal)) + " " + Mode;
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

        private double GetCalculatedAmount(int LedId, DataTable dtVoucher)
        {
            double dAmount = 0;
            if (dtVoucher.Rows.Count > 0)
            {
                double CrAmt = 0;
                double DrAmt = 0;

                CrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(CREDIT)", "LEDGER_ID='" + LedId + "'").ToString());

                CrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(CREDIT)", "LEDGER_ID='" + LedId + "'").ToString());
                DrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(DEBIT)", "LEDGER_ID='" + LedId + "'").ToString());
                dAmount = CrAmt - DrAmt;
                if (dAmount > 0)
                {
                    dAmount = -(dAmount);
                }
                else
                {
                    dAmount = Math.Abs(dAmount);
                }
            }
            return dAmount;
        }

        private double GetCalculatedTempAmount(int LedId, DataTable dtVoucher)
        {
            double dAmount = 0;
            if (dtVoucher.Rows.Count > 0)
            {
                double CrAmt = 0;
                double DrAmt = 0;

                CrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(TEMP_CREDIT)", "LEDGER_ID='" + LedId + "'").ToString());
                DrAmt = this.UtilityMember.NumberSet.ToDouble(dtVoucher.Compute("SUM(TEMP_DEBIT)", "LEDGER_ID='" + LedId + "'").ToString());

                dAmount = CrAmt - DrAmt;
                if (dAmount > 0)
                {
                    dAmount = -(dAmount);
                }
                else
                {
                    dAmount = Math.Abs(dAmount);
                }
            }
            return dAmount;
        }

        private DataTable GetCurrentBalance(DataTable dtVoucher)//, bool isCurrentBalance)
        {
            int LedgerId = 0;
            foreach (DataRow dr in dtVoucher.Rows)
            {
                LedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                if (LedgerId > 0)
                {
                    string Balance = GetLedgerBalanceValues(dtVoucher, LedgerId); //ShowLedgerBalance(LedgerId, dtVoucher, isTransGrid);
                    if (Balance != string.Empty)
                    {
                        dr["LEDGER_BALANCE"] = Balance;
                    }
                }
            }
            return dtVoucher;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterDetails.Rows.Count > 0)
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    //On 21/01/2025 - for journal invoice - let us have voucher date as invoice date -----------------------------------------
                    DtGSTInvoiceMasterDetails.Rows[0].BeginEdit();
                    DtGSTInvoiceMasterDetails.Rows[0][voucherTransaction.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName] = deDate.DateTime;
                    GSTVendorInvoiceDate = deDate.DateTime.ToShortDateString();
                    DtGSTInvoiceMasterDetails.Rows[0].EndEdit();
                    DtGSTInvoiceMasterDetails.AcceptChanges();
                }
                //---------------------------------------------------------------------------------------------------------------------------
            }

            if (IsJournalValid())
            {
                ResultArgs resultArgs = null;
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    //Voucher Master Details
                    //On 22/02/2015, for multi dupication voucher
                    if (glkpVoucherType.EditValue != null)
                    {
                        if (this.UtilityMember.NumberSet.ToInteger(glkpVoucherType.EditValue.ToString()) > 0)
                        {
                            VoucherDefinitionId = this.UtilityMember.NumberSet.ToInteger(glkpVoucherType.EditValue.ToString());
                        }
                    }

                    //On 09/02/2018, If duplication entry, clear voucher Id and make it add mode (Ref_01)
                    if (DuplicateVoucher)
                    {
                        voucherTransaction.VoucherId = 0;
                    }
                    else
                    {
                        voucherTransaction.VoucherId = VoucherId;
                    }

                    //On 25/07/2023, To alert to authorize voucher based on setting in Finance ----------------------------------
                    voucherTransaction.AuthorizedStatus = ConfirmVoucherAuthorization;
                    //-----------------------------------------------------------------------------------------------------------

                    voucherTransaction.ProjectId = ProjectId;
                    voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(deDate.Text, false);
                    voucherTransaction.dteTDSBookingDate = this.UtilityMember.DateSet.ToDate(deDate.Text, false);
                    voucherTransaction.VoucherType = "JN";
                    voucherTransaction.CashBankEntry = isMultiCashBankEntry ? 1 : 0;
                    voucherTransaction.VoucherDefinitionId = VoucherDefinitionId;
                    voucherTransaction.Status = (int)YesNo.Yes;
                    if (VoucherId == 0)
                    {
                        if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual) { voucherTransaction.VoucherNo = txtVoucher.Text; }
                        else { voucherTransaction.TransVoucherMethod = TransVoucherMethod; }
                    }
                    else { voucherTransaction.VoucherNo = txtVoucher.Text; }
                    voucherTransaction.CreatedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                    voucherTransaction.ModifiedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                    voucherTransaction.CreatedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                    voucherTransaction.ModifiedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                    voucherTransaction.Narration = txtJNarration.Text.Trim();
                    voucherTransaction.VoucherSubType = LedgerTypes.GN.ToString();

                    if (IsMultiNarrationEnabled())
                    {
                        voucherTransaction.IsMultiNarrationEnabled = true;
                    }

                    //Voucher Trans Details
                    DataTable dtTrans = gcTransaction.DataSource as DataTable;
                    DataView dvTrans = new DataView(dtTrans);
                    //  dvTrans.RowFilter = "LEDGER_ID>0 AND AMOUNT>0";
                    this.Transaction.TransInfo = dvTrans;

                    ApplyGST(dvTrans.ToTable(), null, true);
                    //Voucher Trans Details
                    //  this.Transaction.TransInfo = ((DataTable)gcTransaction.DataSource).DefaultView;
                    voucherTransaction.TDSBookingId = BookingId;

                    //Voucher TDS Deduction Later
                    voucherTransaction.PartyLedgerId = PartyLedgerId;
                    voucherTransaction.DeducteeTypeId = DeducteeTypeLedgerId;
                    voucherTransaction.ExpenseLedgerId = ExpenseLedgerId;

                    //Cost Centre Details
                    this.Transaction.CostCenterInfo = dsCostCentre;

                    voucherTransaction.dsTDSBooking = dsTDSBooking;
                    voucherTransaction.dtTDSDeductionLater = dtPartyTrans;
                    voucherTransaction.dvTdsUcTransSummary = dvTransUcSummary;
                    voucherTransaction.TDSPartyNarration = TDSPartyNarration;
                    voucherTransaction.TDSBookingDic = TDSAmountValidation;
                    //voucherTransaction.dsTDSDeductionLater = dsTDSDeductionLater;

                    //On 04//11/2022, to assing gst invoice details ----------------------------------------------
                    voucherTransaction.GST_INVOICE_ID = GSTInvoiceId;
                    voucherTransaction.dtGSTInvoiceMasterDetails = DtGSTInvoiceMasterDetails;
                    voucherTransaction.dtGSTInvoiceMasterLedgerDetails = DtGSTInvoiceMasterLedgerDetails;
                    voucherTransaction.GST_VENDOR_INVOICE_NO = GSTVendorInvoiceNo; // STVendorInvoiceNo.Trim();
                    voucherTransaction.GST_VENDOR_INVOICE_DATE = GSTVendorInvoiceDate;
                    voucherTransaction.GST_VENDOR_INVOICE_TYPE = GSTVendorInvoiceType;
                    voucherTransaction.GST_VENDOR_ID = GSTVendorId;
                    //--------------------------------------------------------------------------------------------

                    //On 04/09/2024, To Set Currency details 
                    voucherTransaction.CurrencyCountryId = 0;
                    voucherTransaction.ExchangeRate = 1;
                    voucherTransaction.CalculatedAmount = 0;
                    voucherTransaction.ActualAmount = 0;
                    voucherTransaction.ExchageCountryId = 0;
                    if (this.AppSetting.AllowMultiCurrency == 1)
                    {
                        if (this.Transaction.TransInfo != null)
                        {
                            DataView dv = this.Transaction.TransInfo;
                            foreach (DataRowView drv in dv)
                            {
                                drv.BeginEdit();
                                drv[voucherTransaction.AppSchema.VoucherTransaction.EXCHANGE_RATEColumn.ColumnName] = txtExchangeRate.Text;
                                drv[voucherTransaction.AppSchema.VoucherTransaction.LIVE_EXCHANGE_RATEColumn.ColumnName] = lblLiveExchangeRate.Text;
                                drv.EndEdit();
                            }
                        }

                        voucherTransaction.CurrencyCountryId = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                        voucherTransaction.ExchangeRate = this.UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text);
                        voucherTransaction.CalculatedAmount = this.UtilityMember.NumberSet.ToDecimal(lblCalculatedAmt.Text);
                        voucherTransaction.ContributionAmount = this.UtilityMember.NumberSet.ToDecimal(txtCurrencyAmount.Text);
                        voucherTransaction.ActualAmount = this.UtilityMember.NumberSet.ToDecimal(txtActualAmount.Text);
                        voucherTransaction.ExchageCountryId = this.glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                    }

                    //if (this.TDSBookingFromPartyPayment.Equals(1) && dsTDSBooking.Tables.Count == 0)
                    //{ 
                    //    if (this.ShowConfirmationMessage("This is not TDS Booking.It is just an ordinary journal." + Environment.NewLine + Environment.NewLine + "                        Do you want to proceed now?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //    {
                    //        resultArgs = voucherTransaction.SaveTransactions();
                    //    }
                    //}
                    //else
                    //{
                    resultArgs = voucherTransaction.SaveTransactions();
                    //}
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (this.TDSBookingFromPartyPayment.Equals(1) && dsTDSBooking.Tables.Count > 0)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.VoucherId = voucherTransaction.TDSBookingVoucherId;
                        }
                        else if (this.TDSBookingFromPartyPayment.Equals(1))
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        dsTDSBooking.Tables.Clear();
                        ExpenseLedgerAmount = 0;
                        ExpenseIsTDSLedger = PartyIsTDSLedger = 0;
                        dsTDSDeductionLater.Tables.Clear();
                        if (this.UIAppSetting.UIVoucherPrint == "1")
                        {
                            PrintVoucher(voucherTransaction.VoucherId);
                        }
                        else
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_SAVE));
                        }

                        if (VoucherId == 0)
                        {
                            ClearControls();
                        }
                        else
                        {
                            this.Close();
                        }
                        if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                        {
                            LoadVoucherNo();
                        }
                        else
                        {
                            txtVoucher.Text = string.Empty;
                        }
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                        isDeductTDS = true;
                    }
                    else if (resultArgs != null && !resultArgs.Success)
                    {
                        MessageRender.ShowMessage(resultArgs.Message);
                    }
                }

            }
        }

        private void rbtnDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        private void ucJournalShortcut_DateClicked(object sender, EventArgs e)
        {
            deDate.Focus();
        }

        private void ucJournalShortcut_LedgerClicked(object sender, EventArgs e)
        {
            ShowLedgerForm();
        }

        private void ucJournalShortcut_ConfigureClicked(object sender, EventArgs e)
        {
            LoadConfigure();
        }

        private void ucJournalShortcut_MappingClicked(object sender, EventArgs e)
        {
            LoadMapping();
        }

        private void ucJournalShortcut_ProjectClicked(object sender, EventArgs e)
        {
            ShowProjectSelectionWindow();
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucher.Text = string.Empty;
            }
        }

        private void ucJournalShortcut_LedgerAddClicked(object sender, EventArgs e)
        {
            ShowLedgerForm();
        }

        private void ucJournalShortcut_LedgerOptionsClicked(object sender, EventArgs e)
        {
            ShowLedgerOptionsForm();
        }

        private void bbiDeleteTrans_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CommonMethod.ApplyUserRights((int)Forms.DeleteJournalVoucher) != 0)
            {
                DeleteTransaction();
            }
        }

        private void bbiMoveTransaction_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FocusTransactionGrid();
        }

        private void bbiNewTrans_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClearControls();
        }

        private void ucJournalShortcut_TransactionVoucherViewClicked(object sender, EventArgs e)
        {
            frmTransactionVoucherView voucview = new frmTransactionVoucherView(projectName, ProjectId, deDate.DateTime, true);
            voucview.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                // deDate.Focus(); 
            }
            return base.ProcessCmdKey(ref msg, KeyData);

        }

        private void JournalAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            ApplyRecentPrjectDetails();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void mtxtnaration_Enter(object sender, EventArgs e)
        {
            SetFocusColor(txtJNarration);
        }

        private void txtJNarration_Leave(object sender, EventArgs e)
        {
            RemoveColor(txtJNarration);
        }

        private void ShowCostCentre(double LedgerAmount, int TDSPartyLedgerId = 0)
        {
            try
            {
                int CostCentre = 0;
                string LedgerName = string.Empty;
                DataView dvCostCentre = null;
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    if (!TDSPartyLedgerId.Equals(0))
                    {
                        voucherSystem.LedgerId = TDSPartyLedgerId;
                    }
                    else
                    {
                        voucherSystem.LedgerId = LedgerId;
                    }
                    resultArgs = voucherSystem.FetchCostCentreLedger();
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        LedgerName = resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        CostCentre = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                        if (CostCentre != 0 && !string.IsNullOrEmpty(LedgerName))
                        {
                            int RowIndex = this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetDataSourceRowIndex(gvTransaction.FocusedRowHandle).ToString());

                            if (dsCostCentre.Tables.Contains(RowIndex + "LDR" + voucherSystem.LedgerId))
                            {
                                dvCostCentre = dsCostCentre.Tables[RowIndex + "LDR" + voucherSystem.LedgerId].DefaultView;
                            }
                            frmTransactionCostCenter frmCostCentre = new frmTransactionCostCenter(ProjectId, dvCostCentre, voucherSystem.LedgerId, LedgerAmount, LedgerName);
                            frmCostCentre.ShowDialog();
                            if (frmCostCentre.DialogResult == DialogResult.OK)
                            {
                                DataTable dtValues = frmCostCentre.dtRecord;
                                if (dtValues != null)
                                {
                                    dtValues.TableName = RowIndex + "LDR" + voucherSystem.LedgerId;
                                    if (dsCostCentre.Tables.Contains(dtValues.TableName))
                                    {
                                        dsCostCentre.Tables.Remove(dtValues.TableName);
                                    }
                                    dsCostCentre.Tables.Add(dtValues);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void ucJournalShortcut_CostCentreClicked(object sender, EventArgs e)
        {
            ShowCostCentreForm();
        }

        private void txtJNarration_Enter(object sender, EventArgs e)
        {
            SetFocusColor(txtJNarration);
        }

        private void deDate_EditValueChanged(object sender, EventArgs e)
        {
            // 07/02/2025, Chinna
            rdtMaterilizsed.MinValue = deDate.DateTime;

            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucher.Text = string.Empty;
            }
            FetchDateDuration();

            //On 25/03/2021, To skip SDBINM Auditors mentioend Ledges
            if (AppSetting.IS_SDB_INM)
            {
                LoadLedger(rglkpLedger);
            }

            //26/08/2024, Load currency based on voucher date
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                LoadCountry();
                ShowCurrencyDetails();
            }
            else if (this.AppSetting.IsCountryOtherThanIndia)
            {
                ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, true);
            }
        }

        private void rCostcentre_Click(object sender, EventArgs e)
        {
            double dbtAmt = gvTransaction.GetFocusedRowCellValue(colDebit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colDebit).ToString()) : 0;
            double crtAmt = gvTransaction.GetFocusedRowCellValue(colCredit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colCredit).ToString()) : 0;

            if (crtAmt > 0 && LedgerId > 0)
            {
                ShowCostCentre(crtAmt);
            }
            else if (dbtAmt > 0 && LedgerId > 0)
            {
                ShowCostCentre(dbtAmt);
            }
        }

        private void rTDSBookingView_Click(object sender, EventArgs e)
        {
            int GroupId = 0;
            int isTDSLedger = 0;
            double crtAmt = gvTransaction.GetFocusedRowCellValue(colCredit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colCredit).ToString()) : 0;
            if (crtAmt > 0 && LedgerId > 0)
            {
                using (TDSBookingSystem tdsBookingSystem = new TDSBookingSystem())
                {
                    tdsBookingSystem.ExpenseLedgerId = LedgerId;
                    DataTable dtTdsBooking = tdsBookingSystem.FetchLedgerDetails();
                    if (dtTdsBooking != null && dtTdsBooking.Rows.Count > 0)
                    {
                        GroupId = dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString()) : 0;
                        isTDSLedger = dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString()) : 0;
                    }
                }
                if (isTDSLedger > 0 && GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors))
                {
                    IsTaxDeductable(TransactionMode.CR, crtAmt, true);
                }
            }
        }

        private void ucJournalShortcut_NextVoucherDateClicked(object sender, EventArgs e)
        {
            deDate.DateTime = deDate.DateTime.AddDays(1);
        }

        private void deDate_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void deDate_Enter(object sender, EventArgs e)
        {
            //01/03/2018, keep date value (To show alert message if selected date is locked for the selected project)
            dePreviousVoucherDate = deDate.DateTime;
        }

        /// <summary>
        /// Load Voucher types
        /// </summary>
        private void LoadVoucherType()
        {
            using (ProjectSystem projectsys = new ProjectSystem())
            {
                ResultArgs resultArg = projectsys.ProjectVouchers(ProjectId);
                if (resultArg != null && resultArg.Success)
                {
                    DataTable dtVoucherTypes = resultArg.DataSource.Table;
                    dtVoucherTypes.DefaultView.RowFilter = projectsys.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName + " IN (" + (Int32)DefaultVoucherTypes.Journal + ")";
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpVoucherType, dtVoucherTypes, projectsys.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName,
                        projectsys.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName);
                    glkpVoucherType.EditValue = VoucherDefinitionId;
                }
            }
        }

        private void LoadCountry()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    //On 26/08/2024, If multi currency enabled, let us load only currencies with have exchange rate for voucher date
                    if (this.AppSetting.AllowMultiCurrency == 1)
                        resultArgs = countrySystem.FetchCountryCurrencyDetails(this.UtilityMember.DateSet.ToDate(deDate.Text, false));
                    else
                        resultArgs = countrySystem.FetchCountryDetails();

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtCurencyCountry = resultArgs.DataSource.Table;
                        dtCurencyCountry.DefaultView.RowFilter = "";

                        //26/08/2024, Load Currecny which have exchange rate
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            dtCurencyCountry.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " >0"; ;
                            dtCurencyCountry = dtCurencyCountry.DefaultView.ToTable();
                        }
                        //this.UtilityMember.ComboSet.BindLookUpEditCombo(glkpCurrencyCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCurrencyCountry, resultArgs.DataSource.Table,
                            countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        //26/08/2024, For new voucher, set default currecny (global setting)
                        if (VoucherId == 0 && this.AppSetting.AllowMultiCurrency == 1)
                        {
                            glkpCurrencyCountry.EditValue = string.IsNullOrEmpty(this.AppSetting.Country) ? 0 : UtilityMember.NumberSet.ToInteger(this.AppSetting.Country);

                            object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(this.AppSetting.Country);
                            if (findcountry == null) glkpCurrencyCountry.EditValue = null;
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

        private void deDate_KeyDown(object sender, KeyEventArgs e)
        {
            //On 06/11/2019, to move foucs grid
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    glkpCurrencyCountry.Select();
                    glkpCurrencyCountry.Focus();
                }
                else
                {
                    if (txtVoucher.Enabled)
                    {
                        txtVoucher.Focus();
                    }
                    else
                    {
                        FocusTransactionGrid();
                    }
                }
                e.SuppressKeyPress = true;
            }
        }

        private void rglkpLedger_QueryPopUp(object sender, CancelEventArgs e)
        {
            //19/07/2021, To set Popup widow size
            if (sender != null)
            {
                GridLookUpEdit editor = (GridLookUpEdit)sender;
                SetGridLookPopupWindowSize(editor);
            }
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            ShowVendorGSTInvoiceDetails();
        }

        private void btnRemoveVendorGSTInvoce_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to Remove Vendor GST Invoice details ?", MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    ResultArgs result = vouchersystem.RemoveGSTVendorInvoiceDetailsById(VoucherId, GSTInvoiceId);
                    if (result.Success)
                    {
                        GSTVendorInvoiceNo = string.Empty;
                        GSTVendorInvoiceDate = string.Empty;
                        GSTVendorInvoiceType = 0;
                        GSTVendorId = 0;
                        GSTInvoiceId = 0;
                        DtGSTInvoiceMasterDetails = null;
                        DtGSTInvoiceMasterLedgerDetails = null;
                        ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, true);
                        this.ShowMessageBox("Invoice details is removed");
                    }
                }
            }
        }

        /// <summary>
        /// 25/04/2019, based on the condiditons, show additional buttons
        /// </summary>
        private void ShowAdditionButtons(AdditionButttons additionbuttions, bool show)
        {
            //1. For Vendor GST Invoice details
            if (additionbuttions == AdditionButttons.VendorGSTInvoiceDetails && this.AppSetting.IncludeGSTVendorInvoiceDetails == "2")
            {
                btnVendor.Visible = show;
                lcVendor.Visibility = show ? LayoutVisibility.Always : LayoutVisibility.Never;
                lcRemoveVendorGSTInvoice.Visibility = LayoutVisibility.Never;
                glkpCurrencyCountry.Enabled = true;
                if (show && ((VoucherId > 0 && GSTInvoiceId > 0) || (DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterDetails.Rows.Count > 0)))
                {
                    lcRemoveVendorGSTInvoice.Visibility = LayoutVisibility.Always;

                    //On 20/01/2025 - If GST Invoice is attached, you can't change Currency details 
                    glkpCurrencyCountry.Enabled = false;

                }
            }
        }

        private void LoadGSTLedgerClass()
        {
            using (GSTClassSystem GstClass = new GSTClassSystem())
            {
                resultArgs = GstClass.FetchGSTClassList();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    rglkpLedgerGST.DisplayMember = "GST_NAME";
                    rglkpLedgerGST.ValueMember = "GST_Id";

                    DataTable dtGSTClass = resultArgs.DataSource.Table;
                    dtGSTClass.DefaultView.RowFilter = GstClass.AppSchema.MasterGSTClass.APPLICABLE_FROMColumn.ColumnName + "<'" + UtilityMember.DateSet.ToDate(deDate.DateTime.ToShortDateString()) + "'";
                    dtGSTClass = dtGSTClass.DefaultView.ToTable();

                    rglkpLedgerGST.DataSource = dtGSTClass;
                }
            }
        }

        /// <summary>
        /// This method is used to attach GST legers in Receipt voucher entry (CR grid) automatically and add total GST amount into (Cash/Bank)
        /// 
        /// This method should be called only for 
        ///     Receipts Vouchers, When User Clicks Save button after confimation (Y/N)
        ///     1. GST should be enabled
        ///     2. GST amoutn should be > 0  
        /// </summary>
        private void ApplyGST(DataTable dtVoucherTrans, DataTable dtCashBankTrans, bool isAttach)
        {
            if (colGStAmt.Visible)
            {
                if (deDate.DateTime >= this.AppSetting.GSTStartDate)
                {
                    gstCalcAmount = UpdateGST = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(GST)", "").ToString());
                    cgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(CGST)", "").ToString());
                    sgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(SGST)", "").ToString());
                    igstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(IGST)", "").ToString());
                    double GSTAmount = igstCalcAmount == 0 ? (cgstCalcAmount + sgstCalcAmount) : igstCalcAmount;

                    //0. Check GST Ledges avialbe
                    if (this.AppSetting.CGSTLedgerId > 0 && this.AppSetting.SGSTLedgerId > 0)
                    {
                        //1. Check GST Enabgled 
                        if (AppSetting.EnableGST == "1")
                        {
                            //2. Check GST amount, If GST amout available, ask confirmationh from user to attach GST leges in Voucher entry
                            if (GSTAmount > 0)
                            {
                                if (isAttach)
                                {
                                    //if (this.ShowConfirmationMessage("Do you want to apply GST for this Voucher?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                    //{ //On 29/11/2019
                                    //3. Attach CGST

                                    if (cgstCalcAmount > 0 && this.AppSetting.CGSTLedgerId > 0)
                                    {
                                        DataRow drCGST = dtVoucherTrans.NewRow();
                                        drCGST["LEDGER_ID"] = this.AppSetting.CGSTLedgerId;
                                        // drCGST["AMOUNT"] = cgstCalcAmount;
                                        if (DebitGST > 0 && CreditGST == 0)
                                            drCGST["DEBIT"] = cgstCalcAmount;
                                        else
                                            drCGST["CREDIT"] = cgstCalcAmount;

                                        //if (rgTransactionType.SelectedIndex == 0)
                                        //{
                                        //    drCGST["SOURCE"] = 1;
                                        //}
                                        //else { drCGST["SOURCE"] = 2; }

                                        dtVoucherTrans.Rows.Add(drCGST);
                                    }

                                    //4. Attach SGST
                                    if (sgstCalcAmount > 0 && this.AppSetting.SGSTLedgerId > 0)
                                    {
                                        DataRow drSGST = dtVoucherTrans.NewRow();
                                        drSGST["LEDGER_ID"] = this.AppSetting.SGSTLedgerId;
                                        //drSGST["AMOUNT"] = sgstCalcAmount;
                                        if (DebitGST > 0 && CreditGST == 0)
                                            drSGST["DEBIT"] = sgstCalcAmount;
                                        else
                                            drSGST["CREDIT"] = sgstCalcAmount;

                                        //if (rgTransactionType.SelectedIndex == 0)
                                        //{
                                        //    drSGST["SOURCE"] = 1;
                                        //}
                                        //else { drSGST["SOURCE"] = 2; }
                                        // drSGST["SOURCE"] = 1;
                                        dtVoucherTrans.Rows.Add(drSGST);
                                    }

                                    //5. Attach IGST 
                                    if (igstCalcAmount > 0 && this.AppSetting.IGSTLedgerId > 0)
                                    {
                                        DataRow drIGST = dtVoucherTrans.NewRow();
                                        drIGST["LEDGER_ID"] = this.AppSetting.IGSTLedgerId;
                                        //drIGST["AMOUNT"] = igstCalcAmount;
                                        if (DebitGST > 0 && CreditGST == 0)
                                            drIGST["DEBIT"] = igstCalcAmount;
                                        else
                                            drIGST["CREDIT"] = igstCalcAmount;

                                        //if (rgTransactionType.SelectedIndex == 0)
                                        //{
                                        //    drIGST["SOURCE"] = 1;
                                        //}
                                        //else { drIGST["SOURCE"] = 2; }

                                        //drIGST["SOURCE"] = 1;
                                        dtVoucherTrans.Rows.Add(drIGST);
                                    }

                                    //Chinna 08/08/2023
                                    ////6. Add Total GST amount into Cash/Bank ledgers
                                    //if (dtCashBankTrans.Rows.Count > 0)
                                    //{
                                    //    double cashBankamount = this.UtilityMember.NumberSet.ToDouble(dtCashBankTrans.Rows[0]["Amount"].ToString());
                                    //    //dtCashBankTrans.Rows[0]["Amount"] = cashBankamount + gstCalcAmount;
                                    //    dtCashBankTrans.Rows[0]["Amount"] = cashBankamount;
                                    //    UpdateGST = gstCalcAmount - UpdateGST;
                                    //    dtCashBankTrans.Rows[0]["GST_TOTAL"] = this.UtilityMember.NumberSet.ToNumber(gstCalcAmount);
                                    //    dtCashBankTrans.AcceptChanges();
                                    //}

                                    if (dtVoucherTrans.Rows.Count > 0)
                                    {
                                        double DebitAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Rows[0]["DEBIT"].ToString());
                                        dtVoucherTrans.Rows[0]["DEBIT"] = DebitAmount;
                                        UpdateGST = gstCalcAmount - UpdateGST;
                                        //dtVoucherTrans.Rows[0]["GST_TOTAL"] = this.UtilityMember.NumberSet.ToNumber(gstCalcAmount);
                                        dtVoucherTrans.AcceptChanges();
                                    }

                                    //26/04/2019, to ask confirmation to update gst vendor invoice detials
                                    dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID>0 AND CREDIT>0 OR DEBIT>0"; //AMOUNT>0";
                                    // dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID NOT IN(" + this.AppSetting.CGSTLedgerId + ") AND LEDGER_ID<>" + this.AppSetting.SGSTLedgerId + " AND LEDGER_ID<>" + this.AppSetting.IGSTLedgerId;
                                    // dtVoucherTrans.AcceptChanges();
                                    this.Transaction.TransInfo = dtVoucherTrans.DefaultView;
                                    //On 10/04/2023, move this logic to alert to fill gst invoice detals in validation screen
                                    /*if (CanShowVendorGST)
                                    {
                                        if (string.IsNullOrEmpty(GSTVendorInvoiceNo))
                                        {
                                            if (this.ShowConfirmationMessage("Do you want to update Vendor GST Invoice Details ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                            {
                                                ShowVendorGSTInvoiceDetails();
                                            }
                                            else
                                            {
                                                //26/04/2019, clear vendor gst invoice
                                                GSTVendorInvoiceNo = string.Empty;
                                                GSTVendorInvoiceDate = string.Empty;
                                                GSTVendorInvoiceType = 0;
                                                GSTVendorId = 0;
                                                DtGSTInvoiceMasterDetails = null;
                                                DtGSTInvoiceMasterLedgerDetails = null;
                                            }
                                        }
                                    }*/
                                    //}
                                    //else
                                    //{
                                    //    ClearGSTValues(dtVoucherTrans);
                                    //    dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID NOT IN(" + this.AppSetting.CGSTLedgerId + ") AND LEDGER_ID<>" + this.AppSetting.SGSTLedgerId + " AND LEDGER_ID<>" + this.AppSetting.IGSTLedgerId;
                                    //}
                                }
                                else
                                {
                                    if (dtVoucherTrans.Rows.Count > 0)
                                    {
                                        gstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(GST)", "").ToString());
                                        cgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(CGST)", "").ToString());
                                        sgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(SGST)", "").ToString());
                                        igstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(IGST)", "").ToString());

                                        dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID NOT IN(" + this.AppSetting.CGSTLedgerId + ") AND LEDGER_ID<>" + this.AppSetting.SGSTLedgerId + " AND LEDGER_ID<>" + this.AppSetting.IGSTLedgerId;
                                    }

                                    // Chinna 09/08/2023
                                    //if (dtCashBankTrans != null && dtCashBankTrans.Rows.Count > 0)
                                    //{
                                    //    // Show Actual Cash\Bank Balance without dedect the GST
                                    //    double cashBankamount = this.UtilityMember.NumberSet.ToDouble(dtCashBankTrans.Rows[0]["Amount"].ToString());
                                    //    dtCashBankTrans.Rows[0]["Amount"] = cashBankamount;
                                    //    dtCashBankTrans.Rows[0]["GST_TOTAL"] = this.UtilityMember.NumberSet.ToNumber(gstCalcAmount);
                                    //    dtCashBankTrans.Rows[0]["TEMP_AMOUNT"] = cashBankamount;
                                    //    dtCashBankTrans.Rows[0]["BASE_AMOUNT"] = cashBankamount;
                                    //    dtCashBankTrans.AcceptChanges();
                                    //}
                                }
                            }
                        }
                        else
                        {
                            ClearGSTValues(dtVoucherTrans);
                            dtVoucherTrans.DefaultView.RowFilter = "LEDGER_ID NOT IN(" + this.AppSetting.CGSTLedgerId + ") AND LEDGER_ID<>" + this.AppSetting.SGSTLedgerId + " AND LEDGER_ID<>" + this.AppSetting.IGSTLedgerId;
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage("GST ledgers are not avialble");
                    }
                }
                else
                {
                    DataTable dtVoucher = gcTransaction.DataSource as DataTable;
                    ClearGSTValues(dtVoucher);
                }
            }
            else
            {
                DataTable dtVoucher = gcTransaction.DataSource as DataTable;
                ClearGSTValues(dtVoucher);
            }
        }

        /// <summary>
        /// On 28/12/2019, to get applicable from 
        /// </summary>
        /// <param name="gstclassid"></param>
        /// <returns></returns>
        private DateTime GetGSTApplicableFrom(Int32 gstclassid)
        {
            DateTime gstcalssapplyfrom = this.AppSetting.GSTStartDate;
            using (GSTClassSystem gstclass = new GSTClassSystem(gstclassid))
            {
                if (!string.IsNullOrEmpty(UtilityMember.DateSet.ToDate(gstclass.ApplicableFrom.ToShortDateString())))
                {
                    gstcalssapplyfrom = gstclass.ApplicableFrom;
                }
            }

            return gstcalssapplyfrom;
        }

        private void deDate_Validating(object sender, CancelEventArgs e)
        {
            //01/03/2018, To show alert message if selected date is locked for the selected project
            //if (dePreviousVoucherDate != dtTransactionDate.DateTime)
            //{
            if (IsLockedTransaction(deDate.DateTime))
            {
                if (VoucherId == 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + "'" + ProjectName + "'," +
                            " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                            " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_EDIT) + "'" + ProjectName + "'," +
                            " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                            " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
                }
            }
            //}

            //26/08/2024, Load currency based on voucher date
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                ShowCurrencyDetails();
            }
        }
        #endregion

        #region Methods
        private void LoadLedger()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchLedgerLookup();
                    rglkpLedger.DataSource = null;
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //On 20/10/2021, to skip Closed Ledgers
                        DataTable dtBaseLedger = FilterLedgerByDateClosed(resultArgs.DataSource.Table, false, deDate.DateTime);

                        /*//28/03/2023, Enforce lodger locking in Voucher entry screen ---------------------------------------------------------
                        ResultArgs result = EnforceLockMastersInVoucherEntry(Id.Ledger, dtBaseLedger.DefaultView.ToTable(), deDate.DateTime);
                        if (result.Success)
                        {
                            dtBaseLedger = result.DataSource.Table;
                        }
                        //--------------------------------------------------------------------------------------------------------------------
                        */

                        rglkpLedger.DataSource = dtBaseLedger;
                        rglkpLedger.DisplayMember = "LEDGER_NAME";
                        rglkpLedger.ValueMember = "LEDGER_ID";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadVoucherNo()
        {
            string vType = string.Empty;
            string pId = string.Empty;
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = "JN";
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(deDate.Text, false);
                voucherTransaction.VoucherDefinitionId = VoucherDefinitionId;
                txtVoucher.Text = voucherTransaction.TempVoucherNo();
            }
        }

        private void ApplyUserRights()
        {
            ucJournalShortcut.DisableMapping = CommonMethod.ApplyUserRightsForTransaction((int)Menus.AccountMapping) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
            ucJournalShortcut.DisableConfigure = CommonMethod.ApplyUserRights((int)Donor.CreateDonor) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
            bbiDeleteTrans.Visibility = CommonMethod.ApplyUserRights((int)Journal.DeleteJournalVoucher) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
            bbiNewTrans.Visibility = CommonMethod.ApplyUserRights((int)Journal.CreateJournalVoucher) != 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        private void SetDisableShorcuts()
        {
            ucJournalShortcut.DisableBankAccount = DevExpress.XtraBars.BarItemVisibility.Never;
            ucJournalShortcut.DisableDonor = DevExpress.XtraBars.BarItemVisibility.Never;
            ucJournalShortcut.DisableJournal = DevExpress.XtraBars.BarItemVisibility.Never;
            ucJournalShortcut.DisableContra = DevExpress.XtraBars.BarItemVisibility.Never;
            ucJournalShortcut.DisableReceipt = DevExpress.XtraBars.BarItemVisibility.Never;
            ucJournalShortcut.DisablePayment = DevExpress.XtraBars.BarItemVisibility.Never;
            ucJournalShortcut.DisableConfigure = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void SetLoadDefault()
        {
            SetTitle();
            LoadVoucherType();
            Construct();
            FetchVoucherMethod();
            LoadLedger(rglkpLedger);
            LoadNarrationAutoComplete();

            gstCalcAmount = 0.0;
            cgstCalcAmount = 0.0;
            sgstCalcAmount = 0.0;
            igstCalcAmount = 0.0;

            ShowTransGST();
            AssignValues();

            RealColumnCreditAmount();
            RealColumnEditDebitAmount();
            FetchDateDuration();
            if (IsMultiNarrationEnabled())
            {
                colNarration.Visible = true;
                colNarration.VisibleIndex = 4;
            }

            ShowReferenceNumber();
        }

        private void Construct()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LEDGER_ID", typeof(string));
            dt.Columns.Add("EXCHANGE_RATE", typeof(decimal));
            dt.Columns.Add("LIVE_EXCHANGE_RATE", typeof(decimal));
            dt.Columns.Add("NARRATION", typeof(string));
            dt.Columns.Add("REFERENCE_NUMBER", typeof(string));
            dt.Columns.Add("LEDGER_GST_CLASS_ID", typeof(Int32));
            dt.Columns.Add("CHEQUE_NO", typeof(string));
            dt.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dt.Columns.Add("OLDLEDGERID", typeof(string));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            dt.Columns.Add("DEBIT", typeof(decimal));
            dt.Columns.Add("CREDIT", typeof(decimal));
            dt.Columns.Add("GST_AMOUNT", typeof(string));
            dt.Columns.Add("GST", typeof(decimal));
            dt.Columns.Add("CGST", typeof(decimal));
            dt.Columns.Add("SGST", typeof(decimal));
            dt.Columns.Add("IGST", typeof(decimal));
            dt.Columns.Add("TEMP_CREDIT", typeof(decimal));
            dt.Columns.Add("TEMP_DEBIT", typeof(decimal));
            dt.Columns.Add("LEDGER_BALANCE", typeof(string));
            dt.Columns.Add("VOUCHER_DATE", typeof(string));
            dt.Columns.Add("REF_AMOUNT", typeof(string));
            dt.Columns.Add("VALUE", typeof(int));
            dt.Columns.Add("IS_GST_LEDGERS", typeof(int));
            gcTransaction.DataSource = dt;
        }

        private void LoadLedger(string NatureId, RepositoryItemGridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchLedgerByGroup();
                    glkpLedger.DataSource = null;
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //On 20/10/2021, to skip Closed Ledgers
                        DataTable dtBaseLedger = FilterLedgerByDateClosed(resultArgs.DataSource.Table, false, deDate.DateTime);

                        glkpLedger.DataSource = dtBaseLedger;// resultArgs.DataSource.Table;
                        glkpLedger.DisplayMember = "LEDGER_NAME";
                        glkpLedger.ValueMember = "LEDGER_ID";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void RealColumnCreditAmount()
        {
            colCredit.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvTransaction.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvTransaction.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colCredit)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvTransaction.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditDebitAmount()
        {
            colDebit.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvTransaction.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvTransaction.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colDebit)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvTransaction.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void LoadConfigure()
        {
            //Modules.Master.frmSettings setting = new Modules.Master.frmSettings();
            //setting.ShowDialog() frmDonorAdd frmDonor = new frmDonorAdd(ViewDetails.Donor, (int)AddNewRow.NewRow, ProjectId);
            frmDonorAdd frmDonor = new frmDonorAdd(ViewDetails.Donor, (int)AddNewRow.NewRow, ProjectId);
            frmDonor.ShowDialog();
            if (frmDonor.DialogResult == DialogResult.OK)
            {
                // LoadDonorDetails();
            }
        }

        private void LoadMapping()
        {
            Modules.Master.frmMapProjectLedger objMap = new Modules.Master.frmMapProjectLedger();
            objMap.ShowDialog();
        }

        private void LoadProjects()
        {
            Modules.Transaction.frmProjectSelection frmProject = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod);
            frmProject.ShowDialog();
        }

        private void SetTitle()
        {
            if (VoucherId == 0)
            {
                this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.JOURNAL_ADD_CAPTION);
            }
            else
            {
                if (!DuplicateVoucher)
                {
                    this.Text = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.JOURNAL_EDIT_CAPTION);
                }
                else
                {
                    this.Text = "Voucher (Duplicate Entry)";
                }
            }
        }

        private void LoadLedger(RepositoryItemGridLookUpEdit glkpLedger)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    if (this.UIAppSetting.EnableCashBankJournal == 1)
                    {
                        ledgerSystem.ModeId = 1;
                    }
                    else
                    {
                        ledgerSystem.ModeId = 0;
                    }
                    resultArgs = ledgerSystem.FetchLedgerByGroup();
                    glkpLedger.DataSource = null;
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        CostCentreInfo = resultArgs.DataSource.Table.DefaultView;

                        //On 25/03/2021, To skip SDBINM Auditors mentioend Ledges
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        if (AppSetting.IS_SDB_INM)
                        {
                            dtLedgers = this.ForSDBINMSkipLedgers(deDate.Text, dtLedgers);
                        }

                        //On 20/10/2021, to skip Closed Ledgers
                        DataTable dtBaseLedger = FilterLedgerByDateClosed(dtLedgers, false, deDate.DateTime);

                        glkpLedger.DataSource = dtBaseLedger;//  resultArgs.DataSource.Table;


                        glkpLedger.DisplayMember = "LEDGER_NAME";
                        glkpLedger.ValueMember = "LEDGER_ID";
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_LEDGER_MAPPING_TO_PROJECT) + ProjectName + "");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private bool IsValidaTransactionRow()
        {
            bool IsLedgerValid = false;
            try
            {
                IsLedgerValid = (LedgerId > 0); // 04/09/2023 // (LedgerId > 0 && (LedgerCreditAmount + ledgerDebitAmount) > 0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return IsLedgerValid;
        }

        private void FetchVoucherMethod()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, "4", 4);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    TransVoucherMethod = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][projectSystem.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                    if (TransVoucherMethod == 1) { txtVoucher.Enabled = false; } else { txtVoucher.Enabled = true; }
                }
                else
                {
                    TransVoucherMethod = 2;
                }
            }
        }

        private bool IsJournalValid()
        {
            bool isValid = true;
            DataTable transSource = (DataTable)gcTransaction.DataSource;
            if (transSource.Rows.Count == 0)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NO_RECORD));
                isValid = false;
            }
            else if (!ValidateGST())
            {
                isValid = false;
                gcTransaction.Focus();
            }
            // else if ((SummaryCredit + (AppSetting.EnableGST == "1" ? SummaryGST : 0)) != SummaryDebit) // ummarycredit
            else if ((SummaryCredit + CreditGST) != (SummaryDebit + DebitGST))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_MISMATCH));
                gcTransaction.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(deDate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TRANSACTION_DATE));
                isValid = false;
                deDate.Select();
                deDate.Focus();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && string.IsNullOrEmpty(txtVoucher.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                isValid = false;
                txtVoucher.Focus();
            }
            else if (!IsValidTransGrid())
            {
                isValid = false;
                gcTransaction.Focus();
            }
            else if (this.AppSetting.EnableCashBankJournal == 1 && !IsValidCashBankTransactions())
            {
                isValid = false;
                gcTransaction.Focus();
            }
            else if (SummaryCredit == 0 || SummaryDebit == 0)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                isValid = false;
                gcTransaction.Focus();
            }
            else if (IsLockedTransaction(deDate.DateTime))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED) + "'" + ProjectName + "'" +
                        " during the period " + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                        " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString()));
                deDate.Focus();
                isValid = false;
            }
            else if (!IsValidateBookedReferedValue())
            {
                isValid = false;
                gcTransaction.Focus();

            }
            else if (!ValidateCCAmoutWithLedgerAmount())
            {
                isValid = false;
                gcTransaction.Select();
                gcTransaction.Focus();
            }
            else if (!ValidateGSTInvoiceDetails())
            {
                isValid = false;
                gcTransaction.Select();
                gcTransaction.Focus();
            }
            else if (!isExistInvoiceNo())
            {
                isValid = false;
                MessageRender.ShowMessage("'" + GSTVendorInvoiceNo + "' GST Invoice No is already exists");
                gcTransaction.Select();
                gcTransaction.Focus();
            }
            else if (TDSBookingFromPartyPayment > 0)
            {// this is to validate the Voucher Date is less than or equal to the Party payment Voucher Date if TDSBookingFromPartyPayment greter than 0
                DateTime dtPrvVoucherDate = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);
                if (deDate.DateTime > dtPrvVoucherDate)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TDS_BOOKING_DATE_VALIDATE_PARTYPAYMENT_DATE));
                    deDate.Focus();
                    isValid = false;
                }
            }
            else if (this.AppSetting.AllowMultiCurrency == 1 && !IsCurrencyEnabledVoucher)
            { //On 04/09/2024, If multi currency enabled, all the currency details must be filled
                MessageRender.ShowMessage("As Multi Currency option is enabled, All the Currecny details should be filled.");
                glkpCurrencyCountry.Select();
                glkpCurrencyCountry.Focus();
                isValid = false;
            }
            else if (!ValidateCurrencyAmountWithTransAmount())
            {
                isValid = false;
                FocusTransactionGrid();
            }
            else if (!IsValidateAgainstRandPVoucher())
            {
                isValid = false;
                deDate.Focus();
            }

            if (isValid && DuplicateVoucher)
            {
                if (this.ShowConfirmationMessage("Are you sure to Replicate Voucher Entry ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == System.Windows.Forms.DialogResult.No)
                {
                    isValid = false;
                }
            }

            return isValid;
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
            lblDonorCurrency.Text = string.Empty;
            txtExchangeRate.Text = "1";
            lblLiveExchangeRate.Text = "1.00";
            try
            {
                if (CountryId != 0)
                {
                    using (CountrySystem countrySystem = new CountrySystem())
                    {
                        ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                        if (result.Success)
                        {
                            lblDonorCurrency.Text = countrySystem.CurrencySymbol;
                            txtExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                            lblLiveExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);

                            AssignLiveExchangeRate();

                        }
                    }

                    bool show = (this.AppSetting.IncludeGSTVendorInvoiceDetails == "2" && IsLocalCurrencyVoucher);
                    ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, show);
                }
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
                    double liveExchangeAmount = this.AppSetting.CurrencyLiveExchangeRate(deDate.DateTime.Date, countrySystem.CurrencyCode, AppSetting.CurrencyCode);
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

        private bool IsValidTransGrid()
        {
            DataTable dtTrans = gcTransaction.DataSource as DataTable;

            int GroupId = 0;
            int Id = 0;
            decimal AmtCR = 0;
            decimal AmtDR = 0;
            int RowPosition = 0;
            bool isValid = false;
            DateTime dtClosedDate = DateTime.MinValue;
            string validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_NOT_FILLED);

            DataView dv = new DataView(dtTrans);
            dv.RowFilter = "(LEDGER_ID>0 OR (CREDIT>0 OR DEBIT >0) )";
            gvTransaction.FocusedColumn = colLedger;
            if (dv.Count > 0)
            {
                isValid = true;
                foreach (DataRowView drTrans in dv)
                {
                    Id = this.UtilityMember.NumberSet.ToInteger(drTrans["LEDGER_ID"].ToString());
                    AmtCR = this.UtilityMember.NumberSet.ToDecimal(drTrans["CREDIT"].ToString());
                    AmtDR = this.UtilityMember.NumberSet.ToDecimal(drTrans["DEBIT"].ToString());

                    GroupId = GetLedgerGroupId(Id);

                    //Check Ledger Closed Date
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        dtClosedDate = ledgersystem.GetLedgerClosedDate(Id);
                    }
                    //if ((Id == 0 || Amt == 0 || Source == 0)) //&& !(Id == 0 && Amt == 0))
                    if ((Id == 0 || (AmtCR == 0 && AmtDR == 0)) || (!(dtClosedDate >= deDate.DateTime)) && (!(dtClosedDate == DateTime.MinValue)))
                    {
                        if (Id == 0)
                        {
                            validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_LEDGER_EMPTY);
                            gvTransaction.FocusedColumn = colLedger;
                        }
                        if ((AmtCR == 0 && AmtDR == 0))
                        {
                            validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_AMOUNT_EMPTY);
                            gvTransaction.FocusedColumn = colCredit;
                        }
                        isValid = false;
                        break;
                    }
                    RowPosition = RowPosition + 1;

                    if (GroupId == (int)FixedLedgerGroup.BankAccounts || GroupId == (int)FixedLedgerGroup.Cash)
                    {
                        isMultiCashBankEntry = true;
                    }

                }
            }

            if (!isValid)
            {
                this.ShowMessageBox(validateMessage);
                gvTransaction.CloseEditor();
                gvTransaction.FocusedRowHandle = gvTransaction.GetRowHandle(RowPosition);
                gvTransaction.ShowEditor();
            }

            return isValid;
        }

        private bool IsValidCashBankTransactions()
        {
            DataTable dtTrans = gcTransaction.DataSource as DataTable;

            bool isBankSelected = false;
            bool isCashSelected = false;
            int bankDebitCount = 0;
            int bankCreditCount = 0;
            int cashDebitCount = 0;
            int cashCreditCount = 0;
            int totalDebitEntries = 0;
            int totalCreditEntries = 0;

            decimal AmtCR = 0;
            decimal AmtDR = 0;
            int GroupId = 0;
            int Id = 0;
            int RowPosition = 0;
            bool isValid = false;
            DateTime dtClosedDate = DateTime.MinValue;
            string validateMessage = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_NOT_FILLED);

            DataView dv = new DataView(dtTrans);
            dv.RowFilter = "(LEDGER_ID>0 OR (CREDIT>0 OR DEBIT >0) )";
            gvTransaction.FocusedColumn = colLedger;
            if (dv.Count > 0)
            {
                isValid = true;
                foreach (DataRowView drTrans in dv)
                {
                    Id = this.UtilityMember.NumberSet.ToInteger(drTrans["LEDGER_ID"].ToString());
                    AmtCR = this.UtilityMember.NumberSet.ToDecimal(drTrans["CREDIT"].ToString());
                    AmtDR = this.UtilityMember.NumberSet.ToDecimal(drTrans["DEBIT"].ToString());

                    GroupId = GetLedgerGroupId(Id);


                    if (AmtDR > 0) totalDebitEntries++;
                    if (AmtCR > 0) totalCreditEntries++;

                    if (GroupId == 12)
                    {
                        isBankSelected = true;

                        if (AmtDR > 0) bankDebitCount++;
                        if (AmtCR > 0) bankCreditCount++;
                    }
                    else if (GroupId == 13)
                    {
                        isCashSelected = true;

                        if (AmtDR > 0) cashDebitCount++;
                        if (AmtCR > 0) cashCreditCount++;
                    }
                    RowPosition = RowPosition + 1;
                }

            }
            if (isBankSelected && isCashSelected)
            {
                validateMessage = "Bank and Cash cannot be in the same transaction.";
                isValid = false;
            }
            else if (bankDebitCount > 0 && bankCreditCount > 0)
            {
                validateMessage = "A bank entry cannot exist on both Debit and Credit sides.";
                isValid = false;
            }
            else if (cashDebitCount > 0 && cashCreditCount > 0)
            {
                validateMessage = "A cash entry  cannot exist on both Debit and Credit sides.";
                isValid = false;
            }
            else if ((bankDebitCount > 0 || cashDebitCount > 0) && totalDebitEntries > 1)
            {
                validateMessage = "If a bank or cash entry is in the Debit side, no other Debit entries are allowed.";
                isValid = false;
            }
            else if ((bankCreditCount > 0 || cashCreditCount > 0) && totalCreditEntries > 1)
            {
                validateMessage = "If a bank or cash entry is in the Credit side, no other Credit entries are allowed.";
                isValid = false;
            }


            if (!isValid)
            {
                this.ShowMessageBox(validateMessage);
                gvTransaction.CloseEditor();
                gvTransaction.FocusedRowHandle = gvTransaction.GetRowHandle(RowPosition);
                gvTransaction.ShowEditor();
            }

            return isValid;
        }


        private Int32 GetLedgerGroupId(Int32 ledgerId)
        {
            Int32 LedgerGroupId = 0;
            try
            {
                using (LedgerSystem ledger = new LedgerSystem())
                {
                    ledger.LedgerId = ledgerId;
                    LedgerGroupId = ledger.FetchLedgerGroupById();
                }
            }
            catch (Exception Err)
            {
                MessageRender.ShowMessage("Could not get Ledger Group " + Err.Message);
            }
            return LedgerGroupId;
        }

        private void ClearControls()
        {
            glkpCurrencyCountry.EditValue = null;
            txtActualAmount.Text = string.Empty;
            txtCurrencyAmount.Text = string.Empty;
            txtExchangeRate.Text = string.Empty;
            lblLiveExchangeRate.Text = "0.0";
            lblCalculatedAmt.Text = "0.0";
            txtVoucher.Text = "";
            txtJNarration.Text = "";
            gcTransaction.DataSource = null;
            Construct();
            FocusTransactionGrid();
            DuplicateVoucher = false;//After savining duplication entry, change as default enty mode

            GSTVendorInvoiceNo = string.Empty;
            GSTVendorInvoiceDate = string.Empty;
            GSTVendorInvoiceType = 0;
            GSTVendorId = 0;
            GSTInvoiceId = 0;
            DtGSTInvoiceMasterDetails = null;
            DtGSTInvoiceMasterLedgerDetails = null;
            ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, false);
        }

        private void FocusTransactionGrid()
        {
            gcTransaction.Focus();
            gvTransaction.MoveFirst();
            gvTransaction.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvTransaction.FocusedColumn = gvTransaction.VisibleColumns[0];
            gvTransaction.ShowEditor();
        }

        private void AssignValues()
        {
            try
            {
                if (TDSBookingFromPartyPayment.Equals(1))
                {
                    FetchBookingId();
                }
                if (VoucherId != 0)
                {
                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                    {
                        voucherSystem.VoucherId = VoucherId;

                        if (this.AppSetting.EnableCashBankJournal == 1)
                            voucherSystem.CashBankModeId = 1;
                        else
                            voucherSystem.CashBankModeId = 0;

                        resultArgs = voucherSystem.FetchJournalDetails();
                        voucherSystem.VoucherId = VoucherId;

                        //26/04/2019, for Vendor GST Invoice details
                        GSTVendorInvoiceNo = (string.IsNullOrEmpty(voucherSystem.GST_VENDOR_INVOICE_NO) ? string.Empty : voucherSystem.GST_VENDOR_INVOICE_NO.Trim());
                        GSTVendorInvoiceDate = voucherSystem.GST_VENDOR_INVOICE_DATE;
                        GSTVendorInvoiceType = voucherSystem.GST_VENDOR_INVOICE_TYPE;
                        GSTVendorId = voucherSystem.GST_VENDOR_ID;

                        //04/09/2024, To set currency details
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            //glkpCurrencyCountry.Properties.ForceInitialize();

                            glkpCurrencyCountry.EditValue = voucherSystem.CurrencyCountryId;
                            object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(voucherSystem.CurrencyCountryId);
                            if (findcountry == null) glkpCurrencyCountry.EditValue = null;

                            txtCurrencyAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(voucherSystem.ContributionAmount.ToString())).ToString();
                            lblCalculatedAmt.Text = voucherSystem.CalculatedAmount.ToString();
                            txtActualAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(voucherSystem.ActualAmount.ToString())).ToString();
                            ShowCurrencyDetails();
                            txtExchangeRate.Text = voucherSystem.ExchangeRate.ToString();
                        }

                        //04/11/2022, to get GST vouchers
                        DtGSTInvoiceMasterDetails = voucherSystem.dtGSTInvoiceMasterDetails;
                        DtGSTInvoiceMasterLedgerDetails = voucherSystem.dtGSTInvoiceMasterLedgerDetails;
                        GSTInvoiceId = voucherSystem.GST_INVOICE_ID;

                        AuthorizedStatus = voucherSystem.AuthorizedStatus;

                        DataTable dt = resultArgs.DataSource.Table;
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            //by Alex
                            deDate.DateTime = this.UtilityMember.DateSet.ToDate(dt.Rows[0][voucherSystem.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false);
                            VoucherDefinitionId = this.UtilityMember.NumberSet.ToInteger(dt.Rows[0][voucherSystem.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn.ColumnName].ToString());
                            txtVoucher.Text = dt.Rows[0][voucherSystem.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                            if (TransVoucherMethod == 1) { txtVoucher.Enabled = false; } else { txtVoucher.Enabled = true; }
                            txtJNarration.Text = dt.Rows[0]["JN_NARRATION"].ToString();
                            // Chinna 08/08/2023
                            //  gcTransaction.DataSource = GetCurrentBalance(dt);

                            DataTable dtTrans = GetCurrentBalance(dt);

                            gstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(GST)", "").ToString());
                            cgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(CGST)", "").ToString());
                            sgstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(SGST)", "").ToString());
                            igstCalcAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(IGST)", "").ToString());

                            ApplyGST(dtTrans, null, false);

                            gcTransaction.DataSource = dtTrans.DefaultView.ToTable();
                        }
                        dsCostCentre.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int LedId = this.UtilityMember.NumberSet.ToInteger(dt.Rows[i][voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                            voucherSystem.LedgerId = LedId;
                            voucherSystem.CostCenterTable = i + "LDR" + LedId;
                            resultArgs = voucherSystem.GetCostCentreDetails();
                            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                            {
                                DataTable CostCentreInfo = resultArgs.DataSource.Table;

                                //On 19/02/2019, To make duplication voucher (Ref_01), remove voucher id and make it add mode after getting selected voucher details
                                if (DuplicateVoucher)
                                {
                                    CostCentreInfo.Columns.Remove(voucherSystem.AppSchema.VoucherTransaction.VOUCHER_IDColumn.ColumnName);
                                }

                                CostCentreInfo.TableName = i + "LDR" + LedId;
                                if (CostCentreInfo != null) { dsCostCentre.Tables.Add(CostCentreInfo); }
                            }
                        }
                    }
                }

                //On 09/02/2018, If duplication entry, clear voucher Id and make it add mode (Ref_01)
                if (DuplicateVoucher)
                {
                    VoucherId = GSTInvoiceId = 0;
                }

                //On 20/01/2025 - If GST Invoice is attached, you can't change Currency details 
                if (GSTInvoiceId > 0 || (DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterDetails.Rows.Count > 0))
                {
                    glkpCurrencyCountry.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

            //bool show = ((gstCalcAmount > 0) || (this.AppSetting.IsCountryOtherThanIndia || (this.AppSetting.AllowMultiCurrency == 1 && IsLocalCurrencyVoucher)));
            bool show = false;
            if (gstCalcAmount > 0) show = true;
            else if (this.AppSetting.AllowMultiCurrency == 1)
            {
                show = IsLocalCurrencyVoucher;
            }
            else if (this.AppSetting.IsCountryOtherThanIndia) show = true;

            ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, show);
            //ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, true);
        }

        private void LoadNarrationAutoComplete()
        {
            try
            {
                using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchermastersystem.AutoFetchNarration();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[vouchermastersystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString());
                        }
                        txtJNarration.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtJNarration.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtJNarration.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void Setdefaults()
        {
            DateTime dtProjectFrom;
            DateTime dtProjectTo;
            DateTime dtyearfrom;
            DateTime dtbookbeginfrom;
            DateTime dtYearTo;
            DateTime dtRecentVoucherDate;

            ucCaptionPanel.Caption = ProjectName;

            dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtRecentVoucherDate = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);

            ResultArgs result = FetchProjectDetails();

            DataView dvResult = result.DataSource.Table.DefaultView;
            dvResult.RowFilter = "PROJECT_ID=" + ProjectId;
            DataTable dtResult = dvResult.ToTable();
            dvResult.RowFilter = "";
            if (dtResult.Rows.Count > 0)
            {
                DataRow drProject = dtResult.Rows[0];

                string sProjectFrom = drProject["DATE_STARTED"].ToString();
                string sProjectTo = drProject["DATE_CLOSED"].ToString();

                dtProjectFrom = (!string.IsNullOrEmpty(sProjectFrom)) ? this.UtilityMember.DateSet.ToDate(sProjectFrom, false) : dtyearfrom;

                if (!string.IsNullOrEmpty(sProjectTo))
                {
                    dtProjectTo = this.UtilityMember.DateSet.ToDate(sProjectTo, false);
                    //Added by Carmel Raj M on 20-Oct-2015
                    //Purpose: Setting Project closing date as max value
                    deMaxDate = dtProjectTo;
                }
                else
                {
                    dtProjectTo = dtProjectFrom > dtYearTo ? dtProjectFrom : dtYearTo;
                    //Added by Carmel Raj M on 20-Oct-2015
                    //Setting year to as max value
                    deMaxDate = dtProjectTo;
                }
                if ((dtProjectFrom < dtyearfrom && dtProjectTo < dtyearfrom) || (dtProjectFrom > dtYearTo && dtProjectTo > dtYearTo))
                {
                    deDate.Properties.MinValue = dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                    deDate.Properties.MaxValue = dtYearTo;
                    deDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false);
                    //this.ShowMessageBox("Project Start Date and Closed Date does not fall between the Accounting Period");
                    //this.Close();
                }
                else
                {
                    //To set transaction minimum
                    if (!string.IsNullOrEmpty(sProjectFrom))
                    {
                        if (dtbookbeginfrom > dtyearfrom)
                        {
                            deDate.Properties.MinValue = (dtProjectFrom > dtbookbeginfrom) ? dtProjectFrom : dtbookbeginfrom;
                        }
                        else
                        {
                            deDate.Properties.MinValue = (dtProjectFrom > dtyearfrom) ? dtProjectFrom : dtyearfrom;
                        }
                    }
                    else
                    {
                        deDate.Properties.MinValue = deDate.DateTime = dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                    }

                    //To set transaction maximum
                    if (!string.IsNullOrEmpty(sProjectTo))
                    {
                        deDate.Properties.MaxValue = dtProjectTo < dtYearTo ? dtProjectTo : dtYearTo;
                    }
                    else
                    {
                        deDate.Properties.MaxValue = dtYearTo;
                    }

                    deDate.DateTime = (dtRecentVoucherDate >= deDate.Properties.MinValue && dtRecentVoucherDate <=
                        deDate.Properties.MaxValue) ? dtRecentVoucherDate : deDate.Properties.MinValue;
                    //rdeMaterializedOn.MinValue = dtTransactionDate.DateTime;

                    // 07/02/2025, Chinna
                    rdtMaterilizsed.MinValue = deDate.DateTime;
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
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }
            return resultArgs;
        }

        private void DeleteTransaction()
        {
            int NatureId = 0;
            int GroupId = 0;
            int HasTrans = 0;
            try
            {
                if (gvTransaction.FocusedRowHandle != GridControl.NewItemRowHandle)
                {
                    if (CheckIsTDSLedger().Equals(0))
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvTransaction.DeleteRow(gvTransaction.FocusedRowHandle);
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                        }
                    }
                    else
                    {
                        using (TDSBookingSystem tdsBookingSystem = new TDSBookingSystem())
                        {
                            tdsBookingSystem.ExpenseLedgerId = LedgerId;
                            DataTable dtTdsBooking = tdsBookingSystem.FetchLedgerDetails();
                            if (dtTdsBooking != null && dtTdsBooking.Rows.Count > 0)
                            {
                                GroupId = dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString()) : 0;
                                NatureId = dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString()) : 0;
                            }
                        }

                        if (VoucherId > 0)
                        {
                            if (NatureId.Equals((int)Natures.Expenses))
                            {
                                HasTrans = HasTDSBooking(TDSDefaultLedgers.DirectExpense);
                            }
                            else if (GroupId.Equals((int)TDSDefaultLedgers.DutiesandTaxes))
                            {
                                HasTrans = HasDeduction();
                            }
                            else if (GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors))
                            {
                                HasTrans = HasTDSBooking(TDSDefaultLedgers.SunderyCreditors);
                            }
                            if (HasTrans > 0)
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ASSOCIATION_WITH_TDS));
                            }
                            else
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    gvTransaction.DeleteRow(gvTransaction.FocusedRowHandle);
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                }
                            }
                        }
                        else
                        {
                            if (dtTDSBooking == null)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    gvTransaction.DeleteRow(gvTransaction.FocusedRowHandle);
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.ASSOCIATION_WITH_TDS));
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

        private int HasTDSBooking(TDSDefaultLedgers tdsDeductionInfo)
        {
            using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
            {
                if (tdsDeductionInfo.Equals(TDSDefaultLedgers.DirectExpense))
                {
                    BookingSystem.ExpenseLedgerId = LedgerId;
                }
                else
                {
                    BookingSystem.PartyLedgerId = LedgerId;
                }
                return BookingSystem.HasTDSBooking();
            }
        }

        private int HasDeduction()
        {
            using (TDSDeductionSystem DeductionSystem = new TDSDeductionSystem())
            {
                DeductionSystem.TaxLedgerId = LedgerId;
                return DeductionSystem.HasDeduction();
            }
        }

        private void ShowProjectSelectionWindow()
        {
            frmProjectSelection projectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod);
            projectSelection.ShowDialog();
            if (projectSelection.ProjectName != string.Empty)
            {
                //28/01/2019, to reset voucher definition type when project selection
                if (ProjectId != projectSelection.ProjectId)
                {
                    VoucherDefinitionId = (Int32)DefaultVoucherTypes.Journal;
                }

                ProjectId = projectSelection.ProjectId;
                ProjectName = projectSelection.ProjectName;
                SetLoadDefault();
                Setdefaults();
            }
        }

        private void ShowLedgerForm()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                Modules.Master.frmLedgerDetailAdd frmLedger = new frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, ProjectId);
                frmLedger.ShowDialog();
                //if (frmLedger.DialogResult == DialogResult.OK)
                //{
                SetLoadDefault();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
            //}
        }

        private void ShowLedgerOptionsForm()
        {
            frmLedgerOptions frmledgeroptions = new frmLedgerOptions();
            frmledgeroptions.ShowDialog();
            //if (frmledgeroptions.DialogResult == DialogResult.OK)
            //{
            SetLoadDefault();
            //}
        }

        private void PrintVoucher(int vid)
        {
            if (XtraMessageBox.Show(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.SAVED_PRINT_VOUCHER), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
                    resultArgs = voucherSystem.FetchReportSetting(UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.JOURNALVOUCHER));
                    if (resultArgs != null && resultArgs.Success)
                    {
                        ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                        ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = this.UtilityMember.DateSet.ToDate(deDate.Text, false);
                        report.VoucherPrint(vid.ToString(), UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.JOURNALVOUCHER), ProjectName, ProjectId);
                    }
                    else
                    {
                        this.ShowMessageBoxError(resultArgs.Message);
                    }
                }
            }
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
                        this.AppSetting.UserProjectInfor = resultArgs.DataSource.Table.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void SetFocusColor(TextEdit txtEdit)
        {
            txtEdit.BackColor = Color.Lavender;
        }

        private void RemoveColor(TextEdit txtEdit)
        {
            txtEdit.BackColor = Color.Empty;
        }

        private void ShowCostCentreForm()
        {
            frmCostCentreAdd frmCostCentre = new frmCostCentreAdd((int)AddNewRow.NewRow, ProjectId);
            frmCostCentre.ShowDialog();
            if (frmCostCentre.DialogResult == DialogResult.OK)
            {

            }
        }

        private bool IsMultiNarrationEnabled()
        {
            bool isenabled = false;
            try
            {
                using (VoucherSystem vouchersystem = new VoucherSystem())
                {
                    int Narrval = vouchersystem.CheckNarrationEnabledByType((int)DefaultVoucherTypes.Journal);
                    isenabled = Narrval == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return isenabled;
        }

        public bool CheckCostcentreEnabled(int LedgerId)
        {
            int CostCentre = 0;
            bool IsExists = false;
            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
            {
                voucherSystem.LedgerId = LedgerId;
                resultArgs = voucherSystem.FetchCostCentreLedger();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    CostCentre = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                    IsExists = CostCentre > 0 ? true : false;
                }
            }
            return IsExists;
        }

        private void ShowVendorGSTInvoiceDetails()
        {
            bool showvendor = false;
            string alertmessage = "GST Amount is not available";
            if (CanShowVendorGST)
            {
                DataTable dtVoucherJournalTrans = gcTransaction.DataSource as DataTable;
                if (dtVoucherJournalTrans != null)
                {

                    using (VoucherTransactionSystem sysvoucher = new VoucherTransactionSystem())
                    {
                        DataTable dtVoucherTrans = dtVoucherJournalTrans.DefaultView.ToTable();
                        /*dtVoucherTrans.DefaultView.RowFilter = sysvoucher.AppSchema.VoucherTransaction.GSTColumn.ColumnName + " > 0";
                        dtVoucherTrans = dtVoucherTrans.DefaultView.ToTable();
                        */
                        double VoucherInvoiceAmount = (IsGeneralInvolice ? SummaryDebit : 0); //For multicurrneyc
                        double cgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.CGSTColumn.ColumnName + ")", "").ToString());
                        double sgst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.SGSTColumn.ColumnName + ")", "").ToString());
                        double igst = this.UtilityMember.NumberSet.ToDouble(dtVoucherTrans.Compute("SUM(" + sysvoucher.AppSchema.VoucherTransaction.IGSTColumn.ColumnName + ")", "").ToString());

                        //if (cgst > 0 || sgst > 0 || igst > 0) //On 20/10/2023 Allow GST Invoice always without GST Amount
                        //{
                        ResultArgs result = sysvoucher.AttachVoucherLedgetsToGSTInvoiceLedgerDetails(true, DefaultVoucherTypes.Journal.ToString(), DtGSTInvoiceMasterLedgerDetails, dtVoucherTrans);
                        if (result.Success)
                        {
                            string lname = GetInvoiceLedgerName(); //For multicurrneyc
                            //AttachGSTInvoiceLedgerDetails();
                            DtGSTInvoiceMasterLedgerDetails = result.DataSource.Table;
                            DataTable DtGSTInvoiceMasterLedgerDetailsPrevious = result.DataSource.Table.DefaultView.ToTable();
                            showvendor = true;
                            DateTime transdate = this.UtilityMember.DateSet.ToDate(deDate.Text, false);
                            frmVoucherVendorGSTInvoiceDetails frmVendorGST = new frmVoucherVendorGSTInvoiceDetails(DefaultVoucherTypes.Journal, ProjectId, GSTInvoiceId, DtGSTInvoiceMasterDetails, DtGSTInvoiceMasterLedgerDetails, VoucherId,
                                                    transdate, GSTVendorId, GSTVendorInvoiceNo, GSTVendorInvoiceType, GSTVendorInvoiceDate, (cgst + sgst + igst), cgst, sgst, igst, txtJNarration.Text, SummaryCredit, lname);
                            frmVendorGST.DtGSTInvoiceMasterDetails = DtGSTInvoiceMasterDetails;
                            frmVendorGST.ShowDialog();
                            if (frmVendorGST.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                GSTVendorInvoiceNo = frmVendorGST.InvoiceNo.Trim();
                                GSTVendorInvoiceDate = frmVendorGST.InvoiceDate;
                                GSTVendorInvoiceType = frmVendorGST.InvoiceType;
                                GSTVendorId = frmVendorGST.VendorId;
                                GSTInvoiceId = frmVendorGST.GSTInvoiceId;
                                DtGSTInvoiceMasterDetails = frmVendorGST.DtGSTInvoiceMasterDetails;
                                ShowAdditionButtons(AdditionButttons.VendorGSTInvoiceDetails, true);
                            }
                            else
                            {
                                DtGSTInvoiceMasterLedgerDetails = DtGSTInvoiceMasterLedgerDetailsPrevious;
                                //Temp 03/11/2022
                                /*GSTVendorInvoiceNo = string.Empty;
                                GSTVendorInvoiceDate = string.Empty;
                                GSTVendorInvoiceType = 0;
                                GSTVendorId = 0;
                                GSTInvoiceId = 0;*/
                            }
                        }
                        else
                        {
                            alertmessage = result.Message;
                        }
                        //}
                    }
                }
            }

            if (!showvendor)
            {
                MessageRender.ShowMessage(alertmessage);
            }
        }
        #endregion

        #region TDS Booking
        private void ShowTDSBookingForm(double ExpenseAmount, TransactionMode transMode)
        {
            DataView dvTDSBooking = new DataView();
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.LedgerId = LedgerId;
                    resultArgs = ledgerSystem.FetchTDSLedgers();

                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        string LedgerName = resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                        IsTDSLedger = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString());

                        int NatureId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString());
                        int GroupId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName].ToString());
                        int IdentificationId = gvTransaction.GetFocusedRowCellValue(colIdentification) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colIdentification).ToString()) : 0;

                        if (IsTDSLedger > 0 && !string.IsNullOrEmpty(LedgerName) && (NatureId.Equals((int)Natures.Libilities)))
                        {
                            int RowIndex = this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetDataSourceRowIndex(gvTransaction.FocusedRowHandle).ToString());
                            if (dsTDSBooking.Tables.Contains(RowIndex + "TDS" + LedgerId + "Booking"))
                            {
                                dvTDSBooking = dsTDSBooking.Tables[RowIndex + "TDS" + LedgerId + "Booking"].DefaultView;
                            }
                            ACPP.Modules.TDS.frmTDSTransaction frmTDSBooking = new TDS.frmTDSTransaction(ProjectId, LedgerId, ExpenseAmount, LedgerName, deDate.DateTime);
                            if (dvTDSBooking != null && dvTDSBooking.Count > 0)
                            {
                                frmTDSBooking.dtTDSBookingDetail = dvTDSBooking.ToTable();
                            }
                            frmTDSBooking.BookingId = BookingId;
                            frmTDSBooking.VoucherId = VoucherId;
                            frmTDSBooking.DefaultNatureofPaymentId = NatureofPaymentId;
                            frmTDSBooking.DeducteeTypeId = DeducteeTypeLedgerId;

                            frmTDSBooking.PartyLedgerId = LedgerId;
                            frmTDSBooking.ExpAmount = ExpenseLedgerAmount;
                            frmTDSBooking.isTaxDeductable = isTaxDeductable;
                            frmTDSBooking.isTaxDeductedAlready = IsAlreadyDeducted;
                            frmTDSBooking.TDSAmountValidation = TDSAmountValidation;
                            frmTDSBooking.ShowDialog();
                            if (frmTDSBooking.DialogResult.Equals(DialogResult.OK))
                            {
                                dtPartyTrans = frmTDSBooking.dtPartyTrans;
                                dvTransUcSummary = frmTDSBooking.dvTransUcSummary;
                                TDSPartyNarration = frmTDSBooking.TDSPartyNarration;
                                TDSAmountValidation = frmTDSBooking.TDSAmountValidation;
                                if (VoucherId > 0)
                                {
                                    ExpenseLedgerId = frmTDSBooking.ExpenseLedgerId;
                                }
                                dtTDSBooking = frmTDSBooking.dtTDSBookingDetail;
                                if (dtTDSBooking != null)
                                {
                                    dtTDSBooking.TableName = RowIndex + "TDS" + LedgerId + "Booking";
                                    if (dsTDSBooking.Tables.Contains(dtTDSBooking.TableName))
                                    {
                                        dtRemoveTDSTransSummary = gcTransaction.DataSource as DataTable;
                                        dsTDSBooking.Tables.Remove(dtTDSBooking.TableName);
                                    }
                                    else
                                    {
                                        if (VoucherId > 0 && BookingId > 0)
                                        {
                                            dtRemoveTDSTransSummary = gcTransaction.DataSource as DataTable;
                                        }
                                    }
                                    // changed by Mic - chinna
                                    dsTDSBooking.Tables.Add(dtTDSBooking);
                                    RemoveTDSEntry();
                                    isDeductTDS = false;
                                    AddNewRowSource(TDSDefaultLedgers.SunderyCreditors);
                                    gvTransaction.MoveLast();
                                }
                                txtJNarration.Focus();
                                txtJNarration.Select();
                            }
                            else
                            {
                                ExpenseAmount = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void AddNewRowSource(TDSDefaultLedgers tdsDefaultLedgers)
        {
            try
            {
                if (tdsDefaultLedgers.Equals(TDSDefaultLedgers.SunderyCreditors))
                {

                    if (dtTDSBooking != null && dtTDSBooking.Rows.Count > 0)
                    {
                        int FocusedLedgerId = gvTransaction.GetFocusedRowCellValue(colLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colLedger).ToString()) : 0;
                        double FocusedAmount = gvTransaction.GetFocusedRowCellValue(colCredit) != null ? this.UtilityMember.NumberSet.ToDouble(gvTransaction.GetFocusedRowCellValue(colCredit).ToString()) : 0;
                        double PartyAmount = this.UtilityMember.NumberSet.ToDouble(dtTDSBooking.Compute("SUM(PARTY_AMOUNT)", "").ToString());
                        if (this.TDSBookingFromPartyPayment.Equals(1))
                        {
                            this.TDSPartyLedgerId = PartyLedgerId;
                            this.PartyLedgerAmount = PartyAmount;
                        }
                        if (VoucherId > 0 && BookingId > 0)
                        {
                            isTDSEnableFlag = true;
                            gvTransaction.DeleteRow(LastFocusedRowHandle);
                            gvTransaction.AddNewRow();
                            int PartyLedgerId = this.UtilityMember.NumberSet.ToInteger(dtTDSBooking.Rows[0]["PARTY_LEDGER_ID"].ToString());
                            gvTransaction.SetFocusedRowCellValue(colLedger, PartyLedgerId);
                            gvTransaction.SetFocusedRowCellValue(colCredit, PartyAmount);
                            ShowCostCentre(PartyAmount, PartyLedgerId);
                            gvTransaction.SetFocusedRowCellValue(colIdentification, 2);
                            CalculateLedgerBalance(PartyLedgerId, PartyAmount);
                        }
                        else
                        {
                            isTDSEnableFlag = true;
                            gvTransaction.SetRowCellValue(LastFocusedRowHandle, colCredit, PartyAmount);
                            gvTransaction.SetRowCellValue(LastFocusedRowHandle, colIdentification, (int)YesNo.No);
                            CalculateLedgerBalance(PartyLedgerId, PartyAmount);
                            ShowCostCentre(PartyAmount, PartyLedgerId);
                        }
                        foreach (DataRow dr in dtTDSBooking.Rows)
                        {
                            TaxLedgerId = dr["LEDGER_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString()) : 0;
                            double TaxAmount = dr["TDS_AMOUNT"] != DBNull.Value ? this.UtilityMember.NumberSet.ToDouble(dr["TDS_AMOUNT"].ToString()) : 0;
                            if (TaxLedgerId > 0 && TaxAmount > 0)
                            {
                                gvTransaction.AddNewRow();
                                gvTransaction.SetFocusedRowCellValue(colLedger, TaxLedgerId);
                                gvTransaction.SetFocusedRowCellValue(colCredit, TaxAmount);
                                gvTransaction.SetRowCellValue(gvTransaction.FocusedRowHandle, colIdentification, (int)YesNo.Yes);
                                CalculateLedgerBalance(TaxLedgerId, TaxAmount);
                                ShowCostCentre(TaxAmount, TaxLedgerId);
                            }
                        }
                    }
                    gvTransaction.MoveLast();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void RemoveTDSEntry()
        {
            try
            {
                if (dtRemoveTDSTransSummary != null && dtRemoveTDSTransSummary.Rows.Count > 0)
                {
                    DataView dvFilterRow = dtRemoveTDSTransSummary.DefaultView;
                    dvFilterRow.RowFilter = "VALUE IN(" + (int)YesNo.No + ")";
                    gcTransaction.DataSource = dvFilterRow.ToTable();
                    gcTransaction.RefreshDataSource();
                    dvFilterRow.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void CalculateLedgerBalance(int LedgerId, double Amount)
        {
            try
            {
                string Balance = string.Empty;
                //  string Balance = GetLedgerBalanceValues(gcTransaction.DataSource as DataTable, LedgerId);
                BalanceProperty bpiBalance = FetchCurrentBalance(LedgerId);
                if (VoucherId == 0)
                {
                    if (bpiBalance.Amount > 0 && bpiBalance.TransMode.Equals(TransactionMode.DR.ToString()))
                    {
                        Balance = this.UtilityMember.NumberSet.ToCurrency((bpiBalance.Amount + Amount)) + " " + bpiBalance.TransMode;
                    }
                    else if (bpiBalance.Amount > 0 && bpiBalance.TransMode.Equals(TransactionMode.CR.ToString()))
                    {
                        Balance = this.UtilityMember.NumberSet.ToCurrency((bpiBalance.Amount + Amount)) + " " + bpiBalance.TransMode;
                    }
                    else if (bpiBalance.Amount == 0 && bpiBalance.TransMode.Equals(TransactionMode.DR.ToString()))
                    {
                        Balance = this.UtilityMember.NumberSet.ToCurrency((bpiBalance.Amount + Amount)) + " " + TransactionMode.CR.ToString();
                    }
                    else if (bpiBalance.Amount == 0 && bpiBalance.TransMode.Equals(TransactionMode.CR.ToString()))
                    {

                    }
                    else if (bpiBalance.Amount == 0 && bpiBalance.TransMode.Equals(string.Empty))
                    {
                        Balance = this.UtilityMember.NumberSet.ToCurrency((bpiBalance.Amount + Amount)) + " " + TransactionMode.CR.ToString();
                    }
                    if (Balance != string.Empty)
                    {
                        gvTransaction.SetFocusedRowCellValue(colLedgerBal, Balance);
                    }
                }
                else
                {
                    if (bpiBalance.Amount > 0 && bpiBalance.TransMode.Equals(TransactionMode.DR.ToString()))
                    {
                        Balance = this.UtilityMember.NumberSet.ToCurrency((bpiBalance.Amount)) + " " + bpiBalance.TransMode;
                    }
                    else if (bpiBalance.Amount > 0 && bpiBalance.TransMode.Equals(TransactionMode.CR.ToString()))
                    {
                        Balance = this.UtilityMember.NumberSet.ToCurrency((bpiBalance.Amount)) + " " + bpiBalance.TransMode;
                    }
                    else if (bpiBalance.Amount == 0 && bpiBalance.TransMode.Equals(TransactionMode.DR.ToString()))
                    {
                        Balance = this.UtilityMember.NumberSet.ToCurrency((bpiBalance.Amount)) + " " + TransactionMode.CR.ToString();
                    }
                    else if (bpiBalance.Amount == 0 && bpiBalance.TransMode.Equals(TransactionMode.CR.ToString()))
                    {

                    }
                    else if (bpiBalance.Amount == 0 && bpiBalance.TransMode.Equals(string.Empty))
                    {
                        Balance = this.UtilityMember.NumberSet.ToCurrency((bpiBalance.Amount)) + " " + TransactionMode.CR.ToString();
                    }
                    if (Balance != string.Empty)
                    {
                        gvTransaction.SetFocusedRowCellValue(colLedgerBal, Balance);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void IsTaxDeductable(TransactionMode transMode, double Amount, bool isTaxDeductable = false)
        {
            try
            {
                if (this.AppSetting.TDSEnabled.Equals("1"))
                {
                    using (Bosco.Model.TDS.TDSBookingSystem BookingSystem = new Bosco.Model.TDS.TDSBookingSystem())
                    {
                        BookingSystem.ExpenseLedgerId = LedgerId;
                        DataTable dtTDSLedger = BookingSystem.FetchLedgerDetails();
                        if (dtTDSLedger != null && dtTDSLedger.Rows.Count > 0)
                        {
                            int GroupId = dtTDSLedger.Rows[0][BookingSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTDSLedger.Rows[0][BookingSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString()) : 0;
                            int NatureId = dtTDSLedger.Rows[0][BookingSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTDSLedger.Rows[0][BookingSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString()) : 0;
                            //if (transMode.Equals(TransactionMode.DR) && ((GroupId.Equals((int)TDSDefaultLedgers.DirectExpense)) || NatureId.Equals((int)Natures.Expenses)))
                            if (transMode.Equals(TransactionMode.DR))
                            {
                                PartyIsTDSLedger = 0;
                                NatureofPaymentId = dtTDSLedger.Rows[0][BookingSystem.AppSchema.LedgerProfileData.NATURE_OF_PAYMENT_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTDSLedger.Rows[0][BookingSystem.AppSchema.LedgerProfileData.NATURE_OF_PAYMENT_IDColumn.ColumnName].ToString()) : 0;
                            }
                            else if (transMode.Equals(TransactionMode.CR) && GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors))
                            {
                                DeducteeTypeLedgerId = dtTDSLedger.Rows[0][BookingSystem.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTDSLedger.Rows[0][BookingSystem.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn.ColumnName].ToString()) : 0;
                            }

                            int isTDSLedger = dtTDSLedger.Rows[0][BookingSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTDSLedger.Rows[0][BookingSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString()) : 0;

                            int Identification = gvTransaction.GetFocusedRowCellValue(colIdentification) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colIdentification).ToString()) : 0;
                            //if (isTDSLedger > 0 && (GroupId.Equals((int)TDSDefaultLedgers.DirectExpense) || NatureId.Equals((int)Natures.Expenses)))
                            if (isTDSLedger > 0 && !GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors) && !GroupId.Equals((int)TDSDefaultLedgers.DutiesandTaxes))
                            {
                                ExpenseIsTDSLedger++;
                                ExpenseLedgerId = LedgerId;
                            }
                            else if (isTDSLedger > 0 && GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors))
                            {
                                PartyLedgerId = LedgerId;
                                PartyIsTDSLedger++;
                            }
                            if (ExpenseIsTDSLedger > 0 && PartyIsTDSLedger > 0)
                            {
                                CheckTaxExemptionLimit(Amount);
                                LastFocusedRowHandle = gvTransaction.FocusedRowHandle;
                                ShowTDSBookingForm(Amount, TransactionMode.CR);
                                isDeductTDS = false;
                            }
                            else if (VoucherId > 0 && BookingId > 0 && isTaxDeductable)
                            {
                                isTaxDeductable = true;
                                LastFocusedRowHandle = gvTransaction.FocusedRowHandle;
                                ShowTDSBookingForm(Amount, TransactionMode.CR);
                                isDeductTDS = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private bool CheckTaxExemptionLimit(double Amount)
        {
            bool isValid = false;
            using (DeducteeTaxSystem taxPolicy = new DeducteeTaxSystem())
            {
                taxPolicy.PartyLedgerId = LedgerId;
                taxPolicy.NaturePaymentId = NatureofPaymentId;
                taxPolicy.DeducteeTypeId = DeducteeTypeLedgerId;
                taxPolicy.ApplicableFrom = deDate.DateTime;

                resultArgs = taxPolicy.FetchTaxPolicy();
                if (resultArgs != null && resultArgs.Success)
                {
                    Amount = getExpenseLedgerBalance(Amount, ExpenseLedgerId);
                    if (HasPANNumber() > 0)
                    {
                        if (resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            double TDSRate = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][taxPolicy.AppSchema.DutyTax.TDS_RATEColumn.ColumnName].ToString());
                            ExemptionLimit = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][taxPolicy.AppSchema.DutyTax.TDS_EXEMPTION_LIMITColumn.ColumnName].ToString());
                            if (ExemptionLimit > 0)
                            {
                                if (Amount < ExemptionLimit)
                                {
                                    ExemptionLimitAmount = Amount;
                                    isTaxDeductable = false;
                                    isValid = true;
                                }
                                else
                                {
                                    ExemptionLimitAmount = Amount;
                                    isTaxDeductable = true;
                                }
                            }
                            else
                            {
                                if (TDSRate == 0)
                                    isValid = true;
                            }
                        }
                        else
                        {
                            isValid = true;
                        }
                    }
                    else
                    {
                        ExemptionLimitAmount = Amount;
                        isTaxDeductable = true;
                    }
                }
            }
            return isValid;
        }

        private int FetchGroupIdByLedgerId()
        {
            int GroupId = 0;
            using (LedgerSystem LedSystem = new LedgerSystem())
            {
                LedSystem.LedgerId = LedgerId;
                resultArgs = LedSystem.FetchTDSLedgers();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    GroupId = resultArgs.DataSource.Table.Rows[0][LedSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][LedSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName].ToString()) : 0;
                }
            }
            return GroupId;
        }

        private int CheckIsTDSLedger()
        {
            using (LedgerSystem LedSystem = new LedgerSystem())
            {
                LedSystem.LedgerId = LedgerId;
                IsTDSLedger = LedSystem.IsTDSLedgerExits();
            }
            return IsTDSLedger;
        }

        private int HasPANNumber()
        {
            int HasPanNumber = 0;
            try
            {
                using (LedgerProfileSystem ledgerProfileSystem = new LedgerProfileSystem())
                {
                    ledgerProfileSystem.LedgerID = PartyLedgerId;
                    HasPanNumber = ledgerProfileSystem.HasPanNumber();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return HasPanNumber;
        }

        private void CheckIsTDSPartyLedger()
        {
            int GroupId = 0;
            int isTDSLedger = 0;
            int NatureId = 0;
            try
            {
                if (gvTransaction.FocusedColumn == colLedger)
                {
                    int LedId = gvTransaction.GetFocusedRowCellValue(colLedger) != null ? this.UtilityMember.NumberSet.ToInteger(gvTransaction.GetFocusedRowCellValue(colLedger).ToString()) : 0;
                    if (LedId > 0)
                    {
                        using (TDSBookingSystem tdsBookingSystem = new TDSBookingSystem())
                        {
                            tdsBookingSystem.ExpenseLedgerId = LedId;
                            DataTable dtTdsBooking = tdsBookingSystem.FetchLedgerDetails();
                            if (dtTdsBooking != null && dtTdsBooking.Rows.Count > 0)
                            {
                                GroupId = dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName].ToString()) : 0;
                                NatureId = dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString()) : 0;
                                isTDSLedger = dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtTdsBooking.Rows[0][tdsBookingSystem.AppSchema.Ledger.IS_TDS_LEDGERColumn.ColumnName].ToString()) : 0;
                            }
                        }
                        if (isTDSLedger > 0 && GroupId.Equals((int)TDSDefaultLedgers.SunderyCreditors))
                        {
                            LastFocusedRowHandle = gvTransaction.FocusedRowHandle;
                            IsTaxDeductable(TransactionMode.CR, ExpenseLedgerAmount, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        public int FetchBookingId()
        {
            if (VoucherId > 0)
            {
                using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
                {
                    BookingSystem.VoucherId = VoucherId;
                    BookingId = BookingSystem.FetchBookingId();
                }
            }
            return BookingId;
        }

        private double getExpenseLedgerBalance(double Amount, int ExpLedgerId)
        {
            double ExpenseLedgerAmount = 0;
            BalanceProperty balProperty = FetchCurrentBalance(ExpLedgerId);
            double ExpenseBookingAmount = GetExpenseAmount();
            if (balProperty.TransMode.Equals(TransactionMode.DR.ToString()))
            {
                if (IsAlreadyDeducted == 0)
                    ExpenseLedgerAmount = balProperty.Amount + Amount;
                else
                    ExpenseLedgerAmount = balProperty.Amount + Amount;
            }
            else if (balProperty.TransMode.Equals(TransactionMode.CR.ToString()))
            {
                if (balProperty.Amount >= ExpenseBookingAmount)
                {
                    ExpenseLedgerAmount = balProperty.Amount - (ExpenseBookingAmount + Amount);
                }
                else if (ExpenseBookingAmount >= balProperty.Amount)
                {
                    ExpenseLedgerAmount = ExpenseBookingAmount - (balProperty.Amount + Amount);
                }
                else
                {
                    ExpenseLedgerAmount = balProperty.Amount > Amount ? balProperty.Amount - Amount : Amount - balProperty.Amount;
                }
            }
            else if (balProperty.TransMode.Equals(string.Empty) && balProperty.Amount.Equals(0))
            {
                ExpenseLedgerAmount = balProperty.Amount + Amount;
            }
            return ExpenseLedgerAmount;
        }

        private double GetExpenseAmount()
        {
            double ExpAmount = 0;
            using (TDSBookingSystem BookingSystem = new TDSBookingSystem())
            {
                BookingSystem.ProjectId = ProjectId;
                BookingSystem.ExpenseLedgerId = ExpenseLedgerId;
                ExpAmount = BookingSystem.getExpenseAmount();
                IsAlreadyDeducted = BookingSystem.isDeductedAlready;
            }
            return ExpAmount;
        }

        private string GetLedgerName(int PartyLedgerId)
        {
            string LedgerName = string.Empty;
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                LedgerName = ledgerSystem.GetLegerName(PartyLedgerId);
            }
            return LedgerName;
        }
        #endregion

        #region TDS Events

        private void gvTransaction_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (isDeductTDS)
            {
                CheckIsTDSPartyLedger();
            }
        }

        private void gvTransaction_ColumnChanged(object sender, EventArgs e)
        {
            if (isDeductTDS)
            {
                CheckIsTDSPartyLedger();
            }
        }

        private void rglkpLedger_EditValueChanged(object sender, EventArgs e)
        {
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}"); isMouseClicked = false;
            }
        }

        private void rglkpLedger_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        #endregion

        #region Lock Transaction Methods

        private void FetchDateDuration()
        {
            try
            {

                resultArgs = base.GetAuditVoucherLockedDetails(this.ProjectId, deDate.DateTime);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    using (AuditLockTransSystem AuditSystem = new AuditLockTransSystem())
                    {
                        DataTable dtAuditLockDetails = resultArgs.DataSource.Table;
                        dtLockDateFrom = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_FROMColumn.ColumnName].ToString(), false);
                        dtLockDateTo = this.UtilityMember.DateSet.ToDate(dtAuditLockDetails.Rows[0][AuditSystem.AppSchema.AuditLockTransType.DATE_TOColumn.ColumnName].ToString(), false);
                    }
                }
                else
                {
                    //On 07/02/2024, For SDBINM, Lock Voucehrs before grace period
                    if (this.AppSetting.IS_SDB_INM && this.AppSetting.VoucherGraceDays > 0)
                    {

                        dtLockDateFrom = this.AppSetting.GraceLockDateFrom;
                        dtLockDateTo = this.AppSetting.GraceLockDateTo;
                    }
                    else
                    {
                        dtLockDateFrom = dtLockDateTo = DateTime.MinValue;
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private bool IsLockedTransaction(DateTime dtVoucherDate)
        {
            bool isSuccess = false;
            try
            {
                //Check temporary relaxation
                bool isEnforceTmpRelaxation = this.AppSetting.IsTemporaryGraceLockRelaxDate(dtVoucherDate);

                if (dtLockDateFrom != DateTime.MinValue && dtLockDateTo != DateTime.MinValue)
                {
                    if ((dtVoucherDate >= dtLockDateFrom && dtVoucherDate <= dtLockDateTo) && !isEnforceTmpRelaxation)
                    {
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return isSuccess;
        }

        /// <summary>
        /// On 20/11/2018, 
        /// </summary>
        /// <returns></returns>
        private bool ValidateCCAmoutWithLedgerAmount()
        {
            bool Rtn = true;

            try
            {
                DataTable dtTransaction = gcTransaction.DataSource as DataTable;
                if (dsCostCentre != null && dsCostCentre.Tables.Count > 0)
                {
                    if (dtTransaction != null && dtTransaction.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTransaction.Rows)
                        {
                            int rownumber = dtTransaction.Rows.IndexOf(dr);
                            Int32 ledgerid = UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                            //1. Take leger details in transaction grid
                            if (ledgerid > 0)
                            {
                                string ledgername = GetLedgerName(ledgerid);
                                double ledgeramount = UtilityMember.NumberSet.ToDouble(dr["CREDIT"].ToString());
                                if (ledgeramount == 0) ledgeramount = UtilityMember.NumberSet.ToDouble(dr["DEBIT"].ToString());
                                string CCTableName = rownumber + "LDR" + ledgerid;

                                //2. Compare ledger amount with sum of cc amount for given ledger
                                if (dsCostCentre.Tables.Contains(CCTableName))
                                {
                                    DataTable dtLedgerCC = dsCostCentre.Tables[CCTableName];
                                    if (dtLedgerCC != null && dtLedgerCC.Rows.Count > 0)
                                    {
                                        dtLedgerCC.DefaultView.RowFilter = "COST_CENTRE_ID >0";
                                        if (dtLedgerCC.DefaultView.Count > 0)
                                        {
                                            dtLedgerCC.DefaultView.RowFilter = "";
                                            double CCAmount = UtilityMember.NumberSet.ToDouble(dtLedgerCC.Compute("SUM(AMOUNT)", string.Empty).ToString());

                                            // this is to enable add the Cost Centre Amount 12.07.2019
                                            //double CCamountGSt = CCAmount - ledgeramount;
                                            //On 19/01/2022, To allocate cc amount with GST or withour GST based on Finnace setting
                                            double ledgerGSTamount = 0;
                                            if (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.EnableGST).Equals((int)YesNo.Yes))
                                            {
                                                if (deDate.DateTime >= this.AppSetting.GSTStartDate)
                                                {
                                                    if (AppSetting.AllocateCCAmountWithGST == 1 && dtTransaction.Columns.Contains("GST"))
                                                    {
                                                        ledgerGSTamount = UtilityMember.NumberSet.ToDouble(dr["GST"].ToString()); ;
                                                    }
                                                }
                                            }
                                            double CCamountGSt = CCAmount - (ledgeramount + ledgerGSTamount);


                                            //On 09/11/2021, To check cc amount mis matching
                                            //if (CCAmount != (ledgeramount + CCamountGSt))
                                            if (CCamountGSt != 0)
                                            {
                                                MessageRender.ShowMessage("'" + ledgername + "' ledger amount is mismatching with Cost Centre allocation amount, Check Cost Centre allocation details.");
                                                Rtn = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                Rtn = false;
                MessageRender.ShowMessage("Could not check Cost center amount with ledger amount, " + err.Message);
            }

            return Rtn;
        }

        /// <summary>
        /// On 13/12/2022, To check GST Invoice Master and GST Master Ledger details
        /// </summary>
        /// <returns></returns>
        private bool ValidateGSTInvoiceDetails()
        {
            bool Rtn = true;
            string msg = string.Empty;
            try
            {
                DataTable dtTransaction = gcTransaction.DataSource as DataTable;
                using (VoucherTransactionSystem sysvoucher = new VoucherTransactionSystem())
                {
                    if (!string.IsNullOrEmpty(GSTVendorInvoiceNo))
                    {
                        if (DtGSTInvoiceMasterLedgerDetails == null) DtGSTInvoiceMasterLedgerDetails = sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.DefaultView.ToTable();
                        DataTable dtGSTLegersTrans = dtTransaction.DefaultView.ToTable();

                        if (!IsGeneralInvolice)
                        {
                            //On 20/10/2023 Allow GST Invoice always without GST Amount
                            //dtGSTLegersTrans.DefaultView.RowFilter = sysvoucher.AppSchema.VoucherTransaction.CGSTColumn.ColumnName + " > 0";
                            string filter = sysvoucher.AppSchema.VoucherTransaction.LEDGER_GST_CLASS_IDColumn.ColumnName + " > 0 AND " +
                                                                    sysvoucher.AppSchema.Ledger.IS_GST_LEDGERSColumn.ColumnName + " = 1";

                            dtGSTLegersTrans.DefaultView.RowFilter = filter;
                            dtGSTLegersTrans = dtGSTLegersTrans.DefaultView.ToTable();

                            dtGSTLegersTrans = dtGSTLegersTrans.DefaultView.ToTable(true, new string[] { "LEDGER_ID", "LEDGER_GST_CLASS_ID" });
                            if (DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterLedgerDetails.Rows.Count > 0 && !string.IsNullOrEmpty(GSTVendorInvoiceNo))
                            {
                                foreach (DataRow dr in dtGSTLegersTrans.Rows)
                                {
                                    int rownumber = dtTransaction.Rows.IndexOf(dr);
                                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(dr[sysvoucher.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                                    string ledgername = GetLedgerName(ledgerid);
                                    DtGSTInvoiceMasterLedgerDetails.DefaultView.RowFilter = sysvoucher.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " = " + ledgerid;
                                    if (DtGSTInvoiceMasterLedgerDetails.DefaultView.Count == 0)
                                    {
                                        msg = "'" + ledgername + "' is not available in the GST Invoice details, Click \"Attach Vendor GST Invoice\" and Check details.";
                                        Rtn = false;
                                        break;
                                    }
                                }
                            }
                        }

                        if (lcVendor.Visibility == LayoutVisibility.Always)
                        {
                            DateTime invoicedate = DateTime.MinValue;
                            DateTime invoiceduedate = DateTime.MinValue;
                            if (DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterDetails.Rows.Count > 0)
                            {
                                //On 21/01/2025 - for journal invoice - let us have voucher date as invoice date -----------------------------------------
                                DtGSTInvoiceMasterDetails.Rows[0].BeginEdit();
                                DtGSTInvoiceMasterDetails.Rows[0][sysvoucher.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName] = deDate.DateTime;
                                GSTVendorInvoiceDate = deDate.DateTime.ToShortDateString();
                                DtGSTInvoiceMasterDetails.Rows[0].EndEdit();
                                DtGSTInvoiceMasterDetails.AcceptChanges();
                                //---------------------------------------------------------------------------------------------------------------------------

                                invoicedate = UtilityMember.DateSet.ToDate(DtGSTInvoiceMasterDetails.Rows[0][sysvoucher.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName].ToString(), false);
                                if (!string.IsNullOrEmpty(DtGSTInvoiceMasterDetails.Rows[0][sysvoucher.AppSchema.GSTInvoiceMaster.DUE_DATEColumn.ColumnName].ToString()))
                                    invoiceduedate = UtilityMember.DateSet.ToDate(DtGSTInvoiceMasterDetails.Rows[0][sysvoucher.AppSchema.GSTInvoiceMaster.DUE_DATEColumn.ColumnName].ToString(), false);

                                if (invoicedate > deDate.DateTime)
                                {
                                    Rtn = false;
                                    MessageRender.ShowMessage("Invoice Date must be less than or equal to Voucher Date");
                                    deDate.Focus();
                                }
                                else if (IsGeneralInvolice && invoiceduedate != DateTime.MinValue && invoiceduedate < deDate.DateTime)
                                {
                                    Rtn = false;
                                    MessageRender.ShowMessage("Invoice Due Date must be greater than or equal to Voucher Date");
                                    deDate.Focus();
                                }
                            }

                            if (Rtn && !IsGeneralInvolice)
                            {
                                if (Rtn && DtGSTInvoiceMasterDetails == null && dtGSTLegersTrans.Rows.Count > 0)
                                {
                                    msg = "Ledger(s) are mismatching in the GST Invoice details, Click on \"Attach Vendor GST Invoice\"/ \"Remove Vendor GST Invoice\" and Check details.";
                                    Rtn = false;
                                }
                                if (Rtn && DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterLedgerDetails.Rows.Count != dtGSTLegersTrans.Rows.Count)
                                {
                                    //If Voucher GST Ledger is removed from Voucher grid list
                                    msg = "Ledger(s) are mismatching in the GST Invoice details, Click on \"Attach Vendor GST Invoice\"/ \"Remove Vendor GST Invoice\" and Check details.";
                                    Rtn = false;
                                }
                                else if (Rtn && DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterLedgerDetails.Rows.Count > 0)
                                {
                                    //On 30/10/2023, Check GST mismatching amount vouhcer trans and gst invoice
                                    string fitlergstamtinvoice = "SUM(" + sysvoucher.AppSchema.GSTInvoiceMaster.TOTAL_CGST_AMOUNTColumn.ColumnName + ")";
                                    double cgstamtinvoice = UtilityMember.NumberSet.ToDouble(DtGSTInvoiceMasterDetails.Compute(fitlergstamtinvoice, string.Empty).ToString());
                                    fitlergstamtinvoice = "SUM(" + sysvoucher.AppSchema.GSTInvoiceMaster.TOTAL_SGST_AMOUNTColumn.ColumnName + ")";
                                    double sgstamtinvoice = UtilityMember.NumberSet.ToDouble(DtGSTInvoiceMasterDetails.Compute(fitlergstamtinvoice, string.Empty).ToString());
                                    fitlergstamtinvoice = "SUM(" + sysvoucher.AppSchema.GSTInvoiceMaster.TOTAL_IGST_AMOUNTColumn.ColumnName + ")";
                                    double igstamtinvoice = UtilityMember.NumberSet.ToDouble(DtGSTInvoiceMasterDetails.Compute(fitlergstamtinvoice, string.Empty).ToString());
                                    double gstamtinvoice = (cgstamtinvoice + sgstamtinvoice + igstamtinvoice);

                                    if (UpdateGST != gstamtinvoice)
                                    {
                                        msg = this.GetMessage(MessageCatalog.Transaction.VocherTransaction.LEDGER_GST_INV);
                                        Rtn = false;
                                    }
                                }
                            }
                            else if (Rtn && IsGeneralInvolice)
                            {
                                string lname = GetInvoiceLedgerName();
                                using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem())
                                {
                                    CommonMethod UtilityMethod = new CommonMethod();
                                    ResultArgs resultFind = UtilityMethod.CheckValueCotainsInDataTable(DtGSTInvoiceMasterLedgerDetails,
                                        vsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, lname);


                                    string filter = sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_NAMEColumn.ColumnName + " <> '' AND " +
                                                            sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn.ColumnName + " >0 AND " +
                                                            sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn.ColumnName + " >0";
                                    double SumTotalAmount = UtilityMember.NumberSet.ToDouble(DtGSTInvoiceMasterLedgerDetails.Compute("SUM(" +
                                            sysvoucher.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn.ColumnName + ")", filter).ToString());

                                    if (resultFind.DataSource.Data == null && resultFind.Success)
                                    {
                                        msg = "Ledger(s) are mismatching in the Invoice details, Click on \"Attach Vendor Invoice\"/ \"Remove GST Invoice\" and Check details.";
                                        Rtn = false;
                                    }
                                    else if (SummaryDebit != SumTotalAmount)
                                    {
                                        msg = "Voucher Amount is mismatching with Invoice Amount, Click \"Attach Vendor GST Invoice\" and Check details.";
                                        Rtn = false;
                                    }
                                }
                            }

                        }
                    }
                }
                if (!Rtn)
                {
                    MessageRender.ShowMessage(msg);
                }
            }
            catch (Exception err)
            {
                Rtn = false;
                MessageRender.ShowMessage("Could validate GST Invoice details, " + err.Message);
            }

            return Rtn;
        }

        /// <summary>
        /// This is to Replace the Credit amount to debit amount if equal while using GST
        /// Identify the First Entries are debit while Making Journal Transactions
        /// When you propose to update the Credit Entires check Two Previous rows Count Values always
        /// Ex : (Cr is current Row, Above Dr and Above Cr) OR Above is Empty if it is only one Row
        /// 
        /// </summary>
        private bool ValidateGST()
        {
            bool isValid = true;
            string Msg = "";
            DataTable dtSource = gcTransaction.DataSource as DataTable;
            DataTable dtDebitSource = new DataTable();
            DataTable dtCreditSource = new DataTable();

            try
            {
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    dtSource.DefaultView.RowFilter = "DEBIT>0 AND GST>0";
                    dtDebitSource = dtSource.DefaultView.ToTable();
                    DebitGST = this.UtilityMember.NumberSet.ToDouble(dtSource.Compute("SUM(GST)", "DEBIT>0 AND GST>0").ToString());
                    dtDebitSource.DefaultView.RowFilter = string.Empty;

                    dtSource.DefaultView.RowFilter = "CREDIT>0 AND GST>0";
                    dtCreditSource = dtSource.DefaultView.ToTable();
                    CreditGST = this.UtilityMember.NumberSet.ToDouble(dtSource.Compute("SUM(GST)", "CREDIT>0 AND GST>0").ToString());
                    dtSource.DefaultView.RowFilter = string.Empty;

                    if ((dtDebitSource != null && dtDebitSource.Rows.Count > 0) && (dtCreditSource != null && dtCreditSource.Rows.Count > 0))
                    {
                        isValid = false;
                        Msg = "Both: Debit Side and Credit Side GST Calculations should not be generated";
                    }
                }
                if (!isValid)
                {
                    MessageRender.ShowMessage(Msg);

                }
            }
            catch (Exception err)
            {
                isValid = false;
                MessageRender.ShowMessage("Could validate GST, " + err.Message);
            }

            return isValid;
            //DataRow dtFirstdatarow = gvTransaction.GetDataRow(0);
            //int FirstIndexId = 0;
            //int GroupId = 0;
            //if (dtFirstdatarow != null)
            //{
            //    int LedId = this.UtilityMember.NumberSet.ToInteger(dtFirstdatarow["LEDGER_ID"].ToString());
            //    GroupId = FetchLedgerDetails(LedId);
            //    decimal DebitAmt = this.UtilityMember.NumberSet.ToInteger(dtFirstdatarow["Debit"].ToString());
            //    decimal FirstGSTAmt = this.UtilityMember.NumberSet.ToInteger(dtFirstdatarow["GST"].ToString());
            //    FirstIndexId = DebitAmt > 0 ? 1 : 0;

            //    int CurrentRowIndex = gvTransaction.FocusedRowHandle;

            //    // if (CurrentRowIndex > 0 && !GroupId.Equals((int)TDSDefaultLedgers.SundryDebtors)) // 21/08/2023 // Introduce Group Id also
            //    if (CurrentRowIndex > 0 && FirstGSTAmt == 0)
            //    {
            //        DataRowView drCurentRowValues = gvTransaction.GetRow(CurrentRowIndex) as DataRowView;
            //        DataRowView drPreviousRow1Values = gvTransaction.GetRow(CurrentRowIndex - 1) == null ? null : gvTransaction.GetRow(CurrentRowIndex - 1) as DataRowView;
            //        DataRowView drPreviousRow2Values = gvTransaction.GetRow(CurrentRowIndex - 2) == null ? null : gvTransaction.GetRow(CurrentRowIndex - 2) as DataRowView;

            //        double CurrentCreditAmount = 0;
            //        double PreviousR1DebitAmount = 0;
            //        double PreviousR2CreditAmount = 0;

            //        double PreviousR1GSTAmt = 0;

            //        if (drCurentRowValues != null && drPreviousRow1Values != null)
            //        {
            //            CurrentCreditAmount = this.UtilityMember.NumberSet.ToDouble(drCurentRowValues["Credit"].ToString());
            //            PreviousR1DebitAmount = this.UtilityMember.NumberSet.ToDouble(drPreviousRow1Values["Debit"].ToString());
            //            PreviousR1GSTAmt = this.UtilityMember.NumberSet.ToDouble(drPreviousRow1Values["GST"].ToString());

            //            if (drPreviousRow2Values != null)
            //                PreviousR2CreditAmount = drPreviousRow2Values["Credit"] != null ? this.UtilityMember.NumberSet.ToDouble(drPreviousRow2Values["Credit"].ToString()) : 0;

            //            if (PreviousR1DebitAmount > 0 && (PreviousR2CreditAmount > 0 || PreviousR2CreditAmount == 0))
            //            {
            //                if (CurrentCreditAmount > 0 && PreviousR1GSTAmt == 0)
            //                    drPreviousRow1Values["Debit"] = (CurrentCreditAmount + UpdateGST);
            //            }
            //        }
            //    }
            //}
        }

        private bool isExistInvoiceNo()
        {
            int counts = 0;
            if (!string.IsNullOrEmpty(GSTVendorInvoiceNo))
            {
                using (VoucherTransactionSystem transSystem = new VoucherTransactionSystem())
                {
                    counts = transSystem.IsExistsGSTInvoceNno(GSTVendorInvoiceNo.Trim(), VoucherId);
                }
            }
            return (counts == 0);
        }

        /// <summary>
        /// On 04/09/2024, To check Currency amount wtih currency amount
        /// </summary>
        /// <returns></returns>
        private bool ValidateCurrencyAmountWithTransAmount()
        {
            bool isValid = true;
            if (this.AppSetting.AllowMultiCurrency == 1 && IsCurrencyEnabledVoucher)
            {
                double TransAmount = 0;
                double CurrencyAmount = this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text);
                DataTable dtTrans = gcTransaction.DataSource as DataTable;
                if (dtTrans != null && dtTrans.Rows.Count > 0)
                {
                    TransAmount = this.UtilityMember.NumberSet.ToDouble(dtTrans.Compute("SUM(DEBIT)", string.Empty).ToString()); //(int)Source.To
                }

                if (TransAmount != CurrencyAmount)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_AMOUNT_NOT_EQUAL_ACTUAL_AMOUNT));
                    FocusTransactionGrid();
                    isValid = false;
                }
            }

            return isValid;
        }

        /// <summary>
        /// On 19/12/2024, To get Credit Ledgers alone for Invoice for Multi Currency /Other than india country
        /// </summary>
        /// <returns></returns>
        private string GetInvoiceLedgerName()
        {
            string rtn = string.Empty;
            if (gcTransaction.DataSource != null)
            {
                DataTable dtTransaction = gcTransaction.DataSource as DataTable;
                DataTable dtLegersTrans = dtTransaction.DefaultView.ToTable();

                if (IsGeneralInvolice)
                {
                    using (VoucherTransactionSystem sysvoucher = new VoucherTransactionSystem())
                    {
                        dtLegersTrans.DefaultView.RowFilter = sysvoucher.AppSchema.VoucherTransaction.DEBITColumn.ColumnName + ">0"; //Consider Debit Ledger alone
                        if (dtLegersTrans.DefaultView.Count > 0)
                        {
                            Int32 lid = UtilityMember.NumberSet.ToInteger(dtLegersTrans.DefaultView[0][sysvoucher.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                            using (LedgerSystem lsystem = new LedgerSystem())
                            {
                                rtn = lsystem.GetLegerName(lid);
                            }
                        }
                    }
                }
            }

            return rtn;
        }

        private bool IsValidateBookedReferedValue()
        {
            bool isValid = true;
            DataTable transSource = (DataTable)gcTransaction.DataSource;
            int RowPosition = 0;
            double TempDebitAmount = 0;
            DateTime dtVoucherDate = DateTime.MinValue;
            string ReferenceNumber = string.Empty;

            DataView dv = new DataView(transSource);
            dv.RowFilter = "(LEDGER_ID>0 OR DEBIT>0)";

            if (dv.Count > 0)
            {
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    isValid = true;
                    foreach (DataRowView drTrans in dv)
                    {
                        TempDebitAmount = this.UtilityMember.NumberSet.ToDouble(drTrans["TEMP_DEBIT"].ToString());
                        dtVoucherDate = drTrans["VOUCHER_DATE"] != DBNull.Value ? this.UtilityMember.DateSet.ToDate(drTrans["VOUCHER_DATE"].ToString(), false) : DateTime.MinValue; ;
                        voucherSystem.VoucherId = VoucherId;
                        ReferenceNumber = drTrans["REFERENCE_NUMBER"] != DBNull.Value ? drTrans["REFERENCE_NUMBER"].ToString() : string.Empty;
                        int isExists = voucherSystem.IsExistVoucherJournalRefTrans();
                        if (isExists != 0)
                        {
                            if ((dtVoucherDate != DateTime.MinValue && deDate.DateTime > dtVoucherDate))
                            {
                                this.ShowMessageBox("This Transaction Date is Referered in Payment Voucher Date. So You can not increase the Transaction Date");
                                deDate.Focus();
                                isValid = false;
                                break;
                            }

                            if (SummaryDebit < TempDebitAmount)
                            {
                                voucherSystem.VoucherId = VoucherId;
                                resultArgs = voucherSystem.FetchLedgerAmountValidation(voucherSystem.VoucherId);


                                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    double RefereredAmount = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["JOURNAL_REF_AMOUNT"].ToString());

                                    if (SummaryDebit < RefereredAmount)
                                    {
                                        this.ShowMessageBox("This Amount is Referered in Payment Voucher. So You can not reduce less than the referered Amount");
                                        gvTransaction.FocusedColumn = colDebit;
                                        isValid = false;
                                        break;
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(ReferenceNumber))
                        {
                            int Value = isExistLedger(ReferenceNumber, VoucherId, LedgerId);
                            if (Value > 0)
                            {
                                this.ShowMessageBox("The Reference No is available for the Ledger!");
                                gvTransaction.FocusedColumn = colReferenceNumber;
                                isValid = false;
                                break;
                            }
                        }
                        RowPosition = RowPosition + 1;
                    }
                }
            }

            if (!isValid)
            {
                gvTransaction.CloseEditor();
                gvTransaction.FocusedRowHandle = gvTransaction.GetRowHandle(RowPosition);
                gvTransaction.ShowEditor();
            }

            return isValid;
        }

        /// <summary>
        /// On 17/01/2025 - To valudate journal vouchers invoices's against receipt voucher
        /// Date must be less than agaist RandP voucher
        /// Invoice amount should not be less than receipt amount
        /// </summary>
        /// <returns></returns>
        private bool IsValidateAgainstRandPVoucher()
        {
            bool isValid = true;
            DataTable transSource = (DataTable)gcTransaction.DataSource;
            int RowPosition = 0;
            double dtRPVoucherAmount = 0;
            DateTime dtRPVoucherDate = DateTime.MinValue;

            DataView dv = new DataView(transSource);
            dv.RowFilter = "(LEDGER_ID>0 OR DEBIT>0)";

            if (dv.Count > 0 && !string.IsNullOrEmpty(GSTVendorInvoiceNo) && this.AppSetting.IncludeGSTVendorInvoiceDetails == "2")
            {
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    isValid = true;
                    ResultArgs result = voucherSystem.FetchRandPVoucherAgainstJournalInvoiceByVoucherId(VoucherId);
                    if (result.Success && result.RowsAffected > 0)
                    {
                        DataTable dtRandPAgainstJournal = result.DataSource.Table;
                        Int32 invoiceid = UtilityMember.NumberSet.ToInteger(dtRandPAgainstJournal.Rows[0][voucherSystem.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName].ToString());
                        DateTime dtMinRPdate = UtilityMember.DateSet.ToDate(dtRandPAgainstJournal.Compute("MIN(" + voucherSystem.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName + ")",
                                    string.Empty).ToString(), false);
                        dtRPVoucherAmount = UtilityMember.NumberSet.ToDouble(dtRandPAgainstJournal.Compute("SUM(" + voucherSystem.AppSchema.VoucherTransaction.AMOUNTColumn + ")",
                                string.Empty).ToString());
                        if ((dtMinRPdate != DateTime.MinValue && deDate.DateTime > dtMinRPdate))
                        {
                            this.ShowMessageBox("As Invoice Amount is already settled in Receipt Voucher, Journal Voucher Date should be less than or equal to Receipt Voucher Date");
                            deDate.Focus();
                            isValid = false;
                        }
                        else if (SummaryDebit < dtRPVoucherAmount)
                        {
                            this.ShowMessageBox("As Invoice is referered in Receipt Voucher, You can not reduce less than already settled Amount");
                            gvTransaction.FocusedColumn = colDebit;
                            isValid = false;
                        }

                        if (isValid)
                        {
                            //Check and validate Is Invoice Ledgers is chagned after settlement is done in Receipt Voucher
                            resultArgs = voucherSystem.FetchGSTInvoiceMasterLedgersDetails(invoiceid);
                            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                DataTable dtInvoiceDetails = resultArgs.DataSource.Table;
                                DataTable dtInvoiceLedgers = dtInvoiceDetails.DefaultView.ToTable(true,
                                        new string[] { voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName });

                                foreach (DataRow drInvoiceLedgers in dtInvoiceLedgers.Rows)
                                {
                                    Int32 invoiceLid = UtilityMember.NumberSet.ToInteger(drInvoiceLedgers[voucherSystem.AppSchema.GSTInvoiceMasterLedgerDetails.LEDGER_IDColumn.ColumnName].ToString());
                                    double invoiceLAmount = UtilityMember.NumberSet.ToDouble(dtInvoiceDetails.Compute("SUM(" + voucherSystem.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn.ColumnName + ")",
                                                voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName + "=" + invoiceLid).ToString());

                                    double TransLDebitAmount = UtilityMember.NumberSet.ToDouble(dv.ToTable().Compute("SUM(" + voucherSystem.AppSchema.VoucherTransaction.DEBITColumn.ColumnName + ")",
                                                voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName + "=" + invoiceLid).ToString());
                                    if (TransLDebitAmount < invoiceLAmount)
                                    {
                                        this.ShowMessageBox("As Invoice is referered in Receipt Voucher, You can not change Ledger or Amount in Journal Voucher");
                                        gvTransaction.FocusedColumn = colDebit;
                                        isValid = false;
                                        break;
                                    }
                                }
                            }
                        }

                        RowPosition = RowPosition + 1;
                    }
                }
            }

            if (!isValid)
            {
                gvTransaction.CloseEditor();
                gvTransaction.FocusedRowHandle = gvTransaction.GetRowHandle(RowPosition);
                gvTransaction.ShowEditor();
            }

            return isValid;
        }

        private void rglkpLedgerGST_EditValueChanged(object sender, EventArgs e)
        {
            //29/11/2019, To set Ledger GST Ledger Class
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (edit != null)
            {
                LedgerGSTClassId = this.UtilityMember.NumberSet.ToInteger(edit.EditValue.ToString());
                AssignGSTAmount(LedgerGSTClassId);
                UpdateGSTReplaced();
                //  CalculateFirstRowValue();
            }
        }


        public int isExistLedger(string RefNumbe, int voucherid, int ledgerId)
        {
            int Id = 0;
            using (VoucherTransactionSystem transSystem = new VoucherTransactionSystem())
            {
                Id = transSystem.IsExistReferenceNo(RefNumbe, voucherid, ledgerId);
            }
            return Id;
        }
        #endregion

        private void ucAdditionalInfo_DeleteVoucherClicked(object sender, EventArgs e)
        {
            try
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    if (!IsLockedTransaction(deDate.DateTime) && ucAdditionalInfo.LockDeleteVocuher)
                    {
                        voucherTransaction.VoucherId = VoucherId;
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int IsExists = voucherTransaction.IsExistVoucherJournalRefTrans();
                            if (IsExists > 0)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.Transaction.TRANS_JOURNAL_VIEW_DELETE_REFERERED_VOUCHER), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //resultArgs = voucherTransactionSystem.DeleteRefererdVoucher();
                                    resultArgs = voucherTransaction.DeleteRefererdVouchersByJournalVoucher();
                                }
                            }

                            resultArgs = voucherTransaction.DeleteVoucherTrans();
                            if (resultArgs.Success)
                            {
                                if (UpdateHeld != null)
                                {
                                    UpdateHeld(this, e);
                                }
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_IS_LOCKED_CANNOT_DELETE) + "'" + ProjectName + "'" +
                            this.GetMessage(MessageCatalog.Transaction.VocherTransaction.DURING_PERIOD) + this.UtilityMember.DateSet.ToDate(dtLockDateFrom.ToShortDateString()) +
                                " - " + this.UtilityMember.DateSet.ToDate(dtLockDateTo.ToShortDateString())
                                    );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void ucAdditionalInfo_PrintVoucherClicked(object sender, EventArgs e)
        {
            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
            {
                Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);

                string rptVoucher = UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.JOURNALVOUCHER);
                resultArgs = voucherSystem.FetchReportSetting(rptVoucher);
                if (resultArgs != null && resultArgs.Success)
                {
                    ReportProperty.Current.VoucherPrintSettingInfo = resultArgs.DataSource.TableView;
                    ReportProperty.Current.CashBankVoucherDateFrom = ReportProperty.Current.CashBankVoucherDateTo = this.UtilityMember.DateSet.ToDate(deDate.Text, false);
                    report.VoucherPrint(this.VoucherId.ToString(), rptVoucher, ProjectName, ProjectId);
                }
                else
                {
                    this.ShowMessageBoxError(resultArgs.Message);
                }
            }
        }

        private void lkpCountry_EditValueChanged(object sender, EventArgs e)
        {
            ShowCurrencyDetails();
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

        private void txtCurrencyAmount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) >= 0)
            {
                CalculateExchangeRate();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtCurrencyAmount.Text = "0";
                CalculateExchangeRate();
            }
        }

    }
}