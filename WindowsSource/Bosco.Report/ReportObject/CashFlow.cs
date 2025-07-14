using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using AcMEDSync.Model;

namespace Bosco.Report.ReportObject
{
    public partial class CashFlow : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        double DailyReceipts = 0;
        double DailyPayments = 0;
        double DailyClBalance = 0;
        double PreviousCLBalance = 0;
        int GroupNumber = 0;

        #endregion

        #region Constructor
        public CashFlow()
        {
            InitializeComponent();

            //05/12/2019, to keep Cash Bank LedgerId
            //this.AttachDrillDownToRecord(xrtblBindData, xrCashDate,
            //    new ArrayList { this.reportSetting1.CashBankFlow.DATEColumn.ColumnName }, DrillDownType.LEDGER_CASH, false);
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            DailyReceipts = 0;
            DailyPayments = 0;
            DailyClBalance = 0;
            PreviousCLBalance = 0;
            GroupNumber = 0;
            BindCashFlow();
        }
        #endregion

        #region Method
        private void BindCashFlow()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        //this.ReportTitle = ReportProperty.Current.ReportTitle;
                        // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                        setHeaderTitleAlignment();
                        SetReportTitle();
                        // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                        this.HideCostCenter = false;
                        this.CosCenterName = null;
                        
                        //To show Opening Balance
                        
                        prOPBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.CashBalance,
                                               BalanceSystem.BalanceType.OpeningBalance);

                        prOPBalance.Visible = false;
                        resultArgs = GetReportSource();
                        DataView dvCashFlow = resultArgs.DataSource.TableView;
                        if (dvCashFlow != null && dvCashFlow.Count != 0)
                        {
                            dvCashFlow.Table.TableName = "CashBankFlow";
                            //On 05/06/2017, To add Amount filter condition
                            AttachAmountFilter(dvCashFlow);
                            this.DataSource = dvCashFlow;
                            this.DataMember = dvCashFlow.Table.TableName;
                        }
                        else
                        {
                            this.DataSource = null;
                            xrCashBalanceAmt.Text = xrCashBalance.Text = "";
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
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    // this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                    this.HideCostCenter = false;
                    this.CosCenterName = null;
                    
                    //To show Opening Balance
                    prOPBalance.Value = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, BalanceSystem.LiquidBalanceGroup.CashBalance,
                                           BalanceSystem.BalanceType.OpeningBalance);

                    prOPBalance.Visible = false;
                    resultArgs = GetReportSource();
                    DataView dvCashFlow = resultArgs.DataSource.TableView;
                    if (dvCashFlow != null && dvCashFlow.Count != 0)
                    {
                        dvCashFlow.Table.TableName = "CashBankFlow";
                        //On 05/06/2017, To add Amount filter condition
                        AttachAmountFilter(dvCashFlow);
                        this.DataSource = dvCashFlow;
                        this.DataMember = dvCashFlow.Table.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                        xrCashBalanceAmt.Text = xrCashBalance.Text = "";
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

            setReportBorder();
        }

        private void setReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblBindData = AlignContentTable(xrtblBindData);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            xrtblOpeningBalance = AlignContentTable(xrtblOpeningBalance);
            xrtblCbalance = AlignContentTable(xrtblCbalance);
            this.SetCurrencyFormat(xrCapInFlow.Text, xrCapInFlow);
            this.SetCurrencyFormat(xrCapOutFlow.Text, xrCapOutFlow);
            this.SetCurrencyFormat(xrCapBalance.Text, xrCapBalance);
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string CashFlow = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.CashFlow);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashFlow);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
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
            xrCashBalanceAmt.Text = xrCashBalance.Text = this.UtilityMember.NumberSet.ToNumber(DailyClBalance);
        }

        private void xrOPBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = this.UtilityMember.NumberSet.ToDouble(prOPBalance.Value.ToString());
            e.Handled = true;
        }

        private void xrCashInflow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashInflow = this.ReportProperties.NumberSet.ToDouble(xrCashInflow.Text);
            if (CashInflow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCashInflow.Text = "";
            }
        }

        private void xrCashOutFlow_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashOutFlow = this.ReportProperties.NumberSet.ToDouble(xrCashOutFlow.Text);
            if (CashOutFlow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCashOutFlow.Text = "";
            }
        }

        /// <summary>
        /// This method is used to add filter condition
        /// </summary>
        private DataView AttachAmountFilter(DataView dv)
        {
            //On 05/06/2017, To add Amount filter condition
            string AmountFilter = this.GetAmountFilter();
            lblAmountFilter.Visible = false;
            if (AmountFilter != "")
            {
                dv.RowFilter = "(IN_FLOW > 0 AND IN_FLOW " + AmountFilter + ") OR (OUT_FLOW > 0 AND OUT_FLOW " + AmountFilter + ")";
                lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                lblAmountFilter.Visible = true;
            }
            return dv;
        }
    }
}
