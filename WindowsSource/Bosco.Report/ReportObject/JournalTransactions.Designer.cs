namespace Bosco.Report.ReportObject
{
    partial class JournalTransactions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalTransactions));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblCashBankTrans = new DevExpress.XtraReports.UI.XRTable();
            this.xrRowData = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCashBankTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCashBankTrans});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
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
            this.xrtblHeaderCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.StyleName = "styleColumnHeader";
            this.xrtblHeaderCaption.StylePriority.UseBackColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorderColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorders = false;
            this.xrtblHeaderCaption.StylePriority.UseFont = false;
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapDate,
            this.xrCapLedger,
            this.xrCapVoucherNo,
            this.xrTableCell3,
            this.xrCapDebit,
            this.xrCapCredit});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrCapDate
            // 
            this.xrCapDate.Name = "xrCapDate";
            resources.ApplyResources(this.xrCapDate, "xrCapDate");
            // 
            // xrCapLedger
            // 
            this.xrCapLedger.Name = "xrCapLedger";
            resources.ApplyResources(this.xrCapLedger, "xrCapLedger");
            // 
            // xrCapVoucherNo
            // 
            this.xrCapVoucherNo.Name = "xrCapVoucherNo";
            resources.ApplyResources(this.xrCapVoucherNo, "xrCapVoucherNo");
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            // 
            // xrCapDebit
            // 
            this.xrCapDebit.Multiline = true;
            this.xrCapDebit.Name = "xrCapDebit";
            this.xrCapDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapDebit, "xrCapDebit");
            // 
            // xrCapCredit
            // 
            this.xrCapCredit.Name = "xrCapCredit";
            this.xrCapCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCapCredit, "xrCapCredit");
            // 
            // xrtblCashBankTrans
            // 
            resources.ApplyResources(this.xrtblCashBankTrans, "xrtblCashBankTrans");
            this.xrtblCashBankTrans.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblCashBankTrans.Name = "xrtblCashBankTrans";
            this.xrtblCashBankTrans.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblCashBankTrans.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrRowData});
            this.xrtblCashBankTrans.StyleName = "styleRow";
            this.xrtblCashBankTrans.StylePriority.UseBackColor = false;
            this.xrtblCashBankTrans.StylePriority.UseBorderColor = false;
            this.xrtblCashBankTrans.StylePriority.UseBorders = false;
            this.xrtblCashBankTrans.StylePriority.UseFont = false;
            this.xrtblCashBankTrans.StylePriority.UsePadding = false;
            this.xrtblCashBankTrans.StylePriority.UseTextAlignment = false;
            // 
            // xrRowData
            // 
            this.xrRowData.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrDate,
            this.xrLedger,
            this.xrVoucherNo,
            this.xrNarration,
            this.xrDebit,
            this.xrCredit});
            this.xrRowData.Name = "xrRowData";
            resources.ApplyResources(this.xrRowData, "xrRowData");
            // 
            // xrDate
            // 
            this.xrDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.VOUCHER_DATE", "{0:d}")});
            resources.ApplyResources(this.xrDate, "xrDate");
            this.xrDate.Name = "xrDate";
            this.xrDate.StyleName = "styleDateInfo";
            this.xrDate.StylePriority.UseFont = false;
            // 
            // xrLedger
            // 
            this.xrLedger.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.LEDGER_NAME")});
            this.xrLedger.Name = "xrLedger";
            resources.ApplyResources(this.xrLedger, "xrLedger");
            // 
            // xrVoucherNo
            // 
            this.xrVoucherNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.VOUCHER_NO")});
            this.xrVoucherNo.Name = "xrVoucherNo";
            resources.ApplyResources(this.xrVoucherNo, "xrVoucherNo");
            // 
            // xrNarration
            // 
            this.xrNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.NARRATION")});
            this.xrNarration.Name = "xrNarration";
            resources.ApplyResources(this.xrNarration, "xrNarration");
            // 
            // xrDebit
            // 
            this.xrDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.DEBIT", "{0:n}")});
            this.xrDebit.Name = "xrDebit";
            this.xrDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.IgnoreNullValues = true;
            this.xrDebit.Summary = xrSummary1;
            resources.ApplyResources(this.xrDebit, "xrDebit");
            this.xrDebit.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCredit_BeforePrint);
            // 
            // xrCredit
            // 
            this.xrCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankTransactions.CREDIT", "{0:n}")});
            this.xrCredit.Name = "xrCredit";
            this.xrCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCredit, "xrCredit");
            this.xrCredit.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrDebit_BeforePrint);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // JournalTransactions
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader});
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCashBankTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapLedger;
        private DevExpress.XtraReports.UI.XRTableCell xrCapVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCredit;
        private DevExpress.XtraReports.UI.XRTable xrtblCashBankTrans;
        private DevExpress.XtraReports.UI.XRTableRow xrRowData;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrLedger;
        private DevExpress.XtraReports.UI.XRTableCell xrVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrCredit;
        private ReportSetting reportSetting1;
    }
}
