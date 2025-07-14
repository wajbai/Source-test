using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraPrinting;
namespace Bosco.Report.ReportObject
{
    public partial class FCDonorIndividual : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        bool isLastRecord = false;
        #endregion

        #region Constructor
        public FCDonorIndividual()
        {
            InitializeComponent();
            //   this.AttachDrillDownToRecord(xrGroupIndividual, xrDonorName,
            //new ArrayList { this.ReportParameters.DONAUD_IDColumn.ColumnName, this.ReportParameters.DONORColumn.ColumnName }, DrillDownType.FC_REPORT, false);
            this.AttachDrillDownToRecord(xrtblFDIndividualGroup, xrtblDetailsPurpose,
               new ArrayList { this.ReportParameters.DONAUD_IDColumn.ColumnName, this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName, this.ReportParameters.PURPOSEColumn.ColumnName, this.ReportParameters.DONORColumn.ColumnName, "DATE_AND_MONTH_OF_RECEIPTS" }, DrillDownType.FC_REPORT, false);
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            BindFCDonorIndividual();

        }
        #endregion

        #region Method
        private void BindFCDonorIndividual()
        {
            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));

                        // this.ReportTitle = this.ReportProperties.ReportTitle;
                        setHeaderTitleAlignment();
                        SetReportTitle();
                        // this.ReportSubTitle = "Foreign Projects"; //this.ReportProperties.ProjectTitle;
                        //this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;


                        DataTable dtFCDonor = GetReportSource();
                        if (dtFCDonor != null)
                        {
                            this.DataSource = dtFCDonor;
                            this.DataMember = dtFCDonor.TableName;
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
                    // this.ReportTitle = this.ReportProperties.ReportTitle;
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    // this.ReportSubTitle = "Foreign Projects"; //this.ReportProperties.ProjectTitle;
                    //this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + "-" + this.ReportProperties.DateTo;
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;


                    DataTable dtFCDonor = GetReportSource();
                    if (dtFCDonor != null)
                    {
                        this.DataSource = dtFCDonor;
                        this.DataMember = dtFCDonor.TableName;
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
            SetReportBorder();
        }
        private DataTable GetReportSource()
        {
            string FcDonorIndividual = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCDonorIndividual);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FcDonorIndividual);
            }
            return resultArgs.DataSource.Table;
        }

        private void SetReportBorder()
        {
            xrHeader = AlignHeaderTable(xrHeader);
            xrGroupIndividual = alignDonorTable(xrGroupIndividual);
            xrtblFDIndividualGroup = AlignContentTable(xrtblFDIndividualGroup);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
            this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
        }

        public override XRTable AlignTotalTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Top | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    // tcell.BorderColor = ((int)BorderStyleCell.Regular==0)? System.Drawing.Color.Black :System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignGrandTotalTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                        else if (count == trow.Cells.Count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
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

        private XRTable alignDonorTable(XRTable table)
        {
            int j = table.Rows.Count;
            int rowCount = 0;
            foreach (XRTableRow trow in table.Rows)
            {
                rowCount++;
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {

                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (rowCount == 1)
                        {
                            if (count == 1 && ReportProperties.ShowDonorAddress == 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right;
                            }
                            else if (count == 1 && ReportProperties.ShowDonorAddress != 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                            else if (count == 1)
                            {
                                tcell.Borders = BorderSide.Left;
                            }
                            else if (ReportProperties.ShowDonorAddress == 1)
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Left;
                            }
                            else
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (rowCount == 2)
                        {
                            if (count == 1 && ReportProperties.ShowDonorAddress == 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Bottom | BorderSide.Right;
                            }
                            else if (count == 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            }
                            else
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (rowCount == 1)
                        {

                            if (count == 1 && ReportProperties.ShowDonorAddress == 1)
                            {
                                tcell.Borders = BorderSide.Left;
                            }
                            else if (count == 1 && ReportProperties.ShowDonorAddress != 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            }
                            else if (ReportProperties.ShowDonorAddress == 1)
                            {
                                tcell.Borders = BorderSide.Right;
                            }
                            else if (ReportProperties.ShowDonorAddress != 1)
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (rowCount == 2)
                        {
                            if (count == 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                            }
                            else
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                        }

                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (rowCount == 1)
                        {
                            if (count == 1 && ReportProperties.ShowDonorAddress == 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right;
                            }
                            else if (count == 1 && ReportProperties.ShowDonorAddress != 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                            else if (ReportProperties.ShowDonorAddress == 1)
                            {
                                tcell.Borders = BorderSide.Right;
                            }
                            else if (ReportProperties.ShowDonorAddress != 1)
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                        }
                        else if (rowCount == 2)
                        {
                            if (count == 1)
                            {
                                tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                            }
                            else
                            {
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            }
                        }
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

        #endregion

        private void xrtblDetailsAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrtblDetailsAmount.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrtblDetailsAmount.Text = "";
            }
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            bool isPageBreakEnabled = ReportProperties.BreakbyDonor == 1;
            isLastRecord = IsLastRecordInGroup();

            ReportFooter.KeepTogether = true;
            ReportFooter.Visible = isPageBreakEnabled ? isLastRecord : true;
        }

        private bool IsLastRecordInGroup()
        {
            DataTable dt = this.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0) return false;

            int currentIndex = this.CurrentRowIndex;
            return currentIndex >= 0 && currentIndex == dt.Rows.Count - 1;
        }

        private void grpGroupAmountfooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            bool isPageBreakEnabled = ReportProperties.BreakbyDonor == 1;
            isLastRecord = IsLastRecordInGroup();

            if (isPageBreakEnabled && !isLastRecord)
            {
                grpGroupAmountfooter.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
            }
            else
            {
                grpGroupAmountfooter.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;
            }

            grpGroupAmountfooter.KeepTogether = true;
        }
    }
}
