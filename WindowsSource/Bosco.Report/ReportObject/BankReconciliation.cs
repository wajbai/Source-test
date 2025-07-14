using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;
using Bosco.Utility.ConfigSetting;
namespace Bosco.Report.ReportObject
{
    public partial class BankReconciliation : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        double UnrealizedAmt = 0;
        double UnClearedAmt = 0;

        string UnrealizedCaption = string.Empty;
        string UnClearedCaption = string.Empty;
        #endregion

        #region Constructor
        public BankReconciliation()
        {
            InitializeComponent();
            UnrealizedCaption = xrCapUnrealized.Text;
            UnClearedCaption = xrCapUnCleared.Text;

            // 20/02/2025, Chinna
            //this.AttachDrillDownToRecord(xrTblRecord, xrParticulars,
            //    new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName, this.ReportParameters.DATE_AS_ONColumn.ColumnName },
            //    DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");

            // This is to redirect to Journal Entries too
            this.AttachDrillDownToRecord(xrTblRecord, xrParticulars,
              new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName, "VOUCHER_TYPE" }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindBankReconciliationStatement();

        }
        #endregion

        #region Method
        public void BindBankReconciliationStatement()
        {
            xrCurrentBankBalance.Text = string.Empty;
            UnrealizedAmt = 0;
            UnClearedAmt = 0;

            if (!string.IsNullOrEmpty(this.ReportProperties.DateAsOn)
                && !string.IsNullOrEmpty(this.ReportProperties.Project)
                && this.ReportProperties.CashBankLedger != "0" && !string.IsNullOrEmpty(this.ReportProperties.CashBankLedger))
            {

                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        BindProperty();
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
                    BindProperty();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportSetup();

        }

        private void BindProperty()
        {
            this.SetTitleWidth(xrtblHeaderCaption.WidthF);

            setHeaderTitleAlignment();
            SetReportTitle();
            this.CosCenterName = this.ReportProperties.SelectedBankFD;
            this.HideCostCenter = (!string.IsNullOrEmpty(this.ReportProperties.SelectedBankFD)) ? true : false;
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            resultArgs = GetReportSource();
            DataView dvBankReconciliation = resultArgs.DataSource.TableView;
            if (dvBankReconciliation != null && dvBankReconciliation.Count != 0)
            {
                UnrealizedAmt = ReportProperty.Current.NumberSet.ToDouble(dvBankReconciliation.ToTable().Compute("SUM(UNREALISED)", "").ToString());
                UnClearedAmt = ReportProperty.Current.NumberSet.ToDouble(dvBankReconciliation.ToTable().Compute("SUM(UNCLEARED)", "").ToString());
                dvBankReconciliation.Table.TableName = "BankReconciliationStatement";
                this.DataSource = dvBankReconciliation;
                this.DataMember = dvBankReconciliation.Table.TableName;

                //xrCurrentBankBalance.Text = ReportProperty.Current.NumberSet.ToNumber(this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateAsOn, BalanceSystem.LiquidBalanceGroup.BankBalance,
                //               BalanceSystem.BalanceType.ClosingBalance));

                //double BankAmt = ReportProperty.Current.NumberSet.ToDouble(xrCurrentBankBalance.Text);
                //xrBankUnrealizedAmt.Text = ReportProperty.Current.NumberSet.ToNumber(BankAmt + UnrealizedAmt);

                //double BankTotalAmt = ReportProperty.Current.NumberSet.ToDouble(xrBankUnrealizedAmt.Text);
                //xrBankFinalAmt.Text = ReportProperty.Current.NumberSet.ToNumber(BankTotalAmt - UnClearedAmt);
            }
            else
            {
                this.DataSource = null;
            }

            // resultArgs = GetBankClosingBalance();
            DataTable dtBankClosing = GetBankClosingBalance();
            double amount = 0;
            if (dtBankClosing != null && dtBankClosing.Rows.Count != 0)
            {
                foreach (DataRow dr in dtBankClosing.Rows)
                {
                    amount = ReportProperty.Current.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                    amount = dr["TRANS_MODE"].ToString() == TransMode.CR.ToString() ? -amount : amount;
                }
            }
            // To add opening balance with the bank amount Praveen
            // double FDOPBalance = this.GetBalance(this.ReportProperties.Project, ReportProperty.Current.DateAsOn, BalanceSystem.LiquidBalanceGroup.FDBalance,
            //BalanceSystem.BalanceType.ClosingBalance);

            double FDClBalance = GetFDClosingBalance();
            string BankClosingBalance = (amount + FDClBalance).ToString();   // FDOPBalance +  // Commented by Praveen to include only Bank balance

            //xrCurrentBankBalance.Text = ReportProperty.Current.NumberSet.ToNumber(this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateAsOn, BalanceSystem.LiquidBalanceGroup.BankBalance,
            //                   BalanceSystem.BalanceType.ClosingBalance));
            resultArgs = GetBRSCleared();
            DataView dvBRSCleared = resultArgs.DataSource.TableView;
            double reconciled = 0;
            double cleared = 0;
            if (dvBRSCleared != null && dvBRSCleared.Table.Rows.Count != 0)
            {
                reconciled = ReportProperty.Current.NumberSet.ToDouble(dvBRSCleared.ToTable().Compute("SUM(REALISED)", "").ToString());
                cleared = ReportProperty.Current.NumberSet.ToDouble(dvBRSCleared.ToTable().Compute("SUM(CLEARED)", "").ToString());
            }
            double temp = this.UtilityMember.NumberSet.ToDouble(BankClosingBalance) - UnrealizedAmt;
            double Balance = temp + UnClearedAmt;
            xrCurrentBankBalance.Text = ReportProperty.Current.NumberSet.ToNumber(Balance);
            double BankAmt = ReportProperty.Current.NumberSet.ToDouble(xrCurrentBankBalance.Text);
            xrBankUnrealizedAmt.Text = ReportProperty.Current.NumberSet.ToNumber(BankAmt + UnrealizedAmt);

            double BankTotalAmt = ReportProperty.Current.NumberSet.ToDouble(xrBankUnrealizedAmt.Text);
            xrBankFinalAmt.Text = ReportProperty.Current.NumberSet.ToNumber(BankTotalAmt - UnClearedAmt);
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                /* On 03/02/2022, to have proper cheqe-wise if double entry, more than one bank cheques
                string BRStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankReconcilationStatement);
                //On 01/10/2018, if selected by ledger
                if (this.ReportProperties.Consolidated==1)
                    BRStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankReconcilationStatementByConsolidated);*/
                string BRStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankReconcilationStatementByConsolidated);

                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    //05/12/2019, to keep Cash Bank LedgerId
                    //dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.CashBankLedger);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                    dataManager.Parameters.Add(this.ReportParameters.CONSOLIDATEDColumn, this.ReportProperties.Consolidated);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, BRStatement);
                }

                //if (resultArgs.Success)
                //{
                //    DataView dtBRS = resultArgs.DataSource.TableView;
                //    dtBRS.RowFilter = "UnCleared>0 OR Unrealised>0";
                //    resultArgs.DataSource.Data = dtBRS;
                //}
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }


        private ResultArgs GetBRSCleared()
        {
            try
            {
                string BRStatement = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankReconcilationStatementCleared);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    //05/12/2019, to keep Cash Bank LedgerId
                    //dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.CashBankLedger);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, BRStatement);
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private DataTable GetBankClosingBalance()
        {
            DataTable dtBankClosingBalance = null;
            string BankClosingBalance = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankCurrentClosingBalance);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                //05/12/2019, to keep Cash Bank LedgerId
                //if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                //{
                //    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                //}
                //else
                //{
                //    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, "0");
                //}
                if (!string.IsNullOrEmpty(this.ReportProperties.CashBankLedger) && this.ReportProperties.CashBankLedger != "0")
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.CashBankLedger);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, "0");
                }

                DateTime dteClosingDate = this.ReportProperties.DateSet.ToDate(this.ReportProperties.DateAsOn, false);
                //string ClosingDate = dteClosingDate.AddDays(-1).ToShortDateString();
                dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, dteClosingDate);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankClosingBalance);

                if (resultArgs.Success)
                {
                    dtBankClosingBalance = resultArgs.DataSource.Table;
                }
            }
            return dtBankClosingBalance;
        }

        private double GetFDClosingBalance()
        {
            double FdAmount = 0;
            try
            {
                string FDRegister = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.FetchFDClosingBalanceByFDId);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn,
                        this.ReportProperties.DateSet.ToDate(SettingProperty.Current.YearFrom, false));
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                    dataManager.Parameters.Add(this.ReportParameters.FD_ACCOUNT_IDColumn, (string.IsNullOrEmpty(this.ReportProperties.FDAccountID) ? "0" : this.ReportProperties.FDAccountID));
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FDRegister);

                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        FdAmount = ReportProperties.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0]["BALANCE_AMOUNT"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return FdAmount;
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrCapcode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCapcode.Tag != null && xrCapcode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapcode.Tag.ToString());
            }
            else
            {
                xrCapcode.Tag = xrCapcode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapcode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            // xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            // this.ReportPeriod = this.ReportProperties.ReportDate;
            SetReportBorder();
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTblRecord = AlignContentTable(xrTblRecord);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrTable = AlignTotalTable(xrTable);

            //On 03/09/2024, To set curency symbol based on cash/bank selection --------------------------------
            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                string cashbankcurrencysymbol = ReportProperties.GetCashBankLedgerCurrencySymbol(ReportProperties.CashBankLedger);
                if (!string.IsNullOrEmpty(cashbankcurrencysymbol))
                {
                    xrCapUnrealized.Text = UnrealizedCaption + " (" + cashbankcurrencysymbol + ")";
                    xrCapUnCleared.Text = UnClearedCaption + " (" + cashbankcurrencysymbol + ")";
                }
            }
            else
            {
                this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.UNREALIZED, xrCapUnrealized);
                this.SetCurrencyFormat(MessageCatalog.ReportCommonTitle.UNCLEARED, xrCapUnCleared);
            }
            //----------------------------------------------------------------------------------------------------
        }

        public override XRTable AlignHeaderTable(XRTable table, bool UseSameFont = false)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }
            return table;
        }

        #endregion

        private void xrUnRealized_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrUnRealized.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrUnRealized.Text = "";
            }
        }

        private void xrUncleared_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrUncleared.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrUncleared.Text = "";
            }
        }
    }
}
