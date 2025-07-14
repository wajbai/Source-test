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
    public partial class FeastDayNotificationNetwork : Bosco.Report.Base.ReportHeaderBase
    {

        #region VariableDeclaration
        #endregion

        #region Properties
        public FeastDayNotificationNetwork()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFeastDayNotificationSource();

        }
        #endregion

        #region Events

        #endregion

        #region Methods
        private void BindFeastDayNotificationSource()
        {
            this.SetLandscapeHeader = 1127.00f;
            this.SetLandscapeFooter = 1127.00F;
            this.SetLandscapeFooterDateWidth = 925.50f;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            SetReportTitle();
            this.ReportPeriod = "";
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            ResultArgs resultArgs = GetReportSource();
            DataView dvFeastDay = resultArgs.DataSource.TableView;
            if (dvFeastDay != null)
            {
                dvFeastDay.Table.TableName = "FeastDay";
                this.DataSource = dvFeastDay;
                this.DataMember = dvFeastDay.Table.TableName;
            }
            SplashScreenManager.CloseForm();
            base.ShowReport();
            SetReportBorder();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string FeastMailing = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.FeastDay);

            using (DataManager dataManager = new DataManager())
            {
                if (this.ReportProperties.TaskID != 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.TAG_IDColumn, this.ReportProperties.TaskID);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.TAG_IDColumn, '0');
                }
                dataManager.Parameters.Add(this.ReportParameters.REGISTRATION_TYPE_IDColumn, (int)CommunicationMode.ContactDesk);
                if (this.ReportProperties.IncludeSent != 0 && this.ReportProperties.IncludeNotSent == 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_SENTColumn, this.ReportProperties.IncludeSent);
                }
                else if (this.ReportProperties.IncludeSent == 0 && this.ReportProperties.IncludeNotSent != 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_SENTColumn, this.ReportProperties.IncludeNotSent = 0);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FeastMailing);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xtbFeastDayNetwork = AlignHeaderTable(xtbFeastDayNetwork);
            xtblFeastDayNetwork = AlignContentTable(xtblFeastDayNetwork);
        }
        #endregion
    }
}
