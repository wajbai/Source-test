using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility.ConfigSetting;
using System.Data;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;

namespace Bosco.Report.ReportObject
{
    public partial class SalesDisposeDonateRegister : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public SalesDisposeDonateRegister()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindSalesDisposeDonateSource();
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
                    BindSalesDisposeDonateSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }

            this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
        }
        #endregion

        #region Methods

        public void BindSalesDisposeDonateSource()
        {
            DataTable dtSalesDispose = new DataTable();
            SetReportTitle();

            this.SetLandscapeHeader = 1065.25f;
            this.SetLandscapeFooter = 1065.25f;
            this.SetLandscapeFooterDateWidth = 1065.00f;

            ResultArgs resultArgs = GetReportSource();
            DataView dvReceipt = resultArgs.DataSource.TableView;
            dtSalesDispose = dvReceipt.ToTable();
            if (dvReceipt != null)
            {
                dtSalesDispose.TableName = "SalesDispose";
                this.DataSource = dtSalesDispose.DefaultView;
                this.DataMember = dtSalesDispose.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlSalesDisposeDonate = this.GetAssetReports(SQL.ReportSQLCommand.Asset.SalesDisposeDonateRegister);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlSalesDisposeDonate);
            }
            return resultArgs;
        }

        #endregion
    }
}
