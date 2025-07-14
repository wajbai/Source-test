namespace Bosco.Report.ReportObject
{
    partial class PayrollYearPayRegister
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
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary1 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpHeaderStaff = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblStaffInfo = new DevExpress.XtraReports.UI.XRTable();
            this.xrRowStaffInfo = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCaptionStaffCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrStaffCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCaptionStaffName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrStaffName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCaptionStaffDesingation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrStaffDesingation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCaptionStaffPAN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrStaffPAN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCaptionUAN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrUAN = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCaptionAadharNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAadharNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpFooterStaff = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.grpPGHeader = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.grpPGFooter = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblStaffInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Visible = true;
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 25.41666F;
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
            this.xrSubSignFooter.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrSubSignFooter.Name = "xrSubSignFooter";
            this.xrSubSignFooter.ReportSource = new Bosco.Report.ReportObject.SignReportFooter();
            this.xrSubSignFooter.SizeF = new System.Drawing.SizeF(1112.424F, 55.29167F);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpHeaderStaff
            // 
            this.grpHeaderStaff.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblStaffInfo});
            this.grpHeaderStaff.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpHeaderStaff.HeightF = 24.37499F;
            this.grpHeaderStaff.Name = "grpHeaderStaff";
            xrGroupSortingSummary1.Enabled = true;
            xrGroupSortingSummary1.FieldName = "Name";
            xrGroupSortingSummary1.Function = DevExpress.XtraReports.UI.SortingSummaryFunction.Custom;
            this.grpHeaderStaff.SortingSummary = xrGroupSortingSummary1;
            // 
            // xrTblStaffInfo
            // 
            this.xrTblStaffInfo.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrTblStaffInfo.Name = "xrTblStaffInfo";
            this.xrTblStaffInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTblStaffInfo.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrRowStaffInfo});
            this.xrTblStaffInfo.SizeF = new System.Drawing.SizeF(1112.424F, 24.37499F);
            this.xrTblStaffInfo.StylePriority.UsePadding = false;
            // 
            // xrRowStaffInfo
            // 
            this.xrRowStaffInfo.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCaptionStaffCode,
            this.xrStaffCode,
            this.xrCaptionStaffName,
            this.xrStaffName,
            this.xrCaptionStaffDesingation,
            this.xrStaffDesingation,
            this.xrCaptionStaffPAN,
            this.xrStaffPAN,
            this.xrCaptionUAN,
            this.xrUAN,
            this.xrCaptionAadharNo,
            this.xrAadharNo});
            this.xrRowStaffInfo.Name = "xrRowStaffInfo";
            this.xrRowStaffInfo.Weight = 1D;
            // 
            // xrCaptionStaffCode
            // 
            this.xrCaptionStaffCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrCaptionStaffCode.Name = "xrCaptionStaffCode";
            this.xrCaptionStaffCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCaptionStaffCode.StylePriority.UseFont = false;
            this.xrCaptionStaffCode.StylePriority.UsePadding = false;
            this.xrCaptionStaffCode.StylePriority.UseTextAlignment = false;
            this.xrCaptionStaffCode.Text = "Code";
            this.xrCaptionStaffCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCaptionStaffCode.Weight = 0.3765378189145539D;
            // 
            // xrStaffCode
            // 
            this.xrStaffCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrStaffCode.Name = "xrStaffCode";
            this.xrStaffCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrStaffCode.StylePriority.UseFont = false;
            this.xrStaffCode.StylePriority.UsePadding = false;
            this.xrStaffCode.StylePriority.UseTextAlignment = false;
            this.xrStaffCode.Text = "Code";
            this.xrStaffCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrStaffCode.Weight = 0.51773949282024034D;
            this.xrStaffCode.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrStaffCode_BeforePrint);
            // 
            // xrCaptionStaffName
            // 
            this.xrCaptionStaffName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCaptionStaffName.Name = "xrCaptionStaffName";
            this.xrCaptionStaffName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCaptionStaffName.StylePriority.UseFont = false;
            this.xrCaptionStaffName.StylePriority.UsePadding = false;
            this.xrCaptionStaffName.StylePriority.UseTextAlignment = false;
            this.xrCaptionStaffName.Text = "Name";
            this.xrCaptionStaffName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCaptionStaffName.Weight = 0.39198217529758084D;
            // 
            // xrStaffName
            // 
            this.xrStaffName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrStaffName.Name = "xrStaffName";
            this.xrStaffName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrStaffName.StylePriority.UseFont = false;
            this.xrStaffName.StylePriority.UsePadding = false;
            this.xrStaffName.StylePriority.UseTextAlignment = false;
            this.xrStaffName.Text = "Name";
            this.xrStaffName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrStaffName.Weight = 2.2577945911736026D;
            // 
            // xrCaptionStaffDesingation
            // 
            this.xrCaptionStaffDesingation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCaptionStaffDesingation.Name = "xrCaptionStaffDesingation";
            this.xrCaptionStaffDesingation.StylePriority.UseFont = false;
            this.xrCaptionStaffDesingation.StylePriority.UseTextAlignment = false;
            this.xrCaptionStaffDesingation.Text = "Designation";
            this.xrCaptionStaffDesingation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCaptionStaffDesingation.Weight = 0.72249427954264622D;
            // 
            // xrStaffDesingation
            // 
            this.xrStaffDesingation.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrStaffDesingation.Name = "xrStaffDesingation";
            this.xrStaffDesingation.StylePriority.UseFont = false;
            this.xrStaffDesingation.StylePriority.UseTextAlignment = false;
            this.xrStaffDesingation.Text = "Designation";
            this.xrStaffDesingation.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrStaffDesingation.Weight = 1.3546767615458655D;
            // 
            // xrCaptionStaffPAN
            // 
            this.xrCaptionStaffPAN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrCaptionStaffPAN.Name = "xrCaptionStaffPAN";
            this.xrCaptionStaffPAN.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCaptionStaffPAN.StylePriority.UseFont = false;
            this.xrCaptionStaffPAN.StylePriority.UsePadding = false;
            this.xrCaptionStaffPAN.StylePriority.UseTextAlignment = false;
            this.xrCaptionStaffPAN.Text = "PAN No";
            this.xrCaptionStaffPAN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCaptionStaffPAN.Weight = 0.49671497215988614D;
            // 
            // xrStaffPAN
            // 
            this.xrStaffPAN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrStaffPAN.Name = "xrStaffPAN";
            this.xrStaffPAN.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrStaffPAN.StylePriority.UseFont = false;
            this.xrStaffPAN.StylePriority.UsePadding = false;
            this.xrStaffPAN.StylePriority.UseTextAlignment = false;
            this.xrStaffPAN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrStaffPAN.Weight = 0.81280631075638987D;
            // 
            // xrCaptionUAN
            // 
            this.xrCaptionUAN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCaptionUAN.Name = "xrCaptionUAN";
            this.xrCaptionUAN.StylePriority.UseFont = false;
            this.xrCaptionUAN.StylePriority.UseTextAlignment = false;
            this.xrCaptionUAN.Text = "UAN";
            this.xrCaptionUAN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCaptionUAN.Weight = 0.36124726255161332D;
            // 
            // xrUAN
            // 
            this.xrUAN.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrUAN.Name = "xrUAN";
            this.xrUAN.StylePriority.UseFont = false;
            this.xrUAN.StylePriority.UseTextAlignment = false;
            this.xrUAN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrUAN.Weight = 0.8128063299127255D;
            // 
            // xrCaptionAadharNo
            // 
            this.xrCaptionAadharNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCaptionAadharNo.Name = "xrCaptionAadharNo";
            this.xrCaptionAadharNo.StylePriority.UseFont = false;
            this.xrCaptionAadharNo.StylePriority.UseTextAlignment = false;
            this.xrCaptionAadharNo.Text = "Aadhar No";
            this.xrCaptionAadharNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCaptionAadharNo.Weight = 0.72249450152187022D;
            // 
            // xrAadharNo
            // 
            this.xrAadharNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrAadharNo.Name = "xrAadharNo";
            this.xrAadharNo.StylePriority.UseFont = false;
            this.xrAadharNo.StylePriority.UseTextAlignment = false;
            this.xrAadharNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrAadharNo.Weight = 1.2192080234094205D;
            // 
            // grpFooterStaff
            // 
            this.grpFooterStaff.Expanded = false;
            this.grpFooterStaff.HeightF = 25F;
            this.grpFooterStaff.Name = "grpFooterStaff";
            // 
            // grpPGHeader
            // 
            this.grpPGHeader.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpPGHeader.HeightF = 0F;
            this.grpPGHeader.Level = 1;
            this.grpPGHeader.Name = "grpPGHeader";
            this.grpPGHeader.Visible = false;
            // 
            // grpPGFooter
            // 
            this.grpPGFooter.HeightF = 0F;
            this.grpPGFooter.Level = 1;
            this.grpPGFooter.Name = "grpPGFooter";
            this.grpPGFooter.Visible = false;
            // 
            // PayrollYearPayRegister
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grpHeaderStaff,
            this.grpFooterStaff,
            this.grpPGHeader,
            this.grpPGFooter});
            this.DataMember = "PAYWages";
            this.DataSource = this.reportSetting1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(34, 20, 20, 20);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpPGFooter, 0);
            this.Controls.SetChildIndex(this.grpPGHeader, 0);
            this.Controls.SetChildIndex(this.grpFooterStaff, 0);
            this.Controls.SetChildIndex(this.grpHeaderStaff, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblStaffInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderStaff;
        private DevExpress.XtraReports.UI.XRTable xrTblStaffInfo;
        private DevExpress.XtraReports.UI.XRTableRow xrRowStaffInfo;
        private DevExpress.XtraReports.UI.XRTableCell xrCaptionStaffCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCaptionStaffName;
        private DevExpress.XtraReports.UI.XRTableCell xrCaptionStaffPAN;
        private DevExpress.XtraReports.UI.XRTableCell xrStaffCode;
        private DevExpress.XtraReports.UI.XRTableCell xrStaffName;
        private DevExpress.XtraReports.UI.XRTableCell xrStaffPAN;
        private DevExpress.XtraReports.UI.XRTableCell xrCaptionAadharNo;
        private DevExpress.XtraReports.UI.XRTableCell xrAadharNo;
        private DevExpress.XtraReports.UI.XRTableCell xrCaptionUAN;
        private DevExpress.XtraReports.UI.XRTableCell xrUAN;
        private DevExpress.XtraReports.UI.XRTableCell xrCaptionStaffDesingation;
        private DevExpress.XtraReports.UI.XRTableCell xrStaffDesingation;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooterStaff;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpPGHeader;
        private DevExpress.XtraReports.UI.GroupFooterBand grpPGFooter;
    }
}
