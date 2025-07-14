using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class TDSComputationPayable : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        public TDSComputationPayable()
        {
            InitializeComponent();
        }
        public override void ShowReport()
        {
            ShowTDSPayable();
        }

        #region Methods
        private void ShowTDSPayable()
        {
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0" || String.IsNullOrEmpty(this.ReportProperties.Project))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                ShowTDSPayableDetails();
                base.ShowReport();
            }
        }
        private void ShowTDSPayableDetails()
        {
            try
            {
                if (ReportProperty.Current.ShowDetailedBalance == 1)
                {
                    grpGroupJournal.Visible = true;
                }
                else
                {
                    grpGroupJournal.Visible = false;
                }


                this.SetLandscapeHeader = 1060f;
                this.SetLandscapeFooter = 1060f;
                this.SetLandscapeFooterDateWidth = 900f;
                resultArgs = GetReportSource();
                DataView dvTDSOUTPayable = resultArgs.DataSource.TableView;
                if (dvTDSOUTPayable != null && dvTDSOUTPayable.Count != 0)
                {
                    dvTDSOUTPayable.Table.TableName = "TDSPayable";
                    this.DataSource = dvTDSOUTPayable;
                    this.DataMember = dvTDSOUTPayable.Table.TableName;
                }
                setHeaderTitleAlignment();
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }
        private ResultArgs GetReportSource()
        {
            try
            {
                string TDSOutPayable = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSComputationPayable);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, TDSOutPayable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            return resultArgs;
        }
        private void SetReportBorder()
        {
            xtHeaderCaption = AlignHeaderTable(xtHeaderCaption);
            xtTDSCompPay = AlignContentTable(xtTDSCompPay);
            xtTotal = AlignTotalTable(xtTotal);

            this.SetCurrencyFormat(xrCapOpAmt.Text, xrCapOpAmt);
            this.SetCurrencyFormat(xrCapPendAmt.Text, xrCapPendAmt);

        }
        #endregion
    }
}
