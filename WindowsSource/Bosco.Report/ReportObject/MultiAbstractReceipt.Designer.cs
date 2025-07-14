namespace Bosco.Report.ReportObject
{
    partial class MultiAbstractReceipt
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
            ((System.ComponentModel.ISupportInitialize)(this.pvMultiAbstractReceipts)).BeginInit();
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
            this.colVoucherDate.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherDate.Options.AllowEdit = false;
            this.colVoucherDate.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherDate.Options.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colVoucherDate.UnboundFieldName = "colVoucherDate";
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
            this.pvMultiAbstractReceipts.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
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
            this.colledgercode.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colledgercode.Options.AllowEdit = false;
            this.colledgercode.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colledgercode.Options.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colledgercode.Width = 80;
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
            this.colLedgerName.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerName.Options.AllowEdit = false;
            this.colLedgerName.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerName.Options.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colLedgerName.Width = 200;
            // 
            // colTotal
            // 
            this.colTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.colTotal.AreaIndex = 0;
            this.colTotal.Caption = "Total";
            this.colTotal.FieldName = "TOTAL";
            this.colTotal.Name = "colTotal";
            this.colTotal.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colTotal.Options.AllowEdit = false;
            this.colTotal.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colTotal.Options.AllowSort = DevExpress.Utils.DefaultBoolean.True;
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
            this.ReportFooter.HeightF = 1.041603F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // MultiAbstractReceipt
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.ReportFooter});
            this.Version = "12.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pvMultiAbstractReceipts)).EndInit();
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
    }
}
