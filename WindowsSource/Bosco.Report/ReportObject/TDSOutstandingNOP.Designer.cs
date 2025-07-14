namespace Bosco.Report.ReportObject
{
    partial class TDSOutstandingNOP
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
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TDSOutstandingNOP));
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xtHeaderCap = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCompany = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNonCompany = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalPending = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xtGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNOPCompanyAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNOPNonCompanyAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNOPTotalAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xtNOP = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrNop = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCompanyAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNonCompanyAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.xtHeaderCap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtNOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xtNOP});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xtHeaderCap});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // xtHeaderCap
            // 
            resources.ApplyResources(this.xtHeaderCap, "xtHeaderCap");
            this.xtHeaderCap.Name = "xtHeaderCap";
            this.xtHeaderCap.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xtHeaderCap.StyleName = "styleColumnHeader";
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrCompany,
            this.xrNonCompany,
            this.xrTotalPending});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            // 
            // xrCompany
            // 
            this.xrCompany.Name = "xrCompany";
            this.xrCompany.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCompany, "xrCompany");
            // 
            // xrNonCompany
            // 
            this.xrNonCompany.Name = "xrNonCompany";
            this.xrNonCompany.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrNonCompany, "xrNonCompany");
            // 
            // xrTotalPending
            // 
            this.xrTotalPending.Name = "xrTotalPending";
            this.xrTotalPending.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTotalPending, "xrTotalPending");
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xtGrandTotal});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.StyleName = "styleTotalRow";
            // 
            // xtGrandTotal
            // 
            resources.ApplyResources(this.xtGrandTotal, "xtGrandTotal");
            this.xtGrandTotal.Name = "xtGrandTotal";
            this.xtGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTotal,
            this.xrNOPCompanyAmt,
            this.xrNOPNonCompanyAmt,
            this.xrNOPTotalAmt});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrTotal
            // 
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTotal, "xrTotal");
            // 
            // xrNOPCompanyAmt
            // 
            this.xrNOPCompanyAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TDSNatureOfPayments.COMPANY")});
            this.xrNOPCompanyAmt.Name = "xrNOPCompanyAmt";
            this.xrNOPCompanyAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrNOPCompanyAmt.Summary = xrSummary1;
            resources.ApplyResources(this.xrNOPCompanyAmt, "xrNOPCompanyAmt");
            // 
            // xrNOPNonCompanyAmt
            // 
            this.xrNOPNonCompanyAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TDSNatureOfPayments.NON_COMPANY")});
            this.xrNOPNonCompanyAmt.Name = "xrNOPNonCompanyAmt";
            this.xrNOPNonCompanyAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrNOPNonCompanyAmt.Summary = xrSummary2;
            resources.ApplyResources(this.xrNOPNonCompanyAmt, "xrNOPNonCompanyAmt");
            // 
            // xrNOPTotalAmt
            // 
            this.xrNOPTotalAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TDSNatureOfPayments.TOTAL_AMOUNT")});
            this.xrNOPTotalAmt.Name = "xrNOPTotalAmt";
            this.xrNOPTotalAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrNOPTotalAmt.Summary = xrSummary3;
            resources.ApplyResources(this.xrNOPTotalAmt, "xrNOPTotalAmt");
            // 
            // xtNOP
            // 
            this.xtNOP.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xtNOP, "xtNOP");
            this.xtNOP.Name = "xtNOP";
            this.xtNOP.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xtNOP.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xtNOP.StylePriority.UseBorderColor = false;
            this.xtNOP.StylePriority.UseBorders = false;
            this.xtNOP.StylePriority.UseFont = false;
            this.xtNOP.StylePriority.UsePadding = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrNop,
            this.xrCompanyAmt,
            this.xrNonCompanyAmt,
            this.xrTotalAmount});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrNop
            // 
            this.xrNop.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TDSNatureOfPayments.NAME")});
            this.xrNop.Name = "xrNop";
            this.xrNop.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrNop, "xrNop");
            // 
            // xrCompanyAmt
            // 
            this.xrCompanyAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TDSNatureOfPayments.COMPANY", "{0:n}")});
            this.xrCompanyAmt.Name = "xrCompanyAmt";
            this.xrCompanyAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCompanyAmt, "xrCompanyAmt");
            // 
            // xrNonCompanyAmt
            // 
            this.xrNonCompanyAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TDSNatureOfPayments.NON_COMPANY", "{0:n}")});
            this.xrNonCompanyAmt.Name = "xrNonCompanyAmt";
            this.xrNonCompanyAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrNonCompanyAmt, "xrNonCompanyAmt");
            // 
            // xrTotalAmount
            // 
            this.xrTotalAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TDSNatureOfPayments.TOTAL_AMOUNT", "{0:n}")});
            this.xrTotalAmount.Name = "xrTotalAmount";
            this.xrTotalAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTotalAmount, "xrTotalAmount");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TDSOutstandingNOP
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xtHeaderCap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtNOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xtHeaderCap;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrCompany;
        private DevExpress.XtraReports.UI.XRTableCell xrNonCompany;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalPending;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xtNOP;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrNop;
        private DevExpress.XtraReports.UI.XRTableCell xrCompanyAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrNonCompanyAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalAmount;
        private DevExpress.XtraReports.UI.XRTable xtGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrNOPCompanyAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrNOPNonCompanyAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrNOPTotalAmt;

    }
}
