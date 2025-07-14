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
    public partial class JournalInvoiceStatus : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        double TotalInvoiceAmount = 0;
        double TotalPartyInvoiceAmount = 0;

        double TotalBalanceAmount = 0;
        double TotalPartyBalanceAmount = 0;

        string CapInvoiceAmount = string.Empty;
        string CapReceivedAmt = string.Empty;
        string CapBalance = string.Empty;

        #endregion

        #region Constructor

        public JournalInvoiceStatus()
        {
            InitializeComponent();
            CapInvoiceAmount = xrCapInvoiceAmount.Text;
            CapReceivedAmt = xrCapReceivedAmount.Text;
            CapBalance = xrCapBalanceAmount.Text;
         }

        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            BindJournalStatus();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Bind the Data to Controls
        /// </summary>
        private void BindJournalStatus()
        {
            TotalInvoiceAmount = TotalPartyInvoiceAmount = TotalBalanceAmount = TotalPartyBalanceAmount = 0;
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo) && this.ReportProperties.Project != "0"
                && this.ReportProperties.Ledger != "0")
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
                        SetReportTitle();
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
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }

        private void BindProperty()
        {
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeBudgetNameWidth = xrtblHeaderCaption.WidthF;
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
            setHeaderTitleAlignment();

            SetReportTitle();

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.CosCenterName = null;
            resultArgs = GetReportSource();
            Detail.SortFields.Clear();
            Detail.SortFields.Add(new GroupField(reportSetting1.GST_MASTER_INVOICE.VOUCHER_DATEColumn.ColumnName)).ToString();
            if (resultArgs.Success && resultArgs.DataSource.Table!=null)
            {
                DataTable dtJournalInvoice = resultArgs.DataSource.Table;
                dtJournalInvoice.TableName = this.DataMember;
                this.DataSource = dtJournalInvoice;
                this.DataMember = dtJournalInvoice.TableName;

                grpHeaderInvoiceNo.Visible = Detail.Visible = grpFooterInvoiceNo.Visible = ReportFooter.Visible =  (dtJournalInvoice.Rows.Count > 0);
            }
            else
            {
                this.DataSource = null;
            }
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblHeaderDetails = AlignContentTable(xrtblHeaderDetails);
            xrTblPartyLedger = AlignContentTable(xrTblPartyLedger);
            xrtblPartyTotal = AlignContentTable(xrtblPartyTotal);
            xrtblInvoiceDetails = AlignGroupTable(xrtblInvoiceDetails);
            xrtblDetails = AlignContentTable(xrtblDetails);
            xrtblGrandTotal = AlignContentTable(xrtblGrandTotal);

            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                if (!string.IsNullOrEmpty(settingProperty.Currency )) //ReportProperties.CurrencyCountrySymbol
                {
                    xrCapInvoiceAmount.Text = CapInvoiceAmount + " (" + settingProperty.Currency + ")";
                    xrCapReceivedAmount.Text = CapReceivedAmt + " (" + settingProperty.Currency + ")";
                    xrCapBalanceAmount.Text = CapBalance + " (" + settingProperty.Currency + ")";
                }
            }
            else
            {
                this.SetCurrencyFormat(xrCapInvoiceAmount.Text, xrCapInvoiceAmount);
                this.SetCurrencyFormat(xrCapReceivedAmount.Text, xrCapReceivedAmount);
                this.SetCurrencyFormat(xrCapBalanceAmount.Text, xrCapBalanceAmount);
            }

        }

        /// <summary>
        /// Get Reference Source
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetReportSource()
        {
            try
            {
                string ReferenceNo = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.JournalInvoiceStatus);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                    if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, ReferenceNo);
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

        private void xrInvoiceNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrInvoiceNo.Borders = xrInvoiceDate.Borders = xrInvoiceAmount.Borders = xrReceivedAmount.Borders =
                xrBalanceAmount.Borders = xrDueDate.Borders = DevExpress.XtraPrinting.BorderSide.All;
            
        }

        private void xrBalanceAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null && this.GetCurrentColumnValue(reportSetting1.GST_MASTER_INVOICE.INVOICE_AMOUNTColumn.ColumnName)!=null &&
                    this.GetCurrentColumnValue(reportSetting1.GST_MASTER_INVOICE.PAID_AMOUNTColumn.ColumnName)!=null)
            {
                DataTable dt = this.DataSource as DataTable;
                Int32 invoiceid = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue(reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName).ToString());
                double invoiceamt = UtilityMember.NumberSet.ToDouble(this.GetCurrentColumnValue(reportSetting1.GST_MASTER_INVOICE.INVOICE_AMOUNTColumn.ColumnName).ToString());
                
                double paidamount = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(" + reportSetting1.GST_MASTER_INVOICE.PAID_AMOUNTColumn.ColumnName + ")",
                            reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName + "=" + invoiceid).ToString());
                double amt = (invoiceamt - paidamount);
                TotalBalanceAmount += amt;
                TotalPartyBalanceAmount += amt;
                e.Value = (invoiceamt - paidamount);
            }
        }

        private void xrcellPartyLedger_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrcellPartyLedger.Borders = DevExpress.XtraPrinting.BorderSide.Top |  DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
            TotalPartyInvoiceAmount = TotalPartyBalanceAmount = 0;
        }

        private void xrInvoiceAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.GetCurrentColumnValue(reportSetting1.GST_MASTER_INVOICE.INVOICE_AMOUNTColumn.ColumnName)!=null)
            {
                TotalInvoiceAmount += UtilityMember.NumberSet.ToDouble(this.GetCurrentColumnValue(reportSetting1.GST_MASTER_INVOICE.INVOICE_AMOUNTColumn.ColumnName).ToString());
                TotalPartyInvoiceAmount += UtilityMember.NumberSet.ToDouble(this.GetCurrentColumnValue(reportSetting1.GST_MASTER_INVOICE.INVOICE_AMOUNTColumn.ColumnName).ToString());
            }
        }

        private void xrInvoiceGrandTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = TotalInvoiceAmount;
            e.Handled = true;
        }

        private void xrInvoicePartyTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = TotalPartyInvoiceAmount;
            e.Handled = true;
        }

        private void xrBalanceGrandTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = TotalBalanceAmount;
            e.Handled = true;
        }

        private void xrBalancePartyTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = TotalPartyBalanceAmount;
            e.Handled = true;
        }

        private void xrId_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = string.Empty;
        }

        private void xrInvoicePartyCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrInvoicePartyCaption.Borders = DevExpress.XtraPrinting.BorderSide.All;
        }
        
    }
}
