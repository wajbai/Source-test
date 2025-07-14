using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class TDSOutstandingNOP : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        public TDSOutstandingNOP()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xtNOP, xrNop,
              new ArrayList { this.ReportParameters.NATURE_OF_PAYMENT_IDColumn.ColumnName, this.ReportParameters.PROJECT_IDColumn.ColumnName }, DrillDownType.TDS_OUTSTANDING_NOP, false);
        }
        public override void ShowReport()
        {
            BindTDS();
        }
        public void BindTDS()
        {
            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || string.IsNullOrEmpty(this.ReportProperties.Project) || string.IsNullOrEmpty(this.ReportProperties.NatureofPaymets))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                resultArgs = GetReportSource();
                DataView dvTDSNatureOfPayments = resultArgs.DataSource.TableView;
                if (dvTDSNatureOfPayments != null && dvTDSNatureOfPayments.Count != 0)
                {
                    dvTDSNatureOfPayments.Table.TableName = "TDSNatureOfPayments";
                    this.DataSource = dvTDSNatureOfPayments;
                    this.DataMember = dvTDSNatureOfPayments.Table.TableName;
                }
                setHeaderTitleAlignment();
                SetReportTitle();
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }
            SetReportBorder();
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string TDSOutPayable = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSNatureOfPayments);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.NATURE_OF_PAYMENT_IDColumn, this.ReportProperties.NatureofPaymets);
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
            xtHeaderCap = AlignHeaderTable(xtHeaderCap);
            xtNOP = AlignContentTable(xtNOP);
            xtGrandTotal = AlignGrandTotalTable(xtGrandTotal);

            this.SetCurrencyFormat(xrCompany.Text, xrCompany);
            this.SetCurrencyFormat(xrNonCompany.Text, xrNonCompany);
            this.SetCurrencyFormat(xrTotalPending.Text, xrTotalPending);
        }
    }
}
