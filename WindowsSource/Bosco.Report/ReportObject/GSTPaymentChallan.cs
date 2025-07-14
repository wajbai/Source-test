using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Data;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Xml;

namespace Bosco.Report.ReportObject
{
    public partial class GSTPaymentChallan : Bosco.Report.Base.ReportHeaderBase
    {
        #region Property
        ResultArgs resultArgs = null;
        double CGST = 0;
        double SGST = 0;
        double GSt = 0;
        double IGST = 0;
        #endregion

        #region Constructor

        public GSTPaymentChallan()
        {
            InitializeComponent();
        }

        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            BindGstPaymentSource();
        }

        #endregion

        #region Methods
        private void BindGstPaymentSource()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) || this.ReportProperties.Project != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        SetReportTitle();
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                        this.HideReportHeader = false;
                        this.HidePageFooter = false;
                        xrtblState.Text = ReportProperties.GSTState;
                        resultArgs = GetReportSource();
                        DataView dvgSt = resultArgs.DataSource.TableView;
                        if (dvgSt != null && dvgSt.Count != 0)
                        {
                            dvgSt.Table.TableName = "GSTPaymentChellan";
                            this.DataSource = dvgSt;
                            this.DataMember = dvgSt.Table.TableName;

                            DataTable dtGStSource = dvgSt.ToTable();
                            CGST = this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["CGST"].ToString());
                            SGST = this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["SGST"].ToString());
                            IGST = this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["IGST"].ToString());
                            double SubTotal = this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["CGST"].ToString()) + this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["IGST"].ToString());
                            double Total = SubTotal + SGST;

                            xrtblCGST.Text = xrtblTotalCGST.Text = this.UtilityMember.NumberSet.ToNumber(CGST);
                            xrtblSGST.Text = xrtblTotalSGST.Text = this.UtilityMember.NumberSet.ToNumber(SGST);
                            xrtblIGST.Text = xrtblTotalIGST.Text = this.UtilityMember.NumberSet.ToNumber(IGST);
                            xrSubTotal.Text = xrtblTotalSubTotal.Text = this.UtilityMember.NumberSet.ToNumber(SubTotal);
                            xrTotalAmt.Text = xrtblBAmount.Text = xrtblRemitter.Text = xrtblRemitteeAmount.Text = this.UtilityMember.NumberSet.ToNumber(Total);

                            // This is to fill the Info of Society Details
                            FetchInstPreferece();
                        }
                        else
                        {
                            this.DataSource = null;
                        }

                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    SetReportTitle();
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                    this.HideReportHeader = false;
                    this.HidePageFooter = false;
                    xrtblState.Text = ReportProperties.GSTState;
                    resultArgs = GetReportSource();
                    DataTable dtValue = resultArgs.DataSource.Table;
                    DataView dvgSt = resultArgs.DataSource.TableView;
                    if (dvgSt != null && dvgSt.Count != 0)
                    {
                        dvgSt.Table.TableName = "GSTPaymentChellan";
                        this.DataSource = dvgSt;
                        this.DataMember = dvgSt.Table.TableName;

                        DataTable dtGStSource = dvgSt.ToTable();
                        CGST = this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["CGST"].ToString());
                        SGST = this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["SGST"].ToString());
                        IGST = this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["IGST"].ToString());
                        double SubTotal = this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["CGST"].ToString()) + this.UtilityMember.NumberSet.ToDouble(dtGStSource.Rows[0]["IGST"].ToString());
                        double Total = SubTotal + SGST;

                        xrtblCGST.Text = xrtblTotalCGST.Text = this.UtilityMember.NumberSet.ToNumber(CGST);
                        xrtblSGST.Text = xrtblTotalSGST.Text = this.UtilityMember.NumberSet.ToNumber(SGST);
                        xrtblIGST.Text = xrtblTotalIGST.Text = this.UtilityMember.NumberSet.ToNumber(IGST);

                        xrSubTotal.Text = xrtblTotalSubTotal.Text = this.UtilityMember.NumberSet.ToNumber(SubTotal);
                        xrTotalAmt.Text = xrtblBAmount.Text = xrtblRemitteeAmount.Text = xrtblRemitter.Text = this.UtilityMember.NumberSet.ToNumber(Total);

                        // This is to fill the Info of Society Details
                        FetchInstPreferece();
                    }
                    else
                    {
                        this.DataSource = null;
                    }
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
        }

        private void FetchInstPreferece()
        {
            string query = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.GSTInsPreference);
            using (DataManager dataManager = new DataManager())
            {
                if (ReportProperty.Current.SocietyId != 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CUSTOMERIDColumn, this.ReportProperties.SocietyId);
                }
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, query);
            }
            DataTable dtInstPreference = resultArgs.DataSource.Table;
            if (dtInstPreference != null && dtInstPreference.Rows.Count != 0)
            {
                xrtblGSTIN.Text = dtInstPreference.Rows[0]["GST_NO"].ToString();
                xrtblEmailId.Text = dtInstPreference.Rows[0]["EMAIL"].ToString();
                xrtblMobileNo.Text = xrtblContacNo.Text = dtInstPreference.Rows[0]["PHONE"].ToString();
                xrtblName.Text = xrtblSocietyRemittee.Text = dtInstPreference.Rows[0]["SOCIETYNAME"].ToString();
                xrtblAddress.Text = xrtblAddressRemittee.Text = dtInstPreference.Rows[0]["ADDRESS"].ToString();
            }
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string GStSource = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.GStPaymentChellan);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, GStSource);
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

    }
}

