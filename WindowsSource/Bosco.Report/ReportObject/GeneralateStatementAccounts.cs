using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateStatementAccounts : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        
        #endregion

        #region Constructor
        public GeneralateStatementAccounts()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        DataTable dtReportAssetLedgers;
        DataTable dtReportLiabilityLedgers;
        DataTable dtReportIncomeLedgers;
        DataTable dtReportExpenditureLedgers;

        //Raw sum of all income, asset, expense and liabilities
        double CY_IncomeSum = 0;
        double CY_ExpenseSum = 0;
        double PY_IncomeSum = 0;
        double PY_ExpenseSum = 0;

        double CY_AssetSum = 0;
        double PY_AssetSum = 0;
        double CY_LiabilitySum = 0;
        double PY_LiabilitySum = 0;


        //Final difference
        double CYAL_Difference = 0;
        double PYAL_Difference = 0;
        double CYIE_Difference = 0;
        double PYIE_Difference = 0;

        //Monthly Receipts && Payments----------------------------------------------------
        double TotalReceipts = 0;
        double TotalPayments = 0;
        //--------------------------------------------------------------------------------

        
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindReport();
        }
        #endregion

        #region Method
        private void BindReport()
        {
            try
            {    
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.SetTitleLeftWithoutMargin();
                this.Margins.Left = 20;
                this.Margins.Right = 20;
                this.SetTitleActualWidth(xrtblHeaderCaption.WidthF);
                this.PageInfoWidth = xrLedgerCode.WidthF + xrLedgerName.WidthF;
                                                
                setHeaderTitleAlignment();
                SetReportTitle();
                if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || 
                    this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.Project))
                {
                    ShowReportFilterDialog();
                }
                else
                {
                    if (this.UIAppSetting.UICustomizationForm == "1")
                    {
                        if (ReportProperty.Current.ReportFlag == 0)
                        {
                            SplashScreenManager.ShowForm(typeof(frmReportWait));
                            BindProperty();
                            SplashScreenManager.CloseForm();
                            base.ShowReport();
                        }
                        else
                        {
                            ShowReportFilterDialog();
                        }
                    }
                    else
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindProperty();
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }
        
        private void BindProperty()
        {
            AttachClosedProjectsBasedOnPreviusYear();
            ResultArgs resultArgs = GetStatementReportSource();
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                DataTable dtStatementReportSource = resultArgs.DataSource.Table;
                
                //2. For Process Annual Statement of Accounts ----------------------------------------------------------
                resultArgs = GeneralteAnnualStatement(dtStatementReportSource);
                //------------------------------------------------------------------------------------------------------
                
                //3. Attach Previous PREVIOUS YEARS PROFIT OR LOSS value, TOTAL OF INCOME/EXPENDITURE whichever is higher
                //TOTAL DEPRECIATION CARREID FORWARD to Statement of Accounts
                AttachStatementFooterBalanceDetails(dtStatementReportSource);
                
                //4. Attach Debit balance and Credit Balance
                //string BalanceExpression = "IIF(CON_LEDGER_CODE='B',  CUR_YR_BALANCE, (" + reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName + " - " + reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName + ") )";
                string BalanceExpression = "(" + reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName + " - " + reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName + ")";
                string DebitBalanceExpression = "IIF(" + BalanceExpression + ">=0, " + BalanceExpression + ", 0)";
                string CreditBalanceExpression = "IIF(" + BalanceExpression + "<0, " + BalanceExpression + "  * -1 , 0)";
                dtStatementReportSource.Columns.Add(reportSetting1.GENERALATE_REPORTS.DEBIT_BALANCEColumn.ColumnName, typeof(double), DebitBalanceExpression);
                dtStatementReportSource.Columns.Add(reportSetting1.GENERALATE_REPORTS.CREDIT_BALANCEColumn.ColumnName, typeof(double), CreditBalanceExpression);

                dtStatementReportSource.TableName = "BranchGenerlateStatements";
                this.DataSource = dtStatementReportSource;
                this.DataMember = dtStatementReportSource.TableName;

                //Show Difference in Statement of Accounts
                double genSumbDr = UtilityMember.NumberSet.ToDouble(dtStatementReportSource.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName + ")", string.Empty).ToString());
                double genSumbCr = UtilityMember.NumberSet.ToDouble(dtStatementReportSource.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName + ")", string.Empty).ToString());
                xrLblDiffGeneralStatement.Text = "Note :: Difference in Statement of Accounts " + UtilityMember.NumberSet.ToNumber(genSumbDr - genSumbCr);
            }

            Detail.SortFields.Add(new GroupField(reportSetting1.GENERALATE_REPORTS.IS_CASH_BANK_FD_ORDERColumn.ColumnName));
            Detail.SortFields.Add(new GroupField(reportSetting1.GENERALATE_REPORTS.LEDGER_CODEColumn.ColumnName));
            Detail.SortFields.Add(new GroupField(reportSetting1.GENERALATE_REPORTS.LEDGER_NAMEColumn.ColumnName));
        }
        
        /// <summary>
        /// On 26/02/2024, For Previous year balance, let us take closed projects also based on previous year date from
        /// </summary>
        private void AttachClosedProjectsBasedOnPreviusYear()
        {
            if (!string.IsNullOrEmpty(ReportProperties.DateFrom))
            {
                DateTime PYDateFrom = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-1);
                ResultArgs result = GetProjects(PYDateFrom);
                if (result.Success)
                {
                    string[] selectedprojects = ReportProperties.Project.Split(',');
                    DataTable dtPYClosedProjects = result.DataSource.Table;
                    dtPYClosedProjects.DefaultView.RowFilter = ReportParameters.DATE_CLOSEDColumn.ColumnName + " IS NOT NULL";
                    foreach (DataRow dr in dtPYClosedProjects.Rows)
                    {
                        Int32 pid = UtilityMember.NumberSet.ToInteger(dr[reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn.ColumnName].ToString());
                        if (!selectedprojects.Contains(pid.ToString()))
                        {
                            ReportProperties.Project += "," + pid.ToString();
                        }
                    }
                }
            }
        }
    

        /// <summary>
        /// For Statement Of Accounts
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetStatementReportSource()
        {
            string condition = string.Empty;
            string reportSQL = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.GeneralateReportStatementAccounts);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, settingProperty.FirstFYDateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, reportSQL);

                if (resultArgs != null && resultArgs.Success)
                {
                    DataTable dtReportdata = resultArgs.DataSource.Table;

                    // Calculate Total Receipts and Total Payments----------------------------------------------------------
                    //condition = reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName + " <> 'A'";
                    //TotalReceipts = UtilityMember.NumberSet.ToDouble(dtReportdata.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDIT_CURRENTColumn.ColumnName + ")", condition).ToString());
                    //TotalPayments = UtilityMember.NumberSet.ToDouble(dtReportdata.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBIT_CURRENTColumn.ColumnName + ")", condition).ToString());
                    //lblTotalReceipts.Text = "Total Receipts :: " + UtilityMember.NumberSet.ToNumber(TotalReceipts);
                    //lblTotalPayments.Text = "Total Payments :: " + UtilityMember.NumberSet.ToNumber(TotalPayments);
                    //------------------------------------------------------------------------------------------------------
                    
                    dtReportdata = IncludeDepreciationAccumulatedLedgersByDefault(dtReportdata);
                    condition = string.Empty;
                    //1. Attach Credit and Debit, For IE and Books Begin, add Ledger Opening balance
                    condition = "IIF( ( ( (" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + ") AND CON_LEDGER_CODE <> 'G' " +
                                        " AND '" + UtilityMember.DateSet.ToDate(ReportProperties.DateFrom) + "' = '" + UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString()) + "')" +
                                        " OR (" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Assert + "," + (int)Natures.Libilities + ") AND GROUP_ID NOT IN (12, 13, 14)) ) " +
                                        " AND " + reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName + " > 0 ), " +
                                        reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName + ", 0)";
                    string Debit = reportSetting1.GENERALATE_REPORTS.DEBIT_CURColumn.ColumnName + " + " + condition;

                    condition = "IIF( ( ( (" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + ") AND CON_LEDGER_CODE <> 'G'" +
                                    " AND '" + UtilityMember.DateSet.ToDate(ReportProperties.DateFrom) + "' = '" + UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString()) + "')" +
                                    " OR (" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Assert + "," + (int)Natures.Libilities + ") AND GROUP_ID NOT IN (12, 13, 14) )) " +
                                    " AND " + reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName + " < 0 ), " +
                                    "(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName + " * -1), 0)";

                    string Credit = reportSetting1.GENERALATE_REPORTS.CREDIT_CURColumn.ColumnName + " + " + condition; ;
                    dtReportdata.Columns.Add(reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName, typeof(double), Debit);   
                    dtReportdata.Columns.Add(reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName, typeof(double), Credit); 

                    //1.1. Attach Credit Previous and Debit Previous, For IE and Books Begin, add Ledger previous Opening balance
                    condition = "IIF( ( ( (" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + ") AND CON_LEDGER_CODE <> 'G'" +
                                    " AND '" + UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-1).ToShortDateString() + "' = '" + UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString()) + "')" +
                                    " OR (" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Assert + "," + (int)Natures.Libilities + ") AND GROUP_ID NOT IN (12, 13, 14) )) " +
                                    " AND " + reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName + " > 0 ), " +
                                    reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName + ", 0)";

                    Debit = reportSetting1.GENERALATE_REPORTS.DEBIT_PREVColumn.ColumnName + " +  " + condition;

                    condition = "IIF( ( ( (" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + ") AND CON_LEDGER_CODE <> 'G'" +
                                   " AND '" + UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-1).ToShortDateString() + "' = '" + UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString()) + "')" +
                                   " OR (" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Assert + "," + (int)Natures.Libilities + ")AND GROUP_ID NOT IN (12, 13, 14) )) " +
                                   " AND " + reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName + " < 0 ), " +
                                   "(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName + " * -1), 0)"; 
                    Credit = reportSetting1.GENERALATE_REPORTS.CREDIT_PREVColumn.ColumnName + " + " + condition;                    
                    dtReportdata.Columns.Add(reportSetting1.GENERALATE_REPORTS.DEBIT_PREVIOUSColumn.ColumnName, typeof(double), Debit); 
                    dtReportdata.Columns.Add(reportSetting1.GENERALATE_REPORTS.CREDIT_PREVIOUSColumn.ColumnName, typeof(double), Credit);

                    //2. For Cash/Bank/FDs, After grouping cash and bank ledger, skip indiviudal cash/bank/fd ledgers
                    dtReportdata = AttachStatementCashBankFDDetails(dtReportdata);


                    resultArgs.DataSource.Data = dtReportdata;
                }
                else
                {
                    //Message Box
                }
            }

            return resultArgs;
        }

        /// <summary>
        /// On 07/03/2023, Attach Deprecaition accumulated (Expenses Ledgers to Liabilities Ledgters
        /// </summary>
        /// <param name="dtReportdata"></param>
        private DataTable IncludeDepreciationAccumulatedLedgersByDefault(DataTable dtReportdata)
        {
            string ledgercode = string.Empty;
            string ledgername = string.Empty;
            double CYopamount = 0;
            double CYdebitamount = 0;
            double CYcreditamount = 0;
            double PYopamount = 0;
            double PYdebitamount = 0;
            double PYcreditamount = 0;
            double CYdepreciationamount = 0;
            double PYdepreciationamount = 0;

            string condition = reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn + " = 'G'";
            if (this.ReportProperties.ShowIndividualDepreciationLedgers == 1)
            {
                // On 07/03/2023, Attach Deprecaition accumulated (Expenses Ledgers to Liabilities Ledgters----------------------------------
                // let us add all 'G' expenses depreciation ledgers to 'I' Liabilities Ledgers in against side CR to DR and DR to CR
                dtReportdata.DefaultView.RowFilter = string.Empty;
                dtReportdata.DefaultView.RowFilter = condition;
                foreach (DataRowView drv in dtReportdata.DefaultView)
                {
                    ledgercode = drv[reportSetting1.GENERALATE_REPORTS.LEDGER_CODEColumn.ColumnName].ToString();
                    ledgername = drv[reportSetting1.GENERALATE_REPORTS.LEDGER_NAMEColumn.ColumnName].ToString();
                    CYopamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName].ToString());
                    CYopamount = CYopamount * -1; // For changing nature from Expenses to Liabilities
                    CYdebitamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.DEBIT_CURColumn.ColumnName].ToString());
                    CYcreditamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.CREDIT_CURColumn.ColumnName].ToString());
                    CYdepreciationamount = (CYdebitamount - CYcreditamount);

                    PYopamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName].ToString());
                    PYopamount = PYopamount * -1; // For changing nature from Expenses to Liabilities
                    PYdebitamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.DEBIT_PREVColumn.ColumnName].ToString());
                    PYcreditamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.CREDIT_PREVColumn.ColumnName].ToString());
                    PYdepreciationamount = (PYdebitamount - PYcreditamount);

                    InsertStatementDetailRow(dtReportdata, Natures.Libilities, (Int32)Natures.Libilities, 5, "I", "TOTAL DEPRECIATION CARREID FORWARD", ledgercode, ledgername,
                                            CYcreditamount, CYdebitamount, PYcreditamount, PYdepreciationamount, CYopamount, PYopamount, false);
                }
                dtReportdata.DefaultView.RowFilter = string.Empty;
                //---------------------------------------------------------------------------------------------------------------------------
            }
            else
            {
                CYopamount = UtilityMember.NumberSet.ToDouble(dtReportdata.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName + ")", condition).ToString());
                CYdebitamount = UtilityMember.NumberSet.ToDouble(dtReportdata.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBIT_CURColumn.ColumnName + ")", condition).ToString());
                CYcreditamount = UtilityMember.NumberSet.ToDouble(dtReportdata.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDIT_CURColumn.ColumnName + ")", condition).ToString());

                PYopamount = UtilityMember.NumberSet.ToDouble(dtReportdata.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName + ")", condition).ToString());
                PYdebitamount = UtilityMember.NumberSet.ToDouble(dtReportdata.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBIT_PREVColumn.ColumnName + ")", condition).ToString());
                PYcreditamount = UtilityMember.NumberSet.ToDouble(dtReportdata.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDIT_PREVColumn.ColumnName + ")", condition).ToString());

                dtReportdata.DefaultView.RowFilter = string.Empty;
                dtReportdata.DefaultView.RowFilter = reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn + " <> 'G'";
                dtReportdata = dtReportdata.DefaultView.ToTable();

                //For G (Current Year)
                ledgercode = "G/71";
                ledgername = "TOTAL DEPRECIATION (B4 + B5 + B6)";
                InsertStatementDetailRow(dtReportdata, Natures.Expenses, (Int32)Natures.Expenses, 3, "G", "DEPRECIATION ON FIXED ASSETS", ledgercode, ledgername,
                                             CYdebitamount, CYcreditamount, PYdebitamount, PYcreditamount, CYopamount, PYopamount, false);

                ledgercode = "I/73-75";
                ledgername = "DEPRECIATION ON FIXED ASSETS";
                CYopamount = CYopamount * -1; // For changing nature from Expenses to Liabilities
                PYopamount = PYopamount * -1; // For changing nature from Expenses to Liabilities
                InsertStatementDetailRow(dtReportdata, Natures.Libilities, (Int32)Natures.Libilities, 5, "I", "TOTAL DEPRECIATION CARREID FORWARD", ledgercode, ledgername,
                                            CYcreditamount, CYdebitamount, PYcreditamount, PYdebitamount, CYopamount, PYopamount, false);
            }

            return dtReportdata;
        }
        
        /// <summary>
        /// Generate Annual Statement and calcualte logic
        /// Assign 4 Sub reports data
        /// </summary>
        /// <returns></returns>
        private ResultArgs GeneralteAnnualStatement(DataTable dtAnnaualStatementReport)
        {
            try
            {
                //resultArgs = GetAnnualStatementReportSource();
                if (resultArgs.Success && dtAnnaualStatementReport != null)
                {
                    //dtAnnaualStatementReport = resultArgs.DataSource.Table;
                    //For IE Ledgers, If current year or Previous Year Date From is equal to first FY, let add opening Balance
                    string BalanceExpression = "(" + reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName + " - " + reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName + ")";
                    string BalanceExpression_Previous = "(" + reportSetting1.GENERALATE_REPORTS.DEBIT_PREVIOUSColumn.ColumnName + " - " + reportSetting1.GENERALATE_REPORTS.CREDIT_PREVIOUSColumn.ColumnName + ")";
                    
                    dtAnnaualStatementReport = dtAnnaualStatementReport.DefaultView.ToTable();
                    dtAnnaualStatementReport.Columns.Add(reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName, typeof(double), BalanceExpression);
                    dtAnnaualStatementReport.Columns.Add(reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName, typeof(double), BalanceExpression_Previous);
                                        
                    GeneralateSubAnnualStatementAccounts subreportsAsset = xrSubAssetLedgers.ReportSource as GeneralateSubAnnualStatementAccounts;
                    GeneralateSubAnnualStatementAccounts subreportsLiability = xrSubLiabilityLedgers.ReportSource as GeneralateSubAnnualStatementAccounts;
                    GeneralateSubAnnualStatementAccounts subreportsIncome = xrSubIncomeLedgers.ReportSource as GeneralateSubAnnualStatementAccounts;
                    GeneralateSubAnnualStatementAccounts subreportsExpenditure = xrSubExpenseLedgers.ReportSource as GeneralateSubAnnualStatementAccounts;
                    
                    dtAnnaualStatementReport.DefaultView.RowFilter = string.Empty;
                    ProcessAssetLiabilityLedgers(dtAnnaualStatementReport);
                    ProcessIELedgers(dtAnnaualStatementReport);

                    //1. Calculate IE Ledger difference
                    CY_IncomeSum = UtilityMember.NumberSet.ToDouble(dtReportIncomeLedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString());
                    PY_IncomeSum = UtilityMember.NumberSet.ToDouble(dtReportIncomeLedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString());
                    CY_ExpenseSum = UtilityMember.NumberSet.ToDouble(dtReportExpenditureLedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString());
                    PY_ExpenseSum = UtilityMember.NumberSet.ToDouble(dtReportExpenditureLedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString());

                    //02/03/2023, To attach Previous Year Adjustment entry for if and if only for first FY or previous year is first year -------------
                    //On 22/04/2024, For FMA Mumbai, we add logic to have real balance which is defined in Generlated setting
                    if (AppSetting.HeadofficeCode.ToUpper() == "FMAINB")
                    {
                        if (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToShortDateString() == UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString())
                                && this.AppSetting.GeneralateOpeningIEBalance > 0)
                        {
                            double tmpCYIEBalance = (CY_ExpenseSum - CY_IncomeSum);
                            double tmpIEBalance = this.AppSetting.GeneralateOpeningIEBalance;
                            if (this.AppSetting.GeneralateOpeningIEBalanceMode == 0) //For Operatiing Profile
                            {
                                tmpIEBalance = tmpCYIEBalance + tmpIEBalance;
                                CY_IncomeSum += Math.Abs(tmpIEBalance);
                            }
                            else //For Operatiing Loses
                            {
                                tmpIEBalance = tmpCYIEBalance - tmpIEBalance;
                                CY_ExpenseSum += Math.Abs(tmpIEBalance);
                            }
                        }

                        if (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-1).ToShortDateString() == UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString())
                            && this.AppSetting.GeneralateOpeningIEBalance > 0)
                        {
                            double tmpPYIEBalance = (PY_ExpenseSum - PY_IncomeSum);
                            double tmpIEBalance = this.AppSetting.GeneralateOpeningIEBalance;
                            if (this.AppSetting.GeneralateOpeningIEBalanceMode == 0) //For Operatiing Profile
                            {
                                tmpIEBalance = tmpPYIEBalance + tmpIEBalance;
                                PY_IncomeSum += Math.Abs(tmpIEBalance);
                            }
                            else //For Operatiing Loses
                            {
                                tmpIEBalance = tmpPYIEBalance - tmpIEBalance;
                                PY_ExpenseSum += Math.Abs(tmpIEBalance);
                            }
                        }
                    }
                    //---------------------------------------------------------------------------------------------
                    CYIE_Difference = (CY_ExpenseSum - CY_IncomeSum);
                    PYIE_Difference = (PY_ExpenseSum - PY_IncomeSum);

                    //On 22/04/2024, for other FMA provinces, let us take defined Generlated Opening Balance as it is defined
                    if (AppSetting.HeadofficeCode.ToUpper() != "FMAINB")
                    {
                        if (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToShortDateString() == UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString())
                                && this.AppSetting.GeneralateOpeningIEBalance > 0)
                        {
                            PYIE_Difference = this.AppSetting.GeneralateOpeningIEBalance * (this.AppSetting.GeneralateOpeningIEBalanceMode == 1 ? -1 : 1);
                        }
                        //if (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-1).ToShortDateString() == UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString())
                        //        && this.AppSetting.GeneralateOpeningIEBalance > 0)
                        //{
                            
                        //}
                    }
                    //--------------------------------------------------------------------------------------------------------------------

                    //2. Calcualte Asset and Liability sum difference after adding IE difference
                    CY_AssetSum = UtilityMember.NumberSet.ToDouble(dtReportAssetLedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString());
                    PY_AssetSum = UtilityMember.NumberSet.ToDouble(dtReportAssetLedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString());
                    CY_LiabilitySum = UtilityMember.NumberSet.ToDouble(dtReportLiabilityLedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString());
                    PY_LiabilitySum = UtilityMember.NumberSet.ToDouble(dtReportLiabilityLedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString());
                    //----------------------------------------------------------------------------------------------------

                    //Include IE difference to Asset and Liabilities Difference-------------------------------
                    if (CYIE_Difference > 0)
                        CY_AssetSum += CYIE_Difference;
                    else
                        CY_LiabilitySum += Math.Abs(CYIE_Difference);

                    if (PYIE_Difference > 0)
                        PY_AssetSum += PYIE_Difference;
                    else
                        PY_LiabilitySum += Math.Abs(PYIE_Difference);

                    //------------------------------------------------------------------------------

                    CYAL_Difference = (CY_LiabilitySum - CY_AssetSum);
                    PYAL_Difference = (PY_LiabilitySum - PY_AssetSum);

                    subreportsAsset.BindProperty(Natures.Assert, dtReportAssetLedgers, CYAL_Difference, PYAL_Difference, CYIE_Difference, PYIE_Difference);
                    subreportsLiability.BindProperty(Natures.Libilities, dtReportLiabilityLedgers, CYAL_Difference, PYAL_Difference, CYIE_Difference, PYIE_Difference);
                                       subreportsIncome.BindProperty(Natures.Income, dtReportIncomeLedgers, CYAL_Difference, PYAL_Difference, CYIE_Difference, PYIE_Difference);
                    subreportsExpenditure.BindProperty(Natures.Expenses, dtReportExpenditureLedgers, CYAL_Difference, PYAL_Difference, CYIE_Difference, PYIE_Difference);
                    xrLblDiffAL.Text = "Note :: Difference in Asset & Liabilities : " + UtilityMember.NumberSet.ToNumber((subreportsAsset.CYGrandTotal - subreportsLiability.CYGrandTotal));
                    xrLblDiffIE.Text = "Note :: Difference in Gain & Loss : " + UtilityMember.NumberSet.ToNumber((subreportsIncome.CYGrandTotal - subreportsExpenditure.CYGrandTotal));
                    dtAnnaualStatementReport.DefaultView.RowFilter = string.Empty;
                }
            }
            catch(Exception err)
            {
                resultArgs.Message = "Unable to generate Annual Statement " + err.Message;
            }

            return resultArgs;
        }

        /// <summary>
        /// For Annual Statement Of Accounts - Asset and Liabilities
        /// Take All Asset Ledgers and Take Liabilities with Depreciation accumulated values
        /// </summary>
        private void ProcessAssetLiabilityLedgers(DataTable dtAnnaualReport)
        {
            string condition = string.Empty;
            
            //For Asset and Liabilities -----------------------------------------------------------------------------------------
            dtAnnaualReport.DefaultView.RowFilter = string.Empty;
            //# Group1 - Take all Asset and Liabilities
            condition = "(" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Assert + "," + (int)Natures.Libilities + ") )";
            condition += " AND (" + reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName + " <> 'I')";
            dtAnnaualReport.DefaultView.RowFilter = condition;
            DataTable dtAssetLiabilities = dtAnnaualReport.DefaultView.ToTable();
                        
            //Group 2 - Add Depreciation Accumulated Values for accumulated deprecaition
            AttachAnnualDepreciationAmount(dtAnnaualReport, dtAssetLiabilities, 4, false);

            //For Asset -  take all asset ledgers
            dtAssetLiabilities.DefaultView.RowFilter = string.Empty;
            condition = "(" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " = " + (int)Natures.Assert + ")";
            dtAssetLiabilities.DefaultView.RowFilter = condition;
            //Soruce for Asset in Asset&Liabilities Annual Report
            dtReportAssetLedgers = dtAssetLiabilities.DefaultView.ToTable();
            
            //For Liabilities -  take all ledgers
            dtAssetLiabilities.DefaultView.RowFilter = string.Empty;
            condition = "(" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " = " + (int)Natures.Libilities + ")";
            dtAssetLiabilities.DefaultView.RowFilter = condition;
            //Soruce for Liabilities in Asset&Liabilities Annual Report
            dtReportLiabilityLedgers = dtAssetLiabilities.DefaultView.ToTable();
            
            //For Liability/Income ledgers (common quary logic will display negative by default) ----------------------------
            //Hence we convert to positive by multiplying -1
            string CYColName = reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName;
            string PYColName = reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName;

            dtReportLiabilityLedgers.Columns[reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName].ColumnName = CYColName + "_1";
            dtReportLiabilityLedgers.Columns[reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName].ColumnName = PYColName + "_1";
            dtReportLiabilityLedgers.Columns.Add(CYColName, typeof(System.Double), CYColName + "_1 * - 1");
            dtReportLiabilityLedgers.Columns.Add(PYColName, typeof(System.Double), PYColName + "_1 * - 1");
            //------------------------------------------------------------------------------------------------------------------

            dtAnnaualReport.DefaultView.RowFilter = string.Empty;
            //-------------------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// For Annual Statement Of Accounts - Income and Expense
        /// Take All Income Ledgers and Take Expenses Ledgers with Depreciation current values
        /// </summary>
        private void ProcessIELedgers(DataTable dtAnnaualReport)
        {
            //For Grouping Ledges for IE Report in the Annual Statements
            //All Incomes Ledgers and Contribution From (Income) Ledgers
            //All Expenses Ledgers, Controbution To (Expense) Ledgers, Depreciation (Expense , Current)
            //Get Contribution Ledgers ids (should get it by dynamically from Finance Settings)
            double CYOtherLedgers = 0;
            double PYOtherLedgers = 0;
            string condition = string.Empty;
            //For Income and Expenditure --------------------------------------------------------------
            string ContributionFromLedgerIds = settingProperty.ContributionFromLedgers; 
            string ContributionToLedgerIds = settingProperty.ContributionToLedgers;;
            string ContributionLedgerIds = ContributionFromLedgerIds + "," + ContributionToLedgerIds;
            
            dtAnnaualReport.DefaultView.RowFilter = string.Empty;
            
            //Take all Income and Expenditure Ledges and not deprecaition Ledgers
            condition = reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + ")";
            condition += " AND " + reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName + " <> 'G'";

            dtAnnaualReport.DefaultView.RowFilter = condition;
            DataTable dtIELedgers = dtAnnaualReport.DefaultView.ToTable();

            //# Take all "Contribution From", "Contribution To" Ledgers
            //# Group1 - All Contribution Ledgers (Contribution From, Contribution To)
            condition = string.Empty;
            dtIELedgers.DefaultView.RowFilter = string.Empty;
            condition = reportSetting1.GENERALATE_REPORTS.LEDGER_IDColumn.ColumnName + " IN (" + ContributionLedgerIds + ")";
            dtIELedgers.DefaultView.RowFilter = condition;
            DataTable dtIEReportData = dtIELedgers.DefaultView.ToTable();
            
            //Take all Income Ledgers expect Contribution From Ledgers and depreciation Ledgers
            dtIEReportData.DefaultView.RowFilter = string.Empty;
            //# Group2 - Income - "Contribution From" Ledgers
            condition = "(" + reportSetting1.GENERALATE_REPORTS.LEDGER_IDColumn.ColumnName + " NOT IN (" + ContributionFromLedgerIds + ") " +
                            " AND " + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " = " + (int)Natures.Income + ")";
            dtIELedgers.DefaultView.RowFilter = string.Empty;
            CYOtherLedgers = UtilityMember.NumberSet.ToDouble(dtIELedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName + ")", condition).ToString());
            PYOtherLedgers = UtilityMember.NumberSet.ToDouble(dtIELedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName + ")", condition).ToString());
            InsertAnnualStatementDetailRow(dtIEReportData, (int)Natures.Income, 0, 4, 0, "F", "", "Total from other Income Ledgers", CYOtherLedgers, PYOtherLedgers);
            // Source for Gain Ledger in IE Annual Report (Group1 (Contribution From alone) and Group2)
            condition = "(" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " = " + (int)Natures.Income + ")";
            dtIEReportData.DefaultView.RowFilter = condition;
            dtReportIncomeLedgers = dtIEReportData.DefaultView.ToTable();

            //Take all Expenes Ledgers expect "Contribution To" Ledgers and Depreciation Ledgers
            dtIEReportData.DefaultView.RowFilter = string.Empty;
            //# Group3 - Expenditure - "Contribution To" Ledgers
            condition = "(" + reportSetting1.GENERALATE_REPORTS.LEDGER_IDColumn.ColumnName + " NOT IN (" + ContributionToLedgerIds + ") " +
                            " AND " + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " = " + (int)Natures.Expenses +
                            " AND " + reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName + " <> 'G')";
            dtIELedgers.DefaultView.RowFilter = string.Empty;
            CYOtherLedgers = UtilityMember.NumberSet.ToDouble(dtIELedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName + ")", condition).ToString());
            PYOtherLedgers = UtilityMember.NumberSet.ToDouble(dtIELedgers.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName + ")", condition).ToString());
            InsertAnnualStatementDetailRow(dtIEReportData, (int)Natures.Expenses, 0, 4, 0, "E", "", "Total from other Expenses Ledgers", CYOtherLedgers, PYOtherLedgers);

            // Source for Loss Ledger in IE Annual Report (Group1 (Contribution To alone) and Group3)
            condition = "(" + reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName + " = " + (int)Natures.Expenses + ")"; ;
            dtIEReportData.DefaultView.RowFilter = condition;
            dtReportExpenditureLedgers= dtIEReportData.DefaultView.ToTable();

            //Add deprecaition Ledgers (Current)
            AttachAnnualDepreciationAmount(dtAnnaualReport, dtReportExpenditureLedgers, 5, true);
            
            //For Liability/Income ledgers (common quary logic will display negative by default) ----------------------------
            //Hence we convert to positive by multiplying -1
            string CYColName = reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName;
            string PYColName = reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName;

            dtReportIncomeLedgers.Columns[reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName].ColumnName = CYColName + "_1";
            dtReportIncomeLedgers.Columns[reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName].ColumnName = PYColName + "_1";
            dtReportIncomeLedgers.Columns.Add(CYColName, typeof(System.Double), CYColName + "_1 * - 1");
            dtReportIncomeLedgers.Columns.Add(PYColName, typeof(System.Double), PYColName + "_1 * - 1");
            //------------------------------------------------------------------------------------------------------------------
            dtAnnaualReport.DefaultView.RowFilter = string.Empty;
            //-----------------------------------------------------------------------------------------
        }
        
        /// <summary>
        /// For Statement Of Accounts
        /// Group and Sum of Cash/Bank/FD ledgers
        /// </summary>
        /// <param name="dtReportSource"></param>
        private DataTable AttachStatementCashBankFDDetails(DataTable dtReportSource)
        {
            if (dtReportSource != null)
            {
                dtReportSource.DefaultView.RowFilter = string.Empty;
                dtReportSource.DefaultView.RowFilter = this.ReportParameters.GROUP_IDColumn.ColumnName + " IN (" + (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.FixedDeposit + ")";
                //dtReportSource.DefaultView.RowFilter = reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName + " =  'A'";
                DataTable dtCashBankFD = dtReportSource.DefaultView.ToTable();

                dtReportSource.DefaultView.RowFilter = this.ReportParameters.GROUP_IDColumn.ColumnName + " NOT IN (" + (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.FixedDeposit + ")";
                dtReportSource = dtReportSource.DefaultView.ToTable();

                //For Cash
                double cashamountCredit = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.Cash).ToString());
                double cashamountDebit = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.Cash).ToString());
                double cashamountCreditPrevious = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDIT_PREVIOUSColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.Cash).ToString());
                double cashamountDebitPrevious = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBIT_PREVIOUSColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.Cash).ToString());
                double cashOpAmount = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.Cash).ToString());
                double cashOpAmountPrevious = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.Cash).ToString());
                cashamountDebit += (cashOpAmount > 0 ? cashOpAmount : 0);
                cashamountCredit += (cashOpAmount < 0 ? Math.Abs(cashOpAmount) : 0);
                cashamountDebitPrevious += (cashOpAmountPrevious > 0 ? cashOpAmountPrevious : 0);
                cashamountCreditPrevious += (cashOpAmountPrevious < 0 ? Math.Abs(cashOpAmountPrevious) : 0);
                InsertStatementDetailRow(dtReportSource, Natures.Assert, 0, 0, "A", "FINANCIAL STATUS", "A1", "Cash", cashamountDebit, cashamountCredit, cashamountDebitPrevious, cashamountCreditPrevious, cashOpAmount, cashOpAmountPrevious, true);

                //For BankAccounts
                double bankamountCredit = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.BankAccounts).ToString());
                double bankamountDebit = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.BankAccounts).ToString());
                double bankamountCreditPrevious = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDIT_PREVIOUSColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.BankAccounts).ToString());
                double bankamountDebitPrevious = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBIT_PREVIOUSColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.BankAccounts).ToString());
                double bankOpAmount = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.BankAccounts).ToString());
                double bankOpAmountPrevious = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.BankAccounts).ToString());
                bankamountDebit += (bankOpAmount > 0 ? bankOpAmount : 0);
                bankamountCredit += (bankOpAmount < 0 ? Math.Abs(bankOpAmount) : 0);
                bankamountDebitPrevious += (bankOpAmountPrevious > 0 ? bankOpAmountPrevious : 0);
                bankamountCreditPrevious += (bankOpAmountPrevious < 0 ? Math.Abs(bankOpAmountPrevious) : 0);
                InsertStatementDetailRow(dtReportSource, Natures.Assert, 0, 1, "A", "FINANCIAL STATUS", "A2", "Bank", bankamountDebit, bankamountCredit, bankamountDebitPrevious, bankamountCreditPrevious, bankOpAmount, bankOpAmountPrevious, true);

                //For Fixed Deposit
                double fdamountCredit = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.FixedDeposit).ToString());
                double fdamountDebit = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.FixedDeposit).ToString());
                double fdamountCreditPrevious = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CREDIT_PREVIOUSColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.FixedDeposit).ToString());
                double fdamountDebitPrevious = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.DEBIT_PREVIOUSColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.FixedDeposit).ToString());
                double fdOpAmount = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.FixedDeposit).ToString());
                double fdOpAmountPrevious = UtilityMember.NumberSet.ToDouble(dtCashBankFD.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName + ")", this.ReportParameters.GROUP_IDColumn.ColumnName + "=" + (int)FixedLedgerGroup.FixedDeposit).ToString());
                fdamountDebit += (fdOpAmount > 0 ? fdOpAmount : 0);
                fdamountCredit += (fdOpAmount < 0 ? Math.Abs(fdOpAmount) : 0);
                fdamountDebitPrevious += (fdOpAmountPrevious > 0 ? fdOpAmountPrevious : 0);
                fdamountCreditPrevious += (fdOpAmountPrevious < 0 ? Math.Abs(fdOpAmountPrevious) : 0);
                InsertStatementDetailRow(dtReportSource, Natures.Assert, 0, 2, "A", "FINANCIAL STATUS", "A3", "Fixed Deposit", fdamountDebit, fdamountCredit, fdamountDebitPrevious, fdamountCreditPrevious, fdOpAmount, fdOpAmountPrevious, true);
            }
            return dtReportSource;
        }

        /// <summary>
        /// For Statement Of Accounts
        /// Insert footer details of Totals
        /// # Attach Previous PREVIOUS YEARS PROFIT OR LOSS value, 
        /// # TOTAL OF INCOME/EXPENDITURE whichever is higher
        /// # TOTAL DEPRECIATION CARREID FORWARD to Statement of Accounts
        /// </summary>
        /// <param name="dtReportSource"></param>
        private void AttachStatementFooterBalanceDetails(DataTable dtReportSource)
        {
            if (dtReportSource != null)
            {
                 //On 22/04/2024, for other FMA provinces, let us take defined Generlated Opening Balance as it is defined
                if (AppSetting.HeadofficeCode.ToUpper() == "FMAINB")
                {
                    InsertStatementDetailRow(dtReportSource, 0, 0, 4, "F", "REVENUE", "70", "PREVIOUS YEARS PROFIT OR LOSS", Math.Abs(PYIE_Difference), Math.Abs(PYIE_Difference), 0, 0, 0, 0, true);
                }
                else
                {
                    InsertStatementDetailRow(dtReportSource, 0, 0, 4, "J", "PREVIOUS YEARS PROFIT OR LOSS", "70", "PREVIOUS YEARS PROFIT OR LOSS", Math.Abs(PYIE_Difference), Math.Abs(PYIE_Difference), 0, 0, 0, 0, true);
                }

                ////1. I: For Total Depreciation carry forward--------------------------------------------------------------
                ////Add depreciation closing balance value always to depreciation carrt forward
                //dtReportSource.DefaultView.RowFilter = string.Empty;
                ////dtReportSource.DefaultView.RowFilter = reportSetting1.GENERALATE_REPORTS.IS_DEPRECIATION_LEDGERColumn + " =  1" ;
                //dtReportSource.DefaultView.RowFilter = reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn + " = 'G'";
                //foreach (DataRowView drv in dtReportSource.DefaultView)
                //{
                //    string ledgername = drv[reportSetting1.GENERALATE_REPORTS.LEDGER_NAMEColumn.Caption].ToString();
                //    string ledgercode = drv[reportSetting1.GENERALATE_REPORTS.LEDGER_CODEColumn.Caption].ToString();
                //    double CYopamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName].ToString());
                //    double CYdebitamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName].ToString());
                //    double CYcreditamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName].ToString());
                //    double CYdepreciationamount = (CYdebitamount - CYcreditamount);

                //    double PYopamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName].ToString());
                //    double PYdebitamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.DEBIT_PREVIOUS1Column.ColumnName].ToString());
                //    double PYcreditamount = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.GENERALATE_REPORTS.CREDIT_PREVIOUSColumn.ColumnName].ToString());
                //    double PYdepreciationamount = (PYdebitamount - PYcreditamount);

                //    //For Current Year---------------------------------------------------------------------------------------------------------
                //    //Dont add OP balances for first year, Because opening balance for IE ledgders for first FY, we already added
                //    if (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToShortDateString() != settingProperty.FirstFYDateFrom.ToShortDateString())
                //    {
                //        CYdepreciationamount += CYopamount;
                //    }
                //    double CYdepreciationamountDebitAmount = (CYdepreciationamount > 0 ? CYdepreciationamount : 0);
                //    double CYdepreciationamountCreditAmount = (CYdepreciationamount < 0 ? 0 : CYdepreciationamount);
                //    //---------------------------------------------------------------------------------------------------------------------------

                //    //For Previous Year ---------------------------------------------------------------------------------------------------------
                //    //Dont add OP balances for first year for previous year, Because opening balance for IE ledgders for first FY, we already added
                //    //if (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).AddYears(-1).ToShortDateString() !=  UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString()))
                //    //{
                //        PYdepreciationamount += PYopamount;
                //    //}
                //    double PYdepreciationamountDebitAmount = (PYdepreciationamount > 0 ? 0 : PYdepreciationamount);
                //    double PYdepreciationamountCreditAmount = (PYdepreciationamount < 0 ? PYdepreciationamount : 0);
                //    //---------------------------------------------------------------------------------------------------------------------------
                    
                //    InsertStatementDetailRow(dtReportSource, 0, 0, 5, "I", "TOTAL DEPRECIATION CARREID FORWARD", ledgercode, ledgername,
                //                                CYdepreciationamountCreditAmount, CYdepreciationamountDebitAmount, PYdepreciationamountCreditAmount, PYdepreciationamountDebitAmount, 0, 0);

                //    /*if (depreciationamount > 0)
                //        InsertStatementDetailRow(dtReportSource, 0, 0, 5, "I", "TOTAL DEPRECIATION CARREID FORWARD", ledgercode, ledgername, 0, depreciationamount, 0, 0, 0, 0);
                //    else
                //        InsertStatementDetailRow(dtReportSource, 0, 0, 5, "I", "TOTAL DEPRECIATION CARREID FORWARD", ledgercode, ledgername, depreciationamount, 0, 0, 0, 0, 0);*/
                //}
                //dtReportSource.DefaultView.RowFilter = string.Empty;
                ////--------------------------------------------------------------------------------------------


                //On 30/04/2024 For Other FMA, don;t attach H values
                if (AppSetting.HeadofficeCode.ToUpper() == "FMAINB")
                {
                    //2. H : TOTAL OF INCOME/EXPENDITURE, show greater value of income sum and expense sum
                    if (CY_IncomeSum > CY_ExpenseSum)
                        InsertStatementDetailRow(dtReportSource, 0, 0, 6, "H", "TOTAL OF INCOME/EXPENDITURE", "H/72", "TOTAL OF INCOME/EXPENDITURE whichever is higher", CY_IncomeSum, CY_IncomeSum, 0, 0, 0, 0, true);
                    else
                        InsertStatementDetailRow(dtReportSource, 0, 0, 6, "H", "TOTAL OF INCOME/EXPENDITURE", "H/72", "TOTAL OF INCOME/EXPENDITURE whichever is higher", CY_ExpenseSum, CY_ExpenseSum, 0, 0, 0, 0, true);
                }

                //3. L : NET ASSETS/DEFICIT INCOME
                if (CYAL_Difference > 0)
                {
                    InsertStatementDetailRow(dtReportSource, 0, 0, 7, "L", "NET ASSETS/DEFICIT INCOME", "L/76", "NET ASSETS/DEFICIT INCOME", CYAL_Difference, 0, 0, 0, 0, 0, true );
                }
                else
                    InsertStatementDetailRow(dtReportSource, 0, 0, 7, "L", "NET ASSETS/DEFICIT INCOME", "L/76", "NET ASSETS/DEFICIT INCOME", 0, Math.Abs(CYAL_Difference), 0, 0, 0, 0, true);
            }
        }
        

        /// <summary>
        /// For Statement Of Accounts
        /// Insert defined rows into report datatable
        /// </summary>
        /// <param name="dtReportSource"></param>
        /// <param name="CashBankFDOrder"></param>
        /// <param name="ConLedgerCode"></param>
        /// <param name="ConLedgerName"></param>
        /// <param name="LedgerCode"></param>
        /// <param name="LedgerName"></param>
        /// <param name="debitAmount"></param>
        /// <param name="creditAmount"></param>
        private void InsertStatementDetailRow(DataTable dtReportSource, Natures natureid, Int32 groupId, Int32 CashBankFDOrder, string ConLedgerCode, string ConLedgerName,
               string LedgerCode, string LedgerName, double debitAmount, double creditAmount, double debitAmountPrevious, double creditAmountPrevious, double OpAmount, double OpAmountPrevious, bool forFooter)
        {
            if (dtReportSource != null)
            {
                DataRow drFooterDetails = dtReportSource.NewRow();
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName] = (Int32)natureid;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.NATURE_IDColumn.ColumnName] = (Int32)natureid;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.LEDGER_IDColumn.ColumnName] = 0;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.GROUP_IDColumn.ColumnName] = groupId;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.IS_DEPRECIATION_LEDGERColumn.ColumnName] = 0;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.IS_CASH_BANK_FD_ORDERColumn.ColumnName] = CashBankFDOrder;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName] = ConLedgerCode;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.CON_LEDGER_NAMEColumn.ColumnName] = ConLedgerName;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.LEDGER_CODEColumn.ColumnName] = LedgerCode;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.LEDGER_NAMEColumn.ColumnName] = LedgerName;

                if (!forFooter)
                {
                    drFooterDetails[reportSetting1.GENERALATE_REPORTS.DEBIT_CURColumn.ColumnName] = Math.Abs(debitAmount); 
                    drFooterDetails[reportSetting1.GENERALATE_REPORTS.CREDIT_CURColumn.ColumnName] = Math.Abs(creditAmount);
                    drFooterDetails[reportSetting1.GENERALATE_REPORTS.DEBIT_PREVColumn.ColumnName] = Math.Abs(debitAmountPrevious);
                    drFooterDetails[reportSetting1.GENERALATE_REPORTS.CREDIT_PREVColumn.ColumnName] = Math.Abs(creditAmountPrevious);
                }
                else
                {
                    drFooterDetails[reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName] = Math.Abs(debitAmount);  
                    drFooterDetails[reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName] = Math.Abs(creditAmount);
                    drFooterDetails[reportSetting1.GENERALATE_REPORTS.DEBIT_PREVIOUSColumn.ColumnName] = Math.Abs(debitAmountPrevious);
                    drFooterDetails[reportSetting1.GENERALATE_REPORTS.CREDIT_PREVIOUSColumn.ColumnName] = Math.Abs(creditAmountPrevious);
                }

                drFooterDetails[reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName] = OpAmount;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName] = OpAmountPrevious;
                dtReportSource.Rows.Add(drFooterDetails);
            }
        }

        /// <summary>
        /// For Annual Statement Of Accounts
        /// To Attach Depreciation Amount to Gain and Loss and Balance sheet
        /// For GN report take current depreciation amount
        /// For Balancesheet take accumulated of depreciation amount
        /// </summary>
        /// <param name="dtReportSource"></param>
        private void AttachAnnualDepreciationAmount(DataTable dtBaseReportSource, DataTable dtReportSource, Int32 cashbankfdorder, bool isPLReport)
        {
            double CYdepreciationAmount = 0; double CYdepreciationOPAmount = 0;
            double PYdepreciationAmount = 0; double PYdepreciationOPAmount = 0;
            string condition = string.Empty;

            //For Depreciation ----------------------------------------------------------------
            dtBaseReportSource.DefaultView.RowFilter = string.Empty;
            
            //If you show depreciation in Liabilities side, show depreciation values in opposite
            if (isPLReport)
            {
                condition = "(" + reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn + " = 'G')";
            }
            else
            {
                condition = "(" + reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn + " = 'I')";
            }

            CYdepreciationAmount = UtilityMember.NumberSet.ToDouble(dtBaseReportSource.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName + ")", condition).ToString());
            PYdepreciationAmount = UtilityMember.NumberSet.ToDouble(dtBaseReportSource.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName + ")", condition).ToString());
            CYdepreciationOPAmount = UtilityMember.NumberSet.ToDouble(dtBaseReportSource.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNTColumn.ColumnName + ")", condition).ToString());
            PYdepreciationOPAmount = UtilityMember.NumberSet.ToDouble(dtBaseReportSource.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.OP_AMOUNT_PREVIOUSColumn.ColumnName + ")", condition).ToString());
            Natures depNature = (isPLReport ? Natures.Expenses : Natures.Libilities);
            
            if (depNature == Natures.Libilities)
            {
                InsertAnnualStatementDetailRow(dtReportSource, (int)depNature, 0, cashbankfdorder, 1, "G", "I/73-75", "ACCUMULATED DEPRECIATION", CYdepreciationAmount, PYdepreciationAmount);
            }
            else
            {
                InsertAnnualStatementDetailRow(dtReportSource, (int)depNature, 0, cashbankfdorder, 1, "G", "G/71", "DEPRECIATION & PROVISIONS", CYdepreciationAmount, PYdepreciationAmount);
            }
            
            dtBaseReportSource.DefaultView.RowFilter = string.Empty;
            //---------------------------------------------------------------------------------------------
        }

        
        /// <summary>
        /// For Annual Statement Of Accounts
        /// 
        /// Insert defined rows into repot datasource
        /// </summary>
        /// <param name="dtReportSource"></param>
        /// <param name="GroupId"></param>
        /// <param name="CashBankFDOrder"></param>
        /// <param name="LedgerCode"></param>
        /// <param name="LedgerName"></param>
        /// <param name="CurrentAmount"></param>
        /// <param name="PreviousAmount"></param>
        private void InsertAnnualStatementDetailRow(DataTable dtReportSource, Int32 natureid, Int32 GroupId, Int32 CashBankFDOrder, Int32 IsDepreciationLedger,
            string ConLedgerCode, string LedgerCode, string LedgerName, double CurrentAmount, double PreviousAmount)
        {
            if (dtReportSource != null)
            {
                DataRow drFooterDetails = dtReportSource.NewRow();
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.CON_NATURE_IDColumn.ColumnName] = natureid;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.NATURE_IDColumn.ColumnName] = natureid;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.GROUP_IDColumn.ColumnName] = GroupId;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.LEDGER_IDColumn.ColumnName] = 0;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.IS_CASH_BANK_FD_ORDERColumn.ColumnName] = CashBankFDOrder;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.IS_DEPRECIATION_LEDGERColumn.ColumnName] = IsDepreciationLedger;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName] = ConLedgerCode;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.LEDGER_CODEColumn.ColumnName] = LedgerCode;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.LEDGER_NAMEColumn.ColumnName] = LedgerName;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName] = CurrentAmount;
                drFooterDetails[reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName] = PreviousAmount;
                dtReportSource.Rows.Add(drFooterDetails);
            }
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
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
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
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }


            this.SetCurrencyFormat(xrHeaderAmountDebit.Text, xrHeaderAmountDebit);
            this.SetCurrencyFormat(xrHeaderAmountCredit.Text, xrHeaderAmountCredit);
            this.SetCurrencyFormat(xrHeaderDebitBalance.Text, xrHeaderDebitBalance);
            //this.SetCurrencyFormat(xrHeaderCurrentTransClosingCredit.Text, xrHeaderCurrentTransClosingCredit);

            return table;
        }

        private void MakeEmptyCell(BindingEventArgs ecell)
        {
            if (ecell.Value != null)
            {
                if (UtilityMember.NumberSet.ToDouble(ecell.Value.ToString()) == 0)
                {
                    ecell.Value = "";
                }
            }
        }

        private string GetDepreciationCaption()
        {
            string rtn = string.Empty;
            if (this.DataSource != null)
            {
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                if (conledgercode.ToUpper() == "G" || conledgercode.ToUpper() == "I")
                {
                    rtn = (conledgercode.ToUpper() == "G" ? "CURRENT YEAR" : (conledgercode.ToUpper() == "I" ? " ACCUMULATED" : string.Empty));
                }
            }
            return rtn;
        }

        #endregion
 
        #region Events
        
        private void xrFooterTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                string conledgername = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_NAMEColumn.ColumnName).ToString();

                if (conledgercode.ToUpper() == "G" || conledgercode.ToUpper() == "I")
                {
                    //string rtn = GetDepreciationCaption();
                    //conledgername = conledgername + (!string.IsNullOrEmpty(rtn) ? " (" + rtn + ")" : string.Empty);
                    string deprecaitionFooter = (conledgercode.ToUpper() == "G" ? "TOTAL DEPRECIATION (CURRENT YEAR)" : "DEPRECIATION ON FIXED ASSETS (ACCUMULATED)");
                    cell.Text = deprecaitionFooter;
                }
                else
                {
                    cell.Text = "TOTAL " + conledgercode + " - " + conledgername;
                }
            }
        }

        private void xrDebit_EvaluateBinding(object sender, BindingEventArgs e)
        {
            MakeEmptyCell(e);
        }

        private void xrCredit_EvaluateBinding(object sender, BindingEventArgs e)
        {
            MakeEmptyCell(e);
        }

        private void xrDebitBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            MakeEmptyCell(e);
        }

        private void xrCreditBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            MakeEmptyCell(e);
        }

        private void xrGrandTotal_PreviewDoubleClick(object sender, PreviewMouseEventArgs e)
        {
            //this.ExportToPdf(@"d:\p.pdf");
        }

        private void xrGrpDebitBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 30/01/2023, for Income and Expense , decide closing balance from Credit balance and Debit balance
            if (this.DataSource!=null)
            {
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                if (conledgercode.ToUpper() == "A" || conledgercode.ToUpper() == "B" ||
                    conledgercode.ToUpper() == "D01" || conledgercode.ToUpper() == "D02" ||
                    conledgercode.ToUpper() == "E" || conledgercode.ToUpper() == "F" ||
                    conledgercode.ToUpper() == "G" || conledgercode.ToUpper() == "I") 
                {
                    double ClosingBalance  = getClosingBalance(conledgercode);
                    //For Asset/Expenses
                    if (ClosingBalance > 0 )
                        e.Result = ClosingBalance;
                    else
                        e.Result = 0; 
                    e.Handled = true;
                }
            }
        }

        private void xrGrpCreditBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //On 30/01/2023, for Income and Expense , decide closing balance from Credit balance and Debit balance
            if (this.DataSource != null)
            {
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                if (conledgercode.ToUpper() == "A" || conledgercode.ToUpper() == "B" ||
                    conledgercode.ToUpper() == "D01" || conledgercode.ToUpper() == "D02" ||
                    conledgercode.ToUpper() == "E" || conledgercode.ToUpper() == "F" ||
                    conledgercode.ToUpper() == "G" || conledgercode.ToUpper() == "I") 
                {
                    double ClosingBalance = getClosingBalance(conledgercode);
                    if (ClosingBalance < 0) 
                        //&& (conledgercode.ToUpper() == "F" || conledgercode.ToUpper() == "C" || conledgercode.ToUpper() == "D02"))
                        e.Result = Math.Abs(ClosingBalance);
                    else
                        e.Result = 0;
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Hide empty rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            /// Hide ledgers which are having zero balance
            if (GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName) != null)
            {
                double debit= UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.DEBITColumn.ColumnName).ToString());
                double credit= UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CREDITColumn.ColumnName).ToString());
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                e.Cancel = (debit == 0 && credit == 0);
            }
        }

        private double getClosingBalance(string conledgercode )
        {
            double rtnClosingBalance = 0;
            if (this.DataSource!=null)
            {
                DataTable dtReport = this.DataSource as DataTable;
                string condition = "SUM(" + reportSetting1.GENERALATE_REPORTS.DEBIT_BALANCEColumn.ColumnName + ") - " +
                                    "SUM(" + reportSetting1.GENERALATE_REPORTS.CREDIT_BALANCEColumn.ColumnName + ")";
                rtnClosingBalance = UtilityMember.NumberSet.ToDouble(dtReport.Compute(condition, reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName + " = '" + conledgercode + "'").ToString());
            }
            return rtnClosingBalance;
        }

        private void xrConLedgerName_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                if (conledgercode.ToUpper() == "G" || conledgercode.ToUpper() == "I")
                {
                    string rtn = GetDepreciationCaption();
                    e.Value =  e.Value  + (!string.IsNullOrEmpty(rtn) ? " (" + rtn + ")" : string.Empty);
                }
            }
        }

        private void xrLedgerName_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                if (conledgercode.ToUpper() == "G" || conledgercode.ToUpper() == "I")
                {
                    string rtn = GetDepreciationCaption();
                    e.Value = e.Value + (!string.IsNullOrEmpty(rtn) ? " (" + rtn + ")" : string.Empty);
                }
            }
        }

        private void grpConLedgerHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null)
            {
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                GroupHeaderBand grpConledgerHeader = sender as GroupHeaderBand;
                if (conledgercode.ToUpper() == "F")
                {
                    //xrGrpHeaderRowConLedger.Padding = Padding.All;
                    grpConledgerHeader.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
                }
                else
                {
                    //xrGrpHeaderRowConLedger.Padding = Padding.Left | Padding.Right | Padding.Bottom;
                    grpConledgerHeader.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;
                }
            }
        }

        private void xrFooterTotalCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            /*if (GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                string conledgername = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_NAMEColumn.ColumnName).ToString();

                if (conledgercode.ToUpper() == "G" || conledgercode.ToUpper() == "I")
                {
                    //string rtn = GetDepreciationCaption();
                    //conledgername = conledgername + (!string.IsNullOrEmpty(rtn) ? " (" + rtn + ")" : string.Empty);
                    string deprecaitionFooter = (conledgercode.ToUpper() == "G" ? "G/71" : "I73-75");
                    cell.Text = deprecaitionFooter;
                }
                else
                {
                    cell.Text ="";
                }
            }*/
        }
                        
        #endregion
               
    }
}
