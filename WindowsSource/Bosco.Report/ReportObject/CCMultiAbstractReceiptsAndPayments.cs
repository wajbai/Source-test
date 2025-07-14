using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class CCMultiAbstractReceiptsAndPayments : Bosco.Report.Base.ReportHeaderBase
    {

        public CCMultiAbstractReceiptsAndPayments()
        {
            InitializeComponent();
            this.SetTitleWidth(xrCosSubMultiAbstractReceipt.WidthF);
        }

        public override void ShowReport()
        {
            //if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo) ||
            //    this.ReportProperties.Project == "0" || this.ReportProperties.CostCentre == "0")
            //{
            //    ShowReportFilterDialog();
            //}
            //else
            //{
            //  SplashScreenManager.ShowForm(typeof(frmReportWait));
            LoadMultiAbstractReport();
            //  SplashScreenManager.CloseForm();
            // }


        }

        private void LoadMultiAbstractReport()
        {
            //this.ReportTitle = ReportProperty.Current.ReportTitle;
            //this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            SetReportTitle();
            this.CosCenterName = ReportProperty.Current.CostCentreName;
            // this.ReportPeriod = "For the Period: " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            CCMultiAbstractReceipts multiAbstractReceipts = xrCosSubMultiAbstractReceipt.ReportSource as CCMultiAbstractReceipts;
            CCMultiAbstractPayments multiAbstractPayments = xrCosSubMultiAbstractPayment.ReportSource as CCMultiAbstractPayments;

            multiAbstractReceipts.HideReportHeaderFooter();
            multiAbstractPayments.HideReportHeaderFooter();
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo) ||
                this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
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
                    base.ShowReport();
                }
            }
        }
    }
}
