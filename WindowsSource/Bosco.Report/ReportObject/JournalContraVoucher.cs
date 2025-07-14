using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class JournalContraVoucher : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settings = new SettingProperty();
        double Debitamt = 0;
        int count = 0;
        bool generalnarraton_exists = true;
        private Int32 NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK = 7;
        DataTable dtDataSource = new DataTable();
        Int32 voucherprojectid = 0;
        private Boolean printedwithinsignlepage = false;

        Int32 totalrecords = 0;
        public Int32 TotalRecords
        {
            get { return totalrecords; }
            set { totalrecords = value; }

        }
        #endregion

        #region Constructor
        public JournalContraVoucher()
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

            this.HidePageFooter = this.HideReportHeader = false;
            BindJournal();

            //On 02/03/2021, to show sign details 
            voucherprojectid = ReportProperty.Current.CashBankProjectId;
            ReportProperty.Current.IncludeSignDetails = 1;
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrReportTitle.WidthF;
            //Project/Common Sign details
            (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId = voucherprojectid;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);
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

            this.HidePageFooter = this.HideReportHeader = false;
            BindJournal();

            //On 02/03/2021, to show sign details 
            voucherprojectid = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.Project);
            ReportProperty.Current.IncludeSignDetails = 1;
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrReportTitle.WidthF;
            //Project/Common Sign details
            (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId = voucherprojectid;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);

            //this.ShowPreviewDialog();
            this.ShowPreview();
            this.BringToFront();
        }
        #endregion

        #region Method
        private void BindJournal()
        {
            if (!string.IsNullOrEmpty(ReportProperties.PrintCashBankVoucherId))
            {
                //this.ReportTitle = ReportProperty.Current.ReportTitle;
                xrlnDuplicatedDot.Visible = false;
                printedwithinsignlepage = false;

                //if (string.IsNullOrEmpty(this.AppSetting.DuplicateCopyVoucherPrint) || this.AppSetting.DuplicateCopyVoucherPrint == "0")
                if (string.IsNullOrEmpty(this.AppSetting.DuplicateCopyVoucherPrint) || this.AppSetting.DuplicateCopyVoucherPrint != "1")
                {
                    this.xrlnDuplicatedDot.Visible = false;
                    this.xrDuplicatePageBreak.Visible = false;
                    xrDuplicateCopy.Visible = false;
                }

                float titleLeft = xrpicReportLogoLeft1.LeftF;
                float titleWidth = xrtblLedger.WidthF;
                if (this.ReportProperties.VoucherPrintShowLogo == "1")
                {
                    HideReportLogoLeft = true;
                    titleLeft = xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF;
                    titleWidth = (xrtblLedger.WidthF - (xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF));
                }
                else
                {
                    HideReportLogoLeft = false;
                }
                xrlblInsName.LeftF = xrlblInsAddress.LeftF = xrReportTitle.LeftF = xrHeaderProjectName.LeftF = titleLeft;
                xrlblInsName.WidthF = xrlblInsAddress.WidthF = xrHeaderProjectName.WidthF = xrReportTitle.WidthF = titleWidth;
                //xrlblInsName.Borders = xrlblInsAddress.Borders = xrHedarProjectName.Borders = xrReportTitle.Borders = DevExpress.XtraPrinting.BorderSide.All;

                setHeaderTitleAlignment();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                
                xrlblInsName.Text = this.GetInstituteName();             
                //xrHedarProjectName.Text = ReportProperty.Current.ProjectTitle;
                xrlblInsAddress.Text = ReportProperty.Current.LegalAddress;
                
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

                resultArgs = GetJournal();
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtJournalVoucher  =resultArgs.DataSource.Table;;
                    dtJournalVoucher.DefaultView.RowFilter = "NARRATION <> NULL OR NARRATION <>''";
                    generalnarraton_exists = !(dtJournalVoucher.DefaultView.Count > 0);
                    dtJournalVoucher.DefaultView.RowFilter = string.Empty;

                    resultArgs.DataSource.Table.TableName = "CashBankJournal";
                    this.DataSource = dtJournalVoucher ;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                    dtDataSource = resultArgs.DataSource.Table;
                }
                SetReportSetting();
                base.ShowReport();
            }
            else
            {
                xrHeaderProjectName.Text = settings.InstituteName;
                ShowFiancialReportFilterDialog();
            }
        }

        private void SetReportSetting()
        {
            //Caption Font Style
            lblVoucherNoCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblVoucherNoCaption.Font.FontFamily, lblVoucherNoCaption.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherNoCaption.Font.FontFamily, lblVoucherNoCaption.Font.Size, FontStyle.Regular);
            lblVoucherDateCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblVoucherDateCaption.Font.FontFamily, lblVoucherDateCaption.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherDateCaption.Font.FontFamily, lblVoucherDateCaption.Font.Size, FontStyle.Regular);
            lblParticularsCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblParticularsCaption.Font.FontFamily, lblParticularsCaption.Font.Size, FontStyle.Bold)
                : new Font(lblParticularsCaption.Font.FontFamily, lblParticularsCaption.Font.Size, FontStyle.Regular);
            lblDebitCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblDebitCaption.Font.FontFamily, lblDebitCaption.Font.Size, FontStyle.Bold)
                : new Font(lblDebitCaption.Font.FontFamily, lblDebitCaption.Font.Size, FontStyle.Regular);
            lblCreditCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblCreditCaption.Font.FontFamily, lblCreditCaption.Font.Size, FontStyle.Bold)
                : new Font(lblCreditCaption.Font.FontFamily, lblCreditCaption.Font.Size, FontStyle.Regular);
            lblTotalCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblTotalCaption.Font.FontFamily, lblTotalCaption.Font.Size, FontStyle.Bold)
                : new Font(lblTotalCaption.Font.FontFamily, lblTotalCaption.Font.Size, FontStyle.Regular);

            //lblSumDebit.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblSumDebit.Font.FontFamily, lblSumDebit.Font.Size, FontStyle.Bold)
            //    : new Font(lblSumDebit.Font.FontFamily, lblSumDebit.Font.Size, FontStyle.Regular);
            //lblSumCredit.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblSumCredit.Font.FontFamily, lblSumCredit.Font.Size, FontStyle.Bold)
            //    : new Font(lblSumCredit.Font.FontFamily, lblSumCredit.Font.Size, FontStyle.Regular);
            xrtblcellSumofRupees.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrtblcellSumofRupees.Font.FontFamily, xrtblcellSumofRupees.Font.Size, FontStyle.Bold)
                : new Font(xrtblcellSumofRupees.Font.FontFamily, xrtblcellSumofRupees.Font.Size, FontStyle.Regular);
            xrtblcellbeing.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrtblcellbeing.Font.FontFamily, xrtblcellbeing.Font.Size, FontStyle.Bold)
                : new Font(xrtblcellbeing.Font.FontFamily, xrtblcellbeing.Font.Size, FontStyle.Regular);
            
            //Value Font Style
            lblVoucherNo.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Regular);
            lblVoucherDate.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblVoucherDate.Font.FontFamily, lblVoucherDate.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherDate.Font.FontFamily, lblVoucherDate.Font.Size, FontStyle.Regular);
            xrtblPaidto.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblPaidto.Font.FontFamily, xrtblPaidto.Font.Size, FontStyle.Bold)
                : new Font(xrtblPaidto.Font.FontFamily, xrtblPaidto.Font.Size, FontStyle.Regular);
            xrDebitAmount.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrDebitAmount.Font.FontFamily, xrDebitAmount.Font.Size, FontStyle.Bold)
                : new Font(xrDebitAmount.Font.FontFamily, xrDebitAmount.Font.Size, FontStyle.Regular);
            xrCreditAmount.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrCreditAmount.Font.FontFamily, xrCreditAmount.Font.Size, FontStyle.Bold)
               : new Font(xrCreditAmount.Font.FontFamily, xrCreditAmount.Font.Size, FontStyle.Regular);
            xrtblRuppee.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblRuppee.Font.FontFamily, xrtblRuppee.Font.Size, FontStyle.Bold)
               : new Font(xrtblRuppee.Font.FontFamily, xrtblRuppee.Font.Size, FontStyle.Regular);
            xrtbcellNarration.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtbcellNarration.Font.FontFamily, xrtbcellNarration.Font.Size, FontStyle.Bold)
                : new Font(xrtbcellNarration.Font.FontFamily, xrtbcellNarration.Font.Size, FontStyle.Regular);
            xrtblGeneralNarration.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblGeneralNarration.Font.FontFamily, xrtblGeneralNarration.Font.Size, FontStyle.Bold)
                : new Font(xrtblGeneralNarration.Font.FontFamily, xrtblGeneralNarration.Font.Size, FontStyle.Regular);
            
            //Signatures
            /*
            if (string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign1Row1) && string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign1Row2) &&
                string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign2Row1) && string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign2Row2))
            {
                lblSign1.Borders = lblSign3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            }
            else
            {
                lblSign1.Borders = lblSign3.Borders = DevExpress.XtraPrinting.BorderSide.All;
            }

            lblSign1.Text = this.ReportProperties.VoucherPrintSign1Row1 + Environment.NewLine + this.ReportProperties.VoucherPrintSign1Row2;
            lblSign2.Text = this.ReportProperties.VoucherPrintSign2Row1 + Environment.NewLine + this.ReportProperties.VoucherPrintSign2Row2;
            lblSign3.Text = this.ReportProperties.VoucherPrintSign3Row1 + Environment.NewLine + this.ReportProperties.VoucherPrintSign3Row2;*/

            //this.SetCurrencyFormat(lblDebitCaption.Text, lblDebitCaption);
            //this.SetCurrencyFormat(lblCreditCaption.Text, lblCreditCaption);
        }

        public ResultArgs GetJournal()
        {
            //string JournalVoucher = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.JournalVoucher);
            //using (DataManager dataManager = new DataManager())
            //{
            //    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
            //    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
            //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
            //    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, JournalVoucher);
            //}
            //return resultArgs;
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchJournalContraByVoucher);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, this.ReportProperties.PrintCashBankVoucherId);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.CashBankVoucherDateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
                this.ReportProperties.PrintCashBankVoucherId = string.Empty;
            }
            return resultArgs;
        }
        #endregion

        private void xrtblRuppee_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName) != null)
                Debitamt += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.LedgerParameters.DEBITColumn.ColumnName).ToString());
        }

        private void xrtblRuppee_SummaryReset(object sender, EventArgs e)
        {
            Debitamt = 0;
        }

        private void xrtblRuppee_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrDebitAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double DebitAmount = this.ReportProperties.NumberSet.ToDouble(xrDebitAmount.Text);
            xrDebitAmount.Text = ReportProperty.Current.NumberSet.ToNumber(DebitAmount);
            if (DebitAmount != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrDebitAmount.Text = "";
            }
        }

        private void xrCreditAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CreditAmount = this.ReportProperties.NumberSet.ToDouble(xrCreditAmount.Text);
            xrCreditAmount.Text = ReportProperty.Current.NumberSet.ToNumber(CreditAmount);
            if (CreditAmount != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCreditAmount.Text = "";
            }
        }

        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double sumdebit = ReportProperty.Current.NumberSet.ToDouble(xrTableCell8.Text);
            //xrTableCell18.Text = ReportProperty.Current.NumberSet.ToCurrency(sumdebit);
        }

        private void xrTableCell18_SummaryRowChanged(object sender, EventArgs e)
        {
            //double sumdebit = ReportProperty.Current.NumberSet.ToDouble(xrTableCell8.Text);
            //xrTableCell18.Text = ReportProperty.Current.NumberSet.ToCurrency(sumdebit);
        }

        private void xrTableCell18_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //double sumdebit = ReportProperty.Current.NumberSet.ToDouble(xrTableCell8.Text);
            //xrTableCell18.Text = ReportProperty.Current.NumberSet.ToCurrency(sumdebit);
        }
        
        private void xrtblRowNarration_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrtblRowNarration.Visible = (Narration != string.Empty); 
        }

        private void xrTable3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrrowgeneralnarration.Visible = generalnarraton_exists;
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

        private void xrDuplicateCopy_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.AppSetting.DuplicateCopyVoucherPrint == "1")
            {
                this.ReportProperties.PrintCashBankVoucherId = GetCurrentColumnValue("VOUCHER_ID").ToString();
                this.ReportProperties.Project = voucherprojectid.ToString();
                ((JournalContraVoucherSub)((XRSubreport)sender).ReportSource).BindJournal(true);
            }
        }

        private Int32 GetLedgersCountInVoucher()
        {
            Int32 Rtn = 0;
            if (GetCurrentColumnValue("VOUCHER_ID") != null && this.dtDataSource != null)
            {
                string voucherid = GetCurrentColumnValue("VOUCHER_ID").ToString();
                this.dtDataSource.DefaultView.RowFilter = "VOUCHER_ID IN (" + voucherid + ")"; ;
                Rtn = this.dtDataSource.DefaultView.Count;
                this.dtDataSource.DefaultView.RowFilter = string.Empty;
            }
            return Rtn;
        }

        private Int32 GetLedgersCountInNextVoucher()
        {
            Int32 Rtn = 0;
            if (GetNextColumnValue("VOUCHER_ID") != null && this.dtDataSource != null)
            {
                string voucherid = GetNextColumnValue("VOUCHER_ID").ToString();
                this.dtDataSource.DefaultView.RowFilter = "VOUCHER_ID IN (" + voucherid + ")"; ;
                Rtn = this.dtDataSource.DefaultView.Count;
                this.dtDataSource.DefaultView.RowFilter = string.Empty;
            }
            return Rtn;
        }

        private void SetCurrency(object sender, string caption)
        {
            if (GetCurrentColumnValue("VOUCHER_ID") != null)
            {
                XRTableCell cell = sender as XRTableCell;
                cell.Text = caption;
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
                    this.SetCurrencyFormat(cell.Text, cell);
                }
            }
        }

        private void xrTblRequireSignNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //13/07/2021, Hide Reuire Sign note
            //xrTblRequireSignNote.Visible = !ReportProperty.Current.HideReportSignNoteInFooter;
        }

        private void grpFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void lblVoucherNo_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //On 10/07/2024, Hide Voucher /Receipt Number based on setting
            if (this.ReportProperties.VoucherPrintHideVoucherReceiptNo == 1)
            {
                e.Value = string.Empty;
            }
        }

        private void lblDebitCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetCurrency(sender, "Debit");
        }
               

        private void lblCreditCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SetCurrency(sender, "Credit");
        }
    }
}
