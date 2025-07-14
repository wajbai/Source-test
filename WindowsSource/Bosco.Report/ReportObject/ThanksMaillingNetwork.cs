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
    public partial class ThanksMaillingNetwork : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        #endregion

        #region Properties
        public ThanksMaillingNetwork()
        {
            InitializeComponent();
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindThanksMailingSource();

        }
        #endregion

        #region Events

        #endregion

        #region Methods
        private void BindThanksMailingSource()
        {

            this.SetLandscapeHeader = 1142.00f;
            this.SetLandscapeFooter = 1142.00f;
            this.SetLandscapeFooterDateWidth = 950.50f;
            
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            SetReportTitle();
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
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
            string ThanksMailing = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.Thanksgiving);

            using (DataManager dataManager = new DataManager())
            {
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
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, ThanksMailing);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xtbThanksMailingNetwork = AlignHeaderTable(xtbThanksMailingNetwork);
            xtblThanksMailingNetwork = AlignContentTable(xtblThanksMailingNetwork);

            this.SetCurrencyFormat(xrAmount.Text, xrAmount);
        }
        #endregion
    }
}
