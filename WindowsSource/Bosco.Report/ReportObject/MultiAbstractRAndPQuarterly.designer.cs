﻿namespace Bosco.Report.ReportObject
{
    partial class MultiAbstractRAndPQuarterly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiAbstractRAndPQuarterly));
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.dtlReceipt = new DevExpress.XtraReports.UI.DetailBand();
            this.xrSubMultiAbstractReceiptQtly = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.dtlMulti = new DevExpress.XtraReports.UI.DetailReportBand();
            this.dtlPayment = new DevExpress.XtraReports.UI.DetailBand();
            this.xrSubMultiAbstractPaymentQtly = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
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
            this.xrSubMultiAbstractReceiptQtly,
            this.xrLabel1});
            resources.ApplyResources(this.dtlReceipt, "dtlReceipt");
            this.dtlReceipt.Name = "dtlReceipt";
            // 
            // xrSubMultiAbstractReceiptQtly
            // 
            resources.ApplyResources(this.xrSubMultiAbstractReceiptQtly, "xrSubMultiAbstractReceiptQtly");
            this.xrSubMultiAbstractReceiptQtly.Name = "xrSubMultiAbstractReceiptQtly";
            this.xrSubMultiAbstractReceiptQtly.ReportSource = new Bosco.Report.ReportObject.MultiAbstractReceiptsPaymentsQuarterly();
            // 
            // xrLabel1
            // 
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.StyleName = "styleTitleRow";
            // 
            // dtlMulti
            // 
            this.dtlMulti.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.dtlPayment});
            this.dtlMulti.Level = 0;
            this.dtlMulti.Name = "dtlMulti";
            this.dtlMulti.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            // 
            // dtlPayment
            // 
            this.dtlPayment.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubMultiAbstractPaymentQtly,
            this.xrLabel2});
            resources.ApplyResources(this.dtlPayment, "dtlPayment");
            this.dtlPayment.Name = "dtlPayment";
            // 
            // xrSubMultiAbstractPaymentQtly
            // 
            resources.ApplyResources(this.xrSubMultiAbstractPaymentQtly, "xrSubMultiAbstractPaymentQtly");
            this.xrSubMultiAbstractPaymentQtly.Name = "xrSubMultiAbstractPaymentQtly";
            this.xrSubMultiAbstractPaymentQtly.ReportSource = new Bosco.Report.ReportObject.MultiAbstractReceiptsPaymentsQuarterly();
            // 
            // xrLabel2
            // 
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.StyleName = "styleTitleRow";
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
            // MultiAbstractRAndPQuarterly
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
        private DevExpress.XtraReports.UI.XRSubreport xrSubMultiAbstractReceiptQtly;
        private DevExpress.XtraReports.UI.XRSubreport xrSubMultiAbstractPaymentQtly;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.DetailReportBand dtlMulti;
        private DevExpress.XtraReports.UI.DetailBand dtlPayment;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
    }
}
