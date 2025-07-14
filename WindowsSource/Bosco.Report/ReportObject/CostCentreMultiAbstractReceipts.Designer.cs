namespace Bosco.Report.ReportObject
{
    partial class CostCentreMultiAbstractReceipts
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
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrCosPGMultiAbstractReceipt = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldGROUPCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLEDGERGROUP = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLEDGERCODE = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLEDGERNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldMONTHNAME = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAMOUNT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrLabelOpBal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCosSubBalanceMulti = new DevExpress.XtraReports.UI.XRSubreport();
            this.styleInstitute = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleReportSubTitle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleDateInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.stylePageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleEvenRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeader = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRow = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleGroupRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleColumnHeaderSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.styleTotalRowSmall = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrCosPGGrandTotal = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldGRANTTOTALPARTICULARS = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldGRANTTOTALMONTH = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldGRANTTOTALAMOUNT = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCosPGMultiAbstractReceipt});
            this.Detail.HeightF = 51.04167F;
            this.Detail.Visible = true;
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 1.041603F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.Visible = false;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrCosPGMultiAbstractReceipt
            // 
            this.xrCosPGMultiAbstractReceipt.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractReceipt.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractReceipt.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrCosPGMultiAbstractReceipt.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrCosPGMultiAbstractReceipt.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractReceipt.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractReceipt.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractReceipt.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGMultiAbstractReceipt.CellStyleName = "styleRowSmall";
            this.xrCosPGMultiAbstractReceipt.FieldHeaderStyleName = "styleColumnHeaderSmall";
            this.xrCosPGMultiAbstractReceipt.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldGROUPCODE,
            this.fieldLEDGERGROUP,
            this.fieldLEDGERCODE,
            this.fieldLEDGERNAME,
            this.fieldMONTHNAME,
            this.fieldAMOUNT});
            this.xrCosPGMultiAbstractReceipt.FieldValueGrandTotalStyleName = "styleTotalRowSmall";
            this.xrCosPGMultiAbstractReceipt.FieldValueStyleName = "styleRowSmall";
            this.xrCosPGMultiAbstractReceipt.FieldValueTotalStyleName = "styleTotalRowSmall";
            this.xrCosPGMultiAbstractReceipt.GrandTotalCellStyleName = "styleTotalRowSmall";
            this.xrCosPGMultiAbstractReceipt.HeaderGroupLineStyleName = "styleGroupRowSmall";
            this.xrCosPGMultiAbstractReceipt.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            this.xrCosPGMultiAbstractReceipt.Name = "xrCosPGMultiAbstractReceipt";
            this.xrCosPGMultiAbstractReceipt.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrCosPGMultiAbstractReceipt.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrCosPGMultiAbstractReceipt.OptionsView.ShowColumnHeaders = false;
            this.xrCosPGMultiAbstractReceipt.OptionsView.ShowDataHeaders = false;
            this.xrCosPGMultiAbstractReceipt.SizeF = new System.Drawing.SizeF(1128F, 49.37499F);
            this.xrCosPGMultiAbstractReceipt.CustomFieldSort += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs>(this.xrPGMultiAbstractReceipt_CustomFieldSort);
            this.xrCosPGMultiAbstractReceipt.FieldValueDisplayText += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs>(this.xrPGMultiAbstractReceipt_FieldValueDisplayText);
            this.xrCosPGMultiAbstractReceipt.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPGMultiAbstractReceipt_PrintFieldValue);
            this.xrCosPGMultiAbstractReceipt.PrintCell += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportCellEventArgs>(this.xrPGMultiAbstractReceipt_PrintCell);
            this.xrCosPGMultiAbstractReceipt.AfterPrint += new System.EventHandler(this.xrPGMultiAbstractReceipt_AfterPrint);
            // 
            // fieldGROUPCODE
            // 
            this.fieldGROUPCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldGROUPCODE.AreaIndex = 0;
            this.fieldGROUPCODE.Caption = "G Code";
            this.fieldGROUPCODE.FieldName = "GROUP_CODE";
            this.fieldGROUPCODE.Name = "fieldGROUPCODE";
            this.fieldGROUPCODE.Width = 50;
            // 
            // fieldLEDGERGROUP
            // 
            this.fieldLEDGERGROUP.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERGROUP.AreaIndex = 1;
            this.fieldLEDGERGROUP.Caption = "Group";
            this.fieldLEDGERGROUP.FieldName = "LEDGER_GROUP";
            this.fieldLEDGERGROUP.Name = "fieldLEDGERGROUP";
            // 
            // fieldLEDGERCODE
            // 
            this.fieldLEDGERCODE.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERCODE.AreaIndex = 2;
            this.fieldLEDGERCODE.Caption = "Code";
            this.fieldLEDGERCODE.FieldName = "LEDGER_CODE";
            this.fieldLEDGERCODE.Name = "fieldLEDGERCODE";
            this.fieldLEDGERCODE.Width = 50;
            // 
            // fieldLEDGERNAME
            // 
            this.fieldLEDGERNAME.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldLEDGERNAME.AreaIndex = 3;
            this.fieldLEDGERNAME.Caption = "Particulars";
            this.fieldLEDGERNAME.FieldName = "LEDGER_NAME";
            this.fieldLEDGERNAME.Name = "fieldLEDGERNAME";
            this.fieldLEDGERNAME.Width = 150;
            // 
            // fieldMONTHNAME
            // 
            this.fieldMONTHNAME.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldMONTHNAME.AreaIndex = 0;
            this.fieldMONTHNAME.FieldName = "MONTH_NAME";
            this.fieldMONTHNAME.Name = "fieldMONTHNAME";
            this.fieldMONTHNAME.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.fieldMONTHNAME.Width = 90;
            // 
            // fieldAMOUNT
            // 
            this.fieldAMOUNT.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldAMOUNT.AreaIndex = 0;
            this.fieldAMOUNT.CellFormat.FormatString = "{0:n}";
            this.fieldAMOUNT.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAMOUNT.FieldName = "AMOUNT";
            this.fieldAMOUNT.Name = "fieldAMOUNT";
            this.fieldAMOUNT.ValueFormat.FormatString = "{0:n}";
            this.fieldAMOUNT.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAMOUNT.Width = 90;
            // 
            // xrLabelOpBal
            // 
            this.xrLabelOpBal.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabelOpBal.LocationFloat = new DevExpress.Utils.PointFloat(3.00003F, 5F);
            this.xrLabelOpBal.Name = "xrLabelOpBal";
            this.xrLabelOpBal.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabelOpBal.SizeF = new System.Drawing.SizeF(107.5001F, 12.58332F);
            this.xrLabelOpBal.StyleName = "styleTotalRowSmall";
            this.xrLabelOpBal.Text = "Opening Balance";
            // 
            // xrCosSubBalanceMulti
            // 
            this.xrCosSubBalanceMulti.LocationFloat = new DevExpress.Utils.PointFloat(0F, 7.916641F);
            this.xrCosSubBalanceMulti.Name = "xrCosSubBalanceMulti";
            this.xrCosSubBalanceMulti.ReportSource = new Bosco.Report.ReportObject.AccountBalanceMulti();
            this.xrCosSubBalanceMulti.SizeF = new System.Drawing.SizeF(1130F, 50.00002F);
            // 
            // styleInstitute
            // 
            this.styleInstitute.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleInstitute.Name = "styleInstitute";
            this.styleInstitute.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleInstitute.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleReportTitle
            // 
            this.styleReportTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportTitle.Name = "styleReportTitle";
            this.styleReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleReportSubTitle
            // 
            this.styleReportSubTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleReportSubTitle.Name = "styleReportSubTitle";
            this.styleReportSubTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.styleReportSubTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleDateInfo
            // 
            this.styleDateInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleDateInfo.Name = "styleDateInfo";
            this.styleDateInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.styleDateInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // stylePageInfo
            // 
            this.stylePageInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stylePageInfo.Name = "stylePageInfo";
            this.stylePageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.stylePageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // styleEvenRow
            // 
            this.styleEvenRow.BackColor = System.Drawing.Color.White;
            this.styleEvenRow.BorderColor = System.Drawing.Color.Silver;
            this.styleEvenRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleEvenRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleEvenRow.BorderWidth = 1;
            this.styleEvenRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleEvenRow.Name = "styleEvenRow";
            this.styleEvenRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleGroupRow
            // 
            this.styleGroupRow.BackColor = System.Drawing.Color.Linen;
            this.styleGroupRow.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleGroupRow.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.styleGroupRow.BorderWidth = 1;
            this.styleGroupRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRow.Name = "styleGroupRow";
            this.styleGroupRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleRow
            // 
            this.styleRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRow.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRow.BorderWidth = 1;
            this.styleRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRow.Name = "styleRow";
            this.styleRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleColumnHeader
            // 
            this.styleColumnHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeader.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeader.BorderWidth = 1;
            this.styleColumnHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeader.Name = "styleColumnHeader";
            this.styleColumnHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleTotalRow
            // 
            this.styleTotalRow.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRow.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleTotalRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleTotalRow.BorderWidth = 1;
            this.styleTotalRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRow.Name = "styleTotalRow";
            this.styleTotalRow.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleGroupRowSmall
            // 
            this.styleGroupRowSmall.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleGroupRowSmall.BorderColor = System.Drawing.Color.Silver;
            this.styleGroupRowSmall.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleGroupRowSmall.ForeColor = System.Drawing.Color.IndianRed;
            this.styleGroupRowSmall.Name = "styleGroupRowSmall";
            // 
            // styleRowSmall
            // 
            this.styleRowSmall.BackColor = System.Drawing.Color.Empty;
            this.styleRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleRowSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleRowSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleRowSmall.BorderWidth = 1;
            this.styleRowSmall.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleRowSmall.Name = "styleRowSmall";
            this.styleRowSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.styleRowSmall.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // styleColumnHeaderSmall
            // 
            this.styleColumnHeaderSmall.BackColor = System.Drawing.Color.Gainsboro;
            this.styleColumnHeaderSmall.BorderColor = System.Drawing.Color.DarkGray;
            this.styleColumnHeaderSmall.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.styleColumnHeaderSmall.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.styleColumnHeaderSmall.BorderWidth = 1;
            this.styleColumnHeaderSmall.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleColumnHeaderSmall.Name = "styleColumnHeaderSmall";
            this.styleColumnHeaderSmall.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            // 
            // styleTotalRowSmall
            // 
            this.styleTotalRowSmall.BackColor = System.Drawing.Color.WhiteSmoke;
            this.styleTotalRowSmall.BorderColor = System.Drawing.Color.Gainsboro;
            this.styleTotalRowSmall.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleTotalRowSmall.Name = "styleTotalRowSmall";
            // 
            // xrCosPGGrandTotal
            // 
            this.xrCosPGGrandTotal.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGGrandTotal.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGGrandTotal.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrCosPGGrandTotal.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrCosPGGrandTotal.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGGrandTotal.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGGrandTotal.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGGrandTotal.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrCosPGGrandTotal.CellStyleName = "styleTotalRowSmall";
            this.xrCosPGGrandTotal.DataMember = "MultiAbstract";
            this.xrCosPGGrandTotal.FieldHeaderStyleName = "styleColumnHeaderSmall";
            this.xrCosPGGrandTotal.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldGRANTTOTALPARTICULARS,
            this.fieldGRANTTOTALMONTH,
            this.fieldGRANTTOTALAMOUNT});
            this.xrCosPGGrandTotal.FieldValueGrandTotalStyleName = "styleTotalRowSmall";
            this.xrCosPGGrandTotal.FieldValueStyleName = "styleTotalRowSmall";
            this.xrCosPGGrandTotal.FieldValueTotalStyleName = "styleTotalRowSmall";
            this.xrCosPGGrandTotal.GrandTotalCellStyleName = "styleTotalRowSmall";
            this.xrCosPGGrandTotal.HeaderGroupLineStyleName = "styleGroupRowSmall";
            this.xrCosPGGrandTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrCosPGGrandTotal.Name = "xrCosPGGrandTotal";
            this.xrCosPGGrandTotal.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrCosPGGrandTotal.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrCosPGGrandTotal.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrCosPGGrandTotal.OptionsView.ShowColumnGrandTotals = false;
            this.xrCosPGGrandTotal.OptionsView.ShowColumnHeaders = false;
            this.xrCosPGGrandTotal.OptionsView.ShowDataHeaders = false;
            this.xrCosPGGrandTotal.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrCosPGGrandTotal.OptionsView.ShowRowGrandTotals = false;
            this.xrCosPGGrandTotal.SizeF = new System.Drawing.SizeF(1130F, 44.16669F);
            this.xrCosPGGrandTotal.PrintFieldValue += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs>(this.xrPGGrandTotal_PrintFieldValue);
            // 
            // fieldGRANTTOTALPARTICULARS
            // 
            this.fieldGRANTTOTALPARTICULARS.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldGRANTTOTALPARTICULARS.AreaIndex = 0;
            this.fieldGRANTTOTALPARTICULARS.Caption = "Particulars";
            this.fieldGRANTTOTALPARTICULARS.FieldName = "LEDGER_NAME";
            this.fieldGRANTTOTALPARTICULARS.Name = "fieldGRANTTOTALPARTICULARS";
            this.fieldGRANTTOTALPARTICULARS.Width = 150;
            // 
            // fieldGRANTTOTALMONTH
            // 
            this.fieldGRANTTOTALMONTH.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldGRANTTOTALMONTH.AreaIndex = 0;
            this.fieldGRANTTOTALMONTH.FieldName = "MONTH";
            this.fieldGRANTTOTALMONTH.Name = "fieldGRANTTOTALMONTH";
            this.fieldGRANTTOTALMONTH.Width = 90;
            // 
            // fieldGRANTTOTALAMOUNT
            // 
            this.fieldGRANTTOTALAMOUNT.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldGRANTTOTALAMOUNT.AreaIndex = 0;
            this.fieldGRANTTOTALAMOUNT.CellFormat.FormatString = "{0:n}";
            this.fieldGRANTTOTALAMOUNT.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldGRANTTOTALAMOUNT.FieldName = "AMOUNT";
            this.fieldGRANTTOTALAMOUNT.Name = "fieldGRANTTOTALAMOUNT";
            this.fieldGRANTTOTALAMOUNT.ValueFormat.FormatString = "{0:n}";
            this.fieldGRANTTOTALAMOUNT.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldGRANTTOTALAMOUNT.Width = 90;
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCosSubBalanceMulti,
            this.xrLabelOpBal});
            this.Detail1.HeightF = 57.91667F;
            this.Detail1.KeepTogetherWithDetailReports = true;
            this.Detail1.Name = "Detail1";
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // DetailReport1
            // 
            this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
            this.DetailReport1.Level = 1;
            this.DetailReport1.Name = "DetailReport1";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCosPGGrandTotal});
            this.Detail2.HeightF = 44.16669F;
            this.Detail2.Name = "Detail2";
            // 
            // CostCentreMultiAbstractReceipts
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.DetailReport,
            this.DetailReport1});
            this.DataMember = "MultiAbstract";
            this.DataSource = this.reportSetting1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(21, 18, 61, 76);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.styleEvenRow,
            this.styleGroupRow,
            this.styleColumnHeader,
            this.styleTotalRow,
            this.styleGroupRowSmall,
            this.styleRowSmall,
            this.styleColumnHeaderSmall,
            this.styleTotalRowSmall});
            this.Version = "12.2";
            this.Controls.SetChildIndex(this.DetailReport1, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRPivotGrid xrCosPGMultiAbstractReceipt;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERCODE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAMOUNT;
        private DevExpress.XtraReports.UI.XRSubreport xrCosSubBalanceMulti;
        private DevExpress.XtraReports.UI.XRLabel xrLabelOpBal;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMONTHNAME;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGROUPCODE;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLEDGERGROUP;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleInstitute;
        private DevExpress.XtraReports.UI.XRControlStyle styleReportTitle;
        private DevExpress.XtraReports.UI.XRControlStyle styleReportSubTitle;
        private DevExpress.XtraReports.UI.XRControlStyle styleDateInfo;
        private DevExpress.XtraReports.UI.XRControlStyle stylePageInfo;
        private DevExpress.XtraReports.UI.XRControlStyle styleEvenRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleGroupRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleColumnHeader;
        private DevExpress.XtraReports.UI.XRControlStyle styleTotalRow;
        private DevExpress.XtraReports.UI.XRControlStyle styleRowSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleColumnHeaderSmall;
        private DevExpress.XtraReports.UI.XRControlStyle styleTotalRowSmall;
        private DevExpress.XtraReports.UI.XRPivotGrid xrCosPGGrandTotal;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGRANTTOTALPARTICULARS;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGRANTTOTALMONTH;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldGRANTTOTALAMOUNT;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
    }
}
