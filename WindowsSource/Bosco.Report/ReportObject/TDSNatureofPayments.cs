using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class NatureofPayments : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        public NatureofPayments()
        {
            InitializeComponent();
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
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                xrCompany.Text = this.SetCurrencyFormat(xrCompany.Text);
                xrNonCompany.Text = this.SetCurrencyFormat(xrNonCompany.Text);
                xrTotalPending.Text = this.SetCurrencyFormat(xrTotalPending.Text);
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
            xrTable1 = SetHeadingTableBorder(xrTable1, ReportProperties.ShowHorizontalLine, ReportProperties.ShowVerticalLine);
        }
    }
}
