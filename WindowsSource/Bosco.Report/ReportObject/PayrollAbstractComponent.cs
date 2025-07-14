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

namespace Bosco.Report.ReportObject
{
    public partial class PayrollAbstractComponent : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        clsPayrollBase PayrollBase = new clsPayrollBase();
        double Amount = 0;
        #endregion

        #region Constructor
        public PayrollAbstractComponent()
        {
            InitializeComponent();

        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            Amount = 0;
            BindAbstractReport();
            base.ShowReport();
        }
        #endregion

        #region Methods
        private void BindAbstractReport()
        {
            if (this.ReportProperties.PayrollId.Trim() != string.Empty && this.ReportProperties.PayrollId != "0" && this.ReportProperties.PayrollComponentId.Trim() != string.Empty && this.ReportProperties.PayrollComponentId != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        setHeaderTitleAlignment();
                        SetReportTitle();

                        this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + ReportProperty.Current.PayrollName;
                        this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : ReportProperty.Current.PayrollGroupName;
                        this.CosCenterName = ReportProperty.Current.PayrollComponentName;

                        resultArgs = GetReportSource();
                        if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            DataView dvDayBook = resultArgs.DataSource.Table.AsDataView();
                            dvDayBook.Table.TableName = "PAYWAGES";
                            this.DataSource = dvDayBook;
                            this.DataMember = dvDayBook.Table.TableName;
                        }
                        else
                        {
                            this.DataSource = null;
                        }
                        SplashScreenManager.CloseForm();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                    xrHeader = AlignHeaderTable(xrHeader);
                    xrDetail = AlignContentTable(xrDetail);
                    xrGrandTotal = AlignClosingBalance(xrGrandTotal);
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    setHeaderTitleAlignment();
                    SetReportTitle();

                    this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + ReportProperty.Current.PayrollName;
                    this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : ReportProperty.Current.PayrollGroupName;
                    this.CosCenterName = ReportProperty.Current.PayrollComponentName;

                    resultArgs = GetReportSource();
                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataView dvDayBook = resultArgs.DataSource.Table.AsDataView();
                        dvDayBook.Table.TableName = "PAYWAGES";
                        this.DataSource = dvDayBook;
                        this.DataMember = dvDayBook.Table.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                    }
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                resultArgs = PayrollBase.AbstractComponentReport();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("AMOUNT") != null)
            {
                Amount += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString());
                xrGtotal.Text = this.UtilityMember.NumberSet.ToNumber(Amount);
            }
        }
    }
}
