namespace Bosco.Report.ReportObject
{
    partial class CashBankReceiptsBase
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
            this.xrSubOrginal = new DevExpress.XtraReports.UI.XRSubreport();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
            this.xrdotline = new DevExpress.XtraReports.UI.XRLine();
            this.xrSubDuplicate = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.StylePriority.UseTextAlignment = false;
            // 
            // xrSubOrginal
            // 
            this.xrSubOrginal.LocationFloat = new DevExpress.Utils.PointFloat(2.000086F, 0F);
            this.xrSubOrginal.Name = "xrSubOrginal";
            this.xrSubOrginal.ReportSource = new Bosco.Report.ReportObject.CashBankReceipts();
            this.xrSubOrginal.SizeF = new System.Drawing.SizeF(746.9999F, 23F);
            this.xrSubOrginal.AfterPrint += new System.EventHandler(this.xrSubOrginal_AfterPrint);
            
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak1,
            this.xrdotline,
            this.xrSubOrginal,
            this.xrSubDuplicate});
            this.GroupHeader1.HeightF = 90.91666F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrPageBreak1
            // 
            this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 58F);
            this.xrPageBreak1.Name = "xrPageBreak1";
            this.xrPageBreak1.Visible = false;
            // 
            // xrdotline
            // 
            this.xrdotline.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrdotline.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.xrdotline.LocationFloat = new DevExpress.Utils.PointFloat(0.2083335F, 42F);
            this.xrdotline.Name = "xrdotline";
            this.xrdotline.SizeF = new System.Drawing.SizeF(746.7916F, 2.083332F);
            this.xrdotline.StylePriority.UseBorderDashStyle = false;
            // 
            // xrSubDuplicate
            // 
            this.xrSubDuplicate.LocationFloat = new DevExpress.Utils.PointFloat(0.2083302F, 67.91666F);
            this.xrSubDuplicate.Name = "xrSubDuplicate";
            this.xrSubDuplicate.ReportSource = new Bosco.Report.ReportObject.CashBankReceipts();
            this.xrSubDuplicate.SizeF = new System.Drawing.SizeF(746.9999F, 23F);
            // 
            // CashBankReceiptsBase
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1});
            this.Margins = new System.Drawing.Printing.Margins(49, 29, 20, 20);
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRSubreport xrSubOrginal;
        private DevExpress.XtraReports.UI.XRSubreport xrSubDuplicate;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRLine xrdotline;
        private DevExpress.XtraReports.UI.XRPageBreak xrPageBreak1;
    }
}
