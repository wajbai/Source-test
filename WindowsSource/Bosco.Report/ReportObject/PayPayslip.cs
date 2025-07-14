using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Utility;
using PAYROLL.Modules;
using System.Data;
using Bosco.Utility.ConfigSetting;
using DevExpress.Charts.Model;

namespace Bosco.Report.ReportObject
{
    public partial class PayPayslip : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        private ResultArgs resultArgs = new ResultArgs();
        private double EarningAmount = 0.0;
        private double DeductionAmount = 0.0;
        private double NetAmount = 0.0;
        private DataTable dtStaffPersonalInfo = new DataTable();
        private Int32 staffno = 0;
        private Int32 staffcompno = 0;
        private Int16 NoOfStaffComponentPerPage = 12;
        private Int16 NoOfStaffComponentPerPageWithSign = 9;
        #endregion

        #region Constructor
        public PayPayslip()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            this.HideReportHeader = this.HidePageFooter = false;
            using (clsPayrollBase payBase = new clsPayrollBase())
            {
                resultArgs = payBase.FetchPayrollStaff(this.ReportProperties.PayrollGroupId, this.ReportProperties.PayrollId, string.Empty);
                if (resultArgs.Success)
                {
                    dtStaffPersonalInfo = resultArgs.DataSource.Table;
                    BindPaySlipReport();
                }
            }
        }
        #endregion

        #region Methods
        private void BindPaySlipReport()
        {
            staffno = staffcompno = 0;
            lblSignNote.Visible = true;
            if (ReportProperty.Current.PayrollId != string.Empty && ReportProperty.Current.PayrollId != "0" &&
                 ReportProperty.Current.PayrollGroupId != string.Empty && ReportProperty.Current.PayrollGroupId != "0" &&
                 ReportProperty.Current.PayrollStaffId != string.Empty && ReportProperty.Current.PayrollStaffId != "0")
            {
                xrHeaderProjectName.Text = ReportProperty.Current.PayrollProjectTitle;
                xrlblAddress.Text = ReportProperty.Current.PayrollProjectAddress;
                if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollSignOfEmployer))
                {
                    lblEmployerSign.Text = ReportProperty.Current.PayrollSignOfEmployer;
                }
                else
                {
                    lblEmployerSign.Text = "Signature of Employer";
                }
                if (this.AppSetting.BranchOfficeCode == "bsofttsccinb")
                {
                    ReportProperty.Current.IncludeSignDetails = 1;
                    ReportProperty.Current.RoleName1 = "Signature of Employee";
                    ReportProperty.Current.RoleName3 = "Signature of Employer";
                }

                xrlblPaySlipTitle.Text = "Payslip for the Month of " + ReportProperty.Current.PayrollName;
                
                if (!String.IsNullOrEmpty(ReportProperty.Current.PayrollPayrollDate))
                {
                    xrlblPaySlipTitle.Text = "Payslip for the Month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.PayrollPayrollDate, false).ToString("MMMM yyyy");
                }

                this.SetCurrencyFormat(xrCellEarningAmt.Text, xrCellEarningAmt);
                this.SetCurrencyFormat(xrCellDeductionAmt.Text, xrCellDeductionAmt);
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        BindProperty();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowPayslipForm();
                    }
                }
                else
                {
                    BindProperty();
                }
            }
            else
            {
                SetReportTitle();
                ShowPayslipForm();
            }
        }

        private void BindProperty()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            setHeaderTitleAlignment();
            SetReportTitle();

            resultArgs = GetReportSource();
            if (resultArgs != null && resultArgs.DataSource.TableView.ToTable().Rows.Count > 0)
            {
                DataView dvDayBook = resultArgs.DataSource.TableView;
                //On 26/07/2019, to maintain the same component order
                dvDayBook.Table.Columns.Add("EORDER", typeof(System.Int32), "IIF(EARNINGS_ORDER = '', 9999, EARNINGS_ORDER)");
                dvDayBook.Table.Columns.Add("DORDER", typeof(System.Int32), "IIF(DEDUCTION_ORDER = '', 9999, DEDUCTION_ORDER)");
                //dvDayBook.RowFilter = "EARNINGS <> 'BASIC'";
                dvDayBook.Sort = "EORDER, DORDER";
                dvDayBook.Table.TableName = "PAYWAGES";

                (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = xrlblPaySlipTitle.WidthF;
                (xrSubSignFooter.ReportSource as SignReportFooter).KeepExtractHeight = true;
                (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();

                DataTable dtPaySlip = PaySlipMakeProperOrder(dvDayBook.Table);

                this.DisplayName = xrlblPaySlipTitle.Text;
                //replace special characters which are not valid for file names
                this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
                //--------------------------------------------------------------------------------------

                this.DataSource = dtPaySlip;
                this.DataMember = dtPaySlip.TableName;

                grpStaffHeader.GroupFields.Clear();
                grpPGHeader.GroupFields.Clear();
                grpPGHeader.GroupFields.Add(new GroupField("GROUPNAME"));
                grpStaffHeader.GroupFields.Add(new GroupField("STAFFID"));
                grpStaffHeader.SortingSummary.Enabled = true;
                 
                grpStaffHeader.SortingSummary.FieldName = string.Empty;
                grpStaffHeader.SortingSummary.FieldName = "STAFFORDER";
                grpStaffHeader.SortingSummary.Function = SortingSummaryFunction.Avg;
                grpStaffHeader.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
            }
            else
            {
                this.DataSource = null;
            }

            this.HideReportSubTitle = false;

            SplashScreenManager.CloseForm();
            base.ShowReport();
        }


        /// <summary>
        /// To have proper deduction components for Temp purpose
        /// </summary>
        /// <param name="dtPaySlip"></param>
        /// <returns></returns>
        private DataTable PaySlipMakeProperOrder(DataTable dtPaySlip)
        {
            DataTable dtPaySlipReport = dtPaySlip.DefaultView.ToTable(dtPaySlip.TableName, false, new string[] { "STAFFID", "STAFFORDER", "GROUPNAME", "EARNINGS", "EARNINGS_DESCRIPTION", "EAMOUNT", "NET PAY", "EORDER" });
            dtPaySlipReport.Columns.Add("DEDUCTIONS", dtPaySlip.Columns["EARNINGS"].DataType);
            dtPaySlipReport.Columns.Add("DEDUCTION_DESCRIPTION", dtPaySlip.Columns["EARNINGS_DESCRIPTION"].DataType);
            dtPaySlipReport.Columns.Add("DAMOUNT", dtPaySlip.Columns["EAMOUNT"].DataType);
            dtPaySlipReport.DefaultView.RowFilter = "EORDER <> 9999 ";
            dtPaySlipReport = dtPaySlipReport.DefaultView.ToTable();
            DataTable dtPaySlipEarning = dtPaySlipReport.DefaultView.ToTable();

            DataTable dtPaySlipDeductions = dtPaySlip.DefaultView.ToTable(dtPaySlip.TableName, false, new string[] { "STAFFID", "STAFFORDER", "GROUPNAME", "DEDUCTIONS", "DEDUCTION_DESCRIPTION", "DAMOUNT", "DORDER" });
            dtPaySlipDeductions.DefaultView.Sort = "DORDER";
            dtPaySlipDeductions.DefaultView.RowFilter = "DORDER <> 9999 ";
            dtPaySlipDeductions = dtPaySlipDeductions.DefaultView.ToTable();

            foreach (DataRow drStaffPersonal in dtStaffPersonalInfo.Rows)
            {
                int nStaff = UtilityMember.NumberSet.ToInteger(drStaffPersonal["STAFFID"].ToString());
                dtPaySlipReport.DefaultView.RowFilter = string.Empty;
                dtPaySlipReport.DefaultView.RowFilter = "STAFFID = " + nStaff;
                dtPaySlipEarning.DefaultView.RowFilter = "STAFFID = " + nStaff;
                int NoOfStaffEarnings = dtPaySlipEarning.DefaultView.Count; //dtPaySlipReport.DefaultView.Count;
                if (NoOfStaffEarnings > 0)
                {

                }
                dtPaySlipDeductions.DefaultView.RowFilter = "STAFFID = " + nStaff;
                
                int rowindex = 0;
                foreach (DataRowView drvStaffDeductions in dtPaySlipDeductions.DefaultView)
                {
                    if (dtPaySlipReport.DefaultView.Count > 0)
                    {
                        if (rowindex < NoOfStaffEarnings)
                        {
                            dtPaySlipReport.DefaultView[rowindex].BeginEdit();
                            dtPaySlipReport.DefaultView[rowindex]["STAFFORDER"] = UtilityMember.NumberSet.ToInteger(drStaffPersonal["STAFFORDER"].ToString());
                            dtPaySlipReport.DefaultView[rowindex]["GROUPNAME"] = drStaffPersonal["GROUPNAME"].ToString();
                            dtPaySlipReport.DefaultView[rowindex]["DEDUCTIONS"] = drvStaffDeductions["DEDUCTIONS"].ToString();
                            dtPaySlipReport.DefaultView[rowindex]["DEDUCTION_DESCRIPTION"] = drvStaffDeductions["DEDUCTION_DESCRIPTION"].ToString();
                            dtPaySlipReport.DefaultView[rowindex]["DAMOUNT"] = drvStaffDeductions["DAMOUNT"].ToString();
                            dtPaySlipReport.DefaultView[rowindex].EndEdit();
                            dtPaySlipReport.DefaultView.Table.AcceptChanges();
                        }
                        else
                        {
                            if (drvStaffDeductions["STAFFID"] != null && UtilityMember.NumberSet.ToInteger(drvStaffDeductions["STAFFID"].ToString()) > 0)
                            {
                                DataRowView newrowview = dtPaySlipReport.DefaultView.AddNew();
                                newrowview["STAFFID"] = nStaff;
                                newrowview["STAFFORDER"] = UtilityMember.NumberSet.ToInteger(drStaffPersonal["STAFFORDER"].ToString());
                                newrowview["GROUPNAME"] = drStaffPersonal["GROUPNAME"].ToString(); ;
                                newrowview["DEDUCTIONS"] = drvStaffDeductions["DEDUCTIONS"].ToString();
                                newrowview["DEDUCTION_DESCRIPTION"] = drvStaffDeductions["DEDUCTION_DESCRIPTION"].ToString();
                                newrowview["DAMOUNT"] = drvStaffDeductions["DAMOUNT"].ToString();
                                dtPaySlipReport.DefaultView.Table.AcceptChanges();
                            }
                        }
                        rowindex++;
                    }
                }

                dtPaySlipReport.DefaultView.RowFilter = string.Empty;
                dtPaySlipDeductions.DefaultView.RowFilter = string.Empty;
            }
            dtPaySlipReport.DefaultView.RowFilter = string.Empty;
            return dtPaySlipReport;
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                using (clsPayrollBase payBase = new clsPayrollBase())
                {
                    resultArgs = payBase.RpPayslip(ReportProperty.Current.PayrollId, ReportProperty.Current.PayrollStaffId);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        #region Events


        private void xrName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            EarningAmount = DeductionAmount = 0;
        }
        #endregion

        private void xrNetSalary_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //NetAmount = this.UtilityMember.NumberSet.ToDouble(xrHNetSalaryAmt.Text);
            NetAmount = ReportProperty.Current.NumberSet.ToDouble(xrGrossEarning.Text) - ReportProperty.Current.NumberSet.ToDouble(xrGrossDeduction.Text);
            xrNetSalary.Text = this.UtilityMember.NumberSet.ToNumber(NetAmount);
            this.SetCurrencyFormatPrefix(xrNetSalary.Text, xrNetSalary);
            xrNetSalary.Text += " (" + ConvertRuppessInWord.GetRupeesToWord(NetAmount.ToString()) + ") ";
        }

        private void xrGrossEarning_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrGrossEarning.Text = ReportProperty.Current.NumberSet.ToNumber(EarningAmount).ToString();
            e.Cancel = false;
        }

        private void xrGrossDeduction_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrGrossDeduction.Text = ReportProperty.Current.NumberSet.ToNumber(DeductionAmount).ToString();
            e.Cancel = false;
        }

        private void xrEvenPageBreak_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if ((staffno % 2) == 0)
            //{
            //    this.xrEvenPageBreak.Visible = false;
            //}
            //else
            //{
            //    this.xrEvenPageBreak.Visible = false;
            //}
        }

        private void grpStaffHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("STAFFID") != null)
            {
                staffno++;
                staffcompno = 0;
                string staffid = GetCurrentColumnValue("STAFFID").ToString();
                dtStaffPersonalInfo.DefaultView.RowFilter = string.Empty;
                xrCode.Text = string.Empty;
                xrCode.Tag = null;
                xrName.Text = string.Empty;
                xrDesignation.Text = string.Empty;
                xrUAN.Text = string.Empty;
                xrBankAccount.Text = string.Empty;
                xrDepartment.Text = string.Empty;

                if (dtStaffPersonalInfo != null && dtStaffPersonalInfo.Rows.Count > 0)
                {
                    dtStaffPersonalInfo.DefaultView.RowFilter = "STAFFID=" + staffid;
                    if (dtStaffPersonalInfo.DefaultView.Count > 0)
                    {
                        xrCode.Text = dtStaffPersonalInfo.DefaultView[0]["EMPNO"].ToString().Trim();
                        xrCode.Tag = dtStaffPersonalInfo.DefaultView[0]["STAFFNAME"].ToString().Trim();
                        xrName.Text = dtStaffPersonalInfo.DefaultView[0]["STAFFNAME"].ToString().Trim();
                        xrDesignation.Text = dtStaffPersonalInfo.DefaultView[0]["DESIGNATION"].ToString().Trim();
                        xrUAN.Text = dtStaffPersonalInfo.DefaultView[0]["UAN"].ToString().Trim();
                        xrBankAccount.Text = dtStaffPersonalInfo.DefaultView[0]["ACCOUNT_NUMBER"].ToString().Trim();
                        xrDepartment.Text = dtStaffPersonalInfo.DefaultView[0]["DEPARTMENT"].ToString().Trim();
                        xrLblDepartment.Visible = !string.IsNullOrEmpty(xrDepartment.Text.Trim());
                    }
                }
                if (!xrLblDepartment.Visible)
                {
                    xrLblUAN.LeftF = xrLblDepartment.LeftF;
                    xrUAN.LeftF = xrDepartment.LeftF;
                }
                else
                {
                    xrUAN.LeftF = xrLblBankAccount.LeftF - xrUAN.WidthF;
                    xrLblUAN.LeftF = xrUAN.LeftF - xrLblUAN.WidthF;
                }
                dtStaffPersonalInfo.DefaultView.RowFilter = string.Empty;

                if ((staffno % 2) == 0)
                {
                    xrdotline.Visible = false;
                }
                else
                {
                    xrdotline.TopF = (xrHeadingRow.HeightF * NoOfStaffComponentPerPage + 1);
                    xrdotline.Visible = true;
                }

                lblSignNote.TopF = xrdotline.TopF - (lblSignNote.HeightF);
            }
        }

        private void grpStaffFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //On 21/05/2019, to make two payslip per page
            //or if staff componetns exceed defined like 12, it will move to next page.
            if (((staffno % 2) == 0) || (staffcompno > NoOfStaffComponentPerPage) || 
                (this.ReportProperties.PayrollPayslipInSeparatePages ==1 ) ||
                (this.ReportProperties.IncludeSignDetails == 1 && staffcompno > NoOfStaffComponentPerPageWithSign))
                grpStaffHeader.PageBreak = PageBreak.BeforeBand;
            else
                grpStaffHeader.PageBreak = PageBreak.None;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            staffcompno++;
        }

        private void xrEarnAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null)
            {
                if (this.ReportProperties.PayrollStaffId != string.Empty && this.ReportProperties.PayrollStaffId != "0")
                {
                    if (GetCurrentColumnValue("EAMOUNT") != null && GetCurrentColumnValue("DEDUCTIONS") != null && GetCurrentColumnValue("EARNINGS") != null)
                    {
                        EarningAmount += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("EAMOUNT").ToString());
                        if ((GetCurrentColumnValue("EAMOUNT").ToString() == "0" && GetCurrentColumnValue("EARNINGS").ToString() == string.Empty) || GetCurrentColumnValue("EARNINGS").ToString() == string.Empty)
                        {
                            xrEarnAmount.Text = string.Empty;
                        }
                    }
                }
            }
        }

        private void xrDeductionAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null)
            {
                if (this.ReportProperties.PayrollStaffId != string.Empty && this.ReportProperties.PayrollStaffId != "0")
                {
                    if (GetCurrentColumnValue("DAMOUNT") != null && GetCurrentColumnValue("DEDUCTIONS") != null)
                    {
                        DeductionAmount += this.UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("DAMOUNT").ToString());
                        if (GetCurrentColumnValue("DAMOUNT").ToString() == "0" && GetCurrentColumnValue("DEDUCTIONS").ToString() == string.Empty)
                        {
                            xrDeductionAmount.Text = string.Empty;
                        }
                    }
                }
            }
        }

        private void xrcellEarning_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("EARNINGS") != null)
            {
                XRTableCell cell = sender as XRTableCell;
                string earningcomp = GetCurrentColumnValue("EARNINGS").ToString();
                string earningcompDescrption = GetCurrentColumnValue("EARNINGS_DESCRIPTION").ToString();

                if (this.ReportProperties.PayrollShowComponentDescription == 1 && !string.IsNullOrEmpty(earningcomp) && !string.IsNullOrEmpty(earningcompDescrption) &&
                    earningcomp.Trim().ToUpper() != earningcompDescrption.Trim().ToUpper())
                {
                    cell.Text = earningcomp + " (" + earningcompDescrption + ")";
                }
            }
        }

        private void xrcellDeduction_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("DEDUCTIONS") != null)
            {
                XRTableCell cell = sender as XRTableCell;
                string deductioncomp = GetCurrentColumnValue("DEDUCTIONS").ToString();
                string deductioncompDescrption = GetCurrentColumnValue("DEDUCTION_DESCRIPTION").ToString();

                if (this.ReportProperties.PayrollShowComponentDescription == 1 && !string.IsNullOrEmpty(deductioncomp) && !string.IsNullOrEmpty(deductioncompDescrption) &&
                    deductioncomp.Trim().ToUpper() != deductioncompDescrption.Trim().ToUpper())
                {
                    cell.Text = deductioncomp + " (" + deductioncompDescrption + ")";
                }
            }
        }

        private void xrCode_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //On 20/09/2022, for SDBINB, to assign page document name as staff code,
            //It will be used to export payslips into different pdf pages
            if (sender != null && ReportProperty.Current.PayrollPayslipInSeparatePages == 1)
            {
                XRLabel lbl = (sender as XRLabel);
                if (lbl.Tag!=null && !string.IsNullOrEmpty(lbl.Text) )
                {
                    string staffcode = xrCode.Text.Trim();
                    string staffname = lbl.Tag.ToString();
                    string exportfilename = staffcode + "_" + staffname;
                    XRWatermark textWatermark = new XRWatermark();
                    textWatermark.Text = exportfilename;
                    textWatermark.TextDirection = DevExpress.XtraPrinting.Drawing.DirectionMode.Horizontal;
                    textWatermark.Font = new Font(textWatermark.Font.FontFamily, 20);
                    textWatermark.ForeColor = Color.DarkSlateGray;
                    textWatermark.TextTransparency = 200;
                    textWatermark.ShowBehind = true;

                    Pages[e.PageIndex].AssignWatermark(textWatermark);
                }
            }
        }

        private void xrName_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            
        }

    }
}
