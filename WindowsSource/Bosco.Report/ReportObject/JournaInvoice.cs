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
    public partial class JournaInvoice : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settings = new SettingProperty();
        private Int32 SerialNumber = 1;
        private double InvoiceAmount = 0;
        DataTable dtDataSource = new DataTable();
        string vouchercurrencysymbol = string.Empty;
        string vouchercurrencyname = string.Empty;
        #endregion

        #region Constructor
        public JournaInvoice()
        {
            InitializeComponent();

            vouchercurrencysymbol = settingProperty.Currency;
            vouchercurrencyname = settingProperty.CurrencyName;
            xrcellSumTotalCurrencySymbol.Text = vouchercurrencysymbol;
            // BindReport();
        }


        private bool HideReportLogoLeft
        {
            set
            {
                xrpicReportLogoLeft1.Visible = value;
                xrpicReportLogoLeft1.Image = ImageProcessing.ByteArrayToImage(settings.AcMeERPLogo);

                // To Removed By chinna on 29.09.2020 
                //if (SettingProperty.branachOfficeCode == "sdbinminmdbc")
                //{
                //    // To be set the BoscoSoft Logo
                //}
                //else
                //{
                //    xrpicReportLogoLeft1.Image = ImageProcessing.ByteArrayToImage(settings.AcMeERPLogo);
                //    //xrpicReportLogoLeft1.WidthF = 500;
                //    //xrpicReportLogoLeft1.HeightF = 80;
                //}
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

            //On 02/03/2021, to show sign details 
            ReportProperty.Current.IncludeSignDetails = 1;
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrTblHeader.WidthF;// xrReportTitle.WidthF;
            //Project/Common Sign details
            (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId = ReportProperty.Current.CashBankProjectId; //UtilityMember.NumberSet.ToInteger(ReportProperty.Current.Project);
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);
        }

        public override void ShowPrintDialogue()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            BindReport();

            //On 02/03/2021, to show sign details 
            ReportProperty.Current.IncludeSignDetails = 1;
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrTblHeader.WidthF;//xrReportTitle.WidthF;
            //Project/Common Sign details
            (xrSubSignFooter.ReportSource as SignReportFooter).ProjectId = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.Project); ;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails(true);

            // this.Print();
            //this.ShowPreviewDialog();
            this.ShowPreview();
            this.BringToFront();
        }
        #endregion

        #region Method
        private void BindReport()
        {
            if (!string.IsNullOrEmpty(ReportProperties.PrintCashBankVoucherId))
            {
                
                //04/05/2022, to set report title -----------------------------------------------------------------------------
                this.ReportProperties.HeaderInstituteSocietyName = 0;
                if (this.ReportProperties.VoucherPrintReportTitleType == "1" || this.ReportProperties.VoucherPrintReportTitleType == "0")
                {
                    this.ReportProperties.HeaderInstituteSocietyName = UtilityMember.NumberSet.ToInteger(this.ReportProperties.VoucherPrintReportTitleType);
                }
                this.ReportProperties.HeaderInstituteSocietyAddress = this.ReportProperties.HeaderInstituteSocietyName;
                //---------------------------------------------------------------------------------------------------------------
                this.DisplayName = "Invoice";

                setHeaderTitleAlignment();
                xrlblInsName.Text = settingProperty.InstituteName;
                              
                // This is to show the Logo in the Voucher View Screen  (Chinna)
                float titleLeft = xrpicReportLogoLeft1.LeftF;
                float titleWidth = xrcellBillName.WidthF;//xrTaxTitle.WidthF;

                if (settings.AcMeERPLogo!=null) //(this.ReportProperties.VoucherPrintShowLogo == "1")
                {
                    HideReportLogoLeft = true;
                    titleLeft = xrlblInsName.LeftF = xrlblInsAddress.LeftF = xrEmailAddreses.LeftF = (xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF) + 10;
                    titleWidth = xrlblInsName.WidthF = xrlblInsAddress.WidthF = xrEmailAddreses.WidthF = (titleWidth - (xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF));
                }
                else
                {
                    HideReportLogoLeft = false;
                }
                xrlblInsName.LeftF = xrlblInsAddress.LeftF = xrEmailAddreses.LeftF = xrTelephone.LeftF = titleLeft;
                xrlblInsName.WidthF = xrlblInsAddress.WidthF = xrEmailAddreses.WidthF = xrTelephone.WidthF  = titleWidth;
                //xrReportTitle.Borders = xrlblInsName.Borders = xrlblInsAddress.Borders = BorderSide.All;
                this.HideReportHeader = this.HidePageFooter = false;

                xrlblInsAddress.Text = AppSetting.Address;
                xrTelephone.Text = AppSetting.Phone;
                xrEmailAddreses.Text = AppSetting.Email;
                
                if (this.ReportProperties.PrintCashBankVoucherId == "0" && this.ReportProperties.dtGSTInvoicePrintPreview != null 
                    && this.ReportProperties.dtGSTInvoicePrintPreview.Rows.Count>0)
                {
                    resultArgs = new ResultArgs();
                    resultArgs.DataSource.Data = ReportProperties.dtGSTInvoicePrintPreview;
                    resultArgs.Success = true;
                }
                else
                {
                    resultArgs = BindGSTInvoice();
                }
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "CashBankReceipts";
                    dtDataSource = resultArgs.DataSource.Table;
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
                

                //Set Main and Sub Report settings
                SetReportSetting();
                base.ShowReport();
            }
            else
            {
                xrEmailAddreses.Text = settings.InstituteName;
                ShowFiancialReportFilterDialog();
            }
            //this.ExportToPdf(@"C:\GST_INVOICE.pdf");
        }

        private void SetReportSetting()
        {
            this.TopMarginHeight = 75;
            //Caption Font Style
            //Font fontCaption = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrcellInvoiceNoCaption.Font.FontFamily, xrcellInvoiceNoCaption.Font.Size, FontStyle.Bold)
            //    : new Font(xrcellInvoiceNoCaption.Font.FontFamily, xrcellInvoiceNoCaption.Font.Size, FontStyle.Regular);
                        
            //Caption Font Style
            Font fontCaption = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrcellInvoiceNoCaption.Font.FontFamily, 10, FontStyle.Bold) :
                                        new Font(xrcellInvoiceNoCaption.Font.FontFamily, 10, FontStyle.Regular);
            //Value Font Style
            Font fontValue = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrcellInvoiceNo.Font.FontFamily, 9, FontStyle.Bold)
                : new Font(xrcellInvoiceNo.Font.FontFamily, 9, FontStyle.Regular);

            //For Caption 
            xrcellInvoiceNoCaption.Font = xrcellInvoiceDateCaption.Font = xrcellInvoiceDueDateCaption.Font = fontCaption;
             
            //For Values 
            xrcellInvoiceNo.Font = xrcellInvoiceDate.Font = xrcellInvoiceDueDate.Font = fontValue;
            xrcellBillName.Font = xrcellBillAddress.Font = fontValue;
            xrcellBillState.Font = fontValue;
            xrcellSNo.Font = fontValue;
            xrcellItemName.Font = xrcellItemDescription.Font = xrcellSNo.Font = fontValue;
            xrcellLedgerTotalAmount.Font = fontValue;
            xrcellUnitAmount.Font = fontValue;

            
        }

        public ResultArgs BindGSTInvoice()
        {
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchJournalInvoice);
            using (DataManager dataManager = new DataManager())
            {
                //dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                //dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, this.ReportProperties.PrintCashBankVoucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                if (resultArgs.Success)
                {
                    this.ReportProperties.CashBankJouranlByVoucher = resultArgs.DataSource.Table;
                    this.ReportProperties.PrintCashBankVoucherId = string.Empty;
                }
                else
                {
                    MessageRender.ShowMessage(resultArgs.Message);
                }

            }
            return resultArgs;
        }

        #endregion

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
               

        private void xrcellAmountInRupees_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
               
        private void grpHeaderVoucher_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SerialNumber = 1;
            InvoiceAmount = 0;
        }

        //private void xrcellTotalSumLedgerAmount_EvaluateBinding(object sender, BindingEventArgs e)
        //{
        //    if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName) != null)
        //    {
        //        double rtn = GetTotalInvoiceAmount();
        //        e.Value = GetTotalInvoiceAmount(); 
        //    }
        //}
              

        private void xrCellRemarks_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.NARRATIONColumn.ColumnName) != null)
            {
                XRTableCell xrcell = sender as XRTableCell;
                string narration = GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.NARRATIONColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(narration))
                {
                    xrcell.Text = "Remarks: \"" + narration + "\"";
                }
                else
                {
                    xrcell.Text = string.Empty;
                }
            }
        }

       
        private void xrcellTC2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_NAMEColumn.ColumnName) != null)
            {
                XRTableCell xrcell = sender as XRTableCell;
                string chequename = GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.CHEQUE_IN_FAVOURColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(chequename))
                {
                    xrcell.Text += " " + chequename; //" \"" + chequename + "\"";
                }
                else
                {
                    xrcell.Text = string.Empty;
                }
            }
        }

        private void xrcellSumTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            lblAmountInWords.Text = "";
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName) != null)
            {
                e.Result = InvoiceAmount;
                e.Handled = true;
                lblAmountInWords.Text = "";
            }
        }

        private void xrcellLedgerTotalAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName) != null)
            {
                double qty = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.QUANTITYColumn.ColumnName).ToString());
                double unitprice = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.UNIT_AMOUNTColumn.ColumnName).ToString());
                InvoiceAmount += (qty * unitprice);
                e.Value = (qty * unitprice);
            }
        }

        private void xrcellSNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void lblAmountInWords_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName) != null)
            {
                XRLabel xrlbl = sender as XRLabel;
                                
                ConvertRuppessInWord.VoucherCurrencySymbol = vouchercurrencysymbol;
                ConvertRuppessInWord.VoucherCurrencyName = vouchercurrencyname;
                xrlbl.Text = ConvertRuppessInWord.GetRupeesToWord(InvoiceAmount.ToString());
            }
        }

        private void xrcellQty_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                string txt = cell.Text;
                double amt = UtilityMember.NumberSet.ToDouble(e.Value.ToString());
                double decPart = amt - Math.Truncate(amt);
                
                if (decPart > 0)
                {
                    cell.DataBindings["Text"].FormatString = "{0:n}";
                }
                                
            }
        }

        private void xrcellSumBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName) != null)
            {
                double paidamt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.PAID_AMOUNTColumn.ColumnName).ToString());
                e.Value = (InvoiceAmount - paidamt);
            }
        }

        private void xrBalanceRow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = true;
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName) != null)
            {
                double paidamt = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.PAID_AMOUNTColumn.ColumnName).ToString());
                e.Cancel = (paidamt ==0);
            }
        }

        private void xrcellSNo_EvaluateBinding(object sender, BindingEventArgs e)
        {

            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.GST_INVOICE_IDColumn.ColumnName) != null)
            {
                e.Value = SerialNumber;
                SerialNumber++;
            }
        }

    }
}
