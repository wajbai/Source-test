namespace Bosco.Report.ReportObject
{
    partial class TBVerification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TBVerification));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrHeaderLedgerCodeTransactionEmpty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderCurrentTransDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderCurrentTransCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrHeaderCurrentTransClosingDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableLedger = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCurrentLedgerCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCurrentLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCTransDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCTransCredit = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCTransClosingDebit = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.calcClosingBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.calTotalOpeningDebitAmount = new DevExpress.XtraReports.UI.CalculatedField();
            this.calTotalOpeningCreditBalance = new DevExpress.XtraReports.UI.CalculatedField();
            this.calCurrentTransDebit = new DevExpress.XtraReports.UI.CalculatedField();
            this.calCurrentTransCredit = new DevExpress.XtraReports.UI.CalculatedField();
            this.calTotalClosingDebit = new DevExpress.XtraReports.UI.CalculatedField();
            this.calTotalClosingCredit = new DevExpress.XtraReports.UI.CalculatedField();
            this.calGroupClosingBal = new DevExpress.XtraReports.UI.CalculatedField();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTableGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubreportCCDetails = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableLedger,
            this.xrSubreportCCDetails});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // PageHeader
            // 
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderCaption});
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseBorderColor = false;
            // 
            // xrtblHeaderCaption
            // 
            resources.ApplyResources(this.xrtblHeaderCaption, "xrtblHeaderCaption");
            this.xrtblHeaderCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
            this.xrtblHeaderCaption.StylePriority.UseBorderColor = false;
            this.xrtblHeaderCaption.StylePriority.UseBorders = false;
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrHeaderLedgerCodeTransactionEmpty,
            this.xrTableCell20,
            this.xrHeaderCurrentTransDebit,
            this.xrHeaderCurrentTransCredit,
            this.xrHeaderCurrentTransClosingDebit});
            this.xrTableRow16.Name = "xrTableRow16";
            resources.ApplyResources(this.xrTableRow16, "xrTableRow16");
            // 
            // xrHeaderLedgerCodeTransactionEmpty
            // 
            resources.ApplyResources(this.xrHeaderLedgerCodeTransactionEmpty, "xrHeaderLedgerCodeTransactionEmpty");
            this.xrHeaderLedgerCodeTransactionEmpty.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrHeaderLedgerCodeTransactionEmpty.Name = "xrHeaderLedgerCodeTransactionEmpty";
            this.xrHeaderLedgerCodeTransactionEmpty.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderLedgerCodeTransactionEmpty.StylePriority.UseBackColor = false;
            this.xrHeaderLedgerCodeTransactionEmpty.StylePriority.UseBorders = false;
            this.xrHeaderLedgerCodeTransactionEmpty.StylePriority.UseFont = false;
            this.xrHeaderLedgerCodeTransactionEmpty.StylePriority.UsePadding = false;
            this.xrHeaderLedgerCodeTransactionEmpty.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell20
            // 
            resources.ApplyResources(this.xrTableCell20, "xrTableCell20");
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableCell20.StylePriority.UseBackColor = false;
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.StylePriority.UsePadding = false;
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderCurrentTransDebit
            // 
            resources.ApplyResources(this.xrHeaderCurrentTransDebit, "xrHeaderCurrentTransDebit");
            this.xrHeaderCurrentTransDebit.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrHeaderCurrentTransDebit.Name = "xrHeaderCurrentTransDebit";
            this.xrHeaderCurrentTransDebit.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderCurrentTransDebit.StylePriority.UseBackColor = false;
            this.xrHeaderCurrentTransDebit.StylePriority.UseBorders = false;
            this.xrHeaderCurrentTransDebit.StylePriority.UseFont = false;
            this.xrHeaderCurrentTransDebit.StylePriority.UsePadding = false;
            this.xrHeaderCurrentTransDebit.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderCurrentTransCredit
            // 
            resources.ApplyResources(this.xrHeaderCurrentTransCredit, "xrHeaderCurrentTransCredit");
            this.xrHeaderCurrentTransCredit.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrHeaderCurrentTransCredit.Name = "xrHeaderCurrentTransCredit";
            this.xrHeaderCurrentTransCredit.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderCurrentTransCredit.StylePriority.UseBackColor = false;
            this.xrHeaderCurrentTransCredit.StylePriority.UseBorders = false;
            this.xrHeaderCurrentTransCredit.StylePriority.UseFont = false;
            this.xrHeaderCurrentTransCredit.StylePriority.UsePadding = false;
            this.xrHeaderCurrentTransCredit.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderCurrentTransClosingDebit
            // 
            resources.ApplyResources(this.xrHeaderCurrentTransClosingDebit, "xrHeaderCurrentTransClosingDebit");
            this.xrHeaderCurrentTransClosingDebit.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrHeaderCurrentTransClosingDebit.Name = "xrHeaderCurrentTransClosingDebit";
            this.xrHeaderCurrentTransClosingDebit.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrHeaderCurrentTransClosingDebit.StylePriority.UseBackColor = false;
            this.xrHeaderCurrentTransClosingDebit.StylePriority.UseBorders = false;
            this.xrHeaderCurrentTransClosingDebit.StylePriority.UseFont = false;
            this.xrHeaderCurrentTransClosingDebit.StylePriority.UsePadding = false;
            this.xrHeaderCurrentTransClosingDebit.StylePriority.UseTextAlignment = false;
            // 
            // xrTableLedger
            // 
            resources.ApplyResources(this.xrTableLedger, "xrTableLedger");
            this.xrTableLedger.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableLedger.Name = "xrTableLedger";
            this.xrTableLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableLedger.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
            this.xrTableLedger.StyleName = "styleRow";
            this.xrTableLedger.StylePriority.UseBorderColor = false;
            this.xrTableLedger.StylePriority.UseBorders = false;
            this.xrTableLedger.StylePriority.UseFont = false;
            this.xrTableLedger.StylePriority.UsePadding = false;
            this.xrTableLedger.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCurrentLedgerCode,
            this.xrCurrentLedgerName,
            this.xrTableCTransDebit,
            this.xrTableCTransCredit,
            this.xrTableCTransClosingDebit});
            this.xrTableRow12.Name = "xrTableRow12";
            resources.ApplyResources(this.xrTableRow12, "xrTableRow12");
            // 
            // xrCurrentLedgerCode
            // 
            this.xrCurrentLedgerCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.LEDGER_CODE")});
            this.xrCurrentLedgerCode.Name = "xrCurrentLedgerCode";
            resources.ApplyResources(this.xrCurrentLedgerCode, "xrCurrentLedgerCode");
            // 
            // xrCurrentLedgerName
            // 
            this.xrCurrentLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.LEDGER_NAME")});
            this.xrCurrentLedgerName.Name = "xrCurrentLedgerName";
            this.xrCurrentLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCurrentLedgerName.StylePriority.UsePadding = false;
            this.xrCurrentLedgerName.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrCurrentLedgerName, "xrCurrentLedgerName");
            // 
            // xrTableCTransDebit
            // 
            this.xrTableCTransDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.DEBIT", "{0:n}")});
            this.xrTableCTransDebit.Name = "xrTableCTransDebit";
            this.xrTableCTransDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCTransDebit, "xrTableCTransDebit");
            // 
            // xrTableCTransCredit
            // 
            this.xrTableCTransCredit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.CREDIT", "{0:n}")});
            this.xrTableCTransCredit.Name = "xrTableCTransCredit";
            this.xrTableCTransCredit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrTableCTransCredit, "xrTableCTransCredit");
            // 
            // xrTableCTransClosingDebit
            // 
            this.xrTableCTransClosingDebit.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.BALANCE", "{0:n}")});
            this.xrTableCTransClosingDebit.Name = "xrTableCTransClosingDebit";
            this.xrTableCTransClosingDebit.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.xrTableCTransClosingDebit.Summary = xrSummary1;
            resources.ApplyResources(this.xrTableCTransClosingDebit, "xrTableCTransClosingDebit");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // calcClosingBalance
            // 
            this.calcClosingBalance.DataMember = "TrialBalance";
            this.calcClosingBalance.Name = "calcClosingBalance";
            // 
            // calTotalOpeningDebitAmount
            // 
            this.calTotalOpeningDebitAmount.DataMember = "TrialBalance";
            this.calTotalOpeningDebitAmount.Name = "calTotalOpeningDebitAmount";
            // 
            // calTotalOpeningCreditBalance
            // 
            this.calTotalOpeningCreditBalance.DataMember = "TrialBalance";
            this.calTotalOpeningCreditBalance.Name = "calTotalOpeningCreditBalance";
            // 
            // calCurrentTransDebit
            // 
            this.calCurrentTransDebit.DataMember = "TrialBalance";
            this.calCurrentTransDebit.Name = "calCurrentTransDebit";
            // 
            // calCurrentTransCredit
            // 
            this.calCurrentTransCredit.DataMember = "TrialBalance";
            this.calCurrentTransCredit.Name = "calCurrentTransCredit";
            // 
            // calTotalClosingDebit
            // 
            this.calTotalClosingDebit.DataMember = "TrialBalance";
            this.calTotalClosingDebit.Name = "calTotalClosingDebit";
            // 
            // calTotalClosingCredit
            // 
            this.calTotalClosingCredit.DataMember = "TrialBalance";
            this.calTotalClosingCredit.Name = "calTotalClosingCredit";
            // 
            // calGroupClosingBal
            // 
            this.calGroupClosingBal.DataMember = "TrialBalance";
            this.calGroupClosingBal.Name = "calGroupClosingBal";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableGrandTotal});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTableGrandTotal
            // 
            resources.ApplyResources(this.xrTableGrandTotal, "xrTableGrandTotal");
            this.xrTableGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableGrandTotal.Name = "xrTableGrandTotal";
            this.xrTableGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTableGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTableGrandTotal.StyleName = "styleGroupRow";
            this.xrTableGrandTotal.StylePriority.UseBackColor = false;
            this.xrTableGrandTotal.StylePriority.UseBorderColor = false;
            this.xrTableGrandTotal.StylePriority.UseBorders = false;
            this.xrTableGrandTotal.StylePriority.UseFont = false;
            this.xrTableGrandTotal.StylePriority.UsePadding = false;
            this.xrTableGrandTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.DEBIT")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell3.Summary = xrSummary2;
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.CREDIT")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell4.Summary = xrSummary3;
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TrialBalance.BALANCE")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary4, "xrSummary4");
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell5.Summary = xrSummary4;
            resources.ApplyResources(this.xrTableCell5, "xrTableCell5");
            // 
            // xrSubreportCCDetails
            // 
            this.xrSubreportCCDetails.CanShrink = true;
            resources.ApplyResources(this.xrSubreportCCDetails, "xrSubreportCCDetails");
            this.xrSubreportCCDetails.Name = "xrSubreportCCDetails";
            this.xrSubreportCCDetails.ReportSource = new Bosco.Report.ReportObject.UcCCDetail();
            // 
            // TBVerification
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calcClosingBalance,
            this.calTotalOpeningDebitAmount,
            this.calTotalOpeningCreditBalance,
            this.calCurrentTransDebit,
            this.calCurrentTransCredit,
            this.calTotalClosingDebit,
            this.calTotalClosingCredit,
            this.calGroupClosingBal});
            this.DataMember = "TrialBalance";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField calcClosingBalance;
        private DevExpress.XtraReports.UI.CalculatedField calTotalOpeningDebitAmount;
        private DevExpress.XtraReports.UI.CalculatedField calTotalOpeningCreditBalance;
        private DevExpress.XtraReports.UI.CalculatedField calCurrentTransDebit;
        private DevExpress.XtraReports.UI.CalculatedField calCurrentTransCredit;
        private DevExpress.XtraReports.UI.CalculatedField calTotalClosingDebit;
        private DevExpress.XtraReports.UI.CalculatedField calTotalClosingCredit;
        private DevExpress.XtraReports.UI.CalculatedField calGroupClosingBal;
        private DevExpress.XtraReports.UI.XRTable xrTableLedger;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow12;
        private DevExpress.XtraReports.UI.XRTableCell xrCurrentLedgerCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCurrentLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCTransDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCTransCredit;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCTransClosingDebit;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow16;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderLedgerCodeTransactionEmpty;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderCurrentTransDebit;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderCurrentTransCredit;
        private DevExpress.XtraReports.UI.XRTableCell xrHeaderCurrentTransClosingDebit;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTableGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportCCDetails;
    }
}
