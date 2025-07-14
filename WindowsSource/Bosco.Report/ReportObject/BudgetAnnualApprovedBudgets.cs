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
    public partial class  BudgetAnnualApprovedBudgets : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = new ResultArgs();
        Double GrandIncomePreApprovedTotal = 0;
        Double GrandIncomeProposedTotal = 0;
        Double GrandIncomeApprovedTotal = 0;

        public BudgetAnnualApprovedBudgets()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            BindBudgetAnnualApprovedBudgets();
        }
        #endregion

        public void BindBudgetAnnualApprovedBudgets()
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
                
                this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate( this.AppSetting.YearFrom, false).ToShortDateString();
                this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString();

                if (this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false) > this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false))
                {
                    this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString();
                }
                ReportProperty.Current.ReportTitle = "Budget " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                SetReportTitle();
                AssignBudgetDateRangeTitle();

                this.SetLandscapeBudgetNameWidth = xrSubBudgetIncomeLedgers.WidthF-5;
                this.SetLandscapeHeader = xrSubBudgetIncomeLedgers.WidthF - 5;
                this.SetLandscapeFooter = xrSubBudgetIncomeLedgers.WidthF - 5;
                this.SetLandscapeFooterDateWidth = xrSubBudgetIncomeLedgers.WidthF - 5;
                SetTitleWidth(xrSubBudgetIncomeLedgers.WidthF-5);
                setHeaderTitleAlignment();

                this.ReportProperties.IncludeAllLedger = 0;
                //fix always sort by ledget name for cmf
                FixReportPropertyForCMF();
                
                //To get projectsids for selected repor date
                string annualbudgetsprojects = GetBudgetProjectsByDateProjects();
                this.ReportProperties.Project = annualbudgetsprojects;

                // To get Budget Names
                string annualbudgetnames = GetBudgetNamesByDateProjects();
                //ReportProperty.Current.BudgetName = string.Empty;
                this.BudgetName = ReportProperty.Current.BudgetName;
                //this.DisplayName = "Budget - " + annualbudgetnames;

                if (AppSetting.IS_CMF_CONGREGATION)
                {
                    //On 09/09/2020
                    //this.DisplayName += " - " + AppSetting.InstituteName;
                    ReportProperty.Current.BudgetName = string.Empty;
                    this.BudgetName = ReportProperty.Current.BudgetName;

                    string NewRpTitle = "Approved Budget";
                    string annualbudgetsprojectsNames = GetBudgetProjectsNamesByDateProjects();
                    /*ReportProperty.Current.ReportTitle = annualbudgetsprojectsNames + (string.IsNullOrEmpty(annualbudgetsprojectsNames)?"" : " - ") + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                    this.ReportTitle = ReportProperty.Current.ReportTitle;
                    this.ReportSubTitle = annualbudgetsprojectsNames;*/

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
                        this.DisplayName =  AppSetting.InstituteName +  " - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                    }
                    //else if (ReportProperty.Current.AllProjectsCount > 1)
                    //{
                    //    if (ReportProperty.Current.ReportTitle.Length > 200)
                    //    {
                    //        string[] projectids = ReportProperty.Current.Project.Split(',');
                    //        string firstproject = "Consolidated " + NewRpTitle;
                    //        if (projectids.Length > 0)
                    //        {
                    //            Int32 projectid = UtilityMember.NumberSet.ToInteger(projectids.GetValue(0).ToString());
                    //            firstproject = this.GetProjectName(projectid);
                    //        }
                    //        this.DisplayName = firstproject + "..etc - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                    //    }
                    //}
                    else
                    {
                        this.DisplayName = annualbudgetsprojectsNames +  " - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString(); ;
                    }

                    //replace special characters which are not valid for file names
                    this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
                }
                bool BudgetApproved = false;
                resultArgs = GetReportSource(TransMode.CR);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtBudgetDetails = resultArgs.DataSource.Table;
                    BudgetApproved = (dtBudgetDetails.Rows.Count > 0);
                    string filter = string.Empty;

                    filter = this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName + "='" + TransMode.CR.ToString() + "'" +
                                        " OR (" + this.reportSetting1.BUDGETVARIANCE.NATURE_IDColumn.ColumnName + " IN (1, 3, 4) AND TRANS_MODE IS NULL)";
                    dtBudgetDetails.DefaultView.RowFilter = filter;
                    DataTable dtQuaterlyLedges = dtBudgetDetails.DefaultView.ToTable();
                    //On 30/01/2025 filter cost centre if cc budget enabled and cc is selected --------------------------------------------------------
                    if (this.AppSetting.EnableCostCentreBudget == 1 && !string.IsNullOrEmpty(this.ReportProperties.CostCentre))
                    {
                        filter = this.ReportParameters.COST_CENTRE_IDsColumn.ColumnName + "<>''";
                        dtQuaterlyLedges.DefaultView.RowFilter = filter;
                        dtQuaterlyLedges = dtQuaterlyLedges.DefaultView.ToTable();
                    }
                    //---------------------------------------------------------------------------------------------------------------------------------
                    BudgetAnnualApprovedLedgers xrBudgetAnnualQtlyIncomeLedgers = xrSubBudgetIncomeLedgers.ReportSource as BudgetAnnualApprovedLedgers;
                    xrBudgetAnnualQtlyIncomeLedgers.BindBudgetDetails(dtQuaterlyLedges, TransMode.CR);
                                       
                    resultArgs = GetReportSource(TransMode.DR);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        dtBudgetDetails = resultArgs.DataSource.Table;
                        if (!BudgetApproved)
                        {
                            BudgetApproved = (dtBudgetDetails.Rows.Count > 0);
                        }
                        filter = this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName + "='" + TransMode.DR.ToString() + "'" +
                                            " OR (" + this.reportSetting1.BUDGETVARIANCE.NATURE_IDColumn.ColumnName + " IN (2, 3, 4) AND TRANS_MODE IS NULL)";
                        dtBudgetDetails.DefaultView.RowFilter = filter;
                        dtQuaterlyLedges = dtBudgetDetails.DefaultView.ToTable();
                        //On 30/01/2025 filter cost centre if cc budget enabled and cc is selected --------------------------------------------------------
                        if (this.AppSetting.EnableCostCentreBudget == 1 && !string.IsNullOrEmpty(this.ReportProperties.CostCentre))
                        {
                            filter = this.ReportParameters.COST_CENTRE_IDsColumn.ColumnName + "<>''";
                            dtQuaterlyLedges.DefaultView.RowFilter = filter;
                            dtQuaterlyLedges = dtQuaterlyLedges.DefaultView.ToTable();
                        }
                        //---------------------------------------------------------------------------------------------------------------------------------
                        BudgetAnnualApprovedLedgers xrBudgetAnnualQtlyExpenseLedgers = xrSubBudgetExpenseLedgers.ReportSource as BudgetAnnualApprovedLedgers;
                        xrBudgetAnnualQtlyExpenseLedgers.GrandIncomePreApprovedTotal = xrBudgetAnnualQtlyIncomeLedgers.GrandIncomePreApprovedTotal;
                        xrBudgetAnnualQtlyExpenseLedgers.GrandIncomeProposedTotal = xrBudgetAnnualQtlyIncomeLedgers.GrandIncomeProposedTotal;
                        xrBudgetAnnualQtlyExpenseLedgers.GrandIncomeApprovedTotal = xrBudgetAnnualQtlyIncomeLedgers.GrandIncomeApprovedTotal;
                        xrBudgetAnnualQtlyExpenseLedgers.BindBudgetDetails(dtQuaterlyLedges, TransMode.DR);
                        xrBudgetAnnualQtlyExpenseLedgers.HeaderLoaded = false;
                    }
                }

                ReportFooter.Visible = false;
                if (ReportProperty.Current.IncludeSignDetails == 1)
                {
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowApprovedSign = false;
                    ReportFooter.Visible = true;
                    if (AppSetting.HeadofficeCode.ToUpper() == "CMFNED" || AppSetting.HeadofficeCode.ToUpper() == "BSGESP") //&& budgetAction == (Int32)BudgetAction.Approved
                    { //On 28/02/2024, To set sign details for montfort pune
                        (xrSubSignFooter.ReportSource as SignReportFooter).ShowApprovedSign = BudgetApproved;
                    }

                    (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrSubBudgetIncomeLedgers.WidthF - 10;
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
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

            string budgetvariance = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetAnnualApproved);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.AppSetting.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.AppSetting.YearTo);
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

                //On 30/01/2025 filter cost centre if cc budget enabled and cc is selected --------------------------------------------------------
                if (this.AppSetting.EnableCostCentreBudget == 1 && !string.IsNullOrEmpty(this.ReportProperties.CostCentre))
                {
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre);
                }
                //---------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                Result = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetvariance);
            }

            return Result;
        }
    }
}
