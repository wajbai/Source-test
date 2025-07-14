﻿using System;
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
    public partial class BalanceSheetLiabilities : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        #endregion

        #region Constructor
        public BalanceSheetLiabilities()
        {
            InitializeComponent();

            ArrayList groupfilter = new ArrayList { reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, reportSetting1.ReportParameter.DATE_AS_ONColumn.ColumnName };

            ArrayList ledgerfilter = new ArrayList { reportSetting1.BalanceSheet.LEDGER_IDColumn.ColumnName, reportSetting1.ReportParameter.DATE_AS_ONColumn.ColumnName };

            DrillDownType groupdrilltype = DrillDownType.GROUP_SUMMARY;

            DrillDownType ledgerdrilltype = DrillDownType.LEDGER_SUMMARY;

            this.AttachDrillDownToRecord(xrTblLiabilityLedgerGroup, tcLiabilityParentGroupName,
                 groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTblLiabilityLedgerGroup, tcLiabilityGrpGroupName,
                    groupfilter, groupdrilltype, false, "", false);

            this.AttachDrillDownToRecord(xrTblLiabilityLedgerName, tcLiabilityLedgerName,
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
        string yearto = string.Empty;
        public string YearTo
        {
            get
            {
                yearto = settingProperty.YearTo;
                return yearto;
            }
        }
        public double TotalLiabilities { get; set; }

        public float LiabilitiesLedgerCodeWidth
        {
            set { xrLiaLedgerCode.WidthF = value; }
        }
        public float LiabilitiesLedgerNameWidth
        {
            set { tcLiabilityLedgerName.WidthF = value; }
        }
        public float LiabilitiesAmountWidth
        {
            set { xrLiabilityLedgerAmt.WidthF = value; }
        }

        public float LiabilitiesGroupCodewidth
        {
            set { xrLiaGroupCode.WidthF = value; }
        }
        public float LiabilitiesGroupNamewidth
        {
            set { tcLiabilityGrpGroupName.WidthF = value; }
        }
        public float LiabilitiesGroupAmount
        {
            set { xrGroupTransCredit.WidthF = value; }

        }
        public float LiabilitiesParentGroupName
        {
            set { tcLiabilityParentGroupName.WidthF = value; }
        }

        public float LiabilitiesParentGroupAmt
        {
            set { xrParentGroupAmount.WidthF = value; }
        }
        public float LiabilitiesParentCode
        {
            set { tcLiabilityParentCode.WidthF = value; }
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            //BindBalanceSheetLiability();
            base.ShowReport();
        }

        #endregion

        #region Events

        private void xrtblParentGroupAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ParentGroupAmt = this.ReportProperties.NumberSet.ToDouble(xrParentGroupAmount.Text);
            if (ParentGroupAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrParentGroupAmount.Text = "";
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
        public void HideBalanceSheetLiabilityHeader()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        public void BindBalanceSheetLiability(DataTable dtLiabilityLedgers)
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

                if (dtLiabilityLedgers != null)
                {
                    TotalLiabilities = this.UtilityMember.NumberSet.ToDouble(dtLiabilityLedgers.Compute("SUM(AMOUNT)", string.Empty).ToString());
                    dtLiabilityLedgers.TableName = "BalanceSheet";
                    this.DataSource = dtLiabilityLedgers;
                    this.DataMember = dtLiabilityLedgers.TableName;
                }
                SetReportSetting();
                SortByLedgerorGroup();

                xrTblLiabilityLedgerName = AlignContentTable(xrTblLiabilityLedgerName);
                xrTblLiabilityLedgerGroup = AlignGroupTable(xrTblLiabilityLedgerGroup);
                Detail.Visible = (dtLiabilityLedgers.Rows.Count > 0);
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

            HideLiability();
        }

        private void SortByLedgerorGroup()
        {
            if (grpParentGroup.Visible)
            {
                grpParentGroup.SortingSummary.Enabled = true;
                // grpParentGroup.SortingSummary.FieldName = "SORT_ORDER"; // 05.09.2022
                if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Standard)
                {
                    grpParentGroup.SortingSummary.FieldName = reportSetting1.Payments.PARENT_GROUPColumn.ColumnName; // GROUP_CODE  26.08.2022
                }
                else
                {
                    grpParentGroup.SortingSummary.FieldName = "PARENT_CODE1"; // reportSetting1.Payments.PARENT_CODEColumn.ColumnName; // GROUP_CODE
                }
                grpParentGroup.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpParentGroup.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }

            if (grpLedgerGroup.Visible)
            {
                if (this.ReportProperties.SortByGroup == 0)
                {
                    grpLedgerGroup.SortingSummary.Enabled = true;
                    // grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER";
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
                    // grpLedgerGroup.SortingSummary.FieldName = "SORT_ORDER"; chinna 05.09.2022
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
        #endregion

        private void xrlblReportDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }


        private void tcLiabilityParentGroupName_EvaluateBinding(object sender, BindingEventArgs e)
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
        private void HideLiability()
        {
            // For Generalate, hide sub-group (Receipt Group) even if grouping is on
            if (this.ReportProperties.ReportCodeType == (int)ReportCodeType.Generalate)
            {
                grpLedgerGroup.Visible = false;
            }
        }
        //public ResultArgs GetBalanceSheet()
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
        //        this.ReportPeriod = String.Format("For the Period: {0}", this.ReportProperties.DateAsOn);
        //        setHeaderTitleAlignment();
        //        ResultArgs resultArgs = GetBalanceSheet();
        //        if (resultArgs.Success)
        //        {
        //            DataView dvValue = resultArgs.DataSource.TableView;
        //            if (dvValue != null)
        //            {
        //                dvValue.RowFilter = string.Empty;
        //                dvValue.RowFilter = "(AMOUNT_ACTUAL < 0 AND GROUP_ID NOT IN (12,13,14))";
        //                DataTable dtBind = dvValue.ToTable();
        //                TotalLiabilities = this.UtilityMember.NumberSet.ToDouble(dtBind.Compute("SUM(AMOUNT_ACTUAL)", string.Empty).ToString());
        //                dtBind.TableName = "BalanceSheet";
        //                this.DataSource = dtBind;
        //                this.DataMember = dtBind.TableName;
        //            }
        //            SetReportSetting();
        //            SortByLedgerorGroup();

        //            xrTblLiabilityLedgerName = AlignContentTable(xrTblLiabilityLedgerName);
        //            xrTblLiabilityLedgerGroup = AlignGroupTable(xrTblLiabilityLedgerGroup);
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
