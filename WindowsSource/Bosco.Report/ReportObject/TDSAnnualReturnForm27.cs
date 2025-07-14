using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace Bosco.Report.ReportObject
{
    public partial class TDSAnnualReturnForm27 : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        public TDSAnnualReturnForm27()
        {
            InitializeComponent();
        }
        #region Show Reports
        public override void ShowReport()
        {
            BindForm27();
        }

        private void BindForm27()
        {
            if (!string.IsNullOrEmpty(ReportProperties.DeducteeTypeId) &&
                !string.IsNullOrEmpty(ReportProperties.Project) && !string.IsNullOrEmpty(ReportProperties.DateFrom) &&
                !string.IsNullOrEmpty(ReportProperties.DateTo))
            {
                this.HideReportHeader = this.HidePageFooter = false;
                this.TopMarginHeight = 40;
                this.BottomMarginHeight = 40;
                BindForm27Source();
                base.ShowReport();
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
        }

        private void BindForm27Source()
        {
            try
            {
                this.SetCurrencyFormat(xrlblTDSCap.Text, xrlblTDSCap);
                this.SetCurrencyFormat(xrlblSurchargeCap.Text, xrlblSurchargeCap);
                this.SetCurrencyFormat(xrlblEducationCap.Text, xrlblEducationCap);
                this.SetCurrencyFormat(xrlblInterestCap.Text, xrlblInterestCap);
                this.SetCurrencyFormat(xrlblOthersCap.Text, xrlblOthersCap);
                resultArgs = BindSource();
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs.DataSource.Table.TableName = "Form27";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private ResultArgs BindSource()
        {
            string TDSForm27 = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSForm27);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DEDUCTEE_TYPE_IDColumn, this.ReportProperties.DeducteeTypeId);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TDSForm27);
            }
            return resultArgs;
        }
        #endregion

        private void xrName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("FULL_NAME") != null)
            {
                xrName.Text = GetCurrentColumnValue("FULL_NAME").ToString();
            }
        }
    }
}
