using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Report.Base;
using DevExpress.XtraPrinting;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.ReportObject
{
    public partial class AccountBalance : Report.Base.ReportBase
    {
        ResultArgs resultArgs = null;
        //03/10/2018, For CashBank book opening/closing balance should be splitted Cash--Bank
        //So we make use of progress coulmn with balance for the same date "from date"
        //and hide bank/fd coulmn in period column and make visible BAnk and FD in Progesss column
        //Period balance treat as cash, progess balance as treat as bank
        public bool IsCashBankBook { get; set; }

        public double PeriodBalanceAmount { get; set; }
        public double ProgressiveBalanceAmount { get; set; }
        private DataTable dtFetchACIBalance { get; set; }

        UserProperty settinguserProperty = new UserProperty(); // 02.09.2022

        //On 04/10/2018, show only selected group's (CASH/BANK/FD) detail
        //If general "show detail balance" is selected, will show all (CASH/BANK/FD) detail
        private string SelectedCashBanKFDGroup = string.Empty;

        public string FDLedgerHasOpening { get; set; }

        public float LeftPosition
        {
            set
            {
                xrTableLedgerGroup.LeftF = value;
                xrtblLedger.LeftF = value;
            }
        }
        public float CodeHeaderColumWidth
        {
            set
            {
                tcGroupCode.WidthF = value;
            }
        }

        public float CodeColumnWidth
        {
            set
            {
                tcLedgerCode.WidthF = value;
            }
        }
        public float NameHeaderColumWidth
        {
            set { tcGroupName.WidthF = value; }
        }
        public float NameColumnWidth
        {
            set
            {
                tcLedgerName.WidthF = value;
            }
        }
        public float AmountHeaderColumWidth
        {
            set { tcGroupAmountPeriod.WidthF = value; }
        }

        public bool GroupPeriodVisible
        {
            set { tcGroupAmountPeriod.Visible = value; }
        }

        public float AmountColumnWidth
        {
            set
            {
                tcAmountPeriod.WidthF = value;
            }
        }

        public float AmountProgressiveHeaderColumnWidth
        {
            set { tcGroupAmountProgress.WidthF = value; }
        }

        public float AmountProgressiveColumnWidth
        {
            set
            {
                tcAmountProgress.WidthF = value;
            }
        }

        public bool AmountProgressVisible
        {
            set
            {
                tcAmountProgress.Visible = value;
            }
        }

        public float GroupCode
        {
            set
            {
                tcGroupCode.WidthF = value;
            }
        }

        public float GroupNameWidth
        {
            set { tcGroupName.WidthF = value; }
        }

        public float GroupAmountWidth
        {
            set { tcGroupAmountPeriod.WidthF = value; }
        }

        public bool GroupProgressVisible
        {
            set { tcGroupAmountProgress.Visible = value; }
        }


        public bool LedgerCodeVisible
        {
            set
            {
                tcLedgerCode.Visible = value;
            }
        }

        public bool GroupCodeVisible
        {
            set
            {
                tcGroupCode.Visible = value;
            }
        }

        /// <summary>
        /// On 20/01/2021, For Cash and Bank Book
        /// </summary>
        public Font LedgerGroupNewFont
        {
            set
            {
                tcGroupCode.Font = tcGroupName.Font = tcGroupAmountPeriod.Font = tcGroupAmountProgress.Font = new Font(value.FontFamily, value.Size, value.Style);
            }
        }

        public Font LedgerNewFont
        {
            set
            {
                tcLedgerCode.Font = tcLedgerName.Font = tcAmountPeriod.Font = tcAmountProgress.Font = new Font(value.FontFamily, value.Size, value.Style);
            }
        }
        /// <summary>
        /// On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string BankClosedDate { get; set; }


        public AccountBalance()
        {
            InitializeComponent();

            ArrayList ledgerfilter = null;
            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                //On 18/02/2018, to show cash/bank/fd ledger details ---------------------------------------------------------
                ledgerfilter = new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName, reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, 
                reportSetting1.AccountBalance.IS_OPENING_BALANCEColumn.ColumnName, ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName};
            }
            else
            {
                ledgerfilter = new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName, reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, 
              reportSetting1.AccountBalance.IS_OPENING_BALANCEColumn.ColumnName};
            }

            DrillDownType ledgerdrilltype = DrillDownType.LEDGER_SUMMARY;
            this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName, ledgerfilter, ledgerdrilltype, false, "", true);

            //------------------------------------------------------------------------------------------------------------
            //else
            //{
            //    ArrayList ledgerfilter = new ArrayList { reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName, reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, 
            //    reportSetting1.AccountBalance.IS_OPENING_BALANCEColumn.ColumnName};
            //    DrillDownType ledgerdrilltype = DrillDownType.LEDGER_SUMMARY;
            //    this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName, ledgerfilter, ledgerdrilltype, false, "", true);
            //}
        }

        public override void ShowReport()
        {
            base.ShowReport();

            // 25/04/2025, *Chinna
            //  tcGroupCode.Visible = tcLedgerCode.Visible = false;
            // 19/04/2025, *Chinna
            if (this.settingProperty.IS_SAPPIC)
            {
                HideDetailedCashBankFDVisibility();
            }
        }

        public ResultArgs GetBalance(string balDate, string projectIds, string groupIds)
        {
            //On 27/09/2023, To attach Applicable date Range
            DateTime dFromApplicable = string.IsNullOrEmpty(BankClosedDate) ? settinguserProperty.FirstFYDateFrom :
                UtilityMember.DateSet.ToDate(BankClosedDate, false);
            DateTime dToApplicable = string.IsNullOrEmpty(ReportProperties.DateTo) ? settinguserProperty.LastFYDateTo :
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false);

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalance))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);

                //if (settinguserProperty.IS_SAPVIJ)
                //{
                //    dataManager.Parameters.Add(this.ReportParameters.SHOW_GENERALATEColumn, this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Standard) || this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Province) ? 0 : (int)ReportCodeType.Generalate);
                //}
                //else
                //{
                //    dataManager.Parameters.Add(this.ReportParameters.SHOW_GENERALATEColumn, 0);
                //}

                //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                if (!string.IsNullOrEmpty(BankClosedDate))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_CLOSEDColumn, BankClosedDate);
                }

                //On 27/09/2023, To attach Applicable date Range
                dataManager.Parameters.Add(this.ReportParameters.APPLICABLE_FROMColumn, dFromApplicable);
                dataManager.Parameters.Add(this.ReportParameters.APPLICABLE_TOColumn, dToApplicable);

                //On 26/11/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (settingProperty.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);

                //To skip default ledgers
                ReportProperty.Current.EnforceSkipDefaultLedgers(resultArgs, this.ReportParameters.LEDGER_IDColumn.ColumnName);
            }
            return resultArgs;
        }

        public void BindBalance(bool isOpBalance, bool isProgressive, bool IsReportAsOn = false)
        {
            string dateFrom = ReportProperties.DateFrom;
            string dateTo = ReportProperties.DateTo;
            SelectedCashBanKFDGroup = GetSelectedCashBankFDGroup();
            if (IsReportAsOn)
            {
                dateFrom = ReportProperties.DateAsOn;
                dateTo = ReportProperties.DateAsOn;
            }

            string balDate = "";
            string progressBalDate = GetProgressiveDate(dateFrom);
            string projectIds = ReportProperties.Project;
            string groupIds = this.GetLiquidityGroupIds();

            //03/10/2018, For CashBank book opening/closing balance should be splitted Cash--Bank
            //So we make use of progress coulmn with balance for the same date "from date"
            //and hide bank/fd coulmn in period column and make visible BAnk and FD in Progesss column
            if (IsCashBankBook)
            {
                //progressBalDate = (isOpBalance==true? ReportProperties.DateFrom:ReportProperties.DateTo);
                progressBalDate = ReportProperties.DateFrom;
                isProgressive = true;
            }

            //On 25/04/2018, to skip FD ledgers for Trail balance detail
            if (this.ReportProperties.ReportId == "RPT-030") //For Trail Balance
            {
                groupIds = GetLiquidityGroupIdsWithoutFD();
            }

            double amtPeriod = 0;
            double amtProgress = 0;
            double totAmtPeriod = 0;
            double totAmtProgress = 0;
            //int LedgerId = 0;

            //For few report, Dataason will be date to
            if (dateTo == "") { dateTo = ReportProperties.DateAsOn; }

            if (isOpBalance)
            {
                //For Opening Balance, Finding Balance Date
                DateTime dateBalance = DateTime.Parse(dateFrom).AddDays(-1);
                balDate = dateBalance.ToShortDateString();

                //For progress date, find progress op date
                if (isProgressive)
                {
                    DateTime dateProgBalance = DateTime.Parse(progressBalDate).AddDays(-1);
                    progressBalDate = dateProgBalance.ToShortDateString();
                }
                FDLedgerHasOpening = string.Empty;
            }
            else //Closing Date
            {
                balDate = dateTo;
                progressBalDate = balDate;
                dtFetchACIBalance = FetchACIBalance();
            }

            //Getting Balance amount for (Cash, Bank, FD) ledgers for Balance date
            resultArgs = GetBalance(balDate, projectIds, groupIds);
            DataTable dtBalance = resultArgs.DataSource.Table;
            DataView dvBalance = dtBalance.DefaultView;

            //For progress Date, get balance, 
            if (dvBalance != null && isProgressive)
            {
                dtBalance.Columns.Add(reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName, typeof(double));

                ResultArgs resultProgress = GetBalance(progressBalDate, projectIds, groupIds);
                DataTable dtProgressBalance = resultProgress.DataSource.Table;
                DataView dvProgressBalance = dtProgressBalance.DefaultView;
                string transMode = "";
                string transModeProgress = "";
                double progressAmt = 0;
                double ACIAmount = 0;

                foreach (DataRowView drvBalance in dvBalance)
                {
                    dvProgressBalance.RowFilter = reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName +
                           " = " + drvBalance[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName].ToString();

                    if (dvProgressBalance.Count > 0)
                    {
                        drvBalance.BeginEdit();
                        transMode = drvBalance[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                        transModeProgress = dvProgressBalance[0][reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                        progressAmt = UtilityMember.NumberSet.ToDouble(dvProgressBalance[0][reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                        //LedgerId = UtilityMember.NumberSet.ToInteger(dvProgressBalance[0][reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName].ToString());
                        ////Current Date transmode
                        //if (transMode == TransactionMode.CR.ToString())
                        //{
                        //    drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] =
                        //        -UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                        //}

                        //Progress Date transmode
                        if (transModeProgress == TransactionMode.CR.ToString())
                        {
                            progressAmt = -progressAmt;
                        }

                        if (this.settinguserProperty.IS_SAPPIC)
                        {
                            if (this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Generalate))
                            {
                                int GroupId = this.UtilityMember.NumberSet.ToInteger(dvProgressBalance[0][reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName].ToString());
                                string GeneralateCode = (GroupId == (int)FixedLedgerGroup.Cash) ? "001.001.001" : (GroupId == (int)FixedLedgerGroup.BankAccounts) ? "001.001.002" : "003.001.001";
                                drvBalance[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName] = GeneralateCode;
                                drvBalance[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName] = GeneralateCode;
                            }
                        }
                        drvBalance[reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName] = progressAmt;
                        drvBalance.EndEdit();
                    }

                    dvProgressBalance.RowFilter = "";
                }
            }

            //Calculate Sum of Balance, It will be used in grand toal or group Total
            foreach (DataRowView drvBalance in dvBalance)
            {
                amtPeriod = this.UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                string CurrentTransMode = drvBalance[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                if (CurrentTransMode == TransactionMode.CR.ToString())
                {
                    amtPeriod = -amtPeriod;
                }
                if (isProgressive)
                {
                    amtProgress = this.UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName].ToString());
                }

                if (this.settinguserProperty.IS_SAPPIC)
                {
                    if (this.ReportProperties.ReportCodeType.Equals((int)ReportCodeType.Generalate))
                    {
                        int GroupId = this.UtilityMember.NumberSet.ToInteger(drvBalance[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName].ToString());
                        string GeneralateCode = (GroupId == (int)FixedLedgerGroup.Cash) ? "001.001.001" : (GroupId == (int)FixedLedgerGroup.BankAccounts) ? "001.001.002" : "003.001.001";
                        drvBalance[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName] = GeneralateCode;
                        drvBalance[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName] = GeneralateCode;
                    }
                }

                //Update Amount to banalnce datable, if transmode CR
                drvBalance.BeginEdit();
                drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = amtPeriod;
                drvBalance.EndEdit();

                //On 07/02/2019, Skip hide zero balanced FD balance, but FD ledger in closing balance evern if it has zero balance if it has opening
                if (isOpBalance)
                {
                    Int32 grpid = UtilityMember.NumberSet.ToInteger(drvBalance[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName].ToString());
                    if (grpid == (int)FixedLedgerGroup.FixedDeposit)
                    {
                        Int32 fdledgerid = UtilityMember.NumberSet.ToInteger(drvBalance[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName].ToString());
                        double fdopeningamt = UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                        if (fdopeningamt != 0)
                        {
                            FDLedgerHasOpening = fdledgerid.ToString() + ",";
                        }
                    }
                }

                totAmtPeriod += amtPeriod;
                totAmtProgress += amtProgress;
            }

            PeriodBalanceAmount = totAmtPeriod;
            ProgressiveBalanceAmount = totAmtProgress;
            SetReportSetting(dvBalance, isProgressive);
            if (!string.IsNullOrEmpty(FDLedgerHasOpening))
            {
                FDLedgerHasOpening = FDLedgerHasOpening.TrimEnd(',');
            }
            if (dvBalance != null)
            {
                dvBalance.Table.TableName = "AccountBalance";

                //On 07/02/2019, Skip hide zero balanced FD balance, but FD ledger in closing balance evern if it has zero balance if it has opening
                // Include Cash/Bank balance will be shown even if it has zero balance
                string fdzerobalanedfilter = "(" + reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " = " + (int)FixedLedgerGroup.FixedDeposit +
                                             " AND " + reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + " <> 0)" +
                                             " OR (" + reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + " <> " + (int)FixedLedgerGroup.FixedDeposit + ")";
                //For closing balance, if fd ledger balance is zero but it has opening balance, have to show fd ledger
                if (isOpBalance == false && !string.IsNullOrEmpty(FDLedgerHasOpening))
                {
                    fdzerobalanedfilter += " OR (" + reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName + " IN (" + FDLedgerHasOpening + "))";
                }

                //On 07/02/2019, Skip hide zero balanced FD balance, but FD ledger in closing balance evern if it has zero balance if it has opening
                if (ReportProperties.ReportId == "RPT-027")
                {
                    dvBalance.RowFilter = fdzerobalanedfilter;
                }

                //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling, reportdatefrom, reportdateto will be changed based on its drilling
                DataColumn dcIsOpening = new DataColumn(reportSetting1.AccountBalance.IS_OPENING_BALANCEColumn.ColumnName, typeof(System.Int32));
                dcIsOpening.DefaultValue = isOpBalance;
                dvBalance.Table.Columns.Add(dcIsOpening);
                //----------------------------------------------

                //On 28/11/2024 - to set selected currency average rate for currenct FY
                if (settingProperty.AllowMultiCurrency == 1)
                {
                    DataColumn dccurrency = new DataColumn(ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName, typeof(System.Int32));
                    dccurrency.DefaultValue = (this.ReportProperties.CurrencyCountryId > 0) ? this.ReportProperties.CurrencyCountryId : 0;
                    dvBalance.Table.Columns.Add(dccurrency);
                }

                this.DataSource = dvBalance;
                this.DataMember = dvBalance.Table.TableName;
            }

            //  SetReportSetting();
        }

        private void SetReportSetting(DataView dvBalance, bool isProgressive)
        {
            float actualCodeWidth = tcGroupCode.WidthF;

            //Attach / Detach all ledgers
            dvBalance.RowFilter = "";

            // Show default opening balance for cash and bank even if there is no balance in cash and bank.
            // Comented by Arockia Raj on ( 27-06-2016 )

            //if (ReportProperties.IncludeAllLedger == 0)
            //{
            //    dvBalance.RowFilter = reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + " <> 0";
            //    // dvBalance.RowFilter = reportSetting1.AccountBalance.AMOUNTColumn.ColumnName + " >= 0";

            //    if (dvBalance.Table.Columns.Contains(reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName))
            //    {
            //        dvBalance.RowFilter += " OR " + reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName + " <> 0";
            //        // dvBalance.RowFilter += " OR " + reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName + " >= 0";
            //    }
            //}

            //Include / Exclude Code
            //On 11/05/2021 (To set ShowbyLedger values)
            //ReportProperties.ShowByLedger = ReportProperties.ShowDetailedBalance == 1 ? 1 : ReportProperties.ShowByLedger == 1 ? 1 : 0;

            if (tcGroupCode.Tag != null && tcGroupCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(tcGroupCode.Tag.ToString());
            }
            else
            {
                tcGroupCode.Tag = tcGroupCode.WidthF;
            }

            // 25/04/2205, *Chinna
            // 19/05/2025 *Chinna Modifications
            // Emergency

            if (!this.settingProperty.IS_SAPPIC)
            {
                tcGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            }
            else
            {
                tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
            }

            // tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            if (ReportProperties.ReportName == "Receipts and Payments")
            {
                tcGroupAmountProgress.WidthF = 0;
                tcGroupAmountProgress.Visible = false;

                tcAmountProgress.WidthF = 0;
                tcAmountProgress.Visible = false;
            }

            //Include / Exclude Progressive total Column
            tcGroupAmountProgress.Visible = (isProgressive == true);
            tcAmountProgress.Visible = (isProgressive == true);

            //Include / Exclude Ledger group or Ledger
            //On 11/05/2021 (To set ShowbyLedger values)
            //grpBalanceGroup.Visible = (ReportProperties.ShowByLedger == 1);
            grpBalanceGroup.Visible = (ReportProperties.ShowByLedger == 1 || ReportProperties.ShowDetailedBalance == 1);

            grpBalanceLedger.Visible = (ReportProperties.ShowDetailedBalance == 1 || SelectedCashBanKFDGroup != string.Empty);
            grpBalanceGroup.GroupFields[0].FieldName = "";
            grpBalanceLedger.GroupFields[0].FieldName = "";

            if (grpBalanceGroup.Visible == false && grpBalanceLedger.Visible == false)
            {
                grpBalanceGroup.Visible = true;
            }

            if (grpBalanceGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpBalanceGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
                }
                else
                {
                    grpBalanceGroup.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName;
                }
            }

            if (grpBalanceLedger.Visible)
            {
                if (ReportProperties.SortByLedger == 1)
                {
                    grpBalanceLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpBalanceLedger.GroupFields[0].FieldName = reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName;
                }
            }
            xrTableLedgerGroup = AlignGroupTable(xrTableLedgerGroup);
            xrtblLedger = AlignContentTable(xrtblLedger);

            // 19/05/2025, *Chinna
            if (this.settingProperty.IS_SAPPIC)
            {
                HideDetailedCashBankFDVisibility();
            }
        }


        public void HideDetailedCashBankFDVisibility()
        {
            bool conditionForTopGroupsOnly = (this.settingProperty.IS_SAPPIC &&
                                              this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate);

            float actualCodeWidth = tcGroupCode.WidthF;

            if (conditionForTopGroupsOnly)
            {
                grpBalanceLedger.Visible = false;

            }

            if (conditionForTopGroupsOnly)
            {
                tcLedgerCode.Visible = false;
                tcGroupCode.Visible = true;
                tcGroupCode.WidthF = actualCodeWidth;
            }
            else
            {
                tcGroupCode.Visible = false;
                tcLedgerCode.Visible = false;
                tcGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            }
        }
        //public void SetClosingBalanceWidth()
        //{
        //    tcGroupCode.WidthF = (float)52.06;
        //    tcGroupName.WidthF = (float)171.23;
        //    tcGroupAmountPeriod.WidthF = (float)136.99;
        //    tcGroupAmountProgress.WidthF = 0;
        //    tcGroupAmountProgress.Visible = false;
        //}

        //public void setClosingBalanceDetailWidth()
        //{
        //    //tcGroupCode.WidthF = (float)52.06;
        //    //tcGroupName.WidthF = (float)155.23;
        //    //tcGroupAmountPeriod.WidthF = (float)150.51;

        //    tcLedgerName.WidthF = (float)171.23;
        //    tcAmountPeriod.WidthF = (float)130.23;
        //    tcAmountProgress.WidthF = 0;
        //    tcAmountProgress.Visible = false;

        //    //tcGroupAmountProgress.Visible = false;
        //}

        //public void setCBCClosingBalanceDetailWidth()
        //{
        //    if (ReportProperties.ShowLedgerCode == 1)
        //    {
        //        tcLedgerCode.WidthF = (float)49.50;
        //        tcGroupName.WidthF = (float)330;
        //        tcLedgerName.WidthF = (float)162.00;
        //        tcAmountPeriod.WidthF = (float)168;
        //        tcAmountProgress.WidthF = 0.0f;
        //        tcAmountProgress.Visible = false;
        //        tcGroupAmountProgress.WidthF = 0.0f;
        //        tcGroupAmountProgress.Visible = false;
        //    }
        //    else
        //    {
        //        tcLedgerCode.WidthF = 0;
        //        tcGroupName.WidthF = (float)331;
        //        tcLedgerName.WidthF = (float)162.00 + (float)49.50;
        //        tcAmountPeriod.WidthF = (float)168;
        //        tcAmountProgress.WidthF = 0.0f;
        //        tcAmountProgress.Visible = false;
        //        tcGroupAmountProgress.WidthF = 0.0f;
        //        tcGroupAmountProgress.Visible = false;
        //    }

        //}

        private DataTable FetchACIBalance()
        {
            string FetchACIBalance = this.GetReportSQL(SQL.ReportSQLCommand.Report.FetchACIBalance);

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                //  resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FetchACIBalance);
            }
            return resultArgs.DataSource.Table;
        }

        private void tcGroupName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void tcGroupName_EvaluateBinding(object sender, BindingEventArgs e)
        {
            // #2. Done on 18/05/2017, Modified by Alwar for fixing Cash/Bank/FD Order in all reports. In XtraReport there is no option to Grouping on one field and sorting on another field
            // by default it takes accending order so it shows like Bank/Cash/Fixed Deposit. to resolve this issue, we have added spaces in query like ("  Cash", " Bank", "FD") and trim
            // and assing in display
            if (this.DataSource != null)
            {
                //XRTableCell cell = sender as XRTableCell;
                //cell.Text = cell.Text.Trim();
                e.Value = (e.Value != null ? e.Value.ToString().Trim() : string.Empty);
            }
        }

        private void tcGroupAmountPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //03/10/2018, For CashBank book opening balance should be splitted Cash--Bank
            //So we make use of progress coulmn with blancae for the same date from date
            if (IsCashBankBook)
            {
                Int32 GroupId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                if (GroupId != (int)FixedLedgerGroup.Cash)
                {
                    cell.Summary = new XRSummary(new SummaryRunning());
                    cell.Text = string.Empty;
                }
                else
                {
                    if (this.DataSource != null)
                    {
                        DataView dv = this.DataSource as DataView;
                        string amount = dv.Table.Compute("SUM(" + this.reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName + ")",
                                        this.reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + "=" + GroupId.ToString()).ToString();
                        cell.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(amount));
                    }
                }
            }
        }

        private void tcAmountPeriod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //03/10/2018, For CashBank book opening/closing balance should be splitted Cash--Bank
            //So we make use of progress coulmn with balance for the same date "from date"
            //and hide bank/fd coulmn in period column and make visible BAnk and FD in Progesss column
            if (IsCashBankBook)
            {
                Int32 GroupId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                if (GroupId != (int)FixedLedgerGroup.Cash)
                {
                    cell.Summary = new XRSummary(new SummaryRunning());
                    cell.Text = string.Empty;
                }
                else
                {
                    string amount = cell.Text;
                    cell.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(amount));
                }
            }
        }

        private void tcGroupAmountProgress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //03/10/2018, For CashBank book opening/closing balance should be splitted Cash--Bank
            //So we make use of progress coulmn with balance for the same date "from date"
            //and hide bank/fd coulmn in period column and make visible BAnk and FD in Progesss column
            if (IsCashBankBook)
            {
                Int32 GroupId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                if (GroupId == (int)FixedLedgerGroup.Cash)
                {
                    cell.Summary = new XRSummary(new SummaryRunning());
                    cell.Text = string.Empty;
                }
                else
                {
                    if (this.DataSource != null)
                    {
                        DataView dv = this.DataSource as DataView;
                        string amount = dv.Table.Compute("SUM(" + this.reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName + ")",
                                        this.reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName + "=" + GroupId.ToString()).ToString();
                        cell.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(amount));
                    }
                }
            }
        }

        private void tcAmountProgress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //03/10/2018, For CashBank book opening/closing balance should be splitted Cash--Bank
            //So we make use of progress coulmn with balance for the same date "from date"
            //and hide bank/fd coulmn in period column and make visible BAnk and FD in Progesss column
            if (IsCashBankBook)
            {
                Int32 GroupId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                XRTableCell cell = sender as XRTableCell;
                if (GroupId == (int)FixedLedgerGroup.Cash)
                {
                    cell.Summary = new XRSummary(new SummaryRunning());
                    cell.Text = string.Empty;
                }
                else
                {
                    string amount = cell.Text;
                    cell.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(amount));
                }
            }
        }

        private void xrTableRow8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //(sender as XRTableRow).Visible = true;
            //On 04/10/2018, show only selected group's (CASH/BANK/FD) detail
            //If general "show detail balance" is selected, will show all (CASH/BANK/FD) detail
            if (ReportProperties.ShowDetailedBalance == 0)
            {
                if (!string.IsNullOrEmpty(SelectedCashBanKFDGroup))
                {
                    Int32 GroupId = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName).ToString());
                    if (SelectedCashBanKFDGroup.IndexOf(GroupId.ToString()) >= 0)
                    {
                        (sender as XRTableRow).Visible = true;
                    }
                    else
                    {
                        (sender as XRTableRow).Visible = false;
                    }
                }
            }

        }

        /// <summary>
        /// On 04/10/2018, This method is used to get selected (Cash,Bank,FD) detail balance based on client selecton in Report Criteria.
        /// *** If general "show detail balance" is selected, it will return empty.
        /// </summary>
        /// <returns></returns>
        private string GetSelectedCashBankFDGroup()
        {
            string Rtn = string.Empty;
            if (ReportProperty.Current.ShowDetailedBalance == 0 &&
               (ReportProperty.Current.ShowDetailedCashBalance == 1 || ReportProperty.Current.ShowDetailedBankBalance == 1 || ReportProperty.Current.ShowDetailedFDBalance == 1))
            {
                Rtn += (ReportProperty.Current.ShowDetailedCashBalance == 1 ? ((int)FixedLedgerGroup.Cash).ToString() + "," : string.Empty);
                Rtn += (ReportProperty.Current.ShowDetailedBankBalance == 1 ? ((int)FixedLedgerGroup.BankAccounts).ToString() + "," : string.Empty);
                Rtn += (ReportProperty.Current.ShowDetailedFDBalance == 1 ? ((int)FixedLedgerGroup.FixedDeposit).ToString() + "," : string.Empty);
                Rtn = Rtn.TrimEnd(',');
            }
            return Rtn;
        }


    }
}
