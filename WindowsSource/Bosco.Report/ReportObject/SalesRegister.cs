using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class SalesRegister : Bosco.Report.Base.ReportHeaderBase
    {

        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Property
        
        #endregion

        #region Constructor
        public SalesRegister()
        {
            InitializeComponent();
        }
        #endregion
      
        #region ShowReport
        public override void ShowReport()
        {
            BindAMCRegister();

            //  ShowReportFilterDialog();
        }
        #endregion

        #region Method
        private void BindAMCRegister()
        {
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.SetLandscapeFooterDateWidth = 250.00f;
            this.SetLandscapeHeader = 785.10f;
            this.SetLandscapeFooter = 785.10f;
            SetReportTitle();
            setHeaderTitleAlignment();
            xrtAmount.Text = this.SetCurrencyFormat(xrtAmount.Text);
           // xrtRate.Text = this.SetCurrencyFormat(xrtRate.Text);
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
            xrtSalesHeader = AlignHeaderTable(xrtSalesHeader);
            xrtSalesDetail = AlignContentTable(xrtSalesDetail);
        }
        private DataTable GetReportSource()
        {
            string AssetSales = this.GetAssetReports(SQL.ReportSQLCommand.Asset.AssetSalesRegister);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                if (!string.IsNullOrEmpty(this.ReportProperties.AssetItemID))
                {
                    dataManager.Parameters.Add(this.ReportParameters.ITEM_IDColumn, this.ReportProperties.AssetItemID);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, AssetSales);
            }
            return resultArgs.DataSource.Table;
        }
        private void SetReportBorder()
        {
            xrtSalesHeader = AlignHeaderTable(xrtSalesHeader);
            xrtSalesDetail = AlignContentTable(xrtSalesDetail);
        }
        #endregion

    }
}
