namespace Bosco.Report.ReportObject
{
    partial class NegativeCashBankBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NegativeCashBankBalance));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrHeaderRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapProject = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCash = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTblData = new DevExpress.XtraReports.UI.XRTable();
            this.xrDataRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellProject = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellCash = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblAmountFilter = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrGrandRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotal1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandTotal2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandTotalCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSumCash = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellSumBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrBalance = new DevExpress.XtraReports.UI.XRTableCell();
            this.calCLBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGrandInFlow = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGrandOutFlow = new DevExpress.XtraReports.UI.CalculatedField();
            this.grpDateHeader = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrGrpTblDateHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrpCellDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrpCellProject = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrGrpTblDateHeader)).BeginInit();
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
            this.xrCapDate,
            this.xrCapProject,
            this.xrCapLedgerName,
            this.xrCapCash,
            this.xrCapBank});
            this.xrHeaderRow.Name = "xrHeaderRow";
            resources.ApplyResources(this.xrHeaderRow, "xrHeaderRow");
            // 
            // xrCapDate
            // 
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            this.xrCapDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapDate.StylePriority.UseBackColor = false;
            this.xrCapDate.StylePriority.UseBorderColor = false;
            this.xrCapDate.StylePriority.UseBorders = false;
            this.xrCapDate.StylePriority.UseFont = false;
            this.xrCapDate.StylePriority.UsePadding = false;
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            // 
            // xrCapProject
            // 
            this.xrCapProject.Name = "xrCapProject";
            this.xrCapProject.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapProject.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrCapProject, "xrCapProject");
            // 
            // xrCapLedgerName
            // 
            this.xrCapLedgerName.Name = "xrCapLedgerName";
            this.xrCapLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapLedgerName.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrCapLedgerName, "xrCapLedgerName");
            // 
            // xrCapCash
            // 
            resources.ApplyResources(this.xrCapCash, "xrCapCash");
            this.xrCapCash.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCash.Name = "xrCapCash";
            this.xrCapCash.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapCash.StylePriority.UseBackColor = false;
            this.xrCapCash.StylePriority.UseBorderColor = false;
            this.xrCapCash.StylePriority.UseBorders = false;
            this.xrCapCash.StylePriority.UseFont = false;
            this.xrCapCash.StylePriority.UsePadding = false;
            this.xrCapCash.StylePriority.UseTextAlignment = false;
            // 
            // xrCapBank
            // 
            resources.ApplyResources(this.xrCapBank, "xrCapBank");
            this.xrCapBank.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapBank.Name = "xrCapBank";
            this.xrCapBank.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapBank.StylePriority.UseBackColor = false;
            this.xrCapBank.StylePriority.UseBorderColor = false;
            this.xrCapBank.StylePriority.UseBorders = false;
            this.xrCapBank.StylePriority.UseFont = false;
            this.xrCapBank.StylePriority.UsePadding = false;
            this.xrCapBank.StylePriority.UseTextAlignment = false;
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
            this.xrCellDate,
            this.xrCellProject,
            this.xrCellLedgerName,
            this.xrCellCash,
            this.xrCellBank});
            this.xrDataRow.Name = "xrDataRow";
            resources.ApplyResources(this.xrDataRow, "xrDataRow");
            // 
            // xrCellDate
            // 
            this.xrCellDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NegativeCashBankBalance.VOUCHER_DATE", "{0:d}")});
            this.xrCellDate.Name = "xrCellDate";
            this.xrCellDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellDate.StylePriority.UsePadding = false;
            this.xrCellDate.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCellDate, "xrCellDate");
            // 
            // xrCellProject
            // 
            this.xrCellProject.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NegativeCashBankBalance.PROJECT")});
            this.xrCellProject.Name = "xrCellProject";
            this.xrCellProject.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellProject.StylePriority.UsePadding = false;
            this.xrCellProject.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCellProject, "xrCellProject");
            // 
            // xrCellLedgerName
            // 
            this.xrCellLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NegativeCashBankBalance.LEDGER_NAME", "{0:d}")});
            this.xrCellLedgerName.Name = "xrCellLedgerName";
            this.xrCellLedgerName.StyleName = "styleDateInfo";
            this.xrCellLedgerName.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCellLedgerName, "xrCellLedgerName");
            // 
            // xrCellCash
            // 
            this.xrCellCash.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NegativeCashBankBalance.CASH", "{0:n}")});
            this.xrCellCash.Name = "xrCellCash";
            this.xrCellCash.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellCash.StylePriority.UsePadding = false;
            this.xrCellCash.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            this.xrCellCash.Summary = xrSummary1;
            resources.ApplyResources(this.xrCellCash, "xrCellCash");
            this.xrCellCash.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCellCash_BeforePrint);
            // 
            // xrCellBank
            // 
            this.xrCellBank.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NegativeCashBankBalance.BANK", "{0:n}")});
            this.xrCellBank.Name = "xrCellBank";
            this.xrCellBank.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellBank.StylePriority.UsePadding = false;
            this.xrCellBank.StylePriority.UseTextAlignment = false;
            xrSummary2.IgnoreNullValues = true;
            this.xrCellBank.Summary = xrSummary2;
            resources.ApplyResources(this.xrCellBank, "xrCellBank");
            this.xrCellBank.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCellBank_BeforePrint);
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
            this.xrGrandTotal1,
            this.xrGrandTotal2,
            this.xrGrandTotalCaption,
            this.xrCellSumCash,
            this.xrCellSumBank});
            this.xrGrandRow.Name = "xrGrandRow";
            resources.ApplyResources(this.xrGrandRow, "xrGrandRow");
            // 
            // xrGrandTotal1
            // 
            this.xrGrandTotal1.Name = "xrGrandTotal1";
            this.xrGrandTotal1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrGrandTotal1.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrGrandTotal1, "xrGrandTotal1");
            // 
            // xrGrandTotal2
            // 
            this.xrGrandTotal2.Name = "xrGrandTotal2";
            this.xrGrandTotal2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrGrandTotal2.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrGrandTotal2, "xrGrandTotal2");
            // 
            // xrGrandTotalCaption
            // 
            this.xrGrandTotalCaption.Name = "xrGrandTotalCaption";
            this.xrGrandTotalCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrGrandTotalCaption.StylePriority.UsePadding = false;
            this.xrGrandTotalCaption.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrGrandTotalCaption, "xrGrandTotalCaption");
            // 
            // xrCellSumCash
            // 
            this.xrCellSumCash.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NegativeCashBankBalance.CASH")});
            this.xrCellSumCash.Name = "xrCellSumCash";
            this.xrCellSumCash.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellSumCash.StylePriority.UsePadding = false;
            this.xrCellSumCash.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrCellSumCash.Summary = xrSummary3;
            resources.ApplyResources(this.xrCellSumCash, "xrCellSumCash");
            // 
            // xrCellSumBank
            // 
            this.xrCellSumBank.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NegativeCashBankBalance.BANK")});
            this.xrCellSumBank.Name = "xrCellSumBank";
            this.xrCellSumBank.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellSumBank.StylePriority.UsePadding = false;
            this.xrCellSumBank.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.IgnoreNullValues = true;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrCellSumBank.Summary = xrSummary4;
            resources.ApplyResources(this.xrCellSumBank, "xrCellSumBank");
            // 
            // xrBalance
            // 
            this.xrBalance.Name = "xrBalance";
            this.xrBalance.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary5, "xrSummary5");
            xrSummary5.IgnoreNullValues = true;
            this.xrBalance.Summary = xrSummary5;
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
            // grpDateHeader
            // 
            this.grpDateHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrGrpTblDateHeader});
            this.grpDateHeader.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("VOUCHER_DATE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpDateHeader, "grpDateHeader");
            this.grpDateHeader.Name = "grpDateHeader";
            // 
            // xrGrpTblDateHeader
            // 
            resources.ApplyResources(this.xrGrpTblDateHeader, "xrGrpTblDateHeader");
            this.xrGrpTblDateHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrGrpTblDateHeader.Name = "xrGrpTblDateHeader";
            this.xrGrpTblDateHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGrpTblDateHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrGrpTblDateHeader.StyleName = "styleRow";
            this.xrGrpTblDateHeader.StylePriority.UseBorderColor = false;
            this.xrGrpTblDateHeader.StylePriority.UseBorders = false;
            this.xrGrpTblDateHeader.StylePriority.UseFont = false;
            this.xrGrpTblDateHeader.StylePriority.UsePadding = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrpCellDate,
            this.xrGrpCellProject,
            this.xrTableCell3,
            this.xrTableCell2,
            this.xrTableCell1});
            this.xrTableRow4.Name = "xrTableRow4";
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            // 
            // xrGrpCellDate
            // 
            this.xrGrpCellDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NegativeCashBankBalance.VOUCHER_DATE", "{0:d}")});
            resources.ApplyResources(this.xrGrpCellDate, "xrGrpCellDate");
            this.xrGrpCellDate.Name = "xrGrpCellDate";
            this.xrGrpCellDate.StyleName = "styleDateInfo";
            this.xrGrpCellDate.StylePriority.UseFont = false;
            this.xrGrpCellDate.StylePriority.UseTextAlignment = false;
            // 
            // xrGrpCellProject
            // 
            this.xrGrpCellProject.Name = "xrGrpCellProject";
            this.xrGrpCellProject.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrGrpCellProject.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrGrpCellProject, "xrGrpCellProject");
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell3.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            // 
            // xrTableCell2
            // 
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary6, "xrSummary6");
            xrSummary6.IgnoreNullValues = true;
            this.xrTableCell2.Summary = xrSummary6;
            // 
            // xrTableCell1
            // 
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary7, "xrSummary7");
            xrSummary7.IgnoreNullValues = true;
            this.xrTableCell1.Summary = xrSummary7;
            // 
            // NegativeCashBankBalance
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grpDateHeader});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calCLBalance,
            this.calGrandInFlow,
            this.calGrandOutFlow});
            this.DataMember = "NegativeCashBankBalance";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpDateHeader, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrGrpTblDateHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrTblData;
        private DevExpress.XtraReports.UI.XRTableRow xrDataRow;
        private DevExpress.XtraReports.UI.XRTableCell xrCellLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrCellCash;
        private DevExpress.XtraReports.UI.XRTableCell xrCellBank;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrGrandRow;
        private DevExpress.XtraReports.UI.XRTableCell xrCellSumCash;
        private DevExpress.XtraReports.UI.XRTableCell xrCellSumBank;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrHeaderRow;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCash;
        private DevExpress.XtraReports.UI.XRTableCell xrCapBank;
        private DevExpress.XtraReports.UI.XRTableCell xrBalance;
        private DevExpress.XtraReports.UI.CalculatedField calCLBalance;
        private DevExpress.XtraReports.UI.CalculatedField calGrandInFlow;
        private DevExpress.XtraReports.UI.CalculatedField calGrandOutFlow;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpDateHeader;
        private DevExpress.XtraReports.UI.XRLabel lblAmountFilter;
        private DevExpress.XtraReports.UI.XRTable xrGrpTblDateHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrGrpCellDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCellDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrCellProject;
        private DevExpress.XtraReports.UI.XRTableCell xrCapProject;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotalCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrGrpCellProject;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal1;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal2;
    }
}
