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
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class TDSOutstandingPayable : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion
        public TDSOutstandingPayable()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            BindTDS();
        }

        public void BindTDS()
        {
            if (ReportProperty.Current.ShowDetailedBalance == 1)
            {
                grpOutstandingPayable.Visible = true;
            }
            else
            {
                grpOutstandingPayable.Visible = false;
            }


            this.SetLandscapeHeader = 1030;
            this.SetLandscapeFooter = 1030;
            this.SetLandscapeFooterDateWidth = 800;
            if (string.IsNullOrEmpty(ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || string.IsNullOrEmpty(this.ReportProperties.Project))
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
                DataView dvTDSOUTPayable = resultArgs.DataSource.TableView;
                if (dvTDSOUTPayable != null && dvTDSOUTPayable.Count != 0)
                {
                    dvTDSOUTPayable.Table.TableName = "TDSPayable";
                    this.DataSource = dvTDSOUTPayable;
                    this.DataMember = dvTDSOUTPayable.Table.TableName;
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
            xrtblHeader = AlignHeaderTable(xrtblHeader);
            xtTDSOutPayable = AlignContentTable(xtTDSOutPayable);
            xrtblGrandTotal = AlignTotalTable(xrtblGrandTotal);

            this.SetCurrencyFormat(xrOpeningAmt.Text, xrOpeningAmt);
            this.SetCurrencyFormat(xrPendingamt.Text, xrPendingamt);
        }
    }
}
