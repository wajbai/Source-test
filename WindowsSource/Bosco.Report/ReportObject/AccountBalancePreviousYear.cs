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
    public partial class AccountBalancePreviousYear : Report.Base.ReportBase
    {
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        public double PeriodBalanceAmount { get; set; }
        public double PreviousBalanceAmount { get; set; }
        public double ProgressiveBalanceAmount { get; set; }
        
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
                tcGroupCode.WidthF = value;}
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
            set {
                tcGroupAmountPeriodPrevious.WidthF = value;
                tcGroupAmountPeriod.WidthF = value;
            }
        }

        public bool GroupPeriodVisible
        {
            set { tcGroupAmountPeriod.Visible = value; }
        }

        public float AmountColumnWidth
        {
            set
            {
                tcAmountPeriodPrevious.WidthF = value;
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
            set { tcGroupAmountPeriod.WidthF = value;
                  tcGroupAmountPeriodPrevious.WidthF = value;
            }
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

        private Bosco.Utility.ConfigSetting.SettingProperty AppSetting
        {
            get { return new Bosco.Utility.ConfigSetting.SettingProperty(); }
        }

        /// <summary>
        /// On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
        /// 
        /// This date is apart from balance date. If we take reports for given period (DATE_FROM and DATE_TO)
        /// When we show closing balance for DATE_TO, we have to check bank ledger closed date for DATE_FROM
        /// </summary>
        public string BankClosedDate { get; set; }


        public AccountBalancePreviousYear()
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
            this.AttachDrillDownToRecord(xrtblLedger, tcLedgerName, ledgerfilter, ledgerdrilltype, false, "",true);
        }

        public override void ShowReport()
        {
            base.ShowReport();
        }

        public ResultArgs GetBalance(string balDate, string projectIds, string groupIds)
        {
            DataTable dtBalance = new DataTable();

            //On 28/09/2023, To attach Applicable date Range
            DateTime dFromApplicable = string.IsNullOrEmpty(BankClosedDate) ? this.settingProperty.FirstFYDateFrom :
                UtilityMember.DateSet.ToDate(BankClosedDate, false);
            DateTime dToApplicable = string.IsNullOrEmpty(ReportProperties.DateTo) ? settingProperty.LastFYDateTo :
                UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false);

            //1. Current year balances
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchBalance))
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, projectIds);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, groupIds);
                dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, balDate);
                
                //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                if (!string.IsNullOrEmpty(BankClosedDate))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_CLOSEDColumn, BankClosedDate);
                }

                //On 28/09/2023, To attach Applicable date Range
                dataManager.Parameters.Add(this.ReportParameters.APPLICABLE_FROMColumn, dFromApplicable);
                dataManager.Parameters.Add(this.ReportParameters.APPLICABLE_TOColumn, dToApplicable);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        public void BindBalance(bool isOpBalance, bool isProgressive)
        {
            string dateFrom = ReportProperties.DateFrom;
            string dateTo = ReportProperties.DateTo;
            string balDate = "";
            string progressBalDate = GetProgressiveDate(dateFrom);
            string projectIds = ReportProperties.Project;
            string groupIds = this.GetLiquidityGroupIds();

            double amtPeriod = 0;
            double amtPrevious = 0;
            double amtProgress = 0;
            double totAmtPeriod = 0;
            double totAmtPrevious = 0;
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
            }
            else //Closing Date
            {
                balDate = dateTo;
                progressBalDate = balDate;
            }

            //Getting Balance amount for (Cash, Bank, FD) ledgers for Balance date
            resultArgs = GetBalance(balDate, projectIds, groupIds);
            DataTable dtBalance = resultArgs.DataSource.Table;
            AttachPreviousYearBalance(dtBalance, projectIds, groupIds, isOpBalance);
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
                        
                        //Progress Date transmode
                        if (transModeProgress == TransactionMode.CR.ToString())
                        {
                            progressAmt = -progressAmt;
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
                amtPrevious = this.UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNT_PREVIOUS_YEARColumn.ColumnName].ToString());
                if (isProgressive)
                {
                    amtProgress = this.UtilityMember.NumberSet.ToDouble(drvBalance[reportSetting1.AccountBalance.AMOUNT_PROGRESSColumn.ColumnName].ToString());
                }

                //Update Amount to banalnce datable, if transmode CR
                drvBalance.BeginEdit();
                drvBalance[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = amtPeriod;
                drvBalance[reportSetting1.AccountBalance.AMOUNT_PREVIOUS_YEARColumn.ColumnName] = amtPrevious;
                drvBalance.EndEdit();

                totAmtPeriod += amtPeriod;
                totAmtPrevious += amtPrevious;
                totAmtProgress += amtProgress;
            }

            PeriodBalanceAmount = totAmtPeriod;
            ProgressiveBalanceAmount = totAmtProgress;
            PreviousBalanceAmount = totAmtPrevious;
            SetReportSetting(dvBalance, isProgressive);

            //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling, reportdatefrom, reportdateto will be changed based on its drilling
            DataColumn dcIsOpening = new DataColumn(reportSetting1.AccountBalance.IS_OPENING_BALANCEColumn.ColumnName, typeof(System.Int32));
            dcIsOpening.DefaultValue = isOpBalance;
            dvBalance.Table.Columns.Add(dcIsOpening);
            //----------------------------------------------

            if (dvBalance != null)
            {
                dvBalance.Table.TableName = "AccountBalance";
                this.DataSource = dvBalance;
                this.DataMember = dvBalance.Table.TableName;
            }
        }
            
        private void SetReportSetting(DataView dvBalance, bool isProgressive)
        {
            float actualCodeWidth = tcGroupCode.WidthF;

            //Attach / Detach all ledgers
            dvBalance.RowFilter = "";

            //Include / Exclude Code
            ReportProperties.ShowByLedger = ReportProperties.ShowDetailedBalance == 1 ? 1 : ReportProperties.ShowByLedger == 1 ? 1 : 0;
            if (tcGroupCode.Tag != null && tcGroupCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(tcGroupCode.Tag.ToString());
            }
            else
            {
                tcGroupCode.Tag = tcGroupCode.WidthF;
            }

            tcGroupCode.WidthF = ((ReportProperties.ShowGroupCode == 1) ? actualCodeWidth : 0);
            tcLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
                        
            //Include / Exclude Progressive total Column
            tcGroupAmountProgress.Visible = (isProgressive == true);
            tcAmountProgress.Visible = (isProgressive == true);

            //Include / Exclude Ledger group or Ledger
            grpBalanceGroup.Visible = (ReportProperties.ShowByLedger == 1);
            grpBalanceLedger.Visible = (ReportProperties.ShowDetailedBalance == 1);
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
        }

        private void tcGroupName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //// #2. Done on 18/05/2017, Modified by Alwar for fixing Cash/Bank/FD Order in all reports. In XtraReport there is no option to Grouping on one field and sorting on another field
            //// by default it takes accending order so it shows like Bank/Cash/Fixed Deposit. to resolve this issue, we have added spaces in query like ("  Cash", " Bank", "FD") and trim
            //// and assing in display
            //XRTableCell cell = sender as XRTableCell;
            //cell.Text = cell.Text.Trim(); 
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

        //Attach Previous Year balance for given date, If ledger (cash/bank/fd ledgers are already exists update it or add new )
        private void AttachPreviousYearBalance(DataTable dt, string projectIds, string groupIds, bool IsOB)
        {
            //1. Add previous year column
            DataColumn dcAmountPrevious = new DataColumn(reportSetting1.AccountBalance.AMOUNT_PREVIOUS_YEARColumn.ColumnName, typeof(double));
            dcAmountPrevious.DefaultValue = 0.0;
            dt.Columns.Add(dcAmountPrevious);

            string balDate = "";
            if (IsOB)
            {
                //For Opening Balance, Finding Balance Date
                DateTime dateBalance = DateTime.Parse(AppSetting.YearFromPrevious).AddDays(-1);
                balDate = dateBalance.ToShortDateString();
            }
            else //Closing Date
            {
                balDate = AppSetting.YearToPrevious;
            }
            resultArgs = GetBalance(balDate, projectIds, groupIds);

            //1. Merge Both year legers and balance 
            if (dt != null && resultArgs.Success)
            {
                string transModePrevious = string.Empty;
                double AmtPrevious = 0;
                DataTable dtBalancePrevious = resultArgs.DataSource.Table;
                foreach (DataRow dr in dtBalancePrevious.Rows)
                {
                    transModePrevious = dr[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                    AmtPrevious = UtilityMember.NumberSet.ToDouble(dr[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                    if (transModePrevious == TransactionMode.CR.ToString())
                    {
                        AmtPrevious = -AmtPrevious;
                    }

                    var rows = dt.Select(string.Format(reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName + " = {0}",
                                dr[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName].ToString()));

                    if (rows.Length == 1) //update previous year balance with existing cash, bank, FD ledgers
                    {
                        rows[0][reportSetting1.AccountBalance.AMOUNT_PREVIOUS_YEARColumn.ColumnName] = AmtPrevious;
                    }
                    else if (rows.Length == 0) //Add previous year balance, have to test when filtering bank legers which are clsoed
                    {
                        DataRow drNew = dt.NewRow();
                        drNew[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName] = dr[reportSetting1.AccountBalance.GROUP_IDColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName] = dr[reportSetting1.AccountBalance.GROUP_CODEColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName] = dr[reportSetting1.AccountBalance.LEDGER_GROUPColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName] = dr[reportSetting1.AccountBalance.LEDGER_IDColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName] = dr[reportSetting1.AccountBalance.LEDGER_CODEColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.SORT_IDColumn.ColumnName] = dr[reportSetting1.AccountBalance.SORT_IDColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName] = dr[reportSetting1.AccountBalance.LEDGER_NAMEColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName] = dr[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.AMOUNT_DRColumn.ColumnName] = dr[reportSetting1.AccountBalance.AMOUNT_DRColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.AMOUNT_CRColumn.ColumnName] = dr[reportSetting1.AccountBalance.AMOUNT_CRColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName] = dr[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                        drNew[reportSetting1.AccountBalance.AMOUNT_PREVIOUS_YEARColumn.ColumnName] = AmtPrevious;
                        dt.Rows.Add(dr);
                    }
                    dt.AcceptChanges();
                }
            }
        }

       
    }
}
