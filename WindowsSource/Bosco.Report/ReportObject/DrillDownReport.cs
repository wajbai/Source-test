using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using System.Data;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class DrillDownReport : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        decimal subGrandTotal = 0;
        string VoucherType = "";
        Int32 GrpId = 0;
        Int32 LedgerId = 0;
        string CostCenter = string.Empty;
        string DrillingBaseReportId = string.Empty;
        string RptAsOnDate = string.Empty;

        //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling
        //reportdatefrom, reportdateto will be changed based on its drilling
        string datefrom = string.Empty;
        string dateto = string.Empty;

        string BaseReportId = string.Empty;
        string RecentDrillingReportId = string.Empty;
        Int32 DrillCurrencyCountryId = 0;
        string DrillCurrencySymbol = string.Empty;

        DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
        #endregion

        #region Constructor
        public DrillDownReport()
        {
            InitializeComponent();

            ArrayList alDrillproperties = new ArrayList { reportSetting1.DrillDownReport.PARTICULARS_IDColumn.ColumnName, ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName };
            if (this.ReportProperties.DrillDownProperties != null && this.ReportProperties.DrillDownProperties.Count > 1)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                if (dicDDProperties.ContainsKey("REPORT_ID"))
                    DrillingBaseReportId = dicDDProperties["REPORT_ID"].ToString();

                if (dicDDProperties.ContainsKey("GROUP_ID"))
                    GrpId = UtilityMember.NumberSet.ToInteger(dicDDProperties["GROUP_ID"].ToString());

                if (dicDDProperties.ContainsKey("PARTICULARS_ID"))
                    GrpId = UtilityMember.NumberSet.ToInteger(dicDDProperties["PARTICULARS_ID"].ToString());

                if (dicDDProperties.ContainsKey(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName))
                {
                    CostCenter = dicDDProperties[this.ReportParameters.COST_CENTRE_IDColumn.ColumnName].ToString();
                    alDrillproperties.Add(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName);
                }

                DrillCurrencyCountryId = 0;
                DrillCurrencySymbol = settingProperty.Currency;
                if (dicDDProperties.ContainsKey(this.ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName))
                {
                    DrillCurrencyCountryId = UtilityMember.NumberSet.ToInteger(dicDDProperties[this.ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());

                    if (DrillCurrencyCountryId > 0)
                    {
                        DrillCurrencySymbol = this.GetCurrencySymbol(DrillCurrencyCountryId);
                    }
                }

                switch (ddtypeLinkType)
                {
                    case DrillDownType.GROUP_SUMMARY_RECEIPTS:
                        VoucherType = "RC";
                        break;
                    case DrillDownType.GROUP_SUMMARY_PAYMENTS:
                        VoucherType = "PY";
                        break;
                }

                BaseReportId = GetBaseReportId();

                if (dicDDProperties.ContainsKey("REPORT_ID"))
                    RecentDrillingReportId = dicDDProperties["REPORT_ID"].ToString();
                                
                if (dicDDProperties.ContainsKey(this.ReportParameters.GROUP_IDColumn.ColumnName))
                    GrpId = UtilityMember.NumberSet.ToInteger(dicDDProperties[this.ReportParameters.GROUP_IDColumn.ColumnName].ToString());

                if (dicDDProperties.ContainsKey(this.ReportParameters.COST_CENTRE_IDColumn.ColumnName))
                    CostCenter = dicDDProperties[this.ReportParameters.COST_CENTRE_IDColumn.ColumnName].ToString();

                DrillCurrencyCountryId = 0;
                DrillCurrencySymbol = settingProperty.Currency;
                if (dicDDProperties.ContainsKey(this.ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName))
                {
                    DrillCurrencyCountryId = UtilityMember.NumberSet.ToInteger(dicDDProperties[this.ReportParameters.CURRENCY_COUNTRY_IDColumn.ColumnName].ToString());

                    if (DrillCurrencyCountryId > 0)
                    {
                        DrillCurrencySymbol = this.GetCurrencySymbol(DrillCurrencyCountryId);
                    }
                }

                // On 18.09.2018 changed for Balance Sheet to Assign dataAsOn ( BookBeginFrom to Date As on )
                if (dicDDProperties.ContainsKey(this.ReportParameters.DATE_AS_ONColumn.ColumnName) || BaseReportId == "RPT-031")
                {
                    //If drilling from balancesheet or drilling report (Group/Sub Group)
                    if (RecentDrillingReportId == "RPT-031" || BaseReportId == "RPT-031") //Balancesheet
                    {
                        //21/02/2019, for Balancesheet, Cash/bank/fd opening drilling, reportdatefrom, reportdateto will be changed based on its drilling
                        /*this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom);
                        this.ReportProperties.DateTo = dicDDProperties[this.ReportParameters.DATE_AS_ONColumn.ColumnName].ToString();*/
                        datefrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom);
                        dateto = dicDDProperties[this.ReportParameters.DATE_AS_ONColumn.ColumnName].ToString();
                        this.ReportProperties.ShowLedgerOpBalance = 1;
                    }
                    else if (RecentDrillingReportId == "RPT-030" || BaseReportId == "RPT-030") //TB report - Current and Closing
                    {
                        if (IsFinancialYear() > 0 || (FetchNatureId() == (int)Natures.Assert || FetchNatureId() == (int)Natures.Libilities))
                        {
                            this.ReportProperties.ShowLedgerOpBalance = 1;
                        }
                    }
                    else
                    {
                        RptAsOnDate = dicDDProperties[this.ReportParameters.DATE_AS_ONColumn.ColumnName].ToString();
                    }
                }
            }

            this.AttachDrillDownToRecord(xrtblDrillDown, xrParticulars,
                alDrillproperties, DrillDownType.DRILL_DOWN, false);
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindReceiptSource();
            base.ShowReport();
        }
        #endregion

        #region Methods
        private ResultArgs GetReportSource()
        {
            string sqlMonthlyAbstractReceipts = this.GetReportSQL(string.IsNullOrEmpty(VoucherType) ? 
                        SQL.ReportSQLCommand.Report.DrillDownReportBySummary : SQL.ReportSQLCommand.Report.DrillDownReport);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                //if (!string.IsNullOrEmpty(VoucherType))
                //On 03/06/2019, to filer only pure Receipts and payments 
                if (this.BaseReportId == "RPT-027" || this.BaseReportId == "RPT-001" || this.BaseReportId == "RPT-002" || this.BaseReportId == "RPT-003")
                {
                    dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, VoucherType);
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, AppSetting.TDSOnFDInterestLedgerId);
                }
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, GrpId);
                if (!string.IsNullOrEmpty(CostCenter))
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, CostCenter);

                //On 04/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                if (this.IsDrillDownMode && settingProperty.AllowMultiCurrency == 1 && this.DrillCurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.DrillCurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }
                //----------------------------------------------------------------------------------------------------------------------------------

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, sqlMonthlyAbstractReceipts);
            }

            return resultArgs;
        }
        public void BindReceiptSource()
        {
            SetReportTitle();
            this.ReportTitle = GetGroupName(GrpId); //ReportProperty.Current.ReportTitle;
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            resultArgs = GetReportSource();
            DataView dvReceipt = resultArgs.DataSource.TableView;

            if (dvReceipt != null)
            {
                dvReceipt.RowFilter = "(CREDIT <>0 OR DEBIT <> 0)";
                dvReceipt.Table.TableName = "DrillDownReport";
                this.DataSource = dvReceipt;
                DataTable dt = dvReceipt.ToTable();
                this.DataMember = dvReceipt.Table.TableName;
            }
            SplashScreenManager.CloseForm();
        }

        private string GetBaseReportId()
        {
            string Rtn = string.Empty;

            foreach (object item in this.ReportProperties.stackActiveDrillDownHistory)
            {
                EventDrillDownArgs eventdrilldownarg = item as EventDrillDownArgs;
                if (eventdrilldownarg.DrillDownType == DrillDownType.BASE_REPORT)
                {
                    Rtn = eventdrilldownarg.DrillDownRpt;
                    break;
                }
            }

            return Rtn;
        }

        public int IsFinancialYear()
        {
            ResultArgs resultArgs = new ResultArgs();
            string FinancialYear = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IsFirstFinancialYear);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.Scalar, FinancialYear);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int FetchNatureId()
        {
            ResultArgs resultArgs = new ResultArgs();
            int NatureId = 0;
            string Nature = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.IsNatures);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, LedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Nature);
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    NatureId = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][LedgerParameters.NATURE_IDColumn.ColumnName].ToString());
                }
            }
            return NatureId;
        }
        #endregion

        private void xrParticulars_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView dvrow = GetCurrentRow() as DataRowView;
            XRTableCell FldParticular = ((XRTableCell)sender);
            if (dvrow != null)
            {
                DrillDownType recordType = ((DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dvrow["PARTICULAR_TYPE"].ToString()));
                if (recordType == DrillDownType.GROUP_SUMMARY ||
                    recordType == DrillDownType.GROUP_SUMMARY_RECEIPTS ||
                    recordType == DrillDownType.GROUP_SUMMARY_PAYMENTS)
                    FldParticular.Font = new Font(FldParticular.Font, FontStyle.Bold);
                else
                    FldParticular.Font = new Font(FldParticular.Font, FontStyle.Regular);
            }
        }

        private void xrParticulars_PreviewMouseMove(object sender, PreviewMouseEventArgs e)
        {
            //e.Brick.Value.
            //e.Brick.BorderStyle = DevExpress.XtraPrinting.BrickBorderStyle.Outset;
            //xrtblDrillDown.Borders = DevExpress.XtraPrinting.BorderSide.All;
            //xrtblDrillDown.BackColor = Color.Green;
        }

        private void xrDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(LedgerParameters.DEBITColumn.ColumnName) != null)
            {
                double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrDebit.Text);
                if (debitAmt != 0)
                {
                    e.Cancel = false;
                }
                else
                {
                    xrDebit.Text = "";
                }
            }
        }

        private void xrCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(LedgerParameters.CREDITColumn.ColumnName) != null)
            {
                double creditAmt = this.ReportProperties.NumberSet.ToDouble(xrCredit.Text);
                if (creditAmt != 0)
                {
                    e.Cancel = false;
                }
                else
                {
                    xrCredit.Text = "";
                }
            }
        }
    }
}
