using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetDetails : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        public BudgetDetails()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            BindBudgetDetails();
        }
        #endregion
        
        public void BindBudgetDetails()
        {

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Budget) || this.ReportProperties.Budget.Split(',').Length == 0)
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        FetchBudgetDetails();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    FetchBudgetDetails();
                    base.ShowReport();
                }
            }
        }

        private void FetchBudgetDetails()
        {
            try
            {
                this.BudgetName = ReportProperty.Current.BudgetName;
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                SetReportTitle();
                AssignBudgetDateRangeTitle();
                SetTitleWidth(xrtblHeader.WidthF);
                this.SetLandscapeBudgetNameWidth = xrtblHeader.WidthF;
                this.SetLandscapeHeader = xrtblHeader.WidthF;
                this.SetLandscapeFooter = xrtblHeader.WidthF;
                this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;
                setHeaderTitleAlignment();

                //Hide budget group for ABE 
                //if (AppSetting.IS_ABEBEN_DIOCESE)
                if (AppSetting.IS_ABEBEN_DIOCESE || AppSetting.IS_DIOMYS_DIOCESE)
                {
                    grpBudgetGroup.Visible = false;
                    xrtblHeader.SuspendLayout();
                    if (xrHeaderRow.Cells.Contains(xrcellHeaderBudgetSubGroup))
                        xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrcellHeaderBudgetSubGroup.Name]);
                    xrtblHeader.PerformLayout();

                    xrtblBudget.SuspendLayout();
                    if (xrDataRow.Cells.Contains(xrcellBudgetSubGroup))
                        xrDataRow.Cells.Remove(xrDataRow.Cells[xrcellBudgetSubGroup.Name]);
                    xrtblBudget.PerformLayout();

                    xrcellTotal.WidthF = xrcellHeaderParticular.WidthF + xrcellHeaderCode.WidthF;
                    xrcellSumProposal.WidthF = xrcellHeaderSumProposal.WidthF;
                    xrcellSumApproval.WidthF = xrcellHeaderSumApproval.WidthF;
                }

                string budgetvariance = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetDetails);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.Budget);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                    //On 10/12/2024, To to currency based reports --------------------------------------------------------------------------------------
                    decimal CurrencyAvgExchangeRate = 1;
                    decimal CurrencyPreviousYearAvgExchangeRate = 1;
                    if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                    {
                        CurrencyAvgExchangeRate = GetAvgCurrencyExchangeRateForFY(this.ReportProperties.CurrencyCountryId);
                        CurrencyPreviousYearAvgExchangeRate = GetAvgCurrencyExchangeRateForPreviousFY(this.ReportProperties.CurrencyCountryId);
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                    }
                    else
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                    }
                    dataManager.Parameters.Add(reportSetting1.Country.EXCHANGE_RATEColumn, CurrencyAvgExchangeRate);
                    dataManager.Parameters.Add(reportSetting1.Country.PREVIOUS_YEAR_EX_RATEColumn, CurrencyPreviousYearAvgExchangeRate);
                    //----------------------------------------------------------------------------------------------------------------------------------

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetvariance);

                    Detail.SortFields.Add(new GroupField("LEDGER_CODE"));
                    Detail.SortFields.Add(new GroupField("BUDGET_SUB_GROUP"));

                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtBudgetDetails = resultArgs.DataSource.Table;
                        AppendHOHelpAmountInIncome(dtBudgetDetails);
                        this.DataSource = dtBudgetDetails;
                        this.DataMember = resultArgs.DataSource.Table.TableName;
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
        
        /// <summary>
        /// This method is used to attach HO/Province Help amount with Income side of Budget
        /// </summary>
        /// <param name="dtIncomeSource"></param>
        private void AppendHOHelpAmountInIncome(DataTable dtBudget)
        {
            decimal HOBudgetHelpPropsedAmt = 0;
            decimal HOBudgetHelpApprovedAmt = 0;
            ResultArgs resultArgs = new ResultArgs();
            if (dtBudget!=null && dtBudget.Rows.Count > 0 && AppSetting.ENABLE_BUDGET_HO_HELP_AMOUNT)
            {
                string budgetinfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetInfo);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.BUDGET_IDColumn, this.ReportProperties.Budget);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetinfo);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtBudgetInfo = resultArgs.DataSource.Table;
                        string strHOBudgetHelpPropsedAmt = dtBudgetInfo.Compute("SUM(" + reportSetting1.BUDGETVARIANCE.HO_HELP_PROPOSED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString();
                        string strHOBudgetHelpApprovedAmt = dtBudgetInfo.Compute("SUM(" + reportSetting1.BUDGETVARIANCE.HO_HELP_APPROVED_AMOUNTColumn.ColumnName + ")", string.Empty).ToString();

                        HOBudgetHelpPropsedAmt = UtilityMember.NumberSet.ToDecimal(strHOBudgetHelpPropsedAmt);
                        HOBudgetHelpApprovedAmt = UtilityMember.NumberSet.ToDecimal(strHOBudgetHelpApprovedAmt);

                        DataRow drHOBudgetHelpAmount = dtBudget.NewRow();
                        drHOBudgetHelpAmount[reportSetting1.BUDGET_LEDGER.LEDGER_GROUPColumn.ColumnName] = string.Empty;
                        drHOBudgetHelpAmount[reportSetting1.ReportParameter.LEDGER_IDColumn.ColumnName] = 0;
                        drHOBudgetHelpAmount[reportSetting1.Ledger.LEDGER_CODEColumn.ColumnName] = string.Empty;//To make to come first order
                        drHOBudgetHelpAmount[reportSetting1.Ledger.LEDGER_NAMEColumn.ColumnName] = AppSetting.BUDGET_HO_HELP_AMOUNT_CAPTION; //Make come fist order
                        drHOBudgetHelpAmount[reportSetting1.Ledger.NATURE_IDColumn.ColumnName] = (int)Natures.Income;
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName] = Natures.Income.ToString();
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.PROPOSED_AMOUNTColumn.ColumnName] = HOBudgetHelpPropsedAmt;
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName] = HOBudgetHelpApprovedAmt;
                        
                        drHOBudgetHelpAmount[reportSetting1.ReportParameter.TRANS_MODEColumn.ColumnName] = TransSource.Cr.ToString().ToUpper();
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_GROUPColumn.ColumnName] = " ";
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_GROUP_IDColumn.ColumnName] = 0;
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_SUB_GROUPColumn.ColumnName] = string.Empty;
                        drHOBudgetHelpAmount[reportSetting1.BUDGETVARIANCE.BUDGET_NATUREColumn.ColumnName] = Natures.Income.ToString();
                        dtBudget.Rows.InsertAt(drHOBudgetHelpAmount,0);
                    }
                }
            }
        }

        private void SetReportBorder()
        {
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xrtblBudget = AlignContentTable(xrtblBudget);
        }

        private void xrBudgetGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetgroup = label.Text.Trim();
            label.Text = budgetgroup.ToString().ToUpper();
        }

        private void xrBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetnature = label.Text;
            label.Text = budgetnature.ToString().ToUpper();
        }

        private void grpBudgetGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataRowView drvcrrentrow = (DataRowView)this.GetCurrentRow();
            if (drvcrrentrow != null)
            {
                string budgetnature = drvcrrentrow["BUDGET_NATURE"].ToString().Trim();
                string budgetgroup = drvcrrentrow["BUDGET_GROUP"].ToString().Trim();
                if (budgetnature.ToUpper() == "INCOME" && string.IsNullOrEmpty(budgetgroup))
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }
    }
}
