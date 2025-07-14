using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.Utility;
using Bosco.Report.Base;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Data;
using DevExpress.XtraPrinting;
namespace Bosco.Report.ReportObject
{
    public partial class BoscoSoftCashBankReceipts : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settings = new SettingProperty();
        double Creditamt = 0;
        double CashBankReceiptsAmt = 0;
        #endregion

        #region Constructor
        public BoscoSoftCashBankReceipts()
        {
            InitializeComponent();
            // BindReport();
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



        }
        public override void ShowPrintDialogue()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            this.HideReportSubTitle = false;
            BindReport();

            // this.Print();
            this.ShowPreviewDialog();
        }
        #endregion

        #region Method
        private void BindReport()
        {
            if (!string.IsNullOrEmpty(ReportProperties.PrintCashBankVoucherId))
            {
                // this.ReportTitle = ReportProperty.Current.ReportTitle;
                //  this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                setHeaderTitleAlignment();

                //if (this.ReportProperties.isShowProjectTitle)
                //{
                //if (!string.IsNullOrEmpty(ReportProperty.Current.ProjectTitle))
                //{
                //    string[] projectName = ReportProperty.Current.ProjectTitle.Split('-');
                //    xrlblInsName.Text = projectName[0];
                //}
                //xrlblInsAddress.Text = (string.IsNullOrEmpty(this.GetInstituteName()) ? settings.InstituteName : this.GetInstituteName());
                //xrHeaderProjectName.Text = (string.IsNullOrEmpty(ReportProperty.Current.LegalAddress) ? settings.Address : ReportProperty.Current.LegalAddress);
                //}
                //else
                //{


                //if (!string.IsNullOrEmpty(ReportProperty.Current.ProjectTitle))
                //{
                //    string[] projectName = ReportProperty.Current.ProjectTitle.Split('-');
                //    xrHeaderProjectName.Text = projectName[0];

                //}
                xrlblInsName.Text = this.GetInstituteName();
                xrHeaderProjectName.Text = ""; // ReportProperty.Current.ProjectTitle;
                xrlblInsAddress.Text = ReportProperty.Current.LegalAddress;
                //  }


                //xrlblInstituteName.Text =  ReportProperty.Current.InstituteName;
                //   ReportProperty.Current.ProjectId = ReportProperty.Current.CashBankProjectId.ToString();

                CashBankReceiptPaymentDetails cashbankrec = xrCashBankRecPayDetails.ReportSource as CashBankReceiptPaymentDetails;
                cashbankrec.BindCashBankReceiptsDetails();

                resultArgs = BindCashBankReceipts();
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "CashBankReceipts";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }

                //Set Main and Sub Report settings
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
            lblFundProjectcodeCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblFundProjectcodeCaption.Font.FontFamily, lblFundProjectcodeCaption.Font.Size, FontStyle.Bold)
                : new Font(lblFundProjectcodeCaption.Font.FontFamily, lblFundProjectcodeCaption.Font.Size, FontStyle.Regular);
            lblVoucherDateCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblVoucherDateCaption.Font.FontFamily, lblVoucherDateCaption.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherDateCaption.Font.FontFamily, lblVoucherDateCaption.Font.Size, FontStyle.Regular);
            lblParticularsCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblParticularsCaption.Font.FontFamily, lblParticularsCaption.Font.Size, FontStyle.Bold)
                : new Font(lblParticularsCaption.Font.FontFamily, lblParticularsCaption.Font.Size, FontStyle.Regular);
            lblAmountCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblAmountCaption.Font.FontFamily, lblAmountCaption.Font.Size, FontStyle.Bold)
                : new Font(lblAmountCaption.Font.FontFamily, lblAmountCaption.Font.Size, FontStyle.Regular);
            lblReceivedFromCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblReceivedFromCaption.Font.FontFamily, lblReceivedFromCaption.Font.Size, FontStyle.Bold)
                : new Font(lblReceivedFromCaption.Font.FontFamily, lblReceivedFromCaption.Font.Size, FontStyle.Regular);
            lblSumCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblSumCaption.Font.FontFamily, lblSumCaption.Font.Size, FontStyle.Bold)
                : new Font(lblSumCaption.Font.FontFamily, lblSumCaption.Font.Size, FontStyle.Regular);
            //lblDrawnOnCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblDrawnOnCaption.Font.FontFamily, lblDrawnOnCaption.Font.Size, FontStyle.Bold)
            //    : new Font(lblDrawnOnCaption.Font.FontFamily, lblDrawnOnCaption.Font.Size, FontStyle.Regular);
            lblNarrationCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblNarrationCaption.Font.FontFamily, lblNarrationCaption.Font.Size, FontStyle.Bold)
                : new Font(lblNarrationCaption.Font.FontFamily, lblNarrationCaption.Font.Size, FontStyle.Regular);

            //Value Font Style
            lblVoucherNo.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Regular);
            lblProjectCode.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblProjectCode.Font.FontFamily, lblProjectCode.Font.Size, FontStyle.Bold)
                : new Font(lblProjectCode.Font.FontFamily, lblProjectCode.Font.Size, FontStyle.Regular);
            lblVoucherDate.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblVoucherDate.Font.FontFamily, lblVoucherDate.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherDate.Font.FontFamily, lblVoucherDate.Font.Size, FontStyle.Regular);
            xrtblLedger.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblLedger.Font.FontFamily, xrtblLedger.Font.Size, FontStyle.Bold)
                : new Font(xrtblLedger.Font.FontFamily, xrtblLedger.Font.Size, FontStyle.Regular);
            xrtblProFundCode.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrtblProFundCode.Font.FontFamily, xrtblProFundCode.Font.Size, FontStyle.Bold)
                : new Font(xrtblProFundCode.Font.FontFamily, xrtblProFundCode.Font.Size, FontStyle.Regular);
            lblReceivedFrom.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblReceivedFrom.Font.FontFamily, lblReceivedFrom.Font.Size, FontStyle.Bold)
               : new Font(lblReceivedFrom.Font.FontFamily, lblReceivedFrom.Font.Size, FontStyle.Regular);
            lblSum.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblSum.Font.FontFamily, lblSum.Font.Size, FontStyle.Bold)
               : new Font(lblSum.Font.FontFamily, lblSum.Font.Size, FontStyle.Regular);
            //lblDrawnOn.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblDrawnOn.Font.FontFamily, lblDrawnOn.Font.Size, FontStyle.Bold)
            //   : new Font(lblDrawnOn.Font.FontFamily, lblDrawnOn.Font.Size, FontStyle.Regular);
            lblNarration.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblNarration.Font.FontFamily, lblNarration.Font.Size, FontStyle.Bold)
               : new Font(lblNarration.Font.FontFamily, lblNarration.Font.Size, FontStyle.Regular);

            //Signatures
            if (string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign1Row1) && string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign1Row2) &&
                string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign2Row1) && string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign2Row2) &&
                string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign3Row1) && string.IsNullOrEmpty(this.ReportProperties.VoucherPrintSign3Row2))
            {
                lblSign1.Borders = lblSign2.Borders = lblSign3.Borders = BorderSide.Top;
            }
            else
            {
                lblSign1.Borders = lblSign2.Borders = lblSign3.Borders = BorderSide.All;
            }

            lblSign1.Text = this.ReportProperties.VoucherPrintSign1Row1 + Environment.NewLine + this.ReportProperties.VoucherPrintSign1Row2;
            lblSign2.Text = this.ReportProperties.VoucherPrintSign2Row1 + Environment.NewLine + this.ReportProperties.VoucherPrintSign2Row2;
            lblSign3.Text = this.ReportProperties.VoucherPrintSign3Row1 + Environment.NewLine + this.ReportProperties.VoucherPrintSign3Row2;

            //Set Sub Report column width
            cashbankrec.CodeLedgerWidth = lblParticularsCaption.WidthF - cashbankrec.ColumnThroughWidth;
            cashbankrec.CodeLedgerAmountWidth = xrtblProFundCode.WidthF; //323.90f;

            this.SetCurrencyFormat(lblAmountCaption.Text, lblAmountCaption);
        }

        public ResultArgs BindCashBankReceipts()
        {
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchcashBankByVoucher);
            using (DataManager dataManager = new DataManager())
            {
                //dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                //dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, this.ReportProperties.PrintCashBankVoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
                this.ReportProperties.PrintCashBankVoucherId = string.Empty;

            }
            return resultArgs;
        }


        #endregion

        private void xrtblAmtInWords_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ConvertRuppessInWord.GetRupeesToWord(Creditamt.ToString());
            e.Handled = true;
        }

        private void xrtblAmtInWords_SummaryReset(object sender, EventArgs e)
        {
            Creditamt = 0;
        }

        private void xrtblAmtInWords_SummaryRowChanged(object sender, EventArgs e)
        {
            double tmpCredit = Creditamt;
            Creditamt += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.AMOUNTColumn.ColumnName).ToString());
            string TransMode = GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.AMOUNT_TRANS_TYPEColumn.ColumnName).ToString();
            if (TransMode == "DR")
            {
                Creditamt = tmpCredit + this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.AMOUNTColumn.ColumnName).ToString());
            }
        }

        private void xrtblProFundCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //CashBankReceiptsAmt = this.ReportProperties.NumberSet.ToDouble(xrtblProFundCode.Text);
            //xrtblProFundCode.Text = ReportProperty.Current.NumberSet.ToCurrency(CashBankReceiptsAmt);
            //if (CashBankReceiptsAmt != 0)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    xrtblProFundCode.Text = "";
            //}
            string TransMode = GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.AMOUNT_TRANS_TYPEColumn.ColumnName).ToString();
            
            if (xrtblProFundCode != null)
            {
                e.Cancel = false;
            }
            else
            {
                xrtblProFundCode.Text = "";
            }
        }

        private void xrCashBankRecPayDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((CashBankReceiptPaymentDetails)((XRSubreport)sender).ReportSource).PrintVoucherId = Convert.ToInt32(GetCurrentColumnValue("VOUCHER_ID"));
            //((CashBankReceiptPaymentDetails)((XRSubreport)sender).ReportSource).currency = this.SetCurrencyFormat("",new XRTableCell());

        }
    }
}
