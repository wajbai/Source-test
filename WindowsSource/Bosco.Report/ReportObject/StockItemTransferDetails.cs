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
    public partial class StockItemTransferDetails : Bosco.Report.Base.ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public StockItemTransferDetails()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            SetStockItemTransferredReport();
        }

        private void SetStockItemTransferredReport()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty && this.ReportProperties.Project != "0")
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));

                setHeaderTitleAlignment();
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                DataTable dtStockTransferedItem = BindReportSource();
                if (dtStockTransferedItem != null)
                {
                    this.DataSource = dtStockTransferedItem;
                    this.DataMember = dtStockTransferedItem.TableName;
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
            string stockSummary = this.GetReportTDS(SQL.ReportSQLCommand.Stock.StockTransferredItem);
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
