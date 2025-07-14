using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Globalization;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractReceipt : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        decimal subTotal = 0;
        public MultiAbstractReceipt()
        {
            InitializeComponent();
        }
        public override void ShowReport()
        {
            BindMultiReceiptSource();
            if (this.ReportProperties.DateFrom != "" && this.ReportProperties.DateTo != "")
            {
                base.ShowReport();
            }
        }
        private ResultArgs SetReportSource()
        {
            string sqlMultiAbstractReceipt = this.GetReportSQL(SQL.ReportSQLCommand.Report.MonthlyAbstract);
            using (DataManager dataManager = new DataManager())
            {
                //this.ReportProperties.VoucherType = "RC";
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, TransType.RC.ToString());
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, ReportProperty.Current.DateSet.ToDate(settingProperty.BookBeginFrom));
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.INCLUDE_ALL_LEDGERColumn, this.ReportProperties.IncludeAllLedger);
                dataManager.Parameters.Add(this.ReportParameters.SHOW_BY_LEDGERColumn, this.ReportProperties.ShowByLedger);
                dataManager.Parameters.Add(this.ReportParameters.SHOW_BY_LEDGER_GROUPColumn, this.ReportProperties.ShowByLedgerGroup);
                dataManager.Parameters.Add(this.ReportParameters.PROJECTColumn, this.ReportProperties.Project);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMultiAbstractReceipt);
            }
            return resultArgs;
        }
        public void BindMultiReceiptSource()
        {
            this.HideReportLogoLeft = true;
            if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
            {
                ShowReportFilterDialog();
            }
            else
            {
                resultArgs = SetReportSource();
                this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                this.ReportTitle = ReportProperty.Current.ReportTitle + " " + "for the Period of " + String.Format("{0:y}", ReportProperty.Current.DateSet.ToDate(ReportProperty.Current.DateFrom, false));
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    pvMultiAbstractReceipts.DataSource = resultArgs.DataSource.Table;
                    this.Landscape = true;
                    subTotal = ReportProperty.Current.NumberSet.ToDecimal(resultArgs.DataSource.Table.Compute("SUM(TOTAL)", "").ToString());
                }
            }
        }
    }
}
