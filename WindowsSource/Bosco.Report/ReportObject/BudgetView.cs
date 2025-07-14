using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetView : Bosco.Report.Base.ReportHeaderBase
    {
        public BudgetView()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(this.ReportProperties.BudgetId))
            {
                //ShowBudgetView(); //on 07/07/2020
            }
        }

        #region Show Reports
        public override void ShowReport()
        {
            SetReportTitle();
            ShowBudgetView();
            base.ShowReport();
        }
        #endregion

        #region Methods
        public void ShowBudgetView()
        {
            if (AppSetting.IS_CMF_CONGREGATION)
            {
                xrIncomeTitle.WidthF = this.PageWidth - 50;
                xrExpenseTitle.WidthF = this.PageWidth - 50;
            }

            SetTitleWidth(xrIncomeTitle.WidthF);
            this.SetLandscapeBudgetNameWidth = xrIncomeTitle.WidthF;
            this.SetLandscapeHeader = xrIncomeTitle.WidthF;
            this.SetLandscapeFooter = xrIncomeTitle.WidthF;
            this.SetLandscapeFooterDateWidth = xrIncomeTitle.WidthF - 160;
            setHeaderTitleAlignment();
            BindBudgetView();
        }

        private void BindBudgetView()
        {
            ResultArgs resultArgs = new ResultArgs();
            Int32 PreviousBudgetId = 0;
            string value = string.Empty;
            string Projects = string.Empty;
            string ProjectsIds = string.Empty;

            try
            {

                setHeaderTitleAlignment();
                SetReportTitle();
                //this.ReportProperties.BudgetId = "1,2";
                this.HidePageFooter = false;
                string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetInfo);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGET_STATISTICS.BUDGET_IDColumn, this.ReportProperties.BudgetId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);
                }

                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtBudgetInfo = resultArgs.DataSource.Table;
                    DateTime CurrentYrSelectedDateFrom = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_FROM"].ToString(), false);
                    DateTime CurrentYrSelectedDateTo = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_TO"].ToString(), false);
                    Int32 Budget_Type = UtilityMember.NumberSet.ToInteger(dtBudgetInfo.Rows[0]["BUDGET_TYPE_ID"].ToString());
                    Int32 Budget_Action = UtilityMember.NumberSet.ToInteger(dtBudgetInfo.Rows[0]["BUDGET_ACTION"].ToString());

                    DateTime SelectedPreviousDateFrom = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_FROM"].ToString(), false).AddYears(-1);
                    DateTime SelectedPreviousDateTo = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_TO"].ToString(), false).AddYears(-1);

                    if (this.AppSetting.IS_ABEBEN_DIOCESE)
                    {
                        SelectedPreviousDateFrom = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_FROM"].ToString(), false).AddMonths(-1);
                        SelectedPreviousDateTo = UtilityMember.DateSet.ToDate(dtBudgetInfo.Rows[0]["DATE_TO"].ToString(), false).AddMonths(-1);
                    }

                    if (AppSetting.IS_CMF_CONGREGATION)
                    {
                        xrExpenseTitle.Font = new Font(xrExpenseTitle.Font.FontFamily, 16);
                        xrIncomeTitle.Font = new Font(xrIncomeTitle.Font.FontFamily, 16);
                    }

                    var list = dtBudgetInfo.AsEnumerable().Select(r => r["PROJECT_NAME"].ToString());
                    value = string.Join(",", list);
                    Projects = value; // dtBudgetInfo.Rows[0]["PROJECT_NAME"].ToString();

                    list = dtBudgetInfo.AsEnumerable().Select(r => r["PROJECT"].ToString());
                    value = string.Join(",", list);
                    ProjectsIds = value;// dtBudgetInfo.Rows[0]["PROJECT"].ToString();

                    PreviousBudgetId = GetPreviousBudgetIdByProjectId(ProjectsIds, SelectedPreviousDateFrom, SelectedPreviousDateTo);

                    this.HideDateRange = false;
                    this.HideReportSubTitle = true;
                    this.ReportTitle = ReportProperty.Current.BudgetName;
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        this.ReportTitle += " - " + this.ReportProperties.CurrencyCountry;
                    }
                    if (AppSetting.IS_CMF_CONGREGATION)
                    {
                        this.ReportProperties.ProjectTitle = Projects;
                        this.HidePageFooter = true;
                        this.HidePageInfo = true;

                        this.ReportTitle = Projects + " - Budget Proposal - " + CurrentYrSelectedDateFrom.Year.ToString();
                        this.ReportSubTitle = Projects;

                        //this.DisplayName += " - " + AppSetting.InstituteName;
                        this.DisplayName = Projects + " - Budget Proposal - " + CurrentYrSelectedDateFrom.Year.ToString(); ;
                        this.ReportProperties.SortByLedger = 1;
                        this.ReportProperties.IncludeAllLedger = 1;
                        this.ReportProperties.ShowPageNumber = 1;
                        this.ReportProperties.ShowProjectsinFooter = 1;
                        this.ReportProperties.ShowReportDate = 1;
                        this.ReportProperties.ReportDate = this.UtilityMember.DateSet.ToDate(DateTime.Today.ToShortDateString());
                        this.SetReportDate = this.UtilityMember.DateSet.ToDate(DateTime.Today.ToShortDateString());
                        this.ReportProperties.IncludeSignDetails = 1;
                    }
                    else if (AppSetting.IS_BSG_CONGREGATION)
                    {   //On 28/02/2024, To set sign details for montfort pune
                        this.ReportProperties.IncludeSignDetails = 1;
                    }
                    else
                    {
                        this.ReportSubTitle = Projects;
                    }

                    //replace special characters which are not valid for file names
                    this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
                    this.ReportPeriod = String.Format("For the Period: {0} - {1}", CurrentYrSelectedDateFrom.ToShortDateString(), CurrentYrSelectedDateTo.ToShortDateString());


                    if (this.AppSetting.IS_SDB_INM)
                    {
                        //replace special characters which are not valid for file names
                        this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
                        this.ReportTitle = "";
                        this.ReportPeriod = string.Empty;
                        this.ReportSubTitle = ReportProperty.Current.BudgetName; // String.Format("Budget Period: {0} - {1}", CurrentYrSelectedDateFrom.ToString("dd/MM/yyyy"), CurrentYrSelectedDateTo.ToString("dd/MM/yyyy")); // ReportProperty.Current.BudgetName;
                        //this.ReportSubTitle = String.Format("Budget Period: {0} - {1}", CurrentYrSelectedDateFrom.ToString("dd/MM/yyyy"), CurrentYrSelectedDateTo.ToString("dd/MM/yyyy"));
                        this.CosCenterName = String.Format("Budget Period: {0} - {1}", CurrentYrSelectedDateFrom.ToString("dd-MM-yyyy"), CurrentYrSelectedDateTo.ToString("dd-MM-yyyy"));
                        this.BudgetName = string.Format("Projects : {0}", Projects);
                        this.LegalEntityAddress = "";
                    }

                    BudgetStatistics budgetstatistics = xrSubBudgetStatistics.ReportSource as BudgetStatistics;
                    budgetstatistics.HideReportHeader = false;
                    budgetstatistics.HidePageFooter = false;

                    BudgetLedger BudgetIncomeledger = xrSubBudgetIncomeLedgers.ReportSource as BudgetLedger;
                    BudgetIncomeledger.HideReportHeader = false;
                    BudgetIncomeledger.HidePageFooter = false;
                    BudgetIncomeledger.BindBudgetLedgers(TransMode.CR, ProjectsIds, Budget_Type, CurrentYrSelectedDateFrom, CurrentYrSelectedDateTo,
                        SelectedPreviousDateFrom, SelectedPreviousDateTo, Budget_Action, PreviousBudgetId);

                    BudgetLedger BudgetExpenseledger = xrSubBudgetExpenseLedgers.ReportSource as BudgetLedger;
                    BudgetExpenseledger.HideReportHeader = false;
                    BudgetExpenseledger.HidePageFooter = false;

                    DataTable dtBudgetDevelopmentalProjects = null;
                    grpBudgetDeveNewProject.Visible = (this.AppSetting.CreateBudgetDevNewProjects == 1);
                    if (this.AppSetting.CreateBudgetDevNewProjects == 1)
                    {
                        BudgetDevelopmentalProjectDetails BudgetDevNewProjects = xrSubBudgetDevelopmentalProjects.ReportSource as BudgetDevelopmentalProjectDetails;
                        BudgetDevNewProjects.HideReportHeader = false;
                        BudgetDevNewProjects.HidePageFooter = false;
                        BudgetDevNewProjects.BindBudgetDevelopmentalProjects();
                        dtBudgetDevelopmentalProjects = BudgetDevNewProjects.dtBudgetDevelopmentalProjects;
                        xrSubBudgetDevelopmentalProjects.WidthF = xrSubBudgetIncomeLedgers.WidthF - 10;
                    }
                    if (dtBudgetDevelopmentalProjects != null && dtBudgetDevelopmentalProjects.Rows.Count == 0)
                    {
                        grpBudgetDeveNewProject.Visible = false;
                    }

                    if (this.AppSetting.IS_CMF_CONGREGATION)
                    {
                        BudgetExpenseledger.TotalPrevApprovedBudgetedIncome = BudgetIncomeledger.TotalPrevApprovedBudgetedIncome;
                        BudgetExpenseledger.TotalProposedBudgetedIncome = BudgetIncomeledger.TotalProposedBudgetedIncome;
                        BudgetExpenseledger.TotalApprovedBudgetedIncome = BudgetIncomeledger.TotalApprovedBudgetedIncome;

                        //For 28/01/2022, To assing opening and raw budgeted income
                        BudgetExpenseledger.TotalOpeningBalance = BudgetIncomeledger.TotalOpeningBalance;
                        BudgetExpenseledger.CashOpeningBalance = BudgetIncomeledger.CashOpeningBalance;
                        BudgetExpenseledger.BankOpeningBalance = BudgetIncomeledger.BankOpeningBalance;
                        BudgetExpenseledger.FDOpeningBalance = BudgetIncomeledger.FDOpeningBalance;
                        BudgetExpenseledger.TotalBudgetedRawProposedIncome = BudgetIncomeledger.TotalBudgetedRawProposedIncome;
                    }

                    BudgetExpenseledger.BindBudgetLedgers(TransMode.DR, ProjectsIds, Budget_Type, CurrentYrSelectedDateFrom, CurrentYrSelectedDateTo,
                            SelectedPreviousDateFrom, SelectedPreviousDateTo, Budget_Action, PreviousBudgetId);

                    budgetstatistics.BudgetTypeId = Budget_Type;
                    budgetstatistics.dtBudgetDateFrom = CurrentYrSelectedDateFrom;
                    budgetstatistics.dtBudgetDateTo = CurrentYrSelectedDateTo;

                    budgetstatistics.TotalProposedBudgetedIncome = BudgetIncomeledger.TotalProposedBudgetedIncome;
                    budgetstatistics.TotalApprovedBudgetedIncome = BudgetIncomeledger.TotalApprovedBudgetedIncome;
                    budgetstatistics.TotalProposedBudgetedExpense = BudgetExpenseledger.TotalProposedBudgetedExpense;
                    budgetstatistics.TotalApprovedBudgetedExpense = BudgetExpenseledger.TotalApprovedBudgetedExpense;
                    budgetstatistics.SetPreviousColumnsWidth = BudgetIncomeledger.GetAmountColumnsWidth - 20;
                    budgetstatistics.SetCurrentColumnsWidth = BudgetIncomeledger.GetAmountColumnsWidth;
                    budgetstatistics.SetLedgerNameColumnsWidth = BudgetIncomeledger.GetLedgerNameColumnsWidth;
                    budgetstatistics.BindBudgetStatistics();


                    //On 25/04/2023, For Sign details ----------------------------------------------------------------------------
                    xrTblSignFooter = AlignContentTable(xrTblSignFooter);
                    xrFooterRow3.Visible = xrFooterRowSign.Visible = true;
                    xrCellFooterSgin1.Text = string.Empty;
                    xrSubSignFooter.Visible = xrSubSignFooter.Visible = false;
                    xrTblSignFooter.WidthF = xrSubBudgetIncomeLedgers.WidthF + 3;
                    if (AppSetting.IS_CMF_CONGREGATION || AppSetting.IS_BSG_CONGREGATION) //On 28/02/2024, To set sign details for montfort pune
                    {
                        xrFooterRow3.Visible = xrFooterRowSign.Visible = false;
                        rptFooterSign.Visible = false;
                        (xrSubSignFooter.ReportSource as SignReportFooter).ShowApprovedSign = false;
                        this.ReportProperties.IncludeSignDetails = 1;
                        if (this.ReportProperties.IncludeSignDetails == 1)
                        {
                            rptFooterSign.Visible = true;
                            xrSubSignFooter.Visible = true;
                            (xrSubSignFooter.ReportSource as SignReportFooter).HideRole2 = true;
                            (xrSubSignFooter.ReportSource as SignReportFooter).Role2Width = (xrSubSignFooter.ReportSource as SignReportFooter).Role2Width + 45;
                            if ((AppSetting.HeadofficeCode.ToUpper() == "CMFNED" || AppSetting.HeadofficeCode.ToUpper() == "BSGESP") && Budget_Action == (Int32)BudgetAction.Approved)
                            {
                                (xrSubSignFooter.ReportSource as SignReportFooter).ShowApprovedSign = true;
                            }
                            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrSubBudgetIncomeLedgers.WidthF - 10;
                            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
                        }
                    }
                    //------------------------------------------------------------------------------------------------------------

                }
                else
                {
                    MessageRender.ShowMessage("Invalid Budget");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
        }

        private Int32 GetPreviousBudgetIdByProjectId(string projectids, DateTime SelectedDateFrom, DateTime SelectedDateTo)
        {
            Int32 rtn = 0;
            using (DataManager dataManager = new DataManager())
            {
                string PreviousbudgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.PreviousBudgetInfoByProjects);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn.ColumnName, projectids);
                dataManager.Parameters.Add(this.reportSetting1.ReportParameter.YEAR_FROMColumn, SelectedDateFrom);
                dataManager.Parameters.Add(this.reportSetting1.ReportParameter.YEAR_TOColumn, SelectedDateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                ResultArgs result = dataManager.FetchData(DAO.Data.DataSource.DataTable, PreviousbudgetInfo);

                if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                {
                    rtn = UtilityMember.NumberSet.ToInteger(result.DataSource.Table.Rows[0][reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName].ToString());
                }
            }

            return rtn;
        }
        #endregion
    }
}
