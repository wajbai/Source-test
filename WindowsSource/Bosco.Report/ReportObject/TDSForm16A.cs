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
    public partial class TDSForm16A : Bosco.Report.Base.ReportHeaderBase
    {
        ResultArgs resultArgs = null;
        SettingProperty settingProperty = new SettingProperty();
        private DataTable dtTemp = new DataTable();
        public TDSForm16A()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            if (!string.IsNullOrEmpty(ReportProperties.Ledger) && !string.IsNullOrEmpty(ReportProperties.NatureofPaymets) &&
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
                string TDSOutPayable = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSPrintForm16A);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.NATURE_OF_PAYMENT_IDColumn, this.ReportProperties.NatureofPaymets);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TDSOutPayable);

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtPrintForm16A = dtTemp = resultArgs.DataSource.Table;
                        if (dtPrintForm16A != null)
                        {
                            this.DataSource = dtPrintForm16A;
                            this.DataMember = dtPrintForm16A.TableName;
                        }

                        sunPrintForm16A frm27Q = xrsubForm16A.ReportSource as sunPrintForm16A;
                        frm27Q.BindDetails(dtPrintForm16A);

                        SubPrintFormQuarter16A subPrintFrom16A = xrSubreportQuarter.ReportSource as SubPrintFormQuarter16A;
                        subPrintFrom16A.BindReportSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            return resultArgs;
        }

        private void xrAmountInWords_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                double dAmount = this.UtilityMember.NumberSet.ToDouble(dtTemp.Compute("SUM(TAX_PAID_AMOUNT)", "").ToString());
                xrAmountInWords.Text = ConvertRuppessInWord.GetRupeesToWord(dAmount.ToString());
            }
        }

        private void xrAssessmentYear_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrAssessmentYear.Text = (DateTime.Now.Year) + " - " + (DateTime.Now.AddYears(1).Year).ToString().Remove(0, 2);
        }

        private void xrPeriodFrom_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPeriodFrom.Text = ReportProperty.Current.DateSet.ToDate(settingProperty.YearFrom, false).ToShortDateString();
        }

        private void xrPeridTo_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPeridTo.Text = ReportProperty.Current.DateSet.ToDate(settingProperty.YearTo, false).ToShortDateString();
        }

        private void xrDeductorAddress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string DeductorAddress = string.Empty;
            if (GetCurrentColumnValue("FULL_NAME") != null)
            {
                DeductorAddress += GetCurrentColumnValue("FULL_NAME").ToString() + Environment.NewLine;
            }

            if (GetCurrentColumnValue("ADDRESS") != null)
            {
                DeductorAddress += GetCurrentColumnValue("ADDRESS").ToString() + Environment.NewLine;
            }
            xrDeductorAddress.Text = DeductorAddress;
        }

        private void xrDeducteeAddress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string DeducteeAddress = string.Empty;
            if (GetCurrentColumnValue("PARTY_NAME") != null)
            {
                DeducteeAddress = GetCurrentColumnValue("PARTY_NAME").ToString() + Environment.NewLine;
            }

            if (GetCurrentColumnValue("PARTY_ADDRESS") != null)
            {
                string[] sArray = GetCurrentColumnValue("PARTY_ADDRESS").ToString().Split(',');
                for (int i = 0; i < sArray.Length; i++)
                {
                    DeducteeAddress += sArray[i].ToString() + Environment.NewLine;
                }

                string PinCode = GetCurrentColumnValue("PARTY_PIN_CODE") != null ? GetCurrentColumnValue("PARTY_PIN_CODE").ToString() : string.Empty;
                DeducteeAddress += PinCode;
                xrDeducteeAddress.Text = DeducteeAddress;
            }
        }
    }
}
