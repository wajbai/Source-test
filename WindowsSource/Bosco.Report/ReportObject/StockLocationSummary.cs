using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class StockLocationSummary : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable
        ResultArgs resultArgs = null;
        #endregion

        #region Constructors
        public StockLocationSummary()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Report
        public override void ShowReport()
        {
            ShowStockLocationSummary();
          
        }
        #endregion

        #region Methods
        private void ShowStockLocationSummary()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateAsOn)
                || this.ReportProperties.Project == "0" || String.IsNullOrEmpty(this.ReportProperties.Project))
            {
                SetReportTitle();
                this.ReportPeriod = String.Format("Date as on: {0}", this.ReportProperties.DateAsOn);
                ShowReportFilterDialog();
            }
            else
            {
                LoadStockLocationSummary();
                base.ShowReport();
            }
        }
        private void LoadStockLocationSummary()
        {
            try
            {
                this.ReportTitle = ReportProperty.Current.ReportTitle;
                this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.ReportPeriod = String.Format("Date as on: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                setHeaderTitleAlignment();

                SetReportTitle();
                this.ReportPeriod = String.Format("Date as on: {0}", this.ReportProperties.DateAsOn);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                xrPageBreak1.Visible = (this.ReportProperties.ShowByLocation == 1) ? true : false;
                if (this.ReportProperties.ShowByLocation == 1)
                {
                    GroupFooter1.Visible = true;
                    ReportFooter.Visible = false;
                }
                else
                {
                    GroupFooter1.Visible = false;
                    ReportFooter.Visible = true;
                }
                DataTable dtStockLocationSummary = BindDataSource();
                if (dtStockLocationSummary != null && dtStockLocationSummary.Rows.Count > 0)
                {
                    dtStockLocationSummary.TableName = "StockLocationSummary";
                    this.DataSource = dtStockLocationSummary;
                    this.DataMember = dtStockLocationSummary.TableName;
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
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblGroup = AlignContentTable(xrtblGroup);
            xrtblStockItems = AlignTotalTable(xrtblStockItems);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);
        }

        private DataTable BindDataSource()
        {
            string stockLocationSummary = this.GetReportTDS(SQL.ReportSQLCommand.Stock.StockLocationSummary);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateAsOn);
                if (!string.IsNullOrEmpty(ReportProperty.Current.LocationId))
                {
                    dataManager.Parameters.Add(this.ReportParameters.LOCATION_IDColumn, this.ReportProperties.LocationId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, stockLocationSummary);
            }
            return resultArgs.DataSource.Table;
        }
        #endregion
    }
}
