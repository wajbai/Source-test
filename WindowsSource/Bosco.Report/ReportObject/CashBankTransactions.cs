using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.Report.View;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class CashBankTransactions : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor

        public CashBankTransactions()
        {
            InitializeComponent();
            
            this.AttachDrillDownToRecord(xrtblCashBankTrans, xrLedger,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindCashBankTransactions();

        }
        #endregion

        #region Methods
        private void BindCashBankTransactions()
        {
            this.SetLandscapeHeader = 1030.25f;
            this.SetLandscapeFooter = 1030.25f;
            this.SetLandscapeFooterDateWidth = 860.00f;
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        
                        // this.ReportTitle = ReportProperty.Current.ReportTitle;
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
                    
                    // this.ReportTitle = ReportProperty.Current.ReportTitle;
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
                    SetReportSetting();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblCashBankTrans = AlignContentTable(xrtblCashBankTrans);
            this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankTransaction = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.CashBankTransactions);
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

        private void SetReportSetting()
        {
            try
            {
                grpDonor.Visible = ReportProperty.Current.ShowByDonorGroup.Equals(1) ? true : false;

                if (grpDonor.Visible)
                {
                    grpDonor.GroupFields[0].FieldName = reportSetting1.CashBankTransactions.NAMEColumn.ColumnName;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }
        #endregion

        #region Events

        private void xrCredit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrDebit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        #endregion
    }
}
