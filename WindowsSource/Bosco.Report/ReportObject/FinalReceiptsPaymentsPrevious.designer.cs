namespace Bosco.Report.ReportObject
{
    partial class FinalReceiptsPaymentsPrevious
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinalReceiptsPaymentsPrevious));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapReceiptCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrReceiptLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapReceiptAmountPrevious = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapReceiptAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPaymentCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPaymentLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPaymentAmountPrevious = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPaymentAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpOpeningBalance = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblOpeningBalance = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrOpeningBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubOpeningBalance = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubReceipts = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubPayments = new DevExpress.XtraReports.UI.XRSubreport();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblAmountFilter = new DevExpress.XtraReports.UI.XRLabel();
            this.xtTblClosingBalance = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrColClosingBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotalReceipts = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrReceiptAmtPrevious = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrReceiptAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandTotalPayment = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPaymentAmtPrevious = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPaymentAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubClosingBalance = new DevExpress.XtraReports.UI.XRSubreport();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.clPaymentAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.clfReceiptAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrLineLeft = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.xrLineRight = new DevExpress.XtraReports.UI.XRCrossBandLine();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblOpeningBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtTblClosingBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubPayments,
            this.xrSubReceipts});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderCaption});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // xrtblHeaderCaption
            // 
            resources.ApplyResources(this.xrtblHeaderCaption, "xrtblHeaderCaption");
            this.xrtblHeaderCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.StyleName = "styleColumnHeader";
            this.xrtblHeaderCaption.StylePriority.UseBackColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorderColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorders = false;
            this.xrtblHeaderCaption.StylePriority.UseFont = false;
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapReceiptCode,
            this.xrReceiptLedgerName,
            this.xrCapReceiptAmountPrevious,
            this.xrCapReceiptAmount,
            this.xrCapPaymentCode,
            this.xrCapPaymentLedgerName,
            this.xrCapPaymentAmountPrevious,
            this.xrCapPaymentAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapReceiptCode
            // 
            this.xrCapReceiptCode.Name = "xrCapReceiptCode";
            resources.ApplyResources(this.xrCapReceiptCode, "xrCapReceiptCode");
            // 
            // xrReceiptLedgerName
            // 
            this.xrReceiptLedgerName.Name = "xrReceiptLedgerName";
            resources.ApplyResources(this.xrReceiptLedgerName, "xrReceiptLedgerName");
            // 
            // xrCapReceiptAmountPrevious
            // 
            this.xrCapReceiptAmountPrevious.Name = "xrCapReceiptAmountPrevious";
            this.xrCapReceiptAmountPrevious.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapReceiptAmountPrevious, "xrCapReceiptAmountPrevious");
            // 
            // xrCapReceiptAmount
            // 
            this.xrCapReceiptAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapReceiptAmount.Name = "xrCapReceiptAmount";
            this.xrCapReceiptAmount.StylePriority.UseBorders = false;
            this.xrCapReceiptAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapReceiptAmount, "xrCapReceiptAmount");
            // 
            // xrCapPaymentCode
            // 
            this.xrCapPaymentCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapPaymentCode.Name = "xrCapPaymentCode";
            this.xrCapPaymentCode.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrCapPaymentCode, "xrCapPaymentCode");
            // 
            // xrCapPaymentLedgerName
            // 
            this.xrCapPaymentLedgerName.Name = "xrCapPaymentLedgerName";
            resources.ApplyResources(this.xrCapPaymentLedgerName, "xrCapPaymentLedgerName");
            // 
            // xrCapPaymentAmountPrevious
            // 
            this.xrCapPaymentAmountPrevious.Name = "xrCapPaymentAmountPrevious";
            this.xrCapPaymentAmountPrevious.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapPaymentAmountPrevious, "xrCapPaymentAmountPrevious");
            // 
            // xrCapPaymentAmount
            // 
            this.xrCapPaymentAmount.Name = "xrCapPaymentAmount";
            this.xrCapPaymentAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapPaymentAmount, "xrCapPaymentAmount");
            // 
            // grpOpeningBalance
            // 
            this.grpOpeningBalance.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblOpeningBalance,
            this.xrSubOpeningBalance});
            resources.ApplyResources(this.grpOpeningBalance, "grpOpeningBalance");
            this.grpOpeningBalance.Name = "grpOpeningBalance";
            // 
            // xrTblOpeningBalance
            // 
            resources.ApplyResources(this.xrTblOpeningBalance, "xrTblOpeningBalance");
            this.xrTblOpeningBalance.Name = "xrTblOpeningBalance";
            this.xrTblOpeningBalance.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrOpeningBalance});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrOpeningBalance
            // 
            resources.ApplyResources(this.xrOpeningBalance, "xrOpeningBalance");
            this.xrOpeningBalance.Name = "xrOpeningBalance";
            this.xrOpeningBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrOpeningBalance.StylePriority.UseFont = false;
            this.xrOpeningBalance.StylePriority.UsePadding = false;
            this.xrOpeningBalance.StylePriority.UseTextAlignment = false;
            // 
            // xrSubOpeningBalance
            // 
            resources.ApplyResources(this.xrSubOpeningBalance, "xrSubOpeningBalance");
            this.xrSubOpeningBalance.Name = "xrSubOpeningBalance";
            this.xrSubOpeningBalance.ReportSource = new Bosco.Report.ReportObject.AccountBalancePreviousYear();
            // 
            // xrSubReceipts
            // 
            resources.ApplyResources(this.xrSubReceipts, "xrSubReceipts");
            this.xrSubReceipts.Name = "xrSubReceipts";
            this.xrSubReceipts.ReportSource = new Bosco.Report.ReportObject.ReceiptsPrevious();
            // 
            // xrSubPayments
            // 
            resources.ApplyResources(this.xrSubPayments, "xrSubPayments");
            this.xrSubPayments.Name = "xrSubPayments";
            this.xrSubPayments.ReportSource = new Bosco.Report.ReportObject.PaymentsPrevious();
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblAmountFilter,
            this.xtTblClosingBalance,
            this.xrtblGrandTotal,
            this.xrSubClosingBalance});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblAmountFilter
            // 
            resources.ApplyResources(this.lblAmountFilter, "lblAmountFilter");
            this.lblAmountFilter.Name = "lblAmountFilter";
            this.lblAmountFilter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAmountFilter.StylePriority.UseFont = false;
            this.lblAmountFilter.StylePriority.UseTextAlignment = false;
            // 
            // xtTblClosingBalance
            // 
            resources.ApplyResources(this.xtTblClosingBalance, "xtTblClosingBalance");
            this.xtTblClosingBalance.Name = "xtTblClosingBalance";
            this.xtTblClosingBalance.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrColClosingBalance});
            this.xrTableRow4.Name = "xrTableRow4";
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            // 
            // xrColClosingBalance
            // 
            resources.ApplyResources(this.xrColClosingBalance, "xrColClosingBalance");
            this.xrColClosingBalance.Name = "xrColClosingBalance";
            this.xrColClosingBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrColClosingBalance.StylePriority.UseFont = false;
            this.xrColClosingBalance.StylePriority.UsePadding = false;
            this.xrColClosingBalance.StylePriority.UseTextAlignment = false;
            // 
            // xrtblGrandTotal
            // 
            resources.ApplyResources(this.xrtblGrandTotal, "xrtblGrandTotal");
            this.xrtblGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorders = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UsePadding = false;
            this.xrtblGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotalReceipts,
            this.xrReceiptAmtPrevious,
            this.xrReceiptAmt,
            this.xrGrandTotalPayment,
            this.xrPaymentAmtPrevious,
            this.xrPaymentAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrGrandTotalReceipts
            // 
            this.xrGrandTotalReceipts.Name = "xrGrandTotalReceipts";
            this.xrGrandTotalReceipts.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrGrandTotalReceipts, "xrGrandTotalReceipts");
            // 
            // xrReceiptAmtPrevious
            // 
            this.xrReceiptAmtPrevious.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMTPREVIOUS")});
            this.xrReceiptAmtPrevious.Name = "xrReceiptAmtPrevious";
            this.xrReceiptAmtPrevious.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrReceiptAmtPrevious.Summary = xrSummary1;
            resources.ApplyResources(this.xrReceiptAmtPrevious, "xrReceiptAmtPrevious");
            this.xrReceiptAmtPrevious.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrReceiptAmtPrevious_SummaryGetResult);
            // 
            // xrReceiptAmt
            // 
            this.xrReceiptAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.clfReceiptAmt")});
            this.xrReceiptAmt.Name = "xrReceiptAmt";
            this.xrReceiptAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrReceiptAmt.Summary = xrSummary2;
            resources.ApplyResources(this.xrReceiptAmt, "xrReceiptAmt");
            this.xrReceiptAmt.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrReceiptAmt_SummaryGetResult);
            // 
            // xrGrandTotalPayment
            // 
            this.xrGrandTotalPayment.Name = "xrGrandTotalPayment";
            this.xrGrandTotalPayment.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrGrandTotalPayment, "xrGrandTotalPayment");
            // 
            // xrPaymentAmtPrevious
            // 
            this.xrPaymentAmtPrevious.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.PAYMENTAMTPREVIOUS")});
            this.xrPaymentAmtPrevious.Name = "xrPaymentAmtPrevious";
            this.xrPaymentAmtPrevious.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrPaymentAmtPrevious.Summary = xrSummary3;
            resources.ApplyResources(this.xrPaymentAmtPrevious, "xrPaymentAmtPrevious");
            this.xrPaymentAmtPrevious.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrPaymentAmtPrevious_SummaryGetResult);
            // 
            // xrPaymentAmt
            // 
            this.xrPaymentAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.clPaymentAmt")});
            this.xrPaymentAmt.Name = "xrPaymentAmt";
            this.xrPaymentAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary4.IgnoreNullValues = true;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrPaymentAmt.Summary = xrSummary4;
            resources.ApplyResources(this.xrPaymentAmt, "xrPaymentAmt");
            this.xrPaymentAmt.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrPaymentAmt_SummaryGetResult);
            // 
            // xrSubClosingBalance
            // 
            resources.ApplyResources(this.xrSubClosingBalance, "xrSubClosingBalance");
            this.xrSubClosingBalance.Name = "xrSubClosingBalance";
            this.xrSubClosingBalance.ReportSource = new Bosco.Report.ReportObject.AccountBalancePreviousYear();
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clPaymentAmt
            // 
            this.clPaymentAmt.DataMember = "Payments";
            this.clPaymentAmt.Name = "clPaymentAmt";
            // 
            // clfReceiptAmt
            // 
            this.clfReceiptAmt.DataMember = "Receipts";
            this.clfReceiptAmt.Name = "clfReceiptAmt";
            // 
            // xrLineLeft
            // 
            this.xrLineLeft.EndBand = this.ReportFooter;
            resources.ApplyResources(this.xrLineLeft, "xrLineLeft");
            this.xrLineLeft.Name = "xrLineLeft";
            this.xrLineLeft.StartBand = this.Detail;
            this.xrLineLeft.WidthF = 1F;
            // 
            // xrLineRight
            // 
            this.xrLineRight.EndBand = this.grpOpeningBalance;
            resources.ApplyResources(this.xrLineRight, "xrLineRight");
            this.xrLineRight.Name = "xrLineRight";
            this.xrLineRight.StartBand = this.grpOpeningBalance;
            this.xrLineRight.WidthF = 1.041748F;
            // 
            // FinalReceiptsPaymentsPrevious
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.grpOpeningBalance,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.clPaymentAmt,
            this.clfReceiptAmt});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrLineRight,
            this.xrLineLeft});
            this.DataMember = "FinalReceiptsPayments";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.FinalReceiptsPaymentsPrevious_BeforePrint);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.grpOpeningBalance, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblOpeningBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtTblClosingBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRSubreport xrSubReceipts;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrReceiptLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentAmount;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpOpeningBalance;
        private DevExpress.XtraReports.UI.XRSubreport xrSubOpeningBalance;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotalReceipts;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotalPayment;
        private DevExpress.XtraReports.UI.XRTableCell xrPaymentAmt;
        private DevExpress.XtraReports.UI.XRSubreport xrSubClosingBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrReceiptAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField clPaymentAmt;
        private DevExpress.XtraReports.UI.CalculatedField clfReceiptAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceiptCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceiptAmount;
        private DevExpress.XtraReports.UI.XRTable xrTblOpeningBalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrOpeningBalance;
        private DevExpress.XtraReports.UI.XRTable xtTblClosingBalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrColClosingBalance;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrLineLeft;
        private DevExpress.XtraReports.UI.XRLabel lblAmountFilter;
        private DevExpress.XtraReports.UI.XRTableCell xrCapReceiptAmountPrevious;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPaymentAmountPrevious;
        private DevExpress.XtraReports.UI.XRTableCell xrReceiptAmtPrevious;
        private DevExpress.XtraReports.UI.XRTableCell xrPaymentAmtPrevious;
        private DevExpress.XtraReports.UI.XRSubreport xrSubPayments;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrLineRight;
    }
}
