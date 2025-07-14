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
    public partial class BankChequeIssuedRegister : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public BankChequeIssuedRegister()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblCleared, xrChequeNo,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false, "VOUCHER_SUB_TYPE");
        }
        #endregion

        #region ShowReport

        public override void ShowReport()
        {
            BindBankChequeIssuedRegister();
        }
        #endregion

        #region Methods
        public void BindBankChequeIssuedRegister()
        {
            this.SetLandscapeHeader = this.SetLandscapeBudgetNameWidth = this.SetLandscapeCostCentreWidth = xrtblCaption.WidthF;
            this.SetLandscapeFooter = xrtblCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblCaption.WidthF;

            if (!string.IsNullOrEmpty(this.ReportProperties.DateFrom) && !string.IsNullOrEmpty(this.ReportProperties.DateTo) && !string.IsNullOrEmpty(this.ReportProperties.Project))
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        //SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindReport();
                        //SplashScreenManager.CloseForm();
                    }
                    else
                    {
                        SetReportTitle();
                        ShowReportFilterDialog();
                    }
                }
                else
                {
                    //SplashScreenManager.ShowForm(typeof(frmReportWait));
                    BindReport();
                    //SplashScreenManager.CloseForm();
                }
            }
            else
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            SetReportSetup();
        }

        private void BindReport()
        {
            SetReportTitle();
            this.HideBudgetName = HideCostCenter = true;
            this.BudgetName = this.ReportProperties.SelectedLedgerName;
            this.CosCenterName = this.ReportProperties.BankAccountName;
            setHeaderTitleAlignment();

            resultArgs = GetReportSource();
            DataView dvBankCleared = resultArgs.DataSource.TableView;
            if (dvBankCleared != null)
            {
                dvBankCleared.Table.TableName = "ChequeUncleared";
                
                this.DataSource = dvBankCleared;
                this.DataMember = dvBankCleared.Table.TableName;
                Detail.Visible = ReportFooter.Visible  = (dvBankCleared.Table.Rows.Count > 0);
            }
            else
            {
                this.DataSource = null;
            }
            base.ShowReport();
        }

        private ResultArgs GetReportSource()
        {
            try
            {
                string bankCleared = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.ChequeIssuedRegister);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);

                    //05/12/2019, 
                    if (!string.IsNullOrEmpty(this.ReportProperties.CashBankLedger) && this.ReportProperties.CashBankLedger!="0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.CASHBANK_LEDGER_IDColumn, this.ReportProperties.CashBankLedger);
                    }

                    if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                    {
                        dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    }

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
            float actualCodeWidth = xrCapClearedDate.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCapClearedDate.Tag != null && xrCapClearedDate.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapClearedDate.Tag.ToString());
            }
            else
            {
                xrCapClearedDate.Tag = xrCapClearedDate.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapClearedDate.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrClearedDate.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            SetReportBorder();
        }

        private void SetReportBorder()
        {
            xrtblCaption = AlignHeaderTable(xrtblCaption);
            xrtblCleared = AlignContentTable(xrtblCleared);
            xrtblTotal = AlignTotalTable(xrtblTotal);
            this.SetCurrencyFormat(xrCapIssuedAmount.Text, xrCapIssuedAmount);
            this.SetCurrencyFormat(xrCapAmount.Text, xrCapAmount);
        }

        public override XRTable AlignHeaderTable(XRTable table, bool UseSameFont = false)
        {
            foreach (XRTableRow trow in table.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
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
                    
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? tcell.Font : new Font(tcell.Font, FontStyle.Regular));
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
            if (GetCurrentColumnValue(this.reportSetting1.Ledger.TRANS_MODEColumn.ColumnName) != null)
            {
                double Amt = this.ReportProperties.NumberSet.ToDouble(xrAmount.Text);
                string transMode = GetCurrentColumnValue(this.reportSetting1.Ledger.TRANS_MODEColumn.ColumnName).ToString();
                if (Amt != 0)
                {
                    xrAmount.Text = this.ReportProperties.NumberSet.ToNumber(Amt) + " " + transMode;
                    e.Cancel = false;
                }
                else
                {
                    xrAmount.Text = "";
                }
            }
        }
        #endregion

        private void xrIssuedAmount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double Amt = this.ReportProperties.NumberSet.ToDouble(xrIssuedAmount.Text);
            if (Amt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrIssuedAmount.Text = "";
            }
        }

        private void xrTotalAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.DataSource != null)
            {
                DataTable dt = (this.DataSource as DataView).Table;
                double drsum = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(" + reportSetting1.Transaction.AMOUNTColumn.ColumnName + ")", 
                            reportSetting1.Ledger.TRANS_MODEColumn.ColumnName + "='DR'").ToString());
                double crsum = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(" + reportSetting1.Transaction.AMOUNTColumn.ColumnName + ")",
                            reportSetting1.Ledger.TRANS_MODEColumn.ColumnName + "='CR'").ToString());

                xrTotalAmt.Text = UtilityMember.NumberSet.ToNumber((drsum - crsum));
            }
        }

        
    }
}
