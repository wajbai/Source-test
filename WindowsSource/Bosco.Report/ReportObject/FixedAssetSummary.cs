using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class FixedAssetSummary : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Property
        #endregion

        #region Cunstructor
        public FixedAssetSummary()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateAsOn)
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
                        BindFixedAssetSummarySource();
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
                    BindFixedAssetSummarySource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }
        #endregion

        #region Methods
        public void BindFixedAssetSummarySource()
        {
            this.SetLandscapeFooterDateWidth = 250.00f;
            string datetime = this.GetProgressiveDate(this.ReportProperties.DateAsOn);
            this.SetLandscapeHeader = 723.00f;
            this.SetLandscapeFooter = 723.00f;
            
            this.ReportTitle = ReportProperty.Current.ReportTitle;
            this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            SetReportTitle();
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
            ResultArgs resultArgs = GetReportSource();
            DataView dvFixedAssetSummary = resultArgs.DataSource.TableView;

            if (dvFixedAssetSummary != null)
            {
                dvFixedAssetSummary.Table.TableName = "FixedAssetsSummary";
                this.DataSource = dvFixedAssetSummary;
                this.DataMember = dvFixedAssetSummary.Table.TableName;
            }
            SetReportBorder();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string FixedAssetSummary = this.GetAssetReports(SQL.ReportSQLCommand.Asset.FixedAssetSummary);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FixedAssetSummary);
            }
            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrtblFixedAssetSummaryHeader = AlignHeaderTable(xrtblFixedAssetSummaryHeader);
            xrtAssetFixedAssetSummaryRegiter = AlignContentTable(xrtAssetFixedAssetSummaryRegiter);

            this.SetCurrencyFormat(xrlAmount.Text, xrlAmount);
        }
        #endregion
    }
}
