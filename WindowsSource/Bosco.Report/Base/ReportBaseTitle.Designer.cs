namespace Bosco.Report.Base
{
    partial class ReportBaseTitle
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrlblReportSubTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblInstitute = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrDateInfo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrlnFooter = new DevExpress.XtraReports.UI.XRLine();
            this.styleInstitute = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.StyleReportSubTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleDateInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.stylePageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 28.5F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblReportSubTitle,
            this.xrlblReportTitle,
            this.xrlblInstitute});
            this.ReportHeader.HeightF = 87.5F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrlblReportSubTitle
            // 
            this.xrlblReportSubTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 56.25F);
            this.xrlblReportSubTitle.Name = "xrlblReportSubTitle";
            this.xrlblReportSubTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblReportSubTitle.SizeF = new System.Drawing.SizeF(627F, 28.125F);
            this.xrlblReportSubTitle.StyleName = "StyleReportSubTitle";
            this.xrlblReportSubTitle.StylePriority.UsePadding = false;
            this.xrlblReportSubTitle.StylePriority.UseTextAlignment = false;
            this.xrlblReportSubTitle.Text = "Consolidate Statement";
            // 
            // xrlblReportTitle
            // 
            this.xrlblReportTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 28.125F);
            this.xrlblReportTitle.Name = "xrlblReportTitle";
            this.xrlblReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblReportTitle.SizeF = new System.Drawing.SizeF(627F, 28.125F);
            this.xrlblReportTitle.StyleName = "styleReportTitle";
            this.xrlblReportTitle.StylePriority.UsePadding = false;
            this.xrlblReportTitle.StylePriority.UseTextAlignment = false;
            this.xrlblReportTitle.Text = "Monthly Abstract for the period of";
            // 
            // xrlblInstitute
            // 
            this.xrlblInstitute.CanShrink = true;
            this.xrlblInstitute.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrlblInstitute.Name = "xrlblInstitute";
            this.xrlblInstitute.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblInstitute.SizeF = new System.Drawing.SizeF(627F, 28.125F);
            this.xrlblInstitute.StyleName = "styleInstitute";
            this.xrlblInstitute.StylePriority.UsePadding = false;
            this.xrlblInstitute.StylePriority.UseTextAlignment = false;
            this.xrlblInstitute.Text = "Don Bosco Center, Yellagiri hills";
            this.xrlblInstitute.Visible = false;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrDateInfo,
            this.xrPageInfo,
            this.xrlnFooter});
            this.PageFooter.Name = "PageFooter";
            // 
            // xrDateInfo
            // 
            this.xrDateInfo.Format = "{0}";
            this.xrDateInfo.LocationFloat = new DevExpress.Utils.PointFloat(422.6249F, 7.000001F);
            this.xrDateInfo.Name = "xrDateInfo";
            this.xrDateInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrDateInfo.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrDateInfo.SizeF = new System.Drawing.SizeF(204.3751F, 23F);
            this.xrDateInfo.StyleName = "styleDateInfo";
            this.xrDateInfo.StylePriority.UsePadding = false;
            // 
            // xrPageInfo
            // 
            this.xrPageInfo.Format = "Page {0} of {1}";
            this.xrPageInfo.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 7F);
            this.xrPageInfo.Name = "xrPageInfo";
            this.xrPageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrPageInfo.SizeF = new System.Drawing.SizeF(147.0834F, 23F);
            this.xrPageInfo.StyleName = "stylePageInfo";
            this.xrPageInfo.StylePriority.UsePadding = false;
            // 
            // xrlnFooter
            // 
            this.xrlnFooter.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrlnFooter.Name = "xrlnFooter";
            this.xrlnFooter.SizeF = new System.Drawing.SizeF(627F, 7.291667F);
            // 
            // styleInstitute
            // 
            this.styleInstitute.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleInstitute.Name = "styleInstitute";
            this.styleInstitute.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleReportTitle
            // 
            this.styleReportTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportTitle.Name = "styleReportTitle";
            this.styleReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // StyleReportSubTitle
            // 
            this.StyleReportSubTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StyleReportSubTitle.Name = "StyleReportSubTitle";
            this.StyleReportSubTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.StyleReportSubTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleDateInfo
            // 
            this.styleDateInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleDateInfo.Name = "styleDateInfo";
            this.styleDateInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.styleDateInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // stylePageInfo
            // 
            this.stylePageInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stylePageInfo.Name = "stylePageInfo";
            this.stylePageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.stylePageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ReportBaseTitle
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageFooter});
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 28, 100);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleInstitute,
            this.styleReportTitle,
            this.StyleReportSubTitle,
            this.styleDateInfo,
            this.stylePageInfo});
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo;
        private DevExpress.XtraReports.UI.XRLine xrlnFooter;
        private DevExpress.XtraReports.UI.XRLabel xrlblInstitute;
        private DevExpress.XtraReports.UI.XRControlStyle styleInstitute;
        private DevExpress.XtraReports.UI.XRLabel xrlblReportTitle;
        private DevExpress.XtraReports.UI.XRControlStyle styleReportTitle;
        private DevExpress.XtraReports.UI.XRLabel xrlblReportSubTitle;
        private DevExpress.XtraReports.UI.XRControlStyle StyleReportSubTitle;
        private DevExpress.XtraReports.UI.XRControlStyle styleDateInfo;
        private DevExpress.XtraReports.UI.XRPageInfo xrDateInfo;
        private DevExpress.XtraReports.UI.XRControlStyle stylePageInfo;
        public DevExpress.XtraReports.UI.DetailBand Detail;
    }
}
