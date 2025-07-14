namespace Bosco.Report.ReportObject
{
    partial class BankRegister
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankRegister));
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandTotalExpectAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblAmountFilter = new DevExpress.XtraReports.UI.XRLabel();
            this.calGrandRecTotal = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGrandPayTotal = new DevExpress.XtraReports.UI.CalculatedField();
            this.calcDailyRecTotal = new DevExpress.XtraReports.UI.CalculatedField();
            this.calDailyOpBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrtblMonth = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrMonthName = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportHeaderBase1 = new Bosco.Report.Base.ReportHeaderBase();
            this.xrTblMonthFooter = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPMonthSum = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpHeaderMonth = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.grpFooterMonth = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrBindSource = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPayments = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportHeaderBase1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblMonthFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrBindSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrBindSource});
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
            this.xrtblHeaderCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
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
            this.xrCapDate,
            this.xrCapVoucherNo,
            this.xrCapLedger,
            this.xrCapAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapDate
            // 
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            // 
            // xrCapVoucherNo
            // 
            this.xrCapVoucherNo.Name = "xrCapVoucherNo";
            resources.ApplyResources(this.xrCapVoucherNo, "xrCapVoucherNo");
            // 
            // xrCapLedger
            // 
            this.xrCapLedger.Name = "xrCapLedger";
            resources.ApplyResources(this.xrCapLedger, "xrCapLedger");
            // 
            // xrCapAmount
            // 
            this.xrCapAmount.Name = "xrCapAmount";
            this.xrCapAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapAmount, "xrCapAmount");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblGrandTotal,
            this.lblAmountFilter});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrtblGrandTotal
            // 
            resources.ApplyResources(this.xrtblGrandTotal, "xrtblGrandTotal");
            this.xrtblGrandTotal.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorders = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UsePadding = false;
            this.xrtblGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotal,
            this.xrGrandTotalExpectAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrGrandTotal
            // 
            resources.ApplyResources(this.xrGrandTotal, "xrGrandTotal");
            this.xrGrandTotal.Name = "xrGrandTotal";
            this.xrGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGrandTotal.StylePriority.UseBorderColor = false;
            this.xrGrandTotal.StylePriority.UseFont = false;
            this.xrGrandTotal.StylePriority.UsePadding = false;
            this.xrGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrGrandTotalExpectAmt
            // 
            resources.ApplyResources(this.xrGrandTotalExpectAmt, "xrGrandTotalExpectAmt");
            this.xrGrandTotalExpectAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BankRegister.AMOUNT")});
            this.xrGrandTotalExpectAmt.Name = "xrGrandTotalExpectAmt";
            this.xrGrandTotalExpectAmt.StylePriority.UseBorderColor = false;
            this.xrGrandTotalExpectAmt.StylePriority.UseFont = false;
            this.xrGrandTotalExpectAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrGrandTotalExpectAmt.Summary = xrSummary1;
            // 
            // lblAmountFilter
            // 
            resources.ApplyResources(this.lblAmountFilter, "lblAmountFilter");
            this.lblAmountFilter.Name = "lblAmountFilter";
            this.lblAmountFilter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAmountFilter.StylePriority.UseFont = false;
            this.lblAmountFilter.StylePriority.UseTextAlignment = false;
            // 
            // calGrandRecTotal
            // 
            this.calGrandRecTotal.DataMember = "CashBankJournal";
            this.calGrandRecTotal.Expression = "Iif(IsNullOrEmpty([].Sum([RECEIPT])), 0, [].Sum([RECEIPT])) + [Parameters.prOPBal" +
                "ance]";
            this.calGrandRecTotal.Name = "calGrandRecTotal";
            // 
            // calGrandPayTotal
            // 
            this.calGrandPayTotal.DataMember = "CashBankJournal";
            this.calGrandPayTotal.Expression = "Iif(IsNullOrEmpty([].Sum([PAYMENT])), 0, [].Sum([PAYMENT])) + [Parameters.prCLBal" +
                "ance]";
            this.calGrandPayTotal.Name = "calGrandPayTotal";
            // 
            // calcDailyRecTotal
            // 
            this.calcDailyRecTotal.DataMember = "CashBankJournal";
            this.calcDailyRecTotal.Name = "calcDailyRecTotal";
            // 
            // calDailyOpBalance
            // 
            this.calDailyOpBalance.DataMember = "CashBankJournal";
            this.calDailyOpBalance.Name = "calDailyOpBalance";
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrtblMonth
            // 
            resources.ApplyResources(this.xrtblMonth, "xrtblMonth");
            this.xrtblMonth.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblMonth.Name = "xrtblMonth";
            this.xrtblMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblMonth.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.xrtblMonth.StyleName = "styleGroupRow";
            this.xrtblMonth.StylePriority.UseBackColor = false;
            this.xrtblMonth.StylePriority.UseBorderColor = false;
            this.xrtblMonth.StylePriority.UseBorders = false;
            this.xrtblMonth.StylePriority.UseFont = false;
            this.xrtblMonth.StylePriority.UsePadding = false;
            this.xrtblMonth.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrMonthName});
            this.xrTableRow10.Name = "xrTableRow10";
            resources.ApplyResources(this.xrTableRow10, "xrTableRow10");
            // 
            // xrMonthName
            // 
            resources.ApplyResources(this.xrMonthName, "xrMonthName");
            this.xrMonthName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrMonthName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BankRegister.MONTH_YEAR_NAME")});
            this.xrMonthName.Name = "xrMonthName";
            this.xrMonthName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 1, 1, 1, 100F);
            this.xrMonthName.StylePriority.UseBackColor = false;
            this.xrMonthName.StylePriority.UseBorders = false;
            this.xrMonthName.StylePriority.UseFont = false;
            this.xrMonthName.StylePriority.UsePadding = false;
            this.xrMonthName.StylePriority.UseTextAlignment = false;
            // 
            // reportHeaderBase1
            // 
            this.reportHeaderBase1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.PageHeader,
            this.ReportFooter});
            this.reportHeaderBase1.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calGrandRecTotal,
            this.calGrandPayTotal,
            this.calcDailyRecTotal,
            this.calDailyOpBalance});
            this.reportHeaderBase1.DataMember = "CashBankJournal";
            this.reportHeaderBase1.DataSource = this.reportSetting1;
            this.reportHeaderBase1.IsDrillDownMode = false;
            resources.ApplyResources(this.reportHeaderBase1, "reportHeaderBase1");
            this.reportHeaderBase1.Name = "reportHeaderBase1";
            this.reportHeaderBase1.ReportId = "";
            this.reportHeaderBase1.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
            this.reportHeaderBase1.Version = "13.2";
            // 
            // xrTblMonthFooter
            // 
            resources.ApplyResources(this.xrTblMonthFooter, "xrTblMonthFooter");
            this.xrTblMonthFooter.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblMonthFooter.Name = "xrTblMonthFooter";
            this.xrTblMonthFooter.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
            this.xrTblMonthFooter.StyleName = "styleTotalRow";
            this.xrTblMonthFooter.StylePriority.UseBackColor = false;
            this.xrTblMonthFooter.StylePriority.UseBorderColor = false;
            this.xrTblMonthFooter.StylePriority.UseBorders = false;
            this.xrTblMonthFooter.StylePriority.UseFont = false;
            this.xrTblMonthFooter.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell64,
            this.xrPMonthSum});
            this.xrTableRow12.Name = "xrTableRow12";
            resources.ApplyResources(this.xrTableRow12, "xrTableRow12");
            // 
            // xrTableCell64
            // 
            resources.ApplyResources(this.xrTableCell64, "xrTableCell64");
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell64.StylePriority.UseBorderColor = false;
            this.xrTableCell64.StylePriority.UseFont = false;
            this.xrTableCell64.StylePriority.UsePadding = false;
            this.xrTableCell64.StylePriority.UseTextAlignment = false;
            // 
            // xrPMonthSum
            // 
            resources.ApplyResources(this.xrPMonthSum, "xrPMonthSum");
            this.xrPMonthSum.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BankRegister.AMOUNT")});
            this.xrPMonthSum.Name = "xrPMonthSum";
            this.xrPMonthSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrPMonthSum.StylePriority.UseBorderColor = false;
            this.xrPMonthSum.StylePriority.UseFont = false;
            this.xrPMonthSum.StylePriority.UsePadding = false;
            this.xrPMonthSum.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrPMonthSum.Summary = xrSummary2;
            // 
            // grpHeaderMonth
            // 
            this.grpHeaderMonth.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblMonth});
            this.grpHeaderMonth.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("MONTH_YEAR", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpHeaderMonth, "grpHeaderMonth");
            this.grpHeaderMonth.Name = "grpHeaderMonth";
            // 
            // grpFooterMonth
            // 
            this.grpFooterMonth.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblMonthFooter});
            resources.ApplyResources(this.grpFooterMonth, "grpFooterMonth");
            this.grpFooterMonth.Name = "grpFooterMonth";
            // 
            // xrBindSource
            // 
            resources.ApplyResources(this.xrBindSource, "xrBindSource");
            this.xrBindSource.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrBindSource.Name = "xrBindSource";
            this.xrBindSource.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrBindSource.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow9});
            this.xrBindSource.StyleName = "styleRow";
            this.xrBindSource.StylePriority.UseBorderColor = false;
            this.xrBindSource.StylePriority.UseBorders = false;
            this.xrBindSource.StylePriority.UseFont = false;
            this.xrBindSource.StylePriority.UsePadding = false;
            this.xrBindSource.StylePriority.UseTextAlignment = false;
            this.xrBindSource.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrBindSource_BeforePrint);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrDate,
            this.xrVoucherNo,
            this.xrLedgerName,
            this.xrPayments});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrDate
            // 
            this.xrDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BankRegister.DATE", "{0:d}")});
            this.xrDate.Name = "xrDate";
            this.xrDate.StyleName = "styleDateInfo";
            this.xrDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrDate, "xrDate");
            // 
            // xrVoucherNo
            // 
            this.xrVoucherNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BankRegister.VOUCHER_NO")});
            this.xrVoucherNo.Name = "xrVoucherNo";
            resources.ApplyResources(this.xrVoucherNo, "xrVoucherNo");
            // 
            // xrLedgerName
            // 
            this.xrLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BankRegister.LEDGER_NAME")});
            this.xrLedgerName.Name = "xrLedgerName";
            this.xrLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 1, 100F);
            this.xrLedgerName.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrLedgerName, "xrLedgerName");
            // 
            // xrPayments
            // 
            this.xrPayments.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BankRegister.AMOUNT", "{0:n}")});
            this.xrPayments.Name = "xrPayments";
            this.xrPayments.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrPayments, "xrPayments");
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell31,
            this.xrTableCell33});
            this.xrTableRow9.Name = "xrTableRow9";
            resources.ApplyResources(this.xrTableRow9, "xrTableRow9");
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Name = "xrTableCell28";
            resources.ApplyResources(this.xrTableCell28, "xrTableCell28");
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.Name = "xrTableCell29";
            resources.ApplyResources(this.xrTableCell29, "xrTableCell29");
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BankRegister.NARRATION")});
            resources.ApplyResources(this.xrTableCell31, "xrTableCell31");
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.StylePriority.UsePadding = false;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Name = "xrTableCell33";
            resources.ApplyResources(this.xrTableCell33, "xrTableCell33");
            // 
            // BankRegister
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grpHeaderMonth,
            this.grpFooterMonth});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calGrandRecTotal,
            this.calGrandPayTotal,
            this.calcDailyRecTotal,
            this.calDailyOpBalance});
            this.DataMember = "BankRegister";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpFooterMonth, 0);
            this.Controls.SetChildIndex(this.grpHeaderMonth, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportHeaderBase1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblMonthFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrBindSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCapAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrCapLedger;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.CalculatedField calGrandRecTotal;
        private DevExpress.XtraReports.UI.CalculatedField calGrandPayTotal;
        private DevExpress.XtraReports.UI.CalculatedField calcDailyRecTotal;
        private DevExpress.XtraReports.UI.CalculatedField calDailyOpBalance;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private Base.ReportHeaderBase reportHeaderBase1;
        private DevExpress.XtraReports.UI.XRLabel lblAmountFilter;
        private DevExpress.XtraReports.UI.XRTable xrtblMonth;
        private DevExpress.XtraReports.UI.XRTable xrTblMonthFooter;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow12;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell64;
        private DevExpress.XtraReports.UI.XRTableCell xrPMonthSum;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderMonth;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooterMonth;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotalExpectAmt;
        private DevExpress.XtraReports.UI.XRTable xrBindSource;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrPayments;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell28;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell31;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell33;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow10;
        private DevExpress.XtraReports.UI.XRTableCell xrMonthName;
    }
}
