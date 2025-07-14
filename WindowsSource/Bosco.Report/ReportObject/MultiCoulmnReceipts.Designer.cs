namespace Bosco.Report.ReportObject
{
    partial class MultiCoulmnReceipts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiCoulmnReceipts));
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPGDrMuticolumnCashBank = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldDrDATE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDrPARTICULARS = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDrVOUCHERTYPE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDrNUMBER = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField2 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPGDrMuticolumnCashBank});
            resources.ApplyResources(this.GroupHeader1, "GroupHeader1");
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrPGDrMuticolumnCashBank
            // 
            this.xrPGDrMuticolumnCashBank.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrPGDrMuticolumnCashBank.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldDrDATE,
            this.fieldDrPARTICULARS,
            this.fieldDrVOUCHERTYPE,
            this.fieldDrNUMBER,
            this.xrPivotGridField1,
            this.xrPivotGridField2});
            resources.ApplyResources(this.xrPGDrMuticolumnCashBank, "xrPGDrMuticolumnCashBank");
            this.xrPGDrMuticolumnCashBank.Name = "xrPGDrMuticolumnCashBank";
            this.xrPGDrMuticolumnCashBank.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPGDrMuticolumnCashBank.OptionsPrint.MergeRowFieldValues = false;
            this.xrPGDrMuticolumnCashBank.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPGDrMuticolumnCashBank.OptionsView.ShowColumnHeaders = false;
            this.xrPGDrMuticolumnCashBank.OptionsView.ShowDataHeaders = false;
            this.xrPGDrMuticolumnCashBank.OptionsView.ShowRowTotals = false;
            this.xrPGDrMuticolumnCashBank.OptionsView.ShowTotalsForSingleValues = true;
            this.xrPGDrMuticolumnCashBank.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(0, 50, 0, 0, 100F);
            // 
            // fieldDrDATE
            // 
            this.fieldDrDATE.Appearance.Cell.Font = new System.Drawing.Font("Arial", 7F);
            this.fieldDrDATE.Appearance.FieldHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fieldDrDATE.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 8F);
            this.fieldDrDATE.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldDrDATE.Appearance.FieldValue.Font = new System.Drawing.Font("Arial", 8F);
            this.fieldDrDATE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDrDATE.AreaIndex = 0;
            resources.ApplyResources(this.fieldDrDATE, "fieldDrDATE");
            this.fieldDrDATE.CellFormat.FormatString = "d";
            this.fieldDrDATE.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.fieldDrDATE.Name = "fieldDrDATE";
            this.fieldDrDATE.ValueFormat.FormatString = "d";
            this.fieldDrDATE.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // fieldDrPARTICULARS
            // 
            this.fieldDrPARTICULARS.Appearance.Cell.Font = new System.Drawing.Font("Arial", 7.5F);
            this.fieldDrPARTICULARS.Appearance.Cell.WordWrap = true;
            this.fieldDrPARTICULARS.Appearance.FieldHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fieldDrPARTICULARS.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.fieldDrPARTICULARS.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldDrPARTICULARS.Appearance.FieldValue.Font = new System.Drawing.Font("Arial", 7F);
            this.fieldDrPARTICULARS.Appearance.FieldValue.WordWrap = true;
            this.fieldDrPARTICULARS.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDrPARTICULARS.AreaIndex = 1;
            resources.ApplyResources(this.fieldDrPARTICULARS, "fieldDrPARTICULARS");
            this.fieldDrPARTICULARS.Name = "fieldDrPARTICULARS";
            // 
            // fieldDrVOUCHERTYPE
            // 
            this.fieldDrVOUCHERTYPE.Appearance.Cell.Font = new System.Drawing.Font("Arial", 8F);
            this.fieldDrVOUCHERTYPE.Appearance.FieldHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fieldDrVOUCHERTYPE.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 8F);
            this.fieldDrVOUCHERTYPE.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldDrVOUCHERTYPE.Appearance.FieldValue.Font = new System.Drawing.Font("Arial", 8F);
            this.fieldDrVOUCHERTYPE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDrVOUCHERTYPE.AreaIndex = 2;
            resources.ApplyResources(this.fieldDrVOUCHERTYPE, "fieldDrVOUCHERTYPE");
            this.fieldDrVOUCHERTYPE.Name = "fieldDrVOUCHERTYPE";
            // 
            // fieldDrNUMBER
            // 
            this.fieldDrNUMBER.Appearance.Cell.Font = new System.Drawing.Font("Arial", 8F);
            this.fieldDrNUMBER.Appearance.FieldHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fieldDrNUMBER.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 8F);
            this.fieldDrNUMBER.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldDrNUMBER.Appearance.FieldValue.Font = new System.Drawing.Font("Arial", 8F);
            this.fieldDrNUMBER.Appearance.FieldValue.WordWrap = true;
            this.fieldDrNUMBER.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDrNUMBER.AreaIndex = 3;
            resources.ApplyResources(this.fieldDrNUMBER, "fieldDrNUMBER");
            this.fieldDrNUMBER.Name = "fieldDrNUMBER";
            // 
            // xrPivotGridField1
            // 
            this.xrPivotGridField1.Appearance.Cell.Font = new System.Drawing.Font("Arial", 8F);
            this.xrPivotGridField1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrPivotGridField1.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.xrPivotGridField1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial", 7F);
            this.xrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.xrPivotGridField1.AreaIndex = 0;
            this.xrPivotGridField1.CellFormat.FormatString = "{0:n}";
            this.xrPivotGridField1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            resources.ApplyResources(this.xrPivotGridField1, "xrPivotGridField1");
            this.xrPivotGridField1.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.Custom;
            this.xrPivotGridField1.Name = "xrPivotGridField1";
            this.xrPivotGridField1.UnboundFieldName = "xrPivotGridField1";
            this.xrPivotGridField1.ValueFormat.FormatString = "{0:n}";
            this.xrPivotGridField1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // xrPivotGridField2
            // 
            this.xrPivotGridField2.Appearance.Cell.Font = new System.Drawing.Font("Arial", 8F);
            this.xrPivotGridField2.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrPivotGridField2.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.xrPivotGridField2.Appearance.FieldValue.Font = new System.Drawing.Font("Arial", 7F);
            this.xrPivotGridField2.Appearance.FieldValue.WordWrap = true;
            this.xrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrPivotGridField2.AreaIndex = 0;
            resources.ApplyResources(this.xrPivotGridField2, "xrPivotGridField2");
            this.xrPivotGridField2.Name = "xrPivotGridField2";
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MultiCoulmnReceipts
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPGDrMuticolumnCashBank;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDrDATE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDrPARTICULARS;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDrVOUCHERTYPE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDrNUMBER;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField2;
        private ReportSetting reportSetting1;
    }
}
