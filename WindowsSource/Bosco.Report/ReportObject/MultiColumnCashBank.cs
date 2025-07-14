using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using DevExpress.XtraPrinting;
using AcMEDSync.Model;

namespace Bosco.Report.ReportObject
{
    public partial class MultiColumnCashBank : Bosco.Report.Base.ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;

        int count = 0;
        int Rcount = 0;
        const int HeaderTable = 1;
        const int NonHeaderTable = 0;
        DataTable dtOriginalSource = new DataTable();
        double OPCash = 0;
        double OPBank1 = 0;
        double OPbank2 = 0;

        bool IsloadHeader = false;


        int MonthGroupNumber = 0;
        bool isMonthGroupNumber = false;

        bool isFirstMonthGroup = true;

        double MonthlyGrpCashOpbalance = 0;
        double MonthlyGrpCashClbalance = 0;
        double MonthlyGrpBank1Opbalance = 0;
        double MonthlyGrpBank1Clbalance = 0;
        double MonthlyGrpBank2Opbalance = 0;
        double MonthlyGrpBank2Clbalance = 0;

        double MonthlyCashReceipts = 0;
        double MonthlyBank1Receipts = 0;
        double MonthlyBank2Receipts = 0;

        double MonthlyCashPayments = 0;
        double MonthlyBank1Payments = 0;
        double MonthlyBank2Payments = 0;


        #endregion

        public MultiColumnCashBank()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrtblBindSource, xrR_Column1, new ArrayList { "R_V1" }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
            this.AttachDrillDownToRecord(xrtblBindSource, xrR_column2, new ArrayList { "R_V2" }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
            this.AttachDrillDownToRecord(xrtblBindSource, xrR_column3, new ArrayList { "R_V3" }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");

            this.AttachDrillDownToRecord(xrtblBindSource, xrP_Column1, new ArrayList { "P_V1" }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
            this.AttachDrillDownToRecord(xrtblBindSource, xrP_Column2, new ArrayList { "P_V2" }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
            this.AttachDrillDownToRecord(xrtblBindSource, xr_p_column3, new ArrayList { "P_V3" }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }

        #region Methods
        public override void ShowReport()
        {
            this.DataSource = null;
            IsloadHeader = false;
            OPCash = OPBank1 = OPbank2 = 0;
            LoadMultiColumnCashBank();
            InitializeMonthlyGrouping();
            isFirstMonthGroup = true;
            base.ShowReport();

        }

        public void LoadMultiColumnCashBank()
        {

            SetReportTitle();
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

            LoadHeaderDetails();
            xrtblHeaderCaption = AlignBorders(xrtblHeaderCaption);
            xrtblOpeningBalance = AlignBorders(xrtblOpeningBalance);
            xrtblMonth = AlignBorders(xrtblMonth);
            xrTblMonthFooter = AlignBorders(xrTblMonthFooter);
            xrtblBindSource = AlignBorders(xrtblBindSource);
            xrtblDaillyClosingBalance = AlignBorders(xrtblDaillyClosingBalance);

            //xrTableCell43.Borders =  (this.ReportProperties.ShowVerticalLine == 1) ? BorderSide.None :
            //    (this.ReportProperties.ShowHorizontalLine == 1) ? BorderSide.Bottom : BorderSide.None;
        }

        private void InitializeMonthlyGrouping()
        {
            MonthGroupNumber = 0;
            isMonthGroupNumber = false;
            MonthlyGrpCashOpbalance = 0;
            MonthlyGrpCashClbalance = 0;
            MonthlyGrpBank1Opbalance = 0;
            MonthlyGrpBank1Clbalance = 0;
            MonthlyGrpBank2Opbalance = 0;
            MonthlyGrpBank2Clbalance = 0;

            MonthlyCashReceipts = 0;
            MonthlyBank1Receipts = 0;
            MonthlyBank2Receipts = 0;
            MonthlyCashPayments = 0;
            MonthlyBank1Payments = 0;
            MonthlyBank2Payments = 0;
        }

        private void UpdateMonthlyTotals()
        {
            var rColumn1Value = GetCurrentColumnValue("R_COLUMN1");
            MonthlyCashReceipts += UtilityMember.NumberSet.ToDouble(rColumn1Value != null ? rColumn1Value.ToString() : "0");

            var rColumn2Value = GetCurrentColumnValue("R_COLUMN2");
            MonthlyBank1Receipts += UtilityMember.NumberSet.ToDouble(rColumn2Value != null ? rColumn2Value.ToString() : "0");

            var rColumn3Value = GetCurrentColumnValue("R_COLUMN3");
            MonthlyBank2Receipts += UtilityMember.NumberSet.ToDouble(rColumn3Value != null ? rColumn3Value.ToString() : "0");

            var pColumn1Value = GetCurrentColumnValue("P_COLUMN1");
            MonthlyCashPayments += UtilityMember.NumberSet.ToDouble(pColumn1Value != null ? pColumn1Value.ToString() : "0");

            var pColumn2Value = GetCurrentColumnValue("P_COLUMN2");
            MonthlyBank1Payments += UtilityMember.NumberSet.ToDouble(pColumn2Value != null ? pColumn2Value.ToString() : "0");

            var pColumn3Value = GetCurrentColumnValue("P_COLUMN3");
            MonthlyBank2Payments += UtilityMember.NumberSet.ToDouble(pColumn3Value != null ? pColumn3Value.ToString() : "0");

            MonthlyGrpCashClbalance = (MonthlyGrpCashOpbalance + MonthlyCashReceipts) - MonthlyCashPayments;
            MonthlyGrpBank1Clbalance = (MonthlyGrpBank1Opbalance + MonthlyBank1Receipts) - MonthlyBank1Payments;
            MonthlyGrpBank2Clbalance = (MonthlyGrpBank2Opbalance + MonthlyBank2Receipts) - MonthlyBank2Payments;


        }

        private void BindProperty()
        {
            setHeaderTitleAlignment();
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;// 1062.25f;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            //this.SetLandscapeFooterDateWidth = 890.00f;

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            resultArgs = GetReportSource();
            this.DataSource = null;
            DataView dvCashBankBook = resultArgs.DataSource.TableView;
            // dvCashBankBook.RowFilter = "MULTICASHBANK IN('" + this.ReportProperties.MultiColumn2BankName + "','" + this.ReportProperties.MultiColumn1BankName + "') OR GROUP_ID IN (13)";
            xrReceiptcomlumn1.Text = xrPaymentcomlumn1.Text = this.ReportProperties.MultiColumn1BankName;
            xrReceiptcomlumn2.Text = xrPaymentcomlumn2.Text = this.ReportProperties.MultiColumn2BankName;

            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                DateTime dateBal = DateTime.Parse(this.ReportProperties.DateFrom);

                BalanceProperty bpCash = balancesystem.GetCashBalance(this.ReportProperties.Project,
                    dateBal.ToString(), BalanceSystem.BalanceType.OpeningBalance);
                OPCash = (bpCash.TransMode == TransMode.CR.ToString()) ? (-bpCash.Amount) : bpCash.Amount;
                xrtblCashOPBalance.Text = this.UtilityMember.NumberSet.ToNumber(OPCash);



                if (this.ReportProperties.MultiColumn1LedgerId > 0)
                {
                    BalanceProperty bpbank01 = balancesystem.GetBankBalance("0", this.ReportProperties.Project,
                        this.ReportProperties.MultiColumn1LedgerId.ToString(), dateBal.ToString(), BalanceSystem.BalanceType.OpeningBalance);
                    OPBank1 = (bpbank01.TransMode == TransMode.CR.ToString()) ? (-bpbank01.Amount) : bpbank01.Amount;
                    xrtblOPBankcolumn1Balance.Text = this.UtilityMember.NumberSet.ToNumber(OPBank1);
                }
                else
                {
                    OPBank1 = 0;
                    xrtblOPBankcolumn1Balance.Text = string.Empty;
                    xrRBankMonthOP1.Text = string.Empty;
                }

                if (this.ReportProperties.MultiColumn2LedgerId > 0)
                {
                    BalanceProperty bpbank02 = balancesystem.GetBankBalance("0", this.ReportProperties.Project,
                        this.ReportProperties.MultiColumn2LedgerId.ToString(), dateBal.ToString(), BalanceSystem.BalanceType.OpeningBalance);
                    OPbank2 = (bpbank02.TransMode == TransMode.CR.ToString()) ? (-bpbank02.Amount) : bpbank02.Amount;
                    xrtblOPBankcolumn2Balance.Text = this.UtilityMember.NumberSet.ToNumber(OPbank2);
                }
                else
                {
                    OPbank2 = 0;
                    xrtblOPBankcolumn2Balance.Text = string.Empty;
                    xrRBankMonthOP2.Text = string.Empty;
                }

                this.prOPCashBalance.Value = OPCash;
                this.PrOpBank1Balance.Value = OPBank1;
                this.prOpBank2Balance.Value = OPbank2;
            }

            if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
            {
                dvCashBankBook.RowFilter = "R_COLUMN1>0 OR R_COLUMN2>0 OR R_COLUMN3>0 OR P_COLUMN1>0 OR P_COLUMN2>0 OR P_COLUMN3>0";
                dvCashBankBook = dvCashBankBook.Table.DefaultView;

                this.Detail.Visible = this.ReportFooter.Visible = true;
                dvCashBankBook.Table.TableName = "MultiColumnCashBank";
                //On 05/06/2017, To add Amount filter condition
                AttachAmountFilter(dvCashBankBook);
                this.DataSource = dvCashBankBook;
                this.DataMember = dvCashBankBook.Table.TableName;
                dtOriginalSource = dvCashBankBook.ToTable();

                if (this.ReportProperties.IncludeNarrationwithRefNo == 1 || this.ReportProperties.IncludeNarrationwithNameAddress == 1)
                {
                    this.ReportProperties.IncludeNarration = 1;
                }

                grpHeaderMonth.Visible = grpFooterMonth.Visible = (this.ReportProperties.IncludeLedgerGroupTotal == 1);
            }
            else
            {
                this.DataSource = null;
                this.Detail.Visible = false;
                this.ReportFooter.Visible = true;
                xrTableRow3.Visible = false;

                grpHeaderMonth.Visible = grpFooterMonth.Visible = false;



            }
        }


        //16/05/2025, Load header hide and Show Details
        private void LoadHeaderDetails()
        {
            try
            {
                xrtblHeaderCaption.SuspendLayout();
                xrtblOpeningBalance.SuspendLayout();
                xrtblMonth.SuspendLayout();
                xrTblMonthFooter.SuspendLayout();
                xrtblBindSource.SuspendLayout();
                xrtblDaillyClosingBalance.SuspendLayout();
                if (this.ReportProperties.MultiColumn1LedgerId == 0)
                {
                    /* Receipt side column1 details starts  */
                    // Header
                    if (xrTableRow1.Cells.Contains(xrReceiptcomlumn1))
                        xrTableRow1.Cells.Remove(xrTableRow1.Cells[xrReceiptcomlumn1.Name]);
                    // OP balance
                    if (xrTableRow5.Cells.Contains(xrtblOPBankcolumn1Balance))
                        xrTableRow5.Cells.Remove(xrTableRow5.Cells[xrtblOPBankcolumn1Balance.Name]);

                    // Month Op Balance
                    if (xrTableRow13.Cells.Contains(xrRBankMonthOP1))
                        xrTableRow13.Cells.Remove(xrTableRow13.Cells[xrRBankMonthOP1.Name]);

                    // Detail band
                    if (xrTableRow2.Cells.Contains(xrR_column2))
                        xrTableRow2.Cells.Remove(xrTableRow2.Cells[xrR_column2.Name]);
                    if (xrTableRow9.Cells.Contains(xrTableCell62))
                        xrTableRow9.Cells.Remove(xrTableRow9.Cells[xrTableCell62.Name]);
                    //Total
                    if (xrTableRow3.Cells.Contains(xrtblRcptBank01Total))
                        xrTableRow3.Cells.Remove(xrTableRow3.Cells[xrtblRcptBank01Total.Name]);
                    if (xrTableRow4.Cells.Contains(xrTableCell12))
                        xrTableRow4.Cells.Remove(xrTableRow4.Cells[xrTableCell12.Name]);
                    if (xrTableRow6.Cells.Contains(xrBank01RecGrandTotal))
                        xrTableRow6.Cells.Remove(xrTableRow6.Cells[xrBank01RecGrandTotal.Name]);

                    if (xrTableRow7.Cells.Contains(xrTableCell36))
                        xrTableRow7.Cells.Remove(xrTableRow7.Cells[xrTableCell36.Name]);


                    //Month Closing
                    if (xrTableRow11.Cells.Contains(xrTableCell39))
                        xrTableRow11.Cells.Remove(xrTableRow11.Cells[xrTableCell39.Name]);
                    if (xrTableRow12.Cells.Contains(xrRBankMonthSum1))
                        xrTableRow12.Cells.Remove(xrTableRow12.Cells[xrRBankMonthSum1.Name]);

                    /* Receipt side column1 details ends  */

                    /* Payment side column1 details starts  */
                    // Header
                    if (xrTableRow1.Cells.Contains(xrPaymentcomlumn1))
                        xrTableRow1.Cells.Remove(xrTableRow1.Cells[xrPaymentcomlumn1.Name]);
                    // OP balance
                    if (xrTableRow5.Cells.Contains(xrTableCell21))
                        xrTableRow5.Cells.Remove(xrTableRow5.Cells[xrTableCell21.Name]);

                    // Month OP Balance
                    if (xrTableRow13.Cells.Contains(xrcellPMonthBankOp1))
                        xrTableRow13.Cells.Remove(xrTableRow13.Cells[xrcellPMonthBankOp1.Name]);

                    // Detail band
                    if (xrTableRow2.Cells.Contains(xrP_Column2))
                        xrTableRow2.Cells.Remove(xrTableRow2.Cells[xrP_Column2.Name]);
                    if (xrTableRow9.Cells.Contains(xrTableCell16))
                        xrTableRow9.Cells.Remove(xrTableRow9.Cells[xrTableCell16.Name]);
                    //Total
                    if (xrTableRow3.Cells.Contains(xrTableCell30))
                        xrTableRow3.Cells.Remove(xrTableRow3.Cells[xrTableCell30.Name]);
                    if (xrTableRow4.Cells.Contains(xrCLBColumn1))
                        xrTableRow4.Cells.Remove(xrTableRow4.Cells[xrCLBColumn1.Name]);
                    if (xrTableRow6.Cells.Contains(xrbankPayGrandTotal))
                        xrTableRow6.Cells.Remove(xrTableRow6.Cells[xrbankPayGrandTotal.Name]);
                    /* Payment side column1 details ends  */

                    if (xrTableRow7.Cells.Contains(xrTableCell44))
                        xrTableRow7.Cells.Remove(xrTableRow7.Cells[xrTableCell44.Name]);

                    // Month Clsoing 
                    if (xrTableRow11.Cells.Contains(xrPBankMonthCL1))
                        xrTableRow11.Cells.Remove(xrTableRow11.Cells[xrPBankMonthCL1.Name]);
                    if (xrTableRow12.Cells.Contains(xrPBankMonthSum1))
                        xrTableRow12.Cells.Remove(xrTableRow12.Cells[xrPBankMonthSum1.Name]);
                }
                if (this.ReportProperties.MultiColumn2LedgerId == 0)
                {
                    /* Receipt side column2 details starts  */
                    // Header
                    if (xrTableRow1.Cells.Contains(xrReceiptcomlumn2))
                        xrTableRow1.Cells.Remove(xrTableRow1.Cells[xrReceiptcomlumn2.Name]);
                    // OP balance
                    if (xrTableRow5.Cells.Contains(xrtblOPBankcolumn2Balance))
                        xrTableRow5.Cells.Remove(xrTableRow5.Cells[xrtblOPBankcolumn2Balance.Name]);

                    // MOnth OP balance
                    if (xrTableRow13.Cells.Contains(xrRBankMonthOP2))
                        xrTableRow13.Cells.Remove(xrTableRow13.Cells[xrRBankMonthOP2.Name]);

                    // Detail band
                    if (xrTableRow2.Cells.Contains(xrR_column3))
                        xrTableRow2.Cells.Remove(xrTableRow2.Cells[xrR_column3.Name]);
                    if (xrTableRow9.Cells.Contains(xrTableCell63))
                        xrTableRow9.Cells.Remove(xrTableRow9.Cells[xrTableCell63.Name]);
                    //Total
                    if (xrTableRow3.Cells.Contains(xrtblRcptBank02Total))
                        xrTableRow3.Cells.Remove(xrTableRow3.Cells[xrtblRcptBank02Total.Name]);
                    if (xrTableRow4.Cells.Contains(xrTableCell14))
                        xrTableRow4.Cells.Remove(xrTableRow4.Cells[xrTableCell14.Name]);
                    if (xrTableRow6.Cells.Contains(xrBank02RecGrandTotal))
                        xrTableRow6.Cells.Remove(xrTableRow6.Cells[xrBank02RecGrandTotal.Name]);

                    if (xrTableRow7.Cells.Contains(xrTableCell41))
                        xrTableRow7.Cells.Remove(xrTableRow7.Cells[xrTableCell41.Name]);
                    /* Receipt side column2 details ends  */

                    //Month Closing
                    if (xrTableRow11.Cells.Contains(xrTableCell40))
                        xrTableRow11.Cells.Remove(xrTableRow11.Cells[xrTableCell40.Name]);

                    if (xrTableRow12.Cells.Contains(xrRBankMonthSum2))
                        xrTableRow12.Cells.Remove(xrTableRow12.Cells[xrRBankMonthSum2.Name]);

                    /* Payment side column2 details starts  */
                    // Header
                    if (xrTableRow1.Cells.Contains(xrPaymentcomlumn2))
                        xrTableRow1.Cells.Remove(xrTableRow1.Cells[xrPaymentcomlumn2.Name]);
                    // OP balance
                    if (xrTableRow5.Cells.Contains(xrTableCell24))
                        xrTableRow5.Cells.Remove(xrTableRow5.Cells[xrTableCell24.Name]);
                    // Month OP balance
                    if (xrTableRow13.Cells.Contains(xrcellPMonthBankOP2))
                        xrTableRow13.Cells.Remove(xrTableRow13.Cells[xrcellPMonthBankOP2.Name]);
                    // Detail band
                    if (xrTableRow2.Cells.Contains(xr_p_column3))
                        xrTableRow2.Cells.Remove(xrTableRow2.Cells[xr_p_column3.Name]);
                    if (xrTableRow9.Cells.Contains(xrTableCell18))
                        xrTableRow9.Cells.Remove(xrTableRow9.Cells[xrTableCell18.Name]);
                    //Total
                    if (xrTableRow3.Cells.Contains(xrTableCell31))
                        xrTableRow3.Cells.Remove(xrTableRow3.Cells[xrTableCell31.Name]);

                    if (xrTableRow4.Cells.Contains(xrCLBColumn2))
                        xrTableRow4.Cells.Remove(xrTableRow4.Cells[xrCLBColumn2.Name]);

                    // Month Closing 
                    if (xrTableRow11.Cells.Contains(xrPBankMonthCL2))
                        xrTableRow11.Cells.Remove(xrTableRow11.Cells[xrPBankMonthCL2.Name]);

                    if (xrTableRow12.Cells.Contains(xrPBankMonthSum2))
                        xrTableRow12.Cells.Remove(xrTableRow12.Cells[xrPBankMonthSum2.Name]);


                    if (xrTableRow6.Cells.Contains(xrBank02PayGrandTotal))
                        xrTableRow6.Cells.Remove(xrTableRow6.Cells[xrBank02PayGrandTotal.Name]);

                    if (xrTableRow7.Cells.Contains(xrTableCell45))
                        xrTableRow7.Cells.Remove(xrTableRow7.Cells[xrTableCell45.Name]);
                    /* Payment side column2 details ends  */
                }
                xrtblHeaderCaption.PerformLayout();
                xrtblOpeningBalance.PerformLayout();
                xrtblMonth.PerformLayout();
                xrTblMonthFooter.PerformLayout();
                xrtblBindSource.PerformLayout();
                xrtblDaillyClosingBalance.PerformLayout();
            }
            catch (Exception er)
            {
                MessageRender.ShowMessage(er.Message);
            }
        }


        private void AlignHeaderBorder()
        {
            if (this.ReportProperties.MultiColumn1LedgerId == 0 && this.ReportProperties.MultiColumn2LedgerId == 0)
            {
                float rc = xrReceiptcomlumn1.WidthF + xrReceiptcomlumn2.WidthF;
                xrReceiptcomlumn1.WidthF = 0;
                xrCapPayBank.WidthF = xrCapPayBank.WidthF + rc;
                xrReceiptcomlumn2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)
                            | DevExpress.XtraPrinting.BorderSide.Bottom))));

                xrReceiptcomlumn1.Borders = ((DevExpress.XtraPrinting.BorderSide.Top) | (DevExpress.XtraPrinting.BorderSide.Bottom));

                xrCapPayBank.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                            | DevExpress.XtraPrinting.BorderSide.Bottom))));

                float py = xrPaymentcomlumn1.WidthF + xrPaymentcomlumn2.WidthF;
                xrPaymentcomlumn1.WidthF = 0;
                xrTableCell5.WidthF = xrCapPayBank.WidthF + py;
                xrPaymentcomlumn2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)
                            | DevExpress.XtraPrinting.BorderSide.Bottom))));

                xrPaymentcomlumn1.Borders = ((DevExpress.XtraPrinting.BorderSide.Top) | (DevExpress.XtraPrinting.BorderSide.Bottom));

                xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                            | DevExpress.XtraPrinting.BorderSide.Bottom))));

            }
            else if (this.ReportProperties.MultiColumn1LedgerId > 0 && this.ReportProperties.MultiColumn2LedgerId == 0)
            {
                xrReceiptcomlumn2.Borders = xrPaymentcomlumn2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)
                            | DevExpress.XtraPrinting.BorderSide.Bottom))));

                xrReceiptcomlumn1.Borders = xrPaymentcomlumn1.Borders = ((DevExpress.XtraPrinting.BorderSide.Top) | (DevExpress.XtraPrinting.BorderSide.Left) | (DevExpress.XtraPrinting.BorderSide.Bottom));

                xrCapPayBank.Borders = xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Top)
                            | DevExpress.XtraPrinting.BorderSide.Bottom))));

            }
            else if (this.ReportProperties.MultiColumn2LedgerId > 0 && this.ReportProperties.MultiColumn1LedgerId == 0)
            {
                xrReceiptcomlumn2.Borders = xrPaymentcomlumn2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)
                         | DevExpress.XtraPrinting.BorderSide.Left) | DevExpress.XtraPrinting.BorderSide.Bottom)));

                xrReceiptcomlumn1.Borders = xrPaymentcomlumn1.Borders = ((DevExpress.XtraPrinting.BorderSide.Top) | (DevExpress.XtraPrinting.BorderSide.Bottom));

                xrCapPayBank.Borders = xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Top)
                            | DevExpress.XtraPrinting.BorderSide.Bottom))));

            }
            else
            {
                xrCapPayBank.WidthF = 87.9f;
                xrReceiptcomlumn1.WidthF = 87.9f;
                xrReceiptcomlumn2.WidthF = 86.7f;
                xrPaymentcomlumn1.WidthF = 65.38f;
                xrPaymentcomlumn2.WidthF = 79.36f;
                xrReceiptcomlumn2.Borders = xrPaymentcomlumn2.Borders = xrReceiptcomlumn1.Borders = xrPaymentcomlumn1.Borders =
                    xrCapPayBank.Borders = xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)
                         | DevExpress.XtraPrinting.BorderSide.Left) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            }
        }


        //16/05/2025, To set the Border alignment
        private void AlignHederOPBorder()
        {
            if (this.ReportProperties.MultiColumn1LedgerId == 0 && this.ReportProperties.MultiColumn2LedgerId == 0)
            {
                float rc = xrtblOPBankcolumn1Balance.WidthF + xrtblOPBankcolumn2Balance.WidthF;
                xrtblOPBankcolumn1Balance.WidthF = 0;

                xrRBankMonthOP1.WidthF = 0;

                xrtblCashOPBalance.WidthF = xrtblCashOPBalance.WidthF + rc;

                xrCashRMonthOP.WidthF = xrCashRMonthOP.WidthF + rc;

                xrtblOPBankcolumn2Balance.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right)
                                   | DevExpress.XtraPrinting.BorderSide.Bottom))));
                xrRBankMonthOP2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right)
                                 | DevExpress.XtraPrinting.BorderSide.Bottom))));

                xrtblOPBankcolumn1Balance.Borders = ((DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRBankMonthOP1.Borders = ((DevExpress.XtraPrinting.BorderSide.Bottom));

                xrtblCashOPBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left)
                               | DevExpress.XtraPrinting.BorderSide.Bottom))));
                xrtblCashOPBalance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

                xrCashRMonthOP.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left)
                              | DevExpress.XtraPrinting.BorderSide.Bottom))));
                xrCashRMonthOP.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            }
            else if (this.ReportProperties.MultiColumn1LedgerId > 0 && this.ReportProperties.MultiColumn2LedgerId == 0)
            {
                float rc = xrtblOPBankcolumn2Balance.WidthF;
                xrtblOPBankcolumn1Balance.WidthF = xrtblOPBankcolumn1Balance.WidthF + rc;
                xrRBankMonthOP1.WidthF = xrRBankMonthOP1.WidthF + rc;
                xrtblOPBankcolumn2Balance.Borders = ((DevExpress.XtraPrinting.BorderSide.Right) | (DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRBankMonthOP2.Borders = ((DevExpress.XtraPrinting.BorderSide.Right) | (DevExpress.XtraPrinting.BorderSide.Bottom));
                xrtblOPBankcolumn1Balance.Borders = ((DevExpress.XtraPrinting.BorderSide.Left) | (DevExpress.XtraPrinting.BorderSide.Bottom));

                xrRBankMonthOP1.Borders = ((DevExpress.XtraPrinting.BorderSide.Left) | (DevExpress.XtraPrinting.BorderSide.Bottom));

                xrtblCashOPBalance.Borders = ((DevExpress.XtraPrinting.BorderSide.Bottom));
                xrCashRMonthOP.Borders = ((DevExpress.XtraPrinting.BorderSide.Bottom));
                xrtblOPBankcolumn1Balance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

                xrRBankMonthOP1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            }
            else if (this.ReportProperties.MultiColumn2LedgerId > 0 && this.ReportProperties.MultiColumn1LedgerId == 0)
            {
                float rc = xrtblOPBankcolumn1Balance.WidthF;
                xrtblCashOPBalance.WidthF = xrtblCashOPBalance.WidthF + rc;
                xrCashRMonthOP.WidthF = xrCashRMonthOP.WidthF + rc;
                xrtblOPBankcolumn2Balance.Borders = ((DevExpress.XtraPrinting.BorderSide.Left) | (DevExpress.XtraPrinting.BorderSide.Right));// | (DevExpress.XtraPrinting.BorderSide.Bottom));
                xrRBankMonthOP2.Borders = ((DevExpress.XtraPrinting.BorderSide.Left) | (DevExpress.XtraPrinting.BorderSide.Right));// | (DevExpress.XtraPrinting.BorderSide.Bottom));
                xrtblOPBankcolumn1Balance.Borders = ((DevExpress.XtraPrinting.BorderSide.None));
                xrRBankMonthOP1.Borders = ((DevExpress.XtraPrinting.BorderSide.None));
                xrtblCashOPBalance.Borders = ((DevExpress.XtraPrinting.BorderSide.Left));// | (DevExpress.XtraPrinting.BorderSide.Bottom));
                xrCashRMonthOP.Borders = ((DevExpress.XtraPrinting.BorderSide.Left));
                xrtblOPBankcolumn2Balance.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrRBankMonthOP2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            }
            else
            {
                xrtblCashOPBalance.WidthF = 87.9f;
                xrCashRMonthOP.WidthF = 87.9f;
                xrtblOPBankcolumn1Balance.WidthF = 87.9f;
                xrtblOPBankcolumn1Balance.WidthF = 86.7f;
                xrRBankMonthOP1.WidthF = 87.9f;
                xrRBankMonthOP1.WidthF = 86.7f;
                xrtblOPBankcolumn1Balance.Borders = xrtblOPBankcolumn1Balance.Borders = xrRBankMonthOP1.Borders = xrtblCashOPBalance.Borders = xrCashRMonthOP.Borders =
                    xrCapPayBank.Borders = xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)
                         | DevExpress.XtraPrinting.BorderSide.Left) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            }
        }

        private void AlignDetailsBorder()
        {
            if (this.ReportProperties.MultiColumn1LedgerId == 0 && this.ReportProperties.MultiColumn2LedgerId == 0)
            {
                float rc = xrR_column2.WidthF + xrR_column3.WidthF;
                xrR_Column1.WidthF = 0;
                xrR_Column1.WidthF = xrR_Column1.WidthF + rc;

                xr_p_column3.Borders = xrR_column3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right)
                                     | DevExpress.XtraPrinting.BorderSide.Bottom))));

                xrP_Column2.Borders = xrR_column2.Borders = ((DevExpress.XtraPrinting.BorderSide.Bottom));

                xrP_Column1.Borders = xrR_Column1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left)
                               | DevExpress.XtraPrinting.BorderSide.Bottom))));
                xrR_Column1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

            }
            else if (this.ReportProperties.MultiColumn1LedgerId > 0 && this.ReportProperties.MultiColumn2LedgerId == 0)
            {
                float rc = xrR_column3.WidthF;
                xrR_column2.WidthF = xrR_column2.WidthF + rc;
                xrP_Column2.WidthF = xrP_Column2.WidthF + 79;

                xr_p_column3.Borders = xrR_column3.Borders = ((DevExpress.XtraPrinting.BorderSide.Right) | (DevExpress.XtraPrinting.BorderSide.Bottom));
                xrP_Column2.Borders = xrR_column2.Borders = ((DevExpress.XtraPrinting.BorderSide.Left) | (DevExpress.XtraPrinting.BorderSide.Bottom));
                xrP_Column1.Borders = xrR_Column1.Borders = ((DevExpress.XtraPrinting.BorderSide.Bottom));

                xrR_Column1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            }
            else if (this.ReportProperties.MultiColumn2LedgerId > 0 && this.ReportProperties.MultiColumn1LedgerId == 0)
            {
                float rc = xrR_column3.WidthF;

                xrR_Column1.WidthF = xrR_Column1.WidthF + rc;

                xr_p_column3.Borders = xrR_column3.Borders = ((DevExpress.XtraPrinting.BorderSide.Left) | (DevExpress.XtraPrinting.BorderSide.Top) | (DevExpress.XtraPrinting.BorderSide.Right));
                xrP_Column2.Borders = xrR_column2.Borders = ((DevExpress.XtraPrinting.BorderSide.None) | (DevExpress.XtraPrinting.BorderSide.Top));
                xrP_Column1.Borders = xrR_Column1.Borders = ((DevExpress.XtraPrinting.BorderSide.Left) | (DevExpress.XtraPrinting.BorderSide.Top));
                xr_p_column3.TextAlignment = xrR_column3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            }
            else
            {
                xrR_Column1.WidthF = 87.9f;
                xrR_column2.WidthF = 87.9f;
                xrR_column3.WidthF = 86.7f;

                xrP_Column1.WidthF = 76.88f;
                xrP_Column2.WidthF = 64.51f;
                xr_p_column3.WidthF = 79.36f;

                xrR_Column1.Borders = xrR_column2.Borders = xrR_column3.Borders =
                    xrP_Column1.Borders = xrP_Column2.Borders = xr_p_column3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top)
                         | DevExpress.XtraPrinting.BorderSide.Left) | DevExpress.XtraPrinting.BorderSide.Bottom)));
            }
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.MultiColumnCashbank);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.R_COLUMN2Column, this.ReportProperties.MultiColumn1LedgerId);
                    dataManager.Parameters.Add(this.ReportParameters.R_COLUMN3Column, this.ReportProperties.MultiColumn2LedgerId);
                    dataManager.Parameters.Add(this.ReportParameters.P_COLUMN2Column, this.ReportProperties.MultiColumn1LedgerId);
                    dataManager.Parameters.Add(this.ReportParameters.P_COLUMN3Column, this.ReportProperties.MultiColumn2LedgerId);

                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_PAN_GSTColumn, this.ReportProperties.IncludePanwithGSTNo);

                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_REFNOColumn, this.ReportProperties.IncludeNarrationwithRefNo);
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_NAMEADDRESSColumn, this.ReportProperties.IncludeNarrationwithNameAddress);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankBookQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }

        #endregion

        private void xrtblBindSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("R_NARRATION") == null) ? string.Empty : GetCurrentColumnValue("R_NARRATION").ToString();
            string Narration_Pay = (GetCurrentColumnValue("P_NARRATION") == null) ? string.Empty : GetCurrentColumnValue("P_NARRATION").ToString();
            bool showarrationrow = (ReportProperties.IncludeNarration == 1);
            bool hasnarration = (showarrationrow && (!string.IsNullOrEmpty(Narration) || !string.IsNullOrEmpty(Narration_Pay)));
            xrtblBindSource.Rows[1].Visible = hasnarration;
            xrtblBindSource.Rows.FirstRow.BorderWidth = ReportProperties.ReportBorderStyle == 2 ? 0 : 1;
            xrtblBindSource.Rows[1].BorderWidth = ReportProperties.ReportBorderStyle == 2 ? 0 : 1;
            AssignBordersForNarration(xrtblBindSource.Rows.FirstRow.Cells, hasnarration, false);
            AssignBordersForNarration(xrtblBindSource.Rows[1].Cells, hasnarration, true);

            UpdateMonthlyTotals();
        }

        private void AssignBordersForNarration(XRTableCellCollection cells, bool hasnarration, bool isnarrationrow)
        {

            //if (ReportProperties.ReportBorderStyle > 0 && hasnarration)
            //{
            foreach (XRTableCell cell in cells)
            {
                cell.Borders = hasnarration && !isnarrationrow ? BorderSide.Right : BorderSide.Right | BorderSide.Bottom;
                cell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            }
            //}
        }

        private XRTable AlignBorders(XRTable ttTable)
        {

            foreach (XRTableRow row in ttTable.Rows)
            {
                //For none border, it is make it as 0
                row.BorderWidth = ReportProperties.ReportBorderStyle == 2 ? 0 : 1;
                foreach (XRTableCell cell in row.Cells)
                {
                    //cell.TextAlignment = (cell.TextAlignment == DevExpress.XtraPrinting.TextAlignment.TopLeft ? DevExpress.XtraPrinting.TextAlignment.MiddleLeft :
                    //    cell.TextAlignment == DevExpress.XtraPrinting.TextAlignment.TopRight ? DevExpress.XtraPrinting.TextAlignment.MiddleRight : DevExpress.XtraPrinting.TextAlignment.MiddleLeft);

                    cell.Borders = (ReportProperties.ShowVerticalLine == 1 && ReportProperties.ShowHorizontalLine == 1) ? BorderSide.Right | BorderSide.Bottom :
                        (ReportProperties.ShowVerticalLine == 1) ? BorderSide.Right : (ReportProperties.ShowHorizontalLine == 1) ? BorderSide.Bottom : BorderSide.None;

                    if (ttTable.Name == xrtblHeaderCaption.Name)
                    {
                        cell.Borders = (ReportProperties.ShowVerticalLine == 1 && ReportProperties.ShowHorizontalLine == 1) ? BorderSide.Top | BorderSide.Right | BorderSide.Bottom :
                             (ReportProperties.ShowVerticalLine == 1)
                             ? BorderSide.Top | BorderSide.Right : (ReportProperties.ShowHorizontalLine == 1) ? BorderSide.Top | BorderSide.Bottom : BorderSide.None;
                    }
                    else if (ttTable.Name == xrtblDaillyClosingBalance.Name)
                    {
                        cell.Borders = (ReportProperties.ShowVerticalLine == 1 && ReportProperties.ShowHorizontalLine == 1) ? BorderSide.Right | BorderSide.Bottom :
                             (ReportProperties.ShowVerticalLine == 1)
                             ? BorderSide.Right : (ReportProperties.ShowHorizontalLine == 1) ? BorderSide.Bottom : BorderSide.None;
                    }
                }
            }


            return ttTable;
        }

        private void xrDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xr_p_column3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrP_Column2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrP_Column1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrP_VNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrP_VType_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }



        private void xrP_date_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrR_column3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrR_column2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrR_Column1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrR_VNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrR_VType_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        /*private void SetBorders()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            //xrtblOpeningBalance = AlignOpeningBalanceTable(xrtblOpeningBalance);
            xrtblDaillyClosingBalance = AlignDailyClosingBalance(xrtblDaillyClosingBalance);
            //xrtblClosingBalance = AlignClosingBalance(xrtblClosingBalance);
            //xrtblTotal = AlignTotalTable(xrtblTotal);
            //xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
        }*/

        // to aling cash bank book report table
        public virtual XRTable AlignCashBankBookTable(XRTable table, string bankNarration, string cashNarration, int count)
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
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
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

        private void HandleAmount(BindingEventArgs e)
        {
            if (e.Value != null)
            {
                double amt = UtilityMember.NumberSet.ToDouble(e.Value.ToString());
                if (amt == 0)
                {
                    e.Value = null;
                }
            }
        }

        private void xrCLBCash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            DataTable dtcash = dtOriginalSource;
            double Rcash = 0;
            double Pcash = 0;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                object cashr = dtcash.Compute("SUM(R_COLUMN1)", "");
                object cashp = dtcash.Compute("SUM(P_COLUMN1)", "");
                Rcash = cashr == DBNull.Value || cashr == null ? 0 : this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(R_COLUMN1)", "").ToString());
                Pcash = cashp == DBNull.Value || cashp == null ? 0 : this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(P_COLUMN1)", "").ToString());
            }
            double Total = Rcash + OPCash;
            // xrtblRcptCashTotal.Text = this.UtilityMember.NumberSet.ToNumber(Rcash);
            xrCLBCash.Text = this.UtilityMember.NumberSet.ToNumber(Total - Pcash).ToString();
            xrCashPayGrandTotal.Text = this.UtilityMember.NumberSet.ToNumber((Total - Pcash) + Pcash).ToString();
        }

        private void xrCLBColumn1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double Rcol1 = 0;
            double Pcol1 = 0;

            if (dtOriginalSource != null && dtOriginalSource.Rows.Count > 0)
            {
                object receiptCol1 = dtOriginalSource.Compute("SUM(R_COLUMN2)", "");
                object paymentCol1 = dtOriginalSource.Compute("SUM(P_COLUMN2)", "");

                Rcol1 = receiptCol1 == DBNull.Value || receiptCol1 == null ? 0 : this.UtilityMember.NumberSet.ToDouble(receiptCol1.ToString());
                Pcol1 = paymentCol1 == DBNull.Value || paymentCol1 == null ? 0 : this.UtilityMember.NumberSet.ToDouble(paymentCol1.ToString());
            }

            double Total = Rcol1 + OPBank1;

            if (this.ReportProperties.MultiColumn1LedgerId > 0)
            {
                xrCLBColumn1.Text = this.UtilityMember.NumberSet.ToNumber(Total - Pcol1).ToString();
                xrbankPayGrandTotal.Text = this.UtilityMember.NumberSet.ToNumber((Total - Pcol1) + Pcol1).ToString();
            }
            else
            {
                xrCLBColumn1.Text = xrbankPayGrandTotal.Text = string.Empty;
            }

            //DataTable dtcash = dtOriginalSource;
            //if (dtcash != null && dtcash.Rows.Count > 0)
            //{
            //    double Rcol1 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(R_COLUMN2)", "").ToString());
            //    double Pcol1 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(P_COLUMN2)", "").ToString());
            //    double Total = Rcol1 + OPBank1;
            //    if (this.ReportProperties.MultiColumn1LedgerId > 0)
            //    {
            //        //xrtblRcptBank01Total.Text = this.UtilityMember.NumberSet.ToNumber(Rcol1);
            //        xrCLBColumn1.Text = this.UtilityMember.NumberSet.ToNumber(Total - Pcol1).ToString();
            //        xrbankPayGrandTotal.Text = this.UtilityMember.NumberSet.ToNumber((Total - Pcol1) + Pcol1).ToString();
            //    }
            //    else
            //    {
            //        xrCLBColumn1.Text = xrbankPayGrandTotal.Text = string.Empty;
            //    }
            //}
        }

        private void xrCLBColumn2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double Rcol2 = 0;
            double Pcol2 = 0;

            if (dtOriginalSource != null && dtOriginalSource.Rows.Count > 0)
            {
                object receiptCol2 = dtOriginalSource.Compute("SUM(R_COLUMN3)", "");
                object paymentCol2 = dtOriginalSource.Compute("SUM(P_COLUMN3)", "");

                Rcol2 = receiptCol2 == DBNull.Value || receiptCol2 == null ? 0 : this.UtilityMember.NumberSet.ToDouble(receiptCol2.ToString());
                Pcol2 = paymentCol2 == DBNull.Value || paymentCol2 == null ? 0 : this.UtilityMember.NumberSet.ToDouble(paymentCol2.ToString());
            }

            double Total = Rcol2 + OPbank2;

            if (this.ReportProperties.MultiColumn2LedgerId > 0)
            {
                xrCLBColumn2.Text = this.UtilityMember.NumberSet.ToNumber(Total - Pcol2).ToString();
                xrBank02PayGrandTotal.Text = this.UtilityMember.NumberSet.ToNumber((Total - Pcol2) + Pcol2).ToString();
            }
            else
            {
                xrCLBColumn2.Text = xrBank02PayGrandTotal.Text = string.Empty;
            }

            //DataTable dtcash = dtOriginalSource;
            //if (dtcash != null && dtcash.Rows.Count > 0)
            //{
            //    double Rcol2 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(R_COLUMN3)", "").ToString());
            //    double Pcol2 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(P_COLUMN3)", "").ToString());
            //    double Total = Rcol2 + OPbank2;
            //    if (this.ReportProperties.MultiColumn2LedgerId > 0)
            //    {
            //        //  xrtblRcptBank02Total.Text = this.UtilityMember.NumberSet.ToNumber(Rcol2);
            //        xrCLBColumn2.Text = this.UtilityMember.NumberSet.ToNumber(Total - Pcol2).ToString();
            //        xrBank02PayGrandTotal.Text = this.UtilityMember.NumberSet.ToNumber((Total - Pcol2) + Pcol2).ToString();
            //    }
            //    else
            //    {
            //        xrCLBColumn2.Text = xrBank02PayGrandTotal.Text = string.Empty;
            //    }
            //}
        }

        private void xrtblRcptCashTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            double Rcash = 0;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                Rcash = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(R_COLUMN1)", "").ToString());
                xrtblRcptCashTotal.Text = Rcash != 0 ? this.UtilityMember.NumberSet.ToNumber(Rcash) : "0.00";  // Total Column
                //if (Rcash.Equals(0))
                //    Rcash += OPCash;

            }
            xrCashRecGrandTotal.Text = (Rcash + OPCash) != 0 ? this.UtilityMember.NumberSet.ToNumber(Rcash + OPCash) : "0.00"; // Grand Total Column
        }

        private void xrtblRcptBank01Total_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            double Rbank1 = 0;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                Rbank1 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(R_COLUMN2)", "").ToString());
                //if (Rbank1.Equals(0))
                //    Rbank1 += OPBank1;
                xrtblRcptBank01Total.Text = (Rbank1 != 0) ? this.UtilityMember.NumberSet.ToNumber(Rbank1) : "0.00";
            }
            xrBank01RecGrandTotal.Text = (Rbank1 + OPBank1) != 0 ? this.UtilityMember.NumberSet.ToNumber(Rbank1 + OPBank1) : "0.00"; // Grand Total Column
        }

        private void xrtblRcptBank02Total_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            double Rbank2 = 0;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                Rbank2 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(R_COLUMN3)", "").ToString());
                //if (Rbank2.Equals(0))
                //    Rbank2 += OPbank2;
                xrtblRcptBank02Total.Text = (Rbank2 != 0) ? this.UtilityMember.NumberSet.ToNumber(Rbank2) : "0.00";
            }
            xrBank02RecGrandTotal.Text = (Rbank2 + OPbank2) != 0 ? this.UtilityMember.NumberSet.ToNumber(Rbank2 + OPbank2) : "0.00"; // Grand Total Column
        }

        private void xrTableCell29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                double pCash = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(P_COLUMN1)", "").ToString());
                xrTableCell29.Text = (pCash != 0) ? this.UtilityMember.NumberSet.ToNumber(pCash) : "0.00";
            }
        }

        private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                double Pbank1 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(P_COLUMN2)", "").ToString());
                xrTableCell30.Text = (Pbank1 != 0) ? this.UtilityMember.NumberSet.ToNumber(Pbank1) : "0.00";
            }
        }

        private void xrTableCell31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                double Pbank2 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(P_COLUMN3)", "").ToString());
                xrTableCell31.Text = (Pbank2 != 0) ? this.UtilityMember.NumberSet.ToNumber(Pbank2) : "0.00";
            }
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
                dv.RowFilter = "(R_COLUMN1 > 0 AND R_COLUMN1 " + AmountFilter + ") OR (R_COLUMN2 > 0 AND R_COLUMN2 " + AmountFilter + ")" +
                              " OR (R_COLUMN3 > 0 AND R_COLUMN3 " + AmountFilter + ") OR " +
                              " (P_COLUMN1 > 0 AND P_COLUMN1 " + AmountFilter + ") OR (P_COLUMN2 > 0 AND P_COLUMN2 " + AmountFilter + ")" +
                              " OR (P_COLUMN3 > 0 AND P_COLUMN3 " + AmountFilter + ") ";

                lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                lblAmountFilter.Visible = true;
            }
            return dv;
        }

        private void xrR_Column1_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HandleAmount(e);
        }

        private void xrR_column2_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HandleAmount(e);
        }

        private void xrR_column3_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HandleAmount(e);
        }

        private void xrP_Column1_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HandleAmount(e);
        }

        private void xrP_Column2_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HandleAmount(e);
        }

        private void xr_p_column3_EvaluateBinding(object sender, BindingEventArgs e)
        {
            HandleAmount(e);

        }

        private void xrReceipt_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (e.Value != null)
            {
                string ledgername = e.Value.ToString();
                ledgername = ledgername.Replace("<br>", System.Environment.NewLine);
                e.Value = ledgername;
            }
        }

        private void xrPayments_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (e.Value != null)
            {
                string ledgername = e.Value.ToString();
                ledgername = ledgername.Replace("<br>", System.Environment.NewLine);
                e.Value = ledgername;
            }
        }

        private void grpHeaderMonth_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (isMonthGroupNumber && this.ReportProperties.IncludeLedgerGroupTotal == 1)
            {
                grpHeaderMonth.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            }
            else
            {
                grpHeaderMonth.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;
                isMonthGroupNumber = true;
            }

            if (isFirstMonthGroup)
            {
                MonthlyGrpCashOpbalance = OPCash;
                MonthlyGrpBank1Opbalance = OPBank1;
                MonthlyGrpBank2Opbalance = OPbank2;

                isFirstMonthGroup = false;
            }

            xrCashRMonthOP.Text = this.UtilityMember.NumberSet.ToNumber(MonthlyGrpCashOpbalance);
            xrRBankMonthOP1.Text = this.UtilityMember.NumberSet.ToNumber(MonthlyGrpBank1Opbalance);
            xrRBankMonthOP2.Text = this.UtilityMember.NumberSet.ToNumber(MonthlyGrpBank2Opbalance);

        }

        private void xrCashMonthOP_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyGrpCashOpbalance;
            e.Handled = true;
        }

        private void xrBankMonthOP1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyGrpBank1Opbalance;
            e.Handled = true;
        }

        private void xrBankMonthOP2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyGrpBank2Opbalance;
            e.Handled = true;
        }

        private void xrCashMonthCL_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyGrpCashClbalance; //  double result = (MonthlyGrpCashOpbalance + MonthlyCashReceipts) - MonthlyCashPayments; //
            e.Handled = true;
        }

        private void xrBankMonthCL1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyGrpBank1Clbalance;
            e.Handled = true;
        }

        private void xrBankMonthCL2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyGrpBank2Clbalance;
            e.Handled = true;
        }

        private void xrRCashMonthSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyCashReceipts + MonthlyGrpCashOpbalance;
            e.Handled = true;
        }

        private void xrRBankMonthSum1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyBank1Receipts + MonthlyGrpBank1Opbalance;
            e.Handled = true;
        }

        private void xrRBankMonthSum2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyBank2Receipts + MonthlyGrpBank2Opbalance;
            e.Handled = true;
        }

        private void xrRCashMonthSum_SummaryReset(object sender, EventArgs e)
        {
            if (MonthGroupNumber == 0)
            {
                MonthlyGrpCashOpbalance = OPCash;
                MonthlyGrpBank1Opbalance = OPBank1;
                MonthlyGrpBank2Opbalance = OPbank2;
            }
            else
            {
                MonthlyGrpCashOpbalance = MonthlyGrpCashClbalance;
                MonthlyGrpBank1Opbalance = MonthlyGrpBank1Clbalance;
                MonthlyGrpBank2Opbalance = MonthlyGrpBank2Clbalance;
            }

            MonthlyCashReceipts = MonthlyBank1Receipts = MonthlyBank2Receipts = 0;
            MonthlyCashPayments = MonthlyBank1Payments = MonthlyBank2Payments = 0;

            MonthGroupNumber++;
        }

        private void xrRCashMonthSum_SummaryRowChanged(object sender, EventArgs e)
        {
            UpdateMonthlyTotals();
        }

        private void xrPBankMonthSum1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyBank1Payments + MonthlyGrpBank1Clbalance;
            e.Handled = true;
        }

        private void xrPBankMonthSum2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyBank2Payments + MonthlyGrpBank2Clbalance;
            e.Handled = true;
        }

        private void xrPCashMonthSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = MonthlyCashPayments + MonthlyGrpCashClbalance;
            e.Handled = true;
        }

        private void xrTableCell34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            double Rcash = 0;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                Rcash = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(R_COLUMN1)", "").ToString());
                xrtblRcptCashTotal.Text = Rcash != 0 ? this.UtilityMember.NumberSet.ToNumber(Rcash) : "0.00";  // Total Column
                //if (Rcash.Equals(0))
                //    Rcash += OPCash;

            }
            xrCashRecGrandTotal.Text = (Rcash + OPCash) != 0 ? this.UtilityMember.NumberSet.ToNumber(Rcash + OPCash) : "0.00"; // Grand 
        }

        private void xrTableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            double Rbank1 = 0;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                Rbank1 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(R_COLUMN2)", "").ToString());
                //if (Rbank1.Equals(0))
                //    Rbank1 += OPBank1;
                xrtblRcptBank01Total.Text = (Rbank1 != 0) ? this.UtilityMember.NumberSet.ToNumber(Rbank1) : "0.00";
            }
            xrBank01RecGrandTotal.Text = (Rbank1 + OPBank1) != 0 ? this.UtilityMember.NumberSet.ToNumber(Rbank1 + OPBank1) : "0.00";
        }

        private void xrTableCell41_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            double Rbank2 = 0;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                Rbank2 = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(R_COLUMN3)", "").ToString());
                //if (Rbank2.Equals(0))
                //    Rbank2 += OPbank2;
                xrtblRcptBank02Total.Text = (Rbank2 != 0) ? this.UtilityMember.NumberSet.ToNumber(Rbank2) : "0.00";
            }
            xrBank02RecGrandTotal.Text = (Rbank2 + OPbank2) != 0 ? this.UtilityMember.NumberSet.ToNumber(Rbank2 + OPbank2) : "0.00";
        }

        private void xrTableCell43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable dtcash = dtOriginalSource;
            if (dtcash != null && dtcash.Rows.Count > 0)
            {
                double pCash = this.UtilityMember.NumberSet.ToDouble(dtcash.Compute("SUM(P_COLUMN1)", "").ToString());
                xrTableCell29.Text = (pCash != 0) ? this.UtilityMember.NumberSet.ToNumber(pCash) : "0.00";
            }
        }
    }
}
