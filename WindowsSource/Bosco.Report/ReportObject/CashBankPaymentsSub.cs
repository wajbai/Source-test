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
using DevExpress.XtraPrinting;
namespace Bosco.Report.ReportObject
{
    public partial class CashBankPaymentsSub : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        double Debitamt = 0;
        DataTable dtPayments = new DataTable();
        SettingProperty settings = new SettingProperty();
        double CashBankReceiptsAmt = 0;
        private Int32 NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK = 4;

        private DataTable dtCCDetails = new DataTable();
        private bool PrevLedgerCCFound = false;

        #endregion

        #region Construnctor
        public CashBankPaymentsSub()
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


        public void BindReport(bool DuplicateCopy=false)
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

                //13/07/2021, Hide Reuire Sign note
                xrTblRequireSignNote.Visible = !ReportProperty.Current.HideReportSignNoteInFooter ;
                //if (xrTblRequireSignNote.Visible)
                //{
                //    if (ReportProperty.Current.Sign1Image==null && ReportProperty.Current.Sign2Image==null &&
                //        ReportProperty.Current.Sign3Image==null && ReportProperty.Current.Sign4Image==null && ReportProperty.Current.Sign5Image==null)
                //    xrTblRequireSignNote.Visible =false;
                //}

                resultArgs = BindCashBankPayments();
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "CashBankPayments";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                    //PageHeader.Visible = !(resultArgs.DataSource.Table.Rows.Count > NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK);
                }

                // Sub Report for Cash/Bank Amount
                CashBankReceiptPaymentDetails cashbankrec = xrCashBankRecPayDetails.ReportSource as CashBankReceiptPaymentDetails;
                cashbankrec.BindCashBankReceiptsDetails();

                //On 01/03/2021, to show sign details 
                ReportProperty.Current.IncludeSignDetails = 1;
                (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrTblHeader.WidthF;
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
                (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrTblHeader.WidthF;
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
            lblParticularsCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblParticularsCaption.Font.FontFamily, lblParticularsCaption.Font.Size, FontStyle.Bold)
                : new Font(lblParticularsCaption.Font.FontFamily, lblParticularsCaption.Font.Size, FontStyle.Regular);
            xrtblCreditAmt.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrtblCreditAmt.Font.FontFamily, xrtblCreditAmt.Font.Size, FontStyle.Bold)
                : new Font(xrtblCreditAmt.Font.FontFamily, xrtblCreditAmt.Font.Size, FontStyle.Regular);
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
            xrtblLedger.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblLedger.Font.FontFamily, xrtblLedger.Font.Size, FontStyle.Bold)
                : new Font(xrtblLedger.Font.FontFamily, xrtblLedger.Font.Size, FontStyle.Regular);
            xttblLedgerAmount.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xttblLedgerAmount.Font.FontFamily, xttblLedgerAmount.Font.Size, FontStyle.Bold)
                : new Font(xttblLedgerAmount.Font.FontFamily, xttblLedgerAmount.Font.Size, FontStyle.Regular);
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
            cashbankrec.CodeLedgerWidth = lblParticularsCaption.WidthF - cashbankrec.ColumnThroughWidth;
            //cashbankrec.CodeLedgerAmountWidth = xrtblProFundCode.WidthF; //323.90f;
            cashbankrec.CashBankLedgerTableWidth = xrTblLedgerDetails.WidthF;

            //this.SetCurrencyFormat(xrtblCreditAmt.Text, xrtblCreditAmt);
        }

        public ResultArgs BindCashBankPayments()
        {
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchcashBankByVoucher);
            string vids = this.ReportProperties.PrintCashBankVoucherId;
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, vids);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.CashBankVoucherDateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtPaymentPrint = resultArgs.DataSource.Table;
                    //On 09/07/2024, for nullify vouchers, get pure amount alone
                    dtPaymentPrint.DefaultView.RowFilter = reportSetting1.CashBankReceipts.AMOUNTColumn.ColumnName + ">0 OR " + 
                                reportSetting1.CashBankReceipts.CASH_BANK_AMOUNTColumn.ColumnName + ">0";
                    dtPaymentPrint = dtPaymentPrint.DefaultView.ToTable();
                    resultArgs.DataSource.Data = dtPaymentPrint;
                    this.ReportProperties.CashBankJouranlByVoucher = dtPaymentPrint;
                }

                this.ReportProperties.PrintCashBankVoucherId = string.Empty;
            }

            xrSubreportCCDetails.Visible = false;
            if (ReportProperty.Current.VoucherPrintShowCostCentre == 1)
            {
                xrSubreportCCDetails.Visible = true;
                AssignCCDetailReportSource(vids);
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
            string TransMode = GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.AMOUNT_TRANS_TYPEColumn.ColumnName).ToString();
            
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
            //13/07/2021, Hide Reuire Sign note
            //xrTblRequireSignNote.Visible = !ReportProperty.Current.HideReportSignNoteInFooter;
        }

        private void xttblLedgerAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.AMOUNTColumn.ColumnName) != null)
            //{
            //    //If double entry, show cr and dr
            //    Int32 vid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.VOUCHER_IDColumn.ColumnName).ToString());
            //    if (IsDoubleEntry(vid))
            //    {
            //        double amt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.AMOUNTColumn.ColumnName).ToString());
            //        if (amt <= 0)
            //        {
            //            e.Value = UtilityMember.NumberSet.ToNumber(Math.Abs(amt)) + " Cr";
            //        }
            //        else
            //        {
            //            e.Value = UtilityMember.NumberSet.ToNumber(amt) + " Dr";
            //        }
            //    }
            //}
        }


        /// <summary>
        /// On 10/05/2022 to check double entry or not for concern Voucherid
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        private bool IsDoubleEntry(Int32 vid)
        {
            bool rtn = false;
            if (this.DataSource != null && vid > 0)
            {
                DataTable dtRpt = this.DataSource as DataTable;
                DataTable dt = dtRpt.DefaultView.ToTable();
                dt.DefaultView.RowFilter = this.reportSetting1.CashBankReceipts.VOUCHER_IDColumn.ColumnName + " = " + vid +
                                            " AND " + this.reportSetting1.CashBankReceipts.AMOUNTColumn.ColumnName + " <= 0";

                rtn = (dt.DefaultView.Count > 0);
            }
            return rtn;
        }

        /// <summary>
        /// On 09/10/2023, to get CC deatils for given project and date range
        /// </summary>
        private void AssignCCDetailReportSource(string vids)
        {
            ResultArgs resultArgs = null;
            string sqlccDetail = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CCDetailVouchers);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_IDColumn, vids);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlccDetail);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtCCDetails = resultArgs.DataSource.Table;
                dtCCDetails.Columns[reportSetting1.Ledger.AMOUNT_PERIODColumn.ColumnName].ColumnName = reportSetting1.Ledger.DEBITColumn.ColumnName;

            }
        }

        private void ProperBorderForLedgerRow(bool ccFound)
        {
            if (ccFound)
            {
                xrtblLedger.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xttblLedgerAmount.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
            }
            else
            {
                xrtblLedger.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xttblLedgerAmount.Borders = BorderSide.Right | BorderSide.Bottom;
            }
        }

        private void ShowCCDetails()
        {
            //On 05/10/2021, To show CC detail for given Ledger
            xrSubreportCCDetails.Visible = false;
            if (this.ReportProperties.VoucherPrintShowCostCentre == 1)
            {
                if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName) != null && dtCCDetails.Rows.Count > 0)
                {
                    Int32 voucherid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.VOUCHER_IDColumn.ColumnName).ToString());
                    Int32 ledgerid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.LEDGER_IDColumn.ColumnName).ToString());
                    Int32 sequenceno = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.ReportParameters.SEQUENCE_NOColumn.ColumnName).ToString());
                    UcCCDetail ccDetail = xrSubreportCCDetails.ReportSource as UcCCDetail;
                    dtCCDetails.DefaultView.RowFilter = string.Empty;
                    dtCCDetails.DefaultView.RowFilter = this.ReportParameters.VOUCHER_IDColumn.ColumnName + " = " + voucherid + " AND " +
                                                        this.ReportParameters.LEDGER_IDColumn.ColumnName + " = " + ledgerid + " AND " +
                                                        this.ReportParameters.LEDGER_SEQUENCE_NOColumn.ColumnName + " = " + sequenceno;
                    DataTable dtCC = dtCCDetails.DefaultView.ToTable();

                    ccDetail.BindCCDetails(dtCC, false);
                    ccDetail.DateWidth = 0;
                    xrSubreportCCDetails.LeftF = xrtblLedger.LeftF;
                    ccDetail.ShowIndentInCostCentre = true;
                    ccDetail.CCCreditCaption = "";
                    ccDetail.CCDebitCaption = "";
                    ccDetail.CCNameWidth = ((xrtblLedger.WidthF / 2) + (xrtblLedger.WidthF / 4));
                    ccDetail.CCDebitWidth = xrtblLedger.WidthF / 4;
                    ccDetail.CCCreditWidth = xrtblLedger.WidthF / 4;
                    ccDetail.HideReportHeaderFooter();
                    dtCCDetails.DefaultView.RowFilter = string.Empty;

                    xrSubreportCCDetails.Visible = (dtCC.Rows.Count > 0);
                    PrevLedgerCCFound = (dtCC.Rows.Count > 0);
                }
                else
                {
                    xrSubreportCCDetails.Visible = false;
                    PrevLedgerCCFound = false;
                }
            }
            else
            {
                xrSubreportCCDetails.Visible = false;
                PrevLedgerCCFound = false;
            }

            if (!xrSubreportCCDetails.Visible)
            {
                if (Detail.Controls.Contains(xrSubreportCCDetails))
                {
                    Detail.Controls.Remove(xrSubreportCCDetails);
                }
            }
            else
            {
                Detail.Controls.Add(xrSubreportCCDetails);
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ShowCCDetails();
            Detail.HeightF = 25;
        }

        private void Detail_AfterPrint(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName) != null)
            {
                string ledgername = GetCurrentColumnValue(reportSetting1.MonthlyAbstract.LEDGER_NAMEColumn.ColumnName).ToString();
                ProperBorderForLedgerRow(PrevLedgerCCFound);
            }
        }

        private void xrTblLedgerDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrRowLedgerNarration.Visible = (Narration != string.Empty); 
        }

        private void xrTblGeneralNarration_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string Narration = (GetCurrentColumnValue("GENERAL_NARRATION") == null) ? string.Empty : GetCurrentColumnValue("GENERAL_NARRATION").ToString();
            xrTblGeneralNarration.Visible = (Narration != string.Empty); 
        }

        private void lblVoucherNo_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //On 10/07/2024, Hide Voucher /Receipt Number based on setting
            if (this.ReportProperties.VoucherPrintHideVoucherReceiptNo == 1)
            {
                e.Value = string.Empty;
            }
        }

        private void xrtblCreditAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("VOUCHER_ID") != null)
            {
                XRTableCell cell = sender as XRTableCell;
                cell.Text = "Amount";
                if (settingProperty.AllowMultiCurrency == 1)
                {
                    string vouchercurrency = settingProperty.Currency;
                    if (GetCurrentColumnValue("CURRENCY_SYMBOL") != null)
                    {
                        vouchercurrency = string.IsNullOrEmpty(GetCurrentColumnValue("CURRENCY_SYMBOL").ToString()) ? settingProperty.Currency : GetCurrentColumnValue("CURRENCY_SYMBOL").ToString();
                    }
                    cell.Text = cell.Text + " (" + vouchercurrency + ")";
                }
                else
                {
                    this.SetCurrencyFormat(xrtblCreditAmt.Text, xrtblCreditAmt);
                }
            }
        }

    }
}
