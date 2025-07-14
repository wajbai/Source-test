using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class Cash_Flow : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion
        #region Constructor
        public Cash_Flow()
        {
            InitializeComponent();
        }
        #endregion
        #region ShowReport
        public override void ShowReport()
        {
            BindBankBalanceStatement();
            base.ShowReport();
        }
        #endregion
        #region Method
        private void BindBankBalanceStatement()
        {
            if (this.ReportProperties.DateAsOn != string.Empty)
            {
                this.ReportTitle = ReportProperty.Current.ReportTitle;
                this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.ReportPeriod = "For the Period: " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                resultArgs = GetReportSource();
                DataView dvBankReconciliation = resultArgs.DataSource.TableView;
                if (dvBankReconciliation != null && dvBankReconciliation.Count != 0)
                {
                    dvBankReconciliation.Table.TableName = "CashBankFlow";
                    this.DataSource = dvBankReconciliation;
                    this.DataMember = dvBankReconciliation.Table.TableName;
                }
            }
            else
            {
                ShowReportFilterDialog();
            }
        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string CashFlow = this.GetReportSQL(SQL.ReportSQLCommand.Report.CashFlow);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportProperties.DateTo, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashFlow);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }
        #endregion
    }
}
