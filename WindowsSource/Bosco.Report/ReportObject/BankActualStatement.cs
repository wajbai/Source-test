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
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class BankActualStatement : Bosco.Report.Base.ReportHeaderBase
    {

        #region Variables
        ResultArgs resultArgs = null;
        int recordnumber = 0;
        double dBankActualOpeningBalance = 0;
        double dBankActualBalance = 0;
        string CreditCaption = string.Empty;
        string DebitCaption = string.Empty;
        string BalanceCaption = string.Empty;
        #endregion

        #region Constructor
        public BankActualStatement()
        {
            InitializeComponent();
            //this.AttachDrillDownToRecord(xrTableSource, xrCellNarration,
            //   new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType., false, "VOUCHER_SUB_TYPE");
            CreditCaption = xrCapCredit.Text;
            DebitCaption = xrCapDebit.Text;
            BalanceCaption = xrCapBalance.Text;
            this.AttachDrillDownToRecord(xrTableSource, xrCellNarration,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");

        }
        #endregion


        #region ShowReport
        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                if (dicDDProperties.ContainsKey(this.reportSetting1.CashBankFlow.DATEColumn.ColumnName))
                {
                    //LedgerDate = dicDDProperties[this.reportSetting1.CashBankFlow.DATEColumn.ColumnName].ToString();
                    //this.ReportProperties.BankAccount = "0";
                    //datefrom = dateto = LedgerDate;
                }
            }
            else
            {
                //datefrom = this.ReportProperties.DateFrom;
                //dateto = this.ReportProperties.DateTo;
            }
            BindBankActualStatement();
        }
        #endregion

        #region Methods
        private void BindBankActualStatement()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo)
                && !string.IsNullOrEmpty(this.ReportProperties.Project)
                && this.ReportProperties.CashBankLedger != "0" && !string.IsNullOrEmpty(this.ReportProperties.CashBankLedger))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindReport();
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
                    BindReport();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }
        private void BindReport()
        {
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            SetLandscapeCostCentreWidth = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
            
            SetReportTitle();
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.CosCenterName = ReportProperties.Count == 1 ? ReportProperties.BankAccountName : " ";
            setHeaderTitleAlignment();

            dBankActualBalance = 0;
            recordnumber = 0;

            dBankActualOpeningBalance = getActualBankOpeningBalance();
            praOpeningBalance.Visible  = false;
            praOpeningBalance.Value = dBankActualOpeningBalance;

            resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataTable dtBankActualStatement = resultArgs.DataSource.Table;
                if (dtBankActualStatement != null && dtBankActualStatement.Rows.Count != 0)
                {
                    dtBankActualStatement.TableName = "DAYBOOK";
                    //On 05/06/2017, To add Amount filter condition
                    AttachAmountFilter(dtBankActualStatement.DefaultView);
                    dtBankActualStatement.DefaultView.Sort = "DATE";
                    this.DataSource = dtBankActualStatement.DefaultView;
                    this.DataMember = dtBankActualStatement.TableName;
                }
                else
                {
                    this.DataSource = null;
                }
            }
            else
            {
                MessageRender.ShowMessage("Could not generate Report " + resultArgs.Message, true);
            }
        }
        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblOpeningBalance = AlignContentTable(xrtblOpeningBalance);
            xrTableSource = AlignContentTable(xrTableSource);
            xrtblGrandTotal = AlignContentTable(xrtblGrandTotal);
             //On 03/09/2024, To set curency symbol based on cash/bank selection --------------------------------
            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                string cashbankcurrencysymbol = ReportProperties.GetCashBankLedgerCurrencySymbol(ReportProperties.CashBankLedger);
                if (!string.IsNullOrEmpty(cashbankcurrencysymbol))
                {
                    xrCapDebit.Text = DebitCaption + " (" + cashbankcurrencysymbol + ")";
                    xrCapCredit.Text = CreditCaption + " (" + cashbankcurrencysymbol + ")";
                    xrCapBalance.Text = BalanceCaption + " (" + cashbankcurrencysymbol + ")";
                }
            }
            else
            {
                this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
                this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
                this.SetCurrencyFormat(xrCapBalance.Text, xrCapBalance);
            }
        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string DayBook = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankActualStatement);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.CashBankLedger);
                    dataManager.Parameters.Add(this.ReportParameters.SHOW_FIXED_DEPOSIT_VOUCHER_DETAILColumn, this.ReportProperties.ShowFixedDepositVoucherDetail);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, DayBook);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// On 23/04/2020, To get actual bank balance for date from of report
        /// it means, get CB bank balance and reduce unrealsiezed and add uncleared amount, it will give you real bank balnce
        /// </summary>
        /// <returns></returns>
        private double getActualBankOpeningBalance()
        {
            double CBBalance = 0;
            double UnrealizedAmt = 0;
            double UnClearedAmt = 0;
            ResultArgs resultArgsBalance = new ResultArgs();
                        
            //Get opening balance for datef from
            CBBalance = this.GetBalance(ReportProperties.Project, ReportProperties.DateFrom, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.BankBalance, 
                AcMEDSync.Model.BalanceSystem.BalanceType.OpeningBalance, ReportProperties.CashBankLedger, true);
            string bankopdate = this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddDays(-1).ToShortDateString();
            string BRStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankReconcilationStatementByConsolidated);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.CashBankLedger);
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, bankopdate);
                dataManager.Parameters.Add(this.ReportParameters.CONSOLIDATEDColumn, 1);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgsBalance = dataManager.FetchData(DAO.Data.DataSource.DataTable, BRStatement);
            }

            if (resultArgsBalance.Success && resultArgsBalance.DataSource.Table != null)
            {
                DataTable dtBRSstatement = resultArgsBalance.DataSource.Table;
                UnrealizedAmt = ReportProperty.Current.NumberSet.ToDouble(dtBRSstatement.Compute("SUM(UNREALISED)", "").ToString());
                UnClearedAmt = ReportProperty.Current.NumberSet.ToDouble(dtBRSstatement.Compute("SUM(UNCLEARED)", "").ToString());
            }

            CBBalance += UnClearedAmt;
            CBBalance -= UnrealizedAmt;

            return CBBalance;
        }

        /// <summary>
        /// This method is used to add filter condition
        /// </summary>
        private DataView AttachAmountFilter(DataView dv)
        {
            //On 05/06/2017, To add Amount filter condition
            string AmountFilter = this.GetAmountFilter();
            lblAmountFilter.Visible = false;
            if (AmountFilter != "")
            {
                dv.RowFilter = "(CREDIT > 0 AND CREDIT " + AmountFilter + ") OR (DEBIT > 0 AND DEBIT " + AmountFilter + ")";
                lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                lblAmountFilter.Visible = true;
            }

            //On 30/08/2018, to filter based on Voucher Type
            string VoucherTypeFilter = string.Empty;
            if (this.ReportProperties.DayBookVoucherType != 0)
            {
                //Utility.DefaultVoucherTypes vouchertype = (Utility.DefaultVoucherTypes)UtilityMember.EnumSet.GetEnumItemType(typeof(Utility.DefaultVoucherTypes), 
                //                                            this.ReportProperties.DayBookVoucherType.ToString());
                //VoucherTypeFilter = "(VOUCHER_TYPE =  '" + vouchertype.ToString() + "')"; 
                VoucherTypeFilter = "(VOUCHER_DEFINITION_ID =  " + this.ReportProperties.DayBookVoucherType.ToString() + ")";
            }
            dv.RowFilter = VoucherTypeFilter;
            return dv;
        }


        #endregion

        private void xrCellBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.DAYBOOK.CREDITColumn.ColumnName) != null
                && GetCurrentColumnValue(reportSetting1.DAYBOOK.DEBITColumn.ColumnName) != null)
            {
                double credit = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.DAYBOOK.CREDITColumn.ColumnName).ToString());
                double debit = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.DAYBOOK.DEBITColumn.ColumnName).ToString());

                //To add bank opening balance at first balance
                if (recordnumber == 0)
                {
                    credit += dBankActualOpeningBalance;
                }

                XRTableCell cell = sender as XRTableCell;
                dBankActualBalance += (credit - debit);
                cell.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(dBankActualBalance)) + " " + (dBankActualBalance < 0 ? TransMode.CR.ToString() : TransMode.DR.ToString());
                recordnumber++;
            }
        }

        private void xrcellDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double PaymentAmt = this.ReportProperties.NumberSet.ToDouble(xrcellDebit.Text);
            if (PaymentAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrcellDebit.Text = "";
            }
        }

        private void xrcellCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrcellCredit.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrcellCredit.Text = "";
            }
        }

        #region Events

        #endregion


       

       

      
    }
}
