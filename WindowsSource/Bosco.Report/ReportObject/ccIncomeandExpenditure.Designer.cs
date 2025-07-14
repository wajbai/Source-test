namespace Bosco.Report.ReportObject
{
    partial class ccIncomeandExpenditure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ccIncomeandExpenditure));
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderOption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrccCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xtCapExpenditure = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRecptAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPaymtCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPaymtAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTblTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrccPayTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrccPaymentAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrccRecptTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrccReceiptAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrccSubpayments = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrccSubReceipts = new DevExpress.XtraReports.UI.XRSubreport();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.clfExpenditureTotalamt = new DevExpress.XtraReports.UI.CalculatedField();
            this.clfincomeTotalAmt = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrCrossBandLine1 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            this.xrCrossBandLine2 = new DevExpress.XtraReports.UI.XRCrossBandLine();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderOption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrccSubReceipts,
            this.xrccSubpayments});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderOption});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // xrtblHeaderOption
            // 
            resources.ApplyResources(this.xrtblHeaderOption, "xrtblHeaderOption");
            this.xrtblHeaderOption.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrtblHeaderOption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblHeaderOption.Name = "xrtblHeaderOption";
            this.xrtblHeaderOption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderOption.StylePriority.UseBackColor = false;
            this.xrtblHeaderOption.StylePriority.UseBorderDashStyle = false;
            this.xrtblHeaderOption.StylePriority.UseFont = false;
            this.xrtblHeaderOption.StylePriority.UsePadding = false;
            this.xrtblHeaderOption.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrccCode,
            this.xtCapExpenditure,
            this.xrRecptAmount,
            this.xrPaymtCode,
            this.xrCapIncome,
            this.xrPaymtAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrccCode
            // 
            this.xrccCode.Name = "xrccCode";
            resources.ApplyResources(this.xrccCode, "xrccCode");
            // 
            // xtCapExpenditure
            // 
            this.xtCapExpenditure.Name = "xtCapExpenditure";
            resources.ApplyResources(this.xtCapExpenditure, "xtCapExpenditure");
            // 
            // xrRecptAmount
            // 
            this.xrRecptAmount.Name = "xrRecptAmount";
            this.xrRecptAmount.StylePriority.UseBorders = false;
            this.xrRecptAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrRecptAmount, "xrRecptAmount");
            // 
            // xrPaymtCode
            // 
            this.xrPaymtCode.Name = "xrPaymtCode";
            this.xrPaymtCode.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrPaymtCode, "xrPaymtCode");
            // 
            // xrCapIncome
            // 
            this.xrCapIncome.Name = "xrCapIncome";
            resources.ApplyResources(this.xrCapIncome, "xrCapIncome");
            // 
            // xrPaymtAmount
            // 
            this.xrPaymtAmount.Name = "xrPaymtAmount";
            this.xrPaymtAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrPaymtAmount, "xrPaymtAmount");
            // 
            // xrTblTotal
            // 
            resources.ApplyResources(this.xrTblTotal, "xrTblTotal");
            this.xrTblTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblTotal.Name = "xrTblTotal";
            this.xrTblTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTblTotal.StylePriority.UseBackColor = false;
            this.xrTblTotal.StylePriority.UseBorderColor = false;
            this.xrTblTotal.StylePriority.UseBorders = false;
            this.xrTblTotal.StylePriority.UseFont = false;
            this.xrTblTotal.StylePriority.UsePadding = false;
            this.xrTblTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrccPayTotal,
            this.xrccPaymentAmt,
            this.xrccRecptTotal,
            this.xrccReceiptAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrccPayTotal
            // 
            this.xrccPayTotal.Name = "xrccPayTotal";
            resources.ApplyResources(this.xrccPayTotal, "xrccPayTotal");
            // 
            // xrccPaymentAmt
            // 
            this.xrccPaymentAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Payments.clfExpenditureTotalamt")});
            this.xrccPaymentAmt.Name = "xrccPaymentAmt";
            this.xrccPaymentAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrccPaymentAmt.Summary = xrSummary1;
            resources.ApplyResources(this.xrccPaymentAmt, "xrccPaymentAmt");
            this.xrccPaymentAmt.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrccPaymentAmt_SummaryGetResult);
            // 
            // xrccRecptTotal
            // 
            this.xrccRecptTotal.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrccRecptTotal.Name = "xrccRecptTotal";
            this.xrccRecptTotal.StylePriority.UseBorders = false;
            resources.ApplyResources(this.xrccRecptTotal, "xrccRecptTotal");
            // 
            // xrccReceiptAmt
            // 
            this.xrccReceiptAmt.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrccReceiptAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Receipts.clfincomeTotalAmt")});
            this.xrccReceiptAmt.Name = "xrccReceiptAmt";
            this.xrccReceiptAmt.StylePriority.UseBorders = false;
            this.xrccReceiptAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrccReceiptAmt.Summary = xrSummary2;
            resources.ApplyResources(this.xrccReceiptAmt, "xrccReceiptAmt");
            this.xrccReceiptAmt.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrccReceiptAmt_SummaryGetResult);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblTotal});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrccSubpayments
            // 
            resources.ApplyResources(this.xrccSubpayments, "xrccSubpayments");
            this.xrccSubpayments.Name = "xrccSubpayments";
            this.xrccSubpayments.ReportSource = new Bosco.Report.ReportObject.Payments();
            // 
            // xrccSubReceipts
            // 
            resources.ApplyResources(this.xrccSubReceipts, "xrccSubReceipts");
            this.xrccSubReceipts.Name = "xrccSubReceipts";
            this.xrccSubReceipts.ReportSource = new Bosco.Report.ReportObject.Receipts();
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clfExpenditureTotalamt
            // 
            this.clfExpenditureTotalamt.DataMember = "Payments";
            this.clfExpenditureTotalamt.Name = "clfExpenditureTotalamt";
            // 
            // clfincomeTotalAmt
            // 
            this.clfincomeTotalAmt.DataMember = "Receipts";
            this.clfincomeTotalAmt.Name = "clfincomeTotalAmt";
            // 
            // xrCrossBandLine1
            // 
            this.xrCrossBandLine1.EndBand = this.ReportFooter;
            resources.ApplyResources(this.xrCrossBandLine1, "xrCrossBandLine1");
            this.xrCrossBandLine1.Name = "xrCrossBandLine1";
            this.xrCrossBandLine1.StartBand = this.PageHeader;
            this.xrCrossBandLine1.WidthF = 1.000023F;
            // 
            // xrCrossBandLine2
            // 
            this.xrCrossBandLine2.EndBand = this.ReportFooter;
            resources.ApplyResources(this.xrCrossBandLine2, "xrCrossBandLine2");
            this.xrCrossBandLine2.Name = "xrCrossBandLine2";
            this.xrCrossBandLine2.StartBand = this.PageHeader;
            this.xrCrossBandLine2.WidthF = 1F;
            // 
            // ccIncomeandExpenditure
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.clfExpenditureTotalamt,
            this.clfincomeTotalAmt});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandLine2,
            this.xrCrossBandLine1});
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderOption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderOption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrccCode;
        private DevExpress.XtraReports.UI.XRTableCell xtCapExpenditure;
        private DevExpress.XtraReports.UI.XRTableCell xrRecptAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrPaymtCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapIncome;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRSubreport xrccSubReceipts;
        private DevExpress.XtraReports.UI.XRSubreport xrccSubpayments;
        private DevExpress.XtraReports.UI.XRTable xrTblTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrccPayTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrccPaymentAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrccRecptTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrccReceiptAmt;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.CalculatedField clfExpenditureTotalamt;
        private DevExpress.XtraReports.UI.CalculatedField clfincomeTotalAmt;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine1;
        private DevExpress.XtraReports.UI.XRCrossBandLine xrCrossBandLine2;
        private DevExpress.XtraReports.UI.XRTableCell xrPaymtAmount;
    }
}
