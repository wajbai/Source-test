using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class ThanksNotificationNetwork : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        #endregion

        #region Properties
        public ThanksNotificationNetwork()
        {
            InitializeComponent();
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindSource();

        }
        #endregion

        #region Methods
        private void BindSource()
        {

            this.SetLandscapeHeader = 1136.00f;
            this.SetLandscapeFooter = 1125.00f;
            this.SetLandscapeFooterDateWidth = 950.50f;
            
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            SetReportTitle();
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
            {
                ShowReportFilterDialog();
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                ResultArgs resultArgs = GetReportSource();
                DataView dvNotification = resultArgs.DataSource.TableView;
                if (dvNotification != null)
                {
                    dvNotification.Table.TableName = "Thanksgiving";
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
            string ThanksNotify = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.Thanksgiving);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add("STATUS", 1);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.COMMUNICATION_MODEColumn, (int)CommunicationMode.ContactDesk);
                if (this.ReportProperties.IncludeSent != 0 && this.ReportProperties.IncludeNotSent == 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_SENTColumn, this.ReportProperties.IncludeSent);// 1=Sent
                }
                else if (this.ReportProperties.IncludeSent == 0 && this.ReportProperties.IncludeNotSent != 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_SENTColumn, this.ReportProperties.IncludeNotSent = 0);// 1=Sent
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, ThanksNotify);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xtbThanksgivingNotification = AlignHeaderTable(xtbThanksgivingNotification);
            xtblThanksgivingNotification = AlignContentTable(xtblThanksgivingNotification);
            this.SetCurrencyFormat(xrAmount.Text, xrAmount);
        }
        #endregion

        #region Events

        #endregion
    }
}
