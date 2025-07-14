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

namespace Bosco.Report.ReportObject
{
    public partial class PayrollCustomizeReport : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        clsPayrollBase PayrollBase = new clsPayrollBase();
        #endregion

        #region Constructor
        public PayrollCustomizeReport()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindCustomizeReport();
            base.ShowReport();
        }
        #endregion

        #region Methods

        private DataView GetReportSource()
        {
            DataView dvCustomize = new DataView();
            try
            {

                dvCustomize = PayrollBase.AbstractCustomizeReport();



            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
            finally { }
            return dvCustomize;
        }

        public void BindCustomizeReport()
        {
            //this.SetLandscapeHeader = 1062.25f;
            //this.SetLandscapeFooter = 1062.25f;
            //this.SetLandscapeFooterDateWidth = 890.00f;

            this.SetLandscapeHeader = 1165.50f;
            this.SetLandscapeFooter = 1165.50f;
            this.SetLandscapeFooterDateWidth = 890.00f;
            if (this.ReportProperties.PayrollId.Trim() != string.Empty && this.ReportProperties.PayrollId != "0")
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        setHeaderTitleAlignment();
                        SetReportTitle();

                        this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + ReportProperty.Current.PayrollName;
                        this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : ReportProperty.Current.PayrollGroupName;

                        this.PageHeader.Controls.Clear();
                        this.Detail.Controls.Clear();
                        this.ReportFooter.Controls.Clear();

                        this.PageHeader.Controls.Add(HeaderTable());
                        this.Detail.Controls.Add(DetailTable());
                        this.ReportFooter.Controls.Add(FooterTable());

                        SplashScreenManager.CloseForm();
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
                    setHeaderTitleAlignment();
                    SetReportTitle();

                    this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + ReportProperty.Current.PayrollName;
                    this.ReportSubTitle = SettingProperty.PayrollFinanceEnabled ? ReportProperty.Current.ProjectTitle : ReportProperty.Current.PayrollGroupName;

                    this.PageHeader.Controls.Clear();
                    this.Detail.Controls.Clear();
                    this.ReportFooter.Controls.Clear();

                    this.PageHeader.Controls.Add(HeaderTable());
                    this.Detail.Controls.Add(DetailTable());
                    this.ReportFooter.Controls.Add(FooterTable());
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

        public XRTable HeaderTable()
        {
            DataView dvdata = GetReportSource();
            DataTable dtData = dvdata.ToTable();
            XRTable table = new XRTable();
            //table.Size = new Size(1000,50);
            table.BackColor = System.Drawing.Color.Gainsboro;
            table.BorderColor = System.Drawing.Color.DarkGray;
            table.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            table.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            table.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            table.Name = "xrHeader";
            //table.SizeF = new System.Drawing.SizeF(1063F, 30f);
            table.SizeF = new System.Drawing.SizeF(1165.50f, 30f);
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
            for (int col = 0; col < dtData.Columns.Count; col++)
            {
                XRTableCell cell = new XRTableCell();
                cell.Name = "tcl" + dtData.Columns[col].ColumnName + "";
                cell.Text = dtData.Columns[col].ColumnName;
                if (dtData.Columns[col].ColumnName == "NAME")
                {
                    //cell.WidthF = dtData.Columns[col].ColumnName.Length + 50;
                    cell.WidthF = dtData.Columns[col].ColumnName.Length + 130;
                    cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    cell.Padding = 3;
                }
                else if (dtData.Columns[col].ColumnName == "DESIGNATION")
                {
                    //cell.WidthF = dtData.Columns[col].ColumnName.Length + 38;
                    cell.WidthF = dtData.Columns[col].ColumnName.Length + 180;
                    cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    cell.Padding = 3;
                }
                else
                {
                    //cell.WidthF = dtData.Columns[col].ColumnName.Length + 35;
                    cell.WidthF = dtData.Columns[col].ColumnName.Length + 130;
                    cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                    cell.Padding = 3;
                }
                row.Cells.Add(cell);
            }
            table.Rows.Add(row);
            table.EndInit();
            return table;
        }

        public XRTable DetailTable()
        {
            DataView dvdata = GetReportSource();
            DataTable dtData = dvdata.ToTable();
            XRTable xrDetail = new XRTable();
            xrDetail.BorderColor = System.Drawing.Color.Gainsboro;
            xrDetail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            xrDetail.Font = new System.Drawing.Font("Tahoma", 8.5F);
            xrDetail.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 0F);
            xrDetail.Name = "xrDetail";
            //xrDetail.SizeF = new System.Drawing.SizeF(1063F, 30f);
            xrDetail.SizeF = new System.Drawing.SizeF(1165.50f, 30f);
            xrDetail.StylePriority.UseBorderColor = false;
            xrDetail.StylePriority.UseBorders = false;
            xrDetail.StylePriority.UseFont = false;
            xrDetail.StylePriority.UseTextAlignment = false;
            xrDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            double TAmount = 0;
            for (int rrpow = 0; rrpow < dtData.Rows.Count; rrpow++)
            {
                XRTableRow Prow = new XRTableRow();
                for (int rcoll = 0; rcoll < dtData.Columns.Count; rcoll++)
                {
                    XRTableCell cell = new XRTableCell();
                    cell.Name = "tcl" + rcoll + "";
                    if (dtData.Columns[rcoll].ColumnName == "NAME")
                    {
                        //cell.WidthF = dtData.Columns[rcoll].ColumnName.Length + 50;
                        cell.WidthF = dtData.Columns[rcoll].ColumnName.Length + 130;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        cell.Padding = 3;
                    }
                    else if (dtData.Columns[rcoll].ColumnName == "DESIGNATION")
                    {
                        //cell.WidthF = dtData.Columns[rcoll].ColumnName.Length + 38;
                        cell.WidthF = dtData.Columns[rcoll].ColumnName.Length + 180;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        cell.Padding = 3;
                    }
                    else
                    {
                        //cell.WidthF = dtData.Columns[rcoll].ColumnName.Length + 35;
                        cell.WidthF = dtData.Columns[rcoll].ColumnName.Length + 130;
                        cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        cell.Padding = 3;
                    }
                    string textprint = dtData.Columns[rcoll].ColumnName.ToString();
                    if (clsPayrollBase.PAYROLL_COLUMN_NAMES.ToLower().IndexOf(dtData.Columns[rcoll].ColumnName.ToLower()) != -1)
                    {
                        TAmount = ReportProperty.Current.NumberSet.ToDouble(dtData.Rows[rrpow][dtData.Columns[rcoll].ColumnName].ToString());
                        cell.Text = ReportProperty.Current.NumberSet.ToNumber(TAmount).ToString();
                    }
                    else
                    {
                        cell.Text = dtData.Rows[rrpow][dtData.Columns[rcoll].ColumnName].ToString();
                    }
                    Prow.Cells.Add(cell);
                }
                xrDetail.Rows.Add(Prow);
            }
            xrDetail.EndInit();
            xrDetail.AdjustSize();
            return xrDetail;
        }

        public XRTable FooterTable()
        {
            DataView dvdata = GetReportSource();
            DataTable dtData = dvdata.ToTable();

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
            //table.SizeF = new System.Drawing.SizeF(1063F, 30f);
            table.SizeF = new System.Drawing.SizeF(1165.50f, 30f);
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
            for (int idt = 0; idt < dtData.Columns.Count; idt++)
            {
                XRTableCell cell = new XRTableCell();
                cell.Name = "tcl" + idt + "";
                if (dtData.Columns[idt].ColumnName == "NAME")
                {
                    //cell.WidthF = dtData.Columns[idt].ColumnName.Length + 50;
                    cell.WidthF = dtData.Columns[idt].ColumnName.Length + 130;
                    cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    cell.Padding = 3;
                }
                else if (dtData.Columns[idt].ColumnName == "DESIGNATION")
                {
                    //cell.WidthF = dtData.Columns[idt].ColumnName.Length + 38;
                    cell.WidthF = dtData.Columns[idt].ColumnName.Length + 180;
                    cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    cell.Padding = 3;
                }
                else
                {
                    //cell.WidthF = dtData.Columns[idt].ColumnName.Length + 35;
                    cell.WidthF = dtData.Columns[idt].ColumnName.Length + 130;
                    cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                    cell.Padding = 3;
                }
                string textprint = dtData.Columns[idt].ColumnName.ToString();
                if (clsPayrollBase.PAYROLL_COLUMN_NAMES.ToLower().IndexOf(dtData.Columns[idt].ColumnName.ToLower()) != -1)
                {
                    dtData.Columns.Add("Temp", typeof(System.Double));
                   // dtData.Columns["Temp"].Expression = "IIF([" + textprint + "]='',0,[" + textprint + "])";
                      dtData.Columns["Temp"].Expression = "convert([" + textprint + "],'System.Double')";
                    double grandtotal = ReportProperty.Current.NumberSet.ToDouble(dtData.Compute("SUM(Temp)", "").ToString());
                    cell.Text = ReportProperty.Current.NumberSet.ToNumber(grandtotal);
                    dtData.Columns.Remove("Temp");
                    row.Cells.Add(cell);
                }
                else
                {
                    row.Cells.Add(cell);
                }

            }
            table.Rows.Add(row);
            table.EndInit();
            return table;
        }

        #endregion
    }
}
