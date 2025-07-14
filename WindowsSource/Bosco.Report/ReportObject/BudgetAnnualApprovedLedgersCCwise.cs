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
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetAnnualApprovedLedgersCCwise : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        public BudgetAnnualApprovedLedgersCCwise()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            BindBudgetAnnualLedgerCCwise();
        }
        #endregion

        private void BindBudgetAnnualLedgerCCwise()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                    string.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project.Split(',').Length == 0 ||
                    string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre.Split(',').Length == 0)
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        BindBudgetApprovedCCLedgerCCwise();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    BindBudgetApprovedCCLedgerCCwise();
                    base.ShowReport();
                }
            }
        }

        private void BindBudgetApprovedCCLedgerCCwise()
        {
            try
            {
                float actualCodeWidth = xrcellCode.WidthF;
                bool isCapCodeVisible = true;

                this.BudgetName = ReportProperty.Current.BudgetName;
                //this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                //this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).ToShortDateString();
                this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString();

                if (this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false) > this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                {
                    this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString();
                }
                ReportProperty.Current.ReportTitle = "Budget " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                SetReportTitle();
                AssignBudgetDateRangeTitle();

                SetTitleWidth(xrtblHeader.WidthF - 5);
                setHeaderTitleAlignment();

                isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
                xrcellCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
                xrcellCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

                grpHeaderBudgetNature.SortingSummary.Enabled = true;
                grpHeaderBudgetNature.SortingSummary.FieldName = reportSetting1.BUDGET_LEDGER.BUDGET_TRANS_MODEColumn.ColumnName;
                grpHeaderBudgetNature.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpHeaderBudgetNature.SortingSummary.SortOrder = XRColumnSortOrder.Descending;

                if (this.ReportProperties.SortByLedger == 1)
                    Detail.SortFields.Add(new GroupField("LEDGER_NAME"));
                else
                    Detail.SortFields.Add(new GroupField("LEDGER_CODE"));


                if (this.ReportProperties.ShowByCostCentre == 0)
                {
                    grpHeaderCCName.GroupFields[0].FieldName = "";
                    grpHeaderCCName.Visible = false;
                }
                else
                {
                    grpHeaderCCName.GroupFields[0].FieldName = "COST_CENTRE_NAME";
                    grpHeaderCCName.Visible = true;
                }


                xrcellPrevHeaderApproved.Text = "Approved\r\nBudget - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-1).Year.ToString();
                xrcellHeaderPrevRealized.Text = "Realized\r\nBudget - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-1).Year.ToString();
                xrcellHeaderApproved.Text = "Approved\r\nBudget - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
                xrcellHeaderProposed.Text = "Proposed\r\nBudget - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();

                resultArgs = GetReportSource();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtBudgetLedgersCCWise = resultArgs.DataSource.Table;
                    string filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                              " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName + " <> 0 " +
                              " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                              ")";
                    dtBudgetLedgersCCWise.DefaultView.RowFilter = filter;
                    dtBudgetLedgersCCWise = dtBudgetLedgersCCWise.DefaultView.ToTable();

                    //To show only Budgeted Ledgers alone (based on settings)
                    //if (this.ReportProperties.IncludeAllLedger == 0)
                    //{
                    //    filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 " + 
                    //                 " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                    //                 " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName + " <> 0 " + 
                    //                 " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                    //                 " OR " + this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash + " OR " +
                    //                 this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts + " OR " +
                    //                 this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit + ")";
                    //    //On 22/01/2024, To show ledgers which are used in current year as well as previous year approved too
                    //    //On 19/03/2021, to filter only mapped Budget Ledger
                    //    //To show only Budgeted Ledgers alone (based on settings)
                    //    filter += " AND (" + this.reportSetting1.BUDGET_LEDGER.BUDGET_TRANS_MODEColumn.ColumnName + " <> ''" +
                    //               " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + " > 0)";
                    //    dtBudgetLedgersCCWise.DefaultView.RowFilter = filter;
                    //    dtBudgetLedgersCCWise = dtBudgetLedgersCCWise.DefaultView.ToTable();
                    //}

                    this.DataSource = dtBudgetLedgersCCWise;
                    this.DataMember = dtBudgetLedgersCCWise.TableName;
                }
                else
                {
                    this.DataSource = null;
                }

                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlCCBudgetRealization = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetAnnualCCApproved);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.AppSetting.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.AppSetting.YearTo);

                dataManager.Parameters.Add(this.ReportParameters.BCC_IDColumn, this.ReportProperties.ShowByCostCentre == 1 ? 1 : 0);

                //29/07/2021, to show ledger actual balance based on settings
                if (this.AppSetting.ShowBudgetLedgerActualBalance == "1") //Ledger Closing Balance without Journal Voucher
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "1");
                }
                else if (this.AppSetting.ShowBudgetLedgerActualBalance == "2") // Receipts Vocuehr & Payments Voucher separately
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "2");
                }
                else //Ledger Closing Balance with Journal Voucher
                {
                    dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, "0");
                }

                //On 30/01/2025 filter cost centre if cc budget enabled and cc is selected --------------------------------------------------------
                if (this.AppSetting.EnableCostCentreBudget == 1 && !string.IsNullOrEmpty(this.ReportProperties.CostCentre))
                {
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre);
                }
                //---------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlCCBudgetRealization);
            }

            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblCCCName = AlignContentTable(xrtblCCCName);
            xrTblCCName = AlignContentTable(xrTblCCName);
            xrtblBudget = AlignContentTable(xrtblBudget);
            xrTblBudgetNature = AlignContentTable(xrTblBudgetNature);
            xrTblFooterBudgetNature = AlignContentTable(xrTblFooterBudgetNature);
            xrTblGrandTotal = AlignGrandTotalTable(xrTblGrandTotal);

            if (this.DataSource != null)
            {
                DataTable dtBudgetDetails = this.DataSource as DataTable;
                bool records = dtBudgetDetails.Rows.Count > 0;
                grpHeaderBudgetNature.Visible = grpHeaderCCCName.Visible = grpFooterCCCName.Visible = records;
                grpFooterBudgetNature.Visible = records; // grpHeaderCCName.Visible =
                Detail.Visible = records;
                ReportFooter.Visible = records;
            }
            else
            {
                Detail.Visible = false;
                grpHeaderBudgetNature.Visible = grpHeaderCCCName.Visible = grpFooterCCCName.Visible = false;
                grpFooterBudgetNature.Visible = false; // grpHeaderCCName.Visible =
                Detail.Visible = false;
                ReportFooter.Visible = false;
            }
        }

        public virtual XRTable AlignHeaderTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;

                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;

                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;

                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ?
                                new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Bold) : new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Regular));
                }
            }
            return table;
        }

        private void grpHeaderTransMode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string stransmode = TransMode.CR.ToString();
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName) != null)
            {
                stransmode = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName).ToString();
                e.Cancel = (stransmode == TransMode.CR.ToString());
            }
        }

        private void xrCellBNTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                XRTableCell cell = (XRTableCell)sender;
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper() == TransMode.CR.ToString().ToUpper() ? "Income Total" : "Expenditure Total";
                cell.Text = budgetnature.ToString();
            }
        }

        private void xrCellNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION)
                {
                    (sender as XRTableCell).TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
            }
        }

        private void xrBudgetNature_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                string budgetnature = e.Value.ToString().ToUpper() == TransMode.CR.ToString().ToUpper() ? "INCOME" : "EXPENDITURE";
                e.Value = budgetnature.ToString().ToUpper();
            }
        }

        private void xrcellCCName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrcellCCName.Borders = BorderSide.All;
        }


        private double GetBalance(string fldname)
        {
            double rtn = 0;
            try
            {
                if (this.DataSource != null && !string.IsNullOrEmpty(fldname) &&
                    this.GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.COST_CENTRECATEGORY_IDColumn.ColumnName) != null)
                {
                    DataTable dtRpt = this.DataSource as DataTable;
                    Int32 cccategoryid = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.COST_CENTRECATEGORY_IDColumn.ColumnName).ToString());
                    string filterCR = reportSetting1.BUDGETVARIANCE.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransMode.CR.ToString() + "' AND " +
                                      reportSetting1.BUDGETVARIANCE.COST_CENTRECATEGORY_IDColumn.ColumnName + "=" + cccategoryid;
                    string filterDR = reportSetting1.BUDGETVARIANCE.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransMode.DR.ToString() + "' AND " +
                                      reportSetting1.BUDGETVARIANCE.COST_CENTRECATEGORY_IDColumn.ColumnName + "=" + cccategoryid;

                    double income = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + fldname + ")", filterCR).ToString());
                    double expense = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + fldname + ")", filterDR).ToString());
                    rtn = (income - expense);
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn;
        }

        private void xrcellPrevBNApprovedBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetBalance(reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrcellBNPrevReaslizedBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetBalance(reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrcellBNProposedBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetBalance(reportSetting1.BUDGETVARIANCE.PROPOSED_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrcellBNApprovedBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetBalance(reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }



    }
}
