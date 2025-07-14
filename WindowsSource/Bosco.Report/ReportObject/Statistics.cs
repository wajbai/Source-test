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
    public partial class Statistics : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public Statistics()
        {
            InitializeComponent();
        }
        #endregion

        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindInsuranceDetails();
        }
        #endregion

        #region Method

        private void BindInsuranceDetails()
        {
            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            SetReportTitle();
            setHeaderTitleAlignment();

            if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
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

                        //DataTable dtFARegister = GetReportSource();
                        //if (dtFARegister != null && dtFARegister.Rows.Count > 0)
                        //{
                        //    this.DataSource = dtFARegister;
                        //    this.DataMember = dtFARegister.TableName;
                        //}

                        Statisticmastersucreport staticsmaster = xrSubreport2.ReportSource as Statisticmastersucreport;
                        staticsmaster.BindStaticsDetails();
                        xrtcMasterTotal.Text = staticsmaster.sum.ToString();

                        statisticvouchersubreport staticsvoucher = xrSubreport1.ReportSource as statisticvouchersubreport;
                        staticsvoucher.BindStaticsDetails();
                        xrtcVoucherTotal.Text = staticsvoucher.sum.ToString();

                        //SplashScreenManager.CloseForm();
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

                    //DataTable dtFARegister = GetReportSource();
                    //if (dtFARegister != null && dtFARegister.Rows.Count > 0)
                    //{
                    //    this.DataSource = dtFARegister;
                    //    this.DataMember = dtFARegister.TableName;
                    //}

                    Statisticmastersucreport staticsmaster = xrSubreport2.ReportSource as Statisticmastersucreport;
                    staticsmaster.BindStaticsDetails();
                    xrtcMasterTotal.Text = staticsmaster.sum.ToString();

                    statisticvouchersubreport statics = xrSubreport1.ReportSource as statisticvouchersubreport;
                    statics.BindStaticsDetails();
                    xrtcVoucherTotal.Text = statics.sum.ToString();

                    //SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            //xrInsuranceHeader = AlignHeaderTable(xrInsuranceHeader);
            //xrInsuranceDetail = AlignContentTable(xrInsuranceDetail);
        }

        private DataTable GetReportSource()
        {
            string AssetInsurance = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.StatisticalReport);
            using (DataManager dataManager = new DataManager())
            {
                //dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                //dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                //dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, AssetInsurance);
            }
            return resultArgs.DataSource.Table;
        }

        #endregion
    }
}
