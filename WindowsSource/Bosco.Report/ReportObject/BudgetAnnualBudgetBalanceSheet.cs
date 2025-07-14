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
    public partial class  BudgetAnnualBudgetBalanceSheet : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = new ResultArgs();

        public BudgetAnnualBudgetBalanceSheet()
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
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) ||
               string.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project.Split(',').Length == 0 ||
               this.ReportProperties.Project == "0")
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
                ReportProperty.Current.ReportTitle = "Budget Balance Sheet- " + this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false).Year.ToString();
                SetReportTitle();
                AssignBudgetDateRangeTitle();
                this.SetLandscapeBudgetNameWidth = xrSubBudgetIncomeLedgers.WidthF - 5;
                this.SetLandscapeHeader = xrSubBudgetIncomeLedgers.WidthF - 5;
                this.SetLandscapeFooter = xrSubBudgetIncomeLedgers.WidthF - 5;
                this.SetLandscapeFooterDateWidth = xrSubBudgetIncomeLedgers.WidthF - 5;
                SetTitleWidth(xrSubBudgetIncomeLedgers.WidthF - 5);
                setHeaderTitleAlignment();
                //fix always sort by ledget name for cmf
                this.ReportProperties.IncludeAllLedger = 0;
                if (this.AppSetting.IS_CMF_CONGREGATION)
                {
                    FixReportPropertyForCMF();

                    this.HideReportDate = true;
                }

                ReportFooter.Visible = false;
                xrSubSignFooter.Visible = false;
                if (ReportProperty.Current.IncludeSignDetails == 1)
                {
                    xrSubSignFooter.Visible = true;
                    ReportFooter.Visible = true;
                    (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrSubBudgetIncomeLedgers.WidthF - 10;
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
                }

                if (!(ReportProperty.Current.DateSet.ToDate(this.ReportProperties.DateFrom, false) >= ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearFrom, false) &&
                   ReportProperty.Current.DateSet.ToDate(this.ReportProperties.DateFrom, false) <= ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearTo, false)))
                {
                    this.ReportProperties.DateFrom = ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearFrom);
                }

                if (!(ReportProperty.Current.DateSet.ToDate(this.ReportProperties.DateTo, false) >= ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearFrom, false) &&
                        ReportProperty.Current.DateSet.ToDate(this.ReportProperties.DateTo, false) <= ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearTo, false)))
                {
                    this.ReportProperties.DateTo = ReportProperty.Current.DateSet.ToDate(this.AppSetting.YearTo);
                }

                //To get projectsids for selected repor date
                string annualbudgetsprojects = GetBudgetProjectsByDateProjects();
                this.ReportProperties.Project = annualbudgetsprojects;

                // To get Budget Names
                string annualbudgetnames = GetBudgetNamesByDateProjects();
                //ReportProperty.Current.BudgetName = string.Empty;
                this.BudgetName = ReportProperty.Current.BudgetName;
                //this.DisplayName = "Budget Balance Sheet - " + annualbudgetnames;

                if (AppSetting.IS_CMF_CONGREGATION)
                {
                    //On 09/09/2020
                    //this.DisplayName += " - " + AppSetting.InstituteName;
                    ReportProperty.Current.BudgetName = string.Empty;
                    this.BudgetName = ReportProperty.Current.BudgetName;

                    string NewRpTitle = "Budget Balance Sheet";
                    string annualbudgetsprojectsNames = GetBudgetProjectsNamesByDateProjects();
                    /*string newtitle =  annualbudgetsprojectsNames + (string.IsNullOrEmpty(annualbudgetsprojectsNames) ? "" :  " - ") + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                    ReportProperty.Current.ReportTitle = newtitle;
                    this.ReportTitle = ReportProperty.Current.ReportTitle;
                    this.ReportSubTitle = annualbudgetsprojectsNames;
                    this.DisplayName = ReportProperty.Current.ReportTitle;*/
                    
                    //On 23/09/2020
                    if (ReportProperty.Current.SelectedProjectCount > 1)
                    {
                        NewRpTitle = "Consolidated " + NewRpTitle;
                    }
                    this.ReportTitle = NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                    this.ReportSubTitle = annualbudgetsprojectsNames;

                    //On 12/09/2020, to fix export file name
                    //if (ReportProperty.Current.SelectedProjectCount == ReportProperty.Current.AllProjectsCount)
                    if (ReportProperty.Current.SelectedProjectCount > 1)
                    {
                        this.DisplayName = AppSetting.InstituteName + " - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false).Year.ToString();
                    }
                    else
                    {
                        this.DisplayName = annualbudgetsprojectsNames + " - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false).Year.ToString(); ;
                    }
                    //replace special characters which are not valid for file names
                    this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
                }

                resultArgs = GetReportSource(TransMode.CR);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtBudgetDetails = resultArgs.DataSource.Table;
                    string filter = string.Empty;

                    filter = this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName + "='" + TransMode.CR.ToString() + "'" +
                                        " OR (" + this.reportSetting1.BUDGETVARIANCE.NATURE_IDColumn.ColumnName + " IN (1, 3, 4) AND TRANS_MODE IS NULL)";
                    dtBudgetDetails.DefaultView.RowFilter = filter;
                    DataTable dtBudgetLedgers = dtBudgetDetails.DefaultView.ToTable();
                    BudgetAnnualBudgetBalanceSheetLedgers xrBudgetAnnualIncomeLedgers = xrSubBudgetIncomeLedgers.ReportSource as BudgetAnnualBudgetBalanceSheetLedgers;
                    xrBudgetAnnualIncomeLedgers.BindBudgetDetails(dtBudgetLedgers, TransMode.CR);

                    resultArgs = GetReportSource(TransMode.DR);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        dtBudgetDetails = resultArgs.DataSource.Table;

                        filter = this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName + "='" + TransMode.DR.ToString() + "'" +
                                            " OR (" + this.reportSetting1.BUDGETVARIANCE.NATURE_IDColumn.ColumnName + " IN (2, 3, 4) AND TRANS_MODE IS NULL)";
                        dtBudgetDetails.DefaultView.RowFilter = filter;
                        dtBudgetLedgers = dtBudgetDetails.DefaultView.ToTable();
                        BudgetAnnualBudgetBalanceSheetLedgers xrBudgetAnnualExpenseLedgers = xrSubBudgetExpenseLedgers.ReportSource as BudgetAnnualBudgetBalanceSheetLedgers;
                        xrBudgetAnnualExpenseLedgers.GrandIncomeApprovedTotal = xrBudgetAnnualIncomeLedgers.GrandIncomeApprovedTotal;
                        xrBudgetAnnualExpenseLedgers.GrandIncomeDueTotal = xrBudgetAnnualIncomeLedgers.GrandIncomeDueTotal;
                        xrBudgetAnnualExpenseLedgers.GrandIncomeDifferenceTotal = xrBudgetAnnualIncomeLedgers.GrandIncomeDifferenceTotal;
                        xrBudgetAnnualExpenseLedgers.BindBudgetDetails(dtBudgetLedgers, TransMode.DR);
                        xrBudgetAnnualExpenseLedgers.HeaderLoaded = false;
                        grpHeaderExpenseLedgers.Visible = (dtBudgetDetails.Rows.Count > 0);
                    }
                    else if (!resultArgs.Success)
                    {
                        MessageRender.ShowMessage(resultArgs.Message, false);
                    }
                }
                else if (!resultArgs.Success)
                {
                    MessageRender.ShowMessage(resultArgs.Message, false);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }
   
        private ResultArgs GetReportSource(TransMode BudgetNature)
        {
            ResultArgs Result = new ResultArgs();
            string budgetvariance = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetAnnualBudgetBalanceSheet);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.AppSetting.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.AppSetting.YearTo);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, BudgetNature);

                //On 31/07/2020, to fix ledger balacen
                /*dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, this.AppSetting.ShowBudgetLedgerSeparateReceiptPaymentActualBalance);

                if (this.AppSetting.ShowBudgetLedgerActualBalance == "1")
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, "JN");
                }*/

                //29/07/2021, to show ledger actual balance based on settings
                if (this.AppSetting.ShowBudgetLedgerActualBalance == "1") //Ledger Closing Balance without Journal Voucher
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, 0);
                }
                else if (this.AppSetting.ShowBudgetLedgerActualBalance == "2") // Receipts Vocuehr & Payments Voucher separately
                {
                    //dataManager.Parameters.Add(reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, VoucherSubTypes.JN.ToString());
                    dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, 1);
                }
                else //Ledger Closing Balance with Journal Voucher
                {
                    dataManager.Parameters.Add(reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, 0);
                }

                //On 10/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                decimal CurrencyAvgExchangeRate = 1;
                //decimal CurrencyPreviousYearAvgExchangeRate = 1;
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    CurrencyAvgExchangeRate = GetAvgCurrencyExchangeRateForFY(this.ReportProperties.CurrencyCountryId);
                    //CurrencyPreviousYearAvgExchangeRate = GetAvgCurrencyExchangeRateForPreviousFY(this.ReportProperties.CurrencyCountryId);
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                dataManager.Parameters.Add(reportSetting1.Country.EXCHANGE_RATEColumn, CurrencyAvgExchangeRate);
                //dataManager.Parameters.Add(reportSetting1.Country.PREVIOUS_YEAR_EX_RATEColumn, CurrencyPreviousYearAvgExchangeRate);
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                Result = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetvariance);
            }
            return Result;
        }

    }
}
