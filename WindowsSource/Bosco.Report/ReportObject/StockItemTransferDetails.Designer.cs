namespace Bosco.Report.ReportObject
{
    partial class StockItemTransferDetails
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
            this.FCPurposeHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapItemName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapFromLocation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapToLocation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapTransferredQty = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrFCPurposeDetails = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrItemName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrFromLocation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrToLocation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTransferredQuantity = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.FCPurposeHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrFCPurposeDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrFCPurposeDetails});
            this.Detail.HeightF = 21.875F;
            this.Detail.Visible = true;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.FCPurposeHeader});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // FCPurposeHeader
            // 
            this.FCPurposeHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.FCPurposeHeader.Name = "FCPurposeHeader";
            this.FCPurposeHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.FCPurposeHeader.SizeF = new System.Drawing.SizeF(738.0001F, 25F);
            this.FCPurposeHeader.StyleName = "styleColumnHeader";
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapDate,
            this.xrCapItemName,
            this.xrCapFromLocation,
            this.xrCapToLocation,
            this.xrCapTransferredQty});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrCapDate
            // 
            this.xrCapDate.Name = "xrCapDate";
            this.xrCapDate.Text = "Date";
            this.xrCapDate.Weight = 0.35062264155056722D;
            // 
            // xrCapItemName
            // 
            this.xrCapItemName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapItemName.Name = "xrCapItemName";
            this.xrCapItemName.StylePriority.UseBorders = false;
            this.xrCapItemName.Text = "Item Name";
            this.xrCapItemName.Weight = 0.76739199996990992D;
            // 
            // xrCapFromLocation
            // 
            this.xrCapFromLocation.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapFromLocation.Name = "xrCapFromLocation";
            this.xrCapFromLocation.StylePriority.UseBorders = false;
            this.xrCapFromLocation.Text = "Location From";
            this.xrCapFromLocation.Weight = 0.65379422266678766D;
            // 
            // xrCapToLocation
            // 
            this.xrCapToLocation.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapToLocation.Name = "xrCapToLocation";
            this.xrCapToLocation.StylePriority.UseBorders = false;
            this.xrCapToLocation.Text = "Location To";
            this.xrCapToLocation.Weight = 0.65759578089020487D;
            // 
            // xrCapTransferredQty
            // 
            this.xrCapTransferredQty.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapTransferredQty.Name = "xrCapTransferredQty";
            this.xrCapTransferredQty.StylePriority.UseBorders = false;
            this.xrCapTransferredQty.StylePriority.UseTextAlignment = false;
            this.xrCapTransferredQty.Text = "Total Qty";
            this.xrCapTransferredQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCapTransferredQty.Weight = 0.57059434189089153D;
            // 
            // xrFCPurposeDetails
            // 
            this.xrFCPurposeDetails.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrFCPurposeDetails.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrFCPurposeDetails.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrFCPurposeDetails.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrFCPurposeDetails.Name = "xrFCPurposeDetails";
            this.xrFCPurposeDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrFCPurposeDetails.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrFCPurposeDetails.SizeF = new System.Drawing.SizeF(738.0001F, 21.25F);
            this.xrFCPurposeDetails.StyleName = "styleRow";
            this.xrFCPurposeDetails.StylePriority.UseBorderColor = false;
            this.xrFCPurposeDetails.StylePriority.UseBorders = false;
            this.xrFCPurposeDetails.StylePriority.UseFont = false;
            this.xrFCPurposeDetails.StylePriority.UsePadding = false;
            this.xrFCPurposeDetails.StylePriority.UseTextAlignment = false;
            this.xrFCPurposeDetails.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrDate,
            this.xrItemName,
            this.xrFromLocation,
            this.xrToLocation,
            this.xrTransferredQuantity});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 11.5D;
            // 
            // xrDate
            // 
            this.xrDate.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockTransferDetails.DATE", "{0:d}")});
            this.xrDate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrDate.Name = "xrDate";
            this.xrDate.StylePriority.UseBorders = false;
            this.xrDate.StylePriority.UseFont = false;
            this.xrDate.StylePriority.UseTextAlignment = false;
            this.xrDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrDate.Weight = 0.55944886516702563D;
            // 
            // xrItemName
            // 
            this.xrItemName.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrItemName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockTransferDetails.ITEM_NAME")});
            this.xrItemName.Name = "xrItemName";
            this.xrItemName.StylePriority.UseBorders = false;
            this.xrItemName.Weight = 1.2244400841538377D;
            // 
            // xrFromLocation
            // 
            this.xrFromLocation.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrFromLocation.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockTransferDetails.FROM_LOCATION")});
            this.xrFromLocation.Name = "xrFromLocation";
            this.xrFromLocation.StylePriority.UseBorders = false;
            this.xrFromLocation.Weight = 1.0431852452163233D;
            // 
            // xrToLocation
            // 
            this.xrToLocation.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrToLocation.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockTransferDetails.TO_LOCATION")});
            this.xrToLocation.Name = "xrToLocation";
            this.xrToLocation.StylePriority.UseBorders = false;
            this.xrToLocation.Weight = 1.0492507560453845D;
            // 
            // xrTransferredQuantity
            // 
            this.xrTransferredQuantity.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTransferredQuantity.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockTransferDetails.TRANSFERED_QTY", "{0:n}")});
            this.xrTransferredQuantity.Name = "xrTransferredQuantity";
            this.xrTransferredQuantity.StylePriority.UseBorders = false;
            this.xrTransferredQuantity.StylePriority.UseTextAlignment = false;
            xrSummary1.IgnoreNullValues = true;
            this.xrTransferredQuantity.Summary = xrSummary1;
            this.xrTransferredQuantity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTransferredQuantity.Weight = 0.910432861917429D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // StockItemTransferDetails
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader});
            this.DataSource = this.reportSetting1;
            this.Margins = new System.Drawing.Printing.Margins(49, 40, 20, 20);
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.FCPurposeHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrFCPurposeDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable FCPurposeHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapDate;
        private DevExpress.XtraReports.UI.XRTableCell xrCapItemName;
        private DevExpress.XtraReports.UI.XRTableCell xrCapFromLocation;
        private DevExpress.XtraReports.UI.XRTableCell xrCapToLocation;
        private DevExpress.XtraReports.UI.XRTableCell xrCapTransferredQty;
        private DevExpress.XtraReports.UI.XRTable xrFCPurposeDetails;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrDate;
        private DevExpress.XtraReports.UI.XRTableCell xrItemName;
        private DevExpress.XtraReports.UI.XRTableCell xrFromLocation;
        private DevExpress.XtraReports.UI.XRTableCell xrToLocation;
        private DevExpress.XtraReports.UI.XRTableCell xrTransferredQuantity;
        private ReportSetting reportSetting1;
    }
}
