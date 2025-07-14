using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Linq;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Utility.ConfigSetting;
using System.Text;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class FCPurposeReceivedUtilisedCumulative : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
                
        private double TotalOP = 0;
        //private double TotalReceived = 0;
        private double Totaltotal = 0;
        private double TotalUtilized = 0;
        private double TotalBalance = 0;

        private double dCashOP = 0;
        private double dCashCL = 0;
        private double dBankOP = 0;
        private double dBankCL = 0;
        private double dFDOP = 0;
        private double dFDCL = 0;

        double FCReceivedYear = 0;
        double FDInterestReceivedYear = 0;
        double SBInterestReceivedYear = 0;
        double ExpenditureYear = 0;
        double CommissionYear = 0;

        #endregion

        #region Constructor
        public FCPurposeReceivedUtilisedCumulative()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            //this.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
            BindPurposeDistribution();
        }

        private void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            //xrcellHeaderCode.Font = new Font(xrcellHeaderCode.Font.FontFamily, 10, FontStyle.Bold);
            //xrcellCode.Font = new Font(xrcellHeaderCode.Font.FontFamily, 10, FontStyle.Regular);
        }
        #endregion

        #region Methods
        public void BindPurposeDistribution()
        {
            TotalOP = Totaltotal = TotalUtilized = TotalBalance = 0;
            dCashOP = dCashCL = dBankOP = dBankCL = dFDOP = dFDCL = 0;
            FCReceivedYear = FDInterestReceivedYear = SBInterestReceivedYear = ExpenditureYear = CommissionYear = 0;

            SetReportTitle();
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindProperty();
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
                    BindProperty();
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

        private void BindProperty()
        {
            this.SetLandscapeHeader = xrtblHeader.WidthF;
            this.SetLandscapeFooter = xrtblHeader.WidthF;
            this.SetLandscapeFooterDateWidth = this.PageWidth - 250;
            setHeaderTitleAlignment();
            
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            ReportSetting();
            //AlignContentTable(xrTblOpeningBalanceDetails);
            GetReportSource();
            FCPurposeReceivedUtilisedDistribution purposeDistribution = xrSubFCPurposeDistribution.ReportSource as FCPurposeReceivedUtilisedDistribution;
            purposeDistribution.BindPurposeDistribution();

        }

        private void ReportSetting()
        {
            AlignHeaderTable(xrtblHeader, true);
            AlignContentTable(xrTblData);
            AlignGrandTotalTable(xrtblTotal);
            AlignGrandTotalTable(xrTblOpeningBalanceDetails);

            //this.SetCurrencyFormat(xrcellHeaderOPBalance.Text, xrcellHeaderOPBalance);
            //this.SetCurrencyFormat(xrcellHeaderReceived.Text, xrcellHeaderReceived);
            //this.SetCurrencyFormat(xrcellHeaderUtilized.Text, xrcellHeaderUtilized);
            //this.SetCurrencyFormat(xrcellHeaderTotal.Text, xrcellHeaderTotal);
            //this.SetCurrencyFormat(xrcellHeaderBalance.Text, xrcellHeaderBalance);

            //Interest title
            Int32 nMonths = UtilityMember.DateSet.GetDateDifferentInMonths(UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false), UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false));
            
            if (nMonths == 12)
            {
                bool issameyear = (UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year == UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Year);
                string yearfrom = UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).Year.ToString();
                string yearto = UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).Year.ToString();
                xrcellInterestTitle.Text = "(+) Interest " + (issameyear ? yearfrom : yearfrom + "-" + yearto);
            }
            else if (nMonths == 1)
            {
                xrcellInterestTitle.Text = "(+) Interest " + UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToString("MMM-yyyy");
            }
            else
            {
                xrcellInterestTitle.Text = "(+) Interest " + UtilityMember.DateSet.ToDate(ReportProperties.DateFrom, false).ToString("MMM-yyyy") + " to " +
                                            UtilityMember.DateSet.ToDate(ReportProperties.DateTo, false).ToString("MMM-yyyy");
            }
        }
        /// <summary>
        /// Fetch Report Source
        /// </summary>
        private void GetReportSource()
        {
            DateTime FirstFYFrom = this.AppSetting.FirstFYDateFrom;
           
            try
            {
                string Purpose = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCPurposeCumulative);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_PROGRESS_FROMColumn, FirstFYFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Purpose);
                    if (resultArgs.Success && resultArgs.DataSource.Table!=null)
                    {
                        DataTable dtReportData = resultArgs.DataSource.Table;
                        if (dtReportData != null)
                        {
                            dtReportData.Columns.Add("OP_TOTAL_BALANCE", typeof(System.Double), "(" + reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName + " + " 
                                                                            + reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName + ")");

                            string filter = "(OP_TOTAL_BALANCE<> 0 OR " +
                                            reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName + "<> 0 OR " +
                                            reportSetting1.FC6PURPOSELIST.UTILISEDColumn.ColumnName + "<> 0)";
                            dtReportData.DefaultView.Sort = reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName;
                            dtReportData.DefaultView.RowFilter = filter;
                            dtReportData = dtReportData.DefaultView.ToTable();

                            dtReportData.TableName = "FC6PURPOSELIST";
                            this.DataSource = dtReportData;
                            this.DataMember = dtReportData.TableName;
                            TotalOP = GetOPBalance();
                            Totaltotal = GetTotaltotal();
                            TotalBalance = GetTotalBalance();
                            
                            dCashOP = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.CashBalance, AcMEDSync.Model.BalanceSystem.BalanceType.OpeningBalance);
                            dBankOP = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.BankBalance, AcMEDSync.Model.BalanceSystem.BalanceType.OpeningBalance);
                            dFDOP = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateFrom, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.FDBalance, AcMEDSync.Model.BalanceSystem.BalanceType.OpeningBalance);

                            dCashCL = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.CashBalance, AcMEDSync.Model.BalanceSystem.BalanceType.ClosingBalance);
                            dBankCL = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.BankBalance, AcMEDSync.Model.BalanceSystem.BalanceType.ClosingBalance);
                            dFDCL = this.GetBalance(this.ReportProperties.Project, this.ReportProperties.DateTo, AcMEDSync.Model.BalanceSystem.LiquidBalanceGroup.FDBalance, AcMEDSync.Model.BalanceSystem.BalanceType.ClosingBalance);
                        }
                    }
                    else
                    {
                        this.DataSource = null;
                    }
                    AssignFDCumulativeSummary();

                    //xrcellOPCashValue.Text = UtilityMember.NumberSet.ToNumber(dCashOP);
                    //xrcellOPBankValue.Text = UtilityMember.NumberSet.ToNumber(dBankOP);
                    //xrcellOPFDValue.Text = UtilityMember.NumberSet.ToNumber(dFDOP);

                    xrcellCLCashValue.Text = UtilityMember.NumberSet.ToNumber(dCashCL);
                    xrcellCLBankValue.Text = UtilityMember.NumberSet.ToNumber(dBankCL);
                    xrcellCLFDValue.Text = UtilityMember.NumberSet.ToNumber(dFDCL);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        private void AssignFDCumulativeSummary()
        {
            string Purpose = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCPurposeCumulativeSummary);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Purpose);

                FCReceivedYear = FDInterestReceivedYear = SBInterestReceivedYear = ExpenditureYear = CommissionYear = 0;
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtReportDataSummary = resultArgs.DataSource.Table;
                    if (dtReportDataSummary.Rows.Count > 0)
                    {
                        FCReceivedYear = UtilityMember.NumberSet.ToDouble(dtReportDataSummary.Rows[0]["RECEIPTS"].ToString());
                        FDInterestReceivedYear = UtilityMember.NumberSet.ToDouble(dtReportDataSummary.Rows[0]["FD_INTEREST_AMOUNT"].ToString());
                        SBInterestReceivedYear = UtilityMember.NumberSet.ToDouble(dtReportDataSummary.Rows[0]["SB_INTEREST_AMOUNT"].ToString());
                        ExpenditureYear = UtilityMember.NumberSet.ToDouble(dtReportDataSummary.Rows[0]["UTILISED"].ToString());
                        CommissionYear = UtilityMember.NumberSet.ToDouble(dtReportDataSummary.Rows[0]["BANK_COMMISSION_AMOUNT"].ToString());

                        //Exculde Interest from FCReceivedYear
                        if (FCReceivedYear > 0) FCReceivedYear -= (FDInterestReceivedYear + SBInterestReceivedYear);
                        
                        //Exculde Bank Commision from Expenditure
                        if (ExpenditureYear > 0) ExpenditureYear -= CommissionYear;
                    }
                }

                
                xrcellExpenditureYear.Text = UtilityMember.NumberSet.ToNumber(ExpenditureYear);
                xrcellCommissionYear.Text = UtilityMember.NumberSet.ToNumber(CommissionYear);


                xrcellGrandReceived.Text = UtilityMember.NumberSet.ToNumber(FCReceivedYear + FDInterestReceivedYear + SBInterestReceivedYear);
                xrcellGrandTotal.Text = UtilityMember.NumberSet.ToNumber(ExpenditureYear + CommissionYear); 
                xrcellGrandUtilized.Text = UtilityMember.NumberSet.ToNumber(ExpenditureYear + CommissionYear); 

            }
        }

        private double GetOPBalance()
        {
            double rtn = 0;
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                DataTable dtReport = this.DataSource as DataTable;
                string op = "SUM(" + reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName + ") + " +
                            "SUM(" + reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName + ")";

                rtn = UtilityMember.NumberSet.ToDouble(dtReport.Compute(op, string.Empty).ToString());
            }
            return rtn;
        }

        private double GetTotaltotal()
        {
            double rtn = 0;
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                DataTable dtReport = this.DataSource as DataTable;
                string total = "SUM(" + reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName + ") + " +
                               "SUM(" + reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName + ") + " +
                               "SUM(" + reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName + ")";

                rtn = UtilityMember.NumberSet.ToDouble(dtReport.Compute(total, string.Empty).ToString());
            }
            return rtn;
        }

        private double GetTotalBalance()
        {
            double rtn = 0;
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                DataTable dtReport = this.DataSource as DataTable;
                string TotalReceived = "SUM(" + reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName + ") + " +
                                        "SUM(" + reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName + ") + " +
                                        "SUM(" + reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName + ")";
                string TotalUtlilized = "SUM(" + reportSetting1.FC6PURPOSELIST.UTILISEDColumn.ColumnName + ")";


                double received = UtilityMember.NumberSet.ToDouble(dtReport.Compute(TotalReceived, string.Empty).ToString()); ;
                double utilized = UtilityMember.NumberSet.ToDouble(dtReport.Compute(TotalUtlilized, string.Empty).ToString()); ;

                rtn = (received - utilized);
            }
            return rtn;
        }

        #endregion

        private void xrcellOPBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                double dOpBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName).ToString());
                double dPreviousBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName).ToString());
                e.Value = dOpBalance + dPreviousBalance;
            }
        }

        private void xrcellTotalOPBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrcellGrandOPBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
             
        }

        private void xrcellTotal_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                double dOpBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName).ToString());
                double dPreviousBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName).ToString());
                double dReceived = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName).ToString());
                e.Value = dOpBalance + dPreviousBalance + dReceived;
            }
        }

        private void xrcellTotalTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrcellGrandTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrcellBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                double dOpBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.OP_BALANCEColumn.ColumnName).ToString());
                double dPreviousBalance = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PREVIOUS_BALANCEColumn.ColumnName).ToString());
                double dReceived = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.RECEIPTSColumn.ColumnName).ToString());
                double dUtilized = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.UTILISEDColumn.ColumnName).ToString());
                e.Value = (dOpBalance + dPreviousBalance + dReceived) - dUtilized;
            }
        }

        private void xrcellTotalBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrcellGrandBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrcellSumOPBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                e.Result = TotalOP;
                e.Handled = true;
            }
        }

        private void xrcellSumTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                e.Result = Totaltotal;
                e.Handled = true;
            }
        }

        private void xrcellSumBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.FC6PURPOSELIST.PURPOSEColumn.ColumnName) != null)
            {
                e.Result = TotalBalance;
                e.Handled = true;
            }
        }

        private void xrcellOPCashValue_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

        private void xrcellOPCashValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
           e.Value = UtilityMember.NumberSet.ToNumber(dCashOP);
        }

        private void xrcellOPBankValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(dBankOP);
        }

        private void xrcellOPFDValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(dFDOP);
        }

        private void xrcellCLCashValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(dCashCL);
        }

        private void xrcellCLBankValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(dBankCL);
        }

        private void xrcellCLFDValue_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(dFDCL);
        }

        private void xrcellCommunityOP_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(TotalOP);
        }

        private void xrcellCommunityCL_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(TotalBalance);
        }

        private void xrcellFCReceivedYear_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(FCReceivedYear);
        }

        private void xrcellInterestReceivedYear_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(FDInterestReceivedYear + SBInterestReceivedYear);
        }

        private void xrcellGrandReceived_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(FCReceivedYear + FDInterestReceivedYear + SBInterestReceivedYear);
        }

        private void xrcellGrandTotal_EvaluateBinding(object sender, BindingEventArgs e)
        {
            double GrandTotaltotal = TotalOP + (dCashOP + dBankOP + dFDOP) + (FCReceivedYear + FDInterestReceivedYear + SBInterestReceivedYear);
            e.Value = UtilityMember.NumberSet.ToNumber(GrandTotaltotal);
        }

        private void xrcellGrandUtilized_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(ExpenditureYear + CommissionYear);
        }

        private void xrcellExpenditureYear_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(ExpenditureYear);
        }

        private void xrcellCommissionYear_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(CommissionYear);
        }

        private void xrcellGrandBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            double GrandTotaltotal = TotalOP + (dCashOP + dBankOP + dFDOP) + (FCReceivedYear + FDInterestReceivedYear + SBInterestReceivedYear);
            double GrandBalance = GrandTotaltotal - (ExpenditureYear + CommissionYear);
            e.Value = UtilityMember.NumberSet.ToNumber(GrandBalance);
        }

        private void xrcellGrandOPBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = TotalOP + (dCashOP + dBankOP + dFDOP);
        }
    }
}
