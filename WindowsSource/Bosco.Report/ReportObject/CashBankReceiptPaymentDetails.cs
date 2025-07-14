using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO;
using Bosco.Utility;
using Bosco.Report.Base;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class CashBankReceiptPaymentDetails : Bosco.Report.Base.ReportBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int printVoucherId = 0;
        public DataTable dtTemp = new DataTable();
        private string Currency = string.Empty;
        public int PrintVoucherId
        {
            set
            {
                printVoucherId = value;
                BindCashBankReceiptsDetails();
            }
            get { return printVoucherId; }
        }
        //public string currency
        //{
        //    set
        //    {
        //        Currency = value;
        //    }
        //    get { return Currency; }
        //}
        public float CodeLedgerWidth
        {
            set
            {
                xrCash.WidthF = value;
            }
        }
        public float CodeLedgerAmountWidth
        {
            set
            {
                xrAmount.WidthF = value;
            }
        }

        public float CashBankLedgerTableWidth
        {
            set
            {
                xrTable1.WidthF = value;
            }
        }

        public float ColumnThroughWidth
        {
            get
            {
                return xrlblThrough.WidthF;
            }
            set
            {
                xrlblThrough.WidthF = xrlblThroughSpace1.WidthF = xrlblThroughSpace2.WidthF = value;
            }
        }

        #endregion

        #region Constructor
        public CashBankReceiptPaymentDetails()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            base.ShowReport();
        }
        public ResultArgs BindCashBankReceiptsDetails()
        {
            //On 01/02/2018, to show contra voucher also
            string CashBankReceipts = string.Empty;
            if (this.ReportProperties.ReportId == UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKCONTRA))
                CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchCashBankContraDetails);
            else
                if (this.ReportProperties.ModuleType != "Stock")
                    CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchCashBankReceiptPaymentDetails);

            if (this.ReportProperties.ModuleType == "Stock")
            {
                CashBankReceipts = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchCashBankStockDetails);
            }

            if (!string.IsNullOrEmpty(CashBankReceipts))
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.FC6PURPOSELIST.VOUCHER_IDColumn, PrintVoucherId);
                    dataManager.Parameters.Add(this.reportSetting1.CashBankReceipts.PURCHASE_IDColumn, this.ReportProperties.PrintPurchaseInoutVoucherId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankReceipts);
                    this.DataSource = dtTemp = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;

                    xrlblThrough.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrlblThrough.Font.FontFamily, xrlblThrough.Font.Size, FontStyle.Bold)
                    : new Font(xrlblThrough.Font.FontFamily, xrlblThrough.Font.Size, FontStyle.Regular);
                    xrCellBankReferenceCaption.Font = (this.ReportProperties.VoucherPrintCaptionBold == "1") ? new Font(xrCellBankReferenceCaption.Font.FontFamily, xrCellBankReferenceCaption.Font.Size, FontStyle.Bold)
                    : new Font(xrCellBankReferenceCaption.Font.FontFamily, xrCellBankReferenceCaption.Font.Size, FontStyle.Regular);
                    xrCash.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrCash.Font.FontFamily, xrCash.Font.Size, FontStyle.Bold)
                    : new Font(xrCash.Font.FontFamily, xrCash.Font.Size, FontStyle.Regular);
                    xrAmount.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrAmount.Font.FontFamily, xrAmount.Font.Size, FontStyle.Bold)
                    : new Font(xrAmount.Font.FontFamily, xrAmount.Font.Size, FontStyle.Regular);
                    xrCellChequeReference.Font = (this.ReportProperties.VoucherPrintValueBold == "1") ? new Font(xrCellChequeReference.Font.FontFamily, xrCellChequeReference.Font.Size, FontStyle.Bold)
                    : new Font(xrCellChequeReference.Font.FontFamily, xrAmount.Font.Size, FontStyle.Regular);
                    //this.ReportProperties.PrintCashBankVoucherId = string.Empty;

                    if (!(dtTemp.Rows.Count > 1))
                    {
                        xrCash.WidthF = (xrCash.WidthF + xrAmount.WidthF - 4);
                    }
                }
            }
            else
            {
                resultArgs.Message = "Could not find Database query";
            }
            return resultArgs;
        }
        #endregion

        private void xrAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;

            if (dtTemp.Rows.Count > 1)
            {
                //xrAmount.Text = xrAmount.Text;
                if (xrAmount != null)
                {
                    // To Make row if singe/double entry of transaction
                    xrAmount.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                    e.Cancel = false;
                }
                else
                {
                    // To Make row if singe/double entry of transaction
                    xrAmount.Text = "";
                    xrAmount.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                }
            }
            else
            {
                // To Make row if singe/double entry of transaction
                xrAmount.Text = "";
                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                cell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;




                //xrAmount.WidthF = 50;
                //xrCash.WidthF = xrTable1.WidthF;
                //xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.All;
                //xrTable1.BorderColor = Color.Red;
            }
        }

        private void xrCellBankReferenceCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.ReportProperties.ModuleType == "Stock")

                xrCellBankReferenceCaption.Text = " ";
        }

        private void xrCellChequeReference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string chequeno = GetCurrentColumnValue("CHEQUE_NO") != null ? GetCurrentColumnValue("CHEQUE_NO").ToString() : string.Empty;
            string chequedate = GetCurrentColumnValue("CHEQUE_REF_DATE") != null ? this.ReportProperties.DateSet.ToDate(GetCurrentColumnValue("CHEQUE_REF_DATE").ToString()) : string.Empty;
            string chequeBankName = GetCurrentColumnValue("CHEQUE_REF_BANKNAME") != null ? GetCurrentColumnValue("CHEQUE_REF_BANKNAME").ToString() : string.Empty;
            string chequeBankBranch = GetCurrentColumnValue("CHEQUE_REF_BRANCH") != null ? GetCurrentColumnValue("CHEQUE_REF_BRANCH").ToString() : string.Empty;
            string chequeFundTransferMode = GetCurrentColumnValue("FUND_TRANSFER_TYPE_NAME") != null ? GetCurrentColumnValue("FUND_TRANSFER_TYPE_NAME").ToString() : string.Empty;
            if (!string.IsNullOrEmpty(chequedate) || !string.IsNullOrEmpty(chequeBankName) || !string.IsNullOrEmpty(chequeBankBranch) || !string.IsNullOrEmpty(chequeFundTransferMode))
            {
                xrCellChequeReference.Text = "Cheque/DD/Ref.No.: " + chequeno + " - " + chequedate + " " + chequeBankName + " " + chequeBankBranch + " " + chequeFundTransferMode;
            }
            else
            {
                e.Cancel = true;
                xrRowBankReference.Visible = false;
            }

            //if (this.ReportProperties.ModuleType == "Stock")
            //{
            //    xrCellChequeReference.Text = " ";
            //}
        }

        private void xrRowBankReferenceCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string chequedate = GetCurrentColumnValue("CHEQUE_REF_DATE") != null ? GetCurrentColumnValue("CHEQUE_REF_DATE").ToString() : string.Empty;
            string chequeBankName = GetCurrentColumnValue("CHEQUE_REF_BANKNAME") != null ? GetCurrentColumnValue("CHEQUE_REF_BANKNAME").ToString() : string.Empty;
            string chequeBankBranch = GetCurrentColumnValue("CHEQUE_REF_BRANCH") != null ? GetCurrentColumnValue("CHEQUE_REF_BRANCH").ToString() : string.Empty;
            if (string.IsNullOrEmpty(chequedate) && string.IsNullOrEmpty(chequeBankName) && string.IsNullOrEmpty(chequeBankBranch))
            {
                e.Cancel = true;
            }
        }

        private void xrRowBankReference_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string chequedate = GetCurrentColumnValue("CHEQUE_REF_DATE") != null ? GetCurrentColumnValue("CHEQUE_REF_DATE").ToString() : string.Empty;
            string chequeBankName = GetCurrentColumnValue("CHEQUE_REF_BANKNAME") != null ? GetCurrentColumnValue("CHEQUE_REF_BANKNAME").ToString() : string.Empty;
            string chequeBankBranch = GetCurrentColumnValue("CHEQUE_REF_BRANCH") != null ? GetCurrentColumnValue("CHEQUE_REF_BRANCH").ToString() : string.Empty;
            if (string.IsNullOrEmpty(chequedate) && string.IsNullOrEmpty(chequeBankName) && string.IsNullOrEmpty(chequeBankBranch))
            {
                e.Cancel = true;
            }
        }
    }
}
