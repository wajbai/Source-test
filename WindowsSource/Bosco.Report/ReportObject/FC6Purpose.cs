using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Data;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class FC6Purpose : Bosco.Report.Base.ReportBase
    {

        #region VariableDeclaration
        ResultArgs resultArgs = null;
        public double DonorTotal = 0.0;
        #endregion

        #region Constructor
        public FC6Purpose()
        {
            InitializeComponent();
        }

        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFC6purpose();

        }
        #endregion

        #region Method
        private void BindFC6purpose()
        {
            GetReportDonorSource();
            base.ShowReport();

        }
        private void GetReportDonorSource()
        {
            try
            {
                resultArgs = null;
                string Donor = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FC6Donor);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Donor);

                    DataView dvDonor = resultArgs.DataSource.TableView;
                    if (dvDonor != null && dvDonor.Count != 0)
                    {
                        dvDonor.Table.TableName = "FC6DONORLIST";
                        this.DataSource = dvDonor;
                        this.DataMember = dvDonor.Table.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
        }
        #endregion

        private void xrTableCell81_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //string RecDate = (GetCurrentColumnValue(this.reportSetting1.FC6DONORLIST.DOF_RECEIPTSColumn.ColumnName) == null) ?
            //    string.Empty :
            //   GetCurrentColumnValue(this.reportSetting1.FC6DONORLIST.DOF_RECEIPTSColumn.ColumnName).ToString();
            //e.Result = this.UtilityMember.DateSet.ToDate(RecDate, false).ToString("dd-MMM-yyyy");
        }

        private void xrTableCell81_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string RecDate = (GetCurrentColumnValue(this.reportSetting1.FC6DONORLIST.DOF_RECEIPTSColumn.ColumnName) == null) ?
               string.Empty :
              GetCurrentColumnValue(this.reportSetting1.FC6DONORLIST.DOF_RECEIPTSColumn.ColumnName).ToString();
            if (!string.IsNullOrEmpty(RecDate))
            {
                xrTableCell81.Text = this.UtilityMember.DateSet.ToDate(RecDate, false).ToString("dd-MMM-yyyy");
            }
        }

        private void xrTableCell81_SummaryRowChanged(object sender, EventArgs e)
        {

        }


        #region Events

        #endregion

    }
}
