using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.DAO.Data;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class ChequeRegistger : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public ChequeRegistger()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblCleared, xrChequeNo,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            BindBankChequeCleared();

        }
        #endregion

        #region Methods
        public void BindBankChequeCleared()
        {
            if (!string.IsNullOrEmpty(this.ReportProperties.DateAsOn)
                && !string.IsNullOrEmpty(this.ReportProperties.Project))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                                                
                        // this.ReportTitle = ReportProperty.Current.ReportTitle;
                        //  this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                        SetReportTitle();
                        setHeaderTitleAlignment();
                        this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
                        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                        resultArgs = GetReportSource();
                        DataView dvBankCleared = resultArgs.DataSource.TableView;
                        if (dvBankCleared != null)
                        {
                            dvBankCleared.Table.TableName = "ChequeUncleared";
                            this.DataSource = dvBankCleared;
                            this.DataMember = dvBankCleared.Table.TableName;
                        }
                        else
                        {
                            this.DataSource = null;
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
                    //  this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                    SetReportTitle();
                    setHeaderTitleAlignment();
                    this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
                    this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                    this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

                    resultArgs = GetReportSource();
                    DataView dvBankCleared = resultArgs.DataSource.TableView;
                    if (dvBankCleared != null)
                    {
                        dvBankCleared.Table.TableName = "ChequeUncleared";
                        this.DataSource = dvBankCleared;
                        this.DataMember = dvBankCleared.Table.TableName;
                    }
                    else
                    {
                        this.DataSource = null;
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
            SetReportSetup();
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string bankCleared = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport. ChequeRegisterDetails);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, bankCleared);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
            return resultArgs;
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrCapRealizedDate.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCapRealizedDate.Tag != null && xrCapRealizedDate.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapRealizedDate.Tag.ToString());
            }
            else
            {
                xrCapRealizedDate.Tag = xrCapRealizedDate.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapRealizedDate.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrRealizedDate.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            SetReportBorder();
        }

        private void SetReportBorder()
        {
            xrtblCaption = AlignHeaderTable(xrtblCaption);
            xrtblCleared = AlignContentTable(xrtblCleared);
            xrtblTotal = AlignTotalTable(xrtblTotal);

            this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
        }

        public override XRTable AlignHeaderTable(XRTable table)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    //if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    //{
                    if (count == 1)
                    {
                        tcell.Borders = BorderSide.All;
                    }
                    else if (count == 3)
                    {
                        tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                        if (ReportProperties.ShowLedgerCode != 1)
                        {
                            tcell.Borders = BorderSide.Left;
                        }

                    }
                    else
                        tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    //}
                    //else if (ReportProperties.ShowHorizontalLine == 1)
                    //{
                    //    if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                    //        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    //    else
                    //        tcell.Borders = BorderSide.Bottom;
                    //}
                    //else if (ReportProperties.ShowVerticalLine == 1)
                    //{
                    //    if (count == 1)
                    //        tcell.Borders = BorderSide.Left | BorderSide.Right;
                    //    else if (count == 3 && ReportProperties.ShowLedgerCode != 1)
                    //        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                    //    else
                    //        tcell.Borders = BorderSide.Right;
                    //}
                    //else
                    //{
                    //    tcell.Borders = BorderSide.None;
                    //}
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }
            return table;
        }

        public override XRTable AlignContentTable(XRTable table)
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
                        }
                        else if (count == 3)
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = BorderSide.Left;
                            }

                        }
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Bottom;
                        else if (trow.Cells.Count == count)
                            tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {

                        if (count == 1)
                        {
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        }
                        else if (count == 3)
                        {
                            tcell.Borders = BorderSide.Right;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = BorderSide.None;
                            }
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

        #region Events
        private void xrAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double ReceiptAmt = this.ReportProperties.NumberSet.ToDouble(xrAmount.Text);
            if (ReceiptAmt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrAmount.Text = "";
            }
        }
        #endregion

    }
}
