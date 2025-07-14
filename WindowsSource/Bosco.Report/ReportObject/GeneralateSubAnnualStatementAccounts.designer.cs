namespace Bosco.Report.ReportObject
{
    partial class GeneralateSubAnnualStatementAccounts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralateSubAnnualStatementAccounts));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrHeaderRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrHeaderLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderAmountCurrentYear = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderAmountPreviousYear = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrHeaderLedgerCode1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderLedgerName1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderAmountCurrentYear1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderAmountPreviousYear1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableLedger = new DevExpress.XtraReports.UI.XRTable();
            this.xrRowData = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.calcClosingBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.calTotalOpeningDebitAmount = new DevExpress.XtraReports.UI.CalculatedField();
            this.calTotalOpeningCreditBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.calCurrentTransDebit = new DevExpress.XtraReports.UI.CalculatedField();
            this.calCurrentTransCredit = new DevExpress.XtraReports.UI.CalculatedField();
            this.calTotalClosingDebit = new DevExpress.XtraReports.UI.CalculatedField();
            this.calTotalClosingCredit = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGroupClosingBal = new DevExpress.XtraReports.UI.CalculatedField();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTableGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrRowTotal = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalCYSum = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalPYSum = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrIERow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrIETitle1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCYIEAmount1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPYIEAmount1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrIERow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrIETitle2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCYIEAmount2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPYIEAmount2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRowGrandTotal = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCYGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPYGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableLedger});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.KeepTogether = true;
            // 
            // PageHeader
            // 
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrReportDate,
            this.xrReportTitle,
            this.xrtblHeaderCaption});
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseBorderColor = false;
            // 
            // xrReportDate
            // 
            resources.ApplyResources(this.xrReportDate, "xrReportDate");
            this.xrReportDate.Name = "xrReportDate";
            this.xrReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrReportDate.StylePriority.UseFont = false;
            this.xrReportDate.StylePriority.UseForeColor = false;
            this.xrReportDate.StylePriority.UsePadding = false;
            this.xrReportDate.StylePriority.UseTextAlignment = false;
            // 
            // xrReportTitle
            // 
            resources.ApplyResources(this.xrReportTitle, "xrReportTitle");
            this.xrReportTitle.Name = "xrReportTitle";
            this.xrReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrReportTitle.StylePriority.UseFont = false;
            this.xrReportTitle.StylePriority.UsePadding = false;
            this.xrReportTitle.StylePriority.UseTextAlignment = false;
            // 
            // xrtblHeaderCaption
            // 
            resources.ApplyResources(this.xrtblHeaderCaption, "xrtblHeaderCaption");
            this.xrtblHeaderCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrHeaderRow,
            this.xrHeaderRow1});
            this.xrtblHeaderCaption.StylePriority.UseBorderColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorders = false;
            // 
            // xrHeaderRow
            // 
            this.xrHeaderRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrHeaderLedgerCode,
            this.xrHeaderLedgerName,
            this.xrHeaderAmountCurrentYear,
            this.xrHeaderAmountPreviousYear});
            this.xrHeaderRow.Name = "xrHeaderRow";
            resources.ApplyResources(this.xrHeaderRow, "xrHeaderRow");
            // 
            // xrHeaderLedgerCode
            // 
            resources.ApplyResources(this.xrHeaderLedgerCode, "xrHeaderLedgerCode");
            this.xrHeaderLedgerCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrHeaderLedgerCode.Name = "xrHeaderLedgerCode";
            this.xrHeaderLedgerCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderLedgerCode.StylePriority.UseBackColor = false;
            this.xrHeaderLedgerCode.StylePriority.UseBorderColor = false;
            this.xrHeaderLedgerCode.StylePriority.UseBorders = false;
            this.xrHeaderLedgerCode.StylePriority.UseFont = false;
            this.xrHeaderLedgerCode.StylePriority.UsePadding = false;
            this.xrHeaderLedgerCode.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderLedgerName
            // 
            resources.ApplyResources(this.xrHeaderLedgerName, "xrHeaderLedgerName");
            this.xrHeaderLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrHeaderLedgerName.Name = "xrHeaderLedgerName";
            this.xrHeaderLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderLedgerName.StylePriority.UseBackColor = false;
            this.xrHeaderLedgerName.StylePriority.UseBorderColor = false;
            this.xrHeaderLedgerName.StylePriority.UseBorders = false;
            this.xrHeaderLedgerName.StylePriority.UseFont = false;
            this.xrHeaderLedgerName.StylePriority.UsePadding = false;
            this.xrHeaderLedgerName.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderAmountCurrentYear
            // 
            resources.ApplyResources(this.xrHeaderAmountCurrentYear, "xrHeaderAmountCurrentYear");
            this.xrHeaderAmountCurrentYear.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrHeaderAmountCurrentYear.Name = "xrHeaderAmountCurrentYear";
            this.xrHeaderAmountCurrentYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderAmountCurrentYear.StylePriority.UseBackColor = false;
            this.xrHeaderAmountCurrentYear.StylePriority.UseBorderColor = false;
            this.xrHeaderAmountCurrentYear.StylePriority.UseBorders = false;
            this.xrHeaderAmountCurrentYear.StylePriority.UseFont = false;
            this.xrHeaderAmountCurrentYear.StylePriority.UsePadding = false;
            this.xrHeaderAmountCurrentYear.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderAmountPreviousYear
            // 
            resources.ApplyResources(this.xrHeaderAmountPreviousYear, "xrHeaderAmountPreviousYear");
            this.xrHeaderAmountPreviousYear.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrHeaderAmountPreviousYear.Name = "xrHeaderAmountPreviousYear";
            this.xrHeaderAmountPreviousYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderAmountPreviousYear.StylePriority.UseBackColor = false;
            this.xrHeaderAmountPreviousYear.StylePriority.UseBorderColor = false;
            this.xrHeaderAmountPreviousYear.StylePriority.UseBorders = false;
            this.xrHeaderAmountPreviousYear.StylePriority.UseFont = false;
            this.xrHeaderAmountPreviousYear.StylePriority.UsePadding = false;
            this.xrHeaderAmountPreviousYear.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderRow1
            // 
            this.xrHeaderRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrHeaderLedgerCode1,
            this.xrHeaderLedgerName1,
            this.xrHeaderAmountCurrentYear1,
            this.xrHeaderAmountPreviousYear1});
            this.xrHeaderRow1.Name = "xrHeaderRow1";
            resources.ApplyResources(this.xrHeaderRow1, "xrHeaderRow1");
            // 
            // xrHeaderLedgerCode1
            // 
            this.xrHeaderLedgerCode1.Name = "xrHeaderLedgerCode1";
            this.xrHeaderLedgerCode1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderLedgerCode1.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrHeaderLedgerCode1, "xrHeaderLedgerCode1");
            // 
            // xrHeaderLedgerName1
            // 
            resources.ApplyResources(this.xrHeaderLedgerName1, "xrHeaderLedgerName1");
            this.xrHeaderLedgerName1.Name = "xrHeaderLedgerName1";
            this.xrHeaderLedgerName1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderLedgerName1.StylePriority.UseBorderColor = false;
            this.xrHeaderLedgerName1.StylePriority.UseFont = false;
            this.xrHeaderLedgerName1.StylePriority.UsePadding = false;
            this.xrHeaderLedgerName1.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderAmountCurrentYear1
            // 
            this.xrHeaderAmountCurrentYear1.Name = "xrHeaderAmountCurrentYear1";
            this.xrHeaderAmountCurrentYear1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderAmountCurrentYear1.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrHeaderAmountCurrentYear1, "xrHeaderAmountCurrentYear1");
            // 
            // xrHeaderAmountPreviousYear1
            // 
            this.xrHeaderAmountPreviousYear1.Name = "xrHeaderAmountPreviousYear1";
            this.xrHeaderAmountPreviousYear1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderAmountPreviousYear1.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrHeaderAmountPreviousYear1, "xrHeaderAmountPreviousYear1");
            // 
            // xrTableLedger
            // 
            resources.ApplyResources(this.xrTableLedger, "xrTableLedger");
            this.xrTableLedger.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableLedger.Name = "xrTableLedger";
            this.xrTableLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 3, 3, 100F);
            this.xrTableLedger.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrRowData});
            this.xrTableLedger.StyleName = "styleRow";
            this.xrTableLedger.StylePriority.UseBorderColor = false;
            this.xrTableLedger.StylePriority.UseBorders = false;
            this.xrTableLedger.StylePriority.UseFont = false;
            this.xrTableLedger.StylePriority.UsePadding = false;
            this.xrTableLedger.StylePriority.UseTextAlignment = false;
            // 
            // xrRowData
            // 
            this.xrRowData.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLedgerCode,
            this.xrLedgerName,
            this.xrDebit,
            this.xrCredit});
            this.xrRowData.Name = "xrRowData";
            resources.ApplyResources(this.xrRowData, "xrRowData");
            // 
            // xrLedgerCode
            // 
            this.xrLedgerCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.LEDGER_CODE")});
            this.xrLedgerCode.Name = "xrLedgerCode";
            resources.ApplyResources(this.xrLedgerCode, "xrLedgerCode");
            // 
            // xrLedgerName
            // 
            this.xrLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.LEDGER_NAME")});
            this.xrLedgerName.Name = "xrLedgerName";
            this.xrLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLedgerName.StylePriority.UsePadding = false;
            this.xrLedgerName.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrLedgerName, "xrLedgerName");
            this.xrLedgerName.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrLedgerName_EvaluateBinding);
            // 
            // xrDebit
            // 
            this.xrDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.CURRENT_YEAR_BALANCE", "{0:n}")});
            this.xrDebit.Name = "xrDebit";
            this.xrDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrDebit, "xrDebit");
            this.xrDebit.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrDebit_EvaluateBinding);
            // 
            // xrCredit
            // 
            this.xrCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCE", "{0:n}")});
            this.xrCredit.Name = "xrCredit";
            this.xrCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCredit, "xrCredit");
            this.xrCredit.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrCredit_EvaluateBinding);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // calcClosingBalance
            // 
            this.calcClosingBalance.DataMember = "TrialBalance";
            this.calcClosingBalance.Name = "calcClosingBalance";
            // 
            // calTotalOpeningDebitAmount
            // 
            this.calTotalOpeningDebitAmount.DataMember = "TrialBalance";
            this.calTotalOpeningDebitAmount.Name = "calTotalOpeningDebitAmount";
            // 
            // calTotalOpeningCreditBalance
            // 
            this.calTotalOpeningCreditBalance.DataMember = "TrialBalance";
            this.calTotalOpeningCreditBalance.Name = "calTotalOpeningCreditBalance";
            // 
            // calCurrentTransDebit
            // 
            this.calCurrentTransDebit.DataMember = "TrialBalance";
            this.calCurrentTransDebit.Name = "calCurrentTransDebit";
            // 
            // calCurrentTransCredit
            // 
            this.calCurrentTransCredit.DataMember = "TrialBalance";
            this.calCurrentTransCredit.Name = "calCurrentTransCredit";
            // 
            // calTotalClosingDebit
            // 
            this.calTotalClosingDebit.DataMember = "TrialBalance";
            this.calTotalClosingDebit.Name = "calTotalClosingDebit";
            // 
            // calTotalClosingCredit
            // 
            this.calTotalClosingCredit.DataMember = "TrialBalance";
            this.calTotalClosingCredit.Name = "calTotalClosingCredit";
            // 
            // calGroupClosingBal
            // 
            this.calGroupClosingBal.DataMember = "TrialBalance";
            this.calGroupClosingBal.Name = "calGroupClosingBal";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableGrandTotal});
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTableGrandTotal
            // 
            resources.ApplyResources(this.xrTableGrandTotal, "xrTableGrandTotal");
            this.xrTableGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableGrandTotal.Name = "xrTableGrandTotal";
            this.xrTableGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 3, 3, 100F);
            this.xrTableGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrRowTotal,
            this.xrIERow1,
            this.xrIERow2,
            this.xrRowGrandTotal});
            this.xrTableGrandTotal.StyleName = "styleGroupRow";
            this.xrTableGrandTotal.StylePriority.UseBackColor = false;
            this.xrTableGrandTotal.StylePriority.UseBorderColor = false;
            this.xrTableGrandTotal.StylePriority.UseBorders = false;
            this.xrTableGrandTotal.StylePriority.UseFont = false;
            this.xrTableGrandTotal.StylePriority.UsePadding = false;
            this.xrTableGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrRowTotal
            // 
            this.xrRowTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrRowTotal.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTotal,
            this.xrTotalCYSum,
            this.xrTotalPYSum});
            this.xrRowTotal.Name = "xrRowTotal";
            this.xrRowTotal.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrRowTotal, "xrRowTotal");
            // 
            // xrTotal
            // 
            resources.ApplyResources(this.xrTotal, "xrTotal");
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.StylePriority.UseFont = false;
            this.xrTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTotalCYSum
            // 
            this.xrTotalCYSum.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.CURRENT_YEAR_BALANCE")});
            this.xrTotalCYSum.Name = "xrTotalCYSum";
            this.xrTotalCYSum.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTotalCYSum.Summary = xrSummary1;
            resources.ApplyResources(this.xrTotalCYSum, "xrTotalCYSum");
            // 
            // xrTotalPYSum
            // 
            this.xrTotalPYSum.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCE")});
            this.xrTotalPYSum.Name = "xrTotalPYSum";
            this.xrTotalPYSum.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTotalPYSum.Summary = xrSummary2;
            resources.ApplyResources(this.xrTotalPYSum, "xrTotalPYSum");
            // 
            // xrIERow1
            // 
            this.xrIERow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrIETitle1,
            this.xrCYIEAmount1,
            this.xrPYIEAmount1});
            this.xrIERow1.Name = "xrIERow1";
            resources.ApplyResources(this.xrIERow1, "xrIERow1");
            // 
            // xrIETitle1
            // 
            resources.ApplyResources(this.xrIETitle1, "xrIETitle1");
            this.xrIETitle1.Name = "xrIETitle1";
            this.xrIETitle1.StylePriority.UseFont = false;
            this.xrIETitle1.StylePriority.UseTextAlignment = false;
            // 
            // xrCYIEAmount1
            // 
            this.xrCYIEAmount1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.CURRENT_YEAR_BALANCE")});
            this.xrCYIEAmount1.Name = "xrCYIEAmount1";
            this.xrCYIEAmount1.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCYIEAmount1, "xrCYIEAmount1");
            this.xrCYIEAmount1.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrCYIEAmount1_EvaluateBinding);
            // 
            // xrPYIEAmount1
            // 
            this.xrPYIEAmount1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCE")});
            this.xrPYIEAmount1.Name = "xrPYIEAmount1";
            this.xrPYIEAmount1.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrPYIEAmount1, "xrPYIEAmount1");
            this.xrPYIEAmount1.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrPYIEAmount1_EvaluateBinding);
            // 
            // xrIERow2
            // 
            this.xrIERow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrIETitle2,
            this.xrCYIEAmount2,
            this.xrPYIEAmount2});
            this.xrIERow2.Name = "xrIERow2";
            resources.ApplyResources(this.xrIERow2, "xrIERow2");
            // 
            // xrIETitle2
            // 
            resources.ApplyResources(this.xrIETitle2, "xrIETitle2");
            this.xrIETitle2.Name = "xrIETitle2";
            this.xrIETitle2.StylePriority.UseFont = false;
            this.xrIETitle2.StylePriority.UseTextAlignment = false;
            // 
            // xrCYIEAmount2
            // 
            this.xrCYIEAmount2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.CURRENT_YEAR_BALANCE")});
            this.xrCYIEAmount2.Name = "xrCYIEAmount2";
            this.xrCYIEAmount2.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCYIEAmount2, "xrCYIEAmount2");
            this.xrCYIEAmount2.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrCYIEAmount2_EvaluateBinding);
            // 
            // xrPYIEAmount2
            // 
            this.xrPYIEAmount2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCE")});
            this.xrPYIEAmount2.Name = "xrPYIEAmount2";
            this.xrPYIEAmount2.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrPYIEAmount2, "xrPYIEAmount2");
            this.xrPYIEAmount2.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrPYIEAmount2_EvaluateBinding);
            // 
            // xrRowGrandTotal
            // 
            this.xrRowGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrRowGrandTotal.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotal,
            this.xrCYGrandTotal,
            this.xrPYGrandTotal});
            this.xrRowGrandTotal.Name = "xrRowGrandTotal";
            this.xrRowGrandTotal.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrRowGrandTotal, "xrRowGrandTotal");
            // 
            // xrGrandTotal
            // 
            resources.ApplyResources(this.xrGrandTotal, "xrGrandTotal");
            this.xrGrandTotal.Name = "xrGrandTotal";
            this.xrGrandTotal.StylePriority.UseFont = false;
            this.xrGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrCYGrandTotal
            // 
            this.xrCYGrandTotal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.CURRENT_YEAR_BALANCE")});
            resources.ApplyResources(this.xrCYGrandTotal, "xrCYGrandTotal");
            this.xrCYGrandTotal.Name = "xrCYGrandTotal";
            this.xrCYGrandTotal.StylePriority.UseFont = false;
            this.xrCYGrandTotal.StylePriority.UseTextAlignment = false;
            this.xrCYGrandTotal.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrCYGrandTotal_EvaluateBinding);
            // 
            // xrPYGrandTotal
            // 
            this.xrPYGrandTotal.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCE")});
            resources.ApplyResources(this.xrPYGrandTotal, "xrPYGrandTotal");
            this.xrPYGrandTotal.Name = "xrPYGrandTotal";
            this.xrPYGrandTotal.StylePriority.UseFont = false;
            this.xrPYGrandTotal.StylePriority.UseTextAlignment = false;
            this.xrPYGrandTotal.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrPYGrandTotal_EvaluateBinding);
            // 
            // GeneralateSubAnnualStatementAccounts
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calcClosingBalance,
            this.calTotalOpeningDebitAmount,
            this.calTotalOpeningCreditBalance,
            this.calCurrentTransDebit,
            this.calCurrentTransCredit,
            this.calTotalClosingDebit,
            this.calTotalClosingCredit,
            this.calGroupClosingBal});
            this.DataMember = "GENERALATE_REPORTS";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField calcClosingBalance;
        private DevExpress.XtraReports.UI.CalculatedField calTotalOpeningDebitAmount;
        private DevExpress.XtraReports.UI.CalculatedField calTotalOpeningCreditBalance;
        private DevExpress.XtraReports.UI.CalculatedField calCurrentTransDebit;
        private DevExpress.XtraReports.UI.CalculatedField calCurrentTransCredit;
        private DevExpress.XtraReports.UI.CalculatedField calTotalClosingDebit;
        private DevExpress.XtraReports.UI.CalculatedField calTotalClosingCredit;
        private DevExpress.XtraReports.UI.CalculatedField calGroupClosingBal;
        private DevExpress.XtraReports.UI.XRTable xrTableLedger;
        private DevExpress.XtraReports.UI.XRTableRow xrRowData;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrCredit;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrHeaderRow;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderAmountCurrentYear;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderAmountPreviousYear;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel xrReportTitle;
        private DevExpress.XtraReports.UI.XRTableRow xrHeaderRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderLedgerCode1;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderLedgerName1;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderAmountCurrentYear1;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderAmountPreviousYear1;
        private DevExpress.XtraReports.UI.XRTable xrTableGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrRowTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalCYSum;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalPYSum;
        private DevExpress.XtraReports.UI.XRTableRow xrIERow1;
        private DevExpress.XtraReports.UI.XRTableCell xrIETitle1;
        private DevExpress.XtraReports.UI.XRTableCell xrCYIEAmount1;
        private DevExpress.XtraReports.UI.XRTableCell xrPYIEAmount1;
        private DevExpress.XtraReports.UI.XRTableRow xrIERow2;
        private DevExpress.XtraReports.UI.XRTableCell xrIETitle2;
        private DevExpress.XtraReports.UI.XRTableCell xrCYIEAmount2;
        private DevExpress.XtraReports.UI.XRTableCell xrPYIEAmount2;
        private DevExpress.XtraReports.UI.XRTableRow xrRowGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrCYGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrPYGrandTotal;
        private DevExpress.XtraReports.UI.XRLabel xrReportDate;
    }
}
