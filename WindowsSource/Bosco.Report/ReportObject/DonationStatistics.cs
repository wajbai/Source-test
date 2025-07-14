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
    public partial class DonationStatistics : Bosco.Report.Base.ReportHeaderBase
    {
        public DonationStatistics()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom)
                 || string.IsNullOrEmpty(this.ReportProperties.DateTo))
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
            //this.SetLandscapeHeader = 761.00f;
            //this.SetLandscapeFooter = 761.00f;
            //this.SetLandscapeFooterDateWidth = 600.00f;

            this.SetLandscapeHeader = 1125.25f;
            this.SetLandscapeFooter = 1125.25f;
            this.SetLandscapeFooterDateWidth = 860.00f;

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            
            ResultArgs resultArgs = GetReportSource();
            DataView dvNetworkingTackinSheet = resultArgs.DataSource.TableView;
            if (dvNetworkingTackinSheet != null)
            {
                dvNetworkingTackinSheet.Table.TableName = "DonationStatistics";
                this.DataSource = dvNetworkingTackinSheet;
                this.DataMember = dvNetworkingTackinSheet.Table.TableName;
            }
            SetReportTitle();
            SetReportBorder();
        }
        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string NetworkingTrackingSheet = this.GetNetWorkingReports(SQL.ReportSQLCommand.NetWorking.DonationStatistics);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, (string.IsNullOrEmpty(this.ReportProperties.DateFrom) ? "0" : this.ReportProperties.DateFrom));
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, (string.IsNullOrEmpty(this.ReportProperties.DateTo) ? "0" : this.ReportProperties.DateTo));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, NetworkingTrackingSheet);
            }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrTable1 = AlignHeaderTable(xrTable1);
            xrTable2 = AlignContentTable(xrTable2);
            this.SetCurrencyFormat(xrAmount.Text, xrAmount);
        }
    }
}
