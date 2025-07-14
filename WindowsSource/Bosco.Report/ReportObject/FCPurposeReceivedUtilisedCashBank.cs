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
    public partial class FCPurposeReceivedUtilisedCashBank : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        double TotalReceiptCashInterestCurrent = 0;
        double TotalReceiptBankInterestCurrent = 0;
        double TotalPaymentCashInterestCurrent = 0;
        double TotalPaymentBankInterestCurrent = 0;

        double TotalCashInterestPrevious = 0;
        double TotalBankInterestPrevious = 0;
        #endregion

        #region Constructor
        public FCPurposeReceivedUtilisedCashBank()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Reports
        public override void ShowReport()
        {
            BindPurposeList();
            // base.ShowReport();
        }
        #endregion

        #region Methods
        public void BindPurposeList()
        {
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
            setHeaderTitleAlignment();
            this.SetLandscapeHeader = xrtblPurpose.WidthF; //1068.25f;
            this.SetLandscapeFooter = xrtblPurpose.WidthF; // 1068.25f;
            this.SetLandscapeFooterDateWidth = xrtblPurpose.WidthF; // 900.00f;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            GetReportSource();
            FetchBankAccount();
            FetchDesignatedBankAmount();
        }

        /// <summary>
        /// Fetch Bank Details
        /// </summary>
        private void FetchBankAccount()
        {
            try
            {
                string Purpose = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6BankAccount);
                using (DataManager dataManager = new DataManager())
                {
                    if (ReportProperty.Current.SocietyId != 0)
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CUSTOMERIDColumn, ReportProperty.Current.SocietyId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CUSTOMERIDColumn, "0");
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Purpose);

                    DataTable dvCashFlow = resultArgs.DataSource.Table;
                    if (dvCashFlow != null && dvCashFlow.Rows.Count != 0)
                    {
                        xrBankAcNo.Text = dvCashFlow.Rows[0]["ACCOUNT_NUMBER"].ToString();
                        xrBankName.Text = dvCashFlow.Rows[0]["BANK"].ToString();
                        xrBankBranch.Text = dvCashFlow.Rows[0]["BRANCH"].ToString();
                        xrBankAddress.Text = dvCashFlow.Rows[0]["ADDRESS"].ToString();
                    }
                    else
                    {
                        xrBankAcNo.Text = xrBankName.Text = xrBankBranch.Text = xrBankAddress.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        /// <summary>
        /// Fetch Designated Bank Amount
        /// </summary>
        private void FetchDesignatedBankAmount()
        {
            try
            {
                TotalReceiptBankInterestCurrent = TotalReceiptCashInterestCurrent = TotalPaymentCashInterestCurrent = TotalPaymentBankInterestCurrent = TotalCashInterestPrevious = TotalBankInterestPrevious = 0;
                string BankInterest = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6DesignatedBankAmountCashBank);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, ReportProperty.Current.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, ReportProperty.Current.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, ReportProperty.Current.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_FD_SIMPLE_INTERESTColumn, ReportProperty.Current.IncludeFDSimpleInterest);
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_FD_ACCUMULATED_INTERESTColumn, ReportProperty.Current.IncludeFDAccumulatedInterest);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankInterest);

                    DataTable dvDesignate = resultArgs.DataSource.Table;
                    if (dvDesignate != null && dvDesignate.Rows.Count != 0)
                    {
                        TotalReceiptCashInterestCurrent = this.UtilityMember.NumberSet.ToDouble(dvDesignate.Rows[0]["RECEIPT_CURRENT_AMOUNT_CASH"].ToString());
                        TotalReceiptBankInterestCurrent = this.UtilityMember.NumberSet.ToDouble(dvDesignate.Rows[0]["RECEIPT_CURRENT_AMOUNT_BANK"].ToString());
                        TotalPaymentCashInterestCurrent = this.UtilityMember.NumberSet.ToDouble(dvDesignate.Rows[0]["PAYMENT_CURRENT_AMOUNT_CASH"].ToString());
                        TotalPaymentBankInterestCurrent = this.UtilityMember.NumberSet.ToDouble(dvDesignate.Rows[0]["PAYMENT_CURRENT_AMOUNT_BANK"].ToString());
                        TotalCashInterestPrevious = this.UtilityMember.NumberSet.ToDouble(dvDesignate.Rows[0]["PREVIOUS_AMOUNT_CASH"].ToString());
                        TotalBankInterestPrevious = this.UtilityMember.NumberSet.ToDouble(dvDesignate.Rows[0]["PREVIOUS_AMOUNT_BANK"].ToString());
                        //xrBankInterest.Text = this.UtilityMember.NumberSet.ToNumber(TotalBankInterest);
                    }
                    else
                    {
                        xrReceiptCashInteresrYear.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }

        /// <summary>
        /// Fetch Report Source
        /// </summary>
        private void GetReportSource()
        {
            string dateason = GetProgressiveDate(this.ReportProperties.DateFrom);
            try
            {
                string Purpose = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6PurposeCashBank);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.ToDate(dateason), false));
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Purpose);

                    DataView dvCashFlow = resultArgs.DataSource.TableView;
                    if (dvCashFlow != null && dvCashFlow.Count != 0)
                    {
                        /*if (dvCashFlow != null && dvCashFlow.ToTable().Rows.Count != 0)
                        {
                            double frstCash = this.UtilityMember.NumberSet.ToDouble(dvCashFlow.ToTable().Compute("SUM(FIRST_CASH)", "").ToString());
                            xrFirstCash.Text = this.UtilityMember.NumberSet.ToNumber(frstCash);

                            double scndCash = this.UtilityMember.NumberSet.ToDouble(dvCashFlow.ToTable().Compute("SUM(SECOND_CASH)", "").ToString());
                            xrSecondCash.Text = this.UtilityMember.NumberSet.ToNumber(scndCash);
                        }
                        else
                        {
                            xrFirstCash.Text = xrSecondCash.Text = string.Empty;
                        }*/

                        dvCashFlow.Table.TableName = "FC6PURPOSELIST";
                        this.DataSource = dvCashFlow.Table;
                        this.DataMember = dvCashFlow.Table.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                    }

                    string expression = "(" + reportSetting1.FC6PURPOSELIST.PRE_CASHColumn.ColumnName + " + " + reportSetting1.FC6PURPOSELIST.FIRST_CASHColumn.ColumnName + " + " +
                                            reportSetting1.FC6PURPOSELIST.SECOND_CASHColumn.ColumnName + ") - " + reportSetting1.FC6PURPOSELIST.UTILISED_CASHColumn.ColumnName;
                    dvCashFlow.Table.Columns.Add(reportSetting1.FC6PURPOSELIST.BALANCE_CASHColumn.ColumnName, typeof(System.Double), expression);

                    expression = "(" + reportSetting1.FC6PURPOSELIST.PRE_BANKColumn.ColumnName + " + " + reportSetting1.FC6PURPOSELIST.FIRST_BANKColumn.ColumnName + " + " +
                                        reportSetting1.FC6PURPOSELIST.SECOND_BANKColumn.ColumnName + ")  - " + reportSetting1.FC6PURPOSELIST.UTILISED_BANKColumn.ColumnName;

                    dvCashFlow.Table.Columns.Add(reportSetting1.FC6PURPOSELIST.BALANCE_BANKColumn.ColumnName, typeof(System.Double), expression);

                    if (this.ReportProperties.IncludeAllPurposes == 0)
                    {
                        dvCashFlow.RowFilter = "";
                        dvCashFlow.RowFilter = reportSetting1.FC6PURPOSELIST.BALANCE_CASHColumn.ColumnName + " <> 0 OR " + reportSetting1.FC6PURPOSELIST.BALANCE_BANKColumn.ColumnName + " <> 0";                        
                    }
                    DataTable dtForeign = dvCashFlow.ToTable();
                    this.Detail.Visible = dtForeign.Rows.Count == 0 ? false : true;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
             
        #endregion
                       
        
        private void xrSumRecSecondCash_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtRptData = this.DataSource as DataTable;
                double scndCash = this.UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.FC6PURPOSELIST.SECOND_CASHColumn.ColumnName + ")", "").ToString());
                e.Result = scndCash + TotalReceiptCashInterestCurrent;
                e.Handled = true;
            }
        }
              

        private void xrSumPrevCashBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtRptData = this.DataSource as DataTable;
                double PrevCash = this.UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.FC6PURPOSELIST.PRE_CASHColumn.ColumnName + ")", "").ToString());
                //if (PrevCash > 0)
                //{
                    e.Result = PrevCash + TotalCashInterestPrevious;
                    e.Handled = true;
                //}
            }
        }
        
        private void xrSumUtilisedCash_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtRptData = this.DataSource as DataTable;
                double utilisedCash = this.UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.FC6PURPOSELIST.UTILISED_CASHColumn.ColumnName + ")", "").ToString());
                //if (utilisedCash > 0)
                //{
                    e.Result = utilisedCash + TotalPaymentCashInterestCurrent;
                    e.Handled = true;
                //}
            }
        }

        private void xrCashInterestPrev_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = TotalCashInterestPrevious;
            }
        }

        private void xrBankInterestPrev_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = TotalBankInterestPrevious;
            }
        }

        private void xrReceiptCashInteresrYear_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = TotalReceiptCashInterestCurrent;
            }
        }

        private void xrReceiptBankInteresrYear_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = TotalReceiptBankInterestCurrent;
            }
        }

        private void xrTotalReceiptBankInteresrYear_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = TotalReceiptCashInterestCurrent + TotalReceiptBankInterestCurrent;
            }
        }

        private void xrPaymentCashInteresrYear_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = TotalPaymentBankInterestCurrent;
            }
        }

        private void xrPaymentBankInteresrYear_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = TotalPaymentBankInterestCurrent;
            }
        }

        private void xrCashInterestBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = (TotalCashInterestPrevious + TotalReceiptCashInterestCurrent ) - TotalPaymentCashInterestCurrent;
            }
        }

        private void xrBankInterestBalance_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = (TotalBankInterestPrevious + TotalReceiptBankInterestCurrent) - TotalPaymentBankInterestCurrent;
            }
        }

        private void xrSumRecSecondBank_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtRptData = this.DataSource as DataTable;
                double scndCash = this.UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.FC6PURPOSELIST.SECOND_CASHColumn.ColumnName + ")", "").ToString());
                e.Result = scndCash + TotalReceiptBankInterestCurrent;
                e.Handled = true;
            }
        }

        private void xrSumUtilisedBank_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtRptData = this.DataSource as DataTable;
                double utilisedCash = this.UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.FC6PURPOSELIST.UTILISED_BANKColumn.ColumnName + ")", "").ToString());
                //if (utilisedCash > 0)
                //{
                e.Result = utilisedCash + TotalPaymentBankInterestCurrent;
                e.Handled = true;
                //}
            }
        }

        private void xrSumRecBankTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrSumPrevBankBalance_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtRptData = this.DataSource as DataTable;
                double PrevBank = this.UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.FC6PURPOSELIST.PRE_BANKColumn.ColumnName + ")", "").ToString());
                e.Result = PrevBank + TotalBankInterestPrevious;
                e.Handled = true;
            }
        }

        private void xrSumBalanceCashTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtRptData = this.DataSource as DataTable;
                double Cashbalance = this.UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.FC6PURPOSELIST.BALANCE_CASHColumn.ColumnName + ")", "").ToString());
                //if (Cashbalance > 0)
                //{
                e.Result = Cashbalance + ((TotalCashInterestPrevious + TotalReceiptCashInterestCurrent) - TotalPaymentCashInterestCurrent);
                e.Handled = true;
                //}
            }
        }

        private void xrSumBalanceBankTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dtRptData = this.DataSource as DataTable;
                double bankbalance = this.UtilityMember.NumberSet.ToDouble(dtRptData.Compute("SUM(" + reportSetting1.FC6PURPOSELIST.BALANCE_BANKColumn.ColumnName + ")", "").ToString());
                //if (Cashbalance > 0)
                //{
                e.Result = bankbalance + ((TotalBankInterestPrevious + TotalReceiptBankInterestCurrent) - TotalPaymentBankInterestCurrent);
                e.Handled = true;
                //}
            }
        }

      

      
       

    }
}
