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
    public partial class TDSPartyWise : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        public TDSPartyWise()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xtDetail, xrParticulars,
               new ArrayList { this.ReportParameters.LEDGER_IDColumn.ColumnName, this.ReportParameters.BOOKING_DATEColumn.ColumnName, this.ReportParameters.PROJECT_IDColumn.ColumnName }, DrillDownType.TDS_PARTY_WISE, false);
        }
        public override void ShowReport()
        {
            ShowTDSPartyWise();
        }

        #region Methods
        private void ShowTDSPartyWise()
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
                ShowTDSPartyWiseDetails();
                base.ShowReport();
            }
        }
        private void ShowTDSPartyWiseDetails()
        {
            try
            {
                this.SetLandscapeHeader = 1060f;
                this.SetLandscapeFooter = 1060f;
                this.SetLandscapeFooterDateWidth = 900f;
                resultArgs = GetReportSource();
                DataView dvTDSPartywise = resultArgs.DataSource.Table.DefaultView;
                if (dvTDSPartywise != null && dvTDSPartywise.Count != 0)
                {
                    dvTDSPartywise.Table.TableName = "TDSPartyWise";
                    this.DataSource = dvTDSPartywise;
                    this.DataMember = dvTDSPartywise.Table.TableName;
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
                string TDSPartyWise = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSPartyWise);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TDSPartyWise);
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
            xtHeader = AlignHeaderTable(xtHeader);
            xtDetail = AlignContentTable(xtDetail);
            xtFooter = AlignTotalTable(xtFooter);

            this.SetCurrencyFormat(xrCapTotal.Text, xrCapTotal);
            this.SetCurrencyFormat(xrCapTaxDeductable.Text, xrCapTaxDeductable);
            this.SetCurrencyFormat(xrCapBalDeductable.Text, xrCapBalDeductable);
            this.SetCurrencyFormat(xrCapExcessDeduct.Text, xrCapExcessDeduct);
            this.SetCurrencyFormat(xrCapBalPaid.Text, xrCapBalPaid);
        }

        #endregion
    }
}
