using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class ACYearSignDetails : Bosco.Report.Base.ReportHeaderBase
    {
        public ACYearSignDetails()
        {
            InitializeComponent();
        }

        #region ShowReport
        public override void ShowReport()
        {
            BindACSignDetails();
        }

        private void BindACSignDetails()
        {
            SetReportTitle();
            if (this.UIAppSetting.UICustomizationForm == "1")
            {
                if (ReportProperty.Current.ReportFlag == 0)
                {
                    SplashScreenManager.ShowForm(typeof(frmReportWait));
                    BindReportData();
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
                else
                {
                    ShowReportFilterDialog();
                    SetReportBorder();
                }
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(frmReportWait));
                BindReportData();
                SplashScreenManager.CloseForm();
                base.ShowReport();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ResultArgs BindReportData()
        {
            ResultArgs resultArgs = new ResultArgs(); ;
            string sqlAcSigndetails = this.GetReportSQL(SQL.ReportSQLCommand.Report.ACYearSignDetails);

            using (DataManager dataManager = new DataManager())
            {   
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlAcSigndetails);
            }
             
            if (resultArgs.Success)
            {
                DataTable dtAcSignDetails = resultArgs.DataSource.Table;

                dtAcSignDetails.TableName = "ReportSign";
                this.DataSource = dtAcSignDetails;
                this.DataMember = dtAcSignDetails.TableName;

                Detail.Visible = grpHeaderACYear.Visible = (dtAcSignDetails.Rows.Count > 0);
                
            }
            else
            {
                MessageRender.ShowMessage(resultArgs.Message);
            }
            SetReportBorder();
            return resultArgs;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetReportBorder()
        {
            //xrTblGrpAcName = AlignGroupTable(xrTblGrpAcName);
            AlignContentTable(xrTbllProject);
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            AlignContentTable(xrtblRoleDetails);
            this.HideReportSubTitle = true;
            this.HideDateRange = false;
            this.HideCostCenter = true;
            this.HideBudgetName = true;
        }

        public void AlignContentTable(XRTable table)
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
                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
                            if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            if (count == trow.Cells.Count)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (count == trow.Cells.Count)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }

                        else
                        {
                            tcell.Borders = BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = BorderSide.Left;
                        }
                        else if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right;
                        }
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;

                }
            }
            
        }
        #endregion

        private void xrcellProjectName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.ReportSign.PROJECT_IDColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                Int32 projectid =  UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.ReportSign.PROJECT_IDColumn.ColumnName).ToString());
                if (projectid == 0)
                {
                    cell.Text = "For All Projects";
                }
            }
        }
    }
}
