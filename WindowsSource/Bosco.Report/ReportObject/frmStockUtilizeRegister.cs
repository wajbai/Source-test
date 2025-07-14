using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class frmStockUtilizeRegister : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        public frmStockUtilizeRegister()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            ShowStockRegister();
        }

        #region Methods
        private void ShowStockRegister()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || this.ReportProperties.Project == "0" || String.IsNullOrEmpty(this.ReportProperties.Project))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                ShowStockRegisterDetails();
                base.ShowReport();
            }
        }
        private void ShowStockRegisterDetails()
        {
            try
            {
                this.ReportTitle = ReportProperty.Current.ReportTitle;
                this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateFrom;
                setHeaderTitleAlignment();
                SetReportTitle();
                this.SetLandscapeHeader = 1060f;
                this.SetLandscapeFooter = 1060f;
                this.SetLandscapeFooterDateWidth = 900f;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                DataTable dtStockRegister = BindDataSource();
                if (dtStockRegister != null)
                {
                    dtStockRegister.TableName = "UtilizeRegister";
                    this.DataSource = dtStockRegister;
                    this.DataMember = dtStockRegister.TableName;
                }
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }
        private void SetReportBorder()
        {
            xrHeaderCaption = AlignHeaderTable(xrHeaderCaption);
            xrtblDetail = AlignTotalTable(xrtblDetail);
        }

        private DataTable BindDataSource()
        {
            string stockRegister = this.GetReportTDS(SQL.ReportSQLCommand.Stock.stockUtilizeRegister);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                //if (!string.IsNullOrEmpty(ReportProperty.Current.StockItemId))
                //{
                //    dataManager.Parameters.Add(this.ReportParameters.ITEM_IDColumn, this.ReportProperties.StockItemId);
                //}
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, stockRegister);
            }
            return resultArgs.DataSource.Table;
        }
        #endregion

    }
}
