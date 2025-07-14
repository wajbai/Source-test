namespace Bosco.Report.ReportObject
{
    partial class AuditVoucherStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditVoucherStatistics));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrHeaderRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapVoucherType = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapNoOfCreated = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapNoOfModified = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapNoOfDeleted = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTblData = new DevExpress.XtraReports.UI.XRTable();
            this.xrDataRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellVoucherType = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNoOfCreated = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNoOfModified = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellNoOfDeleted = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblAmountFilter = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrGrandRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotalCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSumNoOfCreated = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSumNoOfModified = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSumNoOfDeleted = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.calCLBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGrandInFlow = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGrandOutFlow = new DevExpress.XtraReports.UI.CalculatedField();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblData});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrHeaderRow});
            this.xrtblHeaderCaption.StyleName = "styleColumnHeader";
            this.xrtblHeaderCaption.StylePriority.UseFont = false;
            // 
            // xrHeaderRow
            // 
            this.xrHeaderRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapVoucherType,
            this.xrCapNoOfCreated,
            this.xrCapNoOfModified,
            this.xrCapNoOfDeleted});
            this.xrHeaderRow.Name = "xrHeaderRow";
            resources.ApplyResources(this.xrHeaderRow, "xrHeaderRow");
            // 
            // xrCapVoucherType
            // 
            resources.ApplyResources(this.xrCapVoucherType, "xrCapVoucherType");
            this.xrCapVoucherType.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapVoucherType.Name = "xrCapVoucherType";
            this.xrCapVoucherType.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapVoucherType.StylePriority.UseBackColor = false;
            this.xrCapVoucherType.StylePriority.UseBorderColor = false;
            this.xrCapVoucherType.StylePriority.UseBorders = false;
            this.xrCapVoucherType.StylePriority.UseFont = false;
            this.xrCapVoucherType.StylePriority.UsePadding = false;
            this.xrCapVoucherType.StylePriority.UseTextAlignment = false;
            // 
            // xrCapNoOfCreated
            // 
            resources.ApplyResources(this.xrCapNoOfCreated, "xrCapNoOfCreated");
            this.xrCapNoOfCreated.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapNoOfCreated.Name = "xrCapNoOfCreated";
            this.xrCapNoOfCreated.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapNoOfCreated.StylePriority.UseBackColor = false;
            this.xrCapNoOfCreated.StylePriority.UseBorderColor = false;
            this.xrCapNoOfCreated.StylePriority.UseBorders = false;
            this.xrCapNoOfCreated.StylePriority.UseFont = false;
            this.xrCapNoOfCreated.StylePriority.UsePadding = false;
            this.xrCapNoOfCreated.StylePriority.UseTextAlignment = false;
            // 
            // xrCapNoOfModified
            // 
            this.xrCapNoOfModified.Name = "xrCapNoOfModified";
            this.xrCapNoOfModified.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapNoOfModified.StylePriority.UsePadding = false;
            this.xrCapNoOfModified.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapNoOfModified, "xrCapNoOfModified");
            // 
            // xrCapNoOfDeleted
            // 
            resources.ApplyResources(this.xrCapNoOfDeleted, "xrCapNoOfDeleted");
            this.xrCapNoOfDeleted.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapNoOfDeleted.Name = "xrCapNoOfDeleted";
            this.xrCapNoOfDeleted.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapNoOfDeleted.StylePriority.UseBackColor = false;
            this.xrCapNoOfDeleted.StylePriority.UseBorderColor = false;
            this.xrCapNoOfDeleted.StylePriority.UseBorders = false;
            this.xrCapNoOfDeleted.StylePriority.UseFont = false;
            this.xrCapNoOfDeleted.StylePriority.UsePadding = false;
            this.xrCapNoOfDeleted.StylePriority.UseTextAlignment = false;
            // 
            // xrTblData
            // 
            resources.ApplyResources(this.xrTblData, "xrTblData");
            this.xrTblData.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblData.Name = "xrTblData";
            this.xrTblData.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblData.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrDataRow});
            this.xrTblData.StyleName = "styleRow";
            this.xrTblData.StylePriority.UseBorderColor = false;
            this.xrTblData.StylePriority.UseBorders = false;
            this.xrTblData.StylePriority.UseFont = false;
            this.xrTblData.StylePriority.UsePadding = false;
            // 
            // xrDataRow
            // 
            this.xrDataRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellVoucherType,
            this.xrCellNoOfCreated,
            this.xrCellNoOfModified,
            this.xrCellNoOfDeleted});
            this.xrDataRow.Name = "xrDataRow";
            resources.ApplyResources(this.xrDataRow, "xrDataRow");
            // 
            // xrCellVoucherType
            // 
            this.xrCellVoucherType.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.VOUCHER_NAME")});
            this.xrCellVoucherType.Name = "xrCellVoucherType";
            this.xrCellVoucherType.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellVoucherType.StylePriority.UsePadding = false;
            this.xrCellVoucherType.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCellVoucherType, "xrCellVoucherType");
            // 
            // xrCellNoOfCreated
            // 
            this.xrCellNoOfCreated.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.NO_CREATED", "{0:#,#}")});
            this.xrCellNoOfCreated.Name = "xrCellNoOfCreated";
            this.xrCellNoOfCreated.StyleName = "styleDateInfo";
            this.xrCellNoOfCreated.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCellNoOfCreated, "xrCellNoOfCreated");
            // 
            // xrCellNoOfModified
            // 
            this.xrCellNoOfModified.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.NO_MODIFIED", "{0:#,#}")});
            this.xrCellNoOfModified.Name = "xrCellNoOfModified";
            this.xrCellNoOfModified.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellNoOfModified.StylePriority.UsePadding = false;
            this.xrCellNoOfModified.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            this.xrCellNoOfModified.Summary = xrSummary1;
            resources.ApplyResources(this.xrCellNoOfModified, "xrCellNoOfModified");
            // 
            // xrCellNoOfDeleted
            // 
            this.xrCellNoOfDeleted.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.NO_DELETED", "{0:#,#}")});
            this.xrCellNoOfDeleted.Name = "xrCellNoOfDeleted";
            this.xrCellNoOfDeleted.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellNoOfDeleted.StylePriority.UsePadding = false;
            this.xrCellNoOfDeleted.StylePriority.UseTextAlignment = false;
            xrSummary2.IgnoreNullValues = true;
            this.xrCellNoOfDeleted.Summary = xrSummary2;
            resources.ApplyResources(this.xrCellNoOfDeleted, "xrCellNoOfDeleted");
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblAmountFilter,
            this.xrTblGrandTotal});
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
            // xrTblGrandTotal
            // 
            resources.ApplyResources(this.xrTblGrandTotal, "xrTblGrandTotal");
            this.xrTblGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblGrandTotal.Name = "xrTblGrandTotal";
            this.xrTblGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrGrandRow});
            this.xrTblGrandTotal.StyleName = "styleTotalRow";
            this.xrTblGrandTotal.StylePriority.UseBackColor = false;
            this.xrTblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrTblGrandTotal.StylePriority.UseBorders = false;
            this.xrTblGrandTotal.StylePriority.UseFont = false;
            this.xrTblGrandTotal.StylePriority.UsePadding = false;
            // 
            // xrGrandRow
            // 
            this.xrGrandRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotalCaption,
            this.xrCellSumNoOfCreated,
            this.xrCellSumNoOfModified,
            this.xrCellSumNoOfDeleted});
            this.xrGrandRow.Name = "xrGrandRow";
            resources.ApplyResources(this.xrGrandRow, "xrGrandRow");
            // 
            // xrGrandTotalCaption
            // 
            this.xrGrandTotalCaption.Name = "xrGrandTotalCaption";
            this.xrGrandTotalCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrGrandTotalCaption.StylePriority.UsePadding = false;
            this.xrGrandTotalCaption.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrGrandTotalCaption, "xrGrandTotalCaption");
            // 
            // xrCellSumNoOfCreated
            // 
            this.xrCellSumNoOfCreated.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.NO_CREATED")});
            this.xrCellSumNoOfCreated.Name = "xrCellSumNoOfCreated";
            this.xrCellSumNoOfCreated.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrCellSumNoOfCreated.Summary = xrSummary3;
            resources.ApplyResources(this.xrCellSumNoOfCreated, "xrCellSumNoOfCreated");
            // 
            // xrCellSumNoOfModified
            // 
            this.xrCellSumNoOfModified.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.NO_MODIFIED")});
            this.xrCellSumNoOfModified.Name = "xrCellSumNoOfModified";
            this.xrCellSumNoOfModified.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellSumNoOfModified.StylePriority.UsePadding = false;
            this.xrCellSumNoOfModified.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.IgnoreNullValues = true;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrCellSumNoOfModified.Summary = xrSummary4;
            resources.ApplyResources(this.xrCellSumNoOfModified, "xrCellSumNoOfModified");
            // 
            // xrCellSumNoOfDeleted
            // 
            this.xrCellSumNoOfDeleted.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.NO_DELETED")});
            this.xrCellSumNoOfDeleted.Name = "xrCellSumNoOfDeleted";
            this.xrCellSumNoOfDeleted.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellSumNoOfDeleted.StylePriority.UsePadding = false;
            this.xrCellSumNoOfDeleted.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary5, "xrSummary5");
            xrSummary5.IgnoreNullValues = true;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrCellSumNoOfDeleted.Summary = xrSummary5;
            resources.ApplyResources(this.xrCellSumNoOfDeleted, "xrCellSumNoOfDeleted");
            // 
            // xrBalance
            // 
            this.xrBalance.Name = "xrBalance";
            this.xrBalance.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary6, "xrSummary6");
            xrSummary6.IgnoreNullValues = true;
            this.xrBalance.Summary = xrSummary6;
            resources.ApplyResources(this.xrBalance, "xrBalance");
            // 
            // calCLBalance
            // 
            this.calCLBalance.DataMember = "CashBankFlow";
            this.calCLBalance.Name = "calCLBalance";
            // 
            // calGrandInFlow
            // 
            this.calGrandInFlow.DataMember = "CashBankFlow";
            this.calGrandInFlow.Expression = "Iif(IsNullOrEmpty([].Sum([IN_FLOW])), 0, [].Sum([IN_FLOW]))  + [Parameters.prOPBa" +
                "lance]";
            this.calGrandInFlow.Name = "calGrandInFlow";
            // 
            // calGrandOutFlow
            // 
            this.calGrandOutFlow.DataMember = "CashBankFlow";
            this.calGrandOutFlow.Expression = "Iif(IsNullOrEmpty([].Sum([OUT_FLOW])), 0, [].Sum([OUT_FLOW])) ";
            this.calGrandOutFlow.Name = "calGrandOutFlow";
            // 
            // AuditVoucherStatistics
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calCLBalance,
            this.calGrandInFlow,
            this.calGrandOutFlow});
            this.DataMember = "AuditLog";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrTblData;
        private DevExpress.XtraReports.UI.XRTableRow xrDataRow;
        private DevExpress.XtraReports.UI.XRTableCell xrCellNoOfCreated;
        private DevExpress.XtraReports.UI.XRTableCell xrCellNoOfModified;
        private DevExpress.XtraReports.UI.XRTableCell xrCellNoOfDeleted;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrGrandRow;
        private DevExpress.XtraReports.UI.XRTableCell xrCellSumNoOfModified;
        private DevExpress.XtraReports.UI.XRTableCell xrCellSumNoOfDeleted;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrHeaderRow;
        private DevExpress.XtraReports.UI.XRTableCell xrCapVoucherType;
        private DevExpress.XtraReports.UI.XRTableCell xrCapNoOfCreated;
        private DevExpress.XtraReports.UI.XRTableCell xrCapNoOfDeleted;
        private DevExpress.XtraReports.UI.XRTableCell xrBalance;
        private DevExpress.XtraReports.UI.CalculatedField calCLBalance;
        private DevExpress.XtraReports.UI.CalculatedField calGrandInFlow;
        private DevExpress.XtraReports.UI.CalculatedField calGrandOutFlow;
        private DevExpress.XtraReports.UI.XRLabel lblAmountFilter;
        private DevExpress.XtraReports.UI.XRTableCell xrCellVoucherType;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotalCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrCapNoOfModified;
        private DevExpress.XtraReports.UI.XRTableCell xrCellSumNoOfCreated;
    }
}
