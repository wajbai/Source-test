using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetDevelopmentalProjectDetails : Bosco.Report.Base.ReportHeaderBase
    {
        double SumOfBalance = 0;
        public BudgetDevelopmentalProjectDetails()
        {
            InitializeComponent();
        }

        public DataTable dtBudgetDevelopmentalProjects  = new DataTable();

        //public float SetLedgerNameColumnsWidth
        //{
        //    set
        //    {
        //        xrCapStatisticsType.WidthF = xrStatisticsType.WidthF = xrTotalCaption.WidthF =
        //         xrSummaryCaption.WidthF = xrSummaryCaption1.WidthF = xrTotalIncome.WidthF = xrTotalExpense.WidthF = xrTotalDifferenceCaption.WidthF = value;
        //    }
        //}

        //public float SetPreviousColumnsWidth
        //{
        //    set
        //    {
        //        xrCapPreviousYear.WidthF = xrPreviousYear.WidthF = xrPreviousSum.WidthF = xrCapProposed.WidthF = 
        //            xrCapProposedAmount.WidthF = xrProposedTotalIncome.WidthF = xrProposedTotalExpense.WidthF = xrProposedTotalDifference.WidthF = value;
        //    }
        //}

        //public float SetCurrentColumnsWidth
        //{
        //    set
        //    {
        //        xrCapCurrentYear.WidthF = xrCurrentYear.WidthF = xrCurrentSum.WidthF = xrCapApproved.WidthF =
        //            xrCapApprovedAmount.WidthF = xrApprovedTotalIncome.WidthF = xrApprovedTotalExpense.WidthF = xrApprovedTotalDifference.WidthF = value;
        //    }
        //}
                
        public void BindBudgetDevelopmentalProjects()
        {
            SumOfBalance = 0;

            xrlblIncomeTitle.Visible = xrtblHeader.Visible = xrtblGrpTotal.Visible = xrtblBudget.Visible = (settingProperty.ConsiderBudgetNewProject == 1);
            xrtblHeader1.Visible = xrtblBudget1.Visible = xrtblGrpTotal1.Visible = (settingProperty.ConsiderBudgetNewProject == 0);
            xrTblDevlopmentalRemarks.Visible = xrtblBudget.Visible;
                        
            //xrRowRemarks.Visible = (settingProperty.ConsiderBudgetNewProject == 0);
            xrtblHeader.TopF = xrtblHeader1.TopF=  (xrLblProjectTitle.HeightF);
            xrtblBudget.TopF = xrtblBudget1.TopF = 0;
            xrtblGrpTotal.TopF = xrtblGrpTotal1.TopF = 0;
            //xrTblDevlopmentalRemarks.TopF = xrtblHeader.HeightF;

            xrtblHeader.HeightF = 25;
            grpHeaderDevNewProject.HeightF = 25;

            grpBudgetFooter.HeightF =  grpHeaderDevNewProject.HeightF = PageHeader.HeightF =  25;
            Detail.HeightF = 20;
                        
            xrLblProjectTitle.Text = "New Projects";
            if (settingProperty.ConsiderBudgetNewProject == 1)
            {
                xrLblProjectTitle.Text = "Developmental Projects";
                xrcellCCDummary.WidthF += xrcellTotal.WidthF;
            }

            ResultArgs resultArgs = new ResultArgs();
            string budgetdevelopmentalprojects = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetDevelopmentalProjectDetailsByBudget);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.ReportBudgetNewProject.ACC_YEAR_IDColumn, settingProperty.AccPeriodId);
                dataManager.Parameters.Add(this.reportSetting1.ReportBudgetNewProject.BUDGET_IDColumn, this.ReportProperties.BudgetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetdevelopmentalprojects);
            }
            SetReportBorder();
            if (resultArgs.Success &&  resultArgs.DataSource.Table!=null)
            {
                dtBudgetDevelopmentalProjects = resultArgs.DataSource.Table;
                this.DataSource = dtBudgetDevelopmentalProjects;
                this.DataMember = dtBudgetDevelopmentalProjects.TableName;
                grpHeaderDevNewProject.SortingSummary.Enabled = true;
                grpHeaderDevNewProject.SortingSummary.FieldName = reportSetting1.ReportBudgetNewProject.SEQUENCE_NOColumn.ColumnName;
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }
        }

        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblHeader1 = AlignHeaderTable(xrtblHeader1);
            xrtblBudget = AlignContentTable(xrtblBudget);
            xrtblBudget1 = AlignContentTable(xrtblBudget1);
            xrTblBudgetCCDetails = AlignContentTable(xrTblBudgetCCDetails);
        }

        private void xrcellNewProjectDifference_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (settingProperty.ConsiderBudgetNewProject == 0)
            {
                double difference = 0;
                XRTableCell cell = sender as XRTableCell;

                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
                {
                    if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName) != null &&
                       GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName) != null)
                    {
                        double dProposedIncome = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_INCOME_AMOUNTColumn.ColumnName).ToString());
                        double dProposedExpenditure = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName).ToString());
                        difference = dProposedIncome - dProposedExpenditure;
                    }
                }
                SumOfBalance += difference;
                e.Value = UtilityMember.NumberSet.ToNumber(difference);
            }
        }

        private void xrcellTotal_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (settingProperty.ConsiderBudgetNewProject == 1)
            {
                double difference = 0;
                XRTableCell cell = sender as XRTableCell;

                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName) != null)
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
                SumOfBalance += difference;
                e.Value = UtilityMember.NumberSet.ToNumber(difference);
            }
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

        private double GetDevelopmentNewProjectSumAmount(string fldname)
        {
            double rnt = 0;
            if (this.DataSource != null)
            {
                DataTable dtBind = this.DataSource as DataTable;
                DataView dv = new DataView(dtBind);
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

        private void xrGrpSumProposedExpense_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrGrpSumProposedIncome_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrGrpSumProposedGN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.GN_HELP_PROPOSED_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrGrpSumProposedProvince_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrGrpSumProposedExpense1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.PROPOSED_EXPENSE_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrGrpSumProposedIncome1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetDevelopmentNewProjectSumAmount(reportSetting1.ReportBudgetNewProject.PROPOSED_INCOME_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrGrpSumDifference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = UtilityMember.NumberSet.ToNumber(SumOfBalance);
        }

        private void xrGrpSumProposedDifference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = UtilityMember.NumberSet.ToNumber(SumOfBalance);
        }

        private void xrcellRemarks_EvaluateBinding(object sender, BindingEventArgs e)
        {
           
        }

        private void xrRowRemarks_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.REMARKSColumn.ColumnName) != null)
            {
                e.Cancel = (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.REMARKSColumn.ColumnName).ToString().Trim() == "");
            }
        }
        
        private void xrcellRemarks_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrTableRow2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //(sender as XRTableRow).Visible = false;
            //e.Cancel = true;
        }

        private void xrtblBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //e.Cancel = true;            
            
        }

        private void grpHeaderDevNewProject_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrTableRow2.Visible = false;
            //grpHeaderDevNewProject.HeightF = 25;
        }

        private void xrCellDevelopmentalRemarks_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.REMARKSColumn.ColumnName) != null)
            {
                e.Value = "Remarks : " + GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.REMARKSColumn.ColumnName).ToString();
            }
        }

        private void xrtblBudget_AfterPrint(object sender, EventArgs e)
        {
          
        }

        //private void xrTblDevlopmentalRemarks_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    XRTable xrtbldevlopemtnaproject = sender as XRTable;
        //    if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.REMARKSColumn.ColumnName) != null)
        //    {
        //        xrtbldevlopemtnaproject.HeightF = xrtblBudget.HeightF;
        //    }
        //}

        private void grpHeaderRemarks_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.REMARKSColumn.ColumnName) != null)
            {
                e.Cancel = (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.REMARKSColumn.ColumnName).ToString() =="");
            }
        }

    }
}
