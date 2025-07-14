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
    public partial class CCDisbusementListDetails : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = new ResultArgs();
        public CCDisbusementListDetails()
        {
            InitializeComponent();
        }
        public override void ShowReport()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {

                ShowReportFilterDialog();
            }
            else
            {
                resultArgs = GetReportSource();
                this.DataSource = null;
                DataView dvCashBankBook = resultArgs.DataSource.TableView;
                if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                {
                    dvCashBankBook.Table.TableName = "Cashbankbook";
                    this.DataSource = dvCashBankBook;
                    this.DataMember = dvCashBankBook.Table.TableName;
                }
                base.ShowReport();
            }
        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string Query = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreDisbursementList);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Query);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }


    }
}
