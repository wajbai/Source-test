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
    public partial class BankChequeCollectedRegister : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public BankChequeCollectedRegister()
        {
            InitializeComponent();
            this.AttachDrillDownToRecord(xrtblCleared, xrVoucherNo,
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
                string bankCleared = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.ChequeCollectedRegister);
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
            SetReportBorder();
        }

        private void SetReportBorder()
        {
            xrtblCaption = AlignHeaderTable(xrtblCaption);
            xrtblCleared = AlignContentTable(xrtblCleared);
            xrtblTotal = AlignTotalTable(xrtblTotal);
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
            double Amt = this.ReportProperties.NumberSet.ToDouble(xrAmount.Text);
            if (Amt != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrAmount.Text = "";
            }
        }

       
        private void xrTotalAmt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (this.DataSource != null)
            //{
            //    DataTable dt = (this.DataSource as DataView).Table;
            //    double drsum = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(" + reportSetting1.Transaction.AMOUNTColumn.ColumnName + ")",
            //                reportSetting1.Ledger.TRANS_MODEColumn.ColumnName + "='DR'").ToString());
            //    double crsum = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(" + reportSetting1.Transaction.AMOUNTColumn.ColumnName + ")",
            //                reportSetting1.Ledger.TRANS_MODEColumn.ColumnName + "='CR'").ToString());

            //    xrTotalAmt.Text = UtilityMember.NumberSet.ToNumber((drsum - crsum));
            //}
        }
        #endregion

        private void xrChequeDate_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = (sender as XRTableCell);
            if (cell != null)
            {
                DateTime chequedate =  UtilityMember.DateSet.ToDate(cell.Text,false);
                if (UtilityMember.DateSet.ToDate(chequedate.ToString()) == "01/01/0001")
                {
                    cell.Text = "";
                }
                //DateTime.int
                //0001-01-01 00:00:00
            }
        }

        

        
    }
}
