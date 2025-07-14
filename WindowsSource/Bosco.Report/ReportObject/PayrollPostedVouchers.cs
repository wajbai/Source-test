using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using PAYROLL.Modules;
using System.Data;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Utility.ConfigSetting;
using Payroll.DAO.Schema;
using Bosco.Utility.Common;

namespace Bosco.Report.ReportObject
{
    public partial class PayrollPostedVouchers : ReportHeaderBase
    {
        #region Declaration
        DataTable dtPostedPayVoucher = new DataTable();
        DataTable dtPostedPayVoucherDetail = new DataTable();
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();
        ApplicationSchema.PRSTAFFGROUPDataTable dtstaffgrp = new ApplicationSchema.PRSTAFFGROUPDataTable();
        DataTable dtStaffPersonalInfo = new DataTable();
        
        #endregion

        #region Constructors

        public PayrollPostedVouchers()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom))
                this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom);
            if (string.IsNullOrEmpty(this.ReportProperties.DateTo))
                this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo);
        }
        #endregion

        #region Show Report
        public override void ShowReport()
        {
            using (clsPayrollBase paybase = new clsPayrollBase())
            {
                ResultArgs resultStaff = paybase.FetchPayrollStaff(clsPayrollBase.payslipGroupid.ToString(), string.Empty, string.Empty);
                if (resultStaff.Success)
                {
                    dtStaffPersonalInfo = resultStaff.DataSource.Table;
                    BindPayRegisterReport();
                }
                else
                {
                    MessageRender.ShowMessage(resultStaff.Message);
                }
            }
            base.ShowReport();
        }
        #endregion

        #region Methods

        public void BindPayRegisterReport()
        {
            
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo) &&
                this.ReportProperties.DateFrom.Trim() != string.Empty && this.ReportProperties.DateTo.Trim() != string.Empty)
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        ReportSetting();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowPayslipForm();
                    }
                }
                else
                {
                    ReportSetting();
                }
            }
            else
            {
                SetReportTitle();
                ShowPayslipForm();
            }
        }

        private void ReportSetting()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));

            this.SetLandscapeHeader = xrtblPayMonth.WidthF; //1165.50f;
            this.SetLandscapeFooter = xrtblPayMonth.WidthF; // 1165.50f;
            this.SetLandscapeFooterDateWidth = 900.00f;
            
            setHeaderTitleAlignment();
            SetReportTitle();

            xrTblHeader = AlignHeaderTable(xrTblHeader);
            xrtblPayMonth = AlignGroupTable(xrtblPayMonth);
            xrTblData = AlignContentTable(xrTblData);

            this.InstituteName = ReportProperty.Current.PayrollProjectTitle;
            this.LegalEntityAddress = ReportProperty.Current.PayrollProjectAddress;

            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom) + " - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo);
            this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : (string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupName) ? string.Empty : ReportProperty.Current.PayrollGroupName);
            this.HideDateRange = false;
            if (!String.IsNullOrEmpty(ReportProperty.Current.DateFrom))
            {
                this.ReportTitle += " from " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false).ToString("MMMM yyyy") +
                                    " to " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateTo, false).ToString("MMMM yyyy");
            }
            this.DisplayName = this.ReportTitle;
            //replace special characters which are not valid for file names
            this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
            //--------------------------------------------------------------------------------------
            this.ReportProperties.ShowPageNumber = 1;
            dtPostedPayVoucherDetail = null;
            bool reportexists = false;
            using (clsPayrollBase paybase = new clsPayrollBase())
            {
                ResultArgs resultArgs = paybase.PostedPayrollVouchers(ReportProperty.Current.DateFrom, ReportProperty.Current.DateTo);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    dtPostedPayVoucher = resultArgs.DataSource.Table;
                    dtPostedPayVoucher.TableName = this.DataMember;

                    (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrtblPayMonth.WidthF;
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();

                    this.DataSource = dtPostedPayVoucher;
                    this.DataMember = dtPostedPayVoucher.TableName;

                    resultArgs = paybase.PostedPayrollVouchersDetail(ReportProperty.Current.DateFrom, ReportProperty.Current.DateTo);
                    reportexists = (dtPostedPayVoucher.Rows.Count > 0);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        dtPostedPayVoucherDetail = resultArgs.DataSource.Table;
                    }
                }
                else
                {
                    reportexists = false;
                    MessageRender.ShowMessage(resultArgs.Message);
                }
            }

            grpHeaderPaymonth.Visible = Detail.Visible = true;
            if (!reportexists)
            {
                grpHeaderPaymonth.Visible = Detail.Visible = false;
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Int32 vid = 0;
            xrSubPostedPayrollVoucherDetail.Visible = true;
            if (this.GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName) != null)
            {
                vid = UtilityMember.NumberSet.ToInteger(this.GetCurrentColumnValue(reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName).ToString());
            }
            UcPostedPayrollVoucherDetail ucPostedPayrollVoucherDetail = xrSubPostedPayrollVoucherDetail.ReportSource as UcPostedPayrollVoucherDetail;
            dtPostedPayVoucherDetail.DefaultView.RowFilter = string.Empty;
            dtPostedPayVoucherDetail.DefaultView.RowFilter = reportSetting1.Ledger.VOUCHER_IDColumn.ColumnName + " = " + vid;
            DataTable dtPostedVouchers = dtPostedPayVoucherDetail.DefaultView.ToTable();
            ucPostedPayrollVoucherDetail.BindPostedPayrollVoucherDetails(dtPostedVouchers);
            ucPostedPayrollVoucherDetail.LedgerWidth = (xrCapVNo.WidthF + xrCapPayGroup.WidthF + xrCapProject.WidthF);
            ucPostedPayrollVoucherDetail.ComponentWidth = (xrCapLedger.WidthF );
            ucPostedPayrollVoucherDetail.DebitWidth = (xrCapCashBank.WidthF / 2);
            ucPostedPayrollVoucherDetail.CreditWidth = (xrCapCashBank.WidthF / 2);
            ucPostedPayrollVoucherDetail.RefNoWidth = xrCapAmount.WidthF;
        }

        
    }
}
