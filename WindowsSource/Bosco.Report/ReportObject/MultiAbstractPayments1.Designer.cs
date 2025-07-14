namespace Bosco.Report.ReportObject
{
    partial class MultiAbstractPayments
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
            this.colAccountDate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.winControlContainer1 = new DevExpress.XtraReports.UI.WinControlContainer();
            this.pgcMultiAbstractPayment = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.colLedgercode = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colLedgerName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colTotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.pgcMultiAbstractPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // colAccountDate
            // 
            this.colAccountDate.Appearance.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.colAccountDate.Appearance.Header.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAccountDate.Appearance.Header.Options.UseBackColor = true;
            this.colAccountDate.Appearance.Header.Options.UseFont = true;
            this.colAccountDate.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.colAccountDate.AreaIndex = 0;
            this.colAccountDate.FieldName = "VOUCHER_DATE";
            this.colAccountDate.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
            this.colAccountDate.Name = "colAccountDate";
            this.colAccountDate.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colAccountDate.Options.AllowEdit = false;
            this.colAccountDate.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAccountDate.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.colAccountDate.UnboundFieldName = "colAccountDate";
            this.colAccountDate.Width = 183;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.winControlContainer1});
            this.Detail.HeightF = 197.9167F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // winControlContainer1
            // 
            this.winControlContainer1.LocationFloat = new DevExpress.Utils.PointFloat(10.00002F, 10.00001F);
            this.winControlContainer1.Name = "winControlContainer1";
            this.winControlContainer1.SizeF = new System.Drawing.SizeF(718.9998F, 177.9167F);
            this.winControlContainer1.WinControl = this.pgcMultiAbstractPayment;
            // 
            // pgcMultiAbstractPayment
            // 
            this.pgcMultiAbstractPayment.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.pgcMultiAbstractPayment.Appearance.Cell.Options.UseBackColor = true;
            this.pgcMultiAbstractPayment.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pgcMultiAbstractPayment.Appearance.FieldValue.Options.UseFont = true;
            this.pgcMultiAbstractPayment.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.pgcMultiAbstractPayment.Appearance.GrandTotalCell.Options.UseFont = true;
            this.pgcMultiAbstractPayment.Appearance.HeaderArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.pgcMultiAbstractPayment.Appearance.HeaderArea.Options.UseBackColor = true;
            this.pgcMultiAbstractPayment.Appearance.HeaderGroupLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pgcMultiAbstractPayment.Appearance.HeaderGroupLine.Options.UseFont = true;
            this.pgcMultiAbstractPayment.Appearance.TotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.pgcMultiAbstractPayment.Appearance.TotalCell.Options.UseBackColor = true;
            this.pgcMultiAbstractPayment.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.colLedgercode,
            this.colLedgerName,
            this.colTotal,
            this.colAccountDate});
            pivotGridGroup1.Caption = "Voucher_Date";
            pivotGridGroup1.Fields.Add(this.colAccountDate);
            pivotGridGroup1.Hierarchy = null;
            pivotGridGroup1.ShowNewValues = true;
            this.pgcMultiAbstractPayment.Groups.AddRange(new DevExpress.XtraPivotGrid.PivotGridGroup[] {
            pivotGridGroup1});
            this.pgcMultiAbstractPayment.Location = new System.Drawing.Point(0, 0);
            this.pgcMultiAbstractPayment.Name = "pgcMultiAbstractPayment";
            this.pgcMultiAbstractPayment.OptionsCustomization.AllowEdit = false;
            this.pgcMultiAbstractPayment.OptionsCustomization.AllowResizing = false;
            this.pgcMultiAbstractPayment.OptionsCustomization.AllowSort = false;
            this.pgcMultiAbstractPayment.OptionsDataField.AreaIndex = 0;
            this.pgcMultiAbstractPayment.OptionsSelection.CellSelection = false;
            this.pgcMultiAbstractPayment.OptionsView.ShowColumnHeaders = false;
            this.pgcMultiAbstractPayment.OptionsView.ShowDataHeaders = false;
            this.pgcMultiAbstractPayment.OptionsView.ShowFilterHeaders = false;
            this.pgcMultiAbstractPayment.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.pgcMultiAbstractPayment.Size = new System.Drawing.Size(690, 171);
            this.pgcMultiAbstractPayment.TabIndex = 0;
            // 
            // colLedgercode
            // 
            this.colLedgercode.Appearance.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.colLedgercode.Appearance.Header.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLedgercode.Appearance.Header.Options.UseBackColor = true;
            this.colLedgercode.Appearance.Header.Options.UseFont = true;
            this.colLedgercode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colLedgercode.AreaIndex = 0;
            this.colLedgercode.Caption = "Code";
            this.colLedgercode.FieldName = "LEDGER_CODE";
            this.colLedgercode.Name = "colLedgercode";
            this.colLedgercode.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending;
            this.colLedgercode.Width = 156;
            // 
            // colLedgerName
            // 
            this.colLedgerName.Appearance.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.colLedgerName.Appearance.Header.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLedgerName.Appearance.Header.Options.UseBackColor = true;
            this.colLedgerName.Appearance.Header.Options.UseFont = true;
            this.colLedgerName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colLedgerName.AreaIndex = 1;
            this.colLedgerName.Caption = "Name";
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending;
            this.colLedgerName.Width = 150;
            // 
            // colTotal
            // 
            this.colTotal.Appearance.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.colTotal.Appearance.Header.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTotal.Appearance.Header.Options.UseBackColor = true;
            this.colTotal.Appearance.Header.Options.UseFont = true;
            this.colTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.colTotal.AreaIndex = 0;
            this.colTotal.Caption = "Total";
            this.colTotal.FieldName = "TOTAL";
            this.colTotal.GrandTotalText = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.Width = 80;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 30.41666F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 160F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.HeightF = 66.66666F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // MultiAbstractPayments
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.Margins = new System.Drawing.Printing.Margins(77, 36, 30, 160);
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this.pgcMultiAbstractPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.WinControlContainer winControlContainer1;
        private DevExpress.XtraPivotGrid.PivotGridControl pgcMultiAbstractPayment;
        private DevExpress.XtraPivotGrid.PivotGridField colLedgercode;
        private DevExpress.XtraPivotGrid.PivotGridField colLedgerName;
        private DevExpress.XtraPivotGrid.PivotGridField colTotal;
        private DevExpress.XtraPivotGrid.PivotGridField colAccountDate;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
    }
}
