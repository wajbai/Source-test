using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using Bosco.DAO.Data;
using System.Data;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class FCCountry : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelartion
        ResultArgs resultArgs = null;
        double DonorAmt = 0;
        double DonorTotalAmt = 0;
        #endregion

        #region Constructor
        public FCCountry()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblCountry, xrCountryName,
         new ArrayList { this.ReportParameters.COUNTRY_IDColumn.ColumnName, "COUNTRY" }, DrillDownType.FC_REPORT, false);
        }

        #endregion

        #region Show Report
        public override void ShowReport()
        {
            FCCountryReport();

        }
        #endregion

        #region Methods
        private void FCCountryReport()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        // this.ReportTitle = ReportProperty.Current.ReportTitle;
                        setHeaderTitleAlignment();
                        SetReportTitle();
                        //this.ReportSubTitle = "Foreign Projects"; //this.ReportProperties.ProjectTitle;
                        //this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                        
                        DataTable dtFCCountry = GetReportSource();
                        if (dtFCCountry != null)
                        {
                            DataView dvCountry = dtFCCountry.AsDataView();

                            if (ReportProperties.ShowAllCountry == 0)
                            {
                                dvCountry.RowFilter = "AMOUNT > 0";
                            }

                            if (dvCountry.ToTable() != null)
                            {
                                dvCountry.ToTable().TableName = "FCCountry";
                                this.DataSource = dvCountry.ToTable();
                                this.DataMember = dvCountry.ToTable().TableName;
                            }
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
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                    // this.ReportTitle = ReportProperty.Current.ReportTitle;
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    //this.ReportSubTitle = "Foreign Projects"; //this.ReportProperties.ProjectTitle;
                    //this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                    

                    DataTable dtFCCountry = GetReportSource();
                    if (dtFCCountry != null)
                    {
                        DataView dvCountry = dtFCCountry.AsDataView();

                        if (ReportProperties.ShowAllCountry == 0)
                        {
                            dvCountry.RowFilter = "AMOUNT > 0";
                        }

                        if (dvCountry.ToTable() != null)
                        {
                            dvCountry.ToTable().TableName = "FCCountry";
                            this.DataSource = dvCountry.ToTable();
                            this.DataMember = dvCountry.ToTable().TableName;
                        }
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
            SetReportBorder();
        }

        private DataTable GetReportSource()
        {
            try
            {
                string FCCountry = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCCountry);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FCCountry);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblCountry = AlignContentTable(xrtblCountry);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
        }
        #endregion

        #region Events
        private void xrAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrAmount.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrAmount.Text = "";
            }
        }
        #endregion
    }
}
