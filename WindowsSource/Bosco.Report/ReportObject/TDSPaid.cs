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
    public partial class TDSPaid : Bosco.Report.Base.ReportHeaderBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        #endregion
        public TDSPaid()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            ShowTDSPaid();
        }

        #region Methods
        private void ShowTDSPaid()
        {
            if (ReportProperty.Current.ShowDetailedBalance == 1)
            {
                grpGroup.Visible = true;
            }
            else
            {
                grpGroup.Visible = false;
            }
            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom)
                || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0" || String.IsNullOrEmpty(this.ReportProperties.Project))
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                ShowTDSPaidDetails();
                base.ShowReport();
            }
        }
        private void ShowTDSPaidDetails()
        {
            try
            {
                this.ReportTitle = ReportProperty.Current.ReportTitle;
                this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                this.SetLandscapeHeader = 1060f;
                this.SetLandscapeFooter = 1060f;
                this.SetLandscapeFooterDateWidth = 900f;
                setHeaderTitleAlignment();

                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                DataTable dtTDSPaid = BindDataSource();
                if (dtTDSPaid != null && dtTDSPaid.Rows.Count > 0)
                {
                    dtTDSPaid.TableName = "TDSPaid";
                    this.DataSource = dtTDSPaid;
                    this.DataMember = dtTDSPaid.TableName;
                }
                SetReportBorder();


            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, false);
            }
            finally { }
        }
        private void SetReportBorder()
        {
            xrCapHeader = AlignHeaderTable(xrCapHeader);
            xtTDSPaid = AlignContentTable(xtTDSPaid);
            xtTotal = AlignTotalTable(xtTotal);
        }

        private DataTable BindDataSource()
        {
            string TDSPaid = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSPaid);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TDSPaid);
            }
            return resultArgs.DataSource.Table;
        }
        #endregion
    }
}
