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
    public partial class CashBankDonorReceipts : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settings = new SettingProperty();
        double Creditamt = 0;
        double CashBankReceiptsAmt = 0;
        #endregion

        #region Constructor
        public CashBankDonorReceipts()
        {
            InitializeComponent();
            // this.HideReportHeader = this.HidePageFooter = false;
            // BindReport();
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            BindReport();

        }
        public override void ShowPrintDialogue()
        {
            this.HideReportHeader = this.HidePageFooter = false;
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
                setHeaderTitleAlignment();

                xrlblCaptionPrint.Text = "It is a computer generated Receipt and does not require signature";
                xrlblCaptionPrint.Font = new Font(xrlblCaptionPrint.Font.FontFamily, xrlblCaptionPrint.Font.Size, FontStyle.Regular);

                if (!string.IsNullOrEmpty(ReportProperty.Current.ProjectTitle))
                {
                    string[] projectName = ReportProperty.Current.ProjectTitle.Split('-');
                    xrtcHeader.Text = projectName[0];
                }
                //lblAmountCaption.Text = this.SetCurrencyFormat(lblAmountCaption.Text);

                resultArgs = BindCashBankReceipts();
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "CashBankDonorReceipts";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }

                //Set Main and Sub Report settings
                SetReportSetting();

                base.ShowReport();
            }
            else
            {
                xrtcHeader.Text = settings.InstituteName;
                ShowFiancialReportFilterDialog();

            }
        }

        private void SetReportSetting()
        {
            //Caption Font Style
            lblVoucherNoCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblVoucherNoCaption.Font.FontFamily, lblVoucherNoCaption.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherNoCaption.Font.FontFamily, lblVoucherNoCaption.Font.Size, FontStyle.Regular);
            lblVoucherNo.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Regular);
            lblReceivedFromCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblReceivedFromCaption.Font.FontFamily, lblReceivedFromCaption.Font.Size, FontStyle.Bold)
                : new Font(lblReceivedFromCaption.Font.FontFamily, lblReceivedFromCaption.Font.Size, FontStyle.Regular);
            lblAddressCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblAddressCaption.Font.FontFamily, lblAddressCaption.Font.Size, FontStyle.Bold)
                : new Font(lblAddressCaption.Font.FontFamily, lblAddressCaption.Font.Size, FontStyle.Regular);
            lblEmailCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblEmailCaption.Font.FontFamily, lblEmailCaption.Font.Size, FontStyle.Bold)
                : new Font(lblEmailCaption.Font.FontFamily, lblEmailCaption.Font.Size, FontStyle.Regular);
            lblPhoneCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblPhoneCaption.Font.FontFamily, lblPhoneCaption.Font.Size, FontStyle.Bold)
                : new Font(lblPhoneCaption.Font.FontFamily, lblPhoneCaption.Font.Size, FontStyle.Regular);
            lblSumCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblSumCaption.Font.FontFamily, lblSumCaption.Font.Size, FontStyle.Bold)
                : new Font(lblSumCaption.Font.FontFamily, lblSumCaption.Font.Size, FontStyle.Regular);
            lblRefNoCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblRefNoCaption.Font.FontFamily, lblRefNoCaption.Font.Size, FontStyle.Bold)
                : new Font(lblRefNoCaption.Font.FontFamily, lblRefNoCaption.Font.Size, FontStyle.Regular);
            lblMaterializedOnCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblMaterializedOnCaption.Font.FontFamily, lblMaterializedOnCaption.Font.Size, FontStyle.Bold)
                : new Font(lblMaterializedOnCaption.Font.FontFamily, lblMaterializedOnCaption.Font.Size, FontStyle.Regular);
            lblDrawnBankCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblDrawnBankCaption.Font.FontFamily, lblDrawnBankCaption.Font.Size, FontStyle.Bold)
                : new Font(lblDrawnBankCaption.Font.FontFamily, lblDrawnBankCaption.Font.Size, FontStyle.Regular);
            lblTowardsCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblTowardsCaption.Font.FontFamily, lblTowardsCaption.Font.Size, FontStyle.Bold)
                : new Font(lblTowardsCaption.Font.FontFamily, lblTowardsCaption.Font.Size, FontStyle.Regular);

            //Value Font Style
            lblVoucherNo.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Bold)
                : new Font(lblVoucherNo.Font.FontFamily, lblVoucherNo.Font.Size, FontStyle.Regular);

            lblVoucherDate.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblVoucherDate.Font.FontFamily, lblVoucherDate.Font.Size, FontStyle.Bold)
               : new Font(lblVoucherDate.Font.FontFamily, lblVoucherDate.Font.Size, FontStyle.Regular);

            lblReceivedFrom.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblReceivedFrom.Font.FontFamily, lblReceivedFrom.Font.Size, FontStyle.Bold)
               : new Font(lblReceivedFrom.Font.FontFamily, lblReceivedFrom.Font.Size, FontStyle.Regular);

            lblAddress.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblAddress.Font.FontFamily, lblAddress.Font.Size, FontStyle.Bold)
               : new Font(lblAddress.Font.FontFamily, lblAddress.Font.Size, FontStyle.Regular);

            lblEmail.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblEmail.Font.FontFamily, lblEmail.Font.Size, FontStyle.Bold)
               : new Font(lblEmail.Font.FontFamily, lblEmail.Font.Size, FontStyle.Regular);

            lblPhone.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblPhone.Font.FontFamily, lblPhone.Font.Size, FontStyle.Bold)
               : new Font(lblPhone.Font.FontFamily, lblPhone.Font.Size, FontStyle.Regular);

            lblSum.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblSum.Font.FontFamily, lblSum.Font.Size, FontStyle.Bold)
               : new Font(lblSum.Font.FontFamily, lblSum.Font.Size, FontStyle.Regular);

            lblRefNo.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblRefNo.Font.FontFamily, lblRefNo.Font.Size, FontStyle.Bold)
              : new Font(lblRefNo.Font.FontFamily, lblRefNo.Font.Size, FontStyle.Regular);

            lblMaterializedOn.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblMaterializedOn.Font.FontFamily, lblMaterializedOn.Font.Size, FontStyle.Bold)
              : new Font(lblMaterializedOn.Font.FontFamily, lblMaterializedOn.Font.Size, FontStyle.Regular);

            lblDrawnBank.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lblDrawnBank.Font.FontFamily, lblDrawnBank.Font.Size, FontStyle.Bold)
              : new Font(lblDrawnBank.Font.FontFamily, lblDrawnBank.Font.Size, FontStyle.Regular);

            lbltowards.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(lbltowards.Font.FontFamily, lbltowards.Font.Size, FontStyle.Bold)
              : new Font(lbltowards.Font.FontFamily, lbltowards.Font.Size, FontStyle.Regular);
        }

        public ResultArgs BindCashBankReceipts()
        {
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.DonorReceipts);
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
        private void xrCashBankRecPayDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((CashBankReceiptPaymentDetails)((XRSubreport)sender).ReportSource).PrintVoucherId = Convert.ToInt32(GetCurrentColumnValue("VOUCHER_ID"));
            //((CashBankReceiptPaymentDetails)((XRSubreport)sender).ReportSource).currency = this.SetCurrencyFormat("",new XRTableCell());
        }

        private void lblSum_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ConvertRuppessInWord.GetRupeesToWord(Creditamt.ToString());
            e.Handled = true;
        }

        private void lblSum_SummaryReset(object sender, EventArgs e)
        {
            Creditamt = 0;
        }

        private void lblSum_SummaryRowChanged(object sender, EventArgs e)
        {
            double tmpCredit = Creditamt;
            Creditamt += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.AMOUNTColumn.ColumnName).ToString());
            string TransMode = GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.AMOUNT_TRANS_TYPEColumn.ColumnName).ToString();
            if (TransMode == "DR")
            {
                Creditamt = tmpCredit + this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.AMOUNTColumn.ColumnName).ToString());
            }
        }

        private void xrLbl80G_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string value = (GetCurrentColumnValue("EIGHTYGNO") == null) ? string.Empty : GetCurrentColumnValue("EIGHTYGNO").ToString();
            if (value != string.Empty)
            {
                xrLbl80G.Visible = true;
                xrLbl80G.Text = "ORDER UNDER SECTION 80G (5) (Vi) OF THE INCOME TAX ACT, 1961 NQ.DIT (E) I 2012-13/132," + ' ' + value + ", VALID FROM 1st APRIL 2011 ONWARDS";
            }
            else
            {
                xrLbl80G.Visible = false;
            }
        }
    }
}
