namespace Bosco.Report.ReportObject
{
    partial class NatureofPayments
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
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCompany = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrNonCompany = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTotalPending = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrtblGrandTotal = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrGrandTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashBalanceAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 25F;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(722.9998F, 25F);
            this.xrTable1.StyleName = "styleColumnHeader";
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrCompany,
            this.xrNonCompany,
            this.xrTotalPending});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "Nature Of Payments";
            this.xrTableCell1.Weight = 3.8229165649414063D;
            // 
            // xrCompany
            // 
            this.xrCompany.Name = "xrCompany";
            this.xrCompany.StylePriority.UseTextAlignment = false;
            this.xrCompany.Text = "Company";
            this.xrCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCompany.Weight = 1.2291668701171876D;
            // 
            // xrNonCompany
            // 
            this.xrNonCompany.Name = "xrNonCompany";
            this.xrNonCompany.StylePriority.UseTextAlignment = false;
            this.xrNonCompany.Text = "Non Company";
            this.xrNonCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrNonCompany.Weight = 1.0833334350585937D;
            // 
            // xrTotalPending
            // 
            this.xrTotalPending.Name = "xrTotalPending";
            this.xrTotalPending.StylePriority.UseTextAlignment = false;
            this.xrTotalPending.Text = "Total Pending";
            this.xrTotalPending.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTotalPending.Weight = 1.0945812988281254D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrtblGrandTotal});
            this.ReportFooter.HeightF = 25F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrtblGrandTotal
            // 
            this.xrtblGrandTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.xrtblGrandTotal.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtblGrandTotal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrtblGrandTotal.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 0F);
            this.xrtblGrandTotal.Name = "xrtblGrandTotal";
            this.xrtblGrandTotal.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrtblGrandTotal.SizeF = new System.Drawing.SizeF(722.9999F, 25F);
            this.xrtblGrandTotal.StylePriority.UseBackColor = false;
            this.xrtblGrandTotal.StylePriority.UseBorderColor = false;
            this.xrtblGrandTotal.StylePriority.UseFont = false;
            this.xrtblGrandTotal.StylePriority.UseTextAlignment = false;
            this.xrtblGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrGrandTotal,
            this.xrCashAmt,
            this.xrCashBalanceAmt,
            this.xrTableCell15});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrGrandTotal
            // 
            this.xrGrandTotal.BackColor = System.Drawing.Color.Gainsboro;
            this.xrGrandTotal.BorderColor = System.Drawing.Color.DarkGray;
            this.xrGrandTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrGrandTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrGrandTotal.Name = "xrGrandTotal";
            this.xrGrandTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrGrandTotal.StylePriority.UseBackColor = false;
            this.xrGrandTotal.StylePriority.UseBorderColor = false;
            this.xrGrandTotal.StylePriority.UseBorders = false;
            this.xrGrandTotal.StylePriority.UseFont = false;
            this.xrGrandTotal.StylePriority.UsePadding = false;
            this.xrGrandTotal.StylePriority.UseTextAlignment = false;
            this.xrGrandTotal.Text = "Total";
            this.xrGrandTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrGrandTotal.Weight = 1.137204729196498D;
            // 
            // xrCashAmt
            // 
            this.xrCashAmt.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCashAmt.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCashAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashAmt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCashAmt.Name = "xrCashAmt";
            this.xrCashAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCashAmt.StylePriority.UseBackColor = false;
            this.xrCashAmt.StylePriority.UseBorderColor = false;
            this.xrCashAmt.StylePriority.UseBorders = false;
            this.xrCashAmt.StylePriority.UseFont = false;
            this.xrCashAmt.StylePriority.UsePadding = false;
            this.xrCashAmt.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.IgnoreNullValues = true;
            this.xrCashAmt.Summary = xrSummary1;
            this.xrCashAmt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCashAmt.Weight = 0.36373793002765165D;
            // 
            // xrCashBalanceAmt
            // 
            this.xrCashBalanceAmt.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCashBalanceAmt.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCashBalanceAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCashBalanceAmt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCashBalanceAmt.Name = "xrCashBalanceAmt";
            this.xrCashBalanceAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCashBalanceAmt.StylePriority.UseBackColor = false;
            this.xrCashBalanceAmt.StylePriority.UseBorderColor = false;
            this.xrCashBalanceAmt.StylePriority.UseBorders = false;
            this.xrCashBalanceAmt.StylePriority.UseFont = false;
            this.xrCashBalanceAmt.StylePriority.UsePadding = false;
            this.xrCashBalanceAmt.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.IgnoreNullValues = true;
            this.xrCashBalanceAmt.Summary = xrSummary2;
            this.xrCashBalanceAmt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrCashBalanceAmt.Weight = 0.32058246795732104D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell15.Weight = 0.31799311254250573D;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(300F, 25F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 1D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // NatureofPayments
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrtblGrandTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrCompany;
        private DevExpress.XtraReports.UI.XRTableCell xrNonCompany;
        private DevExpress.XtraReports.UI.XRTableCell xrTotalPending;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrtblGrandTotal;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrGrandTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrCashAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrCashBalanceAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private ReportSetting reportSetting1;

    }
}
