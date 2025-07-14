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
    public partial class CashBankPayments : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        double Debitamt = 0;
        DataTable dtPayments = new DataTable();
        SettingProperty settings = new SettingProperty();
        double CashBankReceiptsAmt = 0;
        
        private Int32 NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK = 4;
        DataTable dtDataSource = new DataTable();
        private Int32 voucherprojectid = 0;
        private bool printedwithinsignlepage = false;

        private DataTable dtCCDetails = new DataTable();
        private bool PrevLedgerCCFound = false;
        #endregion

        #region Construnctor
        public CashBankPayments()
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
            // This is to show the Logo in the Voucher View Screen  (Chinna)
            float titleLeft =  xrpicReportLogoLeft1.LeftF;
            float titleWidth = xrCashBankRecPayDetails.WidthF;
            if (this.ReportProperties.VoucherPrintShowLogo == "1")
            {
                HideReportLogoLeft = true;
                titleLeft =  xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF;
                titleWidth = (xrCashBankRecPayDetails.WidthF - (xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF));
            }
            else
            {
                HideReportLogoLeft = false;
            }
            xrlblInstituteName.LeftF = xrlblInstituteAddress.LeftF = xrReportTitle.LeftF = xrHeaderProjectName.LeftF = titleLeft;
            xrlblInstituteName.WidthF = xrlblInstituteAddress.WidthF = xrHeaderProjectName.WidthF = xrReportTitle.WidthF = titleWidth;
            //xrlblInstituteName.Borders = xrlblInstituteAddress.Borders = xrHeaderProjectName.Borders = xrReportTitle.Borders = DevExpress.XtraPrinting.BorderSide.All;
            this.HideReportHeader = this.HidePageFooter = false;
                       
            BindReport();

            //On 01/03/2021, to show sign details 
            voucherprojectid = ReportProperty.Current.CashBankProjectId;
            ReportProperty.Current.IncludeSignDetails = 1;
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrTblHeader.WidthF; // xrReportTitle.WidthF;
            //Project/Common Sign details
            (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId = voucherprojectid;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);

            //base.ShowReport();
        }
        public override void ShowPrintDialogue()
        {
            //04/05/2022, to set report title -----------------------------------------------------------------------------
            this.ReportProperties.HeaderInstituteSocietyName = 0;
            if (this.ReportProperties.VoucherPrintReportTitleType == "1" || this.ReportProperties.VoucherPrintReportTitleType == "0")
            {
                this.ReportProperties.HeaderInstituteSocietyName = UtilityMember.NumberSet.ToInteger(this.ReportProperties.VoucherPrintReportTitleType);
            }
            this.ReportProperties.HeaderInstituteSocietyAddress = this.ReportProperties.HeaderInstituteSocietyName;
            //---------------------------------------------------------------------------------------------------------------

            //18/11/2024, to set report address -----------------------------------------------------------------------------
            //this.ReportProperties.HeaderInstituteSocietyAddress = 0;
            if (this.ReportProperties.VoucherPrintReportTitleAddress == "1" || this.ReportProperties.VoucherPrintReportTitleAddress == "0")
            {
                this.ReportProperties.HeaderInstituteSocietyAddress = UtilityMember.NumberSet.ToInteger(this.ReportProperties.VoucherPrintReportTitleAddress);
            }
            //---------------------------------------------------------------------------------------------------------------

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

            this.HideReportHeader = this.HidePageFooter = false;
            this.HideDateRange = false;
            BindReport();

            //On 01/03/2021, to show sign details 
            voucherprojectid = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.Project);
            ReportProperty.Current.IncludeSignDetails = 1; //On 30/0/2023, to fix sing from voucher print
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrTblHeader.WidthF; // xrReportTitle.WidthF;
            //Project/Common Sign details
            (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId = voucherprojectid;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);
            
            //this.ShowPreviewDialog();
            this.ShowPreview();
            this.BringToFront();
        }
        #endregion

        #region Methods


        public void BindReport()
        {
            if (!string.IsNullOrEmpty(ReportProperties.PrintCashBankVoucherId))
            {
                xrlnDuplicatedDot.Visible = false;
                printedwithinsignlepage = false;

                //if (string.IsNullOrEmpty(this.AppSetting.DuplicateCopyVoucherPrint) || this.AppSetting.DuplicateCopyVoucherPrint == "0")
                if (string.IsNullOrEmpty(this.AppSetting.DuplicateCopyVoucherPrint) || this.AppSetting.DuplicateCopyVoucherPrint != "1")
                {
                    this.xrlnDuplicatedDot.Visible = false;
                    this.xrDuplicatePageBreak.Visible = false;
                    xrDuplicateCopy.Visible = false;
                }
                              
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
                xrTblRequireSignNote.Visible = !ReportProperty.Current.HideReportSignNoteInFooter;
                                                
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
                    dtDataSource = resultArgs.DataSource.Table;
                }

                // Sub Report for Cash/Bank Amount
                CashBankReceiptPaymentDetails cashbankrec = xrCashBankRecPayDetails.ReportSource as CashBankReceiptPaymentDetails;
                cashbankrec.BindCashBankReceiptsDetails();

                //Set Main Report and Sub Report settings
                SetReportSetting(cashbankrec);
                base.ShowReport();
            }
            else
            {
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
            xrcellLedgerName.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrcellLedgerName.Font.FontFamily, xrcellLedgerName.Font.Size, FontStyle.Bold)
                : new Font(xrcellLedgerName.Font.FontFamily, xrcellLedgerName.Font.Size, FontStyle.Regular);
            xrcellLedgerAmount.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrcellLedgerAmount.Font.FontFamily, xrcellLedgerAmount.Font.Size, FontStyle.Bold)
                : new Font(xrcellLedgerAmount.Font.FontFamily, xrcellLedgerAmount.Font.Size, FontStyle.Regular);
            lblReceivedFrom.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblReceivedFrom.Font.FontFamily, lblReceivedFrom.Font.Size, FontStyle.Bold)
               : new Font(lblReceivedFrom.Font.FontFamily, lblReceivedFrom.Font.Size, FontStyle.Regular);
            xrtblAmtInWords.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblAmtInWords.Font.FontFamily, xrtblAmtInWords.Font.Size, FontStyle.Bold)
               : new Font(xrtblAmtInWords.Font.FontFamily, xrtblAmtInWords.Font.Size, FontStyle.Regular);
            //lblDrawnOn.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblDrawnOn.Font.FontFamily, lblDrawnOn.Font.Size, FontStyle.Bold)
            //   : new Font(lblDrawnOn.Font.FontFamily, lblDrawnOn.Font.Size, FontStyle.Regular);
            lblNarration.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblNarration.Font.FontFamily, lblNarration.Font.Size, FontStyle.Bold)
               : new Font(lblNarration.Font.FontFamily, lblNarration.Font.Size, FontStyle.Regular);

            /*//Signatures
            //On 02/03/2021, to hide Row1, Row2 and Row3 Voucher Print settings and will have common Sign Details from Finance Settings) 
             * 
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

        /// <summary>
        /// On 09/10/2021, to get CC deatils for given project and date range
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
                    xrSubreportCCDetails.LeftF = xrcellLedgerName.LeftF ;
                    ccDetail.ShowIndentInCostCentre = true;
                    ccDetail.CCCreditCaption = "";
                    ccDetail.CCDebitCaption = "";
                    ccDetail.CCNameWidth = ((xrcellLedgerName.WidthF / 2) + (xrcellLedgerName.WidthF / 4));
                    ccDetail.CCDebitWidth = xrcellLedgerName.WidthF/4;
                    ccDetail.CCCreditWidth = xrcellLedgerName.WidthF/4;
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

        private void ProperBorderForLedgerRow(bool ccFound)
        {
            if (ccFound )
            {
                xrcellLedgerName.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellLedgerAmount.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
            }
            else
            {
                xrcellLedgerName.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrcellLedgerAmount.Borders = BorderSide.Right | BorderSide.Bottom;
            }
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
            
            if (xrcellLedgerAmount != null)
            {
                e.Cancel = false;
            }
            else
            {
                xrcellLedgerAmount.Text = "";
            }

        }

        private void xrtblAmtInWords_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            //On 26/10/2024, To Set currency symbol -----------------------------------------------------------------------------------------
            if (settingProperty.AllowMultiCurrency == 1)
            {
                string vouchercurrencysymbol = settingProperty.Currency;
                string vouchercurrencyname = settingProperty.CurrencyName;
                if (GetCurrentColumnValue(reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName) != null &&
                    GetCurrentColumnValue(reportSetting1.Country.CURRENCY_SYMBOLColumn.ColumnName) != null)
                {
                    vouchercurrencysymbol = string.IsNullOrEmpty(GetCurrentColumnValue(reportSetting1.Country.CURRENCY_SYMBOLColumn.ColumnName).ToString()) ? settingProperty.Currency :
                            GetCurrentColumnValue(reportSetting1.Country.CURRENCY_SYMBOLColumn.ColumnName).ToString();
                    vouchercurrencyname = string.IsNullOrEmpty(GetCurrentColumnValue(reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName).ToString()) ? settingProperty.Currency :
                            GetCurrentColumnValue(reportSetting1.Country.CURRENCY_NAMEColumn.ColumnName).ToString();
                }
                ConvertRuppessInWord.VoucherCurrencySymbol = vouchercurrencysymbol;
                ConvertRuppessInWord.VoucherCurrencyName = vouchercurrencyname;
            }
            //-------------------------------------------------------------------------------------------------------------------------------

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
            ((CashBankReceiptPaymentDetails)((XRSubreport)sender).ReportSource).ColumnThroughWidth = lblReceivedFromCaption.WidthF-1;
        }

        private void xrDuplicateCopy_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.AppSetting.DuplicateCopyVoucherPrint == "1")
            {
                this.ReportProperties.PrintCashBankVoucherId = GetCurrentColumnValue("VOUCHER_ID").ToString();
                this.ReportProperties.ProjectId = voucherprojectid.ToString();
                ((CashBankPaymentsSub)((XRSubreport)sender).ReportSource).BindReport(true);
            }
        }

        private void xrDuplicatePageBreak_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //If singe voucher containts more that 8 Records, it will break to next page
            if (this.AppSetting.DuplicateCopyVoucherPrint == "1")
            {
                this.xrDuplicatePageBreak.Visible = (GetLedgersCountInVoucher() > NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK);
            }
        }

        private void xrlnDuplicatedDot_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.AppSetting.DuplicateCopyVoucherPrint == "1")
            {
                xrlnDuplicatedDot.Visible = !(GetLedgersCountInVoucher() > NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK);
            }
        }

        private Int32 GetLedgersCountInVoucher()
        {
            Int32 Rtn = 0;
            if (GetCurrentColumnValue("VOUCHER_ID") != null && this.dtDataSource != null)
            {
                Int32 voucherid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("VOUCHER_ID").ToString());
                this.dtDataSource.DefaultView.RowFilter = "VOUCHER_ID IN (" + voucherid + ")"; ;
                Rtn = this.dtDataSource.DefaultView.Count + GetCCCountInVoucher(voucherid);
                this.dtDataSource.DefaultView.RowFilter = string.Empty;
            }
            return Rtn;
        }

        private Int32 GetLedgersCountInNextVoucher()
        {
            Int32 Rtn = 0;
            if (GetNextColumnValue("VOUCHER_ID") != null && this.dtDataSource != null)
            {
                Int32 voucherid = UtilityMember.NumberSet.ToInteger(GetNextColumnValue("VOUCHER_ID").ToString());
                this.dtDataSource.DefaultView.RowFilter = "VOUCHER_ID IN (" + voucherid + ")"; ;
                Rtn = this.dtDataSource.DefaultView.Count + GetCCCountInVoucher(voucherid);
                this.dtDataSource.DefaultView.RowFilter = string.Empty;
            }
            return Rtn;
        }

        /// <summary>
        /// On 09/10/2023, To get number of CCs in Voucher
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        private Int32 GetCCCountInVoucher(Int32 vid)
        {
            Int32 Rtn = 0;
            if (GetNextColumnValue("VOUCHER_ID") != null && this.dtDataSource != null && dtCCDetails.Rows.Count > 0)
            {
                dtCCDetails.DefaultView.RowFilter = "VOUCHER_ID IN (" + vid + ")"; ;
                Rtn = dtCCDetails.DefaultView.Count;
                dtCCDetails.DefaultView.RowFilter = string.Empty;
            }
            return Rtn;
        }

        private void xrTblRequireSignNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //13/07/2021, Hide Reuire Sign note
            //xrTblRequireSignNote.Visible = !ReportProperty.Current.HideReportSignNoteInFooter;
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

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.AppSetting.DuplicateCopyVoucherPrint == "2")
            {
                if (GetCurrentColumnValue("VOUCHER_ID") != null && this.dtDataSource != null)
                {
                    GroupFooterBand grpFooter = sender as GroupFooterBand;
                    Int32 NoOfVouchersLedgters = GetLedgersCountInVoucher();
                    Int32 NoOfNextVouchersLedgters = GetLedgersCountInNextVoucher();

                    /*for (int i = 1; i < xrTblNarration.Rows.Count; i++)
                    {
                        xrTblNarration.Rows.Remove(xrTblNarration.Rows[i]);
                    }*/

                    if (!printedwithinsignlepage && NoOfVouchersLedgters <= NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK
                        && NoOfNextVouchersLedgters <= NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK)
                    {
                        Int32 NoOfEmptyRows = (NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK - NoOfVouchersLedgters);
                        if (NoOfEmptyRows > 0)
                        {
                            /*for (int i = 1; i <= NoOfEmptyRows; i++)
                            {
                                XRTableRow emptyrow = new XRTableRow();
                                XRTableCell cell = new XRTableCell();
                                emptyrow.Borders = BorderSide.None;
                                cell.Text = " ";
                                emptyrow.Cells.Add(cell);
                                emptyrow.HeightF = xrTblNarration.Rows[0].HeightF;
                                xrTblNarration.Rows.Add(emptyrow);
                            }*/
                        }
                        xrVouhcerPageBreak.Visible = false;
                        printedwithinsignlepage = true;
                        xrlnDuplicatedDot.Visible = true;
                    }
                    else
                    {
                        xrVouhcerPageBreak.Visible = true;
                        printedwithinsignlepage = false;
                        xrlnDuplicatedDot.Visible = false;
                    }
                }
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
                    if (GetCurrentColumnValue(reportSetting1.Country.CURRENCY_SYMBOLColumn.ColumnName) != null)
                    {
                        vouchercurrency = string.IsNullOrEmpty(GetCurrentColumnValue(reportSetting1.Country.CURRENCY_SYMBOLColumn.ColumnName).ToString()) ? settingProperty.Currency :
                                        GetCurrentColumnValue(reportSetting1.Country.CURRENCY_SYMBOLColumn.ColumnName).ToString();
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
