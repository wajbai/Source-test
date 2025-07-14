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
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.ReportObject
{
    public partial class AppealMaillingNetwork : Bosco.Report.Base.ReportHeaderBase
    {

        #region VariableDeclaration
        #endregion

        #region Properties
        public AppealMaillingNetwork()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindAppealMailingSource();

        }
        #endregion

        #region Events

        #endregion

        #region Methods
        private void BindAppealMailingSource()
        {

            this.SetLandscapeHeader = 803.00f;
            this.SetLandscapeFooter = 803.00f;
            this.SetLandscapeFooterDateWidth = 600.50f;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            
            //string Date = !string.IsNullOrEmpty(this.ReportProperties.DateAsOn) ? this.ReportProperties.DateAsOn :
            //      UtilityMember.DateSet.ToDate(SettingProperty.Current.YearTo, false).ToShortDateString();
            //this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + Date;
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
                DataView dvAppealMailing = resultArgs.DataSource.TableView;
                if (dvAppealMailing != null)
                {
                    dvAppealMailing.Table.TableName = "Appeal";
                    this.DataSource = dvAppealMailing;
                    this.DataMember = dvAppealMailing.Table.TableName;
                }
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }
            SetReportBorder();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string AppealMailing = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.Appeal);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.ReportParameters.COMMUNICATION_MODEColumn, (int)CommunicationMode.MailDesk);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, AppealMailing);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xtbAppealMailingNetwork = AlignHeaderTable(xtbAppealMailingNetwork);
            xtblAppealMailingNetwork = AlignContentTable(xtblAppealMailingNetwork);
        }
        #endregion
    }
}
