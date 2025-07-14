namespace Bosco.Report.ReportObject
{
    partial class TDSLedgerWise
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TDSLedgerWise));
            this.PageHeaderLedgerwise = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xtHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xtTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPendingAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xtLedger = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPenAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.xtHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xtLedger});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // PageHeaderLedgerwise
            // 
            this.PageHeaderLedgerwise.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xtHeaderCaption});
            resources.ApplyResources(this.PageHeaderLedgerwise, "PageHeaderLedgerwise");
            this.PageHeaderLedgerwise.Name = "PageHeaderLedgerwise";
            // 
            // xtHeaderCaption
            // 
            resources.ApplyResources(this.xtHeaderCaption, "xtHeaderCaption");
            this.xtHeaderCaption.Name = "xtHeaderCaption";
            this.xtHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xtHeaderCaption.StyleName = "styleColumnHeader";
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xtTotal});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xtTotal
            // 
            resources.ApplyResources(this.xtTotal, "xtTotal");
            this.xtTotal.Name = "xtTotal";
            this.xtTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xtTotal.StyleName = "styleTotalRow";
            this.xtTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTotal,
            this.xrPendingAmount});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrTotal
            // 
            this.xrTotal.Name = "xrTotal";
            this.xrTotal.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTotal, "xrTotal");
            // 
            // xrPendingAmount
            // 
            this.xrPendingAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TDSNatureOfPayments.PENDING_AMOUNT")});
            this.xrPendingAmount.Name = "xrPendingAmount";
            this.xrPendingAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrPendingAmount.Summary = xrSummary1;
            resources.ApplyResources(this.xrPendingAmount, "xrPendingAmount");
            // 
            // xtLedger
            // 
            resources.ApplyResources(this.xtLedger, "xtLedger");
            this.xtLedger.Name = "xtLedger";
            this.xtLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xtLedger.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xtLedger.StylePriority.UseFont = false;
            this.xtLedger.StylePriority.UsePadding = false;
            this.xtLedger.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLedgerName,
            this.xrPenAmount});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrLedgerName
            // 
            this.xrLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Ledger.LEDGER_NAME")});
            this.xrLedgerName.Name = "xrLedgerName";
            resources.ApplyResources(this.xrLedgerName, "xrLedgerName");
            // 
            // xrPenAmount
            // 
            this.xrPenAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TDSNatureOfPayments.PENDING_AMOUNT", "{0:n}")});
            this.xrPenAmount.Name = "xrPenAmount";
            this.xrPenAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrPenAmount, "xrPenAmount");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TDSLedgerWise
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeaderLedgerwise,
            this.ReportFooter});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeaderLedgerwise, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xtHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeaderLedgerwise;
        private DevExpress.XtraReports.UI.XRTable xtHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTable xtLedger;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrPenAmount;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xtTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrPendingAmount;
        private ReportSetting reportSetting1;
    }
}
