namespace Bosco.Report.ReportObject
{
    partial class UcTOC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcTOC));
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrtblDetails = new DevExpress.XtraReports.UI.XRTable();
            this.xrRowCCDetails = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellTOCName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellPageNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.GrpTOCHeader = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblTocHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblTocHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblDetails});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrtblDetails
            // 
            resources.ApplyResources(this.xrtblDetails, "xrtblDetails");
            this.xrtblDetails.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblDetails.Name = "xrtblDetails";
            this.xrtblDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblDetails.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrRowCCDetails});
            this.xrtblDetails.StyleName = "styleRow";
            this.xrtblDetails.StylePriority.UseBorderColor = false;
            this.xrtblDetails.StylePriority.UseBorders = false;
            this.xrtblDetails.StylePriority.UseFont = false;
            this.xrtblDetails.StylePriority.UsePadding = false;
            // 
            // xrRowCCDetails
            // 
            this.xrRowCCDetails.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellTOCName,
            this.xrCellPageNo});
            this.xrRowCCDetails.Name = "xrRowCCDetails";
            resources.ApplyResources(this.xrRowCCDetails, "xrRowCCDetails");
            // 
            // xrCellTOCName
            // 
            resources.ApplyResources(this.xrCellTOCName, "xrCellTOCName");
            this.xrCellTOCName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellTOCName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TOC.TOC_NAME")});
            this.xrCellTOCName.Name = "xrCellTOCName";
            this.xrCellTOCName.NavigateUrl = "xrlblReportTitle";
            this.xrCellTOCName.StylePriority.UseBorderColor = false;
            this.xrCellTOCName.StylePriority.UseBorders = false;
            this.xrCellTOCName.StylePriority.UseFont = false;
            this.xrCellTOCName.StylePriority.UseTextAlignment = false;
            this.xrCellTOCName.Target = "_self";
            this.xrCellTOCName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCellTOCName_BeforePrint);
            // 
            // xrCellPageNo
            // 
            resources.ApplyResources(this.xrCellPageNo, "xrCellPageNo");
            this.xrCellPageNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellPageNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TOC.PAGE_NO")});
            this.xrCellPageNo.Name = "xrCellPageNo";
            this.xrCellPageNo.NavigateUrl = "xrlblReportTitle";
            this.xrCellPageNo.StylePriority.UseBorderColor = false;
            this.xrCellPageNo.StylePriority.UseBorders = false;
            this.xrCellPageNo.StylePriority.UseFont = false;
            this.xrCellPageNo.StylePriority.UseTextAlignment = false;
            this.xrCellPageNo.Target = "_self";
            this.xrCellPageNo.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCellPageNo_BeforePrint);
            this.xrCellPageNo.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.xrCellPageNo_PrintOnPage);
            // 
            // ReportFooter
            // 
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // GrpTOCHeader
            // 
            this.GrpTOCHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblTocHeader});
            resources.ApplyResources(this.GrpTOCHeader, "GrpTOCHeader");
            this.GrpTOCHeader.Name = "GrpTOCHeader";
            // 
            // xrTblTocHeader
            // 
            resources.ApplyResources(this.xrTblTocHeader, "xrTblTocHeader");
            this.xrTblTocHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblTocHeader.Name = "xrTblTocHeader";
            this.xrTblTocHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblTocHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTblTocHeader.StyleName = "styleRow";
            this.xrTblTocHeader.StylePriority.UseBorderColor = false;
            this.xrTblTocHeader.StylePriority.UseBorders = false;
            this.xrTblTocHeader.StylePriority.UseFont = false;
            this.xrTblTocHeader.StylePriority.UsePadding = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrTableCell1
            // 
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            // 
            // UcTOC
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.ReportFooter,
            this.GrpTOCHeader});
            this.DataMember = "TOC";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GrpTOCHeader, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblTocHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrtblDetails;
        private DevExpress.XtraReports.UI.XRTableRow xrRowCCDetails;
        private DevExpress.XtraReports.UI.XRTableCell xrCellTOCName;
        private DevExpress.XtraReports.UI.XRTableCell xrCellPageNo;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.GroupHeaderBand GrpTOCHeader;
        private DevExpress.XtraReports.UI.XRTable xrTblTocHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
    }
}
