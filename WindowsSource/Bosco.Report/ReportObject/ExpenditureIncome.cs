using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.ConfigSetting;
using Bosco.Report.Base;
using Bosco.Utility;
using Bosco.Report.View;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;
namespace Bosco.Report.ReportObject
{
    public partial class ExpenditureIncome : Bosco.Report.Base.ReportHeaderBase
    {
        #region Variable Declaration
        double SumExpenditureAmount = 0;
        double SumIncomeAmount = 0;
        double TotalExpenditureAmount = 0;
        double TotalIncomeAmount = 0;
        double ExcessIncome = 0;
        double ExcessExpenditure = 0;
        private string EXCESSOFINCOME = "Excess Of Expenditure Over Income";
        private string EXCESSOFEXPENDITURE = "Excess of Income Over Expenditure";
        double GrandTotalExpenditureAmount = 0;
        double GrandTotalIncomeAmount = 0;

        double TotalDepreciationExpenditureAmount = 0;
        double TotalDepreciationIncomeAmount = 0;
        string CapIncAmt = string.Empty;
        string CapExpAmt = string.Empty;
        #endregion

        #region Constructor
        public ExpenditureIncome()
        {
            InitializeComponent();
            CapIncAmt = xrCapIncomeAmt.Text;
            CapExpAmt = xrCapExpenditureAmt.Text;
        }
        #endregion

        #region ShowReport
        public override void ShowReport()
        {
            //25/06/2019, to drill from balance sheet Excess of income or excess of expenses
            if (IsDrillDownMode)
            {
                if (this.ReportProperties.DrillDownProperties != null && this.ReportProperties.DrillDownProperties.Count > 0)
                {
                    Dictionary<string, object> dicDDProperties = this.ReportProperties.DrillDownProperties;
                    DrillDownType ddtypeLinkType = DrillDownType.BASE_REPORT;
                    ddtypeLinkType = (DrillDownType)UtilityMember.EnumSet.GetEnumItemType(typeof(DrillDownType), dicDDProperties["DrillDownLink"].ToString());
                    this.ReportProperties.DateFrom =  UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom);
                    this.ReportProperties.DateTo = ReportProperties.DateAsOn;
                }
            }
            BindExpenditureIncomeSource();
        }
        #endregion

        #region Method
        private void BindExpenditureIncomeSource()
        {
            try
            {
                // this.ReportPeriod = String.Format(MessageCatalog.ReportCommonTitle.PERIOD + " {0} - {1}", this.ReportProperties.DateFrom, this.ReportProperties.DateTo);
                this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
                this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
                
                // this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
                // this.ReportTitle = ReportProperty.Current.ReportTitle;
                SetTitleWidth(xrTblGrandTotal.WidthF);
                setHeaderTitleAlignment();
                SetReportTitle();
                // this.ReportTitle = this.ReportProperties.ReportTitle;
                Payments PaymentsLedger = xrSubExpense.ReportSource as Payments;
                PaymentsLedger.HidePaymentReportHeader();
                Receipts receiptsLedger = xrSubIncome.ReportSource as Receipts;
                receiptsLedger.HideReceiptReportHeader();
                
                //On 20/01/2021, To split and show Depriciation Ledgers sepraterly 
                Payments ExpenseLedgerDepreciation = xrSubExpenseDepreciation.ReportSource as Payments;
                ExpenseLedgerDepreciation.HidePaymentReportHeader();
                Receipts IncomeLedgerDepreciation = xrSubIncomeDepreciation.ReportSource as Receipts;
                IncomeLedgerDepreciation.HideReceiptReportHeader();
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

                            BindSource();

                            SplashScreenManager.CloseForm();
                            base.ShowReport();
                        }
                        else
                        {
                            ShowReportFilterDialog();
                        }
                    }
                    else
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));

                        BindSource();
                        
                        SplashScreenManager.CloseForm();
                        base.ShowReport();
                    }
                }
                SetReportBorder();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            SetReportSetup();
        }

        private void SetReportBorder()
        {
            xtTblHeadTable = AlignHeaderTable(xtTblHeadTable);
            //xrTblIEDifference = AlignExpenseTable(xrTblIEDifference);
            xrTblTotal = AlignTotalTable(xrTblTotal);
            xrTblTotal = AlignContentTable(xrTblDepreciationTitle);
            xrTblIEDifference = AlignContentTable(xrTblIEDifference);
            xrTblTotal = AlignTotalTable(xrTblGrandTotal);

            xrcellExcessIncomeTitle.Borders = BorderSide.All;
            xrcellExcessIncomeAmount.Borders = BorderSide.All;
            
            xrcellExcessExpenseAMount.Borders = BorderSide.All;
            xrcellExcessExpenseTitle.Borders = BorderSide.All;

            if (this.AppSetting.AllowMultiCurrency == 1 && ReportProperties.CurrencyCountryId>0)
            {
                xrCapIncomeAmt.Text = CapIncAmt + " (" + ReportProperties.CurrencyCountrySymbol + ")";
                xrCapExpenditureAmt.Text = CapExpAmt + " (" + ReportProperties.CurrencyCountrySymbol + ")";
            }
            else
            {
                this.SetCurrencyFormat(xrCapExpenditureAmt.Text, xrCapExpenditureAmt);
                this.SetCurrencyFormat(xrCapIncomeAmt.Text, xrCapIncomeAmt);
            }
        }

        public XRTable AlignExpenseTable(XRTable table)
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
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        }
                        else
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
        }

        public override XRTable AlignTotalTable(XRTable table)
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
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        }
                        else
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
            return table;
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
                        else if (count == 4)
                        {
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                            if (ReportProperties.ShowLedgerCode != 1)
                            {
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            }

                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right;
                        else if (count == 4 && ReportProperties.ShowLedgerCode != 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        else
                            tcell.Borders = BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }
            return table;
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrCapExpenditureCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCapExpenditureCode.Tag != null && xrCapExpenditureCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCapExpenditureCode.Tag.ToString());
            }
            else
            {
                xrCapExpenditureCode.Tag = xrCapExpenditureCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            xrCapExpenditureCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCapIncomeCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrlLine1.ForeColor = xrlLine2.ForeColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
        }

        private void SetExpenseTableBorders()
        {
            foreach (XRTableRow trow in xrTblIEDifference.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        }
                        else
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }

        }

        private void SetTotalTableBorders()
        {
            foreach (XRTableRow trow in xrTblTotal.Rows)
            {
                int count = 0;
                foreach (XRTableCell tcell in trow.Cells) //table.Rows.FirstRow.Cells)
                {
                    count++;
                    if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        }
                        else
                        {
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                        else
                            tcell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    }
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                }
            }
        }

        private void BindSource()
        {
            ResultArgs IncomeResultArgs = new ResultArgs();
            ResultArgs ExpenseResultArgs = new ResultArgs();
            ResultArgs FinalIEResultArg = new ResultArgs();
            Payments PaymentsLedger = xrSubExpense.ReportSource as Payments;
            PaymentsLedger.HidePaymentReportHeader();
            Receipts receiptsLedger = xrSubIncome.ReportSource as Receipts;
            receiptsLedger.HideReceiptReportHeader();
            
            //On 20/01/2021, To split and show Depriciation Ledgers sepraterly --------------------
            Payments ExpenseLedgerDepreciation = xrSubExpenseDepreciation.ReportSource as Payments;
            ExpenseLedgerDepreciation.HidePaymentReportHeader();
            Receipts IncomeLedgerDepreciation = xrSubIncomeDepreciation.ReportSource as Receipts;
            IncomeLedgerDepreciation.HideReceiptReportHeader();

            this.AttachDrillDownToSubReport(ExpenseLedgerDepreciation);
            this.AttachDrillDownToSubReport(IncomeLedgerDepreciation);
            //----------------------------------------------------------------------------------------

            this.AttachDrillDownToSubReport(PaymentsLedger);
            this.AttachDrillDownToSubReport(receiptsLedger);

            string sqlFinalIE= this.GetFinalAccountsReportSQL(SQL.ReportSQLCommand.FinalAccounts.FinalIncomeExpenditure);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                dataManager.Parameters.Add(this.ReportParameters.BEGIN_FROMColumn, this.AppSetting.BookBeginFrom);

                dataManager.Parameters.Add(this.ReportParameters.SHOWLEDGERCODEColumn, ReportProperties.ShowByLedger);
                dataManager.Parameters.Add(this.ReportParameters.SHOWGROUPCODEColumn, ReportProperties.ShowByLedgerGroup);

                dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");

                //On 04/12/2024, To set Currnecy
                if (this.AppSetting.AllowMultiCurrency == 1 && this.ReportProperties.CurrencyCountryId > 0)
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, this.ReportProperties.CurrencyCountryId);
                }
                else
                {
                    dataManager.Parameters.Add(this.ReportParameters.CURRENCY_COUNTRY_IDColumn, "0");
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                FinalIEResultArg = dataManager.FetchData(DAO.Data.DataSource.DataTable, sqlFinalIE);
            }
            if (FinalIEResultArg.Success)
            {
                DataTable dtFinalIE = FinalIEResultArg.DataSource.Table;
                //On 21/01/2021, Skip Depreciation Ledgers in Main Report
                dtFinalIE.DefaultView.RowFilter = "AMOUNT< 0 AND IS_DEPRECIATION_LEDGER = 0";
                DataTable dtExpenseLedgers = dtFinalIE.DefaultView.ToTable();
                dtExpenseLedgers.Columns.Add("PAYMENTAMT", dtExpenseLedgers.Columns["AMOUNT"].DataType, "AMOUNT * -1"); //Remove Negative Symbol
                ExpenseResultArgs.Success = true;
                ExpenseResultArgs.DataSource.Data = dtExpenseLedgers;
                PaymentsLedger.SortByLedgerorGroup();

                dtFinalIE.DefaultView.RowFilter = string.Empty;
                //On 21/01/2021, Skip Depreciation Ledgers in Main Report
                dtFinalIE.DefaultView.RowFilter = "AMOUNT > 0 AND IS_DEPRECIATION_LEDGER = 0";
                DataTable dtIncomeLedgers = dtFinalIE.DefaultView.ToTable();
                dtIncomeLedgers.Columns["AMOUNT"].ColumnName = "RECEIPTAMT";
                IncomeResultArgs.Success = true;
                IncomeResultArgs.DataSource.Data = dtIncomeLedgers;
                receiptsLedger.SortByLedgerorGroup();
                               
                PaymentsLedger.BindExpenseSource(ExpenseResultArgs, TransType.EP);
                receiptsLedger.BindIncomeSource(IncomeResultArgs, TransType.IC);
                
                SumIncomeAmount =  receiptsLedger.ReceiptAmount;
                SumExpenditureAmount = PaymentsLedger.PaymentAmout;

                //On 21/01/2021, show Depreciation Ledgers alone -------------------------------------
                dtFinalIE.DefaultView.RowFilter = string.Empty;
                dtFinalIE.DefaultView.RowFilter = "AMOUNT< 0 AND IS_DEPRECIATION_LEDGER = 1";
                dtExpenseLedgers = dtFinalIE.DefaultView.ToTable();
                dtExpenseLedgers.Columns.Add("PAYMENTAMT", dtExpenseLedgers.Columns["AMOUNT"].DataType, "AMOUNT * -1"); //Remove Negative Symbol
                ExpenseResultArgs.Success = true;
                ExpenseResultArgs.DataSource.Data = dtExpenseLedgers;
                ExpenseLedgerDepreciation.SortByLedgerorGroup();

                dtFinalIE.DefaultView.RowFilter = string.Empty;
                dtFinalIE.DefaultView.RowFilter = "AMOUNT > 0 AND IS_DEPRECIATION_LEDGER = 1";
                dtIncomeLedgers = dtFinalIE.DefaultView.ToTable();
                dtIncomeLedgers.Columns["AMOUNT"].ColumnName = "RECEIPTAMT";
                IncomeResultArgs.Success = true;
                IncomeResultArgs.DataSource.Data = dtIncomeLedgers;
                receiptsLedger.SortByLedgerorGroup();
                
                ExpenseLedgerDepreciation.BindExpenseSource(ExpenseResultArgs, TransType.EP);
                IncomeLedgerDepreciation.BindIncomeSource(IncomeResultArgs, TransType.IC);
                
                //Attach Deprecation amount too ----------------------------------------------------
                TotalDepreciationIncomeAmount = IncomeLedgerDepreciation.ReceiptAmount;
                TotalIncomeAmount = SumIncomeAmount + TotalDepreciationIncomeAmount;

                TotalDepreciationExpenditureAmount= ExpenseLedgerDepreciation.PaymentAmout;
                TotalExpenditureAmount = SumExpenditureAmount + TotalDepreciationExpenditureAmount;
                //-----------------------------------------------------------------------------------

                xrTblDepreciationTitle.Visible = xrcellDepreciationTitle.Visible = false;
                xrSubExpenseDepreciation.Visible = xrSubIncomeDepreciation.Visible = false;
                //If only one depreciation title, hide title 
                xrTblDepreciationTitle.Visible = xrcellDepreciationTitle.Visible = !(dtIncomeLedgers.Rows.Count<=1 && dtExpenseLedgers.Rows.Count<=1);;
                
                xrSubExpenseDepreciation.Visible = (ExpenseLedgerDepreciation.PaymentAmout > 0);
                xrSubIncomeDepreciation.Visible = (IncomeLedgerDepreciation.ReceiptAmount > 0);
                //------------------------------------------------------------------------------------

                ExcessIncome = 0;
                if (TotalExpenditureAmount < TotalIncomeAmount)
                {
                    ExcessIncome = TotalIncomeAmount - TotalExpenditureAmount;
                }

                ExcessExpenditure = 0;
                if (TotalIncomeAmount < TotalExpenditureAmount)
                {
                    ExcessExpenditure = TotalExpenditureAmount - TotalIncomeAmount;
                }

            }

            // setting Expenditure table column width while showledgercode is set 1
            if (ReportProperties.ShowLedgerCode == 1)
            {
                PaymentsLedger.CodeColumnWidth = ExpenseLedgerDepreciation.CodeColumnWidth = xrCapExpenditureCode.WidthF;
                PaymentsLedger.NameColumnWidth = ExpenseLedgerDepreciation.NameColumnWidth  = xrCapExpenditureName.WidthF;
                PaymentsLedger.AmountColumnWidth = ExpenseLedgerDepreciation.AmountColumnWidth = xrCapExpenditureAmt.WidthF;
            }
            else
            {
                PaymentsLedger.CodeColumnWidth = ExpenseLedgerDepreciation.CodeColumnWidth = 0;
                PaymentsLedger.NameColumnWidth = ExpenseLedgerDepreciation.NameColumnWidth = xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF - 2;
                PaymentsLedger.AmountColumnWidth = ExpenseLedgerDepreciation.AmountColumnWidth = xrCapExpenditureAmt.WidthF;
            }

            // setting Expenditure table column width while showgroupcode is set 1
            if (ReportProperties.ShowGroupCode == 1)
            {
                PaymentsLedger.GroupCodeColumnWidth = ExpenseLedgerDepreciation.GroupCodeColumnWidth  = xrCapExpenditureCode.WidthF;
                PaymentsLedger.GroupNameColumnWidth = ExpenseLedgerDepreciation.GroupNameColumnWidth= xrCapExpenditureName.WidthF;
                PaymentsLedger.GroupAmountColumnWidth = ExpenseLedgerDepreciation.GroupAmountColumnWidth = xrCapExpenditureAmt.WidthF;
            }
            else
            {
                PaymentsLedger.GroupCodeColumnWidth = ExpenseLedgerDepreciation.GroupCodeColumnWidth = 0;
                PaymentsLedger.GroupNameColumnWidth = ExpenseLedgerDepreciation.GroupNameColumnWidth = xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF - 2;
                PaymentsLedger.GroupAmountColumnWidth = ExpenseLedgerDepreciation.GroupAmountColumnWidth = xrCapExpenditureAmt.WidthF;
            }
            PaymentsLedger.CategoryNameWidth = ExpenseLedgerDepreciation.CategoryNameWidth = xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF + xrCapExpenditureAmt.WidthF;
            
            // setting income table column width while showledgercode is set 1
            if (ReportProperties.ShowLedgerCode == 1)
            {
                if (ReportProperties.ShowByLedger == 1)
                {
                    receiptsLedger.CodeColumnWidth = IncomeLedgerDepreciation.CodeColumnWidth = xrCapExpenditureCode.WidthF - 2;
                    receiptsLedger.NameColumnWidth = IncomeLedgerDepreciation.NameColumnWidth = xrCapExpenditureName.WidthF + 2;
                    receiptsLedger.AmountColumnWidth = IncomeLedgerDepreciation.AmountColumnWidth = xrCapIncomeAmt.WidthF + 1;
                }
                else
                {
                    receiptsLedger.CodeColumnWidth = IncomeLedgerDepreciation.CodeColumnWidth = xrCapExpenditureCode.WidthF - 2;
                    receiptsLedger.NameColumnWidth = IncomeLedgerDepreciation.NameColumnWidth  = xrCapExpenditureName.WidthF + 2;
                    receiptsLedger.AmountColumnWidth = IncomeLedgerDepreciation.AmountColumnWidth =  xrCapIncomeAmt.WidthF + 1;
                }
            }
            else
            {
                receiptsLedger.CodeColumnWidth = IncomeLedgerDepreciation.CodeColumnWidth = 0;
                receiptsLedger.NameColumnWidth = IncomeLedgerDepreciation.NameColumnWidth = xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF - 2;
                receiptsLedger.AmountColumnWidth = IncomeLedgerDepreciation.AmountColumnWidth = xrCapIncomeAmt.WidthF;
            }

            // setting income table column width while showgroupcode is set 1
            if (ReportProperties.ShowGroupCode == 1)
            {
                receiptsLedger.GroupCodeColumnWidth = IncomeLedgerDepreciation.GroupCodeColumnWidth = xrCapExpenditureCode.WidthF - 2;
                receiptsLedger.GroupNameColumnWidth = IncomeLedgerDepreciation.GroupNameColumnWidth = xrCapExpenditureName.WidthF + 2;
                receiptsLedger.GroupAmountColumnWidth = IncomeLedgerDepreciation.GroupAmountColumnWidth = xrCapIncomeAmt.WidthF + 1;
            }
            else
            {
                if (ReportProperties.ShowByLedgerGroup == 1)
                {
                    receiptsLedger.GroupCodeColumnWidth = IncomeLedgerDepreciation.GroupCodeColumnWidth = 0;
                    receiptsLedger.GroupNameColumnWidth = IncomeLedgerDepreciation.GroupNameColumnWidth = xrCapIncomeCode.WidthF + xrCapIncomeLedgerName.WidthF - 4;
                    receiptsLedger.GroupAmountColumnWidth = IncomeLedgerDepreciation.GroupAmountColumnWidth = xrCapIncomeAmt.WidthF;
                }
                else
                {
                    receiptsLedger.GroupCodeColumnWidth = IncomeLedgerDepreciation.GroupCodeColumnWidth = 0;
                    receiptsLedger.GroupNameColumnWidth = IncomeLedgerDepreciation.GroupNameColumnWidth = xrCapIncomeCode.WidthF + xrCapIncomeLedgerName.WidthF - 2;
                    receiptsLedger.GroupAmountColumnWidth = IncomeLedgerDepreciation.GroupAmountColumnWidth = xrCapIncomeAmt.WidthF;
                }
            }

            receiptsLedger.CostCentreCategoryNameWidth =  IncomeLedgerDepreciation.CostCentreCategoryNameWidth = xrCapIncomeCode.WidthF + xrCapIncomeLedgerName.WidthF + xrCapIncomeAmt.WidthF;
            //SplashScreenManager.CloseForm();
            //base.ShowReport();
            
            xtTblHeadTable = HeadingTableBorder(xtTblHeadTable, this.ReportProperties.ShowHorizontalLine, this.ReportProperties.ShowVerticalLine);
            // to align xrTblExpense

            //08/10/2024, To Show Forex split -----------------------------------------------------
            xrsubforex.Visible = false;
            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                xrsubforex.Visible = true;
                UcForexSplit forexsplit = xrsubforex.ReportSource as UcForexSplit;
                xrsubforex.WidthF = xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF + xrCapExpenditureAmt.WidthF;
                forexsplit.CurrencyNameWidth = (xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF) / 2;
                forexsplit.GainWidth = (xrCapExpenditureCode.WidthF + xrCapExpenditureName.WidthF) / 2;
                forexsplit.LossWidth = xrCapExpenditureAmt.WidthF;
                forexsplit.DateAsOn = string.Empty;
                forexsplit.DateFrom = ReportProperties.DateFrom;
                forexsplit.DateTo = ReportProperties.DateTo;
                forexsplit.ShowForex();
            }
            //-------------------------------------------------------------------------------------

            SetExpenseTableBorders();
            SetTotalTableBorders();
        }
        #endregion

        #region Events
      
        private void xrcellExcessIncomeTitle_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessIncome > 0)
            {
                ExcessIncome = TotalIncomeAmount - TotalExpenditureAmount;
                e.Result = EXCESSOFEXPENDITURE;
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrcellExcessIncomeAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessIncome > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessIncome);
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrcellExcessExpenseTitle_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessExpenditure>0)
            {
                e.Result = EXCESSOFINCOME;
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrcellExcessExpenseAMount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (ExcessExpenditure > 0)
            {
                e.Result = this.UtilityMember.NumberSet.ToNumber(ExcessExpenditure);
                e.Handled = true;
            }
            else
            {
                e.Result = "";
                e.Handled = true;
            }
        }

        private void xrGrandTotalExpenseAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            GrandTotalExpenditureAmount = TotalExpenditureAmount + ExcessIncome;
            e.Result = GrandTotalExpenditureAmount;
            e.Handled = true;
        }

        private void xrGrandTotalIncomeAmt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            GrandTotalIncomeAmount = TotalIncomeAmount + ExcessExpenditure;
            e.Result = GrandTotalIncomeAmount;
            e.Handled = true;
        }

        private void xrPaymentsSumTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SumExpenditureAmount;
            e.Handled = true;
        }

        private void xrReceiptsSumTotal_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SumIncomeAmount;
            e.Handled = true;
        }
        #endregion
    }
}