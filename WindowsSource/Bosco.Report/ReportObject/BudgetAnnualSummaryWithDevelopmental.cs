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
    public partial class BudgetAnnualSummaryWithDevelopmental: Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        double SumOfDifference = 0;
        Int32 GrpRowNumber = 1;
        public BudgetAnnualSummaryWithDevelopmental()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            GrpRowNumber = 1;
            SumOfDifference = 0;
            this.PageHeader.Visible = false;
            xrLblProjectTitle.Text = string.Empty;
            BindBudgetAnnualSummaryWithDevelopmental();
        }
        #endregion

        public void BindBudgetAnnualSummaryWithDevelopmental()
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
                    //On 20/04/2023, Load developmental/new project for concern budgets
                    if (SettingProperty.Current.CreateBudgetDevNewProjects == 1)
                    {
                        string budgetids = string.IsNullOrEmpty(ReportProperty.Current.Budget) ? "0" : ReportProperty.Current.Budget;
                        dataManager.Parameters.Add(reportSetting1.ReportBudgetNewProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, budgetids);
                    }
                    else
                        dataManager.Parameters.Add(reportSetting1.ReportBudgetNewProject.DEVELOPMENTAL_NEW_BUDGETIDColumn, "0");

                    dataManager.Parameters.Add(ReportParameters.DATE_FROMColumn, ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearFrom, false));
                    dataManager.Parameters.Add(ReportParameters.DATE_TOColumn, ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearTo, false));
                    dataManager.Parameters.Add(ReportParameters.BUDGET_IDColumn, ReportProperty.Current.Budget);
                    dataManager.Parameters.Add(ReportParameters.YEAR_FROMColumn, DateCalenderFrom);
                    dataManager.Parameters.Add(ReportParameters.YEAR_TOColumn, DateCalenderTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetannuaksummary);

                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtBudgetDetails = resultArgs.DataSource.Table;
                        this.DataSource = dtBudgetDetails;
                        this.DataMember = resultArgs.DataSource.Table.TableName; 
                        
                        BindSummaryFooter(dtBudgetDetails);

                        //grpHeaderBudgetExistingNewProject.GroupFields[0].FieldName = string.Empty;
                        //grpHeaderBudgetExistingNewProject.GroupFields[0].FieldName = reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName;
                        //grpHeaderBudgetExistingNewProject.GroupFields[0].SortOrder = XRColumnSortOrder.Descending;

                        //grpHeaderBudgetExistingNewProject.SortingSummary.Enabled = true;
                        //grpHeaderBudgetExistingNewProject.SortingSummary.FieldName = ReportParameters.PROJECTColumn.ColumnName; // GROUP_CODE
                        //grpHeaderBudgetExistingNewProject.SortingSummary.Function = SortingSummaryFunction.Avg;
                        //grpHeaderBudgetExistingNewProject.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                        
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

            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                if (existingbudget == 1)
                {
                    if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName) != null &&
                       GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName) != null)
                    {
                        double dProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName).ToString());
                        double dProposedExpense = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName).ToString());
                        difference = dProposedIncome - dProposedExpense;
                    }
                }
                else
                {
                    if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName) != null &&
                       GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName) != null)
                    {
                        double dProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName).ToString());
                        double dHOProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName).ToString());
                        double dGNProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName).ToString());
                        difference = dProposedIncome + dHOProposedIncome + dGNProposedIncome;
                    }
                }
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
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                XRLabel lblProjectTitle = sender as XRLabel;

                SumOfDifference = 0;
                lblProjectTitle.Text = (existingbudget == 1 ? "Existing Projects" : "Developmental Projects");
            }
        }

        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblHeader1 = AlignHeaderTable(xrtblHeader1);
            xrtblBudget = AlignContentTable(xrtblBudget);
            xrtblBudget1 = AlignContentTable(xrtblBudget1);
            xrTblBudgetCCDetails = AlignContentTable(xrTblBudgetCCDetails);
            xrtblGrpTotal = AlignGroupTable(xrtblGrpTotal);
            //xrTblSummary = AlignGrandTotalTable(xrTblSummary);         
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

                    //For New Projects
                    amtincome = amtexpense = 0;
                    xrTblSummaryNew.Visible = true;
                    double expectedincome = GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName) +
                                            GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName) +
                                            GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName);
                    amtincome = expectedincome;
                    dGrandIncome += amtincome;
                    xrCellSumaryPIncomeNewProject.Text = UtilityMember.NumberSet.ToNumber(amtincome);

                    amtexpense = GetDevelopmentNewProjectSumAmount(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName);
                    dGrandExpense += amtexpense;
                    xrCellSumaryPExpenseNewProject.Text = UtilityMember.NumberSet.ToNumber(amtexpense);
                    xrCellSumaryPDifferenceNewProject.Text = UtilityMember.NumberSet.ToNumber(amtincome - amtexpense);

                    if (amtincome == 0 && amtexpense == 0)
                    {
                        xrTblSummaryNew.Visible = false;
                    }

                    //Grand Total
                    xrCellGrandTotalPIncome.Text = UtilityMember.NumberSet.ToNumber(dGrandIncome);
                    xrCellGrandTotalPExpense.Text = UtilityMember.NumberSet.ToNumber(dGrandExpense);
                    xrCellGrandTotalPDifference.Text = UtilityMember.NumberSet.ToNumber(dGrandIncome - dGrandExpense);
                }
            }
        }

        private void xrcellHeaderProposalExpense_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                cell.Text = (existingbudget == 1 ? "Expenditure" : "Total Cost of the Project");
            }
        }

        private void xrcellHeaderProposalIncome_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                cell.Text = (existingbudget == 1 ? "Income" : "Own / Local Sources");
            }
        }

        private void xrcellHeaderParticular_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                cell.Text = (existingbudget == 1 ? "Particulars" : "Activity / Project");
            }
        }

        private void xrlblIncomeTitle_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                XRLabel lblincometitle = sender as XRLabel;
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                lblincometitle.Visible = (existingbudget == 1 ? false : true);
            }
        }

        private void xrHeaderRow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            //{
            //    Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());

            //    if (existingbudget == 1)
            //    {
            //        xrcellHeaderGovtHelp.Borders = xrcellGovtHelp.Borders = xrGrpSumProposedGN.Borders = xrcellHeaderRemarks.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;

            //        xrcellHeaderGovtHelp.WidthF = 0;
            //        xrcellHeaderProvinceHelp.WidthF = 0;
            //        xrcellHeaderRemarks.WidthF = 0;

            //        xrcellGovtHelp.WidthF = 0;
            //        xrcellProvinceHelp.WidthF = 0;
            //        xrcellRemarks.WidthF = 0;

            //        xrGrpSumProposedGN.WidthF = 0;
            //        xrGrpSumProposedProvince.WidthF = 0;
            //        xrGrpSumRemarks.WidthF = 0;
            //    }
            //    else
            //    {
            //        xrcellHeaderGovtHelp.Borders = xrcellGovtHelp.Borders = xrcellHeaderRemarks.Borders = xrGrpSumProposedGN.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;

                    
                    
            //        xrcellHeaderRemarks.WidthF = 140;
            //        xrcellHeaderIEDifference.WidthF = xrcellProposalIncome.WidthF;
            //        //xrcellHeaderGovtHelp.WidthF = xrcellProposalIncome.WidthF;
            //        //xrcellHeaderProvinceHelp.WidthF = xrcellProposalIncome.WidthF;
                    
            //        //xrcellProvinceHelp.WidthF = xrcellProposalIncome.WidthF;
            //        //xrcellRemarks.WidthF = 140;
            //        //xrcellGovtHelp.WidthF = xrcellProposalIncome.WidthF;

                    
            //        //xrGrpSumProposedProvince.WidthF = xrcellProposalIncome.WidthF;
            //        //xrGrpSumRemarks.WidthF = 140;
            //        ////xrGrpSumProposedGN.WidthF = xrcellProposalIncome.WidthF;
            //    }
            //}
        }

        private void xrGrpSumProposedGN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                if (existingbudget == 1)
                {
                    e.Result = "";
                    e.Handled = true;
                }
            }
        }

       
        private void HideColumnforExistingProjects(XRTableCell cell)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                if (existingbudget == 1)
                {
                    cell.WidthF = 0;
                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                }
                else
                {
                    cell.WidthF = xrcellHeaderProposalIncome.WidthF;
                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                }
            }
        }

        private void xrTableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double difference = 0;
            XRTableCell cell = sender as XRTableCell;

            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                if (existingbudget == 1)
                {
                    if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName) != null &&
                       GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName) != null)
                    {
                        double dProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName).ToString());
                        double dProposedExpense = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName).ToString());
                        difference = dProposedIncome - dProposedExpense;
                    }
                }
                else
                {
                    if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName) != null &&
                       GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName) != null)
                    {
                        double dProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName).ToString());
                        double dHOProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName).ToString());
                        double dGNProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName).ToString());
                        difference = dProposedIncome + dHOProposedIncome + dGNProposedIncome;
                    }
                }
            }

            SumOfDifference += difference;
            cell.Text = UtilityMember.NumberSet.ToNumber(difference);
        }

        private void grpHeaderBudgetExistingNewProject_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GrpRowNumber = 1;
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                if (existingbudget == 1)
                {
                    Detail.Visible = false;
                    xrtblHeader.Visible = false;
                    xrtblHeader1.Visible = true;
                    xrtblHeader1.TopF = 40;
                }
                else
                {
                    Detail.Visible = true;
                   
                    xrtblHeader.Visible = true;
                    xrtblHeader1.Visible = false;
                }
                (sender as GroupHeaderBand).HeightF = 65;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void grpFooterBudgetExistingNewProject_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
            
                if (existingbudget == 1)
                {
                    xrtblGrpTotal.Visible = false;
                    xrtblGrpTotal1.Visible = true;
                    xrtblGrpTotal1.TopF = 0;
                }
                else
                {
                    xrtblGrpTotal.Visible = true;
                    xrtblGrpTotal1.Visible = false;
                }
            }

            (sender as GroupFooterBand).HeightF = 65;
        }

        private void xrcellHeaderIEDifference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                cell.Text = (existingbudget == 1 ? "Difference" : "Total");
            }
        }

        private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = UtilityMember.NumberSet.ToNumber(SumOfDifference);
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
            {
                Int32 existingbudget = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.IS_EXISTING_PROJECTColumn.ColumnName).ToString());
                grpHeaderBudgetExistingNewProject.HeightF = 25;
                if (existingbudget == 1)
                {
                    xrtblBudget.Visible = false;
                    xrtblBudget1.Visible = true;
                    xrtblBudget1.TopF = xrtblBudget.TopF = 0;
                }
                else
                {
                    xrtblBudget.Visible = true;
                    xrtblBudget1.Visible = false;
                    xrtblBudget.TopF = 0;
                }
            }
            grpHeaderProject.HeightF = 25;
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
            if (xrtblBudget.Visible)
            {
                e.Value = GrpRowNumber;
                GrpRowNumber = GrpRowNumber + 1;
            }
        }

        private void xrTableCell10_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (xrtblBudget1.Visible)
            {
                e.Value = GrpRowNumber;
                GrpRowNumber = GrpRowNumber + 1;
            }
        }

        private void xrGrpSumProposedExpense_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName);
            e.Handled = true;
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

        private void xrGrpSumProposedIncome_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrGrpSumProposedGN_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrGrpSumProposedProvince_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

    }
}
