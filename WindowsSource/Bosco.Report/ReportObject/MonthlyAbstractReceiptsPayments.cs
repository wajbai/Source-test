using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class MonthlyAbstractReceiptsPayments : Bosco.Report.Base.ReportHeaderBase
    {
        public MonthlyAbstractReceiptsPayments()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            LoadMonthlyAbstractReport();
        }

        private void LoadMonthlyAbstractReport()
        {
            this.SetLandscapeBudgetNameWidth = xrSubreportMonthlyReceipts.WidthF;
            this.SetLandscapeHeader = xrSubreportMonthlyReceipts.WidthF;
            this.SetLandscapeFooter = xrSubreportMonthlyReceipts.WidthF;
            this.SetLandscapeFooterDateWidth = xrSubreportMonthlyReceipts.WidthF;
            SetTitleWidth(xrSubreportMonthlyReceipts.WidthF);

            SetReportTitle();
            
            setHeaderTitleAlignment();
            MonthlyAbstractReceipts montlyAbstractReceipts = xrSubreportMonthlyReceipts.ReportSource as MonthlyAbstractReceipts;
            MonthlyAbstractPayments montlyAbstractPayments = xrSubreportMontlyPayments.ReportSource as MonthlyAbstractPayments;
            this.AttachDrillDownToSubReport(montlyAbstractReceipts);
            this.AttachDrillDownToSubReport(montlyAbstractPayments);
            montlyAbstractPayments.Flag = 1;
            montlyAbstractReceipts.Flag = 1;
            montlyAbstractReceipts.HideReportHeaderFooter();
            montlyAbstractPayments.HideReportHeaderFooter();
            
            //On 17/12/2018, Set Sign details
            FixReportPropertyForCMF();
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrSubreportMonthlyReceipts.WidthF;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
            

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                       || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                       || this.ReportProperties.Project == "0")
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
                        montlyAbstractReceipts.BindReceiptSource();
                        montlyAbstractPayments.BindPaymentSource();
                        SplashScreenManager.CloseForm();
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
                    montlyAbstractReceipts.BindReceiptSource();
                    montlyAbstractPayments.BindPaymentSource();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            this.SetReportDate = this.ReportProperties.ReportDate;
        }
    }
}
