namespace Bosco.Report.ReportObject
{
    partial class UcLedgerDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcLedgerDetail));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.grpHeaderLedgerGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblLedgerGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellLedgerGroup = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpFooterLedgerGroup = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTblSumLGTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTotalCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandSumProposedAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpHeaderLedgerName = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblLedgerName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSumProposedAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpFooterLedgerName = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblSumLGTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // PageHeader
            // 
            this.PageHeader.Expanded = false;
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportFooter
            // 
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // grpHeaderLedgerGroup
            // 
            this.grpHeaderLedgerGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblLedgerGroup});
            this.grpHeaderLedgerGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_GROUP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpHeaderLedgerGroup, "grpHeaderLedgerGroup");
            this.grpHeaderLedgerGroup.Level = 1;
            this.grpHeaderLedgerGroup.Name = "grpHeaderLedgerGroup";
            // 
            // xrtblLedgerGroup
            // 
            resources.ApplyResources(this.xrtblLedgerGroup, "xrtblLedgerGroup");
            this.xrtblLedgerGroup.Name = "xrtblLedgerGroup";
            this.xrtblLedgerGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblLedgerGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrtblLedgerGroup.StylePriority.UseBackColor = false;
            this.xrtblLedgerGroup.StylePriority.UsePadding = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellLedgerGroup});
            this.xrTableRow8.Name = "xrTableRow8";
            resources.ApplyResources(this.xrTableRow8, "xrTableRow8");
            // 
            // xrcellLedgerGroup
            // 
            resources.ApplyResources(this.xrcellLedgerGroup, "xrcellLedgerGroup");
            this.xrcellLedgerGroup.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BudgetCostCentre.LEDGER_GROUP")});
            this.xrcellLedgerGroup.Name = "xrcellLedgerGroup";
            this.xrcellLedgerGroup.StylePriority.UseBackColor = false;
            this.xrcellLedgerGroup.StylePriority.UseBorderColor = false;
            this.xrcellLedgerGroup.StylePriority.UseFont = false;
            this.xrcellLedgerGroup.StylePriority.UseTextAlignment = false;
            // 
            // grpFooterLedgerGroup
            // 
            this.grpFooterLedgerGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblSumLGTotal});
            resources.ApplyResources(this.grpFooterLedgerGroup, "grpFooterLedgerGroup");
            this.grpFooterLedgerGroup.Level = 1;
            this.grpFooterLedgerGroup.Name = "grpFooterLedgerGroup";
            // 
            // xrTblSumLGTotal
            // 
            resources.ApplyResources(this.xrTblSumLGTotal, "xrTblSumLGTotal");
            this.xrTblSumLGTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblSumLGTotal.Name = "xrTblSumLGTotal";
            this.xrTblSumLGTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblSumLGTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTblSumLGTotal.StyleName = "styleGroupRow";
            this.xrTblSumLGTotal.StylePriority.UseBackColor = false;
            this.xrTblSumLGTotal.StylePriority.UseBorderColor = false;
            this.xrTblSumLGTotal.StylePriority.UseBorders = false;
            this.xrTblSumLGTotal.StylePriority.UseFont = false;
            this.xrTblSumLGTotal.StylePriority.UsePadding = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTotalCaption,
            this.xrGrandSumProposedAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrTotalCaption
            // 
            resources.ApplyResources(this.xrTotalCaption, "xrTotalCaption");
            this.xrTotalCaption.Name = "xrTotalCaption";
            this.xrTotalCaption.StylePriority.UseBackColor = false;
            this.xrTotalCaption.StylePriority.UseBorderColor = false;
            this.xrTotalCaption.StylePriority.UseFont = false;
            this.xrTotalCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrGrandSumProposedAmount
            // 
            resources.ApplyResources(this.xrGrandSumProposedAmount, "xrGrandSumProposedAmount");
            this.xrGrandSumProposedAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BudgetCostCentre.PROPOSED_AMOUNT")});
            this.xrGrandSumProposedAmount.Name = "xrGrandSumProposedAmount";
            this.xrGrandSumProposedAmount.StylePriority.UseBackColor = false;
            this.xrGrandSumProposedAmount.StylePriority.UseBorderColor = false;
            this.xrGrandSumProposedAmount.StylePriority.UseFont = false;
            this.xrGrandSumProposedAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrGrandSumProposedAmount.Summary = xrSummary1;
            // 
            // grpHeaderLedgerName
            // 
            this.grpHeaderLedgerName.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblLedgerName});
            this.grpHeaderLedgerName.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpHeaderLedgerName, "grpHeaderLedgerName");
            this.grpHeaderLedgerName.Name = "grpHeaderLedgerName";
            // 
            // xrtblLedgerName
            // 
            resources.ApplyResources(this.xrtblLedgerName, "xrtblLedgerName");
            this.xrtblLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblLedgerName.Name = "xrtblLedgerName";
            this.xrtblLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblLedgerName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblLedgerName.StyleName = "styleGroupRow";
            this.xrtblLedgerName.StylePriority.UseBackColor = false;
            this.xrtblLedgerName.StylePriority.UseBorderColor = false;
            this.xrtblLedgerName.StylePriority.UseBorders = false;
            this.xrtblLedgerName.StylePriority.UseFont = false;
            this.xrtblLedgerName.StylePriority.UsePadding = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLedgerName,
            this.xrSumProposedAmount});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrLedgerName
            // 
            this.xrLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BudgetCostCentre.LEDGER_NAME")});
            resources.ApplyResources(this.xrLedgerName, "xrLedgerName");
            this.xrLedgerName.Name = "xrLedgerName";
            this.xrLedgerName.StylePriority.UseFont = false;
            // 
            // xrSumProposedAmount
            // 
            this.xrSumProposedAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BudgetCostCentre.PROPOSED_AMOUNT")});
            resources.ApplyResources(this.xrSumProposedAmount, "xrSumProposedAmount");
            this.xrSumProposedAmount.Name = "xrSumProposedAmount";
            this.xrSumProposedAmount.StylePriority.UseFont = false;
            this.xrSumProposedAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrSumProposedAmount.Summary = xrSummary2;
            // 
            // grpFooterLedgerName
            // 
            this.grpFooterLedgerName.Expanded = false;
            resources.ApplyResources(this.grpFooterLedgerName, "grpFooterLedgerName");
            this.grpFooterLedgerName.Name = "grpFooterLedgerName";
            // 
            // UcLedgerDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grpHeaderLedgerGroup,
            this.grpFooterLedgerGroup,
            this.grpHeaderLedgerName,
            this.grpFooterLedgerName});
            this.DataMember = "BudgetCostCentre";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpFooterLedgerName, 0);
            this.Controls.SetChildIndex(this.grpHeaderLedgerName, 0);
            this.Controls.SetChildIndex(this.grpFooterLedgerGroup, 0);
            this.Controls.SetChildIndex(this.grpHeaderLedgerGroup, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblSumLGTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedgerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderLedgerGroup;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooterLedgerGroup;
        private DevExpress.XtraReports.UI.XRTable xrtblLedgerGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        private DevExpress.XtraReports.UI.XRTableCell xrcellLedgerGroup;
        private DevExpress.XtraReports.UI.XRTable xrTblSumLGTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandSumProposedAmount;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderLedgerName;
        private DevExpress.XtraReports.UI.XRTable xrtblLedgerName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrSumProposedAmount;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooterLedgerName;
    }
}
