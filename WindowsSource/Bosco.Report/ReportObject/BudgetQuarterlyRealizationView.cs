using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class BudgetQuarterlyRealizationView : Bosco.Report.Base.ReportHeaderBase
    {
        public BudgetQuarterlyRealizationView()
        {
            InitializeComponent();
            //this.SetTitleWidth(xrSubBudgeIncomeQtly.WidthF-50);
        }

        public override void ShowReport()
        {
            ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(AppSetting.YearFrom);
            ReportProperties.DateTo = UtilityMember.DateSet.ToDate(AppSetting.YearTo);
            this.BudgetName = ReportProperty.Current.BudgetName;

            LoadBudgetQuaterlyRealization();

            //On 17/12/2018, Set Sign details
            FixReportPropertyForCMF();
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = this.PageWidth - 35;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
        }

        private void LoadBudgetQuaterlyRealization()
        {
            SetReportTitle();
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            BudgetQuarterlyRealization budgetQuaterlyIncomeLedgers = xrSubBudgeIncomeQtly.ReportSource as BudgetQuarterlyRealization;
            BudgetQuarterlyRealization budgetQuaterlyExpenseLedgers = xrSubBudgetExpenditureQtly.ReportSource as BudgetQuarterlyRealization;
            budgetQuaterlyIncomeLedgers.HideReportHeaderFooter();
            budgetQuaterlyExpenseLedgers.HideReportHeaderFooter();

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                       || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                       || String.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project == "0")
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        budgetQuaterlyIncomeLedgers.BindBudgetQuaterlySource(true);
                        budgetQuaterlyExpenseLedgers.BindBudgetQuaterlySource(false);
                        SplashScreenManager.CloseForm();
                        this.Margins.Left = budgetQuaterlyIncomeLedgers.ReportLeftMargin;
                        this.Landscape = budgetQuaterlyIncomeLedgers.IsLandscapeReport;
                        this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
                        this.SetTitleWidth(this.PageWidth - 15);
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    budgetQuaterlyIncomeLedgers.BindBudgetQuaterlySource(true);
                    budgetQuaterlyExpenseLedgers.BindBudgetQuaterlySource(false);
                    SplashScreenManager.CloseForm();
                    this.Landscape = budgetQuaterlyIncomeLedgers.IsLandscapeReport;
                    this.Margins.Left = budgetQuaterlyIncomeLedgers.ReportLeftMargin;
                    this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;

                    this.SetTitleWidth(this.PageWidth - 15);
                    base.ShowReport();
                }
            }
        }

    }
}
