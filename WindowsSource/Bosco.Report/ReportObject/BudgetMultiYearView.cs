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
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetMultiYearView : Bosco.Report.Base.ReportHeaderBase
    {
        DataTable dtYealyOPBalane = new DataTable();
        DataTable dtYealyCLBalane = new DataTable();
        DataTable dtUserDefinedBudgetActual = new DataTable();
        DataTable dtUserDefinedBudgetBalances = new DataTable();
        bool records = false;

        public BudgetMultiYearView()
        {
            InitializeComponent();
        }

        #region Show Reports
        public override void ShowReport()
        {
            //SetReportTitle();
            //ShowBudgetView();
            //base.ShowReport();

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                string.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project.Split(',').Length == 0 ||
                this.ReportProperties.Project == "0")
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
                        ShowBudgetView();
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
                    ShowBudgetView();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }
        #endregion

        #region Methods
        public void ShowBudgetView()
        {
            SetTitleWidth(xrtblLedger.WidthF);
            this.SetLandscapeBudgetNameWidth = xrtblLedger.WidthF - 5;
            this.SetLandscapeHeader = xrtblLedger.WidthF - 5;
            this.SetLandscapeFooter = xrtblLedger.WidthF - 5;
            this.SetLandscapeFooterDateWidth = xrtblLedger.WidthF - 5;
            SetTitleWidth(xrtblLedger.WidthF - 5);
            setHeaderTitleAlignment();
            BindBudgetView();
            AlignContentTable(xrtblLedger);
            AlignHeaderTable(xrtblHeaderCaption);
            AlignContentTable(xrtblGrpHeadgerTransmode);
            AlignContentTable(xrtblGrpLedgerGroup);
            //AlignContentTable(xrTblOPGrpHeader);
            //AlignContentTable(xrTblCLGrpHeader);
        }

        private void BindBudgetView()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {

                this.Landscape = false;
                this.Margins.Left = 15;
                if (this.ReportProperties.NoOfYears > 3)
                {
                    this.Landscape = true;
                    this.Margins.Left = 28;
                }

                this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
                this.SetTitleWidth(this.PageWidth - 15);
                setHeaderTitleAlignment();
                SetReportTitle();
                //this.HidePageFooter = false;
                HideYearColumns();


                //1. Get Budget Ledger details for current FY year with previous 3 years
                string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetAnnualYearComparision);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.reportSetting1.MultiAbstract.NO_OF_YEARColumn, this.ReportProperties.NoOfYears);

                    //if (this.AppSetting.ShowBudgetLedgerActualBalance == "1")
                    //{
                    //    dataManager.Parameters.Add(this.reportSetting1.BUDGET_LEDGER.VOUCHER_TYPEColumn, "JN");
                    //}

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
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);
                }


                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtBudgetAllYearLedgers = resultArgs.DataSource.Table;
                    //Get User defined budtet actual amount
                    AttachUserDefinedBudgetActuals(dtBudgetAllYearLedgers);

                    //Get affected Years
                    DataTable dtYears = dtBudgetAllYearLedgers.DefaultView.ToTable(true, new string[] { this.ReportParameters.YEAR_FROMColumn.ColumnName, this.ReportParameters.YEAR_TOColumn.ColumnName });
                    dtYears.DefaultView.Sort = this.ReportParameters.YEAR_FROMColumn.ColumnName + " DESC";
                    dtYears = dtYears.DefaultView.ToTable();

                    //Prepare report data source with report columns
                    DataTable dtReportSource = new DataTable(); //dtBudgetAllYearLedgers.DefaultView.ToTable(true, new string[] { "LEDGER_GROUP_ID", "LEDGER_ID" });
                    dtReportSource.Columns.Add(new DataColumn(this.ReportParameters.LEDGER_IDColumn.ColumnName, typeof(Int32)));
                    dtReportSource.Columns.Add(new DataColumn(reportSetting1.Ledger.LEDGER_CODEColumn.ColumnName, typeof(String)));
                    dtReportSource.Columns.Add(new DataColumn(reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName, typeof(String)));
                    dtReportSource.Columns.Add(new DataColumn(reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName, typeof(Int32)));
                    dtReportSource.Columns.Add(new DataColumn(reportSetting1.BUDGETVARIANCE.LEDGER_GROUPColumn.ColumnName, typeof(string)));
                    dtReportSource.Columns.Add(new DataColumn(this.ReportParameters.TRANS_MODEColumn.ColumnName, typeof(string)));
                    dtReportSource.Columns.Add(new DataColumn(this.ReportParameters.VOUCHER_TYPEColumn.ColumnName, typeof(string)));

                    dtYealyOPBalane.Rows.Clear();
                    dtYealyCLBalane.Rows.Clear();
                    dtBudgetAllYearLedgers.DefaultView.RowFilter = reportSetting1.BUDGET_LEDGER.LEDGER_IDColumn.ColumnName + " > 0";
                    records = dtBudgetAllYearLedgers.DefaultView.Count > 0;
                    dtBudgetAllYearLedgers.DefaultView.RowFilter = "";

                    foreach (DataRow drAcYear in dtYears.Rows)
                    {
                        int YearNo = (dtYears.Rows.IndexOf(drAcYear) + 1);
                        string YearActualColumn = "Y" + YearNo + "_ACTUAL_AMOUNT";
                        string YearBudgetedColumn = "Y" + YearNo + "_BUDGETED_AMOUNT";
                        string AcYearFrom = UtilityMember.DateSet.ToDate(drAcYear["Year_FROM"].ToString());
                        string AcYearTo = UtilityMember.DateSet.ToDate(drAcYear["Year_TO"].ToString());
                        SetColumnCaptions(YearNo, AcYearFrom, AcYearTo);
                        
                        PrepareYearOpeningClosingBalance(YearActualColumn, YearBudgetedColumn, AcYearFrom, AcYearTo, BalanceSystem.BalanceType.OpeningBalance);
                        PrepareYearOpeningClosingBalance(YearActualColumn, YearBudgetedColumn, AcYearFrom, AcYearTo, BalanceSystem.BalanceType.ClosingBalance);
                        
                        dtReportSource.Columns.Add(new DataColumn(YearActualColumn, typeof(double)));
                        dtReportSource.Columns[YearActualColumn].DefaultValue = 0;
                        dtReportSource.Columns.Add(new DataColumn(YearBudgetedColumn, typeof(double)));
                        dtReportSource.Columns[YearBudgetedColumn].DefaultValue = 0;

                        dtBudgetAllYearLedgers.DefaultView.RowFilter = "YEAR_FROM='" + AcYearFrom + "' AND YEAR_TO = '" + AcYearTo + "'";
                        DataTable dtYearBudget = dtBudgetAllYearLedgers.DefaultView.ToTable();
                        foreach (DataRow drYearLedgers in dtYearBudget.Rows)
                        {
                            Int32 natureid = UtilityMember.NumberSet.ToInteger(drYearLedgers["NATURE_ID"].ToString());
                            Int32 ledgerid = UtilityMember.NumberSet.ToInteger(drYearLedgers["LEDGER_ID"].ToString());
                            string transmode = drYearLedgers[this.ReportParameters.TRANS_MODEColumn.ColumnName].ToString();
                            string vouchertype = drYearLedgers[this.ReportParameters.VOUCHER_TYPEColumn.ColumnName].ToString();
                            string ledgername = drYearLedgers["LEDGER_NAME"].ToString();
                            string ledgercode = drYearLedgers["LEDGER_CODE"].ToString();
                            Int32 ledgergroupid = UtilityMember.NumberSet.ToInteger(drYearLedgers["GROUP_ID"].ToString());
                            string ledgergroup = drYearLedgers["LEDGER_GROUP"].ToString();
                            double budgetdamt = UtilityMember.NumberSet.ToDouble(drYearLedgers["APPROVED_AMOUNT"].ToString());

                            //double amtcr = UtilityMember.NumberSet.ToDouble(drYearLedgers["AMOUNT_CR"].ToString());
                            //double amtdr = UtilityMember.NumberSet.ToDouble(drYearLedgers["AMOUNT_DR"].ToString());
                            double actualamt = UtilityMember.NumberSet.ToDouble(drYearLedgers["ACTUAL_AMOUNT"].ToString()); ;
                            //double actualamtUserDefined = 0; //GetUserDefinedBudtetActualAmount(AcYearFrom, AcYearTo, ledgerid, transmode, "TR");
                            /*if (natureid == 1 || natureid == 1)
                            {
                                actualamt = (natureid == 1 ? (amtcr - amtdr) : (amtdr - amtcr));
                            }
                            else
                            {
                                actualamt = (transmode == TransMode.CR.ToString() ? amtcr : amtdr);
                            }*/

                            if (!string.IsNullOrEmpty(ledgername))
                            {
                                dtReportSource.DefaultView.RowFilter = "LEDGER_ID = " + ledgerid + " AND TRANS_MODE='" + transmode + "'";
                                if (dtReportSource.DefaultView.Count == 0)
                                {
                                    DataRowView newrow = dtReportSource.DefaultView.AddNew();
                                    newrow["LEDGER_ID"] = ledgerid;
                                    newrow["GROUP_ID"] = ledgergroupid;
                                    newrow["LEDGER_CODE"] = ledgercode;
                                    newrow["LEDGER_NAME"] = ledgername;
                                    newrow["LEDGER_GROUP"] = ledgergroup;
                                    newrow["TRANS_MODE"] = transmode;
                                    newrow["VOUCHER_TYPE"] = vouchertype;
                                    newrow[YearActualColumn] = actualamt;
                                    newrow[YearBudgetedColumn] = budgetdamt;
                                    newrow.EndEdit();
                                }
                                else if (dtReportSource.DefaultView.Count == 1)
                                {
                                    dtReportSource.DefaultView[0].BeginEdit();
                                    dtReportSource.DefaultView[0][YearActualColumn] = actualamt;
                                    dtReportSource.DefaultView[0][YearBudgetedColumn] = budgetdamt;
                                    dtReportSource.DefaultView[0].EndEdit();
                                }
                            }
                            dtReportSource.DefaultView.RowFilter = string.Empty;
                        }
                        dtBudgetAllYearLedgers.DefaultView.RowFilter = string.Empty;
                        dtReportSource.DefaultView.RowFilter = string.Empty;
                    }
                    //AssignBalances(BalanceSystem.BalanceType.OpeningBalance);
                    //AssignBalances(BalanceSystem.BalanceType.ClosingBalance);

                    BudgetMultiYear SubrptOpeningBalance = xrSubOpeningBalance.ReportSource as BudgetMultiYear;
                    SubrptOpeningBalance.BindBudgetMultiYear(dtYealyOPBalane, xrtblLedger);

                    BudgetMultiYear SubrptClosingBalance = xrSubClosingBalance.ReportSource as BudgetMultiYear;
                    SubrptClosingBalance.BindBudgetMultiYear(dtYealyCLBalane, xrtblLedger);

                    dtReportSource.TableName = this.DataMember;
                    this.DataSource = dtReportSource;
                    this.DataMember = dtReportSource.TableName;
                    Detail.Visible = (dtReportSource.Rows.Count > 0);

                    grpHeaderLedgerGroup.Visible = grpFooterVoucherType.Visible = grpFooterLedgerGroup.Visible = grpCLFooter.Visible = Detail.Visible;

                    this.HideDateRange = false;
                    //this.HideReportSubTitle = true;

                    grpHeaderLedgerGroup.SortingSummary.Enabled = true;
                    grpHeaderLedgerGroup.SortingSummary.FieldName = "LEDGER_CODE";
                    grpHeaderLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpHeaderLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;

                    Detail.SortFields.Add(new GroupField(this.reportSetting1.Ledger.LEDGER_CODEColumn.ColumnName));
                    Detail.SortFields.Add(new GroupField(this.reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName));

                    string period = string.Empty;
                    string periodfrom = string.Empty;
                    string periodto = string.Empty;
                    if (dtYears.Rows.Count > 0)
                    {
                        periodfrom = UtilityMember.DateSet.ToDate(dtYears.Rows[dtYears.Rows.Count - 1]["YEAR_FROM"].ToString(), false).Year.ToString(); ;
                        periodto = UtilityMember.DateSet.ToDate(dtYears.Rows[0]["YEAR_TO"].ToString(), false).Year.ToString();
                        period = periodfrom + " - " + periodto;
                        period = "For the Period: " + period;
                    }
                    if (!string.IsNullOrEmpty(period))
                    {
                        this.ReportPeriod = period;
                    }
                    if (this.AppSetting.IS_CMF_CONGREGATION)
                    {
                        xrCapY1Actual.Text = xrCapY2Actual.Text = xrCapY3Actual.Text = xrCapY4Actual.Text = xrCapY5Actual.Text = xrCapY6Actual.Text = "Realized";
                        FixReportPropertyForCMF();
                        Detail.SortFields.Add(new GroupField("LEDGER_NAME"));

                        string NewRpTitle = "Budget - Year Comparison";
                        string annualbudgetsprojectsNames = GetBudgetProjectsNamesByDateProjects();
                        /*ReportProperty.Current.ReportTitle = annualbudgetsprojectsNames + (string.IsNullOrEmpty(annualbudgetsprojectsNames)? "": " - ") + NewRpTitle;
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
                            this.DisplayName = AppSetting.InstituteName + " - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
                        }
                        else
                        {
                            this.DisplayName = annualbudgetsprojectsNames + " - " + NewRpTitle + " - " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString(); ;
                        }

                        //replace special characters which are not valid for file names
                        this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
                    }
                }
                else
                {
                    MessageRender.ShowMessage("Invalid Budget");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
        }


        /// <summary>
        /// This method is used to set columns Captions
        /// </summary>
        private void SetColumnCaptions(int year, string datefrom, string dateto)
        {
            string caption = UtilityMember.DateSet.ToDate(datefrom, false).Year.ToString() + "-" + UtilityMember.DateSet.ToDate(dateto, false).ToString("yy");
            switch (year)
            {
                case 1:
                    xrCapY1BudgetedAmount.Text = xrCapY1ActualAmount.Text = caption;
                    break;
                case 2:
                    xrCapY2BudgetedAmount.Text = xrCapY2ActualAmount.Text = caption;
                    break;
                case 3:
                    xrCapY3ActualAmount.Text = caption;
                    break;
                case 4:
                    xrCapY4ActualAmount.Text = caption;
                    break;
                case 5:
                    xrCapY5ActualAmount.Text = caption;
                    break;
                case 6:
                    xrCapY6ActualAmount.Text = caption;
                    break;
            }
        }

        /// <summary>
        /// Prepare opening and closing balance for each year
        /// </summary>
        private void PrepareYearOpeningClosingBalance(string YearActualColumn, string YearBudgetedColumn, string datefrom, string dateto, BalanceSystem.BalanceType balanceType)
        {
            bool enforceCurrency = (settingProperty.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0 ? true : false);

            //For Opening and Closing Balance
            if (balanceType == BalanceSystem.BalanceType.OpeningBalance)
            {
                if (!dtYealyOPBalane.Columns.Contains("LEDGER_GROUP")) dtYealyOPBalane.Columns.Add(new DataColumn("LEDGER_GROUP", typeof(string)));
                if (!dtYealyOPBalane.Columns.Contains("LEDGER_NAME")) dtYealyOPBalane.Columns.Add(new DataColumn("LEDGER_NAME", typeof(string)));
                if (!dtYealyOPBalane.Columns.Contains(YearActualColumn)) dtYealyOPBalane.Columns.Add(new DataColumn(YearActualColumn, typeof(double)));
                if (!dtYealyOPBalane.Columns.Contains(YearBudgetedColumn)) dtYealyOPBalane.Columns.Add(new DataColumn(YearBudgetedColumn, typeof(double)));

                if (records)
                {
                    double dOpeningAmount = 0;
                    if (this.AppSetting.HeadofficeCode.ToUpper().Equals("BSGNEI") && (this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2017 || this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2018))
                    {
                        dOpeningAmount = this.GetFixedLedgerGroupBalancesLedgers(datefrom, dateto, BalanceSystem.BalanceType.OpeningBalance, FixedLedgerGroup.Cash);
                    }
                    else
                    {
                        dOpeningAmount = this.GetBalance(this.ReportProperties.Project, datefrom, BalanceSystem.LiquidBalanceGroup.CashBalance,
                            BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                    }
                    UpdateYearOpeningClosingBalance(BalanceSystem.BalanceType.OpeningBalance, FixedLedgerGroup.Cash, dtYealyOPBalane, YearActualColumn, YearBudgetedColumn, dOpeningAmount);

                    if (this.AppSetting.HeadofficeCode.ToUpper().Equals("BSGNEI") && (this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2017 || this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2018))
                    {
                        dOpeningAmount = this.GetFixedLedgerGroupBalancesLedgers(datefrom, dateto, BalanceSystem.BalanceType.OpeningBalance, FixedLedgerGroup.BankAccounts);
                    }
                    else
                    {
                        dOpeningAmount = this.GetBalance(this.ReportProperties.Project, datefrom, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.BankBalance,
                            AcMEDSync.Model.BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                    }
                    UpdateYearOpeningClosingBalance(BalanceSystem.BalanceType.OpeningBalance, FixedLedgerGroup.BankAccounts, dtYealyOPBalane, YearActualColumn, YearBudgetedColumn, dOpeningAmount);
                    if (this.AppSetting.HeadofficeCode.ToUpper().Equals("BSGNEI") && (this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2017 || this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2018))
                    {
                        dOpeningAmount = this.GetFixedLedgerGroupBalancesLedgers(datefrom, dateto, BalanceSystem.BalanceType.OpeningBalance, FixedLedgerGroup.FixedDeposit);
                    }
                    else
                    {
                        dOpeningAmount = this.GetBalance(this.ReportProperties.Project, datefrom, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.FDBalance,
                            AcMEDSync.Model.BalanceSystem.BalanceType.OpeningBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                    }
                    UpdateYearOpeningClosingBalance(BalanceSystem.BalanceType.OpeningBalance, FixedLedgerGroup.FixedDeposit, dtYealyOPBalane, YearActualColumn, YearBudgetedColumn, dOpeningAmount);
                }
            }
            else if (balanceType == BalanceSystem.BalanceType.ClosingBalance)
            {
                if (!dtYealyCLBalane.Columns.Contains("LEDGER_GROUP")) dtYealyCLBalane.Columns.Add(new DataColumn("LEDGER_GROUP", typeof(string)));
                if (!dtYealyCLBalane.Columns.Contains("LEDGER_NAME")) dtYealyCLBalane.Columns.Add(new DataColumn("LEDGER_NAME", typeof(string)));
                if (!dtYealyCLBalane.Columns.Contains(YearActualColumn)) dtYealyCLBalane.Columns.Add(new DataColumn(YearActualColumn, typeof(double)));
                if (!dtYealyCLBalane.Columns.Contains(YearBudgetedColumn)) dtYealyCLBalane.Columns.Add(new DataColumn(YearBudgetedColumn, typeof(double)));

                if (records)
                {
                    if (UtilityMember.DateSet.ToDate(dateto, false) >= UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false) || this.AppSetting.HeadofficeCode.ToUpper().Equals("BSGNEI"))
                    {
                        double dClosingAmount = 0;
                        if (this.AppSetting.HeadofficeCode.ToUpper().Equals("BSGNEI") && (this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2017 || this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2018))
                        {
                            dClosingAmount = this.GetFixedLedgerGroupBalancesLedgers(datefrom, dateto, BalanceSystem.BalanceType.ClosingBalance, FixedLedgerGroup.Cash);
                        }
                        else
                        {
                            dClosingAmount = dClosingAmount = this.GetBalance(this.ReportProperties.Project, dateto, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.CashBalance,
                                AcMEDSync.Model.BalanceSystem.BalanceType.ClosingBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                        }
                        UpdateYearOpeningClosingBalance(BalanceSystem.BalanceType.ClosingBalance, FixedLedgerGroup.Cash, dtYealyCLBalane, YearActualColumn, YearBudgetedColumn, dClosingAmount);

                        if (this.AppSetting.HeadofficeCode.ToUpper().Equals("BSGNEI") && (this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2017 || this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2018))
                        {
                            dClosingAmount = this.GetFixedLedgerGroupBalancesLedgers(datefrom, dateto, BalanceSystem.BalanceType.ClosingBalance, FixedLedgerGroup.BankAccounts);
                        }
                        else
                        {
                            dClosingAmount = this.GetBalance(this.ReportProperties.Project, dateto, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.BankBalance,
                                AcMEDSync.Model.BalanceSystem.BalanceType.ClosingBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                        }
                        UpdateYearOpeningClosingBalance(BalanceSystem.BalanceType.ClosingBalance, FixedLedgerGroup.BankAccounts, dtYealyCLBalane, YearActualColumn, YearBudgetedColumn, dClosingAmount);

                        if (this.AppSetting.HeadofficeCode.ToUpper().Equals("BSGNEI") && (this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2017 || this.UtilityMember.DateSet.ToDate(datefrom, false).Year == 2018))
                        {
                            dClosingAmount = this.GetFixedLedgerGroupBalancesLedgers(datefrom, dateto, BalanceSystem.BalanceType.ClosingBalance, FixedLedgerGroup.FixedDeposit);
                        }
                        else
                        {
                            dClosingAmount = this.GetBalance(this.ReportProperties.Project, dateto, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.FDBalance,
                                AcMEDSync.Model.BalanceSystem.BalanceType.ClosingBalance, string.Empty, enforceCurrency, this.ReportProperties.CurrencyCountryId);
                        }
                        UpdateYearOpeningClosingBalance(BalanceSystem.BalanceType.ClosingBalance, FixedLedgerGroup.FixedDeposit, dtYealyCLBalane, YearActualColumn, YearBudgetedColumn, dClosingAmount);
                    }
                }
            }
        }

        // to align header tables
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
                        //else if (count == 4)
                        //{
                        //    tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                        //    if (ReportProperties.ShowLedgerCode != 1)
                        //    {
                        //        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        //    }
                        //}
                        //else
                        tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom | BorderSide.Top;

                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;

                    //For FD register, FD history and GST return
                    if (ReportProperties.ReportId == "RPT-094" || ReportProperties.ReportId == "RPT-047" || ReportProperties.ReportId == "RPT-166" || ReportProperties.ReportId == "RPT-181")
                    {
                        tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ?
                                new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Bold) : new System.Drawing.Font(tcell.Font, System.Drawing.FontStyle.Regular));
                    }
                    else
                    {
                        tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ?
                                new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) :
                                new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
                    }

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
                        //if (count == 1)
                        //{
                        //    tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        //    if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        //    {
                        //        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        //    }
                        //}
                        //else
                        //{
                        tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        //}
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
        private double GetFixedLedgerGroupBalancesLedgers(string datefrom, string dateto, BalanceSystem.BalanceType balanceType, FixedLedgerGroup group)
        {
            Double rtn = 0;
            if (dtUserDefinedBudgetBalances.Rows.Count > 0)
            {
                string transmode = (balanceType == BalanceSystem.BalanceType.OpeningBalance ? "CR" : "DR");
                Int32 groupid = (int)group;

                //For Opening Balance
                dtUserDefinedBudgetBalances.DefaultView.RowFilter = "DATE_FROM='" + datefrom + "' AND DATE_TO='" + dateto + "' AND TRANS_MODE='" + transmode + "' AND GROUP_ID =" + groupid;
                if (dtUserDefinedBudgetBalances.DefaultView.Count == 1)
                {
                    rtn = UtilityMember.NumberSet.ToDouble(dtUserDefinedBudgetBalances.DefaultView[0]["ACTUAL_AMOUNT"].ToString());
                }
            }
            return rtn;
        }

        private void UpdateYearOpeningClosingBalance(BalanceSystem.BalanceType balanceType, FixedLedgerGroup group, DataTable dtBalance, string YearActualColumn, string YearBudgetedColumn, double Amount)
        {
            string balancecaption = group == FixedLedgerGroup.Cash ? "Cash in Hand" : group == FixedLedgerGroup.BankAccounts ? "Cash at Bank" : "FD/RD/Post Office Savings/ Flexi";
            dtBalance.DefaultView.RowFilter = "LEDGER_NAME='" + balancecaption + "'";
            if (dtBalance.DefaultView.Count == 0)
            {
                DataRowView newrow = dtBalance.DefaultView.AddNew();
                newrow["LEDGER_GROUP"] = (balanceType == BalanceSystem.BalanceType.OpeningBalance ? "Opening Balance" : "Closing Balance");
                newrow["LEDGER_NAME"] = balancecaption;// group.ToString();
                newrow[YearActualColumn] = Amount;
                newrow[YearBudgetedColumn] = Amount;
                newrow.EndEdit();
            }
            else if (dtBalance.DefaultView.Count == 1)
            {
                dtBalance.DefaultView[0].BeginEdit();
                dtBalance.DefaultView[0][YearActualColumn] = Amount;
                dtBalance.DefaultView[0][YearBudgetedColumn] = Amount;
                dtBalance.DefaultView[0].EndEdit();
            }
            dtBalance.DefaultView.RowFilter = string.Empty;
        }

        ///// <summary>
        ///// This method is used to set columns Captions
        ///// </summary>
        //private void AssignBalances(BalanceSystem.BalanceType balanceType)
        //{
        //    if (balanceType == BalanceSystem.BalanceType.OpeningBalance)
        //    {
        //        xrcellOpCashGrpY1Budgeted.Text = xrcellOpCashGrpY1Budgeted.Text = UtilityMember.NumberSet.ToNumber(GetYearOpeningClosingBalance(dtYealyOPBalane, 1, FixedLedgerGroup.Cash));
        //        xrcellOpCashGrpY2Budgeted.Text = xrcellOpCashGrpY2Budgeted.Text = UtilityMember.NumberSet.ToNumber(GetYearOpeningClosingBalance(dtYealyOPBalane, 2, FixedLedgerGroup.Cash));
        //        xrcellOpCashGrpY3Actual.Text = UtilityMember.NumberSet.ToNumber(GetYearOpeningClosingBalance(dtYealyOPBalane, 3, FixedLedgerGroup.Cash));
        //        xrcellOpCashGrpY4Actual.Text = UtilityMember.NumberSet.ToNumber(GetYearOpeningClosingBalance(dtYealyOPBalane, 4, FixedLedgerGroup.Cash));
        //    }
        //    else if (balanceType == BalanceSystem.BalanceType.ClosingBalance)
        //    {
        //        //xrcellCLCashGrpY1Budgeted.Text = xrcellCLCashGrpY1Budgeted.Text = UtilityMember.NumberSet.ToNumber(GetYearOpeningClosingBalance(dtYealyOPBalane, 1, FixedLedgerGroup.Cash));
        //        //xrcellCLCashGrpY2Budgeted.Text = xrcellCLCashGrpY2Budgeted.Text = UtilityMember.NumberSet.ToNumber(GetYearOpeningClosingBalance(dtYealyOPBalane, 2, FixedLedgerGroup.Cash));
        //        //xrcellCLCashGrpY3Actual.Text = UtilityMember.NumberSet.ToNumber(GetYearOpeningClosingBalance(dtYealyOPBalane, 3, FixedLedgerGroup.Cash));
        //        //xrcellCLCashGrpY4Actual.Text = UtilityMember.NumberSet.ToNumber(GetYearOpeningClosingBalance(dtYealyOPBalane, 4, FixedLedgerGroup.Cash));
        //    }
        //}

        private double GetYearOpeningClosingBalance(DataTable dtBalance, int year, FixedLedgerGroup group)
        {
            double Amount = 0;
            string YearActualColumn = "Y" + year + "_ACTUAL_AMOUNT";
            dtBalance.DefaultView.RowFilter = "GROUP='" + group.ToString() + "'";
            if (dtBalance.DefaultView.Count == 1)
            {
                Amount = UtilityMember.NumberSet.ToDouble(dtBalance.DefaultView[0][YearActualColumn].ToString());
            }
            dtBalance.DefaultView.RowFilter = string.Empty;
            return Amount;
        }


        /// <summary>
        /// This method is used to get receipt total balacne for given column
        /// opening balance + sum of income ledgers 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private double GetReceiptTotal(string column, SummaryGetResultEventArgs e)
        {
            double rtn = 0;
            if (this.DataSource != null)
            {
                if (GetCurrentColumnValue("TRANS_MODE") != null)
                {
                    string transmode = GetCurrentColumnValue("TRANS_MODE").ToString();
                    if (transmode == TransMode.CR.ToString())
                    {
                        DataTable dtRptSource = this.DataSource as DataTable;
                        if (dtRptSource.Columns.Contains(column))
                        {
                            double dTotalExpenseLedgers = UtilityMember.NumberSet.ToDouble(dtRptSource.Compute("SUM(" + column + ")", "TRANS_MODE='" + TransMode.CR + "'").ToString());
                            double dTotalOpening = UtilityMember.NumberSet.ToDouble(dtYealyOPBalane.Compute("SUM(" + column + ")", string.Empty).ToString());
                            rtn = dTotalExpenseLedgers + dTotalOpening;
                        }
                    }
                    else if (transmode == TransMode.DR.ToString())
                    {
                        for (int i = 0; i <= e.CalculatedValues.Count - 1; i++)
                        {
                            rtn += UtilityMember.NumberSet.ToDouble(e.CalculatedValues[i].ToString());
                        }
                    }
                }
            }
            return rtn;
        }

        /// <summary>
        /// This method is used to get payment total balacne for given column
        /// Closing balance + sum of expense ledgers 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private double GetPaymentTotal(string column)
        {
            double rtn = 0;
            if (this.DataSource != null)
            {
                DataTable dtRptSource = this.DataSource as DataTable;
                if (dtRptSource.Columns.Contains(column))
                {
                    double dTotalExpenseLedgers = UtilityMember.NumberSet.ToDouble(dtRptSource.Compute("SUM(" + column + ")", "TRANS_MODE='" + TransMode.DR + "'").ToString());
                    double dTotalClosing = UtilityMember.NumberSet.ToDouble(dtYealyCLBalane.Compute("SUM(" + column + ")", string.Empty).ToString());
                    rtn = dTotalExpenseLedgers + dTotalClosing;
                }
            }
            return rtn;
        }

        /// <summary>
        /// This method is used to get grand total balacne for given column
        /// Closing balance + sum of expense ledgers 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private double GetGrandBalance(string column)
        {
            double rtn = 0;
            if (this.DataSource != null)
            {
                DataTable dtRpt = this.DataSource as DataTable;
                if (dtRpt.Columns.Contains(column))
                {
                    double dTotalIncomeLedgers = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + column + ")", "TRANS_MODE='" + TransMode.CR + "'").ToString());
                    double dTotalOpening = UtilityMember.NumberSet.ToDouble(dtYealyOPBalane.Compute("SUM(" + column + ")", string.Empty).ToString());
                    dTotalIncomeLedgers += dTotalOpening;

                    double dTotalExpenseLedgers = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + column + ")", "TRANS_MODE='" + TransMode.DR + "'").ToString());
                    //double dTotalClosing = UtilityMember.NumberSet.ToDouble(dtYealyCLBalane.Compute("SUM(" + column + ")", string.Empty).ToString());
                    //dTotalExpenseLedgers += dTotalClosing;
                    rtn = dTotalIncomeLedgers - dTotalExpenseLedgers;
                }
            }
            return rtn;
        }

        /// <summary>
        /// This method is used to attach user defined budged actual amounts for 2017-18 and 2018-19 alone
        /// </summary>
        private void AttachUserDefinedBudgetActuals(DataTable dtSource)
        {
            ResultArgs resultArgs = new ResultArgs();
            if (this.AppSetting.HeadofficeCode.ToUpper().Equals("BSGNEI"))
            {
                string Year1From = UtilityMember.DateSet.ToDate("01/04/2017", false).ToShortDateString();
                string Year1To = UtilityMember.DateSet.ToDate("31/03/2018", false).ToShortDateString();

                string Year2From = UtilityMember.DateSet.ToDate("01/04/2018", false).ToShortDateString();
                string Year2To = UtilityMember.DateSet.ToDate("31/03/2019", false).ToShortDateString();

                string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetAnnualYearUserDefined);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);
                }
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    dtUserDefinedBudgetActual = resultArgs.DataSource.Table;

                    //Check Year1 (2017-18)
                    dtSource.DefaultView.RowFilter = string.Empty;
                    dtSource.DefaultView.RowFilter = "YEAR_FROM= '" + Year1From + "' AND YEAR_TO='" + Year1To + "'";

                    dtUserDefinedBudgetActual.DefaultView.RowFilter = "DATE_FROM= '" + Year1From + "' AND DATE_TO='" + Year1To + "'";
                    DataTable dt1 = dtUserDefinedBudgetActual.DefaultView.ToTable();
                    if (dtSource.DefaultView.Count <= 1 && dt1.Rows.Count > 0)
                    {
                        dtSource.Merge(dt1);
                    }

                    //Check Year1 (2018-19)
                    dtSource.DefaultView.RowFilter = string.Empty;
                    dtSource.DefaultView.RowFilter = "YEAR_FROM= '" + Year2From + "' AND YEAR_TO='" + Year2To + "'";

                    dtUserDefinedBudgetActual.DefaultView.RowFilter = "YEAR_FROM= '" + Year2From + "' AND YEAR_TO='" + Year2To + "'";
                    dt1 = dtUserDefinedBudgetActual.DefaultView.ToTable();
                    if (dtSource.DefaultView.Count <= 1 && dt1.Rows.Count > 0)
                    {
                        dtSource.Merge(dt1);
                    }
                }

                dtSource.DefaultView.RowFilter = string.Empty;

                resultArgs = FetchUserDefinedBalances();
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    dtUserDefinedBudgetBalances = resultArgs.DataSource.Table;
                }
            }
        }

        /// <summary>
        /// This method is used to get user defined budget actual amount for ginven date range and ledger
        /// </summary>
        /// <param name="datefrom"></param>
        /// <param name="dateto"></param>
        /// <param name="ledgerid"></param>
        /// <param name="transmode"></param>
        /// <param name="transflag"></param>
        /// <returns></returns>
        private double GetUserDefinedBudtetActualAmount(string datefrom, string dateto, Int32 ledgerid, string transmode, string transflag)
        {
            double rtn = 0;

            if (dtUserDefinedBudgetActual.Rows.Count > 0)
            {
                dtUserDefinedBudgetActual.DefaultView.RowFilter = string.Empty;
                dtUserDefinedBudgetActual.DefaultView.RowFilter = "DATE_FROM= '" + datefrom + "' AND DATE_TO='" + dateto + "' AND LEDGER_ID = " + ledgerid
                                    + " AND TRANS_MODE='" + transmode + "' AND TRANS_FLAG='" + transflag + "'";
                if (dtUserDefinedBudgetActual.DefaultView.Count == 1)
                {
                    rtn = UtilityMember.NumberSet.ToDouble(dtUserDefinedBudgetActual.DefaultView[0]["ACTUAL_AMOUNT"].ToString());
                }
            }
            dtUserDefinedBudgetActual.DefaultView.RowFilter = string.Empty;
            return rtn;
        }

        public ResultArgs FetchUserDefinedBalances()
        {
            ResultArgs resultargs = new ResultArgs();
            string Year1From = UtilityMember.DateSet.ToDate("01/04/2017", false).ToShortDateString();
            string Year1To = UtilityMember.DateSet.ToDate("31/03/2018", false).ToShortDateString();

            string Year2From = UtilityMember.DateSet.ToDate("01/04/2018", false).ToShortDateString();
            string Year2To = UtilityMember.DateSet.ToDate("31/03/2019", false).ToShortDateString();

            string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.FetchuserDefinedBudgetBalance);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_FROMColumn, Year1From);
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_TOColumn, Year1To);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, Year2From);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, Year2To);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable, budgetInfo);
            }

            return resultargs;
        }
        
        private void HideYearColumns()
        {
            for (int i = this.ReportProperties.NoOfYears+2; i <= 6; i++)
            {
               if (i == 3)
                {
                    //Hide Y3 Year - Last Year
                    HideTableCell(xrtblHeaderCaption, xrHeaderRow, xrCapY3Actual);
                    HideTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapY3ActualAmount);
                    HideTableCell(xrtblGrpLedgerGroup, XrRowLGHeader, xrGrpLGY3AcutualSum);
                    HideTableCell(xrtblLedger, XrValueRow, xrY3Actual);
                    HideTableCell(xrTblFooterLG, XrRowLGFooter, xrY3LGSumActual);
                    HideTableCell(xrtblGrpFooterTransmode, xrRowFooterVType, xrcellVTypeGrpY3Actual);
                    HideTableCell(xrTblCLGrpHeader, xrRowGrand1, xrcellGrandGrpY3Actual);
                    HideTableCell(xrTblCLGrpHeader, xrRowGrand2, xrcellBalanceGrpY3Actual);
                }
                else if (i == 4)
                {
                    //Hide Y4 Year - Last Year
                    HideTableCell(xrtblHeaderCaption, xrHeaderRow, xrCapY4Actual);
                    HideTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapY4ActualAmount);
                    HideTableCell(xrtblGrpLedgerGroup, XrRowLGHeader, xrGrpLGY4AcutualSum);
                    HideTableCell(xrtblLedger, XrValueRow, xrY4Actual);
                    HideTableCell(xrTblFooterLG, XrRowLGFooter, xrY4LGSumActual);
                    HideTableCell(xrtblGrpFooterTransmode, xrRowFooterVType, xrcellVTypeGrpY4Actual);
                    HideTableCell(xrTblCLGrpHeader, xrRowGrand1, xrcellGrandGrpY4Actual);
                    HideTableCell(xrTblCLGrpHeader, xrRowGrand2, xrcellBalanceGrpY4Actual);
                }
               else if (i == 5)
               {
                   //Hide Y5 Year - Last Year
                   HideTableCell(xrtblHeaderCaption, xrHeaderRow, xrCapY5Actual);
                   HideTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapY5ActualAmount);
                   HideTableCell(xrtblGrpLedgerGroup, XrRowLGHeader, xrGrpLGY5AcutualSum);
                   HideTableCell(xrtblLedger, XrValueRow, xrY5Actual);
                   HideTableCell(xrTblFooterLG, XrRowLGFooter, xrY5LGSumActual);
                   HideTableCell(xrtblGrpFooterTransmode, xrRowFooterVType, xrcellVTypeGrpY5Actual);
                   HideTableCell(xrTblCLGrpHeader, xrRowGrand1, xrcellGrandGrpY5Actual);
                   HideTableCell(xrTblCLGrpHeader, xrRowGrand2, xrcellBalanceGrpY5Actual);
               }
                else if (i == 6)
                {
                    //Hide Y6 Year - Last Year
                    HideTableCell(xrtblHeaderCaption, xrHeaderRow, xrCapY6Actual);
                    HideTableCell(xrtblHeaderCaption, xrHeaderRow1, xrCapY6ActualAmount);
                    HideTableCell(xrtblGrpLedgerGroup, XrRowLGHeader, xrGrpLGY6AcutualSum);
                    HideTableCell(xrtblLedger, XrValueRow, xrY6Actual);
                    HideTableCell(xrTblFooterLG, XrRowLGFooter, xrY6LGSumActual);
                    HideTableCell(xrtblGrpFooterTransmode, xrRowFooterVType, xrcellVTypeGrpY6Actual);
                    HideTableCell(xrTblCLGrpHeader, xrRowGrand1, xrcellGrandGrpY6Actual);
                    HideTableCell(xrTblCLGrpHeader, xrRowGrand2, xrcellBalanceGrpY6Actual);
                }
            }

            if (this.ReportProperties.NoOfYears <= 3)
            {
                xrtblHeaderCaption.WidthF = xrtblGrpLedgerGroup.WidthF = 800;
                xrtblLedger.WidthF = xrTblFooterLG.WidthF = xrtblGrpFooterTransmode.WidthF = 800;
                xrTblCLGrpHeader.WidthF = xrTblCLGrpHeader.WidthF = 800;
                xrtblGrpHeadgerTransmode.WidthF =  800;                
            }
            else
            {
                xrtblHeaderCaption.WidthF = xrtblGrpLedgerGroup.WidthF = 1065;
                xrtblLedger.WidthF = xrTblFooterLG.WidthF = xrtblGrpFooterTransmode.WidthF = 1065;
                xrTblCLGrpHeader.WidthF = xrTblCLGrpHeader.WidthF = 1065;
                xrtblGrpHeadgerTransmode.WidthF = 1065;
                xrCapLedgerName.WidthF = xrCapGrandTotal.WidthF = xrCapGrandTotal1.WidthF = 230;
                xrCapLedgerName1.WidthF = this.xrgrpLedgerGroup.WidthF = xrCellLGSubTotal.WidthF  = xrCapCode.WidthF + xrCapLedgerName.WidthF;
                xrLedgerName.WidthF = xrCapLedgerName.WidthF;
            }

            
        }

        #endregion

        private void xrY1Budgeted_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double y1budgeted = this.ReportProperties.NumberSet.ToDouble(xrY1Budgeted.Text);
            //if (y1budgeted != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    xrY1Budgeted.Text = "";
            //}
            if (string.IsNullOrEmpty(xrY1Budgeted.Text)) xrY1Budgeted.Text = "0.0";
        }

        private void xrY1Actual_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double y1budgeted = this.ReportProperties.NumberSet.ToDouble(xrY1Actual.Text);
            //if (y1budgeted != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    xrY1Actual.Text = "";
            //}
            if (string.IsNullOrEmpty(xrY1Actual.Text)) xrY1Actual.Text = "0.0";
        }

        private void xrY2Budgeted_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double Y2Actual = this.ReportProperties.NumberSet.ToDouble(xrY2Budgeted.Text);
            //if (Y2Actual != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    xrY2Budgeted.Text = "";
            //}
            if (string.IsNullOrEmpty(xrY2Budgeted.Text)) xrY2Budgeted.Text = "0.0";
        }

        private void xrY2Actual_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double Y2Actual = this.ReportProperties.NumberSet.ToDouble(xrY2Actual.Text);
            //if (Y2Actual != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    xrY2Actual.Text = "";
            //}
            if (string.IsNullOrEmpty(xrY2Actual.Text)) xrY2Actual.Text = "0.0";
        }

        private void xrY3Actual_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double Y3Actual = this.ReportProperties.NumberSet.ToDouble(xrY3Actual.Text);
            //if (Y3Actual != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    xrY3Actual.Text = "";
            //}
            if (string.IsNullOrEmpty(xrY3Actual.Text)) xrY3Actual.Text = "0.0";
        }

        private void xrY4Actual_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //XRTableCell cell = sender as XRTableCell;
            //double xrY4Actual = this.ReportProperties.NumberSet.ToDouble(cell.Text);
            //if (xrY4Actual != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    cell.Text = "";
            //}
            if (string.IsNullOrEmpty(xrY4Actual.Text)) xrY4Actual.Text = "0.0";
        }

        private void xrcellGrandGrpY4Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetPaymentTotal("Y4_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellGrandGrpY3Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetPaymentTotal("Y3_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellGrandGrpY2Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetPaymentTotal("Y2_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellGrandGrpY2Budgeted_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetPaymentTotal("Y2_BUDGETED_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellGrandGrpY1Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetPaymentTotal("Y1_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellGrandGrpY1Budgeted_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetPaymentTotal("Y1_BUDGETED_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellBalanceGrpY4Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetGrandBalance("Y4_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellBalanceGrpY3Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetGrandBalance("Y3_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellBalanceGrpY2Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetGrandBalance("Y2_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellBalanceGrpY2Budgeted_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetGrandBalance("Y2_BUDGETED_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellBalanceGrpY1Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetGrandBalance("Y1_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellBalanceGrpY1Budgeted_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetGrandBalance("Y1_BUDGETED_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellVTypeGrpY4Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = 0;
            rtn = GetReceiptTotal("Y4_ACTUAL_AMOUNT", e);
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellVTypeGrpY3Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = 0;
            rtn = GetReceiptTotal("Y3_ACTUAL_AMOUNT", e);
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellVTypeGrpY2Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = 0;
            rtn = GetReceiptTotal("Y2_ACTUAL_AMOUNT", e);
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellVTypeGrpY2Budgeted_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = 0;
            rtn = GetReceiptTotal("Y2_BUDGETED_AMOUNT", e);
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellVTypeGrpY1Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = 0;
            rtn = GetReceiptTotal("Y1_ACTUAL_AMOUNT", e);
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellVTypeGrpY1Budgeted_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = 0;
            rtn = GetReceiptTotal("Y1_BUDGETED_AMOUNT", e);
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellVTypeGrpY5Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = 0;
            rtn = GetReceiptTotal("Y5_ACTUAL_AMOUNT", e);
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellVTypeGrpY6Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = 0;
            rtn = GetReceiptTotal("Y6_ACTUAL_AMOUNT", e);
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellGrandGrpY5Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetPaymentTotal("Y5_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellGrandGrpY6Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetPaymentTotal("Y6_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellBalanceGrpY5Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetGrandBalance("Y5_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrcellBalanceGrpY6Actual_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rtn = GetGrandBalance("Y6_ACTUAL_AMOUNT");
            e.Result = rtn;
            e.Handled = true;
        }

        private void xrY5Actual_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (string.IsNullOrEmpty(xrY5Actual.Text)) xrY5Actual.Text = "0.0";
        }

        private void xrY6Actual_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (string.IsNullOrEmpty(xrY6Actual.Text)) xrY6Actual.Text = "0.0";
        }

    }
}
