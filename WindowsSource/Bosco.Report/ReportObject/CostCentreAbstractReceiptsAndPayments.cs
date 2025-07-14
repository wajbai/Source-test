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
    public partial class CostCentreAbstractReceiptsAndPayments : Bosco.Report.Base.ReportHeaderBase
    {

        public CostCentreAbstractReceiptsAndPayments()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {

            // SplashScreenManager.ShowForm(typeof(frmReportWait));
            LoadMonthlyAbstractReport();
            //  SplashScreenManager.CloseForm();

            //}



        }

        private void LoadMonthlyAbstractReport()
        {
            // this.ReportTitle = ReportProperty.Current.ReportTitle;
            // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            setHeaderTitleAlignment();
            SetReportTitle();
            this.CosCenterName = ReportProperty.Current.CostCentreName;
            //this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            this.HideCostCenter = (ReportProperties.ShowByCostCentre == 0);
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            CCMothlyReceipts montlyAbstractReceipts = xrSubMonthlyReceipts.ReportSource as CCMothlyReceipts;
            CostCentreMontlyAbstractPayments montlyAbstractPayments = xrSubreport1.ReportSource as CostCentreMontlyAbstractPayments;
            this.AttachDrillDownToSubReport(montlyAbstractReceipts);
            this.AttachDrillDownToSubReport(montlyAbstractPayments);
            montlyAbstractReceipts.HideReportHeaderFooter();
            montlyAbstractPayments.HideReportHeaderFooter();
            
            
            
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                            || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0")
            {
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        montlyAbstractReceipts.BindReceiptSource();
                        montlyAbstractPayments.BindPaymentSource();
                        base.ShowReport();
                    }
                    else
                    {
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    montlyAbstractReceipts.BindReceiptSource();
                    montlyAbstractPayments.BindPaymentSource();                    
                    base.ShowReport();
                }
            }
        }

    }
}
