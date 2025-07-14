using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class JournalTransactions : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;
        Int32 PreviousVoucherId = 0;
        #endregion

        #region Constructor
        public JournalTransactions()
        {
            InitializeComponent();
            
            this.AttachDrillDownToRecord(xrtblCashBankTrans, xrLedger,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_JOURNAL_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            PreviousVoucherId = 0;
            BindJournalTransactions();
        }
        #endregion

        #region Methods
        private void BindJournalTransactions()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        //  this.ReportTitle = ReportProperty.Current.ReportTitle;
                        //  this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                        setHeaderTitleAlignment();
                        SetReportTitle();
                        // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        
                        resultArgs = GetReportSource();
                        DataView dvCashBank = resultArgs.DataSource.TableView;
                        if (dvCashBank != null)
                        {
                            dvCashBank.Table.TableName = "CashBankTransactions";
                            this.DataSource = dvCashBank;
                            this.DataMember = dvCashBank.Table.TableName;
                        }
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
                    //  this.ReportTitle = ReportProperty.Current.ReportTitle;
                    //  this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                    
                    resultArgs = GetReportSource();
                    DataView dvCashBank = resultArgs.DataSource.TableView;
                    if (dvCashBank != null)
                    {
                        dvCashBank.Table.TableName = "CashBankTransactions";
                        this.DataSource = dvCashBank;
                        this.DataMember = dvCashBank.Table.TableName;
                    }
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblCashBankTrans = AlignContentTable(xrtblCashBankTrans);
            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
            this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankTransaction = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.JournalTransactions);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankTransaction);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        #region Events

        private void xrCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrCredit.Text);
            if (debitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCredit.Text = "";
            }
        }

        #endregion

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName) != null)
            {
                Int32 vid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName).ToString());
                if (PreviousVoucherId > 0 && PreviousVoucherId != vid)
                {
                    xrtblCashBankTrans.TopF = 5;
                    xrDate.Borders = xrLedger.Borders = xrVoucherNo.Borders = xrNarration.Borders = xrDebit.Borders = xrCredit.Borders = DevExpress.XtraPrinting.BorderSide.All;
                }
                else
                {
                    xrtblCashBankTrans.TopF = 0;
                    xrDate.Borders = xrLedger.Borders = xrVoucherNo.Borders = xrNarration.Borders =
                        xrDebit.Borders = xrCredit.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                }
                PreviousVoucherId = vid;
            }
        }

    }
}
