/// This method is used to generate Standard reports for all grid view.
/// It will load defualt or standard repot design called RPT-STD (will have only report tab in report criteria property)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Utility;

namespace Bosco.Report.View
{
    public partial class frmStandardReport : DevExpress.XtraEditors.XtraForm
    {
        public frmStandardReport()
        {
            InitializeComponent();
            
            //Set Legal entity
            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.FetchAll))
            {
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                if (resultArgs.Success)
                {
                    Bosco.Report.Base.ReportProperty.dtLedgerEntity = resultArgs.DataSource.Table;
                }
            }
        }

        public frmStandardReport(GridView gv, string standardreporttitle, GridView gvAddition = null, string AdditionalGridTitle = "")
            : this()
        {
            //InitializeComponent();
            Bosco.Report.Base.ReportProperty.Current.ReportId = "RPT-STD";
            Bosco.Report.Base.ReportProperty.Current.ProjectTitle = standardreporttitle;

            Bosco.Report.Base.ReportProperty.Current.GridViewStandardReport = gv;
            Bosco.Report.Base.ReportProperty.Current.AdditionalGridViewStandardReport = gvAddition;
            Bosco.Report.Base.ReportProperty.Current.AdditionText = AdditionalGridTitle;
            this.rptViewer.ReportId = "RPT-STD";
        }

        public frmStandardReport(Int32 BudgetId, string BudgetProjectIds, string budetname, GridView gv)
            : this()
        {
            //InitializeComponent();
            string projectids = "0";
            if (!string.IsNullOrEmpty(BudgetProjectIds))
            {
                projectids = BudgetProjectIds;
            }
            
            Bosco.Report.Base.ReportProperty.Current.ReportId = "RPT-152";
            Bosco.Report.Base.ReportProperty.Current.BudgetName = budetname;
            Bosco.Report.Base.ReportProperty.Current.BudgetId = BudgetId.ToString();
            Bosco.Report.Base.ReportProperty.Current.ProjectId = Bosco.Report.Base.ReportProperty.Current.Project = projectids;
            Bosco.Report.Base.ReportProperty.Current.SelectedProjectCount = string.IsNullOrEmpty(projectids) ? 1 : projectids.Split(',').Length;

            this.rptViewer.ReportId = "RPT-152";
            this.rptViewer.Select();
            this.rptViewer.Focus();
            this.BringToFront();
        }

        public frmStandardReport(Int32 BudgetId, string projectids, GridView gv, DateTime dtYearfrom, DateTime dtYearto, DateTime MonthFrom, DateTime MonthTo, string BudgetName)
            : this()
        {
            //InitializeComponent();
            Bosco.Report.Base.ReportProperty.Current.ReportId = "RPT-177";
            Bosco.Report.Base.ReportProperty.Current.ProjectId = projectids;
            Bosco.Report.Base.ReportProperty.Current.BudgetId = BudgetId.ToString();
            Bosco.Report.Base.ReportProperty.Current.DateFrom = dtYearfrom.ToString();
            Bosco.Report.Base.ReportProperty.Current.DateTo = dtYearto.ToString();
            Bosco.Report.Base.ReportProperty.Current.DateAsOn = MonthFrom.ToString();
            Bosco.Report.Base.ReportProperty.Current.AccounYear = MonthTo.ToString();
            Bosco.Report.Base.ReportProperty.Current.BudgetName = BudgetName;
            Bosco.Report.Base.ReportProperty.Current.SelectedProjectCount = string.IsNullOrEmpty(projectids) ? 1 : projectids.Split(',').Length;
            this.rptViewer.ReportId = "RPT-177";

            this.rptViewer.Select();
            this.rptViewer.Focus();
            this.BringToFront();
        }

        public frmStandardReport(int DeprId, DateTime dtfrom, DateTime dtTo, int proId, XtraForm xrtest)
            : this()
        {
            //InitializeComponent();
            Bosco.Report.Base.ReportProperty.Current.ReportId = "RPT-210";
            Bosco.Report.Base.ReportProperty.Current.ShowIndividualDepreciationLedgers = DeprId;
            Bosco.Report.Base.ReportProperty.Current.DateFrom = dtfrom.ToString();  //Convert.ToDateTime(dtfrom.ToString()).ToString("dd/MM/yyyy");
            Bosco.Report.Base.ReportProperty.Current.DateTo = dtTo.ToString();  // Convert.ToDateTime(dtTo.ToString()).ToString("dd/MM/yyyy");
            Bosco.Report.Base.ReportProperty.Current.ProjectId = proId.ToString();
            Bosco.Report.Base.ReportProperty.Current.SelectedProjectCount = 1;
            

            this.rptViewer.ReportId = "RPT-210";
            this.rptViewer.Select();
            this.rptViewer.Focus();
            this.BringToFront();
        }
        public frmStandardReport(string BudgetId, string ProjectId, bool MonthbyTwo, GridView gv, string datefrom, string dateto, string BudgetName, string ProjectName)
            : this()
        {
            //InitializeComponent();
            string RptId = "RPT-163";
            if (MonthbyTwo)
            {
                RptId = "RPT-180";
            }

            /*//Set Legal entity
            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.FetchAll))
            {
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                if (resultArgs.Success)
                {
                    Bosco.Report.Base.ReportProperty.dtLedgerEntity = resultArgs.DataSource.Table;
                }
            }*/

            Bosco.Report.Base.ReportProperty.Current.ReportId = RptId;
            Bosco.Report.Base.ReportProperty.Current.BudgetId = Bosco.Report.Base.ReportProperty.Current.Budget = BudgetId.ToString();
            Bosco.Report.Base.ReportProperty.Current.ProjectId = Bosco.Report.Base.ReportProperty.Current.Project = ProjectId;

            Bosco.Report.Base.ReportProperty.Current.DateFrom = datefrom.ToString();
            Bosco.Report.Base.ReportProperty.Current.DateTo = dateto.ToString();
            Bosco.Report.Base.ReportProperty.Current.BudgetName = BudgetName;
            Bosco.Report.Base.ReportProperty.Current.BudgetProject = ProjectName;
            Bosco.Report.Base.ReportProperty.Current.SelectedProjectCount = string.IsNullOrEmpty(ProjectId) ? 1 : ProjectId.Split(',').Length;

            Bosco.Report.Base.ReportProperty.Current.HeaderInstituteSocietyName = 1;
            Bosco.Report.Base.ReportProperty.Current.HeaderInstituteSocietyAddress = 1;
            Bosco.Report.Base.ReportProperty.Current.SaveReportSetting();

            this.rptViewer.FromViewScreen = true; //On 08/01/2020, set from view screen or report
            this.rptViewer.ReportId = RptId;

            this.rptViewer.Select();
            this.rptViewer.Focus();
            this.BringToFront();
        }
    }
}