using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class ProspectIndividual : Bosco.Report.Base.ReportHeaderBase
    {
        public ProspectIndividual()
        {
            InitializeComponent();
        }
        public override void ShowReport()
        {
            SetReportTitle();
            if (string.IsNullOrEmpty(this.ReportProperties.RegistrationTypeId) || string.IsNullOrEmpty(this.ReportProperties.CountryId)
                || string.IsNullOrEmpty(this.ReportProperties.StateId) || string.IsNullOrEmpty(this.ReportProperties.DateAsOn))
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
            this.SetLandscapeFooterDateWidth = 1065.00f;
            ResultArgs resultArgs = GetReportSource();
            DataView dvProspectIndividual = resultArgs.DataSource.TableView;
            if (dvProspectIndividual != null)
            {
                dvProspectIndividual.Table.TableName = "ProspectIndividual";
                this.DataSource = dvProspectIndividual;
                this.DataMember = dvProspectIndividual.Table.TableName;
            }
            SetReportBorder();
        }
        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string ProspectIndividual = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.ProspectIndividual);

            using (DataManager dataManager = new DataManager())
            {

               // dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.REGISTRATION_TYPE_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.RegistrationTypeId) ? "0" : this.ReportProperties.RegistrationTypeId));
                dataManager.Parameters.Add(this.ReportParameters.COUNTRY_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.CountryId) ? "0" : this.ReportProperties.CountryId));
                dataManager.Parameters.Add(this.ReportParameters.STATE_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.StateId) ? "0" : this.ReportProperties.StateId));
                if (!string.IsNullOrEmpty(this.ReportProperties.Language))
                {
                    dataManager.Parameters.Add(this.ReportParameters.LANGUAGEColumn, this.ReportProperties.Language);
                }
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_MALEColumn, (this.ReportProperties.IncludeMale == 0 ? 0 : this.ReportProperties.IncludeMale));
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_FEMALEColumn, (this.ReportProperties.IncludeFemale == 0 ? 0 : this.ReportProperties.IncludeFemale));
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_INSTITUTIONALColumn, (this.ReportProperties.IncludeInstitutional == 0 ? 0 : this.ReportProperties.IncludeInstitutional));
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_INDIVIDUALColumn, (this.ReportProperties.IncludeIndividual == 0 ? 0 : this.ReportProperties.IncludeIndividual));
                
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, ProspectIndividual);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xrtblProspectIndividual = AlignHeaderTable(xrtblProspectIndividual);
            xrtProspectIndividual = AlignContentTable(xrtProspectIndividual);
        }
    }
}
