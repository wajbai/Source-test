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
    public partial class FeastDayMaillingNetwork : Bosco.Report.Base.ReportHeaderBase
    {

        #region VariableDeclaration
        #endregion

        #region Properties

        public FeastDayMaillingNetwork()
        {
            InitializeComponent();
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFeastDayMailingSource();

        }
        #endregion

        #region Events

        #endregion

        #region Methods
        /// <summary>
        /// Bind the feast details
        /// </summary>
        private void BindFeastDayMailingSource()
        {
            this.SetLandscapeHeader = 1140.00f;
            this.SetLandscapeFooter = 1140.00f;
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

        /// <summary>
        /// Get the Source 
        /// </summary>
        /// <returns></returns>
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
                dataManager.Parameters.Add(this.ReportParameters.REGISTRATION_TYPE_IDColumn, (int)CommunicationMode.MailDesk);

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

        /// <summary>
        /// Set the Border 
        /// </summary>
        private void SetReportBorder()
        {
            xtbFeastDayMailingNetwork = AlignHeaderTable(xtbFeastDayMailingNetwork);
            xtblFeastDayMailingNetwork = AlignContentTable(xtblFeastDayMailingNetwork);
        }
        #endregion
    }
}
