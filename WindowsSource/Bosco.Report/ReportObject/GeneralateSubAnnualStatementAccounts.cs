using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using AcMEDSync.Model;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class GeneralateSubAnnualStatementAccounts : Bosco.Report.Base.ReportHeaderBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        Natures ReportNature = Natures.Assert;
        DataTable dtReportData = new DataTable();

        public double CYGrandTotal;
        public double PYGrandTotal;
        
        
        double CYAL_difference = 0;
        double PYAL_difference = 0;
        double CYIE_difference = 0;
        double PYIE_difference = 0;
        #endregion

        #region Constructor
        public GeneralateSubAnnualStatementAccounts()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            //BindReport();
        }
        #endregion


        #region Method
        //private void BindReport()
        //{
        //    try
        //    {
        //        this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
        //        this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
        //        this.SetTitleWidth(xrtblHeaderCaption.WidthF);


        //        setHeaderTitleAlignment();
        //        SetReportTitle();
        //        if (string.IsNullOrEmpty(this.ReportProperties.DateFrom) || string.IsNullOrEmpty(this.ReportProperties.DateTo) || 
        //            this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.Project))
        //        {
        //            ShowReportFilterDialog();
        //        }
        //        else
        //        {
        //            if (this.UIAppSetting.UICustomizationForm == "1")
        //            {
        //                if (ReportProperty.Current.ReportFlag == 0)
        //                {
        //                    SplashScreenManager.ShowForm(typeof(frmReportWait));
        //                    BindProperty();
        //                    SplashScreenManager.CloseForm();
        //                    base.ShowReport();
        //                }
        //                else
        //                {
        //                    ShowReportFilterDialog();
        //                }
        //            }
        //            else
        //            {
        //                SplashScreenManager.ShowForm(typeof(frmReportWait));
        //                BindProperty();
        //                SplashScreenManager.CloseForm();
        //                base.ShowReport();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //}

        public void BindProperty(Natures nature, DataTable dtReportSource, double CYAL_Difference, double PYAL_Difference, double CYIE_Difference, double PYIE_Difference)
        {
            this.HideReportHeader = false;
            this.HidePageFooter = false;
            string condition = string.Empty;
            ReportNature = nature;
            dtReportData = dtReportSource;
            CYAL_difference = CYAL_Difference;
            PYAL_difference = PYAL_Difference;
            CYIE_difference = CYIE_Difference;
            PYIE_difference = PYIE_Difference;
            
            if (dtReportSource != null)
            {
                IninilizeTitles();
                dtReportSource.DefaultView.RowFilter = reportSetting1.GENERALATE_REPORTS.LEDGER_NAMEColumn.ColumnName  + " <> ''";
                DataTable dtRptAnnual = dtReportSource.DefaultView.ToTable();
                dtRptAnnual.TableName = "BranchGenerlateAnnualStatements"; 
                this.DataSource = dtRptAnnual;
                this.DataMember = dtRptAnnual.TableName;
            }

            Detail.SortFields.Clear();
            Detail.SortFields.Add(new GroupField(reportSetting1.GENERALATE_REPORTS.IS_CASH_BANK_FD_ORDERColumn.ColumnName));
            Detail.SortFields.Add(new GroupField(reportSetting1.GENERALATE_REPORTS.LEDGER_CODEColumn.ColumnName));
            Detail.SortFields.Add(new GroupField(reportSetting1.GENERALATE_REPORTS.LEDGER_NAMEColumn.ColumnName));
        }
              

        /// <summary>
        /// For Annual Statement Of Accounts
        /// Assign Titles and Captions based on report Nature
        /// </summary>
        private void IninilizeTitles()
        {
            //For Assig Titles
            xrReportTitle.Visible = (ReportNature == Natures.Assert || ReportNature == Natures.Income);
            xrReportDate.Visible = (ReportNature == Natures.Assert || ReportNature == Natures.Income);
            xrHeaderRow.Visible = (ReportNature == Natures.Assert || ReportNature == Natures.Income);

            xrHeaderLedgerName.Text = string.Empty;
            xrHeaderLedgerName1.Text = string.Empty;
            xrIETitle1.Text = string.Empty;
            xrIETitle2.Text = string.Empty;

            xrReportDate.Text = "For the Period : " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).ToShortDateString() + " - " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false).ToShortDateString();
            xrHeaderAmountCurrentYear.Text = "Year " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).Year.ToString();
            xrHeaderAmountPreviousYear.Text = "Previous Year " + UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-1).Year.ToString();
            CYGrandTotal = UtilityMember.NumberSet.ToDouble(dtReportData.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.CURRENT_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString());
            PYGrandTotal = UtilityMember.NumberSet.ToDouble(dtReportData.Compute("SUM(" + reportSetting1.GENERALATE_REPORTS.PREVIOUS_YEAR_BALANCEColumn.ColumnName + ")", string.Empty).ToString()); 
            
            if (ReportNature == Natures.Income || ReportNature == Natures.Expenses)
            {
                xrIERow2.Visible = false;
                xrHeaderLedgerName.Text = (ReportNature == Natures.Income ? "2a Part" : "");
                xrHeaderLedgerName1.Text = (ReportNature == Natures.Income ? "Gain" : "Loss");
                                
                xrIETitle1.Text = (ReportNature == Natures.Income ? "(A breakeven operating losses)" : "(A breakeven operating profit)");
            }
            else
            {
                xrHeaderLedgerName.Text = (ReportNature == Natures.Assert ? "1a Part" : "");
                xrHeaderLedgerName1.Text = (ReportNature == Natures.Assert ? "Assets" : "Liabilities");
                xrIETitle1.Text = (ReportNature == Natures.Assert ? "Operating Loss (1)" : "Operating Income (1)");
                xrIETitle2.Text = (ReportNature == Natures.Assert ? "Deficit Income (2)" : "Net Patrimony (2)");
            }

            // Show excessive expenditure over income / excessive Income over expenditure
            // Show Operating Profile/ Operating Loss, DEFICIT INCOME/NET PATRIMONY
            if ((ReportNature == Natures.Expenses || ReportNature == Natures.Libilities)) //&&  (CYIE_difference <= 0 || PYIE_difference <= 0)
            {
                //For Operating Profile --------------------------------------------------------------------------
                if (CYIE_difference <= 0)
                {
                    xrCYIEAmount1.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(CYIE_difference));
                    CYGrandTotal += Math.Abs(CYIE_difference);
                }

                if (PYIE_difference <= 0)
                {
                    xrPYIEAmount1.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(PYIE_difference));
                    PYGrandTotal += Math.Abs(PYIE_difference);
                }
                //------------------------------------------------------------------------------------------------

                //DEFICIT INCOME/NET PATRIMONY--------------------------------------------------------------------
                if (ReportNature == Natures.Libilities && CYAL_difference <= 0)
                {
                    xrCYIEAmount2.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(CYAL_difference));
                    CYGrandTotal += Math.Abs(CYAL_difference);
                }

                if (ReportNature == Natures.Libilities && PYAL_difference <= 0)
                {
                    xrPYIEAmount2.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(PYAL_difference));
                    PYGrandTotal += Math.Abs(PYAL_difference);
                }
                //------------------------------------------------------------------------------------------------

            }

            if ((ReportNature == Natures.Assert || ReportNature == Natures.Income)) //&& (CYIE_difference > 0 || PYIE_difference > 0)
            {
                //For Operating Loss ------------------------------------------------------------------------------
                if (CYIE_difference > 0)
                {
                    xrCYIEAmount1.Text = UtilityMember.NumberSet.ToNumber(CYIE_difference);
                    CYGrandTotal += CYIE_difference;
                }

                if (PYIE_difference > 0)
                {
                    xrPYIEAmount1.Text = UtilityMember.NumberSet.ToNumber(PYIE_difference);
                    PYGrandTotal += PYIE_difference;
                }
                //-------------------------------------------------------------------------------------------------

                //DEFICIT INCOME/NET PATRIMONY--------------------------------------------------------------------
                if (ReportNature == Natures.Assert && CYAL_difference > 0)
                {
                    xrCYIEAmount2.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(CYAL_difference));
                    CYGrandTotal += Math.Abs(CYAL_difference);
                }

                if (ReportNature == Natures.Assert && PYAL_difference > 0)
                {
                    xrPYIEAmount2.Text = UtilityMember.NumberSet.ToNumber(Math.Abs(PYAL_difference));
                    PYGrandTotal += Math.Abs(PYAL_difference);
                }
                //------------------------------------------------------------------------------------------------
            }

            xrTotal.Text = "Total " + xrHeaderLedgerName1.Text;
            xrCYGrandTotal.Text = UtilityMember.NumberSet.ToNumber(CYGrandTotal);
            xrPYGrandTotal.Text = UtilityMember.NumberSet.ToNumber(PYGrandTotal);


            //Assign values real values into tags to format values while export --------------------
            xrCYIEAmount1.Tag = xrCYIEAmount1.Text; xrPYIEAmount1.Tag = xrPYIEAmount1.Text;
            xrCYIEAmount2.Tag = xrCYIEAmount2.Text; xrPYIEAmount2.Tag = xrPYIEAmount2.Text;
            xrCYGrandTotal.Tag = xrCYGrandTotal.Text; xrPYGrandTotal.Tag = xrPYGrandTotal.Text;
            //---------------------------------------------------------------------------------------

        }
                
        public override XRTable AlignHeaderTable(XRTable table, bool UseSameFont = false)
        {
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
                            tcell.Borders = BorderSide.All;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                            }
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = BorderSide.Left;
                        else if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }

            
            this.SetCurrencyFormat(xrHeaderAmountCurrentYear.Text, xrHeaderAmountCurrentYear);
            this.SetCurrencyFormat(xrHeaderAmountPreviousYear.Text, xrHeaderAmountPreviousYear);
            //this.SetCurrencyFormat(xrHeaderCurrentTransClosingCredit.Text, xrHeaderCurrentTransClosingCredit);

            return table;
        }

        private void MakeEmptyCell(BindingEventArgs ecell)
        {
            if (ecell.Value != null)
            {
                if (UtilityMember.NumberSet.ToDouble(ecell.Value.ToString()) == 0)
                {
                    ecell.Value = "";
                }
            }
        }

        private string GetDepreciationCaption()
        {
            string rtn = string.Empty;
            if (this.DataSource != null)
            {
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                if (ReportNature == Natures.Expenses || ReportNature == Natures.Libilities)
                {
                    rtn = (ReportNature == Natures.Expenses && conledgercode.ToUpper() == "G" ?
                                "CURRENT YEAR" : (ReportNature == Natures.Libilities && conledgercode.ToUpper() == "G" ? "" : string.Empty));
                }
            }
            return rtn;
        }
        
        #endregion

       
        #region Events

        private void xrCashBankFDLedgerName_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.IS_CASH_BANK_FD_ORDERColumn.ColumnName) != null)
            {
                Int32 CashBankFDLedger = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.IS_CASH_BANK_FD_ORDERColumn.ColumnName).ToString());
                e.Value = (CashBankFDLedger == 0 ? "Cash" : (CashBankFDLedger == 1 ? "Bank" : "Fixed Deposit"));
            }
        }

        private void xrFooterTotal_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName) != null)
            {
                XRTableCell cell = sender as XRTableCell;
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                string conledgername = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_NAMEColumn.ColumnName).ToString();
                cell.Text = "TOTAL " + conledgercode + " - " + conledgername;
            }
        }

        private void xrDebit_EvaluateBinding(object sender, BindingEventArgs e)
        {
            MakeEmptyCell(e);
        }

        private void xrCredit_EvaluateBinding(object sender, BindingEventArgs e)
        {
            MakeEmptyCell(e);
        }


        private void xrCYIEAmount1_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void xrCYIEAmount1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrCYIEAmount1_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(xrCYIEAmount1.Tag.ToString()));
        }

        private void xrPYIEAmount1_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(xrPYIEAmount1.Tag.ToString()));
        }

        private void xrCYIEAmount2_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(xrCYIEAmount2.Tag.ToString()));
        }

        private void xrPYIEAmount2_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(xrPYIEAmount2.Tag.ToString()));
        }

        private void xrCYGrandTotal_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(xrCYGrandTotal.Tag.ToString()));
        }

        private void xrPYGrandTotal_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(xrPYGrandTotal.Tag.ToString()));
        }

        private void xrLedgerName_EvaluateBinding(object sender, BindingEventArgs e)
        {
            if (this.DataSource != null)
            {
                string conledgercode = GetCurrentColumnValue(reportSetting1.GENERALATE_REPORTS.CON_LEDGER_CODEColumn.ColumnName).ToString();
                if (ReportNature == Natures.Expenses || ReportNature == Natures.Libilities)
                {
                    string rtn = GetDepreciationCaption();
                    e.Value = e.Value + (!string.IsNullOrEmpty(rtn) ? " (" + rtn + ")" : string.Empty);
                }
            }
        }
       
        #endregion

        

       

    }
}
