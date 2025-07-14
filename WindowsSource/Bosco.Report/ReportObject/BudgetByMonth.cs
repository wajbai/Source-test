using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Schema;
using AcMEDSync.Model;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetByMonth : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        Int32 LedgerGroupId = 0;
        #endregion

        #region Constructor
        public BudgetByMonth()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        #region Show Reports
        public override void ShowReport()
        {
            FetchBudgetByMonth();
        }
        #endregion


        public void FetchBudgetByMonth()
        {
            this.SetLandscapeHeader = Detail.WidthF;
            this.SetLandscapeFooter = Detail.WidthF;
            this.SetLandscapeFooterDateWidth = Detail.WidthF;

            setHeaderTitleAlignment();

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Budget)
                || (ReportProperty.Current.ReportId == "RPT-163" && this.ReportProperties.Budget.Split(',').Length != 1) || (ReportProperty.Current.ReportId == "RPT-180" && this.ReportProperties.Budget.Split(',').Length != 2))
            {
                SetReportTitle();
                AssignBudgetDateRangeTitle();
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        BindBudgetMonth();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        AssignBudgetDateRangeTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    BindBudgetMonth();
                    base.ShowReport();
                }
            }
        }

        private void BindBudgetMonth()
        {
            SetReportTitle();
            AssignBudgetDateRangeTitle();
            //this.BudgetName = ReportProperty.Current.BudgetName;

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
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.MONTH1_BUDGET_IDColumn, month1budgetid);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.MONTH2_BUDGET_IDColumn, month2budgetid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetmonth);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    DataTable dtMonthlyBudget = resultArgs.DataSource.Table;
                    this.DataSource = dtMonthlyBudget;
                    this.DataMember = dtMonthlyBudget.TableName;
                }
            }
        }

        #endregion

        //private DataTable AttachCashBankDetails(DataTable dtBudgetMonthOpening, DataTable dtMonths)
        //{
        //    foreach (DataRow drMonths in dtMonths.Rows)
        //    {
        //        DateTime openingdatefrom = this.UtilityMember.DateSet.ToDate(drMonths["DATE_FROM"].ToString(), false);
        //        DateTime openingdateto = this.UtilityMember.DateSet.ToDate(drMonths["DATE_TO"].ToString(), false);
        //        string MonthName = drMonths["MONTH_NAME"].ToString();

        //        //1. Attach Cash Ledger
        //        ResultArgs result = this.GetBalanceDetail(true, openingdatefrom.ToShortDateString(), this.ReportProperties.Project, ((int)FixedLedgerGroup.Cash).ToString());
        //        if (result.Success)
        //        {
        //            DataTable dtBalanceDetails = result.DataSource.Table;

        //            AppendCashBankFDRow(dtBudgetMonthOpening, dtBalanceDetails, openingdatefrom.ToShortDateString(), openingdateto.ToShortDateString(), MonthName);
        //        }

        //        //2. Attach Bank Ledger
        //        result = this.GetBalanceDetail(true, openingdatefrom.ToShortDateString(), this.ReportProperties.Project, ((int)FixedLedgerGroup.BankAccounts).ToString());
        //        if (result.Success)
        //        {
        //            DataTable dtBalanceDetails = result.DataSource.Table;
        //            AppendCashBankFDRow(dtBudgetMonthOpening, dtBalanceDetails, openingdatefrom.ToShortDateString(), openingdateto.ToShortDateString(), MonthName);
        //        }

        //        //AppendCashBankFDEmptyRow(dtBudgetByMonth, openingdatefrom.ToShortDateString(), openingdateto.ToShortDateString(), MonthName);
        //    }
        //    return dtBudgetMonthOpening;
        //}

        ///// <summary>
        ///// Add detail cash/Bank/fd ledger details into budget ledgers
        ///// </summary>
        ///// <param name="dtIncomeBudget"></param>
        ///// <param name="dtCashBankDetail"></param>
        ///// <param name="isOpeningbalance"></param>
        //private void AppendCashBankFDRow(DataTable dtBudgetExpense, DataTable dtCashBankDetail, string datefrom, string dateto, string monthname)
        //{
        //    foreach (DataRow dr in dtCashBankDetail.Rows)
        //    {
        //        Int32 cashbankledgerid = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
        //        Int32 groupid = UtilityMember.NumberSet.ToInteger(dr["GROUP_ID"].ToString());
        //        DataRow drCashBank = dtBudgetExpense.NewRow();
        //        drCashBank["GROUP_ID"] = groupid;
        //        drCashBank["MONTH_NAME"] = monthname;
        //        drCashBank["DATE_FROM"] = datefrom;
        //        drCashBank["DATE_TO"] = dateto;
        //        drCashBank["NATURE_ID"] = (int)Natures.Assert;
        //        drCashBank["NATURE"] = "Asset";
        //        drCashBank["LEDGER_ID"] = cashbankledgerid;
        //        drCashBank["ACCESS_FLAG"] = "2";
        //        drCashBank["LEDGER_CODE"] = (groupid == (Int32)FixedLedgerGroup.Cash ? " " : dr["LEDGER_CODE"].ToString().Trim());
        //        drCashBank["LEDGER_NAME"] = dr["LEDGER_NAME"].ToString().Trim();
        //        drCashBank["LEDGER_GROUP"] = dr["LEDGER_GROUP"].ToString().Trim();
        //        drCashBank["BUDGET_TRANS_MODE"] = string.Empty;
        //        double balanceamt = this.UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());
        //        if (dr["TRANS_MODE"].ToString().ToUpper() == "CR")
        //            drCashBank["PROPOSED_AMOUNT"] = -balanceamt;
        //        else
        //            drCashBank["PROPOSED_AMOUNT"] = balanceamt;

        //        dtBudgetExpense.DefaultView.Table.Rows.Add(drCashBank);
        //        dtBudgetExpense.DefaultView.Table.AcceptChanges();
        //    }
        //}

        //private double FetchOPBalance(string Date, int LedgerId)
        //{
        //    BalanceProperty balancePropery;
        //    using (BalanceSystem balanceSystem = new BalanceSystem())
        //    {
        //        balancePropery = balanceSystem.GetBalance(this.ReportProperties.Project, LedgerId, Date, BalanceSystem.BalanceType.ClosingBalance);
        //    }
        //    return balancePropery.Amount;
        //}
    }
}
