using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class BalanceSheetAssets : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        #endregion

        #region Constructor
        public BalanceSheetAssets()
        {
            InitializeComponent();

            ArrayList groupfilter = new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, reportSetting1.ReportParameter.DATE_AS_ONColumn.ColumnName };

            ArrayList ledgerfilter = new ArrayList { reportSetting1.BalanceSheet.LEDGER_IDColumn.ColumnName, reportSetting1.ReportParameter.DATE_AS_ONColumn.ColumnName };

            DrillDownType groupdrilltype = DrillDownType.GROUP_SUMMARY;

            DrillDownType ledgerdrilltype = DrillDownType.LEDGER_SUMMARY;

            this.AttachDrillDownToRecord(xrTblAssetLedgerGroup, tcAssetParentGroupName,
                  groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTblAssetLedgerGroup, tcAssetGrpGroupName,
                    groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTblAssetLedgerName, tcAssetLedgerName,
                ledgerfilter, ledgerdrilltype, false, "", false);
        }
        #endregion

        #region Property
        string yearFrom = string.Empty;
        public string YearFrom
        {
            get
            {
                yearFrom = settingProperty.YearFrom;
                return yearFrom;
            }
        }
        public double TotalAssets { get; set; }
        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }

        public float AssetLedgerCodeWidth
        {
            set { xrtblAssetLedgerCode.WidthF = value; }
            get { return xrtblAssetLedgerCode.WidthF; }
        }
        public float AssetLedgerNameWidth
        {
            set { tcAssetLedgerName.WidthF = value; }
            get { return tcAssetLedgerName.WidthF; }
        }
        public float AssetAmountWidth
        {
            set { tcAssetLedgerAmt.WidthF = value; }
            get { return tcAssetLedgerAmt.WidthF; }
        }

        public float AssetGroupCodewidth
        {
            set { xrtblAssetGroupCode.WidthF = value; }
            get { return xrtblAssetGroupCode.WidthF; }
        }
        public float AssetGroupNamewidth
        {
            set { tcAssetGrpGroupName.WidthF = value; }
            get { return tcAssetGrpGroupName.WidthF; }
        }
        public float AssetGroupAmount
        {
            set { xrtblTransDebit.WidthF = value; }
            get { return xrtblTransDebit.WidthF; }
        }
        public float AssetParentGroupName
        {
            set { tcAssetParentGroupName.WidthF = value; }
        }

        public float AssetParentGroupAmt
        {
            set { xrtblParentGroupAmount.WidthF = value; }
        }

        public float AssetParentCode
        {
            set { tcAssetParentCode.WidthF = value; }
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            //BindBalanceSheetAsset();
            base.ShowReport();
        }
        #endregion

        #region Events

        private void xrtblParentGroupAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ParentGroupAmt = this.ReportProperties.NumberSet.ToDouble(xrtblParentGroupAmount.Text);
            if (ParentGroupAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrtblParentGroupAmount.Text = "";
            }
        }

        private void grpLedgerGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null)
            {
                string ParentGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.MonthlyAbstract.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;
                string LedgerGroup = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName) != null ?
                    GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_GROUPColumn.ColumnName).ToString() : string.Empty;

                if (ParentGroup.Trim().Equals(LedgerGroup.Trim()))
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region Methods
        public void HideBalanceSheetAssetCapHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindBalanceSheetAsset(DataTable dtAssetLedgers)
        {
            try
            {
                string datetime = this.GetProgressiveDate(this.ReportProperties.DateAsOn);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.ReportTitle = this.ReportProperties.ReportTitle;

                DateTime dtDateFrom = Convert.ToDateTime(YearFrom);
                string YearFromReducing = dtDateFrom.AddDays(-1).ToShortDateString();
                this.SetLandscapeHeader = 1030.25f;
                this.SetLandscapeFooter = 1030.25f;
                this.SetLandscapeFooterDateWidth = 860.00f;

                //On 12/05/2020, No need to show report criteria for with in sub report
                //if (ReportProperty.Current.ReportFlag == 0)
                //{
                SetReportTitle();
                this.ReportPeriod = String.Format("For the Period: {0}", this.ReportProperties.DateAsOn);
                setHeaderTitleAlignment();

                if (dtAssetLedgers != null)
                {
                    TotalAssets = this.UtilityMember.NumberSet.ToDouble(dtAssetLedgers.Compute("SUM(AMOUNT)", string.Empty).ToString());
                    dtAssetLedgers.TableName = "BalanceSheet";
                    this.DataSource = dtAssetLedgers;
                    this.DataMember = dtAssetLedgers.TableName;
                }
                if (dtAssetLedgers != null)
                {
                    AccountBalance accountBalance = xrSubBalance.ReportSource as AccountBalance;
                    xrSubBalance.Visible = true;
                    accountBalance.BankClosedDate = this.ReportProperties.DateAsOn;
                    accountBalance.BindBalance(false, true, true);
                    this.AttachDrillDownToSubReport(accountBalance);
                    accountBalance.CodeColumnWidth = 0;
                    accountBalance.GroupCode = 0;
                    accountBalance.NameColumnWidth = AssetLedgerNameWidth + AssetLedgerCodeWidth;  // tcAssetLedgerName.WidthF - 2;
                    accountBalance.AmountColumnWidth = AssetAmountWidth;  // tcAssetLedgerAmt.WidthF;
                    accountBalance.GroupNameWidth = AssetLedgerNameWidth + AssetLedgerCodeWidth; //AssetGroupNamewidth + AssetGroupCodewidth;    //tcAssetLedgerName.WidthF - 2;
                    accountBalance.GroupAmountWidth = AssetGroupAmount; //tcAssetLedgerAmt.WidthF;

                    accountBalance.AmountProgressiveHeaderColumnWidth = 0;
                    accountBalance.AmountProgressiveColumnWidth = 0;
                    accountBalance.LedgerCodeVisible = false;
                    accountBalance.GroupCodeVisible = false;
                    accountBalance.AmountProgressVisible = false;
                    accountBalance.GroupProgressVisible = false;
                    TotalAssets += accountBalance.PeriodBalanceAmount;

                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
                    {
                        accountBalance.GroupCode = AssetLedgerCodeWidth;
                        accountBalance.NameColumnWidth = AssetLedgerNameWidth;
                        accountBalance.GroupNameWidth = AssetLedgerNameWidth;
                        accountBalance.GroupAmountWidth = AssetGroupAmount;
                        accountBalance.GroupCodeVisible = true;
                    }
                }
                else
                {
                    xrSubBalance.Visible = false;
                }

                SetReportSetting();
                SortByLedgerorGroup();
                xrTblAssetLedgerName = AlignContentTable(xrTblAssetLedgerName);
                xrTblAssetLedgerGroup = AlignGroupTable(xrTblAssetLedgerGroup);
                Detail.Visible = (dtAssetLedgers.Rows.Count > 0);
                //}
                //else
                //{
                //    SetReportTitle();
                //    ShowReportFilterDialog();
                //}
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        private void SetReportSetting()
        {
            //Include / Exclude Ledger group or Ledger
            grpLedgerGroup.Visible = grpParentGroup.Visible = (ReportProperties.ShowByLedgerGroup == 1);
            grpLedger.Visible = (ReportProperties.ShowByLedger == 1);
            grpLedgerGroup.GroupFields[0].FieldName = "";
            grpParentGroup.GroupFields[0].FieldName = "";
            grpLedger.GroupFields[0].FieldName = "";

            if (grpLedgerGroup.Visible == false && grpLedger.Visible == false)
            {
                grpLedger.Visible = true;
            }

            if (grpParentGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.BalanceSheet.PARENT_GROUPColumn.ColumnName;
                }
                else
                {
                    grpParentGroup.GroupFields[0].FieldName = reportSetting1.BalanceSheet.PARENT_GROUPColumn.ColumnName;
                }
            }

            if (grpLedgerGroup.Visible)
            {
                if (ReportProperties.SortByGroup == 1)
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.BalanceSheet.LEDGER_GROUPColumn.ColumnName;
                }
                else
                {
                    grpLedgerGroup.GroupFields[0].FieldName = reportSetting1.BalanceSheet.LEDGER_GROUPColumn.ColumnName;
                }
            }

            if (grpLedger.Visible)
            {
                if (ReportProperties.SortByLedger == 1)
                {
                    grpLedger.GroupFields[0].FieldName = reportSetting1.BalanceSheet.LEDGER_NAMEColumn.ColumnName;
                }
                else
                {
                    grpLedger.GroupFields[0].FieldName = reportSetting1.BalanceSheet.LEDGER_NAMEColumn.ColumnName;
                }
            }

            HideSubGroup();

        }

        private void SortByLedgerorGroup()
        {
            if (grpParentGroup.Visible)
            {
                grpParentGroup.SortingSummary.Enabled = true;
                // grpParentGroup.SortingSummary.FieldName = "SORT_ORDER"; // commanded by chinna 05.09.2022
                if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                {
                    grpParentGroup.SortingSummary.FieldName = reportSetting1.Payments.PARENT_GROUPColumn.ColumnName; // GROUP_CODE  26.08.2022
                }
                else
                {
                    grpParentGroup.SortingSummary.FieldName = "PARENT_CODE1";     //reportSetting1.Payments.PARENT_CODEColumn.ColumnName; // GROUP_CODE
                }
                grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }

            if (grpLedgerGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    //  grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER";  chinna 05.09.2022
                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                    {
                        // grpReceiptGroup.SortingSummary.FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;  // GROUP_CODE 26.08
                        grpLedgerGroup.SortingSummary.FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;  // GROUP_CODE
                    }
                    else
                    {
                        // grpReceiptGroup.SortingSummary.FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;  // GROUP_CODE 26.08
                        grpLedgerGroup.SortingSummary.FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;  // GROUP_CODE
                    }
                    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    //grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER"; chinna 05.09.2022
                    if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                    {
                        // grpReceiptGroup.SortingSummary.FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;  // GROUP_CODE 26.08
                        grpLedgerGroup.SortingSummary.FieldName = reportSetting1.Payments.LEDGER_GROUPColumn.ColumnName;  // GROUP_CODE
                    }
                    else
                    {
                        // grpReceiptGroup.SortingSummary.FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;  // GROUP_CODE 26.08
                        grpLedgerGroup.SortingSummary.FieldName = reportSetting1.Payments.GROUP_CODEColumn.ColumnName;  // GROUP_CODE
                    }
                    grpLedgerGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedgerGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
            if (grpLedger.Visible)
            {
                if (this.ReportProperties.SortByLedger == 0)
                {
                    grpLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpLedger.SortingSummary.FieldName = "SORT_ORDER";
                        grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    }
                    else
                    {
                        grpLedger.SortingSummary.FieldName = "LEDGER_CODE";
                    }
                    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
                else
                {
                    grpLedger.SortingSummary.Enabled = true;
                    if (this.ReportProperties.ShowByLedgerGroup == 1)
                    {
                        grpLedger.SortingSummary.FieldName = "SORT_ORDER";  // LEDGER_CODE
                        grpLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    else
                    {
                        grpLedger.SortingSummary.FieldName = "LEDGER_NAME";
                    }
                    grpLedger.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpLedger.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
                }
            }
        }

        /// <summary>
        //For closing balance
        /// </summary>
        public void AttachDrillDownToAccountBalance()
        {
            AccountBalance accountBalance = xrSubBalance.ReportSource as AccountBalance;
            accountBalance.CodeColumnWidth = 0;
            accountBalance.GroupCode = 0;
            accountBalance.NameColumnWidth = AssetLedgerNameWidth + AssetLedgerCodeWidth - 2;
            accountBalance.AmountColumnWidth = AssetAmountWidth;
            accountBalance.GroupNameWidth = AssetLedgerNameWidth + AssetLedgerCodeWidth - 2;
            accountBalance.GroupAmountWidth = AssetGroupAmount;

            accountBalance.AmountProgressiveHeaderColumnWidth = 0;
            accountBalance.AmountProgressiveColumnWidth = 0;
            accountBalance.LedgerCodeVisible = false;
            accountBalance.GroupCodeVisible = false;
            accountBalance.AmountProgressVisible = false;
            accountBalance.GroupProgressVisible = false;
            TotalAssets += accountBalance.PeriodBalanceAmount;
            this.AttachDrillDownToSubReport(accountBalance);

            if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
            {
                accountBalance.GroupCode = AssetLedgerCodeWidth;
                accountBalance.NameColumnWidth = AssetLedgerNameWidth;
                accountBalance.GroupNameWidth = AssetLedgerNameWidth;
                accountBalance.GroupAmountWidth = AssetGroupAmount;
                accountBalance.GroupCodeVisible = true;
            }
        }

        #endregion

        private void tcAssetParentGroupName_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (!(this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard))
            {
                if (GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName) != null)
                {
                    string GroupCode = GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName) != null ?
                        GetCurrentColumnValue(reportSetting1.Payments.PARENT_CODEColumn.ColumnName).ToString() : string.Empty;

                    string ParentGroup = GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName) != null ?
                        GetCurrentColumnValue(reportSetting1.Payments.PARENT_GROUPColumn.ColumnName).ToString() : string.Empty;

                    if (ParentGroup.Trim() == "Current Liabilities" || ParentGroup.Trim() == "Fixed Assets")
                    {
                        e.Value = ParentGroup.Trim() + " -(" + GroupCode.Trim() + ")";
                    }
                }
            }
        }



        // hide the Sub 25/05/2025, *Chinna
        private void HideSubGroup()
        {
            // For Generalate, hide sub-group (Receipt Group) even if grouping is on
            if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
            {
                grpLedgerGroup.Visible = false;
            }
        }
        //public ResultArgs GetBalanceSheetSource()
        //{
        //    string BalanceSheet = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.BalanceSheet);
        //    using (DataManager dataManager = new DataManager())
        //    {
        //        dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
        //        dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);

        //        int LedgerPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;
        //        int GroupPaddingRequired = (ReportProperties.ShowByLedgerGroup == 1) ? 1 : 0;

        //        dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, LedgerPaddingRequired);
        //        dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, LedgerPaddingRequired);


        //        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //        resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, BalanceSheet);
        //    }
        //    return resultArgs;
        //}

        //private void BindBalanceSheet()
        //{
        //    if (ReportProperty.Current.ReportFlag == 0)
        //    {
        //        SetReportTitle();
        //        DataTable dtBind = new DataTable();
        //        this.ReportPeriod = String.Format("For the Period: {0}", this.ReportProperties.DateAsOn);
        //        setHeaderTitleAlignment();
        //        ResultArgs resultArgs = GetBalanceSheetSource();
        //        if (resultArgs.Success)
        //        {
        //            DataView dvValue = resultArgs.DataSource.TableView;
        //            DataTable dtva = dvValue.ToTable();
        //            if (dvValue != null)
        //            {
        //                DataTable dt = dvValue.ToTable();
        //                dvValue.RowFilter = "";
        //                dvValue.RowFilter = "(AMOUNT_ACTUAL > 0 AND GROUP_ID NOT IN (12,13,14))";
        //                dtBind = dvValue.ToTable();
        //                TotalAssets = this.UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(AMOUNT_ACTUAL)", string.Empty).ToString());
        //                dtBind.TableName = "BalanceSheet";
        //                this.DataSource = dtBind;
        //                this.DataMember = dtBind.TableName;
        //            }
        //            if (dtBind != null)
        //            {

        //                AccountBalance accountBalance = xrSubBalance.ReportSource as AccountBalance;
        //                xrSubBalance.Visible = true;
        //                accountBalance.BindBalance(false, true, true);
        //                this.AttachDrillDownToSubReport(accountBalance);
        //                accountBalance.CodeColumnWidth = 0;
        //                accountBalance.GroupCode = 0;
        //                accountBalance.NameColumnWidth = tcAssetLedgerName.WidthF - 2;
        //                accountBalance.AmountColumnWidth = tcAssetLedgerAmt.WidthF;
        //                accountBalance.GroupNameWidth = tcAssetLedgerName.WidthF - 2;
        //                accountBalance.GroupAmountWidth = tcAssetLedgerAmt.WidthF;

        //                accountBalance.AmountProgressiveHeaderColumnWidth = 0;
        //                accountBalance.AmountProgressiveColumnWidth = 0;
        //                accountBalance.LedgerCodeVisible = false;
        //                accountBalance.GroupCodeVisible = false;
        //                accountBalance.AmountProgressVisible = false;
        //                accountBalance.GroupProgressVisible = false;
        //                TotalAssets += accountBalance.PeriodBalanceAmount;
        //            }
        //            else
        //            {
        //                xrSubBalance.Visible = false;
        //            }


        //            SetReportSetting();
        //            SortByLedgerorGroup();
        //            xrTblAssetLedgerName = AlignContentTable(xrTblAssetLedgerName);
        //            xrTblAssetLedgerGroup = AlignGroupTable(xrTblAssetLedgerGroup);

        //            Detail.Visible = (dvValue.Count > 0);
        //        }
        //        else
        //        {
        //            MessageRender.ShowMessage("Could not generate Balanesheet, " + resultArgs.Message);
        //        }
        //    }
        //    else
        //    {
        //        SetReportTitle();
        //        ShowReportFilterDialog();
        //    }
        //}
    }
}
