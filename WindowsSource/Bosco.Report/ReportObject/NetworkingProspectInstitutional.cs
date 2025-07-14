using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class NetworkingProspectInstitutional : Bosco.Report.Base.ReportHeaderBase
    {
        public NetworkingProspectInstitutional()
        {
            InitializeComponent();
        }
        public override void ShowReport()
        {
            SetReportTitle();
            if (string.IsNullOrEmpty(this.ReportProperties.RegistrationTypeId) || string.IsNullOrEmpty(this.ReportProperties.CountryId)
                 || string.IsNullOrEmpty(this.ReportProperties.StateId) )
            {
                SetReportTitle();
                ShowReportFilterDialog();
                SetReportBorder();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindPropsectInstitutionalDetails();
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                        SetReportBorder();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    BindPropsectInstitutionalDetails();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }
        public void BindPropsectInstitutionalDetails()
        {
            this.SetLandscapeHeader = 1125.00f;
            this.SetLandscapeFooter = 1125.00f;
            this.SetLandscapeFooterDateWidth = 930.00f;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            ResultArgs resultArgs = GetReportSource();
            DataView dvProspectInstitutional = resultArgs.DataSource.TableView;
            if (dvProspectInstitutional != null)
            {
                dvProspectInstitutional.Table.TableName = "ProspectInstitutional";
                this.DataSource = dvProspectInstitutional;
                this.DataMember = dvProspectInstitutional.Table.TableName;
            }
            SetReportBorder();
        }
        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string ProspectInstitutional = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.ProspectInstitutional);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.REGISTRATION_TYPE_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.RegistrationTypeId) ? "0" : this.ReportProperties.RegistrationTypeId));
                dataManager.Parameters.Add(this.ReportParameters.COUNTRY_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.CountryId) ? "0" : this.ReportProperties.CountryId));
                dataManager.Parameters.Add(this.ReportParameters.STATE_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.StateId) ? "0" : this.ReportProperties.StateId));

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, ProspectInstitutional);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xrtblProspectInstitutional = AlignHeaderTable(xrtblProspectInstitutional);
            xrtProspectInstitutional = AlignContentTable(xrtProspectInstitutional);
        }
    }
}
