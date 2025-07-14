namespace Bosco.Report.ReportObject
{
    partial class ReferenceReportStatus
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
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrReferenceNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPartyName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapActualAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPendingAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcapBalanceAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblAmountFilter = new DevExpress.XtraReports.UI.XRLabel();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrtblDataSourceBind = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblReferenceNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDataSourceBind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblDataSourceBind});
            this.Detail.HeightF = 25F;
            this.Detail.Visible = true;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeaderCaption});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrtblHeaderCaption
            // 
            this.xrtblHeaderCaption.LocationFloat = new DevExpress.Utils.PointFloat(2.002382F, 0F);
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.SizeF = new System.Drawing.SizeF(720.9977F, 25F);
            this.xrtblHeaderCaption.StyleName = "styleGroupRow";
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            this.xrtblHeaderCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapDate,
            this.xrReferenceNo,
            this.xrPartyName,
            this.xrCapActualAmount,
            this.xrPendingAmount,
            this.xrcapBalanceAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrCapDate
            // 
            this.xrCapDate.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapDate.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.StyleName = "styleDateInfo";
            this.xrCapDate.StylePriority.UseBackColor = false;
            this.xrCapDate.StylePriority.UseBorderColor = false;
            this.xrCapDate.StylePriority.UseBorders = false;
            this.xrCapDate.StylePriority.UseFont = false;
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            this.xrCapDate.Text = "Date";
            this.xrCapDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCapDate.Weight = 0.87121677470785575D;
            // 
            // xrReferenceNo
            // 
            this.xrReferenceNo.BackColor = System.Drawing.Color.Gainsboro;
            this.xrReferenceNo.BorderColor = System.Drawing.Color.DarkGray;
            this.xrReferenceNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrReferenceNo.Name = "xrReferenceNo";
            this.xrReferenceNo.StylePriority.UseBackColor = false;
            this.xrReferenceNo.StylePriority.UseBorderColor = false;
            this.xrReferenceNo.StylePriority.UseBorders = false;
            this.xrReferenceNo.Text = "Reference No";
            this.xrReferenceNo.Weight = 0.92407774872082349D;
            // 
            // xrPartyName
            // 
            this.xrPartyName.BackColor = System.Drawing.Color.Gainsboro;
            this.xrPartyName.BorderColor = System.Drawing.Color.DarkGray;
            this.xrPartyName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPartyName.Name = "xrPartyName";
            this.xrPartyName.StylePriority.UseBackColor = false;
            this.xrPartyName.StylePriority.UseBorderColor = false;
            this.xrPartyName.StylePriority.UseBorders = false;
            this.xrPartyName.StylePriority.UseTextAlignment = false;
            this.xrPartyName.Text = "Party\'s Name";
            this.xrPartyName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPartyName.Weight = 1.7409763704204726D;
            // 
            // xrCapActualAmount
            // 
            this.xrCapActualAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapActualAmount.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapActualAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapActualAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapActualAmount.Name = "xrCapActualAmount";
            this.xrCapActualAmount.StylePriority.UseBackColor = false;
            this.xrCapActualAmount.StylePriority.UseBorderColor = false;
            this.xrCapActualAmount.StylePriority.UseBorders = false;
            this.xrCapActualAmount.StylePriority.UseFont = false;
            this.xrCapActualAmount.StylePriority.UseTextAlignment = false;
            this.xrCapActualAmount.Text = "Actual Amount";
            this.xrCapActualAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapActualAmount.Weight = 1.268437683603115D;
            // 
            // xrPendingAmount
            // 
            this.xrPendingAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.xrPendingAmount.BorderColor = System.Drawing.Color.DarkGray;
            this.xrPendingAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPendingAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrPendingAmount.Name = "xrPendingAmount";
            this.xrPendingAmount.StylePriority.UseBackColor = false;
            this.xrPendingAmount.StylePriority.UseBorderColor = false;
            this.xrPendingAmount.StylePriority.UseBorders = false;
            this.xrPendingAmount.StylePriority.UseFont = false;
            this.xrPendingAmount.StylePriority.UseTextAlignment = false;
            this.xrPendingAmount.Text = "Paid Amount";
            this.xrPendingAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrPendingAmount.Weight = 1.28647119610164D;
            // 
            // xrcapBalanceAmount
            // 
            this.xrcapBalanceAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.xrcapBalanceAmount.BorderColor = System.Drawing.Color.DarkGray;
            this.xrcapBalanceAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrcapBalanceAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrcapBalanceAmount.Name = "xrcapBalanceAmount";
            this.xrcapBalanceAmount.StylePriority.UseBackColor = false;
            this.xrcapBalanceAmount.StylePriority.UseBorderColor = false;
            this.xrcapBalanceAmount.StylePriority.UseBorders = false;
            this.xrcapBalanceAmount.StylePriority.UseFont = false;
            this.xrcapBalanceAmount.StylePriority.UseTextAlignment = false;
            this.xrcapBalanceAmount.Text = "Balance";
            this.xrcapBalanceAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcapBalanceAmount.Weight = 1.0190747314247373D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblAmountFilter,
            this.xrtblGrandTotal});
            this.ReportFooter.HeightF = 41F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblAmountFilter
            // 
            this.lblAmountFilter.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lblAmountFilter.LocationFloat = new DevExpress.Utils.PointFloat(2.002382F, 25F);
            this.lblAmountFilter.Name = "lblAmountFilter";
            this.lblAmountFilter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblAmountFilter.SizeF = new System.Drawing.SizeF(487.2096F, 16F);
            this.lblAmountFilter.StylePriority.UseFont = false;
            this.lblAmountFilter.StylePriority.UseTextAlignment = false;
            this.lblAmountFilter.Text = "Amount Filter";
            this.lblAmountFilter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblAmountFilter.Visible = false;
            // 
            // xrtblGrandTotal
            // 
            this.xrtblGrandTotal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrtblGrandTotal.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblGrandTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrtblGrandTotal.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblGrandTotal.SizeF = new System.Drawing.SizeF(721F, 25F);
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UseTextAlignment = false;
            this.xrtblGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell5,
            this.xrTableCell9,
            this.xrTableCell10});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "Grand Total";
            this.xrTableCell8.Weight = 1.4920492397102005D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Voucher_Reference.ACTUAL_AMOUNT")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell5.Summary = xrSummary1;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 0.53518488403828235D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Voucher_Reference.PAID_AMOUNT")});
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.IgnoreNullValues = true;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell9.Summary = xrSummary2;
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell9.Weight = 0.542793453814418D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Voucher_Reference.BALANCE_AMOUNT")});
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:n}";
            xrSummary3.IgnoreNullValues = true;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell10.Summary = xrSummary3;
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell10.Weight = 0.4299724224370991D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrtblDataSourceBind
            // 
            this.xrtblDataSourceBind.BorderColor = System.Drawing.Color.Silver;
            this.xrtblDataSourceBind.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrtblDataSourceBind.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrtblDataSourceBind.LocationFloat = new DevExpress.Utils.PointFloat(2.002382F, 0F);
            this.xrtblDataSourceBind.Name = "xrtblDataSourceBind";
            this.xrtblDataSourceBind.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblDataSourceBind.SizeF = new System.Drawing.SizeF(722.9976F, 25F);
            this.xrtblDataSourceBind.StylePriority.UseBorderColor = false;
            this.xrtblDataSourceBind.StylePriority.UseBorders = false;
            this.xrtblDataSourceBind.StylePriority.UseFont = false;
            this.xrtblDataSourceBind.StylePriority.UseTextAlignment = false;
            this.xrtblDataSourceBind.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrtblReferenceNumber,
            this.xrtblLedger,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell6});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Voucher_Reference.DATE", "{0:d}")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Weight = 0.53240225807618458D;
            // 
            // xrtblReferenceNumber
            // 
            this.xrtblReferenceNumber.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Voucher_Reference.REFERENCE_NUMBER")});
            this.xrtblReferenceNumber.Name = "xrtblReferenceNumber";
            this.xrtblReferenceNumber.Weight = 0.56470566233561781D;
            // 
            // xrtblLedger
            // 
            this.xrtblLedger.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Voucher_Reference.PARTY_NAME")});
            this.xrtblLedger.Name = "xrtblLedger";
            this.xrtblLedger.Weight = 1.0639140041882589D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Voucher_Reference.ACTUAL_AMOUNT", "{0:n}")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 0.77514486089084933D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Voucher_Reference.PAID_AMOUNT", "{0:n}")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell4.Weight = 0.78616537308774D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Voucher_Reference.BALANCE_AMOUNT", "{0:n}")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell6.Weight = 0.63481046367674843D;
            // 
            // ReferenceReportStatus
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.DataMember = "Voucher_Reference";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDataSourceBind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrReferenceNo;
        private DevExpress.XtraReports.UI.XRTableCell xrPartyName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapActualAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrPendingAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrcapBalanceAmount;
        private DevExpress.XtraReports.UI.XRTable xrtblDataSourceBind;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrtblReferenceNumber;
        private DevExpress.XtraReports.UI.XRTableCell xrtblLedger;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRLabel lblAmountFilter;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
    }
}
