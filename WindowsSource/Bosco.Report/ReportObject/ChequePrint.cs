using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using Bosco.Report.SQL;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class ChequePrint : Bosco.Report.Base.ReportHeaderBase
    {
        private Int32 VoucherId = 0;
        private Int32 BankId = 0;
        private string VoucherDate = String.Empty;
        private string PartyName = String.Empty;
        private double Amount = 0;

        public double ChequeWidth = 0;
        public double ChequeHeight = 0;
        public double DateTop = 0;
        public double DateLeft = 0;
        public double DateDigitWidth = 0;
        public double PartyLedgerTop = 0;
        public double PartyLedgerLeft = 0;
        public double AmountWordsTop = 0;
        public double AmountWordsLeft = 0;
        public double AmountTop = 0;
        public double AmountLeft = 0;

        private CommonMember utilityMember = null;
        private CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        public ChequePrint()
        {
            InitializeComponent();
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }
        
        public ChequePrint(Int32 voucherid, Int32 bankid = 0)
            : this()
        {
            VoucherId = voucherid;
            BankId = bankid;
            ResultArgs resultarg =  GenerateChequePrint();
            if (!resultarg.Success)
            {
                Detail.Visible = false;
                MessageRender.ShowMessage(resultarg.Message);
            }

        }

        #region ShowReport
        public override void ShowReport()
        {
            VoucherId = this.ReportProperties.ChequeBankVoucherId;
            if (VoucherId > 0)
            {
                ResultArgs resultargs = GenerateChequePrint();
                if (resultargs.Success)
                {
                    DataTable dtBankVoucher = resultargs.DataSource.Table;
                    this.DataSource = dtBankVoucher;
                    this.DataMember = dtBankVoucher.TableName;
                    base.ShowReport();
                }
                else
                {
                    MessageRender.ShowMessage(resultargs.Message);
                    Detail.Visible = false;
                }
            }
            else
            {
                ShowBankVoucher();
            }
        }
        #endregion

        private ResultArgs GenerateChequePrint()
        {
            //SET default cheque printng values
            ChequeWidth = GetXtraReportSize(8);
            ChequeHeight = GetXtraReportSize(3.8);
            DateLeft = GetXtraReportSize(6.11);
            DateTop= GetXtraReportSize(0.4);
            PartyLedgerTop = GetXtraReportSize(0.12);
            PartyLedgerLeft = GetXtraReportSize(0.15);
            AmountWordsTop = GetXtraReportSize(1.2);
            AmountWordsLeft= GetXtraReportSize(1.8);
            AmountTop = GetXtraReportSize(1.5);
            Amount = GetXtraReportSize(6.11);
            DateDigitWidth = GetXtraReportSize(0);
            
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.DefaultPrinterSettingsUsing.UsePaperKind = false;
            this.DefaultPrinterSettingsUsing.UseMargins = false;
            this.DefaultPrinterSettingsUsing.UseLandscape = false;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageWidth = UtilityMember.NumberSet.ToInteger(Math.Round(ChequeWidth).ToString()); //pagewidth // 158 * 10;
            this.PageHeight = UtilityMember.NumberSet.ToInteger(Math.Round(ChequeHeight).ToString()); // 76 * 10;
            this.Landscape = false;
            xrlblDate.Font = new Font(xrlblDate.Font.FontFamily, xrlblDate.Font.Size, FontStyle.Bold);
            xrlblParty.Font = new Font(xrlblParty.Font.FontFamily, xrlblParty.Font.Size, FontStyle.Bold);
            xrlblAmount.Font = new Font(xrlblAmount.Font.FontFamily, xrlblAmount.Font.Size, FontStyle.Bold);
            xrlblAmountInWords.Font = new Font(xrlblAmountInWords.Font.FontFamily, xrlblAmountInWords.Font.Size, FontStyle.Bold);
            
            this.PageWidth = UtilityMember.NumberSet.ToInteger(Math.Round(ChequeWidth).ToString()); // 76 * 10;
            this.PageHeight = UtilityMember.NumberSet.ToInteger(Math.Round(ChequeHeight).ToString()); //pagewidth // 158 * 10; //UtilityMember.NumberSet.ToInteger(Math.Round(ChequeHeight).ToString()); // 76 * 10;
            this.Detail.HeightF = (float)ChequeHeight;
            VoucherDate = UtilityMember.DateSet.ToDate(DateTime.Now.ToShortDateString());
            Amount = 0;
            ResultArgs resultArgs = GetBankVoucherDetails();
            
            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                DataTable dtBankVoucher = resultArgs.DataSource.Table;
                if (dtBankVoucher.Rows.Count > 0)
                {
                    BankId = UtilityMember.NumberSet.ToInteger(dtBankVoucher.Rows[0]["BANK_ID"].ToString());
                    //Get Posistions from Setting Table, Load values from cheque printing table 
                    resultArgs = loadChequePrintingSetting(BankId);
                    if (resultArgs.Success)
                    {
                        this.PageWidth = UtilityMember.NumberSet.ToInteger(Math.Round(ChequeWidth).ToString()); // 76 * 10;
                        this.PageHeight = UtilityMember.NumberSet.ToInteger(Math.Round(ChequeHeight).ToString()); //pagewidth // 158 * 10; //UtilityMember.NumberSet.ToInteger(Math.Round(ChequeHeight).ToString()); // 76 * 10;
                        this.Detail.HeightF = (float)ChequeHeight;

                        VoucherDate = UtilityMember.DateSet.ToDate(dtBankVoucher.Rows[0]["VOUCHER_DATE"].ToString());
                        PartyName = dtBankVoucher.Rows[0]["NAME_ADDRESS"].ToString();
                        //If name and adress is empty, it will take ledger name as party ledger
                        if (string.IsNullOrEmpty(PartyName))
                        {
                            PartyName = dtBankVoucher.Rows[0]["PARTY_LEDGER"].ToString();
                        }
                        Amount = UtilityMember.NumberSet.ToDouble(dtBankVoucher.Rows[0]["Amount"].ToString());

                        xrlblDate.Text = VoucherDate;
                        xrlblParty.Text = PartyName.ToUpper();
                        xrlblAmount.Text = "**" + UtilityMember.NumberSet.ToNumber(Amount) + "**";
                        xrlblAmountInWords.Text = ConvertRuppessInWord.GetRupeesToWord(Amount.ToString()).ToUpper();
                        resultArgs.DataSource.Data = dtBankVoucher;
                        resultArgs.Success = true;
                    }
                }
                else
                {
                    loadChequePrintingSetting(BankId);
                }
            }
            else
            {
                resultArgs.Message = "Could not get Voucher Details " + resultArgs.Message;
            }
            return resultArgs;
        }

        public ResultArgs GetBankVoucherDetails()
        {
            ResultArgs resultArgs = new ResultArgs();
            string BankCheque = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchChequePrinting); 
            using (DataManager dataManager = new DataManager())
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add("VOUCHER_ID", VoucherId);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankCheque);
            }
            return resultArgs;
        }


        private void drChequePrinting_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Assign locations
            AssignDateProperty();

            xrlblParty.TopF = (float)PartyLedgerTop;
            xrlblParty.LeftF = (float)PartyLedgerLeft;

            xrlblAmountInWords.TopF = (float)AmountWordsTop;
            xrlblAmountInWords.LeftF = (float)AmountWordsLeft;

            xrlblAmount.TopF = (float)AmountTop;
            xrlblAmount.LeftF = (float)AmountLeft; 
        }

        /// <summary>
        /// This method is used to calcalulate positions in XtraReport
        /// Report unit is HunderedthsofAnInch, so we need to multiply by 100
        /// </summary>
        /// <param name="inchsize"></param>
        /// <returns></returns>
        private double GetXtraReportSize(double inchsize)
        {
            double fractionvalue = 0;
            double rtn = inchsize;

            try
            {
                string[] value = inchsize.ToString().Split('.');
                if (value.GetLength(0) == 2)
                {
                    double reminder = UtilityMember.NumberSet.ToInteger(value.GetValue(1).ToString());
                    fractionvalue = GetFractionValue((int)reminder);
                    rtn = UtilityMember.NumberSet.ToInteger(value.GetValue(0).ToString());
                }

                //Conver to report widh by HunderedthsofAnInch
                rtn = (rtn + fractionvalue) * 100;
            }
            catch (Exception err)
            {
                rtn = 0;
                MessageRender.ShowMessage("Could not calculate Report size " + err.Message);
            }
            return rtn;
        }

        /// <summary>
        /// This method is used to calcualte, inches decimal value based on its fraction
        /// </summary>
        /// <param name="reminder"></param>
        /// <returns></returns>
        private double GetFractionValue(int reminder)
        {
            double rtn = 0;
            int rm = reminder;
            try
            {
                switch (reminder)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                    case 15:
                        rtn = (double)(double)reminder / (double)16;
                        break;
                    case 2:
                        rtn = (double)(double)1 / (double)8;
                        break;
                    case 4:
                        rtn = (double)(double)1 / (double)4;
                        break;
                    case 6:
                        rtn = (double)(double)3 / (double)8;
                        break;
                    case 8:
                        rtn = (double)(double)1 / (double)2;
                        break;
                    case 10:
                        rtn = (double)(double)5 / (double)8;
                        break;
                    case 12:
                        rtn = (double)(double)3 / (double)4;
                        break;
                    case 14:
                        rtn = (double)(double)7 / (double)8;
                        break;
                }
            }
            catch (Exception err)
            {
                rtn = 0;
            }
            return rtn;
        }
        
        /// <summary>
        /// This method is used to get cheque printing settings 
        /// 
        /// fields position
        /// </summary>
        private ResultArgs loadChequePrintingSetting(Int32 bankid)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                string chequeprint = this.GetReportCashBankVoucher(SQL.ReportSQLCommand.CashBankVoucher.FetchChequePrintingSetting);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add("BANK_ID", bankid, DataType.Int32);
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, chequeprint);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0 )
                    {
                        DataTable dtChequeSetting = resultArgs.DataSource.Table;
                        foreach (DataRow dr in dtChequeSetting.Rows)
                        {
                            string settingnmae = dr["SETTING_NAME"].ToString();
                            string value = dr["SETTING_VALUE"].ToString();
                            Bosco.Utility.ChequePrinting chequeprintingsetting = Bosco.Utility.ChequePrinting.Width;
                            chequeprintingsetting = (ChequePrinting)UtilityMember.EnumSet.GetEnumItemType(typeof(ChequePrinting), settingnmae);
                            switch (chequeprintingsetting)
                            {
                                case ChequePrinting.Width:
                                    ChequeWidth = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.Height:
                                    ChequeHeight = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.DateTop:
                                    DateTop = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.DateLeft:
                                    DateLeft = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.DateDigitWidth:
                                    DateDigitWidth = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.PartyNameTop:
                                    PartyLedgerTop = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.PartyNameLeft:
                                    PartyLedgerLeft = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.AmountWordsTop:
                                    AmountWordsTop = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.AmountWordsLeft:
                                    AmountWordsLeft = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.AmountTop:
                                    AmountTop = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                                case ChequePrinting.AmountLeft:
                                    AmountLeft = GetXtraReportSize(UtilityMember.NumberSet.ToDouble(value));
                                    break;
                            }
                        }
                        resultArgs.Success = true;
                    }
                    else
                    {
                        resultArgs.Message =  "Cheque Printing Setting is not available" + resultArgs.Message;
                    }
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Error in getting Cheque Printing Setting " + err.Message;
            }
            return resultArgs;
        }

        private void AssignDateProperty()
        {
            xrlblDate.Visible = (DateDigitWidth == 0);
            lblD1.Visible = !(DateDigitWidth == 0);
            lblD2.Visible = lblD1.Visible;
            lblM1.Visible = lblD1.Visible;
            lblM2.Visible = lblD1.Visible;
            lblY1.Visible = lblD1.Visible;
            lblY2.Visible = lblD1.Visible;
            lblY3.Visible = lblD1.Visible;
            lblY4.Visible = lblD1.Visible;

            if (DateDigitWidth == 0)
            {
                xrlblDate.TopF = (float)DateTop;
                xrlblDate.LeftF = (float)DateLeft;
            }
            else
            {
                lblD1.TopF = (float)DateTop; ;
                lblD2.TopF = lblD1.TopF;
                lblM1.TopF = lblD1.TopF;
                lblM2.TopF = lblD1.TopF;
                lblY1.TopF = lblD1.TopF;
                lblY2.TopF = lblD1.TopF;
                lblY3.TopF = lblD1.TopF;
                lblY4.TopF = lblD1.TopF;


                lblD1.WidthF = 9;
                lblD2.WidthF = 9;
                lblM1.WidthF = 9;
                lblM2.WidthF = 9;
                lblY1.WidthF = 9;
                lblY2.WidthF = 9;
                lblY3.WidthF = 9;
                lblY4.WidthF = 9;

                lblD1.LeftF = (float)DateLeft;
                lblD2.LeftF = lblD1.LeftF + (float)DateDigitWidth;
                lblM1.LeftF = (float)(DateLeft + (DateDigitWidth * 2)); //lblD2.LeftF + (float)DateDigitWidth;
                lblM2.LeftF = (float)(DateLeft + (DateDigitWidth * 3)); // lblM1.LeftF + (float)DateDigitWidth;
                lblY1.LeftF = (float)(DateLeft + (DateDigitWidth * 4)); //lblM2.LeftF + (float)DateDigitWidth;
                lblY2.LeftF = (float)(DateLeft + (DateDigitWidth * 5)); //lblY1.LeftF + (float)DateDigitWidth;
                lblY3.LeftF = (float)(DateLeft + (DateDigitWidth * 6)); //lblY2.LeftF + (float)DateDigitWidth;
                lblY4.LeftF = (float)(DateLeft + (DateDigitWidth * 7)); //lblY3.LeftF + (float)DateDigitWidth;

                lblD1.Text = VoucherDate.Substring(0, 1);
                lblD2.Text = VoucherDate.Substring(1, 1);
                lblM1.Text = VoucherDate.Substring(3, 1);
                lblM2.Text = VoucherDate.Substring(4, 1);
                lblY1.Text = VoucherDate.Substring(6, 1);
                lblY2.Text = VoucherDate.Substring(7, 1);
                lblY3.Text = VoucherDate.Substring(8, 1);
                lblY4.Text = VoucherDate.Substring(9, 1);
                
                lblD1.BorderWidth = 0;
                lblD2.BorderWidth = 0;
                lblM1.BorderWidth = 0;
                lblM2.BorderWidth = 0;
                lblY1.BorderWidth = 0;
                lblY2.BorderWidth = 0;
                lblY3.BorderWidth = 0;
                lblY4.BorderWidth = 0;
            }           
            
        }
    }
}