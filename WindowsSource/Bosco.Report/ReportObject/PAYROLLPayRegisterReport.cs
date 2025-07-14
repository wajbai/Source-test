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
    public partial class PAYROLLPayRegisterReport : ReportHeaderBase
    {
        #region Declaration
        DataTable dtPayRegister = new DataTable();
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
        #region Constructors

        public PAYROLLPayRegisterReport()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Report
        public override void ShowReport()
        {
            BindPayRegisterReport();
            base.ShowReport();
        }
        #endregion

        #region Methods

        public void BindPayRegisterReport()
        {
            paperwidth = PageSize.Width-40;

            this.SetLandscapeHeader = paperwidth; //1165.50f;
            this.SetLandscapeFooter = paperwidth; // 1165.50f;
            this.SetLandscapeFooterDateWidth = 900.00f;
            if ( (this.ReportProperties.PayrollId !=string.Empty && this.ReportProperties.PayrollId != "0" &&
                 (this.ReportProperties.PayrollGroupId !=string.Empty  && this.ReportProperties.PayrollGroupId != "0")))
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
            PayGroupwiseSNo = 1;
            //this.ReportTitle = ReportProperty.Current.PayrollProjectTitle;
            this.InstituteName = ReportProperty.Current.PayrollProjectTitle;
            this.LegalEntityAddress = ReportProperty.Current.PayrollProjectAddress;
            this.ReportPeriod = "";//MessageCatalog.ReportCommonTitle.PERIOD + " " + ReportProperty.Current.PayrollName;
            this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : (string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupName) ? string.Empty : ReportProperty.Current.PayrollGroupName);
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

            setHeaderTitleAlignment();

            this.PageHeader.Controls.Clear();
            this.Detail.Controls.Clear();
            this.grpReportFooter.Controls.Clear();
            this.grpFooterPayGroup.Controls.Clear();
            using (clsPayrollBase paybase = new clsPayrollBase())
            {
                ResultArgs resultArgs = paybase.GeneratePayRegister(ReportProperty.Current.PayrollGroupId, ReportProperty.Current.PayrollId,
                           string.Empty, string.Empty, string.Empty, ReportProperty.Current.PayrollDepartmentId);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    dtPayRegister = resultArgs.DataSource.Table;
                    dtPayRegister.TableName = this.DataMember;

                    this.PageHeader.Controls.Add(HeaderTable());
                    if (ReportProperty.Current.PayrollGroupConsolidation == 0)
                    {
                        this.Detail.Controls.Add(DetailTable());
                    }
                    this.grpReportFooter.Controls.Add(FooterTable());

                    //On 09/12/2022, To Show Paygroup-wise---------------------------------------------------- 
                    // Show Paygroup Consolidation 
                    this.HideReportSubTitle = true;
                    this.grpHeaderPayGroup.Visible = false;
                    this.Detail.Visible = true;
                    if (MoreThanPaygroupSelected || ReportProperty.Current.PayrollGroupConsolidation == 1)
                    {
                        this.grpHeaderPayGroup.Visible = ReportProperty.Current.PayrollGroupConsolidation == 1? false:true;
                        this.Detail.Visible = ReportProperty.Current.PayrollGroupConsolidation == 0 ? true : false;
                        this.grpFooterPayGroup.Controls.Add(FooterTable(true));
                        this.grpFooterPayGroup.HeightF = 20;// this.grpFooterGroup.HeightF + 1;
                        this.HideReportSubTitle = false;
                    }
                    //--------------------------------------------------------------------------------------------
                    
                    (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = paperwidth;
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();

                    this.DataSource = dtPayRegister;
                    this.DataMember = dtPayRegister.TableName;  
                }
                else
                {
                    MessageRender.ShowMessage("Not able to generate report : " + resultArgs.Message);
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
            table.Font = new System.Drawing.Font("Tahoma", 7, System.Drawing.FontStyle.Bold);
            table.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            table.Name = "xrHeader";
            //table.SizeF = new System.Drawing.SizeF(1165.50f, 30f);
            table.SizeF = new System.Drawing.SizeF(paperwidth, 30f);
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
            
            // Create a table Header.
            XRTableRow row = new XRTableRow();
            if (dtPayRegister != null )
            {
                for (int col = 0; col < dtPayRegister.Columns.Count; col++)
                {
                    if (dtPayRegister.Columns[col].ColumnName.ToUpper() != dtCompMonth.PAYROLLIDColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtCompMonth.STAFFIDColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtstaffgrp.STAFFORDERColumn.ColumnName &&
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtstaffgrp.GROUPNAMEColumn.ColumnName)
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.Name = "tcl" + dtPayRegister.Columns[col].ColumnName + "";
                        cell.Text = dtPayRegister.Columns[col].ColumnName;
                        cell.BeforePrint += new System.Drawing.Printing.PrintEventHandler(Headercell_BeforePrint);
                        bool isNumberonly = true;
                        using (clsPayrollBase paybase = new clsPayrollBase())
                        {
                            isNumberonly = paybase.IsColumnContainsNubmerOnly(dtPayRegister, dtPayRegister.Columns[col].ColumnName);
                        }
                        if (dtPayRegister.Columns[col].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                        {
                            //this.PaperKind == System.Drawing.Printing.PaperKind.Legal;
                            cell.WidthF = 175;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                        {
                            cell.WidthF = 145;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == "S.NO")
                        {
                            cell.Text = "#";
                            cell.WidthF = 25; // 60;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == "DA" || dtPayRegister.Columns[col].ColumnName.ToUpper() == "HRA" ||
                                dtPayRegister.Columns[col].ColumnName.ToUpper() == "PF" || dtPayRegister.Columns[col].ColumnName.ToUpper() == "PT")
                        {
                            //cell.WidthF = 182.71f;
                            cell.WidthF = 120;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 2;// 3;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == "DEDUCTIONS")
                        {
                            //cell.WidthF = 182.71f;
                            cell.WidthF = 140;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 2;// 3;
                        }
                        else if (!isNumberonly)
                        {
                            cell.WidthF = 100;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;// 3;
                        }
                        else
                        {
                            //cell.WidthF = 182.71f;
                            cell.WidthF = 130;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 2;// 3;
                        }
                        row.Cells.Add(cell);
                    }
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
            if (dtPayRegister != null && dtPayRegister.Rows.Count > 0)
            {
                DataView dvData = dtPayRegister.AsDataView();
                dvData.Sort = "S.No";
                dtPayRegister = dvData.ToTable();
            }

            XRTable xrDetail = new XRTable();
            xrDetail.BorderColor = System.Drawing.Color.Gainsboro;
            xrDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left) //| DevExpress.XtraPrinting.BorderSide.Top
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            xrDetail.Font = new System.Drawing.Font("Tahoma", 8F);
            xrDetail.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            xrDetail.Name = "xrDetail";
            //xrDetail.SizeF = new System.Drawing.SizeF(1165.50f, 35.41667F);
            xrDetail.SizeF = new System.Drawing.SizeF(paperwidth, 35.41667F);
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
                        dc.ColumnName.ToUpper() != dtCompMonth.STAFFIDColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtstaffgrp.STAFFORDERColumn.ColumnName &&
                        dc.ColumnName.ToUpper() != dtstaffgrp.GROUPNAMEColumn.ColumnName)
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.Name = "tcl" + rcoll + "";
                        
                        cell.EvaluateBinding += new BindingEventHandler(Detailcell_EvaluateBinding);
                        bool isNumberonly = false;
                        using (clsPayrollBase paybase = new clsPayrollBase())
                        {
                            isNumberonly = paybase.IsColumnContainsNubmerOnly(dtPayRegister, dtPayRegister.Columns[rcoll].ColumnName);
                        }
                        if (dtPayRegister.Columns[rcoll].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                        {
                            cell.WidthF = 175;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                        }
                        else if (dtPayRegister.Columns[rcoll].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                        {
                            cell.WidthF = 145;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                        }
                        else if (dtPayRegister.Columns[rcoll].ColumnName.ToUpper() == "S.NO")
                        {
                            cell.WidthF = 25; // 60;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                            cell.EvaluateBinding += new BindingEventHandler(Snocell_EvaluateBinding);
                        }
                        else if (dtPayRegister.Columns[rcoll].ColumnName.ToUpper() == "DA" || dtPayRegister.Columns[rcoll].ColumnName.ToUpper() == "HRA" ||
                            dtPayRegister.Columns[rcoll].ColumnName.ToUpper() == "PF" || dtPayRegister.Columns[rcoll].ColumnName.ToUpper() == "PT")
                        {
                            cell.WidthF = 120;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 2;//3
                        }
                        else if (dtPayRegister.Columns[rcoll].ColumnName.ToUpper() == "DEDUCTIONS")
                        {
                            cell.WidthF = 140;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 2;//3
                        }
                        else if (!isNumberonly)
                        {
                            cell.WidthF = 100;
                            cell.DataBindings.Add(new XRBinding("Text", null, dc.ColumnName));
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;//3
                        }
                        else
                        {
                            cell.WidthF = 130;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 2;//3
                        }

                        if (dtPayRegister.Columns[rcoll].DataType == typeof(double))
                        {
                            cell.DataBindings.Add(new XRBinding("Text", null, dc.ColumnName));
                            
                            //On 11/01/2021, to hide decimal part -------------------------------------
                            //cell.DataBindings[0].FormatString = "{0:n}";
                            //cell.DataBindings[0].FormatString = "{0:#,#}";
                            cell.DataBindings[0].FormatString = "{0:#,0}";
                            //-----------------------------------------------------------------------
                        }
                        else if (dc.ColumnName.ToUpper().Contains("DATE")) //Temp 01/02/2023, To format date time
                        {
                            cell.DataBindings.Add(new XRBinding("Text", null, dc.ColumnName));
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        }
                        else
                        {
                            //cell.Text = dtPayRegister.Rows[rrpow][dtPayRegister.Columns[rcoll].ColumnName].ToString();
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

        private void Headercell_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null)
            {
                XRTableCell cell = sender as XRTableCell;
                string fieldname = cell.Tag!=null? cell.Tag.ToString().ToUpper() : string.Empty;
                if (!string.IsNullOrEmpty(fieldname))
                {

                }

                fieldname = ( !string.IsNullOrEmpty(fieldname) ?  fieldname :  cell.Text.ToUpper());
                if (fieldname == "#" || fieldname == "S.NO")
                    cell.WidthF = 25;
                else if (fieldname.Contains("DATE"))
                    cell.WidthF = 65;
            }
        }

        private void Detailcell_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null && e.Value!=null)
            {
                XRTableCell cell = sender as XRTableCell;
                if (cell.DataBindings.Count > 0 && e.Binding.DataMember!=null)
                {
                    if (e.Binding.DataMember.ToUpper().Contains("DATE"))
                    {
                        cell.WidthF = 65;
                        if (!string.IsNullOrEmpty(e.Value.ToString()))
                        {
                            e.Value = UtilityMember.DateSet.ToDate(e.Value.ToString(), false).ToShortDateString();
                        }
                    }
                }
            }
        }

        private void Snocell_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = PayGroupwiseSNo;
            PayGroupwiseSNo++;
            XRTableCell cell = sender as XRTableCell;
            cell.WidthF = 25;
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
                table.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left )
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
            table.Name = "xrHeader";
            //table.SizeF = new System.Drawing.SizeF(1165.50f, 25f);
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
                    if (dtPayRegister.Columns[idt].ColumnName.ToUpper() != dtCompMonth.PAYROLLIDColumn.ColumnName &&
                        dtPayRegister.Columns[idt].ColumnName.ToUpper() != dtCompMonth.STAFFIDColumn.ColumnName &&
                        dtPayRegister.Columns[idt].ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName &&
                        dtPayRegister.Columns[idt].ColumnName.ToUpper() != dtstaffgrp.STAFFORDERColumn.ColumnName &&
                        dtPayRegister.Columns[idt].ColumnName.ToUpper() != dtstaffgrp.GROUPNAMEColumn.ColumnName)
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.BeforePrint += new System.Drawing.Printing.PrintEventHandler(Headercell_BeforePrint);  
                        cell.Name = "tcl" + idt + "";
                        cell.Tag = dtPayRegister.Columns[idt].ColumnName;

                        bool isNumberonly = true;
                        using (clsPayrollBase paybase = new clsPayrollBase())
                        {
                            isNumberonly = paybase.IsColumnContainsNubmerOnly(dtPayRegister, dtPayRegister.Columns[idt].ColumnName);
                        }
                        
                        if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                        {
                            cell.WidthF =  175;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;

                            if (ReportProperty.Current.PayrollGroupConsolidation == 1 && isGroupFooter)
                            {
                                cell.DataBindings.Add(new XRBinding("Text", null, dtPayRegister.Columns[dtstaffgrp.GROUPNAMEColumn.ColumnName].ColumnName));
                            }
                            else
                            {
                                cell.Text = (isGroupFooter || !MoreThanPaygroupSelected) ? "Total" : "Grand Total";
                            }
                        }
                        else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                        {
                            cell.WidthF = 145;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                        }
                        else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == "S.NO")
                        {
                            cell.WidthF = 25;//60;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                        }
                        else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == "DA" || dtPayRegister.Columns[idt].ColumnName.ToUpper() == "HRA" ||
                            dtPayRegister.Columns[idt].ColumnName.ToUpper() == "PF" || dtPayRegister.Columns[idt].ColumnName.ToUpper() == "PT")
                        {
                            cell.WidthF = 120;// 130;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 2;//3;
                        }
                        else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == "DEDUCTIONS")
                        {
                            cell.WidthF = 140;// 130;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 2;//3;
                        }
                        else if (!isNumberonly)
                        {
                            cell.WidthF = 100;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;// 3;
                        }
                        else
                        {
                            cell.WidthF = 130;// 130;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            cell.Padding = 2;//3;
                        }
                        string textprint = dtPayRegister.Columns[idt].ColumnName.ToString();

                        if (dtPayRegister.Columns[idt].DataType == typeof(double))
                        {
                            //dtPayRegister.Columns.Add("Temp", typeof(Double));
                            //dtPayRegister.Columns["Temp"].Expression = "IIF([" + textprint + "]=0,0,[" + textprint + "])";
                            //double grandtotal = ReportProperty.Current.NumberSet.ToDouble(dtPayRegister.Compute("SUM(Temp)", "").ToString());
                            //cell.Text = ReportProperty.Current.NumberSet.ToNumber(grandtotal);
                            //dtPayRegister.Columns.Remove("Temp");

                            if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == "DA" || dtPayRegister.Columns[idt].ColumnName.ToUpper() == "HRA" ||
                                dtPayRegister.Columns[idt].ColumnName.ToUpper() == "PF" || dtPayRegister.Columns[idt].ColumnName.ToUpper() == "PT")
                            {
                                cell.WidthF = 120;
                            }
                            else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == "DEDUCTIONS")
                            {
                                cell.WidthF = 140;
                            }
                            else
                            {
                                cell.WidthF = 130;
                            }
                            cell.DataBindings.Add(new XRBinding("Text", null, dtPayRegister.Columns[idt].ColumnName));
                            //cell.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:n}");
                            //cell.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,#");
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
            }
            table.Rows.Add(row);
            table.EndInit();
            if (!isGroupFooter)
            {
                table.TopF = 5;
            }
            return table;
        }

        private void grpHeaderGroup_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            PayGroupwiseSNo = 1;
        }

        private void PAYROLLPayRegisterReport_AfterPrint(object sender, EventArgs e)
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
