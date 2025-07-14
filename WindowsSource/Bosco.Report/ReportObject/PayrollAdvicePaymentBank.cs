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
    public partial class PayrollAdvicePaymentBank : ReportHeaderBase
    {
        #region Declaration
        DataTable dtPayRegister = new DataTable();
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();

        double dTotalNetAmount = 0;
        #endregion

        #region Constructors

        public PayrollAdvicePaymentBank()
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
                 (this.ReportProperties.PayrollGroupId !=string.Empty  && this.ReportProperties.PayrollGroupId != "0")))
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
                        
            SetReportTitle();
                        
            this.InstituteName = ReportProperty.Current.PayrollProjectTitle;
            this.LegalEntityAddress = ReportProperty.Current.PayrollProjectAddress;
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + ReportProperty.Current.PayrollName;
            this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : (string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupName) ? string.Empty : ReportProperty.Current.PayrollGroupName);
            this.HideDateRange = this.HideReportSubTitle = false;
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            setHeaderTitleAlignment();

            if (!String.IsNullOrEmpty(ReportProperty.Current.PayrollPayrollDate))
            {
                if (ReportProperty.Current.PayrollPaymentModeId != null && ReportProperty.Current.PayrollPaymentModeId>0)
                {
                    this.ReportTitle = "Payment Advice " + ReportProperty.Current.PayrollPaymentMode + " for the Month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.PayrollPayrollDate, false).ToString("MMMM yyyy");
                }
                else
                {
                    this.ReportTitle += " for the Month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.PayrollPayrollDate, false).ToString("MMMM yyyy");
                    ReportProperty.Current.PayrollPaymentModeByBank = 1;
                }
            }

            this.DisplayName = this.ReportTitle;
            //replace special characters which are not valid for file names
            this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
            //--------------------------------------------------------------------------------------

            setHeaderTitleAlignment();

            xrlblAddress.Text = ReportProperty.Current.PayrollPaymentBankAddress;
            xrLblDate.Text = UtilityMember.DateSet.GetDateToday(false);
            xrlblSubject.Text = "Payment Advice from "+ ReportProperty.Current.PayrollProjectTitle +
                                " (A/c No : " + ReportProperty.Current.PayrollPaymentBankAccountNo + ") for the month of " + ReportProperty.Current.PayrollName;

            xrLblInfo.Text = "Please transfer the amount from above account number to the below mentioned account numbers towards employee salaries.";
            
            xrlblTruly.Text = "Yours Truly" + Environment.NewLine
                             + "for " + ReportProperty.Current.PayrollProjectTitle;

            if (ReportProperty.Current.IncludeSignDetails == 1)
            {
                lblSign.Visible = lblSign1.Visible = false;
                (xrSubSignFooter.ReportSource as SignReportFooter).Visible = true;
                (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = this.PageWidth - 50;
                (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
            }
            else
            {
                lblSign.Visible = lblSign1.Visible = true;

                (xrSubSignFooter.ReportSource as SignReportFooter).Visible = false;
                lblSign.Text = "Authorized Signature";
                if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollSignOfEmployer))
                {
                    lblSign.Text = ReportProperty.Current.PayrollSignOfEmployer;
                    lblSign.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
                
                lblSign1.Text = string.Empty;
                if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollAuthorisedSignatory2))
                {
                    lblSign1.Text = ReportProperty.Current.PayrollAuthorisedSignatory2;
                    lblSign1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
                lblSign.Visible = !string.IsNullOrEmpty(lblSign.Text);
                lblSign1.Visible = !string.IsNullOrEmpty(lblSign1.Text); ;
            }
            
            //# Get list of components for given payroll and staff group
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PaymentAdviceBank, "PaymentAdviceBank"))
            {
               dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
               dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
               dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn.ColumnName, ReportProperty.Current.PayrollId);
               dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn.ColumnName, ReportProperty.Current.PayrollGroupId);
                resultArgs  = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
            }
            xrLBlAmountWordsValue.Text = string.Empty;
            if (ReportProperty.Current.PayrollPaymentModeByBank == 1)
            {
                grpBankAddress.Visible = grpSubject.Visible = true;
                xrtblHeaderCaption1.Visible = xrTblData1.Visible = false;
                xrtblHeaderCaption.Visible = xrTblData.Visible = true;
            }
            else
            {
                grpBankAddress.Visible = grpSubject.Visible = false;
                xrtblHeaderCaption1.Visible = xrTblData1.Visible = true;
                xrtblHeaderCaption.Visible = xrTblData.Visible = false;    
            }

            xrtblHeaderCaption.TopF = xrTblData.TopF = xrtblHeaderCaption1.TopF = xrTblData1.TopF = 0;
            GroupHeader1.HeightF = Detail.HeightF = 25;

            if (resultArgs.Success && resultArgs.DataSource.Table != null)
            {
                dtPayRegister = resultArgs.DataSource.Table;
                if (ReportProperty.Current.PayrollPaymentModeId>0)
                {
                    dtPayRegister.DefaultView.RowFilter = "PAYMENT_MODE_ID = " + ReportProperty.Current.PayrollPaymentModeId;
                    dtPayRegister = dtPayRegister.DefaultView.ToTable();
                }
                dtPayRegister.TableName = this.DataMember;
                //dtPayRegister.DefaultView.Sort = "Name";
                this.DataSource = dtPayRegister;
                this.DataMember = dtPayRegister.TableName;

                this.AlignHeaderTable(xrtblHeaderCaption);
                this.AlignHeaderTable(xrtblHeaderCaption1);
                this.AlignContentTable(xrTblData);
                this.AlignContentTable(xrTblData1);

                dTotalNetAmount = UtilityMember.NumberSet.ToDouble(dtPayRegister.Compute("SUM([" + reportSetting1.PAYWages.NET_PAYColumn.ColumnName + "])", string.Empty).ToString());

                if (dTotalNetAmount > 0)
                {
                    xrLBlAmountWordsValue.Text = ConvertRuppessInWord.GetRupeesToWord(dTotalNetAmount.ToString());
                }
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }
            
            SplashScreenManager.CloseForm();
        }
        #endregion

       

    }
}
