using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using PAYROLL.Modules;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using Bosco.Utility.ConfigSetting;
using Payroll.DAO.Schema;

namespace Bosco.Report.ReportObject
{
    public partial class PAYROLLWAGES : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        DataTable dtPayWagesReport = new DataTable();
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();
        ApplicationSchema.PRSTAFFGROUPDataTable dtstaffgrp = new ApplicationSchema.PRSTAFFGROUPDataTable();
        float paperwidth = 1135;
        Int32 PayGroupwiseSNo = 0;
        #endregion

        private bool MoreThanPaygroupSelected
        {
            get
            {
                bool rtn = false;
                if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupId))
                {
                    int d = ReportProperty.Current.PayrollGroupId.Split(',').Length;
                    rtn = (ReportProperty.Current.PayrollGroupId.Split(',').Length > 1);
                }
                return rtn;
            }
        }

        #region Constructor
        public PAYROLLWAGES()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindWagesReport();
            base.ShowReport();
        }
        #endregion
        
        #region Methods

        public void BindWagesReport()
        {
            paperwidth = PageSize.Width - 35;
            this.SetLandscapeHeader = paperwidth;
            this.SetLandscapeFooter = paperwidth;
            this.SetLandscapeFooterDateWidth = 890.00f;

            if (this.ReportProperties.PayrollId.Trim() != string.Empty && this.ReportProperties.PayrollId != "0" &&
                this.ReportProperties.PayrollGroupId.Trim() != string.Empty && this.ReportProperties.PayrollGroupId != "0")
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
            
            setHeaderTitleAlignment();
            SetReportTitle();
            PayGroupwiseSNo = 1;
            this.InstituteName = ReportProperty.Current.PayrollProjectTitle;
            this.LegalEntityAddress = ReportProperty.Current.PayrollProjectAddress;
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + ReportProperty.Current.PayrollName;
            this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : ReportProperty.Current.PayrollGroupName;
            this.CosCenterName = (string.IsNullOrEmpty(ReportProperty.Current.PayrollDepartmentName) ? string.Empty : ReportProperty.Current.PayrollDepartmentName);
            this.HideDateRange = false;
            if (!String.IsNullOrEmpty(ReportProperty.Current.PayrollPayrollDate))
            {
                this.ReportTitle += " for the Month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.PayrollPayrollDate, false).ToString("MMMM yyyy");
            }

            this.DisplayName = this.ReportTitle;
            //replace special characters which are not valid for file names
            this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
            //--------------------------------------------------------------------------------------

            this.ReportProperties.ShowPageNumber = 1;

            this.PageHeader.Controls.Clear();
            this.Detail.Controls.Clear();
            this.GrpRptFooter.Controls.Clear();
            this.grpFooterPayGroup.Controls.Clear();
            this.ReportFooter.Visible = true;

            using (clsPayrollBase paybase = new clsPayrollBase())
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                resultArgs = paybase.GeneratePayRegister(ReportProperty.Current.PayrollGroupId, ReportProperty.Current.PayrollId);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = paperwidth;
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
                    dtPayWagesReport = resultArgs.DataSource.Table;

                    //if (dtPayWagesReport.Columns.Count > 15)
                    //{
                    //    this.PaperKind = System.Drawing.Printing.PaperKind.Legal;
                    //}

                    dtPayWagesReport.TableName = this.DataMember;
                    dtPayWagesReport.Columns.Add(new DataColumn("Signature", typeof(string)));
                    this.PageHeader.Controls.Add(HeaderTable());
                    if (ReportProperty.Current.PayrollGroupConsolidation == 0)
                    {
                        this.Detail.Controls.Add(DetailTable());
                    }
                    this.GrpRptFooter.Controls.Add(FooterTable());

                    //On 09/12/2022, To Show Paygroup-wise---------------------------------------------------- 
                    // Show Paygroup Consolidation 
                    this.HideReportSubTitle = true;
                    this.grpHeaderPayGroup.Visible = false;
                    this.Detail.Visible = true;
                    if (MoreThanPaygroupSelected || ReportProperty.Current.PayrollGroupConsolidation == 1)
                    {
                        this.grpHeaderPayGroup.Visible = ReportProperty.Current.PayrollGroupConsolidation == 1 ? false : true;
                        this.Detail.Visible = ReportProperty.Current.PayrollGroupConsolidation == 0 ? true : false;
                        this.grpFooterPayGroup.Controls.Add(FooterTable(true));
                        this.grpFooterPayGroup.HeightF = 20;// this.grpFooterGroup.HeightF + 1;
                        this.HideReportSubTitle = false;
                    }
                    //--------------------------------------------------------------------------------------------

                    (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = paperwidth;
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();


                    this.DataSource = dtPayWagesReport;
                    this.DataMember = dtPayWagesReport.TableName;   
                }
                else
                {
                    MessageRender.ShowMessage(resultArgs.Message);
                }
                SplashScreenManager.CloseForm();
            }
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
            table.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            table.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            table.Name = "xrHeader";
            table.SizeF = new System.Drawing.SizeF(paperwidth, 30f);
            table.StylePriority.UseBackColor = false;
            table.StylePriority.UseBorderColor = false;
            table.StylePriority.UseBorders = false;
            table.StylePriority.UseFont = false;
            table.StylePriority.UseTextAlignment = false;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            table.AdjustSize();

            xrtblPayGroup.WidthF = paperwidth;

            // Start table initialization.
            table.BeginInit();

            // Enable table borders to see its boundaries.
            table.BorderWidth = 1;
            
            // Create a table Header.
            XRTableRow row = new XRTableRow();
            for (int col = 0; col < dtPayWagesReport.Columns.Count; col++)
            {
                if (dtPayWagesReport.Columns[col].ColumnName.ToUpper() != dtCompMonth.PAYROLLIDColumn.ColumnName &&
                    dtPayWagesReport.Columns[col].ColumnName.ToUpper() != dtstaffgrp.GROUPNAMEColumn.ColumnName &&
                    dtPayWagesReport.Columns[col].ColumnName.ToUpper() != dtCompMonth.STAFFIDColumn.ColumnName &&
                    dtPayWagesReport.Columns[col].ColumnName.ToUpper() != dtstaffgrp.STAFFORDERColumn.ColumnName &&
                    dtPayWagesReport.Columns[col].ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName)
                {
                    XRTableCell cell = new XRTableCell();
                    cell.BeforePrint += new System.Drawing.Printing.PrintEventHandler(Headercell_BeforePrint);  
                    cell.Name = "tcl" + dtPayWagesReport.Columns[col].ColumnName + "";
                    cell.Text = dtPayWagesReport.Columns[col].ColumnName;

                    bool isNumberonly = false;
                    using (clsPayrollBase paybase = new clsPayrollBase())
                    {
                        isNumberonly = paybase.IsColumnContainsNubmerOnly(dtPayWagesReport, dtPayWagesReport.Columns[col].ColumnName);
                    }

                    if (dtPayWagesReport.Columns[col].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                    {
                        cell.WidthF = 150;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        cell.Padding = 3;
                    }
                    else if (dtPayWagesReport.Columns[col].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                    {
                        cell.WidthF = 140;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        cell.Padding = 3;
                    }
                    else if (dtPayWagesReport.Columns[col].ColumnName.ToUpper() == "S.NO")
                    {
                        cell.Text = "#";
                        cell.WidthF = 25; //60;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        cell.Padding = 3;
                    }
                    else if (!isNumberonly)
                    {
                        cell.WidthF = 100;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        cell.Padding = 3;// 3;
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
            table.Rows.Add(row);
            table.EndInit();

            this.xrtblPayGroup.WidthF = table.WidthF;
            this.xrtblPayGroup.HeightF = table.HeightF;
            grpHeaderPayGroup.HeightF = table.HeightF;
            return table;
        }

        private XRTable DetailTable()
        {
            XRTable xrDetail = new XRTable();
            xrDetail.BorderColor = System.Drawing.Color.Gainsboro;
            xrDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left)//| DevExpress.XtraPrinting.BorderSide.Top
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            xrDetail.Font = new System.Drawing.Font("Tahoma", 8.5F);
            xrDetail.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            xrDetail.Name = "xrDetail";
            xrDetail.SizeF = new System.Drawing.SizeF(paperwidth, 35f);
            xrDetail.StylePriority.UseBorderColor = false;
            xrDetail.StylePriority.UseBorders = false;
            xrDetail.StylePriority.UseFont = false;
            xrDetail.StylePriority.UseTextAlignment = false;
            xrDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            if (dtPayWagesReport != null && dtPayWagesReport.Rows.Count > 0)
            {
                XRTableRow Prow = new XRTableRow();
                for (int rcoll = 0; rcoll < dtPayWagesReport.Columns.Count; rcoll++)
                {
                    DataColumn dc = dtPayWagesReport.Columns[rcoll];
                    if (dc.ColumnName.ToUpper() != dtCompMonth.PAYROLLIDColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtstaffgrp.GROUPNAMEColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtCompMonth.STAFFIDColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtstaffgrp.STAFFORDERColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName)
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.EvaluateBinding += new BindingEventHandler(Detailcell_EvaluateBinding);
                        cell.Name = "tcl" + rcoll + "";
                        bool isNumberonly = false;
                        using (clsPayrollBase paybase = new clsPayrollBase())
                        {
                            isNumberonly = paybase.IsColumnContainsNubmerOnly(dtPayWagesReport, dtPayWagesReport.Columns[rcoll].ColumnName);
                        }
                        if (dtPayWagesReport.Columns[rcoll].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                        {
                            cell.WidthF = 150;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (dtPayWagesReport.Columns[rcoll].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                        {
                            cell.WidthF = 140;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else if (dtPayWagesReport.Columns[rcoll].ColumnName.ToUpper() == "S.NO")
                        {
                            cell.WidthF = 25; //60;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;

                            cell.EvaluateBinding += new BindingEventHandler(Snocell_EvaluateBinding);
                        }
                        else if (!isNumberonly)
                        {
                            cell.WidthF = 100;
                            cell.DataBindings.Add(new XRBinding("Text", null, dc.ColumnName));
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 3;
                        }
                        else
                        {
                            cell.WidthF = 100;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 3;
                        }
                        if (dtPayWagesReport.Columns[rcoll].DataType == typeof(double))
                        {
                            cell.DataBindings.Add(new XRBinding("Text", null, dc.ColumnName));
                            //cell.DataBindings[0].FormatString = "{0:n}";
                            //cell.DataBindings[0].FormatString = "{0:#,#}";
                            cell.DataBindings[0].FormatString = "{0:#,0}";
                        }
                        else if (dc.ColumnName.ToUpper().Contains("DATE")) //Temp 01/02/2023, To format date time
                        {
                            cell.DataBindings.Add(new XRBinding("Text", null, dc.ColumnName));
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            //cell.DataBindings[0].FormatString = "{0:dd/MM/yyyy}";
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

        private XRTable FooterTable(bool isGroupFooter = false)
        {
            DataTable dtgrandtotal = new DataTable();
            
            XRTable table = new XRTable();
            //table.Size = new Size(1000,50);
            table.BackColor = System.Drawing.Color.Gainsboro;
            table.BorderColor = System.Drawing.Color.DarkGray;

            if (isGroupFooter)
            {
                table.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left)
                            | DevExpress.XtraPrinting.BorderSide.Right)
                            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            }
            else
            {
                table.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                            | DevExpress.XtraPrinting.BorderSide.Right)
                            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            }

            table.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            table.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            table.Name = "xrTblFooter";
            table.SizeF = new System.Drawing.SizeF(paperwidth, 30f);
            table.TopF = 0;
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
            for (int idt = 0; idt < dtPayWagesReport.Columns.Count; idt++)
            {
                if (dtPayWagesReport.Columns[idt].ColumnName.ToUpper() != dtCompMonth.PAYROLLIDColumn.ColumnName &&
                      dtPayWagesReport.Columns[idt].ColumnName.ToUpper() != dtstaffgrp.GROUPNAMEColumn.ColumnName &&
                      dtPayWagesReport.Columns[idt].ColumnName.ToUpper() != dtCompMonth.STAFFIDColumn.ColumnName &&
                      dtPayWagesReport.Columns[idt].ColumnName.ToUpper() != dtstaffgrp.STAFFORDERColumn.ColumnName &&
                      dtPayWagesReport.Columns[idt].ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName)
                {
                    XRTableCell cell = new XRTableCell();
                    cell.BeforePrint += new System.Drawing.Printing.PrintEventHandler(Headercell_BeforePrint);
                    cell.Name = "tcl" + idt + "";
                    cell.Tag = dtPayWagesReport.Columns[idt].ColumnName;

                    bool isNumberonly = false;
                    using (clsPayrollBase paybase = new clsPayrollBase())
                    {
                        isNumberonly = paybase.IsColumnContainsNubmerOnly(dtPayWagesReport, dtPayWagesReport.Columns[idt].ColumnName);
                    }

                    if (dtPayWagesReport.Columns[idt].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                    {                     
                        cell.WidthF = 150;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        cell.Padding = 3;

                        if (ReportProperty.Current.PayrollGroupConsolidation == 1 && isGroupFooter)
                        {
                            cell.DataBindings.Add(new XRBinding("Text", null, dtPayWagesReport.Columns[dtstaffgrp.GROUPNAMEColumn.ColumnName].ColumnName));
                        }
                        else
                        {
                            cell.Text = (isGroupFooter || !MoreThanPaygroupSelected) ? "Total" : "Grand Total";
                        }
                    }
                    else if (dtPayWagesReport.Columns[idt].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                    {                     
                        cell.WidthF = 140;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;                        
                        cell.Padding = 3;
                    }
                    else if (dtPayWagesReport.Columns[idt].ColumnName.ToUpper() == "S.NO")
                    {
                        cell.WidthF = 25; // 60;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        cell.Padding = 3;
                    }
                    else if (!isNumberonly)
                    {
                        cell.WidthF = 100;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        cell.Padding = 3;// 3;
                    }
                    else
                    {                        
                        cell.WidthF = 100;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        cell.Padding = 3;
                    }

                    if (dtPayWagesReport.Columns[idt].DataType == typeof(double))
                    {
                        cell.DataBindings.Add(new XRBinding("Text", null, dtPayWagesReport.Columns[idt].ColumnName));
                        //cell.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n}");
                        //cell.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,#}");
                        if (isGroupFooter)
                        {
                            cell.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.Sum, "{0:#,0}");
                        }
                        else
                        {
                            cell.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0}");
                        }
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        row.Cells.Add(cell);
                    }
                    else
                    {
                        row.Cells.Add(cell);
                    }
                }
            }
            table.Rows.Add(row);
            table.EndInit();
            if (!isGroupFooter)
            {
                table.TopF = 5;
            }
            return table;
        }

        private void Snocell_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = PayGroupwiseSNo;
            XRTableCell cell = sender as XRTableCell;
            cell.WidthF = 25;
            PayGroupwiseSNo++;
        }

        private void grpHeaderPayGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            PayGroupwiseSNo = 1;
        }

        private void Headercell_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null)
            {
                XRTableCell cell = sender as XRTableCell;
                string fieldname = cell.Tag != null ? cell.Tag.ToString().ToUpper() : string.Empty;
                
                fieldname = (!string.IsNullOrEmpty(fieldname) ? fieldname : cell.Text.ToUpper());
                if (fieldname == "#" || fieldname == "S.NO")
                    cell.WidthF = 25;
                else if (fieldname.Contains("DATE"))
                    cell.WidthF = 68;
                //else if (fieldname == "BASIC")
                //    cell.WidthF = 50;
                //else if (fieldname.Contains("SIGNATURE"))
                //    cell.WidthF = 60;
            }
        }

        private void Detailcell_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null && e.Value != null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (cell.DataBindings.Count > 0 && e.Binding.DataMember != null)
                {
                    if (e.Binding.DataMember.ToUpper().Contains("DATE"))
                    {
                        cell.WidthF = 68;
                        if (!string.IsNullOrEmpty(e.Value.ToString()))
                        {
                            e.Value = UtilityMember.DateSet.ToDate(e.Value.ToString(), false).ToShortDateString();
                        }
                    }
                    //else if (e.Binding.DataMember.ToUpper() == "BASIC")
                    //{
                    //    cell.WidthF = 50;
                    //}
                    else if (e.Binding.DataMember.ToUpper().Contains("SIGNATURE"))
                    {
                        //cell.WidthF = 60;
                    }
                }
            }
        }

        private void PAYROLLWAGES_AfterPrint(object sender, EventArgs e)
        {
            //On 02/03/2023, To reset serial nubmer
            if (this.DataSource != null)
            {
                //if (this.CurrentRowIndex == this.RowCount - 1)
                PayGroupwiseSNo = 1;
            }
        }
        #endregion

        

    }
}
