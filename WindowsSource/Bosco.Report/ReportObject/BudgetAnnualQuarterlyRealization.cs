using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetAnnualQuarterlyRealization : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = new ResultArgs();
        
        public BudgetAnnualQuarterlyRealization()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            BindBudgetAnnualMonthlyRealization();
        }
        #endregion

        public void BindBudgetAnnualMonthlyRealization()
        {

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project.Split(',').Length == 0)
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        FetchBudgetDetails();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    FetchBudgetDetails();
                    base.ShowReport();
                }
            }
        }

        private void FetchBudgetDetails()
        {
            try
            {
                this.BudgetName = ReportProperty.Current.BudgetName;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                ReportProperty.Current.ReportTitle = "Budget Realization - " + this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false).Year.ToString();
                SetReportTitle();
                AssignBudgetDateRangeTitle();
                SetTitleWidth(xrSubQtlyBudgetIncomeLedgers.WidthF);
                setHeaderTitleAlignment();
                
                ReportFooter.Visible = false;
                xrSubSignFooter.Visible = false;
                if (ReportProperty.Current.IncludeSignDetails == 1)
                {
                    xrSubSignFooter.Visible = true;
                    ReportFooter.Visible = true;
                    (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrSubQtlyBudgetIncomeLedgers.WidthF - 10;
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
                }

                if (this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false) > this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                {
                    this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString();
                }

                //To get projectsids for selected repor date
                string annualbudgetsprojects = GetBudgetProjectsByDateProjects();
                this.ReportProperties.Project = annualbudgetsprojects;

                resultArgs = GetReportSource(TransMode.CR);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtBudgetDetails = resultArgs.DataSource.Table;
                    string filter = string.Empty;
                    filter = this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName + "='" + TransMode.CR.ToString() + "'" +
                                        " OR (" + this.reportSetting1.BUDGETVARIANCE.NATURE_IDColumn.ColumnName + " IN (1, 3, 4) AND TRANS_MODE IS NULL)";
                    dtBudgetDetails.DefaultView.RowFilter = filter;
                    DataTable dtQuaterlyLedges = dtBudgetDetails.DefaultView.ToTable();
                    BudgetAnnualQtlyRealizationLedgers xrBudgetAnnualQtlyIncomeLedgers = xrSubQtlyBudgetIncomeLedgers.ReportSource as BudgetAnnualQtlyRealizationLedgers;
                    xrBudgetAnnualQtlyIncomeLedgers.BindBudgetDetails(dtQuaterlyLedges, TransMode.CR);

                    resultArgs = GetReportSource(TransMode.DR);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        dtBudgetDetails = resultArgs.DataSource.Table;

                        filter = this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName + "='" + TransMode.DR.ToString() + "'" +
                                            " OR (" + this.reportSetting1.BUDGETVARIANCE.NATURE_IDColumn.ColumnName + " IN (2, 3, 4) AND TRANS_MODE IS NULL)";
                        dtBudgetDetails.DefaultView.RowFilter = filter;
                        dtQuaterlyLedges = dtBudgetDetails.DefaultView.ToTable();
                        BudgetAnnualQtlyRealizationLedgers xrBudgetAnnualQtlyExpenseLedgers = xrSubQtlyBudgetExpenseLedgers.ReportSource as BudgetAnnualQtlyRealizationLedgers;
                        
                        xrBudgetAnnualQtlyExpenseLedgers.GrandIncomeApprovedTotal = xrBudgetAnnualQtlyIncomeLedgers.GrandIncomeApprovedTotal;
                        xrBudgetAnnualQtlyExpenseLedgers.GrandIncomeDifferenceTotal = xrBudgetAnnualQtlyIncomeLedgers.GrandIncomeDifferenceTotal;
                        xrBudgetAnnualQtlyExpenseLedgers.GrandExpenseDifferenceTotal = xrBudgetAnnualQtlyIncomeLedgers.GrandExpenseDifferenceTotal;
                        xrBudgetAnnualQtlyExpenseLedgers.GrandIncomeDueTotal = xrBudgetAnnualQtlyIncomeLedgers.GrandIncomeDueTotal;

                        xrBudgetAnnualQtlyExpenseLedgers.BindBudgetDetails(dtQuaterlyLedges, TransMode.DR);
                        xrBudgetAnnualQtlyExpenseLedgers.HeaderLoaded= false;
                    }
                    else if (!resultArgs.Success)
                    {
                        MessageRender.ShowMessage(resultArgs.Message, false);
                    }
                }
                else if (!resultArgs.Success)
                {
                    MessageRender.ShowMessage(resultArgs.Message, false);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private ResultArgs GetReportSource(TransMode budgetNature)
        {
            ResultArgs result = new ResultArgs();
            string budgetvariance = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetAnnualQuarterlyRealization);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.AppSetting.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.AppSetting.YearTo);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.AppSetting.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, budgetNature);
                if (this.AppSetting.ShowBudgetLedgerActualBalance == "1")
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, "JN");
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetvariance);
            }
            return result;
        }
    }
}
