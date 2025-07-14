using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Report.Base;
namespace Bosco.Report.ReportObject
{
    public partial class PurchaseslipSub : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        double Debitamt = 0;
        DataTable dtPayments = new DataTable();
        SettingProperty settings = new SettingProperty();
        double CashBankReceiptsAmt = 0;
        private Int32 NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK = 5;

        #endregion

        #region Construnctor
        public PurchaseslipSub()
        {
            InitializeComponent();
        }

        private bool HideReportLogoLeft
        {
            set
            {
                xrpicReportLogoLeft1.Visible = value;
                xrpicReportLogoLeft1.Image = ImageProcessing.ByteArrayToImage(settings.AcMeERPLogo);
            }
        }


        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (this.ReportProperties.VoucherPrintShowLogo == "1")
            {
                HideReportLogoLeft = true;
            }
            else
            {
                HideReportLogoLeft = false;
            }
            this.HideReportHeader = this.HidePageFooter = false;
            BindReport();
            //base.ShowReport();
        }

        public override void ShowPrintDialogue()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            this.HideDateRange = false;
            BindReport();
            this.ShowPreviewDialog();
        }
        #endregion

        #region Methods


        public void BindReport(bool DuplicateCopy = false)
        {
            if (!string.IsNullOrEmpty(ReportProperties.PrintCashBankVoucherId))
            {
                if (DuplicateCopy)
                {
                    this.HideReportHeader = this.HidePageFooter = false;
                    this.HideDateRange = false;
                }

                float titleLeft = xrpicReportLogoLeft1.LeftF;
                float titleWidth = xrCashBankRecPayDetails.WidthF;
                if (this.ReportProperties.VoucherPrintShowLogo == "1")
                {
                    HideReportLogoLeft = true;
                    titleLeft = xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF;
                    titleWidth = (xrCashBankRecPayDetails.WidthF - (xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF));
                }
                else
                {
                    HideReportLogoLeft = false;
                }
                xrlblInstituteName.LeftF = xrlblInstituteAddress.LeftF = xrReportTitle.LeftF = xrHeaderProjectName.LeftF = titleLeft;
                xrlblInstituteName.WidthF = xrlblInstituteAddress.WidthF = xrHeaderProjectName.WidthF = xrReportTitle.WidthF = titleWidth;
                //xrlblInstituteName.Borders = xrlblInstituteAddress.Borders = xrHeaderProjectName.Borders = xrReportTitle.Borders = DevExpress.XtraPrinting.BorderSide.All;

                setHeaderTitleAlignment();
                xrlblInstituteName.Text = this.GetInstituteName();
                //xrHeaderProjectName.Text = ReportProperty.Current.ProjectTitle;
                xrlblInstituteAddress.Text = ReportProperty.Current.LegalAddress;
                if (this.ReportProperties.VoucherPrintProject == "1")
                {
                    xrHeaderProjectName.Text = ReportProperty.Current.ProjectTitle;
                }
                else
                {
                    xrHeaderProjectName.Text = "";
                }

                resultArgs = BindCashBankPayments();
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtSource = new DataTable();
                    if (this.ReportProperties.ModuleType == "Asset")
                    {
                        DataView dvUniqueFields = resultArgs.DataSource.Table.DefaultView;
                        string[] distinct = { "VOUCHER_ID", "ASSET_ITEM", "QUANTITY", "AMOUNT1", "VOUCHER_NO", "TRANS_MODE", "DRAWN_ON", "CHEQUE", "VOUCHER_DATE", "DONOR_ID", "NAME_ADDRESS", "ADDRESS", "PHONE", "PROJECT_CODE", "SEQUENCE_NO", "LEDGER_NAME", "AMOUNT", "AMOUNT_TRANS_TYPE", "NARRATION" };
                        dtSource = dvUniqueFields.ToTable(true, distinct);
                    }
                    else if (this.ReportProperties.ModuleType == "Stock")
                    {
                        dtSource = resultArgs.DataSource.Table;
                    }

                    dtSource.TableName = "CashBankPayments";
                    this.DataSource = dtSource;
                    this.DataMember = dtSource.TableName;

                    //resultArgs.DataSource.Table.TableName = "CashBankPayments";
                    //this.DataSource = resultArgs.DataSource.Table;
                    //this.DataMember = resultArgs.DataSource.Table.TableName;
                    //PageHeader.Visible = !(resultArgs.DataSource.Table.Rows.Count > NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK);
                }

                // Sub Report for Cash/Bank Amount
                CashBankReceiptPaymentDetails cashbankrec = xrCashBankRecPayDetails.ReportSource as CashBankReceiptPaymentDetails;
                cashbankrec.BindCashBankReceiptsDetails();

                //On 01/03/2021, to show sign details 
                ReportProperty.Current.IncludeSignDetails = 1;
                (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrReportTitle.WidthF;
                //Project/Common Sign details
                (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.Project);
                (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);

                //Set Main Report and Sub Report settings
                SetReportSetting(cashbankrec);
                base.ShowReport();
            }
            else
            {
                //On 01/03/2021, to show sign details 
                ReportProperty.Current.IncludeSignDetails = 1;
                (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrReportTitle.WidthF;
                //Project/Common Sign details
                (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.Project);
                (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);

                xrHeaderProjectName.Text = settings.InstituteName;
                ShowFiancialReportFilterDialog();

            }
        }

        private void SetReportSetting(CashBankReceiptPaymentDetails cashbankrec)
        {
            //Caption Font Style
            lblVoucherNoCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblVoucherNoCaption.Font.FontFamily, lblVoucherNoCaption.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherNoCaption.Font.FontFamily, lblVoucherNoCaption.Font.Size, FontStyle.Regular);
            //lblFundProjectcodeCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblFundProjectcodeCaption.Font.FontFamily, lblFundProjectcodeCaption.Font.Size, FontStyle.Bold)
            //    : new Font(lblFundProjectcodeCaption.Font.FontFamily, lblFundProjectcodeCaption.Font.Size, FontStyle.Regular);
            lblVoucherDateCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblVoucherDateCaption.Font.FontFamily, lblVoucherDateCaption.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherDateCaption.Font.FontFamily, lblVoucherDateCaption.Font.Size, FontStyle.Regular);
            lblRatePerItem.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblRatePerItem.Font.FontFamily, lblRatePerItem.Font.Size, FontStyle.Bold)
                : new Font(lblRatePerItem.Font.FontFamily, lblRatePerItem.Font.Size, FontStyle.Regular);
            xrtblCreditAmt.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrtblCreditAmt.Font.FontFamily, xrtblCreditAmt.Font.Size, FontStyle.Bold)
                : new Font(xrtblCreditAmt.Font.FontFamily, xrtblCreditAmt.Font.Size, FontStyle.Regular);
            lblRatePerItem.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblRatePerItem.Font.FontFamily, lblRatePerItem.Font.Size, FontStyle.Bold)
              : new Font(lblRatePerItem.Font.FontFamily, lblRatePerItem.Font.Size, FontStyle.Regular);
            lblQuantity.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblQuantity.Font.FontFamily, lblQuantity.Font.Size, FontStyle.Bold)
              : new Font(lblQuantity.Font.FontFamily, lblQuantity.Font.Size, FontStyle.Regular);
            lblItem.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblItem.Font.FontFamily, lblItem.Font.Size, FontStyle.Bold)
              : new Font(lblItem.Font.FontFamily, lblItem.Font.Size, FontStyle.Regular);
            lblReceivedFromCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblReceivedFromCaption.Font.FontFamily, lblReceivedFromCaption.Font.Size, FontStyle.Bold)
                : new Font(lblReceivedFromCaption.Font.FontFamily, lblReceivedFromCaption.Font.Size, FontStyle.Regular);
            xrlblTotal.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrlblTotal.Font.FontFamily, xrlblTotal.Font.Size, FontStyle.Bold)
             : new Font(xrlblTotal.Font.FontFamily, xrlblTotal.Font.Size, FontStyle.Regular);

            lblSumCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblSumCaption.Font.FontFamily, lblSumCaption.Font.Size, FontStyle.Bold)
                : new Font(lblSumCaption.Font.FontFamily, lblSumCaption.Font.Size, FontStyle.Regular);
            //lblDrawnOnCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblDrawnOnCaption.Font.FontFamily, lblDrawnOnCaption.Font.Size, FontStyle.Bold)
            //    : new Font(lblDrawnOnCaption.Font.FontFamily, lblDrawnOnCaption.Font.Size, FontStyle.Regular);
            lblNarrationCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblNarrationCaption.Font.FontFamily, lblNarrationCaption.Font.Size, FontStyle.Bold)
                : new Font(lblNarrationCaption.Font.FontFamily, lblNarrationCaption.Font.Size, FontStyle.Regular);

            //Value Font Style
            lblVoucherNo.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Regular);
            //lblProjectCode.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblProjectCode.Font.FontFamily, lblProjectCode.Font.Size, FontStyle.Bold)
            //    : new Font(lblProjectCode.Font.FontFamily, lblProjectCode.Font.Size, FontStyle.Regular);
            lblVoucherDate.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblVoucherDate.Font.FontFamily, lblVoucherDate.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherDate.Font.FontFamily, lblVoucherDate.Font.Size, FontStyle.Regular);
            xrtblRateperItem.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblRateperItem.Font.FontFamily, xrtblRateperItem.Font.Size, FontStyle.Bold)
                : new Font(xrtblRateperItem.Font.FontFamily, xrtblRateperItem.Font.Size, FontStyle.Regular);
            xttblLedgerAmount.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xttblLedgerAmount.Font.FontFamily, xttblLedgerAmount.Font.Size, FontStyle.Bold)
                : new Font(xttblLedgerAmount.Font.FontFamily, xttblLedgerAmount.Font.Size, FontStyle.Regular);
            xrtblRateperItem.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblRateperItem.Font.FontFamily, xttblLedgerAmount.Font.Size, FontStyle.Bold)
              : new Font(xrtblRateperItem.Font.FontFamily, xrtblRateperItem.Font.Size, FontStyle.Regular);
            xrtblQuantity.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblQuantity.Font.FontFamily, xrtblQuantity.Font.Size, FontStyle.Bold)
              : new Font(xrtblQuantity.Font.FontFamily, xrtblQuantity.Font.Size, FontStyle.Regular);
            xrtblAssetItem.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblAssetItem.Font.FontFamily, xrtblAssetItem.Font.Size, FontStyle.Bold)
              : new Font(xrtblAssetItem.Font.FontFamily, xrtblAssetItem.Font.Size, FontStyle.Regular);
            lblReceivedFrom.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblReceivedFrom.Font.FontFamily, lblReceivedFrom.Font.Size, FontStyle.Bold)
               : new Font(lblReceivedFrom.Font.FontFamily, lblReceivedFrom.Font.Size, FontStyle.Regular);
            xrtblAmtInWords.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblAmtInWords.Font.FontFamily, xrtblAmtInWords.Font.Size, FontStyle.Bold)
               : new Font(xrtblAmtInWords.Font.FontFamily, xrtblAmtInWords.Font.Size, FontStyle.Regular);
            //lblDrawnOn.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblDrawnOn.Font.FontFamily, lblDrawnOn.Font.Size, FontStyle.Bold)
            //   : new Font(lblDrawnOn.Font.FontFamily, lblDrawnOn.Font.Size, FontStyle.Regular);
            lblNarration.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblNarration.Font.FontFamily, lblNarration.Font.Size, FontStyle.Bold)
               : new Font(lblNarration.Font.FontFamily, lblNarration.Font.Size, FontStyle.Regular);

            //Signatures
            /*
            //On 02/03/2021, to hide Row1, Row2 and Row3 Voucher Print settings and will have common Sign Details from Finance Settings) 
             
            if (string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign1Row1) && string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign1Row2) &&
                string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign2Row1) && string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign2Row2) &&
                string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign3Row1) && string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign3Row2))
            {
                lblSign1.Borders = lblSign2.Borders = lblSign3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            }
            else
            {
                lblSign1.Borders = lblSign2.Borders = lblSign3.Borders = DevExpress.XtraPrinting.BorderSide.All;
            }


            lblSign1.Text = this.ReportProperties.VoucherPrintSign1Row1 + Environment.NewLine + this.ReportProperties.VoucherPrintSign1Row2;
            lblSign2.Text = this.ReportProperties.VoucherPrintSign2Row1 + Environment.NewLine + this.ReportProperties.VoucherPrintSign2Row2;
            lblSign3.Text = this.ReportProperties.VoucherPrintSign3Row1 + Environment.NewLine + this.ReportProperties.VoucherPrintSign3Row2;*/

            //Fix sub report column width
            //cashbankrec.CodeLedgerWidth = xrtblLedger.WidthF - cashbankrec.ColumnThroughWidth;
            //cashbankrec.CodeLedgerAmountWidth = xttblLedgerAmount.WidthF; //323.90f;
            //Set Sub Report column width
            cashbankrec.CodeLedgerWidth = lblRatePerItem.WidthF - cashbankrec.ColumnThroughWidth;
            //cashbankrec.CodeLedgerAmountWidth = xrtblProFundCode.WidthF; //323.90f;
            cashbankrec.CashBankLedgerTableWidth = xrTable3.WidthF;

            this.SetCurrencyFormat(xrtblCreditAmt.Text, xrtblCreditAmt);
        }

        public ResultArgs BindCashBankPayments()
        {
            //string CashBankPayments = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.CashBankVoucher);
            //using (DataManager dataManager = new DataManager())
            //{
            //    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
            //    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
            //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
            //    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankPayments);
            //}
            //return resultArgs;

            if (this.ReportProperties.ModuleType == "Asset")
            {
                string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchPurchaseslipVoucher);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, this.ReportProperties.PrintCashBankVoucherId);
                    dataManager.Parameters.Add(this.reportSetting1.CashBankReceipts.IN_OUT_IDColumn, this.ReportProperties.PrintPurchaseInoutVoucherId);
                    dataManager.Parameters.Add(this.reportSetting1.GSTPaymentChellan.CGSTColumn, this.AppSetting.CGSTLedgerId);
                    dataManager.Parameters.Add(this.reportSetting1.GSTPaymentChellan.SGSTColumn, this.AppSetting.SGSTLedgerId);
                    dataManager.Parameters.Add(this.reportSetting1.GSTPaymentChellan.IGSTColumn, this.AppSetting.IGSTLedgerId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);


                    //DataView dvUniqueFields = resultArgs.DataSource.Table.DefaultView;
                    //string[] distinct = { "VOUCHER_ID", "ASSET_ITEM", "QUANTITY", "AMOUNT1", "VOUCHER_NO", "TRANS_MODE", "DRAWN_ON", "CHEQUE", "VOUCHER_DATE", "DONOR_ID", "NAME_ADDRESS", "ADDRESS", "PHONE", "PROJECT_CODE", "SEQUENCE_NO", "LEDGER_NAME", "AMOUNT", "AMOUNT_TRANS_TYPE", "NARRATION" };
                    //DataTable dtSource = dvUniqueFields.ToTable(true, distinct);

                    this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
                    //  this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
                    this.ReportProperties.PrintCashBankVoucherId = string.Empty;
                }
            }
            else
            {
                string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchPurchaseStockSlip);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, this.ReportProperties.PrintCashBankVoucherId);
                    dataManager.Parameters.Add(this.reportSetting1.CashBankReceipts.PURCHASE_IDColumn, this.ReportProperties.PrintPurchaseInoutVoucherId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                    this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
                    this.ReportProperties.PrintCashBankVoucherId = string.Empty;
                }
            }
            return resultArgs;
        }

        private DataTable UpdateRupeeInWord(DataTable dataSource)
        {
            string ruppeeInWords = "";
            if (dataSource != null && dataSource.Rows.Count > 0)
            {
                if (!dataSource.Columns.Contains("RUPPEE_AMT"))
                    dataSource.Columns.Add("RUPPEE_AMT");

                foreach (DataRow drRecord in dataSource.Rows)
                {
                    if (drRecord != null)
                    {
                        if (!string.IsNullOrEmpty(drRecord["AMOUNT"].ToString()))
                        {
                            ruppeeInWords = ConvertRuppessInWord.GetRupeesToWord(drRecord["AMOUNT"].ToString());
                            drRecord["RUPPEE_AMT"] = ruppeeInWords;
                        }
                    }
                }
            }
            return dataSource;
        }

        #endregion

        private void xttblLedgerAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TransMode = this.ReportProperties.ModuleType == "Asset" ? GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.AMOUNT_TRANS_TYPEColumn.ColumnName).ToString() : "0";

            if (xttblLedgerAmount != null)
            {
                e.Cancel = false;
            }
            else
            {
                xttblLedgerAmount.Text = "";
            }

        }

        private void xrtblAmtInWords_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ConvertRuppessInWord.GetRupeesToWord(Debitamt.ToString());
            e.Handled = true;
        }

        private void xrtblAmtInWords_SummaryReset_1(object sender, EventArgs e)
        {
            Debitamt = 0;
        }

        private void xrtblAmtInWords_SummaryRowChanged_1(object sender, EventArgs e)
        {
            double tmpDebit = Debitamt;
            Debitamt += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.AMOUNTColumn.ColumnName).ToString());
            string TransMode = GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.AMOUNT_TRANS_TYPEColumn.ColumnName).ToString();
            if (TransMode == "CR")
            {
                Debitamt = tmpDebit + this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.AMOUNTColumn.ColumnName).ToString());
            }
        }
        private void xrCashBankRecPayDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((CashBankReceiptPaymentDetails)((XRSubreport)sender).ReportSource).PrintVoucherId = Convert.ToInt32(GetCurrentColumnValue("VOUCHER_ID"));
            //((CashBankReceiptPaymentDetails)((XRSubreport)sender).ReportSource).currency = this.SetCurrencyFormat("",new XRTableCell());
            ((CashBankReceiptPaymentDetails)((XRSubreport)sender).ReportSource).ColumnThroughWidth = lblReceivedFromCaption.WidthF - 1;
        }

        private void xrTblRequireSignNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //14/07/2021, Hide Reuire Sign note
            xrTblRequireSignNote.Visible = !ReportProperty.Current.HideReportSignNoteInFooter;
        }

    }
}
