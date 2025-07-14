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
    public partial class BudgetApprovalByMonth : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;

        Int32 PrevBudgetId = 0;
        DateTime PrevBudgetDateFrom;
        DateTime PrevBudgetDateTo;
        double TotalPreviousBudgetedAmt = 0;
        double TotalPreviousBudgetDiffBalance = 0;

        double TotalPrevBudgetedAmt = 0;
        double TotalPrevBudgetActual = 0;
        double TotalM1PropsedAmount = 0;
        double TotalM2PropsedAmount = 0;

        double StatementBankBalance = 0;
        double NotMatrilzedAmountInBank = 0;
        double StatementCBBankBalance = 0;

        int RecLedgersSerialNo = 0;
        int RecAlphabetSerialNo = 0;
        private string[] FixedRecAlphabetLedgers = { "Net Amount", "Professional Tax", "LIC" };
        private string[] FixedRecESICMainLedgers = { "Management ESIC", "Employee ESIC" };

        public BudgetApprovalByMonth()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            TotalPreviousBudgetedAmt = 0;
            TotalPreviousBudgetDiffBalance = 0;

            BindBudgetExpenseApproval();
        }
        #endregion

        public void BindBudgetExpenseApproval()
        {
            RecLedgersSerialNo = 0;
            RecAlphabetSerialNo = 0;
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Budget) || string.IsNullOrEmpty(this.ReportProperties.Project) ||
               (ReportProperty.Current.ReportId == "RPT-163" && this.ReportProperties.Budget.Split(',').Length != 1) || (ReportProperty.Current.ReportId == "RPT-180" && this.ReportProperties.Budget.Split(',').Length != 2))
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
                //this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                //this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                this.SetLandscapeHeader = xrtblHeader.WidthF;
                this.SetLandscapeFooter = xrtblHeader.WidthF;
                this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;

                //Assign Previous Budget details
                PrevBudgetDateFrom = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddMonths(-1);
                PrevBudgetDateTo = PrevBudgetDateFrom.AddMonths(1).AddDays(-1);
                PrevBudgetId = GetPreviousBudgetId();

                SetReportTitle();
                AssignBudgetDateRangeTitle();
                SetTitleWidth(xrtblHeader.WidthF);
                this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF;
                this.SetLandscapeHeader = xrtblHeader.WidthF;
                this.SetLandscapeFooter = xrtblHeader.WidthF;
                this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;
                setHeaderTitleAlignment();

                string budgetmonth = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetApprovalByMonth);
                using (DataManager dataManager = new DataManager())
                {
                    Int32 month1budgetid = 0;
                    Int32 month2budgetid = 0;
                    string[] monthsbudget = this.ReportProperties.Budget.Split(',');
                    if (monthsbudget.Length == 2)
                    {
                        month1budgetid = UtilityMember.NumberSet.ToInteger(monthsbudget.GetValue(0).ToString());
                        month2budgetid = UtilityMember.NumberSet.ToInteger(monthsbudget.GetValue(1).ToString());
                    }
                    else if (monthsbudget.Length == 1)
                    {
                        month1budgetid = UtilityMember.NumberSet.ToInteger(monthsbudget.GetValue(0).ToString());
                        month2budgetid = 0;
                    }
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.MONTH1_BUDGET_IDColumn, month1budgetid);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.MONTH2_BUDGET_IDColumn, month2budgetid);

                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PREVIOUS_BUDGET_IDColumn, PrevBudgetId);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_FROMColumn, PrevBudgetDateFrom);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_TOColumn, PrevBudgetDateTo);

                    if (this.AppSetting.ShowBudgetLedgerActualBalance == "1")
                    {
                        dataManager.Parameters.Add(this.reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, "JN");
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetmonth);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtMonthlyBudget = resultArgs.DataSource.Table;
                        Detail.SortFields.Add(new GroupField("SORT_ID", XRColumnSortOrder.Ascending));
                        this.DataSource = dtMonthlyBudget;
                        this.DataMember = dtMonthlyBudget.TableName;
                    }
                }

                SetReportProperties();
                BindBRSFooterBudgetBalance();

                grpBSGHeader.GroupFields.Clear();
                grpBSGHeader.GroupFields.Add(new GroupField("BUDGET_SUB_GROUP_ID"));
                Detail.SortFields.Add(new GroupField("SORT_ID", XRColumnSortOrder.Ascending));
                Detail.SortFields.Add(new GroupField("MAIN_LEDGER_NAME"));
                //Detail.SortFields.Add( new GroupField(this.reportSetting1.BUDGET_LEDGER.LEDGER_NAMEColumn.ColumnName));
                Detail.SortFields.Add(new GroupField("SUB_LEDGER_NAME"));
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void SetReportProperties()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblBudget = AlignContentTable(xrtblBudget);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            //Set Column Caption
            xrcellHeaderPrevBudgetedAmt.Text = "Budgeted for " + PrevBudgetDateFrom.ToString("MMM yyyy");
            xrcellHeaderM1Proposal.Text = "Proposed for " + UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToString("MMM yyyy");
            xrcellHeaderM2Proposal.Text = "Proposed for " + UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).ToString("MMM yyyy");

            if (ReportProperty.Current.ReportId.Equals("RPT-163"))
            {
                xrtblHeader.SuspendLayout();
                if (xrHeaderRow.Cells.Contains(xrcellHeaderM2Proposal))
                    xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrcellHeaderM2Proposal.Name]);
                xrtblHeader.PerformLayout();

                xrtblBudget.SuspendLayout();
                if (xrDataRow.Cells.Contains(xrcellM2Proposal))
                    xrDataRow.Cells.Remove(xrDataRow.Cells[xrcellM2Proposal.Name]);
                xrtblBudget.PerformLayout();

                xrtblGrandTotal.SuspendLayout();
                if (xrGrandTotalRow.Cells.Contains(xrCellSumM2PropsedAmt))
                    xrGrandTotalRow.Cells.Remove(xrGrandTotalRow.Cells[xrCellSumM2PropsedAmt.Name]);
                xrtblGrandTotal.PerformLayout();
            }
        }

        private Int32 GetPreviousBudgetId()
        {
            Int32 rtn = 0;
            ResultArgs resultargs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetIdByDateRangeProject))
            {
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_FROMColumn, PrevBudgetDateFrom);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_TOColumn, PrevBudgetDateTo);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            if (resultargs.Success && resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count > 0)
            {
                rtn = UtilityMember.NumberSet.ToInteger(resultargs.DataSource.Table.Rows[0][this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName].ToString());
            }
            return rtn;
        }

        private void BindBRSFooterBudgetBalance()
        {
            ResultArgs resultargs = new ResultArgs();
            DataTable dtBRSList = new DataTable();

            double UnRealizedAmt = 0;
            double UnClearedAmt = 0;

            //# Get BRS details still previous budget date to
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.FetchBRSByMaterialized))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, PrevBudgetDateTo.ToShortDateString());
                resultargs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            if (resultargs.Success && resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count > 0)
            {
                dtBRSList = resultargs.DataSource.Table;

                UnRealizedAmt = this.UtilityMember.NumberSet.ToDouble(dtBRSList.Compute("SUM(UnRealised)", "").ToString());
                UnClearedAmt = this.UtilityMember.NumberSet.ToDouble(dtBRSList.Compute("SUM(UnCleared)", "").ToString());
            }

            //# GEt CB bank balance for previous budget date to, closing balance
            StatementCBBankBalance = this.GetBalance(this.ReportProperties.Project, PrevBudgetDateTo.ToShortDateString(), AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.BankBalance, AcMEDSync.Model.BalanceSystem.BalanceType.ClosingBalance);

            //# Bank Balance
            StatementBankBalance = 0;
            StatementBankBalance = StatementCBBankBalance - UnRealizedAmt;
            StatementBankBalance += UnClearedAmt;

            NotMatrilzedAmountInBank = 0;
            NotMatrilzedAmountInBank = UnRealizedAmt + UnClearedAmt;

            //xrfooterOP.Text = this.UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetedAmt);
            //xrfooterBankBalance.Text = this.UtilityMember.NumberSet.ToNumber(StatementBankBalance);
            //xrfooterCBbalance.Text = this.UtilityMember.NumberSet.ToNumber(Cashbookbankbalance);
            //xrfooterDifference.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(Cashbookbankbalance - Statementbankbalance)).ToString();
            //xrfooterBRSAmt.Text = this.UtilityMember.NumberSet.ToNumber(NotMatrilzedAmountInBank);

            if (this.DataSource != null)
            {
                DataTable dtBind = this.DataSource as DataTable;
                TotalPrevBudgetedAmt = UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                TotalPrevBudgetActual = UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName + ")", string.Empty).ToString());
                TotalM1PropsedAmount = UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.M1_PROPOSED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                TotalM2PropsedAmount = UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(" + this.reportSetting1.BUDGETVARIANCE.M2_PROPOSED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
            }

            //xrfooterOP.Text = this.UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetedAmt);
            //xrfooterTotalBudget.Text = this.UtilityMember.NumberSet.ToNumber(TotalM1PropsedAmount + TotalM2PropsedAmount);
            //xrfooterPreviousBudgetBalance.Text = this.UtilityMember.NumberSet.ToNumber(TotalPrevBudgetedAmt - TotalPrevBudgetActual);
            //xrfooterTotalBalance.Text = this.UtilityMember.NumberSet.ToNumber((TotalM1PropsedAmount + TotalM2PropsedAmount) - (TotalPrevBudgetedAmt - TotalPrevBudgetActual));

            //for temp
            //xrfooterCBbalance.Text = this.UtilityMember.NumberSet.ToNumber(totalPrevBudgetedAmt - totalPrevBudgetActual);
            //xrfooterDifference.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(Statementbankbalance - (totalPrevBudgetedAmt - totalPrevBudgetActual))).ToString();
            //xrfooterCBbalance.Text = this.UtilityMember.NumberSet.ToNumber(Cashbookclosingbankbalance);
            //xrfooterDifference.Text = this.UtilityMember.NumberSet.ToNumber(Math.Abs(StatementBankBalance - Cashbookclosingbankbalance)).ToString();


            if (ReportProperty.Current.ReportId.Equals("RPT-163"))
            {
                xrfooterTotalBudgetCaption.Text = "Total Budget of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, "MMM yyyy");
            }
            else
            {
                xrfooterTotalBudgetCaption.Text = "Total Budget of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, "MMM") + " & " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateTo, "MMM yyyy");
            }
            xrfooterPreviousBudgetBalanceCaption.Text = "Balance from Previous Month Budget " + UtilityMember.DateSet.ToDate(PrevBudgetDateFrom.ToShortDateString(), "MMM yyyy");

            //xrTblBRSbalance.SuspendLayout();
            //xrfooterOP.WidthF = xrfooterBankBalance.WidthF = xrfooterCBbalance.WidthF = xrfooterDifference.WidthF = xrfooterBRS.WidthF = xrfooterBRSAmt.WidthF = xrfooterTotalBudget.WidthF = xrfooterPreviousBudgetBalance.WidthF = xrfooterTotalBalance.WidthF = (xrcellHeaderPrevBudgetedAmt.WidthF + xrcellHeaderPrevBudgetedActual.WidthF);
            //xrTblBRSbalance.PerformLayout();

            xrTblBRSbalance.SuspendLayout();

            xrfooterOPCaption.WidthF = xrcellBudgetSubSNo.WidthF + xrcellParticular.WidthF;
            xrfooterBankBalanceCaption.WidthF = xrcellBudgetSubSNo.WidthF + xrcellParticular.WidthF;
            xrfooterCBbalanceCaption.WidthF = xrcellBudgetSubSNo.WidthF + xrcellParticular.WidthF;
            xrfooterDifferenceCaption.WidthF = xrcellBudgetSubSNo.WidthF + xrcellParticular.WidthF;
            xrfooterBRSCaption.WidthF = xrcellBudgetSubSNo.WidthF + xrcellParticular.WidthF;
            xrfooterEmpty1Caption.WidthF = xrcellBudgetSubSNo.WidthF + xrcellParticular.WidthF;
            xrfooterTotalBudgetCaption.WidthF = xrcellBudgetSubSNo.WidthF + xrcellParticular.WidthF;
            xrfooterPreviousBudgetBalanceCaption.WidthF = xrcellBudgetSubSNo.WidthF + xrcellParticular.WidthF;
            xrfooterTotalBalanceCaption.WidthF = xrcellBudgetSubSNo.WidthF + xrcellParticular.WidthF;
            xrTblBRSbalance.PerformLayout();

            xrTblBRSbalance.SuspendLayout();
            xrfooterOP.WidthF = xrcellHeaderPrevBudgetedAmt.WidthF;// (xrcellHeaderPrevBudgetedAmt.WidthF + xrcellHeaderPrevBudgetedActual.WidthF);
            xrTblBRSbalance.PerformLayout();

        }

        private void xrcellPrevBudgetedBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.Text = "0.0";

            if (GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName) != null
                && GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.PREV_PROPOSED_AMOUNTColumn.ColumnName) != null
                && GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.M1_PROPOSED_AMOUNTColumn.ColumnName) != null
                && GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.M2_PROPOSED_AMOUNTColumn.ColumnName) != null)
            {
                double prevbudgetedamt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName).ToString());
                double prevbudgeteactual = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName).ToString());
                double m1Propsedamt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.M1_PROPOSED_AMOUNTColumn.ColumnName).ToString());
                double m2Propsedamt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.BUDGETVARIANCE.M2_PROPOSED_AMOUNTColumn.ColumnName).ToString());

                double diff = prevbudgetedamt - prevbudgeteactual;
                TotalPreviousBudgetedAmt += prevbudgetedamt;
                TotalPreviousBudgetDiffBalance += diff;

                cell.Text = UtilityMember.NumberSet.ToNumber(diff);
            }

            //if (IsNotBudgetedAmount())
            //{
            //    cell.Text = string.Empty;
            //}
        }



        private void xrcellM2Proposal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            /*if (IsNotBudgetedAmount())
            {
                cell.Text = string.Empty;
            }*/
        }

        private void xrcellM1Proposal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            /*if (IsNotBudgetedAmount())
            {
                cell.Text = string.Empty;
            }*/
        }

        private void xrcellPrevBudgetedActual_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            /*if (IsNotBudgetedAmount())
            {
                cell.Text = string.Empty;
            }*/
        }

        private void xrcellPrevBudgetedAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            /*if (IsNotBudgetedAmount())
            {
                cell.Text = string.Empty;
            }*/
        }

        /// <summary>
        /// This method is used to check Month1 and Month2 Budget Amount
        /// </summary>
        /// <returns></returns>
        private bool IsNotBudgetedAmount()
        {
            bool Rtn = false;
            double m1proposedamount = 0;
            double m2proposedamount = 0;
            if (GetCurrentColumnValue("M1_PROPOSED_AMOUNT") != null)
            {
                m1proposedamount = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("M1_PROPOSED_AMOUNT").ToString());
            }

            if (GetCurrentColumnValue("M2_PROPOSED_AMOUNT") != null)
            {
                m2proposedamount = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("M2_PROPOSED_AMOUNT").ToString());
            }
            Rtn = (m1proposedamount == 0 && m2proposedamount == 0);
            return Rtn;
        }

        private string getAlphabetSerialNo()
        {
            string rtn = string.Empty;

            try
            {
                int pos = (RecAlphabetSerialNo == 1 || RecAlphabetSerialNo == 2) ? RecAlphabetSerialNo + 2 : RecAlphabetSerialNo;
                string[] alphabetArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                rtn = alphabetArray.GetValue(pos).ToString();
                RecAlphabetSerialNo++;
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn.ToLower();
        }

        private void xrfooterOP_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetedAmt); ;
            e.Handled = true;
        }

        private void xrfooterBankBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(StatementBankBalance); ;
            e.Handled = true;
        }

        private void xrfooterBRSAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(NotMatrilzedAmountInBank); ;
            e.Handled = true;
        }

        private void xrfooterCBbalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(StatementCBBankBalance); ;
            e.Handled = true;
        }

        private void xrfooterDifference_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(Math.Abs(StatementBankBalance - StatementCBBankBalance)).ToString();
            e.Handled = true;
        }

        private void xrfooterPreviousBudgetBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = this.UtilityMember.NumberSet.ToNumber(TotalPrevBudgetedAmt - TotalPrevBudgetActual);
            e.Result = this.UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetDiffBalance);
            e.Handled = true;
        }

        private void xrfooterTotalBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber((TotalM1PropsedAmount + TotalM2PropsedAmount) - (TotalPrevBudgetedAmt - TotalPrevBudgetActual));
            e.Handled = true;
        }

        private void xrCellSumPreviousBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = UtilityMember.NumberSet.ToNumber(TotalPreviousBudgetDiffBalance);
            e.Handled = true;
        }

        private void xrfooterTotalBudget_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToNumber(TotalM1PropsedAmount + TotalM2PropsedAmount);
            e.Handled = true;
        }



        private void xrcellBudgetSubGrp_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            string BudgetSubGrpName = string.Empty;
            if (cell != null)
            {
                if (GetCurrentColumnValue("BUDGET_SUB_GROUP_ID") != null)
                {
                    int budgetsupgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                    if (budgetsupgrpid == 1)
                    {
                        BudgetSubGrpName = "Salary";
                    }
                    else if (budgetsupgrpid == 2)
                    {
                        BudgetSubGrpName = " b. PF : ";
                    }
                    else if (budgetsupgrpid == 3)
                    {
                        BudgetSubGrpName = " c. ESIC : ";
                    }
                }

                cell.Text = BudgetSubGrpName;
            }
        }

        private void xrcellSNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int budgetgrpid = 0;
            int budgetsupgrpid = 0;
            int sublegerid = 0;
            string ledgername = string.Empty;
            XRTableCell cell = sender as XRTableCell;
            if (GetCurrentColumnValue("BUDGET_SUB_GROUP_ID") != null)
            {
                budgetgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                budgetsupgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                ledgername = GetCurrentColumnValue("LEDGER_NAME").ToString();
                sublegerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("SUB_LEDGER_ID").ToString());
            }
            bool AlphaLedgersExists = Array.IndexOf(FixedRecAlphabetLedgers, ledgername) >= 0;
            bool FixedESICLedgers = Array.IndexOf(FixedRecESICMainLedgers, ledgername) >= 0;
            if (budgetgrpid == 1 && budgetsupgrpid > 2 && !AlphaLedgersExists && !FixedESICLedgers) //Skip Salary/PF group
            {
                RecLedgersSerialNo++;
                cell.Text = RecLedgersSerialNo.ToString();
            }
            else if (budgetgrpid == 2 && sublegerid == 0) //For Non Rec
            {
                RecLedgersSerialNo++;
                cell.Text = RecLedgersSerialNo.ToString();
            }
            else
            {
                cell.Text = string.Empty;
            }
        }

        private void xrcellParticular_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("BUDGET_GROUP_ID") != null)
            {
                XRTableCell cell = sender as XRTableCell;
                int budgetgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                int budgetsubgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                string ledgername = GetCurrentColumnValue("LEDGER_NAME").ToString();

                bool AlphaLedgersExists = Array.IndexOf(FixedRecAlphabetLedgers, ledgername) >= 0;

                if (AlphaLedgersExists)
                {
                    cell.Text = getAlphabetSerialNo() + ". " + ledgername;
                }

                /*if (budgetgrpid == 1 && budgetsubgrpid==1) // for Rec, Salary group
                {
                    cell.Text = getAlphabetSerialNo() + ". " + ledgername;
                }
                else if (budgetgrpid == 1 && budgetsubgrpid == 3)
                {
                    bool exists = Array.IndexOf(FixedRecAlphabetLedgers, ledgername) >= 0;
                    if (exists)
                    {
                        cell.Text = getAlphabetSerialNo() + ". " + ledgername;
                    }
                }*/
            }
        }



        private void grpBSGHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("BUDGET_GROUP_ID") != null)
            {
                int budgetgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                int budgetsubgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                string ledgername = GetCurrentColumnValue("LEDGER_NAME").ToString();
                e.Cancel = (budgetgrpid == 2);
            }
        }

        private void xrcellBudgetSubSNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (GetCurrentColumnValue("BUDGET_GROUP_ID") != null)
            {
                int budgetgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                int budgetsubgrpid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_SUB_GROUP_ID").ToString());
                string ledgername = GetCurrentColumnValue("LEDGER_NAME").ToString();

                if (budgetgrpid == 1 && budgetsubgrpid == 1) // for Rec, Salary group
                {
                    cell.Text = "1";
                    RecLedgersSerialNo++;
                }
                else
                {
                    cell.Text = "";
                }
            }


        }

        private void grpBGFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("BUDGET_GROUP_ID") != null)
            {
                Int32 budgetgroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("BUDGET_GROUP_ID").ToString());
                xrPageBreak.Visible = true;
                if (budgetgroupid == 2)
                {
                    xrPageBreak.Visible = false;
                }
            }
        }



    }
}
