using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class TDSPartyWiseDrillDown : Bosco.Report.Base.ReportHeaderBase
    {

        #region Variables
        ResultArgs resultArgs = null;
        string LedgerId = string.Empty;
        string DateFrom = string.Empty;
        string ProjectId = string.Empty;
        #endregion

        public TDSPartyWiseDrillDown()
        {
            InitializeComponent();
        }

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
                    DateFrom = dicDDProperties[this.ReportParameters.BOOKING_DATEColumn.ColumnName].ToString();
                    ProjectId=dicDDProperties[this.ReportParameters.PROJECT_IDColumn.ColumnName].ToString();
                }
            }

            ShowTDSPartyWiseDetails();
            base.ShowReport();
        }

        #region Method
        private void ShowTDSPartyWiseDetails()
        {
            try
            {
                SetReportTitle();
                this.SetLandscapeFooterDateWidth = 1065;
                this.SetLandscapeHeader = 1065;
                this.SetLandscapeFooter = 1065;

                this.SetCurrencyFormat(xrRate.Text, xrRate);
                this.SetCurrencyFormat(xrAssessableAmount.Text, xrAssessableAmount);
                this.SetCurrencyFormat(xrBalancePaid.Text, xrBalancePaid);
                this.SetCurrencyFormat(xrBalancetobePaid.Text, xrBalancetobePaid);
                this.SetCurrencyFormat(xrTaxDeductable.Text, xrTaxDeductable);

                resultArgs = GetReportSource();
                DataView dvTDSPartywiseDrillDown = resultArgs.DataSource.TableView;
                if (dvTDSPartywiseDrillDown != null && dvTDSPartywiseDrillDown.Count != 0)
                {
                    dvTDSPartywiseDrillDown.Table.TableName = "TDSPartyWiseDrillDown";
                    this.DataSource = dvTDSPartywiseDrillDown;
                    this.DataMember = dvTDSPartywiseDrillDown.Table.TableName;
                }
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
                string TDSPartyWise = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSComputationPayableDrillDown);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, LedgerId);
                    dataManager.Parameters.Add(this.ReportParameters.BALANCE_DATEColumn, DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, ProjectId);
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
