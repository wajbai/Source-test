namespace Bosco.Report.ReportObject
{
    partial class BudgetView
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
            this.grpBudgetExpenseLedgers = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrExpenseTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrSubBudgetExpenseLedgers = new DevExpress.XtraReports.UI.XRSubreport();
            this.GrpBudgetIncomeLedgers = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrIncomeTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrSubBudgetIncomeLedgers = new DevExpress.XtraReports.UI.XRSubreport();
            this.grpBudgetStatistics = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrSubBudgetStatistics = new DevExpress.XtraReports.UI.XRSubreport();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpBudgetDeveNewProject = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrSubBudgetDevelopmentalProjects = new DevExpress.XtraReports.UI.XRSubreport();
            this.grpSignDetails = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblSignFooter = new DevExpress.XtraReports.UI.XRTable();
            this.xrFooterRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellfooter = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrFooterRowSign = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellFooterSgin1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellFooterSgin2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.rptFooterSign = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblSignFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            this.Detail.HeightF = 30.29162F;
            // 
            // grpBudgetExpenseLedgers
            // 
            this.grpBudgetExpenseLedgers.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrExpenseTitle,
            this.xrSubBudgetExpenseLedgers});
            this.grpBudgetExpenseLedgers.HeightF = 47.99997F;
            this.grpBudgetExpenseLedgers.Level = 3;
            this.grpBudgetExpenseLedgers.Name = "grpBudgetExpenseLedgers";
            // 
            // xrExpenseTitle
            // 
            this.xrExpenseTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrExpenseTitle.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 1.999982F);
            this.xrExpenseTitle.Name = "xrExpenseTitle";
            this.xrExpenseTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrExpenseTitle.SizeF = new System.Drawing.SizeF(776F, 23F);
            this.xrExpenseTitle.StylePriority.UseFont = false;
            this.xrExpenseTitle.StylePriority.UseTextAlignment = false;
            this.xrExpenseTitle.Text = "EXPENDITURE";
            this.xrExpenseTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrSubBudgetExpenseLedgers
            // 
            this.xrSubBudgetExpenseLedgers.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 25F);
            this.xrSubBudgetExpenseLedgers.Name = "xrSubBudgetExpenseLedgers";
            this.xrSubBudgetExpenseLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetLedger();
            this.xrSubBudgetExpenseLedgers.SizeF = new System.Drawing.SizeF(776F, 22.99997F);
            // 
            // GrpBudgetIncomeLedgers
            // 
            this.GrpBudgetIncomeLedgers.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrIncomeTitle,
            this.xrSubBudgetIncomeLedgers});
            this.GrpBudgetIncomeLedgers.HeightF = 75.41669F;
            this.GrpBudgetIncomeLedgers.Level = 4;
            this.GrpBudgetIncomeLedgers.Name = "GrpBudgetIncomeLedgers";
            // 
            // xrIncomeTitle
            // 
            this.xrIncomeTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrIncomeTitle.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrIncomeTitle.Name = "xrIncomeTitle";
            this.xrIncomeTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrIncomeTitle.SizeF = new System.Drawing.SizeF(775.9999F, 23F);
            this.xrIncomeTitle.StylePriority.UseFont = false;
            this.xrIncomeTitle.StylePriority.UseTextAlignment = false;
            this.xrIncomeTitle.Text = "INCOME";
            this.xrIncomeTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrSubBudgetIncomeLedgers
            // 
            this.xrSubBudgetIncomeLedgers.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 25.99999F);
            this.xrSubBudgetIncomeLedgers.Name = "xrSubBudgetIncomeLedgers";
            this.xrSubBudgetIncomeLedgers.ReportSource = new Bosco.Report.ReportObject.BudgetLedger();
            this.xrSubBudgetIncomeLedgers.SizeF = new System.Drawing.SizeF(776F, 22.99997F);
            // 
            // grpBudgetStatistics
            // 
            this.grpBudgetStatistics.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubBudgetStatistics});
            this.grpBudgetStatistics.HeightF = 48.33336F;
            this.grpBudgetStatistics.Level = 5;
            this.grpBudgetStatistics.Name = "grpBudgetStatistics";
            // 
            // xrSubBudgetStatistics
            // 
            this.xrSubBudgetStatistics.LocationFloat = new DevExpress.Utils.PointFloat(1.999998F, 0F);
            this.xrSubBudgetStatistics.Name = "xrSubBudgetStatistics";
            this.xrSubBudgetStatistics.ReportSource = new Bosco.Report.ReportObject.BudgetStatistics();
            this.xrSubBudgetStatistics.SizeF = new System.Drawing.SizeF(775.9999F, 22.99997F);
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpBudgetDeveNewProject
            // 
            this.grpBudgetDeveNewProject.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubBudgetDevelopmentalProjects});
            this.grpBudgetDeveNewProject.HeightF = 31.68053F;
            this.grpBudgetDeveNewProject.KeepTogether = true;
            this.grpBudgetDeveNewProject.Level = 2;
            this.grpBudgetDeveNewProject.Name = "grpBudgetDeveNewProject";
            // 
            // xrSubBudgetDevelopmentalProjects
            // 
            this.xrSubBudgetDevelopmentalProjects.LocationFloat = new DevExpress.Utils.PointFloat(2F, 8.680561F);
            this.xrSubBudgetDevelopmentalProjects.Name = "xrSubBudgetDevelopmentalProjects";
            this.xrSubBudgetDevelopmentalProjects.ReportSource = new Bosco.Report.ReportObject.BudgetDevelopmentalProjectDetails();
            this.xrSubBudgetDevelopmentalProjects.SizeF = new System.Drawing.SizeF(776F, 22.99997F);
            // 
            // grpSignDetails
            // 
            this.grpSignDetails.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblSignFooter});
            this.grpSignDetails.HeightF = 75F;
            this.grpSignDetails.Level = 1;
            this.grpSignDetails.Name = "grpSignDetails";
            // 
            // xrTblSignFooter
            // 
            this.xrTblSignFooter.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTblSignFooter.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblSignFooter.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTblSignFooter.LocationFloat = new DevExpress.Utils.PointFloat(2.000001F, 0F);
            this.xrTblSignFooter.Name = "xrTblSignFooter";
            this.xrTblSignFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblSignFooter.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrFooterRow3,
            this.xrFooterRowSign});
            this.xrTblSignFooter.SizeF = new System.Drawing.SizeF(775.9998F, 75F);
            this.xrTblSignFooter.StyleName = "styleRow";
            this.xrTblSignFooter.StylePriority.UseBorderColor = false;
            this.xrTblSignFooter.StylePriority.UseBorders = false;
            this.xrTblSignFooter.StylePriority.UseFont = false;
            this.xrTblSignFooter.StylePriority.UsePadding = false;
            this.xrTblSignFooter.StylePriority.UseTextAlignment = false;
            this.xrTblSignFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrFooterRow3
            // 
            this.xrFooterRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellfooter});
            this.xrFooterRow3.Name = "xrFooterRow3";
            this.xrFooterRow3.Visible = false;
            this.xrFooterRow3.Weight = 2D;
            // 
            // xrcellfooter
            // 
            this.xrcellfooter.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrcellfooter.Multiline = true;
            this.xrcellfooter.Name = "xrcellfooter";
            this.xrcellfooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 3, 0, 0, 100F);
            this.xrcellfooter.StylePriority.UseBorders = false;
            this.xrcellfooter.StylePriority.UsePadding = false;
            this.xrcellfooter.StylePriority.UseTextAlignment = false;
            this.xrcellfooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrcellfooter.Weight = 3.30762567978252D;
            // 
            // xrFooterRowSign
            // 
            this.xrFooterRowSign.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellFooterSgin1,
            this.xrCellFooterSgin2});
            this.xrFooterRowSign.Name = "xrFooterRowSign";
            this.xrFooterRowSign.Visible = false;
            this.xrFooterRowSign.Weight = 1D;
            // 
            // xrCellFooterSgin1
            // 
            this.xrCellFooterSgin1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCellFooterSgin1.Name = "xrCellFooterSgin1";
            this.xrCellFooterSgin1.StylePriority.UseFont = false;
            this.xrCellFooterSgin1.StylePriority.UseTextAlignment = false;
            this.xrCellFooterSgin1.Text = "Sign1";
            this.xrCellFooterSgin1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellFooterSgin1.Weight = 1.6096285188226931D;
            // 
            // xrCellFooterSgin2
            // 
            this.xrCellFooterSgin2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCellFooterSgin2.Name = "xrCellFooterSgin2";
            this.xrCellFooterSgin2.StylePriority.UseFont = false;
            this.xrCellFooterSgin2.StylePriority.UseTextAlignment = false;
            this.xrCellFooterSgin2.Text = "Signature of the House Council Members";
            this.xrCellFooterSgin2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellFooterSgin2.Weight = 1.6979971609598268D;
            // 
            // rptFooterSign
            // 
            this.rptFooterSign.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubSignFooter});
            this.rptFooterSign.HeightF = 63.97221F;
            this.rptFooterSign.Name = "rptFooterSign";
            this.rptFooterSign.Visible = false;
            // 
            // xrSubSignFooter
            // 
            this.xrSubSignFooter.LocationFloat = new DevExpress.Utils.PointFloat(1.999987F, 8.680546F);
            this.xrSubSignFooter.Name = "xrSubSignFooter";
            this.xrSubSignFooter.ReportSource = new Bosco.Report.ReportObject.SignReportFooter();
            this.xrSubSignFooter.SizeF = new System.Drawing.SizeF(775.9998F, 55.29166F);
            // 
            // BudgetView
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpBudgetExpenseLedgers,
            this.GrpBudgetIncomeLedgers,
            this.grpBudgetStatistics,
            this.grpBudgetDeveNewProject,
            this.grpSignDetails,
            this.rptFooterSign});
            this.DataMember = "BUDGET_STATISTICS";
            this.DataSource = this.reportSetting1;
            this.Margins = new System.Drawing.Printing.Margins(40, 3, 20, 20);
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.rptFooterSign, 0);
            this.Controls.SetChildIndex(this.grpSignDetails, 0);
            this.Controls.SetChildIndex(this.grpBudgetDeveNewProject, 0);
            this.Controls.SetChildIndex(this.grpBudgetStatistics, 0);
            this.Controls.SetChildIndex(this.GrpBudgetIncomeLedgers, 0);
            this.Controls.SetChildIndex(this.grpBudgetExpenseLedgers, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblSignFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand grpBudgetExpenseLedgers;
        private DevExpress.XtraReports.UI.GroupHeaderBand GrpBudgetIncomeLedgers;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBudgetStatistics;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBudgetExpenseLedgers;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBudgetIncomeLedgers;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBudgetStatistics;
        private DevExpress.XtraReports.UI.XRLabel xrExpenseTitle;
        private DevExpress.XtraReports.UI.XRLabel xrIncomeTitle;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpBudgetDeveNewProject;
        private DevExpress.XtraReports.UI.XRSubreport xrSubBudgetDevelopmentalProjects;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpSignDetails;
        private DevExpress.XtraReports.UI.XRTable xrTblSignFooter;
        private DevExpress.XtraReports.UI.XRTableRow xrFooterRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrcellfooter;
        private DevExpress.XtraReports.UI.XRTableRow xrFooterRowSign;
        private DevExpress.XtraReports.UI.XRTableCell xrCellFooterSgin1;
        private DevExpress.XtraReports.UI.XRTableCell xrCellFooterSgin2;
        private DevExpress.XtraReports.UI.GroupHeaderBand rptFooterSign;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
    }
}
