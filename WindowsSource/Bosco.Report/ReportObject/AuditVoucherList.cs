using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;

namespace Bosco.Report.ReportObject
{
    public partial class AuditVoucherList : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public AuditVoucherList()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrTableSource, xrLedgerName,
               new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;

        string BaseReportId = string.Empty;
        string RecentDrillingReportId = string.Empty;

        Int32 DrillVoucherDefinationId = 0;
        string DrillVoucherDefinationName = string.Empty;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                BaseReportId = GetBaseReportId();

                if (dicDDProperties.ContainsKey("REPORT_ID"))
                    RecentDrillingReportId = dicDDProperties["REPORT_ID"].ToString();

                if (dicDDProperties.ContainsKey("VOUCHER_ID"))
                    DrillVoucherDefinationId = UtilityMember.NumberSet.ToInteger(dicDDProperties["VOUCHER_ID"].ToString());

                if (dicDDProperties.ContainsKey("VOUCHER_NAME"))
                    DrillVoucherDefinationName = dicDDProperties["VOUCHER_NAME"].ToString().Trim();
            }
            ShowAuditVoucherLog();
        }
        #endregion

        #region Methods
        private void ShowAuditVoucherLog()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) || !string.IsNullOrEmpty(this.ReportProperties.DateTo) 
                || this.ReportProperties.Project != "0" || string.IsNullOrEmpty(this.ReportProperties.Project))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        SetReportProperty();
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
                    SetReportProperty();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportBorder();
        }

        private void SetReportProperty()
        {
            try
            {
                this.SetLandscapeHeader = 1065.25f;
                this.SetLandscapeFooter = 1065.25f;
                this.SetLandscapeFooterDateWidth = 910.25f;

                SetTitleWidth(xrtblHeaderCaption.WidthF);
                this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
                this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
                this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;

                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.CosCenterName = null;

                SetReportTitle();
                if (!string.IsNullOrEmpty(this.ReportProperties.AuditAction))
                {
                    this.ReportTitle = "List of " + this.ReportProperties.AuditAction +
                             (string.IsNullOrEmpty(this.ReportProperties.DayBookVoucherTypeName) ? string.Empty : " " + this.ReportProperties.DayBookVoucherTypeName) + " Vouchers";
                }
                else if (!string.IsNullOrEmpty(this.ReportProperties.DayBookVoucherTypeName))
                {
                    this.ReportTitle = "List of " + this.ReportProperties.DayBookVoucherTypeName + " Vouchers";
                }
                setHeaderTitleAlignment();
                
                resultArgs = GetReportSource();
                if (resultArgs.Success)
                {
                    DataTable dtAuditLog = resultArgs.DataSource.Table;
                    if (dtAuditLog != null && dtAuditLog.Rows.Count != 0)
                    {
                        dtAuditLog.TableName = "AuditVoucherLog";

                        //On 16/09/2021, To Filter Audit Action ---------------------------------
                        string auditaction = string.Empty;
                        if (!string.IsNullOrEmpty(this.ReportProperties.AuditAction))
                        {
                            auditaction = reportSetting1.AuditLog.AUDIT_ACTIONColumn.ColumnName + " = '" + this.ReportProperties.AuditAction + "'";
                        }
                        if (!string.IsNullOrEmpty(auditaction))
                        {
                            dtAuditLog.DefaultView.RowFilter = auditaction;
                            dtAuditLog = dtAuditLog.DefaultView.ToTable();
                        }
                        //-----------------------------------------------------------------------
                        
                        //On 19/04/2022, To Filter Voucher Type ---------------------------------
                        if (this.ReportProperties.DayBookVoucherType>0)
                        {
                            DefaultVoucherTypes vtype = (DefaultVoucherTypes)this.ReportProperties.DayBookVoucherType;
                            dtAuditLog.DefaultView.RowFilter = reportSetting1.AuditLog.VOUCHER_TYPEColumn.ColumnName + " = '" + vtype.ToString() + "'";
                            dtAuditLog = dtAuditLog.DefaultView.ToTable();
                        }
                        
                        //On 16/09/2021, To add Amount filter condition
                        string filterfields = reportSetting1.AuditLog.CREDITColumn.ColumnName + "," + reportSetting1.AuditLog.DEBITColumn.ColumnName;
                        string filtercondition = this.AttachAmountFilter(dtAuditLog.DefaultView, filterfields);
                        lblAmountFilter.Text = filtercondition;
                        lblAmountFilter.Visible = (!string.IsNullOrEmpty(lblAmountFilter.Text));

                        this.DataSource = dtAuditLog;
                        this.DataMember = dtAuditLog.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
                    }
                }
                else
                {
                    MessageRender.ShowMessage("Could not generate Report " + resultArgs.Message, true);
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Could not generate Report " + err.Message, true);
            }

        }

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption, true);
            xrTableSource = AlignContentTable(xrTableSource);
            xrtblFooter = AlignHeaderTable(xrtblFooter, true);

            this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
        }
        
        private ResultArgs GetReportSource()
        {
            try
            {
                string AuditLog = this.GetAuditReportSQL(SQL.ReportSQLCommand.AuditReports.VoucherAuditLog);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);

                    //On 16/09/2021, to filter selected voucher types
                    if (IsDrillDownMode)
                    {
                        dataManager.Parameters.Add(reportSetting1.AuditLog.VOUCHER_DEFINITION_IDColumn.ColumnName, DrillVoucherDefinationId);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, AuditLog);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return resultArgs;
        }

        private string GetBaseReportId()
        {
            string Rtn = string.Empty;

            foreach (object item in this.ReportProperties.stackActiveDrillDownHistory)
            {
                EventDrillDownArgs eventdrilldownarg = item as EventDrillDownArgs;
                if (eventdrilldownarg.DrillDownType == DrillDownType.BASE_REPORT)
                {
                    Rtn = eventdrilldownarg.DrillDownRpt;
                    break;
                }
            }

            return Rtn;
        }
        
        #endregion

       

        #region Events

        #endregion


    }
}
