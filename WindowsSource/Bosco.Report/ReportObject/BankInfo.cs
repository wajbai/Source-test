using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Report.Base;
namespace Bosco.Report.ReportObject
{
    public partial class BankInfo : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        int SerialNumber = 0;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public BankInfo()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindBankInfo();
        }
        #endregion

        #region Method
        private void BindBankInfo()
        {
            this.SetLandscapeHeader = 1125.25f;
            this.SetLandscapeFooter = 1125.25f;
            this.SetLandscapeFooterDateWidth = 960.00f;


            if (this.UIAppSetting.UICustomizationForm == "1")
            {
                if (ReportProperty.Current.ReportFlag == 0)
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));

                    // this.ReportTitle = ReportProperty.Current.ReportTitle;
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    //  this.ReportSubTitle = "Foreign Projects"; //ReportProperty.Current.ProjectTitle;
                    // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                    this.ReportPeriod = string.Empty;
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;


                    DataTable dtBankInfo = GetReportSource();

                    this.DataSource = dtBankInfo;
                    this.DataMember = dtBankInfo.TableName;
                    //}
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
                else
                {
                    SetReportTitle();
                    ShowReportFilterDialog();
                }
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));

                // this.ReportTitle = ReportProperty.Current.ReportTitle;
                setHeaderTitleAlignment();
                SetReportTitle();
                //  this.ReportSubTitle = "Foreign Projects"; //ReportProperty.Current.ProjectTitle;
                // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                this.ReportPeriod = string.Empty;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                DataTable dtBankInfo = GetReportSource();
                if (dtBankInfo != null)
                {
                    this.DataSource = dtBankInfo;
                    this.DataMember = dtBankInfo.TableName;
                }
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }

            SetReportBorder();
        }

        private DataTable GetReportSource()
        {
            string BankInfo = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.BankInfoDetails);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (this.ReportProperties.SocietyId != 0)
                    dataManager.Parameters.Add(this.ReportParameters.CUSTOMERIDColumn, this.ReportProperties.SocietyId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankInfo);
            }
            return resultArgs.DataSource.Table;
        }
        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblBankInfo = AlignContentTable(xrtblBankInfo);
        }

    }
        #endregion
}
