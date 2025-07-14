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
    public partial class CashBankVoucherNumber : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;
        Int32 CashBankVNo = 0;
        string PrevVoucherDate = string.Empty;
        string PrevVoucherNo = string.Empty;

        string RptTitle = string.Empty;
        string VoucherType = VoucherSubTypes.RC.ToString();
        int SortingSummary;
        #endregion

        #region Constructor

        public CashBankVoucherNumber()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrtblCashBankTrans, xrLedger,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (this.ReportProperties.ReportId == "RPT-153") //Bank/Cash Receipts
            {
                this.ReportProperties.ReportTitle = (this.ReportProperties.CashBankGroupId == "12" ? "Bank Receipts" : "Cash Receipts") + " (Voucher Number)";
                VoucherType = VoucherSubTypes.RC.ToString();
            }
            else //Bank/Cash Payments
            {
                this.ReportProperties.ReportTitle = (this.ReportProperties.CashBankGroupId == "12" ? "Bank Payments" : "Cash Payments") + " (Voucher Number)";
                VoucherType = VoucherSubTypes.PY.ToString();
            }

            //xrCapNo.Text = (this.ReportProperties.CashBankGroupId == "12" ? "B.No" : "C.No");
            //xrCapChq.Visible = (this.ReportProperties.CashBankGroupId == "12" ? true : false);
            //xrCheque.Visible = (this.ReportProperties.CashBankGroupId == "12" ? true : false);
            /*if (xrHeaderRow.Cells.IndexOf(xrCapChq) >= 0 && this.ReportProperties.CashBankGroupId == "13")
            {
                xrtblHeaderCaption.DeleteColumn(xrCapChq);
            }

            if (xrValueRow.Cells.IndexOf(xrCheque) >= 0 && this.ReportProperties.CashBankGroupId == "13")
            {
                xrtblCashBankTrans.DeleteColumn(xrCheque);
            }*/
                        
            xrTblMonthTotal.WidthF = xrtblHeaderCaption.WidthF;
            //xrDebitBalance.WidthF = xrDebit.WidthF;
            //xrCreditBalance.WidthF = xrCredit.WidthF;
            xrMonthTotal.WidthF = xrNarration.LeftF + xrNarration.WidthF;
           
            BindCashBankReceiptsPayments();
        }
        #endregion

        #region Methods
        private void BindCashBankReceiptsPayments()
        {
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeBudgetNameWidth = xrtblHeaderCaption.WidthF;
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
            setHeaderTitleAlignment();
            
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        GetReportSource();
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
                    GetReportSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }

            setReportBorder();
        }

        private void setReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblCashBankTrans = AlignContentTable(xrtblCashBankTrans);
            xrTblMonth = AlignGrandTotalTable(xrTblMonth);
            xrTblMonthTotal = AlignGrandTotalTable(xrTblMonthTotal);
            xrTblGrandTotal = AlignGrandTotalTable(xrTblGrandTotal);

            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                CashBankVNo = 0;
                string CashBankTransaction = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.CashBankReceiptsPayments);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    //05/12/2019, to keep Cash Bank LedgerId
                    //dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.CashBankLedger);
                    dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, VoucherType);
                    dataManager.Parameters.Add(this.ReportParameters.GROUP_IDColumn, (String.IsNullOrEmpty(this.ReportProperties.CashBankGroupId)?" 0" :this.ReportProperties.CashBankGroupId) );
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankTransaction);
                }
                                
                
                grpHeaderVoucherMonth.GroupFields.Clear();
                grpHeaderVoucherMonth.GroupFields.Add(new GroupField("MONTH_YEAR", XRColumnSortOrder.Ascending));
                
                this.HideCostCenter = ReportProperties.Count == 1 ? true : false;
                this.CosCenterName = ReportProperties.Count == 1 ? ReportProperties.BankAccountName : " ";
                setHeaderTitleAlignment();
                SetReportTitle();
                                
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                if (resultArgs.Success && resultArgs.DataSource.TableView!=null)
                {
                    DataView dvCashBank = resultArgs.DataSource.TableView;
                    dvCashBank.RowFilter = "AMOUNT >0 ";
                    dvCashBank = dvCashBank.ToTable().DefaultView;

                    dvCashBank.Table.TableName = "CashBankTransactions";
                    this.DataSource = dvCashBank;
                    this.DataMember = dvCashBank.Table.TableName;

                    if (dvCashBank.Count > 0)
                    {
                        Detail.Visible = true;
                        grpHeaderVoucherMonth.Visible = (ReportProperty.Current.IncludeLedgerGroupTotal == 0 ? false : true);
                        grpFooterVoucherMonth.Visible = (ReportProperty.Current.IncludeLedgerGroupTotal == 0 ? false : true);
                        ReportFooter.Visible = Detail.Visible;
                    }
                    else
                    {
                        Detail.Visible = false;
                        grpHeaderVoucherMonth.Visible = Detail.Visible;
                        grpFooterVoucherMonth.Visible = Detail.Visible;
                        grpHeaderVoucherMonth.Visible = Detail.Visible;
                        grpFooterVoucherMonth.Visible = Detail.Visible;
                        ReportFooter.Visible = Detail.Visible;
                    }
                }
                else
                {
                    MessageRender.ShowMessage(resultArgs.Message, false);
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
            //double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrCredit.Text);
            //if (debitAmt != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    xrCredit.Text = "";
            //}
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

        private void xrNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell tblcell = sender as XRTableCell;

            DataRowView drView =  this.GetCurrentRow() as DataRowView;
            if (drView !=null)
            {
                if (PrevVoucherDate != drView["VOUCHER_DATE"].ToString() || PrevVoucherNo != drView["VOUCHER_NO"].ToString())
                {
                    CashBankVNo++;
                }
                tblcell.Text = CashBankVNo.ToString();
                PrevVoucherDate = drView["VOUCHER_DATE"].ToString();
                PrevVoucherNo = drView["VOUCHER_NO"].ToString();
            }
        }

        #endregion

        private void xrGrandCreditBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double creditAmt = this.ReportProperties.NumberSet.ToDouble(xrGrandCreditBalance.Text);
            //if (creditAmt != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    XRTableCell xrGrandCredit = sender as XRTableCell;
            //    xrGrandCredit.Text = " ";
            //}
        }

        private void xrGrandDebitBalance_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double debitAmt = this.ReportProperties.NumberSet.ToDouble(xrGrandDebitBalance.Text);
            if (debitAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                XRTableCell xrGrandDebit = sender as XRTableCell;
                xrGrandDebit.Text = " ";
            }
        }


    }
}