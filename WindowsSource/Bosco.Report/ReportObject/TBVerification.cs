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
    public partial class TBVerification : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        string dtClosingDateRange = string.Empty;
        string dtClosingDateRangeYear = string.Empty;
        int GroupNumber = 0;
        double OPDebit = 0;
        double OPCredit = 0;
        double CurTransDebit = 0;
        double CurTransCredit = 0;
        double ClosingBalanceDebit = 0;
        double ClosingBalanceCredit = 0;
        double TotalCashClosingBalanceDebit = 0;
        double TotalCashClosingBalanceCredit = 0;
        double TotalBankClosingBalanceDebit = 0;
        double TotalBankClosingBalanceCredit = 0;
        double TotalFDClosingBalanceDebit = 0;
        double TotalFDClosingBalanceCredit = 0;
        double TotalOpeningCashAmount = 0;
        double TotalOpeningBankAmount = 0;
        double TotalOpeningFixedDeposit = 0;
        double AllTotalOpeningLedgerDebitBalance = 0;
        double AllTotalOpeningLedgerCreditBalance = 0;

        double SumCurTransDebit = 0;
        double SumCurTransCredit = 0;
        double SumofClDebit = 0;
        double SumofClCredit = 0;
        double SumClosingBalanceDebit = 0;
        double SumClosingBalanceCredit = 0;
        double IEDebit = 0;
        double IECredit = 0;
        double TotalOpeningCashBankFD = 0;
        double TotalOpeningCashBankFDCredit = 0;
        double OpCashDebit = 0;
        double OpCashCredit = 0;
        double OpBankDebit = 0;
        double OpBankCredit = 0;
        double OpFDDebit = 0;
        double OpFDCredit = 0;
        double IncomeAmt = 0;
        double ExpenceAmt = 0;
        int CurrentFinancialYear = 0;
        double IncomeExpenditureAmountPrevious = 0;
        string DateRangeDFReducing = string.Empty;
        string GroupCode = string.Empty;
        string PrevGroupCode = string.Empty;
        int GroupCodeNumber = 0;
        double GrpClosingDebit = 0.0;
        double GrpClosingCredit = 0.0;
        bool isCapLedgerCodeVisible;
        bool isCapGroupCodeVisible;

        double ExcessDebitAmount;
        double ExcessCreditAmount;

        private DataTable dtCCDetails = new DataTable();
        private bool PrevLedgerCCFound = false;
        #endregion

        #region Constructor
        public TBVerification()
        {
            InitializeComponent();

            // Show Transaction - Separate Table and Table Group to show the Current Transaction 

            //this.AttachDrillDownToRecord(xrTableLedger, xrTableCTransDebit,
            //        new ArrayList { reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false);
            //this.AttachDrillDownToRecord(xrTableLedger, xrTableCTransCredit,
            //       new ArrayList { reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName }, DrillDownType.LEDGER_SUMMARY_PAYMENTS, false);

            ////Main Ledger Closing
            //ArrayList groupfilter = new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, reportSetting1.ReportParameter.DATE_AS_ONColumn.ColumnName };
            //ArrayList ledgerfilter = new ArrayList { reportSetting1.BalanceSheet.LEDGER_IDColumn.ColumnName, reportSetting1.ReportParameter.DATE_AS_ONColumn.ColumnName };
            //DrillDownType ledgerdrilltype = DrillDownType.LEDGER_SUMMARY;

            //this.AttachDrillDownToRecord(xrTableLedger, xrTableCTransClosingDebit,
            //ledgerfilter, ledgerdrilltype, false, "", false);


        }
        #endregion

        #region Property
        string yearfrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearfrom = settingProperty.YearFrom;
                return yearfrom;
            }
        }
        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }
        DataTable dtAssignTrialBalance = null;
        public DataTable dtTrialBalance
        {
            get
            {
                return dtAssignTrialBalance;
            }
            set
            {
                dtAssignTrialBalance = value;
            }
        }


        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindGeneralateVerification();
        }
        #endregion

        #region Method
        private void BindGeneralateVerification()
        {
            try
            {
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
                this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;

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

                isCapLedgerCodeVisible = (ReportProperties.ShowLedgerCode == 1);
                isCapGroupCodeVisible = (ReportProperties.ShowGroupCode == 1);
                Detail.SortFields.Add(new GroupField("LEDGER_CODE"));
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private ResultArgs GetGeneralateVerificationSource()
        {
            string TrialBalance = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.GeneralateVerification);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.FIRST_FY_DATE_FROMColumn, this.settingProperty.FirstFYDateFrom);

                dataManager.Parameters.Add(this.ReportParameters.PREVIOUS_FY_DATE_FROMColumn, UtilityMember.DateSet.ToDate(this.settingProperty.YearFrom, false).AddYears(-1));
                dataManager.Parameters.Add(this.ReportParameters.PREVIOUS_FY_DATE_TOColumn, UtilityMember.DateSet.ToDate(this.settingProperty.YearTo, false).AddYears(-1));

                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                //dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.settingProperty.YearFrom);
                //dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.settingProperty.YearTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, TrialBalance);

                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.TableView.Table != null)
                {
                    DataTable dtVerificaiton = resultArgs.DataSource.TableView.Table;

                    //02/07/2024 - To define automatic rows -----------------------------------------------------------------
                    DataColumn dc = new DataColumn("AUTOMATIC_ROW", typeof(System.Int32));
                    dc.DefaultValue = 0;
                    if (!dtVerificaiton.Columns.Contains(dc.ColumnName))
                    {
                        dtVerificaiton.Columns.Add(dc);
                    }
                    //----------------------------------------------------------------------------------------------------------

                    dtVerificaiton = GroupCashBankFD(dtVerificaiton);
                    AttachProcessedValues(dtVerificaiton);

                    dtVerificaiton.DefaultView.RowFilter = "DEBIT <> 0 OR CREDIT<>0";
                    dtVerificaiton = dtVerificaiton.DefaultView.ToTable();

                    //On 01/10/2024, To Group IE Ledgers -------------------------
                    GroupIELedgers(dtVerificaiton);
                    //------------------------------------------------------------

                    //For Closing Balance
                    string closingbalanceexpression = "IIF( (DEBIT - CREDIT < 0), (CREDIT - DEBIT),  (DEBIT - CREDIT))";

                    dtVerificaiton.Columns.Add(this.reportSetting1.TrialBalance.BALANCEColumn.ColumnName,
                            this.reportSetting1.TrialBalance.BALANCEColumn.DataType,
                           "IIF(" + closingbalanceexpression + " < 0, " + closingbalanceexpression + "," + closingbalanceexpression + ")");

                    dtVerificaiton.DefaultView.Sort = "LEDGER_CODE";
                    dtVerificaiton = dtVerificaiton.DefaultView.ToTable();
                    resultArgs.DataSource.Data = dtVerificaiton;

                    AssignCCDetailReportSource();
                }

            }
            return resultArgs;
        }

        /// <summary>
        /// Group Cash, Bank and FD ledgers
        /// </summary>
        /// <param name="dtVerificaiton"></param>
        private DataTable GroupCashBankFD(DataTable dtVerificaiton)
        {
            string fileter = this.reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.Cash;
            double cashamtDr = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.DEBITColumn.ColumnName + ")", fileter).ToString());
            double cashamtCr = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.CREDITColumn.ColumnName + ")", fileter).ToString());

            fileter = this.reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.BankAccounts;
            double bankamtDr = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.DEBITColumn.ColumnName + ")", fileter).ToString()); ;
            double bankamtCr = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.CREDITColumn.ColumnName + ")", fileter).ToString());

            fileter = this.reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit;
            double fdamtDr = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.DEBITColumn.ColumnName + ")", fileter).ToString()); ;
            double fdamtCr = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.CREDITColumn.ColumnName + ")", fileter).ToString());

            string groupIds = this.GetLiquidityGroupIds();
            dtVerificaiton.DefaultView.RowFilter = this.reportSetting1.BUDGET_LEDGER.GROUP_IDColumn.ColumnName + " NOT IN (" + groupIds + ")"; ;
            dtVerificaiton = dtVerificaiton.DefaultView.ToTable();
            addCashBankFDItem(dtVerificaiton, FixedLedgerGroup.Cash, cashamtCr, cashamtDr);
            addCashBankFDItem(dtVerificaiton, FixedLedgerGroup.BankAccounts, bankamtCr, bankamtDr);
            addCashBankFDItem(dtVerificaiton, FixedLedgerGroup.FixedDeposit, fdamtCr, fdamtDr);
            return dtVerificaiton;
        }

        private void AttachProcessedValues(DataTable dtVerificaiton)
        {
            string filter = string.Empty;
            string calculation = string.Empty;
            double previousYearIEBalance = 0;
            //double IEBalance = 0;
            double netPatrimony = 0;

            //On 03/07/2024, For Depreciation ledger lets is show as accumulated liability style
            //Show debit values to credit side as well as credit values to debit side
            double automaticAgaistDeprecation = 0;
            filter = "CON_LEDGER_CODE='G'";
            dtVerificaiton.DefaultView.RowFilter = filter;
            foreach (DataRowView drv in dtVerificaiton.DefaultView)
            {
                double opamount = UtilityMember.NumberSet.ToDouble(drv["GN_OP_BALANCE"].ToString());
                double drPrevious = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.TrialBalance.DEBIT_PREVIOUSColumn.ColumnName].ToString());
                double crPrevious = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.TrialBalance.CREDIT_PREVIOUSColumn.ColumnName].ToString());
                double dr = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.TrialBalance.DEBITColumn.ColumnName].ToString());
                double cr = UtilityMember.NumberSet.ToDouble(drv[reportSetting1.TrialBalance.CREDITColumn.ColumnName].ToString());
                drv.BeginEdit();
                drv[reportSetting1.TrialBalance.DEBIT_PREVIOUSColumn.ColumnName] = crPrevious;
                drv[reportSetting1.TrialBalance.CREDIT_PREVIOUSColumn.ColumnName] = drPrevious;
                drv[reportSetting1.TrialBalance.DEBITColumn.ColumnName] = cr;
                drv[reportSetting1.TrialBalance.CREDITColumn.ColumnName] = dr;
                drv.EndEdit();
                automaticAgaistDeprecation += ((cr + dr) - opamount);
            }
            dtVerificaiton.DefaultView.RowFilter = string.Empty;

            // // 27/01/2025, chinna
            if (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToShortDateString() == UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString()))
            {
                //07/01/2025 - attach deprecaition skining agaist amount
                if (automaticAgaistDeprecation > 0)
                    addNewRow(dtVerificaiton, 0, 0, 0, "71-G", "Depreciation", "71-G", "Depreciation", 0, automaticAgaistDeprecation, false);
                else
                    addNewRow(dtVerificaiton, 0, 0, 0, "71-G", "Depreciation", "71-G", "Depreciation", automaticAgaistDeprecation, 0, false);
            }

            filter = "NATURE_ID IN (1,2)";
            //calculation = "SUM(" + reportSetting1.TrialBalance.DEBITColumn.ColumnName + ") - " +
            //              "SUM(" + reportSetting1.TrialBalance.CREDITColumn.ColumnName + ")";
            //IEBalance = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute(calculation, filter).ToString());

            calculation = "SUM(" + reportSetting1.TrialBalance.CREDIT_PREVIOUSColumn.ColumnName + ") - " +
                          "SUM(" + reportSetting1.TrialBalance.DEBIT_PREVIOUSColumn.ColumnName + ")";
            previousYearIEBalance = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute(calculation, filter).ToString());

            // 27/01/2025, chinna
            if (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToShortDateString() != UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString()))
            {

                previousYearIEBalance = previousYearIEBalance - automaticAgaistDeprecation;
            }

            //70 - Profit or Loss of the previous year
            if (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToShortDateString() == UtilityMember.DateSet.ToDate(settingProperty.FirstFYDateFrom.ToShortDateString())
                    && this.AppSetting.GeneralateOpeningIEBalance > 0)
            {
                //double amt = this.AppSetting.GeneralateOpeningIEBalance;
                previousYearIEBalance = this.AppSetting.GeneralateOpeningIEBalance * (this.AppSetting.GeneralateOpeningIEBalanceMode == 1 ? -1 : 1);
            }
            addNewRow(dtVerificaiton, 0, 0, 0, "70", "Profit or Loss of the previous year", "70", "Profit or Loss of the previous year", Math.Abs(previousYearIEBalance), Math.Abs(previousYearIEBalance), true);

            //72 - Profit or Loss of the previous year
            /*if (IEBalance>0 )
                addNewRow(dtVerificaiton, 0, 0, 0, "72", "Profit or Loss", "72", "Profit or Loss", 0, IEBalance);
            else
                addNewRow(dtVerificaiton, 0, 0, 0, "72", "Profit or Loss", "72", "Profit or Loss", Math.Abs(IEBalance), 0);
            */

            //On 07/01/2025 - If not first fy, let us affect previous fy
            //if (this.ReportProperties.DateFrom != settingProperty.FirstFYDateFrom.ToShortDateString())
            //{
            if (previousYearIEBalance > 0)
                addNewRow(dtVerificaiton, 0, 0, 0, "72", "Profit or Loss", "72", "Profit or Loss", Math.Abs(previousYearIEBalance), 0, true);
            else
                addNewRow(dtVerificaiton, 0, 0, 0, "72", "Profit or Loss", "72", "Profit or Loss", 0, Math.Abs(previousYearIEBalance), true);
            //}

            //76 - Net Patrimony
            filter = "AUTOMATIC_ROW=0";
            calculation = "SUM(" + reportSetting1.TrialBalance.DEBITColumn.ColumnName + ") - " +
                          "SUM(" + reportSetting1.TrialBalance.CREDITColumn.ColumnName + ")";
            netPatrimony = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute(calculation, filter).ToString());

            //On 07/01/2025 - If not first fy, let us affect previous fy
            //if (this.ReportProperties.DateFrom != settingProperty.FirstFYDateFrom.ToShortDateString())
            //{
            netPatrimony -= previousYearIEBalance;
            //}
            if (netPatrimony > 0)
                addNewRow(dtVerificaiton, 0, 0, 0, "76", "Net Patrimony", "76", "Net Patrimony", netPatrimony, 0, true);
            else
                addNewRow(dtVerificaiton, 0, 0, 0, "76", "Net Patrimony", "76", "Net Patrimony", 0, Math.Abs(netPatrimony), true);

        }

        /// <summary>
        /// To Group IE Ledgers
        /// </summary>
        /// <param name="dtVerificaiton"></param>
        private void GroupIELedgers(DataTable dtVerificaiton)
        {
            string filter = string.Empty;
            string startledgercode = string.Empty;
            string endledgercode = string.Empty;
            double debit, credit = 0;
            //double debitprevious, creditprevious = 0;

            if (ReportProperties.IncludeAllLedger == 0 && dtVerificaiton != null)
            {
                filter = "(" + reportSetting1.CongiregationProfitandLoss.CON_LEDGER_CODEColumn.ColumnName + " = '' OR " +
                           reportSetting1.Ledger.NATURE_IDColumn.ColumnName + " NOT IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + "))";

                //For Income Ledgers
                filter = "(" + reportSetting1.CongiregationProfitandLoss.CON_LEDGER_CODEColumn.ColumnName + " = '' OR " +
                           reportSetting1.Ledger.NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Income + "))";
                startledgercode = dtVerificaiton.Compute("MIN(" + reportSetting1.TrialBalance.LEDGER_CODEColumn.ColumnName + ")", filter).ToString().ToString();
                endledgercode = dtVerificaiton.Compute("MAX(" + reportSetting1.TrialBalance.LEDGER_CODEColumn.ColumnName + ")", filter).ToString().ToString();
                debit = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.DEBITColumn.ColumnName + ")", filter).ToString());
                credit = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.CREDITColumn.ColumnName + ")", filter).ToString());

                string IECode = (startledgercode + (!string.IsNullOrEmpty(startledgercode) && !string.IsNullOrEmpty(endledgercode) ? "-" : string.Empty) + endledgercode);
                addNewRow(dtVerificaiton, 0, 0, 0, "", "Income", IECode, "Total Income Ledgers", credit, debit, true);

                //For Expense Ledgers
                filter = "(" + reportSetting1.CongiregationProfitandLoss.CON_LEDGER_CODEColumn.ColumnName + "= '' OR " +
                           reportSetting1.Ledger.NATURE_IDColumn.ColumnName + " IN (" + (int)Natures.Expenses + "))";
                startledgercode = dtVerificaiton.Compute("MIN(" + reportSetting1.TrialBalance.LEDGER_CODEColumn.ColumnName + ")", filter).ToString().ToString();
                endledgercode = dtVerificaiton.Compute("MAX(" + reportSetting1.TrialBalance.LEDGER_CODEColumn.ColumnName + ")", filter).ToString().ToString();

                debit = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.DEBITColumn.ColumnName + ")", filter).ToString());
                credit = UtilityMember.NumberSet.ToDouble(dtVerificaiton.Compute("SUM(" + reportSetting1.TrialBalance.CREDITColumn.ColumnName + ")", filter).ToString());
                IECode = (startledgercode + (!string.IsNullOrEmpty(startledgercode) && !string.IsNullOrEmpty(endledgercode) ? "-" : string.Empty) + endledgercode);
                addNewRow(dtVerificaiton, 0, 0, 0, "", "Expenses", IECode, "Total Expenses Ledgers", credit, debit, true);

                dtVerificaiton.DefaultView.RowFilter = "(" + reportSetting1.CongiregationProfitandLoss.CON_LEDGER_CODEColumn.ColumnName + " <>'' OR " +
                        reportSetting1.Ledger.NATURE_IDColumn.ColumnName + " NOT IN (" + (int)Natures.Income + "," + (int)Natures.Expenses + "))";
                dtVerificaiton = dtVerificaiton.DefaultView.ToTable();
            }

        }

        private void addCashBankFDItem(DataTable dtVerification, FixedLedgerGroup fixedledgergroup, double crCurrAmt, double drCurrAmt)
        {
            Int32 ledgerid = 0;
            Int32 groupid = 0;
            Int32 natureid = (int)Natures.Assert;
            string ledgercode = string.Empty;
            string ledgername = string.Empty;
            string ledgergroupcode = string.Empty;
            string ledgergroupname = string.Empty;

            switch (fixedledgergroup)
            {
                case FixedLedgerGroup.Cash:
                    ledgerid = 0;
                    groupid = (int)FixedLedgerGroup.Cash;
                    ledgercode = "01";
                    ledgername = "Cash";
                    ledgergroupcode = "01";
                    ledgergroupname = "Cash in Hand";
                    break;
                case FixedLedgerGroup.BankAccounts:
                    ledgerid = 0;
                    groupid = (int)FixedLedgerGroup.BankAccounts;
                    ledgercode = "02";
                    ledgername = "Bank";
                    ledgergroupcode = "02";
                    ledgergroupname = "Bank";
                    break;
                case FixedLedgerGroup.FixedDeposit:
                    ledgerid = 0;
                    groupid = (int)FixedLedgerGroup.FixedDeposit;
                    ledgercode = "02.A";
                    ledgername = "Fixed Deposits";
                    ledgergroupcode = "02";
                    ledgergroupname = "Fixed Deposits";
                    break;
            }
            addNewRow(dtVerification, groupid, ledgerid, natureid, ledgergroupcode, ledgergroupname, ledgercode, ledgername, crCurrAmt, drCurrAmt, false);
        }

        private void addNewRow(DataTable dtVerification, Int32 groupid, Int32 ledgerid, Int32 natureid, string ledgergroupcode, string ledgergroupname, string ledgercode,
                                    string ledgername, double crCurrAmt, double drCurrAmt, bool isAutomaticRow)
        {
            DataRow dr = dtVerification.NewRow();
            dr["GROUP_ID"] = groupid;
            dr[reportSetting1.TrialBalance.LEDGER_IDColumn.ColumnName] = ledgerid;
            dr[reportSetting1.TrialBalance.NATURE_IDColumn.ColumnName] = natureid;
            dr["GROUP_CODE"] = natureid;
            dr[reportSetting1.TrialBalance.LEDGER_GROUPColumn.ColumnName] = ledgergroupname;
            dr[reportSetting1.TrialBalance.LEDGER_CODEColumn.ColumnName] = ledgercode;
            dr[reportSetting1.TrialBalance.LEDGER_NAMEColumn.ColumnName] = ledgername;
            dr[reportSetting1.TrialBalance.DEBITColumn.ColumnName] = drCurrAmt;
            dr[reportSetting1.TrialBalance.CREDITColumn.ColumnName] = crCurrAmt;
            dr["AUTOMATIC_ROW"] = isAutomaticRow;

            dtVerification.Rows.Add(dr);
        }

        private void BindProperty()
        {
            ResultArgs resultArgs = GetGeneralateVerificationSource();
            if (resultArgs.Success)
            {
                dtTrialBalance = resultArgs.DataSource.Table;

                if (dtTrialBalance != null)
                {
                    dtTrialBalance.TableName = "TrialBalance";
                    this.DataSource = dtTrialBalance;
                    this.DataMember = dtTrialBalance.TableName;
                }
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


            this.SetCurrencyFormat(xrHeaderCurrentTransDebit.Text, xrHeaderCurrentTransDebit);
            this.SetCurrencyFormat(xrHeaderCurrentTransCredit.Text, xrHeaderCurrentTransCredit);
            this.SetCurrencyFormat(xrHeaderCurrentTransClosingDebit.Text, xrHeaderCurrentTransClosingDebit);
            return table;
        }

        /// <summary>
        /// On 06/10/2021, to get CC deatils for given project and date range
        /// </summary>
        private void AssignCCDetailReportSource()
        {
            ResultArgs resultArgs = null;
            string sqlccDetail = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CCDetail);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                //On 09/12/2024, To set Currnecy
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlccDetail);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtCCDetails = resultArgs.DataSource.Table;

                //dtCCDetails.Columns[reportSetting1.Ledger.AMOUNT_PERIODColumn.ColumnName].ColumnName = reportSetting1.Ledger.DEBITColumn.ColumnName;

            }
        }

        private void ShowCCDetails()
        {
            //On 06/10/2021, To show CC detail for given Ledger
            if (this.ReportProperties.ShowCCDetails == 1)
            {
                xrSubreportCCDetails.Visible = false;

                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null && dtCCDetails.Rows.Count > 0)
                {
                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName).ToString());
                    UcCCDetail ccDetail = xrSubreportCCDetails.ReportSource as UcCCDetail;
                    dtCCDetails.DefaultView.RowFilter = string.Empty;
                    dtCCDetails.DefaultView.RowFilter = reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName + " = " + ledgerid;
                    DataTable dtCC = dtCCDetails.DefaultView.ToTable();

                    ccDetail.BindCCDetails(dtCC, true, false, false, true); //ccDetail.BindCCDetails(dtCC, true);
                    ccDetail.DateWidth = 0;
                    float ccnamewidth = xrCurrentLedgerName.WidthF;
                    float ccdebitwidth = xrTableCTransDebit.WidthF;
                    float cccreditwidth = xrTableCTransCredit.WidthF;

                    xrSubreportCCDetails.TopF = xrTableLedger.HeightF;
                    xrSubreportCCDetails.LeftF = xrCurrentLedgerCode.WidthF;  // (this.ReportProperties.ShowOpeningBalance == 1 ? xrTblClLedgerCode.WidthF : xrCurrentLedgerCode.WidthF); //-2;
                    ccDetail.CCCreditCaption = "";
                    ccDetail.CCDebitCaption = "";
                    ccDetail.CCTableWidth = (ccnamewidth + ccdebitwidth + cccreditwidth);
                    ccDetail.CCNameWidth = ccnamewidth;
                    ccDetail.CCDebitWidth = ccdebitwidth;
                    ccDetail.CCCreditWidth = cccreditwidth;

                    ccDetail.PRojectNameWidth = (ccnamewidth + ccdebitwidth + cccreditwidth);
                    ccDetail.HideReportHeaderFooter();
                    dtCCDetails.DefaultView.RowFilter = string.Empty;

                    ProperBorderForLedgerRow(PrevLedgerCCFound, false);
                    xrSubreportCCDetails.Visible = (dtCC.Rows.Count > 0);
                    PrevLedgerCCFound = (dtCC.Rows.Count > 0);
                }
                else
                {
                    xrSubreportCCDetails.Visible = false;
                    ProperBorderForLedgerRow(false, false);
                    PrevLedgerCCFound = false;
                }
            }
            else
            {
                xrSubreportCCDetails.Visible = false;
                PrevLedgerCCFound = false;
            }

            if (!xrSubreportCCDetails.Visible)
            {
                if (Detail.Controls.Contains(xrSubreportCCDetails))
                {
                    Detail.Controls.Remove(xrSubreportCCDetails);
                }
            }
            else
            {
                Detail.Controls.Add(xrSubreportCCDetails);
            }
        }

        private void ProperBorderForLedgerRow(bool ccFound, bool ccDonorFound)
        {
            if (ccFound || ccDonorFound)
            {
                xrCurrentLedgerCode.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCurrentLedgerName.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrTableCTransDebit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                xrTableCTransCredit.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
            }
            else
            {
                xrCurrentLedgerCode.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrCurrentLedgerName.Borders = BorderSide.Right | BorderSide.Bottom;
                xrTableCTransDebit.Borders = BorderSide.Right | BorderSide.Bottom;
                xrTableCTransCredit.Borders = BorderSide.Right | BorderSide.Bottom;
            }
        }

        #endregion

        private void detailCC_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ShowCCDetails();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ShowCCDetails();
            Detail.HeightF = 25;
        }

        #region Events

        #endregion

    }
}
