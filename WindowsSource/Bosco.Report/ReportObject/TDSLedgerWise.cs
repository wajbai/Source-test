using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Report.ReportObject
{
    public partial class TDSLedgerWise : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        #endregion
        public TDSLedgerWise()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xtLedger, xrLedgerName,
               new ArrayList { this.ReportParameters.LEDGER_IDColumn.ColumnName, this.ReportParameters.PROJECT_IDColumn.ColumnName }, DrillDownType.TDS_OUTSTANDING_LEDGER, false);
        }

        public override void ShowReport()
        {
            BindTDS();
        }

        public void BindTDS()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty
               || this.ReportProperties.Project != string.Empty)
            {
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                setHeaderTitleAlignment();
                DataTable dtLedger = BindDataSource();
                if (dtLedger != null && dtLedger.Rows.Count > 0)
                {
                    dtLedger.TableName = "TDSLedger";
                    this.DataSource = dtLedger;
                    this.DataMember = dtLedger.TableName;
                }
                SetReportTitle();
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }
        private void SetReportBorder()
        {
            xtHeaderCaption = AlignHeaderTable(xtHeaderCaption);
            xtLedger = AlignContentTable(xtLedger);
            xtTotal = AlignTotalTable(xtTotal);
        }

        private DataTable BindDataSource()
        {
            string TDSLedger = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSLedgerWise);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TDSLedger);
            }
            return resultArgs.DataSource.Table;
        }
    }
}
