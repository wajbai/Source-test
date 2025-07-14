using System;
using System.Drawing;
using System.Data;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraReports.UI;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;
using System.Globalization;

namespace Bosco.Report.ReportObject
{
    public partial class CashBankBook : Bosco.Report.Base.ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        int DailyGroupNumber = 0;
        double DailyGrpCashOpbalance = 0;
        double DailyGrpCashClbalance = 0;
        double DailyGrpBankOpbalance = 0;
        double DailyGrpBankClbalance = 0;

        double DailyCashReceipts = 0;
        double DailyBankPayments = 0;
        double DailyBankReceipts = 0;
        double DailyCashPayments = 0;

        int MonthGroupNumber = 0;
        double MonthlyGrpCashOpbalance = 0;
        double MonthlyGrpCashClbalance = 0;
        double MonthlyGrpBankOpbalance = 0;
        double MonthlyGrpBankClbalance = 0;

        double MonthlyCashReceipts = 0;
        double MonthlyBankPayments = 0;
        double MonthlyBankReceipts = 0;
        double MonthlyCashPayments = 0;

        // To Include Opening Balance of FD Details 17.06.2019
        double TotalBankReceipts = 0;
        double TotalBankPayments = 0;
        double FDOpeningBalance = 0;
        double FDClosingBalance = 0;
        double TotalCashReceipts = 0;
        double TotalCashPayments = 0;

        int count = 0;
        int Rcount = 0;
        const int HeaderTable = 1;
        const int NonHeaderTable = 0;
        string RecledAmount = string.Empty;
        string PayledAmount = string.Empty;

        DataTable dtFDAccounts = null;
        #endregion

        #region Constructor
        public CashBankBook()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblBindSource, xrReceipt,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
            this.AttachDrillDownToRecord(xrtblBindSource, xrPayments,
                new ArrayList { this.ReportParameters.PAY_VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_PAYMENT_SUB_TYPE");
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            CashAndBankBook();
            DailyGrpCashOpbalance = 0;
            DailyGrpCashClbalance = 0;
            DailyGrpBankOpbalance = 0;
            DailyGrpBankClbalance = 0;
            DailyGroupNumber = 0;

            MonthlyGrpCashOpbalance = 0;
            MonthlyGrpCashClbalance = 0;
            MonthlyGrpBankOpbalance = 0;
            MonthlyGrpBankClbalance = 0;
            MonthGroupNumber = 0;
            grpHeaderMonth.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;
            Rcount = 0;

            // To Include Opening Balance of FD Details 17.06.2019
            TotalBankReceipts = 0;
            TotalBankPayments = 0;
            TotalCashReceipts = 0;
            TotalCashPayments = 0;

        }

        private void CashAndBankBook()
        {
            SetReportTitle();

            //On 17/12/2018, Set Sign details
            FixReportPropertyForCMF();
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrtblGrandTotal.WidthF;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {

                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindProperty();
                        SplashScreenManager.CloseForm();
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
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    BindProperty();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            SetReportSettings();
        }

        private void BindProperty()
        {
            //this.ReportTitle = ReportProperty.Current.ReportTitle;
            //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            // SetReportTitle();
            //this.SetLandscapeHeader = 1040.25f;
            //this.SetLandscapeFooter = 1030.25f;
            //this.SetLandscapeFooterDateWidth = 890.00f;

            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;//1045.25f;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;// 1045.25f;
            this.SetLandscapeFooterDateWidth = 890.00f;

            // this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;



            //To Show OP & Cl Balance
            grpHeaderVoucherDate.Visible = (this.ReportProperties.ShowDailyBalance == 1);
            grpFooterVoucherDate.Visible = grpHeaderVoucherDate.Visible;

            //show monthtotal
            //grpHeaderMonth.Visible = (this.ReportProperties.IncludeLedgerGroupTotal == 1);
            //grpFooterMonth.Visible = grpHeaderMonth.Visible;

            grpHeaderOPBalance.Visible = (this.ReportProperties.ShowDailyBalance == 0);
            grpHeaderCLBalance.Visible = xrRowGrandTotal.Visible = grpHeaderOPBalance.Visible;

            prOPCashBalance.Visible = prCLCashBalance.Visible = prOPBankBalance.Visible = prCLBankBalance.Visible = false;
            prOPCashBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.CashBalance,
                                 BalanceSystem.BalanceType.OpeningBalance);

            prCLCashBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, BalanceSystem.LiquidBalanceGroup.CashBalance,
                                 BalanceSystem.BalanceType.ClosingBalance);

            //On 14/07/2021, to have bank balance with FD balance
            //prOPBankBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
            //                    BalanceSystem.BalanceType.OpeningBalance);
            double bankopening = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                BalanceSystem.BalanceType.OpeningBalance);
            prOPBankBalance.Value = bankopening;

            //prCLBankBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, BalanceSystem.LiquidBalanceGroup.BankBalance,
            //                     BalanceSystem.BalanceType.ClosingBalance);
            double bankclosing = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                 BalanceSystem.BalanceType.ClosingBalance);
            prCLBankBalance.Value = bankclosing;

            FDOpeningBalance = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.FDBalance,
                           BalanceSystem.BalanceType.OpeningBalance);

            FDClosingBalance = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, BalanceSystem.LiquidBalanceGroup.FDBalance,
                                 BalanceSystem.BalanceType.ClosingBalance);
            xrSubClosingBalance.Visible = xrSubOpeningBalance.Visible = true;

            //On 14/07/2021, to have bank balance with FD balance
            prOPBankBalance.Value = bankopening; // +FDOpeningBalance;
            prCLBankBalance.Value = bankclosing; // +FDClosingBalance;

            if (ReportProperty.Current.ShowDetailedBalance == 1)
            {
                LoadDetailedBalance();
            }
            else
            {
                xrSubClosingBalance.Visible = xrSubOpeningBalance.Visible = grpHeaderDetailedOPBalance.Visible = grpDetailedCLBalance.Visible = false;
            }

            resultArgs = GetReportSource();
            this.DataSource = null;
            DataView dvCashBankBook = resultArgs.DataSource.TableView;
            if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
            {
                dvCashBankBook.Table.TableName = "CashBankBook";
                //On 05/06/2017, To add Amount filter condition
                AttachAmountFilter(dvCashBankBook);
                this.DataSource = dvCashBankBook;
                this.DataMember = dvCashBankBook.Table.TableName;
                Detail.Visible = true;
                grpHeaderMonth.Visible = grpFooterMonth.Visible = (this.ReportProperties.IncludeLedgerGroupTotal == 1);
            }
            else
            {
                //Hide empty rows if there is not records
                Detail.Visible = grpHeaderMonth.Visible = grpFooterMonth.Visible = false;
            }


            if (this.ReportProperties.IncludeNarrationwithRefNo == 1 || this.ReportProperties.IncludeNarrationwithNameAddress == 1 || this.ReportProperties.IncludeNarrationwithCurrencyDetails == 1)
            {
                this.ReportProperties.IncludeNarration = 1;
            }


            // xrNarration.CanGrow = false;
            if (this.ReportProperties.IncludeNarration == 1)
            {
                string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
                string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
                if (string.IsNullOrEmpty(Narration) && (string.IsNullOrEmpty(Narration_Pay)))
                {
                    // xtNarrRec.ProcessNullValues = xrNarrPay.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
                }
            }



        }

        private void LoadDetailedBalance()
        {
            grpHeaderCLBalance.Visible = ReportFooter.Visible = true;
            grpHeaderOPBalance.Visible = xrSubClosingBalance.Visible = xrSubOpeningBalance.Visible = grpHeaderDetailedOPBalance.Visible = grpDetailedCLBalance.Visible = true;
            AccountBalance accountBalance = xrSubOpeningBalance.ReportSource as AccountBalance;
            accountBalance.LedgerNewFont = xrLedgerCode.Font;
            accountBalance.LedgerGroupNewFont = xrtblOPBankBalance.Font;
            accountBalance.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            accountBalance.IsCashBankBook = true;
            accountBalance.GroupProgressVisible = true;
            accountBalance.GroupPeriodVisible = true;
            accountBalance.BindBalance(true, false);
            this.AttachDrillDownToSubReport(accountBalance);
            if (ReportProperties.ShowLedgerCode == 1)
            {
                //accountBalance.CodeColumnWidth = 93.50F;
                //accountBalance.NameColumnWidth = 170.2F;
                //accountBalance.AmountColumnWidth = 135;
                //accountBalance.GroupNameWidth = 396.90f;
                //accountBalance.GroupPeriodVisible = false;
                accountBalance.CodeColumnWidth = xrCapDate.WidthF; //93.50F;
                accountBalance.NameColumnWidth = (xrCapRNo.WidthF + xrCapReceiptcode.WidthF + xrCapReceipt.WidthF + xrCabRecLedgerAmt.WidthF); //170.2F;
                accountBalance.AmountColumnWidth = xrCapRecCash.WidthF; //135;
                accountBalance.GroupNameWidth = (xrCapDate.WidthF + xrCapRNo.WidthF + xrCapReceiptcode.WidthF + xrCapReceipt.WidthF + xrCabRecLedgerAmt.WidthF) - 2;  //396.90f;
                accountBalance.GroupAmountWidth = xrCapRecCash.WidthF;
            }
            else
            {
                //accountBalance.CodeColumnWidth = 0;
                //accountBalance.NameColumnWidth = 170.2F + 93.50F;
                //accountBalance.AmountColumnWidth = 133;
                //accountBalance.GroupNameWidth = 396.90f;
                //accountBalance.GroupPeriodVisible = false;
                accountBalance.CodeColumnWidth = 0;
                accountBalance.NameColumnWidth = (xrCapDate.WidthF + xrCapRNo.WidthF + xrCapReceipt.WidthF + xrCapReceiptcode.WidthF + xrCabRecLedgerAmt.WidthF) - 2; //170.2F;
                accountBalance.AmountColumnWidth = xrCapRecCash.WidthF; //135;
                accountBalance.GroupNameWidth = (xrCapDate.WidthF + xrCapRNo.WidthF + xrCapReceipt.WidthF + xrCapReceiptcode.WidthF + xrCabRecLedgerAmt.WidthF) - 2;  //396.90f;
                accountBalance.GroupAmountWidth = xrCapRecCash.WidthF;
            }
            accountBalance.AmountProgressiveColumnWidth = xrCapRecBank.WidthF; //135;
            accountBalance.AmountProgressiveHeaderColumnWidth = xrCapRecBank.WidthF;

            Double ReceiptOPAmt = accountBalance.PeriodBalanceAmount;
            AccountBalance accountClosingBalance = xrSubClosingBalance.ReportSource as AccountBalance;
            accountClosingBalance.LedgerNewFont = xrLedgerCode.Font;
            accountClosingBalance.LedgerGroupNewFont = xrtblOPBankBalance.Font;
            // accountClosingBalance.setCBCClosingBalanceDetailWidth();
            accountClosingBalance.BankClosedDate = this.ReportProperties.DateFrom; //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
            accountClosingBalance.IsCashBankBook = true;
            accountClosingBalance.GroupProgressVisible = true;
            accountClosingBalance.GroupPeriodVisible = true;
            accountClosingBalance.BindBalance(false, true);
            this.AttachDrillDownToSubReport(accountClosingBalance);
            Double PaymentClAmt = accountClosingBalance.PeriodBalanceAmount;
            // Double PaymentClAmt = accountClosingBalance.ProgressiveBalanceAmount;

            if (ReportProperties.ShowLedgerCode == 1)
            {
                //accountClosingBalance.CodeColumnWidth = 49.50F;
                //accountClosingBalance.GroupNameWidth = 384;
                //accountClosingBalance.NameColumnWidth = 162F;
                //accountClosingBalance.AmountColumnWidth = 174;

                accountClosingBalance.CodeColumnWidth = xrCapPaymentVNo.WidthF;
                accountClosingBalance.NameColumnWidth = xrCapPaymentCode.WidthF + xrCapPayment.WidthF + xrCapPaymentLedAmt.WidthF;
                accountClosingBalance.AmountColumnWidth = xrCapPayCash.WidthF;
                accountClosingBalance.GroupNameWidth = (xrCapPaymentVNo.WidthF + xrCapPaymentCode.WidthF + xrCapPayment.WidthF + xrCapPaymentLedAmt.WidthF) - 2;
                accountClosingBalance.GroupAmountWidth = xrCapPayCash.WidthF;
            }
            else
            {
                /*accountClosingBalance.CodeColumnWidth = 0;
                accountClosingBalance.GroupNameWidth = 330F + 54;
                accountClosingBalance.NameColumnWidth = 162F;
                accountClosingBalance.AmountColumnWidth = 222;*/
                accountClosingBalance.CodeColumnWidth = 0;
                accountClosingBalance.NameColumnWidth = (xrCapPaymentCode.WidthF + xrCapPayment.WidthF + xrCapPaymentVNo.WidthF + xrCapPaymentLedAmt.WidthF) - 2;
                accountClosingBalance.AmountColumnWidth = xrCapPayCash.WidthF;
                accountClosingBalance.GroupNameWidth = (xrCapPaymentVNo.WidthF + xrCapPaymentCode.WidthF + xrCapPayment.WidthF + xrCapPaymentLedAmt.WidthF) - 2;
                accountClosingBalance.GroupAmountWidth = xrCapPayCash.WidthF;
            }

            //accountClosingBalance.AmountProgressiveColumnWidth = 0.0f;
            //accountClosingBalance.AmountProgressiveHeaderColumnWidth = 0.0f;
            accountClosingBalance.AmountProgressVisible = accountClosingBalance.GroupProgressVisible = true;
            accountClosingBalance.AmountProgressiveColumnWidth = xrCapPayBank.WidthF; //135;
            accountClosingBalance.AmountProgressiveHeaderColumnWidth = xrCapPayBank.WidthF;

            // xrtblBindSource.Borders = xrCapPayBank.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.CashBankBookDouble);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_REFNOColumn, this.ReportProperties.IncludeNarrationwithRefNo);
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_NAMEADDRESSColumn, this.ReportProperties.IncludeNarrationwithNameAddress);

                    // 30/04/2025, *Chinna
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_PAN_GSTColumn, this.ReportProperties.IncludePanwithGSTNo);

                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_CURRENCYColumn, this.ReportProperties.IncludeNarrationwithCurrencyDetails);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankBookQueryPath);
                }

                //On 18/01/2024, To get FD Accounts to show in narration
                dtFDAccounts = null;
                ResultArgs result = this.GetFDAccounts(this.ReportProperties.Project);
                if (result.Success && result.DataSource.Table != null)
                {
                    dtFDAccounts = result.DataSource.Table;
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }

        private void SetReportSettings()
        {
            float actualCodeWidth = xrCapReceiptcode.WidthF;
            bool isCapCodeVisible = true;
            count = 0;
            //Include / Exclude Code
            if (xrCapReceiptcode.Tag != null && xrCapReceiptcode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapReceiptcode.Tag.ToString());
            }
            else
            {
                xrCapReceiptcode.Tag = xrCapReceiptcode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            xrCapReceiptcode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrPay_LedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell19.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell26.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell31.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell32.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell58.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell63.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell38.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell49.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell53.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell43.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell54.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell51.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell55.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell71.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            if (!isCapCodeVisible)
            {
                //xrCapReceiptcode.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                //xrCapPaymentCode.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                //xrLedgerCode.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                //xrPay_LedgerCode.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
            }
            SetBorders();
        }

        private void SetBorders()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblOpeningBalance = AlignOpeningBalanceTable(xrtblOpeningBalance);
            xrtblDaillyClosingBalance = AlignDailyClosingBalance(xrtblDaillyClosingBalance);
            xrtblClosingBalance = AlignClosingBalance(xrtblClosingBalance);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrtblMonth = AlignTotalTable(xrtblMonth);
            xrTblMonthFooter = AlignTotalTable(xrTblMonthFooter);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            this.SetCurrencyFormat(xrCapRecCash.Text, xrCapRecCash);
            this.SetCurrencyFormat(xrCapRecCash.Text, xrCapPayCash);
            this.SetCurrencyFormat(xrCapRecBank.Text, xrCapRecBank);
            this.SetCurrencyFormat(xrCapRecBank.Text, xrCapPayBank);
            this.SetCurrencyFormat(xrCabRecLedgerAmt.Text, xrCabRecLedgerAmt);
            this.SetCurrencyFormat(xrCapPaymentLedAmt.Text, xrCapPaymentLedAmt);
        }

        public override XRTable AlignOpeningBalanceTable(XRTable table)
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
                            if (ReportProperties.IncludeNarration == 1)
                                if (ReportProperties.ShowDetailedBalance == 1)
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                else
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (ReportProperties.IncludeNarration == 1)
                            {
                                if (ReportProperties.ShowDetailedBalance == 1)
                                {
                                    if (count == 3 || count == 8)
                                        tcell.Borders = BorderSide.None;
                                    else
                                        tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    if (count == 3 || count == 8)
                                        tcell.Borders = BorderSide.None;
                                    else
                                        tcell.Borders = BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (ReportProperties.ShowDetailedBalance == 1)
                                {
                                    if (count == 3 || count == 8)
                                        tcell.Borders = BorderSide.None;
                                    else
                                        tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    if (count == 3 || count == 8)
                                        tcell.Borders = BorderSide.None;
                                    else
                                        tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                        }
                        else
                        {
                            if (ReportProperties.IncludeNarration == 1)
                            {
                                if (ReportProperties.ShowDetailedBalance == 1)
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right;
                                }
                            }
                            else
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        }
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignTotalTable(XRTable table)
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
                            tcell.Borders = BorderSide.All;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignGrandTotalTable(XRTable table)
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
                            tcell.Borders = BorderSide.All;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
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
                            tcell.Borders = BorderSide.All;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        }
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }
            return table;
        }

        private XRTable AlignDailyOpeningBalaceTable(XRTable table, int RowCount)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (RowCount == 1)
                        {
                            if (count == 1)
                            {
                                if (ReportProperties.IncludeNarration == 1)
                                    if (ReportProperties.ShowDetailedBalance == 1)
                                        tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Top;
                                    else
                                        if (ReportProperties.ShowDailyBalance == 1)
                                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                                        else
                                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Top;
                                else
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Top;
                            }
                            else if (ReportProperties.ShowLedgerCode != 1)
                            {
                                if (ReportProperties.IncludeNarration == 1)
                                {
                                    if (ReportProperties.ShowDetailedBalance == 1)
                                    {
                                        if (count == 3 || count == 8)
                                            tcell.Borders = BorderSide.None;
                                        else
                                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                                    }
                                    else
                                    {
                                        if (count == 3 || count == 8)
                                            tcell.Borders = BorderSide.None;
                                        else
                                            if (ReportProperties.ShowDailyBalance == 1)
                                                tcell.Borders = BorderSide.Right;
                                            else
                                                tcell.Borders = BorderSide.Right | BorderSide.Top;
                                    }
                                }
                                else
                                {
                                    if (ReportProperties.ShowDetailedBalance == 1)
                                    {
                                        if (count == 3 || count == 8)
                                            tcell.Borders = BorderSide.None;
                                        else
                                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                                    }
                                    else
                                    {
                                        if (count == 3 || count == 8)
                                            tcell.Borders = BorderSide.None;
                                        else
                                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                                    }
                                }
                            }
                            else
                            {
                                if (ReportProperties.IncludeNarration == 1)
                                {
                                    if (ReportProperties.ShowDetailedBalance == 1)
                                    {
                                        tcell.Borders = BorderSide.Right | BorderSide.Top;
                                    }
                                    else
                                    {
                                        if (ReportProperties.ShowDailyBalance == 1)
                                            tcell.Borders = BorderSide.Right;
                                        else
                                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                                    }
                                }
                                else
                                {
                                    if (ReportProperties.ShowDailyBalance == 1)
                                        tcell.Borders = BorderSide.Right;
                                    else
                                        tcell.Borders = BorderSide.Right | BorderSide.Top;
                                }
                            }
                        }

                        else
                        {
                            if (count == 1)
                            {
                                if (ReportProperties.IncludeNarration == 1)
                                    if (ReportProperties.ShowDetailedBalance == 1 || ReportProperties.ShowDailyBalance == 1)
                                        tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                    else
                                        tcell.Borders = BorderSide.Left | BorderSide.Right;
                                else
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                            else if (ReportProperties.ShowLedgerCode != 1)
                            {
                                if (ReportProperties.IncludeNarration == 1)
                                {
                                    if (ReportProperties.ShowDetailedBalance == 1 || ReportProperties.ShowDailyBalance == 1)
                                    {
                                        if (count == 3 || count == 8)
                                            tcell.Borders = BorderSide.None;
                                        else
                                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                    }
                                    else
                                    {
                                        if (count == 3 || count == 8)
                                            tcell.Borders = BorderSide.None;
                                        else
                                            tcell.Borders = BorderSide.Right;
                                    }
                                }
                                else
                                {
                                    if (ReportProperties.ShowDetailedBalance == 1 || ReportProperties.ShowDailyBalance == 1)
                                    {
                                        if (count == 3 || count == 8)
                                            tcell.Borders = BorderSide.None;
                                        else
                                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                    }
                                    else
                                    {
                                        if (count == 3 || count == 8)
                                            tcell.Borders = BorderSide.None;
                                        else
                                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                    }
                                }
                            }
                            else
                            {
                                if (ReportProperties.IncludeNarration == 1)
                                {
                                    if (ReportProperties.ShowDetailedBalance == 1 || ReportProperties.ShowDailyBalance == 1)
                                    {
                                        tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                    }
                                    else
                                    {
                                        tcell.Borders = BorderSide.Right;
                                    }
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        }
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        private XRTable AlignDailyClosingBalance(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        /// <summary>
        /// This method is used to add filter condition
        /// </summary>
        private DataView AttachAmountFilter(DataView dv)
        {
            //On 05/06/2017, To add Amount filter condition
            lblAmountFilter.Visible = false;
            string AmountFilter = this.GetAmountFilter();
            if (AmountFilter != "")
            {
                dv.RowFilter = "(CASH > 0 AND CASH " + AmountFilter + ") OR (BANK > 0 AND BANK " + AmountFilter + ")" +
                              " OR (PAY_CASH > 0 AND PAY_CASH " + AmountFilter + ") OR (PAY_BANK > 0 AND PAY_BANK " + AmountFilter + ")";

                lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                lblAmountFilter.Visible = true;
            }
            return dv;
        }

        /// <summary>
        /// On 18/01/2024, To get FD Account Number
        /// </summary>
        /// <param name="FDAccountId"></param>
        /// <returns></returns>
        private string GetFDAccountNumber(Int32 FDAccountId)
        {
            string rtn = string.Empty;
            if (dtFDAccounts != null && dtFDAccounts.Rows.Count > 0)
            {
                dtFDAccounts.DefaultView.RowFilter = string.Empty;
                dtFDAccounts.DefaultView.RowFilter = ReportParameters.FD_ACCOUNT_IDColumn.ColumnName + " = " + FDAccountId;
                if (dtFDAccounts.DefaultView.Count > 0)
                {
                    rtn = dtFDAccounts.DefaultView[0][reportSetting1.FDRegister.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString();
                }
            }
            return rtn;
        }

        #endregion

        #region Events
        private void xrtblCashDailyOPBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            if (DailyGroupNumber == 0)
            {
                DailyGrpCashOpbalance = this.UtilityMember.NumberSet.ToDouble(this.prOPCashBalance.Value.ToString());
                DailyGrpBankOpbalance = this.UtilityMember.NumberSet.ToDouble(this.prOPBankBalance.Value.ToString());
            }
            else
            {
                DailyGrpCashOpbalance = DailyGrpCashClbalance;
                DailyGrpBankOpbalance = DailyGrpBankClbalance;
            }
            e.Result = DailyGrpCashOpbalance;
            e.Handled = true;
        }

        private void xrtblDailyOPBankBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyGrpBankOpbalance;
            e.Handled = true;
        }

        private void xrtblCashDailyOPBalance_SummaryReset(object sender, EventArgs e)
        {
            DailyGrpCashOpbalance = DailyGrpBankOpbalance = 0;
        }

        private void xrtblDailyRecCashBalance_SummaryReset(object sender, EventArgs e)
        {
            DailyGroupNumber++;
            DailyCashReceipts = DailyCashPayments = DailyBankReceipts = DailyBankPayments = 0;
        }

        private void xrtblDailyRecCashBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            DailyCashReceipts += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.CASHColumn.ColumnName).ToString());
            DailyCashPayments += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.PAY_CASHColumn.ColumnName).ToString());
            DailyBankReceipts += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.BANKColumn.ColumnName).ToString());
            DailyBankPayments += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.PAY_BANKColumn.ColumnName).ToString());
        }

        private void xrtblDailyRecCashBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            DailyGrpCashClbalance = (DailyGrpCashOpbalance + DailyCashReceipts) - DailyCashPayments;
            DailyGrpBankClbalance = (DailyGrpBankOpbalance + DailyBankReceipts) - DailyBankPayments;

            // To Include Opening Balance of FD Details 17.06.2019
            TotalBankReceipts += DailyBankReceipts;
            TotalBankPayments += DailyBankPayments;
            TotalCashReceipts += DailyCashReceipts;
            TotalCashPayments += DailyBankPayments;

            e.Result = DailyCashReceipts + DailyGrpCashOpbalance;
            e.Handled = true;
        }

        private void xrtblDailyRecBankBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            //On 14/07/2021, to have bank balance with FD balance
            //e.Result = DailyBankReceipts + DailyGrpBankOpbalance + FDOpeningBalance;
            e.Result = DailyBankReceipts + DailyGrpBankOpbalance;
            e.Handled = true;
        }

        private void xrtblDailyPayCashBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyCashPayments + DailyGrpCashClbalance;
            e.Handled = true;
        }

        private void xrtblDailyPayBankBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            //On 14/07/2021, to have bank balance with FD balance
            //e.Result = DailyBankPayments + DailyGrpBankClbalance + FDClosingBalance;
            e.Result = DailyBankPayments + DailyGrpBankClbalance;
            e.Handled = true;
        }

        private void xrtblDailyCLCashBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyGrpCashClbalance;
            e.Handled = true;
        }

        private void xrtblDailyCLBankBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyGrpBankClbalance;
            e.Handled = true;
        }

        private void xrPayment_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentCash = this.ReportProperties.NumberSet.ToDouble(xrRec_Cash.Text);
            if (paymentCash != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrRec_Cash.Text = "";
            }
        }

        private void xrPay_Cash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentBank = this.ReportProperties.NumberSet.ToDouble(xrRec_Bank.Text);
            if (paymentBank != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrRec_Bank.Text = "";
            }
        }

        private void xrPaymentCash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentCash = this.ReportProperties.NumberSet.ToDouble(xrPaymentCash.Text);
            if (paymentCash != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrPaymentCash.Text = "";
            }
        }

        private void xrPaymentBank_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentBank = this.ReportProperties.NumberSet.ToDouble(xrPaymentBank.Text);
            if (paymentBank != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrPaymentBank.Text = "";
            }

        }

        private void xrTblDailyOpeningBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Rcount++;
            xrTblDailyOpeningBalance = AlignDailyOpeningBalaceTable(xrTblDailyOpeningBalance, Rcount);
        }

        private void xrtblBindSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // SetTableborder(0);
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
            xrtblBindSource = AlignCashBankBookTable(xrtblBindSource, Narration, Narration_Pay, count);
        }

        private void xtNarrRec_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (this.ReportProperties.IncludeNarration == 1)
            //{
            //    string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            //    string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
            //    if (string.IsNullOrEmpty(Narration) && string.IsNullOrEmpty(Narration_Pay))
            //    {
            //        xrPay_Cash.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
            //        xtNarrRec.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
            //        // xrNarration.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            //        xrDate.Borders = xrReceipt.Borders = xrCash.Borders = xrBank.Borders = xrPayment.Borders = xrPay_Cash.Borders = xrPay_Bank.Borders = xrPaymentCode.Borders = xrPayments.Borders =
            //        xrPaymentCash.Borders = xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
            //        xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
            //        xrtblBindSource.Borders = DevExpress.XtraPrinting.BorderSide.All;
            //        xtNarrRec.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
            //        xrNarrPay.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
            //    }

            //    else
            //    {
            //        xtNarrRec.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
            //        xrNarrPay.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
            //        xrNarration.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //        xrtblBindSource.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //        xrDate.Borders = xrReceipt.Borders = xrCash.Borders = xrBank.Borders = xrPayment.Borders = xrPay_Cash.Borders = xrPay_Bank.Borders = xrPaymentCode.Borders = xrPayments.Borders =
            //        xrPaymentCash.Borders = xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //        xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            //        xrPay_Cash.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            //        xtNarrRec.ProcessNullValues = ValueSuppressType.Leave;
            //        xrNarrPay.ProcessNullValues = ValueSuppressType.Leave;
            //    }
            //}
        }

        private void xrNarrPay_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (this.ReportProperties.IncludeNarration == 1)
            //{
            //    string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            //    string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
            //    if (string.IsNullOrEmpty(Narration_Pay) && string.IsNullOrEmpty(Narration))
            //    {
            //        xrNarrPay.CanGrow = false;
            //        xrNarration.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            //        xrNarrPay.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
            //        xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
            //        xrtblBindSource.Borders = DevExpress.XtraPrinting.BorderSide.All;
            //        xrDate.Borders = xrReceipt.Borders = xrCash.Borders = xrBank.Borders = xrPayment.Borders = xrPay_Cash.Borders = xrPay_Bank.Borders = xrPaymentCode.Borders = xrPayments.Borders =
            //        xrPaymentCash.Borders = xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
            //        xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
            //        xrNarrPay.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
            //    }
            //    else
            //    {
            //        xrNarrPay.CanGrow = true;
            //        xtNarrRec.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
            //        xrNarrPay.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
            //        xrNarration.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //        xrtblBindSource.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //        xrDate.Borders = xrReceipt.Borders = xrCash.Borders = xrBank.Borders = xrPayment.Borders = xrPay_Cash.Borders = xrPay_Bank.Borders = xrPaymentCode.Borders = xrPayments.Borders =
            //        xrPaymentCash.Borders = xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //        xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            //        xrPay_Cash.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            //        xtNarrRec.ProcessNullValues = ValueSuppressType.Leave;
            //        xrNarrPay.ProcessNullValues = ValueSuppressType.Leave;
            //    }
            //}
        }

        //private void xrNarration_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    if (this.ReportProperties.IncludeNarration == 1)
        //    {
        //        string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
        //        string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
        //        if (string.IsNullOrEmpty(Narration.Trim()) && string.IsNullOrEmpty(Narration_Pay.Trim()))
        //        {
        //            //xtNarrRec.CanGrow = false;
        //            xrNarration.HeightF =/* xtNarrRec.HeightF =*/ 1.0f;
        //           // xrNarrPay.CanGrow = false;
        //            xrNarration.HeightF = /*xrNarrPay.HeightF =*/ 1.0f;
        //            Detail.HeightF = 25.0f;
        //            xrTemp1.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
        //            xrTemp2.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
        //            xrTemp3.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
        //            xrTemp4.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
        //            xrTemp5.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
        //            xrTemp6.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
        //            xrTemp7.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
        //            xrTemp8.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
        //            xrNarration.Visible = false;
        //            e.Cancel = true;
        //            //xrDate.Borders = xrReceiptNo.Borders = xrLedgerCode.Borders= xrReceipt.Borders = xrRec_Cash.Borders = xrRec_Bank.Borders = xrPay_VoucherNo.Borders = xrPay_LedgerCode.Borders = xrPayments.Borders= xrPaymentCash.Borders = xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
        //        }
        //        else
        //        {
        //            xrNarration.Visible = true;
        //            xrNarration.HeightF = 25.0f;
        //            Detail.HeightF = 51.0f;
        //            SetTableborder(1); 
        //            xrTemp1.ProcessNullValues = ValueSuppressType.Leave;
        //            xrTemp2.ProcessNullValues = ValueSuppressType.Leave;
        //            xrTemp3.ProcessNullValues = ValueSuppressType.Leave;
        //            xrTemp4.ProcessNullValues = ValueSuppressType.Leave;
        //            xrTemp5.ProcessNullValues = ValueSuppressType.Leave;
        //            xrTemp6.ProcessNullValues = ValueSuppressType.Leave;
        //            xrTemp7.ProcessNullValues = ValueSuppressType.Leave;
        //            xrTemp8.ProcessNullValues = ValueSuppressType.Leave;
        //            xrTemp9.ProcessNullValues = ValueSuppressType.Leave;
        //            if (Narration.Trim() == string.Empty)
        //                xtNarrRec.ProcessNullValues = ValueSuppressType.Leave;
        //            if (Narration_Pay.Trim() == string.Empty)
        //                xrNarrPay.ProcessNullValues = ValueSuppressType.Leave;
        //           // xtNarrRec.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
        //           // xrNarrPay.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
        //           //xrDate.Borders = xrReceiptNo.Borders = xrLedgerCode.Borders = xrReceipt.Borders = xrRec_Cash.Borders = xrRec_Bank.Borders = xrPay_VoucherNo.Borders = xrPay_LedgerCode.Borders = xrPayments.Borders = xrPaymentCash.Borders = xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;

        //            //xrTemp1.Borders = xrTemp2.Borders = xrTemp3.Borders = xrTemp4.Borders = xrTemp5.Borders = xrTemp6.Borders = xrTemp7.Borders = xrTemp8.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        //            ////xtNarrRec.Borders = xrNarrPay.Borders = DevExpress.XtraPrinting.BorderSide.None;
        //            //xrNarrPay.Borders = xtNarrRec.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
        //            //xrNarration.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
        //        }
        //    }
        //    else
        //    {
        //       // xtNarrRec.CanGrow = xrNarrPay.CanGrow = false;
        //        //xrNarration.Visible = false;
        //        xrNarration.HeightF =/* xtNarrRec.HeightF = xrNarrPay.HeightF =*/ 0.0f;
        //        Detail.HeightF = 25.0f;
        //       // xrNarration.Borders = xtNarrRec.Borders = xrNarrPay.Borders = DevExpress.XtraPrinting.BorderSide.None;
        //       // xrDate.Borders = xrReceiptNo.Borders = xrLedgerCode.Borders = xrReceipt.Borders = xrRec_Cash.Borders = xrRec_Bank.Borders = xrPay_VoucherNo.Borders = xrPay_LedgerCode.Borders = xrPayments.Borders = xrPaymentCash.Borders = xrPaymentBank.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
        //        xrNarration.Visible = false;
        //    }
        //}

        private void xrTableCell74_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string RecLedgerAmount = (GetCurrentColumnValue("REC_LEDGER_AMOUNT") == null) ? string.Empty : GetCurrentColumnValue("REC_LEDGER_AMOUNT").ToString();
            if (!string.IsNullOrEmpty(RecLedgerAmount))
            {
                string[] sd = RecLedgerAmount.Split(' ');
                if (sd.Length > 0)
                {
                    double recLedAmt = double.Parse(sd[0], System.Globalization.CultureInfo.InvariantCulture);
                    xrTableCell74.Text = RecledAmount = this.UtilityMember.NumberSet.ToNumber(recLedAmt) + " " + sd[1];
                }
            }
        }

        private void xrTableCell74_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = RecledAmount;
            //e.Handled = true;
        }

        private void xrTableCell76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string PayLedgerAmount = (GetCurrentColumnValue("PAY_LEDGER_AMOUNT") == null) ? string.Empty : GetCurrentColumnValue("PAY_LEDGER_AMOUNT").ToString();
            if (!string.IsNullOrEmpty(PayLedgerAmount))
            {
                string[] sd = PayLedgerAmount.Split(' ');
                if (sd.Length > 0)
                {
                    double recLedAmt = double.Parse(sd[0], System.Globalization.CultureInfo.InvariantCulture);
                    xrTableCell76.Text = PayledAmount = this.UtilityMember.NumberSet.ToNumber(recLedAmt) + " " + sd[1];
                }
            }
        }

        private void xrTableCell76_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = PayledAmount;
            //e.Handled = true;
        }

        private void xrTableCell74_SummaryRowChanged(object sender, EventArgs e)
        {
            //xrTableCell74.Text = PayledAmount;
        }

        private void xrCashMonthOP_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (MonthGroupNumber == 0)
            {
                MonthlyGrpCashOpbalance = this.UtilityMember.NumberSet.ToDouble(this.prOPCashBalance.Value.ToString());
                MonthlyGrpBankOpbalance = this.UtilityMember.NumberSet.ToDouble(this.prOPBankBalance.Value.ToString());
            }
            else
            {
                MonthlyGrpCashOpbalance = MonthlyGrpCashClbalance;
                MonthlyGrpBankOpbalance = MonthlyGrpBankClbalance;
            }
            e.Result = MonthlyGrpCashOpbalance;
            e.Handled = true;
        }

        private void xrCashMonthOP_SummaryReset(object sender, EventArgs e)
        {
            MonthlyGrpCashOpbalance = MonthlyGrpBankOpbalance = 0;
        }

        private void xrBankMonthOP_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyGrpBankOpbalance;
            e.Handled = true;
        }

        private void xrRCashMonthSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            MonthlyGrpCashClbalance = (MonthlyGrpCashOpbalance + MonthlyCashReceipts) - MonthlyCashPayments;
            MonthlyGrpBankClbalance = (MonthlyGrpBankOpbalance + MonthlyBankReceipts) - MonthlyBankPayments;
            e.Result = MonthlyCashReceipts + MonthlyGrpCashOpbalance;
            e.Handled = true;
        }

        private void xrRCashMonthSum_SummaryReset(object sender, EventArgs e)
        {
            MonthGroupNumber++;
            MonthlyCashReceipts = MonthlyCashPayments = MonthlyBankReceipts = MonthlyBankPayments = 0;
        }

        private void xrRCashMonthSum_SummaryRowChanged(object sender, EventArgs e)
        {
            MonthlyCashReceipts += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.CASHColumn.ColumnName).ToString());
            MonthlyCashPayments += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.PAY_CASHColumn.ColumnName).ToString());
            MonthlyBankReceipts += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.BANKColumn.ColumnName).ToString());
            MonthlyBankPayments += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.PAY_BANKColumn.ColumnName).ToString());
        }

        private void xrRBankMonthSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyBankReceipts + MonthlyGrpBankOpbalance;
            e.Handled = true;
        }

        private void xrPCashMonthSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyCashPayments + MonthlyGrpCashClbalance;
            e.Handled = true;
        }

        private void xrPBankMonthSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyBankPayments + MonthlyGrpBankClbalance;
            e.Handled = true;
        }

        private void xrCashMonthCL_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = DailyGrpCashClbalance; //MonthlyGrpCashClbalance;
            e.Handled = true;
        }

        private void xrBankMonthCL_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = DailyGrpBankClbalance; // MonthlyGrpBankClbalance;
            e.Handled = true;
        }

        private void grpHeaderMonth_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (MonthGroupNumber >= 1)
            {
                grpHeaderMonth.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            }
        }

        // To Include Opening Balance of FD Details 17.06.2019
        private void xrCashBankTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalReceipsBankDetails = 0;
            //09/01/2020, to add FD balance always in Opening and Closing
            if (ReportProperty.Current.ShowDetailedBalance == 1)
            {
                double FDaccumulatedDifference = 0;

                //On 16/03/2020, to have equal grand total (FD accumulated interest amount)---------------------------
                //difference amount will be added to receipts side
                double paymentside = this.UtilityMember.NumberSet.ToDouble(this.prCLBankBalance.Value.ToString()) + FDClosingBalance + TotalBankPayments;
                double receiptside = this.UtilityMember.NumberSet.ToDouble(this.prOPBankBalance.Value.ToString()) + FDOpeningBalance + TotalBankReceipts;
                FDaccumulatedDifference = (paymentside - receiptside);
                //------------------------------------------------------------------------------------------------
                //On 14/07/2021, to have bank balance with FD balance
                TotalReceipsBankDetails = this.UtilityMember.NumberSet.ToDouble(this.prOPBankBalance.Value.ToString()) + FDOpeningBalance + TotalBankReceipts + FDaccumulatedDifference;
                //TotalReceipsBankDetails = this.UtilityMember.NumberSet.ToDouble(this.prOPBankBalance.Value.ToString()) + TotalBankReceipts + FDaccumulatedDifference;
            }
            else
            {
                TotalReceipsBankDetails = TotalBankReceipts + this.UtilityMember.NumberSet.ToDouble(this.prOPBankBalance.Value.ToString());
            }

            //On 16/03/2020 (FD accumulated interest amount)
            //TotalReceipsBankDetails = this.UtilityMember.NumberSet.ToDouble(this.prOPBankBalance.Value.ToString()) + FDOpeningBalance + TotalBankReceipts;
            e.Result = TotalReceipsBankDetails;
            e.Handled = true;
        }

        // To Include Opening Balance of FD Details 17.06.2019
        private void xrTableCell12_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalPaymentBankDetails = 0;
            //09/01/2020, to add FD balance always in Opening and Closing
            if (ReportProperty.Current.ShowDetailedBalance == 1)
            {
                //On 14/07/2021, to have bank balance with FD balance
                TotalPaymentBankDetails = this.UtilityMember.NumberSet.ToDouble(this.prCLBankBalance.Value.ToString()) + FDClosingBalance + TotalBankPayments;
                //TotalPaymentBankDetails = this.UtilityMember.NumberSet.ToDouble(this.prCLBankBalance.Value.ToString()) + TotalBankPayments;
            }
            else
            {
                TotalPaymentBankDetails = TotalBankPayments + this.UtilityMember.NumberSet.ToDouble(this.prCLBankBalance.Value.ToString());
            }

            //On 16/03/2020 (FD accumulated interest amount)
            //TotalPaymentBankDetails = this.UtilityMember.NumberSet.ToDouble(this.prCLBankBalance.Value.ToString()) + FDClosingBalance + TotalBankPayments;
            e.Result = TotalPaymentBankDetails;
            e.Handled = true;
        }

        private void xrTableCell10_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalReceipsCashDetails = 0;
            TotalReceipsCashDetails = TotalCashReceipts + this.UtilityMember.NumberSet.ToDouble(this.prOPCashBalance.Value.ToString());

            //On 16/03/2020 (FD accumulated interest amount)
            e.Result = TotalReceipsCashDetails;
            e.Handled = true;
        }

        private void xrTableCell9_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double TotalPaymentCashDetails = 0;
            TotalPaymentCashDetails = TotalCashPayments + this.UtilityMember.NumberSet.ToDouble(this.prCLCashBalance.Value.ToString());

            e.Result = TotalPaymentCashDetails;
            e.Handled = true;
        }
        #endregion

        private void xrcellPayNarration_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //On 18/01/2024, To attach fd number
            if (this.ReportProperties.IncludeNarration == 1 && GetCurrentColumnValue("PAY_FD_ACCOUNT_ID") != null)
            {
                Int32 payfdid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("PAY_FD_ACCOUNT_ID").ToString());
                if (payfdid > 0)
                {
                    string payfdnubmer = GetFDAccountNumber(payfdid);
                    string narration = e.Value == null ? string.Empty : e.Value.ToString();
                    e.Value = narration + " FD : " + payfdnubmer;
                }
            }
        }

        private void xrcellRecNarration_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //On 18/01/2024, To attach fd number
            if (this.ReportProperties.IncludeNarration == 1 && GetCurrentColumnValue("REC_FD_ACCOUNT_ID") != null)
            {
                Int32 recfdid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("REC_FD_ACCOUNT_ID").ToString());
                if (recfdid > 0)
                {
                    string recfdnubmer = GetFDAccountNumber(recfdid);
                    string narration = e.Value == null ? string.Empty : e.Value.ToString();
                    e.Value = narration + " FD : " + recfdnubmer;
                }
            }
        }

        private void xrPaymentCash_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //e.Value = 1;
        }




    }
}
