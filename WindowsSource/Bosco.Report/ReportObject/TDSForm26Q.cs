using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Report.Base;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Report.ReportObject
{
    public partial class TDSForm26Q : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        public TDSForm26Q()
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
                string Form26Q = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSForm26Q);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DEDUCTEE_TYPE_IDColumn, this.ReportProperties.DeducteeTypeId);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, Form26Q);

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtForm26Q = resultArgs.DataSource.Table;
                        if (dtForm26Q != null)
                        {
                            this.DataSource = dtForm26Q;
                            this.DataMember = dtForm26Q.TableName;
                        }
                        subForm26Q form26Q = xrsubForm26Q.ReportSource as subForm26Q;
                        form26Q.BindDetails(dtForm26Q);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            return resultArgs;
        }

        private void xrlblFinancialYear_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrlblFinancialYear.Text = ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false).Year + " - " + ReportProperty.Current.DateSet.ToDate(settingProperty.YearTo, false).Year;
        }

        private void xrlblAssessmentYear_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrlblAssessmentYear.Text = (ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false).AddYears(1).Year) + " - " + (ReportProperty.Current.DateSet.ToDate(settingProperty.YearTo, false).AddYears(1).Year);
           // xrlblAssessmentYear.Text = (DateTime.Now.Year) + " - " + (DateTime.Now.AddYears(1).Year).ToString().Remove(0,2);

            DateTime dtVoucherDate = GetCurrentColumnValue("VOUCHER_DATE") != null ? this.UtilityMember.DateSet.ToDate(GetCurrentColumnValue("VOUCHER_DATE").ToString(), false) : DateTime.MinValue;
            if (dtVoucherDate != DateTime.MinValue)
            {
                if (dtVoucherDate >= this.UtilityMember.DateSet.ToDate(this.settingProperty.YearFrom, false) && dtVoucherDate <=
                this.UtilityMember.DateSet.ToDate(this.settingProperty.YearTo, false))
                {
                    if (dtVoucherDate.Month > (int)Month.March)
                    {
                        xrlblAssessmentYear.Text = (dtVoucherDate.AddYears(1).Year) + " - " + (dtVoucherDate.AddYears(2).Year).ToString().Remove(0, 2);
                    }
                    else
                    {
                        xrlblAssessmentYear.Text = (dtVoucherDate.Year) + " - " + (dtVoucherDate.AddYears(1).Year).ToString().Remove(0, 2);
                    }
                }
                else
                {
                    if (dtVoucherDate.Month <= (int)Month.March)
                    {
                        xrlblAssessmentYear.Text = (dtVoucherDate.Year) + " - " + (dtVoucherDate.AddYears(1).Year).ToString().Remove(0, 2);
                    }
                    else
                    {
                        xrlblAssessmentYear.Text = (dtVoucherDate.AddYears(1).Year) + " - " + (dtVoucherDate.AddYears(2).Year).ToString().Remove(0, 2);
                    }
                }
            }
        }

        private void xrAccountingYear_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrAccountingYear.Text = (ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false).Year + " - " + ReportProperty.Current.DateSet.ToDate(settingProperty.YearTo, false).Year) + " " + "(Year)";
        }

        private void xrFullName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrFullName.Text = GetCurrentColumnValue("FULL_NAME") != null ? GetCurrentColumnValue("FULL_NAME").ToString() : string.Empty;
        }
    }
}
