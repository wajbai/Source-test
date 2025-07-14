namespace Bosco.Report.ReportObject
{
    partial class DayBook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DayBook));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCashBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrTableSource = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellCashBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCrdit = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting2 = new Bosco.Report.ReportSetting();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.calDebit = new DevExpress.XtraReports.UI.CalculatedField();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblAmountFilter = new DevExpress.XtraReports.UI.XRLabel();
            this.calCredit = new DevExpress.XtraReports.UI.CalculatedField();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
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
            this.xrTableCell1,
            this.xrTableCell3,
            this.xrCapCashBank,
            this.xrCapParticulars,
            this.xrTableCell4,
            this.xrCapDebit,
            this.xrCapCredit});
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
            // xrTableCell1
            // 
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBackColor = false;
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            // 
            // xrTableCell3
            // 
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBackColor = false;
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            // 
            // xrCapCashBank
            // 
            resources.ApplyResources(this.xrCapCashBank, "xrCapCashBank");
            this.xrCapCashBank.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCashBank.Name = "xrCapCashBank";
            this.xrCapCashBank.StylePriority.UseBackColor = false;
            this.xrCapCashBank.StylePriority.UseBorderColor = false;
            this.xrCapCashBank.StylePriority.UseBorders = false;
            // 
            // xrCapParticulars
            // 
            resources.ApplyResources(this.xrCapParticulars, "xrCapParticulars");
            this.xrCapParticulars.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapParticulars.Name = "xrCapParticulars";
            this.xrCapParticulars.StylePriority.UseBackColor = false;
            this.xrCapParticulars.StylePriority.UseBorderColor = false;
            this.xrCapParticulars.StylePriority.UseBorders = false;
            this.xrCapParticulars.StylePriority.UseFont = false;
            // 
            // xrTableCell4
            // 
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBackColor = false;
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
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
            this.xrDate,
            this.xrTableCell2,
            this.xrTableCell6,
            this.xrcellCashBank,
            this.xrtblLedger,
            this.xrTableCell5,
            this.xrDebit,
            this.xrCrdit});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrDate
            // 
            resources.ApplyResources(this.xrDate, "xrDate");
            this.xrDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.DATE", "{0:d}")});
            this.xrDate.Name = "xrDate";
            this.xrDate.StyleName = "styleDateInfo";
            this.xrDate.StylePriority.UseBorderColor = false;
            this.xrDate.StylePriority.UseBorders = false;
            this.xrDate.StylePriority.UseFont = false;
            this.xrDate.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.VOUCHER_NO")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.VOUCHER_TYPE_NAME")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCell6, "xrTableCell6");
            // 
            // xrcellCashBank
            // 
            resources.ApplyResources(this.xrcellCashBank, "xrcellCashBank");
            this.xrcellCashBank.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrcellCashBank.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.CASH_BANK")});
            this.xrcellCashBank.Name = "xrcellCashBank";
            this.xrcellCashBank.StylePriority.UseBorderColor = false;
            this.xrcellCashBank.StylePriority.UseBorders = false;
            this.xrcellCashBank.StylePriority.UseTextAlignment = false;
            // 
            // xrtblLedger
            // 
            resources.ApplyResources(this.xrtblLedger, "xrtblLedger");
            this.xrtblLedger.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrtblLedger.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.PARTICULARS")});
            this.xrtblLedger.Name = "xrtblLedger";
            this.xrtblLedger.StylePriority.UseBorderColor = false;
            this.xrtblLedger.StylePriority.UseBorders = false;
            this.xrtblLedger.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.NARRATION")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCell5, "xrTableCell5");
            // 
            // xrDebit
            // 
            resources.ApplyResources(this.xrDebit, "xrDebit");
            this.xrDebit.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.DEBIT", "{0:n}")});
            this.xrDebit.Name = "xrDebit";
            this.xrDebit.StylePriority.UseBorderColor = false;
            this.xrDebit.StylePriority.UseBorders = false;
            this.xrDebit.StylePriority.UseTextAlignment = false;
            // 
            // xrCrdit
            // 
            resources.ApplyResources(this.xrCrdit, "xrCrdit");
            this.xrCrdit.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrCrdit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.CREDIT", "{0:n}")});
            this.xrCrdit.Name = "xrCrdit";
            this.xrCrdit.StylePriority.UseBorderColor = false;
            this.xrCrdit.StylePriority.UseBorders = false;
            this.xrCrdit.StylePriority.UseTextAlignment = false;
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
            this.xrTableCell8,
            this.xrTableCell9});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrGrandTotal
            // 
            this.xrGrandTotal.Name = "xrGrandTotal";
            resources.ApplyResources(this.xrGrandTotal, "xrGrandTotal");
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.calDebit", "{0:n}")});
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            this.xrTableCell8.Summary = xrSummary1;
            resources.ApplyResources(this.xrTableCell8, "xrTableCell8");
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DAYBOOK.calCredit", "{0:n}")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            this.xrTableCell9.Summary = xrSummary2;
            resources.ApplyResources(this.xrTableCell9, "xrTableCell9");
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
            // DayBook
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calDebit,
            this.calCredit});
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCredit;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrTableSource;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrtblLedger;
        private DevExpress.XtraReports.UI.XRTableCell xrDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrCrdit;
        private ReportSetting reportSetting2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.CalculatedField calDebit;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.CalculatedField calCredit;
        private DevExpress.XtraReports.UI.XRLabel lblAmountFilter;
        private DevExpress.XtraReports.UI.XRTableCell xrcellCashBank;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCashBank;
    }
}
