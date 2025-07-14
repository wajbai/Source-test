using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class FDHistory : ReportHeaderBase
    {

        #region Declaration
        ResultArgs resultArgs = null;
        public double WithdrawAmt = 0;
        public double ClosingBalance = 0;
        public double InvestmentAmount = 0;
        public double AccIntAmount = 0;
        public double IntAmountTotal = 0;
        public double RealPricipleAmount = 0;
        string FdAccountid = string.Empty;

        public double BankClosingBalance = 0;
        public double InvestmentClosingBalance = 0;
        public double LedgerClosingBalance = 0;

        public double InterestTotal = 0;
        public double BankInterestTotal = 0;
        public double InvestmentInterestTotal = 0;
        public double LedgerInterestTotal = 0;

        string BaseReportId = string.Empty;
        string FDHistoryDateFrom = string.Empty;

        string PrincipalAmountCaption = string.Empty;
        string ReInvestAmountCaption = string.Empty;
        string WithdrawalAmtCaption = string.Empty;
        string InterestAmountCaption = string.Empty;
        string CapTDSAmount = string.Empty;
        string HeaderPenaltyAmountCaption = string.Empty;

        #endregion

        private Int32 FDAccountInvestmentType
        {
            get;
            set;
        }

        #region Constructor
        public FDHistory()
        {
            InitializeComponent();
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                xrPrincipalAmount.Tag = xrPrincipalAmount.Text;
                xrReInvestAmount.Tag = xrReInvestAmount.Text;
                xrWithdrawalAmt.Tag = xrWithdrawalAmt.Text;
                xrInterestAmount.Tag = xrInterestAmount.Text;
                xrCapTDSAmount.Tag = xrCapTDSAmount.Text;
                xrCellHeaderPenaltyAmount.Tag = xrCellHeaderPenaltyAmount.Text;
            }

            //string FD_RENEWAL_ID = "FD_RENEWAL_ID";
            this.AttachDrillDownToRecord(xrtblFdRegister, xrTableCell12,
             new ArrayList { "FD_VOUCHER_ID", "FD_RENEWAL_ID"}, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
            FDAccountInvestmentType = (Int32)FDInvestmentType.FD;
        }

        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            WithdrawAmt = ClosingBalance = 0;
            if (IsDrillDownMode && (this.ReportProperties.DrillDownProperties != null && this.ReportProperties.DrillDownProperties.Count > 0))
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                FdAccountid = dicDDProperties["FD_ACCOUNT_ID"].ToString();
                BaseReportId = GetBaseReportId();
                if (!string.IsNullOrEmpty(BaseReportId))
                {
                    DataTable dtBaseRptSetting = GetReportSetting(BaseReportId);
                    if (dtBaseRptSetting != null && dtBaseRptSetting.Rows.Count > 0)
                    {
                        this.ReportProperties.ShowByLedger = UtilityMember.NumberSet.ToInteger(dtBaseRptSetting.Rows[0]["SHOWBYLEDGER"].ToString());
                    }

                    //On 06/01/2023, While drilling FD register, FD statement and Mutual Fund Register show fd history fully from Books begin-------
                    if (BaseReportId == "RPT-047" || BaseReportId == "RPT-015" || BaseReportId == "RPT-219")
                    {
                        FDHistoryDateFrom = settingProperty.BookBeginFrom;
                    }

                    ResultArgs result = this.GetFDAccountDetailsByFDId(this.UtilityMember.NumberSet.ToInteger(FdAccountid));
                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count>0)
                    {
                        FDAccountInvestmentType = UtilityMember.NumberSet.ToInteger(result.DataSource.Table.Rows[0][reportSetting1.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName].ToString());
                    }
                    //----------------------------------------------------------------------------------------
                }
            }
            else
            {
                FdAccountid = this.ReportProperties.FDAccountID;
            }
            FetchFDRegisterHistory();
        }
        #endregion

        #region Methods
        public void FetchFDRegisterHistory()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        FetchFDRegisterDetails();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    FetchFDRegisterDetails();
                    base.ShowReport();
                }
            }
        }

        public void FetchFDRegisterDetails()
        {
            try
            {
                this.SetLandscapeHeader = xrtblHeaderTable.WidthF;
                this.SetLandscapeFooter = xrtblHeaderTable.WidthF;
                this.SetLandscapeFooterDateWidth = 970.00f;
                setHeaderTitleAlignment();
                SetReportTitle();

                //On 03/06/2024, For Mutual Fund Register, Change Report Title
                if (FDAccountInvestmentType == (Int32)FDInvestmentType.MutualFund)
                {
                    this.ReportTitle = "Mutual Fund History";
                }
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                grpBankHeader.Visible = (ReportProperty.Current.ShowByBank == 1);
                grpBankFooter.Visible = (ReportProperty.Current.ShowByBank == 1);
                grpHeaderHolderName.Visible = (ReportProperty.Current.ShowByInvestment == 1);
                grpFooterHolderName.Visible = (ReportProperty.Current.ShowByInvestment == 1);
                grpHeaderLedgerName.Visible = (ReportProperty.Current.ShowByLedger == 1);
                grpFooterLedgerName.Visible = (ReportProperty.Current.ShowByLedger == 1);

                string FDRegister = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDHistoryDetails);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    
                    if (!string.IsNullOrEmpty(FDHistoryDateFrom))
                    {
                        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, FDHistoryDateFrom);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);    
                    }

                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.FD_ACCOUNT_IDColumn, (!string.IsNullOrEmpty(FdAccountid) ? FdAccountid : "0"));
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, FDRegister);
                    AttachShowBy();
                    DataView dvCashFlow = resultArgs.DataSource.TableView;
                    if (dvCashFlow != null && dvCashFlow.Count != 0)
                    {
                        dvCashFlow.Table.TableName = "FDRegister";
                        this.DataSource = dvCashFlow.ToTable();
                        this.DataMember = dvCashFlow.Table.TableName;
                        dvCashFlow.RowFilter = "";

                        Detail.SortFields.Clear();
                        Detail.SortFields.Add(new GroupField(reportSetting1.FDRegister.INVESTMENT_DATEColumn.ColumnName));
                    }
                    else
                    {
                        this.DataSource = null;
                    }
                }
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void SetReportBorder()
        {
            //28/11/2018, to lock reinvestment feature based on setting ---------------------------------------------------
            if (this.UIAppSetting.EnableFlexiFD != "1")
            {
                if (xrHeaderRow.Cells.Contains(xrReInvestAmount))
                    xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrReInvestAmount.Name]);

                if (xrDataRow.Cells.Contains(xrvalueReInvestAmount))
                    xrDataRow.Cells.Remove(xrDataRow.Cells[xrvalueReInvestAmount.Name]);

                if (xrClosingBalRow.Cells.Contains(xrClReInvestment))
                    xrClosingBalRow.Cells.Remove(xrClosingBalRow.Cells[xrClReInvestment.Name]);

                if (xrClosingBankBalRow.Cells.Contains(xrBankClReInvestment))
                    xrClosingBankBalRow.Cells.Remove(xrClosingBankBalRow.Cells[xrBankClReInvestment.Name]);

                if (xrClosingHolderBalRow.Cells.Contains(xrHolderClReInvestment))
                    xrClosingHolderBalRow.Cells.Remove(xrClosingHolderBalRow.Cells[xrHolderClReInvestment.Name]);

                if (xrClosingLedgerBalRow.Cells.Contains(xrLedgerClReInvestment))
                    xrClosingLedgerBalRow.Cells.Remove(xrClosingLedgerBalRow.Cells[xrLedgerClReInvestment.Name]);
            }


            //31/07/2024, Other than India, let us lock TDS Amount
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                this.HideTableCell(xrtblHeaderTable, xrHeaderRow, xrCapTDSAmount);
                this.HideTableCell(xrtblFdRegister, xrDataRow, xrTDSAmount);
                this.HideTableCell(xrtblFDNumberClBal, xrClosingBalRow, xrGrpFDNoSumTDSAmount);
                this.HideTableCell(xrtblFDNumberBankTotal, xrClosingBankBalRow, xrGrpBankSumTDSAmount);
                this.HideTableCell(xrtblFDHolderTotal, xrClosingHolderBalRow, xrGrpACHolderSumTDSAmount);
                this.HideTableCell(xrtblFDLedgerNameTotal, xrClosingLedgerBalRow, xrGrpLedgerSumTDSAmount);
            }


            xrtblHeaderTable = AlignHeaderTable(xrtblHeaderTable);
            xrtblFdRegister = AlignContentTable(xrtblFdRegister);
            xrtblFDNumberClBal = AlignGrandTotalTable(xrtblFDNumberClBal);
            xrtblFDNumberBankTotal = AlignGrandTotalTable(xrtblFDNumberBankTotal);
            xrtblFDHolderTotal = AlignGrandTotalTable(xrtblFDHolderTotal);
            xrtblFDLedgerNameTotal = AlignGrandTotalTable(xrtblFDLedgerNameTotal);

            //On 23/10/2024, To set currency symbol based on currency selection
            string cashbankcurrencysymbol = AppSetting.Currency;
            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                cashbankcurrencysymbol = ReportProperties.GetCashBankLedgerCurrencySymbol(ReportProperties.CashBankLedger);
                xrPrincipalAmount.Text = (xrPrincipalAmount.Tag != null ? xrPrincipalAmount.Tag.ToString() : xrPrincipalAmount.Text);
                xrReInvestAmount.Text = (xrReInvestAmount.Tag != null ? xrReInvestAmount.Tag.ToString() : xrReInvestAmount.Text);
                xrWithdrawalAmt.Text = (xrWithdrawalAmt.Tag != null ? xrWithdrawalAmt.Tag.ToString() : xrWithdrawalAmt.Text);
                xrInterestAmount.Text = (xrInterestAmount.Tag != null ? xrInterestAmount.Tag.ToString() : xrInterestAmount.Text);
                xrCapTDSAmount.Text = (xrCapTDSAmount.Tag != null ? xrCapTDSAmount.Tag.ToString() : xrCapTDSAmount.Text);
                xrCellHeaderPenaltyAmount.Text = (xrCellHeaderPenaltyAmount.Tag != null ? xrCellHeaderPenaltyAmount.Tag.ToString() : xrCellHeaderPenaltyAmount.Text);
            }

            this.SetCurrencyFormat(xrPrincipalAmount.Text, xrPrincipalAmount, cashbankcurrencysymbol);
            this.SetCurrencyFormat(xrReInvestAmount.Text, xrReInvestAmount, cashbankcurrencysymbol);
            this.SetCurrencyFormat(xrWithdrawalAmt.Text, xrWithdrawalAmt, cashbankcurrencysymbol);
            this.SetCurrencyFormat(xrInterestAmount.Text, xrInterestAmount, cashbankcurrencysymbol);
            this.SetCurrencyFormat(xrCapTDSAmount.Text, xrCapTDSAmount, cashbankcurrencysymbol);
            this.SetCurrencyFormat(xrCellHeaderPenaltyAmount.Text, xrCellHeaderPenaltyAmount, cashbankcurrencysymbol);
        }

        /// <summary>
        /// This method attach group details settings
        /// </summary>
        private void AttachShowBy()
        {
            //Add Bank Group
            if (ReportProperty.Current.ShowByBank == 0)
            {
                grpBankHeader.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpBankHeader.GroupFields[0].FieldName = this.ReportParameters.BANKColumn.ColumnName;
                //grpBankHeader.GroupFields.Add( new GroupField(this.ReportParameters.BANKColumn.ColumnName,XRColumnSortOrder.Ascending));
            }

            //Add Investment Group
            if (ReportProperty.Current.ShowByInvestment == 0)
            {
                grpHeaderHolderName.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpHeaderHolderName.GroupFields[0].FieldName = this.reportSetting1.FixedDepositStatement.ACCOUNT_HOLDERColumn.ColumnName;
                //grpBankHeader.GroupFields.Add( new GroupField(this.ReportParameters.BANKColumn.ColumnName,XRColumnSortOrder.Ascending));
            }

            //Add Ledger Group
            if (ReportProperty.Current.ShowByLedger == 0)
            {
                grpHeaderLedgerName.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpHeaderLedgerName.GroupFields[0].FieldName = this.reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName;
            }
        }
        
        private string GetBaseReportId()
        {
            string Rtn = string.Empty;

            foreach (object item in this.ReportProperties.stackActiveDrillDownHistory)
            {
                EventDrillDownArgs eventdrilldownarg = item as EventDrillDownArgs;
                if (eventdrilldownarg.DrillDownType == DrillDownType.BASE_REPORT)
                {
                    Rtn = eventdrilldownarg.DrillDownRpt;
                    break;
                }
            }

            return Rtn;
        }

        private void xrCellPrincipleAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string fd_vouchertype = (GetCurrentColumnValue("FD_VOUCHER_TYPE") != null ? GetCurrentColumnValue("FD_VOUCHER_TYPE").ToString() : string.Empty);
            double principleamount = (GetCurrentColumnValue(this.reportSetting1.FDRegister.PRINCIPLE_AMOUNTColumn.ColumnName) != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.FDRegister.PRINCIPLE_AMOUNTColumn.ColumnName).ToString()) : 0);
            double reinvestedamt = (GetCurrentColumnValue(this.reportSetting1.FDRegister.REINVESTED_AMOUNTColumn.ColumnName) != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.FDRegister.REINVESTED_AMOUNTColumn.ColumnName).ToString()) : 0);
            WithdrawAmt = GetCurrentColumnValue(this.reportSetting1.FDRegister.WITHDRAWAL_AMOUNTColumn.ColumnName) != null
                ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.FDRegister.WITHDRAWAL_AMOUNTColumn.ColumnName).ToString()) : 0;
            AccIntAmount = GetCurrentColumnValue(this.reportSetting1.FDRegister.ACCUMULATED_INTEREST_AMOUNTColumn.ColumnName) != null
                ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.FDRegister.ACCUMULATED_INTEREST_AMOUNTColumn.ColumnName).ToString()) : 0;

            if (fd_vouchertype == "IN" || fd_vouchertype == "OP")
            {
                ClosingBalance = principleamount;
                RealPricipleAmount = principleamount;
            }

            double displaypricipleamt = principleamount;
            if (fd_vouchertype == "RIN")
            {
                ClosingBalance += reinvestedamt;
                displaypricipleamt = 0;
            }

            ClosingBalance = (ClosingBalance + AccIntAmount) - WithdrawAmt;

            xrCellPrincipleAmt.Text = this.UtilityMember.NumberSet.ToNumber(RealPricipleAmount);
            xrcellFDAccountClBal.Text = this.UtilityMember.NumberSet.ToNumber(ClosingBalance);
            RealPricipleAmount = (RealPricipleAmount + AccIntAmount + reinvestedamt) - WithdrawAmt;
        }

        private void xrcellInterestAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 08/08/2019, To include TDS amount with interest amount (after SDBINB meeting)
            double TDSAmount = GetCurrentColumnValue(this.reportSetting1.FDRegister.TDS_AMOUNTColumn.ColumnName) != null
                ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.reportSetting1.FDRegister.TDS_AMOUNTColumn.ColumnName).ToString()) : 0;
            double InterestAmt = this.UtilityMember.NumberSet.ToDouble(xrcellInterestAmt.Text);
            InterestTotal += InterestAmt;
            BankInterestTotal += InterestAmt;
            InvestmentInterestTotal += InterestAmt;
            LedgerInterestTotal += InterestAmt;
            xrcellInterestAmt.Text = this.UtilityMember.NumberSet.ToNumber(InterestAmt + TDSAmount);
            InterestTotal = InterestTotal + TDSAmount;
        }

        private void grpBankHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            BankClosingBalance = 0;
            BankInterestTotal = 0;
        }

        private void grpHeaderHolderName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            InvestmentClosingBalance = 0;
            InvestmentInterestTotal = 0;
        }

        private void grpHeaderLedgerName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            LedgerClosingBalance = 0;
            LedgerInterestTotal = 0;
        }

        private void xrCellInvestmentName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ReportProperty.Current.ShowByLedger == 1)
            {
                string investmentname = GetCurrentColumnValue(reportSetting1.FDRegister.ACCOUNT_HOLDERColumn.ColumnName).ToString();
                if (!string.IsNullOrEmpty(investmentname))
                {
                    xrCellInvestmentName.Text = "   " + investmentname;
                }
            }
        }

        private void xrGrpHeaderBankName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string bankname = GetCurrentColumnValue(reportSetting1.FDRegister.BANKColumn.ColumnName).ToString();

            if (ReportProperty.Current.ShowByLedger == 1 && (ReportProperty.Current.ShowByInvestment == 1 || ReportProperty.Current.ShowByLedger == 1))
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    (sender as XRTableCell).Text = "     " + bankname;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(bankname))
                {
                    (sender as XRTableCell).Text = "   " + bankname;
                }
            }
        }

        private void xrCellBankTotalCaption_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string banktitleCaption = (sender as XRTableCell).Text.Trim();

            if (ReportProperty.Current.ShowByLedger == 1 && (ReportProperty.Current.ShowByInvestment == 1 || ReportProperty.Current.ShowByLedger == 1))
            {
                if (!string.IsNullOrEmpty(banktitleCaption))
                {
                    (sender as XRTableCell).Text = "     " + banktitleCaption;
                }
            }
            else if (ReportProperty.Current.ShowByLedger == 0 && ReportProperty.Current.ShowByInvestment == 1)
            {
                if (!string.IsNullOrEmpty(banktitleCaption))
                {
                    (sender as XRTableCell).Text = "   " + banktitleCaption;
                }
            }
        }

        private void xrCellInvestmentNameTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string investmentnameCaption = (sender as XRTableCell).Text.Trim();
            (sender as XRTableCell).Text = "   " + investmentnameCaption.Trim();

            if (ReportProperty.Current.ShowByLedger == 1)
            {
                if (!string.IsNullOrEmpty(investmentnameCaption))
                {
                    investmentnameCaption = "   " + investmentnameCaption;
                }
            }
            xrCellInvestmentNameTotal.Text = investmentnameCaption;
        }

        private void xrcellFDAccountClBal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ClosingBalance;
            BankClosingBalance += ClosingBalance;
            InvestmentClosingBalance += ClosingBalance;
            LedgerClosingBalance += ClosingBalance;
            e.Handled = true;
        }

        private void xrcellBankClBalTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = BankClosingBalance;
            e.Handled = true;
        }

        private void xrcellInvestmentClBalTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = InvestmentClosingBalance;
            e.Handled = true;
        }

        private void xrcellLedgerNameClBalTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = LedgerClosingBalance;
            e.Handled = true;
        }

        private void xrcellFDAccountIntTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = InterestTotal;
            e.Handled = true;
        }

        private void grpFDNumberHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            InterestTotal = 0;
        }

        private void xrcellBankInterestTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = BankInterestTotal;
            e.Handled = true;
        }

        private void xrcellInvestmentInterestTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = InvestmentInterestTotal;
            e.Handled = true;
        }

        private void xrcellLedgerNameInterestTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = LedgerInterestTotal;
            e.Handled = true;
        }

        #endregion

        private void xrGrpHeaderFDNumber_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FDRegister.MF_FOLIO_NOColumn.ColumnName) != null)
            {
               Int32 fdinvestmenttypeid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.Ledger.FD_INVESTMENT_TYPE_IDColumn.ColumnName).ToString());
               if (fdinvestmenttypeid == ((int)FDInvestmentType.MutualFund))
               {
                   e.Value = GetCurrentColumnValue(reportSetting1.FDRegister.MF_FOLIO_NOColumn.ColumnName).ToString();
               }
            }
            //FDAccountInvestmentType
        }

    }
}
