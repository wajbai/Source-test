namespace Bosco.Report.ReportObject
{
    partial class PayrollPostedVouchers
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
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpHeaderPaymonth = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapVNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPayGroup = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapProject = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapCashBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblPayMonth = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellPayMonth = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpFooterPaymonth = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashBank = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrProject = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPayGroup = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrVNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTblData = new DevExpress.XtraReports.UI.XRTable();
            this.xrSubPostedPayrollVoucherDetail = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblPayMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubPostedPayrollVoucherDetail,
            this.xrTblData});
            this.Detail.HeightF = 40F;
            this.Detail.Visible = true;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 0F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Visible = false;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubSignFooter});
            this.ReportFooter.HeightF = 55.29167F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrSubSignFooter
            // 
            this.xrSubSignFooter.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrSubSignFooter.Name = "xrSubSignFooter";
            this.xrSubSignFooter.ReportSource = new Bosco.Report.ReportObject.SignReportFooter();
            this.xrSubSignFooter.SizeF = new System.Drawing.SizeF(804F, 55.29167F);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpHeaderPaymonth
            // 
            this.grpHeaderPaymonth.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblHeader,
            this.xrtblPayMonth});
            this.grpHeaderPaymonth.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("PRNAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpHeaderPaymonth.HeightF = 58F;
            this.grpHeaderPaymonth.Name = "grpHeaderPaymonth";
            // 
            // xrTblHeader
            // 
            this.xrTblHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTblHeader.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTblHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTblHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 33F);
            this.xrTblHeader.Name = "xrTblHeader";
            this.xrTblHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTblHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTblHeader.SizeF = new System.Drawing.SizeF(804F, 25F);
            this.xrTblHeader.StylePriority.UseBackColor = false;
            this.xrTblHeader.StylePriority.UseBorderColor = false;
            this.xrTblHeader.StylePriority.UseBorders = false;
            this.xrTblHeader.StylePriority.UseFont = false;
            this.xrTblHeader.StylePriority.UsePadding = false;
            this.xrTblHeader.StylePriority.UseTextAlignment = false;
            this.xrTblHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapDate,
            this.xrCapVNo,
            this.xrCapPayGroup,
            this.xrCapProject,
            this.xrCapLedger,
            this.xrCapCashBank,
            this.xrCapAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrCapDate
            // 
            this.xrCapDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapDate.StylePriority.UseFont = false;
            this.xrCapDate.StylePriority.UsePadding = false;
            this.xrCapDate.StylePriority.UseTextAlignment = false;
            this.xrCapDate.Text = "Date";
            this.xrCapDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapDate.Weight = 0.67444021518013364D;
            // 
            // xrCapVNo
            // 
            this.xrCapVNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapVNo.Name = "xrCapVNo";
            this.xrCapVNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapVNo.StylePriority.UseFont = false;
            this.xrCapVNo.StylePriority.UsePadding = false;
            this.xrCapVNo.StylePriority.UseTextAlignment = false;
            this.xrCapVNo.Text = "V.No";
            this.xrCapVNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapVNo.Weight = 0.40466413152121089D;
            // 
            // xrCapPayGroup
            // 
            this.xrCapPayGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapPayGroup.Name = "xrCapPayGroup";
            this.xrCapPayGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapPayGroup.StylePriority.UseFont = false;
            this.xrCapPayGroup.StylePriority.UsePadding = false;
            this.xrCapPayGroup.StylePriority.UseTextAlignment = false;
            this.xrCapPayGroup.Text = "Pay Group";
            this.xrCapPayGroup.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapPayGroup.Weight = 1.213992357110802D;
            // 
            // xrCapProject
            // 
            this.xrCapProject.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapProject.Name = "xrCapProject";
            this.xrCapProject.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapProject.StylePriority.UseFont = false;
            this.xrCapProject.StylePriority.UsePadding = false;
            this.xrCapProject.StylePriority.UseTextAlignment = false;
            this.xrCapProject.Text = "Project";
            this.xrCapProject.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapProject.Weight = 1.3488804308112297D;
            // 
            // xrCapLedger
            // 
            this.xrCapLedger.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapLedger.Name = "xrCapLedger";
            this.xrCapLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapLedger.StylePriority.UseFont = false;
            this.xrCapLedger.StylePriority.UsePadding = false;
            this.xrCapLedger.StylePriority.UseTextAlignment = false;
            this.xrCapLedger.Text = "Ledger";
            this.xrCapLedger.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapLedger.Weight = 1.3488804242563752D;
            // 
            // xrCapCashBank
            // 
            this.xrCapCashBank.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapCashBank.Name = "xrCapCashBank";
            this.xrCapCashBank.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapCashBank.StylePriority.UseFont = false;
            this.xrCapCashBank.StylePriority.UsePadding = false;
            this.xrCapCashBank.StylePriority.UseTextAlignment = false;
            this.xrCapCashBank.Text = "Cash/Bank";
            this.xrCapCashBank.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCapCashBank.Weight = 1.348880396837391D;
            // 
            // xrCapAmount
            // 
            this.xrCapAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrCapAmount.Name = "xrCapAmount";
            this.xrCapAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCapAmount.StylePriority.UseFont = false;
            this.xrCapAmount.StylePriority.UsePadding = false;
            this.xrCapAmount.StylePriority.UseTextAlignment = false;
            this.xrCapAmount.Text = "Amount";
            this.xrCapAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapAmount.Weight = 0.89026107606968419D;
            // 
            // xrtblPayMonth
            // 
            this.xrtblPayMonth.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrtblPayMonth.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblPayMonth.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblPayMonth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrtblPayMonth.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.xrtblPayMonth.Name = "xrtblPayMonth";
            this.xrtblPayMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblPayMonth.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblPayMonth.SizeF = new System.Drawing.SizeF(804F, 25F);
            this.xrtblPayMonth.StyleName = "styleGroupRow";
            this.xrtblPayMonth.StylePriority.UseBackColor = false;
            this.xrtblPayMonth.StylePriority.UseBorderColor = false;
            this.xrtblPayMonth.StylePriority.UseBorders = false;
            this.xrtblPayMonth.StylePriority.UseFont = false;
            this.xrtblPayMonth.StylePriority.UsePadding = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellPayMonth});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrcellPayMonth
            // 
            this.xrcellPayMonth.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PayrollPostedVouchers.PRNAME")});
            this.xrcellPayMonth.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrcellPayMonth.Name = "xrcellPayMonth";
            this.xrcellPayMonth.StylePriority.UseFont = false;
            this.xrcellPayMonth.Weight = 0.38876874585684684D;
            // 
            // grpFooterPaymonth
            // 
            this.grpFooterPaymonth.HeightF = 0F;
            this.grpFooterPaymonth.Name = "grpFooterPaymonth";
            this.grpFooterPaymonth.Visible = false;
            // 
            // xrAmount
            // 
            this.xrAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PayrollPostedVouchers.DEBIT", "{0:#,#}")});
            this.xrAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrAmount.Name = "xrAmount";
            this.xrAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrAmount.StylePriority.UseFont = false;
            this.xrAmount.StylePriority.UsePadding = false;
            this.xrAmount.StylePriority.UseTextAlignment = false;
            this.xrAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrAmount.Weight = 0.4172941362601707D;
            // 
            // xrCashBank
            // 
            this.xrCashBank.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashBank.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PayrollPostedVouchers.CASHBANK")});
            this.xrCashBank.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrCashBank.Name = "xrCashBank";
            this.xrCashBank.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCashBank.StylePriority.UseBorders = false;
            this.xrCashBank.StylePriority.UseFont = false;
            this.xrCashBank.StylePriority.UsePadding = false;
            this.xrCashBank.StylePriority.UseTextAlignment = false;
            this.xrCashBank.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCashBank.Weight = 0.63226374086441706D;
            // 
            // xrLedger
            // 
            this.xrLedger.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLedger.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PayrollPostedVouchers.LEDGER_NAME", "{0:#,#}")});
            this.xrLedger.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLedger.Name = "xrLedger";
            this.xrLedger.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrLedger.StylePriority.UseBorders = false;
            this.xrLedger.StylePriority.UseFont = false;
            this.xrLedger.StylePriority.UsePadding = false;
            this.xrLedger.StylePriority.UseTextAlignment = false;
            this.xrLedger.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLedger.Weight = 0.63226375896182019D;
            // 
            // xrProject
            // 
            this.xrProject.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrProject.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PayrollPostedVouchers.PROJECT")});
            this.xrProject.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrProject.Name = "xrProject";
            this.xrProject.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrProject.StylePriority.UseBorders = false;
            this.xrProject.StylePriority.UseFont = false;
            this.xrProject.StylePriority.UsePadding = false;
            this.xrProject.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            this.xrProject.Summary = xrSummary1;
            this.xrProject.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrProject.Weight = 0.632263733542147D;
            // 
            // xrPayGroup
            // 
            this.xrPayGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPayGroup.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PayrollPostedVouchers.PAYROLL_GROUP", "{0:#,#}")});
            this.xrPayGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPayGroup.Name = "xrPayGroup";
            this.xrPayGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrPayGroup.StylePriority.UseBorders = false;
            this.xrPayGroup.StylePriority.UseFont = false;
            this.xrPayGroup.StylePriority.UsePadding = false;
            this.xrPayGroup.StylePriority.UseTextAlignment = false;
            this.xrPayGroup.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrPayGroup.Weight = 0.569037354779811D;
            // 
            // xrVNo
            // 
            this.xrVNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PayrollPostedVouchers.VOUCHER_NO")});
            this.xrVNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrVNo.Name = "xrVNo";
            this.xrVNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrVNo.StylePriority.UseFont = false;
            this.xrVNo.StylePriority.UsePadding = false;
            this.xrVNo.StylePriority.UseTextAlignment = false;
            this.xrVNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrVNo.Weight = 0.18967913593680674D;
            // 
            // xrDate
            // 
            this.xrDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PayrollPostedVouchers.VOUCHER_DATE", "{0:d}")});
            this.xrDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrDate.Name = "xrDate";
            this.xrDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrDate.StylePriority.UseFont = false;
            this.xrDate.StylePriority.UsePadding = false;
            this.xrDate.StylePriority.UseTextAlignment = false;
            this.xrDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrDate.Weight = 0.31613187545701321D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrDate,
            this.xrVNo,
            this.xrPayGroup,
            this.xrProject,
            this.xrLedger,
            this.xrCashBank,
            this.xrAmount});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.8D;
            // 
            // xrTblData
            // 
            this.xrTblData.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTblData.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblData.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTblData.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTblData.Name = "xrTblData";
            this.xrTblData.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTblData.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTblData.SizeF = new System.Drawing.SizeF(804F, 20F);
            this.xrTblData.StyleName = "styleRow";
            this.xrTblData.StylePriority.UseBorderColor = false;
            this.xrTblData.StylePriority.UseBorders = false;
            this.xrTblData.StylePriority.UseFont = false;
            this.xrTblData.StylePriority.UsePadding = false;
            this.xrTblData.StylePriority.UseTextAlignment = false;
            this.xrTblData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrSubPostedPayrollVoucherDetail
            // 
            this.xrSubPostedPayrollVoucherDetail.CanShrink = true;
            this.xrSubPostedPayrollVoucherDetail.LocationFloat = new DevExpress.Utils.PointFloat(75.00001F, 20F);
            this.xrSubPostedPayrollVoucherDetail.Name = "xrSubPostedPayrollVoucherDetail";
            this.xrSubPostedPayrollVoucherDetail.ReportSource = new Bosco.Report.ReportObject.UcPostedPayrollVoucherDetail();
            this.xrSubPostedPayrollVoucherDetail.SizeF = new System.Drawing.SizeF(729F, 20F);
            this.xrSubPostedPayrollVoucherDetail.Visible = false;
            // 
            // PayrollPostedVouchers
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grpHeaderPaymonth,
            this.grpFooterPaymonth});
            this.DataMember = "PayrollPostedVouchers";
            this.DataSource = this.reportSetting1;
            this.Margins = new System.Drawing.Printing.Margins(11, 8, 20, 20);
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpFooterPaymonth, 0);
            this.Controls.SetChildIndex(this.grpHeaderPaymonth, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblPayMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderPaymonth;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooterPaymonth;
        private DevExpress.XtraReports.UI.XRTable xrTblData;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrVNo;
        private DevExpress.XtraReports.UI.XRTableCell xrPayGroup;
        private DevExpress.XtraReports.UI.XRTableCell xrProject;
        private DevExpress.XtraReports.UI.XRTableCell xrLedger;
        private DevExpress.XtraReports.UI.XRTableCell xrCashBank;
        private DevExpress.XtraReports.UI.XRTableCell xrAmount;
        private DevExpress.XtraReports.UI.XRTable xrtblPayMonth;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrcellPayMonth;
        private DevExpress.XtraReports.UI.XRTable xrTblHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapVNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPayGroup;
        private DevExpress.XtraReports.UI.XRTableCell xrCapProject;
        private DevExpress.XtraReports.UI.XRTableCell xrCapLedger;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCashBank;
        private DevExpress.XtraReports.UI.XRTableCell xrCapAmount;
        private DevExpress.XtraReports.UI.XRSubreport xrSubPostedPayrollVoucherDetail;
    }
}
