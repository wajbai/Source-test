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
    public partial class AuditVoucherLog : Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public AuditVoucherLog()
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
        Int32 count = 0;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            if (IsDrillDownMode)
            {
                Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());

                this.ReportProperties.AuditAction = string.Empty;
                this.ReportProperties.CreatedByName = string.Empty;
                this.ReportProperties.ModifiedByName = string.Empty;
                if (!string.IsNullOrEmpty(this.ReportProperties.UserName))
                {
                    this.ReportProperties.CreatedByName = this.ReportProperties.UserName;
                    this.ReportProperties.ModifiedByName = this.ReportProperties.UserName;
                }
                
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
                this.SetTitleLeftWithoutMargin(true);

                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                this.CosCenterName = null;

                SetReportTitle();
                if (!string.IsNullOrEmpty(this.ReportProperties.AuditAction))
                {
                    this.ReportTitle = "Audit Log - " + this.ReportProperties.AuditAction + " Vouchers";
                }
                setHeaderTitleAlignment();
                              
                resultArgs = GetReportSource();
                if (resultArgs.Success)
                {
                    DataTable dtAuditLog = resultArgs.DataSource.Table;
                    if (dtAuditLog != null && dtAuditLog.Rows.Count != 0)
                    {
                        dtAuditLog.TableName = "AuditVoucherLog";

                        //On 19/12/2022, to show GST and GST Invoice Vouchers Alone-------------------------------------------
                        if (this.ReportProperties.ShowGSTVouchers == 1)
                        {
                            dtAuditLog.DefaultView.RowFilter = "GST > 0";
                            dtAuditLog = dtAuditLog.DefaultView.ToTable();
                        }
                        if (this.ReportProperties.ShowGSTInvoiceVouchers == 1)
                        {
                            dtAuditLog.DefaultView.RowFilter = "GST_VENDOR_ID > 0";
                            dtAuditLog = dtAuditLog.DefaultView.ToTable();
                        }

                        //-------------------------------------------------------------------------------------------------------

                        //On 16/09/2021, To Filter Audit Action -----------------------------------------------------------------
                        string auditaction = string.Empty;
                        if (!string.IsNullOrEmpty(this.ReportProperties.AuditAction))
                        {
                            auditaction = reportSetting1.AuditLog.AUDIT_ACTIONColumn.ColumnName + " = '" + this.ReportProperties.AuditAction + "'";
                            
                            //On 02/05/2023, criteria action is created, It should -----------------------------------------------
                            if (!string.IsNullOrEmpty(this.ReportProperties.CreatedByName) && this.ReportProperties.AuditAction.ToUpper() == "CREATED")
                            {
                                auditaction += " OR (" + reportSetting1.AuditLog.CREATED_BY_NAMEColumn.ColumnName + " = '" + this.ReportProperties.CreatedByName.Replace("'", "''") + "')";
                            }
                            //----------------------------------------------------------------------------------------------------
                        }
                        if (!string.IsNullOrEmpty(auditaction))
                        {
                            dtAuditLog.DefaultView.RowFilter = auditaction;
                            dtAuditLog = dtAuditLog.DefaultView.ToTable();
                        }
                        //----------------------------------------------------------------------------------------------------------
                        
                        //On 16/09/2021, To Filter user details---------------------------------------------------------------------
                        string userfitler = string.Empty;
                        if (!string.IsNullOrEmpty(this.ReportProperties.CreatedByName))
                        {
                            userfitler = reportSetting1.AuditLog.CREATED_BY_NAMEColumn.ColumnName + " = '" + this.ReportProperties.CreatedByName.Replace("'", "''") + "'";
                        }

                        if (!string.IsNullOrEmpty(this.ReportProperties.ModifiedByName))
                        {
                            userfitler += (!string.IsNullOrEmpty(userfitler) ? " AND " : "") + reportSetting1.AuditLog.MODIFIED_BY_NAMEColumn.ColumnName + " = '" + this.ReportProperties.ModifiedByName.Replace("'","''") + "'";
                        }

                        if (!string.IsNullOrEmpty(userfitler))
                        {
                            dtAuditLog.DefaultView.RowFilter = userfitler;
                            dtAuditLog = dtAuditLog.DefaultView.ToTable();
                        }
                        //-------------------------------------------------------------------------------------------------------------

                        //On 19/04/2022, To Filter Voucher Type -----------------------------------------------------------------------
                        if (this.ReportProperties.DayBookVoucherType > 0)
                        {
                            //DefaultVoucherTypes vtype = (DefaultVoucherTypes)this.ReportProperties.DayBookVoucherType;
                            //dtAuditLog.DefaultView.RowFilter = reportSetting1.AuditLog.VOUCHER_TYPEColumn.ColumnName + " = '" + vtype.ToString() + "'";
                            dtAuditLog.DefaultView.RowFilter = reportSetting1.AuditLog.VOUCHER_DEFINITION_IDColumn.ColumnName + " = " + this.ReportProperties.DayBookVoucherType;
                            dtAuditLog = dtAuditLog.DefaultView.ToTable();
                        }
                        //---------------------------------------------------------------------------------------------------------------

                        //To show/Hide "Auditor by" column
                        if (AppSetting.IS_SDB_INM)
                        {
                            xrTableSource.SuspendLayout();
                            if (xrHeaderRow.Cells.Contains(xrCapByAuditor))
                                xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrCapByAuditor.Name]);

                            if (xrDataRow.Cells.Contains(xrByAuditor))
                                xrDataRow.Cells.Remove(xrDataRow.Cells[xrByAuditor.Name]);

                            if (xrRowNarration.Cells.Contains(xrcellEmpty13))
                                xrRowNarration.Cells.Remove(xrRowNarration.Cells[xrcellEmpty13.Name]);

                            xrTableSource.PerformLayout();
                        }

                        //To show/Hide "Third Party" column
                        if (string.IsNullOrEmpty(AppSetting.ManagementCode))
                        {
                            xrTableSource.SuspendLayout();
                            if (xrHeaderRow.Cells.Contains(xrCapThirdPartyCode))
                                xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrCapThirdPartyCode.Name]);

                            if (xrDataRow.Cells.Contains(xrThirdPartyCode))
                                xrDataRow.Cells.Remove(xrDataRow.Cells[xrThirdPartyCode.Name]);

                            if (xrRowNarration.Cells.Contains(xrcellEmpty13))
                                xrRowNarration.Cells.Remove(xrRowNarration.Cells[xrcellEmpty13.Name]);
                            xrTableSource.PerformLayout();
                        }

                        //To show/Hide "Authorization" column
                        if (AppSetting.ConfirmAuthorizationVoucherEntry == 0)
                        {
                            xrTableSource.SuspendLayout();
                            if (xrHeaderRow.Cells.Contains(xrCapAuthorization))
                                xrHeaderRow.Cells.Remove(xrHeaderRow.Cells[xrCapAuthorization.Name]);

                            if (xrDataRow.Cells.Contains(xrAuthorization))
                                xrDataRow.Cells.Remove(xrDataRow.Cells[xrAuthorization.Name]);

                            if (xrRowNarration.Cells.Contains(xrcellEmpty14))
                                xrRowNarration.Cells.Remove(xrRowNarration.Cells[xrcellEmpty14.Name]);
                            xrTableSource.PerformLayout();
                        }
                        
                        xrcellEmpty1.WidthF = xrDate.WidthF;
                        xrcellEmpty2.WidthF = xrVNo.WidthF;
                        //xrcellEmpty3.WidthF = xrVType.WidthF; 
                        xrcellEmpty4.WidthF =  xrVType.WidthF+ xrLedgerName.WidthF + xrCashBank.WidthF + xrRefNo.WidthF;
                        //xrcellGSTVendorDetails.WidthF = xrDate.WidthF + xrVNo.WidthF + xrVType.WidthF + xrLedgerName.WidthF + xrCashBank.WidthF;

                        //xrcellEmpty5.WidthF = xrRefNo.WidthF; 
                        xrcellEmpty6.WidthF = xrDebit.WidthF;
                        xrcellEmpty7.WidthF = xrAction.WidthF; 
                        xrcellEmpty8.WidthF = xrCreatedOn.WidthF;
                        xrcellEmpty9.WidthF = xrModifiedOn.WidthF; 
                        xrcellEmpty10.WidthF = xrCreatedBy.WidthF;
                        xrcellEmpty11.WidthF = xrModifiedBy.WidthF;
                        xrcellEmpty12.WidthF = (AppSetting.IS_SDB_INM ? xrThirdPartyCode.WidthF : xrByAuditor.WidthF);
                        xrGrandTotalCaption.WidthF = xrDate.WidthF + xrVNo.WidthF +xrVType.WidthF +xrLedgerName.WidthF+xrCashBank.WidthF;
                        xrGrandTotalAmount.WidthF = xrDebit.WidthF + xrRefNo.WidthF;
                        //xrGrandTotalCaption.WidthF = xrtblFooter.WidthF - (xrDate.WidthF + xrVNo.WidthF +xrVType.WidthF +xrLedgerName.WidthF+xrCashBank.WidthF+
                        //xrcellEmpty12.WidthF = xrByAuditor.WidthF;
                         
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

                    FillAuditStatisticsDetials();
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
            xrTblAuditStatisticsFooter = AlignHeaderTable(xrTblAuditStatisticsFooter, true);
            this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
        }

        private void FillAuditStatisticsDetials()
        {
            xrcellNoCreatedCaption.WidthF = xrcellNoModifiedCaption.WidthF = xrcellNoDeletedCaption.WidthF = (xrCapDate.WidthF + xrCapVNo.WidthF + xrCapVoucherType.WidthF + xrCapParticulars.WidthF);
            xrcellNoCreated.WidthF = xrcellNoModified.WidthF = xrcellNoDeleted.WidthF = xrCapCashBank.WidthF;
            xrcellNoCreated.Text = "0";
            xrcellNoModified.Text = "0";
            xrcellNoDeleted.Text = "0";
            if (this.DataSource != null)
            {
                DataTable dtAuditLog = this.DataSource as DataTable;
                string auditaction = reportSetting1.AuditLog.AUDIT_ACTIONColumn.ColumnName + " = 'CREATED'";
                xrcellNoCreated.Text = dtAuditLog.Compute("COUNT(" + reportSetting1.AuditLog.AUDIT_ACTIONColumn.ColumnName + ")", auditaction).ToString();
                auditaction = reportSetting1.AuditLog.AUDIT_ACTIONColumn.ColumnName + " = 'MODIFIED'";
                xrcellNoModified.Text = dtAuditLog.Compute("COUNT(" + reportSetting1.AuditLog.AUDIT_ACTIONColumn.ColumnName + ")", auditaction).ToString();
                auditaction = reportSetting1.AuditLog.AUDIT_ACTIONColumn.ColumnName + " = 'DELETED'";
                xrcellNoDeleted.Text = dtAuditLog.Compute("COUNT(" + reportSetting1.AuditLog.AUDIT_ACTIONColumn.ColumnName + ")", auditaction).ToString();
            }
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

        private void xrTableSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrTableSource = AlignTable(xrTableSource, Narration, string.Empty, count);
            xrVType.Borders = xrLedgerName.Borders = xrCashBank.Borders = xrRefNo.Borders  = (DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom);
            xrcellGSTDetails.Borders = DevExpress.XtraPrinting.BorderSide.All;// (DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom);

            xrRowGSTDetails.Visible = (this.ReportProperties.ShowGSTVouchers == 1 || this.ReportProperties.ShowGSTInvoiceVouchers == 1);
        }
    
        private void xrcellGSTDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string gstdetails = string.Empty;
            
            if (GetCurrentColumnValue(reportSetting1.GST_MASTER_INVOICE.VOUCHER_IDColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (this.ReportProperties.ShowGSTVouchers == 1 || this.ReportProperties.ShowGSTInvoiceVouchers == 1)
                {
                    string gstinvoicedetails = GetCurrentColumnValue("VENDOR_GST_INVOICE").ToString();
                    gstinvoicedetails = "GST Vendor Invoice Details : " + gstinvoicedetails;

                    string gstamount = "Total GST : " + UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("GST").ToString()));
                    gstamount += "   Total CST : " + UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("CGST").ToString()));
                    gstamount += "   Total SGST : " + UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(GetCurrentColumnValue("SGST").ToString()));
                    gstdetails += (this.ReportProperties.ShowGSTInvoiceVouchers == 1) ? gstinvoicedetails : string.Empty;
                    gstdetails += (this.ReportProperties.ShowGSTVouchers == 1) ? "    "  + gstamount : string.Empty;
                }
                cell.Text = gstdetails;
            }
        }

        private void xrModifiedBy_EvaluateBinding(object sender, BindingEventArgs e)
        {
            /*if (GetCurrentColumnValue(reportSetting1.GST_MASTER_INVOICE.VOUCHER_IDColumn.ColumnName) != null)
            {
                string createdby = (GetCurrentColumnValue(reportSetting1.AuditLog.CREATED_BY_NAMEColumn.ColumnName)==null) ? string.Empty :
                                        GetCurrentColumnValue(reportSetting1.AuditLog.CREATED_BY_NAMEColumn.ColumnName).ToString();
                string modifiedby = (GetCurrentColumnValue(reportSetting1.AuditLog.MODIFIED_BY_NAMEColumn.ColumnName) == null) ? string.Empty :
                                        GetCurrentColumnValue(reportSetting1.AuditLog.MODIFIED_BY_NAMEColumn.ColumnName).ToString();

                string modifiedon = (GetCurrentColumnValue(reportSetting1.AuditLog.MODIFIED_ONColumn.ColumnName)==null) ? string.Empty :
                                        GetCurrentColumnValue(reportSetting1.AuditLog.MODIFIED_ONColumn.ColumnName).ToString();

                if (!string.IsNullOrEmpty(modifiedon) && string.IsNullOrEmpty(modifiedby))
                {
                    e.Value = createdby;
                }
            }*/
        }

        #region Events

        #endregion


    }
}
