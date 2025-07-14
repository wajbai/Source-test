namespace Bosco.Report.ReportObject
{
    partial class BankChequeIssuedRegister
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
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapSno = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAcNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCheque = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapIssuedDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapClearedDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapIssuedAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapNameAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapRemarks = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblCleared = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrSno = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAcNumber = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrChequeNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPayDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrClearedDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrIssuedAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRemarks = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.PageHeader1 = new DevExpress.XtraReports.UI.PageHeaderBand();
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
            this.xrtblCaption.SizeF = new System.Drawing.SizeF(1149F, 25F);
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
            this.xrCapSno,
            this.xrCapAcNo,
            this.xrCapCheque,
            this.xrCapBank,
            this.xrCapIssuedDate,
            this.xrCapClearedDate,
            this.xrCapParticulars,
            this.xrCapAmount,
            this.xrCapIssuedAmount,
            this.xrCapNameAddress,
            this.xrCapRemarks});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrCapSno
            // 
            this.xrCapSno.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapSno.Name = "xrCapSno";
            this.xrCapSno.StylePriority.UseFont = false;
            this.xrCapSno.Text = "SNo";
            this.xrCapSno.Weight = 0.092172589099939251D;
            // 
            // xrCapAcNo
            // 
            this.xrCapAcNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapAcNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapAcNo.Name = "xrCapAcNo";
            this.xrCapAcNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapAcNo.StylePriority.UseBackColor = false;
            this.xrCapAcNo.StylePriority.UseBorders = false;
            this.xrCapAcNo.StylePriority.UseFont = false;
            this.xrCapAcNo.StylePriority.UsePadding = false;
            this.xrCapAcNo.StylePriority.UseTextAlignment = false;
            this.xrCapAcNo.Text = "A/c Number";
            this.xrCapAcNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapAcNo.Weight = 0.31602030460350528D;
            // 
            // xrCapCheque
            // 
            this.xrCapCheque.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCheque.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapCheque.Name = "xrCapCheque";
            this.xrCapCheque.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapCheque.StylePriority.UseBorders = false;
            this.xrCapCheque.StylePriority.UseFont = false;
            this.xrCapCheque.StylePriority.UsePadding = false;
            this.xrCapCheque.StylePriority.UseTextAlignment = false;
            this.xrCapCheque.Text = "Cheque No";
            this.xrCapCheque.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapCheque.Weight = 0.1843451759817597D;
            // 
            // xrCapBank
            // 
            this.xrCapBank.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapBank.Name = "xrCapBank";
            this.xrCapBank.StylePriority.UseFont = false;
            this.xrCapBank.Text = "Bank";
            this.xrCapBank.Weight = 0.34235533796689793D;
            // 
            // xrCapIssuedDate
            // 
            this.xrCapIssuedDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapIssuedDate.Name = "xrCapIssuedDate";
            this.xrCapIssuedDate.StylePriority.UseFont = false;
            this.xrCapIssuedDate.Text = "Issued Date";
            this.xrCapIssuedDate.Weight = 0.17117766557702088D;
            // 
            // xrCapClearedDate
            // 
            this.xrCapClearedDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapClearedDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapClearedDate.Name = "xrCapClearedDate";
            this.xrCapClearedDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapClearedDate.StylePriority.UseBorders = false;
            this.xrCapClearedDate.StylePriority.UseFont = false;
            this.xrCapClearedDate.StylePriority.UsePadding = false;
            this.xrCapClearedDate.StylePriority.UseTextAlignment = false;
            this.xrCapClearedDate.Text = "Cleared Date";
            this.xrCapClearedDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapClearedDate.Weight = 0.17117767545243873D;
            // 
            // xrCapParticulars
            // 
            this.xrCapParticulars.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapParticulars.Name = "xrCapParticulars";
            this.xrCapParticulars.StylePriority.UseFont = false;
            this.xrCapParticulars.Text = "Particulars";
            this.xrCapParticulars.Weight = 0.34235533129906837D;
            // 
            // xrCapAmount
            // 
            this.xrCapAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapAmount.Name = "xrCapAmount";
            this.xrCapAmount.StylePriority.UseFont = false;
            this.xrCapAmount.StylePriority.UseTextAlignment = false;
            this.xrCapAmount.Text = "Amount";
            this.xrCapAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapAmount.Weight = 0.24491573638164985D;
            // 
            // xrCapIssuedAmount
            // 
            this.xrCapIssuedAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapIssuedAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapIssuedAmount.Name = "xrCapIssuedAmount";
            this.xrCapIssuedAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapIssuedAmount.StylePriority.UseBorders = false;
            this.xrCapIssuedAmount.StylePriority.UseFont = false;
            this.xrCapIssuedAmount.StylePriority.UsePadding = false;
            this.xrCapIssuedAmount.StylePriority.UseTextAlignment = false;
            this.xrCapIssuedAmount.Text = "Issued Amount";
            this.xrCapIssuedAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapIssuedAmount.Weight = 0.27651776152597723D;
            // 
            // xrCapNameAddress
            // 
            this.xrCapNameAddress.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapNameAddress.Name = "xrCapNameAddress";
            this.xrCapNameAddress.StylePriority.UseFont = false;
            this.xrCapNameAddress.Text = "Name & Address";
            this.xrCapNameAddress.Weight = 0.38218704019680061D;
            // 
            // xrCapRemarks
            // 
            this.xrCapRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapRemarks.Name = "xrCapRemarks";
            this.xrCapRemarks.StylePriority.UseFont = false;
            this.xrCapRemarks.Text = "Remarks";
            this.xrCapRemarks.Weight = 0.50266981438645975D;
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
            this.xrtblTotal.SizeF = new System.Drawing.SizeF(1149F, 25F);
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
            this.xrCapTotal.Weight = 2.2752751432176721D;
            // 
            // xrTotalAmt
            // 
            this.xrTotalAmt.BorderColor = System.Drawing.Color.Silver;
            this.xrTotalAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTotalAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrTotalAmt.Name = "xrTotalAmt";
            this.xrTotalAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrTotalAmt.StylePriority.UseBorderColor = false;
            this.xrTotalAmt.StylePriority.UseBorders = false;
            this.xrTotalAmt.StylePriority.UseFont = false;
            this.xrTotalAmt.StylePriority.UsePadding = false;
            this.xrTotalAmt.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.IgnoreNullValues = true;
            this.xrTotalAmt.Summary = xrSummary2;
            this.xrTotalAmt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTotalAmt.Weight = 0.33743487252148119D;
            this.xrTotalAmt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTotalAmt_BeforePrint);
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Weight = 1.0797915173266539D;
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
            this.xrtblCleared.SizeF = new System.Drawing.SizeF(1149F, 22F);
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
            this.xrSno,
            this.xrAcNumber,
            this.xrChequeNo,
            this.xrBank,
            this.xrPayDate,
            this.xrClearedDate,
            this.xrParticulars,
            this.xrAmount,
            this.xrIssuedAmount,
            this.xrTableCell2,
            this.xrRemarks});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableRow3.StylePriority.UsePadding = false;
            this.xrTableRow3.Weight = 0.88D;
            // 
            // xrSno
            // 
            this.xrSno.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.ACCOUNT_NUMBER")});
            this.xrSno.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrSno.Name = "xrSno";
            this.xrSno.StylePriority.UseFont = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrSno.Summary = xrSummary1;
            this.xrSno.Weight = 0.11274541981199422D;
            // 
            // xrAcNumber
            // 
            this.xrAcNumber.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrAcNumber.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.ACCOUNT_NUMBER", "{0:d}")});
            this.xrAcNumber.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrAcNumber.Name = "xrAcNumber";
            this.xrAcNumber.StyleName = "styleDateInfo";
            this.xrAcNumber.StylePriority.UseBorders = false;
            this.xrAcNumber.StylePriority.UseFont = false;
            this.xrAcNumber.StylePriority.UseTextAlignment = false;
            this.xrAcNumber.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrAcNumber.Weight = 0.38655574492147027D;
            // 
            // xrChequeNo
            // 
            this.xrChequeNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrChequeNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.CHEQUE_NO"),
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "ChequeUncleared.VOUCHER_ID")});
            this.xrChequeNo.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrChequeNo.Name = "xrChequeNo";
            this.xrChequeNo.ProcessDuplicates = DevExpress.XtraReports.UI.ValueSuppressType.MergeByTag;
            this.xrChequeNo.StylePriority.UseBorders = false;
            this.xrChequeNo.StylePriority.UseFont = false;
            this.xrChequeNo.Weight = 0.22549084712031878D;
            // 
            // xrBank
            // 
            this.xrBank.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.ISSUED_BANK")});
            this.xrBank.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrBank.Name = "xrBank";
            this.xrBank.StylePriority.UseFont = false;
            this.xrBank.Weight = 0.418768732515968D;
            // 
            // xrPayDate
            // 
            this.xrPayDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.VOUCHER_DATE", "{0:d}")});
            this.xrPayDate.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrPayDate.Name = "xrPayDate";
            this.xrPayDate.StylePriority.UseFont = false;
            this.xrPayDate.Weight = 0.20938436741305957D;
            // 
            // xrClearedDate
            // 
            this.xrClearedDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrClearedDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.MATERIALIZED_ON", "{0:d}")});
            this.xrClearedDate.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrClearedDate.Name = "xrClearedDate";
            this.xrClearedDate.StylePriority.UseBorders = false;
            this.xrClearedDate.StylePriority.UseFont = false;
            this.xrClearedDate.Weight = 0.20938436109309883D;
            // 
            // xrParticulars
            // 
            this.xrParticulars.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.LEDGER_NAME")});
            this.xrParticulars.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrParticulars.Name = "xrParticulars";
            this.xrParticulars.StylePriority.UseFont = false;
            this.xrParticulars.Weight = 0.41876872125102671D;
            // 
            // xrAmount
            // 
            this.xrAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.AMOUNT")});
            this.xrAmount.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrAmount.Name = "xrAmount";
            this.xrAmount.StylePriority.UseFont = false;
            this.xrAmount.StylePriority.UseTextAlignment = false;
            this.xrAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrAmount.Weight = 0.29958070178860796D;
            this.xrAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrAmount_BeforePrint);
            // 
            // xrIssuedAmount
            // 
            this.xrIssuedAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrIssuedAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.ISSUED_AMOUNT", "{0:n}"),
            new DevExpress.XtraReports.UI.XRBinding("Tag", null, "ChequeUncleared.VOUCHER_ID")});
            this.xrIssuedAmount.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrIssuedAmount.Name = "xrIssuedAmount";
            this.xrIssuedAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrIssuedAmount.ProcessDuplicates = DevExpress.XtraReports.UI.ValueSuppressType.MergeByTag;
            this.xrIssuedAmount.StylePriority.UseBorders = false;
            this.xrIssuedAmount.StylePriority.UseFont = false;
            this.xrIssuedAmount.StylePriority.UsePadding = false;
            this.xrIssuedAmount.StylePriority.UseTextAlignment = false;
            this.xrIssuedAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrIssuedAmount.Weight = 0.33823627915445265D;
            this.xrIssuedAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrIssuedAmount_BeforePrint);
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.NAME_ADDRESS")});
            this.xrTableCell2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Weight = 0.46749087053010163D;
            // 
            // xrRemarks
            // 
            this.xrRemarks.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ChequeUncleared.NARRATION")});
            this.xrRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.xrRemarks.Name = "xrRemarks";
            this.xrRemarks.StylePriority.UseFont = false;
            this.xrRemarks.Weight = 0.61486518242234911D;
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
            // BankChequeIssuedRegister
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(10, 3, 20, 20);
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
        private DevExpress.XtraReports.UI.XRTableCell xrCapAcNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCheque;
        private DevExpress.XtraReports.UI.XRTableCell xrCapClearedDate;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrCapTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCapIssuedAmount;
        private DevExpress.XtraReports.UI.XRTable xrtblCleared;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrAcNumber;
        private DevExpress.XtraReports.UI.XRTableCell xrChequeNo;
        private DevExpress.XtraReports.UI.XRTableCell xrClearedDate;
        private DevExpress.XtraReports.UI.XRTableCell xrIssuedAmount;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader1;
        private DevExpress.XtraReports.UI.XRTableCell xrRemarks;
        private DevExpress.XtraReports.UI.XRTableCell xrCapRemarks;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrCapNameAddress;
        private DevExpress.XtraReports.UI.XRTableCell xrPayDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapIssuedDate;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrSno;
        private DevExpress.XtraReports.UI.XRTableCell xrCapSno;
        private DevExpress.XtraReports.UI.XRTableCell xrBank;
        private DevExpress.XtraReports.UI.XRTableCell xrCapBank;
        private DevExpress.XtraReports.UI.XRTableCell xrAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrCapAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrCapParticulars;
    }
}
