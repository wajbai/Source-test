namespace Bosco.Report.ReportObject
{
    partial class BudgetAnnualQuarterlyRealization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BudgetAnnualQuarterlyRealization));
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            this.grpIncomeLedgers = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrSubQtlyBudgetIncomeLedgers = new DevExpress.XtraReports.UI.XRSubreport();
            this.grpExpenseLedgers = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrSubQtlyBudgetExpenseLedgers = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // PageHeader
            // 
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // grpIncomeLedgers
            // 
            this.grpIncomeLedgers.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubQtlyBudgetIncomeLedgers});
            resources.ApplyResources(this.grpIncomeLedgers, "grpIncomeLedgers");
            this.grpIncomeLedgers.Level = 1;
            this.grpIncomeLedgers.Name = "grpIncomeLedgers";
            // 
            // xrSubQtlyBudgetIncomeLedgers
            // 
            resources.ApplyResources(this.xrSubQtlyBudgetIncomeLedgers, "xrSubQtlyBudgetIncomeLedgers");
            this.xrSubQtlyBudgetIncomeLedgers.Name = "xrSubQtlyBudgetIncomeLedgers";
            this.xrSubQtlyBudgetIncomeLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetAnnualQtlyRealizationLedgers();
            // 
            // grpExpenseLedgers
            // 
            this.grpExpenseLedgers.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubQtlyBudgetExpenseLedgers});
            resources.ApplyResources(this.grpExpenseLedgers, "grpExpenseLedgers");
            this.grpExpenseLedgers.Name = "grpExpenseLedgers";
            // 
            // xrSubQtlyBudgetExpenseLedgers
            // 
            resources.ApplyResources(this.xrSubQtlyBudgetExpenseLedgers, "xrSubQtlyBudgetExpenseLedgers");
            this.xrSubQtlyBudgetExpenseLedgers.Name = "xrSubQtlyBudgetExpenseLedgers";
            this.xrSubQtlyBudgetExpenseLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetAnnualQtlyRealizationLedgers();
            // 
            // BudgetAnnualQuarterlyRealization
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grpIncomeLedgers,
            this.grpExpenseLedgers});
            this.DataMember = "BUDGETVARIANCE";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpExpenseLedgers, 0);
            this.Controls.SetChildIndex(this.grpIncomeLedgers, 0);
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpIncomeLedgers;
        private DevExpress.XtraReports.UI.XRSubreport xrSubQtlyBudgetIncomeLedgers;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpExpenseLedgers;
        private DevExpress.XtraReports.UI.XRSubreport xrSubQtlyBudgetExpenseLedgers;
    }
}
