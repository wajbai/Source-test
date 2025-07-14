namespace Bosco.Report.ReportObject
{
    partial class BankChequeCollectedRegister
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
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapVoucherDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapDraweeName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapSenderName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapChequeDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblCleared = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrVoucherDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDraweeName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSenderName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrChequeDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.PageHeader1 = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCleared)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCleared});
            this.Detail.HeightF = 22F;
            this.Detail.Visible = true;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCaption});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrtblCaption
            // 
            this.xrtblCaption.BackColor = System.Drawing.Color.Gainsboro;
            this.xrtblCaption.BorderColor = System.Drawing.Color.DarkGray;
            this.xrtblCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrtblCaption.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            this.xrtblCaption.Name = "xrtblCaption";
            this.xrtblCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblCaption.SizeF = new System.Drawing.SizeF(1113F, 25F);
            this.xrtblCaption.StyleName = "styleColumnHeader";
            this.xrtblCaption.StylePriority.UseBackColor = false;
            this.xrtblCaption.StylePriority.UseBorderColor = false;
            this.xrtblCaption.StylePriority.UseBorders = false;
            this.xrtblCaption.StylePriority.UseFont = false;
            this.xrtblCaption.StylePriority.UsePadding = false;
            this.xrtblCaption.StylePriority.UseTextAlignment = false;
            this.xrtblCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapVoucherNo,
            this.xrCapVoucherDate,
            this.xrCapDraweeName,
            this.xrCapParticulars,
            this.xrCapSenderName,
            this.xrCapAmount,
            this.xrCapChequeNo,
            this.xrCapChequeDate});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrCapVoucherNo
            // 
            this.xrCapVoucherNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapVoucherNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapVoucherNo.Name = "xrCapVoucherNo";
            this.xrCapVoucherNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapVoucherNo.StylePriority.UseBackColor = false;
            this.xrCapVoucherNo.StylePriority.UseBorders = false;
            this.xrCapVoucherNo.StylePriority.UseFont = false;
            this.xrCapVoucherNo.StylePriority.UsePadding = false;
            this.xrCapVoucherNo.StylePriority.UseTextAlignment = false;
            this.xrCapVoucherNo.Text = "V No";
            this.xrCapVoucherNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapVoucherNo.Weight = 0.17115272553479841D;
            // 
            // xrCapVoucherDate
            // 
            this.xrCapVoucherDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapVoucherDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapVoucherDate.Name = "xrCapVoucherDate";
            this.xrCapVoucherDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapVoucherDate.StylePriority.UseBorders = false;
            this.xrCapVoucherDate.StylePriority.UseFont = false;
            this.xrCapVoucherDate.StylePriority.UsePadding = false;
            this.xrCapVoucherDate.StylePriority.UseTextAlignment = false;
            this.xrCapVoucherDate.Text = "Date";
            this.xrCapVoucherDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapVoucherDate.Weight = 0.18977499088771244D;
            // 
            // xrCapDraweeName
            // 
            this.xrCapDraweeName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapDraweeName.Name = "xrCapDraweeName";
            this.xrCapDraweeName.StylePriority.UseFont = false;
            this.xrCapDraweeName.Text = "Drawee Name";
            this.xrCapDraweeName.Weight = 0.53205547677557008D;
            // 
            // xrCapSenderName
            // 
            this.xrCapSenderName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapSenderName.Name = "xrCapSenderName";
            this.xrCapSenderName.StylePriority.UseFont = false;
            this.xrCapSenderName.Text = "Sender Name & Address";
            this.xrCapSenderName.Weight = 0.68379869038235452D;
            // 
            // xrCapAmount
            // 
            this.xrCapAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapAmount.Name = "xrCapAmount";
            this.xrCapAmount.StylePriority.UseFont = false;
            this.xrCapAmount.StylePriority.UseTextAlignment = false;
            this.xrCapAmount.Text = "Amount";
            this.xrCapAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapAmount.Weight = 0.31766469867389979D;
            // 
            // xrCapChequeNo
            // 
            this.xrCapChequeNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapChequeNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapChequeNo.Name = "xrCapChequeNo";
            this.xrCapChequeNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapChequeNo.StylePriority.UseBorders = false;
            this.xrCapChequeNo.StylePriority.UseFont = false;
            this.xrCapChequeNo.StylePriority.UsePadding = false;
            this.xrCapChequeNo.StylePriority.UseTextAlignment = false;
            this.xrCapChequeNo.Text = "Cheque No";
            this.xrCapChequeNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapChequeNo.Weight = 0.32474879177521987D;
            // 
            // xrCapChequeDate
            // 
            this.xrCapChequeDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapChequeDate.Name = "xrCapChequeDate";
            this.xrCapChequeDate.StylePriority.UseFont = false;
            this.xrCapChequeDate.Text = "Cheque Dt";
            this.xrCapChequeDate.Weight = 0.26744023313150983D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblTotal});
            this.ReportFooter.HeightF = 25F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrtblTotal
            // 
            this.xrtblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.xrtblTotal.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblTotal.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrtblTotal.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrtblTotal.Name = "xrtblTotal";
            this.xrtblTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblTotal.SizeF = new System.Drawing.SizeF(1113F, 25F);
            this.xrtblTotal.StyleName = "styleTotalRow";
            this.xrtblTotal.StylePriority.UseBackColor = false;
            this.xrtblTotal.StylePriority.UseBorderColor = false;
            this.xrtblTotal.StylePriority.UseBorders = false;
            this.xrtblTotal.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapTotal,
            this.xrTotalAmt,
            this.xrTableCell1});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrCapTotal
            // 
            this.xrCapTotal.BorderColor = System.Drawing.Color.Silver;
            this.xrCapTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapTotal.Name = "xrCapTotal";
            this.xrCapTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrCapTotal.StylePriority.UseBorderColor = false;
            this.xrCapTotal.StylePriority.UseBorders = false;
            this.xrCapTotal.StylePriority.UseFont = false;
            this.xrCapTotal.StylePriority.UsePadding = false;
            this.xrCapTotal.StylePriority.UseTextAlignment = false;
            this.xrCapTotal.Text = "Total";
            this.xrCapTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapTotal.Weight = 2.5423038012488446D;
            // 
            // xrTotalAmt
            // 
            this.xrTotalAmt.BorderColor = System.Drawing.Color.Silver;
            this.xrTotalAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTotalAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.AMOUNT")});
            this.xrTotalAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTotalAmt.Name = "xrTotalAmt";
            this.xrTotalAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrTotalAmt.StylePriority.UseBorderColor = false;
            this.xrTotalAmt.StylePriority.UseBorders = false;
            this.xrTotalAmt.StylePriority.UseFont = false;
            this.xrTotalAmt.StylePriority.UsePadding = false;
            this.xrTotalAmt.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.IgnoreNullValues = true;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTotalAmt.Summary = xrSummary1;
            this.xrTotalAmt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTotalAmt.Weight = 0.39018347295064515D;
            this.xrTotalAmt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTotalAmt_BeforePrint);
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Weight = 0.72737794387101851D;
            // 
            // xrtblCleared
            // 
            this.xrtblCleared.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblCleared.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblCleared.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrtblCleared.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrtblCleared.Name = "xrtblCleared";
            this.xrtblCleared.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblCleared.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblCleared.SizeF = new System.Drawing.SizeF(1113F, 22F);
            this.xrtblCleared.StyleName = "styleRow";
            this.xrtblCleared.StylePriority.UseBorderColor = false;
            this.xrtblCleared.StylePriority.UseBorders = false;
            this.xrtblCleared.StylePriority.UseFont = false;
            this.xrtblCleared.StylePriority.UsePadding = false;
            this.xrtblCleared.StylePriority.UseTextAlignment = false;
            this.xrtblCleared.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrVoucherNo,
            this.xrVoucherDate,
            this.xrDraweeName,
            this.xrParticulars,
            this.xrSenderName,
            this.xrAmount,
            this.xrChequeNo,
            this.xrChequeDate});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableRow3.StylePriority.UsePadding = false;
            this.xrTableRow3.Weight = 0.88D;
            // 
            // xrVoucherNo
            // 
            this.xrVoucherNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrVoucherNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.VOUCHER_NO", "{0:d}")});
            this.xrVoucherNo.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrVoucherNo.Name = "xrVoucherNo";
            this.xrVoucherNo.StyleName = "styleDateInfo";
            this.xrVoucherNo.StylePriority.UseBorders = false;
            this.xrVoucherNo.StylePriority.UseFont = false;
            this.xrVoucherNo.StylePriority.UseTextAlignment = false;
            this.xrVoucherNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrVoucherNo.Weight = 0.20935380449336938D;
            // 
            // xrVoucherDate
            // 
            this.xrVoucherDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrVoucherDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.VOUCHER_DATE", "{0:d}")});
            this.xrVoucherDate.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrVoucherDate.Name = "xrVoucherDate";
            this.xrVoucherDate.StylePriority.UseBorders = false;
            this.xrVoucherDate.StylePriority.UseFont = false;
            this.xrVoucherDate.Weight = 0.23213259157740462D;
            // 
            // xrDraweeName
            // 
            this.xrDraweeName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.DRAWEE_NAME")});
            this.xrDraweeName.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrDraweeName.Name = "xrDraweeName";
            this.xrDraweeName.StylePriority.UseFont = false;
            this.xrDraweeName.Weight = 0.65080962956545407D;
            // 
            // xrSenderName
            // 
            this.xrSenderName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.NAME_ADDRESS")});
            this.xrSenderName.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrSenderName.Name = "xrSenderName";
            this.xrSenderName.StylePriority.UseFont = false;
            this.xrSenderName.Weight = 0.83642159348044709D;
            // 
            // xrAmount
            // 
            this.xrAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.AMOUNT", "{0:n}"),
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "ChequeUncleared.VOUCHER_ID")});
            this.xrAmount.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrAmount.Name = "xrAmount";
            this.xrAmount.ProcessDuplicates = DevExpress.XtraReports.UI.ValueSuppressType.MergeByTag;
            this.xrAmount.StylePriority.UseFont = false;
            this.xrAmount.StylePriority.UseTextAlignment = false;
            this.xrAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrAmount.Weight = 0.38856785934439336D;
            this.xrAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrAmount_BeforePrint);
            // 
            // xrChequeNo
            // 
            this.xrChequeNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrChequeNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.CHEQUE_NO")});
            this.xrChequeNo.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrChequeNo.Name = "xrChequeNo";
            this.xrChequeNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrChequeNo.StylePriority.UseBorders = false;
            this.xrChequeNo.StylePriority.UseFont = false;
            this.xrChequeNo.StylePriority.UsePadding = false;
            this.xrChequeNo.StylePriority.UseTextAlignment = false;
            this.xrChequeNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrChequeNo.Weight = 0.39723240567278428D;
            // 
            // xrChequeDate
            // 
            this.xrChequeDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.CHEQUE_DATE", "{0:d}")});
            this.xrChequeDate.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrChequeDate.Name = "xrChequeDate";
            this.xrChequeDate.StylePriority.UseFont = false;
            this.xrChequeDate.Weight = 0.32713243713683543D;
            this.xrChequeDate.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrChequeDate_BeforePrint);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PageHeader1
            // 
            this.PageHeader1.Name = "PageHeader1";
            // 
            // xrCapParticulars
            // 
            this.xrCapParticulars.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapParticulars.Name = "xrCapParticulars";
            this.xrCapParticulars.StylePriority.UseFont = false;
            this.xrCapParticulars.Text = "Particulars";
            this.xrCapParticulars.Weight = 0.49301411747807711D;
            // 
            // xrParticulars
            // 
            this.xrParticulars.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.LEDGER_NAME")});
            this.xrParticulars.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrParticulars.Name = "xrParticulars";
            this.xrParticulars.StylePriority.UseFont = false;
            this.xrParticulars.Weight = 0.60305442541664633D;
            // 
            // BankChequeCollectedRegister
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(41, 3, 20, 20);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCleared)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCapVoucherDate;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrCapTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCapChequeNo;
        private DevExpress.XtraReports.UI.XRTable xrtblCleared;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell xrVoucherDate;
        private DevExpress.XtraReports.UI.XRTableCell xrChequeNo;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader1;
        private DevExpress.XtraReports.UI.XRTableCell xrChequeDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapChequeDate;
        private DevExpress.XtraReports.UI.XRTableCell xrSenderName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapSenderName;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrDraweeName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDraweeName;
        private DevExpress.XtraReports.UI.XRTableCell xrAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrCapAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrCapParticulars;
    }
}
