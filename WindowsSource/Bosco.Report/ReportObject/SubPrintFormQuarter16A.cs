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
    public partial class SubPrintFormQuarter16A : Bosco.Report.Base.ReportBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion
        public SubPrintFormQuarter16A()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            BindReportSource();
            base.ShowReport();
        }

        public ResultArgs BindReportSource()
        {
            try
            {
                string TDSPrintForm16 = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSPrintForm16AQuarter);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.NATURE_OF_PAYMENT_IDColumn, this.ReportProperties.NatureofPaymets);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TDSPrintForm16);

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        SetSource(resultArgs.DataSource.Table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            return resultArgs;
        }

        private void SetSource(DataTable dtPrintForm16A)
        {
            try
            {
                xrQuarterJan.Text = xrQuarterMar.Text = xrQuarterApr.Text = xrQuarterJun.Text = xrQuarterJul.Text = xrQuarterSep.Text = xrQuarterOct.Text = xrQuarterDec.Text = string.Empty;
                foreach (DataRow dr in dtPrintForm16A.Rows)
                {
                    string Quarter = dr["quarter"] != DBNull.Value ? dr["quarter"].ToString() : string.Empty;
                    switch (Quarter)
                    {
                        case "1":
                            {
                                xrQuarterJan.Text = xrQuarterMar.Text = dr["AMOUNT"] != DBNull.Value ? this.ReportProperties.NumberSet.ToNumber(this.ReportProperties.NumberSet.ToDouble(dr["AMOUNT"].ToString())) : string.Empty;
                                break;
                            }
                        case "2":
                            {
                                xrQuarterApr.Text = xrQuarterJun.Text = dr["AMOUNT"] != DBNull.Value ? this.ReportProperties.NumberSet.ToNumber(this.ReportProperties.NumberSet.ToDouble(dr["AMOUNT"].ToString())) : string.Empty;
                                break;
                            }
                        case "3":
                            {
                                xrQuarterJul.Text = xrQuarterSep.Text = dr["AMOUNT"] != DBNull.Value ? this.ReportProperties.NumberSet.ToNumber(this.ReportProperties.NumberSet.ToDouble(dr["AMOUNT"].ToString())) : string.Empty;
                                break;
                            }
                        case "4":
                            {
                                xrQuarterOct.Text = xrQuarterDec.Text = dr["AMOUNT"] != DBNull.Value ? this.ReportProperties.NumberSet.ToNumber(this.ReportProperties.NumberSet.ToDouble(dr["AMOUNT"].ToString())) : string.Empty;
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
        }

    }
}
