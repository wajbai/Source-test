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
    public partial class PayrollEPFRegister: ReportHeaderBase
    {
        #region Declaration
        DataTable dtPayRegister = new DataTable();
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();

        float paperwidth = 1135;
        #endregion

        #region Constructors

        public PayrollEPFRegister()
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
            this.SetLandscapeHeader = paperwidth; //1165.50f;
            this.SetLandscapeFooter = paperwidth; // 1165.50f;
            this.SetLandscapeFooterDateWidth = 900.00f;
            
            //bool  = (this.ReportProperties.PayrollComponentId2 != string.Empty && this.ReportProperties.PayrollComponentId2 != "0");
            if ( (this.ReportProperties.PayrollId !=string.Empty && this.ReportProperties.PayrollId != "0") &&
                 (this.ReportProperties.PayrollGroupId !=string.Empty  && this.ReportProperties.PayrollGroupId != "0") &&
                 (this.ReportProperties.PayrollComponentId != string.Empty && this.ReportProperties.PayrollComponentId != "0") && 
                 (this.ReportProperties.PayrollComponentId1 != string.Empty && this.ReportProperties.PayrollComponentId1 != "0") )
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

            //this.ReportTitle = ReportProperty.Current.PayrollProjectTitle;
            this.InstituteName = ReportProperty.Current.PayrollProjectTitle;
            this.LegalEntityAddress = ReportProperty.Current.PayrollProjectAddress;
            this.ReportPeriod = "";//MessageCatalog.ReportCommonTitle.PERIOD + " " + ReportProperty.Current.PayrollName;
            this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : (string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupName) ? string.Empty : ReportProperty.Current.PayrollGroupName);
            this.HideDateRange = false;
            if (!String.IsNullOrEmpty(ReportProperty.Current.PayrollPayrollDate))
            {
                this.ReportTitle = "Staff EPF Register for the Month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.PayrollPayrollDate, false).ToString("MMMM yyyy");
            }
            this.DisplayName = "Staff EPF Register for the Month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.PayrollPayrollDate, false).ToString("MMMM yyyy");
            //replace special characters which are not valid for file names
            this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
            //--------------------------------------------------------------------------------------

            this.ReportProperties.ShowPageNumber = 1;

            setHeaderTitleAlignment();

            this.PageHeader.Controls.Clear();
            this.Detail.Controls.Clear();
            this.grpReportFooter.Controls.Clear();
            using (clsPayrollBase paybase = new clsPayrollBase())
            {
                //dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn.ColumnName, ReportProperty.Current.PayrollComponentId);
                ResultArgs resultArgs = paybase.GenerateStaffEPFFormat2(ReportProperty.Current.PayrollGroupId, ReportProperty.Current.PayrollId,
                    ReportProperty.Current.PayrollComponentId, ReportProperty.Current.PayrollComponentId1, ReportProperty.Current.PayrollComponentId2);
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    dtPayRegister = resultArgs.DataSource.Table;
                    dtPayRegister.TableName = this.DataMember;
                    this.PageHeader.Controls.Add(HeaderTable());
                    
                    
                    this.Detail.Controls.Add(DetailTable());
                    this.grpReportFooter.Controls.Add(FooterTable());
                    (xrSubSignFooter.ReportSource as SignReportFooter).SignWidth = paperwidth;
                    (xrSubSignFooter.ReportSource as SignReportFooter).ShowSignDetails();
                    
                    this.DataSource = dtPayRegister;
                    this.DataMember = dtPayRegister.TableName;  
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
            float ColEmployeeShareWidth = 0;
            float ColEmployerShareWidth = 0;
            float ColDummyWidth = 0;
            string[] EmployeeShareColumns = ReportProperties.PayrollComponentName1.Split(',');
            string[] EmployerShareColumns = ReportProperties.PayrollComponentName2.Split(',');

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
                        dtPayRegister.Columns[col].ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName)
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.Padding = 2;
                        cell.Name = "tcl" + dtPayRegister.Columns[col].ColumnName + "";
                        cell.Text = dtPayRegister.Columns[col].ColumnName;
                        if (dtPayRegister.Columns[col].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                        {
                            cell.WidthF = 175;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                        {
                            cell.WidthF = 145;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == "S.NO")
                        {
                            cell.Text = "#";
                            cell.WidthF = 50;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == "DA" || dtPayRegister.Columns[col].ColumnName.ToUpper() == "HRA" ||
                                dtPayRegister.Columns[col].ColumnName.ToUpper() == "PF" || dtPayRegister.Columns[col].ColumnName.ToUpper() == "PT")
                        {
                            //cell.WidthF = 182.71f;
                            cell.WidthF = 120;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == "DEDUCTIONS")
                        {
                            //cell.WidthF = 182.71f;
                            cell.WidthF = 140;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        }
                        else if (dtPayRegister.Columns[col].ColumnName.ToUpper() == "UAN NUMBER")
                        {
                            cell.WidthF = 130;// 130;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;//3;
                        }
                        else
                        {
                            //cell.WidthF = 182.71f;
                            cell.WidthF = 130;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        }
                        row.Cells.Add(cell);
                        
                        //Employee Share column width
                        if (Array.IndexOf(EmployeeShareColumns, cell.Text) >= 0 || cell.Text.ToUpper() == "TOTAL EMPLOYEE")
                        {
                            ColEmployeeShareWidth += cell.WidthF;
                        }
                        //Employee Share column width
                        else if (Array.IndexOf(EmployerShareColumns, cell.Text) >= 0 || cell.Text.ToUpper() == "TOTAL EMPLOYER")
                        {
                            ColEmployerShareWidth += cell.WidthF;
                        }
                        else
                        {
                            ColDummyWidth += cell.WidthF;
                        }
                    }
                }
            }
            table.Rows.Add(row);
            //float dummyWidth = table.WidthF - (ColEmployerShareWidth);
            XRTableRow GrpHeaderrow = new XRTableRow();
            XRTableCell cellDummary = new XRTableCell();
            cellDummary.Text = string.Empty;
            cellDummary.Padding = 2;
            //cellDummary.WidthF = table.WidthF;
            GrpHeaderrow.Cells.Add(cellDummary);
                       
            XRTableCell cellEmployee = new XRTableCell();
            cellEmployee.Text = "Employee Share";
            cellEmployee.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            cellEmployee.Padding = 2;
            //cellEmployee.WidthF = ColEmployeeShareWidth;
            GrpHeaderrow.Cells.Add(cellEmployee);

            XRTableCell cellEmployer = new XRTableCell();
            cellEmployer.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            cellEmployer.Text = "Employer Share";
            cellEmployer.Padding = 2;
            //cellEmployer.WidthF = ColEmployerShareWidth;
            GrpHeaderrow.Cells.Add(cellEmployer);
                        
            GrpHeaderrow.Cells[2].WidthF = ColEmployerShareWidth+2;
            GrpHeaderrow.Cells[1].WidthF = ColEmployeeShareWidth+2;
            GrpHeaderrow.Cells[0].WidthF = ColDummyWidth+5;

            table.EndInit();

            XRTable tblGrp = new XRTable();
            tblGrp.BackColor = System.Drawing.Color.Gainsboro;
            tblGrp.BorderColor = System.Drawing.Color.DarkGray;
            tblGrp.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            tblGrp.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
            tblGrp.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            tblGrp.Name = "xrHeader";
            //table.SizeF = new System.Drawing.SizeF(1165.50f, 30f);
            tblGrp.SizeF = new System.Drawing.SizeF(paperwidth, 30f);
            tblGrp.StylePriority.UseBackColor = false;
            tblGrp.StylePriority.UseBorderColor = false;
            tblGrp.StylePriority.UseBorders = false;
            tblGrp.StylePriority.UseFont = false;
            tblGrp.StylePriority.UseTextAlignment = false;

            tblGrp.Rows.Add(GrpHeaderrow);
            this.PageHeader.Controls.Add(tblGrp);
            table.TopF = tblGrp.TopF + tblGrp.HeightF;
            
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
                        dc.ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName)
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.Name = "tcl" + rcoll + "";
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
                            cell.WidthF = 50;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
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
                        else if (dtPayRegister.Columns[rcoll].ColumnName.ToUpper() == "UAN NUMBER")
                        {
                            cell.WidthF = 130;// 130;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;//3;
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
                        dtPayRegister.Columns[idt].ColumnName.ToUpper() != dtCompMonth.PRNAMEColumn.ColumnName)
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.Name = "tcl" + idt + "";
                        if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == dtCompMonth.NAMEColumn.ColumnName)
                        {
                            cell.WidthF =  175;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                        }
                        else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == dtCompMonth.DESIGNATIONColumn.ColumnName)
                        {
                            cell.WidthF = 145;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;
                        }
                        else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == "S.NO")
                        {
                            cell.WidthF = 50;
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
                        else if (dtPayRegister.Columns[idt].ColumnName.ToUpper() == "UAN NUMBER")
                        {
                            cell.WidthF = 130;// 130;
                            cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            cell.Padding = 2;//3;
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
                            cell.Summary = new XRSummary(SummaryRunning.Report, SummaryFunc.Sum, "{0:#,0}");
                            
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
            return table;
        }

        #endregion

    }
}
