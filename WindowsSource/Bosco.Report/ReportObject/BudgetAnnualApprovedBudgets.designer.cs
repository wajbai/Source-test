namespace Bosco.Report.ReportObject
{
    partial class BudgetAnnualApprovedBudgets
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BudgetAnnualApprovedBudgets));
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            this.grpHeaderIncomeLedgers = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrSubBudgetIncomeLedgers = new DevExpress.XtraReports.UI.XRSubreport();
            this.grpHeaderExpenseLedgers = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrSubBudgetExpenseLedgers = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // PageHeader
            // 
            this.PageHeader.Expanded = false;
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
            // grpHeaderIncomeLedgers
            // 
            this.grpHeaderIncomeLedgers.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubBudgetIncomeLedgers});
            resources.ApplyResources(this.grpHeaderIncomeLedgers, "grpHeaderIncomeLedgers");
            this.grpHeaderIncomeLedgers.Level = 1;
            this.grpHeaderIncomeLedgers.Name = "grpHeaderIncomeLedgers";
            // 
            // xrSubBudgetIncomeLedgers
            // 
            resources.ApplyResources(this.xrSubBudgetIncomeLedgers, "xrSubBudgetIncomeLedgers");
            this.xrSubBudgetIncomeLedgers.Name = "xrSubBudgetIncomeLedgers";
            this.xrSubBudgetIncomeLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetAnnualApprovedLedgers();
            // 
            // grpHeaderExpenseLedgers
            // 
            this.grpHeaderExpenseLedgers.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubBudgetExpenseLedgers});
            resources.ApplyResources(this.grpHeaderExpenseLedgers, "grpHeaderExpenseLedgers");
            this.grpHeaderExpenseLedgers.Name = "grpHeaderExpenseLedgers";
            // 
            // xrSubBudgetExpenseLedgers
            // 
            resources.ApplyResources(this.xrSubBudgetExpenseLedgers, "xrSubBudgetExpenseLedgers");
            this.xrSubBudgetExpenseLedgers.Name = "xrSubBudgetExpenseLedgers";
            this.xrSubBudgetExpenseLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetAnnualApprovedLedgers();
            // 
            // BudgetAnnualApprovedBudgets
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.grpHeaderIncomeLedgers,
            this.grpHeaderExpenseLedgers});
            this.DataMember = "BUDGETVARIANCE";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpHeaderExpenseLedgers, 0);
            this.Controls.SetChildIndex(this.grpHeaderIncomeLedgers, 0);
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
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderIncomeLedgers;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBudgetIncomeLedgers;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderExpenseLedgers;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBudgetExpenseLedgers;
    }
}
