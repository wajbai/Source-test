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
    public partial class MultiAbstractReceiptsAndPayments : Bosco.Report.Base.ReportHeaderBase
    {
        public MultiAbstractReceiptsAndPayments()
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
            

            MultiAbstractReceipts multiAbstractReceipts = xrSubMultiAbstractReceipt.ReportSource as MultiAbstractReceipts;
            MultiAbstractPayments multiAbstractPayments = xrSubMultiAbstractPayment.ReportSource as MultiAbstractPayments;
            multiAbstractReceipts.HideReportHeaderFooter();
            multiAbstractPayments.HideReportHeaderFooter();
            
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
                        multiAbstractReceipts.BindMultiAbstractReceiptSource();
                        multiAbstractPayments.BindMultiAbstractPaymentSource();
                        SplashScreenManager.CloseForm();
                        this.Margins.Left = multiAbstractReceipts.ReportLeftMargin;
                        this.Margins.Right = multiAbstractReceipts.ReportRightMargin;
                        this.Landscape = multiAbstractReceipts.IsLandscapeReport;
                        this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 15;
                        this.SetTitleWidth(this.PageWidth - 15);
                                                
                        if (multiAbstractReceipts.NoOfMonths <= 2)
                        {
                            this.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
                        }
                        else if (multiAbstractReceipts.NoOfMonths == 11)
                        {
                            this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = (this.PageWidth - 35);
                            //this.SetTitleWidth(this.PageWidth);
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
                    multiAbstractReceipts.BindMultiAbstractReceiptSource();
                    multiAbstractPayments.BindMultiAbstractPaymentSource();
                    SplashScreenManager.CloseForm();
                    this.Landscape = multiAbstractReceipts.IsLandscapeReport;
                    this.Margins.Left = multiAbstractReceipts.ReportLeftMargin;
                    this.Margins.Right = multiAbstractReceipts.ReportRightMargin;
                    this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = this.PageWidth - 25;
                    this.SetTitleWidth(this.PageWidth - 25);

                    if (multiAbstractReceipts.NoOfMonths <= 2)
                    {
                        this.PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
                    }
                    else if (multiAbstractReceipts.NoOfMonths == 12)
                    {
                        this.SetLandscapeHeader = this.SetLandscapeFooter = this.SetLandscapeFooterDateWidth = (this.PageWidth - 35);
                        //this.SetTitleWidth(this.PageWidth);
                    }
                    
                    base.ShowReport();
                }
            }
        }

        private void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            PrintingSystemBase printingbase = sender as PrintingSystemBase;
            MultiAbstractReceipts multiAbstractReceipts = xrSubMultiAbstractReceipt.ReportSource as MultiAbstractReceipts;
            multiAbstractReceipts.ReceiptsAndPayments = true;
            multiAbstractReceipts.PrintingSystem_PageSettingsChanged(sender, e);
            MultiAbstractPayments multiAbstractPayments = xrSubMultiAbstractPayment.ReportSource as MultiAbstractPayments;
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
