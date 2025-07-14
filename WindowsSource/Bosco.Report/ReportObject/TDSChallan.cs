using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.Utility.ConfigSetting;
using System.Data;

namespace Bosco.Report.ReportObject
{
    public partial class TDSChallan : Bosco.Report.Base.ReportHeaderBase
    {

        #region Decelaration
        Bosco.Utility.ConfigSetting.SettingProperty settingProperty = new SettingProperty();
        private double Amount { get; set; }
        ResultArgs resultArgs = null;
        #endregion

        public TDSChallan()
        {
            InitializeComponent();
        }

        public override void ShowReport()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            this.TopMarginHeight = 25;
            this.BottomMarginHeight = 25;
            BindTDSChallanReport();
        }

        private void BindTDSChallanReport()
        {
            if (!string.IsNullOrEmpty(ReportProperty.Current.TDSVoucherId) && ReportProperty.Current.TDSVoucherId != "0" && ReportProperty.Current.DateAsOn != string.Empty)
            {
                SetReportSettings();
                base.ShowReport();
            }
            else
            {
                ShowTDSChallanReconciliationForm();
            }
        }

        private void SetReportSettings()
        {
            try
            {
                this.SetLandscapeHeader = 1060f;
                this.SetLandscapeFooter = 1060f;

                this.SetLandscapeFooterDateWidth = 900f;
                setHeaderTitleAlignment();
                SetReportTitle();
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                AssignTDSSource();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void AssignTDSSource()
        {
            string TDSChallanQuery = this.GetReportTDS(SQL.ReportSQLCommand.TDS.TDSChallan);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.VOUCHER_IDColumn, this.ReportProperties.TDSVoucherId);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateAsOn);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, TDSChallanQuery);

                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    resultArgs.DataSource.Table.TableName = "TDSChallan";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
            }
        }

        private void xrCompanyDeductees_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int DeducteeTypeId = GetCurrentColumnValue("DEDUCTEE_TYPE_ID") != null ? this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("DEDUCTEE_TYPE_ID").ToString()) : 0;
            if (DeducteeTypeId.Equals(6))
            {
                xrCompanyDeductees.Checked = true;
            }
            else
            {
                xrCompanyDeductees.Checked = false;
            }
        }

        private void xrNonCompanyDeductees_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int NonCompany = GetCurrentColumnValue("DEDUCTEE_TYPE_ID") != null ? this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("DEDUCTEE_TYPE_ID").ToString()) : 0;
            if (!NonCompany.Equals(6))
            {
                xrNonCompanyDeductees.Checked = true;
            }
            else
            {
                xrNonCompanyDeductees.Checked = false;
            }
        }

        private void xrtRsInWords_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Amount = GetCurrentColumnValue("AMOUNT") != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString()) : 0;
            if (Amount > 0)
            {
                xrtRsInWords.Text = ConvertRuppessInWord.GetRupeesToWord(Amount.ToString());
            }
        }

        private void xrAssessYear_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
         {
            DateTime dtVoucherDate = GetCurrentColumnValue("VOUCHER_DATE") != null ? this.UtilityMember.DateSet.ToDate(GetCurrentColumnValue("VOUCHER_DATE").ToString(), false) : DateTime.MinValue;
            if (dtVoucherDate != DateTime.MinValue)
            {
                if (dtVoucherDate >= this.UtilityMember.DateSet.ToDate(this.settingProperty.YearFrom, false) && dtVoucherDate <=
                this.UtilityMember.DateSet.ToDate(this.settingProperty.YearTo, false))
                {
                    if (dtVoucherDate.Month > (int)Month.March)
                    {
                        xrAssessYear.Text = (dtVoucherDate.AddYears(1).Year) + " - " + (dtVoucherDate.AddYears(2).Year).ToString().Remove(0, 2);
                    }
                    else
                    {
                        xrAssessYear.Text = (dtVoucherDate.Year) + " - " + (dtVoucherDate.AddYears(1).Year).ToString().Remove(0, 2);
                    }
                }
                else
                {
                    if (dtVoucherDate.Month <= (int)Month.March)
                    {
                        xrAssessYear.Text = (dtVoucherDate.Year) + " - " + (dtVoucherDate.AddYears(1).Year).ToString().Remove(0, 2);
                    }
                    else
                    {
                        xrAssessYear.Text = (dtVoucherDate.AddYears(1).Year) + " - " + (dtVoucherDate.AddYears(2).Year).ToString().Remove(0, 2);
                    }
                }
            }
        }

        private void xrtFortheAssessmentYear_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime dtVoucherDate = GetCurrentColumnValue("VOUCHER_DATE") != null ? this.UtilityMember.DateSet.ToDate(GetCurrentColumnValue("VOUCHER_DATE").ToString(), false) : DateTime.MinValue;
            if (dtVoucherDate != DateTime.MinValue)
            {
                if (dtVoucherDate >= this.UtilityMember.DateSet.ToDate(this.settingProperty.YearFrom, false) && dtVoucherDate <=
                this.UtilityMember.DateSet.ToDate(this.settingProperty.YearTo, false))
                {
                    if (dtVoucherDate.Month > (int)Month.March)
                    {
                        xrtFortheAssessmentYear.Text = (dtVoucherDate.AddYears(1).Year) + " - " + (dtVoucherDate.AddYears(2).Year).ToString().Remove(0, 2);
                    }
                    else
                    {
                        xrtFortheAssessmentYear.Text = (dtVoucherDate.Year) + " - " + (dtVoucherDate.AddYears(1).Year).ToString().Remove(0, 2);
                    }
                }
                else
                {
                    if (dtVoucherDate.Month <= (int)Month.March)
                    {
                        xrtFortheAssessmentYear.Text = (dtVoucherDate.Year) + " - " + (dtVoucherDate.AddYears(1).Year).ToString().Remove(0, 2);
                    }
                    else
                    {
                        xrtFortheAssessmentYear.Text = (dtVoucherDate.AddYears(1).Year) + " - " + (dtVoucherDate.AddYears(2).Year).ToString().Remove(0, 2);
                    }
                }
            }
        }

        private void xrCompany_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int DeducteeTypeId = GetCurrentColumnValue("DEDUCTEE_TYPE_ID") != null ? this.UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue("DEDUCTEE_TYPE_ID").ToString()) : 0;
            if (!DeducteeTypeId.Equals(6))
            {
                xrCompany.Text = "Non Company(0021)";
            }
            else
            {
                xrCompany.Text = "Company(0020)";
            }
        }

        private void xrtCrores_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Amount = GetCurrentColumnValue("AMOUNT") != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString()) : 0;
            if (Amount > 0)
            {
                ArrayList Crores = NumberToText(this.ReportProperties.NumberSet.ToInteger(Amount.ToString()));
                string sCrores = ConvertRuppessInWord.GetRupeesToWord(Crores[0].ToString());
                sCrores = sCrores.Remove(sCrores.Length - 4);
                xrtCrores.Text = sCrores.ToUpper();
            }
        }

        private void xrtLacs_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Amount = GetCurrentColumnValue("AMOUNT") != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString()) : 0;
            if (Amount > 0)
            {
                ArrayList Lacs = NumberToText(this.ReportProperties.NumberSet.ToInteger(Amount.ToString()));
                string sLacs = ConvertRuppessInWord.GetRupeesToWord(Lacs[1].ToString());
                sLacs = sLacs.Remove(sLacs.Length - 4);
                xrtLacs.Text = sLacs.ToUpper();
            }
        }

        private void xrtThousands_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Amount = GetCurrentColumnValue("AMOUNT") != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString()) : 0;
            if (Amount > 0)
            {
                ArrayList Thousand = NumberToText(this.ReportProperties.NumberSet.ToInteger(Amount.ToString()));
                string sThousand = ConvertRuppessInWord.GetRupeesToWord(Thousand[2].ToString());
                sThousand = sThousand.Remove(sThousand.Length - 4);
                xrtThousands.Text = sThousand.ToUpper();
            }
        }

        private void xrtHundreds_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Amount = GetCurrentColumnValue("AMOUNT") != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString()) : 0;
            if (Amount > 0)
            {
                ArrayList Hundred = NumberToText(this.ReportProperties.NumberSet.ToInteger(Amount.ToString()));
                string sHundred = ConvertRuppessInWord.GetRupeesToWord(Hundred[3].ToString());
                sHundred = sHundred.Remove(sHundred.Length - 4);
                xrtHundreds.Text = sHundred.ToUpper();
            }
        }

        private void xrtTens_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Amount = GetCurrentColumnValue("AMOUNT") != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString()) : 0;
            if (Amount > 0)
            {
                ArrayList Tens = NumberToText(this.ReportProperties.NumberSet.ToInteger(Amount.ToString()));
                string sTens = ConvertRuppessInWord.GetRupeesToWord(Tens[4].ToString());
                sTens = sTens.Remove(sTens.Length - 4);
                xrtTens.Text = sTens.ToUpper();
            }
        }

        private void xrtUnits_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Amount = GetCurrentColumnValue("AMOUNT") != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("AMOUNT").ToString()) : 0;
            if (Amount > 0)
            {
                ArrayList Units = NumberToText(this.ReportProperties.NumberSet.ToInteger(Amount.ToString()));
                string sUnits = ConvertRuppessInWord.GetRupeesToWord(Units[5].ToString());
                sUnits = sUnits.Remove(sUnits.Length - 4);
                xrtUnits.Text = sUnits.ToUpper();
            }
        }

        private void xrtFullName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string DeducteeAddress = GetCurrentColumnValue("FULL_NAME") != null ? GetCurrentColumnValue("FULL_NAME").ToString() : string.Empty;
            xrtFullName.Text = xrtReceivedFrom.Text = DeducteeAddress;
        }

        private ArrayList NumberToText(int number)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ArrayList finalArray = new ArrayList();
            finalArray.Clear();

            if (number > 0)
            {
                int[] num = new int[6];

                num[0] = number % 1000; // units
                num[1] = number / 1000;
                num[2] = number / 100000;
                num[1] = num[1] - 100 * num[2]; // thousands
                num[3] = number / 10000000; // crores
                num[2] = num[2] - 100 * num[3]; // lakhs

                for (int i = 3; i > 0; i--)
                {
                    finalArray.Add(num[i].ToString());
                }

                if (num[0] != 0)
                {
                    int u = num[0] % 10; // ones
                    int t = num[0] / 10;
                    int h = num[0] / 100; // hundreds
                    t = t - 10 * h; // tens
                    finalArray.Add(h.ToString());
                    finalArray.Add(t.ToString());
                    finalArray.Add(u.ToString());
                }
                else
                {
                    finalArray.Add("0");
                    finalArray.Add("0");
                    finalArray.Add("0");
                }
            }
            return finalArray;
        }

        private void xrtInterest_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double InsAmount = GetCurrentColumnValue("INTEREST_AMOUNT") != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("INTEREST_AMOUNT").ToString()) : 0;
            if (InsAmount <= 0)
            {
                xrtInterest.Text = "";
            }
        }

        private void xrtPenalty_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double InsAmount = GetCurrentColumnValue("PENALTY_AMOUNT") != null ? this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("PENALTY_AMOUNT").ToString()) : 0;
            if (InsAmount <= 0)
            {
                xrtPenalty.Text = "";
            }
        }
    }
}
