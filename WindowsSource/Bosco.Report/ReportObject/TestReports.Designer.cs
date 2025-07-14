namespace Bosco.Report.ReportObject
{
    partial class TestReports
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
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.pivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.pivotGridField2 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.pivotGridField3 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1});
            this.Detail.HeightF = 59.375F;
            this.Detail.Visible = true;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.Lines.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.pivotGridField1,
            this.pivotGridField2,
            this.pivotGridField3});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPivotGrid1.OptionsView.ShowRowTotals = false;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(723.0001F, 50F);
            this.xrPivotGrid1.FieldValueDisplayText += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs>(this.xrPivotGrid1_FieldValueDisplayText);
            this.xrPivotGrid1.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPivotGrid1_PrintFieldValue);
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField1.AreaIndex = 0;
            this.pivotGridField1.FieldName = "Category";
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField2.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField2.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField2.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField2.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField2.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField2.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField2.AreaIndex = 0;
            this.pivotGridField2.FieldName = "Year";
            this.pivotGridField2.Name = "pivotGridField2";
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField3.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField3.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField3.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField3.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField3.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField3.Appearance.TotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField3.AreaIndex = 0;
            this.pivotGridField3.FieldName = "Sales";
            this.pivotGridField3.Name = "pivotGridField3";
            // 
            // PageHeader
            // 
            this.PageHeader.Name = "PageHeader";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Name = "ReportFooter";
            // 
            // TestReports
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField3;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
    }
}
