namespace Bosco.Report.ReportObject
{
    partial class Receipts
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
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary1 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary2 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary3 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Receipts));
            this.grpReceiptGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblReceiptGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGroupCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGroupAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpCostCentreNameReceipts = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPaymentCostCentreName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblCellCostcentreName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellCCAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpcostCenterCategory = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblCostCentreCategoryName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCostCentreCategoryName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellCCCAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpCCBreakup = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
            this.xrCCBreakup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpParentGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrParentGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblParentCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParentGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParentGroupAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableReceipt = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubreportCCDetails = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreportDonorDetails = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblReceiptGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrPaymentCostCentreName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblCostCentreCategoryName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrCCBreakup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrParentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableReceipt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportDonorDetails,
            this.xrSubreportCCDetails,
            this.xrTableReceipt});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            this.Detail.AfterPrint += new System.EventHandler(this.Detail_AfterPrint);
            // 
            // grpReceiptGroup
            // 
            this.grpReceiptGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblReceiptGroup});
            this.grpReceiptGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("GROUP_CODE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpReceiptGroup, "grpReceiptGroup");
            this.grpReceiptGroup.Level = 1;
            this.grpReceiptGroup.Name = "grpReceiptGroup";
            xrGroupSortingSummary1.Enabled = true;
            xrGroupSortingSummary1.FieldName = "GROUP_CODE";
            xrGroupSortingSummary1.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpReceiptGroup.SortingSummary = xrGroupSortingSummary1;
            this.grpReceiptGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpReceiptGroup_BeforePrint);
            // 
            // xrtblReceiptGroup
            // 
            resources.ApplyResources(this.xrtblReceiptGroup, "xrtblReceiptGroup");
            this.xrtblReceiptGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrtblReceiptGroup.Name = "xrtblReceiptGroup";
            this.xrtblReceiptGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblReceiptGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblReceiptGroup.StyleName = "styleGroupRow";
            this.xrtblReceiptGroup.StylePriority.UseBackColor = false;
            this.xrtblReceiptGroup.StylePriority.UseBorderColor = false;
            this.xrtblReceiptGroup.StylePriority.UseBorders = false;
            this.xrtblReceiptGroup.StylePriority.UseFont = false;
            this.xrtblReceiptGroup.StylePriority.UsePadding = false;
            this.xrtblReceiptGroup.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGroupCode,
            this.xrGroupName,
            this.xrGroupAmt});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrGroupCode
            // 
            this.xrGroupCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrGroupCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.GROUP_CODE")});
            resources.ApplyResources(this.xrGroupCode, "xrGroupCode");
            this.xrGroupCode.Name = "xrGroupCode";
            this.xrGroupCode.StylePriority.UseBorders = false;
            this.xrGroupCode.StylePriority.UseFont = false;
            // 
            // xrGroupName
            // 
            this.xrGroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.LEDGER_GROUP")});
            resources.ApplyResources(this.xrGroupName, "xrGroupName");
            this.xrGroupName.Name = "xrGroupName";
            this.xrGroupName.StylePriority.UseBorders = false;
            this.xrGroupName.StylePriority.UseFont = false;
            // 
            // xrGroupAmt
            // 
            this.xrGroupAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrGroupAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT")});
            resources.ApplyResources(this.xrGroupAmt, "xrGroupAmt");
            this.xrGroupAmt.Name = "xrGroupAmt";
            this.xrGroupAmt.StylePriority.UseBorders = false;
            this.xrGroupAmt.StylePriority.UseFont = false;
            this.xrGroupAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrGroupAmt.Summary = xrSummary2;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpCostCentreNameReceipts
            // 
            this.grpCostCentreNameReceipts.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPaymentCostCentreName});
            this.grpCostCentreNameReceipts.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("COST_CENTRE_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpCostCentreNameReceipts, "grpCostCentreNameReceipts");
            this.grpCostCentreNameReceipts.Level = 3;
            this.grpCostCentreNameReceipts.Name = "grpCostCentreNameReceipts";
            xrGroupSortingSummary2.Enabled = true;
            xrGroupSortingSummary2.FieldName = "COST_CENTRE_NAME";
            xrGroupSortingSummary2.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpCostCentreNameReceipts.SortingSummary = xrGroupSortingSummary2;
            // 
            // xrPaymentCostCentreName
            // 
            resources.ApplyResources(this.xrPaymentCostCentreName, "xrPaymentCostCentreName");
            this.xrPaymentCostCentreName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPaymentCostCentreName.Name = "xrPaymentCostCentreName";
            this.xrPaymentCostCentreName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrPaymentCostCentreName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrPaymentCostCentreName.StylePriority.UseBackColor = false;
            this.xrPaymentCostCentreName.StylePriority.UseBorderColor = false;
            this.xrPaymentCostCentreName.StylePriority.UseBorders = false;
            this.xrPaymentCostCentreName.StylePriority.UseFont = false;
            this.xrPaymentCostCentreName.StylePriority.UseForeColor = false;
            this.xrPaymentCostCentreName.StylePriority.UsePadding = false;
            this.xrPaymentCostCentreName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblCellCostcentreName,
            this.xrcellCCAmount});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrtblCellCostcentreName
            // 
            this.xrtblCellCostcentreName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.COST_CENTRE_NAME")});
            resources.ApplyResources(this.xrtblCellCostcentreName, "xrtblCellCostcentreName");
            this.xrtblCellCostcentreName.Name = "xrtblCellCostcentreName";
            this.xrtblCellCostcentreName.StylePriority.UseFont = false;
            // 
            // xrcellCCAmount
            // 
            this.xrcellCCAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT")});
            resources.ApplyResources(this.xrcellCCAmount, "xrcellCCAmount");
            this.xrcellCCAmount.Name = "xrcellCCAmount";
            this.xrcellCCAmount.StylePriority.UseFont = false;
            this.xrcellCCAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrcellCCAmount.Summary = xrSummary3;
            // 
            // grpcostCenterCategory
            // 
            this.grpcostCenterCategory.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblCostCentreCategoryName});
            this.grpcostCenterCategory.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("COST_CENTRE_CATEGORY_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpcostCenterCategory, "grpcostCenterCategory");
            this.grpcostCenterCategory.Level = 4;
            this.grpcostCenterCategory.Name = "grpcostCenterCategory";
            xrGroupSortingSummary3.Enabled = true;
            xrGroupSortingSummary3.FieldName = "COST_CENTRE_CATEGORY_NAME";
            xrGroupSortingSummary3.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpcostCenterCategory.SortingSummary = xrGroupSortingSummary3;
            // 
            // xrTblCostCentreCategoryName
            // 
            resources.ApplyResources(this.xrTblCostCentreCategoryName, "xrTblCostCentreCategoryName");
            this.xrTblCostCentreCategoryName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTblCostCentreCategoryName.Name = "xrTblCostCentreCategoryName";
            this.xrTblCostCentreCategoryName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTblCostCentreCategoryName.StylePriority.UseBackColor = false;
            this.xrTblCostCentreCategoryName.StylePriority.UseBorderColor = false;
            this.xrTblCostCentreCategoryName.StylePriority.UseBorders = false;
            this.xrTblCostCentreCategoryName.StylePriority.UseFont = false;
            this.xrTblCostCentreCategoryName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCostCentreCategoryName,
            this.xrcellCCCAmount});
            this.xrTableRow4.Name = "xrTableRow4";
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            // 
            // xrCostCentreCategoryName
            // 
            this.xrCostCentreCategoryName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.COST_CENTRE_CATEGORY_NAME")});
            resources.ApplyResources(this.xrCostCentreCategoryName, "xrCostCentreCategoryName");
            this.xrCostCentreCategoryName.Name = "xrCostCentreCategoryName";
            this.xrCostCentreCategoryName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCostCentreCategoryName.StylePriority.UseFont = false;
            this.xrCostCentreCategoryName.StylePriority.UsePadding = false;
            // 
            // xrcellCCCAmount
            // 
            this.xrcellCCCAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT")});
            resources.ApplyResources(this.xrcellCCCAmount, "xrcellCCCAmount");
            this.xrcellCCCAmount.Name = "xrcellCCCAmount";
            this.xrcellCCCAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrcellCCCAmount.StylePriority.UseFont = false;
            this.xrcellCCCAmount.StylePriority.UsePadding = false;
            this.xrcellCCCAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrcellCCCAmount.Summary = xrSummary4;
            // 
            // grpCCBreakup
            // 
            this.grpCCBreakup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak1,
            this.xrCCBreakup});
            resources.ApplyResources(this.grpCCBreakup, "grpCCBreakup");
            this.grpCCBreakup.Name = "grpCCBreakup";
            // 
            // xrPageBreak1
            // 
            resources.ApplyResources(this.xrPageBreak1, "xrPageBreak1");
            this.xrPageBreak1.Name = "xrPageBreak1";
            // 
            // xrCCBreakup
            // 
            resources.ApplyResources(this.xrCCBreakup, "xrCCBreakup");
            this.xrCCBreakup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrCCBreakup.Name = "xrCCBreakup";
            this.xrCCBreakup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCCBreakup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrCCBreakup.StyleName = "styleGroupRow";
            this.xrCCBreakup.StylePriority.UseBackColor = false;
            this.xrCCBreakup.StylePriority.UseBorderColor = false;
            this.xrCCBreakup.StylePriority.UseBorders = false;
            this.xrCCBreakup.StylePriority.UseFont = false;
            this.xrCCBreakup.StylePriority.UsePadding = false;
            this.xrCCBreakup.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
            this.xrTableRow5.Name = "xrTableRow5";
            resources.ApplyResources(this.xrTableRow5, "xrTableRow5");
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary5, "xrSummary5");
            xrSummary5.IgnoreNullValues = true;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell4.Summary = xrSummary5;
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            // 
            // grpParentGroup
            // 
            this.grpParentGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrParentGroup});
            this.grpParentGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpParentGroup, "grpParentGroup");
            this.grpParentGroup.Level = 2;
            this.grpParentGroup.Name = "grpParentGroup";
            // 
            // xrParentGroup
            // 
            resources.ApplyResources(this.xrParentGroup, "xrParentGroup");
            this.xrParentGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrParentGroup.Name = "xrParentGroup";
            this.xrParentGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrParentGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrParentGroup.StyleName = "styleGroupRow";
            this.xrParentGroup.StylePriority.UseBackColor = false;
            this.xrParentGroup.StylePriority.UseBorderColor = false;
            this.xrParentGroup.StylePriority.UseBorders = false;
            this.xrParentGroup.StylePriority.UseFont = false;
            this.xrParentGroup.StylePriority.UsePadding = false;
            this.xrParentGroup.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblParentCode,
            this.xrParentGroupName,
            this.xrParentGroupAmt});
            this.xrTableRow6.Name = "xrTableRow6";
            resources.ApplyResources(this.xrTableRow6, "xrTableRow6");
            // 
            // xrtblParentCode
            // 
            this.xrtblParentCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.PARENT_CODE")});
            resources.ApplyResources(this.xrtblParentCode, "xrtblParentCode");
            this.xrtblParentCode.Name = "xrtblParentCode";
            this.xrtblParentCode.StylePriority.UseFont = false;
            // 
            // xrParentGroupName
            // 
            this.xrParentGroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrParentGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.PARENT_GROUP")});
            resources.ApplyResources(this.xrParentGroupName, "xrParentGroupName");
            this.xrParentGroupName.Name = "xrParentGroupName";
            this.xrParentGroupName.StylePriority.UseBorders = false;
            this.xrParentGroupName.StylePriority.UseFont = false;
            this.xrParentGroupName.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrParentGroupName_EvaluateBinding);
            // 
            // xrParentGroupAmt
            // 
            this.xrParentGroupAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrParentGroupAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT")});
            resources.ApplyResources(this.xrParentGroupAmt, "xrParentGroupAmt");
            this.xrParentGroupAmt.Name = "xrParentGroupAmt";
            this.xrParentGroupAmt.StylePriority.UseBorders = false;
            this.xrParentGroupAmt.StylePriority.UseFont = false;
            this.xrParentGroupAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary6, "xrSummary6");
            xrSummary6.IgnoreNullValues = true;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrParentGroupAmt.Summary = xrSummary6;
            // 
            // xrTableReceipt
            // 
            resources.ApplyResources(this.xrTableReceipt, "xrTableReceipt");
            this.xrTableReceipt.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableReceipt.Name = "xrTableReceipt";
            this.xrTableReceipt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableReceipt.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTableReceipt.StyleName = "styleRow";
            this.xrTableReceipt.StylePriority.UseBorderColor = false;
            this.xrTableReceipt.StylePriority.UseBorders = false;
            this.xrTableReceipt.StylePriority.UseFont = false;
            this.xrTableReceipt.StylePriority.UsePadding = false;
            this.xrTableReceipt.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLedgerCode,
            this.xrLedgerName,
            this.xrLedgerAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrLedgerCode
            // 
            this.xrLedgerCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.LEDGER_CODE")});
            resources.ApplyResources(this.xrLedgerCode, "xrLedgerCode");
            this.xrLedgerCode.Name = "xrLedgerCode";
            this.xrLedgerCode.StylePriority.UseBorders = false;
            this.xrLedgerCode.StylePriority.UseFont = false;
            // 
            // xrLedgerName
            // 
            this.xrLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.LEDGER_NAME")});
            resources.ApplyResources(this.xrLedgerName, "xrLedgerName");
            this.xrLedgerName.Name = "xrLedgerName";
            this.xrLedgerName.StylePriority.UseBorders = false;
            this.xrLedgerName.StylePriority.UseFont = false;
            // 
            // xrLedgerAmt
            // 
            this.xrLedgerAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedgerAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.RECEIPTAMT", "{0:n}")});
            resources.ApplyResources(this.xrLedgerAmt, "xrLedgerAmt");
            this.xrLedgerAmt.Name = "xrLedgerAmt";
            this.xrLedgerAmt.StylePriority.UseBorders = false;
            this.xrLedgerAmt.StylePriority.UseFont = false;
            this.xrLedgerAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.xrLedgerAmt.Summary = xrSummary1;
            // 
            // xrSubreportCCDetails
            // 
            this.xrSubreportCCDetails.CanShrink = true;
            resources.ApplyResources(this.xrSubreportCCDetails, "xrSubreportCCDetails");
            this.xrSubreportCCDetails.Name = "xrSubreportCCDetails";
            this.xrSubreportCCDetails.ReportSource = new Bosco.Report.ReportObject.UcCCDetail();
            // 
            // xrSubreportDonorDetails
            // 
            this.xrSubreportDonorDetails.CanShrink = true;
            resources.ApplyResources(this.xrSubreportDonorDetails, "xrSubreportDonorDetails");
            this.xrSubreportDonorDetails.Name = "xrSubreportDonorDetails";
            this.xrSubreportDonorDetails.ReportSource = new Bosco.Report.ReportObject.UcCCDonorDetail();
            // 
            // Receipts
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpReceiptGroup,
            this.grpCostCentreNameReceipts,
            this.grpcostCenterCategory,
            this.grpCCBreakup,
            this.grpParentGroup});
            this.DataMember = "Receipts";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpParentGroup, 0);
            this.Controls.SetChildIndex(this.grpCCBreakup, 0);
            this.Controls.SetChildIndex(this.grpcostCenterCategory, 0);
            this.Controls.SetChildIndex(this.grpCostCentreNameReceipts, 0);
            this.Controls.SetChildIndex(this.grpReceiptGroup, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblReceiptGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrPaymentCostCentreName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblCostCentreCategoryName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrCCBreakup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrParentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableReceipt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand grpReceiptGroup;
        private DevExpress.XtraReports.UI.XRTable xrtblReceiptGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrGroupCode;
        private DevExpress.XtraReports.UI.XRTableCell xrGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrGroupAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpCostCentreNameReceipts;
        private DevExpress.XtraReports.UI.XRTable xrPaymentCostCentreName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrtblCellCostcentreName;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpcostCenterCategory;
        private DevExpress.XtraReports.UI.XRTable xrTblCostCentreCategoryName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrCostCentreCategoryName;
        private DevExpress.XtraReports.UI.GroupFooterBand grpCCBreakup;
        private DevExpress.XtraReports.UI.XRTable xrCCBreakup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRPageBreak xrPageBreak1;
        private DevExpress.XtraReports.UI.XRTableCell xrcellCCCAmount;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpParentGroup;
        private DevExpress.XtraReports.UI.XRTable xrParentGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrParentGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrParentGroupAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrcellCCAmount;
        private DevExpress.XtraReports.UI.XRTable xrTableReceipt;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerAmt;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportCCDetails;
        private DevExpress.XtraReports.UI.XRTableCell xrtblParentCode;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportDonorDetails;
    }
}
