namespace Bosco.Report.ReportObject
{
    partial class AuditVoucherList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditVoucherList));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCashBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapRefNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapThirdParty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAction = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCreatedBy = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapModifiedBy = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrTableSource = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrVNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrVType = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRefNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAction = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCreatedBy = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrModifiedBy = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting2 = new Bosco.Report.ReportSetting();
            this.calDebit = new DevExpress.XtraReports.UI.CalculatedField();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblFooter = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblAmountFilter = new DevExpress.XtraReports.UI.XRLabel();
            this.calCredit = new DevExpress.XtraReports.UI.CalculatedField();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblFooter)).BeginInit();
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
            this.xrCapParticulars,
            this.xrCapCashBank,
            this.xrCapRefNo,
            this.xrCapNarration,
            this.xrCapAmount,
            this.xrCapThirdParty,
            this.xrCapAction,
            this.xrCapCreatedBy,
            this.xrCapModifiedBy});
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
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell1.StylePriority.UseBackColor = false;
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            // 
            // xrTableCell3
            // 
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell3.StylePriority.UseBackColor = false;
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            // 
            // xrCapParticulars
            // 
            resources.ApplyResources(this.xrCapParticulars, "xrCapParticulars");
            this.xrCapParticulars.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapParticulars.Name = "xrCapParticulars";
            this.xrCapParticulars.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapParticulars.StylePriority.UseBackColor = false;
            this.xrCapParticulars.StylePriority.UseBorderColor = false;
            this.xrCapParticulars.StylePriority.UseBorders = false;
            this.xrCapParticulars.StylePriority.UseFont = false;
            this.xrCapParticulars.StylePriority.UsePadding = false;
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
            this.xrCapCashBank.StylePriority.UseFont = false;
            // 
            // xrCapRefNo
            // 
            resources.ApplyResources(this.xrCapRefNo, "xrCapRefNo");
            this.xrCapRefNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapRefNo.Name = "xrCapRefNo";
            this.xrCapRefNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapRefNo.StylePriority.UseBackColor = false;
            this.xrCapRefNo.StylePriority.UseBorderColor = false;
            this.xrCapRefNo.StylePriority.UseBorders = false;
            this.xrCapRefNo.StylePriority.UseFont = false;
            this.xrCapRefNo.StylePriority.UsePadding = false;
            // 
            // xrCapNarration
            // 
            resources.ApplyResources(this.xrCapNarration, "xrCapNarration");
            this.xrCapNarration.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapNarration.Name = "xrCapNarration";
            this.xrCapNarration.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapNarration.StylePriority.UseBackColor = false;
            this.xrCapNarration.StylePriority.UseBorderColor = false;
            this.xrCapNarration.StylePriority.UseBorders = false;
            this.xrCapNarration.StylePriority.UseFont = false;
            this.xrCapNarration.StylePriority.UsePadding = false;
            this.xrCapNarration.StylePriority.UseTextAlignment = false;
            // 
            // xrCapAmount
            // 
            resources.ApplyResources(this.xrCapAmount, "xrCapAmount");
            this.xrCapAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapAmount.Name = "xrCapAmount";
            this.xrCapAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapAmount.StylePriority.UseBackColor = false;
            this.xrCapAmount.StylePriority.UseBorderColor = false;
            this.xrCapAmount.StylePriority.UseBorders = false;
            this.xrCapAmount.StylePriority.UseFont = false;
            this.xrCapAmount.StylePriority.UsePadding = false;
            this.xrCapAmount.StylePriority.UseTextAlignment = false;
            // 
            // xrCapThirdParty
            // 
            resources.ApplyResources(this.xrCapThirdParty, "xrCapThirdParty");
            this.xrCapThirdParty.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapThirdParty.Name = "xrCapThirdParty";
            this.xrCapThirdParty.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapThirdParty.StylePriority.UseBackColor = false;
            this.xrCapThirdParty.StylePriority.UseBorderColor = false;
            this.xrCapThirdParty.StylePriority.UseBorders = false;
            this.xrCapThirdParty.StylePriority.UseFont = false;
            this.xrCapThirdParty.StylePriority.UsePadding = false;
            // 
            // xrCapAction
            // 
            resources.ApplyResources(this.xrCapAction, "xrCapAction");
            this.xrCapAction.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapAction.Name = "xrCapAction";
            this.xrCapAction.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapAction.StylePriority.UseBackColor = false;
            this.xrCapAction.StylePriority.UseBorderColor = false;
            this.xrCapAction.StylePriority.UseBorders = false;
            this.xrCapAction.StylePriority.UseFont = false;
            this.xrCapAction.StylePriority.UsePadding = false;
            // 
            // xrCapCreatedBy
            // 
            resources.ApplyResources(this.xrCapCreatedBy, "xrCapCreatedBy");
            this.xrCapCreatedBy.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCreatedBy.Name = "xrCapCreatedBy";
            this.xrCapCreatedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapCreatedBy.StylePriority.UseBackColor = false;
            this.xrCapCreatedBy.StylePriority.UseBorderColor = false;
            this.xrCapCreatedBy.StylePriority.UseBorders = false;
            this.xrCapCreatedBy.StylePriority.UseFont = false;
            this.xrCapCreatedBy.StylePriority.UsePadding = false;
            // 
            // xrCapModifiedBy
            // 
            resources.ApplyResources(this.xrCapModifiedBy, "xrCapModifiedBy");
            this.xrCapModifiedBy.Name = "xrCapModifiedBy";
            this.xrCapModifiedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapModifiedBy.StylePriority.UseBackColor = false;
            this.xrCapModifiedBy.StylePriority.UseBorderColor = false;
            this.xrCapModifiedBy.StylePriority.UseFont = false;
            this.xrCapModifiedBy.StylePriority.UsePadding = false;
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
            this.xrVNo,
            this.xrVType,
            this.xrLedgerName,
            this.xrCashBank,
            this.xrRefNo,
            this.xrNarration,
            this.xrDebit,
            this.xrTableCell4,
            this.xrAction,
            this.xrCreatedBy,
            this.xrModifiedBy});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrDate
            // 
            resources.ApplyResources(this.xrDate, "xrDate");
            this.xrDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.VOUCHER_DATE", "{0:d}")});
            this.xrDate.Name = "xrDate";
            this.xrDate.StyleName = "styleDateInfo";
            this.xrDate.StylePriority.UseBorderColor = false;
            this.xrDate.StylePriority.UseBorders = false;
            this.xrDate.StylePriority.UseFont = false;
            this.xrDate.StylePriority.UseTextAlignment = false;
            // 
            // xrVNo
            // 
            this.xrVNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.VOUCHER_NO")});
            resources.ApplyResources(this.xrVNo, "xrVNo");
            this.xrVNo.Name = "xrVNo";
            this.xrVNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrVNo.StylePriority.UseFont = false;
            this.xrVNo.StylePriority.UsePadding = false;
            this.xrVNo.StylePriority.UseTextAlignment = false;
            // 
            // xrVType
            // 
            this.xrVType.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.VOUCHER_TYPE")});
            resources.ApplyResources(this.xrVType, "xrVType");
            this.xrVType.Name = "xrVType";
            this.xrVType.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrVType.StylePriority.UseFont = false;
            this.xrVType.StylePriority.UsePadding = false;
            this.xrVType.StylePriority.UseTextAlignment = false;
            // 
            // xrLedgerName
            // 
            resources.ApplyResources(this.xrLedgerName, "xrLedgerName");
            this.xrLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.LEDGER_NAME")});
            this.xrLedgerName.Name = "xrLedgerName";
            this.xrLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrLedgerName.StylePriority.UseBorderColor = false;
            this.xrLedgerName.StylePriority.UseBorders = false;
            this.xrLedgerName.StylePriority.UseFont = false;
            this.xrLedgerName.StylePriority.UsePadding = false;
            this.xrLedgerName.StylePriority.UseTextAlignment = false;
            // 
            // xrCashBank
            // 
            this.xrCashBank.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.CASH_BANK")});
            resources.ApplyResources(this.xrCashBank, "xrCashBank");
            this.xrCashBank.Name = "xrCashBank";
            this.xrCashBank.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCashBank.StylePriority.UseFont = false;
            this.xrCashBank.StylePriority.UsePadding = false;
            this.xrCashBank.StylePriority.UseTextAlignment = false;
            // 
            // xrRefNo
            // 
            this.xrRefNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.CHEQUE_NO")});
            resources.ApplyResources(this.xrRefNo, "xrRefNo");
            this.xrRefNo.Name = "xrRefNo";
            this.xrRefNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrRefNo.StylePriority.UseFont = false;
            this.xrRefNo.StylePriority.UsePadding = false;
            this.xrRefNo.StylePriority.UseTextAlignment = false;
            // 
            // xrNarration
            // 
            this.xrNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.NARRATION")});
            this.xrNarration.Name = "xrNarration";
            this.xrNarration.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrNarration.StylePriority.UsePadding = false;
            this.xrNarration.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrNarration, "xrNarration");
            // 
            // xrDebit
            // 
            resources.ApplyResources(this.xrDebit, "xrDebit");
            this.xrDebit.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.AMOUNT", "{0:n}")});
            this.xrDebit.Name = "xrDebit";
            this.xrDebit.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrDebit.StylePriority.UseBorderColor = false;
            this.xrDebit.StylePriority.UseBorders = false;
            this.xrDebit.StylePriority.UseFont = false;
            this.xrDebit.StylePriority.UsePadding = false;
            this.xrDebit.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell4
            // 
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.CLIENT_CODE")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            // 
            // xrAction
            // 
            this.xrAction.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.AUDIT_ACTION")});
            resources.ApplyResources(this.xrAction, "xrAction");
            this.xrAction.Name = "xrAction";
            this.xrAction.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrAction.StylePriority.UseFont = false;
            this.xrAction.StylePriority.UsePadding = false;
            this.xrAction.StylePriority.UseTextAlignment = false;
            // 
            // xrCreatedBy
            // 
            this.xrCreatedBy.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.CREATED_BY_NAME")});
            resources.ApplyResources(this.xrCreatedBy, "xrCreatedBy");
            this.xrCreatedBy.Name = "xrCreatedBy";
            this.xrCreatedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCreatedBy.StylePriority.UseFont = false;
            this.xrCreatedBy.StylePriority.UsePadding = false;
            this.xrCreatedBy.StylePriority.UseTextAlignment = false;
            // 
            // xrModifiedBy
            // 
            this.xrModifiedBy.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.MODIFIED_BY_NAME")});
            resources.ApplyResources(this.xrModifiedBy, "xrModifiedBy");
            this.xrModifiedBy.Name = "xrModifiedBy";
            this.xrModifiedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrModifiedBy.StylePriority.UseFont = false;
            this.xrModifiedBy.StylePriority.UsePadding = false;
            this.xrModifiedBy.StylePriority.UseTextAlignment = false;
            // 
            // reportSetting2
            // 
            this.reportSetting2.DataSetName = "ReportSetting";
            this.reportSetting2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.xrtblFooter,
            this.lblAmountFilter});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrtblFooter
            // 
            resources.ApplyResources(this.xrtblFooter, "xrtblFooter");
            this.xrtblFooter.Name = "xrtblFooter";
            this.xrtblFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblFooter.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblFooter.StyleName = "styleGroupRow";
            this.xrtblFooter.StylePriority.UsePadding = false;
            this.xrtblFooter.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell2,
            this.xrTableCell11});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrTableCell8
            // 
            resources.ApplyResources(this.xrTableCell8, "xrTableCell8");
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 5, 2, 2, 100F);
            this.xrTableCell8.StylePriority.UseBackColor = false;
            this.xrTableCell8.StylePriority.UseBorderColor = false;
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell2
            // 
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AuditLog.AMOUNT")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell2.StylePriority.UseBackColor = false;
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell2.Summary = xrSummary1;
            // 
            // xrTableCell11
            // 
            resources.ApplyResources(this.xrTableCell11, "xrTableCell11");
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell11.StylePriority.UseBackColor = false;
            this.xrTableCell11.StylePriority.UseBorderColor = false;
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UsePadding = false;
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
            // AuditVoucherList
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calDebit,
            this.calCredit});
            this.DataMember = "AuditLog";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrCapAmount;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrTableSource;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrDebit;
        private ReportSetting reportSetting2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrVNo;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrCapRefNo;
        private DevExpress.XtraReports.UI.XRTableCell xrVType;
        private DevExpress.XtraReports.UI.XRTableCell xrRefNo;
        private DevExpress.XtraReports.UI.CalculatedField calDebit;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.CalculatedField calCredit;
        private DevExpress.XtraReports.UI.XRLabel lblAmountFilter;
        private DevExpress.XtraReports.UI.XRTableCell xrCreatedBy;
        private DevExpress.XtraReports.UI.XRTableCell xrModifiedBy;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCreatedBy;
        private DevExpress.XtraReports.UI.XRTableCell xrCapModifiedBy;
        private DevExpress.XtraReports.UI.XRTableCell xrCashBank;
        private DevExpress.XtraReports.UI.XRTableCell xrAction;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCashBank;
        private DevExpress.XtraReports.UI.XRTableCell xrCapAction;
        private DevExpress.XtraReports.UI.XRTable xrtblFooter;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrCapThirdParty;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrCapNarration;
    }
}
