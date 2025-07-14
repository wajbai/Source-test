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
    public partial class CostCenterCashFlow : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;

        public CostCenterCashFlow()
        {
            InitializeComponent();
        }

        #region ShowReport

        public override void ShowReport()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                ShowReportFilterDialog();
            }
            else
            {
                BindCostCenterCashJournal();
            }

            base.ShowReport();
        }
        #endregion

        private void BindCostCenterCashJournal()
        {
            this.ReportTitle = ReportProperty.Current.ReportTitle;
            this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            this.ReportPeriod = "For the Period: " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;

            xrCapReceipts.Text = this.SetCurrencyFormat("Receipts");
            xrCapPayments.Text = this.SetCurrencyFormat("Payments");

            ResultArgs resultArgs = GetReportSource();
            DataTable dtReceipt = resultArgs.DataSource.Table;

            if (dtReceipt != null)
            {
                dtReceipt.TableName = "CashBankJournal";
                this.DataSource = dtReceipt;
                this.DataMember = dtReceipt.TableName;
            }
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CostCentreCashJournal = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCenterCashJournal);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CostCentreCashJournal);
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
