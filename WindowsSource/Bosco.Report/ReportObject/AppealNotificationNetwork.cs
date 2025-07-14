using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class AppealNotificationNetwork : Bosco.Report.Base.ReportHeaderBase
    {

        #region VariableDeclaration
        #endregion

        #region Properties
        public AppealNotificationNetwork()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindAppealNotificationSource();

        }
        #endregion

        #region Events

        #endregion

        #region Methods
        private void BindAppealNotificationSource()
        {

            this.SetLandscapeHeader = 803.00f;
            this.SetLandscapeFooter = 803.00f;
            this.SetLandscapeFooterDateWidth = 600.50f;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            
            SetReportTitle();
            this.ReportPeriod = String.Format("For the Period: {0}", this.ReportProperties.DateAsOn);
            if (string.IsNullOrEmpty(this.ReportProperties.DateAsOn))
            {
                ShowReportFilterDialog();
                SetReportBorder();
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                ResultArgs resultArgs = GetReportSource();
                DataView dvAppealNotification = resultArgs.DataSource.TableView;
                if (dvAppealNotification != null)
                {
                    dvAppealNotification.Table.TableName = "Appeal";
                    this.DataSource = dvAppealNotification;
                    this.DataMember = dvAppealNotification.Table.TableName;
                }
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }
            SetReportBorder();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string AppealNotification = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.Appeal);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.ReportParameters.COMMUNICATION_MODEColumn, (int)CommunicationMode.ContactDesk);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, AppealNotification);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xtbAppealNotificationNetwork = AlignHeaderTable(xtbAppealNotificationNetwork);
            xtblAppealNotificationNetwork = AlignContentTable(xtblAppealNotificationNetwork);
        }
        #endregion
    }
}
