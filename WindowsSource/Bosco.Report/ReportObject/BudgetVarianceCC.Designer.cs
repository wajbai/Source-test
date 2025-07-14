namespace Bosco.Report.ReportObject
{
    partial class BudgetVarianceCC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BudgetVarianceCC));
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrRowCaption = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCCName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblBreak = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTblCC = new DevExpress.XtraReports.UI.XRTable();
            this.xrRowCC = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellCCAbbrevation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellCCName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellCCApproved = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellCCActual = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellCCVariance = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellCCPercentage = new DevExpress.XtraReports.UI.XRTableCell();
            this.grHeaderCCCategory = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.grFooterCCCategory = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrtblCCCName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellCCCEmpty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCCCName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblCC});
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
            this.xrtblHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrRowCaption});
            this.xrtblHeaderCaption.StyleName = "styleGroupRow";
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrRowCaption
            // 
            this.xrRowCaption.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapDate,
            this.xrCapCCName,
            this.xrCapDebit,
            this.xrCapCredit});
            this.xrRowCaption.Name = "xrRowCaption";
            resources.ApplyResources(this.xrRowCaption, "xrRowCaption");
            // 
            // xrCapDate
            // 
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            this.xrCapDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapDate.CanGrow = false;
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapDate.StyleName = "styleDateInfo";
            this.xrCapDate.StylePriority.UseBackColor = false;
            this.xrCapDate.StylePriority.UseBorderColor = false;
            this.xrCapDate.StylePriority.UseBorders = false;
            this.xrCapDate.StylePriority.UseFont = false;
            this.xrCapDate.StylePriority.UsePadding = false;
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            // 
            // xrCapCCName
            // 
            resources.ApplyResources(this.xrCapCCName, "xrCapCCName");
            this.xrCapCCName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCCName.CanGrow = false;
            this.xrCapCCName.Name = "xrCapCCName";
            this.xrCapCCName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapCCName.StylePriority.UseBackColor = false;
            this.xrCapCCName.StylePriority.UseBorderColor = false;
            this.xrCapCCName.StylePriority.UseBorders = false;
            this.xrCapCCName.StylePriority.UseFont = false;
            this.xrCapCCName.StylePriority.UsePadding = false;
            // 
            // xrCapDebit
            // 
            resources.ApplyResources(this.xrCapDebit, "xrCapDebit");
            this.xrCapDebit.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapDebit.CanGrow = false;
            this.xrCapDebit.Name = "xrCapDebit";
            this.xrCapDebit.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapDebit.StylePriority.UseBackColor = false;
            this.xrCapDebit.StylePriority.UseBorderColor = false;
            this.xrCapDebit.StylePriority.UseBorders = false;
            this.xrCapDebit.StylePriority.UseFont = false;
            this.xrCapDebit.StylePriority.UsePadding = false;
            this.xrCapDebit.StylePriority.UseTextAlignment = false;
            // 
            // xrCapCredit
            // 
            resources.ApplyResources(this.xrCapCredit, "xrCapCredit");
            this.xrCapCredit.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCredit.CanGrow = false;
            this.xrCapCredit.Name = "xrCapCredit";
            this.xrCapCredit.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapCredit.StylePriority.UseBackColor = false;
            this.xrCapCredit.StylePriority.UseBorderColor = false;
            this.xrCapCredit.StylePriority.UseBorders = false;
            this.xrCapCredit.StylePriority.UseFont = false;
            this.xrCapCredit.StylePriority.UsePadding = false;
            this.xrCapCredit.StylePriority.UseTextAlignment = false;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblBreak});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblBreak
            // 
            resources.ApplyResources(this.lblBreak, "lblBreak");
            this.lblBreak.Name = "lblBreak";
            this.lblBreak.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // xrTblCC
            // 
            resources.ApplyResources(this.xrTblCC, "xrTblCC");
            this.xrTblCC.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblCC.Name = "xrTblCC";
            this.xrTblCC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTblCC.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrRowCC});
            this.xrTblCC.StyleName = "styleRow";
            this.xrTblCC.StylePriority.UseBackColor = false;
            this.xrTblCC.StylePriority.UseBorderColor = false;
            this.xrTblCC.StylePriority.UseBorders = false;
            this.xrTblCC.StylePriority.UseFont = false;
            this.xrTblCC.StylePriority.UsePadding = false;
            this.xrTblCC.StylePriority.UseTextAlignment = false;
            // 
            // xrRowCC
            // 
            this.xrRowCC.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellCCAbbrevation,
            this.xrcellCCName,
            this.xrCellCCApproved,
            this.xrcellCCActual,
            this.xrCellCCVariance,
            this.xrCellCCPercentage});
            this.xrRowCC.Name = "xrRowCC";
            resources.ApplyResources(this.xrRowCC, "xrRowCC");
            // 
            // xrcellCCAbbrevation
            // 
            this.xrcellCCAbbrevation.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "BUDGETVARIANCE.LEDGER_ID")});
            resources.ApplyResources(this.xrcellCCAbbrevation, "xrcellCCAbbrevation");
            this.xrcellCCAbbrevation.Name = "xrcellCCAbbrevation";
            this.xrcellCCAbbrevation.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellCCAbbrevation.ProcessDuplicates = DevExpress.XtraReports.UI.ValueSuppressType.MergeByTag;
            this.xrcellCCAbbrevation.StylePriority.UseFont = false;
            this.xrcellCCAbbrevation.StylePriority.UsePadding = false;
            //this.xrcellCCAbbrevation.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrcellCCAbbrevation_BeforePrint);
            // 
            // xrcellCCName
            // 
            this.xrcellCCName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.COST_CENTRE_NAME")});
            resources.ApplyResources(this.xrcellCCName, "xrcellCCName");
            this.xrcellCCName.Name = "xrcellCCName";
            this.xrcellCCName.StylePriority.UseFont = false;
            this.xrcellCCName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrcellCCName_BeforePrint);
            // 
            // xrCellCCApproved
            // 
            this.xrCellCCApproved.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.APPROVED_AMOUNT", "{0:n}")});
            resources.ApplyResources(this.xrCellCCApproved, "xrCellCCApproved");
            this.xrCellCCApproved.Name = "xrCellCCApproved";
            this.xrCellCCApproved.StylePriority.UseFont = false;
            this.xrCellCCApproved.StylePriority.UseTextAlignment = false;
            // 
            // xrcellCCActual
            // 
            this.xrcellCCActual.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.ACTUAL_AMOUNT", "{0:n}")});
            resources.ApplyResources(this.xrcellCCActual, "xrcellCCActual");
            this.xrcellCCActual.Name = "xrcellCCActual";
            this.xrcellCCActual.StylePriority.UseFont = false;
            this.xrcellCCActual.StylePriority.UseTextAlignment = false;
            // 
            // xrCellCCVariance
            // 
            this.xrCellCCVariance.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.BUDGET_VARIANCE", "{0:n}")});
            resources.ApplyResources(this.xrCellCCVariance, "xrCellCCVariance");
            this.xrCellCCVariance.Name = "xrCellCCVariance";
            this.xrCellCCVariance.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellCCVariance.StylePriority.UseFont = false;
            this.xrCellCCVariance.StylePriority.UsePadding = false;
            this.xrCellCCVariance.StylePriority.UseTextAlignment = false;
            // 
            // xrCellCCPercentage
            // 
            this.xrCellCCPercentage.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.PERCENTAGE", "{0:n}")});
            resources.ApplyResources(this.xrCellCCPercentage, "xrCellCCPercentage");
            this.xrCellCCPercentage.Name = "xrCellCCPercentage";
            this.xrCellCCPercentage.StylePriority.UseFont = false;
            this.xrCellCCPercentage.StylePriority.UseTextAlignment = false;
            this.xrCellCCPercentage.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCellCCPercentage_BeforePrint);
            // 
            // grHeaderCCCategory
            // 
            this.grHeaderCCCategory.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCCCName});
            this.grHeaderCCCategory.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("COST_CENTRE_CATEGORY_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grHeaderCCCategory, "grHeaderCCCategory");
            this.grHeaderCCCategory.Name = "grHeaderCCCategory";
            // 
            // grFooterCCCategory
            // 
            resources.ApplyResources(this.grFooterCCCategory, "grFooterCCCategory");
            this.grFooterCCCategory.Name = "grFooterCCCategory";
            // 
            // xrtblCCCName
            // 
            resources.ApplyResources(this.xrtblCCCName, "xrtblCCCName");
            this.xrtblCCCName.Name = "xrtblCCCName";
            this.xrtblCCCName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblCCCName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrtblCCCName.StyleName = "styleGroupRow";
            this.xrtblCCCName.StylePriority.UsePadding = false;
            this.xrtblCCCName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellCCCEmpty,
            this.xrTableCell6});
            this.xrTableRow6.Name = "xrTableRow6";
            resources.ApplyResources(this.xrTableRow6, "xrTableRow6");
            // 
            // xrcellCCCEmpty
            // 
            resources.ApplyResources(this.xrcellCCCEmpty, "xrcellCCCEmpty");
            this.xrcellCCCEmpty.Name = "xrcellCCCEmpty";
            this.xrcellCCCEmpty.StylePriority.UseBackColor = false;
            this.xrcellCCCEmpty.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrcellCCCEmpty_BeforePrint);
            // 
            // xrTableCell6
            // 
            resources.ApplyResources(this.xrTableCell6, "xrTableCell6");
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BUDGETVARIANCE.COST_CENTRE_CATEGORY_NAME")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBackColor = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            // 
            // BudgetVarianceCC
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grHeaderCCCategory,
            this.grFooterCCCategory});
            this.DataMember = "BUDGETVARIANCE";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grFooterCCCategory, 0);
            this.Controls.SetChildIndex(this.grHeaderCCCategory, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCCCName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrRowCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCCName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCredit;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel lblBreak;
        private DevExpress.XtraReports.UI.XRTable xrTblCC;
        private DevExpress.XtraReports.UI.XRTableRow xrRowCC;
        private DevExpress.XtraReports.UI.XRTableCell xrcellCCAbbrevation;
        private DevExpress.XtraReports.UI.XRTableCell xrcellCCName;
        private DevExpress.XtraReports.UI.XRTableCell xrCellCCApproved;
        private DevExpress.XtraReports.UI.XRTableCell xrcellCCActual;
        private DevExpress.XtraReports.UI.XRTableCell xrCellCCVariance;
        private DevExpress.XtraReports.UI.XRTableCell xrCellCCPercentage;
        private DevExpress.XtraReports.UI.GroupHeaderBand grHeaderCCCategory;
        private DevExpress.XtraReports.UI.GroupFooterBand grFooterCCCategory;
        private DevExpress.XtraReports.UI.XRTable xrtblCCCName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrcellCCCEmpty;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
    }
}
