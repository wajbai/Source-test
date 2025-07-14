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

namespace Bosco.Report.ReportObject
{
    public partial class PayrollLoanLedger : ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        clsPayrollBase PayrollBase = new clsPayrollBase();
        #endregion

        #region Constructors
        public PayrollLoanLedger()
        {
            InitializeComponent();
        }
        #endregion


        #region ShowReport

        public override void ShowReport()
        {
            BindLoanReport();
            base.ShowReport();
        }

        #endregion


        #region Methods
        private void BindLoanReport()
        {
            //if (this.ReportProperties.PayrollId.Trim() != string.Empty && this.ReportProperties.PayrollId != "0")
            //{
            if (this.UIAppSetting.UICustomizationForm == "1")
            {
                if (ReportProperty.Current.ReportFlag == 0)
                {
                    BindProperty();
                }
                else
                {
                    SetReportTitle();
                    ShowReportFilterDialog();
                }
            }
            else
            {
                BindProperty();
            }
        }


        private void BindProperty()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            setHeaderTitleAlignment();
            SetReportTitle();

            //this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + "Consolidated Statement";
            this.HideDateRange = false;
            this.HideReportSubTitle = false;

            resultArgs = GetReportSource();
            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DataView dvDayBook = resultArgs.DataSource.Table.AsDataView();
                dvDayBook.Sort = "Name";
                dvDayBook.Table.TableName = "Loan";
                this.DataSource = dvDayBook;
                this.DataMember = dvDayBook.Table.TableName;
            }
            else
            {
                this.DataSource = null;
            }
            SplashScreenManager.CloseForm();

            xrHeader = AlignHeaderTable(xrHeader);
            xrDetail = AlignContentTable(xrDetail);
            //xrGrandTotal = AlignClosingBalance(xrGrandTotal);
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                resultArgs = PayrollBase.LoanRegisterReport();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        #endregion
    }
}
