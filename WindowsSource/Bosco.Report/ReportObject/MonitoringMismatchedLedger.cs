using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Report.View;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class MonitoringMismatchedLedger : Bosco.Report.Base.ReportHeaderBase
    {
        #region VairableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Property

        #endregion

        #region Constructor
        public MonitoringMismatchedLedger()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindMismatchedLedgers();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Bind the Source Controls
        /// </summary>
        private void BindMismatchedLedgers()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            BindSourceDetails();
            SplashScreenManager.CloseForm();
            base.ShowReport();

        }

        /// <summary>
        /// Bind Source
        /// </summary>
        public void BindSourceDetails()
        {
            DataTable dtMismatchedLedgers = new DataTable();
            SetReportTitle();

            this.SetLandscapeHeader = 1125.25f;
            this.SetLandscapeFooter = 1125.25f;
            this.SetLandscapeFooterDateWidth = 960.00f;

            SetReportBorder();

            ResultArgs resultArgs = GetReportSource();
            dtMismatchedLedgers = resultArgs.DataSource.Table;
            if (dtMismatchedLedgers != null)
            {
                this.DataSource = dtMismatchedLedgers;
                this.DataMember = dtMismatchedLedgers.TableName;
            }
        }

        /// <summary>
        /// Get source Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetReportSource()
        {
            string MismatchedLeders = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.MismatchedStatistics);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, MismatchedLeders);
            }
            return resultArgs;
        }

        /// <summary>
        /// Border verification
        /// </summary>
        private void SetReportBorder()
        {
            this.ReportPeriod = "";
        }
        #endregion
    }
}
