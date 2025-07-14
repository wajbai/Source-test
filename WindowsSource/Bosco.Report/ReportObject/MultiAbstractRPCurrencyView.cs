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
    public partial class MultiAbstractRPCurrencyView : Bosco.Report.Base.ReportHeaderBase
    {
        MultiAbstractRPCurrency multiAbstractReceipts;
        MultiAbstractRPCurrency multiAbstractPayments;

        public MultiAbstractRPCurrencyView()
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
            SetReportTitle();
            
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            
            multiAbstractReceipts = xrSubMultiAbstractReceipt.ReportSource as MultiAbstractRPCurrency;
            multiAbstractPayments = xrSubMultiAbstractPayment.ReportSource as MultiAbstractRPCurrency;
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

            xrLblIncomeTitle.Text = "Income                Average Exchange Rates: €1 = "+ settingProperty.Currency +  
                                      UtilityMember.NumberSet.ToNumber(ReportProperties.AvgEuroExchangeRate) +
                                     "    €1 = $" + UtilityMember.NumberSet.ToNumber(ReportProperties.AvgEuroDollarExchangeRate);

            xrlblExpenditureTitle.Text = "Expenditure      Average Exchange Rates: €1 = " + settingProperty.Currency + 
                                    UtilityMember.NumberSet.ToNumber(ReportProperties.AvgEuroExchangeRate) +
                                "    €1 = $" + UtilityMember.NumberSet.ToNumber(ReportProperties.AvgEuroDollarExchangeRate);
        }

        
    }
}
