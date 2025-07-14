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
    public partial class StockPurchase : Bosco.Report.Base.ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public StockPurchase()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            SetStockPurchaseReport();
        }

        private void SetStockPurchaseReport()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty && this.ReportProperties.Project != "0")
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));

                this.SetLandscapeHeader = 1065.25f;
                this.SetLandscapeFooter = 1053.25f;
                this.SetLandscapeFooterDateWidth = 890.00f;

                setHeaderTitleAlignment();
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                DataTable dtStockPurchase = BindReportSource();
                if (dtStockPurchase != null)
                {
                    this.DataSource = dtStockPurchase;
                    this.DataMember = dtStockPurchase.TableName;
                }
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
        }

        private DataTable BindReportSource()
        {
            string stockSummary = this.GetReportTDS(SQL.ReportSQLCommand.Stock.StockPurchase);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                if (!string.IsNullOrEmpty(ReportProperty.Current.StockItemId))
                {
                    dataManager.Parameters.Add(this.ReportParameters.ITEM_IDColumn, this.ReportProperties.StockItemId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, stockSummary);
            }
            return resultArgs.DataSource.Table;
        }
        #endregion
    }
}
