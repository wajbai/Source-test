using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraPrinting.Localization;

namespace Bosco.Report.ReportObject
{
    public partial class Ledger : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public Ledger()
        {
            InitializeComponent();

            CapCredit = xrCapCredit.Text;
            CapDebit = xrCapDebit.Text;
            CapClosingBalance = xrCapclosingBalance.Text;

            this.AttachDrillDownToRecord(xrtblBindSource, xrParticulars,
                   new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE", true);
        }
        #endregion

        #region Variables
        bool hasNarration = false;
        double LedgerDebit = 0;
        double LedgerCredit = 0;
        double LedgerDebitSum = 0;
        int MonthlyGroupNumber = 0;
        double GroupDetbitSum = 0;
        double GroupCreditSum = 0;
        double MonthlyOpeningBalance = 0;
        double MonthlyClosingBalance = 0;
        double MonthlyPriousClBalance = 0;
        int ResetMonthTotal = 0;
        int OpeningBalReset = 0;

        int TOCPageOffset = 1;  // 14/05/2025, started from first page consider as 1 after 2 onwards *Chinna

        double MonthlyCumulativeBalance = 0;
        double LedgerSummaryCrAmtWithLedgerGroup = 0;
        double LedgerSummaryDrAmtWithLedgerGroup = 0;

        public double GrandCreditAmount { get; set; }
        public double GrandDebitAmount { get; set; }
        public double GrandClosingBalance { get; set; }

        string VoucherType = "RC','PY";
        Int32 LedgerId = 0;
        Int32 GroupId = 0;
        string CostCenter = string.Empty;
        string RptAsOnDate = string.Empty;
        string Rec_Pay_VoucherIds = string.Empty; ////12/01/2018, To fitler Reference Recept, Payment Vocuers alone when we drill from reference pending status report

        int count = 0;

        string PriviousTransMode = string.Empty;
        string OPBalanceDate = string.Empty;
        public static string OPBALTITLE = "Opening Balance";
        string OPTitle = string.Empty;

        string BaseReportId = string.Empty;
        string RecentDrillingReportId = string.Empty;
        Int32 DrillCurrencyCountryId = 0;
        string DrillCurrencySymbol = string.Empty;

        DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;

        //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling
        //reportdatefrom, reportdateto will be changed based on its drilling
        string datefrom = string.Empty;
        string dateto = string.Empty;
        string projectid = string.Empty;

        string CapCredit = string.Empty;
        string CapDebit = string.Empty;
        string CapClosingBalance = string.Empty;

        //15/02/2021, To have drilling form ledger monthly summary
        bool showMonthlySummary = false;
        private bool ShowMonthlySummary
        {
            get
            {
                return showMonthlySummary;
            }
            set
            {
                showMonthlySummary = value;
            }
        }

        /// <summary>
        /// On 20/02/2024, can attach project name in Narration
        /// </summary>
        private bool CanShowProjectInNarration
        {
            get
            {
                bool rtn = false;
                if (this.IsDrillDownMode && dtProjectList != null && !string.IsNullOrEmpty(this.ReportProperties.Project))
                {
                    rtn = (this.ReportProperties.SelectedProjectCount > 1);
                }
                return rtn;
            }
        }

        private DataTable dtProjectList = new DataTable();
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            MonthlyGroupNumber = 1;

            LedgerDebit = 0;
            LedgerCredit = 0;
            MonthlyOpeningBalance = 0;
            MonthlyClosingBalance = 0;
            GroupDetbitSum = 0;
            GroupCreditSum = 0;
            MonthlyPriousClBalance = 0;
            ResetMonthTotal = 0;
            MonthlyCumulativeBalance = 0;
            LedgerSummaryCrAmtWithLedgerGroup = 0;
            LedgerSummaryDrAmtWithLedgerGroup = 0;
            xrClosingBalance.Text = xrOpCreditBalance.Text = xrGroupLedgerDebit.Text = xrCLBLsummary.Text = xrtblGrandClBal.Text = xrtblBreakupClTotal.Text = "";

            //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling, reportdatefrom, reportdateto will be changed based on its drilling
            datefrom = string.Empty;
            dateto = string.Empty;
            projectid = string.Empty;

            //On 17/12/2018, Set Sign details
            FixReportPropertyForCMF();
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrtblGrandTotal.WidthF;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
            dtTOCList.Rows.Clear();

            //On 20/09/2023, to fix TOC by defualt (on 04/12/2023, Page setup is locking)
            //ReportProperty.Current.ShowTableofContent = 1;

            if (IsDrillDownMode)
            {
                //14/03/2017
                /// When drill-down , we use existing general ledger report for drill ledger report (for particular ledger).
                /// if user generate general ledger in another tab, it should not overlap drilled and general ledger
                if (this.ReportProperties.DrillDownProperties != null && this.ReportProperties.DrillDownProperties.Count > 0)
                {
                    Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                    //DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                    ddtypeLinkType = DrillDownType.BASE_REPORT;
                    ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                    //10/03/2017, for drill down, we use this ledger report for all reports,
                    //If ledger reports is already showing in another report with its own properties like (monthly total, ledger summery etc)
                    this.ReportProperties.IncludeNarration = 1;
                    this.ReportProperties.IncludeLedgerGroup = 0;
                    this.ReportProperties.IncludeLedgerGroupTotal = 0;
                    this.ReportProperties.ShowMonthTotal = 0;
                    this.ReportProperties.ShowByLedgerSummary = 0;
                    this.ReportProperties.ShowByLedger = 0;

                    this.ReportProperties.ShowLedgerOpBalance = 0;

                    this.ReportProperties.BreakByLedger = 0;
                    this.ReportProperties.DonorConditionSymbol = string.Empty;
                    this.ReportProperties.DonorFilterAmount = 0;
                    this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal = 0;

                    //On 22/02/2021
                    this.ReportProperties.ShowAllAgainstLedgers = 0;

                    switch (ddtypeLinkType)
                    {
                        case DrillDownType.LEDGER_SUMMARY_RECEIPTS:
                            VoucherType = "RC";
                            break;
                        case DrillDownType.LEDGER_SUMMARY_PAYMENTS:
                            VoucherType = "PY";
                            break;
                    }

                    BaseReportId = GetBaseReportId();

                    if (dicDDProperties.ContainsKey("REPORT_ID"))
                        RecentDrillingReportId = dicDDProperties["REPORT_ID"].ToString();

                    if (dicDDProperties.ContainsKey("LEDGER_ID"))
                        LedgerId = UtilityMember.NumberSet.ToInteger(dicDDProperties["LEDGER_ID"].ToString());

                    if (dicDDProperties.ContainsKey("PARTICULARS_ID"))
                        LedgerId = UtilityMember.NumberSet.ToInteger(dicDDProperties["PARTICULARS_ID"].ToString());

                    if (dicDDProperties.ContainsKey(this.ReportParameters.GROUP_IDColumn.ColumnName))
                        GroupId = UtilityMember.NumberSet.ToInteger(dicDDProperties[this.ReportParameters.GROUP_IDColumn.ColumnName].ToString());

                    if (dicDDProperties.ContainsKey(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName))
                        CostCenter = dicDDProperties[this.ReportParameters.COST_CENTRE_IDColumn.ColumnName].ToString();

                    DrillCurrencyCountryId = 0;
                    DrillCurrencySymbol = settingProperty.Currency;
                    if (dicDDProperties.ContainsKey(this.ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName))
                    {
                        DrillCurrencyCountryId = UtilityMember.NumberSet.ToInteger(dicDDProperties[this.ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());

                        if (DrillCurrencyCountryId > 0)
                        {
                            DrillCurrencySymbol = this.GetCurrencySymbol(DrillCurrencyCountryId);
                        }
                    }

                    // On 18.09.2018 changed for Balance Sheet to Assign dataAsOn ( BookBeginFrom to Date As on )
                    if ((dicDDProperties.ContainsKey(this.ReportParameters.DATE_AS_ONColumn.ColumnName) || BaseReportId == "RPT-031")
                        && (GroupId != (int)FixedLedgerGroup.Cash && GroupId != (int)FixedLedgerGroup.BankAccounts && GroupId != (int)FixedLedgerGroup.FixedDeposit))
                    {
                        //If drilling from balancesheet or drilling report (Group/Sub Group)
                        if (RecentDrillingReportId == "RPT-031" || BaseReportId == "RPT-031") //Balancesheet
                        {
                            //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling, reportdatefrom, reportdateto will be changed based on its drilling
                            /*this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom);
                            this.ReportProperties.DateTo = dicDDProperties[this.ReportParameters.DATE_AS_ONColumn.ColumnName].ToString();*/
                            datefrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom);
                            if (dicDDProperties.ContainsKey(this.ReportParameters.DATE_AS_ONColumn.ColumnName))
                            {
                                dateto = dicDDProperties[this.ReportParameters.DATE_AS_ONColumn.ColumnName].ToString();
                            }
                            else
                            {
                                dateto = this.ReportProperties.DateTo;
                            }
                            this.ReportProperties.ShowLedgerOpBalance = 1;
                        }
                        else if (RecentDrillingReportId == "RPT-030" || BaseReportId == "RPT-030") //TB report - Current and Closing
                        {
                            if (IsFinancialYear() > 0 || (FetchNatureId() == (int)Natures.Assert || FetchNatureId() == (int)Natures.Libilities))
                            {
                                this.ReportProperties.ShowLedgerOpBalance = 1;
                            }
                        }
                        else
                        {
                            RptAsOnDate = dicDDProperties[this.ReportParameters.DATE_AS_ONColumn.ColumnName].ToString();
                        }
                    }
                    else if ((ddtypeLinkType == DrillDownType.LEDGER_SUMMARY) &
                        (GroupId == (int)FixedLedgerGroup.Cash || GroupId == (int)FixedLedgerGroup.BankAccounts || GroupId == (int)FixedLedgerGroup.FixedDeposit))
                    {
                        //On 18/02/2019
                        // Show Ledger Opening Balance True if the dicDDProperties Contains Ledger Summary with Group Id (12,13,14), 
                        //In order to drilling the Cash/Bank/FD related ledger Reports
                        Int32 isopeningbalance = 0;
                        if (dicDDProperties.ContainsKey(this.reportSetting1.AccountBalance.IS_OPENING_BALANCEColumn.ColumnName))
                        {
                            isopeningbalance = UtilityMember.NumberSet.ToInteger(dicDDProperties[this.reportSetting1.AccountBalance.IS_OPENING_BALANCEColumn.ColumnName].ToString());
                        }
                        if (isopeningbalance == 1)// For opening balance Cash/Bank/FD drill change date range
                        {
                            DateTime rptDateFrom = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddDays(-1); ;
                            //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling, reportdatefrom, reportdateto will be changed based on its drilling
                            datefrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom);
                            dateto = rptDateFrom.ToShortDateString();
                        }
                        else if ((RecentDrillingReportId == "RPT-031" || BaseReportId == "RPT-031") && (isopeningbalance == 0)) //Balancesheet/ CAsh/Bank/Fd closing
                        {
                            datefrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom);
                            dateto = this.ReportProperties.DateAsOn;
                        }
                        else if (RecentDrillingReportId == "RPT-192") //For Negative Cash/Bank Details
                        {
                            datefrom = dicDDProperties[reportSetting1.NegativeCashBankBalance.VOUCHER_DATEColumn.ColumnName].ToString();
                            dateto = dicDDProperties[reportSetting1.NegativeCashBankBalance.VOUCHER_DATEColumn.ColumnName].ToString();
                            projectid = dicDDProperties[reportSetting1.NegativeCashBankBalance.PROJECT_IDColumn.ColumnName].ToString();
                        }
                        this.ReportProperties.ShowLedgerOpBalance = 1;
                    }
                    else if (dicDDProperties.ContainsKey("VOUCHER_DATE"))
                    {   //On 19/01/2023, for Ledger-wise collection repot (if vocuer date is attached in drill properties, let us show ledger report for voucher date
                        datefrom = dicDDProperties["VOUCHER_DATE"].ToString(); ;
                        dateto = dicDDProperties["VOUCHER_DATE"].ToString();
                    }

                    //04/10/2018, for IE Report
                    if (RecentDrillingReportId == "RPT-028" || BaseReportId == "RPT-028") //IE report
                    {
                        //   if (this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false)
                        //    <= this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false))
                        if (this.UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false)
                                                    == this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false))
                        {
                            this.ReportProperties.ShowLedgerOpBalance = 1;
                        }
                    }


                    //12/01/2018, To fitler Reference Recept, Payment Vocuers alone when we drill from reference pending status report
                    if (dicDDProperties.ContainsKey(reportSetting1.Voucher_Reference.REC_PAY_VOUCHER_IDColumn.ColumnName))
                    {
                        Rec_Pay_VoucherIds = dicDDProperties[reportSetting1.Voucher_Reference.REC_PAY_VOUCHER_IDColumn.ColumnName].ToString();

                        if (string.IsNullOrEmpty(Rec_Pay_VoucherIds))
                        {
                            Rec_Pay_VoucherIds = "-1";
                        }
                    }

                    //On 15/02/2021, While drilling, attach monthly drilling
                    if (AppSetting.ShowMonthlySummaryDrillingReport == 1)
                    {
                        //On 15/02/2021 *******************************************************************************************************************************
                        // For all the report, When we do ledger drilling from any report, we need to show Ledger monthly summary report bsaed on finance setting
                        // If we drill monthly summary from the same report, it will show as usual ledger report, If we drill back, again it will show Monthly summary 
                        //**********************************************************************************************************************************************
                        if (dicDDProperties.ContainsKey(DrillDownPDLFlag.FromLedgerMonthSummary.ToString()))
                        {
                            //bool dicDDProperties[DrillDownPDLFlag.FromLedgerMonthSummary.ToString()].ToString();
                            DateTime date = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false);
                            if (dicDDProperties.ContainsKey(this.reportSetting1.Ledger.DATEColumn.ColumnName))
                            {
                                date = UtilityMember.DateSet.ToDate(dicDDProperties[this.reportSetting1.Ledger.DATEColumn.ColumnName].ToString(), false);

                                datefrom = UtilityMember.DateSet.ToDate(new DateTime(date.Year, date.Month, 1).ToShortDateString());
                                dateto = UtilityMember.DateSet.ToDate(datefrom, false).AddMonths(1).AddDays(-1).ToShortDateString();
                            }

                            showMonthlySummary = false;
                        }
                        else if (this.AppSetting.ShowMonthlySummaryDrillingReport == 1) //Show Ledger monthly summary based on setting
                        {
                            showMonthlySummary = true;
                            //Attach drill for monthly detail
                            ArrayList frmLedgerMonthSummaryfilter = new ArrayList { this.ReportParameters.LEDGER_IDColumn.ColumnName, this.reportSetting1.Ledger.DATEColumn.ColumnName,
                                                                DrillDownPDLFlag.FromLedgerMonthSummary.ToString()  };

                            //On 18/12/2023, For cahs/Bank/FD ledger drilling attach group id
                            if (GroupId > 0)
                            {
                                frmLedgerMonthSummaryfilter.Add(this.ReportParameters.GROUP_IDColumn.ColumnName);
                            }
                            this.AttachDrillDownToRecord(xrGrouptotal, xrCellMonthTotal, frmLedgerMonthSummaryfilter, ddtypeLinkType, false);
                        }
                        //*********************************************************************************************************************************************
                    }

                    SetReportTitle();
                }
            }

            BindReport();


            // 14/05/2025, *Chinna
            if (this.ReportProperties.BreakByLedger == 1)
            {
                grpHeadrLedgerName.GroupFields.Clear();
                grpHeadrLedgerName.GroupFields.Add(new GroupField("LEDGER_NAME"));
                grpBreakUpLedger.Visible = true;
                xrtblGroupGrandTotal.Visible = false;
                xrPageBreak1.Visible = true;
            }
        }

        #endregion

        #region Methods
        public void BindReport()
        {
            /*SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF-5;*/

            this.SetLandscapeBudgetNameWidth = xrtblHeaderCaption.WidthF - 5;
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF - 5;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF - 5;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF - 5;
            SetTitleWidth(xrtblHeaderCaption.WidthF - 5);
            setHeaderTitleAlignment();

            if ((string.IsNullOrEmpty(this.ReportProperties.DateFrom) ||
                string.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0" || this.ReportProperties.Ledger == "0") && (LedgerId == 0))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                xrGrandDebitTotal.Text = xrGrandCreditTotal.Text = string.Empty;
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        BindProperty();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    BindProperty();
                    base.ShowReport();
                }
            }
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblLedgerName = AlignContentTable(xrtblLedgerName);
            xrtblOpeningBalance = AlignContentTable(xrtblOpeningBalance);
            xrtblBindSource = AlignContentTable(xrtblBindSource);
            xrGrouptotal = AlignContentTable(xrGrouptotal);
            xrtblGroupGrandTotal = AlignContentTable(xrtblGroupGrandTotal);
            xrtblLGroup = AlignContentTable(xrtblLGroup);

            if (this.settingProperty.AllowMultiCurrency == 1 && DrillCurrencyCountryId > 0)
            {
                if (!string.IsNullOrEmpty(DrillCurrencySymbol))
                {
                    xrCapCredit.Text = CapCredit + " (" + DrillCurrencySymbol + ")";
                    xrCapDebit.Text = CapDebit + " (" + DrillCurrencySymbol + ")";

                    if (!string.IsNullOrEmpty(xrCapclosingBalance.Text))
                    {
                        xrCapclosingBalance.Text = CapClosingBalance + " (" + DrillCurrencySymbol + ")";
                    }
                }
            }
            else
            {
                this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
                this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
                if (!string.IsNullOrEmpty(xrCapclosingBalance.Text))
                {
                    this.SetCurrencyFormat(xrCapclosingBalance.Text, xrCapclosingBalance);
                }
            }

        }

        private void BindProperty()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            SetReportTitle();
            if (IsDrillDownMode)
                this.ReportTitle = GetLedgerName(LedgerId);
            else
                this.ReportTitle = ReportProperty.Current.ReportTitle;


            //On 02/07/2020, For temp purpose, since variable and caption are changed between IncludeLedgerGroupTotal and ShowMonthTotal
            //in Report criteria page. we assing IncludeLedgerGroupTotal to ShowMonthTotal. *** IT SHOULD BE CORRUPTED ***
            this.ReportProperties.ShowMonthTotal = this.ReportProperties.IncludeLedgerGroupTotal;
            //----------------------------------------------------------------------------------------------------------------

            //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();

            // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            //On 22/07/2022, If monthly cumulative Balance option is enable, disable all the other options
            grpHeaderParticulars.Visible = false;
            grpHeaderParticulars.GroupFields.Clear();
            if (this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 1)
            {
                grpHeaderParticulars.GroupFields.Add(new GroupField(reportSetting1.Ledger.PARTICULARS1Column.ColumnName));
                ReportProperty.Current.IncludeNarration = 0;

                //Om 02/09/2022, 
                //this.ReportProperties.ShowAssetLiabilityLedgerOpBalance = 0;
                ReportProperties.IncludeLedgerGroupTotal = 0;
                ReportProperties.ShowByLedgerSummary = 0;
                ReportProperties.ShowByLedger = 0;

                ReportProperties.ShowOpeningBalance = 1;
                ReportProperty.Current.ShowOpeningBalance = 1;

                //ReportProperties.ShowByLedgerGroup = 1; //On 11/12/2023, To separate Ledger group and narration cumulative
                ReportProperties.ShowMonthTotal = 1;
                grpHeaderParticulars.Visible = true;
                xrCapVNo.Text = xrCapVType.Text = string.Empty;

                /*grpHeaderParticulars.GroupFields.Clear();
                grpHeaderParticulars.GroupFields.Add(new GroupField(reportSetting1.Ledger.PARTICULARSColumn.ColumnName));

                grpHeaderVoucherMth.GroupFields.Clear();
                grpHeaderParticulars.GroupFields.Add(new GroupField("calcVoucherYear"));
                grpHeaderParticulars.GroupFields.Add(new GroupField("calcVoucherMonth"));*/
            }

            xrCapclosingBalance.Text = "Closing Balance";

            grpBreakUpLedger.Visible = (this.ReportProperties.BreakByLedger == 1);

            if (grpBreakUpLedger.Visible)
            {
                xrtblGroupGrandTotal.Visible = false;
            }
            else
            {
                xrtblGroupGrandTotal.Visible = true;
            }
            if ((this.ReportProperties.ShowByLedgerSummary == 1 && this.ReportProperties.ShowMonthTotal == 0) || this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 1)
            {
                Detail.Visible = grpLedgerGroup.Visible = grpFoooterDate.Visible = grpHeaderVoucherMth.Visible = grpHeadrLedgerName.Visible = grpLedgerFooter.Visible = false;
                grpLedgerGroup.Visible = (this.ReportProperties.ShowByLedgerGroup == 1);
                grpHeaderVoucherMth.Visible = (this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 1);
                if (this.ReportProperties.ShowByLedgerGroup == 1 || this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 1) //On 11/12/2023, To separate Ledger group and narration cumulative
                {
                    grpHeadrLedgerName.Visible = true;
                    grpLedgerSummary.Visible = false;
                    if (this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 0)
                    {
                        xrtblLedgerName.Font = new Font(xrtblLedgerName.Font, FontStyle.Regular);
                        if (this.ReportProperties.ShowByLedgerSummary == 0)
                        {
                            xrCapclosingBalance.Text = "";
                        }
                    }
                    //xrCapclosingBalance.Text = "";
                }
                else
                {
                    grpLedgerSummary.Visible = true;
                }
            }
            else if (this.ReportProperties.ShowMonthTotal == 1)
            {
                grpHeaderVoucherMth.Visible = (this.ReportProperties.ShowMonthTotal == 1);
                Detail.Visible = grpHeadrLedgerName.Visible = grpLedgerFooter.Visible = grpHeaderVoucherMth.Visible;
                grpFoooterDate.Visible = grpLedgerSummary.Visible = false;
                grpLedgerGroup.Visible = this.ReportProperties.ShowByLedgerGroup == 1;
            }
            else if (this.ReportProperties.ShowByLedgerGroup == 1)
            {
                grpLedgerGroup.Visible = true;
                Detail.Visible = grpHeadrLedgerName.Visible = grpFoooterDate.Visible = true;
                grpHeaderVoucherMth.Visible = grpLedgerFooter.Visible = grpLedgerSummary.Visible = false;
            }
            else
            {
                Detail.Visible = grpHeadrLedgerName.Visible = grpFoooterDate.Visible = true;
                grpHeaderVoucherMth.Visible = grpLedgerFooter.Visible = grpLedgerGroup.Visible = grpLedgerSummary.Visible = false;
            }

            //18/01/2021, to show/hide Ledger code
            //xrLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? 56 : 0);
            xrLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? xrCapDate.WidthF : 0);
            prOPBalance.Value = "0.00";

            // grpFoooterDate.Visible = (this.ReportProperties.ShowMonthTotal == 0);
            prOPBalance.Visible = false;
            ResultArgs resultArgs = BindLedgerSource();

            DataTable dtLedgerDetails = new DataTable();
            DataView dvLedger = new DataView();

            /* As on 28/06/2021
            if (resultArgs.Success && resultArgs.DataSource.TableView.ToTable().Rows.Count > 0)
            {
                dtLedgerDetails = resultArgs.DataSource.TableView.ToTable();
                if (ReportProperty.Current.ShowLedgerOpBalance == 0)
                {
                    dvLedger = dtLedgerDetails.AsDataView();
                    if (dvLedger != null && dvLedger.ToTable().Rows.Count > 0)
                    {
                        // " + this.LedgerParameters.PARTICULARSColumn.ColumnName + "<> " + OPBALTITLE + "
                        dvLedger.RowFilter = "PARTICULAR_TYPE <> 'LEDGER_OPENING_BALANCE'";
                        dtLedgerDetails = dvLedger.ToTable();
                    }
                }
            }*/

            if (resultArgs.Success && resultArgs.DataSource.TableView.ToTable().Rows.Count > 0)
            {
                dtLedgerDetails = resultArgs.DataSource.TableView.ToTable();
                dvLedger = dtLedgerDetails.AsDataView();

                //On 14/04/2023, To allow include opening balance (Asset & Liabilities/IE)
                //if (ReportProperty.Current.ShowNarrationMonthwiseCumulativeTotal == 0)
                //{
                if (this.IsDrillDownMode == false && ReportProperty.Current.ShowLedgerOpBalance == 0 && ReportProperty.Current.ShowAssetLiabilityLedgerOpBalance == 1)
                {
                    //25/06/2021, Based on Nature (Show Opening Balance only for Asset and Liabilities alone)
                    string AssetLiabilityOpening = "((" + reportSetting1.BUDGETVARIANCE.NATURE_IDColumn.ColumnName +
                            " IN (" + (int)Natures.Assert + "," + (int)Natures.Libilities + "))" +
                             " OR (" + reportSetting1.BUDGETVARIANCE.NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + ")" +
                                      " AND PARTICULAR_TYPE <> 'LEDGER_OPENING_BALANCE'))";
                    //dvLedger.RowFilter = "NATURE_ID IN (3,4) Or (NATURE_ID IN (1,2) AND PARTICULAR_TYPE <> 'LEDGER_OPENING_BALANCE')";
                    dvLedger.RowFilter = AssetLiabilityOpening;
                    dtLedgerDetails = dvLedger.ToTable();
                }
                else if (ReportProperty.Current.ShowLedgerOpBalance == 0 && ReportProperty.Current.ShowAssetLiabilityLedgerOpBalance == 0)
                {
                    if (dvLedger != null && dvLedger.ToTable().Rows.Count > 0)
                    {
                        dvLedger.RowFilter = "PARTICULAR_TYPE <> 'LEDGER_OPENING_BALANCE'";
                        dtLedgerDetails = dvLedger.ToTable();
                    }
                }
                //}
            }

            if (dtLedgerDetails != null && dtLedgerDetails.Rows.Count != 0)
            {
                GrandCreditAmount = this.UtilityMember.NumberSet.ToDouble(dtLedgerDetails.Compute("SUM(CREDIT)", "").ToString());
                GrandDebitAmount = this.UtilityMember.NumberSet.ToDouble(dtLedgerDetails.Compute("SUM(DEBIT)", "").ToString());
                GrandClosingBalance = GrandDebitAmount - GrandCreditAmount;
                /* On 13/12/2023, To have proper grand total
                 * if (GrandDebitAmount >= GrandCreditAmount)
                {
                    xrGrandCreditTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandCreditAmount + (GrandDebitAmount - GrandCreditAmount));
                    xrGrandDebitTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandDebitAmount);
                }
                else
                {
                    xrGrandDebitTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandDebitAmount + (GrandCreditAmount - GrandDebitAmount));
                    xrGrandCreditTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandCreditAmount);
                }*/

                xrGrandClosingBalance.Text = string.Empty;

                xrGrandCreditTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandCreditAmount);
                xrGrandDebitTotal.Text = this.UtilityMember.NumberSet.ToNumber(GrandDebitAmount);
                xrGrandClosingBalance.Text = (GrandClosingBalance >= 0) ? this.UtilityMember.NumberSet.ToNumber(GrandClosingBalance) + " Dr" :
                                    this.UtilityMember.NumberSet.ToNumber(Math.Abs(GrandClosingBalance)) + " Cr";

            }

            DataView dtview = dtLedgerDetails.AsDataView();

            if (dtview != null)
            {
                if (this.ReportProperties.ShowByLedgerGroup == 1)
                {
                    grpLedgerGroup.GroupFields.Clear();
                    grpLedgerGroup.GroupFields.Add(new GroupField("NATURE_ID"));
                    grpLedgerGroup.GroupFields.Add(new GroupField("GROUP"));

                    //On 28/07/2022, To have proper Ledger Name and Ledgder Code order ----------
                    grpHeadrLedgerName.GroupFields.Clear();
                    if (ReportProperties.SortByLedger == 1)
                    {
                        grpHeadrLedgerName.GroupFields.Add(new GroupField("LEDGER_NAME"));
                    }
                    else
                    {
                        grpHeadrLedgerName.GroupFields.Add(new GroupField("LEDGER_CODE"));
                        grpHeadrLedgerName.GroupFields.Add(new GroupField("LEDGER_NAME"));
                    }
                    grpHeadrLedgerName.GroupFields.Add(new GroupField("GROUP"));
                    //--------------------------------------------------------------------------
                }
                else if (this.ReportProperties.ShowByLedgerGroup == 0 && this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 1)
                {   //On 11/12/2023, To separate Ledger group and narration cumulative
                    grpLedgerGroup.GroupFields.Clear();

                    grpHeadrLedgerName.GroupFields.Clear();
                    if (ReportProperties.SortByLedger == 1)
                    {
                        grpHeadrLedgerName.GroupFields.Add(new GroupField("LEDGER_NAME"));
                    }
                    else
                    {
                        grpHeadrLedgerName.GroupFields.Add(new GroupField("LEDGER_CODE"));
                        grpHeadrLedgerName.GroupFields.Add(new GroupField("LEDGER_NAME"));
                    }
                    grpHeadrLedgerName.GroupFields.Add(new GroupField("GROUP"));
                }
                else
                {
                    if (this.ReportProperties.ShowByLedgerGroup == 0 || this.ReportProperties.ShowByLedgerSummary == 0)
                    {
                        //On 28/07/2022, To have proper Ledger Name and Ledgder Code order ----------
                        grpLedgerGroup.GroupFields.Clear();
                        //---------------------------------------------------------------------
                        if (ReportProperties.SortByLedger == 1)
                            grpLedgerGroup.GroupFields.Add(new GroupField("LEDGER_NAME"));
                        else
                        {
                            grpLedgerGroup.GroupFields.Add(new GroupField("LEDGER_CODE"));
                            grpLedgerGroup.GroupFields.Add(new GroupField("LEDGER_NAME"));
                        }
                    }
                }

                if (this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 1)
                {
                    grpHeaderParticulars.SortingSummary.Enabled = true;
                    grpHeaderParticulars.SortingSummary.FieldName = "PARTICULARS_MODE_ORDER";
                    grpHeaderParticulars.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpHeaderParticulars.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }


                //On 12/02/2021, While drilling, hide other panels other than month summary bsaed on setting
                if (IsDrillDownMode && showMonthlySummary)
                {
                    grpLedgerGroup.Visible = grpHeadrLedgerName.Visible = grpBreakUpLedger.Visible = grpFoooterDate.Visible = grpLedgerSummary.Visible = false;
                    PageHeader.Visible = grpHeaderVoucherMth.Visible = grpLedgerFooter.Visible = grpFoooterDate.Visible = true;
                }


                dtview.Table.TableName = "Ledger";
                //On 05/06/2017, To add Amount filter condition
                //AttachAmountFilter(dtview);
                //On 16/09/2021, To add Amount filter condition
                string filterfields = reportSetting1.AuditLog.CREDITColumn.ColumnName + "," + reportSetting1.AuditLog.DEBITColumn.ColumnName;
                string filtercondition = this.AttachAmountFilter(dtview, filterfields);
                lblAmountFilter.Text = filtercondition;
                lblAmountFilter.Visible = (!string.IsNullOrEmpty(lblAmountFilter.Text));

                //On 18/12/2023, For cahs/Bank/FD ledger drilling attach group id
                if (dtview.Table != null && GroupId > 0 && showMonthlySummary && !dtview.Table.Columns.Contains(this.ReportParameters.GROUP_IDColumn.ColumnName))
                {
                    DataColumn dcGroupId = new DataColumn(this.ReportParameters.GROUP_IDColumn.ColumnName, typeof(System.Int32));
                    dcGroupId.DefaultValue = GroupId;
                    dtview.Table.Columns.Add(dcGroupId);
                }

                this.DataSource = dtview;
                this.DataMember = dtview.Table.TableName;

            }

            if (this.ReportProperties.IncludeNarrationwithRefNo == 1 || this.ReportProperties.IncludeNarrationwithNameAddress == 1 || this.ReportProperties.IncludeNarrationwithCurrencyDetails == 1)
            {
                this.ReportProperties.IncludeNarration = 1;
            }

            // BY Alex to set Date from and to when drilldown from the Bank Balance report
            if (RptAsOnDate != string.Empty)
            {
                this.ReportProperties.DateFrom = RptAsOnDate;
                this.ReportProperties.DateTo = RptAsOnDate;
            }

            /*if (!this.IsDrillDownMode && ReportProperty.Current.ShowTableofContent == 1 && 
                grpHeadrLedgerName.Visible && dtview != null && dtview.Table.Rows.Count > 0)
            {
                this.Bookmark = "List of Ledgers";
                this.AttachTOCToReport();
            }
            else
            {
                this.RemoveTOCFromReport();
            }*/

            SplashScreenManager.CloseForm();
        }

        public ResultArgs BindLedgerSource()
        {
            ResultArgs resultArgs = null;
            string Test = this.GetReportSQL(SQL.ReportSQLCommand.Report.Ledger);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, string.IsNullOrEmpty(projectid) ? this.ReportProperties.Project : projectid);

                if (RptAsOnDate != string.Empty)
                {
                    // By Aldrin Because error is throwing date from is empty.
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, RptAsOnDate);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, RptAsOnDate);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, RptAsOnDate);
                }
                else
                {
                    //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling, reportdatefrom, reportdateto will be changed based on its drilling
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, string.IsNullOrEmpty(datefrom) ? this.ReportProperties.DateFrom : datefrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, string.IsNullOrEmpty(dateto) ? this.ReportProperties.DateTo : dateto);
                }

                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_REFNOColumn, this.ReportProperties.IncludeNarrationwithRefNo);
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_NAMEADDRESSColumn, this.ReportProperties.IncludeNarrationwithNameAddress);

                // 30/04/2025, *Chinna
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_PAN_GSTColumn, this.ReportProperties.IncludePanwithGSTNo);

                dataManager.Parameters.Add(this.ReportParameters.SHOW_NARRATION_MONTH_TOTALColumn, this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal);

                if (!string.IsNullOrEmpty(CostCenter))
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, CostCenter);
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, (LedgerId == 0) ? this.ReportProperties.Ledger : LedgerId.ToString());
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, VoucherType);

                //On 22/02/2021, whether to show all against ledger names or show only top of the agaist ledger
                dataManager.Parameters.Add(this.ReportParameters.SHOW_ALL_AGAINST_LEDGERSColumn, this.ReportProperties.ShowAllAgainstLedgers);

                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_CURRENCYColumn, this.ReportProperties.IncludeNarrationwithCurrencyDetails);

                //On 26/11/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (this.IsDrillDownMode && settingProperty.AllowMultiCurrency == 1 && this.DrillCurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.DrillCurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }

                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Test);

                //On 16/11/2018, show only pure receipts or payment alone
                if (resultArgs != null && resultArgs.Success)
                {
                    //On 16/11/2018, Only for Receipts and Payments
                    //On 31/05/2019, to filter only pure receipts or payments (only journal FD (interest or tds on fd interest)
                    //for R&P, Previous R&P, Month Abstract 3 Reports
                    //On 06/09/2019, based on setting
                    if ((BaseReportId == "RPT-027" || BaseReportId == "RPT-148" ||
                        BaseReportId == "RPT-001" || BaseReportId == "RPT-002" || BaseReportId == "RPT-003") && AppSetting.ShowCr_DrAmountDrillingLedgerInAbstract == 0)
                    {
                        // if Journal Entry enable for Cash \ Bank Transactions 20/02/025, Chinna
                        if (this.settingProperty.EnableCashBankJournal == 0) // Not enable Cash and Bank Transactions
                        {
                            if (ddtypeLinkType == DrillDownType.LEDGER_SUMMARY_RECEIPTS)
                            {
                                string fitler = "CREDIT > 0 AND ( (VOUCHER_TYPE='" + DefaultVoucherTypes.Receipt.ToString() + "' OR VOUCHER_TYPE='" + DefaultVoucherTypes.Payment.ToString() + "') OR " +
                                                "(VOUCHER_TYPE ='" + DefaultVoucherTypes.Journal.ToString() + "' AND VOUCHER_SUB_TYPE ='" + VoucherSubTypes.FD + "') )";
                                resultArgs.DataSource.TableView.RowFilter = fitler;
                            }
                            else if (ddtypeLinkType == DrillDownType.LEDGER_SUMMARY_PAYMENTS)
                            {
                                string fitler = "DEBIT > 0 AND ( (VOUCHER_TYPE='" + DefaultVoucherTypes.Receipt.ToString() + "' OR VOUCHER_TYPE='" + DefaultVoucherTypes.Payment.ToString() + "') OR " +
                                                "(VOUCHER_TYPE IN ('" + DefaultVoucherTypes.Journal.ToString() + "','" + DefaultVoucherTypes.Contra.ToString() + "') AND VOUCHER_SUB_TYPE ='" + VoucherSubTypes.FD + "') )";
                                resultArgs.DataSource.TableView.RowFilter = fitler;
                            }
                        }
                        else
                        {
                            // Enabled
                            if (ddtypeLinkType == DrillDownType.LEDGER_SUMMARY_RECEIPTS)
                            {
                                string fitler = "CREDIT > 0 AND ( (VOUCHER_TYPE='" + DefaultVoucherTypes.Receipt.ToString() + "' OR VOUCHER_TYPE='" + DefaultVoucherTypes.Payment.ToString() + "') OR " +
                                              "(VOUCHER_TYPE ='" + DefaultVoucherTypes.Journal.ToString() + "' AND VOUCHER_SUB_TYPE ='" + VoucherSubTypes.FD + "' OR IS_CASH_BANK_STATUS = '1') )";
                                resultArgs.DataSource.TableView.RowFilter = fitler;
                            }
                            else if (ddtypeLinkType == DrillDownType.LEDGER_SUMMARY_PAYMENTS)
                            {
                                string fitler = "DEBIT > 0 AND ( (VOUCHER_TYPE='" + DefaultVoucherTypes.Receipt.ToString() + "' OR VOUCHER_TYPE='" + DefaultVoucherTypes.Payment.ToString() + "') OR " +
                                               "(VOUCHER_TYPE IN ('" + DefaultVoucherTypes.Journal.ToString() + "','" + DefaultVoucherTypes.Contra.ToString() + "') AND VOUCHER_SUB_TYPE ='" + VoucherSubTypes.FD + "' OR IS_CASH_BANK_STATUS = '1') )";
                                resultArgs.DataSource.TableView.RowFilter = fitler;
                            }

                        }
                    }
                    else //On 05/04/2023, if Voucher type is selected, filter based on the selection
                    {
                        if (!IsDrillDownMode)
                        {
                            if (this.ReportProperties.DayBookVoucherType != 0)
                            {
                                string VoucherTypeFilter = "(VOUCHER_DEFINITION_ID=0 OR  VOUCHER_DEFINITION_ID =  " + this.ReportProperties.DayBookVoucherType.ToString() + ")";
                                resultArgs.DataSource.TableView.RowFilter = VoucherTypeFilter;
                            }
                        }
                    }
                }

                //On 20/03/2024, --------------------------------------------------------------
                if (this.IsDrillDownMode)
                {
                    ResultArgs result = GetProjects();
                    if (result.Success && result.DataSource.Table != null)
                    {
                        dtProjectList = result.DataSource.Table;
                    }
                }
                //------------------------------------------------------------------------------
            }

            //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling, reportdatefrom, reportdateto will be changed based on its drilling
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + (string.IsNullOrEmpty(datefrom) ? this.ReportProperties.DateFrom : datefrom) +
                                   " - " + (string.IsNullOrEmpty(dateto) ? this.ReportProperties.DateTo : dateto);

            return resultArgs;
        }

        #endregion

        private void xrtblLedgerDebitBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            GroupDetbitSum += (GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName).ToString());
            GroupCreditSum += (GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName).ToString());
        }

        private void xrtblLedgerDebitBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //if (this.UtilityMember.NumberSet.ToDouble(xrtblClosingBalance.Text) > 0)
            //{
            if (ResetMonthTotal == 0)
            {
                e.Result = MonthlyOpeningBalance + GroupDetbitSum;
                MonthlyPriousClBalance = MonthlyClosingBalance;
                ResetMonthTotal++;
            }
            else
            {
                MonthlyPriousClBalance = MonthlyPriousClBalance + GroupDetbitSum;
                e.Result = GroupDetbitSum; // MonthlyPriousClBalance;
                MonthlyPriousClBalance = MonthlyClosingBalance;

            }
            if (xrClosingBalance.Text.Length > 0)
            {
                PriviousTransMode = xrClosingBalance.Text.Substring(xrClosingBalance.Text.Length - 2);
            }
            // }
            //else
            //{
            // e.Result = Math.Abs(this.UtilityMember.NumberSet.ToDouble(xrtblClosingBalance.Text)) + "Cr";
            // }
            e.Handled = true;
        }

        private void xrtblLedgerDebitBalance_SummaryReset(object sender, EventArgs e)
        {
            GroupDetbitSum = GroupCreditSum = 0;
        }

        private void xttblOpBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (MonthlyGroupNumber == 0)
            {
                // grpHeaderVoucherMth.Visible = false;
                e.Result = "0.00";
                if (xrClosingBalance.Text.Length > 0)
                {
                    //  PriviousTransMode = xrtblClosingBalance.Text.Substring(xrtblClosingBalance.Text.Length - 2);
                    string Clbalance = xrClosingBalance.Text.Remove(xrClosingBalance.Text.Length - 2);
                    MonthlyClosingBalance = this.UtilityMember.NumberSet.ToDouble(Clbalance);
                }
                MonthlyGroupNumber++;
                e.Handled = true;
            }
            else
            {
                if (this.ReportProperties.ShowMonthTotal == 1) { grpHeaderVoucherMth.Visible = true; }

                //if (PriviousTransMode.ToUpper() == TransMode.CR.ToString())
                //{
                //    xrOpCreditBalance.Text = string.Empty;
                //}
                //e.Result = MonthlyClosingBalance;

                if (PriviousTransMode.ToUpper() == TransMode.DR.ToString())
                {
                    e.Result = MonthlyClosingBalance;
                }

                if (xrClosingBalance.Text.Length > 0)
                {
                    string Clbalance = xrClosingBalance.Text.Remove(xrClosingBalance.Text.Length - 2);
                    MonthlyClosingBalance = this.UtilityMember.NumberSet.ToDouble(Clbalance);

                    //this.UtilityMember.NumberSet.ToDouble(xrtblClosingBalance.Text);
                    PriviousTransMode = xrClosingBalance.Text.Substring(xrClosingBalance.Text.Length - 2);

                    if (PriviousTransMode.ToUpper() == TransMode.CR.ToString())
                    {
                        //prOPBalance.Value = "0.00";
                        //e.Result = "0.00";
                        string CrBald = this.UtilityMember.NumberSet.ToNumber(MonthlyClosingBalance).ToString();
                        xrOpCreditBalance.Text = CrBald;
                        //e.Result = string.Empty;
                    }
                    else
                    {
                        //prOPBalance.Value = MonthlyClosingBalance;
                        xrOpCreditBalance.Text = string.Empty;
                    }
                }
                e.Handled = true;
            }

        }

        private void xrDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrDebit.Text);
            if (debitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrDebit.Text = "";
            }

            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }

        private void xrCrdit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double creditAmt = this.ReportProperties.NumberSet.ToDouble(xrCrdit.Text);
            if (creditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCrdit.Text = "";
            }

            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }

        private void xrtblLedgerCreditBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GroupCreditSum;
            e.Handled = true;
        }

        private void xrLedgerDebitBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            LedgerDebit += (GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName).ToString());
            LedgerCredit += (GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.CREDITColumn.ColumnName).ToString());
            //  xrtblClosingBalance.Text = xrGroupLedgerDebit.Text = xrtblGrandClBal.Text = this.UtilityMember.NumberSet.ToNumber(LedgerDebit - LedgerCredit).ToString();
            if ((LedgerDebit - LedgerCredit) < 0)
            {
                string amt = this.UtilityMember.NumberSet.ToNumber(Math.Abs((LedgerDebit - LedgerCredit))).ToString();
                xrCLBLsummary.Text = xrClosingBalance.Text = xrGroupLedgerDebit.Text = xrtblGrandClBal.Text = xrtblBreakupClTotal.Text = amt + " Cr";
            }
            else
            {
                xrCLBLsummary.Text = xrClosingBalance.Text = xrGroupLedgerDebit.Text = xrtblGrandClBal.Text = xrtblBreakupClTotal.Text = this.UtilityMember.NumberSet.ToNumber(LedgerDebit - LedgerCredit).ToString() + " Dr";
            }

            OPBalanceDate = (GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName) == null) ? " " : GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName).ToString();
            if (!string.IsNullOrEmpty(OPBalanceDate))
            {
                string dtTemp = this.UtilityMember.DateSet.ToDate(OPBalanceDate, false).Year + "-" +
                        this.UtilityMember.DateSet.ToDate(OPBalanceDate, false).Month + "-" + 1;
                xrOPBalanceCaption.Text = "Opening Balance as on  " + this.UtilityMember.DateSet.ToDate(dtTemp, "dd-MM-yyyy");
            }
            else
            {
                xrOPBalanceCaption.Text = "Opening Balance";
            }


        }
        private void xrlblLedgerGroup_SummaryReset(object sender, EventArgs e)
        {
            LedgerDebit = LedgerCredit = 0;
            MonthlyClosingBalance = 0;
            ResetMonthTotal = 0;
            xrOpCreditBalance.Text = string.Empty;
        }

        private void xrTable2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // SetTableborder(0);
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString().Trim();
            string LedgerDetail = (GetCurrentColumnValue("LEDGER_DETAIL") == null) ? string.Empty : GetCurrentColumnValue("LEDGER_DETAIL").ToString().Trim();
            if (this.ReportProperties.ShowAllAgainstLedgers == 1)
            {
                Narration += LedgerDetail;
            }
            xrtblBindSource = AlignTable(xrtblBindSource, Narration, string.Empty, count);

            OPTitle = (GetCurrentColumnValue(this.LedgerParameters.PARTICULARSColumn.ColumnName) == null) ?
              string.Empty :
             GetCurrentColumnValue(this.LedgerParameters.PARTICULARSColumn.ColumnName).ToString();

            //26/06/2021, Based on Nature (Show Opening Balance only for Asset and Liabilities alone)
            //if (ReportProperty.Current.ShowLedgerOpBalance == 0)
            if (ReportProperty.Current.ShowLedgerOpBalance == 0 && ReportProperty.Current.ShowAssetLiabilityLedgerOpBalance == 0)
            {
                if (OPBALTITLE.Equals(OPTitle.Trim()))
                {
                    e.Cancel = true;
                }
            }

        }

        private void xttblOpBalance_SummaryReset(object sender, EventArgs e)
        {
            //OpeningBalReset++;
        }

        private void xttblOpBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            OpeningBalReset++;
        }

        private void xrDate_SummaryRowChanged(object sender, EventArgs e)
        {

        }

        private void xrDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string RecDate = (GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName) == null) ?
              string.Empty :
             GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName).ToString();
            if (!string.IsNullOrEmpty(RecDate))
            {
                xrDate.Text = this.UtilityMember.DateSet.ToDate(RecDate, false).ToShortDateString();
            }
            else
            {
                xrDate.Text = string.Empty;
            }

            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }

        private string GetBaseReportId()
        {
            string Rtn = string.Empty;

            foreach (object item in this.ReportProperties.stackActiveDrillDownHistory)
            {
                EventDrillDownArgs eventdrilldownarg = item as EventDrillDownArgs;
                if (eventdrilldownarg.DrillDownType == DrillDownType.BASE_REPORT)
                {
                    Rtn = eventdrilldownarg.DrillDownRpt;
                    break;
                }
            }

            return Rtn;
        }

        public int IsFinancialYear()
        {
            ResultArgs resultArgs = new ResultArgs();
            string FinancialYear = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IsFirstFinancialYear);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.Scalar, FinancialYear);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchNatureId()
        {
            ResultArgs resultArgs = new ResultArgs();
            int NatureId = 0;
            string Nature = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IsNatures);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Nature);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    NatureId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][LedgerParameters.NATURE_IDColumn.ColumnName].ToString());
                }
            }
            return NatureId;
        }

        private void xrSumGrpDR_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //if (this.ReportProperties.ShowByLedgerGroup == 1 && this.ReportProperties.ShowByLedgerSummary == 1)
            if (this.ReportProperties.ShowByLedgerGroup == 1 && this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 0)
            {
                double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrSumGrpDR.Text);
                if (debitAmt != 0)
                {
                    e.Cancel = false;
                }
                else
                {
                    xrSumGrpDR.Text = "";
                }
            }
            else
            {
                xrSumGrpDR.Text = "";
            }
        }

        private void xrSumGrpCR_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //if (this.ReportProperties.ShowByLedgerGroup == 1 && this.ReportProperties.ShowByLedgerSummary == 1)
            if (this.ReportProperties.ShowByLedgerGroup == 1 && this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 0)
            {
                double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrSumGrpCR.Text);
                if (debitAmt != 0)
                {
                    e.Cancel = false;
                }
                else
                {
                    xrSumGrpCR.Text = " ";
                }
            }
            else
            {
                xrSumGrpCR.Text = "";
            }
        }

        private void xrSumLDR_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //On 04/05/2023, Show always Ledger Total
            //if (this.ReportProperties.ShowByLedgerGroup == 1 && this.ReportProperties.ShowByLedgerSummary == 1)
            //{
            double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrSumLDR.Text);

            if (debitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrSumLDR.Text = "";
            }

            //}
            //else
            //{
            //    xrSumLDR.Text = "";
            //}
        }

        private void xrSumLCR_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //On 04/05/2023, Show always Ledger Total
            //if (this.ReportProperties.ShowByLedgerGroup == 1 && this.ReportProperties.ShowByLedgerSummary == 1)
            //{
            double CreditAmt = this.ReportProperties.NumberSet.ToDouble(xrSumLCR.Text);

            if (CreditAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrSumLCR.Text = "";
            }
            //}
            //else
            //{
            //    xrSumLCR.Text = "";
            //}
        }

        private void xrCellMonthTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 12/02/2021, While drilling, show month year name bsaed on setting
            if (IsDrillDownMode && showMonthlySummary)
            {
                XRTableCell cell = sender as XRTableCell;
                string monthOPDate = (GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName) == null) ? " " : GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(monthOPDate))
                {
                    monthOPDate = UtilityMember.DateSet.ToDate(monthOPDate, false).ToString("MMMM - yyyy");
                }

                cell.Padding = new PaddingInfo(165, cell.Padding.Right, cell.Padding.Top, cell.Padding.Bottom);
                cell.Text = monthOPDate;
                cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft; //DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 12/02/2021, While drilling, show opening balance
            if (IsDrillDownMode && showMonthlySummary)
            {
                string vouchersubtype = (GetCurrentColumnValue(this.ReportParameters.VOUCHER_TYPEColumn.ColumnName) == null) ? "" : GetCurrentColumnValue(this.ReportParameters.VOUCHER_TYPEColumn.ColumnName).ToString();
                e.Cancel = (string.IsNullOrEmpty(vouchersubtype.Trim()) ? false : true);
            }
        }

        private void grpHeaderVoucherMth_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //12/02/2021, Hide monthly opening balance
            if (IsDrillDownMode && showMonthlySummary)
            {
                e.Cancel = true;
            }
            else if (this.ReportProperties.ShowMonthTotal == 1 && GetCurrentColumnValue(this.LedgerParameters.VOUCHER_IDColumn.ColumnName) != null)
            {
                Int32 vid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.LedgerParameters.VOUCHER_IDColumn.ColumnName).ToString());

                //Show Ledger opening First month of Ledgder 
                if (this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 1 && vid > 0)
                {
                    e.Cancel = (this.ReportProperties.ShowNarrationMonthwiseCumulativeOpBalance == 0);
                }
                else
                    e.Cancel = (vid == 0);
            }
            else if (this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 1)
            {
                if (GetCurrentColumnValue(this.LedgerParameters.VOUCHER_IDColumn.ColumnName) != null)
                {
                    Int32 vid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.LedgerParameters.VOUCHER_IDColumn.ColumnName).ToString());
                    e.Cancel = (vid == 0);
                }

                //on 02/09/2022, 
                if (GetCurrentColumnValue(this.LedgerParameters.NATURE_IDColumn.ColumnName) != null)
                {
                    if (ReportProperty.Current.ShowNarrationMonthwiseCumulativeTotal == 1)
                    {
                        if (ReportProperty.Current.ShowLedgerOpBalance == 0 && ReportProperty.Current.ShowAssetLiabilityLedgerOpBalance == 1)
                        {
                            Int32 natureid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.LedgerParameters.NATURE_IDColumn.ColumnName).ToString());
                            if (!e.Cancel)
                            {
                                e.Cancel = (natureid == (Int32)Natures.Income || natureid == (Int32)Natures.Expenses);
                            }
                        }
                        else if (ReportProperty.Current.ShowLedgerOpBalance == 0 && ReportProperty.Current.ShowAssetLiabilityLedgerOpBalance == 0)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void grpLedgerFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //16/02/2021, to hide opening month total
            string monthOPDate = (GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName) == null) ? " " : GetCurrentColumnValue(this.LedgerParameters.DATEColumn.ColumnName).ToString();
            e.Cancel = (string.IsNullOrEmpty(monthOPDate));
        }

        private void xrRowNarration_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string LedgerDetail = (GetCurrentColumnValue("LEDGER_DETAIL") == null) ? string.Empty : GetCurrentColumnValue("LEDGER_DETAIL").ToString();
            if (this.ReportProperties.IncludeNarration == 1 || (!string.IsNullOrEmpty(LedgerDetail) && this.ReportProperties.ShowAllAgainstLedgers == 1))
            {
                string narration = (GetCurrentColumnValue(this.LedgerParameters.NARRATIONColumn.ColumnName) == null) ? " " : GetCurrentColumnValue(this.LedgerParameters.NARRATIONColumn.ColumnName).ToString();

                //narration += LedgerDetail;
                if (this.ReportProperties.ShowAllAgainstLedgers == 1)
                {
                    narration += LedgerDetail;
                }

                if (string.IsNullOrEmpty(narration.Trim()) && !CanShowProjectInNarration)
                {
                    xrRowNarration.Visible = false;
                }
                else
                {
                    xrRowNarration.Visible = true;
                }
            }
            else
            {
                xrRowNarration.Visible = false;
            }
        }

        private void xrRowData_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string narration = (GetCurrentColumnValue(this.LedgerParameters.NARRATIONColumn.ColumnName) == null) ? " " : GetCurrentColumnValue(this.LedgerParameters.NARRATIONColumn.ColumnName).ToString();
            //Left,Top,Right
            xrDate.Borders = xrVoucherNo.Borders = xrVoucherType.Borders = xrParticulars.Borders = xrDebit.Borders = xrCrdit.Borders = xrClosingBalance.Borders = BorderSide.Left | BorderSide.Right;
            if (string.IsNullOrEmpty(narration.Trim()) && !this.CanShowProjectInNarration)
            {
                xrRowData.Borders = BorderSide.All;
                xrDate.Borders = xrVoucherNo.Borders = xrVoucherType.Borders = xrParticulars.Borders = xrDebit.Borders = xrCrdit.Borders = xrClosingBalance.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
            }
        }

        private void xrcellParticularsDate_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(this.LedgerParameters.NARRATIONColumn.ColumnName) != null)
            {
                Int32 vid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.LedgerParameters.VOUCHER_IDColumn.ColumnName).ToString());
                if (vid > 0)
                {
                    DateTime date = UtilityMember.DateSet.ToDate(e.Value.ToString(), false);
                    var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
                    DateTime lastdayofthemonth = new DateTime(date.Year, date.Month, lastDayOfMonth);
                    e.Value = lastdayofthemonth.ToShortDateString();
                }
                else
                {
                    e.Value = this.ReportProperties.DateFrom;
                }
            }
        }


        private void xrcellParticularsBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = UtilityMember.NumberSet.ToNumber(Math.Abs(MonthlyCumulativeBalance)) + " " + (MonthlyCumulativeBalance < 0 ? " Cr" : " Dr");
            e.Handled = true;
        }

        private void xrcellParticularsDR_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            for (int i = 0; i <= e.CalculatedValues.Count - 1; i++)
            {
                MonthlyCumulativeBalance += UtilityMember.NumberSet.ToDouble(e.CalculatedValues[i].ToString());
            }
            //e.Handled = true;
        }

        private void xrcellParticularsCR_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            for (int i = 0; i <= e.CalculatedValues.Count - 1; i++)
            {
                MonthlyCumulativeBalance -= UtilityMember.NumberSet.ToDouble(e.CalculatedValues[i].ToString());
            }
            //e.Handled = true;
        }

        private void grpHeaderParticulars_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void grpLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            MonthlyCumulativeBalance = 0;
        }

        private void grpBreakUpLedger_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 04/08/2022, to break ledger-wise only if ledger name changes
            if (this.ReportProperties.BreakByLedger == 1 && GetCurrentColumnValue(reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName) != null)
            {
                //if (GetCurrentColumnValue(reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName).ToString()
                //    == GetNextColumnValue(reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName).ToString())

                if (CurrentRowIndex < RowCount - 1 && GetCurrentColumnValue(reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName).ToString()
                    == GetNextColumnValue(reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName).ToString())
                {
                    e.Cancel = true;
                }

                if (CurrentRowIndex == RowCount - 1)
                {
                    xrPageBreak1.Visible = false;
                }
            }
        }

        /// <summary>
        /// Updated 14/05/2025, *Chinna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grpHeadrLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 09/08/2022 for TOC content --------------------------------------------------------------------
            if (GetCurrentColumnValue(this.LedgerParameters.LEDGER_NAMEColumn.ColumnName) != null)
            {
                Int32 lid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("LEDGER_ID").ToString());
                Int32 pageinex = this.PrintingSystem.PageCount + TOCPageOffset;
                string ledgername = GetCurrentColumnValue(this.LedgerParameters.LEDGER_NAMEColumn.ColumnName).ToString();
                string filter = string.Format("{0} = '{1}'", reportSetting1.TOC.TOC_NAMEColumn.ColumnName, ledgername.Replace("'", "''"));

                if (dtTOCList.Select(filter).Length == 0)
                {
                    DataRow drTOC = dtTOCList.NewRow();
                    drTOC[reportSetting1.TOC.TOC_NAMEColumn.ColumnName] = ledgername;
                    drTOC[reportSetting1.TOC.PAGE_NOColumn.ColumnName] = pageinex; // this.PrintingSystem.Pages.Count + 1; // Temporary page number
                    dtTOCList.Rows.Add(drTOC);
                }
            }
            //------------------------------------------------------------------------------------------------------

            //On 14/04/2023, To reset narration cumulative balance when ledger changes -----------------------------
            if (this.ReportProperties.ShowNarrationMonthwiseCumulativeTotal == 1)
            {
                MonthlyCumulativeBalance = 0;
            }
            //------------------------------------------------------------------------------------------------------
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // 14/05/2025, *Chinna
            //On 09/08/2022 for TOC content -------------------------------------------------------------------------
            //if (!this.IsDrillDownMode && ReportProperty.Current.ShowTableofContent == 1 &&
            //    grpHeadrLedgerName.Visible && this.DataSource != null)
            //{
            //    if ((this.DataSource as DataView).Table.Rows.Count > 0)
            //    {
            //        UcTOC uctoc = new UcTOC();
            //        uctoc.CCTableWidth = xrtblHeaderCaption.WidthF;
            //        uctoc.BindCCDetails(dtTOCList);
            //        uctoc.CreateDocument();
            //        uctoc.NoOfTOCPages = uctoc.Pages.Count;

            //        int pidex = 0;
            //        foreach (Page p in uctoc.Pages)
            //        {
            //            this.Pages.Insert(pidex, p);
            //            pidex++;
            //        }

            //        //CreateLinks(this);
            //    }
            //}
        }

        private void CreateLinks(XtraReport report)
        {
            List<VisualBrick> linkBricks = new List<VisualBrick>();
            List<TargetBrick> targetBricks = new List<TargetBrick>();

            foreach (Page page in report.Pages)
            {
                NestedBrickIterator iterator = new NestedBrickIterator(page.InnerBricks);

                while (iterator.MoveNext())
                {
                    VisualBrick brick = iterator.CurrentBrick as VisualBrick;
                    if (brick != null && brick.Value != null)
                    {
                        string valueString = brick.Value.ToString();

                        if (valueString.StartsWith("Link_"))
                        {
                            linkBricks.Add(brick);

                            LabelBrick brickTarget = new LabelBrick();
                            brickTarget.Text = valueString.Substring(5);
                            brickTarget.TextValue = valueString.Substring(5);
                            brickTarget.Value = "Target_" + valueString.Substring(5);
                            //targetBricks.Add(new TargetBrick() { Brick = brickTarget, Page = page });
                        }

                        if (valueString.StartsWith("Target_"))
                        {
                            targetBricks.Add(new TargetBrick() { Brick = brick, Page = page });
                        }
                    }
                }
            }

            foreach (VisualBrick link in linkBricks)
            {
                string key = link.Value.ToString().Substring(5);

                TargetBrick target = targetBricks.Find(targetBrick => (string)targetBrick.Brick.Value == String.Concat("Target_", key));
                if (target != null)
                {
                    link.NavigationPair = BrickPagePair.Create(target.Brick, target.Page);
                }
            }

        }

        private void xrLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("LEDGER_NAME") != null)
            {
                XRControl label = sender as XRControl;
                label.Tag = String.Format("Target_{0}", GetCurrentColumnValue("LEDGER_NAME"));
            }
        }


        /// <summary>
        /// 14/05/2025, Page Number *Chinna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Ledger_AfterPrint(object sender, EventArgs e)
        {
            if (!this.IsDrillDownMode &&
        ReportProperty.Current.ShowTableofContent == 1 &&
        grpHeadrLedgerName.Visible &&
        this.DataSource != null &&
        dtTOCList != null &&
        dtTOCList.Rows.Count > 0)
            {
                // Step 1: Update TOC page numbers after full layout
                UpdateTOCPages();

                // Step 2: Generate TOC report
                UcTOC uctoc = new UcTOC();
                uctoc.CCTableWidth = xrtblHeaderCaption.WidthF;
                uctoc.BindCCDetails(dtTOCList);
                uctoc.CreateDocument();
                uctoc.NoOfTOCPages = uctoc.Pages.Count;
                TOCPageOffset = uctoc.NoOfTOCPages;


                // Step 3: Insert TOC at beginning
                int insertIndex = 0;
                foreach (Page tocPage in uctoc.Pages)
                {
                    this.Pages.Insert(insertIndex++, tocPage);
                }

                // Step 4: Add navigation links
                CreateLinks(this);
            }
        }

        /// <summary>
        /// 14/05/2025, *Chinna
        /// </summary>
        private void UpdateTOCPages()
        {
            if (dtTOCList == null || dtTOCList.Rows.Count == 0)
                return;

            for (int i = 0; i < this.Pages.Count; i++)
            {
                Page page = this.Pages[i];
                NestedBrickIterator iterator = new NestedBrickIterator(page.InnerBricks);

                while (iterator.MoveNext())
                {
                    VisualBrick brick = iterator.CurrentBrick as VisualBrick;
                    if (brick != null && brick.Value != null && brick.Value.ToString().StartsWith("Target_"))
                    {
                        string ledgerName = brick.Value.ToString().Substring("Target_".Length);
                        dtTOCList.DefaultView.RowFilter = reportSetting1.TOC.TOC_NAMEColumn.ColumnName + " = '" + ledgerName.Replace("'", "''") + "'";

                        if (dtTOCList.DefaultView.Count > 0)
                        {
                            DataRowView rowView = dtTOCList.DefaultView[0];
                            rowView[reportSetting1.TOC.PAGE_NOColumn.ColumnName] = i + 1 + TOCPageOffset;
                            rowView.Row.AcceptChanges();
                        }

                        dtTOCList.DefaultView.RowFilter = string.Empty;
                    }
                }
            }
        }

        private void xrSumLDummy_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void xrSumLDR_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LedgerSummaryDrAmtWithLedgerGroup = 0;
            for (int i = 0; i <= e.CalculatedValues.Count - 1; i++)
            {
                LedgerSummaryDrAmtWithLedgerGroup += UtilityMember.NumberSet.ToDouble(e.CalculatedValues[i].ToString());
            }
        }

        private void xrSumLCR_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LedgerSummaryCrAmtWithLedgerGroup = 0;
            for (int i = 0; i <= e.CalculatedValues.Count - 1; i++)
            {
                LedgerSummaryCrAmtWithLedgerGroup += UtilityMember.NumberSet.ToDouble(e.CalculatedValues[i].ToString());
            }
        }

        private void xrSumLDummy_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double LedgerSummaryBalanceAmtWithLedgerGroup = (LedgerSummaryDrAmtWithLedgerGroup - LedgerSummaryCrAmtWithLedgerGroup);

            if (LedgerSummaryBalanceAmtWithLedgerGroup < 0)
            {
                e.Result = UtilityMember.NumberSet.ToNumber(Math.Abs(LedgerSummaryBalanceAmtWithLedgerGroup)) + " Cr";
            }
            else
            {
                e.Result = UtilityMember.NumberSet.ToNumber(Math.Abs(LedgerSummaryBalanceAmtWithLedgerGroup)) + " Dr";
            }
            e.Handled = true;
        }

        private void xrParticulars_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }

        private void xrcellParticulars_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }


        private void MakeOPTableCellBold(object sender)
        {
            if (GetCurrentColumnValue(this.LedgerParameters.VOUCHER_IDColumn.ColumnName) != null)
            {
                Int32 vid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.LedgerParameters.VOUCHER_IDColumn.ColumnName).ToString());
                string ledgername = GetCurrentColumnValue(this.LedgerParameters.PARTICULARS1Column.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.Font = new Font("Tahoma", 9, FontStyle.Regular);

                if (vid == 0 || ledgername.Trim().Equals(OPBALTITLE))
                {
                    cell.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);//8.75F
                }
            }
        }

        private void xrcellParticularsDR_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }

        private void xrcellParticularsCR_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }

        private void xrcellParticularsBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }

        private void xrcellParticularsDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }

        private void xrClosingBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 07/12/2022, To change Font for Opening Balance
            MakeOPTableCellBold(sender);
        }

        private void xrcellNarration_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (e.Value != null)
            {
                string narration = e.Value.ToString().Trim();
                Int32 pid = 0;

                string LedgerDetail = (GetCurrentColumnValue("LEDGER_DETAIL") == null) ? string.Empty : GetCurrentColumnValue("LEDGER_DETAIL").ToString();
                if (this.ReportProperties.ShowAllAgainstLedgers == 1)
                {
                    if (this.ReportProperties.IncludeNarration == 0 &&
                        this.ReportProperties.IncludeNarrationwithNameAddress == 0 && this.ReportProperties.IncludeNarrationwithRefNo == 0)
                    {
                        narration = string.Empty;
                    }
                    //narration += LedgerDetail;
                    if (this.ReportProperties.ShowAllAgainstLedgers == 1)
                    {
                        narration += LedgerDetail;
                    }
                }

                if (narration.Contains("<Br>"))
                {
                    narration = narration.Trim().Replace("<Br>", System.Environment.NewLine);
                }

                //On 20/02/2024, To attach project details ------------------------
                if (CanShowProjectInNarration)
                {
                    if (dtProjectList != null && GetCurrentColumnValue(reportSetting1.ReportParameter.PROJECT_IDColumn.ColumnName) != null)
                    {
                        pid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.ReportParameter.PROJECT_IDColumn.ColumnName).ToString());
                        dtProjectList.DefaultView.RowFilter = string.Empty;
                        dtProjectList.DefaultView.RowFilter = reportSetting1.ReportParameter.PROJECT_IDColumn.ColumnName + " = " + pid;
                        if (dtProjectList.DefaultView.Count > 0)
                        {
                            string rtn = dtProjectList.DefaultView[0][reportSetting1.ReportParameter.PROJECTColumn.ColumnName].ToString();
                            if (!string.IsNullOrEmpty(rtn))
                            {
                                narration += (string.IsNullOrEmpty(narration) ? string.Empty : System.Environment.NewLine) + "Project : " + rtn;
                            }
                        }

                        dtProjectList.DefaultView.RowFilter = string.Empty;
                    }
                }
                //-----------------------------------------------------------------
                e.Value = narration;
            }
        }

        private void xrDRsumTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.ReportProperties.BreakByLedger == 1 && this.DataSource != null)
            {
                if (GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName) != null)
                {
                    Int32 lid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                    DataView dvBind = this.DataSource as DataView;
                    e.Result = dvBind.Table.Compute("SUM(" + reportSetting1.Ledger.DEBITColumn.ColumnName + ")", ReportParameters.LEDGER_IDColumn.ColumnName + " = " + lid);
                    e.Handled = true;
                }
            }
        }

        private void xrCRsumTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.ReportProperties.BreakByLedger == 1 && this.DataSource != null)
            {
                if (GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName) != null)
                {
                    Int32 lid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                    DataView dvBind = this.DataSource as DataView;
                    e.Result = dvBind.Table.Compute("SUM(" + reportSetting1.Ledger.CREDITColumn.ColumnName + ")", ReportParameters.LEDGER_IDColumn.ColumnName + " = " + lid);
                    e.Handled = true;
                }
            }
        }

        private void xrLedgerName_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //On 11/12/2023, To separate Ledger group and narration cumulative
            if (GetCurrentColumnValue("GROUP") != null && (ReportProperty.Current.ShowNarrationMonthwiseCumulativeTotal == 1
                    && ReportProperty.Current.ShowByLedgerGroup == 0))
            {

                e.Value = GetCurrentColumnValue("LEDGER_NAME").ToString() + " (" + GetCurrentColumnValue("GROUP").ToString() + ")";
            }
        }

        private void xrLedgerDebitBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 29/02/2024, To exclude opening balance when drilling for particular ledger
            if (IsDrillDownMode) //&& showMonthlySummary
            {
                if (GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName) != null)
                {
                    Int32 lid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                    DataView dvBind = this.DataSource as DataView;
                    string filter = ReportParameters.LEDGER_IDColumn.ColumnName + " = " + lid + " AND " + reportSetting1.Ledger.PARTICULARSColumn.ColumnName + "<> 'Opening Balance'"; ;
                    e.Result = dvBind.Table.Compute("SUM(" + reportSetting1.Ledger.DEBITColumn.ColumnName + ")", filter);
                    e.Handled = true;
                }
            }
        }

        private void xrLedgerCreditBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 29/02/2024, To exclude opening balance when drilling for particular ledger
            if (IsDrillDownMode) //&& showMonthlySummary
            {
                if (GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName) != null)
                {
                    Int32 lid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                    DataView dvBind = this.DataSource as DataView;
                    string filter = ReportParameters.LEDGER_IDColumn.ColumnName + " = " + lid + " AND " + reportSetting1.Ledger.PARTICULARSColumn.ColumnName + "<> 'Opening Balance'"; ;
                    e.Result = dvBind.Table.Compute("SUM(" + reportSetting1.Ledger.CREDITColumn.ColumnName + ")", filter);
                    e.Handled = true;
                }
            }
        }

        private void xrtblGrandClBal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 29/02/2024, To exclude opening balance when drilling for particular ledger
            if (IsDrillDownMode) //&& showMonthlySummary
            {
                if (GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName) != null)
                {
                    Int32 lid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                    DataView dvBind = this.DataSource as DataView;
                    string filter = ReportParameters.LEDGER_IDColumn.ColumnName + " = " + lid + " AND " + reportSetting1.Ledger.PARTICULARSColumn.ColumnName + "<> 'Opening Balance'"; ;
                    double debit = UtilityMember.NumberSet.ToDouble(dvBind.Table.Compute("SUM(" + reportSetting1.Ledger.DEBITColumn.ColumnName + ")", filter).ToString());
                    double credit = UtilityMember.NumberSet.ToDouble(dvBind.Table.Compute("SUM(" + reportSetting1.Ledger.CREDITColumn.ColumnName + ")", filter).ToString());
                    double clbalance = (debit - credit);
                    XRTableCell cell = sender as XRTableCell;
                    cell.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(clbalance)).ToString() + (clbalance > 0 ? " Dr" : " Cr");
                }
            }

        }

    }

    public class TargetBrick
    {
        public VisualBrick Brick
        {
            get;
            set;
        }

        public Page Page
        {
            get;
            set;
        }
    }
}
