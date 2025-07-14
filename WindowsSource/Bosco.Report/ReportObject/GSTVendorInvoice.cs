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
    public partial class GSTVendorInvoice : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        SettingProperty settings = new SettingProperty();
        private Int32 SerialNumber = 1;
        private Int32 NO_OF_LEDGERS_IN_VOUCHER_BEFORE_PAGEBREAK = 4; //5;
        DataTable dtDataSource = new DataTable();
        
        #endregion

        #region Constructor
        public GSTVendorInvoice()
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

                setHeaderTitleAlignment();
                xrlblInsName.Text = this.GetInstituteName();

                if (this.ReportProperties.VoucherPrintProject == "1")
                {
                    xrHeaderProjectName.Text = ReportProperty.Current.ProjectTitle;
                }
                else
                {
                    xrHeaderProjectName.Text = "";
                }

                // This is to show the Logo in the Voucher View Screen  (Chinna)
                float titleLeft = xrpicReportLogoLeft1.LeftF;
                float titleWidth = xrTaxTitle.WidthF;
                if (this.ReportProperties.VoucherPrintShowLogo == "1")
                {
                    HideReportLogoLeft = true;
                    titleLeft = xrlblInsName.LeftF = xrlblInsAddress.LeftF = xrlblInsLegalDetails.LeftF = xrHeaderProjectName.LeftF =  (xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF);
                    titleWidth = xrlblInsName.WidthF = xrlblInsAddress.WidthF = xrlblInsLegalDetails.WidthF = xrHeaderProjectName.WidthF = (xrTaxTitle.WidthF - (xrpicReportLogoLeft1.LeftF + xrpicReportLogoLeft1.WidthF)); 
                }
                else
                {
                    HideReportLogoLeft = false;
                }
                xrlblInsName.LeftF = xrlblInsAddress.LeftF = xrlblInsLegalDetails.LeftF = xrHeaderProjectName.LeftF = xrLegaEntityTelephone.LeftF = titleLeft;
                xrlblInsName.WidthF = xrlblInsAddress.WidthF = xrlblInsLegalDetails.WidthF = xrHeaderProjectName.WidthF =  titleWidth;
                //xrReportTitle.Borders = xrlblInsName.Borders = xrlblInsAddress.Borders = BorderSide.All;
                this.HideReportHeader = this.HidePageFooter = false;

                xrlblInsAddress.Text = ReportProperty.Current.LegalAddress;
                xrlblInsLegalDetails.Text = ReportProperty.Current.VoucherPrintLegalEntityFieldsDetails;
                xrLegaEntityTelephone.Text = "Tel : " + ReportProperty.Current.LegalTelephone;
                xrLegaEntityGSTDetails.Text = "GSTIN : " + ReportProperty.Current.LegalGSTNo;

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
                xrHeaderProjectName.Text = settings.InstituteName;
                ShowFiancialReportFilterDialog();
            }
            //this.ExportToPdf(@"C:\GST_INVOICE.pdf");
        }

        private void SetReportSetting()
        {
            this.TopMarginHeight = 75;
            //Caption Font Style
            Font fontCaption = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrcellInvoiceNoCaption.Font.FontFamily, xrcellInvoiceNoCaption.Font.Size, FontStyle.Bold)
                : new Font(xrcellInvoiceNoCaption.Font.FontFamily, xrcellInvoiceNoCaption.Font.Size, FontStyle.Regular);
            
            //fontSumCaption = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblLedgerNameCaption.Font.FontFamily, 8, FontStyle.Bold) : new Font(lblLedgerNameCaption.Font.FontFamily, 8, FontStyle.Regular);
            //fontCaption = new Font(xrcellInvoiceNoCaption.Font.FontFamily, xrcellInvoiceNoCaption.Font.Size, FontStyle.Bold);

            //Caption Font Style
            fontCaption = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrcellInvoiceNoCaption.Font.FontFamily, 8, FontStyle.Bold) :
                                        new Font(xrcellInvoiceNoCaption.Font.FontFamily, 9, FontStyle.Regular);
            //fontSumCaption = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(lblLedgerNameCaption.Font.FontFamily, 8, FontStyle.Bold) : new Font(lblLedgerNameCaption.Font.FontFamily, 9, FontStyle.Regular);

            //Value Font Style
            Font fontValue = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrcellInvoiceNo.Font.FontFamily, 8, FontStyle.Bold)
                : new Font(xrcellInvoiceNo.Font.FontFamily, 8, FontStyle.Regular);

            //For Caption 
            xrcellInvoiceNoCaption.Font = xrcellInvoiceDateCaption.Font = xrcellIsReverseChargeCaption.Font = xrcellTransportModeCaption.Font = fontCaption;
            xrcellVechileNumberCaption.Font = xrcellSupplyDateCaption.Font =  xrcellSupplyPlaceCaption.Font =xrcellBillNameCaption.Font = fontCaption;
            xrcellBillAddressCaption.Font = xrcellBillGSTNoCaption.Font = xrcellBillStateCaption.Font = xrcellBillStateCodeCaption.Font = fontCaption;
            xrcellShipNameCaption.Font = xrcellShipAddressCaption.Font = xrcellShipGSTNoCaption.Font = xrcellShipStateCaption.Font = fontCaption;
            xrcellShipStateCodeCaption.Font = xrInsSocietyStateCodeCaption.Font =  fontCaption;
            xrcellSumTaxValue.Font = xrcellSumCGST.Font = xrcellSumSGST.Font = xrcellTotalSumLedgerAmount.Font = fontCaption;

            xrcellBeforeTaxCaption.Font = xrcellAddCGSTCaption.Font = xrcellAddSGSTCaption.Font = xrcellTotalTaxAmountCaption.Font = fontCaption;
            xrcellAfterTaxCaption.Font = xrcellReverseChargeCaption.Font = xrcellCashBankTitle.Font = xrcellCashBankCaption.Font = fontCaption;
            xrCellRemarksCaption.Font = fontCaption;
            
            //For Values 
            xrcellInvoiceNo.Font = xrcellInvoiceDate.Font =xrcellIsReverseCharge.Font =xrcellTransportMode.Font = xrcellVechileNumber.Font = fontValue;
            xrcellSupplyDate.Font = xrcellSupplyPlace.Font = xrcellBillName.Font = xrcellBillAddress.Font = xrcellBillGSTNo.Font = fontValue;
            xrcellBillState.Font = xrcellBillStateCode.Font = xrcellShipName.Font = xrcellShipAddress.Font = xrcellShipGSTNo.Font = fontValue;
            xrcellShipState.Font = xrcellShipStateCode.Font = xrInsSocietyStateCode.Font = xrcellSNo.Font = fontValue;
            xrcellLedgerName.Font = xrcellHSNSACCode.Font = xrcellQty.Font = xrcellUnit.Font =  fontValue;
            xrcellDiscount.Font = xrcellTaxableValue.Font = xrcellCGST.Font = xrcellSGST.Font = xrcellTotalTaxAmount.Font = fontValue;
            xrcellCGSTRate.Font = xrcellSGSTRate.Font = xrcellLedgerTotalAmount.Font = fontValue;
            xrcellBeforeTax.Font = xrcellAddCGST.Font = xrcellAddSGST.Font = xrcellAfterTax.Font = xrcellReverseCharge.Font = xrcellTotalTaxAmount.Font=  fontValue;
            xrcellCashBankLedger.Font = xrcellChequeFavour.Font = fontValue;
            xrCellRemarksCaption.Font = xrCellRemarksCaption.Font = fontValue;

            xrcellUnit.Font = fontValue;
            xrcellUnit.Font = fontValue;
            xrcellUnitAmount.Font = fontValue;
            xrcellDiscount.Font = fontValue;

            xrcellBeforeTax.Font = xrcellAddCGST.Font = xrcellAddSGST.Font = xrcellTotalTaxAmount.Font = fontValue;
            xrcellAfterTax.Font = xrcellReverseCharge.Font = xrcellCashBankLedger.Font = fontValue;
            xrCellRemarks.Font = fontValue;
        }

        public ResultArgs BindGSTInvoice()
        {
            string CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchGSTInvoiceByRPVoucher);
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

        /// <summary>
        /// Get final Total Invoice Amount
        /// </summary>
        /// <returns></returns>
        private decimal GetTotalInvoiceAmount()
        {
            decimal rtn = 0;
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_NAMEColumn.ColumnName) != null)
            {
                DataTable dtRpt = this.DataSource as DataTable;
                Int32 vid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.VOUCHER_IDColumn.ColumnName).ToString());
                string filter = this.reportSetting1.GST_MASTER_INVOICE.VOUCHER_IDColumn.ColumnName + " = " + vid;
                object totalsumledgeramount = dtRpt.Compute("SUM(" + this.reportSetting1.GST_MASTER_INVOICE.CGSTColumn.ColumnName + ")" +
                            " + SUM(" + this.reportSetting1.GST_MASTER_INVOICE.SGSTColumn.ColumnName + ")" +
                            " + SUM(" + this.reportSetting1.GST_MASTER_INVOICE.LEDGER_AMOUNTColumn.ColumnName + ")"
                            , filter);

                if (totalsumledgeramount != null)
                {
                    rtn = UtilityMember.NumberSet.ToDecimal(totalsumledgeramount.ToString());
                }
            }
            return rtn;
        }
    
        private void xrcellLedgerTotalAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_NAMEColumn.ColumnName) != null)
            {
                Int32 vid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.VOUCHER_IDColumn.ColumnName).ToString());
                decimal cgst = UtilityMember.NumberSet.ToDecimal(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.CGSTColumn.ColumnName).ToString());
                decimal sgst = UtilityMember.NumberSet.ToDecimal(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.SGSTColumn.ColumnName).ToString());
                decimal ledgertotalamount = UtilityMember.NumberSet.ToDecimal(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_AMOUNTColumn.ColumnName).ToString());
                e.Value = (ledgertotalamount + cgst + sgst);
            }
        }

        private void xrcellTaxAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource !=null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_NAMEColumn.ColumnName) != null)
            {
                decimal totaltax = 0;
                DataTable dtRpt = this.DataSource as DataTable;
                Int32 vid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.VOUCHER_IDColumn.ColumnName).ToString());
                string filter = this.reportSetting1.GST_MASTER_INVOICE.VOUCHER_IDColumn.ColumnName + " = " + vid;
                object sumtaxamount =  dtRpt.Compute("SUM(" + this.reportSetting1.GST_MASTER_INVOICE.CGSTColumn.ColumnName + ")"+
                            " + SUM(" + this.reportSetting1.GST_MASTER_INVOICE.SGSTColumn.ColumnName + ")", filter);
                if (sumtaxamount!=null)
                {
                    totaltax = UtilityMember.NumberSet.ToDecimal(sumtaxamount.ToString());
                }

                e.Value = (totaltax);
            }
        }

        private void xrcellChequeFavour_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_NAMEColumn.ColumnName) != null)
            {
                XRTableCell xrcell = sender as XRTableCell;
                string chequename = GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.CHEQUE_IN_FAVOURColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(chequename))
                {
                    xrcell.Text = "Terms and Conditions : Cheque in favour of \"" + chequename + "\"";
                }
                else
                {
                    xrcell.Text = string.Empty;
                }
            }
        }

        private void xrcellAmountInRupees_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_NAMEColumn.ColumnName) != null)
            {
                XRTableCell xrcell = sender as XRTableCell;
                decimal totalamount = GetTotalInvoiceAmount();// UtilityMember.NumberSet.ToDecimal(GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.CASH_BANK_AMOUNTColumn.ColumnName).ToString());
                xrcell.Text = "Rupees : " + ConvertRuppessInWord.GetRupeesToWord(totalamount.ToString()); 
            }
        }

        private void xrcellSNo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_NAMEColumn.ColumnName) != null)
            {
                XRTableCell xrcell = sender as XRTableCell;
                xrcell.Text = SerialNumber.ToString(); ;
                SerialNumber++;
            }
        }

        private void grpHeaderVoucher_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            SerialNumber = 1;
        }

        private void xrcellTotalSumLedgerAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            
        }

        private void xrCellIsReserveCharge_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_NAMEColumn.ColumnName) != null)
            {
                XRTableCell xrcell = sender as XRTableCell;
                string isreversecharge = GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.IS_REVERSE_CHARGEColumn.ColumnName).ToString();
                xrcell.Text = isreversecharge == "1"? "Y": (isreversecharge == "0" ? "N" : isreversecharge);
            }
        }

        private void xrInsSocietyStateCode_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.GST_MASTER_INVOICE.LEDGER_NAMEColumn.ColumnName) != null)
            {
                e.Value = ReportProperty.Current.GSTStateCode;
            }
        }

        private void xrCellRemarks_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.NARRATIONColumn.ColumnName) != null)
            {
                XRTableCell xrcell = sender as XRTableCell;
                string narration = GetCurrentColumnValue(this.reportSetting1.CashBankReceipts.NARRATIONColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(narration))
                {
                    xrcell.Text =narration;
                }
                else
                {
                    xrcell.Text = string.Empty;
                }
            }
        }

        private void xrcellTotalSumLedgerAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = GetTotalInvoiceAmount();
            e.Handled = true;
        }

        private void xrcellAfterTax_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value=  GetTotalInvoiceAmount();
        }

    }
}
