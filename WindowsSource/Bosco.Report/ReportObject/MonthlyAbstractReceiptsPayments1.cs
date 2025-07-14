using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using System.Globalization;
namespace Bosco.Report.ReportObject
{
    public partial class MonthlyAbstractReceiptsPayments : ReportBase
    {
        ResultArgs resultArgs = null;
        public MonthlyAbstractReceiptsPayments()
        {
            InitializeComponent();
        }
         public override void ShowReport()
        {
            BindReceiptsPaymentSource();
            base.ShowReport();
        }

         private ResultArgs SetReportSource()
         {
             string sqlMonthlyAbstractReceiptPayment = this.GetReportSQL(SQL.ReportSQLCommand.Report.MonthlyAbstractReceiptPayment);
             using (DataManager dataManager = new DataManager())
             {
                 dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                 dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                 dataManager.Parameters.Add(this.ReportParameters.INCLUDE_ALL_LEDGERColumn, this.ReportProperties.IncludeAllLedger);
                 dataManager.Parameters.Add(this.ReportParameters.SHOW_BY_LEDGERColumn, this.ReportProperties.ShowByLedger);
                 dataManager.Parameters.Add(this.ReportParameters.SHOW_BY_LEDGER_GROUPColumn, this.ReportProperties.ShowByLedgerGroup);
                 dataManager.Parameters.Add(this.ReportParameters.PROJECTColumn, this.ReportProperties.Project);
                 resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlMonthlyAbstractReceiptPayment);
             }
             return resultArgs;
         }
         private void BindReceiptsPaymentSource()
         {
             if (this.ReportProperties.DateFrom == "" || this.ReportProperties.DateTo == "")
             {
                 ShowReportFilterDialog();
             }
             else
             {
                 resultArgs = SetReportSource();
                // lblRPDateFrom.Text = this.ReportProperties.DateFrom;
                // lblRPDateTo.Text = this.ReportProperties.DateTo;
                 string[] GetMonth = this.ReportProperties.DateFrom.Split('-');
                 string[] GetToMonth = this.ReportProperties.DateTo.Split('-');
                 int FromMonth = this.ReportProperties.NumberSet.ToInteger(GetMonth[1].ToString());
                 int ToMonth = this.ReportProperties.NumberSet.ToInteger(GetToMonth[1].ToString());
                 string Culture = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(FromMonth);
                 string ToCulture = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ToMonth);
                 //lblRPDateFrom.Text = GetMonth[0].ToString() + "-" + Culture + "-" + GetMonth[2].ToString();
                // lblRPDateTo.Text = GetMonth[0].ToString() + "-" + ToCulture + "-" + GetMonth[2].ToString();
                // lblReceiptsPProjectTitle.Text = this.ReportProperties.ProjectTitle;
                 if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                 {
                     this.DataSource = resultArgs.DataSource.Table;
                     DataMember = "";
                 }
             }
         }
    }
}
