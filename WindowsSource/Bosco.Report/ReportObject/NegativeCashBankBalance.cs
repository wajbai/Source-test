using System;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using AcMEDSync.Model;
using DevExpress.XtraReports.UI;

namespace Bosco.Report.ReportObject
{
    public partial class NegativeCashBankBalance : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public NegativeCashBankBalance()
        {
            InitializeComponent();

            //On 18/02/2018, to show cash/bank/fd ledger details ---------------------------------------------------------
            ArrayList ledgerfilter = new ArrayList { reportSetting1.NegativeCashBankBalance.VOUCHER_DATEColumn.ColumnName, reportSetting1.MonthlyAbstract.LEDGER_IDColumn.ColumnName, 
                               reportSetting1.MonthlyAbstract.GROUP_IDColumn.ColumnName, reportSetting1.NegativeCashBankBalance.PROJECT_IDColumn.ColumnName };
            DrillDownType ledgerdrilltype = DrillDownType.LEDGER_SUMMARY;
            this.AttachDrillDownToRecord(xrTblData, xrCellDate, ledgerfilter, ledgerdrilltype, false, "", true);
            //------------------------------------------------------------------------------------------------------------
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            BankNagativeCashBank();
        }

        private void BankNagativeCashBank()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.Project))
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
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindProperties();
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
                    BindProperties();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }

        private void BindProperties()
        {
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            setHeaderTitleAlignment();
            SetReportTitle();
            SetReportBorder();

            DataTable dtCashBankBook = GetReportSource();
            if (dtCashBankBook != null && dtCashBankBook.Rows.Count > 0)
            {
                this.DataSource = dtCashBankBook;
                this.DataMember = dtCashBankBook.TableName;

                if (!string.IsNullOrEmpty(this.ReportProperties.Project))
                {
                    if (this.ReportProperties.Project.Split(',').Length <= 1)
                    {
                        xrtblHeaderCaption.SuspendLayout();
                        if (xrHeaderRow.Cells.Contains(xrCapProject))
                            xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrCapProject.Name]);
                        xrtblHeaderCaption.PerformLayout();

                        xrTblData.SuspendLayout();
                        if (xrDataRow.Cells.Contains(xrCellProject))
                            xrDataRow.Cells.Remove(xrDataRow.Cells[xrCellProject.Name]);
                        xrTblData.PerformLayout();

                        xrTblGrandTotal.SuspendLayout();
                        if (xrGrandRow.Cells.Contains(xrGrandTotal2))
                            xrGrandRow.Cells.Remove(xrGrandRow.Cells[xrGrandTotal2.Name]);
                        xrTblGrandTotal.PerformLayout();
                    }
                }
            }
            else
            {
                this.DataSource = null;
            }
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrTblData = AlignContentTable(xrTblData);
            xrTblGrandTotal = AlignContentTable(xrTblGrandTotal);

            this.SetCurrencyFormat(xrCapCash.Text, xrCapCash);
            this.SetCurrencyFormat(xrCapBank.Text, xrCapBank);
        }

        private DataTable GetReportSource()
        {
            try
            {
                string BankFlowQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.NegativeCashBankBalance);
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
                        
            DataTable dtBankFlow = new DataTable();
            if (resultArgs.Success)
            {
                dtBankFlow = resultArgs.DataSource.Table;
                string AmountFilter = this.GetAmountFilter();
                lblAmountFilter.Visible = false;
                if (AmountFilter != "")
                {
                    string filter = "(" + reportSetting1.NegativeCashBankBalance.CASHColumn.ColumnName + " > 0 AND " + reportSetting1.NegativeCashBankBalance.CASHColumn.ColumnName + AmountFilter + ") " +
                               " OR (" + reportSetting1.NegativeCashBankBalance.BANKColumn.ColumnName + " > 0 AND " + reportSetting1.NegativeCashBankBalance.BANKColumn.ColumnName  + AmountFilter + ")";
                    dtBankFlow.DefaultView.RowFilter = filter;
                    lblAmountFilter.Text = "Amount filtered by " + this.UtilityMember.NumberSet.ToNumber(this.ReportProperties.DonorFilterAmount);
                    lblAmountFilter.Visible = true;
                }
            }
            else
            {
                MessageRender.ShowMessage("Could not generate Negative Cash/Bank Balance", true);
            }

            return dtBankFlow;
        }
        #endregion

        private void xrCellCash_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double BankInFlow = this.ReportProperties.NumberSet.ToDouble(xrCellCash.Text);
            if (BankInFlow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCellCash.Text = "";
            }
        }

        private void xrCellBank_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double BankInFlow = this.ReportProperties.NumberSet.ToDouble(xrCellBank.Text);
            if (BankInFlow != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCellBank.Text = "";
            }
        }       
    }
}
