﻿namespace Bosco.Report.ReportObject
{
    partial class Payments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Payments));
            this.grpPaymentGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblReceiptGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrPaymentGroupCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPaymentGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrgrpPayAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpCostCentreNamePayments = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblCostcenterName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCelCostCentreName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellCCAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpcostCentreCategory = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.tblCostCentreCategoryName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCostCentreCategoryName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellCCCAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpCCBreakup = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
            this.xrCCBreakup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpParentGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblParentGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblParentCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParentGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParentGroupAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblPaymentLedger = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrPaymentLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPaymentLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPayAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubreportCCDetails = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblReceiptGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCostcenterName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCostCentreCategoryName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrCCBreakup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblParentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblPaymentLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportCCDetails,
            this.xrtblPaymentLedger});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // grpPaymentGroup
            // 
            this.grpPaymentGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblReceiptGroup});
            this.grpPaymentGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("GROUP_CODE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpPaymentGroup, "grpPaymentGroup");
            this.grpPaymentGroup.Level = 1;
            this.grpPaymentGroup.Name = "grpPaymentGroup";
            xrGroupSortingSummary1.Enabled = true;
            xrGroupSortingSummary1.FieldName = "GROUP_CODE";
            xrGroupSortingSummary1.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpPaymentGroup.SortingSummary = xrGroupSortingSummary1;
            this.grpPaymentGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpPaymentGroup_BeforePrint);
            // 
            // xrtblReceiptGroup
            // 
            resources.ApplyResources(this.xrtblReceiptGroup, "xrtblReceiptGroup");
            this.xrtblReceiptGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
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
            this.xrtblReceiptGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrtblReceiptGroup_BeforePrint);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrPaymentGroupCode,
            this.xrPaymentGroupName,
            this.xrgrpPayAmt});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrPaymentGroupCode
            // 
            this.xrPaymentGroupCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPaymentGroupCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.GROUP_CODE")});
            resources.ApplyResources(this.xrPaymentGroupCode, "xrPaymentGroupCode");
            this.xrPaymentGroupCode.Name = "xrPaymentGroupCode";
            this.xrPaymentGroupCode.StylePriority.UseBorders = false;
            this.xrPaymentGroupCode.StylePriority.UseFont = false;
            // 
            // xrPaymentGroupName
            // 
            this.xrPaymentGroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrPaymentGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.LEDGER_GROUP")});
            resources.ApplyResources(this.xrPaymentGroupName, "xrPaymentGroupName");
            this.xrPaymentGroupName.Name = "xrPaymentGroupName";
            this.xrPaymentGroupName.StylePriority.UseBorders = false;
            this.xrPaymentGroupName.StylePriority.UseFont = false;
            // 
            // xrgrpPayAmt
            // 
            this.xrgrpPayAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrgrpPayAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.PAYMENTAMT")});
            resources.ApplyResources(this.xrgrpPayAmt, "xrgrpPayAmt");
            this.xrgrpPayAmt.Name = "xrgrpPayAmt";
            this.xrgrpPayAmt.StylePriority.UseBorders = false;
            this.xrgrpPayAmt.StylePriority.UseFont = false;
            this.xrgrpPayAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrgrpPayAmt.Summary = xrSummary2;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpCostCentreNamePayments
            // 
            this.grpCostCentreNamePayments.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCostcenterName});
            this.grpCostCentreNamePayments.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("COST_CENTRE_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpCostCentreNamePayments, "grpCostCentreNamePayments");
            this.grpCostCentreNamePayments.Level = 3;
            this.grpCostCentreNamePayments.Name = "grpCostCentreNamePayments";
            xrGroupSortingSummary2.Enabled = true;
            xrGroupSortingSummary2.FieldName = "COST_CENTRE_NAME";
            xrGroupSortingSummary2.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpCostCentreNamePayments.SortingSummary = xrGroupSortingSummary2;
            // 
            // xrtblCostcenterName
            // 
            resources.ApplyResources(this.xrtblCostcenterName, "xrtblCostcenterName");
            this.xrtblCostcenterName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblCostcenterName.Name = "xrtblCostcenterName";
            this.xrtblCostcenterName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblCostcenterName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblCostcenterName.StylePriority.UseBackColor = false;
            this.xrtblCostcenterName.StylePriority.UseBorderColor = false;
            this.xrtblCostcenterName.StylePriority.UseBorders = false;
            this.xrtblCostcenterName.StylePriority.UseFont = false;
            this.xrtblCostcenterName.StylePriority.UsePadding = false;
            this.xrtblCostcenterName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCelCostCentreName,
            this.xrCellCCAmount});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrCelCostCentreName
            // 
            this.xrCelCostCentreName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.COST_CENTRE_NAME")});
            resources.ApplyResources(this.xrCelCostCentreName, "xrCelCostCentreName");
            this.xrCelCostCentreName.Name = "xrCelCostCentreName";
            this.xrCelCostCentreName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCelCostCentreName.StylePriority.UseFont = false;
            this.xrCelCostCentreName.StylePriority.UsePadding = false;
            // 
            // xrCellCCAmount
            // 
            this.xrCellCCAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.PAYMENTAMT")});
            this.xrCellCCAmount.Name = "xrCellCCAmount";
            this.xrCellCCAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrCellCCAmount.Summary = xrSummary3;
            resources.ApplyResources(this.xrCellCCAmount, "xrCellCCAmount");
            // 
            // grpcostCentreCategory
            // 
            this.grpcostCentreCategory.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tblCostCentreCategoryName});
            this.grpcostCentreCategory.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("COST_CENTRE_CATEGORY_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpcostCentreCategory, "grpcostCentreCategory");
            this.grpcostCentreCategory.Level = 4;
            this.grpcostCentreCategory.Name = "grpcostCentreCategory";
            xrGroupSortingSummary3.Enabled = true;
            xrGroupSortingSummary3.FieldName = "COST_CENTRE_CATEGORY_NAME";
            xrGroupSortingSummary3.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpcostCentreCategory.SortingSummary = xrGroupSortingSummary3;
            // 
            // tblCostCentreCategoryName
            // 
            resources.ApplyResources(this.tblCostCentreCategoryName, "tblCostCentreCategoryName");
            this.tblCostCentreCategoryName.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.tblCostCentreCategoryName.Name = "tblCostCentreCategoryName";
            this.tblCostCentreCategoryName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.tblCostCentreCategoryName.StylePriority.UseBackColor = false;
            this.tblCostCentreCategoryName.StylePriority.UseBorderColor = false;
            this.tblCostCentreCategoryName.StylePriority.UseBorders = false;
            this.tblCostCentreCategoryName.StylePriority.UseFont = false;
            this.tblCostCentreCategoryName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCostCentreCategoryName,
            this.xrCellCCCAmount});
            this.xrTableRow4.Name = "xrTableRow4";
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            // 
            // xrCostCentreCategoryName
            // 
            this.xrCostCentreCategoryName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.COST_CENTRE_CATEGORY_NAME")});
            resources.ApplyResources(this.xrCostCentreCategoryName, "xrCostCentreCategoryName");
            this.xrCostCentreCategoryName.Name = "xrCostCentreCategoryName";
            this.xrCostCentreCategoryName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCostCentreCategoryName.StylePriority.UseFont = false;
            this.xrCostCentreCategoryName.StylePriority.UsePadding = false;
            // 
            // xrCellCCCAmount
            // 
            this.xrCellCCCAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.PAYMENTAMT")});
            resources.ApplyResources(this.xrCellCCCAmount, "xrCellCCCAmount");
            this.xrCellCCCAmount.Name = "xrCellCCCAmount";
            this.xrCellCCCAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCellCCCAmount.StylePriority.UseFont = false;
            this.xrCellCCCAmount.StylePriority.UsePadding = false;
            this.xrCellCCCAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrCellCCCAmount.Summary = xrSummary4;
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
            this.xrCCBreakup.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
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
            this.xrTableCell4,
            this.xrTableCell5});
            this.xrTableRow5.Name = "xrTableRow5";
            resources.ApplyResources(this.xrTableRow5, "xrTableRow5");
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.PAYMENTAMT")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary5, "xrSummary5");
            xrSummary5.IgnoreNullValues = true;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell5.Summary = xrSummary5;
            resources.ApplyResources(this.xrTableCell5, "xrTableCell5");
            // 
            // grpParentGroup
            // 
            this.grpParentGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblParentGroup});
            this.grpParentGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PARENT_CODE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpParentGroup, "grpParentGroup");
            this.grpParentGroup.Level = 2;
            this.grpParentGroup.Name = "grpParentGroup";
            this.grpParentGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpParentGroup_BeforePrint);
            // 
            // xrtblParentGroup
            // 
            resources.ApplyResources(this.xrtblParentGroup, "xrtblParentGroup");
            this.xrtblParentGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblParentGroup.Name = "xrtblParentGroup";
            this.xrtblParentGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblParentGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrtblParentGroup.StyleName = "styleGroupRow";
            this.xrtblParentGroup.StylePriority.UseBackColor = false;
            this.xrtblParentGroup.StylePriority.UseBorderColor = false;
            this.xrtblParentGroup.StylePriority.UseBorders = false;
            this.xrtblParentGroup.StylePriority.UseFont = false;
            this.xrtblParentGroup.StylePriority.UsePadding = false;
            this.xrtblParentGroup.StylePriority.UseTextAlignment = false;
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
            this.xrtblParentCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblParentCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.PARENT_CODE")});
            resources.ApplyResources(this.xrtblParentCode, "xrtblParentCode");
            this.xrtblParentCode.Name = "xrtblParentCode";
            this.xrtblParentCode.StylePriority.UseBorders = false;
            this.xrtblParentCode.StylePriority.UseFont = false;
            // 
            // xrParentGroupName
            // 
            this.xrParentGroupName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrParentGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.PARENT_GROUP")});
            resources.ApplyResources(this.xrParentGroupName, "xrParentGroupName");
            this.xrParentGroupName.Name = "xrParentGroupName";
            this.xrParentGroupName.StylePriority.UseBorders = false;
            this.xrParentGroupName.StylePriority.UseFont = false;
            this.xrParentGroupName.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xrParentGroupName_EvaluateBinding);
            // 
            // xrParentGroupAmt
            // 
            this.xrParentGroupAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrParentGroupAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.PAYMENTAMT")});
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
            // xrtblPaymentLedger
            // 
            resources.ApplyResources(this.xrtblPaymentLedger, "xrtblPaymentLedger");
            this.xrtblPaymentLedger.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblPaymentLedger.Name = "xrtblPaymentLedger";
            this.xrtblPaymentLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblPaymentLedger.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblPaymentLedger.StyleName = "styleRow";
            this.xrtblPaymentLedger.StylePriority.UseBorderColor = false;
            this.xrtblPaymentLedger.StylePriority.UseBorders = false;
            this.xrtblPaymentLedger.StylePriority.UseFont = false;
            this.xrtblPaymentLedger.StylePriority.UsePadding = false;
            this.xrtblPaymentLedger.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrPaymentLedgerCode,
            this.xrPaymentLedgerName,
            this.xrPayAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrPaymentLedgerCode
            // 
            this.xrPaymentLedgerCode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPaymentLedgerCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.LEDGER_CODE")});
            resources.ApplyResources(this.xrPaymentLedgerCode, "xrPaymentLedgerCode");
            this.xrPaymentLedgerCode.Name = "xrPaymentLedgerCode";
            this.xrPaymentLedgerCode.StylePriority.UseBorders = false;
            this.xrPaymentLedgerCode.StylePriority.UseFont = false;
            // 
            // xrPaymentLedgerName
            // 
            this.xrPaymentLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPaymentLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.LEDGER_NAME")});
            resources.ApplyResources(this.xrPaymentLedgerName, "xrPaymentLedgerName");
            this.xrPaymentLedgerName.Name = "xrPaymentLedgerName";
            this.xrPaymentLedgerName.StylePriority.UseBorders = false;
            this.xrPaymentLedgerName.StylePriority.UseFont = false;
            // 
            // xrPayAmt
            // 
            this.xrPayAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPayAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.PAYMENTAMT", "{0:n}")});
            resources.ApplyResources(this.xrPayAmt, "xrPayAmt");
            this.xrPayAmt.Name = "xrPayAmt";
            this.xrPayAmt.StylePriority.UseBorders = false;
            this.xrPayAmt.StylePriority.UseFont = false;
            this.xrPayAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.xrPayAmt.Summary = xrSummary1;
            // 
            // xrSubreportCCDetails
            // 
            this.xrSubreportCCDetails.CanShrink = true;
            resources.ApplyResources(this.xrSubreportCCDetails, "xrSubreportCCDetails");
            this.xrSubreportCCDetails.Name = "xrSubreportCCDetails";
            this.xrSubreportCCDetails.ReportSource = new Bosco.Report.ReportObject.UcCCDetail();
            // 
            // Payments
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.grpPaymentGroup,
            this.Detail,
            this.grpCostCentreNamePayments,
            this.grpcostCentreCategory,
            this.grpCCBreakup,
            this.grpParentGroup});
            this.DataMember = "Payments";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpParentGroup, 0);
            this.Controls.SetChildIndex(this.grpCCBreakup, 0);
            this.Controls.SetChildIndex(this.grpcostCentreCategory, 0);
            this.Controls.SetChildIndex(this.grpCostCentreNamePayments, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            this.Controls.SetChildIndex(this.grpPaymentGroup, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblReceiptGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCostcenterName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblCostCentreCategoryName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrCCBreakup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblParentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblPaymentLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand grpPaymentGroup;
        private DevExpress.XtraReports.UI.XRTable xrtblReceiptGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrPaymentGroupCode;
        private DevExpress.XtraReports.UI.XRTableCell xrPaymentGroupName;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTableCell xrgrpPayAmt;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpCostCentreNamePayments;
        private DevExpress.XtraReports.UI.XRTable xrtblCostcenterName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrCelCostCentreName;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpcostCentreCategory;
        private DevExpress.XtraReports.UI.XRTable tblCostCentreCategoryName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrCostCentreCategoryName;
        private DevExpress.XtraReports.UI.GroupFooterBand grpCCBreakup;
        private DevExpress.XtraReports.UI.XRTable xrCCBreakup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRPageBreak xrPageBreak1;
        private DevExpress.XtraReports.UI.XRTableCell xrCellCCCAmount;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpParentGroup;
        private DevExpress.XtraReports.UI.XRTable xrtblParentGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrParentGroupName;
        private DevExpress.XtraReports.UI.XRTableCell xrParentGroupAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCellCCAmount;
        private DevExpress.XtraReports.UI.XRTable xrtblPaymentLedger;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrPaymentLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell xrPaymentLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrPayAmt;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportCCDetails;
        private DevExpress.XtraReports.UI.XRTableCell xrtblParentCode;

    }
}
