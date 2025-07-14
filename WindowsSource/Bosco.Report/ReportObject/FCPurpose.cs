using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class FCPurpose : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        bool isLastRecord = false;
        int Donornumber = 0;
        #endregion

        #region Constructor
        public FCPurpose()
        {
            InitializeComponent();
            // this.AttachDrillDownToRecord(xrGroupIndividual, xrDonorName,
            //new ArrayList { this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName, this.ReportParameters.PURPOSEColumn.ColumnName }, DrillDownType.FC_REPORT, false);
            this.AttachDrillDownToRecord(xrFCPurposeDetails, xrDonor,
               new ArrayList { this.ReportParameters.DONAUD_IDColumn.ColumnName, this.ReportParameters.CONTRIBUTION_IDColumn.ColumnName, this.ReportParameters.PURPOSEColumn.ColumnName, this.ReportParameters.DONORColumn.ColumnName, "RECEIPT_DATE" }, DrillDownType.FC_REPORT, false);
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            FCPurposeReport();

        }

        private void FCPurposeReport()
        {
            this.SetLandscapeHeader = xrtblTotal.WidthF;
            this.SetLandscapeFooter = xrtblTotal.WidthF;
            this.SetLandscapeFooterDateWidth = 970.00f;
            Donornumber = 0;

            grbBreakupDonor.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;

            if (this.ReportProperties.DateFrom != string.Empty || this.ReportProperties.DateTo != string.Empty)
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));

                        // this.ReportTitle = ReportProperty.Current.ReportTitle;
                        setHeaderTitleAlignment();
                        SetReportTitle();
                        //  this.ReportSubTitle = "Foreign Projects"; //ReportProperty.Current.ProjectTitle;
                        // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                        ShowHideBreakUpByDonor();

                        DataTable dtFCPurpose = GetReportSource();
                        if (dtFCPurpose != null)
                        {
                            this.DataSource = dtFCPurpose;
                            this.DataMember = dtFCPurpose.TableName;
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

                    // this.ReportTitle = ReportProperty.Current.ReportTitle;
                    setHeaderTitleAlignment();
                    SetReportTitle();
                    //  this.ReportSubTitle = "Foreign Projects"; //ReportProperty.Current.ProjectTitle;
                    // this.ReportPeriod = String.Format("For the Period: {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                    ShowHideBreakUpByDonor();

                    DataTable dtFCPurpose = GetReportSource();
                    if (dtFCPurpose != null)
                    {
                        this.DataSource = dtFCPurpose;
                        this.DataMember = dtFCPurpose.TableName;
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
            try
            {
                string FCPurposeQueryPath = this.GetReportForeginContribution(SQL.ReportSQLCommand.ForeginContribution.FCPurpose);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, FCPurposeQueryPath);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }

        private void SetReportBorder()
        {
            FCPurposeHeader = AlignHeaderTable(FCPurposeHeader);
            xrGroupIndividual = AlignPurposeTable(xrGroupIndividual);
            xrDonorName.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
            xrFCPurposeDetails = AlignContentTable(xrFCPurposeDetails);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            xrPCPurposeFooter = AlignGrandTotalTable(xrPCPurposeFooter);
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

        private XRTable AlignPurposeTable(XRTable table)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
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

                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
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
            return table;
        }

        #endregion

        /// <summary>
        /// visibility
        /// </summary>
        private void ShowHideBreakUpByDonor()
        {
            if (ReportProperties.BreakbyDonor == 1)
            {
                grbBreakupDonor.Visible = true;
                grbBreakupDonor.GroupFields[0].FieldName = "Donor";
                xrCapDonor.Visible = xrDonor.Visible = false;
                xrCapDonor.WidthF = xrDonor.WidthF = 0F;
                grbGroupFooter.Visible = false;

                //if (PageHeader.Controls.Contains(xrCapDonor))
                //{
                //    PageHeader.Controls.Remove(xrCapDonor);
                //}

                //if (Detail.Controls.Contains(xrDonor))
                //{
                //    Detail.Controls.Remove(xrDonor);
                //}
            }
            else
            {
                grbBreakupDonor.Visible = false;
                xrCapDonor.Visible = xrDonor.Visible = true;
                xrCapDonor.WidthF = xrDonor.WidthF = 218.36F;
                grbBreakupDonor.GroupFields[0].FieldName = "";
                grbGroupFooter.Visible = false;
                //if (PageHeader.Controls.Contains(xrCapDonor))
                //{
                //    PageHeader.Controls.Add(xrCapDonor);
                //}

                //if (Detail.Controls.Contains(xrDonor))
                //{
                //    Detail.Controls.Add(xrDonor);
                //}
            }
        }

        private void grbBreakupDonor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Donornumber > 0)
            {
                grbBreakupDonor.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            }
            Donornumber++;
        }
    }
}
