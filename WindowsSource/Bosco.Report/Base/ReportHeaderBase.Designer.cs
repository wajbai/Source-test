namespace Bosco.Report.Base
{
    partial class ReportHeaderBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportHeaderBase));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrlblBudgetname = new DevExpress.XtraReports.UI.XRLabel();
            this.xrInstituteAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.xrInstituteName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblCostCenter = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDateRange = new DevExpress.XtraReports.UI.XRLabel();
            this.xrpicReportLogoLeft = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrlblReportSubTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblInstituteProjectName = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrlblProjectName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrlnFooter = new DevExpress.XtraReports.UI.XRLine();
            this.styleInstitute = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportSubTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleDateInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.stylePageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleOddRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleEvenRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeader = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTitleRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRowBase = new DevExpress.XtraReports.UI.XRControlStyle();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // TopMargin
            // 
            resources.ApplyResources(this.TopMargin, "TopMargin");
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // BottomMargin
            // 
            resources.ApplyResources(this.BottomMargin, "BottomMargin");
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblBudgetname,
            this.xrInstituteAddress,
            this.xrInstituteName,
            this.xrlblCostCenter,
            this.xrDateRange,
            this.xrpicReportLogoLeft,
            this.xrlblReportSubTitle,
            this.xrlblReportTitle,
            this.xrlblInstituteProjectName});
            resources.ApplyResources(this.ReportHeader, "ReportHeader");
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrlblBudgetname
            // 
            resources.ApplyResources(this.xrlblBudgetname, "xrlblBudgetname");
            this.xrlblBudgetname.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrlblBudgetname.Name = "xrlblBudgetname";
            this.xrlblBudgetname.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrlblBudgetname.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblBudgetname.StylePriority.UseBackColor = false;
            this.xrlblBudgetname.StylePriority.UseBorderColor = false;
            this.xrlblBudgetname.StylePriority.UseBorders = false;
            this.xrlblBudgetname.StylePriority.UseFont = false;
            this.xrlblBudgetname.StylePriority.UseForeColor = false;
            this.xrlblBudgetname.StylePriority.UsePadding = false;
            this.xrlblBudgetname.StylePriority.UseTextAlignment = false;
            // 
            // xrInstituteAddress
            // 
            this.xrInstituteAddress.CanShrink = true;
            resources.ApplyResources(this.xrInstituteAddress, "xrInstituteAddress");
            this.xrInstituteAddress.Name = "xrInstituteAddress";
            this.xrInstituteAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrInstituteAddress.StylePriority.UseFont = false;
            this.xrInstituteAddress.StylePriority.UseForeColor = false;
            this.xrInstituteAddress.StylePriority.UsePadding = false;
            this.xrInstituteAddress.StylePriority.UseTextAlignment = false;
            // 
            // xrInstituteName
            // 
            this.xrInstituteName.CanShrink = true;
            resources.ApplyResources(this.xrInstituteName, "xrInstituteName");
            this.xrInstituteName.Name = "xrInstituteName";
            this.xrInstituteName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrInstituteName.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrInstituteName.StylePriority.UseFont = false;
            this.xrInstituteName.StylePriority.UseForeColor = false;
            this.xrInstituteName.StylePriority.UsePadding = false;
            this.xrInstituteName.StylePriority.UseTextAlignment = false;
            // 
            // xrlblCostCenter
            // 
            resources.ApplyResources(this.xrlblCostCenter, "xrlblCostCenter");
            this.xrlblCostCenter.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrlblCostCenter.Name = "xrlblCostCenter";
            this.xrlblCostCenter.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrlblCostCenter.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblCostCenter.StylePriority.UseBackColor = false;
            this.xrlblCostCenter.StylePriority.UseBorderColor = false;
            this.xrlblCostCenter.StylePriority.UseBorders = false;
            this.xrlblCostCenter.StylePriority.UseFont = false;
            this.xrlblCostCenter.StylePriority.UseForeColor = false;
            this.xrlblCostCenter.StylePriority.UsePadding = false;
            this.xrlblCostCenter.StylePriority.UseTextAlignment = false;
            // 
            // xrDateRange
            // 
            resources.ApplyResources(this.xrDateRange, "xrDateRange");
            this.xrDateRange.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrDateRange.BorderWidth = 1F;
            this.xrDateRange.Name = "xrDateRange";
            this.xrDateRange.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrDateRange.StylePriority.UseBackColor = false;
            this.xrDateRange.StylePriority.UseBorderColor = false;
            this.xrDateRange.StylePriority.UseBorders = false;
            this.xrDateRange.StylePriority.UseBorderWidth = false;
            this.xrDateRange.StylePriority.UseFont = false;
            this.xrDateRange.StylePriority.UseForeColor = false;
            this.xrDateRange.StylePriority.UsePadding = false;
            this.xrDateRange.StylePriority.UseTextAlignment = false;
            // 
            // xrpicReportLogoLeft
            // 
            this.xrpicReportLogoLeft.Image = ((System.Drawing.Image)(resources.GetObject("xrpicReportLogoLeft.Image")));
            resources.ApplyResources(this.xrpicReportLogoLeft, "xrpicReportLogoLeft");
            this.xrpicReportLogoLeft.Name = "xrpicReportLogoLeft";
            this.xrpicReportLogoLeft.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 1, 0, 100F);
            this.xrpicReportLogoLeft.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrpicReportLogoLeft.StylePriority.UsePadding = false;
            // 
            // xrlblReportSubTitle
            // 
            resources.ApplyResources(this.xrlblReportSubTitle, "xrlblReportSubTitle");
            this.xrlblReportSubTitle.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrlblReportSubTitle.Name = "xrlblReportSubTitle";
            this.xrlblReportSubTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrlblReportSubTitle.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblReportSubTitle.StylePriority.UseBackColor = false;
            this.xrlblReportSubTitle.StylePriority.UseBorderColor = false;
            this.xrlblReportSubTitle.StylePriority.UseBorders = false;
            this.xrlblReportSubTitle.StylePriority.UseFont = false;
            this.xrlblReportSubTitle.StylePriority.UseForeColor = false;
            this.xrlblReportSubTitle.StylePriority.UsePadding = false;
            this.xrlblReportSubTitle.StylePriority.UseTextAlignment = false;
            // 
            // xrlblReportTitle
            // 
            resources.ApplyResources(this.xrlblReportTitle, "xrlblReportTitle");
            this.xrlblReportTitle.Name = "xrlblReportTitle";
            this.xrlblReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrlblReportTitle.StylePriority.UseFont = false;
            this.xrlblReportTitle.StylePriority.UsePadding = false;
            this.xrlblReportTitle.StylePriority.UseTextAlignment = false;
            // 
            // xrlblInstituteProjectName
            // 
            resources.ApplyResources(this.xrlblInstituteProjectName, "xrlblInstituteProjectName");
            this.xrlblInstituteProjectName.Name = "xrlblInstituteProjectName";
            this.xrlblInstituteProjectName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrlblInstituteProjectName.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblInstituteProjectName.StylePriority.UseFont = false;
            this.xrlblInstituteProjectName.StylePriority.UseForeColor = false;
            this.xrlblInstituteProjectName.StylePriority.UsePadding = false;
            this.xrlblInstituteProjectName.StylePriority.UseTextAlignment = false;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblProjectName,
            this.xrlblReportDate,
            this.xrPageInfo,
            this.xrlnFooter});
            resources.ApplyResources(this.PageFooter, "PageFooter");
            this.PageFooter.Name = "PageFooter";
            // 
            // xrlblProjectName
            // 
            resources.ApplyResources(this.xrlblProjectName, "xrlblProjectName");
            this.xrlblProjectName.Name = "xrlblProjectName";
            this.xrlblProjectName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblProjectName.StylePriority.UseFont = false;
            this.xrlblProjectName.StylePriority.UseTextAlignment = false;
            // 
            // xrlblReportDate
            // 
            resources.ApplyResources(this.xrlblReportDate, "xrlblReportDate");
            this.xrlblReportDate.Name = "xrlblReportDate";
            this.xrlblReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblReportDate.StylePriority.UseFont = false;
            this.xrlblReportDate.StylePriority.UseTextAlignment = false;
            // 
            // xrPageInfo
            // 
            resources.ApplyResources(this.xrPageInfo, "xrPageInfo");
            this.xrPageInfo.Name = "xrPageInfo";
            this.xrPageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrPageInfo.StylePriority.UseFont = false;
            this.xrPageInfo.StylePriority.UsePadding = false;
            this.xrPageInfo.StylePriority.UseTextAlignment = false;
            // 
            // xrlnFooter
            // 
            resources.ApplyResources(this.xrlnFooter, "xrlnFooter");
            this.xrlnFooter.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrlnFooter.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrlnFooter.Name = "xrlnFooter";
            this.xrlnFooter.StylePriority.UseBorderColor = false;
            this.xrlnFooter.StylePriority.UseBorderDashStyle = false;
            this.xrlnFooter.StylePriority.UseBorders = false;
            this.xrlnFooter.StylePriority.UseForeColor = false;
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
            // styleReportSubTitle
            // 
            this.styleReportSubTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportSubTitle.Name = "styleReportSubTitle";
            this.styleReportSubTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportSubTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            // styleOddRow
            // 
            this.styleOddRow.BackColor = System.Drawing.Color.White;
            this.styleOddRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleOddRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleOddRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleOddRow.BorderWidth = 1F;
            this.styleOddRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleOddRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.styleOddRow.Name = "styleOddRow";
            this.styleOddRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleEvenRow
            // 
            this.styleEvenRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleEvenRow.BorderColor = System.Drawing.Color.Silver;
            this.styleEvenRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleEvenRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleEvenRow.BorderWidth = 1F;
            this.styleEvenRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleEvenRow.Name = "styleEvenRow";
            this.styleEvenRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleGroupRow
            // 
            this.styleGroupRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleGroupRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleGroupRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleGroupRow.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.styleGroupRow.BorderWidth = 1F;
            this.styleGroupRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRow.Name = "styleGroupRow";
            this.styleGroupRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleGroupRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // styleRow
            // 
            this.styleRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRow.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRow.BorderWidth = 1F;
            this.styleRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRow.Name = "styleRow";
            this.styleRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleColumnHeader
            // 
            this.styleColumnHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeader.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeader.BorderWidth = 1F;
            this.styleColumnHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeader.Name = "styleColumnHeader";
            this.styleColumnHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleColumnHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // styleTotalRow
            // 
            this.styleTotalRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.styleTotalRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleTotalRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleTotalRow.BorderWidth = 1F;
            this.styleTotalRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRow.ForeColor = System.Drawing.Color.Black;
            this.styleTotalRow.Name = "styleTotalRow";
            this.styleTotalRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleTitleRow
            // 
            this.styleTitleRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTitleRow.Name = "styleTitleRow";
            this.styleTitleRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleTitleRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // styleGroupRowBase
            // 
            this.styleGroupRowBase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleGroupRowBase.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRowBase.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleGroupRowBase.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.styleGroupRowBase.BorderWidth = 1F;
            this.styleGroupRowBase.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRowBase.Name = "styleGroupRowBase";
            this.styleGroupRowBase.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleGroupRowBase.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportHeaderBase
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageFooter});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleInstitute,
            this.styleReportTitle,
            this.styleReportSubTitle,
            this.styleDateInfo,
            this.stylePageInfo,
            this.styleOddRow,
            this.styleEvenRow,
            this.styleGroupRow,
            this.styleRow,
            this.styleColumnHeader,
            this.styleTotalRow,
            this.styleTitleRow,
            this.styleGroupRowBase});
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo;
        private DevExpress.XtraReports.UI.XRLine xrlnFooter;
        private DevExpress.XtraReports.UI.XRLabel xrlblInstituteProjectName;
        private DevExpress.XtraReports.UI.XRLabel xrlblReportTitle;
        private DevExpress.XtraReports.UI.XRLabel xrlblReportSubTitle;
        public DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPictureBox xrpicReportLogoLeft;
        private DevExpress.XtraReports.UI.XRLabel xrDateRange;
        private DevExpress.XtraReports.UI.XRControlStyle styleInstitute;
        private DevExpress.XtraReports.UI.XRControlStyle styleReportTitle;
        private DevExpress.XtraReports.UI.XRControlStyle styleReportSubTitle;
        private DevExpress.XtraReports.UI.XRControlStyle styleDateInfo;
        private DevExpress.XtraReports.UI.XRControlStyle stylePageInfo;
        private DevExpress.XtraReports.UI.XRControlStyle styleOddRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleEvenRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleColumnHeader;
        private DevExpress.XtraReports.UI.XRControlStyle styleTotalRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleTitleRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRowBase;
        private DevExpress.XtraReports.UI.XRLabel xrlblReportDate;
        private DevExpress.XtraReports.UI.XRLabel xrlblCostCenter;
        private DevExpress.XtraReports.UI.XRLabel xrInstituteAddress;
        private DevExpress.XtraReports.UI.XRLabel xrlblProjectName;
        private DevExpress.XtraReports.UI.XRLabel xrInstituteName;
        private DevExpress.XtraReports.UI.XRLabel xrlblBudgetname;
        private ReportSetting reportSetting1;
    }
}
