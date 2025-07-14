namespace Bosco.Report.ReportObject
{
    partial class MultiAbstractReceiptRecord
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
            this.colVoucherDate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pvMultiAbstractReceipts = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.colledgercode = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colLedgerName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colTotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.winControlContainer1 = new DevExpress.XtraReports.UI.WinControlContainer();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xtGrandTotalAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrandProgressAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubreportMultiReceiptAmt = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.pvMultiAbstractReceipts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.winControlContainer1});
            this.Detail.HeightF = 143.8331F;
            // 
            // colVoucherDate
            // 
            this.colVoucherDate.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.colVoucherDate.AreaIndex = 0;
            this.colVoucherDate.Caption = "VoucherDate";
            this.colVoucherDate.FieldName = "VOUCHER_DATE";
            this.colVoucherDate.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
            this.colVoucherDate.Name = "colVoucherDate";
            this.colVoucherDate.UnboundFieldName = "colVoucherDate";
            this.colVoucherDate.Width = 129;
            // 
            // pvMultiAbstractReceipts
            // 
            this.pvMultiAbstractReceipts.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pvMultiAbstractReceipts.Appearance.FieldHeader.Options.UseFont = true;
            this.pvMultiAbstractReceipts.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.colledgercode,
            this.colLedgerName,
            this.colTotal,
            this.colVoucherDate});
            pivotGridGroup1.Fields.Add(this.colVoucherDate);
            pivotGridGroup1.Hierarchy = null;
            pivotGridGroup1.ShowNewValues = true;
            this.pvMultiAbstractReceipts.Groups.AddRange(new DevExpress.XtraPivotGrid.PivotGridGroup[] {
            pivotGridGroup1});
            this.pvMultiAbstractReceipts.Location = new System.Drawing.Point(0, 0);
            this.pvMultiAbstractReceipts.Name = "pvMultiAbstractReceipts";
            this.pvMultiAbstractReceipts.OptionsView.ShowColumnHeaders = false;
            this.pvMultiAbstractReceipts.OptionsView.ShowCustomTotalsForSingleValues = true;
            this.pvMultiAbstractReceipts.OptionsView.ShowDataHeaders = false;
            this.pvMultiAbstractReceipts.OptionsView.ShowFilterHeaders = false;
            this.pvMultiAbstractReceipts.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.pvMultiAbstractReceipts.Size = new System.Drawing.Size(643, 136);
            this.pvMultiAbstractReceipts.TabIndex = 0;
            // 
            // colledgercode
            // 
            this.colledgercode.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colledgercode.Appearance.Header.Options.UseFont = true;
            this.colledgercode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colledgercode.AreaIndex = 0;
            this.colledgercode.Caption = "Code";
            this.colledgercode.FieldName = "LEDGER_CODE";
            this.colledgercode.Name = "colledgercode";
            this.colledgercode.Width = 105;
            // 
            // colLedgerName
            // 
            this.colLedgerName.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLedgerName.Appearance.Header.Options.UseFont = true;
            this.colLedgerName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colLedgerName.AreaIndex = 1;
            this.colLedgerName.Caption = "Particulars";
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            // 
            // colTotal
            // 
            this.colTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.colTotal.AreaIndex = 0;
            this.colTotal.Caption = "Total";
            this.colTotal.FieldName = "TOTAL";
            this.colTotal.Name = "colTotal";
            // 
            // winControlContainer1
            // 
            this.winControlContainer1.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 1.666705F);
            this.winControlContainer1.Name = "winControlContainer1";
            this.winControlContainer1.SizeF = new System.Drawing.SizeF(669.7916F, 142.1664F);
            this.winControlContainer1.WinControl = this.pvMultiAbstractReceipts;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrTable2,
            this.xrSubreportMultiReceiptAmt});
            this.ReportFooter.HeightF = 116.6666F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 13F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(47.83333F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(147.0834F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Opening Balance";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(47.83333F, 88.62502F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(458.3332F, 25F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotal,
            this.xtGrandTotalAmt,
            this.xrGrandProgressAmt});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrGrandTotal
            // 
            this.xrGrandTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.xrGrandTotal.Font = new System.Drawing.Font("Tahoma", 8.75F, System.Drawing.FontStyle.Bold);
            this.xrGrandTotal.Name = "xrGrandTotal";
            this.xrGrandTotal.StylePriority.UseBackColor = false;
            this.xrGrandTotal.StylePriority.UseFont = false;
            this.xrGrandTotal.StylePriority.UseTextAlignment = false;
            this.xrGrandTotal.Text = "Grand Total";
            this.xrGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrGrandTotal.Weight = 1.020303745696387D;
            // 
            // xtGrandTotalAmt
            // 
            this.xtGrandTotalAmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.xtGrandTotalAmt.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xtGrandTotalAmt.Name = "xtGrandTotalAmt";
            this.xtGrandTotalAmt.StylePriority.UseBackColor = false;
            this.xtGrandTotalAmt.StylePriority.UseFont = false;
            this.xtGrandTotalAmt.StylePriority.UseTextAlignment = false;
            this.xtGrandTotalAmt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xtGrandTotalAmt.Weight = 1.1942618619974816D;
            // 
            // xrGrandProgressAmt
            // 
            this.xrGrandProgressAmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(227)))));
            this.xrGrandProgressAmt.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrGrandProgressAmt.Name = "xrGrandProgressAmt";
            this.xrGrandProgressAmt.StylePriority.UseBackColor = false;
            this.xrGrandProgressAmt.StylePriority.UseFont = false;
            this.xrGrandProgressAmt.StylePriority.UseTextAlignment = false;
            this.xrGrandProgressAmt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrGrandProgressAmt.Weight = 1.3367928000443798D;
            // 
            // xrSubreportMultiReceiptAmt
            // 
            this.xrSubreportMultiReceiptAmt.LocationFloat = new DevExpress.Utils.PointFloat(47.83333F, 22.91667F);
            this.xrSubreportMultiReceiptAmt.Name = "xrSubreportMultiReceiptAmt";
            this.xrSubreportMultiReceiptAmt.ReportSource = new Bosco.Report.ReportObject.OpBalance();
            this.xrSubreportMultiReceiptAmt.SizeF = new System.Drawing.SizeF(458.3333F, 65.70835F);
            // 
            // MultiAbstractReceiptRecord
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.ReportFooter});
            this.Version = "12.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pvMultiAbstractReceipts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.WinControlContainer winControlContainer1;
        private DevExpress.XtraPivotGrid.PivotGridControl pvMultiAbstractReceipts;
        private DevExpress.XtraPivotGrid.PivotGridField colledgercode;
        private DevExpress.XtraPivotGrid.PivotGridField colLedgerName;
        private DevExpress.XtraPivotGrid.PivotGridField colTotal;
        private DevExpress.XtraPivotGrid.PivotGridField colVoucherDate;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xtGrandTotalAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandProgressAmt;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportMultiReceiptAmt;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
    }
}
