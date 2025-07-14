using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class TrialBalance : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        string dtClosingDateRange = string.Empty;
        string dtClosingDateRangeYear = string.Empty;
        int GroupNumber = 0;
        double OPDebit = 0;
        double OPCredit = 0;
        double CurTransDebit = 0;
        double CurTransCredit = 0;
        double ClosingBalanceDebit = 0;
        double ClosingBalanceCredit = 0;
        double TotalCashClosingBalanceDebit = 0;
        double TotalCashClosingBalanceCredit = 0;
        double TotalBankClosingBalanceDebit = 0;
        double TotalBankClosingBalanceCredit = 0;
        double TotalFDClosingBalanceDebit = 0;
        double TotalFDClosingBalanceCredit = 0;
        double TotalOpeningCashAmount = 0;
        double TotalOpeningBankAmount = 0;
        double TotalOpeningFixedDeposit = 0;
        double AllTotalOpeningLedgerDebitBalance = 0;
        double AllTotalOpeningLedgerCreditBalance = 0;
        double CashDebitCurrentTotal = 0;
        double CashCreditCurrentTotal = 0;
        double BankAccountCreditTotal = 0;
        double BankAccountDebitTotal = 0;
        double FixedDepositCreditTotal = 0;
        double FixedDepositDebitTotal = 0;
        double SumCurTransDebit = 0;
        double SumCurTransCredit = 0;
        double SumofClDebit = 0;
        double SumofClCredit = 0;
        double SumClosingBalanceDebit = 0;
        double SumClosingBalanceCredit = 0;
        double IEDebit = 0;
        double IECredit = 0;
        double TotalOpeningCashBankFD = 0;
        double TotalOpeningCashBankFDCredit = 0;
        double OpCashDebit = 0;
        double OpCashCredit = 0;
        double OpBankDebit = 0;
        double OpBankCredit = 0;
        double OpFDDebit = 0;
        double OpFDCredit = 0;
        double IncomeAmt = 0;
        double ExpenceAmt = 0;
        int CurrentFinancialYear = 0;
        double IncomeExpenditureAmountPrevious = 0;
        string DateRangeDFReducing = string.Empty;
        string GroupCode = string.Empty;
        string PrevGroupCode = string.Empty;
        int GroupCodeNumber = 0;
        double GrpClosingDebit = 0.0;
        double GrpClosingCredit = 0.0;
        bool isCapLedgerCodeVisible;
        bool isCapGroupCodeVisible;

        double ExcessDebitAmount;
        double ExcessCreditAmount;

        private DataTable dtCCDetails = new DataTable();
        private bool PrevLedgerCCFound = false;
        #endregion

        #region Constructor
        public TrialBalance()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrTblGroup, xrtblClCurGrpDebit, new ArrayList { reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName,
                    reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName}, DrillDownType.GROUP_SUMMARY_PAYMENTS, false);
            this.AttachDrillDownToRecord(xrTblGroup, xrtblClCurGrpCredit, new ArrayList { reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName,
                    reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName}, DrillDownType.GROUP_SUMMARY_RECEIPTS, false);

            this.AttachDrillDownToRecord(xrTblLedger, xrCurTransDebit, new ArrayList { reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName,
                     reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName}, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false);
            this.AttachDrillDownToRecord(xrTblLedger, xrCurTransCredit, new ArrayList { reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName,
                    reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName}, DrillDownType.LEDGER_SUMMARY_RECEIPTS, false);

            // Show Transaction - Separate Table and Table Group to show the Current Transaction 
            this.AttachDrillDownToRecord(xrTableCurrentTransGroup, xrTableGroupCTransDebit, new ArrayList { reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName, 
                    reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName }, DrillDownType.GROUP_SUMMARY_PAYMENTS, false);
            this.AttachDrillDownToRecord(xrTableCurrentTransGroup, xrTableGroupCTransCredit, new ArrayList { reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName,
                    reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName}, DrillDownType.GROUP_SUMMARY_RECEIPTS, false);

            this.AttachDrillDownToRecord(xrTableCurrentTransLedger, xrTableCTransDebit, new ArrayList { reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName,
                    reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName}, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false);
            this.AttachDrillDownToRecord(xrTableCurrentTransLedger, xrTableCTransCredit, new ArrayList { reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName,
                    reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName}, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false);

            //Main Ledger Closing
            ArrayList groupfilter = new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, reportSetting1.ReportParameter.DATE_AS_ONColumn.ColumnName,
                               reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName};
            ArrayList ledgerfilter = new ArrayList { reportSetting1.BalanceSheet.LEDGER_IDColumn.ColumnName, reportSetting1.ReportParameter.DATE_AS_ONColumn.ColumnName,
                               reportSetting1.ReportParameter.CURRENCY_COUNTRY_IDColumn.ColumnName};
            DrillDownType groupdrilltype = DrillDownType.GROUP_SUMMARY;
            DrillDownType ledgerdrilltype = DrillDownType.LEDGER_SUMMARY;

            this.AttachDrillDownToRecord(xrTblGroup, xrGroupClosingBalDebit,
                groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTblGroup, xrGroupClosingBalCredit,
                groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTblLedger, xrClosingBalanceDebit,
                ledgerfilter, ledgerdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTblLedger, xrClosingBalanceCredit,
                ledgerfilter, ledgerdrilltype, false, "", false);

            //Only Closing Table
            this.AttachDrillDownToRecord(xrTableClosingGroup, xrTableCell77,
                groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTableClosingGroup, xrTableCell78,
                groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTableClosingLedger, xrTableClosingDebit,
              ledgerfilter, ledgerdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTableClosingLedger, xrTableClosingCredit,
                ledgerfilter, ledgerdrilltype, false, "", false);

            //Opening Closing
            this.AttachDrillDownToRecord(xrTableOpeningGroup, xrTableCell50,
                groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTableOpeningGroup, xrTableCell51,
                groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTableOpeningLedger, xrTableOPClosingDebit,
             ledgerfilter, ledgerdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTableOpeningLedger, xrTableOPClosingCredit,
                ledgerfilter, ledgerdrilltype, false, "", false);

            // Transaction Closing

            this.AttachDrillDownToRecord(xrTableCurrentTransGroup, xrTableCell69,
               groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTableCurrentTransGroup, xrTableCell70,
                groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTableCurrentTransLedger, xrTableCTransClosingDebit,
           ledgerfilter, ledgerdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTableCurrentTransLedger, xrTableCTransClosingCredit,
                ledgerfilter, ledgerdrilltype, false, "", false);


        }
        #endregion

        #region Property
        string yearfrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearfrom = settingProperty.YearFrom;
                return yearfrom;
            }
        }
        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }
        DataTable dtAssignTrialBalance = null;
        public DataTable dtTrialBalance
        {
            get
            {
                return dtAssignTrialBalance;
            }
            set
            {
                dtAssignTrialBalance = value;
            }
        }

        public float CodeColumnWidth
        {
            set
            {
                xrHeaderLedgerCodeClosing.WidthF = value;

            }
        }
        public float CodeColumnWidthEmpty
        {
            set
            {
                xrHeaderLedgerCodeClosingEmpty.WidthF = value;

            }
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            SumofClCredit = 0;
            SumofClDebit = 0;

            // 23.02.2019 make it Zero 
            OpCashDebit = 0;
            OpCashCredit = 0;
            OpBankDebit = 0;
            OpBankCredit = 0;
            OpFDDebit = 0;
            OpFDCredit = 0;

            BindTrialBalance();
            xrTotalSumClosingCreditAmount.Text = xrTotalSumClosingDebitAmount.Text = string.Empty;
            OPDebit = 0;
            OPCredit = 0;
            CurTransDebit = 0;
            CurTransCredit = 0;
            ClosingBalanceDebit = 0;
            ClosingBalanceCredit = 0;

            // 23.02.2019 make it Zero  in order clear old values
            TotalCashClosingBalanceDebit = 0;
            TotalCashClosingBalanceCredit = 0;
            TotalBankClosingBalanceDebit = 0;
            TotalBankClosingBalanceCredit = 0;
            TotalFDClosingBalanceDebit = 0;
            TotalFDClosingBalanceCredit = 0;

            xrClosingBalanceCredit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrClosingBalanceDebit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            ClosingBalance();

            ExcessDebitAmount = 0;
            ExcessCreditAmount = 0;
            GetTrialBanceExcessAmount();

            // IncomeExpenceCalculation();
            CurrentFinancialYear = IsFinancialYear();
            SortByLedgerorGroup();
        }
        #endregion

        #region Method
        private void BindTrialBalance()
        {
            try
            {
                // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                bool enforceCurrency = (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0 ? true : false);

                setHeaderTitleAlignment();
                SetReportTitle();
                this.SetLandscapeHeader = 1030.25f;
                this.SetLandscapeFooter = 1030.25f;
                this.SetLandscapeFooterDateWidth = 860.00f;
                //if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
                if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project == "0")
                {
                    ShowReportFilterDialog();
                    //YearFrom = IsFinancialYearFrom();
                }
                else
                {
                    if (this.UIAppSetting.UICustomizationForm == "1")
                    {
                        if (ReportProperty.Current.ReportFlag == 0)
                        {
                            SplashScreenManager.ShowForm(typeof(frmReportWait));

                            string CloseDate = GetProgressiveDate(this.ReportProperties.DateFrom);
                            DateTime dtClosingDate = this.UtilityMember.DateSet.ToDate(CloseDate, false);
                            dtClosingDateRange = dtClosingDate.AddDays(-1).ToShortDateString();
                            DateTime dtYearClosing = Convert.ToDateTime(YearFrom);
                            dtClosingDateRangeYear = dtYearClosing.AddDays(-1).ToShortDateString();
                            string DFRange = this.ReportProperties.DateFrom;
                            DateTime DFDateRange = Convert.ToDateTime(DFRange);
                            DateRangeDFReducing = DFDateRange.AddDays(-1).ToShortDateString();
                            DataTable dtIncome = GetIncomeExcessAmount();
                            DataTable dtExpence = GetExpenceExcessAmt();
                            CurrentFinancialYear = IsFinancialYear();
                            //  GetTrialBanceExcessAmount();
                            ResultArgs resultArgs = GetTrialBalance();
                            DataView dtValue = resultArgs.DataSource.TableView;
                            DataTable dtOpValue = dtValue.ToTable();
                            dtTrialBalance = dtOpValue;
                            DataTable dtCurrentvalue = GetTrialBalanceCurrent();
                            SumCurTransDebit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CURRENTTRANS_DEBIT)", "").ToString());
                            SumCurTransCredit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CURRENTTRANS_CREDIT)", "").ToString());
                            CashDebitCurrentTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(CASH_IN_HAND_DEBIT)", "").ToString());
                            CashCreditCurrentTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(CASH_IN_HAND_CREDIT)", "").ToString());
                            BankAccountCreditTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(BANK_ACCOUNT_CREDIT)", "").ToString());
                            BankAccountDebitTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(BANK_ACCOUNT_DEBIT)", "").ToString());

                            // chinna
                            //FixedDepositCreditTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(FIXED_DEPOSIT_CREDIT)", "").ToString());
                            //FixedDepositDebitTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(FIXED_DEPOSIT_DEBIT)", "").ToString());

                            SumofClDebit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CLOSING_DEBIT)", "").ToString());
                            SumofClCredit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CLOSING_CREDIT)", "").ToString());

                            if (dtValue != null)
                            {
                                dtValue.Table.TableName = "TrialBalance";
                                this.DataSource = dtValue;
                                this.DataMember = dtValue.Table.TableName;
                            }
                            TotalOpeningCashAmount = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.CashBalance, BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                            TotalOpeningBankAmount = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance, BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                            TotalOpeningFixedDeposit = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.FDBalance, BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);

                            IncomeAmt = this.UtilityMember.NumberSet.ToDouble(dtIncome.Rows[0]["RECEIPTAMT"].ToString());
                            ExpenceAmt = this.UtilityMember.NumberSet.ToDouble(dtExpence.Rows[0]["PAYMENTAMT"].ToString());
                            if (TotalOpeningCashAmount > 0)
                            {
                                OpCashDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningCashAmount.ToString()));
                                xrtblDrOpeningTotalCashAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpCashDebit);
                                xrtblCrOpeningTotalCashAmount.Text = string.Empty;
                            }
                            else
                            {
                                OpCashCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningCashAmount.ToString()));
                                xrtblCrOpeningTotalCashAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpCashCredit);
                                xrtblDrOpeningTotalCashAmount.Text = string.Empty;
                            }
                            if (TotalOpeningBankAmount > 0)
                            {
                                OpBankDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningBankAmount.ToString()));
                                xrtblDrOpeningBankAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpBankDebit);
                                xrtblCrOpeningBankAmount.Text = string.Empty;
                            }
                            else
                            {
                                OpBankCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningBankAmount.ToString()));
                                xrtblCrOpeningBankAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpBankCredit);
                                xrtblDrOpeningBankAmount.Text = string.Empty;
                            }

                            // FD - chinna

                            //if (TotalOpeningFixedDeposit > 0)
                            //{
                            //    OpFDDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningFixedDeposit.ToString()));
                            //    xrDrFixedAmt.Text = this.UtilityMember.NumberSet.ToNumber(OpFDDebit);
                            //    xrCrFixedAmt.Text = string.Empty;
                            //}
                            //else
                            //{
                            //    OpFDCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningFixedDeposit.ToString()));
                            //    xrCrFixedAmt.Text = this.UtilityMember.NumberSet.ToNumber(OpFDCredit);
                            //    xrDrFixedAmt.Text = string.Empty;
                            //}
                            xrtblCurrentTransCashDebit.Text = this.UtilityMember.NumberSet.ToNumber(CashCreditCurrentTotal);
                            xrtblCurrentTransBankDebit.Text = this.UtilityMember.NumberSet.ToNumber(BankAccountCreditTotal);
                            //  xrtblCurrentFixedDepostDebit.Text = this.UtilityMember.NumberSet.ToNumber(FixedDepositCreditTotal);
                            xrtblCurrentTransCashCredit.Text = this.UtilityMember.NumberSet.ToNumber(CashDebitCurrentTotal);
                            xrtblCurrentTransBankCredit.Text = this.UtilityMember.NumberSet.ToNumber(BankAccountDebitTotal);
                            // xrtblCurrentFixedDepostCredit.Text = this.UtilityMember.NumberSet.ToNumber(FixedDepositDebitTotal);
                            grpGroupHeader.Visible = (ReportProperties.ShowByLedgerGroup == 1);
                            GrpTrialBalanceLedger.Visible = ReportProperties.ShowByLedger == 1;
                            if (grpGroupHeader.Visible == false && GrpTrialBalanceLedger.Visible == false)
                            {
                                GrpTrialBalanceLedger.Visible = true;
                            }

                            if (grpGroupHeader.Visible)
                            {
                                if (ReportProperties.SortByGroup == 1)
                                {
                                    grpGroupHeader.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                                    grpGroupHeader.GroupFields[0].FieldName = reportSetting1.TrialBalance.LEDGER_GROUPColumn.ColumnName;
                                }
                                else
                                {
                                    grpGroupHeader.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                                    grpGroupHeader.GroupFields[1].FieldName = reportSetting1.TrialBalance.LEDGER_GROUPColumn.ColumnName;
                                }
                            }
                            if (grpGroupHeader.Visible)
                            {
                                // xrTable31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                //xrtblbalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                xrtblCrOpeningTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                xrtblDrOpeningTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                // xrCellallfooterDifferece.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                diifferOpDebit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                DifferOpCredit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                DifferCurTransDebit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                DifferCurTransCredit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                DifferClosingDebit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                DifferClosingCredit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            }
                            else
                            {
                                //xrtblbalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                xrtblCrOpeningTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                xrtblDrOpeningTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            }
                            if (GrpTrialBalanceLedger.Visible)
                            {
                                if (ReportProperties.SortByLedger == 1)
                                {
                                    GrpTrialBalanceLedger.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                                    GrpTrialBalanceLedger.GroupFields[1].FieldName = reportSetting1.TrialBalance.LEDGER_NAMEColumn.ColumnName;
                                }
                                else
                                {
                                    GrpTrialBalanceLedger.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                                    GrpTrialBalanceLedger.GroupFields[1].FieldName = reportSetting1.TrialBalance.LEDGER_NAMEColumn.ColumnName;
                                }
                            }
                            //Opening
                            this.xrSubOpReports.Visible = xrlblOpBalance.Visible = (this.ReportProperties.ShowDetailedBalance == 1);
                            this.xrSubClosingBalanceDetails.Visible = xrClosingBalance.Visible = (this.ReportProperties.ShowDetailedBalance == 1);
                            if (this.ReportProperties.ShowDetailedBalance == 1)
                            {
                                AccountBalance accountopBalance = xrSubOpReports.ReportSource as AccountBalance;
                                accountopBalance.BankClosedDate = this.ReportProperties.DateFrom;
                                accountopBalance.BindBalance(true, false);
                                this.AttachDrillDownToSubReport(accountopBalance);
                                accountopBalance.CodeColumnWidth = 77;  //xrTotalCodeCaption.WidthF;  //75.35
                                accountopBalance.NameColumnWidth = 315;   // xrTotalSumCaptions.WidthF + xrtblTotalSumOpeningDebitamt.WidthF; // 128+187
                                accountopBalance.AmountColumnWidth = 130; // xrtblTotalSumOpeningCreditamount.WidthF; // 128
                                accountopBalance.GroupNameWidth = 77 + 313; //xrTotalCodeCaption.WidthF + xrTotalSumCaptions.WidthF + xrtblTotalSumOpeningDebitamt.WidthF - 2; //75.35+187+126
                                accountopBalance.GroupAmountWidth = 130; //xrtblTotalSumOpeningCreditamount.WidthF; // 128
                                this.xrSubOpReports.WidthF = xrlblOpBalance.WidthF;
                                accountopBalance.AmountProgressiveHeaderColumnWidth = accountopBalance.AmountProgressiveColumnWidth = 0;

                                //Closing
                                AccountBalance accountClosingBalance = xrSubClosingBalanceDetails.ReportSource as AccountBalance;
                                accountClosingBalance.BankClosedDate = this.ReportProperties.DateFrom;
                                accountClosingBalance.BindBalance(false, false);
                                this.AttachDrillDownToSubReport(accountClosingBalance);
                                accountClosingBalance.CodeColumnWidth = 77; // xrtblTotalSumDebitCurrentTransAmount.WidthF;
                                accountClosingBalance.NameColumnWidth = 315; // xrTotalSumCreditCurrentTransAmount.WidthF + xrTotalSumClosingDebitAmount.WidthF;
                                accountClosingBalance.AmountColumnWidth = 130; //xrTotalSumClosingCreditAmount.WidthF;
                                accountClosingBalance.GroupNameWidth = 77 + 313;  //xrtblTotalSumDebitCurrentTransAmount.WidthF + xrTotalSumCreditCurrentTransAmount.WidthF + xrTotalSumClosingDebitAmount.WidthF - 2;
                                accountClosingBalance.GroupAmountWidth = 130; //xrTotalSumClosingCreditAmount.WidthF;
                                this.xrSubClosingBalanceDetails.WidthF = xrClosingBalance.WidthF;
                                this.xrSubClosingBalanceDetails.LeftF = xrClosingBalance.LeftF;
                                accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                            }
                            if (!(this.ReportProperties.ShowOpeningBalance == 1 && this.ReportProperties.ShowCurrentTransaction == 1))
                            {
                                HideCashBankPanel();
                            }

                            SplashScreenManager.CloseForm();
                            base.ShowReport();
                        }
                        else
                        {
                            ShowReportFilterDialog();
                        }
                    }
                    else
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        //  YearFrom = IsFinancialYearFrom();
                        string CloseDate = GetProgressiveDate(this.ReportProperties.DateFrom);
                        DateTime dtClosingDate = this.UtilityMember.DateSet.ToDate(CloseDate, false);
                        dtClosingDateRange = dtClosingDate.AddDays(-1).ToShortDateString();
                        DateTime dtYearClosing = Convert.ToDateTime(YearFrom);
                        dtClosingDateRangeYear = dtYearClosing.AddDays(-1).ToShortDateString();
                        string DFRange = this.ReportProperties.DateFrom;
                        DateTime DFDateRange = Convert.ToDateTime(DFRange);
                        DateRangeDFReducing = DFDateRange.AddDays(-1).ToShortDateString();
                        DataTable dtIncome = GetIncomeExcessAmount();
                        DataTable dtExpence = GetExpenceExcessAmt();
                        CurrentFinancialYear = IsFinancialYear();
                        // GetTrialBanceExcessAmount();
                        ResultArgs resultArgs = GetTrialBalance();
                        DataView dtValue = resultArgs.DataSource.TableView;
                        DataTable dtOpValue = dtValue.ToTable();
                        dtTrialBalance = dtOpValue;
                        DataTable dtCurrentvalue = GetTrialBalanceCurrent();
                        SumCurTransDebit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CURRENTTRANS_DEBIT)", "").ToString());
                        SumCurTransCredit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CURRENTTRANS_CREDIT)", "").ToString());
                        CashDebitCurrentTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(CASH_IN_HAND_DEBIT)", "").ToString());
                        CashCreditCurrentTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(CASH_IN_HAND_CREDIT)", "").ToString());
                        BankAccountCreditTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(BANK_ACCOUNT_CREDIT)", "").ToString());
                        BankAccountDebitTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(BANK_ACCOUNT_DEBIT)", "").ToString());

                        //FixedDepositCreditTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(FIXED_DEPOSIT_CREDIT)", "").ToString());
                        //FixedDepositDebitTotal = this.UtilityMember.NumberSet.ToDouble(dtCurrentvalue.Compute("SUM(FIXED_DEPOSIT_DEBIT)", "").ToString());

                        SumofClDebit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CLOSING_DEBIT)", "").ToString());
                        SumofClCredit = this.UtilityMember.NumberSet.ToDouble(dtOpValue.Compute("SUM(CLOSING_CREDIT)", "").ToString());


                        if (dtValue != null)
                        {
                            dtValue.Table.TableName = "TrialBalance";
                            this.DataSource = dtValue;
                            this.DataMember = dtValue.Table.TableName;
                        }
                        TotalOpeningCashAmount = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.CashBalance, BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                        TotalOpeningBankAmount = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance, BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                        TotalOpeningFixedDeposit = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.FDBalance, BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);

                        IncomeAmt = this.UtilityMember.NumberSet.ToDouble(dtIncome.Rows[0]["RECEIPTAMT"].ToString());
                        ExpenceAmt = this.UtilityMember.NumberSet.ToDouble(dtExpence.Rows[0]["PAYMENTAMT"].ToString());
                        if (TotalOpeningCashAmount > 0)
                        {
                            OpCashDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningCashAmount.ToString()));
                            xrtblDrOpeningTotalCashAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpCashDebit);
                            xrtblCrOpeningTotalCashAmount.Text = string.Empty;
                        }
                        else
                        {
                            OpCashCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningCashAmount.ToString()));
                            xrtblCrOpeningTotalCashAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpCashCredit);
                            xrtblDrOpeningTotalCashAmount.Text = string.Empty;
                        }
                        if (TotalOpeningBankAmount > 0)
                        {
                            OpBankDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningBankAmount.ToString()));
                            xrtblDrOpeningBankAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpBankDebit);
                            xrtblCrOpeningBankAmount.Text = string.Empty;
                        }
                        else
                        {
                            OpBankCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningBankAmount.ToString()));
                            xrtblCrOpeningBankAmount.Text = this.UtilityMember.NumberSet.ToNumber(OpBankCredit);
                            xrtblDrOpeningBankAmount.Text = string.Empty;
                        }

                        //  FD - chinna

                        //if (TotalOpeningFixedDeposit > 0)
                        //{
                        //    OpFDDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningFixedDeposit.ToString()));
                        //    xrDrFixedAmt.Text = this.UtilityMember.NumberSet.ToNumber(OpFDDebit);
                        //    xrCrFixedAmt.Text = string.Empty;
                        //}
                        //else
                        //{
                        //    OpFDCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(TotalOpeningFixedDeposit.ToString()));
                        //    xrCrFixedAmt.Text = this.UtilityMember.NumberSet.ToNumber(OpFDCredit);
                        //    xrDrFixedAmt.Text = string.Empty;
                        //}

                        xrtblCurrentTransCashDebit.Text = this.UtilityMember.NumberSet.ToNumber(CashCreditCurrentTotal);
                        xrtblCurrentTransBankDebit.Text = this.UtilityMember.NumberSet.ToNumber(BankAccountCreditTotal);

                        // Fd - chinna

                        //  xrtblCurrentFixedDepostDebit.Text = this.UtilityMember.NumberSet.ToNumber(FixedDepositCreditTotal);
                        xrtblCurrentTransCashCredit.Text = this.UtilityMember.NumberSet.ToNumber(CashDebitCurrentTotal);
                        xrtblCurrentTransBankCredit.Text = this.UtilityMember.NumberSet.ToNumber(BankAccountDebitTotal);

                        // Fd - chinna

                        // xrtblCurrentFixedDepostCredit.Text = this.UtilityMember.NumberSet.ToNumber(FixedDepositDebitTotal);

                        grpGroupHeader.Visible = (ReportProperties.ShowByLedgerGroup == 1);
                        GrpTrialBalanceLedger.Visible = ReportProperties.ShowByLedger == 1;
                        if (grpGroupHeader.Visible == false && GrpTrialBalanceLedger.Visible == false)
                        {
                            GrpTrialBalanceLedger.Visible = true;
                        }

                        if (grpGroupHeader.Visible)
                        {
                            if (ReportProperties.SortByGroup == 1)
                            {
                                grpGroupHeader.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                                grpGroupHeader.GroupFields[0].FieldName = reportSetting1.TrialBalance.LEDGER_GROUPColumn.ColumnName;
                            }
                            else
                            {
                                grpGroupHeader.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                                grpGroupHeader.GroupFields[1].FieldName = reportSetting1.TrialBalance.LEDGER_GROUPColumn.ColumnName;
                            }
                        }
                        if (grpGroupHeader.Visible)
                        {
                            // xrTable31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            //xrtblbalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            xrtblCrOpeningTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            xrtblDrOpeningTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            //  xrCellallfooterDifferece.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            diifferOpDebit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            DifferOpCredit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            DifferCurTransDebit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            DifferCurTransCredit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            DifferClosingDebit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            DifferClosingCredit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        }
                        else
                        {
                            //xrtblbalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            xrtblCrOpeningTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            xrtblDrOpeningTotalCashAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        }
                        if (GrpTrialBalanceLedger.Visible)
                        {
                            if (ReportProperties.SortByLedger == 1)
                            {
                                GrpTrialBalanceLedger.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                                GrpTrialBalanceLedger.GroupFields[1].FieldName = reportSetting1.TrialBalance.LEDGER_NAMEColumn.ColumnName;
                            }
                            else
                            {
                                GrpTrialBalanceLedger.GroupFields[0].FieldName = reportSetting1.TrialBalance.SORT_ORDERColumn.ColumnName;
                                GrpTrialBalanceLedger.GroupFields[1].FieldName = reportSetting1.TrialBalance.LEDGER_NAMEColumn.ColumnName;
                            }
                        }
                        //Opening
                        this.xrSubOpReports.Visible = xrlblOpBalance.Visible = (this.ReportProperties.ShowDetailedBalance == 1);
                        this.xrSubClosingBalanceDetails.Visible = xrClosingBalance.Visible = (this.ReportProperties.ShowDetailedBalance == 1);
                        if (this.ReportProperties.ShowDetailedBalance == 1)
                        {
                            AccountBalance accountopBalance = xrSubOpReports.ReportSource as AccountBalance;
                            accountopBalance.BindBalance(true, false);
                            this.AttachDrillDownToSubReport(accountopBalance);
                            accountopBalance.CodeColumnWidth = 77;  //xrTotalCodeCaption.WidthF;  //75.35
                            accountopBalance.NameColumnWidth = 315;   // xrTotalSumCaptions.WidthF + xrtblTotalSumOpeningDebitamt.WidthF; // 128+187
                            accountopBalance.AmountColumnWidth = 130; // xrtblTotalSumOpeningCreditamount.WidthF; // 128
                            accountopBalance.GroupNameWidth = 77 + 313; //xrTotalCodeCaption.WidthF + xrTotalSumCaptions.WidthF + xrtblTotalSumOpeningDebitamt.WidthF - 2; //75.35+187+126
                            accountopBalance.GroupAmountWidth = 130; //xrtblTotalSumOpeningCreditamount.WidthF; // 128
                            this.xrSubOpReports.WidthF = xrlblOpBalance.WidthF;
                            accountopBalance.AmountProgressiveHeaderColumnWidth = accountopBalance.AmountProgressiveColumnWidth = 0;

                            //Closing
                            AccountBalance accountClosingBalance = xrSubClosingBalanceDetails.ReportSource as AccountBalance;
                            accountClosingBalance.BindBalance(false, false);
                            this.AttachDrillDownToSubReport(accountClosingBalance);
                            accountClosingBalance.CodeColumnWidth = 77; // xrtblTotalSumDebitCurrentTransAmount.WidthF;
                            accountClosingBalance.NameColumnWidth = 315; // xrTotalSumCreditCurrentTransAmount.WidthF + xrTotalSumClosingDebitAmount.WidthF;
                            accountClosingBalance.AmountColumnWidth = 130; //xrTotalSumClosingCreditAmount.WidthF;
                            accountClosingBalance.GroupNameWidth = 77 + 313;  //xrtblTotalSumDebitCurrentTransAmount.WidthF + xrTotalSumCreditCurrentTransAmount.WidthF + xrTotalSumClosingDebitAmount.WidthF - 2;
                            accountClosingBalance.GroupAmountWidth = 130; //xrTotalSumClosingCreditAmount.WidthF;
                            this.xrSubClosingBalanceDetails.WidthF = xrClosingBalance.WidthF;
                            this.xrSubClosingBalanceDetails.LeftF = xrClosingBalance.LeftF;
                            accountClosingBalance.AmountProgressiveHeaderColumnWidth = accountClosingBalance.AmountProgressiveColumnWidth = 0;
                        }
                        if (!(this.ReportProperties.ShowOpeningBalance == 1 && this.ReportProperties.ShowCurrentTransaction == 1))
                        {
                            HideCashBankPanel();
                        }
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                }
                float actualCodeWidth = xrHeaderLedgerCode.WidthF;
                if (xrHeaderLedgerCode.Tag != null && xrHeaderLedgerCode.Tag.ToString() != "")
                {
                    actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrHeaderLedgerCode.Tag.ToString());
                }
                else
                {
                    xrHeaderLedgerCode.Tag = xrHeaderLedgerCode.WidthF;
                }


                isCapLedgerCodeVisible = (ReportProperties.ShowLedgerCode == 1);
                isCapGroupCodeVisible = (ReportProperties.ShowGroupCode == 1);


                xrHeaderLedgerCode.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);
                xrTableCell26.WidthF = ((isCapGroupCodeVisible == true) ? actualCodeWidth : 0);
                xrTblClLedgerCode.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);
                //  xrTableCellCashCode.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);
                // xrTableCellCodeBank.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);
                // xrTotalCodeCaption.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);

                // Opening
                xrHeaderOpeningLedgerCode.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);
                xrTableCell34.WidthF = ((isCapGroupCodeVisible == true) ? actualCodeWidth : 0);
                xrOpeningLedgerCode.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);

                //Transaction
                xrHeaderLedgerCodeTransaction.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);
                xrTableCell52.WidthF = ((isCapGroupCodeVisible == true) ? actualCodeWidth : 0);
                xrCurrentLedgerCode.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);

                // Closing
                xrHeaderLedgerCodeClosing.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);
                xrTableCell71.WidthF = ((isCapGroupCodeVisible == true) ? actualCodeWidth : 0);
                xrClosingLedgerCode.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);
                //   xrTableCellCashCode.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);
                //   xrTableCellCodeBank.WidthF = ((isCapLedgerCodeVisible == true) ? actualCodeWidth : 0);

                //Fd - chinna



                xrTrialBalance = AlignContentTable(xrTrialBalance);
                xrTblGroup = AlignGroupTable(xrTblGroup);
                xrTblLedger = AlignContentTable(xrTblLedger);
                xrtblbalance = AlignContentTable(xrtblbalance);
                xrtblTotal = AlignContentTable(xrtblTotal);
                xrtblLedgerHeaderCaption = AlignHeaderTable(xrtblLedgerHeaderCaption);

                // SetColor(xrtblHeadingCRDR);
                // SetColor(xrtblCurrentTransCRDR);
                // SetColor(xrTable8);
                // xrlblHeaderLedgerOpeningDRCR.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                // xrlblCurrentTransCRDR.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                // xrlblLedgerClosingCRDR.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                xrTableOpeningLedger = AlignContentTable(xrTableOpeningLedger);
                xrTableCurrentTransLedger = AlignContentTable(xrTableCurrentTransLedger);
                xrTableClosingLedger = AlignContentTable(xrTableClosingLedger);

                xrTableOpeningGroup = AlignGroupTable(xrTableOpeningGroup);
                xrTableCurrentTransGroup = AlignGroupTable(xrTableCurrentTransGroup);
                xrTableClosingGroup = AlignGroupTable(xrTableClosingGroup);

                //xrTableHeaderOpeningCaption = AlignHeaderTable(xrTableHeaderOpeningCaption);
                //xrTableHeaderCurrentTrans = AlignHeaderTable(xrTableHeaderCurrentTrans);
                //xrTableHeaderClosing = AlignHeaderTable(xrTableHeaderClosing);

                SortByLedgerorGroup();

                ValidateHeader();
                ValidateledgerInfo();
                ValidateledgerGroupInfo();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void ValidateHeader()
        {
            if (this.ReportProperties.ShowOpeningBalance != 0 && this.ReportProperties.ShowCurrentTransaction != 0)
            {
                xrtblOpeningHeaderCaption.Visible = false;
                xrtblOpeningHeaderCaption.LocationF = new PointF(2F, 0F);
                xrtblCurrentTransHeaderCaption.Visible = false;
                xrtblCurrentTransHeaderCaption.LocationF = new PointF(2F, 0F);
                xrtblClosingHeaderCaption.Visible = false;
                xrtblClosingHeaderCaption.LocationF = new PointF(2F, 0F);

                xrtblLedgerHeaderCaption.Visible = true;
                xrtblLedgerHeaderCaption.LocationF = new PointF(2F, 0F);
                xrtblLedgerHeaderCaption.HeightF = 50;
                PageHeader.HeightF = 50;
            }
            else if (this.ReportProperties.ShowOpeningBalance != 0)
            {
                xrtblLedgerHeaderCaption.Visible = false;
                xrtblLedgerHeaderCaption.LocationF = new PointF(2F, 0F);
                xrtblCurrentTransHeaderCaption.Visible = false;
                xrtblCurrentTransHeaderCaption.LocationF = new PointF(2F, 0F);
                xrtblClosingHeaderCaption.Visible = false;
                xrtblClosingHeaderCaption.LocationF = new PointF(2F, 0F);

                xrtblOpeningHeaderCaption.Visible = true;
                xrtblOpeningHeaderCaption.LocationF = new PointF(2F, 0F);
                PageHeader.HeightF = 50;
            }
            else if (this.ReportProperties.ShowCurrentTransaction != 0)
            {
                xrtblLedgerHeaderCaption.Visible = false;
                xrtblLedgerHeaderCaption.LocationF = new PointF(2F, 0F);
                xrtblOpeningHeaderCaption.Visible = false;
                xrtblOpeningHeaderCaption.LocationF = new PointF(2F, 0F);
                xrtblClosingHeaderCaption.Visible = false;
                xrtblClosingHeaderCaption.LocationF = new PointF(2F, 0F);

                xrtblCurrentTransHeaderCaption.Visible = true;
                xrtblCurrentTransHeaderCaption.LocationF = new PointF(2F, 0F);
                PageHeader.HeightF = 50;
            }
            else if (this.ReportProperties.ShowCurrentTransaction == 0 && this.ReportProperties.ShowOpeningBalance == 0)
            {
                xrtblLedgerHeaderCaption.Visible = false;
                xrtblLedgerHeaderCaption.LocationF = new PointF(2F, 0F);
                xrtblOpeningHeaderCaption.Visible = false;
                xrtblOpeningHeaderCaption.LocationF = new PointF(2F, 0F);
                xrtblCurrentTransHeaderCaption.Visible = false;
                xrtblCurrentTransHeaderCaption.LocationF = new PointF(2F, 0F);

                xrtblClosingHeaderCaption.Visible = true;
                xrtblClosingHeaderCaption.LocationF = new PointF(2F, 0F);
                PageHeader.HeightF = 50;
            }
        }

        private void ValidateledgerInfo()
        {
            if (this.ReportProperties.ShowOpeningBalance != 0 && this.ReportProperties.ShowCurrentTransaction != 0)
            {
                xrTableOpeningLedger.Visible = false;
                xrTableOpeningLedger.LocationF = new PointF(2F, 0F);
                xrTableCurrentTransLedger.Visible = false;
                xrTableCurrentTransLedger.LocationF = new PointF(2F, 0F);
                xrTableClosingLedger.Visible = false;
                xrTableClosingLedger.LocationF = new PointF(2F, 0F);

                xrTblLedger.Visible = true;
                xrTblLedger.LocationF = new PointF(2F, 0F);
                GrpTrialBalanceLedger.HeightF = 26;
            }
            else if (this.ReportProperties.ShowOpeningBalance != 0)
            {
                xrTblLedger.Visible = false;
                xrTblLedger.LocationF = new PointF(2F, 0F);
                xrTableCurrentTransLedger.Visible = false;
                xrTableCurrentTransLedger.LocationF = new PointF(2F, 0F);
                xrTableClosingLedger.Visible = false;
                xrTableClosingLedger.LocationF = new PointF(2F, 0F);

                xrTableOpeningLedger.Visible = true;
                xrTableOpeningLedger.LocationF = new PointF(2F, 0F);
                GrpTrialBalanceLedger.HeightF = 26;
            }
            else if (this.ReportProperties.ShowCurrentTransaction != 0)
            {
                xrTblLedger.Visible = false;
                xrTblLedger.LocationF = new PointF(2F, 0F);
                xrTableOpeningLedger.Visible = false;
                xrTableOpeningLedger.LocationF = new PointF(2F, 0F);
                xrTableClosingLedger.Visible = false;
                xrTableClosingLedger.LocationF = new PointF(2F, 0F);

                xrTableCurrentTransLedger.Visible = true;
                xrTableCurrentTransLedger.LocationF = new PointF(2F, 0F);
                GrpTrialBalanceLedger.HeightF = 26;
            }
            else if (this.ReportProperties.ShowCurrentTransaction == 0 && this.ReportProperties.ShowOpeningBalance == 0)
            {
                xrTblLedger.Visible = false;
                xrTblLedger.LocationF = new PointF(2F, 0F);
                xrTableOpeningLedger.Visible = false;
                xrTableOpeningLedger.LocationF = new PointF(2F, 0F);
                xrTableCurrentTransLedger.Visible = false;
                xrTableCurrentTransLedger.LocationF = new PointF(2F, 0F);

                xrTableClosingLedger.Visible = true;
                xrTableClosingLedger.LocationF = new PointF(2F, 0F);
                GrpTrialBalanceLedger.HeightF = 26;
            }
        }

        private void ValidateledgerGroupInfo()
        {
            if (this.ReportProperties.ShowOpeningBalance != 0 && this.ReportProperties.ShowCurrentTransaction != 0)
            {
                xrTableOpeningGroup.Visible = false;
                xrTableOpeningGroup.LocationF = new PointF(2F, 0F);
                xrTableCurrentTransGroup.Visible = false;
                xrTableCurrentTransGroup.LocationF = new PointF(2F, 0F);
                xrTableClosingGroup.Visible = false;
                xrTableClosingGroup.LocationF = new PointF(2F, 0F);

                xrTblGroup.Visible = true;
                xrTblGroup.LocationF = new PointF(2F, 0F);
                grpGroupHeader.HeightF = 26;
            }
            else if (this.ReportProperties.ShowOpeningBalance != 0)
            {
                xrTblGroup.Visible = false;
                xrTblGroup.LocationF = new PointF(2F, 0F);
                xrTableCurrentTransGroup.Visible = false;
                xrTableCurrentTransGroup.LocationF = new PointF(2F, 0F);
                xrTableClosingGroup.Visible = false;
                xrTableClosingGroup.LocationF = new PointF(2F, 0F);

                xrTableOpeningGroup.Visible = true;
                xrTableOpeningGroup.LocationF = new PointF(2F, 0F);
                grpGroupHeader.HeightF = 26;
            }
            else if (this.ReportProperties.ShowCurrentTransaction != 0)
            {
                xrTblGroup.Visible = false;
                xrTblGroup.LocationF = new PointF(2F, 0F);
                xrTableOpeningGroup.Visible = false;
                xrTableOpeningGroup.LocationF = new PointF(2F, 0F);
                xrTableClosingGroup.Visible = false;
                xrTableClosingGroup.LocationF = new PointF(2F, 0F);

                xrTableCurrentTransGroup.Visible = true;
                xrTableCurrentTransGroup.LocationF = new PointF(2F, 0F);
                grpGroupHeader.HeightF = 26;
            }
            else if (this.ReportProperties.ShowCurrentTransaction == 0 && this.ReportProperties.ShowOpeningBalance == 0)
            {
                xrTblGroup.Visible = false;
                xrTblGroup.LocationF = new PointF(2F, 0F);
                xrTableOpeningGroup.Visible = false;
                xrTableOpeningGroup.LocationF = new PointF(2F, 0F);
                xrTableCurrentTransGroup.Visible = false;
                xrTableCurrentTransGroup.LocationF = new PointF(2F, 0F);

                xrTableClosingGroup.Visible = true;
                xrTableClosingGroup.LocationF = new PointF(2F, 0F);
                grpGroupHeader.HeightF = 26;
            }
        }

        public override XRTable AlignHeaderTable(XRTable table, bool UseSameFont = false)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }

            if (this.AppSetting.AllowMultiCurrency == 1 && ReportProperties.CurrencyCountryId > 0)
            {
                //later will be updated
            }
            else
            {
                this.SetCurrencyFormat(xrCellHeaderOpeningDebit.Text, xrCellHeaderOpeningDebit);
                this.SetCurrencyFormat(xrCellHeaderOpeningCredit.Text, xrCellHeaderOpeningCredit);
                this.SetCurrencyFormat(xrCellHeaderCurrentTransDebit.Text, xrCellHeaderCurrentTransDebit);
                this.SetCurrencyFormat(xrCellHeaderCurrentTransCredit.Text, xrCellHeaderCurrentTransCredit);
                this.SetCurrencyFormat(xrCellHeaderClosingDebit.Text, xrCellHeaderClosingDebit);
                this.SetCurrencyFormat(xrCellHeaderClosingCredit.Text, xrCellHeaderClosingCredit);

                this.SetCurrencyFormat(xrHeaderOpeningDebit.Text, xrHeaderOpeningDebit);
                this.SetCurrencyFormat(xrHeaderOpeningCredit.Text, xrHeaderOpeningCredit);
                this.SetCurrencyFormat(xrHeaderOpeningClosingDebit.Text, xrHeaderOpeningClosingDebit);
                this.SetCurrencyFormat(xrHeaderOpeningClosingCredit.Text, xrHeaderOpeningClosingCredit);

                this.SetCurrencyFormat(xrHeaderCurrentTransDebit.Text, xrHeaderCurrentTransDebit);
                this.SetCurrencyFormat(xrHeaderCurrentTransCredit.Text, xrHeaderCurrentTransCredit);
                this.SetCurrencyFormat(xrHeaderCurrentTransClosingDebit.Text, xrHeaderCurrentTransClosingDebit);
                this.SetCurrencyFormat(xrHeaderCurrentTransClosingCredit.Text, xrHeaderCurrentTransClosingCredit);

                this.SetCurrencyFormat(xrHeaderClosingDebit.Text, xrHeaderClosingDebit);
                this.SetCurrencyFormat(xrHeaderClosingCredit.Text, xrHeaderClosingCredit);
            }
            return table;
        }

        public ResultArgs GetTrialBalance()
        {
            string TrialBalance = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.TrialBalanceList);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (CurrentFinancialYear > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, YearTo);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, "");
                }
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, DateRangeDFReducing);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, dtClosingDateRangeYear);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                //On 06/12/2024 - To set currency details -----------------------------------------------------------------------------------
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                //---------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, TrialBalance);

                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtTrialBalance = resultArgs.DataSource.TableView.Table;

                    //modified by alwar on 23/01/2016, to avoid looping and calculating manually
                    //Calculate Closing balances by dynamically, if negative, assign to credit side else debit side of closing balance
                    string closingbalanceexpression = "((OP_DEBIT + CURRENTTRANS_DEBIT) - (OP_CREDIT + CURRENTTRANS_CREDIT))";

                    dtTrialBalance.Columns.Add(this.reportSetting1.TrialBalance.CLOSING_DEBITColumn.ColumnName,
                            this.reportSetting1.TrialBalance.CLOSING_DEBITColumn.DataType,
                            "IIF(" + closingbalanceexpression + " < 0, 0," + closingbalanceexpression + ")");

                    dtTrialBalance.Columns.Add(this.reportSetting1.TrialBalance.CLOSING_CREDITColumn.ColumnName,
                        this.reportSetting1.TrialBalance.CLOSING_CREDITColumn.DataType,
                        "IIF(" + closingbalanceexpression + " > 0, 0, - (" + closingbalanceexpression + "))");

                    string filter = "(CLOSING_CREDIT >0 OR CLOSING_DEBIT >0)";

                    if (this.ReportProperties.ShowOpeningBalance == 1)
                        filter += " OR (OP_CREDIT>0 OR OP_DEBIT>0)";

                    if (this.ReportProperties.ShowCurrentTransaction == 1)
                        filter += " OR (CURRENTTRANS_CREDIT>0 OR CURRENTTRANS_DEBIT>0)";

                    dtTrialBalance.DefaultView.RowFilter = filter;
                    resultArgs.DataSource.Data = dtTrialBalance.DefaultView;

                    xrSubreportCCDetails.Visible = false;
                    
                    if (this.ReportProperties.ShowCurrentTransaction == 1 && ReportProperty.Current.ShowCCDetails == 1)
                    {
                        xrSubreportCCDetails.Visible = true;
                        AssignCCDetailReportSource();
                    }
                }

            }
            return resultArgs;
        }

        private void GetTrialBanceExcessAmount()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.Project) && !string.IsNullOrEmpty(this.ReportProperties.DateFrom))
            {
                string trialBalance = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.TrialBalanceExcessDifference);
                using (DataManager dataManager = new DataManager())
                {
                    if (CurrentFinancialYear == 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);

                        //On 06/12/2024 - To set currency details -----------------------------------------------------------------------------------
                        if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                        {
                            dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                        }
                        else
                        {
                            dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                        }
                        //---------------------------------------------------------------------------------------------------------------------------

                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, trialBalance);
                        if (resultArgs != null && resultArgs.Success)
                        {
                            DataTable dtResource = resultArgs.DataSource.Table;
                            ExcessDebitAmount = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["DEBIT"].ToString());
                            ExcessCreditAmount = this.UtilityMember.NumberSet.ToDouble(dtResource.Rows[0]["CREDIT"].ToString());
                        }
                    }
                }
            }
        }

        public int IsFinancialYear()
        {
            string FinancialYear = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IsFirstFinancialYear);
            using (DataManager dataManager = new DataManager())
            {
                // dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, YearFrom);
                // dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, YearTo);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.Scalar, FinancialYear);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public DataTable GetTrialBalanceCurrent()
        {
            string TrialBalanceCurrent = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.TrialBalaceCurrent);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                //On 06/12/2024 - To set currency details -----------------------------------------------------------------------------------
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                //---------------------------------------------------------------------------------------------------------------------------
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TrialBalanceCurrent);
            }
            return resultArgs.DataSource.Table;
        }

        public DataTable GetIncomeExcessAmount()
        {
            string IncomeAmt = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IEReceitpsAmt);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                //dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, dtClosingDateRangeYear); 
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.ReportProperties.DateFrom);

                //On 06/12/2024 - To set currency details -----------------------------------------------------------------------------------
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                //---------------------------------------------------------------------------------------------------------------------------
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, IncomeAmt);
            }
            return resultArgs.DataSource.Table;
        }

        public DataTable GetExpenceExcessAmt()
        {
            string ExpenceAmt = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IEPaymentsAmt);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                //dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, dtClosingDateRangeYear); 
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.ReportProperties.DateFrom);

                //On 06/12/2024 - To set currency details -----------------------------------------------------------------------------------
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                //---------------------------------------------------------------------------------------------------------------------------
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, ExpenceAmt);
            }
            return resultArgs.DataSource.Table;
        }

        private void xrGroupClosingBal_SummaryRowChanged(object sender, EventArgs e)
        {
            // find out Group By LedgerGroup
            // GroupNumber++;
            OPDebit = (GetCurrentColumnValue(reportSetting1.TrialBalance.OP_DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.OP_DEBITColumn.ColumnName).ToString());
            OPCredit = (GetCurrentColumnValue(reportSetting1.TrialBalance.OP_CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.OP_CREDITColumn.ColumnName).ToString());
            CurTransDebit = (GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_DEBITColumn.ColumnName).ToString());
            CurTransCredit = (GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_CREDITColumn.ColumnName).ToString());
            GroupCode = (GetCurrentColumnValue(reportSetting1.TrialBalance.GROUP_CODEColumn.ColumnName) == null) ? string.Empty : GetCurrentColumnValue(reportSetting1.TrialBalance.GROUP_CODEColumn.ColumnName).ToString();
            grpGroupHeader.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            if (grpGroupHeader.Visible == true)
            {
                if ((OPDebit < CurTransCredit) || (OPCredit < CurTransCredit) || (CurTransDebit < CurTransCredit || OPCredit > CurTransCredit))
                {
                    if (!GroupCode.Equals(PrevGroupCode))
                    {
                        xrGroupClosingBalCredit.Text = GrpClosingCredit.ToString();
                        GrpClosingCredit = GrpClosingDebit = 0;
                    }
                    double DebitAddAmount = OPDebit + CurTransDebit;
                    if (DebitAddAmount < (CurTransCredit + OPCredit))
                    {
                        GrpClosingCredit += (CurTransCredit + OPCredit) - (OPDebit + CurTransDebit);
                        xrGroupClosingBalCredit.Text = this.UtilityMember.NumberSet.ToNumber(GrpClosingCredit);
                        //  xrGroupClosingBalDebit.Text = string.Empty; // chinna on 19.08.2019
                    }
                    else
                    {
                        GrpClosingDebit += DebitAddAmount - (CurTransCredit + OPCredit);
                        xrGroupClosingBalDebit.Text = this.UtilityMember.NumberSet.ToNumber(GrpClosingDebit);
                        //  xrGroupClosingBalCredit.Text = string.Empty; // chinna on 19.08.2019

                    }
                    PrevGroupCode = GroupCode;
                }

                else if (OPDebit != 0 || CurTransDebit != 0)
                {
                    if (!GroupCode.Equals(PrevGroupCode))
                    {
                        GrpClosingCredit = GrpClosingDebit = 0;
                    }
                    GrpClosingDebit += (OPDebit + CurTransDebit) - CurTransCredit;
                    xrGroupClosingBalDebit.Text = this.UtilityMember.NumberSet.ToNumber(GrpClosingDebit);
                    // xrGroupClosingBalCredit.Text = string.Empty;  // chinna on 19.08.2019
                }

                else if (OPCredit != 0 || CurTransCredit != 0)
                {
                    if (!GroupCode.Equals(PrevGroupCode))
                    {
                        GrpClosingCredit = GrpClosingDebit = 0;
                    }
                    GrpClosingCredit = (OPCredit + CurTransCredit) - CurTransDebit;
                    xrGroupClosingBalCredit.Text = this.UtilityMember.NumberSet.ToNumber(ClosingBalanceCredit);
                    //xrGroupClosingBalDebit.Text = string.Empty;
                }
                //else
                //{
                //    xrGroupClosingBalDebit.Text = xrGroupClosingBalCredit.Text = string.Empty;
                //}
                OPDebit = OPCredit = CurTransDebit = CurTransCredit = 0;
            }
        }

        // command by chinna
        //private void xrGroupClosingBalCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    // e.Result = GrpClosingCredit;
        //    //e.Handled = true;
        //}

        private void xrtblTrialBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            // To Validate Amount whether is Debit or Credit
            //GroupNumber++;
            //OPDebit = (GetCurrentColumnValue(reportSetting1.TrialBalance.OP_DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.OP_DEBITColumn.ColumnName).ToString());
            //OPCredit = (GetCurrentColumnValue(reportSetting1.TrialBalance.OP_CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.OP_CREDITColumn.ColumnName).ToString());
            //CurTransDebit = (GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_DEBITColumn.ColumnName).ToString());
            //CurTransCredit = (GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.TrialBalance.CURRENTTRANS_CREDITColumn.ColumnName).ToString());
            //if ((OPDebit < CurTransCredit) || (OPCredit < CurTransCredit) || (CurTransDebit < CurTransCredit || OPCredit > CurTransCredit))
            //{
            //    double DebitAddAmount = OPDebit + CurTransDebit;
            //    if (DebitAddAmount < (CurTransCredit + OPCredit))
            //    {
            //        ClosingBalanceCredit = (CurTransCredit + OPCredit) - (OPDebit + CurTransDebit);
            //        xrClosingBalanceCredit.Text = this.UtilityMember.NumberSet.ToNumber(ClosingBalanceCredit);
            //        xrClosingBalanceDebit.Text = string.Empty;
            //        SumofClCredit += ClosingBalanceCredit;
            //        ClosingBalanceCredit = 0;

            //    }
            //    else
            //    {
            //        ClosingBalanceDebit = DebitAddAmount - (CurTransCredit + OPCredit);
            //        xrClosingBalanceDebit.Text = this.UtilityMember.NumberSet.ToNumber(ClosingBalanceDebit);
            //        xrClosingBalanceCredit.Text = string.Empty;
            //        SumofClDebit += ClosingBalanceDebit;
            //        ClosingBalanceDebit = 0;

            //    }
            //}
            //else if (OPDebit != 0 || CurTransDebit != 0)
            //{

            //    ClosingBalanceDebit = (OPDebit + CurTransDebit) - CurTransCredit;
            //    xrClosingBalanceDebit.Text = this.UtilityMember.NumberSet.ToNumber(ClosingBalanceDebit);
            //    xrClosingBalanceCredit.Text = string.Empty;
            //    SumofClDebit += ClosingBalanceDebit;
            //    ClosingBalanceDebit = 0;

            //}

            //else if (OPCredit != 0 || CurTransCredit != 0)
            //{
            //    ClosingBalanceCredit = (OPCredit + CurTransCredit) - CurTransDebit;
            //    xrClosingBalanceCredit.Text = this.UtilityMember.NumberSet.ToNumber(ClosingBalanceCredit);
            //    xrClosingBalanceDebit.Text = string.Empty;
            //    SumofClCredit += ClosingBalanceCredit;
            //    ClosingBalanceCredit = 0;
            //}
            //else
            //{
            //    xrClosingBalanceDebit.Text = xrClosingBalanceCredit.Text = string.Empty;
            //}
            //OPDebit = OPCredit = CurTransDebit = CurTransCredit = 0;
        }

        public void ClosingBalance()
        {
            //  Cash Closing Balance 
            if (OpCashDebit < CashDebitCurrentTotal || CashCreditCurrentTotal < CashDebitCurrentTotal || OpCashCredit < CashCreditCurrentTotal)
            {
                double DebitAddTotal = OpCashDebit + CashCreditCurrentTotal;
                if (DebitAddTotal < (CashDebitCurrentTotal + OpCashCredit))
                {
                    TotalCashClosingBalanceCredit = (CashDebitCurrentTotal + OpCashCredit) - DebitAddTotal;
                    xrClosingCashTotalCredit.Text = this.UtilityMember.NumberSet.ToNumber(TotalCashClosingBalanceCredit);
                    xrClosingCashTotalDebit.Text = string.Empty;
                }
                else
                {
                    TotalCashClosingBalanceDebit = DebitAddTotal - (CashDebitCurrentTotal + OpCashCredit);
                    xrClosingCashTotalDebit.Text = this.UtilityMember.NumberSet.ToNumber(TotalCashClosingBalanceDebit);
                    xrClosingCashTotalCredit.Text = string.Empty;
                }
            }
            else if (OpCashDebit != 0 || CashCreditCurrentTotal != 0)
            {

                TotalCashClosingBalanceDebit = (TotalOpeningCashAmount + CashCreditCurrentTotal) - CashDebitCurrentTotal;
                xrClosingCashTotalDebit.Text = this.UtilityMember.NumberSet.ToNumber(TotalCashClosingBalanceDebit);
                xrClosingCashTotalCredit.Text = string.Empty;
            }
            else // To Make it null values if old Exists 22.02.2019
            {
                xrClosingCashTotalCredit.Text = xrClosingCashTotalDebit.Text = string.Empty;
            }

            //IE Differece Balance




            //  Bank Closing Balance
            if (OpBankDebit < BankAccountDebitTotal || BankAccountCreditTotal < BankAccountDebitTotal || OpBankCredit < BankAccountCreditTotal)
            {
                double BankDebitAddTotal = OpBankDebit + BankAccountCreditTotal;
                if (BankDebitAddTotal < (BankAccountDebitTotal + OpBankCredit))
                {
                    TotalBankClosingBalanceCredit = (BankAccountDebitTotal + OpBankCredit) - BankDebitAddTotal;
                    xrClosingBankCreditTotal.Text = this.UtilityMember.NumberSet.ToNumber(TotalBankClosingBalanceCredit);
                    xrClosingBankDebitTotal.Text = string.Empty;
                }
                else
                {
                    TotalBankClosingBalanceDebit = BankDebitAddTotal - (BankAccountDebitTotal + OpBankCredit);
                    xrClosingBankDebitTotal.Text = this.UtilityMember.NumberSet.ToNumber(TotalBankClosingBalanceDebit);
                    xrClosingBankCreditTotal.Text = string.Empty;
                }
            }
            else if (OpBankDebit != 0 || BankAccountCreditTotal != 0)
            {

                TotalBankClosingBalanceDebit = (OpBankDebit + BankAccountCreditTotal) - BankAccountDebitTotal;
                xrClosingBankDebitTotal.Text = this.UtilityMember.NumberSet.ToNumber(TotalBankClosingBalanceDebit);
                xrClosingBankCreditTotal.Text = string.Empty;
            }
            else // Make it null values if old values Exists 22.02.2019
            {
                xrClosingBankDebitTotal.Text = xrClosingBankCreditTotal.Text = string.Empty;
            }

            //Fixed Depoist Closing banlance


            double ACIInterestAmount = FetchACIBalance();
            if (OpFDDebit < FixedDepositDebitTotal || FixedDepositCreditTotal < FixedDepositDebitTotal || OpFDCredit < FixedDepositCreditTotal)
            {
                double FDDebitAddTotal = OpFDDebit + FixedDepositCreditTotal;
                if (FDDebitAddTotal < (FixedDepositDebitTotal + OpFDCredit))
                {
                    TotalFDClosingBalanceCredit = (FixedDepositDebitTotal + OpFDCredit) - FDDebitAddTotal;
                    // xrClosingFixedDepositCredit.Text = this.UtilityMember.NumberSet.ToNumber(TotalFDClosingBalanceCredit + ACIInterestAmount);
                    //  xrClosingFixedDepositDebit.Text = string.Empty;
                }
                else
                {
                    TotalFDClosingBalanceDebit = FDDebitAddTotal - (FixedDepositDebitTotal + OpFDCredit);
                    //  xrClosingFixedDepositDebit.Text = this.UtilityMember.NumberSet.ToNumber(TotalFDClosingBalanceDebit + ACIInterestAmount);
                    // xrClosingFixedDepositCredit.Text = string.Empty;
                }
            }
            else if (OpFDDebit == 0 && FixedDepositDebitTotal == 0 && FixedDepositCreditTotal == 0 && OpFDCredit == 0)
            {
                // xrClosingFixedDepositCredit.Text = string.Empty;
                //  xrClosingFixedDepositDebit.Text = string.Empty;
            }
            else if (OpFDDebit != 0 || FixedDepositCreditTotal != 0)
            {
                TotalFDClosingBalanceDebit = (OpFDDebit + FixedDepositCreditTotal) - (FixedDepositDebitTotal + OpFDCredit);
                // xrClosingFixedDepositDebit.Text = this.UtilityMember.NumberSet.ToNumber(TotalFDClosingBalanceDebit + ACIInterestAmount);
                // xrClosingFixedDepositCredit.Text = string.Empty;
            }
        }

        // Changed on 11-04-2015
        //private void IncomeExpenceCalculation()
        //{

        //    if (dtTrialBalance != null && dtTrialBalance.Rows.Count > 0)
        //    {
        //        DataTable dt = dtTrialBalance.Copy();
        //        DataView dvContainsCapitalFund = new DataView(dt);
        //        dvContainsCapitalFund.RowFilter = "LEDGER_NAME='Capital Fund'";
        //        DataTable dtLedgerSource = dvContainsCapitalFund.ToTable();
        //        IncomeExpenditureAmountPrevious = ((-IncomeAmt) - (-ExpenceAmt));
        //        if (CurrentFinancialYear == 0)
        //        {
        //            if (!(dtLedgerSource != null && dtLedgerSource.Rows.Count > 0))
        //            {

        //                if (IncomeExpenditureAmountPrevious > 0)
        //                {
        //                    xrtblIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IncomeExpenditureAmountPrevious);
        //                    xrCurIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IncomeExpenditureAmountPrevious);
        //                    xrtblIECredit.Text = string.Empty;
        //                    IEDebit = IncomeExpenditureAmountPrevious;
        //                }
        //                else
        //                {
        //                    double IE = Math.Abs(this.UtilityMember.NumberSet.ToDouble(IncomeExpenditureAmountPrevious.ToString()));
        //                    xrtblIECredit.Text = this.UtilityMember.NumberSet.ToNumber(IE);
        //                    xrCurIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IE);
        //                    xrtblIEDebit.Text = string.Empty;
        //                    IECredit = IE;
        //                }
        //            }

        //            else
        //            {
        //                xrCurIEDebit.Text = string.Empty;
        //                xrtblIECredit.Text = string.Empty;
        //                xrtblIEDebit.Text = string.Empty;
        //                xrtblIECredit.Text = string.Empty;
        //                if ((IncomeExpenditureAmountPrevious >= 0))
        //                {
        //                    double OpDebit = IncomeExpenditureAmountPrevious;
        //                    xrOp_Debit.Text = this.UtilityMember.NumberSet.ToNumber(OpDebit);
        //                }
        //                else
        //                {
        //                    double OpCredit = Math.Abs(IncomeExpenditureAmountPrevious);
        //                    xrOp_Credit.Text = this.UtilityMember.NumberSet.ToNumber(OpCredit);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        IncomeExpenditureAmountPrevious = ((-IncomeAmt) - (-ExpenceAmt));
        //        if (CurrentFinancialYear == 0)
        //        {
        //            if (IncomeExpenditureAmountPrevious > 0)
        //            {
        //                xrtblIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IncomeExpenditureAmountPrevious);
        //                xrCurIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IncomeExpenditureAmountPrevious);
        //                xrtblIECredit.Text = string.Empty;
        //                IEDebit = IncomeExpenditureAmountPrevious;
        //            }
        //            else
        //            {
        //                double IE = Math.Abs(this.UtilityMember.NumberSet.ToDouble(IncomeExpenditureAmountPrevious.ToString()));
        //                xrtblIECredit.Text = this.UtilityMember.NumberSet.ToNumber(IE);
        //                xrCurIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IE);
        //                xrtblIEDebit.Text = string.Empty;
        //                IECredit = IE;
        //            }
        //        }
        //    }

        //}

        //private void IncomeExpenceCalculation()
        //{
        //    // Verified by chinna need to finalized
        //    if (dtTrialBalance != null && dtTrialBalance.Rows.Count > 0)
        //    {
        //        DataTable dt = dtTrialBalance.Copy();
        //        DataView dvContainsCapitalFund = new DataView(dt);
        //        dvContainsCapitalFund.RowFilter = "LEDGER_NAME='Capital Fund'";
        //        DataTable dtLedgerSource = dvContainsCapitalFund.ToTable();
        //        IncomeExpenditureAmountPrevious = ((-IncomeAmt) - (-ExpenceAmt));
        //        if (CurrentFinancialYear == 0)
        //        {
        //            if (!(dtLedgerSource != null && dtLedgerSource.Rows.Count > 0))
        //            {

        //                if (IncomeExpenditureAmountPrevious > 0)
        //                {
        //                    xrtblIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IncomeExpenditureAmountPrevious);
        //                    xrCurIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IncomeExpenditureAmountPrevious);
        //                    xrtblIECredit.Text = string.Empty;
        //                    IEDebit = IncomeExpenditureAmountPrevious;
        //                }
        //                else
        //                {
        //                    double IE = Math.Abs(this.UtilityMember.NumberSet.ToDouble(IncomeExpenditureAmountPrevious.ToString()));
        //                    xrtblIECredit.Text = this.UtilityMember.NumberSet.ToNumber(IE);
        //                    xrCurIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IE);
        //                    xrtblIEDebit.Text = string.Empty;
        //                    IECredit = IE;
        //                }
        //            }

        //            else
        //            {
        //                xrCurIEDebit.Text = string.Empty;
        //                xrtblIECredit.Text = string.Empty;
        //                xrtblIEDebit.Text = string.Empty;
        //                xrtblIECredit.Text = string.Empty;
        //                if ((IncomeExpenditureAmountPrevious >= 0))
        //                {
        //                    double OpDebit = IncomeExpenditureAmountPrevious;
        //                    xrOp_Debit.Text = this.UtilityMember.NumberSet.ToNumber(OpDebit);
        //                }
        //                else
        //                {
        //                    double OpCredit = Math.Abs(IncomeExpenditureAmountPrevious);
        //                    xrOp_Credit.Text = this.UtilityMember.NumberSet.ToNumber(OpCredit);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        IncomeExpenditureAmountPrevious = ((-IncomeAmt) - (-ExpenceAmt));
        //        if (CurrentFinancialYear == 0)
        //        {
        //            if (IncomeExpenditureAmountPrevious > 0)
        //            {
        //                xrtblIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IncomeExpenditureAmountPrevious);
        //                xrCurIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IncomeExpenditureAmountPrevious);
        //                xrtblIECredit.Text = string.Empty;
        //                IEDebit = IncomeExpenditureAmountPrevious;
        //            }
        //            else
        //            {
        //                double IE = Math.Abs(this.UtilityMember.NumberSet.ToDouble(IncomeExpenditureAmountPrevious.ToString()));
        //                xrtblIECredit.Text = this.UtilityMember.NumberSet.ToNumber(IE);
        //                xrCurIEDebit.Text = this.UtilityMember.NumberSet.ToNumber(IE);
        //                xrtblIEDebit.Text = string.Empty;
        //                IECredit = IE;
        //            }
        //        }
        //    }
        //}

        private double FetchACIBalance()
        {
            double ACIInsAmount = 0;
            string FetchACIBalance = this.GetReportSQL(SQL.ReportSQLCommand.Report.FetchACIBalance);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FetchACIBalance);
            }
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                ACIInsAmount = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Table.Compute("SUM(INTEREST_AMOUNT)", "").ToString());
            }
            return ACIInsAmount;
        }


        /// <summary>
        /// COMMENTED FOR THE SORTING ORDER. THE SORTING ORDER IS BASED ON THE NATURE ID (3,4,1,2).
        /// </summary>
        private void SortByLedgerorGroup()
        {
            if (grpGroupHeader.Visible)
            {
                //if (this.ReportProperties.SortByGroup == 0)
                //{
                //    grpGroupHeader.SortingSummary.Enabled = true;
                //    grpGroupHeader.SortingSummary.FieldName = "GROUP_CODE";
                //    grpGroupHeader.SortingSummary.Function = SortingSummaryFunction.Avg;
                //    grpGroupHeader.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                //}
                //else
                //{
                //    grpGroupHeader.SortingSummary.Enabled = true;
                //    grpGroupHeader.SortingSummary.FieldName = "LEDGER_GROUP";
                //    grpGroupHeader.SortingSummary.Function = SortingSummaryFunction.Avg;
                //    grpGroupHeader.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                //}
            }
            if (GrpTrialBalanceLedger.Visible)
            {
                //if (this.ReportProperties.SortByLedger == 0)
                //{
                //    GrpTrialBalanceLedger.SortingSummary.Enabled = true;
                //    GrpTrialBalanceLedger.SortingSummary.FieldName = "LEDGER_CODE";
                //    GrpTrialBalanceLedger.SortingSummary.Function = SortingSummaryFunction.Custom;
                //    GrpTrialBalanceLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                //}
                //else
                //{
                //    GrpTrialBalanceLedger.SortingSummary.Enabled = true;
                //    GrpTrialBalanceLedger.SortingSummary.FieldName = "LEDGER_NAME";
                //    GrpTrialBalanceLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                //    GrpTrialBalanceLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                //}
            }
        }

        /// <summary>
        /// 0 - Both 
        /// 1 - Opening panel alone
        /// 2 - Current panel alone
        /// </summary>
        private void MakeShowOpeningCurrentPanels(bool showpanel, Int32 panel)
        {
            float columwidh = (showpanel ? 128 : 0);

            if (panel == 0 || panel == 2)
            {
                //  xrHeaderCurrent.Visible = xrCellHeaderCurrentTransDebit.Visible = xrCellHeaderCurrentTransCredit.Visible = showpanel;
                //   xrCellHeaderCurrentTransDebit.WidthF = xrCellHeaderCurrentTransCredit.WidthF = columwidh;
                //  xrHeaderCurrent.WidthF = ((128 * 2) + 2);

                xrtblTotalSumDebitCurrentTransAmount.WidthF = xrTotalSumCreditCurrentTransAmount.WidthF = columwidh;

                xrtblCurrentTransCashCredit.WidthF = xrtblCurrentTransBankCredit.WidthF = xrCellExcessCurrentTransCredit.WidthF = DifferCurTransCredit.WidthF = columwidh;
                xrtblCurrentTransCashDebit.WidthF = xrtblCurrentTransBankDebit.WidthF = xrCellExcessCurrentTransDebit.WidthF = DifferCurTransDebit.WidthF = columwidh;

                //xrClosingCashTotalDebit.WidthF = xrClosingBankDebitTotal.WidthF = xrcellExcessClosingDebit.WidthF = DifferClosingDebit.WidthF = xrClosingCashTotalDebit.WidthF = columwidh;

                xrtblTotalSumDebitCurrentTransAmount.Visible = xrTotalSumCreditCurrentTransAmount.Visible = showpanel;
                xrtblCurrentTransCashCredit.Visible = xrtblCurrentTransBankCredit.Visible = xrCellExcessCurrentTransCredit.Visible = DifferCurTransCredit.Visible = showpanel;
                xrtblCurrentTransCashDebit.Visible = xrtblCurrentTransBankDebit.Visible = xrCellExcessCurrentTransDebit.Visible = DifferCurTransDebit.Visible = showpanel;

                //  xrClosingCashTotalDebit.Visible = xrClosingBankDebitTotal.Visible = xrcellExcessClosingDebit.Visible = DifferClosingDebit.Visible = xrClosingCashTotalDebit.Visible = showpanel;

            }

            if (panel == 0 || panel == 1)
            {
                // xrHeaderOpCap.Visible = xrCellHeaderOpeningDebit.Visible = xrCellHeaderOpeningCredit.Visible = showpanel;
                //  xrCellHeaderOpeningDebit.WidthF = xrCellHeaderOpeningCredit.WidthF = columwidh;
                //  xrHeaderOpCap.WidthF = (128 * 2);

                xrtblTotalSumOpeningDebitamt.WidthF = xrtblTotalSumOpeningCreditamount.WidthF = columwidh;
                xrtblCrOpeningTotalCashAmount.WidthF = xrtblCrOpeningBankAmount.WidthF = xrCellOpeningCredit.WidthF = DifferOpCredit.WidthF = columwidh;
                xrtblDrOpeningTotalCashAmount.WidthF = xrtblDrOpeningBankAmount.WidthF = xrCellOpeningDebit.WidthF = diifferOpDebit.WidthF = columwidh;
                xrtblTotalSumOpeningDebitamt.Visible = xrtblTotalSumOpeningCreditamount.Visible = showpanel;

                // xrClosingCashTotalDebit.WidthF = xrClosingBankDebitTotal.WidthF = xrcellExcessClosingDebit.WidthF = DifferClosingDebit.WidthF = xrClosingCashTotalDebit.WidthF = columwidh;

                xrtblCrOpeningTotalCashAmount.Visible = xrtblCrOpeningBankAmount.Visible = xrCellOpeningCredit.Visible = DifferOpCredit.Visible = showpanel;
                xrtblDrOpeningTotalCashAmount.Visible = xrtblDrOpeningBankAmount.Visible = xrCellOpeningDebit.Visible = diifferOpDebit.Visible = showpanel;

                //  xrClosingCashTotalDebit.Visible = xrClosingBankDebitTotal.Visible = xrcellExcessClosingDebit.Visible = DifferClosingDebit.Visible = xrClosingCashTotalDebit.Visible = showpanel;
            }
        }
        #endregion

        #region Events

        private void xrtblTotalOpeningBalanceDebitamt_SummaryReset(object sender, EventArgs e)
        {
            AllTotalOpeningLedgerDebitBalance = 0;
        }

        private void xrtblTotalOpeningBalanceDebitamt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // Opening Balance of Debit
            TotalOpeningCashBankFD = OpCashDebit + OpBankDebit + OpFDDebit + AllTotalOpeningLedgerDebitBalance + IEDebit + ExcessDebitAmount;
            TotalOpeningCashBankFDCredit = OpCashCredit + OpBankCredit + OpFDCredit + AllTotalOpeningLedgerCreditBalance + IECredit + ExcessCreditAmount;
            if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            {
                if (TotalOpeningCashBankFD < TotalOpeningCashBankFDCredit)
                {
                    double Debitamt = TotalOpeningCashBankFDCredit - TotalOpeningCashBankFD;
                    diifferOpDebit.Text = this.UtilityMember.NumberSet.ToNumber(Debitamt);
                    e.Result = this.UtilityMember.NumberSet.ToNumber(Debitamt + TotalOpeningCashBankFD);
                    e.Handled = true;
                }
                else
                {
                    e.Result = TotalOpeningCashBankFD;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = TotalOpeningCashBankFD;
                e.Handled = true;
            }
        }

        private void xrtblTotalOpeningBalanceDebitamt_SummaryRowChanged(object sender, EventArgs e)
        {
            AllTotalOpeningLedgerDebitBalance += (GetCurrentColumnValue("OP_DEBIT") == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("OP_DEBIT").ToString());

        }

        private void xrtblTotalOpeningBalanceCreditamount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // Opening Balance of Credit
            TotalOpeningCashBankFDCredit = OpCashCredit + OpBankCredit + OpFDCredit + AllTotalOpeningLedgerCreditBalance + IECredit + ExcessCreditAmount;
            if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            {
                if (TotalOpeningCashBankFD > TotalOpeningCashBankFDCredit)
                {
                    double Creditamt = TotalOpeningCashBankFD - TotalOpeningCashBankFDCredit;
                    DifferOpCredit.Text = this.UtilityMember.NumberSet.ToNumber(Creditamt);
                    e.Result = this.UtilityMember.NumberSet.ToNumber(Creditamt + TotalOpeningCashBankFDCredit);
                    e.Handled = true;
                }
                else
                {
                    e.Result = TotalOpeningCashBankFDCredit;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = TotalOpeningCashBankFDCredit;
                e.Handled = true;
            }
        }

        private void xrtblTotalOpeningBalanceCreditamount_SummaryReset(object sender, EventArgs e)
        {
            AllTotalOpeningLedgerCreditBalance = 0;
        }

        private void xrtblTotalOpeningBalanceCreditamount_SummaryRowChanged(object sender, EventArgs e)
        {
            AllTotalOpeningLedgerCreditBalance += (GetCurrentColumnValue("OP_CREDIT") == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("OP_CREDIT").ToString());
        }

        private void xrtblDebitCurrent_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalCredit = CashCreditCurrentTotal + BankAccountCreditTotal + FixedDepositCreditTotal;
            e.Result = TotalCredit + SumCurTransDebit;
            e.Handled = true;
        }

        private void xrCreditCurrent_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalDebit = CashDebitCurrentTotal + BankAccountDebitTotal + FixedDepositDebitTotal;
            e.Result = TotalDebit + SumCurTransCredit;
            e.Handled = true;
        }

        private void xrtblClosingBalanceDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //Debit Closing Balance
            double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit + ExcessDebitAmount;
            SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit + ExcessCreditAmount;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            if (SumClosingBalanceDebit != SumClosingBalanceCredit)
            {
                if (SumClosingBalanceDebit < SumClosingBalanceCredit)
                {
                    double DebitClosingBalance = SumClosingBalanceCredit - SumClosingBalanceDebit;
                    e.Result = SumClosingBalanceDebit + DebitClosingBalance;
                    DebitClosingBalance = 0;
                    // SumofClDebit = 0;
                    e.Handled = true;

                }
                else
                {
                    e.Result = SumClosingBalanceDebit;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = SumClosingBalanceDebit;
                e.Handled = true;
            }

        }

        private void xrtblClosingBalanceCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //Credit Closing Balance
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit + ExcessCreditAmount;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            if (SumClosingBalanceCredit != SumClosingBalanceDebit)
            {
                if (SumClosingBalanceCredit < SumClosingBalanceDebit)
                {
                    double CreditClosingBalace = SumClosingBalanceDebit - SumClosingBalanceCredit;
                    e.Result = SumClosingBalanceCredit + CreditClosingBalace;
                    CreditClosingBalace = 0;
                    //SumofClCredit = 0;
                    e.Handled = true;

                }
                else
                {
                    e.Result = SumClosingBalanceCredit;
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = SumClosingBalanceCredit;
                e.Handled = true;
            }

        }

        private void xrOp_Debit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningDebitAmt = this.ReportProperties.NumberSet.ToDouble(xrOp_Debit.Text);
            if (OpeningDebitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOp_Debit.Text = "";
            }
        }

        private void xrOp_Credit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrOp_Credit.Text);
            if (OpeningCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOp_Credit.Text = "";
            }
        }

        private void xrCurTransDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CurTransDebitAmt = this.ReportProperties.NumberSet.ToDouble(xrCurTransDebit.Text);
            if (CurTransDebitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCurTransDebit.Text = "";
            }
        }

        private void xrCurTransCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CurTransCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrCurTransCredit.Text);
            if (CurTransCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCurTransCredit.Text = "";
            }

        }

        private void diifferOpDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            OPDebit = (AllTotalOpeningLedgerDebitBalance + OpCashDebit + OpBankDebit + OpFDDebit + IEDebit + ExcessDebitAmount) - (AllTotalOpeningLedgerCreditBalance + OpCashCredit + OpBankCredit + OpFDCredit + IECredit + ExcessCreditAmount);
            if (OPDebit < 0)
            {
                double OPDebitValue = Math.Abs(this.UtilityMember.NumberSet.ToDouble(OPDebit.ToString()));
                e.Result = this.UtilityMember.NumberSet.ToNumber(OPDebitValue);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void DifferOpCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            OPCredit = (AllTotalOpeningLedgerCreditBalance + OpCashCredit + OpBankCredit + OpFDCredit + IECredit + ExcessCreditAmount) - (AllTotalOpeningLedgerDebitBalance + OpCashDebit + OpBankDebit + OpFDDebit + IEDebit + ExcessDebitAmount);

            double dd1 = (AllTotalOpeningLedgerCreditBalance + OpCashCredit + OpBankCredit + OpFDCredit + IECredit);
            double dd = (AllTotalOpeningLedgerDebitBalance + OpCashDebit + OpBankDebit + OpFDDebit + IEDebit);
            if (OPCredit < 0)
            {
                double OPCreditValue = Math.Abs(this.UtilityMember.NumberSet.ToDouble(OPDebit.ToString()));
                e.Result = this.UtilityMember.NumberSet.ToNumber(OPCreditValue);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void DifferClosingDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // to check
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit + ExcessCreditAmount;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit + ExcessDebitAmount;  // + IEDebit;
            SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            double DebitClosingdiff = SumClosingBalanceDebit - SumClosingBalanceCredit;
            if (DebitClosingdiff < 0)
            {
                double CloseDebit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(DebitClosingdiff.ToString()));
                e.Result = this.UtilityMember.NumberSet.ToNumber(CloseDebit);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }


            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void allfooterDifferece_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit + IEDebit;
            SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            double DebitClosingdiff = SumClosingBalanceDebit - SumClosingBalanceCredit;
            double CreditClosingdiff = SumClosingBalanceCredit - SumClosingBalanceDebit;
            TotalOpeningCashBankFDCredit = OpCashCredit + OpBankCredit + OpFDCredit + AllTotalOpeningLedgerCreditBalance + IECredit;
            TotalOpeningCashBankFD = OpCashDebit + OpBankDebit + OpFDDebit + AllTotalOpeningLedgerDebitBalance + IEDebit;
            if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            {
                // Opening Balance of Debit
                if (TotalOpeningCashBankFD < TotalOpeningCashBankFDCredit)
                {
                    e.Result = "Diff. in Opening Balances";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;

            }

            if (TotalOpeningCashBankFD != TotalOpeningCashBankFDCredit)
            {
                // Opening Balance of Credit
                if (TotalOpeningCashBankFD > TotalOpeningCashBankFDCredit)
                {
                    e.Result = "Diff. in Opening Balances";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;

            }

            DebitClosingdiff = Math.Round(DebitClosingdiff);
            if (DebitClosingdiff < 0)
            {
                //Closing Balance Debit;
                if (DebitClosingdiff != -0.00000000093132257461547852)
                {
                    e.Result = "Diff. in Opening Balances";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

            CreditClosingdiff = Math.Round(CreditClosingdiff);  // Round of the values (-0) or something started

            if (CreditClosingdiff < 0)
            {
                //Closing Balance Credit;
                e.Result = "Diff. in Opening Balances";
                e.Handled = true;
            }
            //else
            //{
            //    e.Result = "";
            //    e.Handled = true;
            //}
        }

        private void xrGroupClosingBal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double xrGroupHeaderAmount = this.ReportProperties.NumberSet.ToDouble(xrGroupClosingBal.Text);
            if (xrGroupHeaderAmount != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrGroupClosingBal.Text = "";
            }
        }

        private void xrtblTrialBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double xrTrialClosing = this.ReportProperties.NumberSet.ToDouble(xrtblTrialBalance.Text);
            if (xrTrialClosing != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrtblTrialBalance.Text = "";
            }
        }

        private void xrlblCostCenter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void DifferClosingCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            // to check
            double TotalCashBankFdClosingBalCredit = TotalCashClosingBalanceCredit + TotalBankClosingBalanceCredit + TotalFDClosingBalanceCredit + ExcessCreditAmount;
            SumClosingBalanceCredit = TotalCashBankFdClosingBalCredit + SumofClCredit;
            double TotalCashBankFdClosingBalDebit = TotalCashClosingBalanceDebit + TotalBankClosingBalanceDebit + TotalFDClosingBalanceDebit + ExcessDebitAmount; //+ IEDebit;
            SumClosingBalanceDebit = TotalCashBankFdClosingBalDebit + SumofClDebit;
            double CreditClosingdiff = SumClosingBalanceCredit - SumClosingBalanceDebit;
            if (CreditClosingdiff < 0)
            {
                double CloseCredit = Math.Abs(this.UtilityMember.NumberSet.ToDouble(CreditClosingdiff.ToString()));
                e.Result = this.UtilityMember.NumberSet.ToNumber(CloseCredit);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrClosingBalanceDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ClosingCurDebit = this.ReportProperties.NumberSet.ToDouble(xrClosingBalanceDebit.Text);
            if (ClosingCurDebit != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrClosingBalanceDebit.Text = "";
            }
        }

        private void xrClosingBalanceCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CurClosingCredit = this.ReportProperties.NumberSet.ToDouble(xrClosingBalanceCredit.Text);
            if (CurClosingCredit != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrClosingBalanceCredit.Text = "";
            }
        }

        private void xrCellExcess_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > ExcessCreditAmount)
            {
                e.Result = "Excess of Expenditure over Income";
                e.Handled = true;
            }
            else if (ExcessCreditAmount > ExcessDebitAmount)
            {
                e.Result = "Excess of Income Over Expenditure";
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrCellDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessDebitAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }

        }

        private void xrCellCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessCreditAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrcellExcessClosingDebit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessDebitAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessDebitAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrCellExcessClosingCredit_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessCreditAmount > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessCreditAmount);
                e.Handled = true;

                double ZeroValue = this.UtilityMember.NumberSet.ToDouble(e.Result.ToString());
                if (ZeroValue == 0.00)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }
        private void xrOpeningDR_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningDebitAmt = this.ReportProperties.NumberSet.ToDouble(xrOpeningDR.Text);
            if (OpeningDebitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOpeningDR.Text = "";
            }
        }

        private void xrOpeningCR_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrOpeningCR.Text);
            if (OpeningCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOpeningCR.Text = "";
            }
        }

        private void xrTableOPClosingCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningclosingCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrTableOPClosingCredit.Text);
            if (OpeningclosingCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableOPClosingCredit.Text = "";
            }
        }

        private void xrTableOPClosingDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningclosingDebitAmt = this.ReportProperties.NumberSet.ToDouble(xrTableOPClosingDebit.Text);
            if (OpeningclosingDebitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableOPClosingDebit.Text = "";
            }

        }

        private void xrTableCTransDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningTransDebitAmt = this.ReportProperties.NumberSet.ToDouble(xrTableCTransDebit.Text);
            if (OpeningTransDebitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCTransDebit.Text = "";
            }
        }

        private void xrTableCTransCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningTransCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrTableCTransCredit.Text);
            if (OpeningTransCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCTransCredit.Text = "";
            }
        }

        private void xrTableCTransClosingDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningClosingTransCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrTableCTransClosingDebit.Text);
            if (OpeningClosingTransCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCTransClosingDebit.Text = "";
            }
        }

        private void xrTableCTransClosingCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningTransClosingTransCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrTableCTransClosingCredit.Text);
            if (OpeningTransClosingTransCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCTransClosingCredit.Text = "";
            }
        }

        private void xrTableClosingDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double OpeningClosingDebitAmt = this.ReportProperties.NumberSet.ToDouble(xrTableClosingDebit.Text);
            if (OpeningClosingDebitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableClosingDebit.Text = "";
            }
        }

        private void xrTableClosingCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ClosingCreditAmt = this.ReportProperties.NumberSet.ToDouble(xrTableClosingCredit.Text);
            if (ClosingCreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableClosingCredit.Text = "";
            }
        }

        private void SetColor(XRTable table)
        {
            foreach (XRTableRow row in table.Rows)
            {
                foreach (XRTableCell cell in row.Cells)
                {
                    cell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
        }

        #endregion

        private void TrialBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //// xrtblLedgerHeaderCaption.BeginInit();
            //xrtblbalance.BeginInit();
            //xrtblTotal.BeginInit();
            //MakeShowOpeningCurrentPanels(true, 0);

            //if (this.ReportProperties.ShowOpeningBalance != 0 && this.ReportProperties.ShowCurrentTransaction != 0) //by default
            //{
            //    // xrHeaderOpCap.Text = xrHeaderOpCap.Text.Trim();
            //    xrtblbalance.Visible = true;
            //    //xrtblLedgerHeaderCaption.Visible = true;
            //    xrCellExcess.WidthF = xrCellallfooterDifferece.WidthF = xrTblClLedgerCode.WidthF + xrTblClLedger.WidthF;
            //    xrTableCellCash.WidthF = xrTableCellBank.WidthF = xrTotalSumCaptions.WidthF = xrTblClLedger.WidthF; //= xrHeaderLedgerName.WidthF = xrHeaderParticularEmpty.WidthF =

            //    xrClosingCashTotalDebit.WidthF = xrClosingBankDebitTotal.WidthF = xrcellExcessClosingDebit.WidthF = DifferClosingDebit.WidthF = xrTotalSumClosingDebitAmount.WidthF = xrTableClosingDebit.Width + 10;
            //    xrClosingCashTotalDebit.Visible = xrClosingBankDebitTotal.Visible = xrcellExcessClosingDebit.Visible = DifferClosingDebit.Visible = xrTotalSumClosingDebitAmount.Visible = xrClosingCashTotalDebit.Visible = true;

            //}
            //else if (this.ReportProperties.ShowOpeningBalance == 1) //Hide Current Panel
            //{
            //    MakeShowOpeningCurrentPanels(false, 2);

            //    xrtblCrOpeningTotalCashAmount.WidthF = xrtblCrOpeningBankAmount.WidthF = xrCellOpeningCredit.WidthF = xrCellOpeningDebit.WidthF = xrtblTotalSumOpeningCreditamount.WidthF = 128 * 3;
            //    xrtblDrOpeningTotalCashAmount.WidthF = xrtblDrOpeningBankAmount.WidthF = diifferOpDebit.WidthF = DifferOpCredit.WidthF = xrtblTotalSumOpeningDebitamt.WidthF = 128 * 3;

            //    // xrHeaderLedgerName.WidthF = xrHeaderParticularEmpty.WidthF = xrOpeningLedgerName.WidthF + 2;
            //    // xrHeaderOpCap.Text = xrHeaderOpCap.Text.PadLeft(82);
            //    // xrHeaderOpCap.WidthF = (128 * 4);
            //    // xrCellHeaderOpeningDebit.WidthF = (128 * 3);
            //    //  xrCellHeaderOpeningCredit.WidthF = (128 * 3);

            //    //xrHeaderOpCap.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //    //xrCellHeaderOpeningDebit.LeftF = xrCellHeaderOpeningCredit.LeftF = xrHeaderOpCap.LeftF=  0;
            //    xrTotalCodeCaption.WidthF = xrTableCellCashCode.WidthF = xrTableCellCodeBank.WidthF = xrTotalCodeCaption.WidthF + 2;
            //    xrCellExcess.WidthF = xrCellallfooterDifferece.WidthF = xrOpeningLedgerCode.WidthF + xrOpeningLedgerName.WidthF;
            //    xrTableCellCash.WidthF = xrTableCellBank.WidthF = xrTotalSumCaptions.WidthF = xrOpeningLedgerName.WidthF + 2;

            //    xrClosingCashTotalDebit.WidthF = xrClosingBankDebitTotal.WidthF = xrcellExcessClosingDebit.WidthF = DifferClosingDebit.WidthF = xrClosingCashTotalDebit.WidthF = xrTotalSumClosingDebitAmount.WidthF = xrTableClosingDebit.WidthF + 10;
            //    xrClosingCashTotalDebit.Visible = xrClosingBankDebitTotal.Visible = xrcellExcessClosingDebit.Visible = DifferClosingDebit.Visible = xrClosingCashTotalDebit.Visible = xrTotalSumClosingDebitAmount.Visible = xrClosingCashTotalDebit.Visible = true;
            //}
            //else if (this.ReportProperties.ShowCurrentTransaction == 1) //Hide Opening Panel
            //{
            //    MakeShowOpeningCurrentPanels(false, 1);

            //    //  xrHeaderLedgerName.WidthF = xrCurrentLedgerName.WidthF;
            //    xrCellExcess.WidthF = xrCellallfooterDifferece.WidthF = xrCurrentLedgerCode.WidthF + xrCurrentLedgerName.WidthF;
            //    xrTableCellCash.WidthF = xrTableCellBank.WidthF = xrTotalSumCaptions.WidthF = xrCurrentLedgerName.WidthF + 2;

            //    xrClosingCashTotalDebit.WidthF = xrClosingBankDebitTotal.WidthF = xrcellExcessClosingDebit.WidthF = DifferClosingDebit.WidthF = xrClosingCashTotalDebit.WidthF = xrTotalSumClosingDebitAmount.WidthF = xrTableClosingDebit.WidthF + 10;
            //    xrClosingCashTotalDebit.Visible = xrClosingBankDebitTotal.Visible = xrcellExcessClosingDebit.Visible = DifferClosingDebit.Visible = xrClosingCashTotalDebit.Visible = xrTotalSumClosingDebitAmount.Visible = xrClosingCashTotalDebit.Visible = true;
            //    //xrHeaderLedgerName.WidthF = xrHeaderParticularEmpty.WidthF = xrOpeningLedgerName.WidthF;
            //    //  xrHeaderLedgerName.WidthF = xrHeaderParticularEmpty.WidthF = xrCurrentLedgerName.WidthF + 2;
            //    // xrHeaderCurrent.WidthF = ((128 * 2) + 2);
            //    //xrCellHeaderCurrentTransDebit.WidthF = (128 * 1);
            //    // xrCellHeaderCurrentTransCredit.WidthF = (128 * 1);
            //    //  xrCellHeaderCurrentTransDebit.LeftF = xrCellHeaderCurrentTransCredit.LeftF = 0;

            //}
            //else //Hide Both (Opening, Current) Panel
            //{
            //    MakeShowOpeningCurrentPanels(false, 0);

            //    //xrHeaderLedgerName.WidthF = xrClosingLedgerName.WidthF;
            //    // xrHeaderParticularEmpty.WidthF = xrClosingLedgerName.WidthF;
            //    xrCellExcess.WidthF = xrCellallfooterDifferece.WidthF = xrClosingLedgerCode.WidthF + xrClosingLedgerName.WidthF - 2;
            //    xrTableCellCash.WidthF = xrTableCellBank.WidthF = xrTotalSumCaptions.WidthF = xrClosingLedgerName.WidthF;
            //    xrClosingCashTotalDebit.WidthF = xrClosingBankDebitTotal.WidthF = xrcellExcessClosingDebit.WidthF = DifferClosingDebit.WidthF = xrTotalSumClosingDebitAmount.WidthF = xrTableClosingDebit.WidthF + 15;
            //    xrClosingCashTotalDebit.Visible = xrClosingBankDebitTotal.Visible = xrcellExcessClosingDebit.Visible = DifferClosingDebit.Visible = xrTotalSumClosingDebitAmount.Visible = true;
            //}

            //xrtblbalance.ResumeLayout();
            //xrtblTotal.ResumeLayout();
            //// xrtblLedgerHeaderCaption.ResumeLayout();

        }

        private void HideCashBankPanel()
        {
            //Remove Opening
            if (this.ReportProperties.ShowOpeningBalance == 0)
            {
                xrtblbalance.SuspendLayout();
                if (xrCashRow.Cells.Contains(xrtblDrOpeningTotalCashAmount))
                    xrCashRow.Cells.Remove(xrCashRow.Cells[xrtblDrOpeningTotalCashAmount.Name]);
                if (xrCashRow.Cells.Contains(xrtblCrOpeningTotalCashAmount))
                    xrCashRow.Cells.Remove(xrCashRow.Cells[xrtblCrOpeningTotalCashAmount.Name]);

                if (xrBankRow.Cells.Contains(xrtblDrOpeningBankAmount))
                    xrBankRow.Cells.Remove(xrBankRow.Cells[xrtblDrOpeningBankAmount.Name]);
                if (xrBankRow.Cells.Contains(xrtblCrOpeningBankAmount))
                    xrBankRow.Cells.Remove(xrBankRow.Cells[xrtblCrOpeningBankAmount.Name]);

                if (xrDiffIERow.Cells.Contains(xrCellOpeningDebit))
                    xrDiffIERow.Cells.Remove(xrDiffIERow.Cells[xrCellOpeningDebit.Name]);
                if (xrDiffIERow.Cells.Contains(xrCellOpeningCredit))
                    xrDiffIERow.Cells.Remove(xrDiffIERow.Cells[xrCellOpeningCredit.Name]);

                if (xrDiffOPRow.Cells.Contains(diifferOpDebit))
                    xrDiffOPRow.Cells.Remove(xrDiffOPRow.Cells[diifferOpDebit.Name]);
                if (xrDiffOPRow.Cells.Contains(DifferOpCredit))
                    xrDiffOPRow.Cells.Remove(xrDiffOPRow.Cells[DifferOpCredit.Name]);
                xrtblbalance.PerformLayout();

                xrtblTotal.ResumeLayout();
                xrtblTotal.SuspendLayout();
                if (xrDiffTotalRow.Cells.Contains(xrtblTotalSumOpeningDebitamt))
                    xrDiffTotalRow.Cells.Remove(xrDiffTotalRow.Cells[xrtblTotalSumOpeningDebitamt.Name]);
                if (xrDiffTotalRow.Cells.Contains(xrtblTotalSumOpeningCreditamount))
                    xrDiffTotalRow.Cells.Remove(xrDiffTotalRow.Cells[xrtblTotalSumOpeningCreditamount.Name]);
                xrtblTotal.PerformLayout();
            }

            //Remove Current
            if (this.ReportProperties.ShowCurrentTransaction == 0)
            {
                xrtblbalance.SuspendLayout();
                if (xrCashRow.Cells.Contains(xrtblCurrentTransCashDebit))
                    xrCashRow.Cells.Remove(xrCashRow.Cells[xrtblCurrentTransCashDebit.Name]);
                if (xrCashRow.Cells.Contains(xrtblCurrentTransCashCredit))
                    xrCashRow.Cells.Remove(xrCashRow.Cells[xrtblCurrentTransCashCredit.Name]);

                if (xrBankRow.Cells.Contains(xrtblCurrentTransBankDebit))
                    xrBankRow.Cells.Remove(xrBankRow.Cells[xrtblCurrentTransBankDebit.Name]);
                if (xrBankRow.Cells.Contains(xrtblCurrentTransBankCredit))
                    xrBankRow.Cells.Remove(xrBankRow.Cells[xrtblCurrentTransBankCredit.Name]);

                if (xrDiffIERow.Cells.Contains(xrCellExcessCurrentTransDebit))
                    xrDiffIERow.Cells.Remove(xrDiffIERow.Cells[xrCellExcessCurrentTransDebit.Name]);
                if (xrDiffIERow.Cells.Contains(xrCellExcessCurrentTransCredit))
                    xrDiffIERow.Cells.Remove(xrDiffIERow.Cells[xrCellExcessCurrentTransCredit.Name]);

                if (xrDiffOPRow.Cells.Contains(DifferCurTransDebit))
                    xrDiffOPRow.Cells.Remove(xrDiffOPRow.Cells[DifferCurTransDebit.Name]);
                if (xrDiffOPRow.Cells.Contains(DifferCurTransCredit))
                    xrDiffOPRow.Cells.Remove(xrDiffOPRow.Cells[DifferCurTransCredit.Name]);
                xrtblbalance.PerformLayout();

                xrtblTotal.SuspendLayout();
                if (xrDiffTotalRow.Cells.Contains(xrtblTotalSumDebitCurrentTransAmount))
                    xrDiffTotalRow.Cells.Remove(xrDiffTotalRow.Cells[xrtblTotalSumDebitCurrentTransAmount.Name]);
                if (xrDiffTotalRow.Cells.Contains(xrTotalSumCreditCurrentTransAmount))
                    xrDiffTotalRow.Cells.Remove(xrDiffTotalRow.Cells[xrTotalSumCreditCurrentTransAmount.Name]);
                xrtblTotal.PerformLayout();
            }

            if (this.ReportProperties.ShowOpeningBalance == 1)
            {
                FixOpeningClosingWidth();
            }
            else if (this.ReportProperties.ShowCurrentTransaction == 1)
            {
                FixCurrentClosingWidth();
            }
            else
            {
                FixClosingWidth();
            }

        }

        private void FixClosingWidth()
        {
            //Fix Closing Width
            xrtblbalance.WidthF = xrtblTotal.WidthF = xrTableClosingLedger.WidthF + 18;
            xrtblbalance.LeftF = xrtblTotal.LeftF = xrTableClosingLedger.LeftF;
            xrTableCellCashCode.WidthF = xrClosingLedgerCode.WidthF;
            xrTableCellCodeBank.WidthF = xrClosingLedgerCode.WidthF;
            xrTotalCodeCaption.WidthF = xrClosingLedgerCode.WidthF;

            xrTableCellCash.WidthF = xrClosingLedgerName.WidthF;
            xrTableCellBank.WidthF = xrClosingLedgerName.WidthF;
            xrTotalSumCaptions.WidthF = xrClosingLedgerName.WidthF;
            xrCellExcess.WidthF = xrTableCellCodeBank.WidthF + xrTableCellBank.WidthF;
            xrCellallfooterDifferece.WidthF = xrTableCellCodeBank.WidthF + xrTableCellBank.WidthF;

            xrClosingCashTotalDebit.WidthF = xrTableClosingDebit.WidthF;
            xrClosingCashTotalCredit.WidthF = xrTableClosingDebit.WidthF;
            xrClosingBankDebitTotal.WidthF = xrTableClosingCredit.WidthF;
            xrClosingBankCreditTotal.WidthF = xrTableClosingCredit.WidthF;
            xrcellExcessClosingDebit.WidthF = xrTableClosingCredit.WidthF;
            xrCellExcessClosingCredit.WidthF = xrTableClosingCredit.WidthF;
            DifferClosingDebit.WidthF = xrTableClosingCredit.WidthF;
            DifferClosingCredit.WidthF = xrTableClosingCredit.WidthF;
            xrTotalSumClosingDebitAmount.WidthF = xrTableClosingDebit.WidthF;
            xrTotalSumClosingCreditAmount.WidthF = xrTableClosingCredit.WidthF;
        }

        private void FixOpeningClosingWidth()
        {
            //Fix Closing Width
            xrtblbalance.WidthF = xrtblTotal.WidthF = xrTableOpeningLedger.WidthF;
            xrtblbalance.LeftF = xrtblTotal.LeftF = xrTableOpeningLedger.LeftF;
            xrTableCellCashCode.WidthF = xrOpeningLedgerCode.WidthF;
            xrTableCellCodeBank.WidthF = xrOpeningLedgerCode.WidthF;
            xrTotalCodeCaption.WidthF = xrOpeningLedgerCode.WidthF;

            xrTableCellCash.WidthF = xrOpeningLedgerName.WidthF;
            xrTableCellBank.WidthF = xrOpeningLedgerName.WidthF;
            xrTotalSumCaptions.WidthF = xrOpeningLedgerName.WidthF;
            xrCellExcess.WidthF = xrOpeningLedgerCode.WidthF + xrOpeningLedgerName.WidthF;
            xrCellallfooterDifferece.WidthF = xrOpeningLedgerCode.WidthF + xrOpeningLedgerName.WidthF;

            if (xrCashRow.Cells.Contains(xrtblDrOpeningTotalCashAmount))
            {
                xrtblDrOpeningTotalCashAmount.WidthF = xrOpeningDR.WidthF;
                xrtblDrOpeningBankAmount.WidthF = xrOpeningDR.WidthF;
                xrCellOpeningDebit.WidthF = xrOpeningDR.WidthF;
                diifferOpDebit.WidthF = xrOpeningDR.WidthF;
                xrtblTotalSumOpeningDebitamt.WidthF = xrOpeningDR.WidthF;
                xrtblCrOpeningTotalCashAmount.WidthF = xrOpeningCR.WidthF;
                xrtblCrOpeningBankAmount.WidthF = xrOpeningCR.WidthF;
                xrCellOpeningCredit.WidthF = xrOpeningCR.WidthF;
                DifferOpCredit.WidthF = xrOpeningCR.WidthF;
                xrtblTotalSumOpeningCreditamount.WidthF = xrOpeningDR.WidthF;
            }

            xrClosingCashTotalDebit.WidthF = xrTableOPClosingDebit.WidthF;
            xrClosingCashTotalCredit.WidthF = xrTableOPClosingCredit.WidthF;
            xrClosingBankDebitTotal.WidthF = xrTableOPClosingDebit.WidthF;
            xrClosingBankCreditTotal.WidthF = xrTableOPClosingCredit.WidthF;
            xrcellExcessClosingDebit.WidthF = xrTableOPClosingDebit.WidthF;
            xrCellExcessClosingCredit.WidthF = xrTableOPClosingCredit.WidthF;
            DifferClosingDebit.WidthF = xrTableOPClosingDebit.WidthF;
            DifferClosingCredit.WidthF = xrTableOPClosingCredit.WidthF;
            xrTotalSumClosingDebitAmount.WidthF = xrTableOPClosingDebit.WidthF;
            xrTotalSumClosingCreditAmount.WidthF = xrTableOPClosingCredit.WidthF;
        }

        private void FixCurrentClosingWidth()
        {
            //Fix Closing Width
            xrtblbalance.WidthF = xrtblTotal.WidthF = xrTableCurrentTransLedger.WidthF;
            xrtblbalance.LeftF = xrtblTotal.LeftF = xrTableCurrentTransLedger.LeftF;
            xrTableCellCashCode.WidthF = xrCurrentLedgerCode.WidthF;
            xrTableCellCodeBank.WidthF = xrCurrentLedgerCode.WidthF;
            xrTotalCodeCaption.WidthF = xrCurrentLedgerCode.WidthF;

            xrTableCellCash.WidthF = xrCurrentLedgerName.WidthF;
            xrTableCellBank.WidthF = xrCurrentLedgerName.WidthF;
            xrTotalSumCaptions.WidthF = xrCurrentLedgerName.WidthF;
            xrCellExcess.WidthF = xrCurrentLedgerCode.WidthF + xrCurrentLedgerName.WidthF;
            xrCellallfooterDifferece.WidthF = xrCurrentLedgerCode.WidthF + xrCurrentLedgerName.WidthF;

            if (xrCashRow.Cells.Contains(xrtblCurrentTransCashDebit))
            {
                xrtblCurrentTransCashDebit.WidthF = xrTableCTransDebit.WidthF;
                xrtblCurrentTransBankDebit.WidthF = xrTableCTransDebit.WidthF;
                xrCellExcessCurrentTransDebit.WidthF = xrTableCTransDebit.WidthF;
                DifferCurTransDebit.WidthF = xrTableCTransDebit.WidthF;
                xrtblTotalSumDebitCurrentTransAmount.WidthF = xrTableCTransDebit.WidthF;
                xrtblCurrentTransCashCredit.WidthF = xrTableCTransCredit.WidthF;
                xrtblCurrentTransBankCredit.WidthF = xrTableCTransCredit.WidthF;
                xrCellExcessCurrentTransCredit.WidthF = xrTableCTransCredit.WidthF;
                DifferCurTransCredit.WidthF = xrTableCTransCredit.WidthF;
                xrTotalSumCreditCurrentTransAmount.WidthF = xrTableCTransCredit.WidthF;
            }

            xrClosingCashTotalDebit.WidthF = xrTableCTransClosingDebit.WidthF;
            xrClosingCashTotalCredit.WidthF = xrTableCTransClosingCredit.WidthF;
            xrClosingBankDebitTotal.WidthF = xrTableCTransClosingDebit.WidthF;
            xrClosingBankCreditTotal.WidthF = xrTableCTransClosingCredit.WidthF;
            xrcellExcessClosingDebit.WidthF = xrTableCTransClosingDebit.WidthF;
            xrCellExcessClosingCredit.WidthF = xrTableCTransClosingCredit.WidthF;
            DifferClosingDebit.WidthF = xrTableCTransClosingDebit.WidthF;
            DifferClosingCredit.WidthF = xrTableCTransClosingCredit.WidthF;
            xrTotalSumClosingDebitAmount.WidthF = xrTableCTransClosingDebit.WidthF;
            xrTotalSumClosingCreditAmount.WidthF = xrTableCTransClosingCredit.WidthF;
        }

        /// <summary>
        /// On 06/10/2021, to get CC deatils for given project and date range
        /// </summary>
        private void AssignCCDetailReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlccDetail = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CCDetail);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                //On 09/12/2024, To set Currnecy
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlccDetail);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtCCDetails = resultArgs.DataSource.Table;

                //dtCCDetails.Columns[reportSetting1.Ledger.AMOUNT_PERIODColumn.ColumnName].ColumnName = reportSetting1.Ledger.DEBITColumn.ColumnName;

            }
        }

        private void ShowCCDetails()
        {
            xrSubreportCCDetails.Visible = false;
            //On 06/10/2021, To show CC detail for given Ledger
            if (this.ReportProperties.ShowCurrentTransaction == 1 && this.ReportProperties.ShowCCDetails == 1)
            {
                xrSubreportCCDetails.Visible = false;
                
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null && dtCCDetails.Rows.Count > 0)
                {
                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName).ToString());
                    UcCCDetail ccDetail = xrSubreportCCDetails.ReportSource as UcCCDetail;
                    dtCCDetails.DefaultView.RowFilter = string.Empty;
                    dtCCDetails.DefaultView.RowFilter = reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName + " = " + ledgerid;
                    DataTable dtCC = dtCCDetails.DefaultView.ToTable();

                    ccDetail.BindCCDetails(dtCC, true, false, false, true); //ccDetail.BindCCDetails(dtCC, true);
                    ccDetail.DateWidth = 0;
                    float ccnamewidth = (this.ReportProperties.ShowOpeningBalance == 1 ?
                                        xrTblClLedger.WidthF + xrOp_Debit.WidthF + xrOp_Credit.WidthF :
                                        xrCurrentLedgerName.WidthF);
                    float ccdebitwidth = (this.ReportProperties.ShowOpeningBalance == 1 ? xrOp_Debit.WidthF : xrCurTransDebit.WidthF);
                    float cccreditwidth = (this.ReportProperties.ShowOpeningBalance == 1 ? xrOp_Credit.WidthF : xrCurTransCredit.WidthF);

                    xrSubreportCCDetails.TopF = (this.ReportProperties.ShowOpeningBalance == 1 ? xrTblLedger.HeightF : xrTableCurrentTransLedger.HeightF);
                    xrSubreportCCDetails.LeftF = (this.ReportProperties.ShowOpeningBalance == 1 ? xrTblClLedgerCode.WidthF : xrCurrentLedgerCode.WidthF); //-2;
                    ccDetail.CCCreditCaption = "";
                    ccDetail.CCDebitCaption = "";
                    ccDetail.CCTableWidth = (ccnamewidth + ccdebitwidth + cccreditwidth);
                    ccDetail.CCNameWidth = ccnamewidth;
                    ccDetail.CCDebitWidth = ccdebitwidth;
                    ccDetail.CCCreditWidth = cccreditwidth;
                    
                    ccDetail.PRojectNameWidth = (ccnamewidth + ccdebitwidth + cccreditwidth);
                    ccDetail.HideReportHeaderFooter();
                    dtCCDetails.DefaultView.RowFilter = string.Empty;

                    ProperBorderForLedgerRow(PrevLedgerCCFound);
                    xrSubreportCCDetails.Visible = (dtCC.Rows.Count > 0);
                    PrevLedgerCCFound = (dtCC.Rows.Count > 0);
                }
                else
                {
                    xrSubreportCCDetails.Visible = false;
                    ProperBorderForLedgerRow(false);
                    PrevLedgerCCFound = false;
                }
            }
            else
            {
                xrSubreportCCDetails.Visible = false;
                if (GrpTrialBalanceLedger.FindControl(xrSubreportCCDetails.Name,true)!=null)
                {
                    GrpTrialBalanceLedger.Controls.Remove(xrSubreportCCDetails);
                }
                PrevLedgerCCFound = false;
            }
            
            if (!xrSubreportCCDetails.Visible)
            {
                if (GrpTrialBalanceLedger.Controls.Contains(xrSubreportCCDetails))
                {
                    GrpTrialBalanceLedger.Controls.Remove(xrSubreportCCDetails);
                }
            }
            else
            {
                GrpTrialBalanceLedger.Controls.Add(xrSubreportCCDetails);
            }

            GrpTrialBalanceLedger.HeightF = 26;
        }

        private void ProperBorderForLedgerRow(bool ccFound)
        {
            if (ccFound)
            {
                if (this.ReportProperties.ShowOpeningBalance == 1)
                {
                    xrTblClLedgerCode.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrTblClLedger.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    xrOp_Debit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    xrOp_Credit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    xrCurTransDebit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    xrCurTransCredit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    xrClosingBalanceDebit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    xrClosingBalanceCredit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    
                }
                else if (this.ReportProperties.ShowCurrentTransaction == 1)
                {
                    xrCurrentLedgerCode.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrCurrentLedgerName.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrTableCTransCredit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    xrTableCTransDebit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    xrTableCTransClosingDebit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    xrTableCTransClosingCredit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                }
            }
            else
            {
                if (this.ReportProperties.ShowOpeningBalance == 1)
                {
                    xrTblClLedgerCode.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrTblClLedger.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrOp_Debit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrOp_Credit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrCurTransDebit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrCurTransCredit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrClosingBalanceDebit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrClosingBalanceCredit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                }
                else if (this.ReportProperties.ShowCurrentTransaction == 1)
                {
                    xrCurrentLedgerCode.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrCurrentLedgerName.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrTableCTransCredit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrTableCTransDebit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    xrTableCTransClosingDebit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom; ;
                    xrTableCTransClosingCredit.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom; ;
                }
                
            }
        }

        private void GrpTrialBalanceLedger_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ShowCCDetails();
        }
    }
}
