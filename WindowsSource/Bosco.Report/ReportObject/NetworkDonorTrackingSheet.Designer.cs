namespace Bosco.Report.ReportObject
{
    partial class NetworkDonorTrackingSheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkDonorTrackingSheet));
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblDonorTrackingSheet = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDonorName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDateofDonation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAddress = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtTrackingSheet = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrdAssetClass = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrdPreAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrdValidFrom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrdValidTo = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDonorTrackingSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtTrackingSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtTrackingSheet});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblDonorTrackingSheet});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // xrtblDonorTrackingSheet
            // 
            resources.ApplyResources(this.xrtblDonorTrackingSheet, "xrtblDonorTrackingSheet");
            this.xrtblDonorTrackingSheet.Name = "xrtblDonorTrackingSheet";
            this.xrtblDonorTrackingSheet.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblDonorTrackingSheet.StyleName = "styleColumnHeader";
            this.xrtblDonorTrackingSheet.StylePriority.UseBackColor = false;
            this.xrtblDonorTrackingSheet.StylePriority.UseBorderColor = false;
            this.xrtblDonorTrackingSheet.StylePriority.UseBorders = false;
            this.xrtblDonorTrackingSheet.StylePriority.UseFont = false;
            this.xrtblDonorTrackingSheet.StylePriority.UsePadding = false;
            this.xrtblDonorTrackingSheet.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrDonorName,
            this.xrDateofDonation,
            this.xrAddress,
            this.xrAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrDonorName
            // 
            this.xrDonorName.Name = "xrDonorName";
            this.xrDonorName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrDonorName.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrDonorName, "xrDonorName");
            // 
            // xrDateofDonation
            // 
            this.xrDateofDonation.Name = "xrDateofDonation";
            this.xrDateofDonation.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrDateofDonation.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrDateofDonation, "xrDateofDonation");
            // 
            // xrAddress
            // 
            this.xrAddress.Name = "xrAddress";
            this.xrAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrAddress.StylePriority.UsePadding = false;
            resources.ApplyResources(this.xrAddress, "xrAddress");
            // 
            // xrAmount
            // 
            this.xrAmount.Name = "xrAmount";
            this.xrAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrAmount.StylePriority.UsePadding = false;
            this.xrAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrAmount, "xrAmount");
            // 
            // xrtTrackingSheet
            // 
            resources.ApplyResources(this.xrtTrackingSheet, "xrtTrackingSheet");
            this.xrtTrackingSheet.Name = "xrtTrackingSheet";
            this.xrtTrackingSheet.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtTrackingSheet.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtTrackingSheet.StyleName = "styleRow";
            this.xrtTrackingSheet.StylePriority.UseBorderColor = false;
            this.xrtTrackingSheet.StylePriority.UseBorders = false;
            this.xrtTrackingSheet.StylePriority.UseFont = false;
            this.xrtTrackingSheet.StylePriority.UsePadding = false;
            this.xrtTrackingSheet.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrdAssetClass,
            this.xrdPreAmt,
            this.xrdValidFrom,
            this.xrdValidTo});
            this.xrTableRow2.Name = "xrTableRow2";
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // xrdAssetClass
            // 
            this.xrdAssetClass.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NetworkDonorTrackingSheet.NAME")});
            this.xrdAssetClass.Name = "xrdAssetClass";
            this.xrdAssetClass.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrdAssetClass, "xrdAssetClass");
            // 
            // xrdPreAmt
            // 
            this.xrdPreAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NetworkDonorTrackingSheet.DATE_DONATION", "{0:dd/MM/yyyy}")});
            this.xrdPreAmt.Name = "xrdPreAmt";
            this.xrdPreAmt.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrdPreAmt, "xrdPreAmt");
            // 
            // xrdValidFrom
            // 
            this.xrdValidFrom.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NetworkDonorTrackingSheet.ADDRESS")});
            this.xrdValidFrom.Name = "xrdValidFrom";
            this.xrdValidFrom.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrdValidFrom, "xrdValidFrom");
            // 
            // xrdValidTo
            // 
            this.xrdValidTo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "NetworkDonorTrackingSheet.AMOUNT")});
            this.xrdValidTo.Name = "xrdValidTo";
            this.xrdValidTo.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(this.xrdValidTo, "xrdValidTo");
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // NetworkDonorTrackingSheet
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblDonorTrackingSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtTrackingSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblDonorTrackingSheet;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrDonorName;
        private DevExpress.XtraReports.UI.XRTableCell xrDateofDonation;
        private DevExpress.XtraReports.UI.XRTableCell xrAddress;
        private DevExpress.XtraReports.UI.XRTable xrtTrackingSheet;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrdAssetClass;
        private DevExpress.XtraReports.UI.XRTableCell xrdPreAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrdValidFrom;
        private DevExpress.XtraReports.UI.XRTableCell xrdValidTo;
        private DevExpress.XtraReports.UI.XRTableCell xrAmount;
        private ReportSetting reportSetting1;
    }
}
