using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Report.View;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class InsuranceRegister : Bosco.Report.Base.ReportHeaderBase
    {
        #region VairableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Property

        #endregion

        #region Constructor
        public InsuranceRegister()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindInsuranceRegister();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Bind the Source Controls
        /// </summary>
        private void BindInsuranceRegister()
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
                        BindSourceDetails();
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
                    BindSourceDetails();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }

        /// <summary>
        /// Bind Source
        /// </summary>
        public void BindSourceDetails()
        {
            DataTable dtInsuranceDetails = new DataTable();
            SetReportTitle();

            this.SetLandscapeHeader = 1125.25f;
            this.SetLandscapeFooter = 1125.25f;
            this.SetLandscapeFooterDateWidth = 960.00f;

            SetReportBorder();

            ResultArgs resultArgs = GetReportSource();
            dtInsuranceDetails = resultArgs.DataSource.Table;
            if (dtInsuranceDetails != null)
            {
                this.DataSource = dtInsuranceDetails;
                this.DataMember = dtInsuranceDetails.TableName;
            }
        }

        /// <summary>
        /// Get source Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetReportSource()
        {
            string InsuranceRegister = this.GetAssetReports(SQL.ReportSQLCommand.Asset.AssetInsuranceRegister);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, InsuranceRegister);
            }
            return resultArgs;
        }

        /// <summary>
        /// Border verification
        /// </summary>
        private void SetReportBorder()
        {
            this.SetCurrencyFormat(xrTableCell19.Text, xrTableCell19);
            this.SetCurrencyFormat(xrTableCell11.Text, xrTableCell11);
        }
        #endregion
    }
}
