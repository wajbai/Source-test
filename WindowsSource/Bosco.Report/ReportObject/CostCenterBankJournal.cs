using System;
using System.Drawing;
using Bosco.Utility;
using Bosco.Report.Base;
using System.Data;
using Bosco.DAO.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using Bosco.Report.View;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

namespace Bosco.Report.ReportObject
{
    public partial class CostCenterBankJournal : ReportHeaderBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        int count = 0;

        string ReceiptCaption = string.Empty;
        string PaymentCaption = string.Empty;

        #endregion

        #region Constructor
        public CostCenterBankJournal()
        {
            InitializeComponent();

            ReceiptCaption = xrCosCapReceipts.Text;
            PaymentCaption = xrCosCapPayments.Text;

            // 21/02/2025,Chinna
            //this.AttachDrillDownToRecord(xrtblSource, xrCosLedger,
            //    new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName }, DrillDownType.LEDGER_CASHBANK_VOUCHER, false);

            this.AttachDrillDownToRecord(xrtblSource, xrCosLedger,
                new ArrayList { this.ReportParameters.VOUCHER_IDColumn.ColumnName, "VOUCHER_TYPE" }, DrillDownType.LEDGER_VOUCHER, false);
        }
        #endregion

        #region Methods
        public override void ShowReport()
        {
            BankJournalReport();

        }

        private void BankJournalReport()
        {
            this.SetLandscapeHeader = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooter = xrtblHeaderCaption.WidthF;
            this.SetLandscapeFooterDateWidth = xrtblHeaderCaption.WidthF;
            this.SetLandscapeCostCentreWidth = xrtblHeaderCaption.WidthF;

            if (String.IsNullOrEmpty(this.ReportProperties.DateFrom) || String.IsNullOrEmpty(this.ReportProperties.DateTo)
                 || this.ReportProperties.Project == "0" || string.IsNullOrEmpty(this.ReportProperties.CostCentre) || this.ReportProperties.CostCentre == "0"
                 || String.IsNullOrEmpty(this.ReportProperties.CashBankLedger) || this.ReportProperties.CashBankLedger == "0")
            {
                SetReportTitle();
                ShowReportFilterDialog();
            }
            else
            {
                if (this.UIAppSetting.UICustomizationForm == "1")
                {
                    if (ReportProperty.Current.ReportFlag == 0)
                    {
                        SplashScreenManager.ShowForm(typeof(frmReportWait));
                        BindProperty();
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
                    BindProperty();
                    SplashScreenManager.CloseForm();
                }
            }
            SetReportSetup();

            // To show by Date starts
            if (this.ReportProperties.ShowByCostCentre == 0)
                grpHeaderCostCenterName.GroupFields[0].FieldName = string.Empty;
            else
                grpHeaderCostCenterName.GroupFields[0].FieldName = "COST_CENTRE";

            if (this.ReportProperties.ShowByCostCentreCategory == 0)
            {
                grpCostCentreCatogory.GroupFields[0].FieldName = string.Empty;
            }
            else
            {
                grpCostCentreCatogory.GroupFields[0].FieldName = "COST_CENTRE_CATEGORY_NAME";
            }
            // To show by Date ends
            base.ShowReport();

        }

        private void BindProperty()
        {
            prOPBalance.Visible = prCLBalance.Visible = false;
            setHeaderTitleAlignment();
            SetReportTitle();

            grpHeaderCostCenterName.Visible = grpFooterCostCenterName.Visible = (ReportProperties.ShowByCostCentre == 1) ? true : false;

            this.HideReportDate = ReportProperties.ReportDate != string.Empty ? true : false;
            this.SetReportDate = ReportProperties.ReportDate != string.Empty ? this.UtilityMember.DateSet.ToDate(ReportProperties.ReportDate, false).ToShortDateString() : string.Empty;
            if (ReportProperties.IncludeNarrationwithCurrencyDetails == 1)
            {
                ReportProperties.IncludeNarration = 1;
            }

            // To show by costcentre starts
            if (this.ReportProperties.ShowByCostCentreCategory == 1 && this.ReportProperties.ShowByCostCentre == 1)
            {
                grpHeaderCostCenterName.Visible = grpFooterCostCenterName.Visible = Detail.Visible = true;
                grpCostCentreCatogory.Visible = true;
            }
            else if (this.ReportProperties.ShowByCostCentre == 1)
            {
                grpCostCentreCatogory.Visible = false;
                grpHeaderCostCenterName.Visible = grpFooterCostCenterName.Visible = Detail.Visible = true;
            }
            else if (this.ReportProperties.ShowByCostCentreCategory == 1)
            {
                grpCostCentreCatogory.Visible = true;
                Detail.Visible = grpCostCentreCatogory.Visible == true ? false : true;
            }
            else
            {
                grpCostCentreCatogory.Visible = false;
                Detail.Visible = true;
            }
            // To show by costcentre ends


            DataTable dtCashBankBook = GetReportSource();
            if (dtCashBankBook != null)
            {
                this.DataSource = dtCashBankBook;
                this.DataMember = dtCashBankBook.TableName;
            }
        }

        private DataTable GetReportSource()
        {
            try
            {
                string CashBankBookQueryPath = this.GetReportCostCentre(SQL.ReportSQLCommand.CostCentre.CostCenterBankJournal);
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.Parameters.Add(this.ReportParameters.DATE_FROMColumn, this.ReportProperties.DateFrom);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_TOColumn, this.ReportProperties.DateTo);
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.COST_CENTRE_IDColumn, this.ReportProperties.CostCentre != null ? this.ReportProperties.CostCentre : "0");
                    //dataManager.Parameters.Add(this.ReportParameters.COUNTColumn, this.ReportProperties.Count);
                    //05/12/2019, to keep Cash Bank LedgerId
                    //if (!string.IsNullOrEmpty(this.ReportProperties.Ledger) && this.ReportProperties.Ledger != "0")
                    //{
                    //    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    //}
                    //else
                    //{
                    //    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, this.ReportProperties.Ledger);
                    //}
                    dataManager.Parameters.Add(this.ReportParameters.LEDGER_IDColumn, (IsDrillDownMode ? "0" : this.ReportProperties.CashBankLedger));
                    dataManager.Parameters.Add(this.ReportParameters.INCLUDE_NARRATION_CURRENCYColumn, this.ReportProperties.IncludeNarrationwithCurrencyDetails);

                    dataManager.Parameters.Add(this.ReportParameters.CONSOLIDATEDColumn, this.ReportProperties.Consolidated);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, CashBankBookQueryPath);

                    this.AssignCCDetailReportSource();
                }
            }
            catch (Exception ee)
            {
                MessageRender.ShowMessage(ee.Message, true);
            }
            finally { }
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// On 12/07/2024, to get cost centre closing balance 
        /// </summary>
        /// <param name="CashOP"></param>
        /// <returns></returns>
        private double getCCClosingBalance(bool IsCash, bool forGrandTotal)
        {
            double rtn = 0;
            Int32 ccid = 0;
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                DataTable dtRpt = this.DataSource as DataTable;
                if (dtRpt != null)
                {
                    ccid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName).ToString());
                    string filter = reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName + "=" + ccid;
                    if (forGrandTotal) filter = string.Empty;

                    double openingbalance = this.getCCOpeningBalance(IsCash, forGrandTotal);
                    openingbalance = openingbalance * -1;// We show opening in Receipt side and this is costentre (expense unit)

                    double cashreceipt = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankJournal.RECEIPTColumn.ColumnName + ")", filter).ToString());
                    double cashpayment = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankJournal.PAYMENTColumn.ColumnName + ")", filter).ToString());
                    cashreceipt += openingbalance;

                    rtn = (cashreceipt - cashpayment);


                }
            }

            return rtn;
        }

        /// <summary>
        /// On 12/07/2024, to get cost centre closing balance 
        /// </summary>
        /// <param name="CashOP"></param>
        /// <returns></returns>
        private double getCCTotalAmount(bool IsCash, bool isReceipt, bool isGrandTotal)
        {
            double rtn = 0;
            Int32 ccid = 0;
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                DataTable dtRpt = this.DataSource as DataTable;
                if (dtRpt != null)
                {
                    ccid = UtilityMember.NumberSet.ToInteger(GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName).ToString());
                    string filter = reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName + "=" + ccid;
                    if (isGrandTotal) filter = string.Empty;

                    double cashreceipt = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankJournal.RECEIPTColumn.ColumnName + ")", filter).ToString());
                    double cashpayment = UtilityMember.NumberSet.ToDouble(dtRpt.Compute("SUM(" + reportSetting1.CashBankJournal.PAYMENTColumn.ColumnName + ")", filter).ToString());
                    double openingbalance = this.getCCOpeningBalance(IsCash, isGrandTotal);
                    openingbalance = openingbalance * -1;// We show opening in Receipt side and this is costentre (expense unit)

                    double closingbalance = this.getCCClosingBalance(IsCash, isGrandTotal);
                    //cashreceipt += (openingbalance > 0 ? openingbalance : 0);
                    //cashpayment += (openingbalance < 0 ? openingbalance : 0);
                    rtn = (isReceipt ? (cashreceipt + openingbalance) : (cashpayment + closingbalance));
                }
            }

            return rtn;
        }

        private void SetReportSetup()
        {
            float actualCodeWidth = xrCosCapLedgerCode.WidthF;
            bool isCapCodeVisible = true;
            //Include / Exclude Code
            if (xrCosCapLedgerCode.Tag != null && xrCosCapLedgerCode.Tag.ToString() != "")
            {
                actualCodeWidth = (float)this.UtilityMember.NumberSet.ToDouble(xrCosCapLedgerCode.Tag.ToString());
            }
            else
            {
                xrCosCapLedgerCode.Tag = xrCosCapLedgerCode.WidthF;
            }

            isCapCodeVisible = (ReportProperties.ShowLedgerCode == 1);
            grpHeaderCostCenterName.Visible = (ReportProperties.ShowByCostCentre == 1);
            if (grpHeaderCostCenterName.Visible != true)
            {
                this.CosCenterName = ReportProperty.Current.CostCentreName;
            }
            else
            {
                this.CosCenterName = string.Empty;
            }
            xrCosCapLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrCosLedgerCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            // xrCapPaymentCode.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);
            xrTableCell6.WidthF = ((isCapCodeVisible == true) ? actualCodeWidth : 0);

            // this.ReportPeriod = this.ReportProperties.ReportDate;
            SetReportBorder();
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                            else if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom;
                        else
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
                        if (count == 1)
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                if (count == 1)
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Left;
                                else if (count == trow.Cells.Count)
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    tcell.Borders = BorderSide.Bottom;
                        else
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
                            tcell.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 8)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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
                            tcell.Borders = BorderSide.All;
                        else if (ReportProperties.ShowLedgerCode != 1)
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                            else
                                if (count == 1)
                                    tcell.Borders = BorderSide.All;
                                else
                                    tcell.Borders = BorderSide.Bottom | BorderSide.Right | BorderSide.Top;
                        }
                        else
                            if (count == 1)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Left;
                            else if (count == trow.Cells.Count)
                                tcell.Borders = BorderSide.Bottom | BorderSide.Top | BorderSide.Right;
                            else
                                tcell.Borders = BorderSide.Bottom | BorderSide.Top;
                    }
                    else if (ReportProperties.ShowVerticalLine == 1)
                    {
                        if (count == 1)
                            tcell.Borders = BorderSide.All;
                        else if (ReportProperties.ShowLedgerCode != 1)
                        {
                            if (count == 3 || count == 9)
                                tcell.Borders = DevExpress.XtraPrinting.BorderSide.None;

                            else
                                tcell.Borders = BorderSide.Top | BorderSide.Bottom | BorderSide.Right;
                        }
                        else
                            tcell.Borders = BorderSide.Top | BorderSide.Bottom | BorderSide.Right;
                    }
                    else
                    {
                        tcell.Borders = BorderSide.None;
                    }
                    //tcell.BorderColor = ((int)BorderStyleCell.Regular == 0) ? System.Drawing.Color.Black : System.Drawing.Color.Black;
                    tcell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.DarkGray : System.Drawing.Color.Black;
                    tcell.Font = (ReportProperty.Current.ColumnCaptionFontStyle == 0 ? FieldColumnHeaderFont : new Font(FieldColumnHeaderFont, FontStyle.Regular));
                }
            }
            return table;
        }

        public override XRTable AlignCashBankBookTable(XRTable table, string bankNarration, string cashNarration, int count)
        {
            int rowcount = 0;

            foreach (XRTableRow row in table.Rows)
            {
                int cellcount = 0;
                ++rowcount;
                if (rowcount == 2 && ReportProperties.IncludeNarration != 1)
                {
                    row.Visible = false;
                }
                else if (bankNarration == string.Empty && cashNarration == string.Empty && ReportProperties.IncludeNarration == 1 && rowcount == 2)
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
                foreach (XRTableCell cell in row)
                {
                    ++cellcount;
                    if (ReportProperties.IncludeNarration != 1 && rowcount == 1)
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = BorderSide.Right | BorderSide.Left | BorderSide.Bottom;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = BorderSide.None;
                                else
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                            else
                                cell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | BorderSide.Left;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                            else
                                if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (cellcount == 1)
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                            else if (ReportProperties.ShowLedgerCode != 1)
                                if (cellcount == 3 || cellcount == 9)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                            else
                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                        }
                        else
                        {
                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                        }

                    }
                    else
                    {
                        if (ReportProperties.ShowHorizontalLine == 1 && ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (count == 1)
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                    else if (ReportProperties.ShowLedgerCode != 1)
                                    {
                                        if (cellcount == 3 || cellcount == 9)
                                            cell.Borders = BorderSide.None;
                                        else
                                        {
                                            if (bankNarration != string.Empty || cashNarration != string.Empty)
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                            else
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                        }
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top;
                                    }
                                }
                                else
                                {
                                    if (cellcount == 1)
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                    else if (ReportProperties.ShowLedgerCode != 1)
                                    {
                                        if (cellcount == 3 || cellcount == 9)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                        else
                                        {
                                            if (bankNarration != string.Empty || cashNarration != string.Empty)
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                            else
                                                cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                        }
                                    }
                                    else
                                    {
                                        if (bankNarration != string.Empty || cashNarration != string.Empty)
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                        else
                                            cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                    }
                                }

                            }
                            else
                            {
                                if (cellcount == 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                else if (ReportProperties.ShowLedgerCode != 1)
                                {
                                    if (cellcount == 3 || cellcount == 9)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                }
                                else
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowHorizontalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (bankNarration != string.Empty || cashNarration != string.Empty)
                                    if (cellcount == 1)
                                        cell.Borders = BorderSide.Left;
                                    else if (cellcount == row.Cells.Count)
                                        cell.Borders = BorderSide.Right;
                                    else
                                        cell.Borders = BorderSide.None;
                                else if (cellcount == 1)
                                    cell.Borders = BorderSide.Left | BorderSide.Bottom;
                                else if (cellcount == row.Cells.Count)
                                    cell.Borders = BorderSide.Right | BorderSide.Bottom;
                                else
                                    cell.Borders = BorderSide.Bottom;
                            }
                            else
                            {
                                if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                else
                                    if (cellcount == 1)
                                        cell.Borders = BorderSide.Bottom | BorderSide.Left;
                                    else if (cellcount == row.Cells.Count)
                                        cell.Borders = BorderSide.Bottom | BorderSide.Right;
                                    else
                                        cell.Borders = BorderSide.Bottom;
                            }

                        }
                        else if (ReportProperties.ShowVerticalLine == 1)
                        {
                            if (rowcount == 1)
                            {
                                if (cellcount == 1)
                                {
                                    if (bankNarration != string.Empty || cashNarration != string.Empty)
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                    else
                                        cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                            else
                            {
                                if (cellcount == 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                else if (cellcount == 3 && ReportProperties.ShowLedgerCode != 1)
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.None;
                                }
                                else
                                {
                                    cell.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }

                        }
                        else
                        {
                            cell.Borders = BorderSide.None;
                        }
                    }
                    cell.BorderColor = ((int)BorderStyleCell.Regular == this.ReportProperties.ReportBorderStyle) ? System.Drawing.Color.Gainsboro : System.Drawing.Color.Black;
                }
                cellcount = 0;
            }
            return table;
        }

        private XRTable AlignCCCategoryTable(XRTable table)
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
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
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
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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

        private XRTable AlignCostCentreTable(XRTable table)
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
                        else
                        {
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else if (ReportProperties.ShowHorizontalLine == 1)
                    {
                        if (count == 1)
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
                            tcell.Borders = BorderSide.Right | BorderSide.Bottom;
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

        private void SetReportBorder()
        {
            xrtblHeaderCaption = AlignHeaderTable(xrtblHeaderCaption);
            xrtblSource = AlignContentTable(xrtblSource);
            xrtblCCCategoryName = AlignCCCategoryTable(xrtblCCCategoryName);
            xrtblCCName = AlignCostCentreTable(xrtblCCName);
            xrtblGrandTotal = AlignGrandTotalTable(xrtblGrandTotal);

            //On 16/09/2024, To set curency symbol based on cash/bank selection
            if (this.settingProperty.AllowMultiCurrency == 1)
            {
                string cashbankcurrencysymbol = ReportProperties.GetCashBankLedgerCurrencySymbol(ReportProperties.CashBankLedger);
                if (!string.IsNullOrEmpty(cashbankcurrencysymbol))
                {
                    xrCosCapReceipts.Text = ReceiptCaption + " (" + cashbankcurrencysymbol + ")";
                    xrCosCapPayments.Text = PaymentCaption + " (" + cashbankcurrencysymbol + ")";
                }
            }
            else
            {
                this.SetCurrencyFormat(xrCosCapReceipts.Text, xrCosCapReceipts);
                this.SetCurrencyFormat(xrCosCapPayments.Text, xrCosCapPayments);
            }
        }
        #endregion

        #region Events

        private void xrCosReceipt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashReceipts = this.ReportProperties.NumberSet.ToDouble(xrCosReceipt.Text);
            if (CashReceipts != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosReceipt.Text = "";
            }
        }

        private void xrCosPayments_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double CashPayments = this.ReportProperties.NumberSet.ToDouble(xrCosPayments.Text);
            if (CashPayments != 0)
            {
                e.Cancel = false;
            }
            else
            {
                xrCosPayments.Text = "";
            }
        }

        private void xrtblSource_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            string Narration = (GetCurrentColumnValue("NARRATION") == null) ? string.Empty : GetCurrentColumnValue("NARRATION").ToString();
            xrtblSource = AlignCashBankBookTable(xrtblSource, Narration, string.Empty, count);
        }

        #endregion

        private void xrcellOPReceipt_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            string op = string.Empty;
            if (GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                double openingbalance = this.getCCOpeningBalance(false, false);
                openingbalance = openingbalance * -1;// We show opening in Receipt side and this is costentre (expense unit)
                if (this.AppSetting.EnableCCModeReports == 1)
                    op = (openingbalance < 0 ? "-" : "") + UtilityMember.NumberSet.ToNumber(Math.Abs(openingbalance));
                else
                    op = UtilityMember.NumberSet.ToNumber(Math.Abs(openingbalance)) + " " + (openingbalance < 0 ? TransMode.DR.ToString() : TransMode.CR.ToString());
                e.Value = op; //(openingbalance < 0 ? Math.Abs(openingbalance) : openingbalance);
            }
        }

        private void xrcellCLPayment_EvaluateBinding(object sender, BindingEventArgs e)
        {
            e.Value = 0;
            if (GetCurrentColumnValue(reportSetting1.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName) != null)
            {
                double ccclbalance = this.getCCClosingBalance(false, false);
                string cl = string.Empty;
                if (this.AppSetting.EnableCCModeReports == 1)
                    cl = (ccclbalance < 0 ? "-" : "") + UtilityMember.NumberSet.ToNumber(Math.Abs(ccclbalance));
                else
                    cl = UtilityMember.NumberSet.ToNumber(Math.Abs(ccclbalance)) + " " + (ccclbalance < 0 ? TransMode.DR.ToString() : TransMode.CR.ToString());

                e.Value = cl; //(openingbalance < 0 ? Math.Abs(openingbalance) : openingbalance);
                //e.Value = ccclbalance; //(ccclbalance < 0 ? Math.Abs(ccclbalance) : 0);
            }
        }

        private void xrcellTotalReceipt_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && ReportProperty.Current.ShowByCostCentre == 1)
            {
                e.Result = Math.Abs(getCCTotalAmount(false, true, false));

                e.Handled = true;
            }
        }

        private void xrcellTotalPayment_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && ReportProperty.Current.ShowByCostCentre == 1)
            {
                e.Result = Math.Abs(getCCTotalAmount(false, false, false));
                e.Handled = true;
            }
        }

        private void xtblTotalReceipts_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && ReportProperty.Current.ShowByCostCentre == 1)
            {
                e.Result = Math.Abs(getCCTotalAmount(false, true, true));
                e.Handled = true;
            }
        }

        private void xrtblTotalPayments_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (this.AppSetting.ShowCCOpeningBalanceInReports == 1 && ReportProperty.Current.ShowByCostCentre == 1)
            {
                e.Result = Math.Abs(getCCTotalAmount(false, false, true));
                e.Handled = true;
            }
        }

        private void xrcellOPReceipt_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

        private void xrcellCLPayment_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (this.AppSetting.EnableCCModeReports == 1)
            {
                XRTableCell cell = (XRTableCell)sender;
                double Cellvalue = UtilityMember.NumberSet.ToDouble(cell.Text);
                if (Cellvalue < 0)
                {
                    cell.ForeColor = Color.Red;
                }
                else
                {
                    cell.ForeColor = Color.Black;
                }
            }
        }

    }
}
