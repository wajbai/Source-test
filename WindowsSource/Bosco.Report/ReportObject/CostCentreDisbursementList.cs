using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class CostCentreDisbursementList : Bosco.Report.Base.ReportHeaderBase
    {

        #region Declaration
        ResultArgs resultArgs = new ResultArgs();
        public double ClosingBalance { get; set; }
        #endregion

        #region Constructor
        public CostCentreDisbursementList()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            ClosingBalance = 0;
            TransactionDetailsReport();
        }

        private void TransactionDetailsReport()
        {
            SetReportTitle();
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
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
                        setHeaderTitleAlignment();
                        this.SetLandscapeHeader = 1060.25f;
                        this.SetLandscapeFooter = 1050.25f;
                        this.SetLandscapeFooterDateWidth = 890.00f;

                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                        resultArgs = GetReportSource();
                        this.DataSource = null;
                        DataView dvCashBankBook = resultArgs.DataSource.TableView;
                        if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                        {
                            dvCashBankBook.Table.TableName = "DisbursementList";
                            this.DataSource = dvCashBankBook;
                            this.DataMember = dvCashBankBook.Table.TableName;
                        }

                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    setHeaderTitleAlignment();
                    this.SetLandscapeHeader = 1060.25f;
                    this.SetLandscapeFooter = 1050.25f;
                    this.SetLandscapeFooterDateWidth = 890.00f;

                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                    resultArgs = GetReportSource();
                    this.DataSource = null;
                    DataView dvCashBankBook = resultArgs.DataSource.TableView;
                    if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                    {
                        dvCashBankBook.Table.TableName = "DisbursementList";
                        this.DataSource = dvCashBankBook;
                        this.DataMember = dvCashBankBook.Table.TableName;
                    }

                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            GroupField gfDonor = new GroupField("DONOR_NAME", XRColumnSortOrder.Ascending);
            GroupField gfDate = new GroupField("DATE", XRColumnSortOrder.Ascending);

            //  Detail.SortFields.Add(gfDonor);
            Detail.SortFields.Add(gfDate);

        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string Query = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCentreDisbursementList);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Query);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void xrDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string dtDate = string.Empty;

            dtDate = GetCurrentColumnValue("DATE") != null ?
                GetCurrentColumnValue("DATE").ToString() : string.Empty;

            if (!string.IsNullOrEmpty(dtDate))
            {
                xrDate.Text = UtilityMember.DateSet.ToDate(dtDate, false).ToShortDateString();
                e.Cancel = false;
            }
            else
            {
                xrDate.Text = "";
            }
        }

        private void xrTableCell10_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            ClosingBalance = 0;
        }

        private void xrTableCell10_SummaryReset(object sender, EventArgs e)
        {
            ClosingBalance = 0;
        }

        private void xrTableCell10_SummaryRowChanged(object sender, EventArgs e)
        {
            ClosingBalance += GetCurrentColumnValue("CLOSING_BALANCE") != null ?
               UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("CLOSING_BALANCE").ToString()) : 0;
        }

        private void xrPaymentBank_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPaymentBank.Text =UtilityMember.NumberSet.ToNumber(ClosingBalance).ToString();
            e.Cancel = false;
        }
    }
}
