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
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class NetworkDonorTrackingSheet : ReportHeaderBase
    {
        public NetworkDonorTrackingSheet()
        {
            InitializeComponent();
        }
        public override void ShowReport()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.StateDonaud)
                 || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.DonarName))
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
            this.SetLandscapeHeader = 761.00f;
            this.SetLandscapeFooter = 761.00f;
            this.SetLandscapeFooterDateWidth = 600.00f;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            
            ResultArgs resultArgs = GetReportSource();
            DataView dvNetworkingTackinSheet = resultArgs.DataSource.TableView;
            if (dvNetworkingTackinSheet != null)
            {
                dvNetworkingTackinSheet.Table.TableName = "NetworkDonorTrackingSheet";
                this.DataSource = dvNetworkingTackinSheet;
                this.DataMember = dvNetworkingTackinSheet.Table.TableName;
            }
            SetReportTitle();
            SetReportBorder();
        }
        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string NetworkingTrackingSheet = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.TrackingSheet);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, (string.IsNullOrEmpty(this.ReportProperties.DateFrom) ? "0" : this.ReportProperties.DateFrom));
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, (string.IsNullOrEmpty(this.ReportProperties.DateTo) ? "0" : this.ReportProperties.DateTo));
                dataManager.Parameters.Add(this.ReportParameters.DONAUD_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.DonarName) ? "0" : this.ReportProperties.DonarName));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, NetworkingTrackingSheet);
            }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrtblDonorTrackingSheet = AlignHeaderTable(xrtblDonorTrackingSheet);
            xrtTrackingSheet = AlignContentTable(xrtTrackingSheet);

            this.SetCurrencyFormat(xrAmount.Text, xrAmount);
        }
    }
}
