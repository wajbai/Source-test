namespace Bosco.Report.ReportObject
{
    partial class PAYROLLPayRegisterReport
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
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpReportFooter = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.grpHeaderPayGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblPayGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellPayGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpFooterPayGroup = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblPayGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Visible = true;
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
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
            this.xrSubSignFooter.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            this.xrSubSignFooter.Name = "xrSubSignFooter";
            this.xrSubSignFooter.ReportSource = new Bosco.Report.ReportObject.SignReportFooter();
            this.xrSubSignFooter.SizeF = new System.Drawing.SizeF(1127F, 55.29167F);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpReportFooter
            // 
            this.grpReportFooter.HeightF = 0F;
            this.grpReportFooter.Level = 1;
            this.grpReportFooter.Name = "grpReportFooter";
            // 
            // grpHeaderPayGroup
            // 
            this.grpHeaderPayGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblPayGroup});
            this.grpHeaderPayGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("GROUPNAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpHeaderPayGroup.HeightF = 20F;
            this.grpHeaderPayGroup.Name = "grpHeaderPayGroup";
            this.grpHeaderPayGroup.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpHeaderGroup_BeforePrint);
            // 
            // xrtblPayGroup
            // 
            this.xrtblPayGroup.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrtblPayGroup.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblPayGroup.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblPayGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrtblPayGroup.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrtblPayGroup.Name = "xrtblPayGroup";
            this.xrtblPayGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblPayGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblPayGroup.SizeF = new System.Drawing.SizeF(1127F, 20F);
            this.xrtblPayGroup.StyleName = "styleGroupRow";
            this.xrtblPayGroup.StylePriority.UseBackColor = false;
            this.xrtblPayGroup.StylePriority.UseBorderColor = false;
            this.xrtblPayGroup.StylePriority.UseBorders = false;
            this.xrtblPayGroup.StylePriority.UseFont = false;
            this.xrtblPayGroup.StylePriority.UsePadding = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellPayGroupName});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrcellPayGroupName
            // 
            this.xrcellPayGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYWages.GROUPNAME"),
            new DevExpress.XtraReports.UI.XRBinding("Bookmark", null, "Ledger.LEDGER_NAME")});
            this.xrcellPayGroupName.Name = "xrcellPayGroupName";
            this.xrcellPayGroupName.Weight = 0.38876874585684684D;
            // 
            // grpFooterPayGroup
            // 
            this.grpFooterPayGroup.HeightF = 25F;
            this.grpFooterPayGroup.Name = "grpFooterPayGroup";
            // 
            // PAYROLLPayRegisterReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grpReportFooter,
            this.grpHeaderPayGroup,
            this.grpFooterPayGroup});
            this.DataMember = "PAYWages";
            this.DataSource = this.reportSetting1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(20, 10, 20, 20);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.Version = "13.2";
            this.AfterPrint += new System.EventHandler(this.PAYROLLPayRegisterReport_AfterPrint);
            this.Controls.SetChildIndex(this.grpFooterPayGroup, 0);
            this.Controls.SetChildIndex(this.grpHeaderPayGroup, 0);
            this.Controls.SetChildIndex(this.grpReportFooter, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblPayGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
        private DevExpress.XtraReports.UI.GroupFooterBand grpReportFooter;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderPayGroup;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooterPayGroup;
        private DevExpress.XtraReports.UI.XRTable xrtblPayGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrcellPayGroupName;
    }
}
