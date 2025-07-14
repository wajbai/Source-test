namespace Bosco.Report.ReportObject
{
    partial class CashBankReceiptPaymentDetails
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrlblThrough = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCash = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRowBankReferenceCaption = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrlblThroughSpace1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellBankReferenceCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRowBankReference = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrlblThroughSpace2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellChequeReference = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.HeightF = 55F;
            this.Detail.Name = "Detail";
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrRowBankReferenceCaption,
            this.xrRowBankReference});
            this.xrTable1.SizeF = new System.Drawing.SizeF(562.71F, 55F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrlblThrough,
            this.xrCash,
            this.xrAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrlblThrough
            // 
            this.xrlblThrough.BorderColor = System.Drawing.Color.Silver;
            this.xrlblThrough.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrlblThrough.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrlblThrough.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrlblThrough.Name = "xrlblThrough";
            this.xrlblThrough.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrlblThrough.StylePriority.UseBorderColor = false;
            this.xrlblThrough.StylePriority.UseBorderDashStyle = false;
            this.xrlblThrough.StylePriority.UseBorders = false;
            this.xrlblThrough.StylePriority.UseFont = false;
            this.xrlblThrough.StylePriority.UsePadding = false;
            this.xrlblThrough.StylePriority.UseTextAlignment = false;
            this.xrlblThrough.Text = "Through";
            this.xrlblThrough.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrlblThrough.Weight = 0.53867333844303944D;
            // 
            // xrCash
            // 
            this.xrCash.BorderColor = System.Drawing.Color.Silver;
            this.xrCash.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrCash.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCash.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.LEDGER_NAME")});
            this.xrCash.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrCash.Name = "xrCash";
            this.xrCash.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCash.StylePriority.UseBorderColor = false;
            this.xrCash.StylePriority.UseBorderDashStyle = false;
            this.xrCash.StylePriority.UseBorders = false;
            this.xrCash.StylePriority.UseFont = false;
            this.xrCash.StylePriority.UsePadding = false;
            this.xrCash.StylePriority.UseTextAlignment = false;
            this.xrCash.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCash.Weight = 1.6996086643530006D;
            // 
            // xrAmount
            // 
            this.xrAmount.BorderColor = System.Drawing.Color.Silver;
            this.xrAmount.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrAmount.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.AMOUNT", "{0:n}")});
            this.xrAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrAmount.Name = "xrAmount";
            this.xrAmount.StylePriority.UseBorderColor = false;
            this.xrAmount.StylePriority.UseBorderDashStyle = false;
            this.xrAmount.StylePriority.UseBorders = false;
            this.xrAmount.StylePriority.UseFont = false;
            this.xrAmount.StylePriority.UseTextAlignment = false;
            xrSummary1.IgnoreNullValues = true;
            this.xrAmount.Summary = xrSummary1;
            this.xrAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrAmount.Weight = 0.65129741705029121D;
            this.xrAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrAmount_BeforePrint);
            // 
            // xrRowBankReferenceCaption
            // 
            this.xrRowBankReferenceCaption.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrlblThroughSpace1,
            this.xrCellBankReferenceCaption});
            this.xrRowBankReferenceCaption.Name = "xrRowBankReferenceCaption";
            this.xrRowBankReferenceCaption.StylePriority.UseTextAlignment = false;
            this.xrRowBankReferenceCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrRowBankReferenceCaption.Weight = 0.60000000000000009D;
            this.xrRowBankReferenceCaption.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrRowBankReferenceCaption_BeforePrint);
            // 
            // xrlblThroughSpace1
            // 
            this.xrlblThroughSpace1.Name = "xrlblThroughSpace1";
            this.xrlblThroughSpace1.Weight = 0.53867337662505543D;
            // 
            // xrCellBankReferenceCaption
            // 
            this.xrCellBankReferenceCaption.BorderColor = System.Drawing.Color.Silver;
            this.xrCellBankReferenceCaption.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrCellBankReferenceCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellBankReferenceCaption.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrCellBankReferenceCaption.Name = "xrCellBankReferenceCaption";
            this.xrCellBankReferenceCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrCellBankReferenceCaption.StylePriority.UseBorderColor = false;
            this.xrCellBankReferenceCaption.StylePriority.UseBorderDashStyle = false;
            this.xrCellBankReferenceCaption.StylePriority.UseBorders = false;
            this.xrCellBankReferenceCaption.StylePriority.UseFont = false;
            this.xrCellBankReferenceCaption.StylePriority.UsePadding = false;
            this.xrCellBankReferenceCaption.StylePriority.UseTextAlignment = false;
            this.xrCellBankReferenceCaption.Text = "Bank Transaction Details : ";
            this.xrCellBankReferenceCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrCellBankReferenceCaption.Weight = 2.3509060432212761D;
            this.xrCellBankReferenceCaption.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCellBankReferenceCaption_BeforePrint);
            // 
            // xrRowBankReference
            // 
            this.xrRowBankReference.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrRowBankReference.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrlblThroughSpace2,
            this.xrCellChequeReference});
            this.xrRowBankReference.Name = "xrRowBankReference";
            this.xrRowBankReference.StylePriority.UseBorderDashStyle = false;
            this.xrRowBankReference.Weight = 0.60000000000000009D;
            this.xrRowBankReference.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrRowBankReference_BeforePrint);
            // 
            // xrlblThroughSpace2
            // 
            this.xrlblThroughSpace2.BorderColor = System.Drawing.Color.Silver;
            this.xrlblThroughSpace2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrlblThroughSpace2.Name = "xrlblThroughSpace2";
            this.xrlblThroughSpace2.StylePriority.UseBorderColor = false;
            this.xrlblThroughSpace2.StylePriority.UseBorders = false;
            this.xrlblThroughSpace2.Weight = 0.53867341580285677D;
            // 
            // xrCellChequeReference
            // 
            this.xrCellChequeReference.BorderColor = System.Drawing.Color.Silver;
            this.xrCellChequeReference.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCellChequeReference.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrCellChequeReference.Name = "xrCellChequeReference";
            this.xrCellChequeReference.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrCellChequeReference.StylePriority.UseBorderColor = false;
            this.xrCellChequeReference.StylePriority.UseBorders = false;
            this.xrCellChequeReference.StylePriority.UseFont = false;
            this.xrCellChequeReference.StylePriority.UsePadding = false;
            this.xrCellChequeReference.Text = "Cheque/DD 345678 10-2017 SBI TPT";
            this.xrCellChequeReference.Weight = 2.3509060040434746D;
            this.xrCellChequeReference.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCellChequeReference_BeforePrint);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // CashBankReceiptPaymentDetails
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Margins = new System.Drawing.Printing.Margins(48, 132, 20, 20);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCash;
        private ReportSetting reportSetting1;
        public DevExpress.XtraReports.UI.XRTableCell xrAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrlblThrough;
        private DevExpress.XtraReports.UI.XRTableRow xrRowBankReferenceCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrCellBankReferenceCaption;
        private DevExpress.XtraReports.UI.XRTableRow xrRowBankReference;
        private DevExpress.XtraReports.UI.XRTableCell xrCellChequeReference;
        private DevExpress.XtraReports.UI.XRTableCell xrlblThroughSpace1;
        private DevExpress.XtraReports.UI.XRTableCell xrlblThroughSpace2;
    }
}
