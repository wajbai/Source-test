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
    public partial class  BudgetAnnualBudgetBalanceSheetLedgers : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        private int NoOfMonths = 1;
        private TransMode BudgetTransSource = TransMode.CR;
        public bool HeaderLoaded = false;

        public Double GrandIncomeApprovedTotal = 0;
        public Double GrandIncomeDueTotal = 0;
        public Double GrandIncomeDifferenceTotal = 0;

        public Double GrandExpenseApprovedTotal = 0;
        public Double GrandExpenseDueTotal = 0;
        public Double GrandExpenseDifferenceTotal = 0;

        public BudgetAnnualBudgetBalanceSheetLedgers()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            BindBudgetAnnualLedger();
        }
        #endregion

        private void BindBudgetAnnualLedger()
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
                        //FetchBudgetDetails();
                        //base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    //FetchBudgetDetails();
                    //base.ShowReport();
                }
            }
        }

        public void BindBudgetDetails(DataTable dtBudgetLedgers, TransMode BudgetSource)
        {
            try
            {
                BudgetTransSource = BudgetSource;

                this.BudgetName = ReportProperty.Current.BudgetName;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.HideReportHeader = this.HidePageFooter = this.HidePageFooter = HidePageInfo = false;
                //this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).ToShortDateString();
                //this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString();

                //fix always sort by ledget name for cmf
                if (this.AppSetting.IS_CMF_CONGREGATION)
                {
                    this.FixReportPropertyForCMF();
                    xrcellGrandTotal.Text = xrcellGrandTotal.Text.ToUpper();
                }

                NoOfMonths = this.UtilityMember.DateSet.GetDateDifferentInMonths(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false), this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false));

                //SetReportTitle();
                //AssignBudgetDateRangeTitle();
                //SetTitleWidth(xrtblHeader.WidthF);
                //this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF;
                //this.SetLandscapeHeader = xrtblHeader.WidthF;
                //this.SetLandscapeFooter = xrtblHeader.WidthF;
                //this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;
                this.HideReportHeader = this.HidePageFooter = this.HidePageFooter = HidePageInfo = false;
                setHeaderTitleAlignment();

                grpHeaderLedgerGroup.SortingSummary.Enabled = true;
                grpHeaderLedgerGroup.SortingSummary.FieldName = "NATURE_ID";
                grpHeaderLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpHeaderLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;

                Detail.SortFields.Add(new GroupField("LEDGER_GROUP"));

                if (this.ReportProperties.SortByLedger == 1)
                    Detail.SortFields.Add(new GroupField("LEDGER_NAME"));
                else
                    Detail.SortFields.Add(new GroupField("LEDGER_CODE"));

                xrcellHeaderDueMonths.Multiline = true;
                xrcellHeaderDueMonths.Text = "Due -" + NoOfMonths + "\r\n Months";
                xrcellHeaderApproved.Text = "Approved\r\nBudget - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
                //xrcellHeaderRealized.Text = "Realized\r\n as on " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false).ToShortDateString();
                xrcellHeaderRealized.Text = "Realized" ;

                if (dtBudgetLedgers != null)
                {
                    AppendCashBankFDBalances(dtBudgetLedgers);
                                        
                    string filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                                   " OR " + this.reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName + " <> 0 " + ")";
                    dtBudgetLedgers.DefaultView.RowFilter = filter;
                    dtBudgetLedgers = dtBudgetLedgers.DefaultView.ToTable();

                    //To show only Budgeted Ledgers alone (based on settings)
                    if (this.ReportProperties.IncludeAllLedger == 0)
                    {
                        //filter = this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 OR (" +
                        //             this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash + " OR " +
                        //             this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts + " OR " +
                        //             this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit + ")";

                        filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash + " OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts + " OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit + ")";

                        //On 19/03/2021, to filter only mapped Budget Ledger
                        //To show only Budgeted Ledgers alone (based on settings)
                        filter += " AND " + this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName + "='" + BudgetSource + "'";

                        dtBudgetLedgers.DefaultView.RowFilter = filter;
                        dtBudgetLedgers = dtBudgetLedgers.DefaultView.ToTable();
                    }

                    //Assign Balance
                    if (BudgetTransSource == TransMode.CR)
                    {
                        GrandIncomeApprovedTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandIncomeDueTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandIncomeDifferenceTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName + ")", string.Empty).ToString());
                    }
                    else if (BudgetTransSource == TransMode.DR)
                    {
                        GrandExpenseApprovedTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandExpenseDueTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandExpenseDifferenceTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName + ")", string.Empty).ToString());
                    }

                    this.DataSource = dtBudgetLedgers;
                    this.DataMember = dtBudgetLedgers.TableName;
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


        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblBudget = AlignContentTable(xrtblBudget);
            xrtblBudgetNature = AlignContentTable(xrtblBudgetNature);
            xrTblFooterBN = AlignContentTable(xrTblFooterBN);
            xrTblProjectLedgerGroup = AlignContentTable(xrTblProjectLedgerGroup);
            xrTblHeaderLedgerGroup = AlignContentTable(xrTblHeaderLedgerGroup);
            xrTblFooterLedgerGroup = AlignContentTable(xrTblFooterLedgerGroup);
            xrTblGrandFooterTotal = AlignGrandTotalTable(xrTblGrandFooterTotal);

            if (this.DataSource != null)
            {
                DataTable dtBudgetDetails = this.DataSource as DataTable;
                bool records = dtBudgetDetails.Rows.Count > 0;
                grpHeaderBudgetNature.Visible = grpHeaderLedgerGroup.Visible = records;
                grpFooterTransgrpHeaderBudgetNature.Visible = grpFooterLedgerGroup.Visible = records;
                Detail.Visible = records;
                ReportFooter.Visible = records;
            }
            else
            {
                Detail.Visible = false;
                grpHeaderBudgetNature.Visible = grpHeaderLedgerGroup.Visible = false;
                grpFooterTransgrpHeaderBudgetNature.Visible = grpFooterLedgerGroup.Visible = false;
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
                        //if (count == 1)
                        //{
                        //    tcell.Borders = BorderSide.All;
                        //    if (ReportProperties.ShowLedgerCode != 1)
                        //    {
                        //        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        //    }
                        //}
                        //else
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
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


        // to align content tables
        public virtual XRTable AlignContentTable(XRTable table)
        {

            int j = table.Rows.Count;
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            //if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            //{
                            //    tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            //}
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = BorderSide.Left;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            return table;
        }

        /// <summary>
        /// Attach Cash, Bank and FD ledgers opening and closing
        /// 
        /// If HO is CMF, take all Projects  Cash, Bank and FD ledgers opening and closing
        /// </summary>
        /// <param name="dtReport"></param>
        private void AppendCashBankFDBalances(DataTable dtReport)
        {
            //if (this.ReportProperties.ShowDetailedBalance == 1)
            //{
                if (dtReport != null)
                {
                    //For Opening and Closing balances (Current FY and Previous Year)
                    if (BudgetTransSource == TransMode.CR)
                    {
                        //dtReport.Columns["AMOUNT_CR"].ColumnName = this.reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName;
                        if (dtReport.Rows.Count > 0)
                        {
                            AttachBalancesForBudget(dtReport, true, false, true);
                        }
                    }
                    else if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR) //AppendCashBankFDBalances(dtBudgetLedgers);
                    {
                        //dtReport.Columns["AMOUNT_DR"].ColumnName = this.reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName;
                        if (dtReport.Rows.Count > 0)
                        {
                            AttachBalancesForBudget(dtReport, false, false, true);
                        }
                    }

                    //For Budget Nature
                    string colgroupid = reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName;
                    DataColumn dcBudgetNature = new DataColumn(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName, typeof(System.String));
                    if (BudgetTransSource == TransMode.CR)
                    {
                        string budgetnature = "IIF( (" + colgroupid + "=" + (Int32)FixedLedgerGroup.Cash + " OR " + colgroupid + "=" + (Int32)FixedLedgerGroup.BankAccounts + " OR "
                                                + colgroupid + "=" + (Int32)FixedLedgerGroup.FixedDeposit + "),'OPENING BALANCE', 'INCOME')";
                        dcBudgetNature.Expression = budgetnature;
                    }
                    else
                    {
                        string budgetnature = "IIF( (" + colgroupid + "=" + (Int32)FixedLedgerGroup.Cash + " OR " + colgroupid + "=" + (Int32)FixedLedgerGroup.BankAccounts + " OR "
                                                + colgroupid + "=" + (Int32)FixedLedgerGroup.FixedDeposit + "),'CLOSING BALANCE', 'EXPENDITURE')";
                        dcBudgetNature.Expression = budgetnature;
                    }
                    dtReport.Columns.Add(dcBudgetNature);

                    //For Due Months
                    string colannualapprovedamount = reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName;
                    string annaulbugetDuesplit = "(" + colannualapprovedamount + "/12)*" + NoOfMonths;
                    if (!dtReport.Columns.Contains(reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName))
                    {
                        annaulbugetDuesplit = "IIF( (" + colgroupid + "=" + (Int32)FixedLedgerGroup.Cash + " OR " + colgroupid + "=" + (Int32)FixedLedgerGroup.BankAccounts + " OR "
                                                + colgroupid + "=" + (Int32)FixedLedgerGroup.FixedDeposit + ")," + colannualapprovedamount + ", (" + annaulbugetDuesplit + "))";
                        dtReport.Columns.Add(reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName, typeof(double), annaulbugetDuesplit);
                    }


                    //For Difference
                    string isFixedlegerGroups = "(" + reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash + " OR " +
                                                  reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts + " OR " +
                                                  reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit + ")";
                    //string difference = reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName + " - " + reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName;
                    string difference = reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName + " - " + reportSetting1.BUDGETVARIANCE.DUE_MONTHS_APPROVED_AMOUNTColumn.ColumnName;

                    //difference = "IIF(" + isFixedlegerGroups + ", (" + reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + "), " + difference + ")";
                    dtReport.Columns.Add(reportSetting1.BUDGETVARIANCE.DIFFERENCEColumn.ColumnName, typeof(System.Double), difference).DefaultValue = 0;
                }
            //}
        }

       
        private void xrcellLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = cell.Text.Trim();
        }

        private void xrcellTransMode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetnature = label.Text;
            label.Text = budgetnature.ToString().ToUpper();

            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                //string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }


            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        private void grpProjectLGHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                    e.Cancel = true;
                else
                    e.Cancel = false;
            }
        }

        private void xrcellFooterLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (activegroupid == ((int)FixedLedgerGroup.Cash) )
                        cell.Text = "Total Cash";
                    else if (activegroupid == (int)FixedLedgerGroup.BankAccounts) 
                        cell.Text = "Total Bank";
                    else if (activegroupid == (int)FixedLedgerGroup.FixedDeposit)
                        cell.Text = "Total FD";
                    else
                        cell.Text = "Sub Group Total";
                }
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //e.Cancel = (BudgetTransSource == TransMode.DR && !HeaderLoaded);
            //HeaderLoaded = true;

            e.Cancel = (BudgetTransSource == TransMode.DR);
            //HeaderLoaded = true;
        }

        private void xrcellFooterSumApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrcellFooterSumDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    //if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeDifferenceTotal - GrandExpenseDifferenceTotal;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellGSumApprovedAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
           
        }

        private void xrcellGSumDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrcellFooterSumDueMonths_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellGSumDueMonths_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void grpHeaderLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            //{
            //    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
            //    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
            //        e.Cancel = true;
            //    else
            //        e.Cancel = false;
            //}
        }

        private void grpFooterLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            //{
            //    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
            //    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
            //        e.Cancel = true;
            //    else
            //        e.Cancel = false;
            //}
        }

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                        cell.Text = (BudgetTransSource == TransMode.CR ? "Total Opening Balance" : "Total Closing Balance");
                    else
                        cell.Text = "Total";
                }
            }
        }

        private void grpFooterTransgrpHeaderBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        private void xrcellFooterBNApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeApprovedTotal - GrandExpenseApprovedTotal;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellFooterBNDueMonths_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeDueTotal - GrandExpenseDueTotal;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellFooterBNDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeDifferenceTotal - GrandExpenseDifferenceTotal;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellGSumApprovedAmount_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        double closingbalance = GrandIncomeApprovedTotal - GrandExpenseApprovedTotal;
                        e.Result = GrandExpenseApprovedTotal + closingbalance;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellGSumDifference_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        double closingbalance = GrandIncomeDifferenceTotal - GrandExpenseDifferenceTotal;
                        e.Result = GrandExpenseDifferenceTotal + closingbalance;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellGSumDueMonths_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        double closingbalance = GrandIncomeDueTotal - GrandExpenseDueTotal;
                        e.Result = GrandExpenseDueTotal + closingbalance;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNApproved_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

        private void xrcellBNDueMonths_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

        private void xrcellBNRealized_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

        private void xrcellBNDifference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                XRTableCell cell = sender as XRTableCell;
                cell.BackColor = Color.Silver;
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    cell.BackColor = Color.Transparent;
                }
            }
        }

        private void xrcellBNApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeApprovedTotal - GrandExpenseApprovedTotal;
                        e.Handled = true;
                    }
                }

                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
                {
                    string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                    if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNDueMonths_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeDueTotal - GrandExpenseDueTotal;
                        e.Handled = true;
                    }
                }

                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
                {
                    string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                    if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeDifferenceTotal - GrandExpenseDifferenceTotal;
                        e.Handled = true;
                    }
                }

                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
                {
                    string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                    if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNRealized_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
                {
                    string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                    if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                    {
                        e.Result = "";
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
                {
                    string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                    XRTableCell cell = sender as XRTableCell;
                    cell.BackColor = Color.Silver;
                    if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                    {
                        cell.BackColor = Color.Transparent;
                    }
                }
            }
        }

        private void xrtblBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrcellTransMode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    //if (AppSetting.IS_CMF_CONGREGATION)
                    //{
                    //    xrcellTransMode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    //}
            
                    if (xrBNHeaderRow.Cells.Contains(xrcellBNApproved))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellBNApproved);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellBNDueMonths))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellBNDueMonths);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellBNRealized))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellBNRealized);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellBNDifference))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellBNDifference);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellBNNote))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellBNNote);
                    }
                }
            }
        }

       

       
    }
}
