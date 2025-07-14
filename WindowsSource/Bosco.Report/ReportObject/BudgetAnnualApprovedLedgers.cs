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
    public partial class BudgetAnnualApprovedLedgers : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        private int NoOfMonths = 1;
        public bool HeaderLoaded = false;
        public Double GrandIncomePreApprovedTotal = 0;
        public Double GrandIncomeProposedTotal = 0;
        public Double GrandIncomeApprovedTotal = 0;

        public Double GrandExpensePreApprovedTotal = 0;
        public Double GrandExpenseProposedTotal = 0;
        public Double GrandExpenseApprovedTotal = 0;

        private TransMode BudgetTransSource = TransMode.CR;

        private DataTable dtCCDetails = new DataTable();
        private bool PrevLedgerCCFound = false;
        public BudgetAnnualApprovedLedgers()
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
                        //BindBudgetDetails();
                        //base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    //BindBudgetDetails();
                    //base.ShowReport();
                }
            }
        }

        public void BindBudgetDetails(DataTable dtBudgetLedgers, TransMode BudgetSource)
        {
            try
            {
                float actualCodeWidth = xrcellHeaderCode.WidthF;
                bool isCapCodeVisible = true;

                this.BudgetName = ReportProperty.Current.BudgetName;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                BudgetTransSource = BudgetSource;

                //SetReportTitle();
                //AssignBudgetDateRangeTitle();
                //SetTitleWidth(xrtblHeader.WidthF);

                //to get CC detail for Budget, it will be used to when reports generates
                xrSubreportCCDetails.Visible = false;
                if (this.ReportProperties.ShowCCDetails == 1)
                {
                    xrSubreportCCDetails.Visible = true;
                    AssignCCDetailReportSource();
                }

                this.HideReportHeader = this.HidePageFooter = this.HidePageFooter = HidePageInfo = false;
                setHeaderTitleAlignment();
                this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).ToShortDateString();
                this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString();

                if (this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false) > this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                {
                    this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString();
                }
                //fix always sort by ledget name for cmf
                if (this.AppSetting.IS_CMF_CONGREGATION)
                {
                    this.FixReportPropertyForCMF();
                    xrcellGrandTotal.Text = xrcellGrandTotal.Text.ToUpper();
                }

                isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
                xrcellCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
                xrcellHeaderCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

                grpHeaderLedgerGroup.SortingSummary.Enabled = true;
                grpHeaderLedgerGroup.SortingSummary.FieldName = "NATURE_ID";
                grpHeaderLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpHeaderLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;

                if (this.ReportProperties.SortByLedger == 1)
                    Detail.SortFields.Add(new GroupField("LEDGER_NAME"));
                else
                    Detail.SortFields.Add(new GroupField("LEDGER_CODE"));

                xrcellPrevHeaderApproved.Text = "Approved\r\nBudget - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-1).Year.ToString();
                xrcellHeaderPrevRealized.Text = "Realized\r\nBudget - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-1).Year.ToString();
                xrcellHeaderApproved.Text = "Approved\r\nBudget - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
                xrcellHeaderProposed.Text = "Proposed\r\nBudget - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();

                if (dtBudgetLedgers != null)
                {

                    /*if (BudgetTransSource == TransMode.CR)
                    {
                        dtBudgetLedgers.Columns["AMOUNT_CR"].ColumnName = this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName;
                    }
                    else
                    {
                        dtBudgetLedgers.Columns["AMOUNT_DR"].ColumnName = this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName;
                    }*/

                    if (dtBudgetLedgers.Rows.Count > 0)
                    {
                        AppendCashBankFDBalances(dtBudgetLedgers);
                    }

                    string filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                              " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName + " <> 0 " +
                              " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                              ")";
                    dtBudgetLedgers.DefaultView.RowFilter = filter;
                    dtBudgetLedgers = dtBudgetLedgers.DefaultView.ToTable();

                    //To show only Budgeted Ledgers alone (based on settings)
                    if (this.ReportProperties.IncludeAllLedger == 0)
                    {
                        filter = "(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                                     " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                                     " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName + " <> 0 " +
                                     " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + " > 0 " +
                                     " OR " + this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash + " OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts + " OR " +
                                     this.reportSetting1.Receipts.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit + ")";

                        //On 22/01/2024, To show ledgers which are used in current year as well as previous year approved too
                        //On 19/03/2021, to filter only mapped Budget Ledger
                        //To show only Budgeted Ledgers alone (based on settings)
                        filter += " AND (" + this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName + "='" + BudgetSource + "'" +
                                   " OR " + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + " > 0)";
                        dtBudgetLedgers.DefaultView.RowFilter = filter;
                        dtBudgetLedgers = dtBudgetLedgers.DefaultView.ToTable();
                    }

                    if (BudgetTransSource == TransMode.CR)
                    {
                        GrandIncomePreApprovedTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandIncomeProposedTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.PROPOSED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandIncomeApprovedTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                    }
                    else if (BudgetTransSource == TransMode.DR)
                    {
                        GrandExpensePreApprovedTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandExpenseProposedTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.PROPOSED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                        GrandExpenseApprovedTotal = UtilityMember.NumberSet.ToDouble(dtBudgetLedgers.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
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

        private void ShowCCDetails()
        {
            //On 25/02/2021, To show CC detail for given Ledger
            if (this.ReportProperties.ShowCCDetails == 1)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null && dtCCDetails.Rows.Count > 0)
                {
                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName).ToString());

                    BudgetAnnualApprovedCC ccDetail = xrSubreportCCDetails.ReportSource as BudgetAnnualApprovedCC;
                    dtCCDetails.DefaultView.RowFilter = string.Empty;
                    dtCCDetails.DefaultView.RowFilter = reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName + " = " + ledgerid + " AND " +
                                                    reportSetting1.MonthlyAbstract.BUDGET_TRANS_MODEColumn.ColumnName + " = '" + BudgetTransSource.ToString() + "'";
                    dtCCDetails.DefaultView.Sort = reportSetting1.MonthlyAbstract.COST_CENTRE_NAMEColumn.ColumnName;
                    DataTable dtCC = dtCCDetails.DefaultView.ToTable();

                    ccDetail.BindCCDetails(dtCC);
                    ccDetail.CCTableWidth = xrtblBudget.WidthF;
                    ccDetail.CodeWidth = xrcellCode.WidthF;
                    ccDetail.CCNameWidth = xrcellParticular.WidthF;
                    ccDetail.CCPrevApprovedAmount = xrcellPrevApproved.WidthF;
                    ccDetail.CCPrevRelaized = xrcellPrevActual.WidthF;
                    ccDetail.CCProposedAmount = xrcellProposedAmount.WidthF;
                    ccDetail.CCApprovedAmount = xrcellApprovedAmount.WidthF;
                    ccDetail.CCNote = xrCellNote.WidthF;
                    dtCCDetails.DefaultView.RowFilter = string.Empty;

                    ProperBorderForLedgerRow(PrevLedgerCCFound);
                    xrSubreportCCDetails.Visible = (dtCC.Rows.Count > 0);
                    PrevLedgerCCFound = (dtCC.Rows.Count > 0);
                }
                else
                {
                    ProperBorderForLedgerRow(PrevLedgerCCFound);
                    xrSubreportCCDetails.Visible = false;
                    PrevLedgerCCFound = false;
                }
            }
            else
            {
                xrSubreportCCDetails.Visible = false;
                PrevLedgerCCFound = false;
            }
        }

        private void ProperBorderForLedgerRow(bool ccFound)
        {
            if (ccFound)
            {
                xrcellCode.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellParticular.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrcellPrevActual.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrcellPrevApproved.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrcellProposedAmount.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrcellApprovedAmount.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrCellNote.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
            }
            else
            {
                xrcellCode.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellParticular.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellPrevActual.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellPrevApproved.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellProposedAmount.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellApprovedAmount.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCellNote.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
            }
        }

        private void AssignCCDetailReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlCCBudgetRealization = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetAnnualCCApproved);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.AppSetting.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.AppSetting.YearTo);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, BudgetTransSource);

                dataManager.Parameters.Add(this.ReportParameters.BCC_IDColumn, 1);

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
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtCCDetails = resultArgs.DataSource.Table;
            }
        }

        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblBudget = AlignContentTable(xrtblBudget);
            xrTblBudgetNature = AlignContentTable(xrTblBudgetNature);
            xrTblHeaderLedgerGroup = AlignContentTable(xrTblHeaderLedgerGroup);
            xrTblFooterLedgerGroup = AlignContentTable(xrTblFooterLedgerGroup);
            xrTblFooterBudgetNature = AlignContentTable(xrTblFooterBudgetNature);
            xrTblGrandFooterTotal = AlignGrandTotalTable(xrTblGrandFooterTotal);

            if (this.DataSource != null)
            {
                DataTable dtBudgetDetails = this.DataSource as DataTable;
                bool records = dtBudgetDetails.Rows.Count > 0;
                grpHeaderBudgetNature.Visible = grpHeaderLedgerGroup.Visible = records;
                grpFooterBudgetNature.Visible = grpFooterLedgerGroup.Visible = records;
                Detail.Visible = records;
                ReportFooter.Visible = records;
            }
            else
            {
                Detail.Visible = false;
                grpHeaderBudgetNature.Visible = grpHeaderLedgerGroup.Visible = false;
                grpFooterBudgetNature.Visible = grpFooterLedgerGroup.Visible = false;
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
                string colgroupid = reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName;

                //For Budget Nature
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

                //For Opening and Closing balances (Current FY and Previous Year)
                if (BudgetTransSource == TransMode.CR)
                {
                    AttachBalancesForBudget(dtReport, true, true, false);
                }
                else if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR) //AppendCashBankFDBalances(dtBudgetLedgers);
                {
                    AttachBalancesForBudget(dtReport, false, true, false);
                }
            }
            //}
        }

        private void xrBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                        cell.Text = (BudgetTransSource == TransMode.CR ? "Total Opening Balance" : "Total Closing Balance");//Sub Group Total";
                    else
                        cell.Text = "Total";
                }
            }
        }

        private void xrcellLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = cell.Text.Trim();
        }

        private void xrcellLGTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (activegroupid == ((int)FixedLedgerGroup.Cash))
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


        private void grpFooterBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //e.Cancel = (BudgetTransSource == TransMode.DR && !HeaderLoaded);
            //HeaderLoaded = true;
            e.Cancel = (BudgetTransSource == TransMode.DR);
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

        private void grpHeaderBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            //{
            //    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
            //    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
            //        e.Cancel = false;
            //    else
            //        e.Cancel = true;
            //}
        }

        private void xrCellNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            //{
            //    XRTableCell cell = sender as XRTableCell;
            //    cell.Text = string.Empty;

            //    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
            //    if (BudgetTransSource==TransMode.CR &&  (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
            //    {
            //        cell.Text = "1";
            //    }
            //}

            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION)
                {
                    (sender as XRTableCell).TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
            }
        }

        private void xrcellPrevBNSumApprovedAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                //For CMF
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomePreApprovedTotal - GrandExpensePreApprovedTotal;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellPrevGSumApprovedAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //For CMF 
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        double closingbalance = GrandIncomePreApprovedTotal - GrandExpensePreApprovedTotal;
                        e.Result = GrandExpensePreApprovedTotal + closingbalance;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNSumProposed_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                //For CMF
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeProposedTotal - GrandExpenseProposedTotal;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellBNSumApprovedAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                //For CMF
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
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

        private void xrcellGSumProposed_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                //For CMF
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        double closingbalance = GrandIncomeProposedTotal - GrandExpenseProposedTotal;
                        e.Result = GrandExpenseProposedTotal + closingbalance;
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrcellGSumApprovedAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //For CMF
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
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

        private void xrcellSumBNPrvApproved_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrcellSumBNPrvActual_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrcellSumBNProposed_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrcellSumBNApproved_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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



        private void xrcellSumBNPrvApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //For CMF
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomePreApprovedTotal - GrandExpensePreApprovedTotal;
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

        private void xrcellSumBNProposed_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //For CMF
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (BudgetTransSource == TransMode.DR && (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit)))
                    {
                        e.Result = GrandIncomeProposedTotal - GrandExpenseProposedTotal;
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

        private void xrcellSumBNApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
            {
                //For CMF
                if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
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

        private void xrcellSumBNPrvActual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrBNHeaderRow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrBudgetNature.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName) != null)
            {
                string budgetnature = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName).ToString();
                if (budgetnature.ToUpper() == "INCOME" || budgetnature.ToUpper() == "EXPENDITURE")
                {
                    //if (AppSetting.IS_CMF_CONGREGATION)
                    //{
                    //    xrBudgetNature.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    //}

                    if (xrBNHeaderRow.Cells.Contains(xrcellSumBNPrvApproved))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellSumBNPrvApproved);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellSumBNPrvActual))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellSumBNPrvActual);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellSumBNProposed))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellSumBNProposed);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellSumBNApproved))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellSumBNApproved);
                    }

                    if (xrBNHeaderRow.Cells.Contains(xrcellSumBNNote))
                    {
                        xrBNHeaderRow.Cells.Remove(xrcellSumBNNote);
                    }
                }
            }
        }

        private void xrcellSumBNNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrcellSumLGPrvApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (AppSetting.IS_CMF_CONGREGATION && BudgetTransSource == TransMode.DR)
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrcellLGSumProposed_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrcellLGSumApprovedAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ShowCCDetails();
        }



    }
}
