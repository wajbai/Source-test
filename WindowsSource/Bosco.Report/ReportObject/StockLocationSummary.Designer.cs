namespace Bosco.Report.ReportObject
{
    partial class StockLocationSummary
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
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            this.xrtblStockItems = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellParticularsDetails = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellQuantityDetails = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellRateDetails = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellValueDetails = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrtblHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellParticulars = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellQuantity = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellRate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellValue = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpGroup = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrtblGroup = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xtcellGroupName = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellTotalQuantity = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellTotalValue = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblStockItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblStockItems});
            this.Detail.HeightF = 25F;
            this.Detail.Visible = true;
            // 
            // xrtblStockItems
            // 
            this.xrtblStockItems.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrtblStockItems.LocationFloat = new DevExpress.Utils.PointFloat(20.83334F, 0F);
            this.xrtblStockItems.Name = "xrtblStockItems";
            this.xrtblStockItems.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblStockItems.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrtblStockItems.SizeF = new System.Drawing.SizeF(704.1665F, 25F);
            this.xrtblStockItems.StylePriority.UseFont = false;
            this.xrtblStockItems.StylePriority.UsePadding = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellParticularsDetails,
            this.xrcellQuantityDetails,
            this.xrcellRateDetails,
            this.xrcellValueDetails});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrcellParticularsDetails
            // 
            this.xrcellParticularsDetails.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockLocationSummary.NAME")});
            this.xrcellParticularsDetails.Name = "xrcellParticularsDetails";
            this.xrcellParticularsDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellParticularsDetails.StylePriority.UsePadding = false;
            this.xrcellParticularsDetails.StylePriority.UseTextAlignment = false;
            this.xrcellParticularsDetails.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrcellParticularsDetails.Weight = 2.2149995422363284D;
            // 
            // xrcellQuantityDetails
            // 
            this.xrcellQuantityDetails.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockLocationSummary.QUANTITY")});
            this.xrcellQuantityDetails.Name = "xrcellQuantityDetails";
            this.xrcellQuantityDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellQuantityDetails.StylePriority.UsePadding = false;
            this.xrcellQuantityDetails.StylePriority.UseTextAlignment = false;
            this.xrcellQuantityDetails.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellQuantityDetails.Weight = 1.2537497611864032D;
            // 
            // xrcellRateDetails
            // 
            this.xrcellRateDetails.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockLocationSummary.RATE", "{0:n}")});
            this.xrcellRateDetails.Name = "xrcellRateDetails";
            this.xrcellRateDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellRateDetails.StylePriority.UsePadding = false;
            this.xrcellRateDetails.StylePriority.UseTextAlignment = false;
            this.xrcellRateDetails.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellRateDetails.Weight = 1.8041645594038505D;
            // 
            // xrcellValueDetails
            // 
            this.xrcellValueDetails.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockLocationSummary.VALUE_AMOUNT", "{0:n}")});
            this.xrcellValueDetails.Name = "xrcellValueDetails";
            this.xrcellValueDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellValueDetails.StylePriority.UsePadding = false;
            this.xrcellValueDetails.StylePriority.UseTextAlignment = false;
            this.xrcellValueDetails.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellValueDetails.Weight = 1.7687508710601372D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblHeader});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrtblHeader
            // 
            this.xrtblHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.xrtblHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrtblHeader.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            this.xrtblHeader.Name = "xrtblHeader";
            this.xrtblHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrtblHeader.SizeF = new System.Drawing.SizeF(723.0007F, 25F);
            this.xrtblHeader.StylePriority.UseBackColor = false;
            this.xrtblHeader.StylePriority.UseFont = false;
            this.xrtblHeader.StylePriority.UsePadding = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellParticulars,
            this.xrcellQuantity,
            this.xrcellRate,
            this.xrcellValue});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrcellParticulars
            // 
            this.xrcellParticulars.Multiline = true;
            this.xrcellParticulars.Name = "xrcellParticulars";
            this.xrcellParticulars.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellParticulars.StylePriority.UseFont = false;
            this.xrcellParticulars.StylePriority.UsePadding = false;
            this.xrcellParticulars.StylePriority.UseTextAlignment = false;
            this.xrcellParticulars.Text = "Item Name";
            this.xrcellParticulars.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrcellParticulars.Weight = 1D;
            // 
            // xrcellQuantity
            // 
            this.xrcellQuantity.Name = "xrcellQuantity";
            this.xrcellQuantity.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellQuantity.StylePriority.UseFont = false;
            this.xrcellQuantity.StylePriority.UsePadding = false;
            this.xrcellQuantity.StylePriority.UseTextAlignment = false;
            this.xrcellQuantity.Text = "Quantity";
            this.xrcellQuantity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellQuantity.Weight = 0.52167130045685783D;
            // 
            // xrcellRate
            // 
            this.xrcellRate.Name = "xrcellRate";
            this.xrcellRate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellRate.StylePriority.UsePadding = false;
            this.xrcellRate.StylePriority.UseTextAlignment = false;
            this.xrcellRate.Text = "Rate Per";
            this.xrcellRate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellRate.Weight = 0.75069267706798282D;
            // 
            // xrcellValue
            // 
            this.xrcellValue.Multiline = true;
            this.xrcellValue.Name = "xrcellValue";
            this.xrcellValue.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellValue.StylePriority.UsePadding = false;
            this.xrcellValue.StylePriority.UseTextAlignment = false;
            this.xrcellValue.Text = "Amount";
            this.xrcellValue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellValue.Weight = 0.73596059135193626D;
            // 
            // grpGroup
            // 
            this.grpGroup.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblGroup});
            this.grpGroup.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LOCATION_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpGroup.HeightF = 25F;
            this.grpGroup.Name = "grpGroup";
            // 
            // xrtblGroup
            // 
            this.xrtblGroup.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrtblGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrtblGroup.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrtblGroup.Name = "xrtblGroup";
            this.xrtblGroup.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblGroup.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblGroup.SizeF = new System.Drawing.SizeF(725.0006F, 25F);
            this.xrtblGroup.StylePriority.UseBackColor = false;
            this.xrtblGroup.StylePriority.UseFont = false;
            this.xrtblGroup.StylePriority.UsePadding = false;
            this.xrtblGroup.StylePriority.UseTextAlignment = false;
            this.xrtblGroup.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xtcellGroupName});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xtcellGroupName
            // 
            this.xtcellGroupName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockLocationSummary.LOCATION_NAME")});
            this.xtcellGroupName.Name = "xtcellGroupName";
            this.xtcellGroupName.Weight = 1D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblGrandTotal});
            this.ReportFooter.HeightF = 25F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrtblGrandTotal
            // 
            this.xrtblGrandTotal.BackColor = System.Drawing.Color.Gainsboro;
            this.xrtblGrandTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrtblGrandTotal.LocationFloat = new DevExpress.Utils.PointFloat(1.251698E-05F, 0F);
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrtblGrandTotal.SizeF = new System.Drawing.SizeF(724.9999F, 25F);
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UsePadding = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellGrandTotal,
            this.xrcellTotalQuantity,
            this.xrcellAmount,
            this.xrcellTotalValue});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrcellGrandTotal
            // 
            this.xrcellGrandTotal.Name = "xrcellGrandTotal";
            this.xrcellGrandTotal.StylePriority.UseTextAlignment = false;
            this.xrcellGrandTotal.Text = "Grand Total";
            this.xrcellGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrcellGrandTotal.Weight = 2.4233328247070318D;
            // 
            // xrcellTotalQuantity
            // 
            this.xrcellTotalQuantity.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockLocationSummary.QUANTITY")});
            this.xrcellTotalQuantity.Name = "xrcellTotalQuantity";
            this.xrcellTotalQuantity.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:#}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrcellTotalQuantity.Summary = xrSummary1;
            this.xrcellTotalQuantity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellTotalQuantity.Weight = 1.2537500049810095D;
            // 
            // xrcellAmount
            // 
            this.xrcellAmount.Name = "xrcellAmount";
            this.xrcellAmount.StylePriority.UseTextAlignment = false;
            this.xrcellAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellAmount.Weight = 1.8041650307844028D;
            // 
            // xrcellTotalValue
            // 
            this.xrcellTotalValue.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockLocationSummary.VALUE_AMOUNT")});
            this.xrcellTotalValue.Name = "xrcellTotalValue";
            this.xrcellTotalValue.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrcellTotalValue.Summary = xrSummary2;
            this.xrcellTotalValue.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellTotalValue.Weight = 1.7687506136486506D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak1,
            this.xrTable1});
            this.GroupFooter1.HeightF = 37.5F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrPageBreak1
            // 
            this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 35.49998F);
            this.xrPageBreak1.Name = "xrPageBreak1";
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTable1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(20.83336F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable1.SizeF = new System.Drawing.SizeF(704.1665F, 25F);
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UsePadding = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Total";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell3.Weight = 2.2149992458299788D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockLocationSummary.QUANTITY")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell4.Summary = xrSummary3;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell4.Weight = 1.2537500049810095D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell5.Weight = 1.804164420432866D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "StockLocationSummary.VALUE_AMOUNT")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:n}";
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell6.Summary = xrSummary4;
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell6.Weight = 1.7687512240001875D;
            // 
            // StockLocationSummary
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.grpGroup,
            this.ReportFooter,
            this.GroupFooter1});
            this.DataMember = "StockLocationSummary";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.grpGroup, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblStockItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrtblStockItems;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrcellParticularsDetails;
        private DevExpress.XtraReports.UI.XRTableCell xrcellQuantityDetails;
        private DevExpress.XtraReports.UI.XRTableCell xrcellRateDetails;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpGroup;
        private DevExpress.XtraReports.UI.XRTable xrtblHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrcellParticulars;
        private DevExpress.XtraReports.UI.XRTableCell xrcellQuantity;
        private DevExpress.XtraReports.UI.XRTableCell xrcellRate;
        private DevExpress.XtraReports.UI.XRTable xrtblGroup;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xtcellGroupName;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrcellGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrcellAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrcellTotalValue;
        private DevExpress.XtraReports.UI.XRTableCell xrcellValue;
        private DevExpress.XtraReports.UI.XRTableCell xrcellValueDetails;
        private DevExpress.XtraReports.UI.XRTableCell xrcellTotalQuantity;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRPageBreak xrPageBreak1;
    }
}
