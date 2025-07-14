namespace Bosco.Report.ReportObject
{
    partial class CCEnabledLedgers
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
            this.xrtblCCDetails = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCCName = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpHeaderLedgerName = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblGrpLedgerName = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrpLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpFooterLedgerName = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.grpHeaderCCName = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.grpFooterCCName = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.grpHeaderVoucherMade = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblCCVoucherMade = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCCVooucherMode = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpFooterVoucherMade = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCCDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblGrpLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblCCVoucherMade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 0F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrtblCCDetails
            // 
            this.xrtblCCDetails.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblCCDetails.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblCCDetails.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrtblCCDetails.LocationFloat = new DevExpress.Utils.PointFloat(22F, 0F);
            this.xrtblCCDetails.Name = "xrtblCCDetails";
            this.xrtblCCDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtblCCDetails.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrtblCCDetails.SizeF = new System.Drawing.SizeF(701.125F, 25F);
            this.xrtblCCDetails.StyleName = "styleRow";
            this.xrtblCCDetails.StylePriority.UseBorderColor = false;
            this.xrtblCCDetails.StylePriority.UseBorders = false;
            this.xrtblCCDetails.StylePriority.UseFont = false;
            this.xrtblCCDetails.StylePriority.UsePadding = false;
            this.xrtblCCDetails.StylePriority.UseTextAlignment = false;
            this.xrtblCCDetails.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCCName});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrCCName
            // 
            this.xrCCName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCCName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Ledger.COST_CENTRE_NAME")});
            this.xrCCName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrCCName.Name = "xrCCName";
            this.xrCCName.StyleName = "styleDateInfo";
            this.xrCCName.StylePriority.UseBorders = false;
            this.xrCCName.StylePriority.UseFont = false;
            this.xrCCName.StylePriority.UseTextAlignment = false;
            this.xrCCName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCCName.Weight = 3.6594746310771358D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpHeaderLedgerName
            // 
            this.grpHeaderLedgerName.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblGrpLedgerName});
            this.grpHeaderLedgerName.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("LEDGER_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpHeaderLedgerName.HeightF = 25F;
            this.grpHeaderLedgerName.Level = 1;
            this.grpHeaderLedgerName.Name = "grpHeaderLedgerName";
            // 
            // xrTblGrpLedgerName
            // 
            this.xrTblGrpLedgerName.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTblGrpLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblGrpLedgerName.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrTblGrpLedgerName.LocationFloat = new DevExpress.Utils.PointFloat(2F, 0F);
            this.xrTblGrpLedgerName.Name = "xrTblGrpLedgerName";
            this.xrTblGrpLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblGrpLedgerName.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTblGrpLedgerName.SizeF = new System.Drawing.SizeF(723F, 25F);
            this.xrTblGrpLedgerName.StyleName = "styleRow";
            this.xrTblGrpLedgerName.StylePriority.UseBorderColor = false;
            this.xrTblGrpLedgerName.StylePriority.UseBorders = false;
            this.xrTblGrpLedgerName.StylePriority.UseFont = false;
            this.xrTblGrpLedgerName.StylePriority.UsePadding = false;
            this.xrTblGrpLedgerName.StylePriority.UseTextAlignment = false;
            this.xrTblGrpLedgerName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrpLedgerName});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrGrpLedgerName
            // 
            this.xrGrpLedgerName.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrGrpLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Ledger.LEDGER_NAME", "{0:d}")});
            this.xrGrpLedgerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrGrpLedgerName.Name = "xrGrpLedgerName";
            this.xrGrpLedgerName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrGrpLedgerName.StyleName = "styleDateInfo";
            this.xrGrpLedgerName.StylePriority.UseBorders = false;
            this.xrGrpLedgerName.StylePriority.UseFont = false;
            this.xrGrpLedgerName.StylePriority.UsePadding = false;
            this.xrGrpLedgerName.StylePriority.UseTextAlignment = false;
            this.xrGrpLedgerName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrGrpLedgerName.Weight = 3.6594746310771358D;
            // 
            // grpFooterLedgerName
            // 
            this.grpFooterLedgerName.HeightF = 0F;
            this.grpFooterLedgerName.Level = 1;
            this.grpFooterLedgerName.Name = "grpFooterLedgerName";
            this.grpFooterLedgerName.Visible = false;
            // 
            // grpHeaderCCName
            // 
            this.grpHeaderCCName.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblCCDetails});
            this.grpHeaderCCName.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("COST_CENTRE_NAME", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpHeaderCCName.HeightF = 25F;
            this.grpHeaderCCName.Name = "grpHeaderCCName";
            // 
            // grpFooterCCName
            // 
            this.grpFooterCCName.HeightF = 0F;
            this.grpFooterCCName.Name = "grpFooterCCName";
            this.grpFooterCCName.Visible = false;
            // 
            // grpHeaderVoucherMade
            // 
            this.grpHeaderVoucherMade.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblCCVoucherMade});
            this.grpHeaderVoucherMade.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("RECORD_EXISTS", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpHeaderVoucherMade.HeightF = 25F;
            this.grpHeaderVoucherMade.Level = 2;
            this.grpHeaderVoucherMade.Name = "grpHeaderVoucherMade";
            this.grpHeaderVoucherMade.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpHeaderVoucherMade_BeforePrint);
            // 
            // xrTblCCVoucherMade
            // 
            this.xrTblCCVoucherMade.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTblCCVoucherMade.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblCCVoucherMade.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.xrTblCCVoucherMade.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            this.xrTblCCVoucherMade.Name = "xrTblCCVoucherMade";
            this.xrTblCCVoucherMade.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblCCVoucherMade.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTblCCVoucherMade.SizeF = new System.Drawing.SizeF(721F, 25F);
            this.xrTblCCVoucherMade.StyleName = "styleRow";
            this.xrTblCCVoucherMade.StylePriority.UseBorderColor = false;
            this.xrTblCCVoucherMade.StylePriority.UseBorders = false;
            this.xrTblCCVoucherMade.StylePriority.UseFont = false;
            this.xrTblCCVoucherMade.StylePriority.UsePadding = false;
            this.xrTblCCVoucherMade.StylePriority.UseTextAlignment = false;
            this.xrTblCCVoucherMade.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCCVooucherMode});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrCCVooucherMode
            // 
            this.xrCCVooucherMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.xrCCVooucherMode.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrCCVooucherMode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Ledger.RECORD_EXISTS")});
            this.xrCCVooucherMode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrCCVooucherMode.Name = "xrCCVooucherMode";
            this.xrCCVooucherMode.StyleName = "styleDateInfo";
            this.xrCCVooucherMode.StylePriority.UseBackColor = false;
            this.xrCCVooucherMode.StylePriority.UseBorders = false;
            this.xrCCVooucherMode.StylePriority.UseFont = false;
            this.xrCCVooucherMode.StylePriority.UseTextAlignment = false;
            this.xrCCVooucherMode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCCVooucherMode.Weight = 3.6594746310771358D;
            this.xrCCVooucherMode.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCCVooucherMode_BeforePrint);
            // 
            // grpFooterVoucherMade
            // 
            this.grpFooterVoucherMade.HeightF = 0F;
            this.grpFooterVoucherMade.Level = 2;
            this.grpFooterVoucherMade.Name = "grpFooterVoucherMade";
            this.grpFooterVoucherMade.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            // 
            // CCEnabledLedgers
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.grpHeaderLedgerName,
            this.grpFooterLedgerName,
            this.grpHeaderCCName,
            this.grpFooterCCName,
            this.grpHeaderVoucherMade,
            this.grpFooterVoucherMade});
            this.DataMember = "Ledger";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpFooterVoucherMade, 0);
            this.Controls.SetChildIndex(this.grpHeaderVoucherMade, 0);
            this.Controls.SetChildIndex(this.grpFooterCCName, 0);
            this.Controls.SetChildIndex(this.grpHeaderCCName, 0);
            this.Controls.SetChildIndex(this.grpFooterLedgerName, 0);
            this.Controls.SetChildIndex(this.grpHeaderLedgerName, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrtblCCDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblGrpLedgerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblCCVoucherMade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrtblCCDetails;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrCCName;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderLedgerName;
        private DevExpress.XtraReports.UI.XRTable xrTblGrpLedgerName;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrGrpLedgerName;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooterLedgerName;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderCCName;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooterCCName;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderVoucherMade;
        private DevExpress.XtraReports.UI.XRTable xrTblCCVoucherMade;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrCCVooucherMode;
        private DevExpress.XtraReports.UI.GroupFooterBand grpFooterVoucherMade;
    }
}
