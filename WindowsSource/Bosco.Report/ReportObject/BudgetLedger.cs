﻿using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using AcMEDSync.Model;
using Bosco.DAO.Schema;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetLedger : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        DateTime dtPrevBudgetDateFrom = new DateTime();
        DateTime dtPrevBudgetDateTo = new DateTime();
        DateTime dtBudgetDateFrom = new DateTime();
        DateTime dtBudgetDateTo = new DateTime();
        string BudgetProjects = string.Empty;
        Int32 ActiveGroup = 0;
        decimal HOBudgetHelpPropsedAmt = 0;
        decimal HOBudgetHelpApprovedAmt = 0;

        TransMode BudgetTransMode = TransMode.CR;

        double ordinaryExprense = 0;
        double extraordinaryExprense = 0;
        double amtDifference = 0;

        #endregion

        #region Properties

        public float GetLedgerNameColumnsWidth
        {
            get
            {
                return xrCapCode.WidthF + xrCapLedgerName.WidthF;
            }
        }

        public float GetAmountColumnsWidth
        {
            get
            {
                return xrCapActualPreviousAmount.WidthF;
            }
        }

        double totalProposedBudgetedIncome = 0;
        public double TotalProposedBudgetedIncome
        {
            get
            {
                return totalProposedBudgetedIncome;
            }
            set
            {
                totalProposedBudgetedIncome = value;
            }
        }

        double totalProposedBudgetedExpense = 0;
        public double TotalProposedBudgetedExpense
        {
            get
            {
                return totalProposedBudgetedExpense;
            }
        }

        double totalApprovedBudgetedIncome = 0;
        public double TotalApprovedBudgetedIncome
        {
            get
            {
                return totalApprovedBudgetedIncome;
            }
            set
            {
                totalApprovedBudgetedIncome = value;
            }
        }

        double totalApprovedBudgetedExpense = 0;
        public double TotalApprovedBudgetedExpense
        {
            get
            {
                return totalApprovedBudgetedExpense;
            }
        }

        double totalPrevApprovedBudgetedIncome = 0;
        public double TotalPrevApprovedBudgetedIncome
        {
            get
            {
                return totalPrevApprovedBudgetedIncome;
            }
            set
            {
                totalPrevApprovedBudgetedIncome = value;
            }
        }

        double totalPrevApprovedBudgetedExpense = 0;
        public double TotalPrevApprovedBudgetedExpense
        {
            get
            {
                return totalPrevApprovedBudgetedExpense;
            }
        }

        double totalOpeningBalance = 0;
        public double TotalOpeningBalance
        {
            get
            {
                return totalOpeningBalance;
            }
            set
            {
                totalOpeningBalance = value;
            }
        }

        double totalBudgetedRawProposedIncome = 0;
        public double TotalBudgetedRawProposedIncome
        {
            get
            {
                return totalBudgetedRawProposedIncome;
            }
            set
            {
                totalBudgetedRawProposedIncome = value;
            }
        }

        double cashOpeningBalance = 0;
        public double CashOpeningBalance
        {
            get
            {
                return cashOpeningBalance;
            }
            set
            {
                cashOpeningBalance = value;
            }
        }

        double bankOpeningBalance = 0;
        public double BankOpeningBalance
        {
            get
            {
                return bankOpeningBalance;
            }
            set
            {
                bankOpeningBalance = value;
            }
        }

        double fdOpeningBalance = 0;
        public double FDOpeningBalance
        {
            get
            {
                return fdOpeningBalance;
            }
            set
            {
                fdOpeningBalance = value;
            }
        }

        #endregion

        public BudgetLedger()
        {
            InitializeComponent();
        }

        #region Methods
        public void BindBudgetLedgers(TransMode transmode, string budgetprojectIds, Int32 BudgetTypeId, DateTime dtBDateFrom, DateTime dtBDateTo,
            DateTime dtPDateFrom, DateTime dtPDateTo, Int32 budgetAction, Int32 PreviousBudgetId)
        {
            ordinaryExprense = 0;
            extraordinaryExprense = 0;
            amtDifference = 0;

            dtPrevBudgetDateFrom = dtPDateFrom;
            dtPrevBudgetDateTo = dtPDateTo;

            dtBudgetDateFrom = dtBDateFrom;
            dtBudgetDateTo = dtBDateTo;
            BudgetProjects = budgetprojectIds;
            BudgetTransMode = transmode;
            //xrFooterRow1.Visible = xrFooterRow3.Visible = xrFooterRowSign.Visible = false;
            xrRowGrandTotal.Visible = false;
            grpBudgetGroupFooter.Visible = false;
            if (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE || AppSetting.IS_CMF_CONGREGATION)
            {
                //On 08/07/2020, to have proper ledger name, code order without budget group and budget sub group--------------
                if (!AppSetting.IS_CMF_CONGREGATION || BudgetTransMode == TransMode.CR) //On 27/01/2022 for CMF
                {
                    grpBudgetGroupHeader.GroupFields.Clear();
                    grpBudgetGroupHeader.SortingSummary.Enabled = false;
                    grpBudgetGroupHeader.Visible = false;
                    xrtblGrpBudgetGroup.Visible = false;
                    grpBudgetGroupFooter.Visible = false;
                    xrRowGrandTotal.Visible = true;
                }
                else
                {
                    grpBudgetGroupFooter.Visible = true;
                }

                grpBudgetSubGroupHeader.SortingSummary.Enabled = false;
                grpBudgetSubGroupHeader.GroupFields.Clear();
                grpBudgetSubGroupHeader.Visible = false;
                xrtblGrpBudgetSubGroup.Visible = false;
                //-------------------------------------------------------------------------------------------------------------

                grpLedgerGroupHeader.Visible = true;
                grpLedgerGroupFooter.Visible = true;
            }
            else
            {
                //On 28/02/2024, To show ledger group for all
                /*grpLedgerGroupHeader.GroupFields.Clear();
                grpLedgerGroupHeader.Visible = false;
                grpLedgerGroupFooter.Visible = false;*/

                grpLedgerGroupHeader.Visible = true;
                grpLedgerGroupFooter.Visible = true;
            }

            if (transmode == TransMode.CR)
            {
                xrCapApprovedPreviousAmount.Text = " Approved Income "; //#29/01/2020 purposely to diff current year approved income (Import excell process); 
                xrCapActualPreviousAmount.Text = "Realized Income"; //Actual Income";
                xrCapProposedAmount.Text = "Proposed Income";
                xrCapApprovedAmount.Text = "Approved Income";
                grpBudgetSubGroupHeader.Visible = false;
            }
            else
            {
                xrCapApprovedPreviousAmount.Text = "Approved Expenditure";
                xrCapActualPreviousAmount.Text = "Realized Expenditure"; //"Actual Expenditure";
                xrCapProposedAmount.Text = "Proposed Expenditure";
                xrCapApprovedAmount.Text = "Approved Expenditure";
                grpBudgetSubGroupHeader.Visible = true;

                //On 25/04/20203
                //xrFooterRow1.Visible = xrFooterRow3.Visible = xrFooterRowSign.Visible = true;
                //xrCellFooterSgin1.Text = string.Empty;
                //xrSubSignFooter.Visible = xrSubSignFooter.Visible = false;
                //if (AppSetting.IS_CMF_CONGREGATION)
                //{
                //    xrFooterRow1.Visible = xrFooterRow3.Visible = xrFooterRowSign.Visible = false;
                //    rptFooterSign.Visible = false;
                //    (xrSubSignFooter.ReportSource as SignReportFooter).ShowApprovedSign = false;
                //    this.ReportProperties.IncludeSignDetails = 1;
                //    if (transmode == TransMode.DR && this.ReportProperties.IncludeSignDetails == 1)
                //    {
                //        rptFooterSign.Visible = true;
                //        xrSubSignFooter.Visible = true;
                //        (xrSubSignFooter.ReportSource as SignReportFooter).HideRole2 = true;
                //        (xrSubSignFooter.ReportSource as SignReportFooter).Role2Width = (xrSubSignFooter.ReportSource as SignReportFooter).Role2Width + 45;
                //        if (AppSetting.HeadofficeCode.ToUpper() == "CMFNED" && budgetAction == (Int32)BudgetAction.Approved)
                //        {
                //            (xrSubSignFooter.ReportSource as SignReportFooter).ShowApprovedSign = true;
                //        }
                //        (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrtblHeaderCaption.WidthF - 10;
                //        (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
                //    }
                //}
            }

            if (BudgetTypeId == (int)BudgetType.BudgetMonth)
            {
                xrCapApprovedPreviousYear.Text = dtPrevBudgetDateFrom.ToString("MMMM yyyy");
                xrCapActualPreviousYear.Text = dtPrevBudgetDateFrom.ToString("MMMM yyyy");
                xrCapProposedCurrentYear.Text = dtBDateFrom.ToString("MMMM yyyy");
                xrCapApprovedCurrentYear.Text = dtBDateFrom.ToString("MMMM yyyy");
            }
            else
            {
                xrCapApprovedPreviousYear.Text = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).AddYears(-1).Year + "-" + UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).AddYears(-1).Year;
                xrCapActualPreviousYear.Text = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).AddYears(-1).Year + "-" + UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).AddYears(-1).Year;
                xrCapProposedCurrentYear.Text = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year + "-" + UtilityMember.DateSet.ToDate(AppSetting.YearTo, false).Year;
                xrCapApprovedCurrentYear.Text = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).Year + "-" + UtilityMember.DateSet.ToDate(AppSetting.YearTo, false).Year;
            }

            if (AppSetting.IS_CMF_CONGREGATION)
            {
                xrcellGrandTotal.Text = xrcellGrandTotal.Text.ToUpper();
            }

            ResultArgs resultArgs = new ResultArgs();
            string budgetledger = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetLedgers);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.BUDGET_STATISTICS.BUDGET_IDColumn, this.ReportProperties.BudgetId);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PREVIOUS_BUDGET_IDColumn, PreviousBudgetId);

                dataManager.Parameters.Add(this.reportSetting1.ReportParameter.PROJECT_IDColumn, budgetprojectIds);
                dataManager.Parameters.Add(this.reportSetting1.ReportParameter.YEAR_FROMColumn, dtPDateFrom);
                dataManager.Parameters.Add(this.reportSetting1.ReportParameter.YEAR_TOColumn, dtPDateTo);

                dataManager.Parameters.Add(this.reportSetting1.BUDGET_LEDGER.BUDGET_TRANS_MODEColumn, transmode.ToString());

                /*if (this.AppSetting.ShowBudgetLedgerActualBalance == "1")
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, "JN");
                }

                //On 31/07/2020, to fix ledger balacen
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, this.AppSetting.ShowBudgetLedgerSeparateReceiptPaymentActualBalance);
                */

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

                //On 10/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                decimal CurrencyAvgExchangeRate = 1;
                decimal CurrencyPreviousYearAvgExchangeRate = 1;
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    CurrencyAvgExchangeRate = GetAvgCurrencyExchangeRateForFY(this.ReportProperties.CurrencyCountryId);
                    CurrencyPreviousYearAvgExchangeRate = GetAvgCurrencyExchangeRateForPreviousFY(this.ReportProperties.CurrencyCountryId);
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                dataManager.Parameters.Add(reportSetting1.Country.EXCHANGE_RATEColumn, CurrencyAvgExchangeRate);
                dataManager.Parameters.Add(reportSetting1.Country.PREVIOUS_YEAR_EX_RATEColumn, CurrencyPreviousYearAvgExchangeRate);
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetledger);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                DataTable dtIncomeSource = resultArgs.DataSource.Table;

                //On 18/07/2023, To show all budget mapped ledgers
                if (this.ReportProperties.IncludeAllBudgetLedgers == 0)
                {
                    //On 19/03/2021, to filter only mapped Budget Ledger
                    //To show only Budgeted Ledgers alone (based on settings)
                    if (this.ReportProperties.IncludeAllLedger == 0)
                    {
                        dtIncomeSource.DefaultView.RowFilter = " MAP_BUDGET_LEDGER_ID>0";
                        dtIncomeSource = dtIncomeSource.DefaultView.ToTable();
                    }

                    //On 22/01/2024, To show ledgers which are used in current year as well as previous year approved too
                    //On 22/03/2021, To show only proposed ledgers alone
                    //dtIncomeSource.DefaultView.RowFilter = "PROPOSED_CURRENT_YR > 0 OR APPROVED_CURRENT_YR >";

                    if (this.AppSetting.IS_SDB_INM)
                    {

                        dtIncomeSource.DefaultView.RowFilter = "PROPOSED_CURRENT_YR > 0 OR APPROVED_CURRENT_YR > 0 OR APPROVED_PREVIOUS_YR > 0 OR ACTUAL>0";
                    }
                    else
                    {
                        dtIncomeSource.DefaultView.RowFilter = "PROPOSED_CURRENT_YR > 0 OR APPROVED_CURRENT_YR > 0 OR APPROVED_PREVIOUS_YR > 0";
                    }

                    dtIncomeSource = dtIncomeSource.DefaultView.ToTable();
                }

                if (transmode == TransMode.CR)
                {
                    if (this.AppSetting.ShowCashBankFDDetailLedgerInBudgetProposed == 1 ||
                        (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE || AppSetting.IS_CMF_CONGREGATION))
                    {
                        AppendCashBankDetailBalances(dtIncomeSource);
                    }
                    else
                        AppendCashBankBalances(dtIncomeSource);

                    //On 29/06/2019, Attach Province Help ------------------------------------------------------------------------
                    AppendHOHelpAmountInIncome(dtIncomeSource);
                    //------------------------------------------------------------------------------------------------------------

                    //On 10/05/2019, to exculude Cash/Bank/FD opening balance
                    //totalProposedBudgetedIncome = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(PROPOSED_CURRENT_YR)", string.Empty).ToString());
                    //totalApprovedBudgetedIncome = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(APPROVED_CURRENT_YR)", string.Empty).ToString());
                    totalPrevApprovedBudgetedIncome = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(APPROVED_PREVIOUS_YR)", string.Empty).ToString());

                    //On 09/02/2024, If we add decimal variable in the compute (decimal variable decimal seprator "," is not accepting like 50,00 (50.00))
                    //totalProposedBudgetedIncome = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(PROPOSED_CURRENT_YR) + " + HOBudgetHelpPropsedAmt, "(GROUP_ID>0 AND LEDGER_ID>0)").ToString());
                    //totalApprovedBudgetedIncome = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(APPROVED_CURRENT_YR) + " + HOBudgetHelpApprovedAmt, "(GROUP_ID>0 AND LEDGER_ID>0)").ToString());
                    totalProposedBudgetedIncome = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(PROPOSED_CURRENT_YR) + " + UtilityMember.NumberSet.ToDouble(HOBudgetHelpPropsedAmt.ToString()).ToString(), "(GROUP_ID>0 AND LEDGER_ID>0)").ToString());
                    totalApprovedBudgetedIncome = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(APPROVED_CURRENT_YR) + " + UtilityMember.NumberSet.ToDouble(HOBudgetHelpApprovedAmt.ToString()).ToString(), "(GROUP_ID>0 AND LEDGER_ID>0)").ToString());

                    //28/01/2022, to assing total opening  balance and raw budgeted income
                    cashOpeningBalance = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(PROPOSED_CURRENT_YR)", "GROUP_ID IN (13)").ToString());
                    bankOpeningBalance = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(PROPOSED_CURRENT_YR)", "GROUP_ID IN (12)").ToString());
                    fdOpeningBalance = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(PROPOSED_CURRENT_YR)", "GROUP_ID IN (14)").ToString());
                    totalOpeningBalance = cashOpeningBalance + bankOpeningBalance + fdOpeningBalance;
                    totalBudgetedRawProposedIncome = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(PROPOSED_CURRENT_YR)", "(GROUP_ID>0 AND LEDGER_ID>0) AND GROUP_ID NOT IN (12, 13, 14)").ToString());

                    xrGSumProposed.Text = UtilityMember.NumberSet.ToNumber(totalProposedBudgetedIncome);
                    xrGSumApproved.Text = UtilityMember.NumberSet.ToNumber(totalApprovedBudgetedIncome);
                }
                else if (AppSetting.IS_CMF_CONGREGATION && transmode == TransMode.DR)
                {
                    AppendCashBankDetailBalances(dtIncomeSource, true);

                    totalPrevApprovedBudgetedExpense = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(APPROVED_PREVIOUS_YR)", string.Empty).ToString());
                    totalProposedBudgetedExpense = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(PROPOSED_CURRENT_YR)", string.Empty).ToString());
                    totalApprovedBudgetedExpense = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(APPROVED_CURRENT_YR)", string.Empty).ToString());

                    xrGSumProposed.Text = UtilityMember.NumberSet.ToNumber(totalProposedBudgetedExpense);
                    xrGSumApproved.Text = UtilityMember.NumberSet.ToNumber(totalApprovedBudgetedExpense);
                }
                else
                {
                    totalPrevApprovedBudgetedExpense = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(APPROVED_PREVIOUS_YR)", string.Empty).ToString());
                    totalProposedBudgetedExpense = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(PROPOSED_CURRENT_YR)", string.Empty).ToString());
                    totalApprovedBudgetedExpense = UtilityMember.NumberSet.ToDouble(dtIncomeSource.Compute("SUM(APPROVED_CURRENT_YR)", string.Empty).ToString());

                    xrGSumProposed.Text = UtilityMember.NumberSet.ToNumber(totalProposedBudgetedExpense);
                    xrGSumApproved.Text = UtilityMember.NumberSet.ToNumber(totalApprovedBudgetedExpense);
                }
                dtIncomeSource.TableName = "BUDGET_LEDGER";
                SetReportBorder();

                Detail.SortFields.Clear();
                //07/07/2020
                if (AppSetting.IS_CMF_CONGREGATION)
                {
                    this.ReportProperties.SortByLedger = 1;//fix by default for cmf

                    grpLedgerGroupHeader.SortingSummary.Enabled = true;
                    grpLedgerGroupHeader.SortingSummary.FieldName = "NATURE_ID";
                    grpLedgerGroupHeader.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroupHeader.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                    Detail.SortFields.Add(new GroupField("LEDGER_GROUP"));
                }


                grpBudgetSubGroupHeader.GroupFields[0].FieldName = "";

                if (grpBudgetSubGroupHeader.Visible)
                {
                    grpBudgetSubGroupHeader.GroupFields[0].FieldName = reportSetting1.Receipts.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;
                }

                //For Temp : On 29/02/2024, To have proper budget group order (fix opening balance is top)
                //Later it will be changed budget group sort id order ---------------------------------------------------------
                if (grpBudgetHeader.Visible)
                {
                    grpBudgetGroupHeader.SortingSummary.Enabled = true;
                    grpBudgetGroupHeader.SortingSummary.FieldName = "BUDGET_GROUP_SORT_ID";
                    grpBudgetGroupHeader.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpBudgetGroupHeader.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                //-------------------------------------------------------------------------------------------------------------

                if (this.ReportProperties.SortByLedger == 1)
                    Detail.SortFields.Add(new GroupField("LEDGER_NAME"));
                else
                    Detail.SortFields.Add(new GroupField("LEDGER_CODE"));

                //On 18/07/2023, To show all budget mapped ledgers
                if (this.ReportProperties.IncludeAllBudgetLedgers == 0)
                {
                    //# 07/07/2020, load only amount enabled ledgers
                    string filter = string.Empty;
                    if (this.AppSetting.IS_SDB_INM)
                    {
                        filter = "(ACTUAL <> 0 OR APPROVED_PREVIOUS_YR <>0 OR PROPOSED_CURRENT_YR <>0 OR APPROVED_CURRENT_YR <>0 OR ACTUAL>0)";
                    }
                    else
                    {
                        filter = "(ACTUAL <> 0 OR APPROVED_PREVIOUS_YR <>0 OR PROPOSED_CURRENT_YR <>0 OR APPROVED_CURRENT_YR <>0)";
                    }
                    //On 29/01/2022, allow cash, bank and fd zero values
                    filter += " OR GROUP_ID IN (12, 13, 14)";
                    dtIncomeSource.DefaultView.RowFilter = filter;
                    dtIncomeSource = dtIncomeSource.DefaultView.ToTable();
                }

                this.DataSource = dtIncomeSource;
                this.DataMember = dtIncomeSource.TableName;

                ShowCMFNoteToProvince();

                HideSomePropertyforSDBINM();
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }
        }

        private void HideSomePropertyforSDBINM()
        {
            if (this.AppSetting.IS_SDB_INM)
            {
                grpBudgetSubGroupHeader.Visible = false;

                xrTableCell28.Visible = false;

                xrgrpLedgerGroup.Visible = false;

                xrGrpLGPreviousApprovedSum.Visible = false;

                xrGrpLGActualSum.Visible = false;

                xrGrpLGProposedSum.Visible = false;


                xrGrpLGApprovedSum.Visible = false;
            }
            else
            {
                grpBudgetSubGroupHeader.Visible = true;

                xrTableCell28.Visible = true;
                xrTableCell28.WidthF = 125;

                xrgrpLedgerGroup.Visible = true;
                xrgrpLedgerGroup.WidthF = 125;

                xrGrpLGPreviousApprovedSum.Visible = true;
                xrGrpLGPreviousApprovedSum.WidthF = 125;

                xrGrpLGActualSum.Visible = true;
                xrGrpLGActualSum.WidthF = 125;

                xrGrpLGProposedSum.Visible = true;
                xrGrpLGProposedSum.WidthF = 125;

                xrGrpLGApprovedSum.Visible = true;
                xrGrpLGApprovedSum.WidthF = 125;
            }
        }

        private void SetReportBorder()
        {
            xrtblLedger = AlignContentTable(xrtblLedger);
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTblSumFooter = AlignContentTable(xrTblSumFooter);
            xrtblGrpBudgetGroup = AlignContentTable(xrtblGrpBudgetGroup);

            if (AppSetting.IS_CMF_CONGREGATION)
            {

                float actualCodeWidth = xrCapCode.WidthF;

                //Include / Exclude Code
                if (xrCapCode.Tag != null && xrCapCode.Tag.ToString() != "")
                {
                    actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapCode.Tag.ToString());
                }
                else
                {
                    xrCapCode.Tag = xrCapCode.WidthF;
                }


                xrCapCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
                xrCapCode1.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
                xrLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            }
        }

        /// <summary>
        /// Add cash/bank group balance in Budget ledger
        /// </summary>
        /// <param name="dtIncomeBudget"></param>
        private void AppendCashBankBalances(DataTable dtIncomeBudget)
        {
            //string PrevDateFrom = UtilityMember.DateSet.ToDate(dtBudgetDateFrom.AddYears(-1).ToShortDateString());
            //string  PrevDateTo = UtilityMember.DateSet.ToDate(dtBudgetDateTo.AddYears(-1).ToShortDateString());

            string PrevDateFrom = UtilityMember.DateSet.ToDate(dtBudgetDateFrom.AddYears(-1).ToShortDateString());
            double CashOPBalance = 0;
            double CashCLBalance = 0;
            double BankOPBalance = 0;
            double BankCLBalance = 0;
            double FDOPBalance = 0;
            double FDCLBalance = 0;
            if (this.AppSetting.IS_SDB_INM)
            {
                bool enforceCurrency = (settingProperty.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0 ? true : false);

                CashOPBalance = this.GetBalance(BudgetProjects, UtilityMember.DateSet.ToDate(PrevDateFrom), BalanceSystem.LiquidBalanceGroup.CashBalance,
                        BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                CashCLBalance = this.GetBalance(BudgetProjects, UtilityMember.DateSet.ToDate(dtPrevBudgetDateTo.ToShortDateString()), BalanceSystem.LiquidBalanceGroup.CashBalance,
                        BalanceSystem.BalanceType.ClosingBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);

                BankOPBalance = this.GetBalance(BudgetProjects, PrevDateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                        BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                BankCLBalance = this.GetBalance(BudgetProjects, dtPrevBudgetDateTo.ToShortDateString(), BalanceSystem.LiquidBalanceGroup.BankBalance,
                        BalanceSystem.BalanceType.ClosingBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);

                FDOPBalance = this.GetBalance(BudgetProjects, PrevDateFrom, BalanceSystem.LiquidBalanceGroup.FDBalance,
                        BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                FDCLBalance = this.GetBalance(BudgetProjects, dtPrevBudgetDateTo.ToShortDateString(), BalanceSystem.LiquidBalanceGroup.FDBalance,
                        BalanceSystem.BalanceType.ClosingBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
            }
            else
            {

                bool enforceCurrency = (settingProperty.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0 ? true : false);

                CashOPBalance = this.GetBalance(BudgetProjects, UtilityMember.DateSet.ToDate(dtBudgetDateFrom.ToShortDateString()), BalanceSystem.LiquidBalanceGroup.CashBalance,
                        BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                CashCLBalance = this.GetBalance(BudgetProjects, UtilityMember.DateSet.ToDate(dtPrevBudgetDateTo.ToShortDateString()), BalanceSystem.LiquidBalanceGroup.CashBalance,
                        BalanceSystem.BalanceType.ClosingBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);

                BankOPBalance = this.GetBalance(BudgetProjects, dtBudgetDateFrom.ToShortDateString(), BalanceSystem.LiquidBalanceGroup.BankBalance,
                        BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                BankCLBalance = this.GetBalance(BudgetProjects, dtPrevBudgetDateTo.ToShortDateString(), BalanceSystem.LiquidBalanceGroup.BankBalance,
                        BalanceSystem.BalanceType.ClosingBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);

                FDOPBalance = this.GetBalance(BudgetProjects, dtBudgetDateFrom.ToShortDateString(), BalanceSystem.LiquidBalanceGroup.FDBalance,
                        BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                FDCLBalance = this.GetBalance(BudgetProjects, dtPrevBudgetDateTo.ToShortDateString(), BalanceSystem.LiquidBalanceGroup.FDBalance,
                        BalanceSystem.BalanceType.ClosingBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
            }


            DataRow drCashBank = dtIncomeBudget.NewRow();
            drCashBank["GROUP_ID"] = 0;
            drCashBank["LEDGER_ID"] = 0;
            drCashBank["LEDGER_CODE"] = string.Empty;
            drCashBank["LEDGER_NAME"] = "Cash + Bank";
            drCashBank["BUDGET_GROUP"] = "Opening Balance";
            drCashBank["BUDGET_SUB_GROUP"] = string.Empty;
            drCashBank["BUDGET_GROUP_ID"] = 0;
            drCashBank["BUDGET_SUB_GROUP_ID"] = 0;
            drCashBank["NARRATION"] = string.Empty;
            drCashBank["LEDGER_GROUP"] = string.Empty;
            drCashBank["BUDGET_TRANS_MODE"] = string.Empty;

            drCashBank["ACTUAL"] = (CashOPBalance + BankOPBalance).ToString();
            drCashBank["PROPOSED_CURRENT_YR"] = (CashCLBalance + BankCLBalance).ToString();
            drCashBank["APPROVED_CURRENT_YR"] = (CashCLBalance + BankCLBalance).ToString();
            drCashBank["APPROVED_PREVIOUS_YR"] = 0;
            dtIncomeBudget.Rows.Add(drCashBank);

            DataRow drFD = dtIncomeBudget.NewRow();
            drFD["GROUP_ID"] = 0;
            drFD["LEDGER_ID"] = 0;
            drFD["LEDGER_CODE"] = string.Empty;
            drFD["LEDGER_NAME"] = "Fixed Deposit (Scholarship)";
            drFD["BUDGET_GROUP"] = "Opening Balance";
            drFD["BUDGET_SUB_GROUP"] = string.Empty;
            drFD["BUDGET_GROUP_ID"] = 0;
            drFD["BUDGET_SUB_GROUP_ID"] = 0;
            drFD["NARRATION"] = string.Empty;
            drFD["LEDGER_GROUP"] = string.Empty;
            drFD["BUDGET_TRANS_MODE"] = string.Empty;

            drFD["ACTUAL"] = FDOPBalance.ToString();
            drFD["PROPOSED_CURRENT_YR"] = FDCLBalance.ToString();
            drFD["APPROVED_CURRENT_YR"] = FDCLBalance.ToString(); ;
            drFD["APPROVED_PREVIOUS_YR"] = 0;
            dtIncomeBudget.Rows.Add(drFD);
        }

        /// <summary>
        /// Add detail cash/Bank ledger details into budget ledgers
        /// </summary>
        /// <param name="dtIncomeBudget"></param>
        /// <param name="dtCashBankDetail"></param>
        /// <param name="isOpeningbalance"></param>
        private void AppendCashBankDetailBalances(DataTable dtIncomeBudget, bool isAttachClosingBalanes = false)
        {
            string OpeningDate = dtPrevBudgetDateFrom.ToShortDateString();
            string ClosingDate = dtPrevBudgetDateTo.ToShortDateString();

            //For 06/07/2020, to attach closint balance too
            if (isAttachClosingBalanes)
            {
                OpeningDate = dtBudgetDateFrom.ToShortDateString();
                ClosingDate = dtBudgetDateTo.ToShortDateString();
            }

            //1. Append Cash detail balance
            ResultArgs resultarg = this.GetBalanceDetail(true, OpeningDate, BudgetProjects, ((int)FixedLedgerGroup.Cash).ToString());
            if (resultarg.Success)
            {
                ReportProperties.EnforceSkipDefaultLedgers(resultarg, reportSetting1.Ledger.LEDGER_IDColumn.ColumnName);
                DataTable dtCashDetails = resultarg.DataSource.Table;
                AppendCashBankFDRow(dtIncomeBudget, dtCashDetails, true, isAttachClosingBalanes);
                resultarg = this.GetBalanceDetail(false, ClosingDate, BudgetProjects, ((int)FixedLedgerGroup.Cash).ToString());
                if (resultarg.Success)
                {
                    ReportProperties.EnforceSkipDefaultLedgers(resultarg, reportSetting1.Ledger.LEDGER_IDColumn.ColumnName);
                    dtCashDetails = resultarg.DataSource.Table;
                    AppendCashBankFDRow(dtIncomeBudget, dtCashDetails, false, isAttachClosingBalanes);
                }
            }

            //2. Append Bank detail balance
            resultarg = this.GetBalanceDetail(true, OpeningDate, BudgetProjects, ((int)FixedLedgerGroup.BankAccounts).ToString());
            if (resultarg.Success)
            {
                DataTable dtBankDetails = resultarg.DataSource.Table;
                AppendCashBankFDRow(dtIncomeBudget, dtBankDetails, true, isAttachClosingBalanes);
                resultarg = this.GetBalanceDetail(false, ClosingDate, BudgetProjects, ((int)FixedLedgerGroup.BankAccounts).ToString());
                if (resultarg.Success)
                {
                    dtBankDetails = resultarg.DataSource.Table;
                    AppendCashBankFDRow(dtIncomeBudget, dtBankDetails, false, isAttachClosingBalanes);
                }
            }

            //3. FD balance detail
            resultarg = this.GetBalanceDetail(true, OpeningDate, BudgetProjects, ((int)FixedLedgerGroup.FixedDeposit).ToString());
            if (resultarg.Success)
            {
                ReportProperties.EnforceSkipDefaultLedgers(resultarg, reportSetting1.Ledger.LEDGER_IDColumn.ColumnName);
                DataTable dtFDDetails = resultarg.DataSource.Table;
                AppendCashBankFDRow(dtIncomeBudget, dtFDDetails, true, isAttachClosingBalanes);
                resultarg = this.GetBalanceDetail(false, ClosingDate, BudgetProjects, ((int)FixedLedgerGroup.FixedDeposit).ToString());
                if (resultarg.Success)
                {
                    ReportProperties.EnforceSkipDefaultLedgers(resultarg, reportSetting1.Ledger.LEDGER_IDColumn.ColumnName);
                    dtFDDetails = resultarg.DataSource.Table;
                    AppendCashBankFDRow(dtIncomeBudget, dtFDDetails, false, isAttachClosingBalanes);
                }
            }

        }

        /// <summary>
        /// Add detail cash/Bank/fd ledger details into budget ledgers
        /// </summary>
        /// <param name="dtIncomeBudget"></param>
        /// <param name="dtCashBankDetail"></param>
        /// <param name="isOpeningbalance"></param>
        private void AppendCashBankFDRow(DataTable dtIncomeBudget, DataTable dtCashBankDetail, bool isOpeningbalance, bool isAttachClosingBalanes = false)
        {
            foreach (DataRow dr in dtCashBankDetail.Rows)
            {
                Int32 cashbankledgerid = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                dtIncomeBudget.DefaultView.RowFilter = "LEDGER_ID=" + cashbankledgerid;

                double balanceamt = this.UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                if (dr["TRANS_MODE"].ToString().ToUpper() == "CR")
                    balanceamt = -this.UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());

                if (dtIncomeBudget.DefaultView.Count > 0)
                {
                    //dtIncomeBudget.DefaultView[0]["PROPOSED_CURRENT_YR"] = balanceamt;
                    //dtIncomeBudget.DefaultView[0]["APPROVED_CURRENT_YR"] = balanceamt;
                    //31/07/2020, for cmf
                    if (isAttachClosingBalanes)
                    {
                        if (!AppSetting.IS_CMF_CONGREGATION)
                        {
                            dtIncomeBudget.DefaultView[0]["PROPOSED_CURRENT_YR"] = balanceamt;
                            dtIncomeBudget.DefaultView[0]["APPROVED_CURRENT_YR"] = balanceamt;
                        }
                    }
                    else
                    {
                        dtIncomeBudget.DefaultView[0]["PROPOSED_CURRENT_YR"] = balanceamt;
                        dtIncomeBudget.DefaultView[0]["APPROVED_CURRENT_YR"] = balanceamt;
                    }

                }
                else
                {
                    DataRow drCashBank = dtIncomeBudget.NewRow();
                    Int32 groupid = UtilityMember.NumberSet.ToInteger(dr["GROUP_ID"].ToString());
                    drCashBank["NATURE"] = "Asset";
                    //on 07/07/2020, to show ledger group opening and closing balances in first and last of the report
                    drCashBank["NATURE_ID"] = isAttachClosingBalanes ? 5 : 0;//(Int32)Natures.Assert; 
                    drCashBank["GROUP_ID"] = groupid;
                    drCashBank["LEDGER_ID"] = cashbankledgerid;
                    drCashBank["LEDGER_CODE"] = string.Empty;
                    drCashBank["LEDGER_NAME"] = dr["LEDGER_NAME"].ToString();

                    //On 27/01/2022 for CMF
                    drCashBank["BUDGET_GROUP"] = string.Empty; //isAttachClosingBalanes ? "Closing Balance" : "Opening Balance";

                    drCashBank["BUDGET_SUB_GROUP"] = string.Empty;
                    drCashBank["BUDGET_GROUP_ID"] = 0;
                    drCashBank["BUDGET_SUB_GROUP_ID"] = 0;
                    drCashBank["NARRATION"] = string.Empty;
                    drCashBank["LEDGER_GROUP"] = (groupid == (int)FixedLedgerGroup.FixedDeposit ? " " : string.Empty) + dr["LEDGER_GROUP"].ToString();
                    drCashBank["BUDGET_TRANS_MODE"] = string.Empty;
                    drCashBank["APPROVED_PREVIOUS_YR"] = 0;

                    //31/07/2020, for cmf
                    if (isAttachClosingBalanes)
                    {
                        if (!AppSetting.IS_CMF_CONGREGATION)
                        {
                            drCashBank["APPROVED_PREVIOUS_YR"] = balanceamt;
                        }
                    }
                    else
                    {
                        drCashBank["APPROVED_PREVIOUS_YR"] = balanceamt;
                    }

                    drCashBank["ACTUAL"] = balanceamt;
                    //drCashBank["PROPOSED_CURRENT_YR"] = dr["AMOUNT"].ToString();
                    drCashBank["APPROVED_CURRENT_YR"] = 0;
                    dtIncomeBudget.DefaultView.Table.Rows.Add(drCashBank);
                }
                dtIncomeBudget.DefaultView.Table.AcceptChanges();

                dtIncomeBudget.DefaultView.RowFilter = string.Empty;
            }
        }

        /// <summary>
        /// This method is used to attach HO/Province Help amount with Income side of Budget
        /// </summary>
        /// <param name="dtIncomeSource"></param>
        private void AppendHOHelpAmountInIncome(DataTable dtIncomeSource)
        {
            decimal HOProvinceHelpPreviousApprovedAmount = GetPreviousBudgetHOApprovedAmount();
            ResultArgs resultArgs = new ResultArgs();
            if (dtIncomeSource.Rows.Count > 0 && AppSetting.ENABLE_BUDGET_HO_HELP_AMOUNT)
            {
                string budgetinfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetInfo);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.BudgetId);
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetinfo);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        HOBudgetHelpPropsedAmt = UtilityMember.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName].ToString());
                        HOBudgetHelpApprovedAmt = UtilityMember.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][reportSetting1.BUDGETVARIANCE.HO_HELP_APPROVED_AMOUNTColumn.ColumnName].ToString());
                        DataRow drHOBudgetHelpAmount = dtIncomeSource.NewRow();
                        drHOBudgetHelpAmount[reportSetting1.ReportParameter.GROUP_IDColumn.ColumnName] = 0;
                        drHOBudgetHelpAmount[reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName] = 0;
                        drHOBudgetHelpAmount[reportSetting1.Ledger.LEDGER_CODEColumn.ColumnName] = "  ";//To make to come first order
                        drHOBudgetHelpAmount[reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName] = AppSetting.BUDGET_HO_HELP_AMOUNT_CAPTION;
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName] = string.Empty;
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_SUB_GROUPColumn.ColumnName] = string.Empty;
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_GROUP_IDColumn.ColumnName] = 0;
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_SUB_GROUP_IDColumn.ColumnName] = 0;
                        drHOBudgetHelpAmount[reportSetting1.ReportParameter.NARRATIONColumn.ColumnName] = string.Empty;
                        drHOBudgetHelpAmount[reportSetting1.Receipts.LEDGER_GROUPColumn.ColumnName] = string.Empty;
                        drHOBudgetHelpAmount[reportSetting1.BUDGET_LEDGER.BUDGET_TRANS_MODEColumn.ColumnName] = string.Empty;

                        drHOBudgetHelpAmount[reportSetting1.BUDGET_LEDGER.ACTUALColumn.ColumnName] = 0;
                        drHOBudgetHelpAmount[reportSetting1.BUDGET_LEDGER.PROPOSED_CURRENT_YRColumn.ColumnName] = HOBudgetHelpPropsedAmt.ToString();
                        drHOBudgetHelpAmount[reportSetting1.BUDGET_LEDGER.APPROVED_CURRENT_YRColumn.ColumnName] = HOBudgetHelpApprovedAmt.ToString();
                        drHOBudgetHelpAmount[reportSetting1.BUDGET_LEDGER.APPROVED_PREVIOUS_YRColumn.ColumnName] = HOProvinceHelpPreviousApprovedAmount.ToString();
                        dtIncomeSource.Rows.Add(drHOBudgetHelpAmount);
                    }
                }
            }
        }

        private decimal GetPreviousBudgetHOApprovedAmount()
        {
            ResultArgs resultargs = new ResultArgs();
            Int32 PrevBudgetId = 0;
            decimal HOBudgetHelpApprovedPreviousBudgetAmt = 0;

            using (DataManager dataManager = new DataManager(SQLCommand.Budget.FetchBudgetIdByDateRangeProject))
            {
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_FROMColumn, dtPrevBudgetDateFrom);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_TOColumn, dtPrevBudgetDateTo);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn, BudgetProjects);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            if (resultargs.Success && resultargs.DataSource.Table != null && resultargs.DataSource.Table.Rows.Count > 0)
            {
                PrevBudgetId = UtilityMember.NumberSet.ToInteger(resultargs.DataSource.Table.Rows[0][this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn.ColumnName].ToString());
            }

            if (PrevBudgetId > 0)
            {
                string budgetinfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetInfo);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, PrevBudgetId);
                    ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetinfo);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        //UtilityMember.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName].ToString());
                        HOBudgetHelpApprovedPreviousBudgetAmt = UtilityMember.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][reportSetting1.BUDGETVARIANCE.HO_HELP_APPROVED_AMOUNTColumn.ColumnName].ToString());
                    }
                }
            }

            return HOBudgetHelpApprovedPreviousBudgetAmt;
        }

        private void hideGroupAmount(XRTableCell cell)
        {
            if (GetCurrentColumnValue("GROUP_ID") != null)
            {
                int groupid = UtilityMember.NumberSet.ToInteger(cell.Tag.ToString());
                if (!(groupid == (int)FixedLedgerGroup.Cash || groupid == (int)FixedLedgerGroup.BankAccounts || groupid == (int)FixedLedgerGroup.FixedDeposit))
                {
                    //cell.Text = "";
                    //On 07/07/2020, to show ledger group total
                    if (!(AppSetting.IS_CMF_CONGREGATION))
                    {
                        cell.Text = "";
                    }
                }
            }
        }

        private void ShowCMFNoteToProvince()
        {
            //xrcellfooter.Text = string.Empty;

            if (AppSetting.IS_CMF_CONGREGATION && BudgetTransMode == TransMode.DR)
            {
                if (this.DataSource != null)
                {
                    DataTable dtRpt = this.DataSource as DataTable;
                    string ordinaryexpense = reportSetting1.BUDGET_LEDGER.BUDGET_GROUPColumn.ColumnName + "='Ordinary Expenses'";
                    ordinaryExprense = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.BUDGET_LEDGER.PROPOSED_CURRENT_YRColumn.ColumnName + ")", ordinaryexpense).ToString());

                    ordinaryexpense = reportSetting1.BUDGET_LEDGER.BUDGET_GROUPColumn.ColumnName + "='Extraordinary Expenses'";
                    extraordinaryExprense = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.BUDGET_LEDGER.PROPOSED_CURRENT_YRColumn.ColumnName + ")", ordinaryexpense).ToString());
                    //ordinaryExprense += 797830389;
                    amtDifference = ordinaryExprense - (totalBudgetedRawProposedIncome + cashOpeningBalance + bankOpeningBalance);

                    //if (amtDifference != 0)
                    //{
                    //xrcellCMFNote.Visible = true;
                    grpFooterCMFBudgetNote.Visible = true;
                    string notemsg = string.Empty;
                    /*notemsg = " " + System.Environment.NewLine;
                    notemsg = System.Environment.NewLine + "BUDGET NOTE: " + System.Environment.NewLine;
                    notemsg += "Opening Balance : " +  System.Environment.NewLine;
                    notemsg += "   Cash          : " + UtilityMember.NumberSet.ToNumber(cashOpeningBalance) + System.Environment.NewLine;
                    notemsg += "   Bank          : " + UtilityMember.NumberSet.ToNumber(bankOpeningBalance) + System.Environment.NewLine;
                    notemsg += "   Fixed Deposit : " + UtilityMember.NumberSet.ToNumber(fdOpeningBalance) + System.Environment.NewLine;
                    notemsg += System.Environment.NewLine;
                    notemsg += "Budgeted Proposed Income    : " + UtilityMember.NumberSet.ToNumber(totalBudgetedRawProposedIncome) + System.Environment.NewLine;
                    notemsg += "Budgeted Ordinary Expenses  : " + UtilityMember.NumberSet.ToNumber(ordinaryExprense) + System.Environment.NewLine;
                    notemsg += System.Environment.NewLine;*/

                    //xrcellCMFNoteOpBalance.Text = UtilityMember.NumberSet.ToNumber(totalOpeningBalance);
                    //xrcellCMFNoteCashOpBalance.Text = UtilityMember.NumberSet.ToNumber(cashOpeningBalance);
                    //xrcellCMFNoteBankOpBalance.Text = UtilityMember.NumberSet.ToNumber(bankOpeningBalance);
                    //xrcellCMFNoteFDOpBalance.Text = UtilityMember.NumberSet.ToNumber(fdOpeningBalance);
                    //xrcellCMFNoteTotalBudgetIncome.Text = UtilityMember.NumberSet.ToNumber(totalBudgetedRawProposedIncome);
                    //xrcellCMFNoteTotalOrdinaryExpense.Text = UtilityMember.NumberSet.ToNumber(ordinaryExprense);
                    //xrcellCMFNoteTotalExtraOrdinaryExpense.Text = UtilityMember.NumberSet.ToNumber(extraordinaryExprense);

                    if (amtDifference == 0)
                    {
                        xrcellCMFNote.Text = "Surplus/Deficit";
                    }
                    else if (amtDifference > 0)
                    {
                        xrcellCMFNote.Text = "Deficit contribute by Province";
                    }
                    else
                    {
                        xrcellCMFNote.Text = "Surplus transfer to Province";
                    }
                    //xrcellCMFNoteAmount.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(amtDifference));

                    //notemsg += System.Environment.NewLine;
                    //xrcellNoteToCMFProvince.Text = notemsg;
                    //xrcellNoteToCMFProvince.Font = new Font(xrcellGrandTotal.Font.FontFamily, 12, FontStyle.Bold);
                    //xrcellNoteToCMFProvince.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    //}
                }
            }
        }
        #endregion

        #region Events
        private void xrgrpLedgerValue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetgroup = label.Text;
            label.Text = budgetgroup.ToString().ToUpper();
        }

        private void grpBudgetSubGroupHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((!AppSetting.IS_ABEBEN_DIOCESE) || (!(AppSetting.IS_DIOMYS_DIOCESE)) || (!(AppSetting.IS_CMF_CONGREGATION)))
            {
                DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
                if (drvcrrentrow != null)
                {
                    string budgetsubgrp = this.GetCurrentColumnValue("BUDGET_SUB_GROUP") == null ? string.Empty : this.GetCurrentColumnValue("BUDGET_SUB_GROUP").ToString();
                    if (string.IsNullOrEmpty(budgetsubgrp))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void xrLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE || AppSetting.IS_CMF_CONGREGATION)
            {
                if (this.GetCurrentColumnValue("GROUP_ID") != null)
                {
                    Int32 activegroupid = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue("GROUP_ID").ToString());
                    XRTableCell xrcell = sender as XRTableCell;
                    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                    {
                        xrcell.Font = new Font(xrcell.Font.FontFamily, 9);
                    }
                    else
                    {
                        xrcell.Font = new Font(xrcell.Font, FontStyle.Regular);
                    }
                }
            }
        }

        private void xrgrpLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cellLedgerGroup = sender as XRTableCell;
            cellLedgerGroup.Text = cellLedgerGroup.Text.Trim();
        }

        private void xrGrpLGProposedSum_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //On 17/07/2024, To show Ledger Group Total too
            /*if (GetCurrentColumnValue("GROUP_ID") != null)
            {
                XRTableCell xrCell = sender as XRTableCell;
                hideGroupAmount(xrCell);
            }*/
        }

        private void xrGrpLGApprovedSum_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //On 17/07/2024, To show Ledger Group Total too
            /*if (GetCurrentColumnValue("GROUP_ID") != null)
            {
                XRTableCell xrCell = sender as XRTableCell;
                hideGroupAmount(xrCell);
            }*/
        }

        private void xrGrpLGActualSum_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //On 17/07/2024, To show Ledger Group Total too
            /*if (GetCurrentColumnValue("GROUP_ID") != null)
            {
                XRTableCell xrCell = sender as XRTableCell;
                hideGroupAmount(xrCell);
            }*/
        }

        private void grpLedgerGroupHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GroupHeaderBand grpLedgerGroup = sender as GroupHeaderBand;
            xrRowOPCL.Visible = false;
            if (GetCurrentColumnValue("GROUP_ID") != null)
            {
                Int32 groupid = this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("GROUP_ID").ToString());
                if (groupid == (int)FixedLedgerGroup.Cash)
                {
                    xrRowOPCL.Visible = true;
                    if (AppSetting.IS_CMF_CONGREGATION)
                    {
                        xrCellLedgerGrpOpClBalances.Text = BudgetTransMode == TransMode.CR ? "OPENING BALANCE" : "CLOSING BALANCE";
                    }
                    else
                    {
                        xrCellLedgerGrpOpClBalances.Text = BudgetTransMode == TransMode.CR ? "Opening Balance" : "Closing Balance";
                    }
                }

                //on 03/08/2020, for cmf
                xrHeaderRowLGSum.Visible = true;
                if (AppSetting.IS_CMF_CONGREGATION &&
                    (groupid == (int)FixedLedgerGroup.Cash || groupid == (int)FixedLedgerGroup.BankAccounts || groupid == (int)FixedLedgerGroup.FixedDeposit))
                {
                    xrHeaderRowLGSum.Visible = false;
                }
            }
        }

        private void xrGrpLGPreviousApprovedSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 17/07/2024, To show Ledger Group Total too
            /*e.Result = "";
            e.Handled = true;*/
        }

        private void xrGrpLGActualSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 17/07/2024, To show Ledger Group Total too
            /*e.Result = "";
            e.Handled = true;*/
        }

        private void xrGrpLGProposedSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 17/07/2024, To show Ledger Group Total too
            /*e.Result = "";
            e.Handled = true;*/
        }

        private void xrGrpLGApprovedSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 17/07/2024, To show Ledger Group Total too
            /*e.Result = "";
            e.Handled = true;*/
        }

        private void xrGSumApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (BudgetTransMode == TransMode.DR)
                e.Result = totalApprovedBudgetedExpense; //UtilityMember.NumberSet.ToNumber(totalApprovedBudgetedExpense);
            else
                e.Result = totalApprovedBudgetedIncome; // UtilityMember.NumberSet.ToNumber(totalApprovedBudgetedIncome);
            e.Handled = true;

            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                double closingbalance = TotalApprovedBudgetedIncome - TotalApprovedBudgetedExpense;
                e.Result = TotalApprovedBudgetedExpense + closingbalance;
                e.Handled = true;
            }
        }

        private void xrGSumProposed_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (BudgetTransMode == TransMode.DR)
                e.Result = totalProposedBudgetedExpense; //UtilityMember.NumberSet.ToNumber(totalProposedBudgetedExpense);
            else
                e.Result = totalProposedBudgetedIncome; //UtilityMember.NumberSet.ToNumber(totalProposedBudgetedIncome);
            e.Handled = true;

            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                double closingbalance = TotalProposedBudgetedIncome - TotalProposedBudgetedExpense;
                e.Result = TotalProposedBudgetedExpense + closingbalance;
                e.Handled = true;
            }
        }

        private void xrSPrevSumAapproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                e.Result = TotalPrevApprovedBudgetedIncome - TotalPrevApprovedBudgetedExpense;
                e.Handled = true;
            }
        }

        private void xrSPrevSumPrevActual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                double prevActual = 0;
                if (this.DataSource != null)
                {
                    DataTable dtRpt = this.DataSource as DataTable;
                    //double prevApproved = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(APPROVED_PREVIOUS_YR)", "GROUP_ID IN (12, 13, 14)").ToString());
                    prevActual = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(ACTUAL)", "GROUP_ID IN (12, 13, 14)").ToString());
                    //double currentProposed = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(PROPOSED_CURRENT_YR)", "GROUP_ID IN (12, 13, 14)").ToString());
                    //double currentApproved = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(APPROVED_CURRENT_YR)", "GROUP_ID IN (12, 13, 14)").ToString());
                }
                e.Result = prevActual;
                e.Handled = true;
            }
        }

        private void xrSSumProposed_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                e.Result = TotalProposedBudgetedIncome - TotalProposedBudgetedExpense;
                e.Handled = true;
            }
        }

        private void xrGPrevSumAapproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (BudgetTransMode == TransMode.DR)
                e.Result = TotalPrevApprovedBudgetedExpense;
            else
                e.Result = totalPrevApprovedBudgetedIncome;
            e.Handled = true;

            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                double closingbalance = TotalPrevApprovedBudgetedIncome - TotalPrevApprovedBudgetedExpense;
                e.Result = TotalPrevApprovedBudgetedExpense + closingbalance;
                e.Handled = true;
            }
        }

        private void grpLedgerGroupFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.AppSetting.IS_CMF_CONGREGATION)
            {
                if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    int activeNextgroupid = 0;
                    if (GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName) != null)
                    {
                        activeNextgroupid = UtilityMember.NumberSet.ToInteger(GetNextColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    }

                    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                    {
                        e.Cancel = true;
                        if (BudgetTransMode == TransMode.CR)
                        {
                            if (activeNextgroupid != ((int)FixedLedgerGroup.Cash) && activeNextgroupid != ((int)FixedLedgerGroup.BankAccounts) && activeNextgroupid != ((int)FixedLedgerGroup.FixedDeposit))
                            {
                                e.Cancel = false;
                            }
                        }
                        else if (BudgetTransMode == TransMode.DR)
                        {
                            if (activeNextgroupid == 0)
                            {
                                e.Cancel = false;
                            }
                        }
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
        }

        private void grpBudgetFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrRowGrandSTotalCLBalance.Visible = false;
            //xrFooterRowSign.Visible = true;

            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                xrRowGrandSTotalCLBalance.Visible = true;
            }
        }

        private void xrGrpLGPreviousApprovedSumFooter_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.CR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                {
                    double prevApproved = 0;
                    if (this.DataSource != null)
                    {
                        DataTable dtRpt = this.DataSource as DataTable;
                        prevApproved = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(APPROVED_PREVIOUS_YR)", "GROUP_ID IN (12, 13, 14)").ToString());
                    }
                    e.Result = prevApproved;
                    e.Handled = true;
                }
            }
        }

        private void xrGrpLGActualSumFooter_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.CR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                {
                    double prevActual = 0;
                    if (this.DataSource != null)
                    {
                        DataTable dtRpt = this.DataSource as DataTable;
                        prevActual = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(ACTUAL)", "GROUP_ID IN (12, 13, 14)").ToString());
                    }
                    e.Result = prevActual;
                    e.Handled = true;
                }
            }
        }

        private void xrGrpLGProposedSumFooter_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.CR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                {
                    double currentProposed = 0;
                    if (this.DataSource != null)
                    {
                        DataTable dtRpt = this.DataSource as DataTable;
                        currentProposed = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(PROPOSED_CURRENT_YR)", "GROUP_ID IN (12, 13, 14)").ToString());
                    }
                    e.Result = currentProposed;
                    e.Handled = true;
                }
            }
        }

        private void xrGrpLGApprovedSumFooter_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 03/08/2020, for cmf show cash, bank and fd closing balance
            if (BudgetTransMode == TransMode.CR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                {
                    double currentApproved = 0;
                    if (this.DataSource != null)
                    {
                        DataTable dtRpt = this.DataSource as DataTable;
                        currentApproved = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(APPROVED_CURRENT_YR)", "GROUP_ID IN (12, 13, 14)").ToString());
                    }
                    e.Result = currentApproved;
                    e.Handled = true;
                }
            }
        }

        private void xrCellLedgerGrpOpClBalances_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (AppSetting.IS_CMF_CONGREGATION)
            {
                cell.Font = new Font(cell.Font.FontFamily, 16);
            }
        }

        private void xrPreviousApprovedAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                {
                    cell.Text = "";
                }
            }
        }

        private void xrCurrentProposedAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                {
                    cell.Text = "";
                }
            }
        }

        private void xrCurrentApprovedAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (BudgetTransMode == TransMode.DR && this.AppSetting.IS_CMF_CONGREGATION)
            {
                int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                {
                    cell.Text = "";
                }
            }
        }

        private void xrcellGrandTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (AppSetting.IS_CMF_CONGREGATION)
            {
                cell.Font = new Font(cell.Font.FontFamily, 16);
            }
        }

        private void xrNarration_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION)
                {
                    (sender as XRTableCell).TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
            }
        }

        private void xrgrpBudgetGroupSubTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName) != null)
                {
                    (sender as XRTableCell).Text = GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName).ToString().ToUpper() + " TOTAL";
                }
            }
        }

        private void grpBudgetGroupFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName) != null)
                {
                    int activegroupid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName).ToString());
                    if (activegroupid == ((int)FixedLedgerGroup.Cash) || activegroupid == ((int)FixedLedgerGroup.BankAccounts) || activegroupid == ((int)FixedLedgerGroup.FixedDeposit))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void xrcellTTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (AppSetting.IS_CMF_CONGREGATION && BudgetTransMode == TransMode.CR)
            {
                cell.Text = "Total Income";
                cell.Font = new Font(cell.Font.FontFamily, 14);
            }
        }

        private void xrTPrevSumAapproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName) != null)
                {
                    if (this.DataSource != null)
                    {
                        DataTable dtRpt = this.DataSource as DataTable;
                        e.Result = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.BUDGET_LEDGER.APPROVED_PREVIOUS_YRColumn.ColumnName + ")", "GROUP_ID NOT IN (12, 13, 14)").ToString());
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrTPrevSumActual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName) != null)
                {
                    if (this.DataSource != null)
                    {
                        DataTable dtRpt = this.DataSource as DataTable;
                        e.Result = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.BUDGET_LEDGER.ACTUALColumn.ColumnName + ")", "GROUP_ID NOT IN (12, 13, 14)").ToString());
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrTSumProposed_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName) != null)
                {
                    if (this.DataSource != null)
                    {
                        DataTable dtRpt = this.DataSource as DataTable;
                        e.Result = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.BUDGET_LEDGER.PROPOSED_CURRENT_YRColumn.ColumnName + ")", "GROUP_ID NOT IN (12, 13, 14)").ToString());
                        e.Handled = true;
                    }
                }
            }
        }

        private void xrTSumApproved_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.NARRATIONColumn.ColumnName) != null)
            {
                if (AppSetting.IS_CMF_CONGREGATION && GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName) != null)
                {
                    if (this.DataSource != null)
                    {
                        DataTable dtRpt = this.DataSource as DataTable;
                        e.Result = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.BUDGET_LEDGER.APPROVED_CURRENT_YRColumn.ColumnName + ")", "GROUP_ID NOT IN (12, 13, 14)").ToString());
                        e.Handled = true;
                    }
                }
            }
        }
        #endregion

        private void xrcellCMFNoteOpBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (grpFooterCMFBudgetNote.Visible)
            {
                e.Result = UtilityMember.NumberSet.ToNumber(totalOpeningBalance);
                e.Handled = true;
            }
        }

        private void xrcellCMFNoteCashOpBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (grpFooterCMFBudgetNote.Visible)
            {
                e.Result = UtilityMember.NumberSet.ToNumber(cashOpeningBalance);
                e.Handled = true;
            }
        }

        private void xrcellCMFNoteBankOpBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (grpFooterCMFBudgetNote.Visible)
            {
                e.Result = UtilityMember.NumberSet.ToNumber(bankOpeningBalance);
                e.Handled = true;
            }
        }

        private void xrcellCMFNoteFDOpBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (grpFooterCMFBudgetNote.Visible)
            {
                e.Result = UtilityMember.NumberSet.ToNumber(fdOpeningBalance);
                e.Handled = true;
            }
        }

        private void xrcellCMFNoteTotalBudgetIncome_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (grpFooterCMFBudgetNote.Visible)
            {
                e.Result = UtilityMember.NumberSet.ToNumber(totalBudgetedRawProposedIncome);
                e.Handled = true;
            }
        }

        private void xrcellCMFNoteTotalOrdinaryExpense_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (grpFooterCMFBudgetNote.Visible)
            {
                e.Result = UtilityMember.NumberSet.ToNumber(ordinaryExprense);
                e.Handled = true;
            }
        }

        private void xrcellCMFNoteTotalExtraOrdinaryExpense_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (grpFooterCMFBudgetNote.Visible)
            {
                e.Result = UtilityMember.NumberSet.ToNumber(extraordinaryExprense);
                e.Handled = true;
            }
        }

        private void xrcellCMFNoteAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (grpFooterCMFBudgetNote.Visible)
            {
                e.Result = UtilityMember.NumberSet.ToNumber(Math.Abs(amtDifference));
                e.Handled = true;
            }
        }


    }
}