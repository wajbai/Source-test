using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class AnniversaryNotificationNetwork : Bosco.Report.Base.ReportHeaderBase
    {

        #region VariableDeclaration
        #endregion

        #region Properties
        public AnniversaryNotificationNetwork()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindAnniversaryMailingSource();

        }
        #endregion

        #region Events

        #endregion

        #region Methods
        private void BindAnniversaryMailingSource()
        {

            this.SetLandscapeHeader = 1125.00f;
            this.SetLandscapeFooter = 1125.00f;
            this.SetLandscapeFooterDateWidth = 925.50f;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            SetReportTitle();
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                ShowReportFilterDialog();
                SetReportBorder();
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                ResultArgs resultArgs = GetReportSource();
                DataView dvNotification = resultArgs.DataSource.TableView;
                if (dvNotification != null)
                {
                    dvNotification.Table.TableName = "Anniversaries";
                    this.DataSource = dvNotification;
                    this.DataMember = dvNotification.Table.TableName;
                }
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }
            SetReportBorder();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string Anniversary = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.AnniversariesSMS);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.ANNIVERSARY_TYPEColumn, this.ReportProperties.AnniversaryType);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.COMMUNICATION_MODEColumn, (int)CommunicationMode.ContactDesk);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Anniversary);
            }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xtbAnniversaryNotificationNetwork = AlignHeaderTable(xtbAnniversaryNotificationNetwork);
            xtblAnniversaryNotificationNetwork = AlignContentTable(xtblAnniversaryNotificationNetwork);
        }
        #endregion
    }
}
