namespace Bosco.Report.ReportObject
{
    partial class MultiAbstractReceiptPayment
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
            DevExpress.XtraPivotGrid.PivotGridGroup pivotGridGroup1 = new DevExpress.XtraPivotGrid.PivotGridGroup();
            this.VoucherDate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.winControlContainer1 = new DevExpress.XtraReports.UI.WinControlContainer();
            this.pvtMultiReceiptPayment = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.ledgerCode = new DevExpress.XtraPivotGrid.PivotGridField();
            this.LedgerName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Total = new DevExpress.XtraPivotGrid.PivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.pvtMultiReceiptPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // VoucherDate
            // 
            this.VoucherDate.Appearance.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.VoucherDate.Appearance.Header.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.VoucherDate.Appearance.Header.Options.UseBackColor = true;
            this.VoucherDate.Appearance.Header.Options.UseFont = true;
            this.VoucherDate.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.VoucherDate.AreaIndex = 0;
            this.VoucherDate.Caption = "VoucherDate";
            this.VoucherDate.FieldName = "VOUCHER_DATE";
            this.VoucherDate.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
            this.VoucherDate.Name = "VoucherDate";
            this.VoucherDate.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending;
            this.VoucherDate.UnboundFieldName = "VoucherDate";
            this.VoucherDate.Width = 239;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.winControlContainer1});
            this.Detail.HeightF = 150.4167F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // winControlContainer1
            // 
            this.winControlContainer1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
            this.winControlContainer1.Name = "winControlContainer1";
            this.winControlContainer1.SizeF = new System.Drawing.SizeF(690F, 142.9167F);
            this.winControlContainer1.WinControl = this.pvtMultiReceiptPayment;
            // 
            // pvtMultiReceiptPayment
            // 
            this.pvtMultiReceiptPayment.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pvtMultiReceiptPayment.Appearance.FieldValue.Options.UseFont = true;
            this.pvtMultiReceiptPayment.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.pvtMultiReceiptPayment.Appearance.GrandTotalCell.Options.UseFont = true;
            this.pvtMultiReceiptPayment.Appearance.HeaderGroupLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pvtMultiReceiptPayment.Appearance.HeaderGroupLine.Options.UseFont = true;
            this.pvtMultiReceiptPayment.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.ledgerCode,
            this.LedgerName,
            this.Total,
            this.VoucherDate});
            pivotGridGroup1.Fields.Add(this.VoucherDate);
            pivotGridGroup1.Hierarchy = null;
            pivotGridGroup1.ShowNewValues = true;
            this.pvtMultiReceiptPayment.Groups.AddRange(new DevExpress.XtraPivotGrid.PivotGridGroup[] {
            pivotGridGroup1});
            this.pvtMultiReceiptPayment.Location = new System.Drawing.Point(0, 0);
            this.pvtMultiReceiptPayment.Name = "pvtMultiReceiptPayment";
            this.pvtMultiReceiptPayment.OptionsCustomization.AllowEdit = false;
            this.pvtMultiReceiptPayment.OptionsCustomization.AllowResizing = false;
            this.pvtMultiReceiptPayment.OptionsCustomization.AllowSort = false;
            this.pvtMultiReceiptPayment.OptionsSelection.CellSelection = false;
            this.pvtMultiReceiptPayment.OptionsView.ShowColumnHeaders = false;
            this.pvtMultiReceiptPayment.OptionsView.ShowCustomTotalsForSingleValues = true;
            this.pvtMultiReceiptPayment.OptionsView.ShowDataHeaders = false;
            this.pvtMultiReceiptPayment.OptionsView.ShowFilterHeaders = false;
            this.pvtMultiReceiptPayment.Size = new System.Drawing.Size(662, 137);
            this.pvtMultiReceiptPayment.TabIndex = 0;
            // 
            // ledgerCode
            // 
            this.ledgerCode.Appearance.Header.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.ledgerCode.Appearance.Header.Options.UseFont = true;
            this.ledgerCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.ledgerCode.AreaIndex = 0;
            this.ledgerCode.Caption = "LedgerCode";
            this.ledgerCode.FieldName = "LEDGER_CODE";
            this.ledgerCode.Name = "ledgerCode";
            this.ledgerCode.Width = 123;
            // 
            // LedgerName
            // 
            this.LedgerName.Appearance.Header.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.LedgerName.Appearance.Header.Options.UseFont = true;
            this.LedgerName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.LedgerName.AreaIndex = 1;
            this.LedgerName.Caption = "LedgerName";
            this.LedgerName.FieldName = "LEDGER_NAME";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending;
            this.LedgerName.Width = 150;
            // 
            // Total
            // 
            this.Total.Appearance.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.Total.Appearance.Header.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.Total.Appearance.Header.Options.UseBackColor = true;
            this.Total.Appearance.Header.Options.UseFont = true;
            this.Total.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.Total.AreaIndex = 0;
            this.Total.Caption = "TOTAL";
            this.Total.FieldName = "TOTAL";
            this.Total.Name = "Total";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 64F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 101F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.HeightF = 118.5417F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Name = "ReportFooter";
            // 
            // MultiAbstractReceiptPayment
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Margins = new System.Drawing.Printing.Margins(85, 55, 64, 101);
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this.pvtMultiReceiptPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.WinControlContainer winControlContainer1;
        private DevExpress.XtraPivotGrid.PivotGridControl pvtMultiReceiptPayment;
        private DevExpress.XtraPivotGrid.PivotGridField ledgerCode;
        private DevExpress.XtraPivotGrid.PivotGridField LedgerName;
        private DevExpress.XtraPivotGrid.PivotGridField Total;
        private DevExpress.XtraPivotGrid.PivotGridField VoucherDate;
    }
}
