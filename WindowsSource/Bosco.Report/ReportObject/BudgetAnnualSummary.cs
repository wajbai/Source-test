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
    public partial class BudgetAnnualSummary : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        double SumOfDifference = 0;
        Int32 GrpRecordNumber = 1;
        public BudgetAnnualSummary()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            SumOfDifference = 0;
            GrpRecordNumber = 1;
            this.PageHeader.Visible = false;
            xrLblProjectTitle.Text = string.Empty;
            BindBudgetAnnualSummary();
        }
        #endregion
        
        public void BindBudgetAnnualSummary()
        {
            this.ReportProperties.DateFrom = ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearFrom);
            this.ReportProperties.DateTo = ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearTo);

            if (string.IsNullOrEmpty(this.ReportProperties.Budget) || this.ReportProperties.Budget.Split(',').Length == 0)
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        FetchBudgetAnnualSummary();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    FetchBudgetAnnualSummary();
                    base.ShowReport();
                }
            }
        }

        private void FetchBudgetAnnualSummary()
        {
            try
            {
                SetReportTitle();
                this.HideReportSubTitle = false;
                SetTitleWidth(xrtblHeader.WidthF);
                this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF;
                this.SetLandscapeHeader = xrtblHeader.WidthF;
                this.SetLandscapeFooter = xrtblHeader.WidthF;
                this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;
                setHeaderTitleAlignment();
              
                string budgetannuaksummary= this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetAnnualSummary);

                using (DataManager dataManager = new DataManager())
                {
                    //Date from must be calender date, it will cover, calender budget of this year
                    DateTime DateCalenderFrom = new DateTime(ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearFrom, false).Year, 1, 1);
                    DateTime DateCalenderTo = new DateTime(ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearTo, false).Year, 12, 31);

                    //show only current financcial year budgets, only for AB
                    if (AppSetting.IS_ABEBEN_DIOCESE)
                    {
                        DateCalenderFrom = ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearFrom, false);
                        DateCalenderTo = ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearTo, false);
                    }

                    dataManager.Parameters.Add(ReportParameters.ACC_YEAR_IDColumn, SettingProperty.Current.AccPeriodId);
                    dataManager.Parameters.Add(ReportParameters.DATE_FROMColumn, ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearFrom, false));
                    dataManager.Parameters.Add(ReportParameters.DATE_TOColumn, ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearTo, false));
                    dataManager.Parameters.Add(ReportParameters.BUDGET_IDColumn, ReportProperty.Current.Budget);
                    dataManager.Parameters.Add(ReportParameters.YEAR_FROMColumn, DateCalenderFrom);
                    dataManager.Parameters.Add(ReportParameters.YEAR_TOColumn, DateCalenderTo);
                    //On 20/04/2023, Load developmental/new project for concern budgets
                    if (SettingProperty.Current.CreateBudgetDevNewProjects == 1)
                    {
                        string budgetids = string.IsNullOrEmpty(ReportProperty.Current.Budget) ? "0" : ReportProperty.Current.Budget;
                        dataManager.Parameters.Add(reportSetting1.ReportBudgetNewProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, budgetids);
                    }
                    else
                        dataManager.Parameters.Add(reportSetting1.ReportBudgetNewProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, "0");

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetannuaksummary);
                                       
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtBudgetDetails = resultArgs.DataSource.Table;
                        this.DataSource = dtBudgetDetails;
                        this.DataMember = resultArgs.DataSource.Table.TableName; 
                        
                        BindSummaryFooter(dtBudgetDetails);
                        Detail.SortFields.Clear();
                        Detail.SortFields.Add(new GroupField(ReportParameters.PROJECTColumn.ColumnName));

       
                        xrtblBudget.Visible = xrTblSummary.Visible = xrtblGrpTotal.Visible = xrlblSummary.Visible = true;
                    }
                    else
                    {
                        xrtblBudget.Visible =  xrTblSummary.Visible = xrtblGrpTotal.Visible = xrlblSummary.Visible = false;
                        this.DataSource = null;
                    }
                }
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void xrcellIEDifference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double difference = 0;
            XRTableCell cell = sender as XRTableCell;
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName) != null &&
               GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName) != null)
            {
                double dProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName).ToString());
                double dProposedExpense = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName).ToString());
                difference = dProposedIncome - dProposedExpense;
            }
            SumOfDifference += difference;
            cell.Text = UtilityMember.NumberSet.ToNumber(difference);
        }

        private void xrGrpSumProposedDifference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = UtilityMember.NumberSet.ToNumber(SumOfDifference);
        }

        private void xrLblProjectTitle_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GrpRecordNumber = 1;
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                XRLabel lblProjectTitle = sender as XRLabel;

                SumOfDifference = 0;
                lblProjectTitle.Text = (existingbudget == 1 ? "Existing Projects" : "New Projects");
            }
        }

        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblBudget = AlignContentTable(xrtblBudget);
            xrtblGrpTotal = AlignGroupTable(xrtblGrpTotal);
            xrTblSummary = AlignGrandTotalTable(xrTblSummary);         
        }

        private void BindSummaryFooter(DataTable dtBudgetDetails)
        {
            dtBudgetDetails.DefaultView.RowFilter = reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName + " = 0";
            bool newprojectexists = (dtBudgetDetails.DefaultView.Count > 0);
            dtBudgetDetails.DefaultView.RowFilter = string.Empty;
            ReportFooter.Visible = newprojectexists;
            if (newprojectexists)
            {
                if (dtBudgetDetails.Rows.Count > 0)
                {
                    double dGrandIncome = 0;
                    double dGrandExpense = 0;
                    double dGrandProvinceHelp = 0;

                    //For Existing Projects 
                    double amtincome = UtilityMember.NumberSet.ToDouble(dtBudgetDetails.Compute("Sum(" + reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName + ")",
                                                            reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName + " = 1").ToString());
                    dGrandIncome = amtincome;
                    xrCellSumaryPIncomeExistingProject.Text = UtilityMember.NumberSet.ToNumber(amtincome);

                    double amtexpense = UtilityMember.NumberSet.ToDouble(dtBudgetDetails.Compute("Sum(" + reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + ")",
                                                            reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName + " = 1").ToString());
                    dGrandExpense = amtexpense;
                    xrCellSumaryPExpenseExistingProject.Text = UtilityMember.NumberSet.ToNumber(amtexpense);

                    xrCellSumaryPDifferenceExistingProject.Text = UtilityMember.NumberSet.ToNumber(amtincome - amtexpense);
                    
                    double amtprovincehelp = UtilityMember.NumberSet.ToDouble(dtBudgetDetails.Compute("Sum(" + reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + ")",
                                                            reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName + " = 1").ToString());
                    dGrandProvinceHelp = amtprovincehelp;
                    xrCellSumaryPProvinceHelpExistingProject.Text = UtilityMember.NumberSet.ToNumber(amtprovincehelp);

                    //For New Projects
                    amtincome = amtexpense = amtprovincehelp = 0;
                    xrTblSummaryNew.Visible = true;
                    amtincome = GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName);

                    dGrandIncome += amtincome;
                    xrCellSumaryPIncomeNewProject.Text = UtilityMember.NumberSet.ToNumber(amtincome);

                    amtexpense = GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName);

                    dGrandExpense += amtexpense;
                    xrCellSumaryPExpenseNewProject.Text = UtilityMember.NumberSet.ToNumber(amtexpense);

                    xrCellSumaryPDifferenceNewProject.Text = UtilityMember.NumberSet.ToNumber(amtincome - amtexpense);

                    amtprovincehelp = GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName);

                    dGrandProvinceHelp += amtprovincehelp;
                    xrCellSumaryPProvinceHelpNewProject.Text = UtilityMember.NumberSet.ToNumber(amtprovincehelp);

                    if (amtincome == 0 && amtexpense == 0 && amtprovincehelp == 0)
                    {
                        xrTblSummaryNew.Visible = false;
                    }

                    //Grand Total
                    xrCellGrandTotalPIncome.Text = UtilityMember.NumberSet.ToNumber(dGrandIncome);
                    xrCellGrandTotalPExpense.Text = UtilityMember.NumberSet.ToNumber(dGrandExpense);
                    xrCellGrandTotalPDifference.Text = UtilityMember.NumberSet.ToNumber(dGrandIncome - dGrandExpense);
                    xrCellGrandTotalPProvinceHelp.Text = UtilityMember.NumberSet.ToNumber(dGrandProvinceHelp);
                }
            }
        }

        private double GetDevelopmentNewProjectSumAmount(string fldname)
        {
            double rnt = 0;
            if (this.DataSource != null)
            {
                DataTable dtBind = this.DataSource as DataTable;
                DataView dv = new DataView(dtBind);
                dv.RowFilter = "IS_EXISTING_PROJECT=0";
                string fldsnames = reportSetting1.BUDGETVARIANCE.PROJECTColumn.ColumnName + "," +
                                    reportSetting1.ReportBudgetNewProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName + "," +
                                    reportSetting1.ReportBudgetNewProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName + "," +
                                    reportSetting1.ReportBudgetNewProject.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName + "," +
                                    reportSetting1.ReportBudgetNewProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName;

                string[] flds = fldsnames.Split(',');
                DataTable dtSum = dv.ToTable(true, flds);
                rnt = UtilityMember.NumberSet.ToDouble(dtSum.Compute("SUM(" + fldname + ")", "").ToString());
            }
            return rnt;
        }

        private void xrTblBudgetCCDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                XRTable tblccc = sender as XRTable;
                double ccamount = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_CC_EXPENSE_AMOUNTColumn.ColumnName).ToString());
                tblccc.Visible = (settingProperty.EnableCostCentreBudget==1 && ccamount > 0);
            }
        }

        private void xrcellSNo_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                e.Value = GrpRecordNumber;
                GrpRecordNumber++;
            }
        }

        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                //cell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
            }
        }

        private void xrGrpSumProposedIncome_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                if (existingbudget == 0)
                {
                    e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName);
                    e.Handled = true;
                }
            }
        }

        private void xrGrpSumProposedExpense_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                if (existingbudget == 0)
                {
                    e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName);
                    e.Handled = true;
                }
            }
        }

        private void xrGrpSumProposedHoHelp_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                if (existingbudget == 0)
                {
                    e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName);
                    e.Handled = true;
                }
            }
        }
    }
}
