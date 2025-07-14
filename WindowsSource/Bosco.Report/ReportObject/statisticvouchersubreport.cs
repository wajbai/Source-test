using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Bosco.DAO.Data;

using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using Bosco.DAO.Data;


namespace Bosco.Report.ReportObject
{
    public partial class statisticvouchersubreport : Bosco.Report.Base.ReportBase
    {
        ResultArgs resultArgs = null;

        public int sum { get; set; }

        public statisticvouchersubreport()
        {
            InitializeComponent();
        }

        public void BindStaticsDetails()
        {
            DataTable dtFARegister = GetReportSource();
            if (dtFARegister != null && dtFARegister.Rows.Count > 0)
            {
                this.DataSource = dtFARegister;
                this.DataMember = dtFARegister.TableName;
                sum = UtilityMember.NumberSet.ToInteger(dtFARegister.Compute("SUM(VOUCHER_COUNT)", "").ToString());
            }
            SplashScreenManager.CloseForm();
            base.ShowReport();
        }

        private DataTable GetReportSource()
        {
            string AssetInsurance = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.StatisticalSubReport);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, AssetInsurance);
            }
            return resultArgs.DataSource.Table;
        }
    }
}
;