namespace Bosco.Report.ReportObject
{
    partial class MultiAbstractRPCurrencyView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiAbstractRPCurrencyView));
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.dtlReceipt = new DevExpress.XtraReports.UI.DetailBand();
            this.xrSubMultiAbstractReceipt = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLblIncomeTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.dtlMulti = new DevExpress.XtraReports.UI.DetailReportBand();
            this.dtlPayment = new DevExpress.XtraReports.UI.DetailBand();
            this.xrSubMultiAbstractPayment = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrlblExpenditureTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.dtlReceipt,
            this.dtlMulti});
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // dtlReceipt
            // 
            this.dtlReceipt.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubMultiAbstractReceipt,
            this.xrLblIncomeTitle});
            resources.ApplyResources(this.dtlReceipt, "dtlReceipt");
            this.dtlReceipt.Name = "dtlReceipt";
            // 
            // xrSubMultiAbstractReceipt
            // 
            resources.ApplyResources(this.xrSubMultiAbstractReceipt, "xrSubMultiAbstractReceipt");
            this.xrSubMultiAbstractReceipt.Name = "xrSubMultiAbstractReceipt";
            this.xrSubMultiAbstractReceipt.ReportSource = new Bosco.Report.ReportObject.MultiAbstractRPCurrency();
            // 
            // xrLblIncomeTitle
            // 
            resources.ApplyResources(this.xrLblIncomeTitle, "xrLblIncomeTitle");
            this.xrLblIncomeTitle.Name = "xrLblIncomeTitle";
            this.xrLblIncomeTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblIncomeTitle.StyleName = "styleTitleRow";
            // 
            // dtlMulti
            // 
            this.dtlMulti.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.dtlPayment});
            this.dtlMulti.Level = 0;
            this.dtlMulti.Name = "dtlMulti";
            // 
            // dtlPayment
            // 
            this.dtlPayment.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubMultiAbstractPayment,
            this.xrlblExpenditureTitle});
            resources.ApplyResources(this.dtlPayment, "dtlPayment");
            this.dtlPayment.Name = "dtlPayment";
            // 
            // xrSubMultiAbstractPayment
            // 
            resources.ApplyResources(this.xrSubMultiAbstractPayment, "xrSubMultiAbstractPayment");
            this.xrSubMultiAbstractPayment.Name = "xrSubMultiAbstractPayment";
            this.xrSubMultiAbstractPayment.ReportSource = new Bosco.Report.ReportObject.MultiAbstractRPCurrency();
            // 
            // xrlblExpenditureTitle
            // 
            resources.ApplyResources(this.xrlblExpenditureTitle, "xrlblExpenditureTitle");
            this.xrlblExpenditureTitle.Name = "xrlblExpenditureTitle";
            this.xrlblExpenditureTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblExpenditureTitle.StyleName = "styleTitleRow";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubSignFooter});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrSubSignFooter
            // 
            resources.ApplyResources(this.xrSubSignFooter, "xrSubSignFooter");
            this.xrSubSignFooter.Name = "xrSubSignFooter";
            this.xrSubSignFooter.ReportSource = new Bosco.Report.ReportObject.SignReportFooter();
            // 
            // MultiAbstractRPCurrencyView
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.DetailReport,
            this.ReportFooter});
            resources.ApplyResources(this, "$this");
            this.ShowPrintMarginsWarning = false;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.DetailReport, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand dtlReceipt;
        private DevExpress.XtraReports.UI.XRSubreport xrSubMultiAbstractReceipt;
        private DevExpress.XtraReports.UI.XRSubreport xrSubMultiAbstractPayment;
        private DevExpress.XtraReports.UI.XRLabel xrLblIncomeTitle;
        private DevExpress.XtraReports.UI.XRLabel xrlblExpenditureTitle;
        private DevExpress.XtraReports.UI.DetailReportBand dtlMulti;
        private DevExpress.XtraReports.UI.DetailBand dtlPayment;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
    }
}
