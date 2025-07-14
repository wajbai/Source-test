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
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class AudittedVouchersTrackChanges: Bosco.Report.Base.ReportHeaderBase
    {
        #region Constructor
        public AudittedVouchersTrackChanges()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrTableSource, xrLedgerName,
               new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
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
            this.SetLandscapeHeader = 1065.25f;
            this.SetLandscapeFooter = 1065.25f;
            this.SetLandscapeFooterDateWidth = 910.25f;

            setHeaderTitleAlignment();
            SetReportTitle();

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            this.CosCenterName = null;
            SetTitleWidth(xrtblHeaderCaption.WidthF);
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;


            resultArgs = GetReportSource();
            if (resultArgs.Success)
            {
                DataTable dtTrackOnAuditedChanges = resultArgs.DataSource.Table;
                Detail.Visible = grpVoucherHeader.Visible = grpVoucherFooter.Visible = false;
                if (dtTrackOnAuditedChanges != null && dtTrackOnAuditedChanges.Rows.Count != 0)
                {
                    dtTrackOnAuditedChanges.TableName = "AuditVoucherLog";

                    //On 16/09/2021, To Filter Audit Action ---------------------------------
                    string auditaction = string.Empty;
                    if (!string.IsNullOrEmpty(this.ReportProperties.AuditAction))
                    {
                        auditaction = reportSetting1.AuditLog.AUDIT_ACTIONColumn.ColumnName + " = '" + this.ReportProperties.AuditAction + "'";
                    }
                    if (!string.IsNullOrEmpty(auditaction))
                    {
                        dtTrackOnAuditedChanges.DefaultView.RowFilter = auditaction;
                        dtTrackOnAuditedChanges = dtTrackOnAuditedChanges.DefaultView.ToTable();
                    }
                    //-----------------------------------------------------------------------

                    //On 16/09/2021, To Filter user details---------------------------------
                    string userfitler = string.Empty;
                    if (!string.IsNullOrEmpty(this.ReportProperties.CreatedByName))
                    {
                        userfitler = reportSetting1.AuditLog.CREATED_BY_NAMEColumn.ColumnName + " = '" + this.ReportProperties.CreatedByName.Replace("'", "''") + "'";
                    }

                    if (!string.IsNullOrEmpty(this.ReportProperties.ModifiedByName))
                    {
                        userfitler += (!string.IsNullOrEmpty(userfitler) ? " AND " : "") + reportSetting1.AuditLog.MODIFIED_BY_NAMEColumn.ColumnName + " = '" + this.ReportProperties.ModifiedByName.Replace("'", "''") + "'";
                    }

                    if (!string.IsNullOrEmpty(userfitler))
                    {
                        dtTrackOnAuditedChanges.DefaultView.RowFilter = userfitler;
                        dtTrackOnAuditedChanges = dtTrackOnAuditedChanges.DefaultView.ToTable();
                    }
                    //----------------------------------------------------------------------

                    //On 16/09/2021, To add Amount filter condition
                    string filterfields = reportSetting1.AuditLog.CREDITColumn.ColumnName + "," + reportSetting1.AuditLog.DEBITColumn.ColumnName;
                    string filtercondition = this.AttachAmountFilter(dtTrackOnAuditedChanges.DefaultView, filterfields);
                    lblAmountFilter.Text = filtercondition;
                    lblAmountFilter.Visible = (!string.IsNullOrEmpty(lblAmountFilter.Text));

                    Detail.SortFields.Clear();
                    Detail.SortFields.Add(new GroupField(reportSetting1.AuditLog.TRACK_MODIFIED_ONColumn.ColumnName));
                    Detail.SortFields[0].SortOrder = XRColumnSortOrder.Descending;

                    this.DataSource = dtTrackOnAuditedChanges;
                    this.DataMember = dtTrackOnAuditedChanges.TableName;
                    Detail.Visible = grpVoucherHeader.Visible = grpVoucherFooter.Visible = (dtTrackOnAuditedChanges.Rows.Count > 0);
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

        

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption, true);
            xrTblHeaderAuditHistory = AlignHeaderTable(xrTblHeaderAuditHistory, true);
            xrTableSource = AlignContentTable(xrTableSource);
            xrTblAuditHistory = AlignContentTable(xrTblAuditHistory);
            
            this.SetCurrencyFormat(xrCapDebit.Text, xrCapDebit);
            this.SetCurrencyFormat(xrCapCredit.Text, xrCapCredit);
        }

        private XRTable AlignContentTable(XRTable table)
        {

            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = BorderSide.All; //BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = BorderSide.Bottom; //BorderSide.Left | BorderSide.Right | BorderSide.Bottom;

                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = tcell.Borders = BorderSide.Left | BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string AuditLog = this.GetAuditReportSQL(SQL.ReportSQLCommand.AuditReports.AuditedVouchersTrackChanges);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(reportSetting1.AuditLog.IS_AUDITOR_MODIFIEDColumn.ColumnName, "1");
                    dataManager.Parameters.Add(reportSetting1.User.USER_IDColumn, this.AppSetting.DefaultAuditorUserId);

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
        
        #endregion

                      
        private void xrlblAuditorTrackType_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.AuditLog.AUDITOR_TRACKColumn.ColumnName) != null)
            {
                string auditortrack = GetCurrentColumnValue(reportSetting1.AuditLog.AUDITOR_TRACKColumn.ColumnName).ToString();
                XRLabel lblAuditorTrack = sender as XRLabel;
                e.Value = (auditortrack.ToUpper() == "YES" ? "Audited Vouchers with History" : "Audited Vouchers without History");
            }
        }

        #region Events

        #endregion


    }
}
