using System;
using System.Drawing;
using System.Drawing;
using System.Data;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class CostCentreCashBankBook : ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        int count = 0;
        int ccCount = 0;
        DataTable dtCCOpBalance = new DataTable();
        #endregion

        #region Constructor
        public CostCentreCashBankBook()
        {
            InitializeComponent();
            //this.AttachDrillDownToRecord(xrtblBindSource, xrCosBank,
            //    new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName , "REC_VOUCHER_TYPE" }, DrillDownType.LEDGER_VOUCHER, false);
            //this.AttachDrillDownToRecord(xrtblBindSource, xrCosPayments,
            //    new ArrayList { this.ReportParameters.PAY_VOUCHER_IDColumn.ColumnName, "PAY_VOUCHER_TYPE" }, DrillDownType.LEDGER_VOUCHER, false);

            this.AttachDrillDownToRecord(xrtblBindSource, xrCosBank,
               new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName, "REC_VOUCHER_TYPE" }, DrillDownType.LEDGER_VOUCHER, false);
            this.AttachDrillDownToRecord(xrtblBindSource, xrCosPayments,
                new ArrayList { this.ReportParameters.PAY_VOUCHER_IDColumn.ColumnName, "PAY_VOUCHER_TYPE" }, DrillDownType.LEDGER_VOUCHER, false);
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            count = 0;
            ccCount = 0;
            CashAndBankBook();
            base.ShowReport();
        }

        private void CashAndBankBook()
        {
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
            this.SetLandscapeCostCentreWidth = xrtblHeaderCaption.WidthF;

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
            {
                SetReportTitle();
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
                }
            }
            SetReportSetup();

            // To show by Date starts
            if (this.ReportProperties.ShowByCostCentre == 0)
                grpHeaderCCName.GroupFields[0].FieldName = string.Empty;
            else
                grpHeaderCCName.GroupFields[0].FieldName = "COST_CENTRE_NAME";

            if (this.ReportProperties.ShowByCostCentreCategory == 0)
            {
                grpHeaderCostCategory.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpHeaderCostCategory.GroupFields[0].FieldName = "COST_CENTRE_CATEGORY_NAME";
            }
        }

        private void BindProperty()
        {
            setHeaderTitleAlignment();
            SetReportTitle();

            grpHeaderCostCategory.Visible = (this.ReportProperties.ShowByCostCentreCategory == 1);
            grpHeaderCCName.Visible = (ReportProperties.ShowByCostCentre == 1);
            grpFooterCCName.Visible = (this.ReportProperties.ShowByCostCentre == 1);  // (ReportProperties.BreakByCostCentre == 1) ? true : false;

            if (ReportProperties.IncludeNarrationwithCurrencyDetails == 1)
            {
                ReportProperties.IncludeNarration = 1;
            }

            if (grpHeaderCCName.Visible != true)
            {
                this.CosCenterName = ReportProperty.Current.CostCentreName;
            }
            else
            {
                this.CosCenterName = string.Empty;
            }
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            prCLBankBalance.Visible = prCLCashBalance.Visible = prOPCashBalance.Visible = prOPBankBalance.Visible = false;
            resultArgs = GetReportSource();
            DataView dvCashBankBook = resultArgs.DataSource.TableView;
            if (dvCashBankBook != null)
            {
                dvCashBankBook.Table.TableName = "CashBankBook";
                this.DataSource = dvCashBankBook;
                this.DataMember = dvCashBankBook.Table.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCenterCashBankBook);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_CURRENCYColumn, this.ReportProperties.IncludeNarrationwithCurrencyDetails);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankBookQueryPath);
                }
                if (resultArgs.Success)
                {
                    //On 12/07/2024, to get CC Opening Banacne based on cash and Bank
                    ResultArgs result = this.AssignCCDetailReportSource();
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// On 12/07/2024, to get cost centre closing balance 
        /// </summary>
        /// <param name="CashOP"></param>
        /// <returns></returns>
        private double getCCClosingBalance(bool IsCash, bool forGrandTotal)
        {
            double rtn = 0;
            Int32 ccid = 0;
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                DataView dvRprt = this.DataSource as DataView;
                if (dvRprt != null)
                {
                    DataTable dtRpt = dvRprt.Table;
                    ccid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName).ToString());
                    string filter = reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName + "=" + ccid;
                    if (forGrandTotal) filter = string.Empty;

                    double openingbalance = this.getCCOpeningBalance(IsCash, forGrandTotal);
                    openingbalance = openingbalance * -1;// We show opening in Receipt side and this is costentre (expense unit)
                    double cashreceipt = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankBook.CASHColumn.ColumnName + ")", filter).ToString());
                    double cashpayment = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankBook.PAY_CASHColumn.ColumnName + ")", filter).ToString());

                    double bankreceipt = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankBook.BANKColumn.ColumnName + ")", filter).ToString());
                    double bankpayment = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankBook.PAY_BANKColumn.ColumnName + ")", filter).ToString());
                    if (IsCash)
                    {
                        rtn = (openingbalance + cashreceipt) - cashpayment;
                    }
                    else
                    {
                        rtn = (openingbalance + bankreceipt) - bankpayment;
                    }
                }
            }

            return rtn;
        }

        /// <summary>
        /// On 12/07/2024, to get cost centre closing balance 
        /// </summary>
        /// <param name="CashOP"></param>
        /// <returns></returns>
        private double getCCTotalAmount(bool IsCash, bool isReceipt, bool isGrandTotal)
        {
            double rtn = 0;
            Int32 ccid = 0;
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                DataView dvRprt = this.DataSource as DataView;
                if (dvRprt != null)
                {
                    DataTable dtRpt = dvRprt.Table;
                    ccid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName).ToString());
                    string filter = reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName + "=" + ccid;
                    if (isGrandTotal) filter = string.Empty;

                    double cashreceipt = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankBook.CASHColumn.ColumnName + ")", filter).ToString());
                    double cashpayment = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankBook.PAY_CASHColumn.ColumnName + ")", filter).ToString());

                    double bankreceipt = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankBook.BANKColumn.ColumnName + ")", filter).ToString());
                    double bankpayment = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankBook.PAY_BANKColumn.ColumnName + ")", filter).ToString());

                    if (isReceipt)
                    {
                        double openingbalance = this.getCCOpeningBalance(IsCash, isGrandTotal);
                        openingbalance = openingbalance * -1;// We show opening in Receipt side and this is costentre (expense unit)

                        if (IsCash) rtn = openingbalance + cashreceipt;
                        if (!IsCash) rtn = openingbalance + bankreceipt;
                    }
                    else if (!isReceipt)
                    {
                        double closingbalance = getCCClosingBalance(IsCash, isGrandTotal);

                        if (IsCash) rtn = closingbalance + cashpayment;
                        if (!IsCash) rtn = closingbalance + bankpayment;
                    }
                }
            }

            return rtn;
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrCosCapReceiptCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCosCapReceiptCode.Tag != null && xrCosCapReceiptCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCosCapReceiptCode.Tag.ToString());
            }
            else
            {
                xrCosCapReceiptCode.Tag = xrCosCapReceiptCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            grpHeaderCCName.Visible = (ReportProperties.ShowByCostCentre == 1);
            if (grpHeaderCCName.Visible != true)
            {
                this.CosCenterName = ReportProperty.Current.CostCentreName;
            }
            else
            {
                this.CosCenterName = string.Empty;

            }
            xrCosCapReceiptCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCosPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCosCashCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell6.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell23.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell16.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell20.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            // xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            // this.ReportPeriod = this.ReportProperties.ReportDate;
            SetReportBorder();
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblCCCategory = AlignCCCategoryTable(xrtblCCCategory);
            xrtblCCName = AlignCostCentreTable(xrtblCCName);
            xrtblBindSource = AlignContentTable(xrtblBindSource);
            xrTblCCBreakup = AlignTotalTable(xrTblCCBreakup);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            this.SetCurrencyFormat(xrCapRecCash.Text, xrCapRecCash);
            this.SetCurrencyFormat(xrCapRecCash.Text, xrCapPayCash);
            this.SetCurrencyFormat(xrCapRecBank.Text, xrCapRecBank);
            this.SetCurrencyFormat(xrCapRecBank.Text, xrCapPayBank);
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

        private XRTable AlignCCCategoryTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (ReportProperties.IncludeNarration != 1)
                        {
                            if (ReportProperties.ShowByCostCentre == 1)
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right;
                                }
                            }
                        }
                        else
                        {
                            if (ReportProperties.ShowByCostCentre == 1)
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right;
                                }
                            }
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
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

        private XRTable AlignCostCentreTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (ccCount == 1)
                        {
                            if (ReportProperties.IncludeNarration == 1)
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right;
                                }
                            }
                        }
                        else
                        {
                            if (ReportProperties.IncludeNarration == 1)
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                }
                                else
                                {
                                    tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
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
                        if (ccCount == 1)
                        {
                            if (count == 1)
                            {
                                tcell.Borders = BorderSide.Left;
                                if (count == trow.Cells.Count)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right;
                                }
                            }
                        }
                        else
                        {
                            if (count == 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                                if (count == trow.Cells.Count)
                                {
                                    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                                }
                            }
                            else if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }

                            else
                            {
                                tcell.Borders = BorderSide.Bottom;
                            }
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
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
                                if (cellcount == 1)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                                else if (bankNarration != string.Empty || cashNarration != string.Empty)
                                {
                                    if (cellcount == row.Cells.Count)
                                    {
                                        cell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                                    }
                                    else
                                    {
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                    }
                                }
                                else
                                    if (cellcount == row.Cells.Count)
                                    {
                                        cell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                                    }
                                    else
                                    {
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | BorderSide.Top;
                                    }
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

        #endregion

        #region Events

        private void xrCosPayment_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosPayment.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosPayment.Text = "";
            }
        }

        private void xrCosPay_Cash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosPay_Cash.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosPay_Cash.Text = "";
            }
        }

        private void xrCosPaymentCash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosPaymentCash.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosPaymentCash.Text = "";
            }
        }

        private void xrCosPaymentBank_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosPaymentBank.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosPaymentBank.Text = "";
            }
        }

        private void xrtblBindSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            string Narration_Pay = (GetCurrentColumnValue("NARRATION_PAY") == null) ? string.Empty : GetCurrentColumnValue("NARRATION_PAY").ToString();
            xrtblBindSource = AlignCashBankBookTable(xrtblBindSource, Narration, Narration_Pay, count);
        }

        private void xrtblCCName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ccCount++;
            xrtblCCName = AlignCostCentreTable(xrtblCCName);

            xrCCOpeningRow.Visible = (this.AppSetting.ShowCCOpeningBalanceInReports == 1);
        }

        private void xrcellCCOPCash_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            if (GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                double openingbalance = this.getCCOpeningBalance(true, false);
                openingbalance = openingbalance * -1;// We show opening in Receipt side and this is costentre (expense unit)
                string op = string.Empty;
                if (this.AppSetting.EnableCCModeReports == 1)
                    op = (openingbalance < 0 ? "-" : "") + UtilityMember.NumberSet.ToNumber(Math.Abs(openingbalance));
                else
                    op = UtilityMember.NumberSet.ToNumber(Math.Abs(openingbalance)) + " " + (openingbalance < 0 ? TransMode.DR.ToString() : TransMode.CR.ToString());
                e.Value = op;
            }
        }

        private void xrcellCCOPBank_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            if (GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                double openingbalance = this.getCCOpeningBalance(false, false);
                openingbalance = openingbalance * -1;// We show opening in Receipt side and this is costentre (expense unit)
                string op = string.Empty;
                if (this.AppSetting.EnableCCModeReports == 1)
                    op = (openingbalance < 0 ? "-" : "") + UtilityMember.NumberSet.ToNumber(Math.Abs(openingbalance));
                else
                    op = UtilityMember.NumberSet.ToNumber(Math.Abs(openingbalance)) + " " + (openingbalance < 0 ? TransMode.DR.ToString() : TransMode.CR.ToString());
                e.Value = op;
            }
        }

        private void xrcellCCCLCash_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            if (GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                double Cashclosingbalance = getCCClosingBalance(true, false);
                string cl = string.Empty;
                if (this.AppSetting.EnableCCModeReports == 1)
                    cl = (Cashclosingbalance < 0 ? "-" : "") + UtilityMember.NumberSet.ToNumber(Math.Abs(Cashclosingbalance));
                else
                    cl = UtilityMember.NumberSet.ToNumber(Math.Abs(Cashclosingbalance)) + " " + (Cashclosingbalance < 0 ? TransMode.DR.ToString() : TransMode.CR.ToString());
                e.Value = cl;

            }
        }

        private void xrcellCCCLBank_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            if (GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                double Bankclosingbalance = getCCClosingBalance(false, false);
                string cl = string.Empty;
                if (this.AppSetting.EnableCCModeReports == 1)
                    cl = (Bankclosingbalance < 0 ? "-" : "") + UtilityMember.NumberSet.ToNumber(Math.Abs(Bankclosingbalance));
                else
                    cl = UtilityMember.NumberSet.ToNumber(Math.Abs(Bankclosingbalance)) + " " + (Bankclosingbalance < 0 ? TransMode.DR.ToString() : TransMode.CR.ToString());
                e.Value = cl;
            }
        }

        private void xrCCBreakup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrCCClosingRow.Visible = (this.AppSetting.ShowCCOpeningBalanceInReports == 1);
        }

        private void xrcellTotalReceiptCash_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1)
            {
                e.Result = Math.Abs(getCCTotalAmount(true, true, false));
                e.Handled = true;
            }
        }

        private void xrcellTotalReceiptBank_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1)
            {
                e.Result = Math.Abs(getCCTotalAmount(false, true, false));
                e.Handled = true;
            }
        }

        private void xrcellTotalPaymentCash_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1)
            {
                e.Result = Math.Abs(getCCTotalAmount(true, false, false));
                e.Handled = true;
            }
        }

        private void xrcellTotalPaymentBank_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1)
            {
                e.Result = Math.Abs(getCCTotalAmount(false, false, false));
                e.Handled = true;
            }
        }

        private void xrcellSumReceiptCash_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && ReportProperty.Current.ShowByCostCentre == 1)
            {
                e.Value = Math.Abs(getCCTotalAmount(true, true, true));
            }
        }

        private void xrcellSumReceiptBank_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && ReportProperty.Current.ShowByCostCentre == 1)
            {
                e.Value = Math.Abs(getCCTotalAmount(false, true, true));
            }
        }

        private void xrcellSumPaymentCash_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && ReportProperty.Current.ShowByCostCentre == 1)
            {
                e.Value = Math.Abs(getCCTotalAmount(true, false, true));
            }
        }

        private void xrcellSumPaymentBank_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && ReportProperty.Current.ShowByCostCentre == 1)
            {
                e.Value = Math.Abs(getCCTotalAmount(false, false, true));
            }
        }
        #endregion

        private void xrcellCCOPCash_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        private void xrcellCCOPBank_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        private void xrcellCCCLCash_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        private void xrcellCCCLBank_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }
    }
}
