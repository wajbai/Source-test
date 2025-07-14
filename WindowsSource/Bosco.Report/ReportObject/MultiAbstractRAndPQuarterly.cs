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
    public partial class MultiAbstractRAndPQuarterly: Bosco.Report.Base.ReportHeaderBase
    {
        public MultiAbstractRAndPQuarterly()
        {
            InitializeComponent();
            this.SetTitleWidth(xrSubMultiAbstractReceiptQtly.WidthF);
        }

        public override void ShowReport()
        {
            LoadMultiAbstractReport();
            
            //On 17/12/2018, Set Sign details
            FixReportPropertyForCMF();
            (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = this.PageWidth - 25;// xrSubMultiAbstractReceiptQtly.WidthF;
            (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
         
        }

        private void LoadMultiAbstractReport()
        {
            SetReportTitle();
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            MultiAbstractReceiptsPaymentsQuarterly multiAbstractReceiptsQtly = xrSubMultiAbstractReceiptQtly.ReportSource as MultiAbstractReceiptsPaymentsQuarterly;
            MultiAbstractReceiptsPaymentsQuarterly multiAbstractPaymentsQtly = xrSubMultiAbstractPaymentQtly.ReportSource as MultiAbstractReceiptsPaymentsQuarterly;
            multiAbstractReceiptsQtly.HideReportHeaderFooter();
            multiAbstractPaymentsQtly.HideReportHeaderFooter();
            
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                       || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                       || String.IsNullOrEmpty(this.ReportProperties.Project) || this.ReportProperties.Project=="0")
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
                        multiAbstractReceiptsQtly.BindMultiAbstractReceiptSource(true);
                        multiAbstractPaymentsQtly.BindMultiAbstractReceiptSource(false);
                        SplashScreenManager.CloseForm();
                        this.Margins.Left = multiAbstractReceiptsQtly.ReportLeftMargin;
                        this.Landscape = multiAbstractReceiptsQtly.IsLandscapeReport;
                        this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
                        this.SetTitleWidth(this.PageWidth - 15);
                        if (multiAbstractReceiptsQtly.NoOfQuters <= 2)
                        {
                            this.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
                        }
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
                    multiAbstractReceiptsQtly.BindMultiAbstractReceiptSource(true);
                    multiAbstractPaymentsQtly.BindMultiAbstractReceiptSource(false);
                    SplashScreenManager.CloseForm();
                    this.Landscape = multiAbstractReceiptsQtly.IsLandscapeReport;
                    this.Margins.Left = multiAbstractReceiptsQtly.ReportLeftMargin;
                    this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
                    this.SetTitleWidth(this.PageWidth - 15);
                    base.ShowReport();
                }
            }
        }

        private void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            PrintingSystemBase printingbase = sender as PrintingSystemBase;
            MultiAbstractReceipts multiAbstractReceipts = xrSubMultiAbstractReceiptQtly.ReportSource as MultiAbstractReceipts;
            multiAbstractReceipts.ReceiptsAndPayments = true;
            multiAbstractReceipts.PrintingSystem_PageSettingsChanged(sender, e);
            MultiAbstractPayments multiAbstractPayments = xrSubMultiAbstractPaymentQtly.ReportSource as MultiAbstractPayments;
            multiAbstractPayments.ReceiptsAndPayments = true;
            multiAbstractPayments.PrintingSystem_PageSettingsChanged(sender, e);
            this.Landscape = multiAbstractReceipts.Landscape;
            int newPageWidth = printingbase.PageBounds.Width - printingbase.PageMargins.Left - printingbase.PageMargins.Right;
            this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = newPageWidth - 15;
            this.SetTitleWidth(newPageWidth - 15);
            this.CreateDocument();
        }
    }
}
