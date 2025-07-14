namespace Bosco.Report.ReportObject
{
    partial class PayrollPTRegister
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
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeaderCaption = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapSNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapEmployeeName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapEGross = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapPT = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubPTRateDetails = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrTblTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrSumEmpty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSumEarnedGross = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSumPT = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrTblData = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrSno = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrEmployeeName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAcNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNetAmount = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblData});
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
            this.xrtblHeaderCaption.LocationFloat = new DevExpress.Utils.PointFloat(2F, 0F);
            this.xrtblHeaderCaption.Name = "xrtblHeaderCaption";
            this.xrtblHeaderCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblHeaderCaption.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblHeaderCaption.SizeF = new System.Drawing.SizeF(615.4589F, 25F);
            this.xrtblHeaderCaption.StyleName = "styleGroupRow";
            this.xrtblHeaderCaption.StylePriority.UsePadding = false;
            this.xrtblHeaderCaption.StylePriority.UseTextAlignment = false;
            this.xrtblHeaderCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapSNo,
            this.xrCapEmployeeName,
            this.xrCapEGross,
            this.xrCapPT});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrCapSNo
            // 
            this.xrCapSNo.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapSNo.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapSNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapSNo.BorderWidth = 1F;
            this.xrCapSNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapSNo.Name = "xrCapSNo";
            this.xrCapSNo.StyleName = "styleDateInfo";
            this.xrCapSNo.StylePriority.UseBackColor = false;
            this.xrCapSNo.StylePriority.UseBorderColor = false;
            this.xrCapSNo.StylePriority.UseBorders = false;
            this.xrCapSNo.StylePriority.UseBorderWidth = false;
            this.xrCapSNo.StylePriority.UseFont = false;
            this.xrCapSNo.StylePriority.UseTextAlignment = false;
            this.xrCapSNo.Text = "S.No";
            this.xrCapSNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCapSNo.Weight = 0.39446760019091759D;
            // 
            // xrCapEmployeeName
            // 
            this.xrCapEmployeeName.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapEmployeeName.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapEmployeeName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapEmployeeName.BorderWidth = 1F;
            this.xrCapEmployeeName.Name = "xrCapEmployeeName";
            this.xrCapEmployeeName.StylePriority.UseBackColor = false;
            this.xrCapEmployeeName.StylePriority.UseBorderColor = false;
            this.xrCapEmployeeName.StylePriority.UseBorders = false;
            this.xrCapEmployeeName.StylePriority.UseBorderWidth = false;
            this.xrCapEmployeeName.Text = "Name";
            this.xrCapEmployeeName.Weight = 2.7007248275108298D;
            // 
            // xrCapEGross
            // 
            this.xrCapEGross.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapEGross.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapEGross.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapEGross.Name = "xrCapEGross";
            this.xrCapEGross.StylePriority.UseBackColor = false;
            this.xrCapEGross.StylePriority.UseBorderColor = false;
            this.xrCapEGross.StylePriority.UseBorders = false;
            this.xrCapEGross.StylePriority.UseTextAlignment = false;
            this.xrCapEGross.Text = "Earned Gross";
            this.xrCapEGross.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapEGross.Weight = 1.5081622097223684D;
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
            this.xrCapPT.Text = "P.T.";
            this.xrCapPT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapPT.Weight = 1.4661106998599962D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubPTRateDetails,
            this.xrTblTotal});
            this.ReportFooter.HeightF = 54.95834F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrSubPTRateDetails
            // 
            this.xrSubPTRateDetails.LocationFloat = new DevExpress.Utils.PointFloat(2.000014F, 31.95834F);
            this.xrSubPTRateDetails.Name = "xrSubPTRateDetails";
            this.xrSubPTRateDetails.ReportSource = new Bosco.Report.ReportObject.PayrollPTRates();
            this.xrSubPTRateDetails.SizeF = new System.Drawing.SizeF(615.459F, 23F);
            // 
            // xrTblTotal
            // 
            this.xrTblTotal.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTblTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblTotal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTblTotal.LocationFloat = new DevExpress.Utils.PointFloat(2F, 0F);
            this.xrTblTotal.Name = "xrTblTotal";
            this.xrTblTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTblTotal.SizeF = new System.Drawing.SizeF(615.4589F, 25F);
            this.xrTblTotal.StyleName = "styleRow";
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
            this.xrSumEmpty.Weight = 1.3429958887997513D;
            // 
            // xrSumEarnedGross
            // 
            this.xrSumEarnedGross.BackColor = System.Drawing.Color.Gainsboro;
            this.xrSumEarnedGross.BorderColor = System.Drawing.Color.DarkGray;
            this.xrSumEarnedGross.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrSumEarnedGross.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.EARNED_GROSS")});
            this.xrSumEarnedGross.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrSumEarnedGross.Name = "xrSumEarnedGross";
            this.xrSumEarnedGross.StylePriority.UseBackColor = false;
            this.xrSumEarnedGross.StylePriority.UseBorderColor = false;
            this.xrSumEarnedGross.StylePriority.UseBorders = false;
            this.xrSumEarnedGross.StylePriority.UseFont = false;
            this.xrSumEarnedGross.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#,#}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrSumEarnedGross.Summary = xrSummary3;
            this.xrSumEarnedGross.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrSumEarnedGross.Weight = 0.65438764409765027D;
            // 
            // xrSumPT
            // 
            this.xrSumPT.BackColor = System.Drawing.Color.Gainsboro;
            this.xrSumPT.BorderColor = System.Drawing.Color.DarkGray;
            this.xrSumPT.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrSumPT.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.PT_AMOUNT")});
            this.xrSumPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrSumPT.Name = "xrSumPT";
            this.xrSumPT.StylePriority.UseBackColor = false;
            this.xrSumPT.StylePriority.UseBorderColor = false;
            this.xrSumPT.StylePriority.UseBorders = false;
            this.xrSumPT.StylePriority.UseFont = false;
            this.xrSumPT.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:#,0}";
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrSumPT.Summary = xrSummary4;
            this.xrSumPT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrSumPT.Weight = 0.63614163968907866D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrTblData
            // 
            this.xrTblData.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTblData.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblData.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTblData.LocationFloat = new DevExpress.Utils.PointFloat(1.999985F, 0F);
            this.xrTblData.Name = "xrTblData";
            this.xrTblData.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblData.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTblData.SizeF = new System.Drawing.SizeF(615.4589F, 25F);
            this.xrTblData.StyleName = "styleRow";
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
            this.xrSno,
            this.xrEmployeeName,
            this.xrAcNo,
            this.xrNetAmount});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrSno
            // 
            this.xrSno.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrSno.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYWages.StaffId")});
            this.xrSno.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrSno.Name = "xrSno";
            this.xrSno.StylePriority.UseBorders = false;
            this.xrSno.StylePriority.UseFont = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrSno.Summary = xrSummary1;
            this.xrSno.Weight = 0.1711584246360201D;
            // 
            // xrEmployeeName
            // 
            this.xrEmployeeName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.NAME")});
            this.xrEmployeeName.Name = "xrEmployeeName";
            this.xrEmployeeName.Weight = 1.1718374641637313D;
            // 
            // xrAcNo
            // 
            this.xrAcNo.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrAcNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.EARNED_GROSS", "{0:#,#}")});
            this.xrAcNo.Name = "xrAcNo";
            this.xrAcNo.StylePriority.UseBorders = false;
            this.xrAcNo.StylePriority.UseTextAlignment = false;
            this.xrAcNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrAcNo.Weight = 0.65438777468121179D;
            // 
            // xrNetAmount
            // 
            this.xrNetAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrNetAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYROLLREPORT.PT_AMOUNT", "{0:#,0}")});
            this.xrNetAmount.Name = "xrNetAmount";
            this.xrNetAmount.StylePriority.UseBorders = false;
            this.xrNetAmount.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            this.xrNetAmount.Summary = xrSummary2;
            this.xrNetAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrNetAmount.Weight = 0.63614150910551714D;
            // 
            // PayrollPTRegister
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.DataMember = "PAYROLLREPORT";
            this.DataSource = this.reportSetting1;
            this.Margins = new System.Drawing.Printing.Margins(42, 52, 20, 20);
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeaderCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrTblData;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrSno;
        private DevExpress.XtraReports.UI.XRTableCell xrEmployeeName;
        private DevExpress.XtraReports.UI.XRTableCell xrAcNo;
        private DevExpress.XtraReports.UI.XRTableCell xrNetAmount;
        private DevExpress.XtraReports.UI.XRTable xrtblHeaderCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapSNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCapEmployeeName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapEGross;
        private DevExpress.XtraReports.UI.XRTableCell xrCapPT;
        private DevExpress.XtraReports.UI.XRTable xrTblTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrSumEmpty;
        private DevExpress.XtraReports.UI.XRTableCell xrSumEarnedGross;
        private DevExpress.XtraReports.UI.XRTableCell xrSumPT;
        private DevExpress.XtraReports.UI.XRSubreport xrSubPTRateDetails;
    }
}
