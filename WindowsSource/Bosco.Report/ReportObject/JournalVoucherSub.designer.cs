namespace Bosco.Report.ReportObject
{
    partial class JournalVoucherSub
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalVoucherSub));
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrtblLedger = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblPaidto = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDebitAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCreditAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblRowNarration = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblNarrationGap = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtbcellNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpHeaderVoucherNo = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblVoucherNoCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblVoucherDateCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblVoucherDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblParticularsCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblDebitCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblCreditCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrlblInsAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.xrReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblInsName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrHeaderProjectName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrpicReportLogoLeft1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.grpFooter = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrTblRequireSignNote = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow42 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellRequireSignNote = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblTotalCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblSumDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblSumCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblcellSumofRupees = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblRuppee = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrrowgeneralnarration = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblcellbeing = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblGeneralNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader1 = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrCrossBandBox2 = new DevExpress.XtraReports.UI.XRCrossBandBox();
            this.xrCrossBandBox1 = new DevExpress.XtraReports.UI.XRCrossBandBox();
            this.xrSubreportCCDetails = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblRequireSignNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportCCDetails,
            this.xrtblLedger});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            this.Detail.AfterPrint += new System.EventHandler(this.Detail_AfterPrint);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.xrLabel14});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel13
            // 
            resources.ApplyResources(this.xrLabel13, "xrLabel13");
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UsePadding = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel14
            // 
            this.xrLabel14.CanShrink = true;
            resources.ApplyResources(this.xrLabel14, "xrLabel14");
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseForeColor = false;
            this.xrLabel14.StylePriority.UsePadding = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrtblLedger
            // 
            resources.ApplyResources(this.xrtblLedger, "xrtblLedger");
            this.xrtblLedger.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrtblLedger.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblLedger.BorderWidth = 1F;
            this.xrtblLedger.Name = "xrtblLedger";
            this.xrtblLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 1, 100F);
            this.xrtblLedger.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrtblRowNarration});
            this.xrtblLedger.StyleName = "styleRow";
            this.xrtblLedger.StylePriority.UseBorderColor = false;
            this.xrtblLedger.StylePriority.UseBorderDashStyle = false;
            this.xrtblLedger.StylePriority.UseBorders = false;
            this.xrtblLedger.StylePriority.UseBorderWidth = false;
            this.xrtblLedger.StylePriority.UseFont = false;
            this.xrtblLedger.StylePriority.UseForeColor = false;
            this.xrtblLedger.StylePriority.UsePadding = false;
            this.xrtblLedger.StylePriority.UseTextAlignment = false;
            this.xrtblLedger.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTable1_BeforePrint);
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow4.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblPaidto,
            this.xrDebitAmount,
            this.xrCreditAmount});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.StylePriority.UseBorderDashStyle = false;
            this.xrTableRow4.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            // 
            // xrtblPaidto
            // 
            this.xrtblPaidto.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrtblPaidto.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.LEDGER_NAME")});
            resources.ApplyResources(this.xrtblPaidto, "xrtblPaidto");
            this.xrtblPaidto.Name = "xrtblPaidto";
            this.xrtblPaidto.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 0, 100F);
            this.xrtblPaidto.StylePriority.UseBorders = false;
            this.xrtblPaidto.StylePriority.UseFont = false;
            this.xrtblPaidto.StylePriority.UsePadding = false;
            // 
            // xrDebitAmount
            // 
            this.xrDebitAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrDebitAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.DEBIT", "{0:n}")});
            resources.ApplyResources(this.xrDebitAmount, "xrDebitAmount");
            this.xrDebitAmount.Name = "xrDebitAmount";
            this.xrDebitAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 0, 100F);
            this.xrDebitAmount.StylePriority.UseBorders = false;
            this.xrDebitAmount.StylePriority.UseFont = false;
            this.xrDebitAmount.StylePriority.UsePadding = false;
            this.xrDebitAmount.StylePriority.UseTextAlignment = false;
            this.xrDebitAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrDebitAmount_BeforePrint);
            // 
            // xrCreditAmount
            // 
            this.xrCreditAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrCreditAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.CREDIT", "{0:n}")});
            resources.ApplyResources(this.xrCreditAmount, "xrCreditAmount");
            this.xrCreditAmount.Name = "xrCreditAmount";
            this.xrCreditAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 1, 0, 100F);
            this.xrCreditAmount.StylePriority.UseBorders = false;
            this.xrCreditAmount.StylePriority.UseFont = false;
            this.xrCreditAmount.StylePriority.UsePadding = false;
            this.xrCreditAmount.StylePriority.UseTextAlignment = false;
            this.xrCreditAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCreditAmount_BeforePrint);
            // 
            // xrtblRowNarration
            // 
            this.xrtblRowNarration.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrtblRowNarration.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblNarrationGap,
            this.xrtbcellNarration});
            this.xrtblRowNarration.Name = "xrtblRowNarration";
            this.xrtblRowNarration.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrtblRowNarration.StylePriority.UseBorderDashStyle = false;
            this.xrtblRowNarration.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrtblRowNarration, "xrtblRowNarration");
            // 
            // xrtblNarrationGap
            // 
            this.xrtblNarrationGap.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrtblNarrationGap.Name = "xrtblNarrationGap";
            this.xrtblNarrationGap.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrtblNarrationGap, "xrtblNarrationGap");
            // 
            // xrtbcellNarration
            // 
            this.xrtbcellNarration.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrtbcellNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.NARRATION")});
            resources.ApplyResources(this.xrtbcellNarration, "xrtbcellNarration");
            this.xrtbcellNarration.Name = "xrtbcellNarration";
            this.xrtbcellNarration.StylePriority.UseBorders = false;
            this.xrtbcellNarration.StylePriority.UseFont = false;
            this.xrtbcellNarration.StylePriority.UseTextAlignment = false;
            // 
            // grpHeaderVoucherNo
            // 
            this.grpHeaderVoucherNo.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.xrlblInsAddress,
            this.xrReportTitle,
            this.xrlblInsName,
            this.xrHeaderProjectName,
            this.xrpicReportLogoLeft1});
            this.grpHeaderVoucherNo.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("VOUCHER_DATE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("VOUCHER_NO", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpHeaderVoucherNo, "grpHeaderVoucherNo");
            this.grpHeaderVoucherNo.Name = "grpHeaderVoucherNo";
            // 
            // xrTable2
            // 
            resources.ApplyResources(this.xrTable2, "xrTable2");
            this.xrTable2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.BorderWidth = 1F;
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow5});
            this.xrTable2.StyleName = "styleRow";
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseBorderDashStyle = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseForeColor = false;
            this.xrTable2.StylePriority.UsePadding = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow1.BorderWidth = 1F;
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblVoucherNoCaption,
            this.lblVoucherNo,
            this.xrTableCell6,
            this.lblVoucherDateCaption,
            this.lblVoucherDate});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseBorderDashStyle = false;
            this.xrTableRow1.StylePriority.UseBorders = false;
            this.xrTableRow1.StylePriority.UseBorderWidth = false;
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // lblVoucherNoCaption
            // 
            this.lblVoucherNoCaption.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.lblVoucherNoCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.lblVoucherNoCaption, "lblVoucherNoCaption");
            this.lblVoucherNoCaption.Name = "lblVoucherNoCaption";
            this.lblVoucherNoCaption.StylePriority.UseBorderDashStyle = false;
            this.lblVoucherNoCaption.StylePriority.UseBorders = false;
            this.lblVoucherNoCaption.StylePriority.UseFont = false;
            // 
            // lblVoucherNo
            // 
            this.lblVoucherNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.VOUCHER_NO")});
            resources.ApplyResources(this.lblVoucherNo, "lblVoucherNo");
            this.lblVoucherNo.Name = "lblVoucherNo";
            this.lblVoucherNo.StylePriority.UseFont = false;
            this.lblVoucherNo.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblVoucherNo_EvaluateBinding);
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            resources.ApplyResources(this.xrTableCell6, "xrTableCell6");
            // 
            // lblVoucherDateCaption
            // 
            resources.ApplyResources(this.lblVoucherDateCaption, "lblVoucherDateCaption");
            this.lblVoucherDateCaption.Name = "lblVoucherDateCaption";
            this.lblVoucherDateCaption.StylePriority.UseFont = false;
            // 
            // lblVoucherDate
            // 
            this.lblVoucherDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.VOUCHER_DATE", "{0:d}")});
            resources.ApplyResources(this.lblVoucherDate, "lblVoucherDate");
            this.lblVoucherDate.Name = "lblVoucherDate";
            this.lblVoucherDate.StylePriority.UseFont = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblParticularsCaption,
            this.lblDebitCaption,
            this.lblCreditCaption});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.StylePriority.UseBorderDashStyle = false;
            resources.ApplyResources(this.xrTableRow5, "xrTableRow5");
            // 
            // lblParticularsCaption
            // 
            resources.ApplyResources(this.lblParticularsCaption, "lblParticularsCaption");
            this.lblParticularsCaption.Name = "lblParticularsCaption";
            this.lblParticularsCaption.StylePriority.UseFont = false;
            this.lblParticularsCaption.StylePriority.UseTextAlignment = false;
            // 
            // lblDebitCaption
            // 
            resources.ApplyResources(this.lblDebitCaption, "lblDebitCaption");
            this.lblDebitCaption.Name = "lblDebitCaption";
            this.lblDebitCaption.StylePriority.UseFont = false;
            this.lblDebitCaption.StylePriority.UseTextAlignment = false;
            // 
            // lblCreditCaption
            // 
            resources.ApplyResources(this.lblCreditCaption, "lblCreditCaption");
            this.lblCreditCaption.Name = "lblCreditCaption";
            this.lblCreditCaption.StylePriority.UseFont = false;
            this.lblCreditCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrlblInsAddress
            // 
            resources.ApplyResources(this.xrlblInsAddress, "xrlblInsAddress");
            this.xrlblInsAddress.Name = "xrlblInsAddress";
            this.xrlblInsAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblInsAddress.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblInsAddress.StylePriority.UseFont = false;
            this.xrlblInsAddress.StylePriority.UseTextAlignment = false;
            // 
            // xrReportTitle
            // 
            resources.ApplyResources(this.xrReportTitle, "xrReportTitle");
            this.xrReportTitle.Name = "xrReportTitle";
            this.xrReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrReportTitle.StylePriority.UseFont = false;
            this.xrReportTitle.StylePriority.UsePadding = false;
            this.xrReportTitle.StylePriority.UseTextAlignment = false;
            // 
            // xrlblInsName
            // 
            resources.ApplyResources(this.xrlblInsName, "xrlblInsName");
            this.xrlblInsName.Name = "xrlblInsName";
            this.xrlblInsName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblInsName.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblInsName.StylePriority.UseFont = false;
            this.xrlblInsName.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderProjectName
            // 
            this.xrHeaderProjectName.CanShrink = true;
            resources.ApplyResources(this.xrHeaderProjectName, "xrHeaderProjectName");
            this.xrHeaderProjectName.Name = "xrHeaderProjectName";
            this.xrHeaderProjectName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrHeaderProjectName.StylePriority.UseFont = false;
            this.xrHeaderProjectName.StylePriority.UseForeColor = false;
            this.xrHeaderProjectName.StylePriority.UsePadding = false;
            this.xrHeaderProjectName.StylePriority.UseTextAlignment = false;
            // 
            // xrpicReportLogoLeft1
            // 
            resources.ApplyResources(this.xrpicReportLogoLeft1, "xrpicReportLogoLeft1");
            this.xrpicReportLogoLeft1.Name = "xrpicReportLogoLeft1";
            this.xrpicReportLogoLeft1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 1, 0, 100F);
            this.xrpicReportLogoLeft1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrpicReportLogoLeft1.StylePriority.UsePadding = false;
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubSignFooter,
            this.xrTblRequireSignNote,
            this.xrTable3});
            resources.ApplyResources(this.grpFooter, "grpFooter");
            this.grpFooter.Name = "grpFooter";
            // 
            // xrSubSignFooter
            // 
            resources.ApplyResources(this.xrSubSignFooter, "xrSubSignFooter");
            this.xrSubSignFooter.Name = "xrSubSignFooter";
            this.xrSubSignFooter.ReportSource = new Bosco.Report.ReportObject.SignReportFooter();
            // 
            // xrTblRequireSignNote
            // 
            this.xrTblRequireSignNote.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrTblRequireSignNote, "xrTblRequireSignNote");
            this.xrTblRequireSignNote.Name = "xrTblRequireSignNote";
            this.xrTblRequireSignNote.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow42});
            this.xrTblRequireSignNote.StylePriority.UseBorders = false;
            this.xrTblRequireSignNote.StylePriority.UseFont = false;
            this.xrTblRequireSignNote.StylePriority.UseTextAlignment = false;
            this.xrTblRequireSignNote.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTblRequireSignNote_BeforePrint);
            // 
            // xrTableRow42
            // 
            this.xrTableRow42.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellRequireSignNote});
            this.xrTableRow42.Name = "xrTableRow42";
            resources.ApplyResources(this.xrTableRow42, "xrTableRow42");
            // 
            // xrCellRequireSignNote
            // 
            resources.ApplyResources(this.xrCellRequireSignNote, "xrCellRequireSignNote");
            this.xrCellRequireSignNote.Name = "xrCellRequireSignNote";
            this.xrCellRequireSignNote.StylePriority.UseFont = false;
            this.xrCellRequireSignNote.StylePriority.UseTextAlignment = false;
            // 
            // xrTable3
            // 
            resources.ApplyResources(this.xrTable3, "xrTable3");
            this.xrTable3.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.BorderWidth = 1F;
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow6,
            this.xrrowgeneralnarration});
            this.xrTable3.StyleName = "styleRow";
            this.xrTable3.StylePriority.UseBorderColor = false;
            this.xrTable3.StylePriority.UseBorderDashStyle = false;
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseBorderWidth = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseForeColor = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTable3_BeforePrint);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblTotalCaption,
            this.lblSumDebit,
            this.lblSumCredit});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseBorderDashStyle = false;
            this.xrTableRow3.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // lblTotalCaption
            // 
            this.lblTotalCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.lblTotalCaption, "lblTotalCaption");
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.StylePriority.UseBorders = false;
            this.lblTotalCaption.StylePriority.UseFont = false;
            this.lblTotalCaption.StylePriority.UseTextAlignment = false;
            // 
            // lblSumDebit
            // 
            resources.ApplyResources(this.lblSumDebit, "lblSumDebit");
            this.lblSumDebit.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.lblSumDebit.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblSumDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.DEBIT")});
            this.lblSumDebit.Name = "lblSumDebit";
            this.lblSumDebit.StylePriority.UseBorderColor = false;
            this.lblSumDebit.StylePriority.UseBorderDashStyle = false;
            this.lblSumDebit.StylePriority.UseBorders = false;
            this.lblSumDebit.StylePriority.UseFont = false;
            this.lblSumDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.lblSumDebit.Summary = xrSummary1;
            this.lblSumDebit.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrTableCell18_SummaryGetResult);
            this.lblSumDebit.SummaryRowChanged += new System.EventHandler(this.xrTableCell18_SummaryRowChanged);
            this.lblSumDebit.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell18_BeforePrint);
            // 
            // lblSumCredit
            // 
            resources.ApplyResources(this.lblSumCredit, "lblSumCredit");
            this.lblSumCredit.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.lblSumCredit.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblSumCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.CREDIT")});
            this.lblSumCredit.Name = "lblSumCredit";
            this.lblSumCredit.StylePriority.UseBorderColor = false;
            this.lblSumCredit.StylePriority.UseBorderDashStyle = false;
            this.lblSumCredit.StylePriority.UseBorders = false;
            this.lblSumCredit.StylePriority.UseFont = false;
            this.lblSumCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.lblSumCredit.Summary = xrSummary2;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblcellSumofRupees,
            this.xrtblRuppee});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.StylePriority.UseBorderDashStyle = false;
            resources.ApplyResources(this.xrTableRow6, "xrTableRow6");
            // 
            // xrtblcellSumofRupees
            // 
            resources.ApplyResources(this.xrtblcellSumofRupees, "xrtblcellSumofRupees");
            this.xrtblcellSumofRupees.Name = "xrtblcellSumofRupees";
            this.xrtblcellSumofRupees.StylePriority.UseFont = false;
            // 
            // xrtblRuppee
            // 
            this.xrtblRuppee.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblRuppee.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.RUPPEE_AMT")});
            resources.ApplyResources(this.xrtblRuppee, "xrtblRuppee");
            this.xrtblRuppee.Name = "xrtblRuppee";
            this.xrtblRuppee.StylePriority.UseBorders = false;
            this.xrtblRuppee.StylePriority.UseFont = false;
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblRuppee.Summary = xrSummary3;
            this.xrtblRuppee.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrtblRuppee_SummaryGetResult);
            this.xrtblRuppee.SummaryReset += new System.EventHandler(this.xrtblRuppee_SummaryReset);
            this.xrtblRuppee.SummaryRowChanged += new System.EventHandler(this.xrtblRuppee_SummaryRowChanged);
            // 
            // xrrowgeneralnarration
            // 
            this.xrrowgeneralnarration.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblcellbeing,
            this.xrtblGeneralNarration});
            this.xrrowgeneralnarration.Name = "xrrowgeneralnarration";
            resources.ApplyResources(this.xrrowgeneralnarration, "xrrowgeneralnarration");
            // 
            // xrtblcellbeing
            // 
            this.xrtblcellbeing.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            resources.ApplyResources(this.xrtblcellbeing, "xrtblcellbeing");
            this.xrtblcellbeing.Name = "xrtblcellbeing";
            this.xrtblcellbeing.StylePriority.UseBorderDashStyle = false;
            this.xrtblcellbeing.StylePriority.UseFont = false;
            // 
            // xrtblGeneralNarration
            // 
            this.xrtblGeneralNarration.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrtblGeneralNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.GENERAL_NARRATION")});
            resources.ApplyResources(this.xrtblGeneralNarration, "xrtblGeneralNarration");
            this.xrtblGeneralNarration.Name = "xrtblGeneralNarration";
            this.xrtblGeneralNarration.StylePriority.UseBorderDashStyle = false;
            this.xrtblGeneralNarration.StylePriority.UseFont = false;
            // 
            // PageHeader1
            // 
            resources.ApplyResources(this.PageHeader1, "PageHeader1");
            this.PageHeader1.Name = "PageHeader1";
            // 
            // xrCrossBandBox2
            // 
            this.xrCrossBandBox2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCrossBandBox2.BorderWidth = 1F;
            this.xrCrossBandBox2.EndBand = this.grpFooter;
            resources.ApplyResources(this.xrCrossBandBox2, "xrCrossBandBox2");
            this.xrCrossBandBox2.Name = "xrCrossBandBox2";
            this.xrCrossBandBox2.StartBand = this.grpFooter;
            this.xrCrossBandBox2.WidthF = 161.9396F;
            // 
            // xrCrossBandBox1
            // 
            this.xrCrossBandBox1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCrossBandBox1.BorderWidth = 1F;
            this.xrCrossBandBox1.EndBand = this.grpFooter;
            resources.ApplyResources(this.xrCrossBandBox1, "xrCrossBandBox1");
            this.xrCrossBandBox1.Name = "xrCrossBandBox1";
            this.xrCrossBandBox1.StartBand = this.grpFooter;
            this.xrCrossBandBox1.WidthF = 161.0604F;
            // 
            // xrSubreportCCDetails
            // 
            this.xrSubreportCCDetails.CanShrink = true;
            resources.ApplyResources(this.xrSubreportCCDetails, "xrSubreportCCDetails");
            this.xrSubreportCCDetails.Name = "xrSubreportCCDetails";
            this.xrSubreportCCDetails.ReportSource = new Bosco.Report.ReportObject.UcCCDetail();
            // 
            // JournalVoucherSub
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpHeaderVoucherNo,
            this.grpFooter,
            this.PageHeader1});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandBox1,
            this.xrCrossBandBox2});
            this.DataMember = "CashBankReceipts";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.PageHeader1, 0);
            this.Controls.SetChildIndex(this.grpFooter, 0);
            this.Controls.SetChildIndex(this.grpHeaderVoucherNo, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblRequireSignNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel14;
        private DevExpress.XtraReports.UI.XRTable xrtblLedger;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrtblPaidto;
        private DevExpress.XtraReports.UI.XRTableCell xrCreditAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrDebitAmount;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderVoucherNo;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell lblVoucherNoCaption;
        private DevExpress.XtraReports.UI.XRTableCell lblVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell lblVoucherDateCaption;
        private DevExpress.XtraReports.UI.XRTableCell lblVoucherDate;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell lblParticularsCaption;
        private DevExpress.XtraReports.UI.XRTableCell lblDebitCaption;
        private DevExpress.XtraReports.UI.XRTableCell lblCreditCaption;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell lblTotalCaption;
        private DevExpress.XtraReports.UI.XRTableCell lblSumDebit;
        private DevExpress.XtraReports.UI.XRTableCell lblSumCredit;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrtblRuppee;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader1;
        private DevExpress.XtraReports.UI.XRLabel xrReportTitle;
        private DevExpress.XtraReports.UI.XRLabel xrHeaderProjectName;
        private DevExpress.XtraReports.UI.XRLabel xrlblInsAddress;
        private DevExpress.XtraReports.UI.XRLabel xrlblInsName;
        private DevExpress.XtraReports.UI.XRPictureBox xrpicReportLogoLeft1;
        private DevExpress.XtraReports.UI.XRTableRow xrtblRowNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrtbcellNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrtblcellSumofRupees;
        private DevExpress.XtraReports.UI.XRTableRow xrrowgeneralnarration;
        private DevExpress.XtraReports.UI.XRTableCell xrtblcellbeing;
        private DevExpress.XtraReports.UI.XRTableCell xrtblGeneralNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrtblNarrationGap;
        private DevExpress.XtraReports.UI.XRCrossBandBox xrCrossBandBox2;
        private DevExpress.XtraReports.UI.XRCrossBandBox xrCrossBandBox1;
        private DevExpress.XtraReports.UI.XRTable xrTblRequireSignNote;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow42;
        private DevExpress.XtraReports.UI.XRTableCell xrCellRequireSignNote;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportCCDetails;
    }
}
