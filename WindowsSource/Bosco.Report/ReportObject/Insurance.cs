using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class Insurance : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public Insurance()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindInsuranceDetails();
        }
        #endregion

        #region Method

        private void BindInsuranceDetails()
        {
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            SetReportTitle();
            setHeaderTitleAlignment();
            this.SetLandscapeHeader = 1100.25f;
            this.SetLandscapeFooter = 1100.25f;
            this.SetLandscapeFooterDateWidth = 400.00f;

            if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));

                        DataTable dtFARegister = GetReportSource();
                        if (dtFARegister != null)
                        {
                            this.DataSource = dtFARegister;
                            this.DataMember = dtFARegister.TableName;
                        }

                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));

                    DataTable dtFARegister = GetReportSource();
                    if (dtFARegister != null)
                    {
                        this.DataSource = dtFARegister;
                        this.DataMember = dtFARegister.TableName;
                    }

                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            xrInsuranceHeader = AlignHeaderTable(xrInsuranceHeader);
            xrInsuranceDetail = AlignContentTable(xrInsuranceDetail);
            this.SetCurrencyFormat(xrAmount.Text, xrAmount);
            this.SetCurrencyFormat(xrtValue.Text, xrtValue);
        }

        #endregion

        private DataTable GetReportSource()
        {
            string AssetInsurance = this.GetAssetReports(SQL.ReportSQLCommand.Asset.AssetInsuranceRegister);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, AssetInsurance);
            }
            return resultArgs.DataSource.Table;
        }
    }
}
