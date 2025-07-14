namespace Bosco.Report.ReportObject
{
    partial class BankActualStatement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankActualStatement));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapRefNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrTableSource = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellRefNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting2 = new Bosco.Report.ReportSetting();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandSumDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandSumCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellGrandBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.calDebit = new DevExpress.XtraReports.UI.CalculatedField();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblAmountFilter = new DevExpress.XtraReports.UI.XRLabel();
            this.calCredit = new DevExpress.XtraReports.UI.CalculatedField();
            this.calBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.grpHeaderOpeningBalance = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblOpeningBalance = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellGrpOPCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellGrpOPCaption1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellGrpOPDr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellGrpOPCr = new DevExpress.XtraReports.UI.XRTableCell();
            this.praOpeningBalance = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrcellGrpOPBalance = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblOpeningBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableSource});
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
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.StyleName = "styleGroupRow";
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapDate,
            this.xrCapNarration,
            this.xrCapRefNo,
            this.xrCapDebit,
            this.xrCapCredit,
            this.xrCapBalance});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapDate
            // 
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            this.xrCapDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.StyleName = "styleDateInfo";
            this.xrCapDate.StylePriority.UseBackColor = false;
            this.xrCapDate.StylePriority.UseBorderColor = false;
            this.xrCapDate.StylePriority.UseBorders = false;
            this.xrCapDate.StylePriority.UseFont = false;
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            // 
            // xrCapNarration
            // 
            resources.ApplyResources(this.xrCapNarration, "xrCapNarration");
            this.xrCapNarration.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapNarration.Name = "xrCapNarration";
            this.xrCapNarration.StylePriority.UseBackColor = false;
            this.xrCapNarration.StylePriority.UseBorderColor = false;
            this.xrCapNarration.StylePriority.UseBorders = false;
            this.xrCapNarration.StylePriority.UseFont = false;
            // 
            // xrCapRefNo
            // 
            resources.ApplyResources(this.xrCapRefNo, "xrCapRefNo");
            this.xrCapRefNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapRefNo.Name = "xrCapRefNo";
            this.xrCapRefNo.StylePriority.UseBackColor = false;
            this.xrCapRefNo.StylePriority.UseBorderColor = false;
            this.xrCapRefNo.StylePriority.UseBorders = false;
            this.xrCapRefNo.StylePriority.UseTextAlignment = false;
            // 
            // xrCapDebit
            // 
            resources.ApplyResources(this.xrCapDebit, "xrCapDebit");
            this.xrCapDebit.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapDebit.Name = "xrCapDebit";
            this.xrCapDebit.StylePriority.UseBackColor = false;
            this.xrCapDebit.StylePriority.UseBorderColor = false;
            this.xrCapDebit.StylePriority.UseBorders = false;
            this.xrCapDebit.StylePriority.UseFont = false;
            this.xrCapDebit.StylePriority.UseTextAlignment = false;
            // 
            // xrCapCredit
            // 
            resources.ApplyResources(this.xrCapCredit, "xrCapCredit");
            this.xrCapCredit.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCredit.Name = "xrCapCredit";
            this.xrCapCredit.StylePriority.UseBackColor = false;
            this.xrCapCredit.StylePriority.UseBorderColor = false;
            this.xrCapCredit.StylePriority.UseBorders = false;
            this.xrCapCredit.StylePriority.UseFont = false;
            this.xrCapCredit.StylePriority.UseTextAlignment = false;
            // 
            // xrCapBalance
            // 
            resources.ApplyResources(this.xrCapBalance, "xrCapBalance");
            this.xrCapBalance.Name = "xrCapBalance";
            this.xrCapBalance.StylePriority.UseBackColor = false;
            this.xrCapBalance.StylePriority.UseBorderColor = false;
            this.xrCapBalance.StylePriority.UseTextAlignment = false;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrTableSource
            // 
            resources.ApplyResources(this.xrTableSource, "xrTableSource");
            this.xrTableSource.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableSource.Name = "xrTableSource";
            this.xrTableSource.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableSource.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTableSource.StyleName = "styleRow";
            this.xrTableSource.StylePriority.UseBorderColor = false;
            this.xrTableSource.StylePriority.UseBorders = false;
            this.xrTableSource.StylePriority.UseFont = false;
            this.xrTableSource.StylePriority.UsePadding = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellDate,
            this.xrCellNarration,
            this.xrcellRefNo,
            this.xrcellDebit,
            this.xrcellCredit,
            this.xrCellBalance});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrCellDate
            // 
            resources.ApplyResources(this.xrCellDate, "xrCellDate");
            this.xrCellDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrCellDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.DATE", "{0:d}")});
            this.xrCellDate.Name = "xrCellDate";
            this.xrCellDate.StyleName = "styleDateInfo";
            this.xrCellDate.StylePriority.UseBorderColor = false;
            this.xrCellDate.StylePriority.UseBorders = false;
            this.xrCellDate.StylePriority.UseTextAlignment = false;
            // 
            // xrCellNarration
            // 
            resources.ApplyResources(this.xrCellNarration, "xrCellNarration");
            this.xrCellNarration.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrCellNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.NARRATION")});
            this.xrCellNarration.Name = "xrCellNarration";
            this.xrCellNarration.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellNarration.StylePriority.UseBorderColor = false;
            this.xrCellNarration.StylePriority.UseBorders = false;
            this.xrCellNarration.StylePriority.UsePadding = false;
            this.xrCellNarration.StylePriority.UseTextAlignment = false;
            // 
            // xrcellRefNo
            // 
            this.xrcellRefNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.CHEQUE_NO")});
            this.xrcellRefNo.Name = "xrcellRefNo";
            this.xrcellRefNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellRefNo.StylePriority.UsePadding = false;
            this.xrcellRefNo.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrcellRefNo, "xrcellRefNo");
            // 
            // xrcellDebit
            // 
            resources.ApplyResources(this.xrcellDebit, "xrcellDebit");
            this.xrcellDebit.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrcellDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.DEBIT", "{0:n}")});
            this.xrcellDebit.Name = "xrcellDebit";
            this.xrcellDebit.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellDebit.StylePriority.UseBorderColor = false;
            this.xrcellDebit.StylePriority.UseBorders = false;
            this.xrcellDebit.StylePriority.UsePadding = false;
            this.xrcellDebit.StylePriority.UseTextAlignment = false;
            this.xrcellDebit.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrcellDebit_BeforePrint);
            // 
            // xrcellCredit
            // 
            resources.ApplyResources(this.xrcellCredit, "xrcellCredit");
            this.xrcellCredit.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrcellCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.CREDIT", "{0:n}")});
            this.xrcellCredit.Name = "xrcellCredit";
            this.xrcellCredit.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellCredit.StylePriority.UseBorderColor = false;
            this.xrcellCredit.StylePriority.UseBorders = false;
            this.xrcellCredit.StylePriority.UsePadding = false;
            this.xrcellCredit.StylePriority.UseTextAlignment = false;
            this.xrcellCredit.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrcellCredit_BeforePrint);
            // 
            // xrCellBalance
            // 
            this.xrCellBalance.Name = "xrCellBalance";
            this.xrCellBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellBalance.StylePriority.UsePadding = false;
            this.xrCellBalance.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrCellBalance.Summary = xrSummary1;
            resources.ApplyResources(this.xrCellBalance, "xrCellBalance");
            this.xrCellBalance.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCellBalance_BeforePrint);
            // 
            // reportSetting2
            // 
            this.reportSetting2.DataSetName = "ReportSetting";
            this.reportSetting2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrtblGrandTotal
            // 
            resources.ApplyResources(this.xrtblGrandTotal, "xrtblGrandTotal");
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblGrandTotal.StyleName = "styleGroupRow";
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotal,
            this.xrGrandSumDebit,
            this.xrGrandSumCredit,
            this.xrCellGrandBalance});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrGrandTotal
            // 
            this.xrGrandTotal.Name = "xrGrandTotal";
            resources.ApplyResources(this.xrGrandTotal, "xrGrandTotal");
            // 
            // xrGrandSumDebit
            // 
            this.xrGrandSumDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.calDebit", "{0:n}")});
            this.xrGrandSumDebit.Name = "xrGrandSumDebit";
            this.xrGrandSumDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            this.xrGrandSumDebit.Summary = xrSummary2;
            resources.ApplyResources(this.xrGrandSumDebit, "xrGrandSumDebit");
            // 
            // xrGrandSumCredit
            // 
            this.xrGrandSumCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.calCredit", "{0:n}")});
            this.xrGrandSumCredit.Name = "xrGrandSumCredit";
            this.xrGrandSumCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.IgnoreNullValues = true;
            this.xrGrandSumCredit.Summary = xrSummary3;
            resources.ApplyResources(this.xrGrandSumCredit, "xrGrandSumCredit");
            // 
            // xrCellGrandBalance
            // 
            this.xrCellGrandBalance.Name = "xrCellGrandBalance";
            this.xrCellGrandBalance.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCellGrandBalance, "xrCellGrandBalance");
            // 
            // calDebit
            // 
            this.calDebit.DataMember = "DAYBOOK";
            this.calDebit.Expression = "Iif(IsNullOrEmpty([].Sum([DEBIT])), 0, [].Sum([DEBIT]))";
            this.calDebit.Name = "calDebit";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblAmountFilter,
            this.xrtblGrandTotal});
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
            // calCredit
            // 
            this.calCredit.DataMember = "DAYBOOK";
            this.calCredit.Expression = "Iif(IsNullOrEmpty([].Sum([CREDIT])), 0, [].Sum([CREDIT]))";
            this.calCredit.Name = "calCredit";
            // 
            // calBalance
            // 
            this.calBalance.DataMember = "DAYBOOK";
            this.calBalance.Expression = "Iif(IsNullOrEmpty([].Sum([CREDIT])), 0, [].Sum([CREDIT])) - Iif(IsNullOrEmpty([]." +
                "Sum([DEBIT])), 0, [].Sum([DEBIT]))";
            this.calBalance.Name = "calBalance";
            // 
            // grpHeaderOpeningBalance
            // 
            this.grpHeaderOpeningBalance.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblOpeningBalance});
            resources.ApplyResources(this.grpHeaderOpeningBalance, "grpHeaderOpeningBalance");
            this.grpHeaderOpeningBalance.Name = "grpHeaderOpeningBalance";
            // 
            // xrtblOpeningBalance
            // 
            resources.ApplyResources(this.xrtblOpeningBalance, "xrtblOpeningBalance");
            this.xrtblOpeningBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblOpeningBalance.Name = "xrtblOpeningBalance";
            this.xrtblOpeningBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblOpeningBalance.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrtblOpeningBalance.StyleName = "styleGroupRow";
            this.xrtblOpeningBalance.StylePriority.UseBackColor = false;
            this.xrtblOpeningBalance.StylePriority.UseBorderColor = false;
            this.xrtblOpeningBalance.StylePriority.UseBorders = false;
            this.xrtblOpeningBalance.StylePriority.UseFont = false;
            this.xrtblOpeningBalance.StylePriority.UsePadding = false;
            this.xrtblOpeningBalance.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellGrpOPCaption,
            this.xrcellGrpOPCaption1,
            this.xrcellGrpOPDr,
            this.xrcellGrpOPCr,
            this.xrcellGrpOPBalance});
            this.xrTableRow7.Name = "xrTableRow7";
            resources.ApplyResources(this.xrTableRow7, "xrTableRow7");
            // 
            // xrcellGrpOPCaption
            // 
            resources.ApplyResources(this.xrcellGrpOPCaption, "xrcellGrpOPCaption");
            this.xrcellGrpOPCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrcellGrpOPCaption.Name = "xrcellGrpOPCaption";
            this.xrcellGrpOPCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellGrpOPCaption.StylePriority.UseBackColor = false;
            this.xrcellGrpOPCaption.StylePriority.UseBorderColor = false;
            this.xrcellGrpOPCaption.StylePriority.UseBorders = false;
            this.xrcellGrpOPCaption.StylePriority.UseFont = false;
            this.xrcellGrpOPCaption.StylePriority.UsePadding = false;
            this.xrcellGrpOPCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrcellGrpOPCaption1
            // 
            resources.ApplyResources(this.xrcellGrpOPCaption1, "xrcellGrpOPCaption1");
            this.xrcellGrpOPCaption1.Name = "xrcellGrpOPCaption1";
            this.xrcellGrpOPCaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellGrpOPCaption1.StylePriority.UseBackColor = false;
            this.xrcellGrpOPCaption1.StylePriority.UseBorderColor = false;
            this.xrcellGrpOPCaption1.StylePriority.UsePadding = false;
            // 
            // xrcellGrpOPDr
            // 
            resources.ApplyResources(this.xrcellGrpOPDr, "xrcellGrpOPDr");
            this.xrcellGrpOPDr.Name = "xrcellGrpOPDr";
            this.xrcellGrpOPDr.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellGrpOPDr.StylePriority.UseBackColor = false;
            this.xrcellGrpOPDr.StylePriority.UseBorderColor = false;
            this.xrcellGrpOPDr.StylePriority.UsePadding = false;
            // 
            // xrcellGrpOPCr
            // 
            resources.ApplyResources(this.xrcellGrpOPCr, "xrcellGrpOPCr");
            this.xrcellGrpOPCr.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrcellGrpOPCr.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.praOpeningBalance, "Text", "{0:n}")});
            this.xrcellGrpOPCr.Name = "xrcellGrpOPCr";
            this.xrcellGrpOPCr.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellGrpOPCr.StylePriority.UseBackColor = false;
            this.xrcellGrpOPCr.StylePriority.UseBorderColor = false;
            this.xrcellGrpOPCr.StylePriority.UseBorders = false;
            this.xrcellGrpOPCr.StylePriority.UseFont = false;
            this.xrcellGrpOPCr.StylePriority.UsePadding = false;
            this.xrcellGrpOPCr.StylePriority.UseTextAlignment = false;
            // 
            // praOpeningBalance
            // 
            resources.ApplyResources(this.praOpeningBalance, "praOpeningBalance");
            this.praOpeningBalance.Name = "praOpeningBalance";
            this.praOpeningBalance.Type = typeof(double);
            this.praOpeningBalance.ValueInfo = "0";
            this.praOpeningBalance.Visible = false;
            // 
            // xrcellGrpOPBalance
            // 
            resources.ApplyResources(this.xrcellGrpOPBalance, "xrcellGrpOPBalance");
            this.xrcellGrpOPBalance.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrcellGrpOPBalance.Name = "xrcellGrpOPBalance";
            this.xrcellGrpOPBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellGrpOPBalance.StylePriority.UseBackColor = false;
            this.xrcellGrpOPBalance.StylePriority.UseBorderColor = false;
            this.xrcellGrpOPBalance.StylePriority.UseBorders = false;
            this.xrcellGrpOPBalance.StylePriority.UsePadding = false;
            // 
            // BankActualStatement
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grpHeaderOpeningBalance});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calDebit,
            this.calCredit,
            this.calBalance});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.praOpeningBalance});
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpHeaderOpeningBalance, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblOpeningBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCredit;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrTableSource;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrCellDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCellNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrcellDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrcellCredit;
        private ReportSetting reportSetting2;
        private DevExpress.XtraReports.UI.XRTableCell xrCapRefNo;
        private DevExpress.XtraReports.UI.XRTableCell xrcellRefNo;
        private DevExpress.XtraReports.UI.CalculatedField calDebit;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandSumDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandSumCredit;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.CalculatedField calCredit;
        private DevExpress.XtraReports.UI.XRLabel lblAmountFilter;
        private DevExpress.XtraReports.UI.XRTableCell xrCellBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrCapBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrCellGrandBalance;
        private DevExpress.XtraReports.UI.CalculatedField calBalance;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderOpeningBalance;
        private DevExpress.XtraReports.UI.XRTable xrtblOpeningBalance;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        private DevExpress.XtraReports.UI.XRTableCell xrcellGrpOPCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrcellGrpOPCr;
        private DevExpress.XtraReports.UI.XRTableCell xrcellGrpOPBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrcellGrpOPDr;
        private DevExpress.XtraReports.Parameters.Parameter praOpeningBalance;
        private DevExpress.XtraReports.UI.XRTableCell xrcellGrpOPCaption1;
    }
}
