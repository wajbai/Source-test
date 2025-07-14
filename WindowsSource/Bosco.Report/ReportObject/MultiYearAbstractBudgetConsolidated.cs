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
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class MultiYearAbstractBudgetConsolidated : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        
        double Y2TotalReceipts = 0;
        double Y1TotalReceipts = 0;
        double CYTotalReceipts = 0;
        double TotalReceipts = 0;
        
        double Y2TotalPayments = 0;
        double Y1TotalPayments = 0;
        double CYTotalPayments = 0;
        double TotalPayments = 0;

        double Y2TurnOver = 0;
        double Y1TurnOver = 0;
        double CYTurnOver = 0;
        double TurnOver = 0;
        #endregion

        #region Constructor

        public MultiYearAbstractBudgetConsolidated()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(settingProperty.YearFrom, false).ToShortDateString();
            this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(settingProperty.YearTo, false).ToShortDateString();
            
            if ( this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.Project) ||
                 this.ReportProperties.Budget == "0" || string.IsNullOrEmpty(this.ReportProperties.Budget))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindReportSource();
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    BindReportSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }

        }

        #endregion

        #region Methods

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindReportSource()
        {
            string filter = string.Empty;
            Y2TurnOver = 0;
            Y1TurnOver = 0;
            TurnOver = 0;
            this.SetLandscapeHeader = xrTableHeader.WidthF;
            this.SetLandscapeFooter = xrTableHeader.WidthF;
            this.SetLandscapeFooterDateWidth = xrTableHeader.WidthF;
            ReportProperty.Current.ProjectTitle = ReportProperties.BudgetName;
            SetReportTitle();
            
            ResultArgs resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataTable dtMultiAbstractBudget = resultArgs.DataSource.Table;
                if (dtMultiAbstractBudget != null)
                {
                    dtMultiAbstractBudget.DefaultView.RowFilter = "BUDGET_TRANS_MODE IN ('CR','DR')";
                    dtMultiAbstractBudget = dtMultiAbstractBudget.DefaultView.ToTable();
                    dtMultiAbstractBudget.TableName = "MonthlyAbstractAbstract";
                    this.DataSource = dtMultiAbstractBudget;
                    this.DataMember = dtMultiAbstractBudget.TableName;
                    
                    //Receipts --- Caluculte Total Recepts and Opening Balance
                    //Payments --- Caluculte Total Payments and Opening Balance                    
                    
                    filter = reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransactionMode.CR.ToString() + "'";
                    Y2TotalReceipts= UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    Y1TotalReceipts = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    CYTotalReceipts = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + ")", filter).ToString());
                    TotalReceipts = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName + ")", filter).ToString());

                    filter = reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransactionMode.DR.ToString() + "'";
                    Y2TotalPayments= UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    Y1TotalPayments = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", filter).ToString());
                    CYTotalPayments = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + ")", filter).ToString());
                    TotalPayments = UtilityMember.NumberSet.ToDouble(dtMultiAbstractBudget.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName + ")", filter).ToString()); 
                                        
                    SetReportBorder();
                }
            }
            SortByLedgerorGroup();
        }

        private ResultArgs GetReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlMultiYearAbstractBudget = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.MultiAbstractBudgetConsolidated);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.BUDGET_IDColumn, this.ReportProperties.Budget);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiYearAbstractBudget);
            }

            return resultArgs;
        }

        private void SetReportBorder()
        {
            xrTableHeader = AlignHeaderTable(xrTableHeader);
            xrTblBudget = AlignContentTable(xrTblBudget);
            xrTableBGGroup = AlignContentTable(xrTableBGGroup);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrTblTurnOver = AlignGrandTotalTable(xrTblTurnOver);

            this.SetCurrencyFormat(tcCapPY1Amount1.Text, tcCapPY1Amount1);
            this.SetCurrencyFormat(tcCapPY2Amount1.Text, tcCapPY2Amount1);
            this.SetCurrencyFormat(tcCapCYAmount1.Text, tcCapCYAmount1);
            this.SetCurrencyFormat(tcCapBudgetPropsed1.Text, tcCapBudgetPropsed1);

            tcCapPY2Amount.Text = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-2).Year.ToString() + "-" +
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).AddYears(-2).Year.ToString();
            tcCapPY1Amount.Text = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-1).Year.ToString() + "-" + 
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).AddYears(-1).Year.ToString();
            tcCapCYAmount.Text = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year.ToString() + "-" +
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Year.ToString();
            tcCapBudgetPropsed.Text = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year.ToString() + "-" +
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Year.ToString();
        }

        public override XRTable AlignHeaderTable(XRTable table, bool UseSameFont = false)
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
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Top;
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                        }
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? this.styleColumnHeader.Font : new Font(this.styleColumnHeader.Font, FontStyle.Regular));
                }
            }
            return table;
        }

        private void SortByLedgerorGroup()
        {
            if (grpHeaderBGGroup.Visible)
            {
                grpHeaderBGGroup.SortingSummary.Enabled = true;
                grpHeaderBGGroup.SortingSummary.FieldName = reportSetting1.ProfitandLossbyHouse.BUDGET_GROUP_SORT_IDColumn.ColumnName;
                grpHeaderBGGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpHeaderBGGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }

            Detail.SortFields.Clear();
            if (this.ReportProperties.SortByLedger == 0)
            {
                Detail.SortFields.Add(new GroupField("LEDGER_CODE", XRColumnSortOrder.Ascending));
                Detail.SortFields.Add(new GroupField("LEDGER_NAME", XRColumnSortOrder.Ascending));
            }
            else
            {
                Detail.SortFields.Add(new GroupField("LEDGER_NAME", XRColumnSortOrder.Ascending));
            }

            //grpHeaderBGGroup.SortingSummary.Enabled = true;
            //grpHeaderBGGroup.SortingSummary.FieldName = reportSetting1.ProfitandLossbyHouse.BUDGET_GROUP_SORT_IDColumn.ColumnName;
            //grpHeaderBGGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
            //grpHeaderBGGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
        }

        private void BindBudgetDifference()
        {
            tcDiffAmount.Text = tcDiffPY1Amount.Text = tcDiffPY2Amount.Text = "0.0";
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_IDColumn.ColumnName) !=null)
            {
                Int32 BudgetId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_IDColumn.ColumnName).ToString());
                string TransMode = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString();
                if (this.DataSource != null && BudgetId > 0 && TransMode.ToUpper() ==  TransactionMode.CR.ToString().ToUpper())
                {
                    string Recurring_NonRecurringExpense = "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.RecurringExpenses as Enum).ToString() + "'," +
                                        "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.NonRecurringExpenses as Enum).ToString() + "'";
                    string Recurring_NonRecurringIncome = "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.RecurringIncome as Enum).ToString() + "'," +
                                        "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.NonRecurringIncome as Enum).ToString() + "'";
                    
                    string fitlerCR = "(" + reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransSource.Cr.ToString().ToUpper() + "' AND " +
                                    reportSetting1.MonthlyAbstract.BUDGET_IDColumn.ColumnName + " = " + BudgetId + " AND " +
                                    reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName + " IN (" + Recurring_NonRecurringIncome + "," + Recurring_NonRecurringExpense + "))";

                    string fitlerDR = "(" + reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransSource.Dr.ToString().ToUpper() + "' AND " +
                                    reportSetting1.MonthlyAbstract.BUDGET_IDColumn.ColumnName + " = " + BudgetId + " AND " +
                                    reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName + " IN (" + Recurring_NonRecurringIncome + "," + Recurring_NonRecurringExpense + "))";
                                    
                    DataTable dtRptData = this.DataSource as DataTable;

                    double diffamt = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName +")", fitlerCR).ToString()) - 
                                     UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName +")", fitlerDR).ToString()));
                    
                    double diffamtCY = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + ")", fitlerCR).ToString()) -
                                        UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + ")", fitlerDR).ToString()));

                    double diffamtPY2 = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) -
                                        UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", fitlerDR).ToString()));

                    double diffamtPY1 = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) -
                                        UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", fitlerDR).ToString()));
                    tcDiffAmount.Text = UtilityMember.NumberSet.ToNumber(diffamt);
                    tcDiffCYAmount.Text = UtilityMember.NumberSet.ToNumber(diffamtCY);
                    tcDiffPY1Amount.Text = UtilityMember.NumberSet.ToNumber(diffamtPY1);
                    tcDiffPY2Amount.Text = UtilityMember.NumberSet.ToNumber(diffamtPY2);
                }
            }
        }

        private void BindBudgetSurplus()
        {
            tcSurplusAmount.Text = tcSurplusCYAmount.Text = tcSurplusPY1Amount.Text = tcSurplusPY2Amount.Text = "0.0";
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_IDColumn.ColumnName) != null)
            {

                string TransMode = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString();
                if (this.DataSource != null && TransMode.ToUpper() == TransactionMode.CR.ToString().ToUpper())
                {
                    string fitlerCR = reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransSource.Cr.ToString() + "'";
                    string fitlerDR = reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransSource.Dr.ToString() + "'";
                    DataTable dtRptData = this.DataSource as DataTable;

                    double diffamt = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) -
                                     UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName + ")", fitlerDR).ToString()));

                    double diffamtCY = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + ")", fitlerCR).ToString()) -
                                        UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + ")", fitlerDR).ToString()));

                    double diffamtPY2 = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) -
                                        UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", fitlerDR).ToString()));

                    double diffamtPY1 = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) -
                                        UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", fitlerDR).ToString()));
                    tcSurplusAmount.Text = UtilityMember.NumberSet.ToNumber(diffamt);
                    tcSurplusCYAmount.Text = UtilityMember.NumberSet.ToNumber(diffamtCY);
                    tcSurplusPY1Amount.Text = UtilityMember.NumberSet.ToNumber(diffamtPY1);
                    tcSurplusPY2Amount.Text = UtilityMember.NumberSet.ToNumber(diffamtPY2);
                }
            }
        }
        #endregion

        #region Events
        
      

        private void tcGTotalPY2Amount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper() == TransactionMode.CR.ToString().ToUpper())
                {
                    e.Result = Y2TotalReceipts ;
                }
                else
                {
                    e.Result = Y2TotalPayments ;
                }
                e.Handled = true;
            }
        }

        private void tcGTotalPY1Amount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper() == TransactionMode.CR.ToString().ToUpper())
                {
                    e.Result = Y1TotalReceipts;
                }
                else
                {
                    e.Result = Y1TotalPayments;
                }
                e.Handled = true;
            }
        }

        private void tcGTotalBudgetPropsed_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper() == TransactionMode.CR.ToString().ToUpper())
                {
                    e.Result = TotalReceipts;
                }
                else
                {
                    e.Result = TotalPayments;
                }
                e.Handled = true;
            }
        }

        private void tcBudgetGroupTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_GROUPColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                cell.Text = "Total - " + GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_GROUPColumn.ColumnName).ToString();
            }
        }
        #endregion

        private void grpFooterBudget_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper()
                     == TransactionMode.CR.ToString().ToUpper())
                {
                    BindBudgetDifference();
                }
            }
        }

        private void grpFooterBGTransMode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper()
                     == TransactionMode.CR.ToString().ToUpper())
                {
                    BindBudgetSurplus();
                }
            }
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tcTurnoverAmount.Text = "0.0";
            tcTurnoverCPYAmount.Text = "0.0";
            tcTurnoverPY1Amount.Text = "0.0";
            tcTurnoverPY2Amount.Text = "0.0";
            
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_IDColumn.ColumnName) != null)
            {
                string TransMode = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString();
                if (this.DataSource != null)
                {
                    string Recurring_NonRecurringExpense = "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.RecurringExpenses as Enum).ToString() + "'," +
                                        "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.NonRecurringExpenses as Enum).ToString() + "'";
                    string Recurring_NonRecurringIncome = "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.RecurringIncome as Enum).ToString() + "'," +
                                        "'" + UtilityMember.EnumSet.GetDescriptionFromEnumValue(BudgetGroup.NonRecurringIncome as Enum).ToString() + "'";

                    string fitlerCR = "(" + reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransSource.Cr.ToString().ToUpper() + "' AND " +
                                    reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName + " IN (" + Recurring_NonRecurringIncome + "," +  Recurring_NonRecurringExpense + "))";

                    string fitlerDR = "(" + reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + TransSource.Dr.ToString().ToUpper() + "' AND " +
                                    reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName + " IN (" + Recurring_NonRecurringIncome + "," + Recurring_NonRecurringExpense + "))";

                    DataTable dtRptData = this.DataSource as DataTable;

                    //TurnOver = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) -
                    //                 UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName + ")", fitlerDR).ToString()));
                    TurnOver = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.BUDGET_PROPOSED_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()));

                    CYTurnOver = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.AMOUNT_PERIODColumn.ColumnName + ")", fitlerCR).ToString()));

                    //Y2TurnOver = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) -
                    //                    UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", fitlerDR).ToString()));
                    Y2TurnOver = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR2_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) );

                    //Y1TurnOver = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) -
                    //                    UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", fitlerDR).ToString()));

                    Y1TurnOver = (UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.MonthlyAbstract.PREVIOUS_YEAR1_AMOUNTColumn.ColumnName + ")", fitlerCR).ToString()) );

                    tcTurnoverAmount.Text = UtilityMember.NumberSet.ToNumber(TurnOver);
                    tcTurnoverCPYAmount.Text = UtilityMember.NumberSet.ToNumber(CYTurnOver);
                    tcTurnoverPY1Amount.Text = UtilityMember.NumberSet.ToNumber(Y1TurnOver);
                    tcTurnoverPY2Amount.Text = UtilityMember.NumberSet.ToNumber(Y2TurnOver);
                }
            }
        }

        private void xrcellTransMode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrcellTransMode.Text = string.Empty;
            xrRowDifference.Visible = false;
            xrRowSurplus.Visible = false;
            //xrTblCLTitle.Visible = false;
            //xrTblClosingBalance.Visible = false;
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper()
                     == TransactionMode.CR.ToString().ToUpper())
                {
                    xrcellTransMode.Text = "INCOME";
                    xrRowDifference.Visible = true;
                    xrRowSurplus.Visible = true;
                }
                else if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper()
                     == TransactionMode.DR.ToString().ToUpper())
                {
                    xrcellTransMode.Text = "EXPENDITURE";
                }
            }
        }

        private void tcGTotalCYAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName).ToString().ToUpper() == TransactionMode.CR.ToString().ToUpper())
                {
                    e.Result = CYTotalReceipts;
                }
                else
                {
                    e.Result = CYTotalPayments;
                }
                e.Handled = true;
            }
        }
              
    }
}
