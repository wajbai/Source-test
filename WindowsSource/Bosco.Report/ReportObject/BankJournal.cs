using System;
using System.Drawing;
using System.Drawing;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;
using System.Collections;
using System.Collections.Generic;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraReports.UI;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class BankJournal : ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        int DailyGroupNumber = 0;
        double DailyGrpOpbalance = 0;
        double DailyGrpClbalance = 0;
        double DailyReceipts = 0;
        double DailyPayments = 0;

        double MonthlyReceipts = 0;
        double MonthlyPayments = 0;
        int MonthGroupNumber = 0;
        double MonthlyGrpOpbalance = 0;
        double MonthlyGrpClbalance = 0;

        string LedgerDate = string.Empty;
        string datefrom = string.Empty;
        string dateto = string.Empty;

        int count = 0;
        int OPcount = 0;

        string ReceiptCaption = string.Empty;
        string PaymentCaption = string.Empty;

        #endregion

        #region Constructor
        public BankJournal()
        {
            InitializeComponent();

            ReceiptCaption = xrCapReceipts.Text;
            PaymentCaption = xrCapPayments.Text;

            //this.AttachDrillDownToRecord(xrtblSource, xrLedger,
            //    new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");

            this.AttachDrillDownToRecord(xrtblSource, xrLedger,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName, "VOUCHER_TYPE" }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");

        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                if (dicDDProperties.ContainsKey(this.reportSetting1.CashBankFlow.DATEColumn.ColumnName))
                {
                    LedgerDate = dicDDProperties[this.reportSetting1.CashBankFlow.DATEColumn.ColumnName].ToString();
                    this.ReportProperties.BankAccount = "0";
                    datefrom = dateto = LedgerDate;
                }
            }
            else
            {
                datefrom = this.ReportProperties.DateFrom;
                dateto = this.ReportProperties.DateTo;
            }

            DailyGroupNumber = 0;
            DailyGrpOpbalance = 0;
            DailyGrpClbalance = 0;
            DailyReceipts = 0;
            DailyPayments = 0;

            MonthGroupNumber = 0;
            MonthlyGrpOpbalance = 0;
            MonthlyGrpClbalance = 0;
            MonthlyReceipts = 0;
            MonthlyPayments = 0;
            grpHeaderMonth.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;

            count = 0;
            OPcount = 0;
            BankJournalReport();

        }

        private void BankJournalReport()
        {
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;


            if (string.IsNullOrEmpty(datefrom) || string.IsNullOrEmpty(dateto) || this.ReportProperties.Project == "0" ||
                string.IsNullOrEmpty(this.ReportProperties.CashBankLedger))
            {
                if (string.IsNullOrEmpty(this.ReportProperties.BankAccount))
                {
                    if (LedgerDate == string.Empty)
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
            }
            else
            {
                //On 09/04/2019, if show only receipts /payments, lock daily and monthly balances
                if ((this.ReportProperties.ShowOnlyReceipts == 1 && this.ReportProperties.ShowOnlyPayments == 0) ||
                    (this.ReportProperties.ShowOnlyPayments == 1 && this.ReportProperties.ShowOnlyReceipts == 0))
                {
                    if (ReportProperty.Current.ShowDailyBalance == 1 || ReportProperty.Current.IncludeLedgerGroupTotal == 1)
                    {
                        MessageRender.ShowMessage("Show Daily Balance/Month-wise Total/Ledger Summary option will be disabled");
                    }

                    ReportProperty.Current.ShowDailyBalance = 0;
                    ReportProperty.Current.ShowMonthTotal = 0;
                    ReportProperty.Current.ShowByLedgerSummary = 0;
                    ReportProperty.Current.IncludeLedgerGroupTotal = 0;
                    ReportProperty.Current.SaveReportSetting();
                }

                //On 04/03/2019, if monthly leger summar disalbe all other options like include narration, show daily balance
                grpMonthLedgerSummary.GroupFields.Clear();
                if (ReportProperty.Current.ShowByLedgerSummary == 1)
                {
                    this.ReportProperties.ShowDailyBalance = 0;
                    grpMonthLedgerSummary.GroupFields.Add(new GroupField("LEDGER", XRColumnSortOrder.Ascending));
                }

                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindProperty();
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                        SetReportBorder();
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
                    SetReportBorder();
                }
            }
        }

        private void BindProperty()
        {
            // this.ReportTitle = ReportProperty.Current.ReportTitle;
            // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            // this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", datefrom, dateto);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;

            this.HideCostCenter = ReportProperties.Count == 1 ? true : false;
            this.CosCenterName = ReportProperties.Count == 1 ? ReportProperties.BankAccountName : " ";

            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            setHeaderTitleAlignment();
            SetReportTitle();

            //show daiy balance
            grpheaderVoucherDate.Visible = (this.ReportProperties.ShowDailyBalance == 1);
            grpfooterVoucherDate.Visible = grpheaderVoucherDate.Visible;

            //show by month
            grpHeaderMonth.Visible = (ReportProperty.Current.IncludeLedgerGroupTotal == 0 ? false : true);
            grpFooterMonth.Visible = (ReportProperty.Current.IncludeLedgerGroupTotal == 0 ? false : true);

            xrRowGrandTotal.Visible = (this.ReportProperties.ShowDailyBalance == 0 && this.ReportProperties.IncludeLedgerGroupTotal == 0);
            xrtblCLBalanceRow.Visible = grpheaderVoucherBalance.Visible = xrRowGrandTotal.Visible;

            SetReportProperty();
            prOPBalance.Visible = prCLBalance.Visible = false;
            //To calculate Opening balance of Bank and FD
            prOPBalance.Value = this.GetBalance(this.ReportProperties.Project, datefrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                 BalanceSystem.BalanceType.OpeningBalance, (IsDrillDownMode ? "" : this.ReportProperties.CashBankLedger), true);
            double FDOPBalance = this.GetBalance(this.ReportProperties.Project, datefrom, BalanceSystem.LiquidBalanceGroup.FDBalance,
             BalanceSystem.BalanceType.OpeningBalance, (IsDrillDownMode ? "" : this.ReportProperties.CashBankLedger));
            prOPBalance.Value = this.UtilityMember.NumberSet.ToDouble(prOPBalance.Value.ToString());// +FDOPBalance;

            //To calculate closing balance of Bank and FD
            prCLBalance.Value = this.GetBalance(this.ReportProperties.Project, dateto, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                 BalanceSystem.BalanceType.ClosingBalance, (IsDrillDownMode ? "" : this.ReportProperties.CashBankLedger), true);
            double FDClBalance = this.GetBalance(this.ReportProperties.Project, dateto, BalanceSystem.LiquidBalanceGroup.FDBalance,
                                BalanceSystem.BalanceType.ClosingBalance, (IsDrillDownMode ? "" : this.ReportProperties.CashBankLedger));
            prCLBalance.Value = this.UtilityMember.NumberSet.ToDouble(prCLBalance.Value.ToString());// +FDClBalance;

            DataTable dtCashBankBook = GetReportSource();
            if (dtCashBankBook != null)
            {
                this.DataSource = dtCashBankBook;
                this.DataMember = dtCashBankBook.TableName;
            }

            if (this.ReportProperties.IncludeNarrationwithRefNo == 1 || this.ReportProperties.IncludeNarrationwithNameAddress == 1 || this.ReportProperties.IncludeNarrationwithCurrencyDetails == 1)
            {
                this.ReportProperties.IncludeNarration = 1;
            }

            //Hide blank sections if there is no records
            HideSections(dtCashBankBook.Rows.Count > 0);

            //On 03/09/2020, to hide general opening balance row if daily balance is enabled 
            grpheaderVoucherBalance.Visible = true;
            if (this.ReportProperties.ShowDailyBalance == 1)
            {
                grpheaderVoucherBalance.Visible = false;
            }
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblOpeningBalance = AlignOpeningBalanceTable(xrtblOpeningBalance);
            xrtblDailyOpeningBalance = AlignDailyOpeningBalaceTable(xrtblDailyOpeningBalance);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrtblMonth = AlignTotalTable(xrtblMonth);
            xrTblMonthFooter = AlignTotalTable(xrTblMonthFooter);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            //On 27/08/2024, To set curency symbol based on cash/bank selection
            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                string cashbankcurrencysymbol = ReportProperties.GetCashBankLedgerCurrencySymbol(ReportProperties.CashBankLedger);
                if (!string.IsNullOrEmpty(cashbankcurrencysymbol))
                {
                    xrCapReceipts.Text = ReceiptCaption + " (" + cashbankcurrencysymbol + ")";
                    xrCapPayments.Text = PaymentCaption + " (" + cashbankcurrencysymbol + ")";
                }
            }
            else
            {
                this.SetCurrencyFormat(xrCapReceipts.Text, xrCapReceipts);
                this.SetCurrencyFormat(xrCapPayments.Text, xrCapPayments);
            }
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
                                        tcell.Borders = BorderSide.Right;
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
                                    tcell.Borders = BorderSide.Right;
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
                            if (count == 1)
                                tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            else if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        }
                        else
                            if (count == 1)
                                tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                            else if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        else
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                if (count == 1)
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                                else if (count == trow.Cells.Count)
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    tcell.Borders = BorderSide.Bottom;
                        else
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
                                if (count == 1)
                                    tcell.Borders = BorderSide.All;
                                else
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right | BorderSide.Top;
                        }
                        else
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom | BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;

                            else
                                tcell.Borders = BorderSide.Top | BorderSide.Bottom | BorderSide.Right;
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Bottom | BorderSide.Right;
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

        private XRTable AlignDailyOpeningBalaceTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (OPcount == 1)
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
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                            else if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        }
                        else
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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

        public override XRTable AlignCashBankBookTable(XRTable table, string bankNarration, string cashNarration, int count)
        {
            int rowcount = 0;

            foreach (XRTableRow row in table.Rows)
            {
                int cellcount = 0;
                ++rowcount;
                if (rowcount == 2 && ReportProperties.IncludeNarration != 1)
                {
                    row.Visible = false;
                }
                else if (bankNarration == string.Empty && cashNarration == string.Empty && ReportProperties.IncludeNarration == 1 && rowcount == 2)
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
                foreach (XRTableCell cell in row)
                {
                    ++cellcount;
                    if (ReportProperties.IncludeNarration != 1 && rowcount == 1)
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = BorderSide.Right | BorderSide.Left | BorderSide.Bottom;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = BorderSide.None;
                                else
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                cell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | BorderSide.Left;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            else
                                if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                        }
                        else
                        {
                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        }

                    }
                    else
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                    else if (ReportProperties.ShowLedgerCode != 1)
                                    {
                                        if (cellcount == 3 || cellcount == 9)
                                            cell.Borders = BorderSide.None;
                                        else
                                        {
                                            if (bankNarration != string.Empty || cashNarration != string.Empty)
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                            else
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                        }
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                }
                                else
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                    else if (ReportProperties.ShowLedgerCode != 1)
                                    {
                                        if (cellcount == 3 || cellcount == 9)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                        else
                                        {
                                            if (bankNarration != string.Empty || cashNarration != string.Empty)
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                            else
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                        }
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                }

                            }
                            else
                            {
                                if (cellcount == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                else if (ReportProperties.ShowLedgerCode != 1)
                                {
                                    if (cellcount == 3 || cellcount == 9)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                }
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    if (cellcount == 1)
                                        cell.Borders = BorderSide.Left;
                                    else if (cellcount == row.Cells.Count)
                                        cell.Borders = BorderSide.Right;
                                    else
                                        cell.Borders = BorderSide.None;
                                else if (cellcount == 1)
                                    cell.Borders = BorderSide.Left | BorderSide.Bottom;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                                else
                                    cell.Borders = BorderSide.Bottom;
                            }
                            else
                            {
                                if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    if (cellcount == 1)
                                        cell.Borders = BorderSide.Bottom | BorderSide.Left;
                                    else if (cellcount == row.Cells.Count)
                                        cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                    else
                                        cell.Borders = BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (cellcount == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (cellcount == 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }

                        }
                        else
                        {
                            cell.Borders = BorderSide.None;
                        }
                    }
                    cell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
                cellcount = 0;
            }
            return table;
        }

        private DataTable GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankJournal);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, datefrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, dateto);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                    dataManager.Parameters.Add(this.ReportParameters.COUNTColumn, this.ReportProperties.Count);

                    //05/12/2019, to keep Cash Bank LedgerId
                    //if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                    //{
                    //    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    //}
                    //else
                    //{
                    //    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    //}
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, (IsDrillDownMode ? "0" : this.ReportProperties.CashBankLedger));

                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_REFNOColumn, this.ReportProperties.IncludeNarrationwithRefNo);
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_NAMEADDRESSColumn, this.ReportProperties.IncludeNarrationwithNameAddress);
                    dataManager.Parameters.Add(this.ReportParameters.CONSOLIDATEDColumn, this.ReportProperties.Consolidated);

                    //On 14/03/2024, To show/hide contra note (Cash withdrwal/Cash Deposit)
                    dataManager.Parameters.Add(this.ReportParameters.HIDE_CONTRA_NOTEColumn, this.ReportProperties.HideContraNote);

                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_CURRENCYColumn, this.ReportProperties.IncludeNarrationwithCurrencyDetails);
                    dataManager.Parameters.Add(this.ReportParameters.SHOW_INDIVIDUAL_LEDGERColumn, this.ReportProperties.ShowIndividualLedger);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankBookQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }

            //On 05/06/2017, To add Amount filter condition
            DataTable dtBankJournal = new DataTable();
            if (resultArgs.Success)
            {
                dtBankJournal = resultArgs.DataSource.Table;

                try
                {
                    //On 09/04/2019, to show Receipts/Payments alone
                    if (ReportProperties.ShowOnlyReceipts == 1 && ReportProperties.ShowOnlyPayments == 0)
                        dtBankJournal.DefaultView.RowFilter = "RECEIPT <> 0";
                    else if (ReportProperties.ShowOnlyPayments == 1 && ReportProperties.ShowOnlyReceipts == 0)
                        dtBankJournal.DefaultView.RowFilter = "PAYMENT <> 0";

                    //On 12/04/2019, to exclude Cash Withdrwal and Deposit
                    string cashwithdrwaldepositfitler = string.Empty;
                    if (ReportProperties.ExcludeCashWithdrawal == 1)
                    {
                        string excludeWithdrwal = " (VOUCHER_TYPE <> '" + VoucherSubTypes.CN.ToString() + "' OR PAYMENT <> 0)";
                        dtBankJournal.DefaultView.RowFilter += string.IsNullOrEmpty(dtBankJournal.DefaultView.RowFilter) ? excludeWithdrwal : " AND " + excludeWithdrwal;
                        cashwithdrwaldepositfitler = "Cash Withdrawal";
                    }

                    if (ReportProperties.ExcludeCashDeposit == 1)
                    {
                        string excludedeposit = " (VOUCHER_TYPE <> '" + VoucherSubTypes.CN.ToString() + "' OR RECEIPT <> 0)";
                        dtBankJournal.DefaultView.RowFilter += string.IsNullOrEmpty(dtBankJournal.DefaultView.RowFilter) ? excludedeposit : " AND " + excludedeposit;
                        cashwithdrwaldepositfitler += (string.IsNullOrEmpty(cashwithdrwaldepositfitler) ? "" : " and ") + "Cash Deposit";
                    }
                    dtBankJournal = dtBankJournal.DefaultView.ToTable();

                    string AmountFilter = this.GetAmountFilter();
                    lblAmountFilter.Visible = false;
                    lblAmountFilter.Text = string.Empty;
                    if (AmountFilter != "")
                    {
                        dtBankJournal.DefaultView.RowFilter = "(RECEIPT <> 0 AND RECEIPT " + AmountFilter + ") OR (PAYMENT <> 0 AND PAYMENT " + AmountFilter + ")";
                        lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                        lblAmountFilter.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(cashwithdrwaldepositfitler))
                    {
                        lblAmountFilter.Text += " Excluded by " + cashwithdrwaldepositfitler;
                        lblAmountFilter.Visible = true;
                    }
                }
                catch (Exception ee)
                {
                    dtBankJournal.DefaultView.RowFilter = String.Empty;
                    MessageRender.ShowMessage(ee.Message, true);
                }
            }
            else
            {
                MessageRender.ShowMessage("Could not generate Bank Journal Report", true);
            }
            return dtBankJournal;
        }

        /// <summary>
        /// Hide blank sections if there is no records
        /// </summary>
        /// <param name="RecordExists"></param>
        private void HideSections(bool RecordExists)
        {
            if (RecordExists)
            {
                Detail.Visible = grpMonthLedgerSummary.Visible = false;
                if (ReportProperty.Current.ShowByLedgerSummary == 0)
                {
                    Detail.Visible = true;
                }
                else
                {
                    grpMonthLedgerSummary.Visible = true;
                }

                grpHeaderMonth.Visible = (ReportProperty.Current.IncludeLedgerGroupTotal == 0 ? false : true);
                grpFooterMonth.Visible = (ReportProperty.Current.IncludeLedgerGroupTotal == 0 ? false : true);
            }
            else
            {
                Detail.Visible = false;
                grpHeaderMonth.Visible = Detail.Visible;
                grpFooterMonth.Visible = Detail.Visible;
                grpMonthLedgerSummary.Visible = Detail.Visible;
            }
            MakeOnlyReceiptsPayments();
        }


        /// <summary>
        /// This method is used to hide/unhide Receipts or payments sections based on criteria selection
        /// </summary>
        private void MakeOnlyReceiptsPayments()
        {
            float ledgernamewidth = 307;
            float amountwidth = 120;


            xrtblHeaderCaption.BeginInit();
            xrCapReceipts.Visible = xrCapPayments.Visible = true;
            xrCapLedgerName.WidthF = ledgernamewidth;
            xrCapChequeNo.WidthF = 55;
            xrCapReceipts.WidthF = amountwidth;
            xrCapPayments.WidthF = amountwidth;
            xrCapChequeNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrtblHeaderCaption.ResumeLayout();

            xrtblOpeningBalance.BeginInit();
            xrOpeningReceipts.Visible = xrOpeningPayments.Visible = true;
            xrOpeningReceipts.WidthF = xrOpeningPayments.WidthF = amountwidth;
            xrOpening.WidthF = 362;
            xrtblOpeningBalance.ResumeLayout();

            xrtblSource.BeginInit();
            xrReceipt.Visible = xrPayments.Visible = true;
            xrLedger.WidthF = xrNarration.WidthF = ledgernamewidth;
            xrChequeNo.WidthF = 55;
            xrReceipt.WidthF = xrPayments.WidthF = xrNarration2.WidthF = xrNarration3.WidthF = amountwidth;
            xrChequeNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            xrtblSource.ResumeLayout();

            xrtblGrandTotal.BeginInit();
            grpheaderVoucherBalance.Visible = xrGrandTotalReceipts.Visible = xrGrandTotalPayments.Visible =
                 xrClosingReceipts.Visible = xrClosingPayments.Visible = xrtblCLBalanceRow.Visible = true;
            xrGrandTotalReceipts.WidthF = xrGrandTotalPayments.WidthF = amountwidth;
            xrClosing.WidthF = xrGrandTotal.WidthF = 362;
            xrClosingReceipts.WidthF = amountwidth;
            xrClosingPayments.WidthF = amountwidth;
            xrtblGrandTotal.ResumeLayout();


            if ((this.ReportProperties.ShowOnlyReceipts == 1 && this.ReportProperties.ShowOnlyPayments == 0) ||
               (this.ReportProperties.ShowOnlyPayments == 1 && this.ReportProperties.ShowOnlyReceipts == 0))
            {
                //Header    
                xrtblHeaderCaption.SuspendLayout();
                if (this.ReportProperties.ShowOnlyReceipts == 1)
                {
                    xrCapPayments.Visible = false;
                    xrCapReceipts.WidthF += xrCapPayments.WidthF;
                    xrCapChequeNo.WidthF += xrCapPayments.WidthF;
                    xrCapLedgerName.WidthF += xrCapPayments.WidthF;
                    xrCapChequeNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
                }
                else if (this.ReportProperties.ShowOnlyPayments == 1)
                {
                    xrCapReceipts.Visible = false;
                    xrCapChequeNo.WidthF += xrCapReceipts.WidthF;
                    xrCapLedgerName.WidthF += xrCapReceipts.WidthF;
                    xrCapChequeNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
                }
                xrtblHeaderCaption.PerformLayout();

                //Opening
                xrtblOpeningBalance.BeginInit();
                if (this.ReportProperties.ShowOnlyReceipts == 1)
                {
                    xrOpeningPayments.Visible = false;
                    xrOpeningReceipts.WidthF += xrOpeningPayments.WidthF;
                    xrOpeningReceipts.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                    xrOpening.WidthF += 55;
                }
                else if (this.ReportProperties.ShowOnlyPayments == 1)
                {
                    xrOpeningReceipts.Visible = false;
                    xrOpening.WidthF += xrOpeningReceipts.WidthF;
                }
                xrtblOpeningBalance.ResumeLayout();


                //Data
                xrtblSource.BeginInit();
                if (this.ReportProperties.ShowOnlyReceipts == 1)
                {
                    xrPayments.Visible = false;
                    xrReceipt.WidthF += xrPayments.WidthF;
                    xrChequeNo.WidthF += xrPayments.WidthF;
                    xrLedger.WidthF += xrPayments.WidthF;
                    xrNarration.WidthF = xrLedger.WidthF;
                    xrNarration1.WidthF = xrChequeNo.WidthF;
                    xrNarration2.WidthF = xrNarration3.WidthF = xrReceipt.WidthF;
                    xrChequeNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                }
                else if (this.ReportProperties.ShowOnlyPayments == 1)
                {
                    xrReceipt.Visible = false;
                    xrChequeNo.WidthF += xrReceipt.WidthF;
                    xrLedger.WidthF += xrReceipt.WidthF;
                    xrNarration.WidthF = xrLedger.WidthF;
                    xrNarration1.WidthF = xrChequeNo.WidthF;
                    xrNarration2.WidthF = xrReceipt.WidthF;
                    xrChequeNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                }
                xrtblSource.ResumeLayout();

                //Grand Total
                xrtblGrandTotal.BeginInit();
                if (this.ReportProperties.ShowOnlyReceipts == 1)
                {
                    xrtblCLBalanceRow.Visible = false;
                    xrGrandTotalPayments.Visible = false;
                    xrGrandTotalReceipts.WidthF += xrGrandTotalPayments.WidthF;
                    xrGrandTotal.WidthF += xrGrandTotalPayments.WidthF;
                }
                else if (this.ReportProperties.ShowOnlyPayments == 1)
                {
                    grpheaderVoucherBalance.Visible = xrGrandTotalReceipts.Visible = false;
                    xrClosingReceipts.Visible = false;
                    xrClosing.WidthF += xrClosingReceipts.WidthF;
                    xrGrandTotal.WidthF += xrGrandTotalReceipts.WidthF;
                }
                xrtblGrandTotal.ResumeLayout();

            }

            if (!this.ReportProperties.ReportTitle.Contains("-"))
            {
                if (this.ReportProperties.ShowOnlyReceipts == 1 && this.ReportProperties.ShowOnlyPayments == 0)
                    this.ReportTitle = this.ReportProperties.ReportTitle + " - Receipts";
                else if (this.ReportProperties.ShowOnlyPayments == 1 && this.ReportProperties.ShowOnlyReceipts == 0)
                    this.ReportTitle = this.ReportProperties.ReportTitle + " - Payments";
            }
        }
        #endregion

        #region Events

        private void xrtblDailyOpBalance_SummaryReset(object sender, EventArgs e)
        {
            DailyGrpOpbalance = 0;
        }

        private void xrtblDailyOpBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            if (DailyGroupNumber == 0)
            {
                DailyGrpOpbalance = this.UtilityMember.NumberSet.ToDouble(this.prOPBalance.Value.ToString());
                DailyGroupNumber++;
            }
            else
            {
                DailyGrpOpbalance = DailyGrpClbalance;
                DailyGrpClbalance = 0;
            }
            e.Result = DailyGrpOpbalance;
            e.Handled = true;
        }

        private void xtrtblDailyRecTotal_SummaryReset(object sender, EventArgs e)
        {
            DailyReceipts = DailyPayments = 0;
        }

        private void xtrtblDailyRecTotal_SummaryRowChanged(object sender, EventArgs e)
        {
            DailyReceipts += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.RECEIPTColumn.ColumnName).ToString());
            DailyPayments += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.PAYMENTColumn.ColumnName).ToString());
        }

        private void xtrtblDailyRecTotal_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyReceipts + DailyGrpOpbalance;
            e.Handled = true;
        }


        private void xrDailyClosingBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            if (ReportProperties.ShowOnlyPayments != 1 || this.ReportProperties.ShowDailyBalance == 1)
            {
                DailyGrpClbalance = (DailyGrpOpbalance + DailyReceipts) - DailyPayments;

                //if (this.ReportProperties.ShowDailyBalance == 1) { prCLBalance.Value = DailyGrpClbalance; }
                prCLBalance.Value = DailyGrpClbalance;
                e.Result = DailyGrpClbalance;
                e.Handled = true;
            }
        }

        private void xtrtblDailyPayTotal_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = DailyPayments + DailyGrpClbalance;
            e.Handled = true;
        }

        private void xttblOpBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToDouble(this.prOPBalance.Value.ToString());
            e.Handled = true;
        }

        private void SetReportProperty()
        {
            //float actualCCWidth = xrtblCostCenterCap.WidthF;
            //float actualLedgerWidth = xrCapChequeNo.WidthF;
            float actualCodeWidth = xrCapLedgerCode.WidthF;
            bool isCapCodeVisible = true;

            if (xrCapLedgerCode.Tag != null && xrCapLedgerCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapLedgerCode.Tag.ToString());
            }
            else
            {
                xrCapLedgerCode.Tag = xrCapLedgerCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell24.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell29.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell50.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell33.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell35.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell39.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
            xrTableCell41.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : (float)0.0);
        }

        private void xrtblSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrtblSource = AlignCashBankBookTable(xrtblSource, Narration, string.Empty, count);
        }

        private void xrtblDailyOpeningBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            OPcount++;
            xrtblDailyOpeningBalance = AlignDailyOpeningBalaceTable(xrtblDailyOpeningBalance);
        }

        private void xrReceipt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double receiptAmt = this.ReportProperties.NumberSet.ToDouble(xrReceipt.Text);
            if (receiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrReceipt.Text = "";
            }
        }

        private void xrPayments_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentAmt = this.ReportProperties.NumberSet.ToDouble(xrPayments.Text);
            if (paymentAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrPayments.Text = "";
            }
        }

        private void xrMonthOP_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (MonthGroupNumber == 0)
            {
                MonthlyGrpOpbalance = this.UtilityMember.NumberSet.ToDouble(this.prOPBalance.Value.ToString());
                MonthGroupNumber++;
            }
            else
            {
                MonthlyGrpOpbalance = MonthlyGrpClbalance;
                MonthlyGrpClbalance = 0;
            }
            e.Result = MonthlyGrpOpbalance; ;
            e.Handled = true;
        }

        private void xrMonthOP_SummaryReset(object sender, EventArgs e)
        {
            MonthlyGrpOpbalance = 0;
        }

        private void xrRMonthSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyReceipts + MonthlyGrpOpbalance;
            e.Handled = true;
        }

        private void xrRMonthSum_SummaryReset(object sender, EventArgs e)
        {
            MonthlyReceipts = MonthlyPayments = 0;
        }

        private void xrRMonthSum_SummaryRowChanged(object sender, EventArgs e)
        {
            MonthlyReceipts += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.RECEIPTColumn.ColumnName).ToString());
            MonthlyPayments += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.PAYMENTColumn.ColumnName).ToString());
        }

        private void xrPMonthSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyPayments + MonthlyGrpClbalance;
            e.Handled = true;
        }

        private void xrMonthCL_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //MonthlyGrpClbalance = (MonthlyGrpOpbalance + MonthlyReceipts) - MonthlyPayments;
            //prCLBalance.Value = MonthlyGrpClbalance;
            MonthlyGrpClbalance = this.UtilityMember.NumberSet.ToDouble(prCLBalance.Value.ToString());
            e.Result = prCLBalance.Value;
            e.Handled = true;
        }

        private void grpHeaderMonth_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (MonthGroupNumber >= 1 && ReportProperty.Current.ShowByLedgerSummary == 0)
            {
                grpHeaderMonth.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            }
        }
        #endregion

    }
}
