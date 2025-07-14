using System;
using System.Data;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraReports.UI;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bosco.Report.ReportObject
{
    public partial class MulticolumnCashBankBook : Bosco.Report.Base.ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        int DailyGroupNumber = 0;
        double DailyGrpCashOpbalance = 0;
        double DailyGrpCashClbalance = 0;
        double DailyGrpBankOpbalance = 0;
        double DailyGrpBankClbalance = 0;

        double DailyCashReceipts = 0;
        double DailyBankPayments = 0;
        double DailyBankReceipts = 0;
        double DailyCashPayments = 0;

        int count = 0;
        int Rcount = 0;
        const int HeaderTable = 1;
        const int NonHeaderTable = 0;
        string RecledAmount = string.Empty;
        string PayledAmount = string.Empty;
        #endregion


        public MulticolumnCashBankBook()
        {
            InitializeComponent();
        }

        #region Methods
        public override void ShowReport()
        {
            CashAndBankBook();
            DailyGrpCashOpbalance = 0;
            DailyGrpCashClbalance = 0;
            DailyGrpBankOpbalance = 0;
            DailyGrpBankClbalance = 0;
            DailyGroupNumber = 0;
            Rcount = 0;

        }

        private void CashAndBankBook()
        {
            SetReportTitle();
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {

                ShowReportFilterDialog();
            }
            else
            {
                xrPGDrMuticolumnCashBank.SnapLineMargin = PaddingInfo.Empty;
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        setHeaderTitleAlignment();
                        this.SetLandscapeHeader = 1040.25f;
                        this.SetLandscapeFooter = 1030.25f;
                        this.SetLandscapeFooterDateWidth = 890.00f;

                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            
                        resultArgs = GetReportSource();
                        this.DataSource = null;
                        DataView dvCashBankBook = resultArgs.DataSource.TableView;
                        if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                        {
                            dvCashBankBook.Table.TableName = "CashBankBook";
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
                    this.SetLandscapeHeader = 1040.25f;
                    this.SetLandscapeFooter = 1030.25f;
                    this.SetLandscapeFooterDateWidth = 890.00f;
                    
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                    
                    resultArgs = GetReportSource();
                    this.DataSource = null;
                    DataView dvCashBankBook = resultArgs.DataSource.TableView;
                    if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                    {
                        dvCashBankBook.Table.TableName = "CashBankBook";
                        this.DataSource = dvCashBankBook;
                        this.DataMember = dvCashBankBook.Table.TableName;
                    }
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
        }
        
        private ResultArgs GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.MultiColumnCashbank);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, CashBankBookQueryPath);
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
    }
}
