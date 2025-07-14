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
using System.Data;
using DevExpress.XtraCharts;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractReceiptsProjectCashBankView : Bosco.Report.Base.ReportHeaderBase
    {
        MultiAbstractReceiptsProjectCashBank multiAbstractReceipts;
        MultiAbstractReceiptsProjectCashBank multiAbstractPayments;

        public MultiAbstractReceiptsProjectCashBankView()
        {
            InitializeComponent();
            this.SetTitleWidth(xrSubMultiAbstractReceipt.WidthF);
        }

        public override void ShowReport()
        {

            //    SplashScreenManager.ShowForm(typeof(frmReportWait));
            LoadMultiAbstractReport();
            
            //On 17/12/2018, Set Sign details
            FixReportPropertyForCMF();
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = this.PageWidth - 50;//xrSubMultiAbstractReceipt.WidthF;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
            
            //   SplashScreenManager.CloseForm();
            // }

        }

        private void LoadMultiAbstractReport()
        {
            // this.ReportTitle = ReportProperty.Current.ReportTitle;
            // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            // this.ReportPeriod = "For the Period: " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            SetReportTitle();
            
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            multiAbstractReceipts = xrSubMultiAbstractReceipt.ReportSource as MultiAbstractReceiptsProjectCashBank;
            multiAbstractPayments = xrSubMultiAbstractPayment.ReportSource as MultiAbstractReceiptsProjectCashBank;
            multiAbstractReceipts.HideReportHeaderFooter();
            multiAbstractPayments.HideReportHeaderFooter();

            this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
            this.SetTitleWidth(this.PageWidth - 15);
                        
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                       || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                       || String.IsNullOrEmpty(this.ReportProperties.Project))
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
                        multiAbstractReceipts.BindMultiAbstractReceiptSource(true);
                        multiAbstractPayments.BindMultiAbstractReceiptSource(false);
                        
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
                    multiAbstractReceipts.BindMultiAbstractReceiptSource(true);
                    multiAbstractPayments.BindMultiAbstractReceiptSource(false);
                    SplashScreenManager.CloseForm();

                    base.ShowReport();
                }
            }
        }

        
    }
}
