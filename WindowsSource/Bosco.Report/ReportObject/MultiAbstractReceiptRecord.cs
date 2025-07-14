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
namespace Bosco.Report.ReportObject
{
    public partial class MultiAbstractReceiptRecord : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        decimal subTotal = 0;
        decimal subGrandTotal = 0;
        public MultiAbstractReceiptRecord()
        {
            InitializeComponent();
        }
        public override void ShowReport()
        {
            BindMultiReceiptSource();
            base.ShowReport();
        }
        private ResultArgs SetReportSource()
        {
            string sqlMultiAbstractReceipt = this.GetReportSQL(SQL.ReportSQLCommand.Report.MonthlyAbstract);
            using (DataManager dataManager = new DataManager())
            {
                this.ReportProperties.VoucherType = "RC";
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_TYPEColumn, this.ReportProperties.VoucherType);
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
            if (((OpBalance)xrSubreportMultiReceiptAmt.ReportSource) != null)
            {
                ((OpBalance)xrSubreportMultiReceiptAmt.ReportSource).BindCashBankOpBalance();
                if (this.xrSubreportMultiReceiptAmt.ReportSource.DataSource as DataTable != null && (this.xrSubreportMultiReceiptAmt.ReportSource.DataSource as DataTable).Rows.Count != 0)
                {
                    subGrandTotal = ReportProperty.Current.NumberSet.ToDecimal((this.xrSubreportMultiReceiptAmt.ReportSource.DataSource as DataTable).Compute("Sum(AMOUNT)", "").ToString());
                }
            }
            if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
            {
                ShowReportFilterDialog();
            }
            else
            {
                resultArgs = SetReportSource();
                this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    pvMultiAbstractReceipts.DataSource = resultArgs.DataSource.Table;
                    subTotal = ReportProperty.Current.NumberSet.ToDecimal(resultArgs.DataSource.Table.Compute("SUM(TOTAL)", "").ToString());
                }
                xtGrandTotalAmt.Text = (subTotal + subGrandTotal).ToString("F2");
                xrGrandProgressAmt.Text = (subTotal + subGrandTotal).ToString("F2");
            }
        }
    }
}
