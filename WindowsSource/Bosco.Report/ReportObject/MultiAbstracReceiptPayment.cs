using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstracReceiptPayment :ReportBase
    {
        ResultArgs resultArgs = null;
        public MultiAbstracReceiptPayment()
        {
            InitializeComponent();
        }

        private ResultArgs SetReportSource()
        {
            string sqlMonthlyAbstractReceiptPayment = this.GetReportSQL(SQL.ReportSQLCommand.Report.MonthlyAbstractReceiptPayment);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_ALL_LEDGERColumn, this.ReportProperties.IncludeAllLedger);
                dataManager.Parameters.Add(this.ReportParameters.SHOW_BY_LEDGERColumn, this.ReportProperties.ShowByLedger);
                dataManager.Parameters.Add(this.ReportParameters.SHOW_BY_LEDGER_GROUPColumn, this.ReportProperties.ShowByLedgerGroup);
                dataManager.Parameters.Add(this.ReportParameters.PROJECTColumn, this.ReportProperties.Project);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceiptPayment);
            }
            return resultArgs;
        }

        private void BindReceiptsPaymentSource()
        {
            if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
            {
                ShowReportFilterDialog();
            }
            else
            {
                resultArgs = SetReportSource();
                lblRPDateFrom.Text = this.ReportProperties.DateFrom;
                lblRPDateTo.Text = this.ReportProperties.DateTo;
                lblReceiptsPProjectTitle.Text = this.ReportProperties.ProjectTitle;
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    this.DataSource = resultArgs.DataSource.Table;
                }
            }
        }
    }
}
