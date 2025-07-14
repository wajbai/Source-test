using System;
using System.Drawing;
using System.Data;
using System.Linq;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraReports.UI;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class CashBankBookManual : ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;

        double FDOpeningBalance = 0;
        double FDClosingBalance = 0;

        int count = 0;
        int Rcount = 0;
        const int HeaderTable = 1;
        const int NonHeaderTable = 0;
        string RecledAmount = string.Empty;
        string PayledAmount = string.Empty;
        #endregion

        #region Constructor
        public CashBankBookManual()
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
            Rcount = 0;
            // To Include Opening Balance of FD Details 17.06.2019
         
        }

        private void CashAndBankBook()
        {
            SetReportTitle();

            //On 17/12/2018, Set Sign details
            this.FixReportPropertyForCMF();
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
                        BindProperty();
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
                }
            }
            SetReportSettings();
        }

        private void BindProperty()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            setHeaderTitleAlignment();
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;// 1045.25f;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;// 1045.25f;
            this.SetLandscapeFooterDateWidth = 890.00f;
            xrOpDate.Text = this.UtilityMember.DateSet.ToDate(ReportProperties.DateFrom);
            xrCLDate.Text = this.UtilityMember.DateSet.ToDate(ReportProperties.DateTo);

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            grpHeaderOPBalance.Visible = (this.ReportProperties.ShowDailyBalance == 0);
            grpHeaderCLBalance.Visible = ReportFooter.Visible = grpHeaderOPBalance.Visible;

            prOPCashBalance.Visible = prCLCashBalance.Visible = prOPBankBalance.Visible = prCLBankBalance.Visible = false;
            prOPCashBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.CashBalance,
                                 BalanceSystem.BalanceType.OpeningBalance);

            prCLCashBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, BalanceSystem.LiquidBalanceGroup.CashBalance,
                                 BalanceSystem.BalanceType.ClosingBalance);

            prOPBankBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                BalanceSystem.BalanceType.OpeningBalance);

            prCLBankBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                 BalanceSystem.BalanceType.ClosingBalance);

            FDOpeningBalance = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.FDBalance,
                               BalanceSystem.BalanceType.OpeningBalance);

            FDClosingBalance = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, BalanceSystem.LiquidBalanceGroup.FDBalance,
                                BalanceSystem.BalanceType.ClosingBalance);


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
            }
            else
            {
                //Hide empty rows if there is not records
                Detail.Visible = false;
            }

            if (this.ReportProperties.IncludeNarrationwithRefNo == 1 || this.ReportProperties.IncludeNarrationwithNameAddress == 1)
            {
                this.ReportProperties.IncludeNarration = 1;
            }

            // xrNarration.CanGrow = false;
            if (this.ReportProperties.IncludeNarration == 1)
            {
                string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
                string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
            }

            SplashScreenManager.CloseForm();
            base.ShowReport();
        }

        private ResultArgs GetReportSource()
        {
            DataTable dtReceipts =new DataTable();
            DataTable dtPayments = new DataTable();
            try
            {
                string CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.CashBankBookManual);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_REFNOColumn, this.ReportProperties.IncludeNarrationwithRefNo);
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_NAMEADDRESSColumn, this.ReportProperties.IncludeNarrationwithNameAddress);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankBookQueryPath);
                }

                //# Common Records
                if (resultArgs.Success && resultArgs.DataSource.Table!= null)
                {
                    DataTable dtCB = resultArgs.DataSource.Table;

                    //#. Extra Payments
                    CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.CashBankBookManual1);
                    using (DataManager dataManager = new DataManager())
                    {
                        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                        dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                        dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                        dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_REFNOColumn, this.ReportProperties.IncludeNarrationwithRefNo);
                        dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_NAMEADDRESSColumn, this.ReportProperties.IncludeNarrationwithNameAddress);

                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankBookQueryPath);
                    }

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtCBExtraPayments = resultArgs.DataSource.Table;
                        dtCB.Merge(dtCBExtraPayments);
                        resultArgs.DataSource.Data = dtCB.DefaultView;
                    }
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
            xrTableCell58.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell63.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell43.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell54.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell51.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell55.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
                        
            SetBorders();
        }

        private void SetBorders()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblOpeningBalance = AlignOpeningBalanceTable(xrtblOpeningBalance);
            xrtblClosingBalance = AlignClosingBalance(xrtblClosingBalance);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            this.SetCurrencyFormat(xrCapRecCash.Text, xrCapRecCash);
            this.SetCurrencyFormat(xrCapRecCash.Text, xrCapPayCash);
            this.SetCurrencyFormat(xrCapRecBank.Text, xrCapRecBank);
            this.SetCurrencyFormat(xrCapRecBank.Text, xrCapPayBank);
        }

        public override XRTable AlignClosingBalance(XRTable table)
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
                            if (ReportProperties.ShowDetailedBalance == 1)
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            else
                                tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (ReportProperties.ShowLedgerCode != 1)
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
                        else
                            if (ReportProperties.ShowDetailedBalance == 1)
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                tcell.Borders = BorderSide.Right;
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
                                if (cellcount == 3 || cellcount == 8)
                                    cell.Borders = BorderSide.None;
                                else
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                cell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 8)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 8)
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
                                        if (cellcount == 3 || cellcount == 8)
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
                                        if (cellcount == 3 || cellcount == 8)
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
                                    if (cellcount == 3 || cellcount == 8)
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
                                if (count == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                else if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            }
                            else
                            {
                                if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
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
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Right;
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

        /// <summary>
        /// This method is used to add filter condition
        /// </summary>
        private DataView AttachAmountFilter(DataView dv)
        {
            //On 05/06/2017, To add Amount filter condition
            string AmountFilter = this.GetAmountFilter();
            lblAmountFilter.Visible = false;
            if (AmountFilter != "")
            {
                dv.RowFilter = "(CASH > 0 AND CASH " + AmountFilter + ") OR (BANK > 0 AND BANK " + AmountFilter + ")" +
                              " OR (PAY_CASH > 0 AND PAY_CASH " + AmountFilter + ") OR (PAY_BANK > 0 AND PAY_BANK " + AmountFilter + ")";
                lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                lblAmountFilter.Visible = true;
            }
            return dv;
        }
        #endregion

        #region Events
        
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
        }

        private void xrtblBindSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // SetTableborder(0);
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
            xrtblBindSource = AlignCashBankBookTable(xrtblBindSource, Narration, Narration_Pay, count);
        }

     
        private void xrTableCell74_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string RecLedgerAmount = (GetCurrentColumnValue("REC_LEDGER_AMOUNT") == null) ? string.Empty : GetCurrentColumnValue("REC_LEDGER_AMOUNT").ToString();
            if (!string.IsNullOrEmpty(RecLedgerAmount))
            {
                string[] sd = RecLedgerAmount.Split(' ');
                if (sd.Length > 0)
                {
                    double recLedAmt = this.UtilityMember.NumberSet.ToDouble(sd[0]);
                }
            }
        }

       
        private void xrTableCell76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string PayLedgerAmount = (GetCurrentColumnValue("PAY_LEDGER_AMOUNT") == null) ? string.Empty : GetCurrentColumnValue("PAY_LEDGER_AMOUNT").ToString();
            if (!string.IsNullOrEmpty(PayLedgerAmount))
            {
                string[] sd = PayLedgerAmount.Split(' ');
                if (sd.Length > 0)
                {
                    double recLedAmt = this.UtilityMember.NumberSet.ToDouble(sd[0]);
                }
            }
        }

                
        #endregion

       

        private DataTable MergeTablesByIndex(DataTable dtReceipts, DataTable dtPayments)
        {
            DataTable dtResult = null;
            if (dtReceipts != null && dtPayments != null)
            {
                dtResult = dtReceipts.Clone();  // first add columns from table1
                foreach (DataColumn col in dtPayments.Columns)
                {
                    string newColumnName = col.ColumnName;
                    int colNum = 1;
                    while (dtResult.Columns.Contains(newColumnName))
                    {
                        newColumnName = string.Format("{0}_{1}", col.ColumnName, ++colNum);
                    }
                    dtResult.Columns.Add(newColumnName, col.DataType);
                }

                foreach (DataRow drpayment in dtPayments.Rows)
                {
                    
                }
            }

            return dtResult;
        }

        private void xrSumRecCash_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double TotalRecCash = UtilityMember.NumberSet.ToDouble(xrSumRecCash.Text);
            double SumRecCash = TotalRecCash + UtilityMember.NumberSet.ToDouble(prOPCashBalance.Value.ToString());

            xrSumRecCash.Text = UtilityMember.NumberSet.ToNumber(SumRecCash);
        }

        private void xrSumRecBank_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double TotalRecBank = UtilityMember.NumberSet.ToDouble(xrSumRecBank.Text);
            double SumRecBank = TotalRecBank + UtilityMember.NumberSet.ToDouble(prOPBankBalance.Value.ToString());

            xrSumRecBank.Text = UtilityMember.NumberSet.ToNumber(SumRecBank);
        }

        private void xrSumPayCash_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double TotalPayCash = UtilityMember.NumberSet.ToDouble(xrSumPayCash.Text);
            double SumPayCash = TotalPayCash+ UtilityMember.NumberSet.ToDouble(prCLCashBalance.Value.ToString());

            xrSumPayCash.Text = UtilityMember.NumberSet.ToNumber(SumPayCash);
        }

        private void xrSumPayBank_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            double TotalPayBank = UtilityMember.NumberSet.ToDouble(xrSumPayBank.Text);
            double SumPayBank = TotalPayBank + UtilityMember.NumberSet.ToDouble(prCLBankBalance.Value.ToString());

            xrSumPayBank.Text = UtilityMember.NumberSet.ToNumber(SumPayBank);
        }

       
    }
}
