namespace Bosco.Report.ReportObject
{
    partial class PayrollPTRates
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTblData = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrPTRate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrEarnedGross = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPTAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTblTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrSumEmpty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSumEarnedGross = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSumPT = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLblSummary = new DevExpress.XtraReports.UI.XRLabel();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapEmployeeName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapEGross = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPT = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblData});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
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
            this.xrTblData.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblData.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTblData.SizeF = new System.Drawing.SizeF(358.1672F, 25F);
            this.xrTblData.StylePriority.UseBorderColor = false;
            this.xrTblData.StylePriority.UseBorders = false;
            this.xrTblData.StylePriority.UseFont = false;
            this.xrTblData.StylePriority.UsePadding = false;
            this.xrTblData.StylePriority.UseTextAlignment = false;
            this.xrTblData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrPTRate,
            this.xrEarnedGross,
            this.xrPTAmount});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrPTRate
            // 
            this.xrPTRate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.RATE_OF_PT")});
            this.xrPTRate.Name = "xrPTRate";
            this.xrPTRate.Weight = 0.72343786813934363D;
            // 
            // xrEarnedGross
            // 
            this.xrEarnedGross.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrEarnedGross.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.NO_STAFFS")});
            this.xrEarnedGross.Name = "xrEarnedGross";
            this.xrEarnedGross.StylePriority.UseBorders = false;
            this.xrEarnedGross.StylePriority.UseTextAlignment = false;
            this.xrEarnedGross.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrEarnedGross.Weight = 0.27106420774340356D;
            // 
            // xrPTAmount
            // 
            this.xrPTAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPTAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.TOTAL_PT_AMOUNT", "{0:n}")});
            this.xrPTAmount.Name = "xrPTAmount";
            this.xrPTAmount.StylePriority.UseBorders = false;
            this.xrPTAmount.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            this.xrPTAmount.Summary = xrSummary1;
            this.xrPTAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrPTAmount.Weight = 0.53808169202035183D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblTotal});
            this.ReportFooter.HeightF = 25F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTblTotal
            // 
            this.xrTblTotal.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTblTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblTotal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTblTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTblTotal.Name = "xrTblTotal";
            this.xrTblTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTblTotal.SizeF = new System.Drawing.SizeF(358.1672F, 25F);
            this.xrTblTotal.StylePriority.UseBorderColor = false;
            this.xrTblTotal.StylePriority.UseBorders = false;
            this.xrTblTotal.StylePriority.UseFont = false;
            this.xrTblTotal.StylePriority.UsePadding = false;
            this.xrTblTotal.StylePriority.UseTextAlignment = false;
            this.xrTblTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrSumEmpty,
            this.xrSumEarnedGross,
            this.xrSumPT});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrSumEmpty
            // 
            this.xrSumEmpty.BackColor = System.Drawing.Color.Gainsboro;
            this.xrSumEmpty.BorderColor = System.Drawing.Color.DarkGray;
            this.xrSumEmpty.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrSumEmpty.Name = "xrSumEmpty";
            this.xrSumEmpty.StylePriority.UseBackColor = false;
            this.xrSumEmpty.StylePriority.UseBorderColor = false;
            this.xrSumEmpty.StylePriority.UseBorders = false;
            this.xrSumEmpty.Weight = 0.72343786813934374D;
            // 
            // xrSumEarnedGross
            // 
            this.xrSumEarnedGross.BackColor = System.Drawing.Color.Gainsboro;
            this.xrSumEarnedGross.BorderColor = System.Drawing.Color.DarkGray;
            this.xrSumEarnedGross.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrSumEarnedGross.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.NO_STAFFS")});
            this.xrSumEarnedGross.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrSumEarnedGross.Name = "xrSumEarnedGross";
            this.xrSumEarnedGross.StylePriority.UseBackColor = false;
            this.xrSumEarnedGross.StylePriority.UseBorderColor = false;
            this.xrSumEarnedGross.StylePriority.UseBorders = false;
            this.xrSumEarnedGross.StylePriority.UseFont = false;
            this.xrSumEarnedGross.StylePriority.UseTextAlignment = false;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrSumEarnedGross.Summary = xrSummary2;
            this.xrSumEarnedGross.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrSumEarnedGross.Weight = 0.27106401186806767D;
            // 
            // xrSumPT
            // 
            this.xrSumPT.BackColor = System.Drawing.Color.Gainsboro;
            this.xrSumPT.BorderColor = System.Drawing.Color.DarkGray;
            this.xrSumPT.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrSumPT.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.TOTAL_PT_AMOUNT")});
            this.xrSumPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrSumPT.Name = "xrSumPT";
            this.xrSumPT.StylePriority.UseBackColor = false;
            this.xrSumPT.StylePriority.UseBorderColor = false;
            this.xrSumPT.StylePriority.UseBorders = false;
            this.xrSumPT.StylePriority.UseFont = false;
            this.xrSumPT.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:n}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrSumPT.Summary = xrSummary3;
            this.xrSumPT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrSumPT.Weight = 0.53808195318745922D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLblSummary,
            this.xrtblHeaderCaption});
            this.PageHeader.HeightF = 49F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLblSummary
            // 
            this.xrLblSummary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrLblSummary.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLblSummary.Name = "xrLblSummary";
            this.xrLblSummary.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblSummary.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLblSummary.StylePriority.UseFont = false;
            this.xrLblSummary.StylePriority.UseTextAlignment = false;
            this.xrLblSummary.Text = "Summary";
            this.xrLblSummary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrtblHeaderCaption
            // 
            this.xrtblHeaderCaption.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24F);
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.SizeF = new System.Drawing.SizeF(358.1672F, 25F);
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            this.xrtblHeaderCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapEmployeeName,
            this.xrCapEGross,
            this.xrCapPT});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrCapEmployeeName
            // 
            this.xrCapEmployeeName.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapEmployeeName.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapEmployeeName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapEmployeeName.BorderWidth = 1F;
            this.xrCapEmployeeName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapEmployeeName.Name = "xrCapEmployeeName";
            this.xrCapEmployeeName.StylePriority.UseBackColor = false;
            this.xrCapEmployeeName.StylePriority.UseBorderColor = false;
            this.xrCapEmployeeName.StylePriority.UseBorders = false;
            this.xrCapEmployeeName.StylePriority.UseBorderWidth = false;
            this.xrCapEmployeeName.StylePriority.UseFont = false;
            this.xrCapEmployeeName.Text = "Rate Of PT";
            this.xrCapEmployeeName.Weight = 1.6673020937091674D;
            // 
            // xrCapEGross
            // 
            this.xrCapEGross.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapEGross.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapEGross.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapEGross.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapEGross.Name = "xrCapEGross";
            this.xrCapEGross.StylePriority.UseBackColor = false;
            this.xrCapEGross.StylePriority.UseBorderColor = false;
            this.xrCapEGross.StylePriority.UseBorders = false;
            this.xrCapEGross.StylePriority.UseFont = false;
            this.xrCapEGross.StylePriority.UseTextAlignment = false;
            this.xrCapEGross.Text = "No of Staff";
            this.xrCapEGross.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapEGross.Weight = 0.62471826139236286D;
            // 
            // xrCapPT
            // 
            this.xrCapPT.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapPT.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapPT.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapPT.Name = "xrCapPT";
            this.xrCapPT.StylePriority.UseBackColor = false;
            this.xrCapPT.StylePriority.UseBorderColor = false;
            this.xrCapPT.StylePriority.UseBorders = false;
            this.xrCapPT.StylePriority.UseFont = false;
            this.xrCapPT.StylePriority.UseTextAlignment = false;
            this.xrCapPT.Text = "P.T. Amount";
            this.xrCapPT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapPT.Weight = 1.2401140422350374D;
            // 
            // PayrollPTRates
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.ReportFooter,
            this.PageHeader});
            this.DataMember = "PAYROLLREPORT";
            this.DataSource = this.reportSetting1;
            this.Margins = new System.Drawing.Printing.Margins(6, 132, 20, 20);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTblData;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrPTRate;
        private DevExpress.XtraReports.UI.XRTableCell xrEarnedGross;
        private DevExpress.XtraReports.UI.XRTableCell xrPTAmount;
        private DevExpress.XtraReports.UI.XRTable xrTblTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrSumEmpty;
        private DevExpress.XtraReports.UI.XRTableCell xrSumEarnedGross;
        private DevExpress.XtraReports.UI.XRTableCell xrSumPT;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapEmployeeName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapEGross;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPT;
        private DevExpress.XtraReports.UI.XRLabel xrLblSummary;
    }
}
