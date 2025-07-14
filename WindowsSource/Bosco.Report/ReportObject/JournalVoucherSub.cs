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
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class JournalVoucherSub : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settings = new SettingProperty();
        double Debitamt = 0;
        int count = 0;
        bool generalnarraton_exists = true;
        private Int32 NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK =7;

        private DataTable dtCCDetails = new DataTable();
        private bool PrevLedgerCCFound = false;
        #endregion

        #region Constructor
        public JournalVoucherSub()
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

        }
        public override void ShowPrintDialogue()
        {

            this.HidePageFooter = this.HideReportHeader = false;
            BindJournal();
            this.ShowPreviewDialog();
        }
        #endregion

        #region Method
        public void BindJournal(bool DuplicateCopy=false)
        {
            if (!string.IsNullOrEmpty(ReportProperties.PrintCashBankVoucherId))
            {
                if (DuplicateCopy)
                {
                    this.HideReportHeader = this.HidePageFooter = false;
                    this.HideDateRange = false;
                }
                //  this.ReportTitle = ReportProperty.Current.ReportTitle;
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
                    DataTable dtJournalVoucher  =resultArgs.DataSource.Table;
                    //On 29/02/2024, To show general narration is also, if ledger narration available
                    //dtJournalVoucher.DefaultView.RowFilter = "NARRATION <> NULL OR NARRATION <>''";
                    //generalnarraton_exists = !(dtJournalVoucher.DefaultView.Count > 0);
                    dtJournalVoucher.DefaultView.RowFilter = "GENERAL_NARRATION <> NULL OR GENERAL_NARRATION<>''";
                    generalnarraton_exists = (dtJournalVoucher.DefaultView.Count > 0);
                    dtJournalVoucher.DefaultView.RowFilter = string.Empty;

                    resultArgs.DataSource.Table.TableName = "CashBankJournal";
                    this.DataSource = dtJournalVoucher ;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                    //PageHeader1.Visible = !(resultArgs.DataSource.Table.Rows.Count > NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK);
                }
                SetReportSetting();

                //On 02/03/2021, to show sign details 
                ReportProperty.Current.IncludeSignDetails = 1;
                (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrReportTitle.WidthF;
                //Project/Common Sign details
                (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.Project);
                (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);

                base.ShowReport();
            }
            else
            {
                xrHeaderProjectName.Text = settings.InstituteName;
                ShowFiancialReportFilterDialog();

                //On 02/03/2021, to show sign details 
                ReportProperty.Current.IncludeSignDetails = 1;
                (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrReportTitle.WidthF;
                //Project/Common Sign details
                (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId =  UtilityMember.NumberSet.ToInteger(ReportProperty.Current.Project);
                (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);
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
                : new Font(xrtblcellSumofRupees.Font.FontFamily, lblSumCredit.Font.Size, FontStyle.Regular);
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
            xrtblGeneralNarration.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblGeneralNarration.Font.FontFamily, xrtbcellNarration.Font.Size, FontStyle.Bold)
                : new Font(xrtblGeneralNarration.Font.FontFamily, xrtbcellNarration.Font.Size, FontStyle.Regular);

            //Signatures
            /*
             * //On 02/03/2021, to hide Row1, Row2 and Row3 Voucher Print settings and will have common Sign Details from Finance Settings) 
             * 
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

            this.SetCurrencyFormat(lblDebitCaption.Text, lblDebitCaption);
            this.SetCurrencyFormat(lblCreditCaption.Text, lblCreditCaption);
        }
        
        public ResultArgs GetJournal()
        {
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchJournalByVoucher);
            string vids = this.ReportProperties.PrintCashBankVoucherId;
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, vids);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.CashBankVoucherDateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
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
                    xrSubreportCCDetails.LeftF = xrtblPaidto.LeftF;
                    ccDetail.ShowIndentInCostCentre = true;
                    ccDetail.CCCreditCaption = "";
                    ccDetail.CCDebitCaption = "";
                    ccDetail.CCNameWidth = ((xrtblPaidto.WidthF / 2) + (xrtblPaidto.WidthF / 4));
                    ccDetail.CCDebitWidth = xrtblPaidto.WidthF / 4;
                    ccDetail.CCCreditWidth = xrtblPaidto.WidthF / 4;
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
            if (ccFound)
            {
                xrtblPaidto.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrDebitAmount.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
            }
            else
            {
                xrtblPaidto.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                xrDebitAmount.Borders = BorderSide.Right | BorderSide.Bottom;
            }
        }

        /// <summary>
        /// On 09/10/2023, To get number of CCs in Voucher
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        private Int32 GetCCCountInVoucher(Int32 vid)
        {
            Int32 Rtn = 0;
            if (GetNextColumnValue("VOUCHER_ID") != null && this.DataSource != null && dtCCDetails.Rows.Count > 0)
            {
                dtCCDetails.DefaultView.RowFilter = "VOUCHER_ID IN (" + vid + ")"; ;
                Rtn = dtCCDetails.DefaultView.Count;
                dtCCDetails.DefaultView.RowFilter = string.Empty;
            }
            return Rtn;
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

        private void xrTblRequireSignNote_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //13/07/2021, Hide Reuire Sign note
            //xrTblRequireSignNote.Visible = !ReportProperty.Current.HideReportSignNoteInFooter;
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

        private void lblVoucherNo_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //On 10/07/2024, Hide Voucher /Receipt Number based on setting
            if (this.ReportProperties.VoucherPrintHideVoucherReceiptNo == 1)
            {
                e.Value = string.Empty;
            }
        }
    }
}
