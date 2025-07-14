using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Globalization;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using System.Linq;
using DevExpress.XtraReports.UI.PivotGrid;
using DevExpress.Data.Filtering;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetQuarterlyRealization : Bosco.Report.Base.ReportHeaderBase
    {
        bool IsIncomeBudget = true;
        bool rowemptyremoved = false;
        private Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        private string TotalBudgetAppprovedAmount = "";
        
        double BudgetAmountbyLG = 0;
        double ActualAmountbyLG = 0;

        public bool IsLandscapeReport
        {
            get
            {
                return this.Landscape;
            }
        }

        public int ReportLeftMargin
        {
            get
            {
                return this.Margins.Left;
            }
        }

        public BudgetQuarterlyRealization()
        {
            InitializeComponent();

            CheckDateRangeByQuater();
            //this.SetTitleWidth(this.PageWidth - 50);
            //this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 25;
        }

        public void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }

        #region ShowReport
        public override void ShowReport()
        {
            rowemptyremoved = false;
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || String.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project == "0")
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
                        BindBudgetQuaterlySource();
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
                    BindBudgetQuaterlySource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }
        #endregion

        #region Methods
        public void BindBudgetQuaterlySource(bool IsReceipts = true)
        {
            float actualCodeWidth = xrcellCode.WidthF;
            bool isCapCodeVisible = true;

            IsIncomeBudget = IsReceipts;
            setHeaderTitleAlignment();
            SetReportTitle();
            //this.HideDateRange = false;            
            //grpOpeningBalane.Visible = false;
            //detailClosingBalance.Visible = false;
            xrtblDetails = AlignContentTable(xrtblDetails);
            //xrTblLGHeader = AlignGroupTable(xrTblLGHeader);
            //xrTblLGFooter = AlignGroupTable(xrTblLGFooter);
            isCapCodeVisible = (ReportProperties.ShowGroupCode == 1 || ReportProperties.ShowLedgerCode == 1);
            xrcellCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);
            xrCellHeaderLedgerCode.WidthF = ((ReportProperties.ShowLedgerCode == 1) ? actualCodeWidth : 0);

            //To Set Quaterly captions
            string year = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year.ToString();
            xrCellHeaderApproved.Text = year + "\n\rBUDGET";
            xrCellHeaderQ1Actual.Text = year + "\n\rRealized\n\r1st Quarter";
            xrCellHeaderQ2Actual.Text = year + "\n\rRealized\n\r2nd Quarter";
            xrCellHeaderQ3Actual.Text = year + "\n\rRealized\n\r3rd Quarter";
            xrCellHeaderQ4Actual.Text = year + "\n\rRealized\n\r4th Quarter";
            xrCellHeaderTotalActual.Text = year + "\n\rTotal Actual";

            Detail.SortFields.Clear();
            if (this.ReportProperties.SortByLedger == 1)
                Detail.SortFields.Add(new GroupField("LEDGER_NAME"));
            else
                Detail.SortFields.Add(new GroupField("LEDGER_CODE"));

            ResultArgs resultArgs = GetReportSource(IsIncomeBudget);
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                DataTable dtReceiptPaymentsQuaterly = resultArgs.DataSource.Table;
                if (dtReceiptPaymentsQuaterly != null)
                {
                    dtReceiptPaymentsQuaterly.Columns.Add(reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName, typeof(System.Double), "Q1+Q2+Q3+Q4");

                    this.DataSource = dtReceiptPaymentsQuaterly;
                    this.DataMember = resultArgs.DataSource.Table.TableName;

                    bool records = (dtReceiptPaymentsQuaterly.Rows.Count > 0);
                    grpLGHeader.Visible = grpLGFooter.Visible = records;
                    Detail.Visible = ReportFooter1.Visible = records;
                }
                else
                {
                    grpLGHeader.Visible = grpLGFooter.Visible = false;
                    Detail.Visible = ReportFooter1.Visible = false;
                }
            }

            //this.SetTitleWidth(this.PageWidth - 50);
            //this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 25;
        }

        private ResultArgs GetReportSource(bool ReceiptReport)
        {
            ResultArgs resultArgs = null;
            string sqlBudgetQuaterlyRealization = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetQuaterlyRealization);
            string liquidityGroupIds = this.GetLiquidityGroupIds();

            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.YEAR_FROMColumn, this.AppSetting.YearFrom);
                dataManager.Parameters.Add(this.ReportParameters.YEAR_TOColumn, this.AppSetting.YearTo);
                //dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, (ReceiptReport ? TransType.RC.ToString() : TransType.PY.ToString()));
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, liquidityGroupIds);
                dataManager.Parameters.Add(this.ReportParameters.TRANS_MODEColumn, (ReceiptReport ? TransMode.CR.ToString() : TransMode.DR.ToString()));
                dataManager.Parameters.Add(this.ReportParameters.BUDGET_IDColumn, this.ReportProperties.Budget);

                //On 31/07/2020, to fix ledger balacen
               /* dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.SHOWBUDGETLEDGERSEPARATERECEIPTPAYMENTACTUALBALANCEColumn, this.AppSetting.ShowBudgetLedgerSeparateReceiptPaymentActualBalance);

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
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlBudgetQuaterlyRealization);
            }

            return resultArgs;
        }

        
        private void CheckDateRangeByQuater()
        {
            /*DateTime rptdatefrom = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false);
            DateTime rptdateto = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false);
            DateTime dateQuaterFrom = ReportProperty.Current.DateSet.FirstDayOfQuater(rptdatefrom, this.AppSetting.YearFrom, this.AppSetting.YearTo);
            DateTime dateQuaterTo = ReportProperty.Current.DateSet.LastDayOfQuater(rptdateto, this.AppSetting.YearFrom, this.AppSetting.YearTo);

            if ((rptdatefrom != dateQuaterFrom) || (rptdateto != dateQuaterTo))
            {
                this.ReportProperties.DateFrom = dateQuaterFrom.ToShortDateString();
                this.ReportProperties.DateTo = dateQuaterTo.ToShortDateString();
            }*/
        }

        #endregion

        #region Events
        private void xrCellVaraiance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName) != null &&
                GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                double approvedamount = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName).ToString());
                double actual = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName).ToString());
                double variance = (actual - approvedamount);
                cell.Text = (Math.Round( (variance / approvedamount) * 100)).ToString() + " %";

                BudgetAmountbyLG += approvedamount;
                ActualAmountbyLG += actual;
            }

        }

        private void xrcellTotalPercentage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource!=null && GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName) != null &&
                GetCurrentColumnValue(reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                DataTable dtRptData = this.DataSource as DataTable;
                double approvedamount =   UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                double actual = UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                double variance = (actual - approvedamount);
                cell.Text = (Math.Round((variance / approvedamount) * 100)).ToString() + " %";
            }
        }

        private void xrcellLGHeaderSumPercentage_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Round(((ActualAmountbyLG - BudgetAmountbyLG) / BudgetAmountbyLG) * 100) + "%";
            e.Handled = true;
        }

        private void xrcellLGFooterSumPercentage_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Math.Round(((ActualAmountbyLG - BudgetAmountbyLG) / BudgetAmountbyLG) * 100) + "%";
            e.Handled = true;
            ActualAmountbyLG = BudgetAmountbyLG = 0;
        }
        #endregion

        

        
    }
}