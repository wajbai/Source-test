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
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class TrackingSheet : Bosco.Report.Base.ReportHeaderBase
    {
        public TrackingSheet()
        {
            InitializeComponent();
        }
        public override void ShowReport()
        {
            SetReportTitle();
            if  (String.IsNullOrEmpty(this.ReportProperties.DateAsOn)
              || this.ReportProperties.Project == "0")
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
                        BindTrackingSheetDetails();
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
                    BindTrackingSheetDetails();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }
        public void BindTrackingSheetDetails()
        {
            this.SetLandscapeHeader = 802.00f;
            this.SetLandscapeFooter = 802.00f;
            this.SetLandscapeFooterDateWidth = 400.00f;
            ResultArgs resultArgs = GetReportSource();
            DataView dvLabelPrint = resultArgs.DataSource.TableView;
            if (dvLabelPrint != null)
            {
                dvLabelPrint.Table.TableName = "LabelPrint";
                this.DataSource = dvLabelPrint;
                this.DataMember = dvLabelPrint.Table.TableName;
            }
            SetReportBorder();
        }
        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string ProspectIndividual = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.TrackingSheet);

            using (DataManager dataManager = new DataManager())
            {
                // dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.REGISTRATION_TYPE_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.RegistrationTypeId) ? "0" : this.ReportProperties.RegistrationTypeId));
                dataManager.Parameters.Add(this.ReportParameters.COUNTRY_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.CountryId) ? "0" : this.ReportProperties.CountryId));
                dataManager.Parameters.Add(this.ReportParameters.STATE_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.StateId) ? "0" : this.ReportProperties.StateId));
                dataManager.Parameters.Add(this.ReportParameters.LANGUAGEColumn, (string.IsNullOrEmpty(this.ReportProperties.Language) ? "0" : this.ReportProperties.Language));
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, ProspectIndividual);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xrtblTrackingSheet = AlignHeaderTable(xrtblTrackingSheet);
            xrtTrackingSheet = AlignContentTable(xrtTrackingSheet);
        }
    }
}
