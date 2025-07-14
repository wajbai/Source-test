using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class TDSOutstandingLedgerDrillDown : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        string LedgerId = string.Empty;
        string ProjectId = string.Empty;
        #endregion

        #region Constructor
        public TDSOutstandingLedgerDrillDown()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                if (dicDDProperties.ContainsKey(this.ReportParameters.LEDGER_IDColumn.ColumnName))
                {
                    LedgerId = dicDDProperties[this.ReportParameters.LEDGER_IDColumn.ColumnName].ToString();
                    ProjectId = dicDDProperties[this.ReportParameters.PROJECT_IDColumn.ColumnName].ToString();
                }
            }
            ShowOutStandingLedger();
            base.ShowReport();
        }

        private void ShowOutStandingLedger()
        {
            try
            {
                SetReportTitle();
                this.SetLandscapeFooterDateWidth = 1065;
                this.SetLandscapeHeader = 1065;
                this.SetLandscapeFooter = 1065;
                this.SetCurrencyFormat(xrOpeningAmt.Text, xrOpeningAmt);
                this.SetCurrencyFormat(xrPendingamt.Text, xrPendingamt);
                resultArgs = GetReportSource();
                DataView dvTDSPartywiseDrillDown = resultArgs.DataSource.TableView;
                if (dvTDSPartywiseDrillDown != null && dvTDSPartywiseDrillDown.Count != 0)
                {
                    dvTDSPartywiseDrillDown.Table.TableName = "TDSOutstandingLedger";
                    this.DataSource = dvTDSPartywiseDrillDown;
                    this.DataMember = dvTDSPartywiseDrillDown.Table.TableName;
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string TDSPartyWise = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSOutstandingLedgerDrillDown);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn,ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, TDSPartyWise);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            return resultArgs;
        }
        #endregion


    }
}
