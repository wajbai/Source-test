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
    public partial class BudgetAnnualRealizationLedgersCCwise : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        private int NoOfMonths = 1;
        public bool HeaderLoaded = false;

        public BudgetAnnualRealizationLedgersCCwise()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            BindBudgetAnnualMonthlyRealization();
        }
        #endregion

        private void BindBudgetAnnualMonthlyRealization()
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
                        BindBudgetCCRealization();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    BindBudgetCCRealization();
                    base.ShowReport();
                }
            }
        }

        private void BindBudgetCCRealization()
        {
            try
            {
                float actualCodeWidth = xrcellCode.WidthF;
                bool isCapCodeVisible = true;

                ReportProperty.Current.ReportTitle = "Budget Realization - " + this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false).Year.ToString();
                SetReportTitle();
                AssignBudgetDateRangeTitle();
                this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF - 5;
                this.SetLandscapeHeader = xrtblHeader.WidthF - 5;
                this.SetLandscapeFooter = xrtblHeader.WidthF - 5;
                this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF - 5;
                SetTitleWidth(xrtblHeader.WidthF - 5);
                setHeaderTitleAlignment();

                MakeCaption();

                //fix always sort by ledget name for cmf
                if (this.AppSetting.IS_CMF_CONGREGATION)
                {
                    this.FixReportPropertyForCMF();
                    xrcellGrandTotal.Text = xrcellGrandTotal.Text.ToUpper();
                }

                isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
                xrcellCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

                grpHeaderBudgetNature.SortingSummary.Enabled = true;
                grpHeaderBudgetNature.SortingSummary.FieldName = reportSetting1.BUDGET_LEDGER.BUDGET_TRANS_MODEColumn.ColumnName;
                grpHeaderBudgetNature.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpHeaderBudgetNature.SortingSummary.SortOrder = XRColumnSortOrder.Descending;

                if (this.ReportProperties.SortByLedger == 1)
                    Detail.SortFields.Add(new GroupField(this.reportSetting1.BUDGETVARIANCE.LEDGER_NAMEColumn.ColumnName));
                else
                    Detail.SortFields.Add(new GroupField(this.reportSetting1.BUDGETVARIANCE.LEDGER_CODEColumn.ColumnName));

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

                resultArgs = GetReportSource();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtRptBudgetSource = resultArgs.DataSource.Table;
                    SplitReportDatasourceQuter(dtRptBudgetSource);

                    string filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                                    " OR " + this.reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName + " <> 0 " + ")";
                    dtRptBudgetSource.DefaultView.RowFilter = filter;
                    dtRptBudgetSource = dtRptBudgetSource.DefaultView.ToTable();

                    //To show only Budgeted Ledgers alone (based on settings)
                    if (this.ReportProperties.IncludeAllLedger == 0)
                    {
                        filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 OR " + this.reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName + " > 0 OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash + " OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts + " OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit + ")";

                        dtRptBudgetSource.DefaultView.RowFilter = filter;
                        dtRptBudgetSource = dtRptBudgetSource.DefaultView.ToTable();
                    }

                    this.DataSource = dtRptBudgetSource;
                    this.DataMember = dtRptBudgetSource.TableName;
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
            string sqlCCBudgetRealization = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetCCRealizationUnderCC);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.AppSetting.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.AppSetting.YearTo);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
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
            xrTblGrandFooterTotal = AlignGrandTotalTable(xrTblGrandFooterTotal);

            float actualCodeWidth = xrcellCode.WidthF;
            bool isCapCodeVisible = true;
            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrcellCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            if (this.DataSource != null)
            {
                DataTable dtBudgetDetails = this.DataSource as DataTable;
                bool records = dtBudgetDetails.Rows.Count > 0;
                grpHeaderBudgetNature.Visible = grpHeaderCCCName.Visible = records; // grpHeaderCCName.Visible 27/02/2025
                grpFooterBudgetNature.Visible = grpFooterCCCName.Visible = grpFooterCCName.Visible = records;
                Detail.Visible = records;
                ReportFooter.Visible = records;
            }
            else
            {
                Detail.Visible = false;
                grpHeaderBudgetNature.Visible = grpHeaderCCCName.Visible = false; // = grpHeaderCCName.Visible 27/02/2025
                grpFooterBudgetNature.Visible = grpFooterCCCName.Visible = grpFooterCCName.Visible = false;
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

        /// <summary>
        /// Change Captions based on Budget
        /// </summary>
        private void MakeCaption()
        {
            NoOfMonths = this.UtilityMember.DateSet.GetDateDifferentInMonths(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false), this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false));
            xrcellCurrentHeaderApproved.Multiline = true;
            xrcellCurrentHeaderApproved.Text = "Approved\r\nBudget - " + this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();

            xrcellHeaderDueMonths.Text = "Due -" + NoOfMonths + "\r\nMonths";
            //xrcellHeaderRealized.Text = "Realized as on\r\n" + this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, true).ToShortDateString();
            xrcellHeaderRealized.Text = "Realized";
            int StartQuater = this.UtilityMember.DateSet.GetQuarter(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false), this.AppSetting.YearFrom);
            int EndQuater = this.UtilityMember.DateSet.GetQuarter(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false), this.AppSetting.YearFrom);
        }

        /// <summary>
        /// Split Report datasource based on FYs
        /// </summary>
        /// <param name="dtReport"></param>
        private void SplitReportDatasourceQuter(DataTable dtReport)
        {
            NoOfMonths = this.UtilityMember.DateSet.GetDateDifferentInMonths(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false), this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false));
            if (dtReport != null)
            {
                string colgroupid = reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName;
                string colannualapprovedamount = reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName;
                string annaulbugetQutersplit = "(" + colannualapprovedamount + "/12)";
                string annaulbugetDuesplit = "(" + colannualapprovedamount + "/12)*" + NoOfMonths;

                if (!dtReport.Columns.Contains(reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName))
                {
                    annaulbugetDuesplit = "IIF( (" + colgroupid + "=" + (Int32)FixedLedgerGroup.Cash + " OR " + colgroupid + "=" + (Int32)FixedLedgerGroup.BankAccounts + " OR "
                                            + colgroupid + "=" + (Int32)FixedLedgerGroup.FixedDeposit + ")," + colannualapprovedamount + ", (" + annaulbugetDuesplit + "))";
                    dtReport.Columns.Add(reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName, typeof(double), annaulbugetDuesplit);
                }

                if (!dtReport.Columns.Contains(reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName))
                {
                    string isFixedlegerGroups = "(" + reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash + " OR " +
                                                 reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts + " OR " +
                                                 reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit + ")";
                    //string difference = reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName + " - " + reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName;
                    string difference = reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName + " - " + reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName;

                    dtReport.Columns.Add(reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName, typeof(double), difference);
                }

            }
        }

        private void xrBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrBudgetNature_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.BUDGET_TRANS_MODEColumn.ColumnName) != null)
            {
                string budgetnature = e.Value.ToString().ToUpper() == TransMode.CR.ToString().ToUpper() ? "INCOME" : "EXPENDITURE";
                e.Value = budgetnature.ToString().ToUpper();
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

        private void xrcellCCName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrcellCCName.Borders = BorderSide.All;
        }

        private void xrcellApprovedBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetBalance(reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName);
            e.Handled = true;
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

        private void xrcellGDueMonthsBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetBalance(reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrcellRealizsedBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetBalance(reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName);
            e.Handled = true;
        }

        private void xrcellDifferenceBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetBalance(reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName);
            e.Handled = true;
        }
    }
}
