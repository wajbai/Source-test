using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class TDSForm27Q : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        public TDSForm27Q()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            if (!string.IsNullOrEmpty(ReportProperties.DeducteeTypeId) &&
        !string.IsNullOrEmpty(ReportProperties.Project) && !string.IsNullOrEmpty(ReportProperties.DateFrom) &&
        !string.IsNullOrEmpty(ReportProperties.DateTo))
            {
                this.HideReportHeader = this.HidePageFooter = false;
                this.TopMarginHeight = 40;
                this.BottomMarginHeight = 40;
                BindReportSource();
                base.ShowReport();
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
        }

        private ResultArgs BindReportSource()
        {
            try
            {
                string TDSOutPayable = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSForm27Q);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DEDUCTEE_TYPE_IDColumn, this.ReportProperties.DeducteeTypeId);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TDSOutPayable);

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtForm27Q = resultArgs.DataSource.Table;
                        if (dtForm27Q != null)
                        {
                            this.DataSource = dtForm27Q;
                            this.DataMember = dtForm27Q.TableName;
                        }

                        subForm27Q frm27Q = xrsubForm27Q.ReportSource as subForm27Q;
                        frm27Q.BindDetails(dtForm27Q);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            return resultArgs;
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell16.Text = ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false).Year + " - " + ReportProperty.Current.DateSet.ToDate(settingProperty.YearTo, false).Year;
        }

        private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime dtVoucherDate = GetCurrentColumnValue("VOUCHER_DATE") != null ? this.UtilityMember.DateSet.ToDate(GetCurrentColumnValue("VOUCHER_DATE").ToString(), false) : DateTime.MinValue;
            if (dtVoucherDate != DateTime.MinValue)
            {
                if (dtVoucherDate >= this.UtilityMember.DateSet.ToDate(this.settingProperty.YearFrom, false) && dtVoucherDate <=
                this.UtilityMember.DateSet.ToDate(this.settingProperty.YearTo, false))
                {
                    if (dtVoucherDate.Month > (int)Month.March)
                    {
                        xrTableCell20.Text = (dtVoucherDate.AddYears(1).Year) + " - " + (dtVoucherDate.AddYears(2).Year).ToString().Remove(0, 2);
                    }
                    else
                    {
                        xrTableCell20.Text = (dtVoucherDate.Year) + " - " + (dtVoucherDate.AddYears(1).Year).ToString().Remove(0, 2);
                    }
                }
                else
                {
                    if (dtVoucherDate.Month <= (int)Month.March)
                    {
                        xrTableCell20.Text = (dtVoucherDate.Year) + " - " + (dtVoucherDate.AddYears(1).Year).ToString().Remove(0, 2);
                    }
                    else
                    {
                        xrTableCell20.Text = (dtVoucherDate.AddYears(1).Year) + " - " + (dtVoucherDate.AddYears(2).Year).ToString().Remove(0, 2);
                    }
                }
            }
                 
        }

        private void xrAccountingYear_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrAccountingYear.Text = ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false).Year + " - " + ReportProperty.Current.DateSet.ToDate(settingProperty.YearTo, false).Year + " " + "(Year)";
        }
    }
}
