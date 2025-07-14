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
    public partial class BankReconciliationStatement : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        #endregion
        #region Constructor
        public BankReconciliationStatement()
        {
            InitializeComponent();
        }
        #endregion
        #region ShowReport
        public override void ShowReport()
        {
           BindBankReconciliationStatement();
           base.ShowReport();
        }
        #endregion
        #region Method
        public void BindBankReconciliationStatement()
        {
             if (this.ReportProperties.DateAsOn != string.Empty)
             {
                this.ReportTitle = ReportProperty.Current.ReportTitle;
                this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.ReportPeriod = "For the Period: " + this.ReportProperties.DateAsOn;
                ResultArgs resultArgs = GetReportSource();
                DataView dvBankReconciliation = resultArgs.DataSource.TableView;
                if (dvBankReconciliation != null)
                {
                    dvBankReconciliation.Table.TableName = "BankReconciliationStatement";
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
                ResultArgs resultArgs = null;
                string BRStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankReconcilationStatement);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, BRStatement);
                }
                return resultArgs;
        }
        #endregion
    }
}
