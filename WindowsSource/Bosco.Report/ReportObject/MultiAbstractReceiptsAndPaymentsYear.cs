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
    public partial class MultiAbstractReceiptsAndPaymentsYear : Bosco.Report.Base.ReportHeaderBase
    {
        MultiAbstractReceiptsYear multiAbstractReceipts;
        MultiAbstractReceiptsYear multiAbstractPayments;

        public MultiAbstractReceiptsAndPaymentsYear()
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
            //On 27/01/2021, To set Year From/Year To Title ----------------------------------------------------------------------------------
            string YearFrom = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
            string YearTo = UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
            if (this.ReportProperties.NoOfYears > 0)
            {
                YearFrom = (UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year - this.ReportProperties.NoOfYears).ToString();
            }
            //--------------------------------------------------------------------------------------------------------------------------------
            this.ReportPeriod = "Year From " + YearFrom + " To " + YearTo;
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            multiAbstractReceipts = xrSubMultiAbstractReceipt.ReportSource as MultiAbstractReceiptsYear;
            multiAbstractPayments = xrSubMultiAbstractPayment.ReportSource as MultiAbstractReceiptsYear;
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
                        grpFooterChartReport.Visible = (this.ReportProperties.ChartViewType > 0);
                        grpFooterChartReport.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;
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
                    grpFooterChartReport.Visible = (this.ReportProperties.ChartViewType > 0);
                    grpFooterChartReport.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;
                    SplashScreenManager.CloseForm();

                    base.ShowReport();
                }
            }
        }

        /// <summary>
        /// On 22/01/2021, Report Chart Properties
        /// </summary>
        private void AssignChartProperties(DataTable dtGrandReceipts, double TotalReceipts, DataTable dtGrandPayments, double TotalPayments)
        {
            if (grpFooterChartReport.Visible)
            {
                //For Arrage Date in to single --------------------------------------------------------
                double receiptamount = 0;
                double paymentamount = 0;
                DataTable dtRPChart = dtGrandReceipts.DefaultView.ToTable();
                dtRPChart.Columns["AMOUNT"].ColumnName = "AMOUNT_RECEIPT";
                dtRPChart.Columns.Add("AMOUNT_PAYMENT", dtGrandReceipts.Columns["AMOUNT"].DataType);
                dtRPChart.Columns.Add("AMOUNT_RECEIPT_PERCENTAGE", dtGrandReceipts.Columns["AMOUNT"].DataType);
                dtRPChart.Columns.Add("AMOUNT_PAYMENT_PERCENTAGE", dtGrandReceipts.Columns["AMOUNT"].DataType);

                foreach (DataRow dr in dtGrandPayments.Rows)
                {
                    dtRPChart.DefaultView.RowFilter = string.Empty;
                    dtRPChart.DefaultView.RowFilter = "AC_YEAR_NAME=" +  UtilityMember.NumberSet.ToInteger(dr["AC_YEAR_NAME"].ToString());
                    if (dtRPChart.DefaultView.Count > 0)
                    {
                        dtRPChart.DefaultView[0].BeginEdit();
                        receiptamount = UtilityMember.NumberSet.ToDouble(dtRPChart.DefaultView[0]["AMOUNT_RECEIPT"].ToString());
                        paymentamount = UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                        dtRPChart.DefaultView[0]["AMOUNT_PAYMENT"] = paymentamount;

                        if (receiptamount > 0 && TotalReceipts > 0)
                        {
                            dtRPChart.DefaultView[0]["AMOUNT_RECEIPT_PERCENTAGE"] = Math.Round((receiptamount / TotalReceipts) * 100, 2);
                        }
                        if (paymentamount > 0 && TotalPayments > 0)
                        {
                            dtRPChart.DefaultView[0]["AMOUNT_PAYMENT_PERCENTAGE"] = Math.Round((paymentamount / TotalPayments) * 100, 2);
                        }
                        dtRPChart.DefaultView[0].EndEdit();
                    }
                    dtRPChart.DefaultView.RowFilter = string.Empty;
                }
                //-------------------------------------------------------------------------------------
                
                grpFooterChartReport.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
                grpFooterChartReport.Visible = true;
                UcChart reportchart = xrSubChartReport.ReportSource as UcChart;

                reportchart.ChartHeight = 500;
                reportchart.ChartWidth = xrSubChartReport.WidthF - 5;
                reportchart.ChartDataSouce = dtRPChart;

                reportchart.ClearChartTitle = true;
                reportchart.AddChartTitle = this.ReportTitle;
                reportchart.AddChartTitle = this.ReportPeriod;

                reportchart.ChartSeriesProperties.Clear();
                ChartSeries newChartSeriesProperties = new ChartSeries("Receipts", new string[] { "AMOUNT_RECEIPT" }, "AC_YEAR", dtRPChart);
                if (ReportProperty.Current.ChartInPercentage == 1)
                {
                    newChartSeriesProperties = new ChartSeries("Receipts", new string[] { "AMOUNT_RECEIPT_PERCENTAGE" }, "AC_YEAR", dtRPChart);
                }
                reportchart.ChartSeriesProperties.Add("Receipts", newChartSeriesProperties);

                newChartSeriesProperties = new ChartSeries("Payments", new string[] { "AMOUNT_PAYMENT" }, "AC_YEAR", dtRPChart);
                if (ReportProperty.Current.ChartInPercentage == 1)
                {
                    newChartSeriesProperties = new ChartSeries("Payments", new string[] { "AMOUNT_PAYMENT_PERCENTAGE" }, "AC_YEAR", dtRPChart);
                }
                reportchart.ChartSeriesProperties.Add("Payments", newChartSeriesProperties);

                reportchart.GenerateReportChart();
            }
        }

        private void grpFooterChartReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            AssignChartProperties(multiAbstractReceipts.RPTotalTable, multiAbstractReceipts.TotalReceipts, multiAbstractPayments.RPTotalTable, multiAbstractPayments.TotalPayments);
        }
    }
}
