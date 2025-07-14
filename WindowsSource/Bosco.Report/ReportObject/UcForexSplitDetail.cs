using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class UcForexSplitDetail : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public UcForexSplitDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        double gain = 0;
        double loss = 0;
        double forexdifference = 0;
        double totalactualamount = 0;
        string titledate = string.Empty;
        #endregion

        public float CurrencyNameWidth
        {
            set
            {
                xrCapCurrencyName.WidthF = xrcellCurrencyName.WidthF = xrTotalCaption.WidthF 
                    = xrGrandTotalCaption.WidthF = value;
            }
        }
        
        public float GainWidth
        {
            set
            {
                xrCapGain.WidthF = xrcellAvgExchangeRate.WidthF = xrSumForexGain.WidthF = xrGrandTotalForexGain.WidthF = value;
            }
        }

        public float LossWidth
        {
            set
            {
                xrCapLoss.WidthF = xrcellLoss.WidthF = xrSumForexLoss.WidthF = xrGrandTotalForexLoss.WidthF = value;
            }
        }

        string dateFrom = string.Empty;
        public string DateFrom
        {
            set { dateFrom = value; }
        }

        string dateTo = string.Empty;
        public string DateTo
        {
            set { dateTo = value; }
        }

        string dateAsOn = string.Empty;
        public string DateAsOn
        {
            set { dateAsOn = value; }
        }

        #region ShowReport
        //public override void ShowReport()
        //{
        //    LedgerDebit = 0;
        //    LedgerCredit = 0;
        //    BindConsolidatedStatement();
        //}
        #endregion

        #region Methods

        private void HideReportHeaderFooter()
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
        }


        public void ShowForex()
        {
            HideReportHeaderFooter();
            //xrtblHeader.WidthF = xrCapCurrencyName.WidthF + xrCapGain.WidthF + xrcellLoss.WidthF;
            SetTitleWidth(xrtblHeader.WidthF);
            this.SetLandscapeHeader = xrtblHeader.WidthF;
            this.SetLandscapeFooter = xrtblHeader.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeader.WidthF;
                        
            resultArgs =  GetReportSource();
            xrcellTitle.Text = "Forex Split Details " + titledate;
            this.DataSource = null;
            gain = loss = forexdifference = totalactualamount = 0;
            ReportFooter.Visible = false;
            
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table!=null)
            {
                DataTable dtForex = resultArgs.DataSource.Table;
                dtForex.TableName = this.DataMember;
                this.DataSource = dtForex;
                this.DataMember = dtForex.TableName;
                Detail.Visible = true;
                ReportFooter.Visible = (dtForex.Rows.Count>0);
                gain = UtilityMember.NumberSet.ToDouble(dtForex.Compute("SUM(" + reportSetting1.FOREX.GAINColumn.ColumnName + ")", string.Empty).ToString());
                loss = UtilityMember.NumberSet.ToDouble(dtForex.Compute("SUM(" + reportSetting1.FOREX.LOSSColumn.ColumnName + ")", string.Empty).ToString());
                forexdifference = UtilityMember.NumberSet.ToDouble(dtForex.Compute("SUM(" + reportSetting1.FOREX.FOREX_DIFFColumn.ColumnName + ")", string.Empty).ToString());
                xrGrandTotalCaption.Text = " ";
                if (forexdifference !=0)
                {
                    xrGrandTotalCaption.Text = forexdifference < 0 ? "Loss" : "Gain";
                }

            }
        }


        private ResultArgs GetReportSource()
        {
            string TrialBalance = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.ForexSplitDetails);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                if (!string.IsNullOrEmpty(dateAsOn))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, dateAsOn);
                    titledate = " as on " + dateAsOn;
                }
                else if (!string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, dateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, dateTo);
                    titledate = " between " + dateFrom + " and " + dateTo;
                }
                else if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo))
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    titledate = " between " + this.ReportProperties.DateFrom + " and " + this.ReportProperties.DateTo;
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, settingProperty.LastFYDateTo.ToShortDateString());
                    titledate = " as on " + settingProperty.LastFYDateTo.ToShortDateString();
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, TrialBalance);

                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.TableView.Table != null)
                {
                    string filter = "";
                    DataTable dtForex = resultArgs.DataSource.TableView.Table;

                    //To define Gain ----------------------------------------------------------------
                    filter = "IIF(" + reportSetting1.FOREX.FOREX_DIFFColumn.ColumnName + ">0, " + reportSetting1.FOREX.FOREX_DIFFColumn.ColumnName + ",0)";
                    DataColumn dc = new DataColumn(reportSetting1.FOREX.GAINColumn.ColumnName, typeof(System.Double), filter);
                    dc.DefaultValue = 0;
                    dtForex.Columns.Add(dc);
                    //-------------------------------------------------------------------------------

                    //To define Loss ----------------------------------------------------------------
                    filter = "IIF(" + reportSetting1.FOREX.FOREX_DIFFColumn.ColumnName + "<0, " + reportSetting1.FOREX.FOREX_DIFFColumn.ColumnName + ",0)";
                    dc = new DataColumn(reportSetting1.FOREX.LOSSColumn.ColumnName, typeof(System.Double), filter);
                    dc.DefaultValue = 0;
                    dtForex.Columns.Add(dc);
                    //-------------------------------------------------------------------------------

                    //On 04/10/2024, To skip zero values
                    dtForex.DefaultView.RowFilter = reportSetting1.FOREX.CURRENCY_COUNTRY_IDColumn.ColumnName + " > 0 ";
                    dtForex = dtForex.DefaultView.ToTable();

                    resultArgs.DataSource.Data = dtForex.DefaultView.ToTable();
                }

            }
            return resultArgs;
        }

        #endregion

        
        private void xrGrandTotalForexGain_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = string.Empty;
            if (forexdifference > 0)
            {
                e.Result = forexdifference;
            }
            e.Handled = true;
        }

        private void xrGrandTotalForexLoss_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = string.Empty;
            if (forexdifference < 0)
            {
                e.Result = Math.Abs(forexdifference);
            }
            e.Handled = true;
        }

        private void xrSumForexLoss_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            /*e.Result = 0;
            if (forexdifference < 0)
            {
                e.Result = Math.Abs(loss);
            }
            e.Handled = true;*/

            if (this.DataSource != null)
            {
                DataTable dt = this.DataSource as DataTable;
                e.Result = Math.Abs(UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(" + reportSetting1.FOREX.LOSSColumn.ColumnName + ")", string.Empty).ToString()));
                e.Handled = true;
            }
        }

        private void xrcellLoss_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = Math.Abs(UtilityMember.NumberSet.ToDouble(e.Value.ToString()));
        }

        private void xrcellActualAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null && GetCurrentColumnValue(reportSetting1.FOREX.LIVE_EXCHANGE_RATEColumn.ColumnName)!=null
                 && GetCurrentColumnValue(reportSetting1.FOREX.CONTRIBUTION_AMOUNTColumn.ColumnName)!=null)
            {
                double averageexchangerate =  UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FOREX.LIVE_EXCHANGE_RATEColumn.ColumnName).ToString());
                double currencyamount = UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue(reportSetting1.FOREX.CONTRIBUTION_AMOUNTColumn.ColumnName).ToString());
                e.Value = (currencyamount * averageexchangerate);
                totalactualamount += (currencyamount * averageexchangerate);
            }
        }

        private void xrSumActualAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                e.Value = totalactualamount;
            }
        }

        #region Events
       
        #endregion

    }
}

