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

namespace Bosco.Report.ReportObject
{
    public partial class LedgerCashBankCollection : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public LedgerCashBankCollection()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrTblReportData, xrLedger,
               new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");

            this.AttachDrillDownToRecord(xrTblDateGroup, xrgrpLedger, new ArrayList { this.ReportParameters.LEDGER_IDColumn.ColumnName, 
                    "VOUCHER_DATE" }, DrillDownType.LEDGER_SUMMARY_RECEIPTS, false, "", true);

            //this.AttachDrillDownToRecord(xrTableReceipt, xrLedgerName, ledgerfilter, ledgerdrilltype, false, "", true);
        }

        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindLedgerCashBank();
        }
        #endregion

        #region Methods
        private void BindLedgerCashBank()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        AssignReportProperty();
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
                    AssignReportProperty();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }

        private void AssignReportProperty()
        {
            this.SetTitleWidth(xrTblReportData.WidthF);
            setHeaderTitleAlignment();
            SetReportTitle();

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.CosCenterName = null;
            
            float fontsize = (this.ReportProperties.IncludeDetailed == 1 ? 8.5F : xrCapDate.Font.Size);
            Font grpfnt = new System.Drawing.Font(xrgrpDate.Font.FontFamily, fontsize, (this.ReportProperties.IncludeDetailed == 1 ? FontStyle.Bold : FontStyle.Regular));
            xrgrpDate.Font = xrgrpLedger.Font = xrgrpDateReceiptsCash.Font = xrgrpDateReceiptsBank.Font = xrgrpDatePaymentsCash.Font = xrgrpDatePaymentsBank.Font = grpfnt;
            Detail.Visible = (this.ReportProperties.IncludeDetailed == 1);
            grpVoucherDateHeader.Visible = true;
            if (this.ReportProperties.Consolidated == 1)
            {
                grpVoucherDateHeader.Visible = false;
            }
            
            MakeRandPVisible();
            resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataTable dtLedgerwisecollection = resultArgs.DataSource.Table;
                if (dtLedgerwisecollection != null && dtLedgerwisecollection.Rows.Count != 0)
                {
                    dtLedgerwisecollection.TableName = this.DataMember;
                    //On 05/06/2017, To add Amount filter condition
                    AttachAmountFilter(dtLedgerwisecollection);
                    
                    this.DataSource = dtLedgerwisecollection;
                    this.DataMember = dtLedgerwisecollection.TableName;
                    if (grpVoucherDateHeader.Visible) grpVoucherDateHeader.Visible = true;
                    ReportFooter.Visible = true;


                }
                else
                {
                    grpVoucherDateHeader.Visible = Detail.Visible = ReportFooter.Visible = false;
                    
                    this.DataSource = null;
                }
            }
            else
            {
                MessageRender.ShowMessage("Could not generate Report " + resultArgs.Message, true);
            }

           
        }

        private void MakeRandPVisible()
        {
            try
            {
                if (this.ReportProperties.DayBookVoucherType != 0)
                {
                    bool showReceiptonly = (this.ReportProperties.DayBookVoucherType == 1);
                    
                    //1. For Header
                    xrtblHeaderCaption.SuspendLayout();
                    if (showReceiptonly)
                    {
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow, xrCapPayments);
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapPaymentsCash);
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapPaymentsBank);
                    }
                    else
                    {
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow, xrCapReceipts);
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapReceiptsCash);
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapReceiptsBank);
                    }
                                        
                    xrtblHeaderCaption.PerformLayout();

                    //2. For Day ledger group
                    xrTblLedgerGrp.SuspendLayout();
                    if (showReceiptonly)
                    {
                        RemoveTableCell(xrTblLedgerGrp, xrLedgerGrpRow, xrgrpLedgerPaymentsCash);
                        RemoveTableCell(xrTblLedgerGrp, xrLedgerGrpRow, xrgrpLedgerPaymentsBank);
                    }
                    else
                    {
                        RemoveTableCell(xrTblLedgerGrp, xrLedgerGrpRow, xrgrpLedgerReceiptsCash);
                        RemoveTableCell(xrTblLedgerGrp, xrLedgerGrpRow, xrgrpLedgerReceiptsBank);
                    }
                    
                    xrTblLedgerGrp.PerformLayout();

                    //3. For Date
                    xrTblDateGroup.SuspendLayout();
                    if (showReceiptonly)
                    {
                        RemoveTableCell(xrTblDateGroup, xrDateGrpupRow, xrgrpDatePaymentsCash);
                        RemoveTableCell(xrTblDateGroup, xrDateGrpupRow, xrgrpDatePaymentsBank);
                    }
                    else
                    {
                        RemoveTableCell(xrTblDateGroup, xrDateGrpupRow, xrgrpDateReceiptsCash);
                        RemoveTableCell(xrTblDateGroup, xrDateGrpupRow, xrgrpDateReceiptsBank);
                    }
                    xrTblDateGroup.PerformLayout();

                    //4. For Detail
                    xrTblReportData.SuspendLayout();
                    if (showReceiptonly)
                    {
                        RemoveTableCell(xrTblReportData, xrDataRow, xrPaymentsCash);
                        RemoveTableCell(xrTblReportData, xrDataRow, xrPaymentsBank);
                    }
                    else
                    {
                        RemoveTableCell(xrTblReportData, xrDataRow, xrReceiptsCash);
                        RemoveTableCell(xrTblReportData, xrDataRow, xrReceiptsBank);
                    }
                    xrTblReportData.PerformLayout();

                    //5. For Grand Total 
                    xrtblGrandTotal.SuspendLayout();
                    if (showReceiptonly)
                    {
                        RemoveTableCell(xrtblGrandTotal, xrGrandRow, xrSumPaymentsCash);
                        RemoveTableCell(xrtblGrandTotal, xrGrandRow, xrSumPaymentsBank);
                    }
                    else
                    {
                        RemoveTableCell(xrtblGrandTotal, xrGrandRow, xrSumReceiptsCash);
                        RemoveTableCell(xrtblGrandTotal, xrGrandRow, xrSumReceiptsBank);
                    }
                    xrtblGrandTotal.PerformLayout();
                }

                //Remove Closing Balance for Particular voucher type
                //1. For Header
                xrtblHeaderCaption.SuspendLayout();
                if (this.ReportProperties.DayBookVoucherType !=0 || ReportProperties.ShowClosingBalance == 0)
                {
                    RemoveTableCell(xrtblHeaderCaption, xrHeaderRow, xrCapBalance);
                    RemoveTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapClosingBalance);
                }
                xrtblHeaderCaption.PerformLayout();

                //2. For Day ledger group					
                xrTblLedgerGrp.SuspendLayout();
                //Remove Closing Balance for Particular voucher type
                if (this.ReportProperties.DayBookVoucherType != 0 || ReportProperties.ShowClosingBalance == 0)
                {
                    RemoveTableCell(xrTblLedgerGrp, xrLedgerGrpRow, xrgrpLedgerCLBalance);
                }
                xrTblLedgerGrp.PerformLayout();

                //3. For Date
                xrTblDateGroup.SuspendLayout();
                if (this.ReportProperties.DayBookVoucherType != 0 || ReportProperties.ShowClosingBalance == 0)
                {
                    RemoveTableCell(xrTblDateGroup, xrDateGrpupRow, xrgrpDateCLBalance);
                }
                xrTblDateGroup.PerformLayout();

                //4. For Detail					
                xrTblReportData.SuspendLayout();
                if (this.ReportProperties.DayBookVoucherType != 0 || ReportProperties.ShowClosingBalance == 0)
                {
                    RemoveTableCell(xrTblReportData, xrDataRow, xrCLBalance);
                }
                xrTblReportData.PerformLayout();

                //5. For Grand Total 
                xrtblGrandTotal.SuspendLayout();
                if (this.ReportProperties.DayBookVoucherType != 0 || ReportProperties.ShowClosingBalance == 0)
                {
                    RemoveTableCell(xrtblGrandTotal, xrGrandRow, xrSumCLBalance);
                }
                xrtblGrandTotal.PerformLayout();


                if (ReportProperties.ShowCash != 1 || ReportProperties.ShowBank != 1)
                {
                    if (ReportProperties.ShowCash == 1)
                    {
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapPaymentsBank);
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapReceiptsBank);
                        RemoveTableCell(xrTblLedgerGrp, xrLedgerGrpRow, xrgrpLedgerPaymentsBank);
                        RemoveTableCell(xrTblLedgerGrp, xrLedgerGrpRow, xrgrpLedgerReceiptsBank);
                        RemoveTableCell(xrTblDateGroup, xrDateGrpupRow, xrgrpDatePaymentsBank);
                        RemoveTableCell(xrTblDateGroup, xrDateGrpupRow, xrgrpDateReceiptsBank);
                        RemoveTableCell(xrTblReportData, xrDataRow, xrPaymentsBank);
                        RemoveTableCell(xrTblReportData, xrDataRow, xrReceiptsBank);
                        RemoveTableCell(xrtblGrandTotal, xrGrandRow, xrSumPaymentsBank);
                        RemoveTableCell(xrtblGrandTotal, xrGrandRow, xrSumReceiptsBank);

                        xrCapDate.WidthF = xrgrpDate.WidthF;
                        xrCapLedger.WidthF = xrLedger.WidthF;
                        if (this.ReportProperties.DayBookVoucherType == 1)
                            xrCapReceipts.WidthF = xrCapReceiptsCash.WidthF = xrgrpLedgerReceiptsCash.WidthF;
                        else if (this.ReportProperties.DayBookVoucherType == 2)
                            xrCapPayments.WidthF = xrCapPaymentsCash.WidthF = xrgrpLedgerPaymentsCash.WidthF;
                        else
                        {
                            xrCapReceipts.WidthF = xrCapReceiptsCash.WidthF = xrgrpLedgerReceiptsCash.WidthF;
                            xrCapPayments.WidthF = xrCapPaymentsCash.WidthF = xrgrpLedgerPaymentsCash.WidthF;
                        }
                    }
                    else if (ReportProperties.ShowBank == 1)
                    {
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapPaymentsCash);
                        RemoveTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapReceiptsCash);
                        RemoveTableCell(xrTblLedgerGrp, xrLedgerGrpRow, xrgrpLedgerPaymentsCash);
                        RemoveTableCell(xrTblLedgerGrp, xrLedgerGrpRow, xrgrpLedgerReceiptsCash);
                        RemoveTableCell(xrTblDateGroup, xrDateGrpupRow, xrgrpDatePaymentsCash);
                        RemoveTableCell(xrTblDateGroup, xrDateGrpupRow, xrgrpDateReceiptsCash);
                        RemoveTableCell(xrTblReportData, xrDataRow, xrPaymentsCash);
                        RemoveTableCell(xrTblReportData, xrDataRow, xrReceiptsCash);
                        RemoveTableCell(xrtblGrandTotal, xrGrandRow, xrSumPaymentsCash);
                        RemoveTableCell(xrtblGrandTotal, xrGrandRow, xrSumReceiptsCash);

                        xrCapDate.WidthF = xrgrpDate.WidthF;
                        xrCapLedger.WidthF = xrLedger.WidthF;

                        if (this.ReportProperties.DayBookVoucherType == 1)
                            xrCapReceipts.WidthF = xrCapReceiptsBank.WidthF = xrgrpLedgerReceiptsBank.WidthF;                        
                        else if (this.ReportProperties.DayBookVoucherType == 2)
                            xrCapPayments.WidthF = xrCapPaymentsBank.WidthF = xrgrpLedgerPaymentsBank.WidthF;
                        else
                        {
                            xrCapReceipts.WidthF = xrCapReceiptsBank.WidthF = xrgrpLedgerReceiptsBank.WidthF;
                            xrCapPayments.WidthF = xrCapPaymentsBank.WidthF = xrgrpLedgerPaymentsBank.WidthF;
                        }
                    }
                    
                }
            }
            catch (Exception err)
            {
                //MessageRender.ShowMessage("Could not generate Report " + err.Message, true);
            }
        }
        
        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTblLedgerGrp = AlignContentTable(xrTblLedgerGrp);
            xrTblReportData = AlignContentTable(xrTblReportData);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            this.SetCurrencyFormat(xrCapReceiptsCash.Text, xrCapReceiptsCash);
            this.SetCurrencyFormat(xrCapReceiptsBank.Text, xrCapReceiptsBank);
            this.SetCurrencyFormat(xrCapPaymentsCash.Text, xrCapPaymentsCash);
            this.SetCurrencyFormat(xrCapPaymentsBank.Text, xrCapPaymentsBank);
        }

        public XRTable AlignContentTable(XRTable table)
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
                        tcell.Borders = BorderSide.Right | BorderSide.Top;
                        /*if (count == 1)
                        {
                            tcell.Borders = BorderSide.Right;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Top;
                        }*/
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        }
                        else if (count == 1)
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
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = BorderSide.Left;
                        }
                        else if (count == 1)
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
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string DayBook = this.GetReportSQL(SQL.ReportSQLCommand.Report.LedgerwiseCashBankCollection);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, DayBook);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
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

        /// <summary>
        /// This method is used to add filter condition
        /// </summary>
        private DataTable AttachAmountFilter(DataTable dt)
        {
            //On 05/06/2017, To add Amount filter condition
            string AmountFilter = this.GetAmountFilter();
            string amountfilter = string.Empty;
            lblAmountFilter.Visible = false;
            
            try
            {
                //On 30/08/2018, to filter based on Voucher Type
                string VoucherTypeFilter = string.Empty;
                if (this.ReportProperties.DayBookVoucherType == 1) //For Receipts
                {
                    VoucherTypeFilter = "(" + reportSetting1.Ledger.RECEIPT_CASH_AMOUNTColumn.ColumnName + " > 0 OR " + reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName + " > 0)";
                    if (!string.IsNullOrEmpty(AmountFilter))
                    {
                        if (this.ReportProperties.ShowCash == 1 && this.ReportProperties.ShowByBank == 0)
                            amountfilter = GenerateCondition(true, false, false, false);
                        else if (this.ReportProperties.ShowCash == 0 && this.ReportProperties.ShowByBank == 1)
                            amountfilter = GenerateCondition(false, true, false, false);
                        else
                            amountfilter = GenerateCondition(true, true, false, false);
                    }
                }
                else if (this.ReportProperties.DayBookVoucherType == 2) //For Payments
                {
                    VoucherTypeFilter = "(" + reportSetting1.Ledger.PAYMENT_CASH_AMOUNTColumn.ColumnName + " > 0 OR " + reportSetting1.Ledger.PAYMENT_BANK_AMOUNTColumn.ColumnName + " > 0)";
                    if (!string.IsNullOrEmpty(AmountFilter))
                    {
                        if (this.ReportProperties.ShowCash == 1 && this.ReportProperties.ShowByBank == 0)
                            amountfilter = GenerateCondition(false, false, true, false);
                        else if (this.ReportProperties.ShowCash == 0 && this.ReportProperties.ShowByBank == 1)
                            amountfilter = GenerateCondition(true, false, false, true);
                        else
                            amountfilter = GenerateCondition(true, false, true, true);
                    }
                }
                else if (!string.IsNullOrEmpty(AmountFilter))
                {
                    if (this.ReportProperties.ShowCash == 1 && this.ReportProperties.ShowByBank == 0)
                        amountfilter = GenerateCondition(true, false, true, false);
                    else if (this.ReportProperties.ShowCash == 0 && this.ReportProperties.ShowByBank == 1)
                        amountfilter = GenerateCondition(false, true, false, true);
                    else
                    {
                        amountfilter = GenerateCondition(true, true, true, true);
                    }
                }

                if (this.ReportProperties.ShowCash == 1 && this.ReportProperties.ShowByBank == 0)
                {
                    string cashbankonly = " (" + reportSetting1.Ledger.RECEIPT_CASH_AMOUNTColumn.ColumnName + "> 0 OR " + reportSetting1.Ledger.PAYMENT_CASH_AMOUNTColumn.ColumnName + " > 0 )";
                    amountfilter += (string.IsNullOrEmpty(amountfilter) ? "" : " AND ") + cashbankonly;
                }
                else if (this.ReportProperties.ShowCash == 0 && this.ReportProperties.ShowBank == 1)
                {
                    string cashbankonly = " (" + reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName + " > 0 OR " + reportSetting1.Ledger.PAYMENT_BANK_AMOUNTColumn.ColumnName + " > 0 )";
                    amountfilter += (string.IsNullOrEmpty(amountfilter) ? "" : " AND ") + cashbankonly;
                }

                if (!string.IsNullOrEmpty(amountfilter))
                {
                    dt.DefaultView.RowFilter = VoucherTypeFilter + (string.IsNullOrEmpty(VoucherTypeFilter) ? "" : " AND ") + amountfilter;
                }
                else
                {
                    dt.DefaultView.RowFilter = VoucherTypeFilter;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            return dt.DefaultView.Table;
        }

        private string GenerateCondition(bool receiptcash, bool receiptbank, bool paymentcash, bool paymentbank)
        {
            string rtn = string.Empty;
            string AmountFilter = this.GetAmountFilter();
            string amountfilter = string.Empty;

            if (receiptcash || receiptbank)
            {
                if (receiptcash)
                    rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : " OR " ) + " (" + reportSetting1.Ledger.RECEIPT_CASH_AMOUNTColumn.ColumnName + " > 0 AND " + reportSetting1.Ledger.RECEIPT_CASH_AMOUNTColumn.ColumnName + AmountFilter + ")";
                if (receiptbank)
                    rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : " OR ")  + " (" + reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName + " > 0 AND " + reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName + AmountFilter + ")";
            }

            if (paymentcash || paymentbank)
            {
                if (paymentcash)
                    rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : " OR ")  +  " (" + reportSetting1.Ledger.PAYMENT_CASH_AMOUNTColumn.ColumnName + " > 0 AND " + reportSetting1.Ledger.PAYMENT_CASH_AMOUNTColumn.ColumnName + AmountFilter + ")";
                if (paymentbank)
                    rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : " OR ")  + "  (" + reportSetting1.Ledger.PAYMENT_BANK_AMOUNTColumn.ColumnName + " > 0 AND " + reportSetting1.Ledger.PAYMENT_BANK_AMOUNTColumn.ColumnName + AmountFilter + ")";
            }

            if (!string.IsNullOrEmpty(rtn))
            {
                rtn = "(" + rtn + ")";
            }

            return rtn;
        }
        #endregion

        #region Events
        private void xrgrpDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            /*XRTableCell cell = sender as XRTableCell;
            cell.ProcessDuplicates = ValueSuppressType.MergeByValue;
            if (this.ReportProperties.IncludeDetailed == 1)
            {
                cell.ProcessDuplicates = ValueSuppressType.Leave;
            }*/
        }

        private void xrCLBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            if (this.GetCurrentColumnValue(reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName) != null)
            {
                double amtReceiptCash = UtilityMember.NumberSet.ToDouble(this.GetCurrentColumnValue(reportSetting1.Ledger.RECEIPT_CASH_AMOUNTColumn.ColumnName).ToString());
                double amtReceiptBank = UtilityMember.NumberSet.ToDouble(this.GetCurrentColumnValue(reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName).ToString());
                double amtPaymentCash = UtilityMember.NumberSet.ToDouble(this.GetCurrentColumnValue(reportSetting1.Ledger.PAYMENT_CASH_AMOUNTColumn.ColumnName).ToString()); ;
                double amtPaymentBank = UtilityMember.NumberSet.ToDouble(this.GetCurrentColumnValue(reportSetting1.Ledger.PAYMENT_BANK_AMOUNTColumn.ColumnName).ToString()); ;
                e.Value = (amtReceiptCash + amtReceiptBank) - (amtPaymentCash + amtPaymentBank);
            }
        }

        private void xrgrpDateCLBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            if (this.DataSource != null && this.GetCurrentColumnValue(reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName) != null)
            {
                string date = UtilityMember.DateSet.ToDate(this.GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_DATEColumn.ColumnName).ToString(), false).ToShortDateString();
                Int32 lid = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                DataTable dtReportData = this.DataSource as DataTable;
                string condition = reportSetting1.Ledger.VOUCHER_DATEColumn.ColumnName + " = '" + date + "'";
                condition += " AND " + ReportParameters.LEDGER_IDColumn.ColumnName + " = " + lid;
                double amtReceipt = UtilityMember.NumberSet.ToDouble(dtReportData.Compute("SUM(" + reportSetting1.Ledger.RECEIPT_CASH_AMOUNTColumn.ColumnName + ") + "
                                    + "SUM(" + reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName + ")", condition).ToString());
                double amtPayment = UtilityMember.NumberSet.ToDouble(dtReportData.Compute("SUM(" + reportSetting1.Ledger.PAYMENT_CASH_AMOUNTColumn.ColumnName + ") + "
                                    + "SUM(" + reportSetting1.Ledger.PAYMENT_BANK_AMOUNTColumn.ColumnName + ")", condition).ToString());
                e.Value = (amtReceipt - amtPayment);
            }
        }

        private void xrgrpLedgerCLBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            if (this.DataSource != null && this.GetCurrentColumnValue(reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName) != null)
            {
                Int32 lid = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue(ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                DataTable dtReportData = this.DataSource as DataTable;
                string condition = ReportParameters.LEDGER_IDColumn.ColumnName + " = " + lid;
                double amtReceipt = UtilityMember.NumberSet.ToDouble(dtReportData.Compute("SUM(" + reportSetting1.Ledger.RECEIPT_CASH_AMOUNTColumn.ColumnName + ") + "
                                    + "SUM(" + reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName + ")", condition).ToString());
                double amtPayment = UtilityMember.NumberSet.ToDouble(dtReportData.Compute("SUM(" + reportSetting1.Ledger.PAYMENT_CASH_AMOUNTColumn.ColumnName + ") + "
                                    + "SUM(" + reportSetting1.Ledger.PAYMENT_BANK_AMOUNTColumn.ColumnName + ")", condition).ToString());
                e.Value = (amtReceipt - amtPayment);
            }
        }

        private void xrSumCLBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            if (this.DataSource != null && this.GetCurrentColumnValue(reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName) != null)
            {
                DataTable dtReportData = this.DataSource as DataTable;   
                double amtReceipt = UtilityMember.NumberSet.ToDouble(dtReportData.Compute("SUM(" + reportSetting1.Ledger.RECEIPT_CASH_AMOUNTColumn.ColumnName + ") + "
                                    + "SUM(" + reportSetting1.Ledger.RECEIPT_BANK_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                double amtPayment = UtilityMember.NumberSet.ToDouble(dtReportData.Compute("SUM(" + reportSetting1.Ledger.PAYMENT_CASH_AMOUNTColumn.ColumnName + ") + "
                                    + "SUM(" + reportSetting1.Ledger.PAYMENT_BANK_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                e.Value = (amtReceipt - amtPayment);
            }
        }
        #endregion

       


      
    }
}
