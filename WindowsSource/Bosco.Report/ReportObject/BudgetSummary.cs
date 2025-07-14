using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.DAO.Schema;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetSummary : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public BudgetSummary()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            FetchBudgetSummary();
        }
        #endregion

        #region Methods
        public void FetchBudgetSummary()
        {
            SetReportTitle();
            if (string.IsNullOrEmpty(this.ReportProperties.BudgetId) || string.IsNullOrEmpty(this.ReportProperties.CompareBudgetId))
            {
                ShowReportFilterDialog();
            }
            else
            {
                ShowBudgetSummaryReport();
                base.ShowReport();
            }
        }

        private void ShowBudgetSummaryReport()
        {
            try
            {
                string BudgetDateFrom = string.Empty;
                string BudgetDateTo = string.Empty;
                string BudgetCompareDateFrom = string.Empty;
                string BudgetCompareDateTo = string.Empty;
                setHeaderTitleAlignment();
                SetReportTitle();
                this.HideDateRange = false;

                this.ReportTitle = ReportProperty.Current.BudgetName;
                string BudgetSummary = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetSummary);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.BudgetId);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PREVIOUS_BUDGET_IDColumn, this.ReportProperties.CompareBudgetId);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BudgetSummary);

                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        BudgetDateFrom = resultArgs.DataSource.Table.Compute("MIN(YEAR_FROM)", String.Empty).ToString();
                        BudgetDateTo = resultArgs.DataSource.Table.Compute("MAX(YEAR_TO)", String.Empty).ToString();
                        BudgetCompareDateFrom = resultArgs.DataSource.Table.Compute("MIN(CURRENT_BUD_YEAR)", String.Empty).ToString();
                        BudgetCompareDateTo = resultArgs.DataSource.Table.Compute("MAX(CURRENT_BUD_TO)", String.Empty).ToString();

                        BudgetOpClosingBalance accountBalance = xrSubOpeningBalance.ReportSource as BudgetOpClosingBalance;
                        accountBalance.BindBalance(true, BudgetDateFrom, BudgetCompareDateFrom); //For Opening Balance
                        accountBalance.GroupCodeWith = accountBalance.LedgerCodeWith = 0;
                        accountBalance.GroupNameWidth = xrCode.WidthF + xrLedgerName.WidthF + xrComBudgetProjected.WidthF - 2;
                        accountBalance.NameColumnWidth = xrCode.WidthF + xrLedgerName.WidthF + xrComBudgetProjected.WidthF;
                        accountBalance.AmountColumnWidth = accountBalance.GroupAmountWidth = xrComBudgetActual.WidthF;

                        resultArgs.DataSource.Table.TableName = "BUDGETVARIANCE";
                        this.SetCurrencyFormat("Budgeted", xrComBudgetProjected);
                        this.SetCurrencyFormat("Actual", xrComBudgetActual);
                        this.SetCurrencyFormat("Budget", xrCurrentProjected);
                        string PreviousBudget = !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0]["BUDGET"].ToString()) ? resultArgs.DataSource.Table.Rows[0]["BUDGET"].ToString() : string.Empty;
                        string CurrentBud = !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0]["COMPARE BUDGET"].ToString()) ? resultArgs.DataSource.Table.Rows[0]["COMPARE BUDGET"].ToString() : string.Empty;
                        //string PreviousDate = String.Format("{0}-{1}", UtilityMember.DateSet.ToDate(BudgetDateFrom, false).Year.ToString(), UtilityMember.DateSet.ToDate(BudgetDateTo, false).Year.ToString());
                        //string CurrentBudDate = String.Format("{0}-{1}", UtilityMember.DateSet.ToDate(BudgetCompareDateFrom, false).Year.ToString(), UtilityMember.DateSet.ToDate(BudgetCompareDateTo, false).Year.ToString());
                        xrComBudgetProjected.Text = String.Format("{0}{2} ({1})", xrComBudgetProjected.Text, PreviousBudget, Environment.NewLine);
                        xrComBudgetActual.Text = String.Format("{0}{2} ({1})", xrComBudgetActual.Text, PreviousBudget, Environment.NewLine);
                        xrCurrentProjected.Text = String.Format("{0} {2}({1})", xrCurrentProjected.Text, CurrentBud, Environment.NewLine);
                        this.DataSource = resultArgs.DataSource.Table;
                        this.DataMember = resultArgs.DataSource.Table.TableName;

                        BudgetOpClosingBalance accountClosingBalance = xrSubClosingBalance.ReportSource as BudgetOpClosingBalance;
                        accountClosingBalance.BindBalance(false, BudgetDateTo, BudgetDateTo); //For Closing Balance
                        accountClosingBalance.GroupCodeWith = accountClosingBalance.LedgerCodeWith = 0;
                        accountClosingBalance.GroupNameWidth = accountClosingBalance.NameColumnWidth = xrComBudgetProjected.WidthF;
                        accountClosingBalance.AmountColumnWidth = accountClosingBalance.GroupAmountWidth = xrComBudgetActual.WidthF;
                        accountClosingBalance.BudgetAmount = accountClosingBalance.GroupBudgetAmount = xrCurrentProjected.WidthF;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
        }
        #endregion
    }
}


