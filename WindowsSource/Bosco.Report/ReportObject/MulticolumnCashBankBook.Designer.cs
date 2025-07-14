namespace Bosco.Report.ReportObject
{
    partial class MulticolumnCashBankBook
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
            this.xrPGDrMuticolumnCashBank = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldDrDATE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDrPARTICULARS = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDrVOUCHERTYPE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDrNUMBER = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDrBANK = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            this.Detail.HeightF = 68.95836F;
            this.Detail.Visible = true;
            // 
            // xrPGDrMuticolumnCashBank
            // 
            this.xrPGDrMuticolumnCashBank.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGDrMuticolumnCashBank.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGDrMuticolumnCashBank.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGDrMuticolumnCashBank.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGDrMuticolumnCashBank.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGDrMuticolumnCashBank.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGDrMuticolumnCashBank.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGDrMuticolumnCashBank.Appearance.Lines.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGDrMuticolumnCashBank.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPGDrMuticolumnCashBank.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldDrDATE,
            this.fieldDrPARTICULARS,
            this.fieldDrVOUCHERTYPE,
            this.fieldDrNUMBER,
            this.fieldDrBANK,
            this.xrPivotGridField1});
            this.xrPGDrMuticolumnCashBank.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPGDrMuticolumnCashBank.Name = "xrPGDrMuticolumnCashBank";
            this.xrPGDrMuticolumnCashBank.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPGDrMuticolumnCashBank.OptionsPrint.MergeRowFieldValues = false;
            this.xrPGDrMuticolumnCashBank.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPGDrMuticolumnCashBank.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPGDrMuticolumnCashBank.OptionsView.ShowColumnGrandTotals = false;
            this.xrPGDrMuticolumnCashBank.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrPGDrMuticolumnCashBank.OptionsView.ShowRowGrandTotals = false;
            this.xrPGDrMuticolumnCashBank.OptionsView.ShowTotalsForSingleValues = true;
            this.xrPGDrMuticolumnCashBank.SizeF = new System.Drawing.SizeF(1067F, 58.95835F);
            this.xrPGDrMuticolumnCashBank.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(0, 50, 0, 0, 100F);
            // 
            // fieldDrDATE
            // 
            this.fieldDrDATE.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.fieldDrDATE.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrDATE.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.fieldDrDATE.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrDATE.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrDATE.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrDATE.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrDATE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDrDATE.AreaIndex = 0;
            this.fieldDrDATE.Caption = "Date";
            this.fieldDrDATE.CellFormat.FormatString = "d";
            this.fieldDrDATE.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.fieldDrDATE.FieldName = "DATE";
            this.fieldDrDATE.Name = "fieldDrDATE";
            this.fieldDrDATE.ValueFormat.FormatString = "d";
            this.fieldDrDATE.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.fieldDrDATE.Width = 75;
            // 
            // fieldDrPARTICULARS
            // 
            this.fieldDrPARTICULARS.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrPARTICULARS.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrPARTICULARS.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.fieldDrPARTICULARS.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrPARTICULARS.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrPARTICULARS.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrPARTICULARS.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrPARTICULARS.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDrPARTICULARS.AreaIndex = 1;
            this.fieldDrPARTICULARS.Caption = "Particulars";
            this.fieldDrPARTICULARS.FieldName = "RECEIPT";
            this.fieldDrPARTICULARS.Name = "fieldDrPARTICULARS";
            // 
            // fieldDrVOUCHERTYPE
            // 
            this.fieldDrVOUCHERTYPE.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrVOUCHERTYPE.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrVOUCHERTYPE.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.fieldDrVOUCHERTYPE.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrVOUCHERTYPE.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrVOUCHERTYPE.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrVOUCHERTYPE.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrVOUCHERTYPE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDrVOUCHERTYPE.AreaIndex = 2;
            this.fieldDrVOUCHERTYPE.Caption = "Voucher Type";
            this.fieldDrVOUCHERTYPE.FieldName = "VOUCHER_TYPE";
            this.fieldDrVOUCHERTYPE.Name = "fieldDrVOUCHERTYPE";
            this.fieldDrVOUCHERTYPE.Width = 85;
            // 
            // fieldDrNUMBER
            // 
            this.fieldDrNUMBER.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrNUMBER.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrNUMBER.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.fieldDrNUMBER.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrNUMBER.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrNUMBER.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrNUMBER.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrNUMBER.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDrNUMBER.AreaIndex = 3;
            this.fieldDrNUMBER.Caption = "V.No";
            this.fieldDrNUMBER.FieldName = "RECEIPT_NO";
            this.fieldDrNUMBER.Name = "fieldDrNUMBER";
            this.fieldDrNUMBER.Width = 50;
            // 
            // fieldDrBANK
            // 
            this.fieldDrBANK.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrBANK.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrBANK.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.fieldDrBANK.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrBANK.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrBANK.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrBANK.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.fieldDrBANK.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldDrBANK.AreaIndex = 0;
            this.fieldDrBANK.Caption = "CashBank";
            this.fieldDrBANK.FieldName = "MULTICASHBANK";
            this.fieldDrBANK.Name = "fieldDrBANK";
            this.fieldDrBANK.Visible = false;
            // 
            // xrPivotGridField1
            // 
            this.xrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.xrPivotGridField1.AreaIndex = 0;
            this.xrPivotGridField1.CellFormat.FormatString = "{0:n}";
            this.xrPivotGridField1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.xrPivotGridField1.FieldName = "REC_LEDGER_AMOUNT";
            this.xrPivotGridField1.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.Custom;
            this.xrPivotGridField1.Name = "xrPivotGridField1";
            this.xrPivotGridField1.UnboundFieldName = "xrPivotGridField1";
            this.xrPivotGridField1.ValueFormat.FormatString = "{0:n}";
            this.xrPivotGridField1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPGDrMuticolumnCashBank});
            this.GroupHeader1.HeightF = 58.95835F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // MulticolumnCashBankBook
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1});
            this.Landscape = true;
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRPivotGrid xrPGDrMuticolumnCashBank;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDrDATE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDrPARTICULARS;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDrVOUCHERTYPE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDrNUMBER;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDrBANK;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
    }
}
