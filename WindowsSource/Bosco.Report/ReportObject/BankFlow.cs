using System;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using AcMEDSync.Model;

namespace Bosco.Report.ReportObject
{
    public partial class BankFlow : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        double DailyReceipts = 0;
        double DailyPayments = 0;
        double DailyClBalance = 0;
        double PreviousCLBalance = 0;
        int GroupNumber = 0;
        #endregion

        #region Constructor
        public BankFlow()
        {
            InitializeComponent();

            this.AttachDrillDownToRecord(xrtblBindBankFlow, xrDate,
                new ArrayList { this.reportSetting1.CashBankFlow.DATEColumn.ColumnName }, DrillDownType.LEDGER_BANK, false);
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            // BankFlowReport();
            DailyReceipts = 0;
            DailyPayments = 0;
            DailyClBalance = 0;
            PreviousCLBalance = 0;
            GroupNumber = 0;
            BankFlowReport();

        }
        private void BankFlowReport()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty
                || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        //this.ReportTitle = ReportProperty.Current.ReportTitle;
                        // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                        // this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                       setHeaderTitleAlignment();
                       SetReportTitle();
                       SetReportBorder();
                        //To show Opening Balance
                        //prOPBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                        //                       BalanceSystem.BalanceType.OpeningBalance);

                        // To include FD Balance with the bank balance
                        double BankOpBal = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                               BalanceSystem.BalanceType.OpeningBalance);
                        //double FDOpBal = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.FDBalance,
                        //                       BalanceSystem.BalanceType.OpeningBalance);
                        double FDOpBal = 0;

                        prOPBalance.Value = BankOpBal + FDOpBal;

                        double ClBalance = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                               BalanceSystem.BalanceType.ClosingBalance);
                        prOPBalance.Visible = false;
                        DataTable dtCashBankBook = GetReportSource();
                        if (dtCashBankBook != null && dtCashBankBook.Rows.Count > 0)
                        {
                            this.DataSource = dtCashBankBook;
                            this.DataMember = dtCashBankBook.TableName;
                        }
                        else
                        {
                            this.DataSource = null;
                            xrCashAmount.Text = xrCashBalance.Text = "";
                        }
                        SplashScreenManager.CloseForm();
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
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    //this.ReportTitle = ReportProperty.Current.ReportTitle;
                    // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                    // this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                    
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    SetReportBorder();
                    //To show Opening Balance
                    //prOPBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                    //                       BalanceSystem.BalanceType.OpeningBalance);

                    // To include FD Balance with the bank balance
                    double BankOpBal = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                           BalanceSystem.BalanceType.OpeningBalance);
                    //double FDOpBal = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.FDBalance,
                    //                       BalanceSystem.BalanceType.OpeningBalance);
                    double FDOpBal = 0;
                    prOPBalance.Value = BankOpBal + FDOpBal;

                    double ClBalance = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.BankBalance,
                                           BalanceSystem.BalanceType.ClosingBalance);
                    prOPBalance.Visible = false;
                    DataTable dtCashBankBook = GetReportSource();
                    if (dtCashBankBook != null && dtCashBankBook.Rows.Count > 0)
                    {
                        this.DataSource = dtCashBankBook;
                        this.DataMember = dtCashBankBook.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                        xrCashAmount.Text = xrCashBalance.Text = "";
                    }
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrOpeningBalance = AlignOpeningBalanceTable(xrOpeningBalance);
            xrtblBindBankFlow = AlignContentTable(xrtblBindBankFlow);
            xrtblCbalance = AlignClosingBalance(xrtblCbalance);
            xrtblGrandTotal = AlignContentTable(xrtblGrandTotal);

            this.SetCurrencyFormat(xrCapInFlow.Text, xrCapInFlow);
            this.SetCurrencyFormat(xrCapOutFlow.Text, xrCapOutFlow);
            this.SetCurrencyFormat(xrCapBalance.Text, xrCapBalance);
        }

        private DataTable GetReportSource()
        {
            try
            {
                string BankFlowQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.BankFlow);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankFlowQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }

            //On 05/06/2017, To add Amount filter condition
            DataTable dtBankFlow = new DataTable();
            if (resultArgs.Success)
            {
                dtBankFlow = resultArgs.DataSource.Table;
                string AmountFilter = this.GetAmountFilter();
                lblAmountFilter.Visible = false;
                if (AmountFilter != "")
                {
                    dtBankFlow.DefaultView.RowFilter = "(IN_FLOW > 0 AND IN_FLOW " + AmountFilter + ") OR (OUT_FLOW > 0 AND OUT_FLOW " + AmountFilter + ")";
                    lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                    lblAmountFilter.Visible = true;
                }
            }
            else
            {
                MessageRender.ShowMessage("Could not generate Bank Flow Report", true);
            }

            return dtBankFlow;
        }
        #endregion

        private void xrtblClosingBalance_SummaryRowChanged(object sender, EventArgs e)
        {
            GroupNumber++;
            DailyReceipts = (GetCurrentColumnValue(this.ReportParameters.IN_FLOWColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.IN_FLOWColumn.ColumnName).ToString());
            DailyPayments = (GetCurrentColumnValue(this.ReportParameters.OUT_FLOWColumn.ColumnName) == null) ? 0 : this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(this.ReportParameters.OUT_FLOWColumn.ColumnName).ToString());
            if (GroupNumber == 1)
                DailyClBalance = PreviousCLBalance = (this.UtilityMember.NumberSet.ToDouble(prOPBalance.Value.ToString()) + DailyReceipts) - DailyPayments;
            else
                DailyClBalance = PreviousCLBalance = (PreviousCLBalance + DailyReceipts) - DailyPayments;
            xrCashAmount.Text = xrCashBalance.Text = this.UtilityMember.NumberSet.ToNumber(DailyClBalance);
        }

        private void xrOPBalance_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToDouble(prOPBalance.Value.ToString());
            e.Handled = true;
        }

        private void xrOutflow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double BankOutFlow = this.ReportProperties.NumberSet.ToDouble(xrOutflow.Text);
            if (BankOutFlow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrOutflow.Text = "";
            }
        }

        private void xrInflow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double BankInFlow = this.ReportProperties.NumberSet.ToDouble(xrInflow.Text);
            if (BankInFlow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrInflow.Text = "";
            }
        }
    }
}
