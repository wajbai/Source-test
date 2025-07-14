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
    public partial class PayrollStaffEPF: ReportHeaderBase
    {
        #region Declaration
        DataTable dtPayRegister = new DataTable();
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();
        #endregion

        #region Constructors

        public PayrollStaffEPF()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Report
        public override void ShowReport()
        {
            BindPayRegisterReport();
            base.ShowReport();
        }
        #endregion

        #region Methods

        public void BindPayRegisterReport()
        {            
            if ( (this.ReportProperties.PayrollId !=string.Empty && this.ReportProperties.PayrollId != "0" &&
                 (this.ReportProperties.PayrollGroupId !=string.Empty  && this.ReportProperties.PayrollGroupId != "0") &&
                 (this.ReportProperties.PayrollComponentId != string.Empty && this.ReportProperties.PayrollComponentId != "0")))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        ReportSetting();
                    }
                    else
                    {
                        SetPayTitle();
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
                SetPayTitle();
                ShowPayslipForm();
            }
        }

        private void SetPayTitle()
        {
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;

            setHeaderTitleAlignment();
            SetReportTitle();

            this.InstituteName = ReportProperty.Current.PayrollProjectTitle;
            this.LegalEntityAddress = ReportProperty.Current.PayrollProjectAddress;
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + ReportProperty.Current.PayrollName;
            this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : (string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupName) ? string.Empty : ReportProperty.Current.PayrollGroupName);

            this.HideDateRange = false;
            if (!String.IsNullOrEmpty(ReportProperty.Current.PayrollPayrollDate))
            {
                this.ReportTitle = "Staff EPF Register for the Month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.PayrollPayrollDate, false).ToString("MMMM yyyy");
            }

            this.DisplayName = "Staff EPF Register for the Month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.PayrollPayrollDate, false).ToString("MMMM yyyy");
            //replace special characters which are not valid for file names
            this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
            //--------------------------------------------------------------------------------------
            this.ReportProperties.ShowPageNumber = 1;

            setHeaderTitleAlignment();

            Detail.Visible = ReportFooter.Visible = false;
        }

        private void ReportSetting()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));

            SetPayTitle();
            
            //# Get list of components for given payroll and staff group
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.StaffEPF, "StaffEPF"))
            {
               dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
               dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
               dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn.ColumnName, ReportProperty.Current.PayrollId);
               dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn.ColumnName, ReportProperty.Current.PayrollGroupId);
               dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn.ColumnName, ReportProperty.Current.PayrollComponentId);
               resultArgs  = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtPayRegister = resultArgs.DataSource.Table;
                dtPayRegister.TableName = this.DataMember;
                Detail.Visible = ReportFooter.Visible = (dtPayRegister.Rows.Count > 0);
                //dtPayRegister.DefaultView.Sort = "Name";
                this.DataSource = dtPayRegister;
                this.DataMember = dtPayRegister.TableName;
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }

            this.AlignHeaderTable(xrtblHeaderCaption);
            this.AlignContentTable(xrTblData);
            
            SplashScreenManager.CloseForm();
        }
        #endregion

        private void xrAbsentDays_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell lblAbsentdays = sender as XRTableCell;
            if (lblAbsentdays != null)
            {
                double Absetndays = UtilityMember.NumberSet.ToDouble(lblAbsentdays.Text);
                if (Absetndays <= 0)
                {
                    lblAbsentdays.Text = string.Empty;
                }
            }
        }

        

    }
}
