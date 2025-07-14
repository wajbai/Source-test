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
using System.Globalization;


namespace Bosco.Report.ReportObject
{

    public partial class Budgetmonth : ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        string MonthCurrentYear = string.Empty;
        string MonthPreviousYear = string.Empty;
        public Budgetmonth()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(this.ReportProperties.BudgetId) && !string.IsNullOrEmpty(this.ReportProperties.BudgetId))
            {
                FetchBudgetDistribution();

                //Year
                string FinCalPerioCurrentYear = String.Format("{0}-{1}", UtilityMember.DateSet.ToDate(AppSetting.YearFrom, true).Year,
               UtilityMember.DateSet.ToDate(AppSetting.YearTo, true).ToString("yy"));

                xrtblAnnualBudgetYear.Text = String.Format("Expense for {0}", FinCalPerioCurrentYear);

                //Month
                string Previous = new DateTime(this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateAsOn, false).Year, this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateAsOn, false).AddMonths(-1).Month, this.UtilityMember.DateSet.ToDate(this.ReportProperties.DateAsOn, false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                string Current = new DateTime(this.UtilityMember.DateSet.ToDate(this.ReportProperties.AccounYear, false).Year, this.UtilityMember.DateSet.ToDate(this.ReportProperties.AccounYear, false).Month, this.UtilityMember.DateSet.ToDate(this.ReportProperties.AccounYear, false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                MonthPreviousYear = String.Format("{0}-{1}", Previous, UtilityMember.DateSet.ToDate(this.ReportProperties.DateAsOn, true).AddMonths(-1).Year);
                MonthCurrentYear = String.Format("{0}-{1}", Current, UtilityMember.DateSet.ToDate(this.ReportProperties.AccounYear, true).Year);

                xrtblCurrentMonth.Text = String.Format(MonthCurrentYear);
                xrtblPreviousMonth.Text = string.Format("Expense for {0}", MonthPreviousYear);
            }
        }
        #region Show Reports
        public override void ShowReport()
        {
            FetchBudgetDistribution();
        }
        #endregion

        public void FetchBudgetDistribution()
        {
            FetchBudgetMonthDistributionDetails();
            base.ShowReport();
        }

        private void FetchBudgetMonthDistributionDetails()
        {
            try
            {
                this.SetLandscapeHeader = 1135.25f;
                this.SetLandscapeFooter = 1135.25f;
                this.SetLandscapeFooterDateWidth = 960.25f;
                setHeaderTitleAlignment();
                SetReportTitle();

                string Current = new DateTime(this.UtilityMember.DateSet.ToDate(this.ReportProperties.AccounYear, false).Year, this.UtilityMember.DateSet.ToDate(this.ReportProperties.AccounYear, false).Month, this.UtilityMember.DateSet.ToDate(this.ReportProperties.AccounYear, false).Day).ToString("MMM", CultureInfo.InvariantCulture);
                MonthCurrentYear = String.Format("{0}-{1}", Current, UtilityMember.DateSet.ToDate(this.ReportProperties.AccounYear, true).Year);
                this.ReportTitle = String.Format("Proposed Budget for the Month of {0} ", String.Format(MonthCurrentYear));

                this.ReportPeriod = string.Empty;
                this.ReportSubTitle = String.Format( String.IsNullOrEmpty(this.ReportProperties.BudgetName)? string.Empty: this.ReportProperties.BudgetName );

                if (!string.IsNullOrEmpty(this.ReportProperties.BudgetId) && !string.IsNullOrEmpty(this.ReportProperties.BudgetId))
                {
                    string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetMonthDistribution);
                    using (DataManager dataManager = new DataManager())
                    {
                        dataManager.Parameters.Add(this.reportSetting1.BUDGET_STATISTICS.BUDGET_IDColumn, this.ReportProperties.BudgetId);
                        dataManager.Parameters.Add(this.reportSetting1.ReportParameter.PROJECT_IDColumn, this.ReportProperties.ProjectId);

                        dataManager.Parameters.Add(this.reportSetting1.ReportParameter.YEAR_FROMColumn, this.ReportProperties.DateFrom);
                        dataManager.Parameters.Add(this.reportSetting1.ReportParameter.YEAR_TOColumn, this.ReportProperties.DateTo);

                        dataManager.Parameters.Add(this.reportSetting1.ReportParameter.DATE_FROMColumn, this.ReportProperties.DateAsOn);
                        dataManager.Parameters.Add(this.reportSetting1.ReportParameter.DATE_TOColumn, this.ReportProperties.AccounYear);

                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);

                        DataTable dt = resultArgs.DataSource.Table;
                        dt.TableName = "BudgetMonth";
                        SetReportBorder();

                        this.DataSource = dt;
                        this.DataMember = dt.TableName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }

        private void SetReportBorder()
        {
        }

        private void xrBudgetGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetgroup = label.Text;
            label.Text = budgetgroup.ToString().ToUpper();
        }

        private void xrBudgetNature_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = (XRTableCell)sender;
            string budgetnature = label.Text;
            label.Text = budgetnature.ToString().ToUpper();
        }

        private void xrcellBudgetSubGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double AnnualBudgeted = this.ReportProperties.NumberSet.ToDouble(xrcellBudgetSubGroup.Text);
            if (AnnualBudgeted != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrcellBudgetSubGroup.Text = "";
            }
        }

        private void xrCellApproved_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double AnnualActual = this.ReportProperties.NumberSet.ToDouble(xrCellApproved.Text);
            if (AnnualActual != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCellApproved.Text = "";
            }
        }

        private void xrCellActualAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double AnnualVariance = this.ReportProperties.NumberSet.ToDouble(xrCellActualAmt.Text);
            if (AnnualVariance != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCellActualAmt.Text = "";
            }
        }

        private void xrTableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double Balance = this.ReportProperties.NumberSet.ToDouble(xrTableCell17.Text);
            if (Balance != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCell17.Text = "";
            }
        }

        private void xrTableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double pMonthBudget = this.ReportProperties.NumberSet.ToDouble(xrTableCell24.Text);
            if (pMonthBudget != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCell24.Text = "";
            }
        }

        private void xrTableCell26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double pMonthActual = this.ReportProperties.NumberSet.ToDouble(xrTableCell26.Text);
            if (pMonthActual != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCell26.Text = "";
            }
        }

        private void xrTableCell25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double pMonthvairance = this.ReportProperties.NumberSet.ToDouble(xrTableCell25.Text);
            if (pMonthvairance != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCell25.Text = "";
            }
        }

        private void xrCellInPercentage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double MonthCurrentBuudget = this.ReportProperties.NumberSet.ToDouble(xrCellInPercentage.Text);
            if (MonthCurrentBuudget != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCellInPercentage.Text = "";
            }
        }

        private void xrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double PreApprovedBudget = this.ReportProperties.NumberSet.ToDouble(xrTableCell8.Text);
            if (PreApprovedBudget != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCell8.Text = "";
            }
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double MonthApprovedBudget = this.ReportProperties.NumberSet.ToDouble(xrTableCell15.Text);
            if (MonthApprovedBudget != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCell15.Text = "";
            }
        }
    }
}
