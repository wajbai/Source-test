using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using PAYROLL.Modules;
using System.Data;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.Utility.ConfigSetting;
using Payroll.DAO.Schema;
using Bosco.Utility.Common;

namespace Bosco.Report.ReportObject
{
    public partial class PayrollYearPayRegister : ReportHeaderBase
    {
        #region Declaration
        DataTable dtPayRegister = new DataTable();
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();
        ApplicationSchema.PRSTAFFGROUPDataTable dtstaffgrp = new ApplicationSchema.PRSTAFFGROUPDataTable();
        DataTable dtStaffPersonalInfo = new DataTable();
        float paperwidth = 1135;
        #endregion

        #region Constructors

        public PayrollYearPayRegister()
        {
            InitializeComponent();

            this.ReportProperties.DateFrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom);
            this.ReportProperties.DateTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo);
        }
        #endregion

        #region Show Report
        public override void ShowReport()
        {
            using (clsPayrollBase paybase = new clsPayrollBase())
            {
                ResultArgs resultStaff = paybase.FetchPayrollStaff(clsPayrollBase.payslipGroupid.ToString(), string.Empty, string.Empty);
                if (resultStaff.Success)
                {
                    dtStaffPersonalInfo = resultStaff.DataSource.Table;
                    BindPayRegisterReport();
                }
                else
                {
                    MessageRender.ShowMessage(resultStaff.Message);
                }
            }
            base.ShowReport();
        }
        #endregion

        #region Methods

        public void BindPayRegisterReport()
        {
            paperwidth = xrTblStaffInfo.WidthF;
            this.SetLandscapeHeader = paperwidth;
            this.SetLandscapeFooter = paperwidth;
            this.SetLandscapeFooterDateWidth = 890.00f;
            
            if (this.ReportProperties.PayrollGroupId.Trim() != string.Empty && this.ReportProperties.PayrollGroupId != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        ReportSetting();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowPayslipForm();
                    }
                }
                else
                {
                    ReportSetting();
                }
            }
            else
            {
                SetReportTitle();
                ShowPayslipForm();
            }
        }

        private void ReportSetting()
        {
            SplashScreenManager.ShowForm(typeof(frmReportWait));
            setHeaderTitleAlignment();
            SetReportTitle();

            this.InstituteName = ReportProperty.Current.PayrollProjectTitle;
            this.LegalEntityAddress = ReportProperty.Current.PayrollProjectAddress;

            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom) + " - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo);
            this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : (string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupName) ? string.Empty : ReportProperty.Current.PayrollGroupName);
            this.HideDateRange = false;
            if (!String.IsNullOrEmpty(ReportProperty.Current.DateFrom))
            {
                this.ReportTitle += " from " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false).ToString("MMMM yyyy") +
                                    " to " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateTo, false).ToString("MMMM yyyy");
            }

            this.DisplayName = this.ReportTitle;
            //replace special characters which are not valid for file names
            this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
            //--------------------------------------------------------------------------------------

            this.ReportProperties.ShowPageNumber = 1;

            this.PageHeader.Controls.Clear();
            this.Detail.Controls.Clear();
            //this.ReportFooter.Controls.Clear();
            this.grpPGHeader.GroupFields[0].FieldName = string.Empty;
            this.grpHeaderStaff.GroupFields[0].FieldName = string.Empty;    
            using (clsPayrollBase paybase = new clsPayrollBase())
            {
                ResultArgs resultArgs = paybase.GeneratePayRegister(ReportProperty.Current.PayrollGroupId, string.Empty,
                        ReportProperty.Current.PayrollStaffId, ReportProperty.Current.DateFrom, ReportProperty.Current.DateTo);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    dtPayRegister = resultArgs.DataSource.Table;
                    dtPayRegister.TableName = this.DataMember;

                    this.grpPGHeader.GroupFields[0].FieldName = string.Empty;
                    this.grpHeaderStaff.GroupFields[0].FieldName = string.Empty;    
                    this.grpPGHeader.GroupFields[0].FieldName = "GROUPNAME";
                    this.grpHeaderStaff.GroupFields[0].FieldName = dtCompMonth.STAFFIDColumn.ColumnName;
                    
                    //this.PageHeader.Controls.Add(HeaderTable());
                    this.grpHeaderStaff.Controls.Add(HeaderTable());
                    this.Detail.Controls.Add(DetailTable());
                    this.grpFooterStaff.Controls.Add(FooterTable());
                    //this.ReportFooter.Controls.Add(FooterTable());

                    (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = paperwidth;
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();

                    this.DataSource = dtPayRegister;
                    this.DataMember = dtPayRegister.TableName;

                    
                    grpHeaderStaff.SortingSummary.Enabled = true;
                    grpHeaderStaff.SortingSummary.FieldName = dtstaffgrp.STAFFORDERColumn.ColumnName;
                    grpHeaderStaff.SortingSummary.Function = SortingSummaryFunction.Avg;
                    grpHeaderStaff.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;

                }
                else
                {
                    MessageRender.ShowMessage(resultArgs.Message);
                }
            }
            SplashScreenManager.CloseForm();
        }

        private XRTable HeaderTable()
        {
            XRTable table = new XRTable();
            //table.Size = new Size(1000,50);
            table.BackColor = System.Drawing.Color.Gainsboro;
            table.BorderColor = System.Drawing.Color.DarkGray;
            table.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            table.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            table.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            table.Name = "xrTblHeader";
            table.SizeF = new System.Drawing.SizeF(paperwidth, 30f);
            table.StylePriority.UseBackColor = false;
            table.StylePriority.UseBorderColor = false;
            table.StylePriority.UseBorders = false;
            table.StylePriority.UseFont = false;
            table.StylePriority.UseTextAlignment = false;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            table.AdjustSize();

            //# to set staff info and header Position
            xrTblStaffInfo.LeftF = 0;
            xrTblStaffInfo.TopF = 25;
            table.TopF = xrTblStaffInfo.HeightF + xrTblStaffInfo.TopF + 10;
            
            // Start table initialization.
            table.BeginInit();

            // Enable table borders to see its boundaries.
            table.BorderWidth =1;
            
            // Create a table Header.
            XRTableRow row = new XRTableRow();
            if (dtPayRegister != null )
            {
                for (int col = 0; col < dtPayRegister.Columns.Count; col++)
                {
                    if (dtPayRegister.Columns[col].ColumnName.ToUpper() != dtCompMonth.PAYROLLIDColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtstaffgrp.GROUPNAMEColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtCompMonth.STAFFIDColumn.ColumnName &&
                        //dtPayRegister.Columns[col].ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtCompMonth.NAMEColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtCompMonth.DESIGNATIONColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtstaffgrp.STAFFORDERColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != "S.NO" &&
                        !dtPayRegister.Columns[col].ColumnName.ToUpper().Contains("DATE"))
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.Name = "tcl" + dtPayRegister.Columns[col].ColumnName + "";
                        cell.Text = dtPayRegister.Columns[col].ColumnName;
                        bool isNumberonly = false;
                        using (clsPayrollBase paybase = new clsPayrollBase())
                        {
                            isNumberonly = paybase.IsColumnContainsNubmerOnly(dtPayRegister, dtPayRegister.Columns[col].ColumnName);
                        }

                        if (dtPayRegister.Columns[col].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                        {
                            cell.WidthF = 150;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                        {
                            cell.WidthF = 140;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == "S.NO")
                        {
                            cell.Text = "#";
                            cell.WidthF = 50;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (!isNumberonly)
                        {
                            cell.WidthF = 100;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;;
                        }
                        else
                        {
                            cell.WidthF = 100;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 3;
                        }
                        row.Cells.Add(cell);
                    }
                }
            }
            table.Rows.Add(row);
            table.EndInit();
            return table;
        }

        private XRTable DetailTable()
        {
            //if (dtPayRegister != null && dtPayRegister.Rows.Count > 0)
            //{
            //    DataView dvData = dtPayRegister.AsDataView();
            //    dvData.Sort = "RECENT_STAFF_ORDER";
            //    dtPayRegister = dvData.ToTable();
            //}

            XRTable xrDetail = new XRTable();
            xrDetail.BorderColor = System.Drawing.Color.Gainsboro;
            xrDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left) //| DevExpress.XtraPrinting.BorderSide.Top
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            xrDetail.Font = new System.Drawing.Font("Tahoma", 8.5F);
            xrDetail.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            xrDetail.Name = "xrTblDetail";
            xrDetail.SizeF = new System.Drawing.SizeF(paperwidth, 30.41667F);
            xrDetail.StylePriority.UseBorderColor = false;
            xrDetail.StylePriority.UseBorders = false;
            xrDetail.StylePriority.UseFont = false;
            xrDetail.StylePriority.UseTextAlignment = false;
            xrDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            
            if (dtPayRegister != null && dtPayRegister.Rows.Count > 0)
            {
                XRTableRow Prow = new XRTableRow();
                for (int rcoll = 0; rcoll < dtPayRegister.Columns.Count; rcoll++)
                {
                    DataColumn dc = dtPayRegister.Columns[rcoll];
                    if (dc.ColumnName.ToUpper() != dtCompMonth.PAYROLLIDColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtstaffgrp.GROUPNAMEColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtCompMonth.STAFFIDColumn.ColumnName &&
                        //dc.ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtCompMonth.NAMEColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtCompMonth.DESIGNATIONColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtstaffgrp.STAFFORDERColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != "S.NO" &&
                        !dc.ColumnName.ToUpper().Contains("DATE"))
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.Name = "tcl" + rcoll + "";

                        bool isNumberonly = false;
                        using (clsPayrollBase paybase = new clsPayrollBase())
                        {
                            isNumberonly = paybase.IsColumnContainsNubmerOnly(dtPayRegister, dc.ColumnName);
                        }

                        if (dc.ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                        {
                            cell.WidthF = 150;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (dc.ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                        {
                            cell.WidthF = 140;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (dc.ColumnName.ToUpper() == "S.NO")
                        {
                            cell.WidthF = 50;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (!isNumberonly)
                        {
                            cell.WidthF = 100;
                            cell.DataBindings.Add(new XRBinding("Text", null, dc.ColumnName));
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;//3
                        }
                        else
                        {
                            cell.WidthF = 100;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 3;
                        }
                        if (dc.DataType == typeof(double))
                        {
                            cell.DataBindings.Add(new XRBinding("Text", null, dc.ColumnName));
                            //cell.DataBindings[0].FormatString = "{0:n}";
                            //cell.DataBindings[0].FormatString = "{0:#,#}";
                            cell.DataBindings[0].FormatString = "{0:#,0}";
                        }
                        else
                        {
                            cell.DataBindings.Add(new XRBinding("Text", null, dc.ColumnName));
                        }
                        Prow.Cells.Add(cell);
                    }
                }
                xrDetail.Rows.Add(Prow);
            }
            xrDetail.EndInit();
            xrDetail.AdjustSize();
            return xrDetail;
        }

        private XRTable FooterTable()
        {
            DataTable dtgrandtotal = new DataTable();

            XRTable table = new XRTable();
            //table.Size = new Size(1000,50);
            table.BackColor = System.Drawing.Color.Gainsboro;
            table.BorderColor = System.Drawing.Color.DarkGray;
            table.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            table.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            table.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            table.Name = "xrHeader";
            table.SizeF = new System.Drawing.SizeF(paperwidth, 25f);
            table.StylePriority.UseBackColor = false;
            table.StylePriority.UseBorderColor = false;
            table.StylePriority.UseBorders = false;
            table.StylePriority.UseFont = false;
            table.StylePriority.UseTextAlignment = false;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            table.AdjustSize();

            // Start table initialization.
            table.BeginInit();

            // Enable table borders to see its boundaries.
            table.BorderWidth = 1;

            XRTableRow row = new XRTableRow();
            if (dtPayRegister != null && dtPayRegister.Rows.Count > 0)
            {
                for (int idt = 0; idt < dtPayRegister.Columns.Count; idt++)
                {
                    DataColumn dc = dtPayRegister.Columns[idt];
                    if (dc.ColumnName.ToUpper() != dtCompMonth.PAYROLLIDColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtstaffgrp.GROUPNAMEColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtCompMonth.STAFFIDColumn.ColumnName &&
                        //dc.ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtCompMonth.NAMEColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtCompMonth.DESIGNATIONColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtstaffgrp.STAFFORDERColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != "S.NO" &&
                        !dc.ColumnName.ToUpper().Contains("DATE"))
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.Name = "tcl" + idt + "";
                        string textprint = dtPayRegister.Columns[idt].ColumnName.ToString();
                        if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                        {
                            cell.WidthF = 150;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                        {
                            cell.WidthF = 140;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == "S.NO")
                        {
                            cell.WidthF = 50;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else
                        {
                            cell.WidthF = 100;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 3;
                        }

                        if (dc.DataType == typeof(double))
                        {
                            cell.DataBindings.Add(new XRBinding("Text", null, dtPayRegister.Columns[idt].ColumnName));
                            //cell.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:n}");
                            //cell.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,#}");
                            cell.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0}");
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            row.Cells.Add(cell);
                        }
                        else
                        {
                            row.Cells.Add(cell);
                        }
                        row.Cells.Add(cell);
                    }
                }
            }
            table.Rows.Add(row);
            table.EndInit();
            return table;
        }

        #endregion

        private void xrStaffCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrStaffCode.Text = xrStaffName.Text = xrStaffDesingation.Text = xrStaffPAN.Text = xrAadharNo.Text = xrUAN.Text  = string.Empty;
            
            if (GetCurrentColumnValue(dtCompMonth.STAFFIDColumn.ColumnName) != null)
            {
                string staffid = GetCurrentColumnValue("STAFFID").ToString();
                dtStaffPersonalInfo.DefaultView.RowFilter = string.Empty;
                
                if (dtStaffPersonalInfo != null && dtStaffPersonalInfo.Rows.Count > 0)
                {
                    dtStaffPersonalInfo.DefaultView.RowFilter = dtCompMonth.STAFFIDColumn.ColumnName +  "=" + staffid;
                    if (dtStaffPersonalInfo.DefaultView.Count > 0)
                    {
                        xrStaffCode.Text = dtStaffPersonalInfo.DefaultView[0]["EMPNO"].ToString().Trim();
                        xrStaffName.Text = dtStaffPersonalInfo.DefaultView[0]["STAFFNAME"].ToString().Trim();
                        xrStaffDesingation.Text = dtStaffPersonalInfo.DefaultView[0]["DESIGNATION"].ToString().Trim();
                        xrStaffPAN.Text = dtStaffPersonalInfo.DefaultView[0]["PAN_NO"].ToString().Trim();
                        xrAadharNo.Text = dtStaffPersonalInfo.DefaultView[0]["AADHAR_NO"].ToString().Trim();
                        xrUAN.Text = dtStaffPersonalInfo.DefaultView[0]["UAN"].ToString().Trim();
                    }
                }
                dtStaffPersonalInfo.DefaultView.RowFilter = string.Empty;
            }
        }
    }
}
