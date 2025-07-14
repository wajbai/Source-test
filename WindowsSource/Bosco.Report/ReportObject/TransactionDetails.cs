using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using System.Data;
using Bosco.DAO.Data;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class TransactionDetails : Bosco.Report.Base.ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        int DailyGroupNumber = 0;
        #endregion

        #region Constructor
        public TransactionDetails()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblBindSource, xrLedger,
                   new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_VOUCHER, false, "VOUCHER_SUB_TYPE", true);
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            TransactionDetailsReport();
        }

        private void TransactionDetailsReport()
        {
            this.SetLandscapeHeader = 760.25f;
            this.SetLandscapeFooter = 760.25f;

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;

            setHeaderTitleAlignment();
            SetReportTitle();

            if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo)
                || this.ReportProperties.Project == "0")
            {

                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        resultArgs = GetReportSource();
                        this.DataSource = null;
                        DataView dvCashBankBook = resultArgs.DataSource.TableView;
                        if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                        {
                            dvCashBankBook.Table.TableName = "Ledger";
                            this.DataSource = dvCashBankBook;
                            this.DataMember = dvCashBankBook.Table.TableName;
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
                    resultArgs = GetReportSource();
                    this.DataSource = null;
                    DataView dvCashBankBook = resultArgs.DataSource.TableView;
                    if (dvCashBankBook != null && dvCashBankBook.Table.Rows.Count > 0)
                    {
                        dvCashBankBook.Table.TableName = "Ledger";
                        this.DataSource = dvCashBankBook;
                        this.DataMember = dvCashBankBook.Table.TableName;
                    }
                    SplashScreenManager.CloseForm();
                    base.ShowReport();
                }
            }

            //GroupField gfDate = new GroupField("DATE", XRColumnSortOrder.Ascending);
            //GroupField gfvid = new GroupField("VOUCHER_ID", XRColumnSortOrder.Ascending);
            //GroupHeader1.GroupFields.Add(gfDate);
            //GroupHeader1.GroupFields.Add(gfvid);


            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblBindSource = AlignContentTable(xrtblBindSource);
            xrtblContent = AlignContentTable(xrtblContent);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);
        }

        public override XRTable AlignContentTable(XRTable table)
        {
            int j = table.Rows.Count;
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells)
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom | BorderSide.Top;
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
            return table;
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string Query = this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.TransactionDetails);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataView, Query);
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        private void xrTableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentdebit = this.ReportProperties.NumberSet.ToDouble(xrTableCell7.Text);
            if (paymentdebit != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCell7.Text = "";
            }
        }

        private void xrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double paymentCredit = this.ReportProperties.NumberSet.ToDouble(xrTableCell8.Text);
            if (paymentCredit != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrTableCell8.Text = "";
            }
        }





    }
}
